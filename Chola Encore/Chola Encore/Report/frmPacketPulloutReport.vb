Public Class frmPacketPulloutReport
    Dim msSql As String

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        btnRefresh.Enabled = True
        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False
        dtpImpFrom.Checked = False
        dtpImpTo.Checked = False
        txtagreementno.Text = ""
        txtgnsarefno.Text = ""
        cbopaymode.SelectedIndex = -1
        cboDisc.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
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

        dtpEntryFrom.Value = Now
        dtpEntryTo.Value = Now

        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False

        With cboStatus
            .Items.Clear()
            .Items.Add("Retrieved")
            .Items.Add("Yet To Retrieve")
        End With

        With cboReport
            .Items.Clear()
            .Items.Add("Summary")
            .Items.Add("Detail")
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
        Dim lsTxt As String
        Dim lsCond As String = ""

        Try
            If dtpEntryFrom.Checked Then lsCond &= " and p.packetpullout_postedon >= '" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked Then lsCond &= " and p.packetpullout_postedon < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpImpFrom.Checked Then lsCond &= " and f.import_on >= '" & Format(dtpImpFrom.Value, "yyyy-MM-dd") & "'"
            If dtpImpTo.Checked Then lsCond &= " and f.import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImpTo.Value), "yyyy-MM-dd") & "'"

            If txtagreementno.Text.Trim <> "" Then lsCond &= " and c.agreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%'"

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and a.packet_gnsarefno ='" & txtgnsarefno.Text.Trim & "'"
            End If

            If Not (cbopaymode.SelectedIndex = -1 Or cbopaymode.Text.Trim = "") Then
                lsCond &= " and a.packet_mode ='" & cbopaymode.Text & "'"
            End If

            If Not (cboDisc.SelectedIndex = -1 Or cboDisc.Text.Trim = "") Then
                If (cboDisc.Text = "Yes") Then
                    cboDisc.Text = "Y"
                Else
                    cboDisc.Text = "N"
                End If
                lsCond &= " and a.packet_paymodedisc ='" & cboDisc.Text & "'"
            End If

            Select Case cboStatus.Text
                Case "Yet To Retrieve"
                    lsCond &= " and p.packetpullout_postedon is null "
                Case "Retrieved"
                    lsCond &= " and p.packetpullout_postedon is not null "
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Packet' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            Select Case cboReport.Text
                Case "Summary"
                    lsSql = ""
                    lsSql &= " select "
                    lsSql &= " a.packet_gnsarefno as 'GNSA Ref No',c.shortagreement_no as 'Short Agreement No',"
                    lsSql &= " p.packetpullout_reason as 'Packet Pullout Reason',"
                    lsSql &= " c.agreement_no as 'Agreement No',c.agreement_closeddate as 'Closed Date',"

                    lsSql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid = packet_gid and chq_status & " & GCPACKETPULLOUT & " > 0) as 'Pdc Packet Pullout',"
                    lsSql &= " (select count(*) from chola_trn_tspdcchqentry where chqentry_packet_gid = packet_gid and chqentry_status & " & GCPACKETPULLOUT & " > 0) as 'Spdc Packet Pullout',"

                    lsSql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid = packet_gid and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0) as 'Pdc Available',"
                    lsSql &= " (select count(*) from chola_trn_tspdcchqentry where chqentry_packet_gid = packet_gid and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0) as 'Spdc Available',"
                    lsSql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid = packet_gid and chq_status & " & GCLOOSECHQ & " > 0) as 'Loose Chq',"
                    'lsSql &= " (select group_concat(l.loosechqentry_sno) from chola_trn_tpdcentry as e inner join chola_trn_tloosechqentry as l on l.loosechqentry_pdcgid = e.entry_gid "
                    'lsSql &= " where e.chq_packet_gid = packet_gid and e.chq_status & " & GCLOOSECHQ & " > 0 and l.loosechqentry_deleteflag = 'N') as 'Loose Chq SNo',"
                    lsSql &= " s.spdcentry_spdccount as 'SPDC Count',s.spdcentry_ecsmandatecount as 'Mandate Count',"
                    lsSql &= " s.spdcentry_ctschqcount as 'SPDC CTS Count',s.spdcentry_nonctschqcount as 'SPDC Non CTS Count',"
                    lsSql &= " s.spdcentry_accountno as 'SPDC A/C No',s.spdcentry_micrcode as 'SPDC Micr',"
                    lsSql &= " s.spdcentry_startdate as 'Ecs Start Date',s.spdcentry_enddate as 'Ecs End Date',"
                    lsSql &= " s.spdcentry_ecsamount as 'Ecs Amount',s.spdcentry_remarks as 'SPDC Remark',"

                    lsSql &= " p.packetpullout_postflag as 'Post Flag',"
                    lsSql &= " p.packetpullout_postedon as 'Entry On',"
                    lsSql &= " p.packetpullout_postedby as 'Entry By',"
                    lsSql &= " p.packetpullout_undoon as 'Undo On',"
                    lsSql &= " p.packetpullout_undoby as 'Undo By',"
                    lsSql &= " f.import_on as 'Import On',"
                    lsSql &= " f.import_by as 'Import By',"
                    lsSql &= " f.file_name as 'File Name',"
                    lsSql &= " f.file_sheetname as 'Sheet Name',"
                    lsSql &= " f.file_gid as 'File Id' "
                    lsSql &= " from chola_trn_tpacketpullout as p "
                    lsSql &= " left join chola_mst_tfile as f on f.file_gid = p.packetpullout_file_gid "
                    lsSql &= " inner join chola_trn_tpacket as a on a.packet_gid = p.packetpullout_packet_gid "
                    lsSql &= " inner join chola_mst_tagreement c on a.packet_agreement_gid=c.agreement_gid "
                    lsSql &= " left join chola_trn_tspdcentry as s on s.spdcentry_packet_gid = a.packet_gid "
                    lsSql &= " where true "
                    lsSql &= lsCond
                    'lsSql &= " and p.packetpullout_postedon is not null "
                Case Else
                    lsSql = ""
                    lsSql &= " select "
                    lsSql &= " a.packet_gnsarefno as 'GNSA Ref No',c.shortagreement_no as 'Short Agreement No',"
                    lsSql &= " p.packetpullout_reason as 'Packet Pullout Reason',"
                    lsSql &= " p.packetpullout_postflag as 'Post Flag',"
                    lsSql &= " p.packetpullout_postedon as 'Entry On',"
                    lsSql &= " p.packetpullout_postedby as 'Entry By',"
                    lsSql &= " p.packetpullout_undoon as 'Undo On',"
                    lsSql &= " p.packetpullout_undoby as 'Undo By',"
                    lsSql &= " f.import_on as 'Import On',"
                    lsSql &= " f.import_by as 'Import By',"
                    lsSql &= " f.file_name as 'File Name',"
                    lsSql &= " f.file_sheetname as 'Sheet Name',"
                    lsSql &= " f.file_gid as 'File Id',"
                    lsSql &= " a.packet_receiveddate as 'Received Date',a.packet_entryon as 'Entry Date',c.agreement_no as 'Agreement No',"
                    lsSql &= " a.packet_mode as 'Pay Mode', "
                    lsSql &= " a.packet_status as 'Packet Status',make_set(a.packet_status," & lsTxt & ") as 'Packet',"
                    lsSql &= " a.packet_paymodedisc as 'Paymode Disc',a.packet_remarks as 'Remark',a.packet_entryby as 'Entry By',"
                    lsSql &= " b.box_no as 'Box No',a.packet_ismultiplebank as 'Multiple Bank',"
                    lsSql &= " s.spdcentry_spdccount as 'SPDC Count',s.spdcentry_ecsmandatecount as 'Mandate Count',"
                    lsSql &= " s.spdcentry_ctschqcount as 'SPDC CTS Count',s.spdcentry_nonctschqcount as 'SPDC Non CTS Count',"
                    lsSql &= " s.spdcentry_accountno as 'SPDC A/C No',s.spdcentry_micrcode as 'SPDC Micr',"
                    lsSql &= " s.spdcentry_startdate as 'Ecs Start Date',s.spdcentry_enddate as 'Ecs End Date',"
                    lsSql &= " s.spdcentry_ecsamount as 'Ecs Amount',s.spdcentry_remarks as 'SPDC Remark',"
                    lsSql &= " a.packet_gid as 'Packet Id',a.packet_inward_gid as 'Inward Id',c.agreement_gid as 'Agreement Id',"
                    lsSql &= " b.box_gid as 'Box Id',s.spdcentry_gid as 'SPDC Entry Id' "
                    lsSql &= " from chola_trn_tpacketpullout as p "
                    lsSql &= " left join chola_mst_tfile as f on f.file_gid = p.packetpullout_file_gid "
                    lsSql &= " left join chola_trn_tpacket as a on a.packet_gid = p.packetpullout_packet_gid "
                    lsSql &= " left join chola_trn_tbox b on a.packet_box_gid=b.box_gid"
                    lsSql &= " left join chola_mst_tagreement c on a.packet_agreement_gid=c.agreement_gid "
                    lsSql &= " left join chola_trn_tspdcentry as s on s.spdcentry_packet_gid = a.packet_gid "

                    lsSql &= " where true "
                    lsSql &= lsCond
            End Select

            dgvRpt.Columns.Clear()

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
        msSql &= " select file_gid,concat(file_name,'-',file_sheetname) as file_name from chola_mst_tfile "
        msSql &= " where true "
        msSql &= IIf(dtpImpFrom.Checked = True, " and import_on >= '" & Format(dtpImpFrom.Value, "yyyy-MM-dd") & "' ", "")
        msSql &= IIf(dtpImpTo.Checked = True, " and import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImpTo.Value), "yyyy-MM-dd") & "' ", "")

        Call gpBindCombo(msSql, "file_name", "file_gid", cboFile, gOdbcConn)
    End Sub

    Private Sub cboFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFile.SelectedIndexChanged

    End Sub
End Class