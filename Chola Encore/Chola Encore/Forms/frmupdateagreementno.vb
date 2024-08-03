Public Class frmupdateagreementno

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtcuragreementno.Text = ""
        txtgnsarefno.Text = ""
        txtnewagreementno.Text = ""
        txtcuragreementno.Focus()
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim drpacket As Odbc.OdbcDataReader
        Dim drInward As Odbc.OdbcDataReader

        Dim lnPacketGid As Long
        Dim lnInwardGid As Long
        Dim lnAgreementGid As Long
        Dim lnNewInwardGid As Long

        Dim liResult As Integer
        Dim liStatus As Integer

        Dim lssql As String

        'If txtcuragreementno.Text.Trim = "" Then
        '    MsgBox("Please Enter Current Agreement No..!", MsgBoxStyle.Critical, gProjectName)
        '    txtcuragreementno.Focus()
        '    Exit Sub
        'End If

        If txtgnsarefno.Text.Trim = "" Then
            MsgBox("Please Enter GNSA Ref No..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        End If

        If txtnewagreementno.Text.Trim = "" Then
            MsgBox("Please Enter New Agreement No..!", MsgBoxStyle.Critical, gProjectName)
            txtnewagreementno.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select * "
        lssql &= " from chola_trn_tinward "
        lssql &= " where inward_agreementno = '" & txtnewagreementno.Text.Trim & "' "
        lssql &= " and inward_receiveddate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "' "

        drInward = gfExecuteQry(lssql, gOdbcConn)

        If drInward.HasRows Then
            lnNewInwardGid = Val(drInward.Item("inward_gid").ToString)
            liStatus = Val(drInward.Item("inward_status").ToString)
            If (liStatus And (GCNOTRECEIVED Or GCRECEIVED Or GCCOMBINED)) Then
                MsgBox("Invalid New Agreement No..!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
        Else
            MsgBox("Invalid New Agreement No..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        If txtcuragreementno.Text <> "" Then
            lssql = ""
            lssql &= " select packet_gid,packet_inward_gid "
            lssql &= " from chola_trn_tpacket "
            lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
            lssql &= " where packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
            lssql &= " and agreement_no='" & txtcuragreementno.Text & "'"
        Else
            lssql = ""
            lssql &= " select packet_gid,packet_inward_gid "
            lssql &= " from chola_trn_tpacket "
            lssql &= " where packet_gnsarefno='" & txtgnsarefno.Text.Trim & "' "
            lssql &= " and packet_agreement_gid = 0 "
        End If

        drpacket = gfExecuteQry(lssql, gOdbcConn)

        If drpacket.HasRows Then
            lnPacketGid = Val(drpacket.Item("packet_gid").ToString)
            lnInwardGid = Val(drpacket.Item("packet_inward_gid").ToString)

            lssql = ""
            lssql &= " select agreement_gid "
            lssql &= " from chola_mst_tagreement "
            lssql &= " where agreement_no='" & txtnewagreementno.Text.Trim & "'"
            lnAgreementGid = Val(gfExecuteScalar(lssql, gOdbcConn))

            If lnAgreementGid = 0 Then
                lssql = ""
                lssql &= " insert into chola_mst_tagreement(agreement_no,shortagreement_no) values( "
                lssql &= "'" & drInward.Item("inward_agreementno").ToString & "',"
                lssql &= "'" & drInward.Item("inward_shortagreementno").ToString & "')"

                gfInsertQry(lssql, gOdbcConn)

                lssql = ""
                lssql &= " select agreement_gid from chola_mst_tagreement "
                lssql &= " where agreement_no='" & drInward.Item("inward_agreementno").ToString & "'"

                lnAgreementGid = Val(gfExecuteScalar(lssql, gOdbcConn))
            End If

            lssql = ""
            lssql &= " update chola_trn_tpacket set "
            lssql &= " packet_agreement_gid=" & lnAgreementGid & ","
            lssql &= " packet_inward_gid=" & lnNewInwardGid & ","
            lssql &= " packet_remarks='Old AgreementNo." & txtcuragreementno.Text & " NEW AgreementNo." & txtnewagreementno.Text & "'"
            lssql &= " where packet_gid=" & lnPacketGid

            liResult = gfInsertQry(lssql, gOdbcConn)

            lssql = ""
            lssql &= " update chola_trn_tpdcentry set "
            lssql &= " chq_agreement_gid=" & lnAgreementGid
            lssql &= " where chq_packet_gid=" & lnPacketGid

            liResult = gfInsertQry(lssql, gOdbcConn)

            If lnInwardGid > 0 Then
                lssql = ""
                lssql &= " update chola_trn_tinward set "
                lssql &= " inward_packet_gid=0,"
                lssql &= " inward_status = (inward_status | " & GCRECEIVED & ") ^ " & GCRECEIVED
                lssql &= " where inward_gid=" & lnInwardGid
                liResult = Val(gfExecuteScalar(lssql, gOdbcConn))
            Else
                lssql = ""
                lssql &= " update chola_trn_tinward set "
                lssql &= " inward_packet_gid=0,"
                lssql &= " inward_status = (inward_status | " & GCRECEIVED & ") ^ " & GCRECEIVED
                lssql &= " where inward_packet_gid =" & lnPacketGid
                liResult = Val(gfExecuteScalar(lssql, gOdbcConn))
            End If

            lssql = ""
            lssql &= " update chola_trn_tinward set "
            lssql &= " inward_packet_gid=" & lnPacketGid & ","
            lssql &= " inward_status = inward_status | " & GCRECEIVED
            lssql &= " where inward_gid=" & lnNewInwardGid

            liResult = gfInsertQry(lssql, gOdbcConn)

            UpdatePacket(lnPacketGid)

            LogPacketHistory("", GCAGREEMENTNOCHANGED, lnPacketGid)

            MsgBox("Successfully Updated..!", MsgBoxStyle.Information, gProjectName)
            btnclear.PerformClick()
        Else
            MsgBox("Invalid Details..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If
    End Sub
End Class