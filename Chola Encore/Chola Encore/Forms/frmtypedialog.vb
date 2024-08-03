Public Class frmtypedialog

    
    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        If cbotype.Text.Trim = "" Then
            MsgBox("Please select Type", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            cbotype.Focus()
            Exit Sub
        End If

        If Me.Tag = "Entry" Then
            Dim objfrm As New frmsummary(cbotype.Text.Trim)
            objfrm.MdiParent = frmMain
            objfrm.Show()
        ElseIf Me.Tag = "Report" Then
            Dim objfrm As New frmFileReport(cbotype.Text.Trim)
            objfrm.MdiParent = frmMain
            objfrm.Show()       
        ElseIf Me.Tag = "Box" Then
            Dim objfrm As New frmBoxEntry(cbotype.Text.Trim)
            objfrm.MdiParent = frmMain
            objfrm.Show()
        End If
        
        Me.Close()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub
End Class