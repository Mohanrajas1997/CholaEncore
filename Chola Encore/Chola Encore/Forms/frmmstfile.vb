Imports Microsoft.VisualBasic
Imports System.IO
Public Class frmmstfile
    Dim lival, lidup, litotal As Integer
    Dim lsErrorLogPath As String = Application.StartupPath & "\errorlog.txt"
    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim liday As Integer
        Try
            If dtpImportdate.Checked = False Then
                MessageBox.Show("Please select Import Date", gProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpImportdate.Focus()
                Exit Sub
            Else
                liday = DateDifference(dtpImportdate.Value)
                If liday > 0 Then
                    MessageBox.Show("Please select Past Date", gProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    dtpImportdate.Focus()
                    Exit Sub
                End If
            End If
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

            If cbofileformat.Text.Trim = "" Then
                MessageBox.Show("File Format should not be empty ", gProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                cboSheetName.Focus()
                Exit Sub
            End If

            lival = 0
            lidup = 0
            litotal = 0
            pnlWrapper.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            lblTotal.Visible = True
            Importexcel(txtFileName.Text, cboSheetName.Text, cbofileformat.Text)
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
    Private Sub Importexcel(ByVal FilePath As String, ByVal SheetName As String, ByVal FileFormat As String)
        Dim lsgnsarefno As String, lsref1 As String = ""
        Dim lExcelDatatable As New DataTable
        Dim isnew As Boolean = False
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileGid, lsFileMstGid As String
        Dim lsDuplicatedtl As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName(), fsResult As String
        Dim lsFldNmesInfo As String = ""
        Dim lschqcnt As String
        Dim lsagreementgid As String

        If FileFormat = "SPDC" Then
            lsFldNmesInfo = "GNSA Refno|Agreement Number|Repayment Mode|Finnone Records|SPDC count|ECS Mandate Count|Remarks / combined agr no|H/off date|Remarks"
        Else
            lsFldNmesInfo = "GNSA Refno|Client Code|Contract Number|Parent Contract Number|Drawee Name|Cheque No|Cheque Date|Cycle Date|Contract Amount|Cheque Amt|MICR Code|Bank Name|Branch|Payable Location|Pick-up Location|Mode|Type|Branch Name|"
        End If


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

            lsFileMstGid = gfExecuteScalar(lssql, gOdbcConn)

            If FileFormat = "SPDC" Then
                'Insert Record to the Table
                For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                    litotal = lExcelDatatable.Rows.Count
                    lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                    Application.DoEvents()

                    If lExcelDatatable.Rows(liIndex).Item("Agreement Number").ToString.Trim = "" Then
                        GoTo GoNext
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim() = "" Then
                        PrintLine(1, "GNSA Ref no Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Agreement Number").ToString.Trim)
                        GoTo GoNext
                    ElseIf lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim().Length > 5 Then
                        PrintLine(1, "Invalid GNSA Ref no" & lExcelDatatable.Rows(liIndex).Item("Agreement Number").ToString.Trim)
                        GoTo GoNext
                    End If


                    lsgnsarefno = Format(dtpImportdate.Value, "yyMMdd") & "00000"
                    lsgnsarefno = Val(lsgnsarefno) + Val(lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim())

                    'Check for Duplicate
                    lssql = " Select spdc_gid from chola_trn_tspdc where " & _
                                " spdc_gnsarefno='" & lsgnsarefno & "'"

                    lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                    'Write Dulicate Record in Error log
                    If Val(lsFileGid) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1
                        lsDuplicatedtl = Now() & " Duplicate exist in GNSA REF no" & lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext
                    End If

                    'Check for Duplicate
                    lssql = " Select spdc_gid from chola_trn_tspdc where " & _
                                " spdc_agreementno='" & lExcelDatatable.Rows(liIndex).Item("Agreement Number").ToString.Trim & "'"

                    lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                    'Write Dulicate Record in Error log
                    If Val(lsFileGid) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1
                        lsDuplicatedtl = Now() & " Duplicate exist " & lExcelDatatable.Rows(liIndex).Item("Agreement Number").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("Repayment Mode").ToString.Trim & " "
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext
                    End If

                    'Check for Duplicate
                    lssql = " Select spdc_gid from chola_trn_tspdc where " & _
                                " spdc_shortagreementno='" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement Number").ToString), 7)), "000000") & "'"

                    lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                    'Write Dulicate Record in Error log
                    If Val(lsFileGid) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1
                        lsDuplicatedtl = Now() & " Duplicate exist " & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement Number").ToString), 7)), "000000") & " " & lExcelDatatable.Rows(liIndex).Item("Repayment Mode").ToString.Trim & " "
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext
                    End If

                    Try
                        Dim lsdate As Date
                        If IsDate(lExcelDatatable.Rows(liIndex)("H/off date").ToString) Then
                            lsdate = Format(CDate(lExcelDatatable.Rows(liIndex)("H/off date").ToString), "dd-MM-yyyy")
                        End If

                        lssql = " Insert into chola_trn_tspdc (file_mst_gid,spdc_gnsarefno,spdc_agreementno,spdc_repaymentmode,spdc_dumpspdccnt,"
                        lssql &= "spdc_ecsmandatecnt,spdc_handsoffdate,spdc_dumpremarks,spdc_dumpremarks1,spdc_fineonerecords,spdc_shortagreementno,spdc_filetype,spdc_importdate) values ("
                        lssql &= lsFileMstGid & ","
                        lssql &= "'" & lsgnsarefno & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement Number").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Repayment Mode").ToString) & "',"
                        lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("SPDC count").ToString)) & ","
                        lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("ECS Mandate Count").ToString)) & ","
                        lssql &= "'" & Format(lsdate, "yyyy-MM-dd") & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Remarks / combined agr no").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Remarks").ToString) & "',"
                        lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("Finnone Records").ToString)) & ","
                        lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement Number").ToString), 7)), "000000") & "',"
                        lssql &= "'" & cbofileformat.Text.Trim & "',"
                        lssql &= "'" & Format(dtpImportdate.Value, "yyyy-MM-dd") & "')"

                        fsResult = gfInsertQry(lssql, gOdbcConn)
                        If Val(fsResult) = 0 Then
                            MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        lival += 1
                    Catch ex As Exception
                        MsgBox("Error occured in inserting", MsgBoxStyle.Information, gProjectName)
                        Application.UseWaitCursor = False
                        Exit Sub
                    End Try

                    LogTransaction(lsgnsarefno, 1, gUserName)
GoNext:
                Next
            Else
                'Insert Record to the Table
                For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                    litotal = lExcelDatatable.Rows.Count
                    lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                    Application.DoEvents()

                    If lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim = "" Then
                        GoTo GoNext1
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim = "" Then
                        PrintLine(1, "Contract Number Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim)
                        GoTo GoNext1
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("Cheque No").ToString.Trim = "" Then
                        PrintLine(1, "Cheque No Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim)
                        GoTo GoNext1
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("Cheque Date").ToString.Trim = "" Then
                        PrintLine(1, "Cheque Date Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim)
                        GoTo GoNext1
                    ElseIf Not IsDate(lExcelDatatable.Rows(liIndex).Item("Cheque Date").ToString.Trim) Then
                        PrintLine(1, "Cheque Date not valid-" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim)
                        GoTo GoNext1
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("Cheque Amt").ToString.Trim = "" Then
                        PrintLine(1, "Cheque Amt Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim)
                        GoTo GoNext1
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("Mode").ToString.Trim = "" Then
                        PrintLine(1, "Mode Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim)
                        GoTo GoNext1
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("Type").ToString.Trim = "" Then
                        PrintLine(1, "Type Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim)
                        GoTo GoNext1
                    End If

                    If lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim() = "" Then
                        PrintLine(1, "GNSA Ref no Should not blank-" & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim)
                        GoTo GoNext1
                    ElseIf lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim().Length > 5 Then
                        PrintLine(1, "Invalid GNSA Ref no" & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim)
                        GoTo GoNext1
                    End If


                    lsgnsarefno = Format(dtpImportdate.Value, "yyMMdd") & "00000"
                    lsgnsarefno = Val(lsgnsarefno) + Val(lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim())
                    lsgnsarefno = "P" & lsgnsarefno


                    If lsref1 = lsgnsarefno Then
                        isnew = False
                    Else
                        isnew = True
                    End If

                    lsref1 = lsgnsarefno

                    'Check for Duplicate
                    lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                                " pdc_gnsarefno='" & lsgnsarefno & "'" & _
                                 " and pdc_parentcontractno='" & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim & "'" & _
                                " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim & "'" & _
                                " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("Cheque No").ToString.Trim & "'"

                    lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                    'Write Dulicate Record in Error log
                    If Val(lsFileGid) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1
                        lsDuplicatedtl = Now() & " Duplicate exist in GNSA REF no" & lExcelDatatable.Rows(liIndex).Item("GNSA Refno").ToString.Trim
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext1
                    End If

                    'Check for Duplicate
                    lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                                " pdc_parentcontractno='" & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim & "'" & _
                                " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim & "'" & _
                                " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("Cheque No").ToString.Trim & "'"


                    lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                    'Write Dulicate Record in Error log
                    If Val(lsFileGid) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1
                        lsDuplicatedtl = Now() & " Duplicate exist " & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("Cheque No").ToString.Trim
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext1
                    End If

                    'Check for Duplicate
                    lssql = " Select pdc_gid from chola_trn_tpdcfile where " & _
                                 " pdc_shortpdc_parentcontractno='" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim), 7)), "000000") & "'" & _
                                " and pdc_contractno='" & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim & "'" & _
                                " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("Cheque No").ToString.Trim & "'"

                    lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                    'Write Dulicate Record in Error log
                    If Val(lsFileGid) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1
                        lsDuplicatedtl = Now() & " Duplicate exist " & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim), 7)), "000000") & " " & lExcelDatatable.Rows(liIndex).Item("Contract Number").ToString.Trim & " " & lExcelDatatable.Rows(liIndex).Item("Cheque No").ToString.Trim
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext1
                    End If


                    lssql = " select count(*) from chola_trn_tpdcfile where "
                    lssql &= " pdc_parentcontractno='" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim), 7)), "000000") & "'"
                    lssql &= " and pdc_chqno='" & lExcelDatatable.Rows(liIndex).Item("Cheque No").ToString.Trim & "'"

                    lschqcnt = gfExecuteScalar(lssql, gOdbcConn)
                    lschqcnt = Val(lschqcnt) + 1

                    lssql = " select agreement_gid"
                    lssql &= " from chola_mst_tagreement"
                    lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim & "'"

                    lsagreementgid = gfExecuteScalar(lssql, gOdbcConn)

                    If Val(lsagreementgid) = 0 Then
                        lssql = " insert into chola_mst_tagreement"
                        lssql &= "(agreement_no,shortagreement_no) values ("
                        lssql &= "'" & lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim & "',"
                        lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lExcelDatatable.Rows(liIndex).Item("Parent Contract Number").ToString.Trim, 7)), "000000") & "')"
                        gfInsertQry(lssql, gOdbcConn)
                    End If

                    Try
                        Dim lsdate As Date
                        If IsDate(lExcelDatatable.Rows(liIndex)("Cheque Date").ToString) Then
                            lsdate = Format(CDate(lExcelDatatable.Rows(liIndex)("Cheque Date").ToString), "dd-MM-yyyy")
                        End If

                        lssql = " Insert into chola_trn_tpdcfile (file_mst_gid,pdc_gnsarefno,"
                        lssql &= "pdc_clientcode,pdc_contractno,pdc_parentcontractno,pdc_draweename,pdc_chqno,pdc_chqdate,pdc_cycle,"
                        lssql &= "pdc_contractamount,pdc_chqamount,pdc_micrcode,pdc_bankname,pdc_bankbranch,pdc_payablelocation,pdc_pickuplocation,"
                        lssql &= "pdc_mode,pdc_type,pdc_branchname,pdc_shortpdc_parentcontractno,pdc_filetype,atpar_flag,chq_rec_slno,pdc_importdate) values ("
                        lssql &= lsFileMstGid & ","
                        lssql &= "'" & lsgnsarefno & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Client Code").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Contract Number").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Parent Contract Number").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Drawee Name").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Cheque No").ToString) & "',"
                        lssql &= "'" & Format(lsdate, "yyyy-MM-dd") & "',"
                        lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("Cycle Date").ToString)) & ","
                        lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("Contract Amount").ToString)) & ","
                        lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("Cheque Amt").ToString)) & ","
                        lssql &= "'" & Replace(QuoteFilter(lExcelDatatable.Rows(liIndex)("MICR Code").ToString), Convert.ToChar(39), "") & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Bank Name").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Branch").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Payable Location").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Pick-up Location").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Mode").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Type").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Branch Name").ToString) & "',"
                        lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(QuoteFilter(lExcelDatatable.Rows(liIndex)("Parent Contract Number").ToString), 7)), "000000") & "',"
                        lssql &= "'" & cbofileformat.Text.Trim & "',"

                        If QuoteFilter(lExcelDatatable.Rows(liIndex)("Payable Location").ToString.ToUpper.Trim) = "PAYABLE" Then
                            lssql &= "'Y',"
                        Else
                            lssql &= "'N',"
                        End If

                        lssql &= lschqcnt & ","
                        lssql &= "'" & Format(dtpImportdate.Value, "yyyy-MM-dd") & "')"

                        fsResult = gfInsertQry(lssql, gOdbcConn)
                        If Val(fsResult) = 0 Then
                            MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        lival += 1
                    Catch ex As Exception
                        MsgBox("Error occured in inserting", MsgBoxStyle.Information, gProjectName)
                        Application.UseWaitCursor = False
                        Exit Sub
                    End Try

                    If isnew Then
                        LogTransaction(lsgnsarefno, 1, gUserName)
                    End If
GoNext1:
                Next
            End If
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
End Class