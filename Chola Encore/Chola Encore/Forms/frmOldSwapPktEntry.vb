Public Class frmOldSwapPktEntry
    Dim mnPktId As Long = 0

    Private Sub frmOldSwapPktEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmOldSwapPktEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpSwapDate.Value = Now
        KeyPreview = True
        txtPktNo.Focus()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim lsSql As String
        Dim lnResult As Long
        Dim lnOldPktSlNo As Long
        Dim lnSwapId As Long
        Dim ds As New DataSet

        ' chk with packet
        lsSql = ""
        lsSql &= " select s.swap_gid,p.packet_gid,a.agreement_no,p.packet_mode from chola_trn_tpacket as p "
        lsSql &= " inner join chola_mst_tagreement as a on a.agreement_gid = p.packet_agreement_gid "
        lsSql &= " inner join chola_trn_tswap as s on s.agreement_gid = a.agreement_gid and s.swap_date = '" & Format(dtpSwapDate.Value, "yyyy-MM-dd") & "' "
        lsSql &= " where p.packet_gnsarefno = '" & QuoteFilter(txtPktNo.Text) & "' "
        lsSql &= " and p.packet_status & " & (GCPACKETCHEQUEENTRY Or GCPACKETCHEQUEREENTRY Or GCPACKETVAULTED) & " > 0 "
        lsSql &= " and p.packet_status & " & (GCPACKETRETRIEVAL Or GCIPACKETPULLOUT Or GCPKTOLDSWAP) & " = 0 "

        Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

        If ds.Tables("pkt").Rows.Count > 0 Then
            mnPktId = ds.Tables("pkt").Rows(0).Item("packet_gid")
            lnSwapId = ds.Tables("pkt").Rows(0).Item("swap_gid")
            txtAgmntNo.Text = ds.Tables("pkt").Rows(0).Item("agreement_no").ToString
            txtPayMode.Text = ds.Tables("pkt").Rows(0).Item("packet_mode").ToString

            ' Find new old packet serial no
            lsSql = " select max(oldpacket_slno) from chola_trn_toldswappacket"

            lnOldPktSlNo = Val(gfExecuteScalar(lsSql, gOdbcConn)) + 1

            ' insert line
            lsSql = ""
            lsSql &= " insert into chola_trn_toldswappacket ("
            lsSql &= " swap_gid,oldpacket_gid,oldpacket_slno,oldentry_date,oldentry_by) values ("
            lsSql &= " " & lnSwapId & ","
            lsSql &= " " & mnPktId & ","
            lsSql &= " " & lnOldPktSlNo & ","
            lsSql &= " sysdate(),"
            lsSql &= " '" & QuoteFilter(gUserName) & "')"

            lnResult = gfInsertQry(lsSql, gOdbcConn)

            ' update in packet table
            lsSql = ""
            lsSql &= " update chola_trn_tpacket set "
            lsSql &= " packet_status = packet_status | " & GCPKTOLDSWAP & " "
            lsSql &= " where packet_gid = " & mnPktId & " "

            lnResult = gfInsertQry(lsSql, gOdbcConn)

            ' swap packet updation
            lsSql = ""
            lsSql &= " update chola_trn_tswap set "
            lsSql &= " oldpacket_count = oldpacket_count + 1 "
            lsSql &= " where swap_gid = " & lnSwapId & " "

            lnResult = gfInsertQry(lsSql, gOdbcConn)

            MsgBox("Old Swap Packet Serial No : " & lnOldPktSlNo, MsgBoxStyle.Information, gProjectName)

            mnPktId = 0
            txtPktNo.Text = ""
            txtAgmntNo.Text = ""
            txtPayMode.Text = ""

            txtPktNo.Focus()
        Else
            mnPktId = 0
            txtAgmntNo.Text = ""
            txtPayMode.Text = ""

            MsgBox("Invalid packet !", MsgBoxStyle.Information, gProjectName)
            Exit Sub
        End If

        ds.Tables("pkt").Rows.Clear()
    End Sub

    Private Function RetrievePktInfo() As Boolean
        Dim lsSql As String
        Dim ds As New DataSet

        ' chk with packet
        lsSql = ""
        lsSql &= " select p.packet_gid,a.agreement_no,p.packet_mode from chola_trn_tpacket as p "
        lsSql &= " inner join chola_mst_tagreement as a on a.agreement_gid = p.packet_agreement_gid "
        lsSql &= " inner join chola_trn_tswap as s on s.agreement_gid = a.agreement_gid and s.swap_date = '" & Format(dtpSwapDate.Value, "yyyy-MM-dd") & "' "
        lsSql &= " where p.packet_gnsarefno = '" & QuoteFilter(txtPktNo.Text) & "' "
        lsSql &= " and p.packet_status & " & (GCPACKETCHEQUEENTRY Or GCPACKETCHEQUEREENTRY Or GCPACKETVAULTED) & " > 0 "
        lsSql &= " and p.packet_status & " & (GCPACKETRETRIEVAL Or GCIPACKETPULLOUT Or GCPKTOLDSWAP) & " = 0 "

        Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

        If ds.Tables("pkt").Rows.Count > 0 Then
            mnPktId = ds.Tables("pkt").Rows(0).Item("packet_gid")
            txtAgmntNo.Text = ds.Tables("pkt").Rows(0).Item("agreement_no").ToString
            txtPayMode.Text = ds.Tables("pkt").Rows(0).Item("packet_mode").ToString
        Else
            mnPktId = 0
            txtAgmntNo.Text = ""
            txtPayMode.Text = ""

            MsgBox("Invalid packet !", MsgBoxStyle.Information, gProjectName)
            Return False
        End If

        ds.Tables("pkt").Rows.Clear()
        Return True
    End Function

    Private Sub txtPktNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPktNo.Validating
        If txtPktNo.Text <> "" Then
            If RetrievePktInfo() = False Then e.Cancel = True
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtPktNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPktNo.TextChanged

    End Sub
End Class