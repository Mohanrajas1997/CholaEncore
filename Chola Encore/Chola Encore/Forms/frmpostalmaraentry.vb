Public Class frmpostalmaraentry
    Dim lssql As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objdt As DataTable

        lssql = " select * from chola_trn_almaraentry where almaraentry_deleteflag='N' "
        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            Application.DoEvents()
            Label1.Text = "Processing- Cuboard:" & Val(objdt.Rows(i).Item("almaraentry_cupboardno").ToString) & " Shelf:" & Val(objdt.Rows(i).Item("almaraentry_shelfno").ToString) & " Box:" & Val(objdt.Rows(i).Item("almaraentry_boxno").ToString)

            Sqlstr = ""
            Sqlstr &= " UPDATE chola_trn_tpacket "
            Sqlstr &= " SET packet_box_gid=" & Val(objdt.Rows(i).Item("almaraentry_gid").ToString)
            Sqlstr &= " WHERE mid(packet_gnsarefno,2,length(packet_gnsarefno)) BETWEEN " & Val(Replace(objdt.Rows(i).Item("almaraentry_refnofrom").ToString, "P", "")) & " AND " & Val(Replace(objdt.Rows(i).Item("almaraentry_refnoto").ToString, "P", ""))
            Sqlstr &= " AND packet_box_gid =0 "

            gfInsertQry(Sqlstr, gOdbcConn)
        Next
        MsgBox("Process Completed")
    End Sub
End Class