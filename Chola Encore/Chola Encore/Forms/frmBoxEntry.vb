Public Class frmBoxEntry
    Dim lsFileType As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal FileType As String)
        lsFileType = FileType
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmBoxEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmBoxEntry_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmBoxEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call EnableSave(False)
        Call lp_ClearControl()
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim llBoxNo As Long
        Call EnableSave(True)
        Call lp_ClearControl()

        ' Fetching Box No 
        Sqlstr = ""
        Sqlstr &= " SELECT MAX(box_no) FROM chola_trn_tbox "
        Sqlstr &= " WHERE delete_flag ='N'"
        llBoxNo = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
        txtBoxNo.Text = llBoxNo + 1

        txtBoxBarcode.Focus()
    End Sub

    Private Sub lp_ClearControl()
        txtId.Text = ""
        txtBoxBarcode.Text = ""
        txtBoxNo.Text = ""
        txtGNSAREfNoFrom.Text = ""
        txtGNSAREfNoTo.Text = ""
        txtDocCount.Text = ""
        txtRemark.Text = ""
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Val(txtId.Text) <> 0 Then
                Call EnableSave(True)
                txtBoxBarcode.Focus()
            Else
                MsgBox("Select Record to Edit", MsgBoxStyle.Information, gProjectName)
                Call btnFind_Click(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As Search
        Try

            SearchDialog = New Search(gOdbcConn, _
                             " SELECT box_gid 'Box Gid', box_no 'Box No',  " & _
                             " gnsaref_from 'GNSA Ref From', gnsaref_to 'GNSA Ref To', " & _
                             " remarks as 'Remark' FROM chola_trn_tbox ", _
                             " box_gid, box_no,  gnsaref_from, gnsaref_to, remarks", _
                             " 1 = 1 AND delete_flag ='N' and doc_type='" & lsFileType & "' ORDER BY box_gid DESC ")

            SearchDialog.ShowDialog()

            If Val(txt) <> 0 Then
                Call ListAll("SELECT  * FROM chola_trn_tbox " _
                    & " WHERE box_gid = " & txt & " " _
                    & " AND delete_flag ='N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As Odbc.OdbcConnection)
        Dim ds As New DataSet
        Try
            ds = gfDataSet(SqlStr, "box", gOdbcConn)
            If ds.Tables("box").Rows.Count > 0 Then
                txtId.Text = Val(ds.Tables("box").Rows(0).Item("box_gid").ToString)
                txtBoxNo.Text = ds.Tables("box").Rows(0).Item("box_no").ToString
                txtGNSAREfNoFrom.Text = ds.Tables("box").Rows(0).Item("gnsaref_from").ToString
                txtGNSAREfNoTo.Text = ds.Tables("box").Rows(0).Item("gnsaref_to").ToString

                If lsFileType = "SPDC" Then
                    SqlStr = " SELECT COUNT(*) 'cnt' FROM chola_trn_tspdc "
                    SqlStr &= " WHERE spdc_box_gid = " & Val(txtId.Text.Trim)

                Else
                    SqlStr = " SELECT COUNT(*) 'cnt' FROM chola_trn_tpdcfile "
                    SqlStr &= " WHERE pdc_box_gid = " & Val(txtId.Text.Trim)

                End If



                txtDocCount.Text = Val(gfExecuteScalar(SqlStr, gOdbcConn))
                txtRemark.Text = ds.Tables("box").Rows(0).Item("remarks").ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnResult As Integer
        Try
            If Val(txtId.Text) <> 0 Then
                If MsgBox("Are you sure want to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                    Exit Sub
                End If
                Sqlstr = ""
                Sqlstr &= " UPDATE chola_trn_tbox"
                Sqlstr &= " SET delete_flag='Y'"
                Sqlstr &= " WHERE box_gid = " & txtId.Text.Trim
                Sqlstr &= " AND shelf_gid =0"
                Sqlstr &= " AND delete_flag ='N'"

                lnResult = gfInsertQry(Sqlstr, gOdbcConn)
                If lnResult <> 0 Then

                    If lsFileType = "SPDC" Then
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tspdc "
                        Sqlstr &= " SET spdc_box_gid = 0 "
                        Sqlstr &= " WHERE box_gid = " & Val(txtId.Text.Trim)

                    Else
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpdcfile "
                        Sqlstr &= " SET pdc_box_gid = 0"
                        Sqlstr &= " WHERE box_gid = " & Val(txtId.Text.Trim)

                    End If


                    lnResult = gfInsertQry(Sqlstr, gOdbcConn)
                    If lnResult <> 0 Then
                        MsgBox("Record Deleted!", MsgBoxStyle.Information, gProjectName)
                        Call lp_ClearControl()
                        Call EnableSave(False)
                    End If
                Else
                    MsgBox("Deletion Failed..", MsgBoxStyle.Information, gProjectName)
                    Exit Sub
                End If

            Else
                MsgBox("Select Record to Delete", MsgBoxStyle.Information, gProjectName)
                Call btnFind_Click(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
            MyBase.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call lp_ClearControl()
        Call EnableSave(False)
        btnClose.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnResult As Integer
        Dim llBoxGid As Long

        Try
            'If txtBoxBarcode.Text = "" Then
            '    If MsgBox("Box Barcode can't be blank, Do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            '        txtBoxBarcode.Focus()
            '        Exit Sub
            '    End If
            'End If

            If txtGNSAREfNoFrom.Text.Trim = "" Then
                MsgBox("GNSA REF No From can't be blank", MsgBoxStyle.Critical, gProjectName)
                txtGNSAREfNoFrom.Focus()
                Exit Sub
            End If

            If txtGNSAREfNoTo.Text.Trim = "" Then
                MsgBox("GNSA REF No To can't be blank", MsgBoxStyle.Critical, gProjectName)
                txtGNSAREfNoTo.Focus()
                Exit Sub
            End If

            Sqlstr = ""
            Sqlstr &= " SELECT box_gid FROM chola_trn_tbox"
            Sqlstr &= " WHERE ("
            Sqlstr &= "  box_no='" & txtBoxNo.Text & "') "
            If Val(txtId.Text) <> 0 Then
                Sqlstr &= " AND box_gid<>" & txtId.Text
            End If
            Sqlstr &= " AND delete_flag ='N'"

            llBoxGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

            If llBoxGid <> 0 Then
                MsgBox("Box No Duplicate", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            Sqlstr = ""
            Sqlstr &= " SELECT box_gid FROM chola_trn_tbox"
            Sqlstr &= " WHERE (" & txtGNSAREfNoFrom.Text & " BETWEEN gnsaref_from AND gnsaref_to"
            Sqlstr &= " OR " & txtGNSAREfNoTo.Text & " BETWEEN gnsaref_from AND gnsaref_to)"
            If Val(txtId.Text) <> 0 Then
                Sqlstr &= " AND box_gid<> " & txtId.Text
            End If
            Sqlstr &= " AND delete_flag ='N'"

            llBoxGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
            If llBoxGid <> 0 Then
                MsgBox("GNSA Ref No From/GNSA Ref No To already available", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            If Val(txtId.Text) = 0 Then
                Sqlstr = ""
                Sqlstr &= " INSERT INTO chola_trn_tbox "
                Sqlstr &= " ( box_no, gnsaref_from ,"
                Sqlstr &= " gnsaref_to, entry_date, entry_by, remarks,doc_type)"
                Sqlstr &= " VALUES("
                Sqlstr &= "" & txtBoxNo.Text & "," & Val(txtGNSAREfNoFrom.Text) & ","
                Sqlstr &= "" & Val(txtGNSAREfNoTo.Text) & ", "
                Sqlstr &= " SYSDATE(), "
                Sqlstr &= "'" & gUserName & "',"
                Sqlstr &= "'" & QuoteFilter(txtRemark.Text) & "',"
                Sqlstr &= "'" & QuoteFilter(lsFileType) & "')"
            Else
                ' Check shelf_gid mapped or not
                Sqlstr = ""
                Sqlstr &= " SELECT box_gid FROM chola_trn_tbox "
                Sqlstr &= " WHERE box_gid = " & txtId.Text
                Sqlstr &= " AND shelf_gid =0"
                Sqlstr &= " AND delete_flag ='N'"

                llBoxGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
                If llBoxGid = 0 Then
                    MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
                    Exit Sub
                End If

                Sqlstr = ""
                Sqlstr &= " UPDATE chola_trn_tbox"
                Sqlstr &= " SET "
                Sqlstr &= " box_no=" & txtBoxNo.Text & ","
                Sqlstr &= " gnsaref_from=" & Val(txtGNSAREfNoFrom.Text) & ","
                Sqlstr &= " gnsaref_to=" & Val(txtGNSAREfNoTo.Text) & ","
                Sqlstr &= " remarks='" & QuoteFilter(txtRemark.Text) & "', "
                Sqlstr &= " doc_type='" & QuoteFilter(lsFileType) & "' "
                Sqlstr &= " WHERE box_gid=" & txtId.Text.Trim
                Sqlstr &= " AND delete_flag ='N'"
            End If

            lnResult = gfInsertQry(Sqlstr, gOdbcConn)

            If Val(txtId.Text) <> 0 And lnResult <> 0 Then
                If lsFileType = "SPDC" Then
                    Sqlstr = ""
                    Sqlstr &= " UPDATE chola_trn_tspdc "
                    Sqlstr &= " SET spdc_box_gid=" & Val(txtId.Text)
                    Sqlstr &= " WHERE spdc_gnsarefno BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                    Sqlstr &= " AND spdc_box_gid =0 "
                Else
                    Sqlstr = ""
                    Sqlstr &= " UPDATE chola_trn_tpdcfile "
                    Sqlstr &= " SET pdc_box_gid=" & Val(txtId.Text)
                    Sqlstr &= " WHERE mid(pdc_gnsarefno,2,length(pdc_gnsarefno)) BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                    Sqlstr &= " AND pdc_box_gid =0 "
                End If

                lnResult = gfInsertQry(Sqlstr, gOdbcConn)
                MsgBox("Box Details updated successfully!", MsgBoxStyle.Information, gProjectName)

                Call lp_ClearControl()
                Call EnableSave(False)
            ElseIf Val(txtId.Text) = 0 And lnResult <> 0 Then
                Sqlstr = "SELECT MAX(box_gid) FROM chola_trn_tbox WHERE delete_flag ='N'"
                llBoxGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

                If lsFileType = "SPDC" Then
                    Sqlstr = ""
                    Sqlstr &= " UPDATE chola_trn_tspdc "
                    Sqlstr &= " SET spdc_box_gid=" & llBoxGid
                    Sqlstr &= " WHERE spdc_gnsarefno BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                    Sqlstr &= " AND spdc_box_gid =0 "
                Else
                    Sqlstr = ""
                    Sqlstr &= " UPDATE chola_trn_tpdcfile "
                    Sqlstr &= " SET pdc_box_gid=" & llBoxGid
                    Sqlstr &= " WHERE mid(pdc_gnsarefno,2,length(pdc_gnsarefno)) BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                    Sqlstr &= " AND pdc_box_gid =0 "
                End If



                gfInsertQry(Sqlstr, gOdbcConn)

                MsgBox("Box Details saved successfully", MsgBoxStyle.Information, gProjectName)
                If MsgBox("Do you want to add another Entry?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
                    Call btnNew_Click(sender, e)
                Else
                    Call lp_ClearControl()
                    Call EnableSave(False)
                End If
            Else
                MsgBox("Operation Failed", MsgBoxStyle.Information, gProjectName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNoTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGNSAREfNoTo.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtDocNoTo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtGNSAREfNoTo.Validating
        Dim lnDocCnt As Integer
        Try
            If Val(txtGNSAREfNoFrom.Text) > Val(txtGNSAREfNoTo.Text) And txtGNSAREfNoTo.Text <> "" Then
                MsgBox("Invalid Doc No To", MsgBoxStyle.Critical, gProjectName)
                e.Cancel = True
                txtGNSAREfNoTo.Focus()
                Exit Sub
            ElseIf txtGNSAREfNoFrom.Text <> "" And txtGNSAREfNoTo.Text <> "" Then

                If lsFileType = "SPDC" Then
                    Sqlstr = ""
                    Sqlstr &= " SELECT COUNT(*) FROM chola_trn_tspdc "
                    Sqlstr &= " WHERE (spdc_gnsarefno BETWEEN " & Val(txtGNSAREfNoFrom.Text.Trim) & " AND " & Val(txtGNSAREfNoTo.Text.Trim) & ")"
                    If txtId.Text = "" Then
                        Sqlstr &= " AND spdc_box_gid = 0 "
                    Else
                        Sqlstr &= " AND (spdc_box_gid = 0 OR spdc_box_gid =" & txtId.Text & ")"
                    End If

                Else
                    Sqlstr = ""
                    Sqlstr &= " SELECT COUNT(distinct(pdc_gnsarefno)) FROM chola_trn_tpdcfile "
                    Sqlstr &= " WHERE (mid(pdc_gnsarefno,2,length(pdc_gnsarefno)) BETWEEN " & Val(txtGNSAREfNoFrom.Text.Trim) & " AND " & Val(txtGNSAREfNoTo.Text.Trim) & ")"
                    If txtId.Text = "" Then
                        Sqlstr &= " AND pdc_box_gid = 0 "
                    Else
                        Sqlstr &= " AND (pdc_box_gid = 0 OR pdc_box_gid =" & txtId.Text & ")"
                    End If

                End If



                lnDocCnt = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
                If lnDocCnt < (Val(txtGNSAREfNoTo.Text) - Val(txtGNSAREfNoFrom.Text)) + 1 Then
                    MsgBox("Invalid GNSA Ref Nos.", MsgBoxStyle.Critical, gProjectName)
                    e.Cancel = True
                    txtGNSAREfNoTo.Focus()
                    Exit Sub
                End If
                txtDocCount.Text = lnDocCnt
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNoFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGNSAREfNoFrom.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtDocCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDocCount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtBoxNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBoxNo.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtBoxBarcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBoxBarcode.KeyPress
        If Asc(e.KeyChar) = 8 Then
            e.Handled = False
            Exit Sub
        End If
        'If AlphaNumeric(e.KeyChar) = "" Then e.Handled = True
    End Sub
End Class