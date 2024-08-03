Public Class frmPatchupdate

    Private Sub btnupdatepatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdatepatch.Click
        Dim lssql As String
        Dim objdt As DataTable
        Dim dtrecords As DataTable

        Me.Cursor = Cursors.WaitCursor

        lssql = " select pdc_shortpdc_parentcontractno,pdc_parentcontractno,pdc_chqno from chola_trn_tpdcfile "
        lssql &= " where chq_rec_slno='0' "
        lssql &= " group by pdc_shortpdc_parentcontractno,pdc_chqno"

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lbltotalrecords.Text = "Processing " & objdt.Rows.Count & "/" & i + 1
            Application.DoEvents()
            lssql = " select pdc_gid from chola_trn_tpdcfile"
            lssql &= " where pdc_parentcontractno='" & objdt.Rows(i).Item("pdc_parentcontractno").ToString & "'"
            lssql &= " and pdc_chqno='" & objdt.Rows(i).Item("pdc_chqno").ToString & "'"
            dtrecords = GetDataTable(lssql)

            For j As Integer = 0 To dtrecords.Rows.Count - 1
                lssql = " update chola_trn_tpdcfile set chq_rec_slno=" & j + 1
                lssql &= " where pdc_gid=" & dtrecords.Rows(j).Item("pdc_gid").ToString
                gfInsertQry(lssql, gOdbcConn)
            Next

        Next

        MsgBox("Process Completed", MsgBoxStyle.Information)
        Me.Cursor = Cursors.Default
    End Sub
End Class