Public Class frmbatchsummary
    Dim lssql As String
    Dim isload As Boolean
    Private Sub frmsummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtAgreementNo.Focus()
        txtAgreementNo.Text = ""

        isload = True
        lssql = " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N' "
        gpBindCombo(lssql, "type_name", "type_flag", cboproduct, gOdbcConn)
        cboproduct.SelectedIndex = -1
        isload = False
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

        lsSql = ""
        lsSql &= " select entry_gid,batch_displayno 'Batch No',type_name as 'Product',"
        lsSql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lsSql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',"
        lsSql &= " if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lsSql &= " from chola_trn_tpdcentry a "
        lsSql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lsSql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lsSql &= " inner join chola_mst_ttype on type_flag=chq_prodtype  and type_deleteflag='N'"
        lsSql &= " inner join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lsSql &= " where 1=1 and chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"

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
            lsSql &= " and batch_prodtype=" & cboproduct.SelectedValue
        End If

        If cbobatchno.Text.Trim <> "" Then
            lsSql &= " and batch_gid=" & cbobatchno.SelectedValue
        End If

        If cbostatus.Text.Trim <> "" Then
            If cbostatus.Text.Trim = "Completed" Then
                lsSql &= " and chq_status & " & GCPRESENTATIONDE & " > 0 "
            Else
                lsSql &= " and chq_status & " & GCPRESENTATIONDE & " = 0 "
            End If
        End If
        lsSql &= " order by batch_no,type_name,packet_gnsarefno "

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            Dim dgButtonColumn2 As New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "Pullout"
            dgButtonColumn2.Name = "Pullout"
            dgButtonColumn2.ToolTipText = "Pullout"
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

        If cboproduct.Text.Trim = "" Then
            MsgBox("Please select Product", MsgBoxStyle.Critical, gProjectName)
            cboproduct.Focus()
            Exit Sub
        End If

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
        txtAgreementNo.Text = ""
        dgvsummary.DataSource = Nothing
        dgvsummary.Columns.Clear()
        lbltotrec.Text = ""
        txtchqno.Text = ""
        isload = True
        cboproduct.SelectedIndex = -1
        cbobatchno.SelectedIndex = -1
        cbobatchno.Text = ""
        cboproduct.Text = ""
        isload = False
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Try
            Dim lnPdcId As Long

            Select Case e.ColumnIndex
                Case Is > -1

                    If sender.Columns(e.ColumnIndex).Name = "Pullout" Then
                        If dgvsummary.CurrentRow.Cells("status").Value.ToString = "Entry Completed" Then Exit Sub

                        If MsgBox("Are You Sure Want to Pullout", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        lnPdcId = dgvsummary.CurrentRow.Cells("entry_gid").Value.ToString
                        Dim frmpullout As New frmpulloutentry(lnPdcId)
                        frmpullout.ShowDialog()
                        LoadData()
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub cboproduct_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboproduct.SelectedValueChanged
        If isload Then Exit Sub

        lssql = " select batch_gid,batch_displayno "
        lssql &= " from chola_trn_tbatch"
        lssql &= " where batch_cycledate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and batch_prodtype=" & cboproduct.SelectedValue
        gpBindCombo(lssql, "batch_displayno", "batch_gid", cbobatchno, gOdbcConn)
        cbobatchno.SelectedIndex = -1
    End Sub
End Class