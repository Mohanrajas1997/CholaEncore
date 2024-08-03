Imports System.IO
Public Class frmretrievalimport
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
            MessageBox.Show("Total Records:" & litotal & " ; Valid Records:" & lival & " ; Invalid Record:" & lidup & vbCrLf & "Please review the Error Log in the path " & lsErrorLogPath, "CHOLA", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim lsAgreementNo As String
        Dim lsPktMode As String
        Dim lsagreementgid As String
        Dim lnPacketGid As Long
        Dim lschqdisc As String = ""
        Dim ds As New DataSet
        Dim lbInsertFlag As Boolean = True

        lsFldNmesInfo = "Sno|Request Date|Agreement No|Retrieval Mode|Retrieval Reason|Cheque No|Packet No|"

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
                lbInsertFlag = True

                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                lsAgreementNo = QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Agreement No").ToString.Trim)
                lsPktMode = QuoteFilter(lExcelDatatable.Rows(liIndex)("Retrieval Mode").ToString.ToUpper)

                If lsAgreementNo = "" Then
                    lbInsertFlag = False
                    lsDuplicatedtl = (liIndex + 1).ToString & ";Agreement No Blank "
                    PrintLine(1, lsDuplicatedtl)
                End If

                lssql = ""
                lssql &= " select * from chola_mst_tagreement "
                lssql &= " where 1 = 1 "

                If IsNumeric(lsAgreementNo) = True Then
                    lssql &= " and shortagreement_no='" & lsAgreementNo & "' "
                Else
                    lssql &= " and agreement_no='" & lsAgreementNo & "' "
                End If

                Call gpDataSet(lssql, "agr", gOdbcConn, ds)

                If ds.Tables("agr").Rows.Count = 1 Then
                    lsagreementgid = ds.Tables("agr").Rows(0).Item("agreement_gid").ToString
                    lsAgreementNo = ds.Tables("agr").Rows(0).Item("agreement_no").ToString
                Else
                    lsagreementgid = "0"
                End If

                ds.Tables("agr").Rows.Clear()

                lsagreementgid = gfExecuteScalar(lssql, gOdbcConn)

                If Val(lsagreementgid) = 0 Then
                    lbInsertFlag = False
                    lsDuplicatedtl = (liIndex + 1).ToString & ";Invalid Agreement No :" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsDuplicatedtl)
                End If

                If Not IsDate(lExcelDatatable.Rows(liIndex).Item("Request Date").ToString.Trim) Then
                    lbInsertFlag = False
                    lsDuplicatedtl = (liIndex + 1).ToString & ";Invalid Request Date :" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsDuplicatedtl)
                End If

                If lExcelDatatable.Rows(liIndex).Item("Retrieval Mode").ToString.Trim = "" Then
                    lbInsertFlag = False
                    lsDuplicatedtl = (liIndex + 1).ToString & ";Retrieval Mode Blank :" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsDuplicatedtl)
                End If

                If lExcelDatatable.Rows(liIndex).Item("Retrieval Reason").ToString.Trim = "" Then
                    lbInsertFlag = False
                    lsDuplicatedtl = (liIndex + 1).ToString & ";Retrieval Reason Blank :" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsDuplicatedtl)
                End If

                'Check for Duplicate
                lssql = " Select retrieval_gid from chola_trn_tretrieval where 1=1 "
                lssql &= " and retrieval_requestdate='" & Format(CDate(lExcelDatatable.Rows(liIndex)("Request Date").ToString), "yyyy-MM-dd") & "'"
                lssql &= " and retrieval_agreement_gid=" & lsagreementgid & ""
                lssql &= " and retrieval_mode='" & lExcelDatatable.Rows(liIndex)("Retrieval Mode").ToString & "'"

                lsFileGid = gfExecuteScalar(lssql, gOdbcConn)

                'Write Dulicate Record in Error log
                If Val(lsFileGid) > 0 Then
                    lbInsertFlag = False
                    lsDuplicatedtl = (liIndex + 1).ToString & ";Duplicate exist " & QuoteFilter(lExcelDatatable.Rows(liIndex)("Agreement No").ToString)
                    PrintLine(1, lsDuplicatedtl)
                End If

                Select Case lsPktMode
                    Case "PACKET"
                        lssql = ""
                        lssql &= " select packet_gid "
                        lssql &= " from chola_trn_tpacket "
                        lssql &= " where 1=1 "
                        lssql &= " and packet_gnsarefno='" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Packet No").ToString) & "'"

                        lnPacketGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                        If lnPacketGid = 0 Then
                            lschqdisc = "Y"
                        Else
                            lschqdisc = "N"
                        End If
                    Case "PDC"
                        lssql = ""
                        lssql &= " select chq_packet_gid "
                        lssql &= " from chola_trn_tpdcentry "
                        lssql &= " where 1=1 "
                        lssql &= " and chq_agreement_gid=" & lsagreementgid
                        lssql &= " and chq_no='" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Cheque No").ToString) & "'"

                        lnPacketGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                        If lnPacketGid = 0 Then
                            lschqdisc = "Y"
                        Else
                            lschqdisc = "N"
                        End If
                    Case "SPDC"
                        lssql = ""
                        lssql &= " select p.packet_gid "
                        lssql &= " from chola_trn_tspdcchqentry as s "
                        lssql &= " inner join chola_trn_tpacket as p on p.packet_gid = s.chqentry_packet_gid "
                        lssql &= " where 1=1 "
                        lssql &= " and s.chqentry_chqno ='" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Cheque No").ToString) & "'"
                        lssql &= " and p.packet_agreement_gid=" & lsagreementgid

                        lnPacketGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                        If lnPacketGid = 0 Then
                            lschqdisc = "Y"
                        Else
                            lschqdisc = "N"
                        End If
                    Case Else
                        lbInsertFlag = False
                        lsDuplicatedtl = (liIndex + 1).ToString & ";Invalid Packet Mode :" & lsPktMode
                        PrintLine(1, lsDuplicatedtl)
                End Select

                Try
                    If lbInsertFlag = True Then
                        lssql = " Insert into chola_trn_tretrieval (retrieval_file_gid,retrieval_agreement_gid,retrieval_packet_gid,"
                        lssql &= " retrieval_requestdate,retrieval_agreementno,retrieval_shortagreementno,retrieval_mode,retrieval_reason,"
                        lssql &= " retrieval_chqno,retrieval_gnsarefno,retrieval_discflag)"
                        lssql &= " values ("
                        lssql &= lsFileMstGid & ","
                        lssql &= "" & lsagreementgid & ","
                        lssql &= "" & lnPacketGid & ","
                        lssql &= "'" & Format(CDate(QuoteFilter(lExcelDatatable.Rows(liIndex)("Request Date").ToString)), "yyyy-MM-dd") & "',"
                        lssql &= "'" & lsAgreementNo & "',"
                        lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(lsAgreementNo, 7)), "000000") & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Retrieval Mode").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Retrieval Reason").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Cheque No").ToString) & "',"
                        lssql &= "'" & QuoteFilter(lExcelDatatable.Rows(liIndex)("Packet No").ToString) & "',"
                        lssql &= "'" & lschqdisc & "')"

                        fsResult = gfInsertQry(lssql, gOdbcConn)

                        lival += 1
                    Else
                        lidup += 1
                    End If
                Catch ex As Exception
                    MsgBox("Error occured in inserting", MsgBoxStyle.Information, gProjectName)
                    Application.UseWaitCursor = False
                    FileClose(1)
                    Exit Sub
                End Try
GoNext:
            Next

            FileClose(1)

            If lidup > 0 Then
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

    Private Sub frmretrievalimport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmretrievalimport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTotal.Visible = False
        Me.KeyPreview = True
        txtFileName.Focus()
        txtFileName.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class