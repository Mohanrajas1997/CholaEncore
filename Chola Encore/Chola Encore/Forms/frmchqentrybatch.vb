Public Class frmchqentrybatch

    Dim lssql As String

    Private Sub frmchqentrybatch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadData()
    End Sub
    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Dim objdt As DataTable
        Dim ds As New DataSet
        Dim lnBatchGid As Long
        Dim libatchno As Integer

        Dim lnStatus As Integer
        Dim lnTempStatus As Integer
        Dim lnValidStatus As Integer
        Dim lnPdcId As Long

        Dim liBatchSlno As Integer
        Dim lnChqAmt As Double
        Try
            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "CreateBatch" Then
                        If MsgBox("Are You Sure Want to Create Batch", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If


                        lssql = ""
                        lssql &= " select * "
                        lssql &= " from chola_trn_chqentry "
                        lssql &= " inner join chola_trn_tpdcentry on entry_gid=chqentry_entry_gid "
                        lssql &= " inner join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
                        lssql &= " where chqentry_isactive='Y' "
                        lssql &= " and chqentry_entryby='" & gUserName & "'"
                        lssql &= " and chq_date='" & Format(CDate(dgvsummary.Rows(e.RowIndex).Cells("Cycle Date").Value.ToString), "yyyy-MM-dd") & "'"
                        lssql &= " and type_name='" & dgvsummary.Rows(e.RowIndex).Cells("Product").Value.ToString & "'"
                        lssql &= " and chqentry_systemip='" & dgvsummary.Rows(e.RowIndex).Cells("System IP").Value.ToString & "'"
                        lssql &= " order by chqentry_gid "

                        objdt = GetDataTable(lssql)

                        If objdt.Rows.Count = 0 Then Exit Sub

                        libatchno = GetBatchNo()

                        lssql = ""
                        lssql &= " insert into chola_trn_tbatch ("
                        lssql &= " batch_no,batch_displayno,batch_prodtype,batch_cycledate,batch_inserdate,batch_insertby ) "
                        lssql &= " values ( "
                        lssql &= "" & libatchno & ","
                        lssql &= "'" & Format(Val(libatchno), "0000") & "',"
                        lssql &= "" & dgvsummary.Rows(e.RowIndex).Cells("chq_prodtype").Value.ToString & ","
                        lssql &= "'" & Format(CDate(dgvsummary.Rows(e.RowIndex).Cells("Cycle Date").Value.ToString), "yyyy-MM-dd") & "',"
                        lssql &= "sysdate(),'" & gUserName & "')"

                        gfInsertQry(lssql, gOdbcConn)

                        lssql = ""
                        lssql &= " select batch_gid "
                        lssql &= " from chola_trn_tbatch "
                        lssql &= " where batch_deleteflag='N' "
                        lssql &= " and batch_no=" & libatchno

                        lnBatchGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                        For i As Integer = 0 To objdt.Rows.Count - 1

                            lssql = " select * from chola_trn_tpdcentry "
                            lssql &= " where 1 = 1 "
                            lssql &= " and entry_gid=" & objdt.Rows(i).Item("entry_gid").ToString

                            Call gpDataSet(lssql, "pdc", gOdbcConn, ds)

                            lssql = ""
                            lssql &= " select count(*) "
                            lssql &= " from chola_trn_tpdcentry "
                            lssql &= " where chq_batch_gid = " & lnBatchGid
                            liBatchSlno = gfExecuteScalar(lssql, gOdbcConn)
                            liBatchSlno += 1

                            With ds.Tables("pdc")
                                Select Case .Rows.Count
                                    Case 1
                                        lnStatus = .Rows(0).Item("chq_status")
                                        lnPdcId = .Rows(0).Item("entry_gid")
                                        lnChqAmt = Math.Round(.Rows(0).Item("chq_amount"), 2)

                                        ' Check pullout or despatch or Packet Pullout or Presentation Pullout or Presentation Dataentry
                                        lnValidStatus = GCPULLOUT Or GCDESPATCH Or GCPACKETPULLOUT Or GCPRESENTATIONDE Or GCPRESENTATIONPULLOUT
                                        lnTempStatus = lnStatus And lnValidStatus


                                        If lnTempStatus Then

                                            lssql = " update chola_trn_chqentry "
                                            lssql &= " set chqentry_isactive='N' "
                                            lssql &= " where chqentry_gid=" & objdt.Rows(i).Item("chqentry_gid").ToString
                                            gfInsertQry(lssql, gOdbcConn)
                                        Else
                                            lssql = " update chola_trn_tpdcentry set "
                                            lssql &= " chq_status = chq_status | " & GCPRESENTATIONPULLOUT & " | " & GCPRESENTATIONDE & ","
                                            lssql &= " chq_batch_gid = " & lnBatchGid & ","
                                            lssql &= " chq_batchslno = " & liBatchSlno
                                            lssql &= " where entry_gid=" & lnPdcId & ""
                                            gfInsertQry(lssql, gOdbcConn)

                                            lssql = " update chola_trn_tbatch set batch_totalchq = if(batch_totalchq is null,0,batch_totalchq) + 1,"
                                            lssql &= " batch_totalchqamt= if (batch_totalchqamt is null,0,batch_totalchqamt) + " & lnChqAmt & ","
                                            lssql &= " batch_entrychq=if(batch_entrychq is null,0,batch_entrychq) + 1 ,"
                                            lssql &= " batch_entrychqamt=if(batch_entrychqamt is null,0,batch_entrychqamt) + " & lnChqAmt
                                            lssql &= " where batch_gid=" & lnBatchGid
                                            gfInsertQry(lssql, gOdbcConn)

                                            lssql = " update chola_trn_chqentry "
                                            lssql &= " set chqentry_batch_gid=" & lnBatchGid
                                            lssql &= " where chqentry_gid=" & objdt.Rows(i).Item("chqentry_gid").ToString
                                            gfInsertQry(lssql, gOdbcConn)
                                        End If

                                    Case 0
                                        lssql = " update chola_trn_chqentry "
                                        lssql &= " set chqentry_isactive='N' "
                                        lssql &= " where chqentry_gid=" & objdt.Rows(i).Item("chqentry_gid").ToString
                                        gfInsertQry(lssql, gOdbcConn)
                                    Case Else
                                        lssql = " update chola_trn_chqentry "
                                        lssql &= " set chqentry_isactive='N' "
                                        lssql &= " where chqentry_gid=" & objdt.Rows(i).Item("chqentry_gid").ToString
                                        gfInsertQry(lssql, gOdbcConn)
                                End Select

                                .Rows.Clear()
                            End With
                        Next
                        LoadData()
                        MsgBox("Batch No: " & libatchno, MsgBoxStyle.Information, gProjectName)
                    End If
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub LoadData()
        lssql = ""
        lssql &= " select chq_prodtype,date_format(chq_date,'%d-%m-%Y') as 'Cycle Date', "
        lssql &= " type_name as 'Product',chqentry_systemip as 'System IP',"
        lssql &= " count(*) as 'Cheque Count',sum(chq_amount) as 'Total Amount' "
        lssql &= " from chola_trn_chqentry "
        lssql &= " inner join chola_trn_tpdcentry on entry_gid=chqentry_entry_gid "
        lssql &= " inner join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N'"
        lssql &= " where chqentry_isactive='Y' "
        lssql &= " and chqentry_batch_gid=0 "
        lssql &= " and chqentry_entryby='" & gUserName & "'"

        lssql &= " group by chq_date,type_name,chqentry_systemip "

        dgvsummary.Columns.Clear()
        gpPopGridView(dgvsummary, lssql, gOdbcConn)
        dgvsummary.Columns(0).Visible = False
        Dim dgButtonColumn2 As New DataGridViewButtonColumn
        dgButtonColumn2.HeaderText = ""
        dgButtonColumn2.UseColumnTextForButtonValue = True
        dgButtonColumn2.Text = "Create Batch"
        dgButtonColumn2.Name = "CreateBatch"
        dgButtonColumn2.ToolTipText = "Batch Creation"
        dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgButtonColumn2.FlatStyle = FlatStyle.System
        dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
        dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
        dgvsummary.Columns.Add(dgButtonColumn2)
    End Sub
    Private Function GetBatchNo() As String
        lssql = ""
        lssql &= " select max(batch_no) "
        lssql &= " from chola_trn_tbatch "
        lssql &= " where batch_deleteflag='N' "

        Return Val(gfExecuteScalar(lssql, gOdbcConn)) + 1
    End Function
End Class