Public Class frmBounceEntryReport
    Private Sub frmPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        Try
            dtpChqFrom.Value = Now
            dtpChqTo.Value = Now

            dtpChqFrom.Checked = False
            dtpChqTo.Checked = False

            dtpEntryFrom.Value = Now
            dtpEntryTo.Value = Now

            dtpEntryFrom.Checked = False
            dtpEntryTo.Checked = False

            dtpRtnFrom.Value = Now
            dtpRtnTo.Value = Now

            dtpRtnFrom.Checked = False
            dtpRtnTo.Checked = False

            dtpClosedFrom.Value = Now
            dtpClosedTo.Value = Now

            dtpClosedFrom.Checked = False
            dtpClosedTo.Checked = False

            dtpPulloutFrom.Value = Now
            dtpPulloutTo.Value = Now

            dtpPulloutFrom.Checked = False
            dtpPulloutTo.Checked = False

            lsSql = ""
            lsSql &= " select * from chola_mst_tbouncereason "
            lsSql &= " where true "
            lsSql &= " order by reason_name "

            Call gpBindCombo(lsSql, "reason_name", "reason_gid", cboReason, gOdbcConn)

            With cboStatus
                .Items.Clear()
                .Items.Add("Matched With Pdc")
                .Items.Add("Not Matched With Pdc")
                .Items.Add("Pullout")
                .Items.Add("Matched With Inward")
                .Items.Add("Not Matched With Inward")
            End With

            dtpChqFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim lsSql As String
        Dim lsCond As String

        Try
            lsCond = ""

            If dtpEntryFrom.Checked = True Then lsCond &= " and e.bounceentry_insertdate >='" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked = True Then lsCond &= " and e.bounceentry_insertdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpChqFrom.Checked = True Then lsCond &= " and e.bounceentry_chqdate >='" & Format(dtpChqFrom.Value, "yyyy-MM-dd") & "'"
            If dtpChqTo.Checked = True Then lsCond &= " and e.bounceentry_chqdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpChqTo.Value), "yyyy-MM-dd") & "'"

            If dtpRtnFrom.Checked = True Then lsCond &= " and b.bounce_returndate >='" & Format(dtpRtnFrom.Value, "yyyy-MM-dd") & "'"
            If dtpRtnTo.Checked = True Then lsCond &= " and b.bounce_returndate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRtnTo.Value), "yyyy-MM-dd") & "'"

            If dtpClosedFrom.Checked = True Then lsCond &= " and a.agreement_closeddate >='" & Format(dtpClosedFrom.Value, "yyyy-MM-dd") & "'"
            If dtpClosedTo.Checked = True Then lsCond &= " and a.agreement_closeddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpClosedTo.Value), "yyyy-MM-dd") & "'"

            If dtpPulloutFrom.Checked = True Then lsCond &= " and p.bouncepullout_insertdate >='" & Format(dtpPulloutFrom.Value, "yyyy-MM-dd") & "'"
            If dtpPulloutTo.Checked = True Then lsCond &= " and p.bouncepullout_insertdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpPulloutTo.Value), "yyyy-MM-dd") & "'"

            If cboReason.SelectedIndex <> -1 Then lsCond &= " and r.reason_gid = '" & QuoteFilter(cboReason.SelectedValue) & "'"

            If txtChqNo.Text.Trim <> "" Then
                lsCond &= " and e.bounceentry_chqno like '" & QuoteFilter(txtChqNo.Text) & "%'"
            End If

            If txtBounceSNo.Text <> "" Then
                lsCond &= " and e.bounceentry_slno = " & Val(txtBounceSNo.Text) & " "
            End If

            If txtChqAmt.Text.Trim <> "" Then
                lsCond &= " and e.bounceentry_chqamount = " & Val(txtChqAmt.Text)
            End If

            If txtAgmtNo.Text.Trim <> "" Then lsCond &= " and a.shortagreement_no like '" & QuoteFilter(txtAgmtNo.Text) & "%'"
            If txtAWBNo.Text <> "" Then lsCond &= " and b.bounce_awbno like '" & QuoteFilter(txtAWBNo.Text) & "%' "
            If txtBounceId.Text <> "" Then lsCond &= " and b.bounce_gid = " & Val(txtBounceId.Text) & " "
            If txtBounceEntryId.Text <> "" Then lsCond &= " and e.bounceentry_gid = " & Val(txtBounceEntryId.Text) & " "
            If txtPulloutId.Text <> "" Then lsCond &= " and e.bounceentry_pullout_gid = " & Val(txtPulloutId.Text) & " "

            Select Case cboStatus.Text
                Case "Matched With Pdc"
                    lsCond &= " and e.bounceentry_entry_gid > 0 "
                Case "Not Matched With Pdc"
                    lsCond &= " and e.bounceentry_entry_gid = 0 "
                Case "Pullout"
                    lsCond &= " and e.bounceentry_pullout_gid > 0 "
                Case "Matched With Inward"
                    lsCond &= " and e.bounceentry_bounce_gid > 0 "
                Case "Not Matched With Inward"
                    lsCond &= " and e.bounceentry_bounce_gid = 0 "
            End Select

            If chkChqQry.Checked = True Then lsCond &= " and q.import_by = '" & gUserName & "' "

            lsSql = ""
            lsSql &= " select e.bounceentry_slno as 'Bounce SNo',e.bounceentry_insertdate as 'Entry Date',"
            lsSql &= " e.bounceentry_chqno as 'Chq No',e.bounceentry_chqdate as 'Chq Date',e.bounceentry_chqamount as 'Chq Amount',"
            lsSql &= " b.bounce_agreementno as 'Bounce Agreement No',a.agreement_no as 'Agreement No',b.bounce_returndate as 'Return Date',r.reason_name,b.bounce_awbno as 'AWB No',"
            lsSql &= " e.bounceentry_remarks as 'Bounce Remark',e.bounceentry_insertby as 'Entry By',"
            lsSql &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsSql &= " p.bouncepullout_insertdate as 'Pullout Date',p.bouncepullout_insertby as 'Pullout By',p.bouncepullout_remarks as 'Pullout Remark',"
            lsSql &= " a.agreement_closeddate as 'Closed Date',b.bounce_gid as 'Bounce Id',e.bounceentry_gid as 'Bounce Entry Id',e.bounceentry_pullout_gid as 'Pullout Id',"
            lsSql &= " e.bounceentry_entry_gid as 'Pdc Id',bounceentry_inward_gid as 'Bounce Inward Id' "

            If chkChqQry.Checked = False Then
                lsSql &= " from chola_trn_tbounceentry as e "

                If lsCond = "" Then lsCond &= " and 1 = 2 "
            Else
                lsSql &= " from chola_rpt_tchqqry as q "
                lsSql &= " inner join chola_trn_tbounceentry as e on e.bounceentry_chqno = q.chq_no "
            End If

            lsSql &= " left join chola_trn_tbounce as b on  b.bounce_gid = e.bounceentry_bounce_gid  "
            lsSql &= " left join chola_trn_tbouncepullout as p on p.bouncepullout_gid = e.bounceentry_pullout_gid "
            lsSql &= " left join chola_mst_tbouncereason as r on r.reason_gid = e.bounceentry_bouncereason_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = e.bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_tpdcentry as c on c.entry_gid = e.bounceentry_entry_gid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " where true " & lsCond & " "

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            dgvRpt.AutoResizeColumns()

            lblRecCount.Text = "Record Count: " & dgvRpt.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        MyBase.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmPaymentReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            With dgvRpt
                .Width = Me.Width - 30
                .Height = Me.Height - pnlButtons.Height - 90
                pnlDisplay.Width = Me.Width - 30
                pnlDisplay.Top = pnlButtons.Top + pnlButtons.Height + dgvRpt.Height + 15
                btnExport.Left = pnlDisplay.Width - (btnExport.Width + 10)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False
        dtpChqFrom.Checked = False
        dtpChqTo.Checked = False

        cboReason.Text = ""
        cboReason.SelectedIndex = -1

        Call frmCtrClear(Me)

        dgvRpt.DataSource = Nothing
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If dgvRpt.RowCount = 0 Then
                MsgBox("No Details to Export!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
            PrintDGViewXML(dgvRpt, gsReportPath & "Payment Report.xls", "Payment Details")

            MsgBox(" Exported to Excel !!" & Chr(13) & _
                   " Saved Path : " & gsReportPath & "Payment Report", MsgBoxStyle.Information, gProjectName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtChqNo_keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        If gfIntEntryOnly(e) Then e.Handled = True
    End Sub

    Private Sub txtChqAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqAmt.KeyPress
        If gfAmtEntryOnly(e) Then e.Handled = True
    End Sub
End Class