Public Class frmremovefinonepreconversion

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpimportdate.Value = Now
        dtpimportdate.Checked = False
        cbofilename.Items.Clear()
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        Dim lssql As String

        If dtpimportdate.Checked = False Then
            MsgBox("Please Select Import Date..!", MsgBoxStyle.Critical, gProjectName)
            dtpimportdate.Focus()
            Exit Sub
        End If

        If cbofilename.Text.Trim = "" Then
            MsgBox("Please Select File Name..!", MsgBoxStyle.Critical, gProjectName)
            cbofilename.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " delete from chola_trn_tfinonepreconverfile "
        lssql &= " where finone_file_gid=" & cbofilename.SelectedValue
        gfInsertQry(lssql, gOdbcConn)

        MsgBox("File Deleted Successfully..!", MsgBoxStyle.Information, gProjectName)
        btnclear.PerformClick()

    End Sub
    Public Sub FillCombo()
        Dim lsSql As String
        Dim dt As New DataTable
        lsSql = ""
        lsSql = " Select"
        lsSql &= " distinct mf.file_gid as file_gid,"
        lsSql &= " mf.file_name as file_name"
        lsSql &= " FROM chola_mst_tfile as mf"
        lsSql &= " inner join chola_trn_tfinonepreconverfile as tf where tf.finone_file_gid=mf.file_gid"
        lsSql &= " and date_format(import_on,'%Y-%m-%d')='" & Format(CDate(dtpimportdate.Text), "yyyy-MM-dd") & "'"

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

    Private Sub dtpimportdate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpimportdate.ValueChanged
        FillCombo()
    End Sub
End Class