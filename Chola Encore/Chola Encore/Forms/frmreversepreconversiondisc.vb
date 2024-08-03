Public Class frmreversepreconversiondisc

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpcycledate.Value = Now
        dtpcycledate.Checked = False
    End Sub

    Private Sub btnreverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreverse.Click
        Dim lssql As String

        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date..!", MsgBoxStyle.Critical, gProjectName)
            dtpcycledate.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " update chola_trn_tpdcentry set "
        lssql &= " chq_status = (chq_status | " & GCMATCHFINONEPRECOVERFILE & " ) ^ " & GCMATCHFINONEPRECOVERFILE & ","
        lssql &= " chq_predisc=0 "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        gfInsertQry(lssql, gOdbcConn)

        MsgBox("Successfully Reversed..!", MsgBoxStyle.Information, gProjectName)
    End Sub
End Class