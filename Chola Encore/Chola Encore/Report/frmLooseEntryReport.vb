Public Class frmLooseEntryReport
    Private Sub frmPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dtpChqFrom.Value = Now
            dtpChqTo.Value = Now

            dtpChqFrom.Checked = False
            dtpChqTo.Checked = False

            dtpEntryFrom.Value = Now
            dtpEntryTo.Value = Now

            dtpEntryFrom.Checked = False
            dtpEntryTo.Checked = False

            dtpClosedFrom.Value = Now
            dtpClosedTo.Value = Now

            dtpClosedFrom.Checked = False
            dtpClosedTo.Checked = False

            dtpChqFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal l As System.EventArgs) Handles btnRefresh.Click
        Dim lsSql As String
        Dim lsTxt As String
        Dim lsCond As String

        Try
            lsCond = ""

            If dtpEntryFrom.Checked = True Then lsCond &= " and l.loosechqentry_insertdate >='" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked = True Then lsCond &= " and l.loosechqentry_insertdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpChqFrom.Checked = True Then lsCond &= " and c.chq_date >='" & Format(dtpChqFrom.Value, "yyyy-MM-dd") & "'"
            If dtpChqTo.Checked = True Then lsCond &= " and c.chq_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpChqTo.Value), "yyyy-MM-dd") & "'"

            If dtpClosedFrom.Checked = True Then lsCond &= " and a.agreement_closeddate >='" & Format(dtpClosedFrom.Value, "yyyy-MM-dd") & "'"
            If dtpClosedTo.Checked = True Then lsCond &= " and a.agreement_closeddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpClosedTo.Value), "yyyy-MM-dd") & "'"

            If txtChqNo.Text.Trim <> "" Then
                lsCond &= " and c.chq_no like '" & QuoteFilter(txtChqNo.Text) & "%'"
            End If

            If txtChqAmt.Text.Trim <> "" Then
                lsCond &= " and c.chq_amount = " & Val(txtChqAmt.Text)
            End If

            If txtAgmtNo.Text.Trim <> "" Then
                If IsNumeric(txtAgmtNo.Text) = True Then
                    lsCond &= " and a.shortagreement_no = '" & QuoteFilter(txtAgmtNo.Text) & "'"
                Else
                    lsCond &= " and a.agreement_no like '" & QuoteFilter(txtAgmtNo.Text) & "%'"
                End If
            End If

            If txtPdcId.Text <> "" Then lsCond &= " and l.loosechqentry_pdcgid = " & Val(txtPdcId.Text) & " "
            If txtLooseId.Text <> "" Then lsCond &= " and l.loosechqentry_gid = " & Val(txtLooseId.Text) & " "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            ' Pdc
            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Cheque' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            lsSql = ""
            lsSql &= " select l.loosechqentry_sno as 'Loose Chq SNo',c.chq_date as 'Chq Date',c.chq_no as 'Chq No',c.chq_amount as 'Chq Amount',"
            lsSql &= " a.shortagreement_no as 'Short Agreement No',l.loosechqentry_remarks as 'Remark',"
            lsSql &= " make_set(c.chq_status," & lsTxt & ") as 'Chq Status',"
            lsSql &= " c.chq_status as 'Chq Status Value',"
            lsSql &= " a.agreement_no as 'Agreement No',"
            lsSql &= " l.loosechqentry_insertdate as 'Entry Date',l.loosechqentry_insertby as 'Entry By',"
            lsSql &= " l.loosechqentry_pdcgid as 'Pdc Id',l.loosechqentry_gid as 'Loose Chq Id',"
            lsSql &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsSql &= " a.agreement_closeddate as 'Closed Date'"
            lsSql &= " from chola_trn_tloosechqentry as l "
            lsSql &= " left join chola_trn_tpdcentry as c on c.entry_gid = l.loosechqentry_pdcgid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = k.packet_agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and loosechqentry_deleteflag = 'N' "

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