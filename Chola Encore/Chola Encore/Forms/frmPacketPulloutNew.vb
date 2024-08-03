Public Class frmPacketPulloutNew
    Dim msSql As String

    Private Sub txtPktNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPktNo.Validating
        Dim ds As DataSet
        Dim i As Integer
        Dim n As Integer
        Dim lnResult As Long

        Dim lobjChkBox As New DataGridViewCheckBoxColumn

        If txtId.Text = "" Then
            msSql = ""
            msSql &= " select p.packet_gid,a.shortagreement_no,a.agreement_no from chola_trn_tpacket as p "
            msSql &= " inner join chola_mst_tagreement as a on a.agreement_gid = p.packet_agreement_gid  "
            msSql &= " where p.packet_gnsarefno = '" & QuoteFilter(txtPktNo.Text) & "' "
            msSql &= " and p.packet_status & " & GCIPACKETPULLOUT & " = 0"

            ds = New DataSet

            Call gpDataSet(msSql, "pkt", gOdbcConn, ds)

            With ds.Tables("pkt")
                If .Rows.Count = 1 Then
                    txtAgrNo.Text = .Rows(0).Item("shortagreement_no").ToString & "-" & .Rows(0).Item("agreement_no").ToString
                    txtPktId.Text = .Rows(0).Item("packet_gid").ToString

                    ' load cheque info
                    lnResult = gfInsertQry("set @a := 0", gOdbcConn)

                    msSql = ""
                    msSql &= " select "
                    msSql &= " @a:=@a+1 as 'SNo',"
                    msSql &= " chq_date as 'Chq Date',"
                    msSql &= " chq_no as 'Chq No',"
                    msSql &= " chq_amount as 'Amount',"
                    msSql &= " make_set(chq_type,'PDC','SPDC') as 'Chq',"
                    msSql &= " 'N' as 'loose_chq',"
                    msSql &= " 'PDC' as 'table_type',"
                    msSql &= " chq_packet_gid as packet_gid,entry_gid as ref_gid "
                    msSql &= " from chola_trn_tpdcentry "
                    msSql &= " where chq_packet_gid=" & txtPktId.Text & " "
                    msSql &= " and chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    msSql &= " union "

                    msSql &= " select "
                    msSql &= " @a:=@a+1 as 'SNo',"
                    msSql &= " chq_date as 'Chq Date',"
                    msSql &= " chq_no as 'Chq No',"
                    msSql &= " chq_amount as 'Amount',"
                    msSql &= " make_set(chq_type,'PDC','SPDC') as 'Chq',"
                    msSql &= " 'Y' as 'loose_chq',"
                    msSql &= " 'PDC' as 'table_type',"
                    msSql &= " chq_packet_gid as packet_gid,entry_gid as ref_gid "
                    msSql &= " from chola_trn_tpdcentry "
                    msSql &= " where chq_packet_gid=" & txtPktId.Text & " "
                    msSql &= " and chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0 "
                    msSql &= " and chq_status & " & GCLOOSECHQ & " > 0 "

                    msSql &= " union "

                    msSql &= " select "
                    msSql &= " @a:=@a+1 as 'SNo',"
                    msSql &= " null as 'Chq Date',"
                    msSql &= " chqentry_chqno as 'Chq No',"
                    msSql &= " null as 'Amount',"
                    msSql &= " 'SPDC' as 'chq_type',"
                    msSql &= " 'Y' as 'loose_chq',"
                    msSql &= " 'SPDC' as 'table_type',"
                    msSql &= " chqentry_packet_gid as packet_gid,chqentry_gid as ref_gid "
                    msSql &= " from chola_trn_tspdcchqentry "
                    msSql &= " where chqentry_packet_gid=" & txtPktId.Text & " "
                    msSql &= " and chqentry_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0 "

                    msSql &= " order by 'Chq No' "

                    dgvChq.DataSource = Nothing
                    dgvChq.Columns.Clear()

                    Call gpPopGridView(dgvChq, msSql, gOdbcConn)

                    With dgvChq
                        .Columns("table_type").Visible = False
                        .Columns("packet_gid").Visible = False
                        .Columns("ref_gid").Visible = False

                        For i = 0 To .Columns.Count - 1
                            .Columns(i).ReadOnly = True
                            .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                        Next i

                        n = .ColumnCount

                        lobjChkBox.HeaderText = "Select"
                        lobjChkBox.Name = "Select"
                        .Columns.Insert(0, lobjChkBox)

                        For i = 0 To .RowCount - 1
                            .Rows(i).Cells("Select").Value = False
                        Next i

                        .Columns("SNo").Width = 32
                        .Columns("Select").Width = 48

                        lblCount.Text = "Total Records : " & .RowCount
                    End With
                Else
                    txtPktId.Text = ""
                End If

                .Rows.Clear()
            End With
        End If
    End Sub

    Private Sub frmPacketPullout_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmPacketPullout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' load packet pullout reason
        msSql = ""
        msSql &= " select * from chola_mst_tpulloutreason "
        msSql &= " where 1 = 1 "
        msSql &= " and reason_deleteflag = 'N' "
        msSql &= " order by reason_name "

        Call gpBindCombo(msSql, "reason_name", "reason_gid", cboReason, gOdbcConn)

        Call EnableSave(False)
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        If txtId.Text = "" Then Call gpSelectColumnGrid(dgvChq, "Select", True)
    End Sub

    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
        If txtId.Text = "" Then Call gpSelectColumnGrid(dgvChq, "Select", False)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Call ClearControl()
        Call EnableSave(False)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim c As Long = 0

        pnlSave.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        frmMain.lblstatus.Text = "Records updating ..."

        Call SaveRecords()

        frmMain.lblstatus.Text = ""
        Me.Cursor = System.Windows.Forms.Cursors.Default
        pnlSave.Enabled = True
    End Sub

    Private Sub SaveRecords()
        Dim i As Integer
        Dim lnPktId As Long = 0
        Dim lnPktPulloutId As Long = 0
        Dim lnRefId As Long = 0
        Dim lsTableType As String = ""
        Dim lsLooseChq As String = ""
        Dim lsReasonDesc As String = ""
        Dim lnResult As Long = 0

        lnPktId = Val(txtPktId.Text)

        If lnPktId = 0 Then
            MsgBox("Invalid packet !", MsgBoxStyle.Critical, gProjectName)
            txtPktNo.Focus()
            Exit Sub
        End If

        If cboReason.Text = "" Or cboReason.SelectedIndex = -1 Then
            MsgBox("Please select valid reason !", MsgBoxStyle.Information, gProjectName)
            cboReason.Focus()
            Exit Sub
        End If

        ' Validate cheques
        With dgvChq
            If .Rows.Count = 0 Then
                If MsgBox("No cheque found ! Do you want to mark it as packet pullout ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                    Exit Sub
                End If
            Else
                For i = 0 To .RowCount - 1
                    If .Rows(i).Cells("Select").Value = False Then
                        MsgBox("All cheques in the grid was not selected !", MsgBoxStyle.Information, gProjectName)
                        dgvChq.Focus()
                        Exit Sub
                    End If
                Next i
            End If
        End With

        ' formating values
        lsReasonDesc = Mid(QuoteFilter(cboReason.Text), 1, 255)

        If txtId.Text = "" Then
            msSql = ""
            msSql &= " select packet_gid from chola_trn_tpacket "
            msSql &= " where packet_gid = " & lnPktId & " "
            msSql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "

            lnPktId = Val(gfExecuteScalar(msSql, gOdbcConn))

            If lnPktId > 0 Then
                ' insert in packet pullout table
                msSql = ""
                msSql &= " insert into chola_trn_tpacketpullout (packetpullout_file_gid,packetpullout_packet_gid,packetpullout_reason,packetpullout_postflag,"
                msSql &= " packetpullout_postedon,packetpullout_postedby) values ("
                msSql &= " 0," & lnPktId & ","
                msSql &= " '" & lsReasonDesc & "',"
                msSql &= " 'Y',"
                msSql &= " sysdate(),"
                msSql &= " '" & gUserName & "')"

                lnResult = gfInsertQry(msSql, gOdbcConn)

                ' find new packetpullout_gid
                msSql = ""
                msSql &= " select max(packetpullout_gid) from chola_trn_tpacketpullout "
                msSql &= " where packetpullout_packet_gid = " & lnPktId & " "

                lnPktPulloutId = Val(gfExecuteScalar(msSql, gOdbcConn))

                ' update in cheque table
                With dgvChq
                    If .RowCount > 0 Then
                        For i = 0 To .RowCount - 1
                            lnRefId = Val(.Rows(i).Cells("ref_gid").Value)
                            lsTableType = .Rows(i).Cells("table_type").Value
                            lsLooseChq = .Rows(i).Cells("loose_chq").Value

                            Select Case lsTableType
                                Case "PDC"
                                    msSql = ""

                                    Select Case lsLooseChq
                                        Case "N"
                                            msSql = ""
                                            msSql &= " update chola_trn_tpdcentry set "
                                            msSql &= " chq_status=chq_status | " & GCPACKETPULLOUT & ","
                                            msSql &= " chq_desc='" & lsReasonDesc & "'"
                                            msSql &= " where entry_gid = " & lnRefId & " "
                                            msSql &= " and chq_packet_gid=" & lnPktId & " "
                                            msSql &= " and chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
                                        Case "Y"
                                            msSql = ""
                                            msSql &= " update chola_trn_tpdcentry set "
                                            msSql &= " chq_status=chq_status | " & GCPACKETPULLOUT & ","
                                            msSql &= " chq_desc='" & lsReasonDesc & "'"
                                            msSql &= " where entry_gid = " & lnRefId & " "
                                            msSql &= " and chq_packet_gid=" & lnPktId & " "
                                            msSql &= " and chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0 "
                                            msSql &= " and chq_status & " & GCLOOSECHQ & " > 0 "
                                    End Select

                                    If msSql <> "" Then lnResult = gfInsertQry(msSql, gOdbcConn)
                                Case "SPDC"
                                    msSql = ""
                                    msSql &= " update chola_trn_tspdcchqentry set "
                                    msSql &= " chqentry_status = chqentry_status | " & GCPACKETPULLOUT & ","
                                    msSql &= " chqentry_remarks ='" & lsReasonDesc & "'"
                                    msSql &= " where chqentry_gid = " & lnRefId & " "
                                    msSql &= " and chqentry_packet_gid=" & lnPktId & " "
                                    msSql &= " and chqentry_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                                    lnResult = gfInsertQry(msSql, gOdbcConn)
                            End Select
                        Next i
                    End If
                End With

                ' update in packet table
                msSql = ""
                msSql &= " update chola_trn_tpacket set "
                msSql &= " packet_status=packet_status | " & GCIPACKETPULLOUT & " "
                msSql &= " where packet_gid = " & lnPktId
                msSql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "

                lnResult = gfInsertQry(msSql, gOdbcConn)

                MsgBox("Ref No : " & lnPktPulloutId, MsgBoxStyle.Information, gProjectName)

                Call ClearControl()

                If MsgBox("Do you want to add another record ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                    txtPktNo.Focus()
                Else
                    Call EnableSave(False)
                    btnNew.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub EnableSave(ByVal Status As Boolean)
        pnlButtons.Visible = Not Status
        pnlSave.Visible = Status
        pnlMain.Enabled = Status
    End Sub

    Private Sub ClearControl()
        Call frmCtrClear(Me)
        txtPktNo.Focus()

        dgvChq.DataSource = Nothing
        dgvChq.Columns.Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Call ClearControl()
        Call EnableSave(True)
        txtPktNo.Focus()
    End Sub

    Private Sub txtPktNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPktNo.TextChanged

    End Sub

    Private Sub ListAll(ByVal PacketPulloutId As Long)
        Dim i As Integer
        Dim ds As New DataSet
        Dim lnResult As Long = 0

        msSql = ""
        msSql &= " select a.*,b.packet_gnsarefno,c.shortagreement_no,c.agreement_no from chola_trn_tpacketpullout as a "
        msSql &= " inner join chola_trn_tpacket as b on b.packet_gid = a.packetpullout_packet_gid "
        msSql &= " left join chola_mst_tagreement as c on c.agreement_gid = b.packet_agreement_gid  "
        msSql &= " where a.packetpullout_gid = " & PacketPulloutId & " "

        Call gpDataSet(msSql, "pkt", gOdbcConn, ds)

        With ds.Tables("pkt")
            If .Rows.Count > 0 Then
                txtId.Text = .Rows(0).Item("packetpullout_gid").ToString
                txtPktId.Text = .Rows(0).Item("packetpullout_packet_gid").ToString
                txtPktNo.Text = .Rows(0).Item("packet_gnsarefno").ToString
                txtAgrNo.Text = .Rows(0).Item("shortagreement_no").ToString & "-" & .Rows(0).Item("agreement_no").ToString

                cboReason.Text = .Rows(0).Item("packetpullout_reason").ToString
            End If

            .Rows.Clear()
        End With

        ' load cheque info
        lnResult = gfInsertQry("set @a := 0", gOdbcConn)

        msSql = ""
        msSql = ""
        msSql &= " select "
        msSql &= " @a:=@a+1 as 'SNo',"
        msSql &= " chq_date as 'Chq Date',"
        msSql &= " chq_no as 'Chq No',"
        msSql &= " chq_amount as 'Amount',"
        msSql &= " make_set(chq_type,'PDC','SPDC') as 'Chq',"
        msSql &= " if(chq_status & " & GCLOOSECHQ & " = 0,'N','Y') as 'loose_chq',"
        msSql &= " 'PDC' as 'table_type',"
        msSql &= " chq_packet_gid as packet_gid,entry_gid as ref_gid "
        msSql &= " from chola_trn_tpdcentry "
        msSql &= " where chq_packet_gid=" & txtPktId.Text & " "
        msSql &= " and chq_status & " & GCPACKETPULLOUT & " > 0 "

        msSql &= " union "

        msSql &= " select "
        msSql &= " @a:=@a+1 as 'SNo',"
        msSql &= " null as 'Chq Date',"
        msSql &= " chqentry_chqno as 'Chq No',"
        msSql &= " null as 'Amount',"
        msSql &= " 'SPDC' as 'Chq',"
        msSql &= " 'N' as 'loose_chq',"
        msSql &= " 'SPDC' as 'table_type',"
        msSql &= " chqentry_packet_gid as packet_gid,chqentry_gid as ref_gid "
        msSql &= " from chola_trn_tspdcchqentry "
        msSql &= " where chqentry_packet_gid=" & txtPktId.Text & " "
        msSql &= " and chqentry_status & " & GCPACKETPULLOUT & " > 0 "

        msSql &= " order by 'Chq No' "

        Call gpPopGridView(dgvChq, msSql, gOdbcConn)

        With dgvChq
            .Columns("table_type").Visible = False
            .Columns("packet_gid").Visible = False
            .Columns("ref_gid").Visible = False

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblCount.Text = "Total Records : " & .RowCount
        End With
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As Search
        Try
            SearchDialog = New Search(gOdbcConn, _
                             " select a.packetpullout_gid as 'SPDC Pullout Id',b.packet_gnsarefno as 'Packet No'," & _
                             " c.shortagreement_no as 'Short Agreement No',c.agreement_no as 'Agreement No'," & _
                             " a.packetpullout_reason as 'Reason',a.packetpullout_postedon as 'Entry Date',a.packetpullout_postedby as 'Entry By' " & _
                             " FROM chola_trn_tpacketpullout as a " & _
                             " inner join chola_trn_tpacket as b on b.packet_gid = a.packetpullout_packet_gid " & _
                             " left join chola_mst_tagreement as c on c.agreement_gid = b.packet_agreement_gid  ",
                             " a.packetpullout_gid,b.packet_gnsarefno,c.shortagreement_no,c.agreement_no,a.packetpullout_reason,a.packetpullout_postedon,a.packetpullout_postedby", _
                             " 1 = 1 and a.packetpullout_file_gid = 0 ")
            SearchDialog.ShowDialog()

            If txt <> 0 Then
                Call ListAll(txt)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If txtId.Text <> "" Then
            If MsgBox("Are you sure to delete ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
                Call UndoPacket(Val(txtPktId.Text))
            End If
        End If
    End Sub

    Private Sub UndoPacket(ByVal PktId As Long)
        Dim licnt As Integer = 0

        If MsgBox("Are you sure to undo ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        msSql = " select packet_gid from chola_trn_tpacket "
        msSql &= " where packet_gid = " & PktId
        msSql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "

        PktId = Val(gfExecuteScalar(msSql, gOdbcConn))

        If PktId > 0 Then
            msSql = " update chola_trn_tpacket set "
            msSql &= " packet_status = (packet_status | " & GCIPACKETPULLOUT & ") ^ " & GCIPACKETPULLOUT
            msSql &= " where packet_gid = " & PktId
            msSql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "

            gfInsertQry(msSql, gOdbcConn)

            'Cheque Level
            msSql = " update chola_trn_tpdcentry set "
            msSql &= " chq_status = (chq_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT
            msSql &= " where chq_packet_gid=" & PktId & " and chq_status & " & GCPACKETPULLOUT & " > 0 "

            gfInsertQry(msSql, gOdbcConn)

            msSql = " update chola_trn_tspdcchqentry set "
            msSql &= " chqentry_status = (chqentry_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT
            msSql &= " where chqentry_packet_gid=" & PktId & " and chqentry_status & " & GCPACKETPULLOUT & " > 0 "

            gfInsertQry(msSql, gOdbcConn)

            msSql = " update chola_trn_tecsemientry set "
            msSql &= " ecsemientry_status = (ecsemientry_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT
            msSql &= " where ecsemientry_packet_gid = " & PktId & " and ecsemientry_status & " & GCPACKETPULLOUT & " > 0 "

            gfInsertQry(msSql, gOdbcConn)

            ' Packet pullout table
            msSql = " update chola_trn_tpacketpullout set "
            msSql &= " packetpullout_undoon = sysdate(),packetpullout_undoby = '" & gUserName & "',packetpullout_postflag = 'U' "
            msSql &= " where packetpullout_packet_gid=" & PktId
            msSql &= " and packetpullout_postflag = 'Y' "

            gfInsertQry(msSql, gOdbcConn)

            MsgBox("Packet undo made successfully !", MsgBoxStyle.Information, gProjectName)

            Call ClearControl()
            Call EnableSave(False)
        Else
            MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
        End If
    End Sub

    Private Sub txtAgrNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAgrNo.TextChanged

    End Sub
End Class