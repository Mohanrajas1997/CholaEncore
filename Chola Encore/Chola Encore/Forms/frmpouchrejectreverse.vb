Public Class frmpouchrejectreverse
    Dim isload As Boolean = True
    Private Sub frmpouchrejectreverse_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub
    Private Sub frmpouchnotreceived_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmpouchnotreceived_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        isload = True
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        isload = False
    End Sub

    Private Sub frmpouchnotreceived_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()
        Dim lssql As String

        lssql = " Select packet_gid,inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
        lssql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',"
        lssql &= " packet_gnsarefno as 'GNSAREF#'"
        lssql &= " from chola_trn_tinward"
        lssql &= " inner join chola_trn_tpacket on packet_inward_gid=inward_gid "
        lssql &= " where true "

        If cbofilename.SelectedIndex <> -1 Then
            lssql &= " and inward_file_gid=" & Val(cbofilename.SelectedValue)
        End If

        If rbtrejected.Checked Then
            lssql &= " and packet_status & " & GCREJECTENTRY & " > 0"
        Else
            lssql &= " and packet_status & " & GCAUTHENTRY & " > 0"
            lssql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " = 0"
        End If

        If txtagreementno.Text.Trim <> "" Then
            lssql &= " and inward_shortagreementno='" & txtagreementno.Text.Trim & "'"
        End If

        dgvsummary.Columns.Clear()

        gpPopGridView(dgvsummary, lssql, gOdbcConn)

        dgvsummary.Columns(0).Visible = False

        If rbtrejected.Checked Then
            Dim dgButtonColumn2 As DataGridViewButtonColumn

            dgButtonColumn2 = New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "Reverse"
            dgButtonColumn2.Name = "Reject"
            dgButtonColumn2.ToolTipText = "Reverse"
            dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn2.FlatStyle = FlatStyle.System
            dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn2)

            dgButtonColumn2 = New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "Reprocess"
            dgButtonColumn2.Name = "Reprocess"
            dgButtonColumn2.ToolTipText = "Reprocess"
            dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn2.FlatStyle = FlatStyle.System
            dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn2)
        Else
            Dim dgButtonColumn3 As New DataGridViewButtonColumn
            dgButtonColumn3.HeaderText = ""
            dgButtonColumn3.UseColumnTextForButtonValue = True
            dgButtonColumn3.Text = "Reverse"
            dgButtonColumn3.Name = "Auth"
            dgButtonColumn3.ToolTipText = "Reverse"
            dgButtonColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn3.FlatStyle = FlatStyle.System
            dgButtonColumn3.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn3.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn3)
        End If

    End Sub
    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click

        'If dtpimportdate.Checked = False Then
        '    MsgBox("Please Select Import Date", MsgBoxStyle.Critical, gProjectName)
        '    dtpimportdate.Focus()
        '    Exit Sub
        'End If

        'If cbofilename.Text = "" Then
        '    MsgBox("Please Select File Name", MsgBoxStyle.Critical, gProjectName)
        '    cbofilename.Focus()
        '    Exit Sub
        'End If

        Call LoadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpimportdate.Value = Now()
        dtpimportdate.Checked = False
        cbofilename.SelectedIndex = -1
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Dim lsqry As String
        Dim lsTxt As String

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        Select Case e.ColumnIndex
            Case Is > -1
                If sender.Columns(e.ColumnIndex).Name = "Reject" Then
                    If MsgBox("Are You Sure Want to Reverse Reject Entry", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    lsqry = ""
                    lsqry &= " update chola_trn_tpacket set "
                    lsqry &= " packet_status=(packet_status | " & GCREJECTENTRY & " ) ^ " & GCREJECTENTRY
                    lsqry &= " where packet_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)

                    LoadData()
                ElseIf sender.Columns(e.ColumnIndex).Name = "Auth" Then
                    If MsgBox("Are You Sure Want to Reverse Auth Entry", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    lsqry = ""
                    lsqry &= " update chola_trn_tpacket set "
                    lsqry &= " packet_status=(packet_status | " & GCAUTHENTRY & " ) ^ " & GCAUTHENTRY
                    lsqry &= " where packet_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)

                    LoadData()
                ElseIf sender.Columns(e.ColumnIndex).Name = "Reprocess" Then
                    If MsgBox("Are you sure to reprocess ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    Do
                        lsTxt = InputBox("Reprocess Date : ", "Reprocess Date", Format(Now, "dd-MMM-yyyy"))
                    Loop Until IsDate(lsTxt) = True

                    lsqry = ""
                    lsqry &= " update chola_trn_tinward "
                    lsqry &= " set inward_userauthdate='" & Format(CDate(lsTxt), "yyyy-MM-dd") & "' "
                    lsqry &= " where inward_packet_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)

                    lsqry = ""
                    lsqry &= " update chola_trn_tpacket set "
                    lsqry &= " packet_status=(packet_status | " & (GCAUTHENTRY Or GCPKTREPROCESS Or GCREJECTENTRY) & " ) ^ " & GCREJECTENTRY
                    lsqry &= " where packet_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)

                    LogPacketHistory("", GCPKTREPROCESS, dgvsummary.Rows(e.RowIndex).Cells(0).Value)

                    LoadData()
                End If
        End Select
    End Sub

    Private Sub dtpimportdate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpimportdate.ValueChanged
        Dim lssql As String
        If dtpimportdate.Checked = False Then Exit Sub

        lssql = ""
        lssql &= " select distinct file_name,file_gid "
        lssql &= " from chola_mst_tfile "
        lssql &= " inner join chola_trn_tinward on inward_file_gid=file_gid "
        lssql &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(dtpimportdate.Value, "yyyy-MM-dd") & "'"
        gpBindCombo(lssql, "file_name", "file_gid", cbofilename, gOdbcConn)

        If cbofilename.Items.Count = 0 Then
            cbofilename.SelectedValue = 0
        Else
            cbofilename.SelectedValue = -1
        End If
    End Sub

    Private Sub rbtrejected_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtrejected.CheckedChanged
        If isload = False Then
            LoadData()
        End If
    End Sub

    Private Sub rbtauth_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtauth.CheckedChanged
        If isload = False Then
            LoadData()
        End If
    End Sub
End Class