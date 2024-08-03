Public Class frmretrievalmissingentry
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtagreementno.Text = ""
        txtremarks.Text = ""
        txtworkitemno.Text = ""
        cboretrievalmode.Text = ""
        rbtnmissing.Checked = False
        rbtnrequested.Checked = False
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lssql As String
        Dim liresult As Integer

        If txtagreementno.Text.Trim = "" Then
            MsgBox("Please Enter Agreement No..!", MsgBoxStyle.Critical, gProjectName)
            txtagreementno.Focus()
            Exit Sub
        End If

        If txtworkitemno.Text.Trim = "" Then
            MsgBox("Please Enter Work Item No..!", MsgBoxStyle.Critical, gProjectName)
            txtworkitemno.Focus()
            Exit Sub
        End If

        If cboretrievalmode.Text.Trim = "" Then
            MsgBox("Please select Retrieval Mode..!", MsgBoxStyle.Critical, gProjectName)
            cboretrievalmode.Focus()
            Exit Sub
        End If

        If rbtnmissing.Checked = False And rbtnrequested.Checked = False Then
            MsgBox("Please Select Status..!", MsgBoxStyle.Critical, gProjectName)
            rbtnmissing.Focus()
            Exit Sub
        End If

        If rbtnmissing.Checked Then
            lssql = ""
            lssql &= " update chola_trn_tretrieval "
            lssql &= " set retrieval_status = retrieval_status |" & GCMISSING
            lssql &= " where retrieval_gid=" & txtworkitemno.Text.Trim
            lssql &= " and retrieval_status = " & GCREQUESTED
            liresult = gfInsertQry(lssql, gOdbcConn)
        ElseIf rbtnrequested.Checked Then
            lssql = ""
            lssql &= " update chola_trn_tretrieval "
            lssql &= " set retrieval_status = = " & GCREQUESTED
            lssql &= " where retrieval_gid=" & txtworkitemno.Text.Trim
            lssql &= " and retrieval_status & " & GCRETRIEVED & " = 0 "
            liresult = gfInsertQry(lssql, gOdbcConn)
        ElseIf rbtnCancel.Checked Then
            lssql = ""
            lssql &= " update chola_trn_tretrieval "
            lssql &= " set retrieval_status = retrieval_status |" & GCRTRCANCEL
            lssql &= " where retrieval_gid=" & txtworkitemno.Text.Trim
            lssql &= " and retrieval_status = " & GCREQUESTED
            liresult = gfInsertQry(lssql, gOdbcConn)
        End If

        MsgBox("Successfully Updated..!", MsgBoxStyle.Information, gProjectName)

    End Sub
    Private Sub txtworkitemno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtworkitemno.LostFocus
        Dim drretrieval As Odbc.OdbcDataReader
        Dim lssql As String

        If txtworkitemno.Text.Trim <> "" Then
            If txtagreementno.Text.Trim = "" Then
                MsgBox("Please Enter Agreement No..!", MsgBoxStyle.Critical, gProjectName)
                txtagreementno.Focus()
                Exit Sub
            End If

            lssql = ""
            lssql &= " select retrieval_gid,retrieval_mode,retrieval_gnsarefno,retrieval_chqno,retrieval_status "
            lssql &= " from chola_trn_tretrieval "
            lssql &= " where 1=1 "
            lssql &= " and retrieval_shortagreementno='" & txtagreementno.Text.Trim & "'"
            lssql &= " and retrieval_gid=" & txtworkitemno.Text.Trim
            lssql &= " and retrieval_status & " & GCRETRIEVED & " = 0 "

            drretrieval = gfExecuteQry(lssql, gOdbcConn)

            If drretrieval.HasRows Then
                cboretrievalmode.Text = drretrieval.Item("retrieval_mode").ToString

                If (drretrieval.Item("retrieval_status") And GCMISSING) = GCMISSING Then
                    rbtnmissing.Checked = True
                ElseIf (drretrieval.Item("retrieval_status") And GCRTRCANCEL) = GCRTRCANCEL Then
                    rbtnCancel.Checked = True
                Else
                    rbtnrequested.Checked = True
                End If
            Else
                MsgBox("Invalid Details..!", MsgBoxStyle.Critical, gProjectName)
                txtagreementno.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Private Sub txtworkitemno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtworkitemno.TextChanged

    End Sub
End Class