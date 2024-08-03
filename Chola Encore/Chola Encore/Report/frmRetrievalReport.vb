Public Class frmRetrievalReport
    Dim msSql As String

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)

        btnRefresh.Enabled = True
        dtpRtrFrom.Checked = False
        dtpRtrTo.Checked = False
        dtpReqFrom.Checked = False
        dtpReqTo.Checked = False

        txtagreementno.Text = ""
        txtgnsarefno.Text = ""
        cboRetrieveMode.SelectedIndex = -1
        cboDisc.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
        txtChqNo.Text = ""
        dgvRpt.DataSource = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmPacketReport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvRpt.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmPacketReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmPacketReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpImpFrom.Value = Now
        dtpImpTo.Value = Now

        dtpImpFrom.Checked = False
        dtpImpTo.Checked = False

        dtpReqFrom.Value = Now
        dtpReqTo.Value = Now

        dtpReqFrom.Checked = False
        dtpReqTo.Checked = False

        dtpRtrFrom.Value = Now
        dtpRtrTo.Value = Now

        dtpRtrFrom.Checked = False
        dtpRtrTo.Checked = False

        With cboStatus
            .Items.Clear()
            .Items.Add("Pending")
            .Items.Add("Retrieved")
            .Items.Add("Missing")
        End With

        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
    End Sub


    Private Sub frmPacketReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            With dgvRpt
                .Width = Me.Width - 30
                .Height = Me.Height - Panel1.Height - 90
                pnlDisplay.Width = Me.Width - 30
                pnlDisplay.Top = Panel1.Top + Panel1.Height + dgvRpt.Height + 15
                btnExport.Left = pnlDisplay.Width - (btnExport.Width + 10)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        btnRefresh.Enabled = False

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        Try
            If txtWorkItemNo.Text <> "" Then lsCond &= " and q.retrieval_gid = " & Val(txtWorkItemNo.Text) & " "

            If dtpImpFrom.Checked Then lsCond &= " and f.import_on >= '" & Format(dtpImpFrom.Value, "yyyy-MM-dd") & "'"
            If dtpImpTo.Checked Then lsCond &= " and f.import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImpTo.Value), "yyyy-MM-dd") & "'"

            If cboFile.SelectedIndex <> -1 Then lsCond &= " and f.file_gid = " & Val(cboFile.SelectedValue.ToString) & " "

            If dtpReqFrom.Checked Then lsCond &= " and q.retrieval_requestdate >= '" & Format(dtpReqFrom.Value, "yyyy-MM-dd") & "'"
            If dtpReqTo.Checked Then lsCond &= " and q.retrieval_requestdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpReqTo.Value), "yyyy-MM-dd") & "'"

            If dtpRtrFrom.Checked Then lsCond &= " and r.retrievalentry_insertdate >= '" & Format(dtpRtrFrom.Value, "yyyy-MM-dd") & "'"
            If dtpRtrTo.Checked Then lsCond &= " and r.retrievalentry_insertdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRtrTo.Value), "yyyy-MM-dd") & "'"

            If txtagreementno.Text.Trim <> "" Then
                If IsNumeric(txtagreementno.Text) = True Then
                    lsCond &= " and a.shortagreement_no = '" & QuoteFilter(txtagreementno.Text.Trim) & "'"
                Else
                    lsCond &= " and a.agreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%'"
                End If
            End If

            If txtgnsarefno.Text <> "" Then lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            If cboRetrieveMode.SelectedIndex <> -1 And cboRetrieveMode.Text.Trim <> "" Then lsCond &= " and q.retrieval_mode = '" & cboRetrieveMode.Text & "' "
            If txtChqNo.Text <> "" Then lsCond &= " and q.retrieval_chqno = " & txtChqNo.Text & " "
            If txtReason.Text <> "" Then lsCond &= " and q.retrieval_reason = '" & QuoteFilter(txtReason.Text) & "' "

            Select Case cboDisc.Text.ToUpper
                Case "YES"
                    lsCond &= " and q.retrieval_discflag = 'Y' "
                Case "NO"
                    lsCond &= " and q.retrieval_discflag = 'N' "
            End Select

            Select Case cboStatus.Text.ToUpper
                Case "PENDING"
                    lsCond &= " and q.retrieval_status = " & GCREQUESTED & " "
                Case "RETRIEVED"
                    lsCond &= " and q.retrieval_status & " & GCRETRIEVED & " > 0 "
                Case "MISSING"
                    lsCond &= " and q.retrieval_status & " & GCMISSING & " > 0 "
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select q.retrieval_gid as 'Work Item No',q.retrieval_requestdate as 'Req Date',q.retrieval_agreementno as 'Agreement No',q.retrieval_shortagreementno as 'Short Agreement No',"
            lsSql &= " q.retrieval_mode as 'Retrieval Mode',p.packet_gnsarefno as 'GNSA Ref No',q.retrieval_chqno as 'Chq No',"
            lsSql &= " q.retrieval_reason as 'Retrieval Reason',"
            lsSql &= " make_set(q.retrieval_status,'Request','Retrieved','Missing') as 'Retrival Status',"
            lsSql &= " e.almaraentry_cupboardno as 'Cupboard No',e.almaraentry_shelfno as 'Shelf No',e.almaraentry_boxno as 'Box No',"
            lsSql &= " q.retrieval_discflag as 'Disc Flag',q.retrieval_status as 'Retrieval Status Value',"
            lsSql &= " r.retrievalentry_insertdate as 'Retrieved Date',"
            lsSql &= " r.retrievalentry_insertby as 'Retrieved By',"
            lsSql &= " q.retrieval_agreement_gid as 'Agreement Id',q.retrieval_packet_gid as 'Packet Id',"
            lsSql &= " f.file_gid as 'File Id',f.file_name as 'File Name',f.import_on as 'Import Date',f.import_by as 'Import By' "
            lsSql &= " from chola_trn_tretrieval as q "
            lsSql &= " left join chola_mst_tfile as f on f.file_gid = q.retrieval_file_gid "
            lsSql &= " left join chola_trn_tretrievalentry as r on r.retrievalentry_retrieval_gid = retrieval_gid "
            lsSql &= " left join chola_trn_tpacket as p on p.packet_gid = q.retrieval_packet_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = q.retrieval_agreement_gid "
            lsSql &= " left join chola_trn_almaraentry as e on e.almaraentry_gid = p.packet_box_gid "
            lsSql &= " where true "
            lsSql &= lsCond

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            For i = 0 To dgvRpt.Columns.Count - 1
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvRpt.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If dgvRpt.RowCount = 0 Then
                MsgBox("No Details to Export!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            Call PrintDGViewXML(dgvRpt, gsReportPath & "Packet Report.xls", "Packet Details")

            MsgBox(" Exported to Excel !!" & Chr(13) & _
                   " Saved Path : " & gsReportPath & "Packet Report", MsgBoxStyle.Information, gProjectName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboFile_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFile.GotFocus
        msSql = ""
        msSql &= " select file_gid,file_name from chola_mst_tfile "
        msSql &= " where true "
        msSql &= IIf(dtpImpFrom.Checked = True, " and import_on >= '" & Format(dtpImpFrom.Value, "yyyy-MM-dd") & "' ", "")
        msSql &= IIf(dtpImpTo.Checked = True, " and import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImpTo.Value), "yyyy-MM-dd") & "' ", "")

        Call gpBindCombo(msSql, "file_name", "swapfile_gid", cboFile, gOdbcConn)
    End Sub
End Class