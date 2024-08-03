Public Class frmretrievalentry
    Dim lssql As String
    Dim dtentry As New DataTable
    Dim lnRetrievalGid As Long
    Dim lsRefno As String
    Dim lsChqno As String
    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim lnRefGid As Long

        If txtagreementno.Text.Trim = "" Then
            MsgBox("Please Enter Agreement No..!", MsgBoxStyle.Critical, gProjectName)
            txtagreementno.Focus()
            Exit Sub
        End If

        If txtworkitemno.Text.Trim = "" Then
            MsgBox("Please Enter Work item No..!", MsgBoxStyle.Critical, gProjectName)
            txtagreementno.Focus()
            Exit Sub
        End If

        If cboretrievalmode.Text.Trim = "" Then
            MsgBox("Please Select Retrieval Mode..!", MsgBoxStyle.Critical, gProjectName)
            cboretrievalmode.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To dtentry.Rows.Count - 1
            If dtentry.Rows(i).Item("Reference No") = txtreferenceno.Text.Trim Then
                MsgBox("Duplicate Entry..!", MsgBoxStyle.Critical, gProjectName)
                txtreferenceno.Focus()
                Exit Sub
            End If
        Next

        If cboretrievalmode.Text.ToUpper = "PACKET" Then

            If lsRefno.ToUpper <> txtreferenceno.Text.ToUpper And lsRefno.ToUpper <> "" Then
                MsgBox("Incorrect GNSA Reference No..!", MsgBoxStyle.Critical, gProjectName)
                txtreferenceno.Focus()
                Exit Sub
            End If

            lssql = ""
            lssql &= " select packet_gid "
            lssql &= " from chola_trn_tpacket "
            lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
            lssql &= " where 1=1 "
            lssql &= " and shortagreement_no='" & txtagreementno.Text.Trim & "'"
            lssql &= " and packet_gnsarefno='" & txtreferenceno.Text.Trim & "'"
            lssql &= " and packet_status & " & GCPACKETRETRIEVAL & " = 0 "

            lnRefGid = Val(gfExecuteScalar(lssql, gOdbcConn))
        ElseIf cboretrievalmode.Text.ToUpper = "PDC" Then

            If lsChqno.ToUpper <> txtreferenceno.Text.ToUpper And lsChqno.ToUpper <> "" Then
                MsgBox("Incorrect Cheque No..!", MsgBoxStyle.Critical, gProjectName)
                txtreferenceno.Focus()
                Exit Sub
            End If

            lssql = ""
            lssql &= " select entry_gid "
            lssql &= " from chola_trn_tpdcentry "
            lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
            lssql &= " where 1=1 "
            lssql &= " and shortagreement_no='" & txtagreementno.Text.Trim & "'"
            lssql &= " and chq_no='" & txtreferenceno.Text.Trim & "'"
            lssql &= " and chq_status & " & GCCHQRETRIEVAL & " = 0 "
            lssql &= " and chq_status & " & GCPULLOUT & " = 0 "
            lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
            lssql &= " and chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "

            lnRefGid = Val(gfExecuteScalar(lssql, gOdbcConn))
        ElseIf cboretrievalmode.Text.ToUpper = "SPDC" Then

            If lsChqno.ToUpper <> txtreferenceno.Text.ToUpper And lsChqno.ToUpper <> "" Then
                MsgBox("Incorrect Cheque No..!", MsgBoxStyle.Critical, gProjectName)
                txtreferenceno.Focus()
                Exit Sub
            End If

            lssql = ""
            lssql &= " select chqentry_gid "
            lssql &= " from chola_trn_tspdcchqentry "
            lssql &= " inner join chola_trn_tpacket on packet_gid=chqentry_packet_gid "
            lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
            lssql &= " where 1=1 "
            lssql &= " and shortagreement_no='" & txtagreementno.Text.Trim & "'"
            lssql &= " and chqentry_chqno='" & txtreferenceno.Text.Trim & "'"
            lssql &= " and chqentry_status & " & GCCHQRETRIEVAL & " = 0 "
            lssql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
            lssql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
            lssql &= " and chqentry_status & " & GCPRESENTATIONPULLOUT & " = 0 "

            lnRefGid = Val(gfExecuteScalar(lssql, gOdbcConn))
        End If

        If lnRefGid = 0 Then
            MsgBox("Invalid Reference No..!", MsgBoxStyle.Critical, gProjectName)
            txtreferenceno.Focus()
            Exit Sub
        End If

        dtentry.Rows.Add()
        dtentry.Rows(dtentry.Rows.Count - 1).Item("Sl No") = dtentry.Rows.Count
        dtentry.Rows(dtentry.Rows.Count - 1).Item("Reference No") = txtreferenceno.Text.Trim
        dtentry.Rows(dtentry.Rows.Count - 1).Item("Remarks") = txtremarks.Text.Trim
        dtentry.Rows(dtentry.Rows.Count - 1).Item("Refgid") = lnRefGid
        dgvsummary.DataSource = dtentry
        txtreferenceno.Text = ""
        txtreferenceno.Focus()
    End Sub

    Private Sub frmretrievalentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtentry.Columns.Add("Sl No")
        dtentry.Columns.Add("Reference No")
        dtentry.Columns.Add("Remarks")
        dtentry.Columns.Add("Refgid")

        dgvsummary.DataSource = dtentry

        Dim dgButtonColumn As New DataGridViewButtonColumn
        dgButtonColumn.HeaderText = ""
        dgButtonColumn.UseColumnTextForButtonValue = True
        dgButtonColumn.Text = "Delete"
        dgButtonColumn.Name = "Delete"
        dgButtonColumn.ToolTipText = "Delete Row"
        dgButtonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgButtonColumn.FlatStyle = FlatStyle.System
        dgButtonColumn.DefaultCellStyle.BackColor = Color.Gray
        dgButtonColumn.DefaultCellStyle.ForeColor = Color.White
        dgvsummary.Columns.Add(dgButtonColumn)
        dgvsummary.Columns(3).Visible = False
    End Sub

    Private Sub txtworkitemno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtworkitemno.LostFocus
        Dim drretrieval As Odbc.OdbcDataReader

        If txtworkitemno.Text.Trim <> "" Then
            If txtagreementno.Text.Trim = "" Then
                MsgBox("Please Enter Agreement No..!", MsgBoxStyle.Critical, gProjectName)
                txtagreementno.Focus()
                Exit Sub
            End If

            lssql = ""
            lssql &= " select retrieval_gid,retrieval_mode,retrieval_gnsarefno,retrieval_chqno "
            lssql &= " from chola_trn_tretrieval "
            lssql &= " where 1=1 "
            lssql &= " and retrieval_shortagreementno='" & txtagreementno.Text.Trim & "'"
            lssql &= " and retrieval_gid=" & txtworkitemno.Text.Trim
            lssql &= " and retrieval_status & " & GCRETRIEVED & " = 0 "
            lssql &= " and retrieval_status & " & GCMISSING & " = 0 "

            drretrieval = gfExecuteQry(lssql, gOdbcConn)

            If drretrieval.HasRows Then
                cboretrievalmode.Text = drretrieval.Item("retrieval_mode").ToString
                lnRetrievalGid = Val(drretrieval.Item("retrieval_gid").ToString)
                lsRefno = drretrieval.Item("retrieval_gnsarefno").ToString
                lsChqno = drretrieval.Item("retrieval_chqno").ToString
            Else
                MsgBox("Invalid Details..!", MsgBoxStyle.Critical, gProjectName)
                txtagreementno.Focus()
                Exit Sub
            End If
        End If
    End Sub
    Private Sub dgventry_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Try
            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Delete" Then
                        dtentry.Rows(e.RowIndex).Delete()
                        dtentry.AcceptChanges()
                        dgvsummary.DataSource = dtentry
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        If dtentry.Rows.Count = 0 Then
            MsgBox("Please Enter Atleast one Record..!", MsgBoxStyle.Critical, gProjectName)
            txtagreementno.Focus()
            Exit Sub
        End If

        If MsgBox("Are You Sure Want to Submit", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To dtentry.Rows.Count - 1

            lssql = ""
            lssql &= " insert into chola_trn_tretrievalentry ("
            lssql &= " retrievalentry_retrieval_gid,retrievalentry_refno,"
            lssql &= " retrievalentry_refflag,retrievalentry_refgid,"
            lssql &= " retrievalentry_insertdate,retrievalentry_insertby)"
            lssql &= " values ("
            lssql &= "" & lnRetrievalGid & ","
            lssql &= "'" & dtentry.Rows(i).Item("Reference No").ToString & "',"
            lssql &= "'" & cboretrievalmode.Text & "',"
            lssql &= "" & dtentry.Rows(i).Item("Refgid").ToString & ","
            lssql &= " sysdate(),"
            lssql &= "'" & gUserName & "')"
            gfInsertQry(lssql, gOdbcConn)

            lssql = ""
            lssql &= " update chola_trn_tretrieval "
            lssql &= " set retrieval_status= retrieval_status | " & GCRETRIEVED
            lssql &= " where retrieval_gid=" & lnRetrievalGid
            gfInsertQry(lssql, gOdbcConn)

            If cboretrievalmode.Text.ToUpper = "PACKET" Then
                'Packet Level
                lssql = ""
                lssql &= " update chola_trn_tpacket "
                lssql &= " set packet_status = packet_status | " & GCPACKETRETRIEVAL
                lssql &= " where packet_gid=" & dtentry.Rows(i).Item("Refgid").ToString
                lssql &= " and packet_status & " & GCPACKETRETRIEVAL & " = 0 "

                gfInsertQry(lssql, gOdbcConn)

                'Cheque Level
                'PDC
                lssql = ""
                lssql &= " update chola_trn_tpdcentry "
                lssql &= " set chq_status = chq_status | " & GCCHQRETRIEVAL
                lssql &= " where chq_packet_gid=" & dtentry.Rows(i).Item("Refgid").ToString
                lssql &= " and chq_status & " & GCCHQRETRIEVAL & " = 0 "
                lssql &= " and chq_status & " & GCPULLOUT & " = 0 "
                lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                lssql &= " and chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                gfInsertQry(lssql, gOdbcConn)

                'SPDC
                lssql = ""
                lssql &= " update chola_trn_tspdcchqentry "
                lssql &= " set chqentry_status=chqentry_status | " & GCCHQRETRIEVAL
                lssql &= " where chqentry_packet_gid=" & dtentry.Rows(i).Item("Refgid").ToString
                lssql &= " and chqentry_status & " & GCCHQRETRIEVAL & " = 0 "
                lssql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                lssql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                lssql &= " and chqentry_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                gfInsertQry(lssql, gOdbcConn)

                'ECS
                lssql = ""
                lssql &= " update chola_trn_tecsemientry "
                lssql &= " set ecsemientry_status=ecsemientry_status | " & GCCHQRETRIEVAL & ","
                lssql &= " ecsemientry_isactive='N' "
                lssql &= " where ecsemientry_packet_gid=" & dtentry.Rows(i).Item("Refgid").ToString
                lssql &= " and ecsemientry_status & " & GCCHQRETRIEVAL & " = 0 "
                lssql &= " and ecsemientry_status & " & GCPULLOUT & " = 0 "
                lssql &= " and ecsemientry_status & " & GCPACKETPULLOUT & " = 0 "
                lssql &= " and ecsemientry_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                gfInsertQry(lssql, gOdbcConn)

            ElseIf cboretrievalmode.Text.ToUpper = "PDC" Then
                lssql = ""
                lssql &= " update chola_trn_tpdcentry "
                lssql &= " set chq_status = chq_status | " & GCCHQRETRIEVAL
                lssql &= " where entry_gid=" & dtentry.Rows(i).Item("Refgid").ToString
                lssql &= " and chq_status & " & GCCHQRETRIEVAL & " = 0 "
                lssql &= " and chq_status & " & GCPULLOUT & " = 0 "
                lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                lssql &= " and chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                gfInsertQry(lssql, gOdbcConn)
            ElseIf cboretrievalmode.Text.ToUpper = "SPDC" Then
                lssql = ""
                lssql &= " update chola_trn_tspdcchqentry "
                lssql &= " set chqentry_status=chqentry_status | " & GCCHQRETRIEVAL
                lssql &= " where chqentry_gid=" & dtentry.Rows(i).Item("Refgid").ToString
                lssql &= " and chqentry_status & " & GCCHQRETRIEVAL & " = 0 "
                lssql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                lssql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                lssql &= " and chqentry_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                gfInsertQry(lssql, gOdbcConn)
            End If            
        Next
        ClearValues()
        MsgBox("Successfully Completed..!", MsgBoxStyle.Information, gProjectName)
    End Sub
    Private Sub ClearValues()
        txtagreementno.Text = ""
        txtreferenceno.Text = ""
        txtremarks.Text = ""
        txtworkitemno.Text = ""
        cboretrievalmode.Text = ""
        dtentry.Rows.Clear()
        dgvsummary.DataSource = dtentry
        txtagreementno.Focus()
    End Sub
End Class