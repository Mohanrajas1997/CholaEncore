Imports System.IO
Public Class frmalmaraupload
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

            If cboformat.Text.Trim = "" Then
                MessageBox.Show("Format should not be empty ", gProjectName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                cboformat.Focus()
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
        Dim isdiff As Boolean = True
        Dim lssql As String = ""
        Dim lsboxgid As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileGid, lsFileMstGid As String
        Dim lsDuplicatedtl As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName(), fsResult As String
        Dim lsFldNmesInfo As String = ""

        lsFldNmesInfo = "SNo|GNSAREF|Box"


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
            lExcelDatatable = gpExcelDataset("SELECT * FROM [" & lsSheetName & "$] order by Box,GNSAREF", FilePath)

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


            'Insert Record to the Table
            For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                If lExcelDatatable.Rows(liIndex).Item("GNSAREF").ToString.Trim = "" Then
                    GoTo GoNext
                End If

                'Check for Box
                
                lssql = " Select box_gid from chola_trn_tbox where " & _
                            " box_no=" & Val(lExcelDatatable.Rows(liIndex).Item("Box").ToString.Trim)


                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)


                If (lsboxgid <> lsFileGid) Then
                    'Write  Record in Error log
                    If Val(lsFileGid) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1
                        lsDuplicatedtl = Now() & " Box Already exist " & lExcelDatatable.Rows(liIndex).Item("Box").ToString.Trim & "-" & lExcelDatatable.Rows(liIndex).Item("GNSAREF").ToString.Trim & " "
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext
                    Else
                        isdiff = True
                    End If
                ElseIf lsboxgid <> "" Then
                    isdiff = False
                End If




                'Check for Duplicate
                lssql = " Select almara_gid from chola_trn_almara where " & _
                            " almara_gnsaref='" & lExcelDatatable.Rows(liIndex).Item("GNSAREF").ToString.Trim & "' "

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & " Duplicate exist " & lExcelDatatable.Rows(liIndex).Item("GNSAREF").ToString.Trim & " "
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                Try

                    If isdiff = True Then
                        lssql = " insert into chola_trn_tbox(box_no,gnsaref_from,gnsaref_to,doc_type,entry_date,entry_by) values ("
                        lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("Box").ToString)) & ","
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("GNSAREF").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("GNSAREF").ToString) & "',"
                        lssql &= "'" & cboformat.Text & "',sysdate(),"
                        lssql &= "'" & gUserName & "')"

                        fsResult = gfInsertQry(lssql, gOdbcConn)

                        If Val(fsResult) = 0 Then
                            MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                        lssql = " Select box_gid from chola_trn_tbox where " & _
                             " box_no=" & Val(lExcelDatatable.Rows(liIndex).Item("Box").ToString.Trim)


                        lsboxgid = gfExecuteScalar(lssql, gOdbcConn)

                    Else
                        lssql = " update chola_trn_tbox set "
                        lssql &= " gnsaref_to='" & QuoteFilter(lExcelDatatable.Rows(liIndex)("GNSAREF").ToString) & "'"
                        lssql &= " where box_gid=" & Val(lsboxgid)

                        fsResult = gfInsertQry(lssql, gOdbcConn)

                        If Val(fsResult) = 0 Then
                            MessageBox.Show("Some Error occurred while updating ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End If

                    If InStr(QuoteFilter(lExcelDatatable.Rows(liIndex)("GNSAREF").ToString.ToUpper), "P") > 0 Then
                        lssql = " update chola_trn_tpdcfile set pdc_box_gid=" & Val(lsboxgid)
                        lssql &= " where pdc_gnsarefno='" & QuoteFilter(lExcelDatatable.Rows(liIndex)("GNSAREF").ToString.ToUpper) & "'"
                    Else
                        lssql = " update chola_trn_tspdc set spdc_box_gid=" & Val(lsboxgid)
                        lssql &= " where spdc_gnsarefno='" & QuoteFilter(lExcelDatatable.Rows(liIndex)("GNSAREF").ToString.ToUpper) & "'"
                    End If

                    fsResult = gfInsertQry(lssql, gOdbcConn)

                    If Val(fsResult) = 0 Then
                        MessageBox.Show("Some Error occurred while updating ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If


                    lssql = " Insert into chola_trn_almara (almara_file_gid,almara_gnsaref,almara_box,"
                    lssql &= "almara_insertdate,almara_insertby) values ("
                    lssql &= lsFileMstGid & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("GNSAREF").ToString) & "',"
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex)("Box").ToString)) & ","
                    lssql &= "sysdate(),'" & gUserName & "')"

                    fsResult = gfInsertQry(lssql, gOdbcConn)

                    If Val(fsResult) = 0 Then
                        MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    lssql = ""
                    lival += 1
                Catch ex As Exception
                    MsgBox("Error occured in inserting", MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
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

    Private Sub frmalmaraupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmalmaraupload_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTotal.Visible = False
        Me.KeyPreview = True
        txtFileName.Focus()
        txtFileName.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class