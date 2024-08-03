Imports System.IO
Public Class frmgetrefno
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
        Dim dragreement As Odbc.OdbcDataReader
        Dim objdt As New DataTable

        Dim lExcelDatatable As New DataTable
        Dim lssql As String = ""
        Dim lsSheetName As String = SheetName
        Dim lsFileName As String
        Dim lsFieldName() As String
        Dim lsFldNmesInfo As String = ""
        Dim liinvalid As Integer = 0
        Dim lsstr As String = ""
        Dim lbAddFlag As Boolean

        lsFldNmesInfo = "AGREEMENTNO"
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

            objdt.Columns.Add("Agreement No")
            objdt.Columns.Add("GNSAREF")
            objdt.Columns.Add("Mode")
            objdt.Columns.Add("Cupboard No")
            objdt.Columns.Add("Shelf No")
            objdt.Columns.Add("Box No")

            'Get Record from the Table
            For liIndex As Integer = 0 To lExcelDatatable.Rows.Count - 1
                litotal = lExcelDatatable.Rows.Count
                lblTotal.Text = "Processing " & liIndex + 1 & "/" & lExcelDatatable.Rows.Count
                Application.DoEvents()

                lssql = ""

                If rdoPkt.Checked Then
                    lssql &= " select distinct agreement_no as 'AgreementNo',packet_gnsarefno as 'GNSAREF',packet_mode as 'Mode', "
                    lssql &= " almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No',packet_status "
                    lssql &= " from chola_trn_tpacket "
                    lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
                    lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
                    lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim & "'"
                    'lssql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "
                    lssql &= " order by packet_gnsarefno"
                ElseIf rdoDetail.Checked Then
                    lssql &= " select distinct pdc_parentcontractno as 'AgreementNo', pdc_gnsarefno as 'GNSAREF',"
                    lssql &= " '' as 'Mode', '' as 'Cupboard No','' as 'Shelf No','' as 'Box No' "
                    lssql &= " from chola_trn_tpdcfile "
                    lssql &= " left join chola_trn_tpacket on packet_gnsarefno=pdc_gnsarefno "
                    lssql &= " where packet_gid is null and pdc_parentcontractno='" & lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim & "'"
                    lssql &= " union "
                    lssql &= " select distinct agreement_no as 'AgreementNo',packet_gnsarefno as 'GNSAREF',packet_mode as 'Mode', "
                    lssql &= " almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No' "
                    lssql &= " from chola_trn_tpacket "
                    lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
                    lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
                    lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim & "'"
                    lssql &= " order by packet_gnsarefno"
                Else
                    lssql &= " select agreement_no as 'AgreementNo',"
                    lssql &= " group_concat(ifnull(packet_gnsarefno,'')) as 'GNSAREF',"
                    lssql &= " '' as 'Mode', "
                    lssql &= " group_concat(ifnull(cast(almaraentry_cupboardno as nchar),'')) as 'Cupboard No',"
                    lssql &= " group_concat(ifnull(cast(almaraentry_shelfno as nchar),'')) as 'Shelf No',"
                    lssql &= " group_concat(ifnull(cast(almaraentry_boxno as nchar),'')) as 'Box No' "
                    lssql &= " from chola_trn_tpacket "
                    lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
                    lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
                    lssql &= " where agreement_no='" & lExcelDatatable.Rows(liIndex).Item("AGREEMENTNO").ToString.Trim & "'"
                    lssql &= " group by agreement_no "
                End If

                dragreement = gfExecuteQry(lssql, gOdbcConn)

                If dragreement.HasRows Then
                    While dragreement.Read
                        lbAddFlag = True

                        If rdoPkt.Checked = True Then
                            If (dragreement.Item("packet_status") And GCIPACKETPULLOUT) > 0 Then
                                lbAddFlag = False
                                liinvalid += 1
                                lsstr = Now() & "  Packet Already Pulled Out : " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString) & "," & dragreement.Item("GNSAREF").ToString
                                PrintLine(1, lsstr)
                            End If
                        End If

                        If lbAddFlag = True Then
                            objdt.Rows.Add()
                            objdt.Rows(objdt.Rows.Count - 1).Item("Agreement No") = dragreement.Item("AgreementNo").ToString
                            objdt.Rows(objdt.Rows.Count - 1).Item("GNSAREF") = dragreement.Item("GNSAREF").ToString
                            objdt.Rows(objdt.Rows.Count - 1).Item("Mode") = dragreement.Item("Mode").ToString
                            objdt.Rows(objdt.Rows.Count - 1).Item("Cupboard No") = dragreement.Item("Cupboard No").ToString
                            objdt.Rows(objdt.Rows.Count - 1).Item("Shelf No") = dragreement.Item("Shelf No").ToString
                            objdt.Rows(objdt.Rows.Count - 1).Item("Box No") = dragreement.Item("Box No").ToString
                        End If
                    End While
                Else
                    liinvalid += 1
                    lsstr = Now() & "  Invalid Agreement No. " & QuoteFilter(lExcelDatatable.Rows(liIndex)("AGREEMENTNO").ToString)
                    PrintLine(1, lsstr)
                End If
            Next

            FileClose(1)

            If objdt.Rows.Count > 0 Then
                dgview.DataSource = objdt
                PrintDGridXML(dgview, gsReportPath & "\Report.xls", "Report")
            End If

            If liinvalid > 0 Then
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