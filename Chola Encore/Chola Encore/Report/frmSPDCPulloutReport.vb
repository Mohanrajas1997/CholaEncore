Public Class frmSPDCPulloutReport
    Private Sub frmPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        Try
            dtpEntryFrom.Value = Now
            dtpEntryTo.Value = Now

            dtpEntryFrom.Checked = False
            dtpEntryTo.Checked = False

            dtpClosedFrom.Value = Now
            dtpClosedTo.Value = Now

            dtpClosedFrom.Checked = False
            dtpClosedTo.Checked = False

            lsSql = ""
            lsSql &= " select * from chola_mst_tpulloutreason "
            lsSql &= " where true "
            lsSql &= " order by reason_name "

            Call gpBindCombo(lsSql, "reason_name", "reason_gid", cboReason, gOdbcConn)

            dtpEntryFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal p As System.EventArgs) Handles btnRefresh.Click
        Dim lsSql As String
        Dim lsCond As String

        Try
            lsCond = ""

            If dtpEntryFrom.Checked = True Then lsCond &= " and p.spdcpullout_insertdate >='" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked = True Then lsCond &= " and p.spdcpullout_insertdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpClosedFrom.Checked = True Then lsCond &= " and a.agreement_closeddate >='" & Format(dtpClosedFrom.Value, "yyyy-MM-dd") & "'"
            If dtpClosedTo.Checked = True Then lsCond &= " and a.agreement_closeddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpClosedTo.Value), "yyyy-MM-dd") & "'"

            If cboReason.SelectedIndex <> -1 Then lsCond &= " and r.reason_gid = '" & QuoteFilter(cboReason.SelectedValue) & "'"

            If txtChqNo.Text.Trim <> "" Then
                lsCond &= " and p.spdcpullout_chqno like '" & QuoteFilter(txtChqNo.Text) & "%'"
            End If

            If IsNumeric(txtAgmtNo.Text) = True Then
                If txtAgmtNo.Text.Trim <> "" Then lsCond &= " and a.shortagreement_no = '" & QuoteFilter(txtAgmtNo.Text) & "'"
            Else
                If txtAgmtNo.Text.Trim <> "" Then lsCond &= " and a.agreement_no like '" & QuoteFilter(txtAgmtNo.Text) & "%'"
            End If

            If txtSpdcId.Text <> "" Then lsCond &= " and p.spcpullout_chqentrygid = " & Val(txtSpdcId.Text) & " "
            If txtSpdcPulloutId.Text <> "" Then lsCond &= " and p.spdcpullout_gid = " & Val(txtSpdcPulloutId.Text) & " "

            If chkChqQry.Checked = True Then lsCond &= " and q.import_by = '" & gUserName & "' "

            lsSql = ""
            lsSql &= " select p.spdcpullout_insertdate as 'Entry Date',p.spdcpullout_shortagreementno as 'Short Agreement No',"
            lsSql &= " p.spdcpullout_chqno as 'Chq No',p.spdcpullout_remarks as 'Pullout Remark',"
            lsSql &= " r.reason_name as 'Pullout Reason',"
            lsSql &= " a.agreement_no as 'Agreement No',"
            lsSql &= " p.spdcpullout_chqentrygid as 'Spdc Id',p.spdcpullout_gid as 'SPDC Pullout Id',"
            lsSql &= " p.spdcpullout_reasongid as 'Pullout Reason Id',p.spdcpullout_insertby as 'Entry By',"
            lsSql &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsSql &= " a.agreement_closeddate as 'Closed Date'"

            If chkChqQry.Checked = False Then
                lsSql &= " from chola_trn_tspdcpullout as p "

                If lsCond = "" Then lsCond &= " and 1 = 2 "
            Else
                lsSql &= " from chola_rpt_tchqqry as q "
                lsSql &= " inner join chola_trn_tspdcpullout as p on p.spdcpullout_chqno = q.chq_no "
            End If

            lsSql &= " left join chola_mst_tpulloutreason as r on r.reason_gid = p.spdcpullout_reasongid "
            lsSql &= " left join chola_trn_tspdcchqentry as c on c.chqentry_gid = p.spdcpullout_chqentrygid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chqentry_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = k.packet_agreement_gid "
            lsSql &= " where true " & lsCond & " "
            lsSql &= " and spdcpullout_deleteflag = 'N'"

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
End Class