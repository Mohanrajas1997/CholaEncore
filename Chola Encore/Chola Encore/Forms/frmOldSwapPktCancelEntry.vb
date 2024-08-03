Public Class frmOldSwapPktCancelEntry
    Dim mnOldSwapPktId As Long = 0
    Dim mnFlag As Integer = 0

    Private Sub frmOldSwapPktCancelEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub frmOldSwapPktEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case mnFlag
            Case GCOLDSWAPPKTCANCELLED
                Me.Text = "Old Swap Packet Cancellation Entry"
            Case GCOLDSWAPPKTPULLOUT
                Me.Text = "Old Swap Packet Pullout Entry"
            Case GCOLDSWAPPKTPULLOUTUNDO
                Me.Text = "Undo Old Swap Packet Pullout"
        End Select

        KeyPreview = True
        txtPktNo.Focus()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim lsSql As String
        Dim lnResult As Long
        Dim lnSwapId As Long
        Dim lnPktId As Long
        Dim lnOldPacketStatus As Integer
        Dim ds As New DataSet

        ' chk with packet
        If mnFlag <> GCOLDSWAPPKTPULLOUTUNDO Then
            lsSql = ""
            lsSql &= " select swap_gid,oldswappacket_gid,packet_gid,oldpacket_status from chola_trn_toldswappacket "
            lsSql &= " inner join chola_trn_tpacket on packet_gid = oldpacket_gid "
            lsSql &= " and packet_status & " & (GCPACKETRETRIEVAL Or GCIPACKETPULLOUT) & " = 0 "
            lsSql &= " inner join chola_mst_tagreement on agreement_gid = packet_agreement_gid "
            lsSql &= " where oldpacket_slno = '" & Val(txtSlNo.Text) & "' "
            lsSql &= " and oldpacket_status & " & (GCOLDSWAPPKTCANCELLED Or GCOLDSWAPPKTPULLOUT) & " = 0 "
        Else
            lsSql = ""
            lsSql &= " select swap_gid,oldswappacket_gid,packet_gid,oldpacket_status from chola_trn_toldswappacket "
            lsSql &= " inner join chola_trn_tpacket on packet_gid = oldpacket_gid "
            lsSql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
            lsSql &= " inner join chola_mst_tagreement on agreement_gid = packet_agreement_gid "
            lsSql &= " where oldpacket_slno = '" & Val(txtSlNo.Text) & "' "
            lsSql &= " and oldpacket_status & " & GCOLDSWAPPKTPULLOUT & " > 0 "
        End If

        Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

        If ds.Tables("pkt").Rows.Count > 0 Then
            mnOldSwapPktId = ds.Tables("pkt").Rows(0).Item("oldswappacket_gid")
            lnSwapId = ds.Tables("pkt").Rows(0).Item("swap_gid")
            lnPktId = ds.Tables("pkt").Rows(0).Item("packet_gid")
            lnOldPacketStatus = ds.Tables("pkt").Rows(0).Item("oldpacket_status")

            Select Case mnFlag
                Case GCOLDSWAPPKTCANCELLED
                    If (lnOldPacketStatus And GCOLDSWAPCHQTRANSFERED) > 0 Then
                        If MsgBox("Old packet cheques were transferred to new packet ! Are you sure to cancel ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If
                    End If

                    ' update cancel
                    lsSql = ""
                    lsSql &= " update chola_trn_toldswappacket set "
                    lsSql &= " oldpacket_status = oldpacket_status | " & GCOLDSWAPPKTCANCELLED & " "
                    lsSql &= " where oldswappacket_gid = " & mnOldSwapPktId & " "
                    lsSql &= " and oldpacket_status & " & (GCOLDSWAPPKTCANCELLED Or GCOLDSWAPPKTPULLOUT) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    ' update in packet table
                    lsSql = ""
                    lsSql &= " update chola_trn_tpacket set "
                    lsSql &= " packet_status = (packet_status | " & GCPKTOLDSWAP & ") ^ " & GCPKTOLDSWAP & " "
                    lsSql &= " where packet_gid = " & lnPktId & " "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    ' update in swap table
                    lsSql = ""
                    lsSql &= " update chola_trn_tswap set "
                    lsSql &= " oldpacket_count = oldpacket_count - 1 "
                    lsSql &= " where swap_gid = " & lnSwapId & " "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)
                Case GCOLDSWAPPKTPULLOUT
                    If (lnOldPacketStatus Or GCOLDSWAPCHQTRANSFERED) = 0 Then
                        If MsgBox("Old packet cheques were not transferred to new packet ! Are you sure to pullout ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If
                    End If

                    ' update pullout
                    lsSql = ""
                    lsSql &= " update chola_trn_toldswappacket set "
                    lsSql &= " oldpacket_status = oldpacket_status | " & GCOLDSWAPPKTPULLOUT & " "
                    lsSql &= " where oldswappacket_gid = " & mnOldSwapPktId & " "
                    lsSql &= " and oldpacket_status & " & (GCOLDSWAPPKTCANCELLED Or GCOLDSWAPPKTPULLOUT) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    ' update in packet table
                    lsSql = ""
                    lsSql &= " update chola_trn_tpacket set "
                    lsSql &= " packet_status = packet_status | " & GCIPACKETPULLOUT & " "
                    lsSql &= " where packet_gid = " & lnPktId & " "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    'Cheque Level
                    lsSql = " update chola_trn_tpdcentry set "
                    lsSql &= " chq_status=chq_status | " & GCPACKETPULLOUT & ","
                    lsSql &= " chq_desc = 'Swap Pullout'"
                    lsSql &= " where chq_packet_gid=" & lnPktId & " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    lsSql = " update chola_trn_tspdcchqentry set "
                    lsSql &= " chqentry_status = chqentry_status | " & GCPACKETPULLOUT & ","
                    lsSql &= " chqentry_remarks ='Swap Pullout'"
                    lsSql &= " where chqentry_packet_gid=" & lnPktId & " and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    lsSql = " update chola_trn_tecsemientry set "
                    lsSql &= " ecsemientry_status = ecsemientry_status | " & GCPACKETPULLOUT & " "
                    lsSql &= " where ecsemientry_packet_gid = " & lnPktId & " and ecsemientry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)
                Case GCOLDSWAPPKTPULLOUTUNDO
                    If MsgBox("Are you sure to undo ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    ' update pullout
                    lsSql = ""
                    lsSql &= " update chola_trn_toldswappacket set "
                    lsSql &= " oldpacket_status = (oldpacket_status | " & (GCOLDSWAPPKTPULLOUT Or GCOLDSWAPPKTPULLOUTUNDO) & ") ^ " & GCOLDSWAPPKTPULLOUT & " "
                    lsSql &= " where oldswappacket_gid = " & mnOldSwapPktId & " "
                    lsSql &= " and oldpacket_status & " & GCOLDSWAPPKTPULLOUT & " > 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    ' update in packet table
                    lsSql = ""
                    lsSql &= " update chola_trn_tpacket set "
                    lsSql &= " packet_status = (packet_status | " & GCIPACKETPULLOUT & ") ^ " & GCIPACKETPULLOUT
                    lsSql &= " where packet_gid = " & lnPktId & " "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    'Cheque Level
                    lsSql = " update chola_trn_tpdcentry set "
                    lsSql &= " chq_status=(chq_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT & ","
                    lsSql &= " chq_desc = ''"
                    lsSql &= " where chq_packet_gid=" & lnPktId & " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    lsSql = " update chola_trn_tspdcchqentry set "
                    lsSql &= " chqentry_status = (chqentry_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT & ","
                    lsSql &= " chqentry_remarks =''"
                    lsSql &= " where chqentry_packet_gid=" & lnPktId & " and chqentry_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)

                    lsSql = " update chola_trn_tecsemientry set "
                    lsSql &= " ecsemientry_status = (ecsemientry_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT
                    lsSql &= " where ecsemientry_packet_gid = " & lnPktId & " and ecsemientry_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    lnResult = gfInsertQry(lsSql, gOdbcConn)
            End Select

            MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)

            mnOldSwapPktId = 0
            txtSlNo.Text = ""
            txtPktNo.Text = ""
            txtAgmntNo.Text = ""
            txtPayMode.Text = ""

            txtSlNo.Focus()
        Else
            mnOldSwapPktId = 0
            txtPktNo.Text = ""
            txtAgmntNo.Text = ""
            txtPayMode.Text = ""

            MsgBox("Invalid packet !", MsgBoxStyle.Information, gProjectName)
            Exit Sub
        End If

        ds.Tables("pkt").Rows.Clear()
    End Sub

    Private Function RetrieveOldSwapPktInfo() As Boolean
        Dim lsSql As String
        Dim ds As New DataSet

        ' chk with packet
        If mnFlag <> GCOLDSWAPPKTPULLOUTUNDO Then
            lsSql = ""
            lsSql &= " select oldswappacket_gid,packet_gnsarefno,agreement_no,packet_mode from chola_trn_toldswappacket "
            lsSql &= " inner join chola_trn_tpacket on packet_gid = oldpacket_gid "
            lsSql &= " and packet_status & " & (GCPACKETRETRIEVAL Or GCIPACKETPULLOUT) & " = 0 "
            lsSql &= " inner join chola_mst_tagreement on agreement_gid = packet_agreement_gid "
            lsSql &= " where oldpacket_slno = '" & Val(txtSlNo.Text) & "' "
            lsSql &= " and oldpacket_status & " & (GCOLDSWAPPKTCANCELLED Or GCOLDSWAPPKTPULLOUT) & " = 0 "
        Else
            lsSql = ""
            lsSql &= " select oldswappacket_gid,packet_gnsarefno,agreement_no,packet_mode from chola_trn_toldswappacket "
            lsSql &= " inner join chola_trn_tpacket on packet_gid = oldpacket_gid "
            lsSql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
            lsSql &= " inner join chola_mst_tagreement on agreement_gid = packet_agreement_gid "
            lsSql &= " where oldpacket_slno = '" & Val(txtSlNo.Text) & "' "
            lsSql &= " and oldpacket_status & " & GCOLDSWAPPKTPULLOUT & " > 0 "
        End If

        Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

        If ds.Tables("pkt").Rows.Count > 0 Then
            mnOldSwapPktId = ds.Tables("pkt").Rows(0).Item("oldswappacket_gid")
            txtPktNo.Text = ds.Tables("pkt").Rows(0).Item("packet_gnsarefno").ToString
            txtAgmntNo.Text = ds.Tables("pkt").Rows(0).Item("agreement_no").ToString
            txtPayMode.Text = ds.Tables("pkt").Rows(0).Item("packet_mode").ToString
        Else
            mnOldSwapPktId = 0
            txtPktNo.Text = ""
            txtAgmntNo.Text = ""
            txtPayMode.Text = ""

            MsgBox("Invalid packet !", MsgBoxStyle.Information, gProjectName)
            Return False
        End If

        ds.Tables("pkt").Rows.Clear()
        Return True
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtSlNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSlNo.TextChanged

    End Sub

    Private Sub txtSlNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSlNo.Validating
        If txtSlNo.Text <> "" Then
            If RetrieveOldSwapPktInfo() = False Then e.Cancel = True
        End If
    End Sub

    Public Sub New(ByVal Flag As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        mnFlag = Flag
    End Sub
End Class