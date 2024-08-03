Imports System.Data.Odbc
Public Class frmholidayreason
    Dim fsSql As String = String.Empty
    Dim mnResult As Integer = 0
    Dim ldholidaydate As Date
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
                dtpHolidayDate.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        ldholidaydate = dtpHolidayDate.Value
        Dim SearchDialog As Search
        Try
            SearchDialog = New Search(gOdbcConn, _
                               " select holiday_gid as 'Gid',holiday_date as 'Holiday Date'," & _
                               " holiday_reason as 'Reason Name' from chola_mst_tholiday ", _
                               " holiday_gid,holiday_reason", _
                               "  holiday_deleteflag = 'N'")

            SearchDialog.ShowDialog()

            If Val(txt) <> 0 Then
                Call ListAll("select * from chola_mst_tholiday" _
                            & " where holiday_gid = '" & txt & "'" _
                            & " and holiday_deleteflag = 'N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub ListAll(ByVal fsSql As String, ByVal odbcconn As Odbc.OdbcConnection)
        Dim ds As New DataSet

        Try
            ds = gfDataSet(fsSql, "Holiday", gOdbcConn)

            txtid.Text = Val(ds.Tables("Holiday").Rows(0).Item("holiday_gid").ToString)
            ldholidaydate = ds.Tables("Holiday").Rows(0).Item("holiday_date").ToString
            dtpHolidayDate.Value = ds.Tables("Holiday").Rows(0).Item("holiday_date").ToString
            txtholidayReason.Text = ds.Tables("Holiday").Rows(0).Item("holiday_reason").ToString

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ldholidaydate = dtpHolidayDate.Value
        Try
            If txtid.Text = "" Then
                If MsgBox("Do you want to select the record ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
                    'Calling Find Button to select record
                    btnFind.PerformClick()
                End If
            Else
                EnableSave(True)
                If MsgBox("Are you sure to delete this record?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then

                    fsSql = " update chola_mst_tholiday set holiday_deleteflag = 'Y' "
                    fsSql &= " where holiday_gid = " & txtid.Text

                    mnResult = gfInsertQry(fsSql, gOdbcConn)

                    If mnResult > 0 Then
                        MsgBox("Record Deleted Successfully !!", MsgBoxStyle.Information, gProjectName)
                    End If

                    Call lp_ClearCtrl()
                    EnableSave(False)
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
        ldholidaydate = Now()
        txtholidayReason.Text = ""
        dtpHolidayDate.Value = Now()
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        ldholidaydate = dtpHolidayDate.Value
        EnableSave(True)
        Call lp_ClearCtrl()
        dtpHolidayDate.Focus()
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
        ldholidaydate = dtpHolidayDate.Value
        Try
            Dim lschqlevel As Char = ""
            If Not IsDate(ldholidaydate) Then
                MsgBox("Please enter the Holiday Date")
                dtpHolidayDate.Focus()
                Exit Sub
            End If
            If txtholidayReason.Text = "" Then
                MsgBox("Please enter the Reason Name")
                txtholidayReason.Focus()
                Exit Sub
            End If

            If ChkDuplicate() = False Then
                MsgBox("Duplicate records are not allowed")
                Call lp_ClearCtrl()
                dtpHolidayDate.Focus()
                Exit Sub
            End If


            If txtid.Text = "" Then
                fsSql = " insert into chola_mst_tholiday(holiday_date,holiday_reason,holiday_insertdate,holiday_insertby)"
                fsSql &= " values( '" & Format((ldholidaydate), "yyyy-MM-dd") & "', "
                fsSql &= "'" & QuoteFilter(txtholidayReason.Text.Trim) & "',"
                fsSql &= "sysdate(),'" & gUserName & "')"
            Else
                fsSql = " update chola_mst_tholiday set"
                fsSql &= " holiday_date = '" & Format((ldholidaydate), "yyyy-MM-dd") & "', "
                fsSql &= " holiday_reason = '" & QuoteFilter(txtholidayReason.Text.Trim) & "',"
                fsSql &= " holiday_updatedate=sysdate(),"
                fsSql &= " holiday_updateby='" & gUserName & "'"
                fsSql &= " where holiday_gid = " & Val(txtid.Text.Trim)
                fsSql &= " and holiday_deleteflag = 'N'"
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
        fsSql = " select holiday_gid from chola_mst_tholiday"
        fsSql &= " where holiday_date = '" & Format((ldholidaydate), "yyyy-MM-dd") & "' "
        If txtid.Text <> "" Then
            fsSql &= " and holiday_gid <> '" & Val(txtid.Text.Trim) & "'"
        End If
        fsSql &= " and holiday_deleteflag = 'N'"
        lnGid = Val(gfExecuteScalar(fsSql, gOdbcConn))

        If lnGid <> 0 Then
            ChkDuplicate = False
        Else
            ChkDuplicate = True
        End If
        Return ChkDuplicate
    End Function

    Private Sub dtpHolidayDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpHolidayDate.ValueChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class