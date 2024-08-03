Imports System.IO
Public Class frmfinoneimport
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
            Importexcel(txtFileName.Text, cboSheetName.Text)
            MessageBox.Show("Total Records:" & litotal & " ;Valid Records:" & lival & " ;Duplicate Record:" & lidup & vbCrLf & "Please review the Error Log in the path " & lsErrorLogPath, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            pnlWrapper.Enabled = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            pnlWrapper.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub
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
    Private Sub Importexcel(ByVal FilePath As String, ByVal SheetName As String)
        Dim lExcelDatatable As New DataTable
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileGid, lsFileMstGid As String
        Dim lsDuplicatedtl As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName(), fsResult As String
        Dim lsFldNmesInfo As String = ""

        lsFldNmesInfo = "CLENT_CODE|	BATCHID|	AGREEMENTNO|	BRANCH|	PRODUCTFLAG|	INST_DATE|	INST_NO|	AMOUNT|	BANK_DETAILS|	CLR_LOCATION|	CUST_BANK_ACCTNO|	BANKDESC|	PRODUCT|"



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

            ''Checking Column Header
            'For liIndex As Integer = 0 To lExcelDatatable.Columns.Count - 1
            '    If (lsFieldName(liIndex).Trim.ToUpper <> lExcelDatatable.Columns(liIndex).ColumnName.ToString.ToUpper) Then
            '        MessageBox.Show("Invalid File Header, Correct Header is " & lsFldNmesInfo, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        FileClose(1)
            '        Exit Sub
            '    End If
            'Next

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
                    FileClose(1)
                    Exit Sub
                End If

            Catch ex As Exception
                MsgBox("Error occured in insertion", MsgBoxStyle.Information, gProjectName)
                Application.UseWaitCursor = False
                FileClose(1)
                Exit Sub
            End Try

            lssql = " select file_gid from chola_mst_tfile where file_name='" & lsFileName & "' and file_sheetname='" & SheetName & "'"

            lsFileMstGid = gfExecuteScalar(lssql, gOdbcConn)


            'Insert Record to the Table
            For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                If lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim = "" Then
                    GoTo GoNext
                End If


                If lExcelDatatable.Rows(liIndex).Item("INST_NO").ToString.Trim = "" Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Cheque No Empty.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                If Not IsDate(lExcelDatatable.Rows(liIndex).Item("INST_DATE").ToString.Trim) Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Invalid Cheque Date.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString) & "- Chqno:" & lExcelDatatable.Rows(liIndex).Item("INST_NO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                'Check for Duplicate
                lssql = " Select finone_gid from chola_trn_tfinone where finone_deleteflag='N' and " & _
                            " finone_agreementno='" & lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim & "'" & _
                            " and finone_chqno='" & lExcelDatatable.Rows(liIndex).Item("INST_NO").ToString.Trim & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Duplicate Found.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString) & "- Chqno:" & lExcelDatatable.Rows(liIndex).Item("INST_NO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                Try

                    lssql = " Insert into chola_trn_tfinone (finone_file_gid,finone_agreementno,finone_shortagreementno,"
                    lssql &= " finone_orgchqno,finone_orgchqdate,finone_orgchqamount,"
                    lssql &= " finone_chqno,finone_chqdate,finone_chqamount,finone_bankdetails,finone_bankdesc,"
                    lssql &= " finone_cust_bank_account,finone_bankbranch,"
                    lssql &= " finone_clientcode,finone_batchid,finone_productflag,finone_clrlocation) "
                    lssql &= " values ("
                    lssql &= lsFileMstGid & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim, 7)), "000000") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("INST_NO").ToString.Trim) & "',"
                    lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("INST_DATE").ToString)), "yyyy-MM-dd") & "',"
                    lssql &= "" & Math.Round(Val(lExcelDatatable.Rows(liIndex).Item("AMOUNT").ToString.Trim), 2) & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("INST_NO").ToString.Trim) & "',"
                    lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("INST_DATE").ToString)), "yyyy-MM-dd") & "',"
                    lssql &= "" & Math.Round(Val(lExcelDatatable.Rows(liIndex).Item("AMOUNT").ToString.Trim), 2) & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BANK_DETAILS").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BANKDESC").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CUST_BANK_ACCTNO").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BRANCH").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CLENT_CODE").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BATCHID").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PRODUCTFLAG").ToString.Trim) & "',"
                    'lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PRODUCT").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CLR_LOCATION").ToString.Trim) & "')"
                    

                    fsResult = gfInsertQry(lssql, gOdbcConn)

                    If Val(fsResult) = 0 Then
                        MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    lival += 1
                Catch ex As Exception
                    MsgBox(ex.Message.ToString, MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    FileClose(1)
                    Exit Sub
                End Try
GoNext:
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

    Private Sub ImportexcelOld(ByVal FilePath As String, ByVal SheetName As String)
        Dim lExcelDatatable As New DataTable
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileGid, lsFileMstGid As String
        Dim lsDuplicatedtl As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName(), fsResult As String
        Dim lsFldNmesInfo As String = ""

        lsFldNmesInfo = "SL_NO|AGREEMENTNO|CHQ Number|CHQ Date|AMOUNT|BANK_DETAILS|MICR_CODE|CUSTOMERNAME|BANKDESC|CUST_BANK_ACCTNO|BANK_BRANCH"

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
                    FileClose(1)
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
                    FileClose(1)
                    Exit Sub
                End If

            Catch ex As Exception
                MsgBox("Error occured in insertion", MsgBoxStyle.Information, gProjectName)
                Application.UseWaitCursor = False
                FileClose(1)
                Exit Sub
            End Try

            lssql = " select file_gid from chola_mst_tfile where file_name='" & lsFileName & "' and file_sheetname='" & SheetName & "'"

            lsFileMstGid = gfExecuteScalar(lssql, gOdbcConn)


            'Insert Record to the Table
            For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                If lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim = "" Then
                    GoTo GoNext
                End If


                If lExcelDatatable.Rows(liIndex).Item("CHQ Number").ToString.Trim = "" Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Cheque No Empty.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                If Not IsDate(lExcelDatatable.Rows(liIndex).Item("CHQ Date").ToString.Trim) Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Invalid Cheque Date.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString) & "- Chqno:" & lExcelDatatable.Rows(liIndex).Item("CHQ Number").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                'Check for Duplicate
                lssql = " Select finone_gid from chola_trn_tfinone where finone_deleteflag='N' and " & _
                            " finone_agreementno='" & lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim & "'" & _
                            " and finone_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHQ Number").ToString.Trim & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Duplicate Found.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString) & "- Chqno:" & lExcelDatatable.Rows(liIndex).Item("CHQ Number").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                Try

                    lssql = " Insert into chola_trn_tfinone (finone_file_gid,finone_agreementno,finone_shortagreementno,"
                    lssql &= " finone_orgchqno,finone_orgchqdate,finone_orgchqamount,"
                    lssql &= " finone_chqno,finone_chqdate,finone_chqamount,finone_bankdetails,finone_micrcode,finone_customername,finone_bankdesc,"
                    lssql &= " finone_cust_bank_account,finone_bankbranch,"
                    lssql &= " finone_clientcode,finone_batchid,finone_productflag,finone_product,finone_clrlocation) "
                    lssql &= " values ("
                    lssql &= lsFileMstGid & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim, 7)), "000000") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CHQ Number").ToString.Trim) & "',"
                    lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("CHQ Date").ToString)), "yyyy-MM-dd") & "',"
                    lssql &= "" & Math.Round(Val(lExcelDatatable.Rows(liIndex).Item("AMOUNT").ToString.Trim), 2) & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CHQ Number").ToString.Trim) & "',"
                    lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("CHQ Date").ToString)), "yyyy-MM-dd") & "',"
                    lssql &= "" & Math.Round(Val(lExcelDatatable.Rows(liIndex).Item("AMOUNT").ToString.Trim), 2) & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BANK_DETAILS").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("MICR_CODE").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CUSTOMERNAME").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BANKDESC").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CUST_BANK_ACCTNO").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BANK_BRANCH").ToString.Trim) & "')"

                    fsResult = gfInsertQry(lssql, gOdbcConn)

                    If Val(fsResult) = 0 Then
                        MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    lival += 1
                Catch ex As Exception
                    MsgBox("Error occured in inserting", MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    FileClose(1)
                    Exit Sub
                End Try
GoNext:
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

    Private Sub frmhandsoffimport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmhandsoffimport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTotal.Visible = False
        Me.KeyPreview = True
        txtFileName.Focus()
        txtFileName.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class