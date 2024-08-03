Public Class frmpostproductupdate
    Public Sub FillCombo()
        Dim lsSql As String
        Dim dt As New DataTable
        lsSql = ""
        lsSql = " Select"
        lsSql &= " distinct mf.file_gid as file_gid,"
        lsSql &= " mf.file_name as file_name"
        lsSql &= " FROM chola_mst_tfile as mf"
        lsSql &= " inner join chola_trn_tproductupdate as tf on tf.update_file_gid=mf.file_gid "
        lsSql &= " where import_on >= '" & Format(CDate(dtpDate.Text), "yyyy-MM-dd") & "' "
        lsSql &= " and import_on < '" & Format(DateAdd(DateInterval.Day, 1, CDate(dtpDate.Text)), "yyyy-MM-dd") & "' "

        dt = GetDataTable(lsSql)

        If dt.Rows.Count > 0 Then
            cbofilename.DataSource = dt
            cbofilename.DisplayMember = "file_name"
            cbofilename.ValueMember = "file_gid"
            cbofilename.SelectedIndex = -1
        Else
            cbofilename.DataSource = Nothing
            cbofilename.SelectedIndex = -1
        End If
    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged
        FillCombo()
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Dim lssql As String
        Dim objdt As DataTable

        Dim lnAgreementGid As Long

        If dtpDate.Checked = False Then
            MsgBox("Please Select Import Date", MsgBoxStyle.Information)
            Exit Sub
        End If

        If cbofilename.Text.Trim = "" Then
            MsgBox("Please Select File Name", MsgBoxStyle.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor


        lssql = ""
        lssql &= " select update_gid,update_type_gid,update_agreementno"
        lssql &= " from chola_trn_tproductupdate "
        lssql &= " where update_file_gid=" & cbofilename.SelectedValue
        lssql &= " and update_postflag='N' "

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lssql = ""
            lssql &= " select agreement_gid "
            lssql &= " from chola_mst_tagreement "
            lssql &= " where agreement_no='" & objdt.Rows(i).Item("update_agreementno").ToString & "'"
            lnAgreementGid = Val(gfExecuteScalar(lssql, gOdbcConn))

            If lnAgreementGid > 0 Then
                lssql = ""
                lssql &= " update chola_mst_tagreement set "
                lssql &= " agreement_prodtype=" & Val(objdt.Rows(i).Item("update_type_gid").ToString)
                lssql &= " where agreement_gid=" & lnAgreementGid
                gfInsertQry(lssql, gOdbcConn)

                lssql = ""
                lssql &= " update chola_trn_tpdcentry set "
                lssql &= " chq_prodtype=" & Val(objdt.Rows(i).Item("update_type_gid").ToString)
                lssql &= " where chq_agreement_gid=" & lnAgreementGid
                lssql &= " and chq_status & " & GCDESPATCH & " = 0 "
                lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
                lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0"
                lssql &= " and chq_status & " & GCPULLOUT & " = 0"
                lssql &= " and chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "

                gfInsertQry(lssql, gOdbcConn)

                lssql = ""
                lssql &= " update chola_trn_tproductupdate set "
                lssql &= " update_postflag='Y'"
                lssql &= " where update_gid=" & Val(objdt.Rows(i).Item("update_gid").ToString)
                gfInsertQry(lssql, gOdbcConn)
            End If
        Next

        MsgBox("process Completed", MsgBoxStyle.Information)
        Me.Cursor = Cursors.Default
    End Sub

End Class