Public Class frmchqentrydelete
    Dim lssql As String
    Private Sub frmchqentrydelete_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmchqentrydelete_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmchqentrydelete_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtShortAgreementNo.Focus()
        txtShortAgreementNo.Text = ""

        lssql = ""
        lssql &= " select type_flag,type_name "
        lssql &= " from chola_mst_ttype "
        lssql &= " where type_deleteflag='N' "
        lssql &= " order by type_name "

        gpBindCombo(lssql, "type_name", "type_flag", cboproduct, gOdbcConn)
        cboproduct.SelectedIndex = -1
    End Sub

    Private Sub frmchqentrydelete_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

        lsSql = ""
        lsSql &= " select chqentry_gid,@slno:= @slno + 1 as 'SL No',packet_gnsarefno as 'GNSA REF#',"
        lsSql &= " shortagreement_no as 'Short Agreement No',agreement_no as 'Agreement No',"
        lsSql &= " chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',chq_amount as 'Amount',"
        lsSql &= " if(chq_papflag='Y','PAP','NON PAP') as 'PAP/NON PAP',chqentry_isactive as 'Is Active' "
        lsSql &= " from chola_trn_chqentry  "
        lsSql &= " inner join chola_trn_tpdcentry on entry_gid=chqentry_entry_gid "
        lsSql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lsSql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lsSql &= " where chqentry_systemip='" & IPAddresses(Net.Dns.GetHostName) & "'"
        lsSql &= " and chqentry_entryby='" & gUserName & "'"
        lsSql &= " and chqentry_batch_gid = 0 "

        If dtpcycledate.Checked = True Then
            lsSql &= " and chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        End If

        If txtShortAgreementNo.Text <> "" Then
            lsSql &= " and shortagreement_no='" & txtShortAgreementNo.Text & "'"
        End If

        If txtAgreementNo.Text <> "" Then
            lsSql &= " and agreement_no='" & txtAgreementNo.Text & "'"
        End If

        If txtchqno.Text.Trim <> "" Then
            lsSql &= " and chq_no='" & txtchqno.Text.Trim & "'"
        End If

        If cboproduct.Text.Trim <> "" Then
            lsSql &= " and chq_prodtype=" & cboproduct.SelectedValue
        End If

        lsSql &= " order by chqentry_gid "

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
        dtpcycledate.Value = Now()
        dtpcycledate.Checked = False
        txtShortAgreementNo.Text = ""
        dgvsummary.DataSource = Nothing
        dgvsummary.Columns.Clear()
        lbltotrec.Text = ""
        txtchqno.Text = ""
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Try
            Dim lnChqentryId As Long

            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Delete" Then
                        If MsgBox("Are You Sure Want to Delete", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        lnChqentryId = dgvsummary.CurrentRow.Cells("chqentry_gid").Value.ToString

                        lssql = ""
                        lssql &= " delete from chola_trn_chqentry "
                        lssql &= " where chqentry_gid = " & lnChqentryId

                        gfInsertQry(lssql, gOdbcConn)
                        LoadData()
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class