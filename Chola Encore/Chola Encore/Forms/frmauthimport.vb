Imports System.IO
Public Class frmauthimport
    Dim lival, lidup, litotal As Integer
    Dim lsErrorLogPath As String = Application.StartupPath & "\errorlog.txt"
    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            If txtFileName.Text.Trim = "" Then
                MessageBox.Show("File path should not be empty", gProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtFileName.Focus()
                Exit Sub
            End If

            If cboSheetName.Text.Trim = "" Then
                MessageBox.Show("Sheet name should not be empty ", gProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                cboSheetName.Focus()
                Exit Sub
            End If

            lival = 0
            lidup = 0
            litotal = 0
            pnlWrapper.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            lblTotal.Visible = True

            If rdoOld.Checked = True Then
                Importexcel(txtFileName.Text, cboSheetName.Text)
            Else
                ImportExcelNew(txtFileName.Text, cboSheetName.Text)
            End If

            MessageBox.Show("Total Records:" & litotal & " ;Valid Records:" & lival & " ;Duplicate Record:" & lidup & vbCrLf & "Please review the Error Log in the path " & lsErrorLogPath, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            pnlWrapper.Enabled = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pnlWrapper.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function DateDifference(ByVal ImportDate As DateTime) As Integer
        Dim lsdate As String
        Dim liday As Integer
        lsdate = gfExecuteScalar("select sysdate()", gOdbcConn)
        liday = DateDiff(DateInterval.Day, CDate(lsdate), ImportDate)
        Return liday
    End Function
    Private Sub LoadSheet()
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objBook As Microsoft.Office.Interop.Excel.Workbook

        If Trim(txtFileName.Text) <> "" Then
            If File.Exists(txtFileName.Text) Then
                objBook = objXls.Workbooks.Open(txtFileName.Text)
                cboSheetName.Items.Clear()
                For i As Integer = 1 To objXls.ActiveWorkbook.Worksheets.Count
                    cboSheetName.Items.Add(objXls.ActiveWorkbook.Worksheets(i).Name)
                Next i
                objXls.Workbooks.Close()
            End If
        End If
        objXls.Quit()
        ' KillProcess(objXls.Hwnd)
    End Sub

    Private Sub ImportExcel(ByVal FilePath As String, ByVal SheetName As String)
        Dim lsgnsarefno As String = ""
        Dim lsref1 As String = ""
        Dim lExcelDatatable As New DataTable
        Dim isnew As Boolean = False
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileGid, lsFileMstGid As String
        Dim lsTmpFileMstGid As String

        Dim lsDuplicatedtl As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName(), fsResult As String
        Dim lsFldNmesInfo As String = ""
        Dim lschqcnt As String
        Dim lsagreementgid As String
        Dim dtpacket As DataTable
        Dim lnPacketGid As Long
        Dim lnInwardGid As Long

        lsFldNmesInfo = "CLENT_CODE|CONTRACT_NUMBER|PARENT_CONTRACT_NUMBER|DRAWEE_NAME|CHEQUESNO|CHEQUEDATE|CYCLE_DATE|CONTRACT_AMOUNT|CHEQUEAMOUNT|MICR_CODE|BANK_NAME|BRANCH_NAME|PAYABLE_LOCATION|PICKUP|NEW_TYPE|REPAYMENT_MODE|BRANCHDESC|BANK_ACCOUNTNO|Auth Date"

        lsFieldName = Split(lsFldNmesInfo, "|")

        'Open Error Log File
        If File.Exists(lsErrorLogPath) Then
            FileOpen(1, lsErrorLogPath, OpenMode.Output)
        Else
            File.Create(lsErrorLogPath)
        End If

        Try

            lsFileName = Path.GetFileName(FilePath).ToUpper

            'Retrive Datas From Excel Sheet
            lExcelDatatable = gpExcelDataset("SELECT * FROM [" & lsSheetName & "$]", FilePath)

            'Checking Column Header
            For liIndex As Integer = 0 To lExcelDatatable.Columns.Count - 1
                If (lsFieldName(liIndex).Trim.ToUpper <> lExcelDatatable.Columns(liIndex).ColumnName.ToString.ToUpper) Then
                    MessageBox.Show("Invalid File Header, Correct Header is " & lsFldNmesInfo, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Next

            lsFileName = Mid(lsFileName, 1, 45)
            SheetName = Mid(SheetName, 1, 36)

            lssql = " select file_gid from chola_mst_tfile where file_name='" & lsFileName & "' and file_sheetname='" & SheetName & "'"

            If Val(gfExecuteScalar(lssql, gOdbcConn)) <> 0 Then
                MsgBox("File Name Already Imported", MsgBoxStyle.Information)
                Me.Cursor = Cursors.Default
                btnImport.Enabled = True
                lblTotal.Visible = False
                FileClose(1)
                Exit Sub
            End If

            Try
                lssql = " insert into chola_mst_tfile(file_name,file_sheetname,import_by,import_on)"
                lssql &= " values"
                lssql &= "("
                lssql &= "'" & lsFileName & "',"
                lssql &= "'" & SheetName & "',"
                lssql &= "'" & gUserName & "',"
                lssql &= "'" & Format(CDate(Now()), "yyyy-MM-dd") & "'"
                lssql &= ")"

                lssql = gfInsertQry(lssql, gOdbcConn)
                If lssql = "" Then
                    MsgBox("Error occured in insertion", MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    Exit Sub
                End If

            Catch ex As Exception
                MsgBox("Error occured in insertion", MsgBoxStyle.Information, gProjectName)
                Application.UseWaitCursor = False
                Exit Sub
            End Try

            lssql = " select file_gid from chola_mst_tfile where file_name='" & lsFileName & "' and file_sheetname='" & SheetName & "'"

            lsFileMstGid = Val(gfExecuteScalar(lssql, gOdbcConn)).ToString

            'Insert Record to the Table
            For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                If lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim = "" Then
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim = "" Then
                    PrintLine(1, "Contract Number Should not blank-" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim = "" Then
                    PrintLine(1, "CHEQUESNO Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim = "" Then
                    PrintLine(1, "Cheque Date Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim)
                    GoTo GoNext1
                ElseIf Not IsDate(lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim) Then
                    PrintLine(1, "Cheque Date not valid-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CHEQUEAMOUNT").ToString.Trim = "" Then
                    PrintLine(1, "CHEQUEAMOUNT Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("REPAYMENT_MODE").ToString.Trim = "" Then
                    PrintLine(1, "REPAYMENT_MODE Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("NEW_TYPE").ToString.Trim = "" Then
                    PrintLine(1, "NEW_TYPE Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If


                'lsgnsarefno = Format(dtpImportdate.Value, "yyMMdd") & "00000"
                'lsgnsarefno = Val(lsgnsarefno) + Val(lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim())
                'lsgnsarefno = "P" & lsgnsarefno

                'Inward Gid
                lssql = ""
                lssql = " select inward_gid,inward_packet_gid,packet_gnsarefno "
                lssql &= " from chola_trn_tinward "
                lssql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lssql &= " where inward_agreementno='" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & "'"
                lssql &= " and inward_userauthdate='" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "'"

                dtpacket = GetDataTable(lssql)

                If dtpacket.Rows.Count > 0 Then
                    lsgnsarefno = dtpacket.Rows(0).Item("packet_gnsarefno").ToString
                    lnPacketGid = Val(dtpacket.Rows(0).Item("inward_packet_gid").ToString)
                    lnInwardGid = Val(dtpacket.Rows(0).Item("inward_gid").ToString)
                Else
                    lsgnsarefno = ""
                    lnPacketGid = 0
                    lnInwardGid = 0
                End If

                If lsref1 = lsgnsarefno Then
                    isnew = False
                Else
                    isnew = True
                End If

                lsref1 = lsgnsarefno

                ''Check for Duplicate
                'lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                '            " pdc_gnsarefno='" & lsgnsarefno & "'" & _
                '             " and pdc_parentcontractno='" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & "'" & _
                '            " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim & "'" & _
                '            " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                'lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                ''Write Dulicate Record in Error log
                'If Val(lsFileGid) > 0 Then
                '    lidup += 1
                '    liIsDuplicate += 1
                '    lsDuplicatedtl = Now() & " Duplicate exist in GNSA REF no" & lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim
                '    PrintLine(1, lsDuplicatedtl)
                '    GoTo GoNext1
                'End If

                'Check for Duplicate
                lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                            " pdc_parentcontractno='" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & "'" & _
                            " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim & "'" & _
                            " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & " Duplicate exist " & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext1
                End If

                'Check for Duplicate
                lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                             " pdc_shortpdc_parentcontractno='" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim), 7)), "000000") & "'" & _
                            " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim & "'" & _
                            " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & " Duplicate exist " & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim) & " " & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext1
                End If


                lssql = " select count(*) from chola_trn_tpdcfile where "
                lssql &= " pdc_shortpdc_parentcontractno='" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim), 7)), "000000") & "'"
                lssql &= " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                lschqcnt = gfExecuteScalar(lssql, gOdbcConn)
                lschqcnt = Val(lschqcnt) + 1

                lssql = " select agreement_gid"
                lssql &= " from chola_mst_tagreement"
                lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & "'"

                lsagreementgid = gfExecuteScalar(lssql, gOdbcConn)

                If Val(lsagreementgid) = 0 Then
                    lssql = " insert into chola_mst_tagreement"
                    lssql &= "(agreement_no,shortagreement_no) values ("
                    lssql &= "'" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim, 7)), "000000") & "')"
                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " select agreement_gid"
                    lssql &= " from chola_mst_tagreement"
                    lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & "'"

                    lsagreementgid = gfExecuteScalar(lssql, gOdbcConn)
                End If

                'lssql = " select head_gid from chola_trn_tpdcfilehead "
                'lssql &= " where head_file_gid=" & lsFileMstGid
                'lssql &= " and head_packet_gid=" & lnPacketGid
                'lssql &= " and head_agreement_gid= " & lsagreementgid

                lssql = " select head_file_gid from chola_trn_tpdcfilehead "
                lssql &= " where true "
                lssql &= " and head_agreement_gid= " & lsagreementgid
                lssql &= " and head_systemauthdate = '" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "' "

                lsTmpFileMstGid = gfExecuteScalar(lssql, gOdbcConn)

                If lsTmpFileMstGid = "" Then
                    lssql = ""
                    lssql &= " insert into chola_trn_tpdcfilehead ("
                    lssql &= " head_file_gid,head_inward_gid,head_packet_gid,head_agreement_gid,head_agreementno,"
                    lssql &= " head_shortagreementno,head_systemauthdate,head_mode) "
                    lssql &= " values ( "
                    lssql &= "" & lsFileMstGid & ","
                    lssql &= "" & lnInwardGid & ","
                    lssql &= "" & lnPacketGid & ","
                    lssql &= "" & lsagreementgid & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT_CONTRACT_NUMBER").ToString) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT_CONTRACT_NUMBER").ToString), 7)), "000000") & "',"
                    lssql &= "'" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("REPAYMENT_MODE").ToString) & "')"

                    gfInsertQry(lssql, gOdbcConn)

                    If lnPacketGid > 0 Then
                        lssql = ""
                        lssql &= " update chola_trn_tpacket set "
                        lssql &= " packet_status = packet_status | " & GCPKTAUTHFINONE & " "
                        lssql &= " where packet_gid = " & lnPacketGid & " "

                        Call gfInsertQry(lssql, gOdbcConn)
                    End If

                    'LogTransaction(lsgnsarefno, 1, gUserName)
                End If

                Try
                    Dim lsdate As Date
                    If IsDate(lExcelDatatable.Rows(liIndex)("CHEQUEDATE").ToString) Then
                        lsdate = Format(CDate(lExcelDatatable.Rows(liIndex)("CHEQUEDATE").ToString), "dd-MM-yyyy")
                    End If

                    lssql = " Insert into chola_trn_tpdcfile (file_mst_gid,pdc_gnsarefno,"
                    lssql &= "pdc_clientcode,pdc_contractno,pdc_parentcontractno,pdc_draweename,pdc_chqno,pdc_chqdate,pdc_cycle,"
                    lssql &= "pdc_contractamount,pdc_chqamount,pdc_micrcode,pdc_bankname,pdc_bankbranch,pdc_payablelocation,pdc_pickuplocation,"
                    lssql &= "pdc_mode,pdc_type,pdc_branchname,pdc_shortpdc_parentcontractno,pdc_filetype,atpar_flag,chq_rec_slno,pdc_importdate,pdc_acc_no) values ("
                    lssql &= IIf(lsTmpFileMstGid = "", lsFileMstGid, lsTmpFileMstGid) & ","
                    lssql &= "'',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("CLENT_CODE").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("CONTRACT_NUMBER").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT_CONTRACT_NUMBER").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("DRAWEE_NAME").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("CHEQUESNO").ToString) & "',"
                    lssql &= "'" & Format(lsdate, "yyyy-MM-dd") & "',"
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("CYCLE_DATE").ToString)) & ","
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("CONTRACT_AMOUNT").ToString)) & ","
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("CHEQUEAMOUNT").ToString)) & ","
                    lssql &= "'" & Replace(QuoteFilter(lExcelDatatable.Rows(liIndex)("MICR_CODE").ToString), Convert.ToChar(39), "") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("BANK_NAME").ToString) & "',"
                    lssql &= "'" & Mid(QuoteFilter(lExcelDatatable.Rows(liIndex)("BRANCH_NAME").ToString), 1, 45) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PAYABLE_LOCATION").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PICKUP").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("REPAYMENT_MODE").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("NEW_TYPE").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("BRANCHDESC").ToString) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT_CONTRACT_NUMBER").ToString), 7)), "000000") & "',"
                    lssql &= "'PDC',"

                    If QuoteFilter(lExcelDatatable.Rows(liIndex)("PAYABLE_LOCATION").ToString.ToUpper.Trim) = "PAYABLE" Then
                        lssql &= "'Y',"
                    Else
                        lssql &= "'N',"
                    End If

                    lssql &= lschqcnt & ","
                    lssql &= "'" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "',"

                    If IsNumeric(lExcelDatatable.Rows(liIndex)("BANK_ACCOUNTNO").ToString) = True Then
                        lssql &= "'" & Mid(Format(Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("BANK_ACCOUNTNO").ToString.ToUpper.Trim)), "0"), 1, 32) & "')"
                    Else
                        lssql &= "'" & Mid(QuoteFilter(lExcelDatatable.Rows(liIndex)("BANK_ACCOUNTNO").ToString.ToUpper.Trim), 1, 32).Replace("'", "") & "')"
                    End If

                    fsResult = gfInsertQry(lssql, gOdbcConn)
                    If Val(fsResult) = 0 Then
                        MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    lival += 1
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    Exit Sub
                End Try
GoNext1:
            Next

            FileClose(1)

            If liIsDuplicate > 0 Then
                System.Diagnostics.Process.Start(lsErrorLogPath)
            End If

        Catch ex As Exception
            FileClose(1)
            MessageBox.Show(ex.Message, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        OpenFileDialog.Filter = "Excel Files|*.xls"
        OpenFileDialog.ShowDialog()

        If OpenFileDialog.FileName.Length <> 0 Then
            txtFileName.Text = OpenFileDialog.FileName
        End If
        Call LoadSheet()
        Exit Sub
    End Sub

    Private Sub frmmstfile_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmmstfile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTotal.Visible = False
        Me.KeyPreview = True
        txtFileName.Focus()
        txtFileName.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub ImportExcelNew(ByVal FilePath As String, ByVal SheetName As String)
        Dim lsgnsarefno As String = ""
        Dim lsref1 As String = ""
        Dim lExcelDatatable As New DataTable
        Dim isnew As Boolean = False
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileGid, lsFileMstGid As String
        Dim lsTmpFileMstGid As String

        Dim lsDuplicatedtl As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName(), fsResult As String
        Dim lsFldNmesInfo As String = ""
        Dim lschqcnt As String
        Dim lsagreementgid As String
        Dim dtpacket As DataTable
        Dim lnPacketGid As Long
        Dim lnInwardGid As Long

        lsFldNmesInfo = "CLENT CODE|CONTRACT NUMBER|PARENT CONTRACT NUMBER|DRAWEE NAME|CHEQUESNO|CHEQUEDATE|CYCLE DATE|CONTRACT AMOUNT|CHEQUEAMOUNT|MICR CODE|BANK NAME|BRANCH NAME|PAYABLE LOCATION|PICKUP|NEW TYPE|REPAYMENT MODE|BRANCHDESC|BANK ACCOUNTNO|Auth Date"

        lsFieldName = Split(lsFldNmesInfo, "|")

        'Open Error Log File
        If File.Exists(lsErrorLogPath) Then
            FileOpen(1, lsErrorLogPath, OpenMode.Output)
        Else
            File.Create(lsErrorLogPath)
        End If

        Try

            lsFileName = Path.GetFileName(FilePath).ToUpper

            'Retrive Datas From Excel Sheet
            lExcelDatatable = gpExcelDataset("SELECT * FROM [" & lsSheetName & "$]", FilePath)

            'Checking Column Header
            For liIndex As Integer = 0 To lExcelDatatable.Columns.Count - 1
                If (lsFieldName(liIndex).Trim.ToUpper <> lExcelDatatable.Columns(liIndex).ColumnName.ToString.ToUpper) Then
                    MessageBox.Show("Invalid File Header, Correct Header is " & lsFldNmesInfo, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Next

            lsFileName = Mid(lsFileName, 1, 45)
            SheetName = Mid(SheetName, 1, 36)

            lssql = " select file_gid from chola_mst_tfile where file_name='" & lsFileName & "' and file_sheetname='" & SheetName & "'"

            If Val(gfExecuteScalar(lssql, gOdbcConn)) <> 0 Then
                MsgBox("File Name Already Imported", MsgBoxStyle.Information)
                Me.Cursor = Cursors.Default
                btnImport.Enabled = True
                lblTotal.Visible = False
                FileClose(1)
                Exit Sub
            End If

            Try
                lssql = " insert into chola_mst_tfile(file_name,file_sheetname,import_by,import_on)"
                lssql &= " values"
                lssql &= "("
                lssql &= "'" & lsFileName & "',"
                lssql &= "'" & SheetName & "',"
                lssql &= "'" & gUserName & "',"
                lssql &= "'" & Format(CDate(Now()), "yyyy-MM-dd") & "'"
                lssql &= ")"

                lssql = gfInsertQry(lssql, gOdbcConn)
                If lssql = "" Then
                    MsgBox("Error occured in insertion", MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    Exit Sub
                End If

            Catch ex As Exception
                MsgBox("Error occured in insertion", MsgBoxStyle.Information, gProjectName)
                Application.UseWaitCursor = False
                Exit Sub
            End Try

            lssql = " select file_gid from chola_mst_tfile where file_name='" & lsFileName & "' and file_sheetname='" & SheetName & "'"

            lsFileMstGid = Val(gfExecuteScalar(lssql, gOdbcConn)).ToString

            'Insert Record to the Table
            For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                If lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim = "" Then
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim = "" Then
                    PrintLine(1, "Contract Number Should not blank-" & lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim = "" Then
                    PrintLine(1, "CHEQUESNO Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim = "" Then
                    PrintLine(1, "Cheque Date Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim)
                    GoTo GoNext1
                ElseIf Not IsDate(lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim) Then
                    PrintLine(1, "Cheque Date not valid-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("CHEQUEAMOUNT").ToString.Trim = "" Then
                    PrintLine(1, "CHEQUEAMOUNT Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("REPAYMENT MODE").ToString.Trim = "" Then
                    PrintLine(1, "REPAYMENT MODE Should not blank-" & lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim)
                    GoTo GoNext1
                End If

                If lExcelDatatable.Rows(liIndex).Item("NEW TYPE").ToString.Trim = "" Then
                    PrintLine(1, "NEW TYPE Should not blank-" & lExcelDatatable.Rows(liIndex).Item("NEW TYPE").ToString.Trim)
                    GoTo GoNext1
                End If


                'lsgnsarefno = Format(dtpImportdate.Value, "yyMMdd") & "00000"
                'lsgnsarefno = Val(lsgnsarefno) + Val(lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim())
                'lsgnsarefno = "P" & lsgnsarefno

                'Inward Gid
                lssql = ""
                lssql = " select inward_gid,inward_packet_gid,packet_gnsarefno "
                lssql &= " from chola_trn_tinward "
                lssql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lssql &= " where inward_agreementno='" & lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim & "'"
                lssql &= " and inward_userauthdate='" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "'"

                dtpacket = GetDataTable(lssql)

                If dtpacket.Rows.Count > 0 Then
                    lsgnsarefno = dtpacket.Rows(0).Item("packet_gnsarefno").ToString
                    lnPacketGid = Val(dtpacket.Rows(0).Item("inward_packet_gid").ToString)
                    lnInwardGid = Val(dtpacket.Rows(0).Item("inward_gid").ToString)
                Else
                    lsgnsarefno = ""
                    lnPacketGid = 0
                    lnInwardGid = 0
                End If

                If lsref1 = lsgnsarefno Then
                    isnew = False
                Else
                    isnew = True
                End If

                lsref1 = lsgnsarefno

                ''Check for Duplicate
                'lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                '            " pdc_gnsarefno='" & lsgnsarefno & "'" & _
                '             " and pdc_parentcontractno='" & lExcelDatatable.Rows(liIndex).Item("PARENT_CONTRACT_NUMBER").ToString.Trim & "'" & _
                '            " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim & "'" & _
                '            " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                'lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                ''Write Dulicate Record in Error log
                'If Val(lsFileGid) > 0 Then
                '    lidup += 1
                '    liIsDuplicate += 1
                '    lsDuplicatedtl = Now() & " Duplicate exist in GNSA REF no" & lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim
                '    PrintLine(1, lsDuplicatedtl)
                '    GoTo GoNext1
                'End If

                'Check for Duplicate
                lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                            " pdc_parentcontractno='" & lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim & "'" & _
                            " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim & "'" & _
                            " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & " Duplicate exist " & lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext1
                End If

                'Check for Duplicate
                lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                             " pdc_shortpdc_parentcontractno='" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim), 7)), "000000") & "'" & _
                            " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("CONTRACT NUMBER").ToString.Trim & "'" & _
                            " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & " Duplicate exist " & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("CONTRACT_NUMBER").ToString.Trim) & " " & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext1
                End If


                lssql = " select count(*) from chola_trn_tpdcfile where "
                lssql &= " pdc_shortpdc_parentcontractno='" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim), 7)), "000000") & "'"
                lssql &= " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'"

                lschqcnt = gfExecuteScalar(lssql, gOdbcConn)
                lschqcnt = Val(lschqcnt) + 1

                lssql = " select agreement_gid"
                lssql &= " from chola_mst_tagreement"
                lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim & "'"

                lsagreementgid = gfExecuteScalar(lssql, gOdbcConn)

                If Val(lsagreementgid) = 0 Then
                    lssql = " insert into chola_mst_tagreement"
                    lssql &= "(agreement_no,shortagreement_no) values ("
                    lssql &= "'" & lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim, 7)), "000000") & "')"
                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " select agreement_gid"
                    lssql &= " from chola_mst_tagreement"
                    lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("PARENT CONTRACT NUMBER").ToString.Trim & "'"

                    lsagreementgid = gfExecuteScalar(lssql, gOdbcConn)
                End If

                'lssql = " select head_gid from chola_trn_tpdcfilehead "
                'lssql &= " where head_file_gid=" & lsFileMstGid
                'lssql &= " and head_packet_gid=" & lnPacketGid
                'lssql &= " and head_agreement_gid= " & lsagreementgid

                lssql = " select head_file_gid from chola_trn_tpdcfilehead "
                lssql &= " where true "
                lssql &= " and head_agreement_gid= " & lsagreementgid
                lssql &= " and head_systemauthdate = '" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "' "

                lsTmpFileMstGid = gfExecuteScalar(lssql, gOdbcConn)

                If lsTmpFileMstGid = "" Then
                    lssql = ""
                    lssql &= " insert into chola_trn_tpdcfilehead ("
                    lssql &= " head_file_gid,head_inward_gid,head_packet_gid,head_agreement_gid,head_agreementno,"
                    lssql &= " head_shortagreementno,head_systemauthdate,head_mode) "
                    lssql &= " values ( "
                    lssql &= "" & lsFileMstGid & ","
                    lssql &= "" & lnInwardGid & ","
                    lssql &= "" & lnPacketGid & ","
                    lssql &= "" & lsagreementgid & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT CONTRACT NUMBER").ToString) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT CONTRACT NUMBER").ToString), 7)), "000000") & "',"
                    lssql &= "'" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("REPAYMENT MODE").ToString) & "')"

                    gfInsertQry(lssql, gOdbcConn)

                    If lnPacketGid > 0 Then
                        lssql = ""
                        lssql &= " update chola_trn_tpacket set "
                        lssql &= " packet_status = packet_status | " & GCPKTAUTHFINONE & " "
                        lssql &= " where packet_gid = " & lnPacketGid & " "

                        Call gfInsertQry(lssql, gOdbcConn)
                    End If

                    'LogTransaction(lsgnsarefno, 1, gUserName)
                End If

                Try
                    Dim lsdate As Date
                    If IsDate(lExcelDatatable.Rows(liIndex)("CHEQUEDATE").ToString) Then
                        lsdate = Format(CDate(lExcelDatatable.Rows(liIndex)("CHEQUEDATE").ToString), "dd-MM-yyyy")
                    End If

                    lssql = " Insert into chola_trn_tpdcfile (file_mst_gid,pdc_gnsarefno,"
                    lssql &= "pdc_clientcode,pdc_contractno,pdc_parentcontractno,pdc_draweename,pdc_chqno,pdc_chqdate,pdc_cycle,"
                    lssql &= "pdc_contractamount,pdc_chqamount,pdc_micrcode,pdc_bankname,pdc_bankbranch,pdc_payablelocation,pdc_pickuplocation,"
                    lssql &= "pdc_mode,pdc_type,pdc_branchname,pdc_shortpdc_parentcontractno,pdc_filetype,atpar_flag,chq_rec_slno,pdc_importdate,pdc_acc_no) values ("
                    lssql &= IIf(lsTmpFileMstGid = "", lsFileMstGid, lsTmpFileMstGid) & ","
                    lssql &= "'',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("CLENT CODE").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("CONTRACT NUMBER").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT CONTRACT NUMBER").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("DRAWEE NAME").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("CHEQUESNO").ToString) & "',"
                    lssql &= "'" & Format(lsdate, "yyyy-MM-dd") & "',"
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("CYCLE DATE").ToString)) & ","
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("CONTRACT AMOUNT").ToString)) & ","
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("CHEQUEAMOUNT").ToString)) & ","
                    lssql &= "'" & Replace(QuoteFilter(lExcelDatatable.Rows(liIndex)("MICR CODE").ToString), Convert.ToChar(39), "") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("BANK NAME").ToString) & "',"
                    lssql &= "'" & Mid(QuoteFilter(lExcelDatatable.Rows(liIndex)("BRANCH NAME").ToString), 1, 45) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PAYABLE LOCATION").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("PICKUP").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("REPAYMENT MODE").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("NEW TYPE").ToString) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("BRANCHDESC").ToString) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("PARENT CONTRACT NUMBER").ToString), 7)), "000000") & "',"
                    lssql &= "'PDC',"

                    If QuoteFilter(lExcelDatatable.Rows(liIndex)("PAYABLE LOCATION").ToString.ToUpper.Trim) = "PAYABLE" Then
                        lssql &= "'Y',"
                    Else
                        lssql &= "'N',"
                    End If

                    lssql &= lschqcnt & ","
                    lssql &= "'" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("Auth Date").ToString), "yyyy-MM-dd") & "',"

                    If IsNumeric(lExcelDatatable.Rows(liIndex)("BANK ACCOUNTNO").ToString) = True Then
                        lssql &= "'" & Mid(Format(Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("BANK ACCOUNTNO").ToString.ToUpper.Trim)), "0"), 1, 32) & "')"
                    Else
                        lssql &= "'" & Mid(QuoteFilter(lExcelDatatable.Rows(liIndex)("BANK ACCOUNTNO").ToString.ToUpper.Trim), 1, 32).Replace("'", "") & "')"
                    End If

                    fsResult = gfInsertQry(lssql, gOdbcConn)
                    If Val(fsResult) = 0 Then
                        MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    lival += 1
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    Exit Sub
                End Try
GoNext1:
            Next

            FileClose(1)

            If liIsDuplicate > 0 Then
                System.Diagnostics.Process.Start(lsErrorLogPath)
            End If

        Catch ex As Exception
            FileClose(1)
            MessageBox.Show(ex.Message, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class