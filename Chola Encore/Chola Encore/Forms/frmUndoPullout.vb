Public Class frmUndoPullout
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

            dtpClosedFrom.Value = Now
            dtpClosedTo.Value = Now

            dtpClosedFrom.Checked = False
            dtpClosedTo.Checked = False

            lsSql = ""
            lsSql &= " select * from chola_mst_tpulloutreason "
            lsSql &= " where true "
            lsSql &= " order by reason_name "

            Call gpBindCombo(lsSql, "reason_name", "reason_gid", cboReason, gOdbcConn)

            dtpChqFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal p As System.EventArgs) Handles btnRefresh.Click
        Dim ctrlBtn As DataGridViewButtonColumn
        Dim i As Integer
        Dim lsSql As String
        Dim lsCond As String

        Try
            lsCond = ""

            If dtpEntryFrom.Checked = True Then lsCond &= " and p.pullout_insertdate >='" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked = True Then lsCond &= " and p.pullout_insertdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpChqFrom.Checked = True Then lsCond &= " and c.chq_date >='" & Format(dtpChqFrom.Value, "yyyy-MM-dd") & "'"
            If dtpChqTo.Checked = True Then lsCond &= " and c.chq_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpChqTo.Value), "yyyy-MM-dd") & "'"

            If dtpClosedFrom.Checked = True Then lsCond &= " and a.agreement_closeddate >='" & Format(dtpClosedFrom.Value, "yyyy-MM-dd") & "'"
            If dtpClosedTo.Checked = True Then lsCond &= " and a.agreement_closeddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpClosedTo.Value), "yyyy-MM-dd") & "'"

            If cboReason.SelectedIndex <> -1 Then lsCond &= " and r.reason_gid = '" & QuoteFilter(cboReason.SelectedValue) & "'"

            If txtChqNo.Text.Trim <> "" Then
                lsCond &= " and c.chq_no like '" & QuoteFilter(txtChqNo.Text) & "%'"
            End If

            If txtChqAmt.Text.Trim <> "" Then
                lsCond &= " and p.pullout_chqamount = " & Val(txtChqAmt.Text)
            End If

            If txtAgmtNo.Text.Trim <> "" Then lsCond &= " and a.agreement_no like '" & QuoteFilter(txtAgmtNo.Text) & "%'"
            If txtShortAgreementNo.Text.Trim <> "" Then lsCond &= " and a.shortagreement_no like '" & QuoteFilter(txtShortAgreementNo.Text) & "%'"

            If txtPdcId.Text <> "" Then lsCond &= " and p.pullout_entrygid = " & Val(txtPdcId.Text) & " "
            If txtPulloutId.Text <> "" Then lsCond &= " and p.pullout_gid = " & Val(txtPulloutId.Text) & " "

            If chkChqQry.Checked = True Then lsCond &= " and q.import_by = '" & gUserName & "' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select p.pullout_insertdate as 'Entry Date',p.pullout_shortagreementno as 'Short Agreement No',"
            lsSql &= " p.pullout_chqdate as 'Chq Date',p.pullout_chqno as 'Chq No',p.pullout_chqamount as 'Chq Amount',"
            lsSql &= " r.reason_name as 'Pullout Reason',"
            lsSql &= " a.agreement_no as 'Agreement No',"
            lsSql &= " p.pullout_entrygid as 'Pdc Id',p.pullout_gid as 'Pullout Id',"
            lsSql &= " p.pullout_reasongid as 'Pullout Reason Id',p.pullout_insertby as 'Entry By',"
            lsSql &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsSql &= " a.agreement_closeddate as 'Closed Date'"

            If chkChqQry.Checked = False Then
                lsSql &= " from chola_trn_tpullout as p "

                If lsCond = "" Then lsCond &= " and 1 = 2 "
            Else
                lsSql &= " from chola_rpt_tchqqry as q "
                lsSql &= " inner join chola_trn_tpullout as p on p.pullout_chqno = q.chq_no "
            End If

            lsSql &= " left join chola_mst_tpulloutreason as r on r.reason_gid = p.pullout_reasongid "
            lsSql &= " left join chola_trn_tpdcentry as c on c.entry_gid = p.pullout_entrygid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = k.packet_agreement_gid "
            lsSql &= " where true " & lsCond & " "

            dgvRpt.Columns.Clear()

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            dgvRpt.AutoResizeColumns()

            For i = 0 To dgvRpt.ColumnCount - 1
                dgvRpt.Columns(i).ReadOnly = True
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            ctrlBtn = New DataGridViewButtonColumn

            With ctrlBtn
                .HeaderText = "Remove"
                .Name = "Remove"
                .Text = "Remove"
            End With

            dgvRpt.Columns.Insert(0, ctrlBtn)

            For i = 0 To dgvRpt.Rows.Count - 1
                dgvRpt.Rows(i).Cells("Remove").Value = "Remove"
            Next i

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

    Private Sub dgvRpt_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRpt.CellContentClick
        Dim lnPdcId As Long = 0
        Dim lnPulloutId As Long = 0
        Dim lsSql As String = ""

        With dgvRpt
            If .Columns(e.ColumnIndex).Name = "Remove" And e.RowIndex >= 0 Then
                If MsgBox("Are you sure to remove ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                    lnPdcId = .Rows(e.RowIndex).Cells("Pdc Id").Value
                    lnPulloutId = .Rows(e.RowIndex).Cells("Pullout Id").Value

                    'Reverse Pulout Entry
                    lsSql = ""
                    lsSql &= " update chola_trn_tpdcentry "
                    lsSql &= " set chq_status = ( chq_status | 256 |64 | 32 ) ^ (256 | 64 | 32) "
                    lsSql &= " where entry_gid = " & lnPdcId

                    Call gfInsertQry(lsSql, gOdbcConn)

                    'Copy to Pullout delete table
                    lsSql = ""
                    lsSql &= " insert into chola_trn_tpulloutdel "
                    lsSql &= " select * from chola_trn_tpullout "
                    lsSql &= " where pullout_gid = " & lnPulloutId

                    Call gfInsertQry(lsSql, gOdbcConn)

                    'Reverse From Pullout Table
                    lsSql = ""
                    lsSql &= " delete from chola_trn_tpullout "
                    lsSql &= " where pullout_gid = " & lnPulloutId

                    Call gfInsertQry(lsSql, gOdbcConn)

                    .Rows.RemoveAt(e.RowIndex)
                    MsgBox("Record removed successfully !", MsgBoxStyle.Information, gProjectName)
                End If
            End If
        End With
    End Sub
End Class