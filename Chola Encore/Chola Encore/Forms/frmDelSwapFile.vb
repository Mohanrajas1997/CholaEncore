Public Class frmDelSwapFile
    Dim msSQL As String
    Dim mnCount As Long
    Dim mbDelFlag As Boolean = False

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmDeleteFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpImpDate.Value = DateSerial(2000, 1, 1)
        dtpImpDate.Value = Now
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub dtpImportDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpImpDate.ValueChanged
        msSQL = ""
        msSQL &= " select swapfile_gid,concat(file_name,'-',sheet_name) as file_name from chola_trn_tswapfile "
        msSQL &= " where import_date >= '" & Format(CDate(dtpImpDate.Value), "yyyy-MM-dd") & "' "
        msSQL &= " and import_date < '" & Format(DateAdd("d", 1, CDate(dtpImpDate.Value)), "yyyy-MM-dd") & "' "

        gpBindCombo(msSQL, "file_name", "swapfile_gid", cboFileName, gOdbcConn)
    End Sub

    Private Sub cboFileName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFileName.TextChanged
        If cboFileName.SelectedIndex = -1 And cboFileName.Text <> "" Then gpAutoFillCombo(cboFileName)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnFileId As Long = 0
        Dim n As Integer

        Dim lnResult As Integer

        Try
            If cboFileName.SelectedIndex = -1 Or cboFileName.Text = "" Then
                MsgBox("Please select the file name to delete ?", MsgBoxStyle.Information, gProjectName)
                cboFileName.Focus()
                Exit Sub
            Else
                lnFileId = Val(cboFileName.SelectedValue)
            End If

            If MsgBox("Are you sure to delete ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
                msSQL = ""
                msSQL &= " select count(*) from chola_trn_tswap "
                msSQL &= " where swapfile_gid = " & lnFileId & " "
                msSQL &= " and oldpacket_count > 0 "

                n = gfExecuteScalar(msSQL, gOdbcConn)

                If n > 0 Then
                    MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
                    cboFileName.Focus()
                    Exit Sub
                End If

                msSQL = "delete from chola_trn_tswap where swapfile_gid = " & lnFileId & " "
                lnResult = gfInsertQry(msSQL, gOdbcConn)

                MsgBox("File deleted successfully  !", MsgBoxStyle.Information, gProjectName)

                dtpImpDate.Tag = dtpImpDate.Value
                dtpImpDate.Value = DateAdd(DateInterval.Day, 1, Now)
                dtpImpDate.Value = dtpImpDate.Tag
                dtpImpDate.Tag = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
End Class