Public Class frmmanualbatch
    Dim lssql As String
    Private Sub btnopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnopen.Click
        Dim lnResult As Long
        Dim lnbatchid As Long
        Dim drbatch As Odbc.OdbcDataReader

        If txtbatchno.Text.Trim = "" Then
            MsgBox("Please Enter Batch No.!", MsgBoxStyle.Critical, gProjectName)
            txtbatchno.Focus()
            Exit Sub
        End If

        If cboproduct.Text.Trim = "" Then
            MsgBox("Please Select Product.!", MsgBoxStyle.Critical, gProjectName)
            cboproduct.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select * "
        lssql &= " from chola_trn_tbatch "
        lssql &= " where batch_deleteflag='N' "
        lssql &= " and batch_no=" & txtbatchno.Text.Trim
        drbatch = gfExecuteQry(lssql, gOdbcConn)

        If drbatch.HasRows Then
            lssql = ""
            lssql &= " select batch_gid "
            lssql &= " from chola_trn_tbatch "
            lssql &= " where batch_deleteflag='N' "
            lssql &= " and batch_gid=" & drbatch.Item("batch_gid").ToString
            lssql &= " and batch_istally='N' "
            lssql &= " and batch_totalchq=batch_entrychq "
            lssql &= " and batch_totalchqamt=batch_entrychqamt "
            lnbatchid = Val(gfExecuteScalar(lssql, gOdbcConn))
            If lnbatchid > 0 Then

                If MsgBox("Batch Already open.." & vbCrLf & " Do You Want to continue.?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                    Exit Sub
                End If

                Dim frmentry As New frmmanualbatchentry(lnbatchid)
                frmentry.ShowDialog()
            Else
                MsgBox("Access Denied..!", MsgBoxStyle.Critical, gProjectName)
            End If
        Else
            lssql = ""
            lssql &= " insert into chola_trn_tbatch ("
            lssql &= " batch_no,batch_displayno,batch_prodtype,batch_cycledate,batch_inserdate,batch_insertby ) "
            lssql &= " values ( "
            lssql &= "" & Val(txtbatchno.Text.Trim) & ","
            lssql &= "'" & Format(Val(txtbatchno.Text.Trim), "0000") & "',"
            lssql &= "" & cboproduct.SelectedValue & ","
            lssql &= "'" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "',"
            lssql &= "sysdate(),'" & gUserName & "')"

            gfInsertQry(lssql, gOdbcConn)

            Do
                ' count
                lssql = ""
                lssql &= " select count(*) "
                lssql &= " from chola_trn_tbatch "
                lssql &= " where true "
                lssql &= " and batch_no=" & txtbatchno.Text.Trim
                lssql &= " and batch_deleteflag='N' "

                lnResult = Val(gfExecuteScalar(lssql, gOdbcConn))

                If lnResult > 1 Then
                    lssql = ""
                    lssql &= " select max(batch_gid) "
                    lssql &= " from chola_trn_tbatch "
                    lssql &= " where true "
                    lssql &= " and batch_no=" & txtbatchno.Text.Trim
                    lssql &= " and batch_totalchq = 0 "
                    lssql &= " and batch_deleteflag='N' "

                    lnbatchid = Val(gfExecuteScalar(lssql, gOdbcConn))

                    lssql = ""
                    lssql &= " delete from chola_trn_tbatch "
                    lssql &= " where batch_gid = " & lnbatchid

                    Call gfInsertQry(lssql, gOdbcConn)

                    lnbatchid = 0
                End If
            Loop Until lnResult <= 1

            lssql = ""
            lssql &= " select batch_gid "
            lssql &= " from chola_trn_tbatch "
            lssql &= " where batch_deleteflag='N' "
            lssql &= " and batch_no=" & txtbatchno.Text.Trim

            lnbatchid = Val(gfExecuteScalar(lssql, gOdbcConn))

            Dim frmentry As New frmmanualbatchentry(lnbatchid)
            frmentry.ShowDialog()
        End If

    End Sub

    Private Sub frmmanualbatch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lssql = ""
        lssql &= " select type_flag,type_name "
        lssql &= " from chola_mst_ttype "
        lssql &= " where type_deleteflag='N' "
        lssql &= " order by type_name "

        gpBindCombo(lssql, "type_name", "type_flag", cboproduct, gOdbcConn)
        cboproduct.SelectedIndex = -1
    End Sub

    Private Sub txtbatchno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbatchno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub
End Class