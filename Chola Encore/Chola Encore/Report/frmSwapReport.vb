Public Class frmSwapReport

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        btnRefresh.Enabled = True
        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False
        txtagreementno.Text = ""
        txtgnsarefno.Text = ""
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
        dtpEntryFrom.Value = Now
        dtpEntryTo.Value = Now

        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False

        With cboStatus
            .Items.Clear()
            .Items.Add("Cancelled")
            .Items.Add("Pullout")
            .Items.Add("Transferred")
            .Items.Add("Progress")
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
        Dim lsSwap As String
        Dim lsCond As String = ""

        Try
            If dtpEntryFrom.Checked Then lsCond &= " and o.oldentry_date >= '" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked Then lsCond &= " and o.oldentry_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If txtagreementno.Text.Trim <> "" Then lsCond &= " and (c.agreement_no = '" & QuoteFilter(txtagreementno.Text.Trim) & "' or c.shortagreement_no = '" & QuoteFilter(txtagreementno.Text.Trim) & "') "
            If txtgnsarefno.Text <> "" Then lsCond &= " and a.packet_gnsarefno ='" & txtgnsarefno.Text.Trim & "'"
            If txtOldPktSNo.Text <> "" Then lsCond &= " and o.oldpacket_slno = " & Val(txtOldPktSNo.Text) & " "

            Select Case cboStatus.Text
                Case "Cancelled"
                    lsCond &= " and o.oldpacket_status & " & GCOLDSWAPPKTCANCELLED & " > 0 "
                Case "Pullout"
                    lsCond &= " and o.oldpacket_status &" & GCOLDSWAPPKTPULLOUT & " > 0"
                Case "Transferred"
                    lsCond &= " and o.oldpacket_status &" & GCOLDSWAPCHQTRANSFERED & " > 0"
                Case "Progress"
                    lsCond &= " and o.oldpacket_status = 0 "
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Swap' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsSwap = gfExecuteScalar(lsSql, gOdbcConn)
            lsSwap = "'" & lsSwap.Replace(",", "','") & "'"

            lsSql = " select o.oldpacket_slno as 'Old Swap Packet SNo',o.oldentry_date as 'Entry Date',c.agreement_no as 'Agreement No',"
            lsSql &= " a.packet_gnsarefno as 'GNSA Ref No',c.shortagreement_no as 'Short Agreement No',a.packet_mode as 'Pay Mode', "
            lsSql &= " n.packet_gnsarefno as 'New Packet GNSA Ref No',n.packet_mode as 'New Packet Pay Mode',"
            lsSql &= " make_set(o.oldpacket_status," & lsSwap & ") as 'Swap Packet Status',o.oldpacket_status as 'Swap Status',"
            lsSql &= " b.box_no as 'Box No',o.oldentry_by as 'Entry By',o.oldswappacket_gid as 'Old Swap Packet Id',"
            lsSql &= " a.packet_gid as 'Old Packet Id',o.newpacket_gid as 'New Packet Id',c.agreement_gid as 'Agreement Id',"
            lsSql &= " b.box_gid as 'Box Id',a.packet_inward_gid as 'Inward Id' "
            lsSql &= " from chola_trn_toldswappacket o "
            lsSql &= " left join chola_trn_tpacket a on a.packet_gid = o.oldpacket_gid "
            lsSql &= " left join chola_trn_tbox b on a.packet_box_gid=b.box_gid"
            lsSql &= " left join chola_mst_tagreement c on a.packet_agreement_gid=c.agreement_gid "
            lsSql &= " left join chola_trn_tpacket n on n.packet_gid = o.newpacket_gid "

            lsSql &= " where true "
            lsSql &= lsCond

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            For i = 0 To dgvRpt.Columns.Count - 1
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            dgvRpt.AutoResizeColumns()

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