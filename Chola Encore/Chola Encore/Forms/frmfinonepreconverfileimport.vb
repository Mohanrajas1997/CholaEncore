Imports System.IO
Public Class frmfinonepreconverfileimport
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

        lsFldNmesInfo = "SL_NO|BRANCH|PRODUCTFLAG|APPLICATION_ID|AGR_NO|MICR_CODE|BANKNAME|PAYABLE_PLACE|CHEQUESNO|CHEQUEDATE|" & _
            "CHEQUEAMOUNT|CUSTOMERID|CUSTOMERNAME|CUST_BANK_ACCTNO|REPAYMENT_MODE|BANK_BRANCH|"


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

                If lExcelDatatable.Rows(liIndex).Item("AGR_NO").ToString.Trim = "" Then
                    GoTo GoNext
                End If


                If lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim = "" Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Cheque No Empty.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGR_NO").ToString)
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                If Not IsDate(lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim) Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Invalid Cheque Date.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGR_NO").ToString) & "- Chqno:" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                'If liIndex = 0 Then
                '    'Check for Duplicate
                '    lssql = " Select finone_gid from chola_trn_tfinonepreconverfile where finone_deleteflag='N' " & _
                '                " and finone_chqdate='" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim), "yyyy-MM-dd") & "'" & _
                '                " and finone_file_gid<>" & lsFileMstGid

                '    lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                '    If Val(lsFileGid) > 0 Then
                '        MsgBox("Preconversion File Already Imported for Cycle Date:" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim), "yyyy-MM-dd"), MsgBoxStyle.Critical, gProjectName)
                '        Exit Sub
                '    End If
                'End If            

                'Check for Duplicate
                lssql = " Select finone_gid from chola_trn_tfinonepreconverfile where finone_deleteflag='N' and " & _
                            " finone_agreementno='" & lExcelDatatable.Rows(liIndex).Item("AGR_NO").ToString.Trim & "'" & _
                            " and finone_chqno='" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim & "'" & _
                            " and finone_chqdate='" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("CHEQUEDATE").ToString.Trim), "yyyy-MM-dd") & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Duplicate Found.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGR_NO").ToString) & "- Chqno:" & lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                Try

                    lssql = " Insert into chola_trn_tfinonepreconverfile (finone_file_gid,finone_branch,finone_prodflag,"
                    lssql &= " finone_appid,finone_agreementno,finone_shortagreementno,finone_micrcode,finone_bankname,"
                    lssql &= " finone_payableplace,finone_chqno,finone_chqdate,finone_chqamount,finone_orgchqno,"
                    lssql &= " finone_orgchqdate,finone_orgchqamount,finone_custid,finone_custname,finone_custbankaccno,"
                    lssql &= " finone_repaymode,finone_bankbranch) "
                    lssql &= " values ("
                    lssql &= lsFileMstGid & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BRANCH").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PRODUCTFLAG").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("APPLICATION_ID").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("AGR_NO").ToString.Trim) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lExcelDatatable.Rows(liIndex).Item("AGR_NO").ToString.Trim, 7)), "000000") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("MICR_CODE").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BANKNAME").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PAYABLE_PLACE").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim) & "',"
                    lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("CHEQUEDATE").ToString)), "yyyy-MM-dd") & "',"
                    lssql &= "" & Math.Round(Val(lExcelDatatable.Rows(liIndex).Item("CHEQUEAMOUNT").ToString.Trim), 2) & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CHEQUESNO").ToString.Trim) & "',"
                    lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("CHEQUEDATE").ToString)), "yyyy-MM-dd") & "',"
                    lssql &= "" & Math.Round(Val(lExcelDatatable.Rows(liIndex).Item("CHEQUEAMOUNT").ToString.Trim), 2) & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CUSTOMERID").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CUSTOMERNAME").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CUST_BANK_ACCTNO").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("REPAYMENT_MODE").ToString.Trim) & "',"
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