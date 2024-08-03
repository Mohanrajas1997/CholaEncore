Public Class frmPacketBlockReport
    Dim msSql As String

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
        btnRefresh.Enabled = True
        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False
        dtpPulloutFrom.Checked = False
        dtpPulloutTo.Checked = False
        txtagreementno.Text = ""
        txtgnsarefno.Text = ""
        cbopaymode.SelectedIndex = -1
        cboDisc.SelectedIndex = -1
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
        dtpPulloutFrom.Value = Now
        dtpPulloutTo.Value = Now

        dtpPulloutFrom.Checked = False
        dtpPulloutTo.Checked = False

        dtpEntryFrom.Value = Now
        dtpEntryTo.Value = Now

        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False

        dtpDelFrom.Value = Now
        dtpDelTo.Value = Now

        dtpDelFrom.Checked = False
        dtpDelTo.Checked = False

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
            If dtpEntryFrom.Checked Then lsCond &= " and p.packetblock_entryon >= '" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked Then lsCond &= " and p.packetblock_entryon < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpDelFrom.Checked Then lsCond &= " and p.packetblock_deleteon >= '" & Format(dtpDelFrom.Value, "yyyy-MM-dd") & "'"
            If dtpDelTo.Checked Then lsCond &= " and p.packetblock_deleteon < '" & Format(DateAdd(DateInterval.Day, 1, dtpDelTo.Value), "yyyy-MM-dd") & "'"

            If dtpPulloutFrom.Checked Then lsCond &= " and o.packetpullout_postedon >= '" & Format(dtpPulloutFrom.Value, "yyyy-MM-dd") & "'"
            If dtpPulloutTo.Checked Then lsCond &= " and o.packetpullout_postedon < '" & Format(DateAdd(DateInterval.Day, 1, dtpPulloutTo.Value), "yyyy-MM-dd") & "'"

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

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Packet' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            lsSql = ""
            lsSql &= " select "
            lsSql &= " p.packetblock_sno as 'Packet Block SNo',"
            lsSql &= " a.packet_gnsarefno as 'GNSA Ref No',c.shortagreement_no as 'Short Agreement No',"
            lsSql &= " p.packetblock_reason as 'Packet Pullout Reason',"
            lsSql &= " c.agreement_no as 'Agreement No',c.agreement_closeddate as 'Closed Date',"
            lsSql &= " p.packetblock_entryon as 'Entry On',"
            lsSql &= " p.packetblock_entryby as 'Entry By',"
            lsSql &= " p.packetblock_deleteon as 'Delete On',"
            lsSql &= " p.packetblock_deleteby as 'Delete By',"
            lsSql &= " o.packetpullout_postedon as 'Pullout On',"
            lsSql &= " o.packetpullout_postedby as 'Pullout By',"

            lsSql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid = packet_gid and chq_status & " & GCPACKETPULLOUT & " > 0) as 'Pdc Packet Pullout',"
            lsSql &= " (select count(*) from chola_trn_tspdcchqentry where chqentry_packet_gid = packet_gid and chqentry_status & " & GCPACKETPULLOUT & " > 0) as 'Spdc Packet Pullout',"
            lsSql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid = packet_gid and chq_status & " & GCLOOSECHQ & " > 0) as 'Loose Chq',"
            lsSql &= " s.spdcentry_spdccount as 'SPDC Count',s.spdcentry_ecsmandatecount as 'Mandate Count',"
            lsSql &= " s.spdcentry_ctschqcount as 'SPDC CTS Count',s.spdcentry_nonctschqcount as 'SPDC Non CTS Count',"
            lsSql &= " s.spdcentry_accountno as 'SPDC A/C No',s.spdcentry_micrcode as 'SPDC Micr',"
            lsSql &= " s.spdcentry_startdate as 'Ecs Start Date',s.spdcentry_enddate as 'Ecs End Date',"
            lsSql &= " s.spdcentry_ecsamount as 'Ecs Amount',s.spdcentry_remarks as 'SPDC Remark' "

            lsSql &= " from chola_trn_tpacketblock as p "
            lsSql &= " inner join chola_trn_tpacket as a on a.packet_gid = p.packetblock_packet_gid "
            lsSql &= " inner join chola_mst_tagreement c on a.packet_agreement_gid=c.agreement_gid "
            lsSql &= " left join chola_trn_tspdcentry as s on s.spdcentry_packet_gid = a.packet_gid "
            lsSql &= " left join chola_trn_tpacketpullout as o on o.packetpullout_packet_gid = a.packet_gid "
            lsSql &= " where true "
            lsSql &= lsCond

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
End Class