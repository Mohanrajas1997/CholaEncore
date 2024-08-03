Imports System.IO
Public Class frmPacketPullout
    Dim lival, lidup, litotal As Integer
    Dim lsErrorLogPath As String = Application.StartupPath & "\errorlog.txt"
    Dim msSql As String = ""

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

    Private Sub ImportexcelOld(ByVal FilePath As String, ByVal SheetName As String)
        Dim lExcelDatatable As New DataTable
        Dim ds As New DataSet
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileMstGid As String
        Dim lsDuplicatedtl As String = ""
        Dim lsDisc As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName() As String
        Dim lsFldNmesInfo As String = ""
        Dim lsPktNo As String = ""
        Dim lsReason As String = ""
        Dim n As Long = 0
        Dim lnPktId As Long
        Dim lnPktStatus As Long

        lsFldNmesInfo = "SNo|Packet No|Reason"

        lsFieldName = Split(lsFldNmesInfo, "|")

        'Open Error Log File
        If File.Exists(lsErrorLogPath) Then
            FileOpen(1, lsErrorLogPath, OpenMode.Output)
        Else
            File.Create(lsErrorLogPath)
        End If

        Try
            lsFileName = Path.GetFileName(FilePath).ToUpper

            PrintLine(1, "SNo|Packet No|Reason|Error")

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
                n += 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                lsPktNo = QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Packet No").ToString.Trim)
                lsReason = Mid(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Reason").ToString.Trim), 1, 125)

                If lsPktNo = "" And lsReason = "" Then
                    GoTo GoNext
                End If

                If lsPktNo = "" Or lsReason = "" Then
                    lidup += 1
                    liIsDuplicate += 1

                    Select Case ""
                        Case lsPktNo
                            lsDisc = "Blank Packet No"
                        Case lsReason
                            lsDisc = "Blank Reason"
                    End Select

                    lsDuplicatedtl = n & "|" & lsPktNo & "|" & lsReason & "|" & lsDisc
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                lssql = " select packet_gid,packet_status from chola_trn_tpacket "
                lssql &= " where packet_gnsarefno = '" & lsPktNo & "'"

                Call gpDataSet(lssql, "pkt", gOdbcConn, ds)

                If ds.Tables("pkt").Rows.Count > 0 Then
                    lnPktId = ds.Tables("pkt").Rows(0).Item("packet_gid")
                    lnPktStatus = ds.Tables("pkt").Rows(0).Item("packet_status")

                    If (lnPktStatus And GCIPACKETPULLOUT) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1

                        lsDuplicatedtl = n & "|" & lsPktNo & "|" & lsReason & "|Packet already pulled out"
                        PrintLine(1, lsDuplicatedtl)
                        GoTo GoNext
                    End If
                Else
                    lidup += 1
                    liIsDuplicate += 1

                    lsDuplicatedtl = n & "|" & lsPktNo & "|" & lsReason & "|Invalid Packet No"
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                ds.Tables("pkt").Rows.Clear()

                Try

                    lssql = " Insert into chola_trn_tpacketpullout (packetpullout_file_gid,packetpullout_packet_gid,packetpullout_reason)"
                    lssql &= " values ("
                    lssql &= lsFileMstGid & ","
                    lssql &= "" & lnPktId & ","
                    lssql &= "'" & lsReason & "')"

                    Call gfInsertQry(lssql, gOdbcConn)
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

    Private Sub Importexcel(ByVal FilePath As String, ByVal SheetName As String)
        Dim lExcelDatatable As New DataTable
        Dim ds As New DataSet
        Dim lssql As String = ""
        Dim liIsDuplicate As Integer = 0
        Dim lsFileMstGid As String
        Dim lsDuplicatedtl As String = ""
        Dim lnAgreementId As Long = 0
        Dim lsDisc As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName() As String
        Dim lsFldNmesInfo As String = ""
        Dim lsPktNo As String = ""
        Dim lsAgreementNo As String = ""
        Dim lsReason As String = ""
        Dim n As Long = 0
        Dim lnPktId As Long
        Dim lnPktStatus As Long
        Dim lnPktPulloutId As Long = 0
        Dim lnResult As Long = 0

        lsFldNmesInfo = "SNo|Packet No|Agreement No|Reason"

        lsFieldName = Split(lsFldNmesInfo, "|")

        'Open Error Log File
        If File.Exists(lsErrorLogPath) Then
            FileOpen(1, lsErrorLogPath, OpenMode.Output)
        Else
            File.Create(lsErrorLogPath)
        End If

        Try
            lsFileName = Path.GetFileName(FilePath).ToUpper

            PrintLine(1, "SNo|Packet No|Agreement No|Reason|Error")

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
                n += 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                lsPktNo = QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Packet No").ToString.Trim)
                lsAgreementNo = QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Agreement No").ToString.Trim)
                lsReason = Mid(QuoteFilter(lExcelDatatable.Rows(liIndex).Item("Reason").ToString.Trim), 1, 125)

                If lsPktNo = "" And lsAgreementNo = "" And lsReason = "" Then
                    GoTo GoNext
                End If

                If lsPktNo = "" Or lsReason = "" Or lsAgreementNo = "" Then
                    lidup += 1
                    liIsDuplicate += 1

                    Select Case ""
                        Case lsPktNo
                            lsDisc = "Blank Packet No"
                        Case lsAgreementNo
                            lsDisc = "Blank Agreement No"
                        Case lsReason
                            lsDisc = "Blank Reason"
                    End Select

                    lsDuplicatedtl = n & "|" & lsPktNo & "|" & lsAgreementNo & "|" & lsReason & "|" & lsDisc
                    PrintLine(1, lsDuplicatedtl)
                    GoTo GoNext
                End If

                ' get agreement_gid

                lssql = ""
                lssql &= " select agreement_gid from chola_mst_tagreement "
                lssql &= " where agreement_no = '" & lsAgreementNo & "' "

                lnAgreementId = Val(gfExecuteScalar(lssql, gOdbcConn))

                If lnAgreementId = 0 Then
                    PrintLine(1, n & "|" & lsPktNo & "|" & lsAgreementNo & "|" & lsReason & "|Invalid Agreement")
                    GoTo GoNext
                End If

                lssql = " select packet_gid,packet_status from chola_trn_tpacket "
                lssql &= " where packet_gnsarefno = '" & lsPktNo & "'"
                lssql &= " and packet_agreement_gid = " & lnAgreementId & " "

                Call gpDataSet(lssql, "pkt", gOdbcConn, ds)

                If ds.Tables("pkt").Rows.Count > 0 Then
                    lnPktId = ds.Tables("pkt").Rows(0).Item("packet_gid")
                    lnPktStatus = ds.Tables("pkt").Rows(0).Item("packet_status")

                    If (lnPktStatus And GCIPACKETPULLOUT) > 0 Then
                        lidup += 1
                        liIsDuplicate += 1

                        lsDuplicatedtl = n & "|" & lsPktNo & "|" & lsReason & "|Packet already pulled out"
                        PrintLine(1, lsDuplicatedtl)
                        ds.Tables("pkt").Rows.Clear()
                        GoTo GoNext
                    End If
                Else
                    lidup += 1
                    liIsDuplicate += 1

                    lsDuplicatedtl = n & "|" & lsPktNo & "|" & lsReason & "|Invalid Packet No"
                    PrintLine(1, lsDuplicatedtl)
                    ds.Tables("pkt").Rows.Clear()
                    GoTo GoNext
                End If

                ds.Tables("pkt").Rows.Clear()

                Try

                    lssql = " Insert into chola_trn_tpacketpullout (packetpullout_file_gid,packetpullout_packet_gid,packetpullout_reason)"
                    lssql &= " values ("
                    lssql &= lsFileMstGid & ","
                    lssql &= "" & lnPktId & ","
                    lssql &= "'" & lsReason & "')"

                    Call gfInsertQry(lssql, gOdbcConn)

                    ' find new packetpullout_gid
                    msSql = ""
                    msSql &= " select max(packetpullout_gid) from chola_trn_tpacketpullout "
                    msSql &= " where packetpullout_packet_gid = " & lnPktId & " "

                    lnPktPulloutId = Val(gfExecuteScalar(msSql, gOdbcConn))

                    ' update in packet table
                    msSql = ""
                    msSql &= " update chola_trn_tpacket set "
                    msSql &= " packet_status=packet_status | " & GCIPACKETPULLOUT & " "
                    msSql &= " where packet_gid = " & lnPktId
                    msSql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "

                    lnResult = gfInsertQry(msSql, gOdbcConn)

                    ' pdc entry
                    msSql = ""
                    msSql &= " update chola_trn_tpdcentry set "
                    msSql &= " chq_status=chq_status | " & GCPACKETPULLOUT & ","
                    msSql &= " chq_desc='" & Mid(lsReason, 1, 255) & "'"
                    msSql &= " where chq_packet_gid=" & lnPktId & " "
                    msSql &= " and chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(msSql, gOdbcConn)

                    msSql = ""
                    msSql &= " update chola_trn_tpdcentry set "
                    msSql &= " chq_status=chq_status | " & GCPACKETPULLOUT & ","
                    msSql &= " chq_desc='" & Mid(lsReason, 1, 255) & "'"
                    msSql &= " where chq_packet_gid=" & lnPktId & " "
                    msSql &= " and chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0 "
                    msSql &= " and chq_status & " & GCLOOSECHQ & " > 0 "

                    lnResult = gfInsertQry(msSql, gOdbcConn)

                    msSql = ""
                    msSql &= " update chola_trn_tspdcchqentry set "
                    msSql &= " chqentry_status = chqentry_status | " & GCPACKETPULLOUT & ","
                    msSql &= " chqentry_remarks ='" & Mid(lsReason, 1, 255) & "'"
                    msSql &= " where chqentry_packet_gid=" & lnPktId & " "
                    msSql &= " and chqentry_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(msSql, gOdbcConn)

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

    Private Sub frmpacketpullout_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmpacketpullout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblTotal.Visible = False
        Me.KeyPreview = True
        txtFileName.Focus()
        txtFileName.Text = ""
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class