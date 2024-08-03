Public Class frmauthdateupdate

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtgnsarefno.Text = ""
        dtpauthdate.Value = Now
        dtpauthdate.Checked = False
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Dim lssql As String
        Dim lnInwardGid As Long
        Dim drpacket As Odbc.OdbcDataReader

        If txtgnsarefno.Text.Trim = "" Then
            MsgBox("Please Enter GNSA Ref#..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        End If

        If dtpauthdate.Checked = False Then
            MsgBox("Please Select Auth Date..!", MsgBoxStyle.Critical, gProjectName)
            dtpauthdate.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select packet_inward_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " where packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
        drpacket = gfExecuteQry(lssql, gOdbcConn)

        If drpacket.HasRows Then
            drpacket.Read()
            lnInwardGid = Val(drpacket.Item("packet_inward_gid").ToString)

            lssql = ""
            lssql &= " update chola_trn_tinward set "
            lssql &= " inward_userauthdate='" & Format(dtpauthdate.Value, "yyyy-MM-dd") & "'"
            lssql &= " where inward_gid=" & lnInwardGid
            gfInsertQry(lssql, gOdbcConn)
            btnclear.PerformClick()
            MsgBox("Update Successfully..!", MsgBoxStyle.Information, gProjectName)
        Else
            MsgBox("Invalid GNSA REF#..!", MsgBoxStyle.Critical, gProjectName)
            txtgnsarefno.Focus()
            Exit Sub
        End If

    End Sub
End Class