Public Class frmShelfEntry
#Region "Local Declaration"
    Dim dsBox As DataSet
    Dim ObjDataColumn As DataColumn
    Dim ObjDataTable As DataTable

    Dim llCartonGid As Long
    Dim llRecordCount As Long
    Dim llResult As Long
    Dim lni As Integer
#End Region

    Private Sub frmcartonEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmCartonEntry_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmcartonEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call lp_ClearControl()
            Call EnableSave(False)
            MyBase.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmCartonEntry_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        dGBox.Width = Me.Width - 25
        dGBox.Height = Me.Height - 165
        pnlButtons.Top = Me.Height - 60
        pnlSave.Top = Me.Height - 60
        pnlButtons.Left = Me.Width / 2 - pnlButtons.Width / 2
        pnlSave.Left = Me.Width / 2 - pnlSave.Width / 2
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Quit?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            MyBase.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call lp_ClearControl()
        Call EnableSave(False)
    End Sub

    Private Sub lp_ClearControl()
        Try
            txtId.Text = ""
            txtShelfNo.Text = ""
            txtRemarks.Text = ""
            dGBox.DataSource = Nothing
            btnNew.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
        dGBox.Enabled = Status
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim llCartonNo As Long
        Try
            Call lp_ClearControl()

            ' Fetching Shelf No 
            Sqlstr = ""
            Sqlstr &= " SELECT MAX(shelf_no) FROM chola_trn_tshelf "
            Sqlstr &= " WHERE delete_flag ='N' "
            llCartonNo = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
            txtShelfNo.Text = llCartonNo + 1

            'Box Details Display in Data Grid
            Sqlstr = ""
            Sqlstr &= " SELECT box_gid, box_no 'Box No', "
            Sqlstr &= " gnsaref_from 'GNSA Ref From', gnsaref_to 'GNSA Ref To', shelf_gid, "
            Sqlstr &= " entry_date 'Entry Date', remarks 'Remarks',doc_type as 'Type', entry_by 'Entry By' "
            Sqlstr &= " FROM chola_trn_tbox "
            Sqlstr &= " WHERE shelf_gid =0 AND delete_flag ='N' "

            dsBox = gfDataSet(Sqlstr, "Box", gOdbcConn)

            ObjDataTable = dsBox.Tables("Box")
            dGBox.DataSource = ObjDataTable
            dGBox.ReadOnly = False

            dGBox.Columns(0).Visible = False
            dGBox.Columns(4).Visible = False

            For lni = 0 To dGBox.ColumnCount - 1
                dGBox.Columns(lni).ReadOnly = True
            Next

            ObjDataColumn = New DataColumn
            ObjDataColumn.DataType = System.Type.GetType("System.Boolean")
            ObjDataColumn.ColumnName = "Select"
            ObjDataColumn.DefaultValue = False
            ObjDataColumn.ReadOnly = False

            dsBox.Tables("Box").Columns.Add(ObjDataColumn)

            dGBox.Refresh()
            dGBox.AutoResizeColumns()

            If dsBox.Tables("Box").Rows.Count > 0 Then
                Call EnableSave(True)
            End If

            txtShelfNo.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Val(txtId.Text) <> 0 Then
                Call EnableSave(True)
            Else
                MsgBox("Select Shelf Detail To Edit", MsgBoxStyle.Information, gProjectName)
                Call btnFind_Click(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As Search
        Dim sqlstr As String
        Dim lsFields As String
        Dim lsConds As String
        Try
            sqlstr = " SELECT shelf_gid as 'Shelf Gid', shelf_no as 'Shelf No',"
            sqlstr &= " remarks as 'Remark' "
            sqlstr &= " FROM chola_trn_tshelf  "

            lsFields = " shelf_gid, shelf_no,   remarks"

            lsConds = " 1 = 1 and delete_flag ='N' ORDER BY shelf_gid DESC "

            SearchDialog = New Search(gOdbcConn, sqlstr, lsFields, lsConds)
            SearchDialog.ShowDialog()

            If Val(txt) <> 0 Then
                Call ListAll(" SELECT * FROM chola_trn_tshelf " _
                            & " WHERE shelf_gid = '" & txt & "' " _
                            & " AND delete_flag ='N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal sqlstr As String, ByVal odbcConn As Odbc.OdbcConnection)
        Try
            dsBox = gfDataSet(sqlstr, "Box", gOdbcConn)

            If dsBox.Tables("Box").Rows.Count > 0 Then
                txtId.Text = dsBox.Tables("Box").Rows(0).Item("shelf_gid").ToString
                txtShelfNo.Text = dsBox.Tables("Box").Rows(0).Item("shelf_no").ToString
                txtRemarks.Text = dsBox.Tables("Box").Rows(0).Item("remarks").ToString

                sqlstr = ""
                sqlstr &= " SELECT box_gid, box_no 'Box No',  "
                sqlstr &= " gnsaref_from 'GNSA Ref From', gnsaref_to 'GNSA Ref To', shelf_gid 'Shelf Gid',"
                sqlstr &= " entry_date 'Entry Date', entry_by 'Entry By',  remarks 'Remark',doc_type as 'Type' "
                sqlstr &= " FROM chola_trn_tbox "
                sqlstr &= " WHERE (shelf_gid=" & Val(dsBox.Tables("Box").Rows(0).Item("shelf_gid").ToString)
                sqlstr &= " OR shelf_gid=0)"
                sqlstr &= " AND delete_flag ='N' "

                'gpPopGridView(dgvFileDetails, sqlstr, gOdbcConn)
                dsBox = gfDataSet(sqlstr, "Box", gOdbcConn)

                ObjDataTable = dsBox.Tables("Box")
                dGBox.DataSource = ObjDataTable

                ObjDataColumn = New DataColumn
                ObjDataColumn.DataType = System.Type.GetType("System.Boolean")
                ObjDataColumn.ColumnName = "Select"
                ObjDataColumn.DefaultValue = False
                ObjDataColumn.ReadOnly = False

                dsBox.Tables("Box").Columns.Add(ObjDataColumn)

                dGBox.DataSource = dsBox.Tables("Box")
                dGBox.ReadOnly = False
                dGBox.Columns(0).ReadOnly = True
                dGBox.Columns(1).ReadOnly = True
                dGBox.Columns(2).ReadOnly = True
                dGBox.Columns(3).ReadOnly = True
                dGBox.Columns(4).ReadOnly = True
                dGBox.Columns(5).ReadOnly = True
                dGBox.Columns(6).ReadOnly = True
                dGBox.Columns(7).ReadOnly = True
                dGBox.Columns(8).ReadOnly = True

                dGBox.Columns(0).Visible = False
                dGBox.Columns(5).Visible = False

                dGBox.Columns(1).Width = 60
                dGBox.Columns(2).Width = 125
                dGBox.Columns(3).Width = 75
                dGBox.Columns(4).Width = 75
                dGBox.Columns(5).Width = 250
                dGBox.Columns(6).Width = 75
                dGBox.Columns(7).Width = 50
                dGBox.Columns(8).Width = 50

                dGBox.AutoResizeColumns()

                For lni = 0 To dGBox.Rows.Count - 1
                    sqlstr = ""
                    sqlstr &= " SELECT COUNT(*) FROM chola_trn_tbox"
                    sqlstr &= " WHERE box_gid =" & dGBox.Item(0, lni).Value
                    sqlstr &= " AND shelf_gid=0"
                    sqlstr &= " AND delete_flag ='N' "

                    llRecordCount = Val(gfExecuteScalar(sqlstr, gOdbcConn))
                    If llRecordCount = 0 Then
                        dGBox.Item(9, lni).Value = True
                    Else
                        dGBox.Item(9, lni).Value = False
                    End If
                Next
                dGBox.Columns(9).ReadOnly = False
                dGBox.Columns(0).Width = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Val(txtId.Text) <> 0 Then
                If MsgBox("Are you sure want to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                    Exit Sub
                End If
                llCartonGid = Val(txtId.Text)

                Sqlstr = ""
                Sqlstr &= " UPDATE chola_trn_tshelf SET delete_flag='Y',"
                Sqlstr &= " delete_date=SYSDATE(),"
                Sqlstr &= " delete_by = '" & gUserName & "' "
                Sqlstr &= " WHERE shelf_gid=" & llCartonGid
                Sqlstr &= " AND cupboard_gid = 0 AND delete_flag ='N'"

                llResult = gfInsertQry(Sqlstr, gOdbcConn)

                If llResult <> 0 Then
                    For lni = 0 To dGBox.Rows.Count - 1
                        If dGBox.Item(9, lni).Value = True Then
                            Sqlstr = ""
                            Sqlstr &= " UPDATE chola_trn_tbox SET shelf_gid=0"
                            Sqlstr &= " WHERE box_gid=" & Val(dGBox.Item(0, lni).Value)
                            Sqlstr &= " AND shelf_gid <>0 AND delete_flag ='N' "
                            llResult = gfInsertQry(Sqlstr, gOdbcConn)
                        End If
                    Next
                    MsgBox("Record Deleted", MsgBoxStyle.Information, gProjectName)
                    Call lp_ClearControl()
                    EnableSave(False)
                Else
                    MsgBox("Deletion Failed", MsgBoxStyle.Information, gProjectName)
                    Call lp_ClearControl()
                    EnableSave(False)
                End If

            Else
                MsgBox("Select Shelf Detail To Delete", MsgBoxStyle.Information, gProjectName)
                Call btnFind_Click(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            llRecordCount = 0


            Sqlstr = ""
            Sqlstr &= " SELECT shelf_gid FROM chola_trn_tshelf"
            Sqlstr &= " WHERE (shelf_no=" & txtShelfNo.Text
            Sqlstr &= " )"
            If Val(txtId.Text) <> 0 Then
                Sqlstr &= " AND shelf_gid <> " & txtId.Text
            End If
            Sqlstr &= " AND delete_flag ='N'"

            llCartonGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
            If llCartonGid <> 0 Then
                MsgBox("Shelf No Duplicate", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            For lni = 0 To dGBox.Rows.Count - 1
                If dGBox.Item(9, lni).Value = True Then
                    llRecordCount += 1
                End If
            Next

            If llRecordCount = 0 Then
                MsgBox("Select atleast one Record", MsgBoxStyle.Information, gProjectName)
                dGBox.Focus()
                Exit Sub
            End If

            If Val(txtId.Text.Trim) = 0 Then
                Sqlstr = ""
                Sqlstr &= " INSERT INTO chola_trn_tshelf(shelf_no, entry_date, entry_by, remarks)"
                Sqlstr &= " VALUES(" & Val(txtShelfNo.Text) & ","
                Sqlstr &= " SYSDATE(),'" & gProjectName & "','" & QuoteFilter(txtRemarks.Text) & "') "

                llResult = gfInsertQry(Sqlstr, gOdbcConn)

                If llResult <> 0 Then
                    Sqlstr = " SELECT MAX(shelf_gid) FROM chola_trn_tshelf "
                    Sqlstr &= " WHERE delete_flag ='N'"
                    llCartonGid = gfExecuteScalar(Sqlstr, gOdbcConn)

                    For lni = 0 To dGBox.Rows.Count - 1
                        If dGBox.Item(9, lni).Value = True Then
                            Sqlstr = ""
                            Sqlstr &= " UPDATE chola_trn_tbox SET shelf_gid=" & llCartonGid
                            Sqlstr &= " WHERE box_gid=" & Val(dGBox.Item(0, lni).Value)
                            Sqlstr &= " AND shelf_gid =0 "
                            Sqlstr &= " AND delete_flag ='N' "
                            llResult = gfInsertQry(Sqlstr, gOdbcConn)
                        End If
                    Next
                End If
            Else
                llCartonGid = Val(txtId.Text)

                Sqlstr = ""
                Sqlstr &= " SELECT shelf_gid FROM chola_trn_tshelf "
                Sqlstr &= " WHERE shelf_gid = " & llCartonGid
                Sqlstr &= " AND cupboard_gid =0"
                Sqlstr &= " AND delete_flag ='N' "

                llCartonGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

                If llCartonGid = 0 Then
                    MsgBox("Access Denied !", MsgBoxStyle.Critical, gProjectName)
                    Exit Sub
                End If

                Sqlstr = ""
                Sqlstr &= " UPDATE chola_trn_tshelf SET shelf_no=" & Val(txtShelfNo.Text) & ","
                Sqlstr &= " remarks='" & QuoteFilter(txtRemarks.Text) & "'"
                Sqlstr &= " WHERE shelf_gid=" & llCartonGid

                gfInsertQry(Sqlstr, gOdbcConn)

                For lni = 0 To dGBox.Rows.Count - 1
                    If dGBox.Item(9, lni).Value = True Then
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tbox SET shelf_gid=" & llCartonGid
                        Sqlstr &= " WHERE box_gid=" & Val(dGBox.Item(0, lni).Value)
                        Sqlstr &= " AND shelf_gid =0 "
                        Sqlstr &= " AND delete_flag ='N'"
                        llResult = gfInsertQry(Sqlstr, gOdbcConn)
                    Else
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tbox SET shelf_gid=0"
                        Sqlstr &= " WHERE box_gid = " & Val(dGBox.Item(0, lni).Value)
                        Sqlstr &= " AND shelf_gid = " & llCartonGid
                        Sqlstr &= " AND delete_flag ='N'"
                        llResult = gfInsertQry(Sqlstr, gOdbcConn)
                    End If
                Next
            End If

            If Val(txtId.Text) = 0 Then
                MsgBox("Record Saved Successfully!", MsgBoxStyle.Information, gProjectName)
                If MsgBox("Do you want to add another entry", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                    Call lp_ClearControl()
                    Call btnNew_Click(sender, e)
                Else
                    Call lp_ClearControl()
                    EnableSave(False)
                End If
            Else
                MsgBox("Record Updation finished!", MsgBoxStyle.Information, gProjectName)
                Call lp_ClearControl()
                EnableSave(False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtCartonNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShelfNo.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtCartonBarcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 8 Then
            e.Handled = False
            Exit Sub
        End If
        'If AlphaNumeric(e.KeyChar) = "" Then e.Handled = True
    End Sub

    Private Sub chkSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelect.CheckedChanged
        If chkSelect.Checked = True Then
            For lni = 0 To dGBox.Rows.Count - 1
                dGBox.Item(9, lni).Value = True
            Next
        Else
            For lni = 0 To dGBox.Rows.Count - 1
                dGBox.Item(9, lni).Value = False
            Next
        End If
    End Sub
End Class