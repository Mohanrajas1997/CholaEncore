Public Class frmbatchdespatch
#Region "Local Declaration"
    Dim dsBox As DataSet
    Dim ObjDataColumn As DataColumn
    Dim ObjDataTable As DataTable

    Dim llDespatchGid As Long
    Dim llRecordCount As Long
    Dim llResult As Long
#End Region

    Private Sub frmbatchdespatch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmbatchdespatch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmbatchdespatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call lp_ClearControl()
            Call EnableSave(False)

            Sqlstr = ""
            Sqlstr &= " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N' "
            gpBindCombo(Sqlstr, "type_name", "type_flag", cboproducttype, gOdbcConn)
            cboproducttype.SelectedIndex = -1

            MyBase.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmbatchdespatch_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        dGBatch.Width = Me.Width - 25
        dGBatch.Height = Me.Height - 165
        pnlButtons.Top = Me.Height - 60
        pnlSave.Top = Me.Height - 60
        pnlButtons.Left = Me.Width / 2 - pnlButtons.Width / 2
        pnlSave.Left = Me.Width / 2 - pnlSave.Width / 2
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
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
            txtdespatchno.Text = ""
            txtRemarks.Text = ""
            txtairwaybillno.Text = ""
            txtsentto.Text = ""
            cboproducttype.SelectedIndex = -1
            cboproducttype.Text = ""
            dtpcycledate.Value = Now
            dtpcycledate.Checked = False
            dGBatch.DataSource = Nothing
            btnNew.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
        dGBatch.Enabled = Status
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim llDespatchNo As Long
        Try
            Call lp_ClearControl()

            Sqlstr = ""
            Sqlstr &= " SELECT MAX(despatch_no) FROM chola_trn_tdespatch "
            Sqlstr &= " WHERE despatch_deleteflag ='N' "
            llDespatchNo = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
            txtdespatchno.Text = llDespatchNo + 1
            cboproducttype.SelectedIndex = -1
            Call EnableSave(True)
            txtdespatchno.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Val(txtId.Text) <> 0 Then
                Call EnableSave(True)
            Else
                MsgBox("Select Despatch Detail To Edit", MsgBoxStyle.Information, gProjectName)
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
            sqlstr = " SELECT despatch_gid as 'Despatch Gid', despatch_no as 'Despatch No',despatch_awbno as 'AWB NO',"
            sqlstr &= "despatch_sentto as 'Sent To',date_format(despatch_senton,'%d-%m-%Y') as 'Sent On',despatch_sentby as 'Sent By',"
            sqlstr &= " despatch_remarks as 'Remark' "
            sqlstr &= " FROM chola_trn_tdespatch  "

            lsFields = " despatch_gid, despatch_no,despatch_awbno,despatch_sentto,despatch_senton,despatch_sentby,despatch_remarks"

            lsConds = " 1 = 1 and despatch_deleteflag ='N' ORDER BY despatch_gid DESC "

            SearchDialog = New Search(gOdbcConn, sqlstr, lsFields, lsConds)
            SearchDialog.ShowDialog()

            If Val(txt) <> 0 Then
                Call ListAll(" SELECT * FROM chola_trn_tdespatch " _
                            & " WHERE despatch_gid = '" & txt & "' " _
                            & " AND despatch_deleteflag ='N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal sqlstr As String, ByVal odbcConn As Odbc.OdbcConnection)
        Dim lni As Integer

        Try
            dsBox = gfDataSet(sqlstr, "Batch", gOdbcConn)

            If dsBox.Tables("Batch").Rows.Count > 0 Then
                txtId.Text = dsBox.Tables("Batch").Rows(0).Item("despatch_gid").ToString
                txtdespatchno.Text = dsBox.Tables("Batch").Rows(0).Item("despatch_no").ToString
                txtairwaybillno.Text = dsBox.Tables("Batch").Rows(0).Item("despatch_awbno").ToString
                txtsentto.Text = dsBox.Tables("Batch").Rows(0).Item("despatch_sentto").ToString
                txtRemarks.Text = dsBox.Tables("Batch").Rows(0).Item("despatch_remarks").ToString

                sqlstr = ""
                sqlstr &= " SELECT batch_gid, batch_displayno 'Batch No',  "
                sqlstr &= " batch_prodtype 'Product', batch_cycledate 'Cycle Date', batch_totalchq 'Cheque Count',"
                sqlstr &= " batch_totalchqamt 'Cheque Amount' "
                sqlstr &= " FROM chola_trn_tbatch "
                sqlstr &= " WHERE batch_despatch_gid=" & Val(txtId.Text)
                sqlstr &= " AND batch_deleteflag ='N' and batch_istally='Y'  "
                dsBox = gfDataSet(sqlstr, "Batch", gOdbcConn)

                'cboproducttype.SelectedValue = dsBox.Tables("Batch").Rows(0).Item("Product").ToString
                dtpcycledate.Value = Format(CDate(dsBox.Tables("Batch").Rows(0).Item("Cycle Date").ToString), "dd-MM-yyyy")

                sqlstr = ""
                sqlstr &= " SELECT batch_gid, batch_displayno 'Batch No',  "
                sqlstr &= " type_name 'Product', batch_cycledate 'Cycle Date', batch_totalchq 'Cheque Count',"
                sqlstr &= " batch_totalchqamt 'Cheque Amount' "
                sqlstr &= " FROM chola_trn_tbatch "
                sqlstr &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
                sqlstr &= " WHERE (batch_despatch_gid=" & Val(txtId.Text)
                sqlstr &= " OR batch_despatch_gid=0)"
                sqlstr &= " AND batch_deleteflag ='N' and batch_istally='Y' "
                'sqlstr &= " and batch_prodtype=" & cboproducttype.SelectedValue
                sqlstr &= " and batch_cycledate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"

                dsBox = gfDataSet(sqlstr, "Batch", gOdbcConn)


                ObjDataColumn = New DataColumn
                ObjDataColumn.DataType = System.Type.GetType("System.Boolean")
                ObjDataColumn.ColumnName = "Select"
                ObjDataColumn.DefaultValue = False
                ObjDataColumn.ReadOnly = False

                dsBox.Tables("Batch").Columns.Add(ObjDataColumn)

                dGBatch.DataSource = dsBox.Tables("Batch")
                dGBatch.ReadOnly = False
                dGBatch.Columns(0).ReadOnly = True
                dGBatch.Columns(1).ReadOnly = True
                dGBatch.Columns(2).ReadOnly = True
                dGBatch.Columns(3).ReadOnly = True
                dGBatch.Columns(4).ReadOnly = True
                dGBatch.Columns(5).ReadOnly = True


                dGBatch.Columns(0).Visible = False



                dGBatch.AutoResizeColumns()

                For lni = 0 To dGBatch.Rows.Count - 1
                    sqlstr = ""
                    sqlstr &= " SELECT COUNT(*) FROM chola_trn_tbatch"
                    sqlstr &= " WHERE batch_gid =" & dGBatch.Item(0, lni).Value
                    sqlstr &= " AND batch_despatch_gid=0"
                    sqlstr &= " AND batch_deleteflag ='N' "

                    llRecordCount = Val(gfExecuteScalar(sqlstr, gOdbcConn))
                    If llRecordCount = 0 Then
                        dGBatch.Item(6, lni).Value = True
                    Else
                        dGBatch.Item(6, lni).Value = False
                    End If
                Next
                dGBatch.Columns(6).ReadOnly = False
                dGBatch.Columns(0).Width = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lni As Integer

        Try
            If Val(txtId.Text) <> 0 Then
                If MsgBox("Are you sure want to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                    Exit Sub
                End If
                llDespatchGid = Val(txtId.Text)

                Sqlstr = ""
                Sqlstr &= " UPDATE chola_trn_tdespatch SET despatch_deleteflag='Y',"
                Sqlstr &= " despatch_updatedate=SYSDATE(),"
                Sqlstr &= " despatch_updateby = '" & gUserName & "' "
                Sqlstr &= " WHERE despatch_gid=" & llDespatchGid
                Sqlstr &= " AND despatch_deleteflag ='N'"

                llResult = gfInsertQry(Sqlstr, gOdbcConn)

                If llResult <> 0 Then
                    For lni = 0 To dGBatch.Rows.Count - 1
                        If dGBatch.Item(6, lni).Value = True Then

                            Sqlstr = ""
                            Sqlstr &= " update chola_trn_tpdcentry "
                            Sqlstr &= " set chq_status = (chq_status | " & GCDESPATCH & " ) ^ " & GCDESPATCH
                            Sqlstr &= " where chq_batch_gid=" & Val(dGBatch.Item(0, lni).Value)
                            llResult = gfInsertQry(Sqlstr, gOdbcConn)

                            Sqlstr = ""
                            Sqlstr &= " UPDATE chola_trn_tbatch SET batch_despatch_gid=0"
                            Sqlstr &= " WHERE batch_gid=" & Val(dGBatch.Item(0, lni).Value)
                            Sqlstr &= " AND batch_despatch_gid <>0 AND batch_deleteflag ='N' "
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
                MsgBox("Select Despatch Detail To Delete", MsgBoxStyle.Information, gProjectName)
                Call btnFind_Click(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lni As Integer

        Try
            llRecordCount = 0

            If txtdespatchno.Text.Trim = "" Then
                MsgBox("Please Enter Despatch No", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            If txtairwaybillno.Text.Trim = "" Then
                MsgBox("Please Enter Airwaybill No", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            If txtsentto.Text.Trim = "" Then
                MsgBox("Please Enter Sent to", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            If dtpcycledate.Checked = False Then
                MsgBox("Please Select Cycle Date", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            If cboproducttype.Text.Trim = "" Then
                MsgBox("Please Select Product Type", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If


            Sqlstr = ""
            Sqlstr &= " SELECT despatch_gid FROM chola_trn_tdespatch"
            Sqlstr &= " WHERE (despatch_no=" & txtdespatchno.Text
            Sqlstr &= " )"
            If Val(txtId.Text) <> 0 Then
                Sqlstr &= " AND despatch_gid <> " & txtId.Text
            End If
            Sqlstr &= " AND despatch_deleteflag ='N'"

            llDespatchGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))
            If llDespatchGid <> 0 Then
                MsgBox("Despatch No Duplicate", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            For lni = 0 To dGBatch.Rows.Count - 1
                If dGBatch.Item(6, lni).Value = True Then
                    llRecordCount += 1
                End If
            Next

            If llRecordCount = 0 Then
                MsgBox("Select atleast one Record", MsgBoxStyle.Information, gProjectName)
                dGBatch.Focus()
                Exit Sub
            End If

            If Val(txtId.Text.Trim) = 0 Then
                Sqlstr = ""
                Sqlstr &= " INSERT INTO chola_trn_tdespatch(despatch_no, despatch_awbno, despatch_sentto,"
                Sqlstr &= " despatch_senton,despatch_sentby,despatch_remarks)"
                Sqlstr &= " VALUES(" & Val(txtdespatchno.Text) & ","
                Sqlstr &= "'" & txtairwaybillno.Text.Trim & "',"
                Sqlstr &= "'" & txtsentto.Text.Trim & "',"
                Sqlstr &= " SYSDATE(),'" & gUserName & "','" & QuoteFilter(txtRemarks.Text) & "') "

                llResult = gfInsertQry(Sqlstr, gOdbcConn)

                If llResult <> 0 Then
                    Sqlstr = " SELECT MAX(despatch_gid) FROM chola_trn_tdespatch "
                    Sqlstr &= " WHERE despatch_deleteflag ='N'"
                    llDespatchGid = gfExecuteScalar(Sqlstr, gOdbcConn)

                    For lni = 0 To dGBatch.Rows.Count - 1
                        If dGBatch.Item(6, lni).Value = True Then
                            Sqlstr = ""
                            Sqlstr &= " UPDATE chola_trn_tbatch SET batch_despatch_gid=" & llDespatchGid
                            Sqlstr &= " WHERE batch_gid=" & Val(dGBatch.Item(0, lni).Value)
                            Sqlstr &= " AND batch_despatch_gid =0 "
                            Sqlstr &= " AND batch_deleteflag ='N' "
                            llResult = gfInsertQry(Sqlstr, gOdbcConn)

                            Sqlstr = ""
                            Sqlstr &= " update chola_trn_tpdcentry "
                            Sqlstr &= " set chq_status = chq_status | " & GCDESPATCH
                            Sqlstr &= " where chq_batch_gid=" & Val(dGBatch.Item(0, lni).Value)
                            llResult = gfInsertQry(Sqlstr, gOdbcConn)

                        End If
                    Next
                End If
            Else
                llDespatchGid = Val(txtId.Text)

                Sqlstr = ""
                Sqlstr &= " UPDATE chola_trn_tdespatch SET despatch_no=" & Val(txtdespatchno.Text) & ","
                Sqlstr &= " despatch_awbno='" & txtairwaybillno.Text & "',"
                Sqlstr &= " despatch_sentto='" & txtsentto.Text.Trim & "',"
                Sqlstr &= " despatch_senton=sysdate(),"
                Sqlstr &= " despatch_sentby='" & gUserName & "',"
                Sqlstr &= " despatch_remarks='" & QuoteFilter(txtRemarks.Text) & "'"
                Sqlstr &= " WHERE despatch_gid=" & llDespatchGid

                gfInsertQry(Sqlstr, gOdbcConn)

                For lni = 0 To dGBatch.Rows.Count - 1
                    If dGBatch.Item(6, lni).Value = True Then
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tbatch SET batch_despatch_gid=" & llDespatchGid
                        Sqlstr &= " WHERE batch_gid=" & Val(dGBatch.Item(0, lni).Value)
                        Sqlstr &= " AND batch_despatch_gid =0 "
                        Sqlstr &= " AND batch_deleteflag ='N'"
                        llResult = gfInsertQry(Sqlstr, gOdbcConn)
                    Else
                        Sqlstr = ""
                        Sqlstr &= " UPDATE chola_trn_tbatch SET batch_despatch_gid=0"
                        Sqlstr &= " WHERE batch_gid = " & Val(dGBatch.Item(0, lni).Value)
                        Sqlstr &= " AND batch_despatch_gid = " & llDespatchGid
                        Sqlstr &= " AND batch_deleteflag ='N'"
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

    Private Sub txtdespatchno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdespatchno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub chkSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelect.CheckedChanged
        Dim lni As Integer

        If chkSelect.Checked = True Then
            For lni = 0 To dGBatch.Rows.Count - 1
                dGBatch.Item(6, lni).Value = True
            Next
        Else
            For lni = 0 To dGBatch.Rows.Count - 1
                dGBatch.Item(6, lni).Value = False
            Next
        End If
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        Dim lni As Integer

        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date", MsgBoxStyle.Information)
            Exit Sub
        End If

        'Details Display in Data Grid
        Sqlstr = ""
        Sqlstr &= " SELECT batch_gid, batch_displayno 'Batch No',  "
        Sqlstr &= " type_name 'Product', batch_cycledate 'Cycle Date', batch_totalchq 'Cheque Count',"
        Sqlstr &= " batch_totalchqamt 'Cheque Amount' "
        Sqlstr &= " FROM chola_trn_tbatch "
        Sqlstr &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        Sqlstr &= " WHERE batch_despatch_gid =0 AND batch_deleteflag ='N' and batch_istally='Y' "

        If cboproducttype.SelectedIndex <> -1 Then Sqlstr &= " and batch_prodtype=" & cboproducttype.SelectedValue

        Sqlstr &= " and batch_cycledate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"

        dsBox = gfDataSet(Sqlstr, "Batch", gOdbcConn)

        ObjDataTable = dsBox.Tables("Batch")
        dGBatch.DataSource = ObjDataTable
        dGBatch.ReadOnly = False

        dGBatch.Columns(0).Visible = False

        For lni = 0 To dGBatch.ColumnCount - 1
            dGBatch.Columns(lni).ReadOnly = True
        Next

        ObjDataColumn = New DataColumn
        ObjDataColumn.DataType = System.Type.GetType("System.Boolean")
        ObjDataColumn.ColumnName = "Select"
        ObjDataColumn.DefaultValue = False
        ObjDataColumn.ReadOnly = False

        dsBox.Tables("Batch").Columns.Add(ObjDataColumn)

        dGBatch.Refresh()
        dGBatch.AutoResizeColumns()

        If dsBox.Tables("Batch").Rows.Count > 0 Then
            Call EnableSave(True)
        End If
    End Sub
End Class