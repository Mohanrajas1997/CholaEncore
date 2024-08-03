Public Class frmChngPktPayMode

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lsSql As String
        Dim lnPktId As Long

        If txtGnsaRefNo.Text.Trim = "" Then
            MsgBox("Please enter GNSA Ref No !", MsgBoxStyle.Information, gProjectName)
            txtGnsaRefNo.Focus()
            Exit Sub
        End If

        If (txtGnsaRefNo.Text.Length > 12 Or txtGnsaRefNo.Text.Length < 11) Then
            MsgBox("Invalid GNSA Ref No !", MsgBoxStyle.Information, gProjectName)
            txtGnsaRefNo.Focus()
            Exit Sub
        End If

        If txtGnsaRefNo.Text.Length = 12 Then
            If IsNumeric(txtGnsaRefNo.Text.Substring(0, 1)) Then
                MsgBox("Invalid GNSA Ref No !", MsgBoxStyle.Information, gProjectName)
                txtGnsaRefNo.Focus()
                Exit Sub
            End If
        End If

        If cboPayMode.Text = "" Or cboPayMode.SelectedIndex = -1 Then
            MsgBox("Please select the payment mode !", MsgBoxStyle.Information, gProjectName)
            cboPayMode.Focus()
            Exit Sub
        End If

        If txtGnsaRefNo.Text.Length = 11 Then
            If Not IsNumeric(txtGnsaRefNo.Text.Substring(0, 1)) Then
                MsgBox("Invalid GNSA Ref No", MsgBoxStyle.Critical, gProjectName)
                txtGnsaRefNo.Focus()
                Exit Sub
            End If
        End If

        lssql = ""
        lssql &= " select packet_gid from chola_trn_tpacket "
        lssql &= " where packet_gnsarefno='" & txtGnsaRefNo.Text.Trim & "'"

        lnPktId = Val(gfExecuteScalar(lsSql, gOdbcConn))

        If lnPktId = 0 Then
            MsgBox("Invalid GNSA Ref No !", MsgBoxStyle.Information, gProjectName)
            txtGnsaRefNo.Focus()
            Exit Sub
        End If

        ' update in packet
        lsSql = ""
        lsSql &= " update chola_trn_tpacket set "
        lsSql &= " packet_mode = '" & cboPayMode.Text.ToUpper & "' "
        lsSql &= " where packet_gid = " & lnPktId & " "

        Call gfInsertQry(lsSql, gOdbcConn)

        MsgBox("Packet updated successfully !", MsgBoxStyle.Information, gProjectName)

        txtGnsaRefNo.Text = ""
        cboPayMode.Text = ""

        txtGnsaRefNo.Focus()
    End Sub

    Private Sub frmChngPktPayMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With cboPayMode
            .Items.Clear()
            .Items.Add("PDC")
            .Items.Add("SPDC")
        End With
    End Sub
End Class