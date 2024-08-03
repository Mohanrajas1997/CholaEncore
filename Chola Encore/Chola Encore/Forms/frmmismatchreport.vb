Public Class frmmismatchreport
    Private Sub frmmismatchreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmmismatchreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtgnsarefno.Focus()
        txtgnsarefno.Text = ""
    End Sub

    Private Sub frmmismatchreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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
        Dim lsSubSql As String

        ' sub query for mismatch packets
        'pdc
        lsSql = ""
        lsSql &= " select distinct packet_gid "
        lsSql &= " from chola_trn_tpacket as a "
        lsSql &= " inner join chola_trn_tpdcentry b on a.packet_gid=b.chq_packet_gid"
        lsSql &= " left join chola_trn_tpdcfile as c on c.pdc_gid = b.chq_pdc_gid "
        lsSql &= " left join chola_mst_tpdctype as d on d.pdc_type = c.pdc_type "
        lsSql &= " where 1=1 "
        lsSql &= " and packet_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        lsSql &= " and (chq_pdc_gid = 0 or chq_pdc_gid is null or if(b.chq_type = 2,'SPDC','PDC') <> ifnull(d.new_pdc_type,'')) "

        If txtgnsarefno.Text <> "" Then
            lsSql &= " and a.packet_gnsarefno = '" & txtgnsarefno.Text & "' "
        End If

        lsSql &= " union "

        ' spdc
        lsSql &= " select distinct packet_gid "
        lsSql &= " from chola_trn_tpacket as a "
        lsSql &= " inner join chola_trn_tspdcchqentry b on a.packet_gid=b.chqentry_packet_gid"
        lsSql &= " left join chola_trn_tpdcfile as c on c.pdc_gid = b.chqentry_pdc_gid "
        lsSql &= " left join chola_mst_tpdctype as d on d.pdc_type = c.pdc_type "
        lsSql &= " where 1=1 "
        lsSql &= " and packet_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        lsSql &= " and (chqentry_pdc_gid = 0 or chqentry_pdc_gid is null or ifnull(d.new_pdc_type,'') <> 'SPDC') "

        If txtgnsarefno.Text <> "" Then
            lsSql &= " and a.packet_gnsarefno = '" & txtgnsarefno.Text & "' "
        End If

        lsSql &= " union "

        ' ecs
        lsSql &= " select distinct packet_gid "
        lsSql &= " from chola_trn_tpacket as a "
        lsSql &= " inner join chola_trn_tecsemientry b on a.packet_gid=b.ecsemientry_packet_gid"
        lsSql &= " left join chola_trn_tpdcfile as c on c.pdc_gid = b.ecsemientry_pdc_gid "
        lsSql &= " left join chola_mst_tpdctype as d on d.pdc_type = c.pdc_type "
        lsSql &= " where 1=1 "
        lsSql &= " and packet_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        lsSql &= " and (ecsemientry_pdc_gid = 0 or ecsemientry_pdc_gid is null or ifnull(d.new_pdc_type,'') <> 'ECS') "

        If txtgnsarefno.Text <> "" Then
            lsSql &= " and a.packet_gnsarefno = '" & txtgnsarefno.Text & "' "
        End If

        If chkPktPayModeDisc.Checked = True Then
            lsSql &= " union "

            ' packet disc
            lsSql &= " select distinct packet_gid from chola_trn_tpacket as a "
            lsSql &= " inner join chola_trn_tpdcfilehead as b on b.head_packet_gid = a.packet_gid "
            lsSql &= " inner join chola_mst_tpdcmode as c on c.pdc_mode = b.head_mode and c.new_pdc_mode <> a.packet_mode "
            lsSql &= " where 1 = 1 "
            lsSql &= " and a.packet_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "' "
            lsSql &= " and a.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "' "
            lsSql &= " and a.packet_mode <> '' "

            If txtgnsarefno.Text <> "" Then
                lsSql &= " and a.packet_gnsarefno = '" & txtgnsarefno.Text & "' "
            End If
        End If

        lsSubSql = lsSql

        Call gfInsertQry("set @slno := 0", gOdbcConn)

        lsSql = ""
        lsSql &= " select a.packet_gid,@slno:= @slno + 1 as 'SL No',packet_gnsarefno as 'GNSA REF#',"
        lsSql &= " agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No',packet_mode as 'Packet Mode' "
        lsSql &= " from (" & lsSubSql & ") as s "
        lsSql &= " inner join chola_trn_tpacket as a on a.packet_gid = s.packet_gid "
        lsSql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)

            .Columns(0).Visible = False

            Dim dgButtonColumn2 As New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "View"
            dgButtonColumn2.Name = "View"
            dgButtonColumn2.ToolTipText = "View"
            dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn2.FlatStyle = FlatStyle.System
            dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn2)

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
            Dim lnPdcId As Long
            Dim lsPktNo As String
            Dim lsPktMode As String

            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "View" Then
                        lnPdcId = dgvsummary.CurrentRow.Cells("packet_gid").Value.ToString
                        lsPktNo = dgvsummary.CurrentRow.Cells("GNSA REF#").Value.ToString
                        lsPktMode = dgvsummary.CurrentRow.Cells("Packet Mode").Value.ToString

                        Dim frmentry As New frmviewentry(lnPdcId, lsPktNo, lsPktMode)
                        frmentry.ShowDialog()
                        dgvsummary.Rows(e.RowIndex).Selected = True
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class