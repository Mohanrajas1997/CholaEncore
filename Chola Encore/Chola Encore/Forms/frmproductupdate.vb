Imports System.IO
Public Class frmproductupdate
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
            MessageBox.Show("Total Records:" & litotal & " ;Valid Records:" & lival & " ;Duplicate/Invalid Record:" & lidup & vbCrLf & "Please review the Error Log in the path " & lsErrorLogPath, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName() As String
        Dim lsFldNmesInfo As String = ""
        Dim liinvalid As Integer = 0
        Dim lsstr As String = ""
        Dim lsFileMstGid As String

        Dim lnProductGid As Long
        Dim lnUpdateGid As Long

        lsFldNmesInfo = "Agreement No|Product Type|"
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



            'Get Record from the Table
            For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                If lExcelDatatable.Rows(liIndex).Item("Agreement No").ToString.Trim = "" Then
                    liinvalid += 1
                    lsstr = "Agreement No. Blank.." & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsstr)
                    GoTo GoNext
                End If

                If lExcelDatatable.Rows(liIndex).Item("Product Type").ToString.Trim = "" Then
                    liinvalid += 1
                    lsstr = "Product Type Blank.." & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsstr)
                    GoTo GoNext
                End If

                'Product Type
                lssql = ""
                lssql &= " select type_flag "
                lssql &= " from chola_mst_ttype "
                lssql &= " where type_deleteflag='N' "
                lssql &= " and type_name='" & lExcelDatatable.Rows(liIndex).Item("Product Type").ToString.Trim & "'"
                lnProductGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                If lnProductGid = 0 Then
                    liinvalid += 1
                    lsstr = "Invalid Product Type :" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsstr)
                    GoTo GoNext
                End If

                'Duplicate Check
                lssql = ""
                lssql &= " select update_gid "
                lssql &= " from chola_trn_tproductupdate "
                lssql &= " where update_file_gid=" & lsFileMstGid
                lssql &= " and update_agreementno='" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString) & "'"
                lnUpdateGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                If lnUpdateGid > 0 Then
                    liinvalid += 1
                    lsstr = " Duplcate Agreement No :" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsstr)
                    GoTo GoNext
                End If


                lssql = ""
                lssql &= " insert into chola_trn_tproductupdate ("
                lssql &= " update_file_gid,update_type_gid,update_productcode,update_agreementno )"
                lssql &= " values ("
                lssql &= "" & lsFileMstGid & ","
                lssql &= "" & lnProductGid & ","
                lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Product Type").ToString) & "',"
                lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString) & "')"

                gfInsertQry(lssql, gOdbcConn)
GoNext:
            Next

            FileClose(1)

            If liinvalid > 0 Then
                lidup = liinvalid
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

    Private Sub frmgetrefno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmgetrefno_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTotal.Visible = False
        Me.KeyPreview = True
        txtFileName.Focus()
        txtFileName.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class