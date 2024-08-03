Public Class frmremovefinoneunmatchedcase
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtagreementno.Text = ""
        txtagreementno.Focus()
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        'Dim lssql As String
        'Dim lnPDCGid As Long

        'If txtagreementno.Text.Trim = "" Then
        '    MsgBox("Please Enter Agreement No..!", MsgBoxStyle.Critical, gProjectName)
        '    txtagreementno.Focus()
        '    Exit Sub
        'End If

        'lssql = ""
        'lssql &= " select pdc_gid "
        'lssql &= " from chola_trn_tpdcfile "
        'lssql &= " where pdc_shortpdc_parentcontractno='" & txtagreementno.Text.Trim & "'"
        'lnPDCGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        'If lnPDCGid = 0 Then
        '    MsgBox("Invalid Agreement No..!", MsgBoxStyle.Critical, gProjectName)
        '    txtagreementno.Focus()
        '    Exit Sub
        'End If

        'lssql = ""
        'lssql &= " delete from chola_trn_tpdcfile "
        'lssql &= " where pdc_shortpdc_parentcontractno='" & txtagreementno.Text.Trim & "'"
        'lssql &= " and (entry_gid = 0 or entry_gid is null) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "
        'gfInsertQry(lssql, gOdbcConn)

        'MsgBox("Removed Successfully..!", MsgBoxStyle.Information, gProjectName)
    End Sub
End Class