Public Class frmalmaraentry
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

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub lp_ClearControl()
        txtId.Text = ""
        txtcupboardno.Text = ""
        txtBoxNo.Text = ""
        txtGNSAREfNoFrom.Text = ""
        txtGNSAREfNoTo.Text = ""
        txtDocCount.Text = ""
        txtshelfno.Text = ""
    End Sub

    Private Sub ListAll(ByVal SqlStr As String, ByVal odbcConn As Odbc.OdbcConnection)
        Dim ds As New DataSet
        Try
            ds = gfDataSet(SqlStr, "box", gOdbcConn)
            If ds.Tables("box").Rows.Count > 0 Then
                txtId.Text = Val(ds.Tables("box").Rows(0).Item("almaraentry_gid").ToString)
                txtcupboardno.Text = ds.Tables("box").Rows(0).Item("almaraentry_cupboardno").ToString
                txtshelfno.Text = ds.Tables("box").Rows(0).Item("almaraentry_shelfno").ToString
                txtboxno.Text = ds.Tables("box").Rows(0).Item("almaraentry_boxno").ToString
                txtGNSAREfNoFrom.Text = Replace(ds.Tables("box").Rows(0).Item("almaraentry_refnofrom").ToString, "P", "")
                txtGNSAREfNoTo.Text = Replace(ds.Tables("box").Rows(0).Item("almaraentry_refnoto").ToString, "P", "")

                SqlStr = " SELECT COUNT(*) 'cnt' FROM chola_trn_tpacket "
                SqlStr &= " WHERE packet_box_gid = " & Val(txtId.Text.Trim)
                SqlStr &= " and packet_mode='" & lsFileType & "'"

                txtDocCount.Text = Val(gfExecuteScalar(SqlStr, gOdbcConn))
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    'Private Sub txtDocNoFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    e.Handled = gfIntEntryOnly(e)
    'End Sub

    'Private Sub txtDocCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    e.Handled = gfIntEntryOnly(e)
    'End Sub

    'Private Sub txtBoxNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBoxNo.KeyPress
    '    e.Handled = gfIntEntryOnly(e)
    'End Sub


    'Private Sub txtcupboardno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcupboardno.KeyPress
    '    e.Handled = gfIntEntryOnly(e)
    'End Sub

    'Private Sub txtshelfno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtshelfno.KeyPress
    '    e.Handled = gfIntEntryOnly(e)
    'End Sub

    Private Sub frmalmaraentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmalmaraentry_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmalmaraentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call EnableSave(False)
        Call lp_ClearControl()
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

            If txtcupboardno.Text.Trim = "" Then
                MsgBox("Cupboard No can't be blank", MsgBoxStyle.Critical, gProjectName)
                txtcupboardno.Focus()
                Exit Sub
            End If

            If txtshelfno.Text.Trim = "" Then
                MsgBox("Shelf No can't be blank", MsgBoxStyle.Critical, gProjectName)
                txtshelfno.Focus()
                Exit Sub
            End If

            If txtboxno.Text.Trim = "" Then
                MsgBox("Box No can't be blank", MsgBoxStyle.Critical, gProjectName)
                txtboxno.Focus()
                Exit Sub
            End If

            If txtGNSAREfNoFrom.Text.Trim = "" Then
                MsgBox("GNSA REF No From can't be blank", MsgBoxStyle.Critical, gProjectName)
                txtGNSAREfNoFrom.Focus()
                Exit Sub
            End If

            If txtGNSAREfNoFrom.Text.Length > 12 Then
                MsgBox("Please Enter 12 digit GNSA REF No", MsgBoxStyle.Critical, gProjectName)
                txtGNSAREfNoFrom.Focus()
                Exit Sub
            End If

            If txtGNSAREfNoTo.Text.Trim <> "" Then
                If txtGNSAREfNoTo.Text.Length > 12 Then
                    MsgBox("Please Enter 12 digit GNSA REF No", MsgBoxStyle.Critical, gProjectName)
                    txtGNSAREfNoTo.Focus()
                    Exit Sub
                End If
            End If

            'If txtGNSAREfNoTo.Text.Trim = "" Then
            '    MsgBox("GNSA REF No To can't be blank", MsgBoxStyle.Critical, gProjectName)
            '    txtGNSAREfNoTo.Focus()
            '    Exit Sub
            'End If

            Sqlstr = ""
            Sqlstr &= " SELECT almaraentry_gid FROM chola_trn_almaraentry"
            Sqlstr &= " WHERE "
            Sqlstr &= "  almaraentry_cupboardno='" & txtcupboardno.Text & "'"
            Sqlstr &= " and almaraentry_shelfno='" & txtshelfno.Text & "'"
            Sqlstr &= " and almaraentry_boxno='" & txtboxno.Text & "'"
            Sqlstr &= " and almaraentry_refnofrom = '" & txtGNSAREfNoFrom.Text & "' "
            Sqlstr &= " and almaraentry_refnoto = '" & txtGNSAREfNoTo.Text & "' "
            If Val(txtId.Text) <> 0 Then
                Sqlstr &= " AND almaraentry_gid<>" & txtId.Text
            End If
            Sqlstr &= " AND almaraentry_deleteflag ='N'"

            llBoxGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

            If llBoxGid <> 0 Then
                MsgBox("Duplicate Entry", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            'If txtGNSAREfNoTo.Text.Trim <> "" Then
            '    Sqlstr = ""
            '    Sqlstr &= " SELECT almaraentry_gid FROM chola_trn_almaraentry"
            '    Sqlstr &= " WHERE (" & txtGNSAREfNoFrom.Text.Replace("P", "").Replace("N", "") & " BETWEEN almaraentry_refnofrom AND almaraentry_refnoto"
            '    Sqlstr &= " OR " & txtGNSAREfNoTo.Text.Replace("P", "").Replace("N", "") & " BETWEEN almaraentry_refnofrom AND almaraentry_refnoto)"
            '    If Val(txtId.Text) <> 0 Then
            '        Sqlstr &= " AND almaraentry_gid<> " & txtId.Text
            '    End If
            '    Sqlstr &= " AND almaraentry_deleteflag ='N'"
            '    Sqlstr &= " and almaraentry_type='" & lsFileType & "'"

            '    llBoxGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
            '    If llBoxGid <> 0 Then
            '        MsgBox("GNSA Ref No From/GNSA Ref No To already available", MsgBoxStyle.Critical, gProjectName)
            '        Exit Sub
            '    End If
            'End If

            If Val(txtId.Text) = 0 Then
                Sqlstr = ""
                Sqlstr &= " INSERT INTO chola_trn_almaraentry "
                Sqlstr &= " (almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno, almaraentry_refnofrom ,"
                Sqlstr &= " almaraentry_refnoto,almaraentry_type,almaraentry_insertdate,almaraentry_insertby)"
                Sqlstr &= " VALUES("
                Sqlstr &= "" & txtcupboardno.Text & ","
                Sqlstr &= "" & txtshelfno.Text & ","
                Sqlstr &= "" & txtboxno.Text & ",'" & txtGNSAREfNoFrom.Text & "',"

                If txtGNSAREfNoTo.Text.Trim <> "" Then
                    Sqlstr &= "'" & txtGNSAREfNoTo.Text & "', "
                Else
                    Sqlstr &= "null, "
                End If

                Sqlstr &= "'" & QuoteFilter(lsFileType) & "',"
                Sqlstr &= " SYSDATE(), "
                Sqlstr &= "'" & gUserName & "')"

            Else

                Sqlstr = ""
                Sqlstr &= " update chola_trn_almaraentry set "
                Sqlstr &= "almaraentry_cupboardno=" & txtcupboardno.Text & ","
                Sqlstr &= "almaraentry_shelfno=" & txtshelfno.Text & ","
                Sqlstr &= "almaraentry_boxno=" & txtboxno.Text & ",almaraentry_refnofrom='" & txtGNSAREfNoFrom.Text & "',"
                If txtGNSAREfNoTo.Text.Trim <> "" Then
                    Sqlstr &= "almaraentry_refnoto='" & txtGNSAREfNoTo.Text & "', "
                Else
                    Sqlstr &= "almaraentry_refnoto='null, "
                End If
                Sqlstr &= "almaraentry_type='" & QuoteFilter(lsFileType) & "',"
                Sqlstr &= "almaraentry_updatedate= SYSDATE(), "
                Sqlstr &= "almaraentry_updateby='" & gUserName & "'"
                Sqlstr &= " WHERE almaraentry_gid=" & txtId.Text.Trim
                Sqlstr &= " AND almaraentry_deleteflag ='N'"
            End If

            lnResult = gfInsertQry(Sqlstr, gOdbcConn)

            If Val(txtId.Text) <> 0 And lnResult <> 0 Then

                Sqlstr = " UPDATE chola_trn_tpacket "
                Sqlstr &= " SET packet_box_gid=0"
                Sqlstr &= " ,packet_status= (packet_status | " & GCPACKETVAULTED & ") ^ " & GCPACKETVAULTED
                Sqlstr &= " where packet_box_gid=" & Val(txtId.Text)
                gfInsertQry(Sqlstr, gOdbcConn)

                If lsFileType = "PDC" Then
                    If txtGNSAREfNoTo.Text.Trim <> "" Then
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & Val(txtId.Text)
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','') BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    Else
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & Val(txtId.Text)
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','')='" & txtGNSAREfNoFrom.Text & "'"
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    End If
                Else
                    If txtGNSAREfNoTo.Text.Trim <> "" Then
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & Val(txtId.Text)
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','') BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    Else
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & Val(txtId.Text)
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','')='" & txtGNSAREfNoFrom.Text & "'"
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    End If
                End If
                lnResult = gfInsertQry(Sqlstr, gOdbcConn)
                MsgBox("Almara Details updated successfully!", MsgBoxStyle.Information, gProjectName)

                Call lp_ClearControl()
                Call EnableSave(False)
            ElseIf Val(txtId.Text) = 0 And lnResult <> 0 Then
                Sqlstr = "SELECT MAX(almaraentry_gid) FROM chola_trn_almaraentry WHERE almaraentry_deleteflag ='N'"
                llBoxGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

                If lsFileType = "PDC" Then
                    If txtGNSAREfNoTo.Text.Trim <> "" Then
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & llBoxGid
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','') BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    Else
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & llBoxGid
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','')='" & txtGNSAREfNoFrom.Text & "'"
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    End If

                Else
                    If txtGNSAREfNoTo.Text.Trim <> "" Then
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & llBoxGid
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','') BETWEEN " & Val(txtGNSAREfNoFrom.Text) & " AND " & Val(txtGNSAREfNoTo.Text)
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    Else
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tpacket "
                        Sqlstr &= " SET packet_box_gid=" & llBoxGid
                        Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
                        Sqlstr &= " WHERE replace(replace(upper(packet_gnsarefno),'P',''),'N','')='" & txtGNSAREfNoFrom.Text & "'"
                        Sqlstr &= " AND packet_box_gid =0 "
                        Sqlstr &= " and packet_mode='" & lsFileType & "'"
                    End If
                End If
                gfInsertQry(Sqlstr, gOdbcConn)

                MsgBox("Almara Details saved successfully", MsgBoxStyle.Information, gProjectName)
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
            MyBase.Close()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnResult As Integer
        Try
            If Val(txtId.Text) <> 0 Then
                If MsgBox("Are you sure want to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                    Exit Sub
                End If
                Sqlstr = ""
                Sqlstr &= " UPDATE chola_trn_almaraentry"
                Sqlstr &= " SET almaraentry_deleteflag='Y'"
                Sqlstr &= " WHERE almaraentry_gid = " & txtId.Text.Trim
                Sqlstr &= " AND almaraentry_deleteflag ='N'"

                lnResult = gfInsertQry(Sqlstr, gOdbcConn)
                If lnResult <> 0 Then

                    Sqlstr = ""
                    Sqlstr &= " UPDATE chola_trn_tpacket "
                    Sqlstr &= " SET packet_box_gid = 0"
                    Sqlstr &= " WHERE packet_box_gid = " & Val(txtId.Text.Trim)
                    Sqlstr &= " and packet_mode='" & lsFileType & "'"

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

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Val(txtId.Text) <> 0 Then
                Call EnableSave(True)
                txtcupboardno.Focus()
            Else
                MsgBox("Select Record to Edit", MsgBoxStyle.Information, gProjectName)
                Call btnFind_Click(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As Search
        Try

            SearchDialog = New Search(gOdbcConn, _
                             " SELECT almaraentry_gid 'Gid',almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno 'Box No',  " & _
                             " almaraentry_refnofrom 'GNSA Ref From', almaraentry_refnoto 'GNSA Ref To' " & _
                             "  FROM chola_trn_almaraentry ", _
                             " almaraentry_gid, almaraentry_cupboardno,  almaraentry_shelfno, almaraentry_boxno", _
                             " 1 = 1 AND almaraentry_deleteflag ='N' and almaraentry_type='" & lsFileType & "' ORDER BY almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno ")

            SearchDialog.ShowDialog()

            If Val(txt) <> 0 Then
                Call ListAll("SELECT  * FROM chola_trn_almaraentry " _
                    & " WHERE almaraentry_gid = " & txt & " " _
                    & " AND almaraentry_deleteflag ='N'", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Call EnableSave(True)
        Call lp_ClearControl()
        txtcupboardno.Focus()
    End Sub

    Private Sub txtcupboardno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcupboardno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtcupboardno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcupboardno.TextChanged

    End Sub

    Private Sub txtshelfno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtshelfno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtshelfno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtshelfno.TextChanged

    End Sub

    Private Sub txtboxno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtboxno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtboxno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtboxno.TextChanged

    End Sub

    Private Sub txtGNSAREfNoFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGNSAREfNoFrom.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtGNSAREfNoFrom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGNSAREfNoFrom.TextChanged

    End Sub

    Private Sub txtGNSAREfNoTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGNSAREfNoTo.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtGNSAREfNoTo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGNSAREfNoTo.TextChanged

    End Sub

    Private Sub txtGNSAREfNoTo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGNSAREfNoTo.Validated

    End Sub

    Private Sub txtGNSAREfNoTo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtGNSAREfNoTo.Validating
        Dim lnDocCnt As Integer
        Try
            If Val(txtGNSAREfNoFrom.Text) > Val(txtGNSAREfNoTo.Text) And txtGNSAREfNoTo.Text <> "" Then
                MsgBox("Invalid Doc No To", MsgBoxStyle.Critical, gProjectName)
                e.Cancel = True
                txtGNSAREfNoTo.Focus()
                Exit Sub
            ElseIf txtGNSAREfNoFrom.Text <> "" And txtGNSAREfNoTo.Text <> "" Then

                If lsFileType = "PDC" Then

                    Sqlstr = ""
                    Sqlstr &= " SELECT COUNT(distinct(packet_gnsarefno)) FROM chola_trn_tpacket "
                    Sqlstr &= " WHERE (replace(replace(upper(packet_gnsarefno),'P',''),'N','') BETWEEN " & Val(txtGNSAREfNoFrom.Text.Trim) & " AND " & Val(txtGNSAREfNoTo.Text.Trim) & ")"
                    If txtId.Text = "" Then
                        Sqlstr &= " AND packet_box_gid = 0 "
                    Else
                        Sqlstr &= " AND (packet_box_gid = 0 OR packet_box_gid =" & txtId.Text & ")"
                    End If
                    Sqlstr &= " and packet_mode='" & lsFileType & "'"

                    lnDocCnt = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
                Else

                    Sqlstr = ""
                    Sqlstr &= " SELECT COUNT(distinct(packet_gnsarefno)) FROM chola_trn_tpacket "
                    Sqlstr &= " WHERE (replace(replace(upper(packet_gnsarefno),'P',''),'N','') BETWEEN " & Val(txtGNSAREfNoFrom.Text.Trim) & " AND " & Val(txtGNSAREfNoTo.Text.Trim) & ")"
                    If txtId.Text = "" Then
                        Sqlstr &= " AND packet_box_gid = 0 "
                    Else
                        Sqlstr &= " AND (packet_box_gid = 0 OR packet_box_gid =" & txtId.Text & ")"
                    End If
                    Sqlstr &= " and packet_mode='" & lsFileType & "'"

                    lnDocCnt = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

                End If

                txtDocCount.Text = lnDocCnt
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class