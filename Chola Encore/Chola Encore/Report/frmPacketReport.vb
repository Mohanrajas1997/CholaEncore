Public Class frmPacketReport

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        btnRefresh.Enabled = True
        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False
        dtpRcvdFrom.Checked = False
        dtpRcvdTo.Checked = False
        txtagreementno.Text = ""
        txtgnsarefno.Text = ""
        cbopaymode.SelectedIndex = -1
        cboDisc.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
        cboVault.SelectedIndex = -1
        txtboxno.Text = ""
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
        dtpRcvdFrom.Value = Now
        dtpRcvdTo.Value = Now

        dtpRcvdFrom.Checked = False
        dtpRcvdTo.Checked = False

        dtpEntryFrom.Value = Now
        dtpEntryTo.Value = Now

        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False

        dtpClosedFrom.Value = Now
        dtpClosedTo.Value = Now

        dtpClosedFrom.Checked = False
        dtpClosedTo.Checked = False

        With cboStatus
            .Items.Clear()
            .Items.Add("Inward")
            .Items.Add("Authorized")
            .Items.Add("Rejected")
            .Items.Add("Packet Entry")
            .Items.Add("Packet Re-Entry")
            .Items.Add("Yet to Vault")
            .Items.Add("Vault")
            .Items.Add("Pullout ")
            .Items.Add("GNSA REF Changed")
            .Items.Add("AGRMT No Changed")
            .Items.Add("Retrieved")
            .Items.Add("Closed Available")
            .Items.Add("Pay Mode Blank")
            .Items.Add("Box Not Given")
        End With

        With cboQueue
            .Items.Clear()
            .Items.Add("Auth")
            .Items.Add("Packet Entry")
            .Items.Add("Yet to Vault")
            .Items.Add("Vault")
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
            If dtpEntryFrom.Checked Then lsCond &= " and a.packet_entryon >= '" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked Then lsCond &= " and a.packet_entryon < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpRcvdFrom.Checked Then lsCond &= " and a.packet_receiveddate >= '" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            If dtpRcvdTo.Checked Then lsCond &= " and a.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"

            If txtagreementno.Text.Trim <> "" Then lsCond &= " and c.agreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%'"

            If dtpClosedFrom.Checked Then lsCond &= " and c.agreement_closeddate >= '" & Format(dtpClosedFrom.Value, "yyyy-MM-dd") & "'"
            If dtpClosedTo.Checked Then lsCond &= " and c.agreement_closeddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpClosedTo.Value), "yyyy-MM-dd") & "'"

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

            If txtboxno.Text <> "" Then
                lsCond &= " and b.box_no= " & txtboxno.Text & " "
            End If

            Select Case cboQueue.Text
                Case "Auth"
                    lsCond &= " and a.packet_status & " & GCINWARDENTRY & " > 0 "
                    lsCond &= " and a.packet_status &" & GCREJECTENTRY & " = 0"
                    lsCond &= " and a.packet_status &" & GCAUTHENTRY & " = 0"
                Case "Packet Entry"
                    lsCond &= " and a.packet_status &" & GCAUTHENTRY & " > 0"
                    lsCond &= " and a.packet_status &" & GCPACKETCHEQUEENTRY & " = 0"
                Case "Yet to Vault"
                    lsCond &= " and a.packet_status &" & GCPACKETCHEQUEENTRY & " > 0"
                    lsCond &= " and a.packet_status &" & GCPACKETVAULTED & " = 0"
                    lsCond &= " and a.packet_status &" & GCPACKETPULLOUT & " = 0"
                    lsCond &= " and a.packet_status &" & GCPACKETRETRIEVAL & " = 0"
                Case "Vault"
                    lsCond &= " and a.packet_status &" & GCPACKETVAULTED & " > 0"
                    lsCond &= " and a.packet_status &" & GCPACKETPULLOUT & " = 0"
                    lsCond &= " and a.packet_status &" & GCPACKETRETRIEVAL & " = 0"
            End Select

            Select Case cboStatus.Text
                Case "Inward"
                    lsCond &= " and a.packet_status <=" & GCINWARDENTRY & ""
                Case "Authorized"
                    lsCond &= " and a.packet_status &" & GCAUTHENTRY & " > 0"
                    'lsCond &= " and a.packet_status &" & GCPACKETCHEQUEENTRY & " = 0"
                Case "Rejected"
                    lsCond &= " and a.packet_status &" & GCREJECTENTRY & " > 0"
                Case "Packet Entry"
                    lsCond &= " and a.packet_status &" & GCPACKETCHEQUEENTRY & " > 0"
                Case "Packet Re-Entry"
                    lsCond &= " and a.packet_status &" & GCPACKETCHEQUEREENTRY & ">0"
                Case "Yet to Vault"
                    lsCond &= " and a.packet_status &" & GCPACKETCHEQUEENTRY & " > 0"
                    lsCond &= " and a.packet_status &" & GCPACKETVAULTED & " = 0"
                Case "Vault"
                    lsCond &= " and a.packet_status &" & GCPACKETVAULTED & ">0"
                Case "Pullout"
                    lsCond &= " and a.packet_status &" & GCPACKETPULLOUT & ">0"
                Case "GNSA REF Changed"
                    lsCond &= " and a.packet_status &" & GCGNSAREFCHANGED & ">0"
                Case "AGRMT No Changed"
                    lsCond &= " and a.packet_status &" & GCAGREEMENTNOCHANGED & ">0"
                Case "Retrieved"
                    lsCond &= " and a.packet_status &" & GCPACKETRETRIEVAL & ">0"
                Case "Closed Available"
                    lsCond &= " and c.agreement_closeddate is not null "
                    lsCond &= " and a.packet_status &" & (GCPACKETPULLOUT Or GCPACKETRETRIEVAL) & " = 0"
                Case "Pay Mode Blank"
                    lsCond &= " and a.packet_mode = '' "
                Case "Box Not Given"
                    lsCond &= " and b.almaraentry_gid is null "
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Packet' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            lsSql = " select a.packet_receiveddate as 'Received Date',a.packet_entryon as 'Entry Date',"
            lsSql &= " a.packet_gnsarefno as 'GNSA Ref No',c.shortagreement_no as 'Short Agreement No',"
            lsSql &= " c.agreement_no as 'Agreement No',a.packet_mode as 'Pay Mode', "
            lsSql &= " a.packet_status as 'Packet Status',make_set(a.packet_status," & lsTxt & ") as 'Packet',"
            lsSql &= " c.agreement_closeddate as 'Closed Date',"
            lsSql &= " a.packet_paymodedisc as 'Paymode Disc',a.packet_remarks as 'Remark',a.packet_entryby as 'Entry By',"
            lsSql &= " b.almaraentry_cupboardno as 'Cup Board No',"
            lsSql &= " b.almaraentry_shelfno as 'Shelf No',"
            lsSql &= " b.almaraentry_boxno as 'Box No',a.packet_ismultiplebank as 'Multiple Bank',"
            lsSql &= " s.spdcentry_spdccount as 'SPDC Count',s.spdcentry_ecsmandatecount as 'Mandate Count',"
            lsSql &= " s.spdcentry_ctschqcount as 'SPDC CTS Count',s.spdcentry_nonctschqcount as 'SPDC Non CTS Count',"
            lsSql &= " s.spdcentry_accountno as 'SPDC A/C No',s.spdcentry_micrcode as 'SPDC Micr',"
            lsSql &= " s.spdcentry_startdate as 'Ecs Start Date',s.spdcentry_enddate as 'Ecs End Date',"
            lsSql &= " s.spdcentry_ecsamount as 'Ecs Amount',s.spdcentry_remarks as 'SPDC Remark',"
            lsSql &= " a.packet_gid as 'Packet Id',a.packet_inward_gid as 'Inward Id',c.agreement_gid as 'Agreement Id',"
            lsSql &= " b.almaraentry_gid as 'Box Id',s.spdcentry_gid as 'SPDC Entry Id' "
            lsSql &= " from chola_trn_tpacket a"

            lsSql &= " left join chola_trn_almaraentry b on a.packet_box_gid=b.almaraentry_gid"
            lsSql &= " left join chola_mst_tagreement c on a.packet_agreement_gid=c.agreement_gid "
            lsSql &= " left join chola_trn_tspdcentry as s on s.spdcentry_packet_gid = a.packet_gid "

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

End Class