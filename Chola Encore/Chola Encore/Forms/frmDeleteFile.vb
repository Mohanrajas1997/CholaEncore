Public Class frmDeleteFile
    Dim msPTbName As String
    Dim msPFldName As String
    Dim msPImpDateFldName As String
    Dim msPTbCondition As String
    Dim msDispFldName As String
    Dim msRTbName As String
    Dim msRTbFldName As String
    Dim msRTbFlag As String
    Dim msRTbCondition As String
    Dim msSQL As String
    Dim mnCount As Long
    Dim mbDelFlag As Boolean = False
    Dim mbDelFldFlag As Boolean = True

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmDeleteFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpImportDate.Value = DateSerial(2000, 1, 1)
        dtpImportDate.Value = Now
    End Sub

    Public Sub New(ByVal PTbName As String, ByVal PFldName As String, ByVal PImpDateFldName As String, ByVal DispFldName As String, ByVal PTbCondition As String, _
                   ByVal RTbName As String, ByVal RTbFldName As String, ByVal RTbFlag As String, ByVal RTbCondition As String, ByVal FrmCaption As String, Optional ByVal DelFlag As Boolean = False, Optional ByVal DelFldFlag As Boolean = True)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        msPTbName = PTbName
        msPFldName = PFldName
        msPImpDateFldName = PImpDateFldName
        msPTbCondition = PTbCondition
        msDispFldName = DispFldName
        msRTbName = RTbName
        msRTbFldName = RTbFldName
        msRTbFlag = RTbFlag.ToUpper
        msRTbCondition = RTbCondition
        mbDelFlag = DelFlag
        mbDelFldFlag = DelFldFlag

        Me.Text = FrmCaption
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub dtpImportDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpImportDate.ValueChanged
        msSQL = ""
        msSQL &= " select " & msPFldName & "," & msDispFldName & " as file_name from " & msPTbName
        msSQL &= " where " & msPImpDateFldName & " >= '" & Format(CDate(dtpImportDate.Value), "yyyy-MM-dd") & "' "
        msSQL &= " and " & msPImpDateFldName & " < '" & Format(DateAdd("d", 1, CDate(dtpImportDate.Value)), "yyyy-MM-dd") & "' "
        msSQL &= msPTbCondition
        If mbDelFldFlag = True Then msSQL &= " and delete_flag = 'N'"

        gpBindCombo(msSQL, "file_name", msPFldName, cboFileName, gOdbcConn)
    End Sub

    Private Sub cboFileName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFileName.TextChanged
        If cboFileName.SelectedIndex = -1 And cboFileName.Text <> "" Then gpAutoFillCombo(cboFileName)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ds As DataSet
        Dim i As Integer, n As Integer

        Dim lnResult As Integer

        Try
            If cboFileName.SelectedIndex = -1 Or cboFileName.Text = "" Then
                MsgBox("Please select the file name to delete ?", MsgBoxStyle.Information, gProjectName)
                cboFileName.Focus()
                Exit Sub
            End If

            If MsgBox("Are you sure to delete ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
                n = msRTbName.Split(",").Length

                ' Foreign key reference check
                For i = 0 To n - 1
                    If msRTbFlag.Split(",")(i) = "TRUE" Then
                        msSQL = ""
                        msSQL &= " select * from " & msRTbName.Split(",")(i)
                        msSQL &= " where " & msRTbFldName.Split(",")(i) & " = " & cboFileName.SelectedValue.ToString
                        msSQL &= " " & msRTbCondition
                        If mbDelFldFlag = True Then msSQL &= " and delete_flag = 'N'"

                        ds = gfDataSet(msSQL, "fkey", gOdbcConn)

                        If ds.Tables("fkey").Rows.Count > 0 Then
                            MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
                            cboFileName.Focus()
                            Exit Sub
                        End If
                    End If
                Next i

                If mbDelFlag = False And mbDelFldFlag = True Then
                    msSQL = ""
                    msSQL &= " update " & msPTbName & " set delete_flag = 'Y',delete_date = sysdate(),delete_by = '" & gUserName & "' "
                    msSQL &= " where " & msPFldName & " = " & cboFileName.SelectedValue.ToString & " "
                    msSQL &= msPTbCondition
                    If mbDelFldFlag = True Then msSQL &= " and delete_flag = 'N'"
                Else
                    msSQL = ""
                    msSQL &= " delete from " & msPTbName & " "
                    msSQL &= " where " & msPFldName & " = " & cboFileName.SelectedValue.ToString & " "
                    msSQL &= msPTbCondition
                    If mbDelFldFlag = True Then msSQL &= " and delete_flag = 'N'"
                End If

                lnResult = gfInsertQry(msSQL, gOdbcConn)

                For i = 0 To n - 1
                    If mbDelFlag = False And mbDelFldFlag = True Then
                        msSQL = ""
                        msSQL &= " update " & msRTbName.Split(",")(i) & " set delete_flag = 'Y' "
                        msSQL &= " where " & msRTbFldName.Split(",")(i) & " = " & cboFileName.SelectedValue.ToString & " "
                        If mbDelFldFlag = True Then msSQL &= " and delete_flag = 'N'"
                    Else
                        msSQL = ""
                        msSQL &= " delete from " & msRTbName.Split(",")(i) & " "
                        msSQL &= " where " & msRTbFldName.Split(",")(i) & " = " & cboFileName.SelectedValue.ToString & " "
                        If mbDelFldFlag = True Then msSQL &= " and delete_flag = 'N'"
                    End If

                    lnResult = gfInsertQry(msSQL, gOdbcConn)
                Next i

                MsgBox("File deleted successfully  !", MsgBoxStyle.Information, gProjectName)

                dtpImportDate.Tag = dtpImportDate.Value
                dtpImportDate.Value = DateAdd(DateInterval.Day, 1, Now)
                dtpImportDate.Value = dtpImportDate.Tag
                dtpImportDate.Tag = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
End Class