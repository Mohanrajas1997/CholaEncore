Public Class frmremovefinone

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtagreementno.Text = ""
    End Sub

    Private Sub btnreverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        Dim ds As New DataSet
        Dim lssql As String
        Dim lnPDCGid As Long
        Dim lnPktId As Long

        If txtagreementno.Text.Trim = "" Then
            MsgBox("Please Enter Agreement No..!", MsgBoxStyle.Critical, gProjectName)
            txtagreementno.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select pdc_gid "
        lssql &= " from chola_trn_tpdcfile "
        lssql &= " where pdc_parentcontractno='" & txtagreementno.Text.Trim & "'"
        lssql &= " and pdc_importdate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "' "

        lnPDCGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnPDCGid = 0 Then
            MsgBox("Invalid Agreement No..!", MsgBoxStyle.Critical, gProjectName)
            txtagreementno.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select pdc_gid "
        lssql &= " from chola_trn_tpdcfile "
        lssql &= " where pdc_parentcontractno='" & txtagreementno.Text.Trim & "'"
        lssql &= " and pdc_importdate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "' "
        lssql &= " and (entry_gid > 0 or pdc_spdcentry_gid > 0 or pdc_ecsentry_gid > 0) "
        lnPDCGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnPDCGid > 0 Then
            MsgBox("Access Denied..!", MsgBoxStyle.Critical, gProjectName)
            txtagreementno.Focus()
            Exit Sub
        Else
            lssql = ""
            lssql &= " select head_packet_gid from chola_trn_tpdcfilehead "
            lssql &= " where head_agreementno = '" & txtagreementno.Text.Trim & "' "
            lssql &= " and head_systemauthdate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "' "

            lnPktId = Val(gfExecuteScalar(lssql, gOdbcConn))

            If lnPktId > 0 Then
                lssql = ""
                lssql &= " update chola_trn_tpacket set "
                lssql &= " packet_status = packet_status ^ " & GCPKTAUTHFINONE & " "
                lssql &= " where packet_gid = " & lnPktId & " "
                lssql &= " and packet_status & " & GCPKTAUTHFINONE & " > 0 "

                Call gfInsertQry(lssql, gOdbcConn)
            End If

            lssql = ""
            lssql &= " delete from chola_trn_tpdcfile "
            lssql &= " where pdc_parentcontractno='" & txtagreementno.Text.Trim & "'"
            lssql &= " and pdc_importdate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "' "

            Call gfInsertQry(lssql, gOdbcConn)

            lssql = ""
            lssql &= " delete from chola_trn_tpdcfilehead "
            lssql &= " where head_agreementno='" & txtagreementno.Text.Trim & "'"
            lssql &= " and head_systemauthdate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "' "

            Call gfInsertQry(lssql, gOdbcConn)

            MsgBox("Removed Successfully..!", MsgBoxStyle.Information, gProjectName)
        End If
    End Sub

    Private Sub frmremovefinone_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDate.Value = Now
    End Sub
End Class