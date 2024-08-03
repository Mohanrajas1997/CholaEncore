Public Class frmauthremovalpostentry
    Dim lssql As String
    Private Sub btnreverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreverse.Click
        Dim lnPDCGid As Long

        If txtgnsarefno.Text.Trim = "" Then
            MsgBox("Please Enter GNSA Refno..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select packet_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " where packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
        lssql &= " and packet_status & " & GCAUTHENTRY & " > 0"
        lssql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0"

        lnPDCGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnPDCGid <= 0 Then
            MsgBox("Invalid GNSA REF#..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        Else
            ReverseAuth(lnPDCGid)
        End If

    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtgnsarefno.Text = ""
    End Sub

    Private Sub ReverseAuth(ByVal PacketGid As Long)
        Dim listatus As Integer

        If MsgBox("Are You Sure Want to Reverse Entry", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        'PDC Status
        lssql = " select "
        lssql &= " bit_or((((chq_status|" & GCMATCHFINONE & ")^" & GCMATCHFINONE & ")|" & GCMATCHFINONEPRECOVERFILE & ")^" & GCMATCHFINONEPRECOVERFILE & ") as 'PDCStatus'"
        lssql &= " from chola_trn_tpdcentry"
        lssql &= " where 1=1 "
        lssql &= " and chq_packet_gid=" & PacketGid
        listatus = Val(gfExecuteScalar(lssql, gOdbcConn))

        If listatus > GCINPROCESS Then
            MsgBox("Access Denied..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        End If

        'SPDC Status
        lssql = " select "
        lssql &= " bit_or((((chqentry_status|" & GCMATCHFINONE & ")^" & GCMATCHFINONE & ")|" & GCMATCHFINONEPRECOVERFILE & ")^" & GCMATCHFINONEPRECOVERFILE & ") as 'PDCStatus'"
        lssql &= " from chola_trn_tspdcchqentry"
        lssql &= " where 1=1 "
        lssql &= " and chqentry_packet_gid=" & PacketGid
        listatus = Val(gfExecuteScalar(lssql, gOdbcConn))

        If listatus > GCINPROCESS Then
            MsgBox("Access Denied..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        End If

        'ECS Status
        lssql = " select "
        lssql &= " bit_or((((ecsemientry_status|" & GCMATCHFINONE & ")^" & GCMATCHFINONE & ")|" & GCMATCHFINONEPRECOVERFILE & ")^" & GCMATCHFINONEPRECOVERFILE & ") as 'PDCStatus'"
        lssql &= " from chola_trn_tecsemientry "
        lssql &= " where 1=1 "
        lssql &= " and ecsemientry_packet_gid=" & PacketGid
        listatus = Val(gfExecuteScalar(lssql, gOdbcConn))

        If listatus > GCINPROCESS Then
            MsgBox("Access Denied..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        End If

        'PDC Cheque Entry
        lssql = ""
        lssql &= " insert into chola_trn_tpdcentrydelete "
        lssql &= " select * from chola_trn_tpdcentry "
        lssql &= " where chq_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " update chola_trn_tpdcentry b "
        lssql &= " inner join chola_trn_tpdcfile a on a.entry_gid=b.entry_gid "
        lssql &= " set a.entry_gid=0 ,"
        lssql &= " a.pdc_status_flag=1 "
        lssql &= " where chq_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " delete from chola_trn_tpdcentry "
        lssql &= " where chq_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        'SPDC Entry
        lssql = ""
        lssql &= " insert into chola_trn_tspdcentrydelete "
        lssql &= " select * from chola_trn_tspdcentry "
        lssql &= " where spdcentry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lssql = " delete from chola_trn_tspdcentry "
        lssql &= " where spdcentry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        'SPDC Cheque Entry
        lssql = ""
        lssql &= " insert into chola_trn_tspdcchqentrydelete "
        lssql &= " select * from chola_trn_tspdcchqentry "
        lssql &= " where chqentry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " update chola_trn_tspdcchqentry b "
        lssql &= " inner join chola_trn_tpdcfile a on a.pdc_spdcentry_gid=b.chqentry_gid "
        lssql &= " set a.pdc_spdcentry_gid=0 ,"
        lssql &= " a.pdc_status_flag=1 "
        lssql &= " where chqentry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " delete from chola_trn_tspdcchqentry "
        lssql &= " where chqentry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        'ECS Entry
        lssql = ""
        lssql &= " insert into chola_trn_tecsemientrydelete "
        lssql &= " select * from chola_trn_tecsemientry "
        lssql &= " where ecsemientry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " update chola_trn_tecsemientry b "
        lssql &= " inner join chola_trn_tpdcfile a on a.pdc_ecsentry_gid=b.ecsemientry_gid "
        lssql &= " set a.pdc_ecsentry_gid=0 ,"
        lssql &= " a.pdc_status_flag=1 "
        lssql &= " where ecsemientry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " delete from chola_trn_tecsemientry "
        lssql &= " where ecsemientry_packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        'Reverse Packet
        lssql = ""
        lssql &= " update chola_trn_tpacket set "
        lssql &= " packet_status=(packet_status | " & GCPACKETCHEQUEENTRY & " ) ^ " & GCPACKETCHEQUEENTRY
        lssql &= " where packet_gid=" & PacketGid
        gfInsertQry(lssql, gOdbcConn)

        MsgBox("Process Completed..!", MsgBoxStyle.Information, gProjectName)
        txtgnsarefno.Text = ""
        txtgnsarefno.Focus()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class