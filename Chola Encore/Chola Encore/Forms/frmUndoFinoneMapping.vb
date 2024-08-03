Public Class frmUndoFinoneMapping
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtGnsaRefNo.Text = ""
        txtGnsaRefNo.Focus()
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        Dim lsSql As String
        Dim lnPktId As Long

        If txtGnsaRefNo.Text.Trim = "" Then
            MsgBox("Please Enter GNSA Refno..!", MsgBoxStyle.Critical, gProjectName)
            txtGnsaRefNo.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select packet_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " where packet_gnsarefno='" & txtGnsaRefNo.Text.Trim & "'"
        lssql &= " and packet_status & " & GCAUTHENTRY & " > 0"
        lssql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0"

        lnPktId = Val(gfExecuteScalar(lsSql, gOdbcConn))

        If lnPktId <= 0 Then
            MsgBox("Invalid GNSA REF#..!", MsgBoxStyle.Critical, gProjectName)
            txtGnsaRefNo.Focus()
            Exit Sub
        Else
            ReverseAuth(lnPktId)
        End If
    End Sub

    Private Sub ReverseAuth(ByVal PacketGid As Long)
        Dim lsSql As String

        If MsgBox("Are you sure to undo ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        'PDC
        lsSql = ""
        lssql &= " update chola_trn_tpdcfile a "
        lsSql &= " inner join chola_trn_tpdcentry b on a.pdc_gid = b.chq_pdc_gid and a.entry_gid=b.entry_gid "
        lsSql &= " set a.entry_gid = 0,b.chq_pdc_gid = 0 "
        lsSql &= " where chq_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lsSql = ""
        lsSql &= " update chola_trn_tpdcentry b "
        lsSql &= " inner join chola_trn_tpdcfile a on a.pdc_gid = b.chq_pdc_gid and a.entry_gid=0 "
        lsSql &= " set a.entry_gid = 0,b.chq_pdc_gid = 0 "
        lsSql &= " where chq_packet_gid=" & PacketGid
        gfInsertQry(lsSql, gOdbcConn)

        'SPDC Cheque Entry
        lsSql = ""
        lssql &= " update chola_trn_tpdcfile a "
        lsSql &= " inner join chola_trn_tspdcchqentry b on a.pdc_gid = b.chqentry_pdc_gid and a.pdc_spdcentry_gid=b.chqentry_gid "
        lsSql &= " set a.pdc_spdcentry_gid = 0,b.chqentry_pdc_gid = 0"
        lsSql &= " where chqentry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lsSql = ""
        lsSql &= " update chola_trn_tspdcchqentry b "
        lsSql &= " inner join chola_trn_tpdcfile a on a.pdc_gid = b.chqentry_pdc_gid and a.pdc_spdcentry_gid=0 "
        lsSql &= " set a.pdc_spdcentry_gid = 0,b.chqentry_pdc_gid = 0"
        lsSql &= " where chqentry_packet_gid=" & PacketGid
        gfInsertQry(lsSql, gOdbcConn)

        'ECS Entry
        lsSql = ""
        lssql &= " update chola_trn_tpdcfile a "
        lsSql &= " inner join chola_trn_tecsemientry b on a.pdc_gid = b.ecsemientry_pdc_gid and a.pdc_ecsentry_gid=b.ecsemientry_gid "
        lsSql &= " set a.pdc_ecsentry_gid = 0,b.ecsemientry_pdc_gid = 0"
        lsSql &= " where ecsemientry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lsSql = ""
        lsSql &= " update chola_trn_tecsemientry b  "
        lsSql &= " inner join chola_trn_tpdcfile a on a.pdc_gid = b.ecsemientry_pdc_gid and a.pdc_ecsentry_gid=0 "
        lsSql &= " set a.pdc_ecsentry_gid = 0,b.ecsemientry_pdc_gid = 0"
        lsSql &= " where ecsemientry_packet_gid=" & PacketGid
        gfInsertQry(lsSql, gOdbcConn)

        MsgBox("Process Completed..!", MsgBoxStyle.Information, gProjectName)
        txtGnsaRefNo.Text = ""
        txtGnsaRefNo.Focus()
    End Sub
End Class