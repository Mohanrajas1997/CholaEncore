Public Class frmbouncepulloutentry
    Dim lssql As String
    Dim lnBounceGid As Long
    Dim lnPdcId As Long
    Dim isclear As Boolean
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        isclear = True
        cboagreement.Text = ""
        txtchequeamt.Text = ""
        txtchqno.Text = ""
        mtxtchqdate.Text = ""
        cboreason.SelectedIndex = -1
        cboagreement.DataSource = Nothing
        cboagreement.SelectedIndex = -1
        txtchqslno.Text = ""
        txtremarks.Text = ""
        txtchqslno.Focus()
        isclear = False
    End Sub
    Private Sub frmbouncepulloutentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmbouncepulloutentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lssql = " select reason_gid,reason_name from chola_mst_tbouncereason where reason_deleteflag='N' "
        gpBindCombo(lssql, "reason_name", "reason_gid", cboreason, gOdbcConn)
        cboreason.SelectedIndex = -1

    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnPulloutGid As Long

        If Val(txtchqslno.Text) = 0 Then
            MsgBox("please enter Valid Slno", MsgBoxStyle.Critical)
            txtchqslno.Focus()
            Exit Sub
        End If

        If txtremarks.Text.Trim = "" Then
            MsgBox("please enter Remarks", MsgBoxStyle.Critical)
            txtremarks.Focus()
            Exit Sub
        End If

        lssql = " update chola_trn_tpdcentry set "
        lssql &= " chq_status = chq_status | " & GCBOUNCEPULLOUTENTRY & ""
        lssql &= " where entry_gid=" & lnPdcId & ""

        gfInsertQry(lssql, gOdbcConn)

        lssql = " insert into chola_trn_tbouncepullout (bouncepullout_bounceentry_gid,bouncepullout_agreement_gid,bouncepullout_chqno,"
        lssql &= "bouncepullout_chqdate, bouncepullout_chqamount,bouncepullout_remarks,bouncepullout_insertdate,bouncepullout_insertby)"
        lssql &= " values ("
        lssql &= "" & lnBounceGid & ","
        If cboagreement.SelectedValue Is Nothing Then
            lssql &= "" & 0 & ","
        Else
            lssql &= "" & cboagreement.SelectedValue & ","
        End If

        lssql &= "'" & txtchqno.Text.Trim & "',"
        lssql &= "'" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "',"
        lssql &= "" & Val(txtchequeamt.Text) & ","
        lssql &= "'" & txtremarks.Text.Trim & "',"
        lssql &= " sysdate(),'" & gUserName & "')"
        gfInsertQry(lssql, gOdbcConn)

        lssql = " select bouncepullout_gid "
        lssql &= " from chola_trn_tbouncepullout "
        lssql &= " where bouncepullout_bounceentry_gid=" & lnBounceGid
        lnPulloutGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        lssql = " update chola_trn_tbounceentry set "
        lssql &= " bounceentry_pullout_gid=" & lnPulloutGid
        lssql &= " where bounceentry_gid=" & lnBounceGid
        gfInsertQry(lssql, gOdbcConn)

        MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)

        btnclear.PerformClick()
        txtchqslno.Focus()
    End Sub

    Private Sub txtchequeamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchequeamt.LostFocus
        If Val(txtchequeamt.Text) > 0 Then
            If txtchqno.Text.Trim = "" Then
                MsgBox("please enter Cheque No", MsgBoxStyle.Information)
                Exit Sub
            ElseIf Not IsDate(mtxtchqdate.Text) Then
                MsgBox("please enter Valid Cheque Date", MsgBoxStyle.Information)
                Exit Sub
            Else
                lssql = " select agreement_gid,concat(shortagreement_no,'-',agreement_no) as agreement_no "
                lssql &= " from chola_trn_tpdcentry "
                lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid"
                lssql &= " where chq_no='" & txtchqno.Text.Trim & "'"
                lssql &= " and chq_amount=" & Val(txtchequeamt.Text.Trim)
                lssql &= " and chq_date='" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"

                gpBindCombo(lssql, "agreement_no", "agreement_gid", cboagreement, gOdbcConn)

                If cboagreement.Items.Count = 1 Then
                    cboagreement.SelectedIndex = 0
                Else
                    cboagreement.SelectedIndex = -1
                End If
            End If
        End If
    End Sub

    Private Sub txtchqslno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqslno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtchqno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtchqslno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchqslno.LostFocus
        If isclear Then Exit Sub
        If txtchqslno.Text.Trim = "" Then Exit Sub
        Dim drbounce As Odbc.OdbcDataReader

        lssql = ""
        lssql &= " select * "
        lssql &= " from chola_trn_tbounceentry "
        lssql &= " where bounceentry_slno=" & Val(txtchqslno.Text)
        lssql &= " and bounceentry_pullout_gid=0 "
        drbounce = gfExecuteQry(lssql, gOdbcConn)

        If drbounce.HasRows Then
            lnBounceGid = drbounce.Item("bounceentry_gid").ToString
            lnPdcId = drbounce.Item("bounceentry_entry_gid").ToString
            txtchqno.Text = drbounce.Item("bounceentry_chqno").ToString
            mtxtchqdate.Text = Format(CDate(drbounce.Item("bounceentry_chqdate").ToString), "dd-MM-yyyy")
            txtchequeamt.Text = drbounce.Item("bounceentry_chqamount").ToString
            txtchequeamt_LostFocus(sender, e)
            cboagreement.SelectedValue = drbounce.Item("bounceentry_agreement_gid").ToString
            cboreason.SelectedValue = drbounce.Item("bounceentry_bouncereason_gid").ToString
            txtremarks.Focus()
        Else
            MsgBox("Invalid Slno", MsgBoxStyle.Critical)
            txtchqslno.Focus()
            Exit Sub
        End If
    End Sub
End Class