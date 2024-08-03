Imports System.IO
Public Class frminwardimport
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
        Dim lExcelChkDatatable As New DataTable
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileGid, lsFileMstGid As String
        Dim lsDuplicatedtl As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName(), fsResult As String
        Dim lsFldNmesInfo As String = ""
        Dim lslmsAuthDate As String = ""
        Dim lsEMIDate As String = ""
        Dim lnPdc As Long = 0
        Dim lnSpdc As Long = 0
        Dim lnTotPdcCount As Long = 0
        Dim lnTotSpdcCount As Long = 0
        Dim lnTotMandateCount As Long = 0

        lsFldNmesInfo = "S#No|PRODUCT|APPLICATION_ID|APPL_FORM_NO|BRANCH|AGREEMENTNO|CUSTOMER NAME|PAY_MODE|" & _
                        "Pdc|Spdc|mandate|LMS_AUTH_DATE|TENURE|FIRST_EMI_DATE|Instl-Mode|	REMARKS|Comb agr|RECEIVED_DATE"

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

            lExcelChkDatatable = gpExcelDataset("SELECT sum(Pdc) as tot_pdc,sum(Spdc) as tot_spdc,sum(mandate) as tot_mandate FROM [" & lsSheetName & "$]", FilePath)

            If lExcelChkDatatable.Rows.Count > 0 Then
                lnTotPdcCount = Val(lExcelChkDatatable.Rows(0).Item("tot_pdc").ToString)
                lnTotSpdcCount = Val(lExcelChkDatatable.Rows(0).Item("tot_spdc").ToString)
                lnTotMandateCount = Val(lExcelChkDatatable.Rows(0).Item("tot_mandate").ToString)

                If Not (Val(txtPdcCount.Text) = lnTotPdcCount _
                           And Val(txtSpdcCount.Text) = lnTotSpdcCount _
                           And Val(txtMandateCount.Text) = lnTotMandateCount) Then
                    MessageBox.Show("Pdc/Spdc/Mandate count not tallied !", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    FileClose(1)
                    Exit Sub
                End If
            Else
                MessageBox.Show("Invalid Excel !", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                FileClose(1)
                Exit Sub
            End If
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

                If lExcelDatatable.Rows(liIndex).Item("PAY_MODE").ToString.Trim = "" Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Pay Mode Empty.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                If lExcelDatatable.Rows(liIndex).Item("RECEIVED_DATE").ToString.Trim = "" Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Received Date Empty.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                If Not IsDate(lExcelDatatable.Rows(liIndex).Item("RECEIVED_DATE").ToString.Trim) Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Invalid Received Date.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString) & "-" & lExcelDatatable.Rows(liIndex).Item("RECEIVED_DATE").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                If CheckReceivedDateIsValid(CDate(lExcelDatatable.Rows(liIndex).Item("RECEIVED_DATE").ToString.Trim)) = False Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Invalid Received Date.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString) & "-" & lExcelDatatable.Rows(liIndex).Item("RECEIVED_DATE").ToString.Trim
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                'Check for Duplicate
                lssql = " Select inward_gid from chola_trn_tinward where " & _
                            " inward_agreementno='" & lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim & "'" & _
                            " and inward_receiveddate='" & Format(CDate(lExcelDatatable.Rows(liIndex).Item("RECEIVED_DATE").ToString.Trim), "yyyy-MM-dd") & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lidup += 1
                    liIsDuplicate += 1
                    lsDuplicatedtl = Now() & "  Duplicate Found.. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                lnPdc = Val(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Pdc").ToString.Trim))
                lnSpdc = Val(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Spdc").ToString.Trim))

                ''Write Pdc/Spdc Record in Error log
                'If lnPdc = 0 Or lnSpdc = 0 Then
                '    lidup += 1
                '    liIsDuplicate += 1
                '    If lnPdc = 0 And lnSpdc = 0 Then
                '        lsDuplicatedtl = Now() & "  Pdc and Spdc Found zero ... " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                '    ElseIf lnPdc = 0 Then
                '        lsDuplicatedtl = Now() & "  Pdc Found zero ... " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                '    Else
                '        lsDuplicatedtl = Now() & "  Spdc Found zero ... " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                '    End If

                '    PrintLine(1, lsDuplicatedtl)
                '    GoTo GoNext
                'End If

                If QuoteFilter(lExcelDatatable.Rows(liIndex)("LMS_AUTH_DATE").ToString) = "" Then
                    lslmsAuthDate = "null"
                Else
                    lslmsAuthDate = "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("LMS_AUTH_DATE").ToString)), "yyyy-MM-dd") & "'"
                End If

                If QuoteFilter(lExcelDatatable.Rows(liIndex)("FIRST_EMI_DATE").ToString) = "" Then
                    lsEMIDate = "null"
                Else
                    lsEMIDate = "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("FIRST_EMI_DATE").ToString)), "yyyy-MM-dd") & "'"
                End If

                Try

                    lssql = " Insert into chola_trn_tinward (inward_file_gid,inward_product,inward_applicationid,"
                    lssql &= " inward_applicationformno,inward_branch,inward_agreementno,"
                    lssql &= " inward_shortagreementno,inward_customername,inward_paymode,inward_pdc,inward_spdc,"
                    lssql &= " inward_mandate,inward_lmsauthdate,inward_tenure,inward_firstemidate,inward_installmode,inward_dumpremarks,inward_compagr,inward_receiveddate)"
                    lssql &= " values ("
                    lssql &= lsFileMstGid & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PRODUCT").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("APPLICATION_ID").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("APPL_FORM_NO").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("BRANCH").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim) & "',"
                    lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim, 7)), "000000") & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("CUSTOMER NAME").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("PAY_MODE").ToString.Trim) & "',"
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Pdc").ToString.Trim)) & ","
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Spdc").ToString.Trim)) & ","
                    lssql &= "" & Val(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("mandate").ToString.Trim)) & ","
                    lssql &= "" & lslmsAuthDate & ","
                    lssql &= "" & Val(lExcelDatatable.Rows(liIndex).Item("TENURE").ToString.Trim) & ","
                    lssql &= "" & lsEMIDate & ","
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Instl-Mode").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("REMARKS").ToString.Trim) & "',"
                    lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Comb agr").ToString.Trim) & "',"
                    lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("RECEIVED_DATE").ToString.Trim)), "yyyy-MM-dd") & "')"


                    fsResult = gfInsertQry(lssql, gOdbcConn)

                    If Val(fsResult) = 0 Then
                        MessageBox.Show("Some Error occurred while inserting ", "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    lival += 1
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    FileClose(1)
                    Exit Sub
                End Try
GoNext:
            Next

            lssql = ""
            lssql &= " update chola_trn_tinward set inward_parent_gid=inward_gid "
            lssql &= " where inward_parent_gid=0 "
            gfInsertQry(lssql, gOdbcConn)

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

    Private Sub frminwardimport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frminwardimport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTotal.Visible = False
        Me.KeyPreview = True
        txtFileName.Focus()
        txtFileName.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Function CheckReceivedDateIsValid(ByVal ReceivedDate As Date) As Boolean
        Dim ldcurrentdate As Date
        Dim lidatediff As Integer

        ldcurrentdate = CDate(gfExecuteScalar("select sysdate();", gOdbcConn))

        lidatediff = DateDiff(DateInterval.Day, ldcurrentdate, CDate(ReceivedDate))

        If lidatediff > 0 Or lidatediff <= -10 Then
            Return False
        Else
            Return True
        End If

    End Function
End Class