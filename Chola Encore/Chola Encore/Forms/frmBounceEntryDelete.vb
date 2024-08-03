Public Class frmBounceEntryDelete
    Private Sub frmPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim lsSql As String
        Dim lsCond As String
        Dim lobjBtn As New DataGridViewButtonColumn
        Dim n As Integer
        Dim i As Integer

        Try
            dgvRpt.DataSource = Nothing
            dgvRpt.Columns.Clear()

            lsCond = " and e.bounceentry_slno = " & Val(txtBounceSNo.Text) & " "

            lsSql = ""
            lsSql &= " select e.bounceentry_slno as 'Bounce SNo',e.bounceentry_insertdate as 'Entry Date',"
            lsSql &= " e.bounceentry_chqno as 'Chq No',e.bounceentry_chqdate as 'Chq Date',e.bounceentry_chqamount as 'Chq Amount',"
            lsSql &= " b.bounce_agreementno as 'Bounce Agreement No',a.agreement_no as 'Agreement No',b.bounce_returndate as 'Return Date',r.reason_name,b.bounce_awbno as 'AWB No',"
            lsSql &= " e.bounceentry_remarks as 'Bounce Remark',e.bounceentry_insertby as 'Entry By',"
            lsSql &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsSql &= " p.bouncepullout_insertdate as 'Pullout Date',p.bouncepullout_insertby as 'Pullout By',p.bouncepullout_remarks as 'Pullout Remark',"
            lsSql &= " a.agreement_closeddate as 'Closed Date',b.bounce_gid as 'Bounce Id',e.bounceentry_gid as 'Bounce Entry Id',e.bounceentry_pullout_gid as 'Pullout Id',"
            lsSql &= " e.bounceentry_entry_gid as 'Pdc Id',bounceentry_inward_gid as 'Bounce Inward Id' "
            lsSql &= " from chola_trn_tbounceentry as e "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql &= " left join chola_trn_tbounce as b on  b.bounce_gid = e.bounceentry_bounce_gid  "
            lsSql &= " left join chola_trn_tbouncepullout as p on p.bouncepullout_gid = e.bounceentry_pullout_gid "
            lsSql &= " left join chola_mst_tbouncereason as r on r.reason_gid = e.bounceentry_bouncereason_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = e.bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_tpdcentry as c on c.entry_gid = e.bounceentry_entry_gid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " where true " & lsCond & " "

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            For i = 0 To dgvRpt.Columns.Count - 1
                dgvRpt.Columns(i).ReadOnly = True
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            n = dgvRpt.ColumnCount

            lobjBtn.HeaderText = "Delete"
            dgvRpt.Columns.Add(lobjBtn)

            For i = 0 To dgvRpt.RowCount - 1
                dgvRpt.Rows(i).Cells(n).Value = "Delete"
            Next i

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

    Private Sub dgvRpt_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvRpt.CellContentClick
        Dim lsSql As String
        Dim lnResult As Long

        With dgvRpt
            Select Case e.ColumnIndex
                Case .ColumnCount - 1
                    If Val(.Rows(e.RowIndex).Cells("Pullout Id").Value.ToString) > 0 Then
                        MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
                        Exit Sub
                    End If

                    If MsgBox("Are you sure to delete ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                        ' Update in bounce entry
                        lsSql = ""
                        lsSql &= " delete from chola_trn_tbounceentry "
                        lsSql &= " where bounceentry_gid = " & Val(.Rows(e.RowIndex).Cells("Bounce Entry Id").Value.ToString)

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        ' Update bounce dump
                        lsSql = ""
                        lsSql &= " update chola_trn_tbounce set "
                        lsSql &= " bounce_isentry = 'N' "
                        lsSql &= " where bounce_gid = " & Val(.Rows(e.RowIndex).Cells("Bounce Id").Value.ToString)

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        ' Update in pdc table
                        lsSql = ""
                        lsSql &= " update chola_trn_tpdcentry set "
                        lsSql &= " chq_status = chq_status ^ " & GCBOUNCERECEIVED & " "
                        lsSql &= " where entry_gid=" & Val(.Rows(e.RowIndex).Cells("Pdc Id").Value.ToString) & " "
                        lsSql &= " and chq_status & " & GCBOUNCERECEIVED & " > 0 "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        MsgBox("Record deleted successfully !", MsgBoxStyle.Information, gProjectName)
                    End If
            End Select
        End With
    End Sub
End Class