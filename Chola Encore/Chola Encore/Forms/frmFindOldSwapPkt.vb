Public Class frmFindOldSwapPkt

    Private Sub frmFindOldSwapPkt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtShortAgreementNo.Focus()
    End Sub

    Private Sub txtShortAgreementNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShortAgreementNo.GotFocus
        With txtShortAgreementNo
            .SelectionStart = 0
            .SelectionLength = .Text.Length
        End With
    End Sub

    Private Sub txtShortAgreementNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtShortAgreementNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call LoadPacket()

            With txtShortAgreementNo
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End With
        End If
    End Sub

    Private Sub LoadPacket()
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select oldpacket_slno as 'Swap SNo',packet_gnsarefno as 'GNSA Ref No',packet_mode as 'Packet Mod' from chola_mst_tagreement "
        lsSql &= " inner join chola_trn_tpacket on packet_agreement_gid = agreement_gid "
        lsSql &= " and packet_status & " & (GCPACKETRETRIEVAL Or GCIPACKETPULLOUT) & " = 0 "
        lssql &= " and packet_status & " & GCPKTOLDSWAP & " > 0 "
        lsSql &= " inner join chola_trn_toldswappacket on oldpacket_gid = packet_gid "
        lsSql &= " and oldpacket_status & " & (GCOLDSWAPPKTCANCELLED Or GCOLDSWAPPKTPULLOUT) & " = 0 "
        lsSql &= " where shortagreement_no = '" & Val(txtShortAgreementNo.Text) & "' "
        lsSql &= " order by oldpacket_slno "

        Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)
    End Sub

    Private Sub txtShortAgreementNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShortAgreementNo.TextChanged

    End Sub
End Class