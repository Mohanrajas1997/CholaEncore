Public Class frmPulloutReverseSummary
    Dim lssql As String
    Private Sub frmsummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtagreementno.Focus()
        txtagreementno.Text = ""

        lssql = " select reason_gid,reason_name from chola_mst_tpulloutreason where reason_deleteflag='N' "
        gpBindCombo(lssql, "reason_name", "reason_gid", cboreason, gOdbcConn)
        cboreason.SelectedIndex = -1
    End Sub

    Private Sub frmsummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        lsSql = " "
        lsSql &= " SELECT pullout_gid,pullout_entrygid,pullout_shortagreementno as 'Agreement Number',"
        lsSql &= " pullout_chqno as 'Cheque No',date_format(pullout_chqdate,'%d-%m-%Y') as 'Cheque Date',"
        lsSql &= " pullout_chqamount as 'Cheque Amount',reason_name as 'Reason',date_format(pullout_insertdate,'%d-%m-%Y') as 'Pullout Date',"
        lsSql &= " pullout_insertby as 'Pullout By' "
        lsSql &= " FROM chola_trn_tpullout as tf"
        lsSql &= " inner join chola_mst_tpulloutreason on reason_gid=pullout_reasongid "
        lsSql &= " where 1=1 "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(pullout_insertdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(pullout_insertdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtagreementno.Text <> "" Then
            lsSql &= " and pullout_shortagreementno='" & txtagreementno.Text & "'"
        End If

        If txtchqno.Text.Trim <> "" Then
            lsSql &= " and pullout_chqno='" & txtchqno.Text.Trim & "'"
        End If

        If cboreason.Text.Trim <> "" Then
            lsSql &= " and pullout_reasongid=" & cboreason.SelectedValue
        End If

        lsSql &= " order by pullout_insertdate"

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            Dim dgButtonColumn2 As New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "Delete"
            dgButtonColumn2.Name = "Delete"
            dgButtonColumn2.ToolTipText = "Delete"
            dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn2.FlatStyle = FlatStyle.System
            dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
            .Columns.Add(dgButtonColumn2)
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub


    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtagreementno.Text = ""
        dgvsummary.DataSource = Nothing
        dgvsummary.Columns.Clear()
        lbltotrec.Text = ""
        txtchqno.Text = ""
        cboreason.SelectedIndex = -1
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Try
            Dim lnPdcId As Long
            Dim lnPulloutId As Long

            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Delete" Then
                        If MsgBox("Are You Sure Want to Delete", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        lnPdcId = dgvsummary.CurrentRow.Cells("pullout_entrygid").Value.ToString
                        lnPulloutId = dgvsummary.CurrentRow.Cells("pullout_gid").Value.ToString

                        'Reverse Pulout Entry
                        lssql = ""
                        lssql &= " update chola_trn_tpdcentry "
                        lssql &= " set chq_status = ( chq_status | 256 |64 | 32 ) ^ (256 | 64 | 32) "
                        lssql &= " where entry_gid = " & lnPdcId

                        gfInsertQry(lssql, gOdbcConn)

                        'Copy to Pullout delete table
                        lssql = ""
                        lssql &= " insert into chola_trn_tpulloutdel "
                        lssql &= " select * from chola_trn_tpullout "
                        lssql &= " where pullout_gid = " & lnPulloutId
                        gfInsertQry(lssql, gOdbcConn)

                        'Reverse From Pullout Table
                        lssql = ""
                        lssql &= " delete from chola_trn_tpullout "
                        lssql &= " where pullout_gid = " & lnPulloutId

                        gfInsertQry(lssql, gOdbcConn)
                        LoadData()
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class