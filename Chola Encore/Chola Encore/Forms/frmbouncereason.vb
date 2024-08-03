Imports System.Data.Odbc
Public Class frmbouncereason
    Dim fsSql As String = String.Empty
    Dim mnResult As Integer = 0
    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub frmbouncereason_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmbouncereason_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmbouncereason_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EnableSave(False)
        btnNew.Focus()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If txtid.Text = "" Then
                If MsgBox("Do you want to select the record ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    Call btnFind_Click(sender, e)
                    EnableSave(False)
                End If
            Else
                EnableSave(True)
                txtreasonname.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As Search
        Try
            SearchDialog = New Search(gOdbcConn, _
                               " select reason_gid as 'Gid',reason_name as 'Reason Name'" & _
                               " from chola_mst_tbouncereason ", _
                               " reason_gid,reason_name", _
                               " 1=1 and reason_deleteflag = 'N'")

            SearchDialog.ShowDialog()

            If Val(txt) <> 0 Then
                Call ListAll("select * from chola_mst_tbouncereason" _
                            & " where reason_gid = '" & txt & "'" _
                            & " and reason_deleteflag = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub ListAll(ByVal fsSql As String, ByVal odbcconn As Odbc.OdbcConnection)
        Dim ds As New DataSet

        Try
            ds = gfDataSet(fsSql, "Handsofftype", gOdbcConn)

            txtid.Text = Val(ds.Tables("Handsofftype").Rows(0).Item("reason_gid").ToString)
            txtreasonname.Text = ds.Tables("Handsofftype").Rows(0).Item("reason_name").ToString

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If txtid.Text = "" Then
                If MsgBox("Do you want to select the record ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    btnFind.PerformClick()
                End If
            Else
                EnableSave(True)
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then

                    fsSql = " select pullout_gid from chola_trn_tpullout where pullout_reasongid=" & txtid.Text

                    If Val(gfExecuteScalar(fsSql, gOdbcConn)) = 0 Then

                        fsSql = " update chola_mst_tbouncereason set reason_deleteflag = 'Y' "
                        fsSql &= " where reason_gid = " & txtid.Text

                        mnResult = gfInsertQry(fsSql, gOdbcConn)

                        If mnResult > 0 Then
                            MsgBox("Record Deleted Successfully !!", MsgBoxStyle.Information, gProjectName)
                        End If

                        Call lp_ClearCtrl()
                        EnableSave(False)
                    Else
                        MsgBox("Select Reason Mapped With Pull out list.. You can't delete..", MsgBoxStyle.Information, gProjectName)
                    End If

                Else
                    Call lp_ClearCtrl()
                    EnableSave(False)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub lp_ClearCtrl()
        txtid.Text = ""
        txtreasonname.Text = ""
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        EnableSave(True)
        Call lp_ClearCtrl()
        txtreasonname.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        EnableSave(False)
        Call lp_ClearCtrl()
        btnNew.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim lschqlevel As Char = ""

            If txtreasonname.Text = "" Then
                MsgBox("Please enter the Reason Name")
                txtreasonname.Focus()
                Exit Sub
            End If

            If ChkDuplicate() = False Then
                MsgBox("Duplicate records are not allowed")
                Call lp_ClearCtrl()
                txtreasonname.Focus()
                Exit Sub
            End If


            If txtid.Text = "" Then
                fsSql = " insert into chola_mst_tbouncereason(reason_name,reason_insertdate,reason_insertby)"
                fsSql &= " values('" & QuoteFilter(txtreasonname.Text.Trim) & "',"
                fsSql &= "sysdate(),'" & gUserName & "')"
            Else
                fsSql = " update chola_mst_tbouncereason set"
                fsSql &= " reason_name = '" & QuoteFilter(txtreasonname.Text.Trim) & "',"
                fsSql &= " reason_updatedate=sysdate(),"
                fsSql &= " reason_updateby='" & gUserName & "'"
                fsSql &= " where reason_gid = " & Val(txtid.Text.Trim)
                fsSql &= " and reason_deleteflag = 'N'"
            End If

            mnResult = gfInsertQry(fsSql, gOdbcConn)

            If mnResult > 0 Then
                MsgBox("Record saved successfully !", MsgBoxStyle.Information, gProjectName)
            End If

            If MsgBox("Do you want to insert new record ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
                Call btnNew_Click(sender, e)
            Else
                Call lp_ClearCtrl()
                EnableSave(False)
                btnNew.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function ChkDuplicate() As Boolean
        Dim lnGid As Long = 0
        fsSql = " select reason_gid from chola_mst_tbouncereason"
        fsSql &= " where reason_name = '" & QuoteFilter(txtreasonname.Text.Trim) & "'"
        If txtid.Text <> "" Then
            fsSql &= " and reason_gid <> '" & Val(txtid.Text.Trim) & "'"
        End If
        fsSql &= " and reason_deleteflag = 'N'"
        lnGid = Val(gfExecuteScalar(fsSql, gOdbcConn))

        If lnGid <> 0 Then
            ChkDuplicate = False
        Else
            ChkDuplicate = True
        End If
        Return ChkDuplicate
    End Function
End Class