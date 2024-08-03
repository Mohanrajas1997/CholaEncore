Public Class frmbounceinward

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtawbno.Text = ""
        txtchqfromslno.Text = ""
        txtchqslnoto.Text = ""
        txtcouriername.Text = ""
        txtcouriername.Focus()
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lssql As String
        'Dim lnInwardGid As Long

        If txtcouriername.Text.Trim = "" Then
            MsgBox("Please Enter Courier Name..!", MsgBoxStyle.Critical, gProjectName)
            txtcouriername.Focus()
            Exit Sub
        End If

        If txtawbno.Text.Trim = "" Then
            MsgBox("Please Enter Airwaybill no..!", MsgBoxStyle.Critical, gProjectName)
            txtawbno.Focus()
            Exit Sub
        End If

        'If Val(txtchqfromslno.Text.Trim) = 0 Then
        '    MsgBox("Please Enter From Chq sl no..!", MsgBoxStyle.Critical, gProjectName)
        '    txtchqfromslno.Focus()
        '    Exit Sub
        'End If

        'If Val(txtchqslnoto.Text.Trim) = 0 Then
        '    MsgBox("Please Enter To Chq sl no..!", MsgBoxStyle.Critical, gProjectName)
        '    txtchqslnoto.Focus()
        '    Exit Sub
        'End If

        'lssql = ""
        'lssql &= " SELECT inward_gid FROM chola_trn_tbounceinward"
        'lssql &= " WHERE (" & txtchqfromslno.Text & " BETWEEN inward_chqfromslno AND inward_chqtoslno"
        'lssql &= " OR " & txtchqslnoto.Text & " BETWEEN inward_chqfromslno AND inward_chqtoslno)"
        'lssql &= " and inward_deleteflag='N' "

        'lnInwardGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        'If lnInwardGid > 0 Then
        '    MsgBox("Chq Sl From/Chq Sl To already available", MsgBoxStyle.Critical, gProjectName)
        '    Exit Sub
        'End If

        lssql = ""
        lssql &= " insert into chola_trn_tbounceinward ("
        lssql &= " inward_couriername,inward_awbno,inward_chqfromslno,inward_chqtoslno,"
        lssql &= " inward_insertdate,inward_insertby)"
        lssql &= " values ("
        lssql &= "'" & txtcouriername.Text.Trim & "',"
        lssql &= "'" & txtawbno.Text.Trim & "',"
        lssql &= "" & Val(txtchqfromslno.Text.Trim) & ","
        lssql &= "" & Val(txtchqslnoto.Text.Trim) & ","
        lssql &= "sysdate(),'" & gUserName & "')"

        gfInsertQry(lssql, gOdbcConn)

        'lssql = ""
        'lssql &= " SELECT inward_gid FROM chola_trn_tbounceinward"
        'lssql &= " WHERE (" & txtchqfromslno.Text & " BETWEEN inward_chqfromslno AND inward_chqtoslno"
        'lssql &= " OR " & txtchqslnoto.Text & " BETWEEN inward_chqfromslno AND inward_chqtoslno)"
        'lssql &= " and inward_deleteflag='N' "

        'lnInwardGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        'If lnInwardGid > 0 Then
        '    lssql = ""
        '    lssql &= " update chola_trn_tbounceentry set "
        '    lssql &= " bounceentry_inward_gid=" & lnInwardGid
        '    lssql &= " where bounceentry_slno between " & txtchqfromslno.Text & " and " & txtchqslnoto.Text
        '    gfInsertQry(lssql, gOdbcConn)

        '    MsgBox("Sucessfully Updated..!", MsgBoxStyle.Information, gProjectName)
        '    btnclear.PerformClick()
        'End If

        MsgBox("Sucessfully Updated..!", MsgBoxStyle.Information, gProjectName)
        btnclear.PerformClick()
    End Sub
End Class