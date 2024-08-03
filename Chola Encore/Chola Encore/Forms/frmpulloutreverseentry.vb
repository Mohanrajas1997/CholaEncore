Public Class frmpulloutreverseentry
    Dim lssql As String
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        cboagreement.Text = ""
        txtchequeamt.Text = ""
        txtchqno.Text = ""
        mtxtchqdate.Text = ""
        cboagreement.DataSource = Nothing
        cboagreement.SelectedIndex = -1

    End Sub
    Private Sub frmpulloutreverseentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnPdcId As Long
        Dim lnPulloutId As Long
        Dim ds As New DataSet

        If txtchqno.Text.Trim = "" Then
            MsgBox("please enter Cheque No", MsgBoxStyle.Critical)
            txtchqno.Focus()
            Exit Sub
        End If

        If Not IsDate(mtxtchqdate.Text) Then
            MsgBox("please enter Valid Cheque Date", MsgBoxStyle.Critical)
            mtxtchqdate.Focus()
            Exit Sub
        End If

        If Val(txtchequeamt.Text) = 0 Then
            MsgBox("please enter Valid Cheque Amount", MsgBoxStyle.Critical)
            txtchequeamt.Focus()
            Exit Sub
        End If

        If cboagreement.Text.Trim = "" Then
            MsgBox("please enter Agreementno", MsgBoxStyle.Critical)
            cboagreement.Focus()
            Exit Sub
        End If


        lssql = " select * from chola_trn_tpullout "
        lssql &= " where 1 = 1 "
        lssql &= " and pullout_shortagreementno='" & cboagreement.Text & "'"
        lssql &= " and pullout_chqno = '" & txtchqno.Text.Trim & "' "
        lssql &= " and pullout_chqdate = '" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"
        lssql &= " and pullout_chqamount = " & Val(txtchequeamt.Text) & ""

        Call gpDataSet(lssql, "pdc", gOdbcConn, ds)

        With ds.Tables("pdc")
            Select Case .Rows.Count
                Case 1
                    lnPdcId = .Rows(0).Item("pullout_entrygid")
                    lnPulloutId = .Rows(0).Item("pullout_gid")

                    'Reverse Pulout Entry
                    lssql = ""
                    lssql &= " update chola_trn_tpdcentry "
                    lssql &= " set chq_status = ( chq_status | 256 |64 | 32 ) ^ (256 | 64 | 32) "
                    lssql &= " where entry_gid = " & lnPdcId

                    gfInsertQry(lssql, gOdbcConn)

                    'Copy to Pullout delete table
                    lssql = ""
                    lssql &= " insert into chola_trn_tpulloutdel "
                    lssql &= " select * from chola_trn_tpullout "
                    lssql &= " where pullout_gid = " & lnPulloutId
                    gfInsertQry(lssql, gOdbcConn)

                    'Reverse From Pullout Table
                    lssql = ""
                    lssql &= " delete from chola_trn_tpullout "
                    lssql &= " where pullout_gid = " & lnPulloutId

                    gfInsertQry(lssql, gOdbcConn)

                    MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)
                Case 0
                    MsgBox("Invalid record !", MsgBoxStyle.Critical, gProjectName)
                Case Else
                    MsgBox("More than one record found !", MsgBoxStyle.Critical, gProjectName)
            End Select

            .Rows.Clear()
        End With
        btnclear.PerformClick()
        txtchqno.Focus()
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
                lssql = " select pullout_shortagreementno"
                lssql &= " from chola_trn_tpullout "
                lssql &= " where pullout_chqno='" & txtchqno.Text.Trim & "'"
                lssql &= " and pullout_chqamount=" & Val(txtchequeamt.Text.Trim)
                lssql &= " and pullout_chqdate='" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"
                gpBindCombo(lssql, "pullout_shortagreementno", "pullout_shortagreementno", cboagreement, gOdbcConn)
                cboagreement.SelectedIndex = -1
            End If
        End If
    End Sub
End Class
