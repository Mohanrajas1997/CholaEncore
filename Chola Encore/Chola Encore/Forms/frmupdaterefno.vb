Public Class frmupdaterefno

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtcurrefno.Text = ""
        txtagreementno.Text = ""
        txtnewrefno.Text = ""
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnPacketGid As Long
        Dim liResult As Integer
        Dim lssql As String

        If txtcurrefno.Text.Trim = "" Then
            MsgBox("Please Enter Current Refno..!", MsgBoxStyle.Critical, gProjectName)
            txtcurrefno.Focus()
            Exit Sub
        End If

        If txtagreementno.Text.Trim = "" Then
            MsgBox("Please Enter Valid Current Refno..!", MsgBoxStyle.Critical, gProjectName)
            txtcurrefno.Focus()
            Exit Sub
        End If

        If txtnewrefno.Text.Trim = "" Then
            MsgBox("Please Enter New Refno..!", MsgBoxStyle.Critical, gProjectName)
            txtnewrefno.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select packet_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " where packet_gnsarefno='" & txtnewrefno.Text & "'"
        lnPacketGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnPacketGid > 0 Then
            MsgBox("Duplicate New Ref no..!", MsgBoxStyle.Critical, gProjectName)
            txtnewrefno.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select packet_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where packet_gnsarefno='" & txtcurrefno.Text & "'"
        lssql &= " and agreement_no='" & txtagreementno.Text & "'"
        lnPacketGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        lssql = ""
        lssql &= " update chola_trn_tpacket set "
        lssql &= " packet_gnsarefno='" & txtnewrefno.Text & "',"
        lssql &= " packet_remarks='Old REFNo." & txtcurrefno.Text & " NEW REFNo." & txtnewrefno.Text & "'"
        lssql &= " where packet_gid=" & lnPacketGid
        liResult = gfInsertQry(lssql, gOdbcConn)

        If liResult = 0 Then
            MsgBox("Some error occurred..!", MsgBoxStyle.Critical, gProjectName)
        Else

            'Log Packet
            LogPacketHistory("", GCGNSAREFCHANGED, lnPacketGid)
            MsgBox("Successfully Updated..!", MsgBoxStyle.Information, gProjectName)
            btnclear.PerformClick()
            txtcurrefno.Focus()
        End If
    End Sub

    Private Sub txtcurrefno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcurrefno.LostFocus
        Dim lssql As String
        Dim lsAgreementNo As String

        If txtcurrefno.Text.Trim = "" Then Exit Sub

        lssql = ""
        lssql &= " select agreement_no "
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where packet_gnsarefno='" & txtcurrefno.Text & "'"

        lsAgreementNo = gfExecuteScalar(lssql, gOdbcConn)
        txtagreementno.Text = lsAgreementNo
    End Sub
End Class