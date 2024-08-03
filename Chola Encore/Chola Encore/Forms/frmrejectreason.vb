Public Class frmrejectreason

    Private Sub frmrejectreason_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lssql As String
        lssql = ""
        lssql &= " select reason_name from chola_mst_trejectreason "
        lssql &= " where reason_deleteflag='N' "
        gpBindCombo(lssql, "reason_name", "reason_name", cboreason, gOdbcConn)

    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        If cboreason.Text = "" Then
            Exit Sub
        End If
        GRejectReason = cboreason.Text
        Me.Close()
    End Sub
End Class