Public Class frmauthsummary
    Private Sub frmauthsummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmauthsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmauthsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtgnsarefno.Focus()
        txtgnsarefno.Text = ""
    End Sub

    Private Sub frmauthsummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub LoadData()
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select packet_gid,packet_gnsarefno as 'GNSA REF#',agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No' "
        lsSql &= " from chola_trn_tpacket "
        lsSql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lsSql &= " where packet_status & " & GCINWARDENTRY & " > 0 "
        lsSql &= " and packet_status & " & GCAUTHENTRY & " = 0 "
        lsSql &= " and packet_status & " & GCREJECTENTRY & " = 0 "

        If dtpfrom.Checked Then
            lsSql &= " and  date_format(packet_receiveddate,'%Y-%m-%d') >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked Then
            lsSql &= " and  date_format(packet_receiveddate,'%Y-%m-%d') <='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
        End If

        If txtgnsarefno.Text.Trim <> "" Then
            lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
        End If

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            Dim dgButtonColumn2 As New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "Auth"
            dgButtonColumn2.Name = "Auth"
            dgButtonColumn2.ToolTipText = "Auth"
            dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn2.FlatStyle = FlatStyle.System
            dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
            .Columns.Add(dgButtonColumn2)
            Dim dgButtonColumn3 As New DataGridViewButtonColumn
            dgButtonColumn3.HeaderText = ""
            dgButtonColumn3.UseColumnTextForButtonValue = True
            dgButtonColumn3.Text = "Reject"
            dgButtonColumn3.Name = "Reject"
            dgButtonColumn3.ToolTipText = "Reject"
            dgButtonColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn3.FlatStyle = FlatStyle.System
            dgButtonColumn3.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn3.DefaultCellStyle.ForeColor = Color.White
            .Columns.Add(dgButtonColumn3)
            .Columns(0).Visible = False
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtgnsarefno.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub
    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Try
            Dim lnGId As Long
            Dim lssql As String

            Select Case e.ColumnIndex
                Case Is > -1
                    lnGId = dgvsummary.CurrentRow.Cells("packet_gid").Value.ToString
                    If sender.Columns(e.ColumnIndex).Name = "Auth" Then
                        If MsgBox("Are You Sure Want to Auth", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        lssql = ""
                        lssql &= " update chola_trn_tinward "
                        lssql &= " set inward_userauthdate=sysdate()"
                        lssql &= " where inward_packet_gid=" & lnGId
                        gfInsertQry(lssql, gOdbcConn)

                        LogPacket("", GCAUTHENTRY, lnGId)
                        LogPacketHistory("", GCAUTHENTRY, lnGId)

                        LoadData()
                    ElseIf sender.Columns(e.ColumnIndex).Name = "Reject" Then
                        If MsgBox("Are You Sure Want to Reject", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If
                        Do
                            Dim objfrm As New frmrejectreason
                            objfrm.ShowDialog()
                        Loop Until GRejectReason <> ""

                        lssql = ""
                        lssql &= " update chola_trn_tpacket "
                        lssql &= " set packet_status = packet_status | " & GCREJECTENTRY & ","
                        lssql &= " packet_remarks='" & GRejectReason & "'"
                        lssql &= " where packet_gid=" & lnGId
                        gfInsertQry(lssql, gOdbcConn)
                        GRejectReason = ""
                        LogPacketHistory("", GCREJECTENTRY, lnGId)
                        LoadData()
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class