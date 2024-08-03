Public Class frmpostwithfinone

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtgnsarefno.Text = ""
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click

        If txtgnsarefno.Text.Trim = "" Then
            If MsgBox("GNSAREFNo Blank.." & vbCrLf & " Are You Sure want to update unmatched Case.?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        btnpost.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        frmMain.lblstatus.Text = "Posting records ..."

        Call UpdateUnmatchedCases()

        frmMain.lblstatus.Text = ""
        Me.Cursor = System.Windows.Forms.Cursors.Default
        btnpost.Enabled = True

        MsgBox("Posted Successfully..!", MsgBoxStyle.Information, gProjectName)
        txtgnsarefno.Text = ""
    End Sub

    Private Sub UpdateUnmatchedCases()
        Dim objdt As DataTable
        Dim lssql As String

        lssql = ""
        lssql &= " select distinct packet_gid "
        lssql &= " from chola_trn_tpacket as a "
        lssql &= " inner join chola_trn_tpdcentry b on a.packet_gid=b.chq_packet_gid"
        lssql &= " where 1=1 "
        lssql &= " and a.packet_receiveddate >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        lssql &= " and a.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "
        lssql &= " and b.chq_pdc_gid=0 "

        If txtgnsarefno.Text <> "" Then
            lssql &= " and a.packet_gnsarefno = '" & txtgnsarefno.Text & "' "
        End If

        lssql &= " union "

        lssql &= " select distinct packet_gid "
        lssql &= " from chola_trn_tpacket as a "
        lssql &= " inner join chola_trn_tspdcchqentry b on a.packet_gid=b.chqentry_packet_gid"
        lssql &= " where 1=1 "
        lssql &= " and a.packet_receiveddate >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        lssql &= " and a.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "
        lssql &= " and b.chqentry_pdc_gid=0 "

        If txtgnsarefno.Text <> "" Then
            lssql &= " and a.packet_gnsarefno = '" & txtgnsarefno.Text & "' "
        End If

        lssql &= " union "

        lssql &= " select distinct packet_gid "
        lssql &= " from chola_trn_tpacket as a "
        lssql &= " inner join chola_trn_tecsemientry b on a.packet_gid=b.ecsemientry_packet_gid"
        lssql &= " where 1=1 "
        lssql &= " and a.packet_receiveddate >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        lssql &= " and a.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "
        lssql &= " and b.ecsemientry_pdc_gid=0 "

        If txtgnsarefno.Text <> "" Then
            lssql &= " and a.packet_gnsarefno = '" & txtgnsarefno.Text & "' "
        End If

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            UpdatePacket(objdt.Rows(i).Item("packet_gid").ToString)

            Application.DoEvents()
            frmMain.lblstatus.Text = "Out of " & objdt.Rows.Count & " record(s) reading " & i & " record..."
        Next i
    End Sub

    Private Sub frmpostwithfinone_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFrom.Value = Now
        dtpTo.Value = Now
    End Sub
End Class