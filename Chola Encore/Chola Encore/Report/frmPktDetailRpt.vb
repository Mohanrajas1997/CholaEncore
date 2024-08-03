Public Class frmPktDetailRpt

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        btnRefresh.Enabled = True
        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False
        dtpRcvdFrom.Checked = False
        dtpRcvdTo.Checked = False
        txtagreementno.Text = ""
        txtgnsarefno.Text = ""
        cboStatus.SelectedIndex = -1
        txtBatchNo.Text = ""
        dgvRpt.DataSource = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub frmPacketReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmPacketReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpRcvdFrom.Value = Now
        dtpRcvdTo.Value = Now

        dtpRcvdFrom.Checked = False
        dtpRcvdTo.Checked = False

        dtpEntryFrom.Value = Now
        dtpEntryTo.Value = Now

        dtpEntryFrom.Checked = False
        dtpEntryTo.Checked = False

        With cboStatus
            .Items.Clear()
            .Items.Add("Vault")
            .Items.Add("Presentation Pullout")
            .Items.Add("Presentation DE")
            .Items.Add("Despatch")
            .Items.Add("Closure")
            .Items.Add("Packet Pullout")
            .Items.Add("Pullout")
            .Items.Add("Bounce")
            .Items.Add("Bounce Pullout")
        End With

        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtagreementno.Focus()
        txtagreementno.Text = ""
    End Sub


    Private Sub frmPacketReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            With dgvRpt
                .Width = Me.Width - 30
                .Height = Me.Height - Panel1.Height - 90
                pnlDisplay.Width = Me.Width - 30
                pnlDisplay.Top = Panel1.Top + Panel1.Height + dgvRpt.Height + 15
                btnExport.Left = pnlDisplay.Width - (btnExport.Width + 10)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        btnRefresh.Enabled = False

        Select Case True
            Case rdoPdc.Checked
                Call PdcData()
            Case rdoSpdc.Checked
                Call SpdcData()
            Case rdoEcs.Checked
                Call EcsData()
        End Select

        btnRefresh.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PdcData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsTxt As String
        Dim lsDisc As String
        Dim lnResult As Long
        Dim lsCond As String = ""
        Dim lsFld As String = ""

        Try
            If dtpEntryFrom.Checked Then lsCond &= " and p.packet_entryon >= '" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked Then lsCond &= " and p.packet_entryon < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpRcvdFrom.Checked Then lsCond &= " and p.packet_receiveddate >= '" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            If dtpRcvdTo.Checked Then lsCond &= " and p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"

            If dtpChqDate.Checked Then lsCond &= " and c.chq_date = '" & Format(dtpChqDate.Value, "yyyy-MM-dd") & "'"

            If txtagreementno.Text <> "" Then
                If IsNumeric(txtagreementno.Text) Then
                    lsCond &= " and a.shortagreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%' "
                Else
                    lsCond &= " and a.agreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%'"
                End If
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno ='" & txtgnsarefno.Text.Trim & "'"
            End If

            If txtBatchNo.Text <> "" Then
                lsCond &= " and t.batch_no= " & Val(txtBatchNo.Text) & " "
            End If

            If txtChqNo.Text <> "" Then
                lsCond &= " and c.chq_no like '" & QuoteFilter(txtChqNo.Text) & "%' "
            End If

            If Val(txtChqAmt.Text) > 0 Then
                lsCond &= " and c.chq_amount = " & Val(txtChqAmt.Text) & " "
            End If

            Select Case cboStatus.Text
                Case "Vault"
                    lsCond &= " and c.chq_status & " & (GCPULLOUT Or GCPACKETPULLOUT Or GCCLOSURE) & " = 0 "
                    lsCond &= " and (c.chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsCond &= " or c.chq_status & " & GCBOUNCERECEIVED & " > 0 ) "
                Case "Presentation Pullout"
                    lsCond &= " and c.chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                Case "Presentation DE"
                    lsCond &= " and c.chq_status & " & GCPRESENTATIONDE & " > 0 "
                Case "Despatch"
                    lsCond &= " and c.chq_status & " & GCDESPATCH & " > 0 "
                Case "Closure"
                    lsCond &= " and c.chq_status & " & GCCLOSURE & " > 0 "
                Case "Packet Pullout"
                    lsCond &= " and c.chq_status & " & GCPACKETPULLOUT & " > 0 "
                Case "Pullout"
                    lsCond &= " and c.chq_status & " & GCPULLOUT & " > 0 "
                Case "Bounce"
                    lsCond &= " and c.chq_status & " & GCBOUNCERECEIVED & " > 0 "
                Case "Bounce Pullout"
                    lsCond &= " and c.chq_status & " & GCBOUNCEPULLOUTENTRY & " > 0 "
            End Select

            If Val(txtPktId.Text) > 0 Then lsCond &= " and p.packet_gid = " & Val(txtPktId.Text) & " "
            If txtMicrCode.Text <> "" Then lsCond &= " and c.chq_micrcode = '" & QuoteFilter(txtMicrCode.Text) & "' "

            If chkQry.Checked = True Then lsCond &= " and q.import_by = '" & gUserName & "' "
            If lsCond = "" Then lsCond &= " and 1 = 2 "

            ' Pdc
            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Cheque' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            ' Chq Disc
            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Disc' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsDisc = gfExecuteScalar(lsSql, gOdbcConn)
            lsDisc = "'" & lsDisc.Replace(",", "','") & "'"

            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',c.chq_pdc_gid as 'Finone Id',"
            lsFld &= " c.chq_date as 'Chq Date',c.chq_no as 'Chq No',c.chq_amount as 'Chq Amount',"
            lsFld &= " make_set(c.chq_status," & lsTxt & ") as 'Chq Status',"
            lsFld &= " make_set(c.chq_disc_value," & lsDisc & ") as 'Chq Disc',"
            lsFld &= " c.chq_desc as 'Chq Desc',"
            lsFld &= " c.chq_status as 'Chq Status Value',"
            lsFld &= " c.chq_disc_value as 'Chq Disc Value',"
            lsFld &= " c.chq_accno as 'A/C No',"
            lsFld &= " c.chq_micrcode as 'Micr Code',"
            lsFld &= " c.chq_bankcode as 'Bank Code',"
            lsFld &= " c.chq_bankname as 'Bank Name',"
            lsFld &= " c.chq_bankbranch as 'Bank Branch',"
            lsFld &= " c.chq_type as 'Chq Type',"
            lsFld &= " c.chq_papflag as 'PAP Flag',"
            lsFld &= " c.chq_iscts as 'CTS Flag',"
            lsFld &= " c.chq_ismicr as 'Micr Flag',"
            lsFld &= " c.chq_prodtype as 'Prod Type',"
            lsFld &= " c.chq_paymodedisc as 'PDC Paymode Disc',"
            lsFld &= " c.chq_contractno as 'Contract No',"
            lsFld &= " make_set(c.chq_predisc," & lsDisc & ") as 'Pre Disc',"
            lsFld &= " make_set(c.chq_postdisc," & lsDisc & ") as 'Post Disc',"
            lsFld &= " c.chq_predisc as 'Pre Disc Value',"
            lsFld &= " c.chq_postdisc as 'Post Disc Value',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Packet Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " t.batch_displayno as 'Batch No',"
            lsFld &= " p.packet_paymodedisc as 'Packet Paymode Disc',p.packet_remarks as 'Remark',p.packet_receiveddate as 'Packet Received Date',p.packet_entryon as 'Packet Entry Date',p.packet_entryby as 'Packet Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',c.chq_batch_gid as 'Batch Id',c.entry_gid as 'Pdc Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld

            If chkQry.Checked = True Then
                lsSql &= " from chola_rpt_tchqqry as q "
                lsSql &= " inner join chola_trn_tpdcentry c on c.chq_no = q.chq_no and c.chq_amount = q.chq_amount "
            Else
                lsSql &= " from chola_trn_tpdcentry c "
            End If

            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " left join chola_trn_tbatch t on t.batch_gid = c.chq_batch_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " order by c.chq_packet_gid"

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            For i = 0 To dgvRpt.Columns.Count - 1
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvRpt.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If dgvRpt.RowCount = 0 Then
                MsgBox("No Details to Export!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            Call PrintDGViewXML(dgvRpt, gsReportPath & "Packet Report.xls", "Packet Details")

            MsgBox(" Exported to Excel !!" & Chr(13) & _
                   " Saved Path : " & gsReportPath & "Packet Report", MsgBoxStyle.Information, gProjectName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SpdcData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsTxt As String
        Dim lsDisc As String
        Dim lnResult As Long
        Dim lsCond As String = ""
        Dim lsFld As String = ""

        Try
            If dtpEntryFrom.Checked Then lsCond &= " and p.packet_entryon >= '" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked Then lsCond &= " and p.packet_entryon < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpRcvdFrom.Checked Then lsCond &= " and p.packet_receiveddate >= '" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            If dtpRcvdTo.Checked Then lsCond &= " and p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"

            If txtagreementno.Text <> "" Then
                If IsNumeric(txtagreementno.Text) Then
                    lsCond &= " and a.shortagreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%' "
                Else
                    lsCond &= " and a.agreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%'"
                End If
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno ='" & txtgnsarefno.Text.Trim & "'"
            End If

            If txtChqNo.Text <> "" Then
                lsCond &= " and c.chqentry_chqno like '" & QuoteFilter(txtChqNo.Text) & "%' "
            End If

            Select Case cboStatus.Text
                Case "Vault"
                    lsCond &= " and c.chqentry_status & " & (GCPULLOUT Or GCPACKETPULLOUT Or GCCLOSURE) & " = 0 "
                    lsCond &= " and (c.chqentry_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsCond &= " or c.chqentry_status & " & GCBOUNCERECEIVED & " > 0 ) "
                Case "Presentation Pullout"
                    lsCond &= " and c.chqentry_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                Case "Presentation DE"
                    lsCond &= " and c.chqentry_status & " & GCPRESENTATIONDE & " > 0 "
                Case "Despatch"
                    lsCond &= " and c.chqentry_status & " & GCDESPATCH & " > 0 "
                Case "Closure"
                    lsCond &= " and c.chqentry_status & " & GCCLOSURE & " > 0 "
                Case "Packet Pullout"
                    lsCond &= " and c.chqentry_status & " & GCPACKETPULLOUT & " > 0 "
                Case "Pullout"
                    lsCond &= " and c.chqentry_status & " & GCPULLOUT & " > 0 "
                Case "Bounce"
                    lsCond &= " and c.chqentry_status & " & GCBOUNCERECEIVED & " > 0 "
                Case "Bounce Pullout"
                    lsCond &= " and c.chqentry_status & " & GCBOUNCEPULLOUTENTRY & " > 0 "
            End Select

            If Val(txtPktId.Text) > 0 Then lsCond &= " and p.packet_gid = " & Val(txtPktId.Text) & " "
            If txtMicrCode.Text <> "" Then lsCond &= " and c.chqentry_micrcode = '" & QuoteFilter(txtMicrCode.Text) & "' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            ' Pdc
            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Cheque' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            ' Chq Disc
            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Disc' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsDisc = gfExecuteScalar(lsSql, gOdbcConn)
            lsDisc = "'" & lsDisc.Replace(",", "','") & "'"

            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',c.chqentry_pdc_gid as 'Finone Id',"
            lsFld &= " c.chqentry_chqno as 'Chq No',"
            lsFld &= " c.chqentry_accno as 'A/C No',"
            lsFld &= " c.chqentry_micrcode as 'Micr Code',"
            lsFld &= " c.chqentry_bankcode as 'Bank Code',"
            lsFld &= " c.chqentry_bankname as 'Bank Name',"
            lsFld &= " c.chqentry_branchname as 'Bank Branch',"
            lsFld &= " c.chqentry_iscts as 'CTS Flag',"
            lsFld &= " c.chqentry_ismicr as 'Micr Flag',"
            lsFld &= " c.chqentry_paymodedisc as 'SPDC Paymode Disc',"
            lsFld &= " c.chqentry_remarks as 'Chq Remark',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Packet Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " make_set(c.chqentry_status," & lsTxt & ") as 'Chq Status',"
            lsFld &= " c.chqentry_status as 'Chq Status Value',"
            lsFld &= " p.packet_paymodedisc as 'Packet Paymode Disc',p.packet_remarks as 'Remark',p.packet_entryon as 'Packet Entry Date',p.packet_receiveddate as 'Packet Received Date',p.packet_entryby as 'Packet Entry By',"
            lsFld &= " c.chqentry_entryon as 'SPDC Entry Date',c.chqentry_entryby as 'SPDC Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',c.chqentry_gid as 'Spdc Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tspdcchqentry c "
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid = c.chqentry_packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " order by c.chqentry_packet_gid,c.chqentry_gid"

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            For i = 0 To dgvRpt.Columns.Count - 1
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvRpt.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub EcsData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsTxt As String
        Dim lsDisc As String
        Dim lnResult As Long
        Dim lsCond As String = ""
        Dim lsFld As String = ""

        Try
            If dtpEntryFrom.Checked Then lsCond &= " and p.packet_entryon >= '" & Format(dtpEntryFrom.Value, "yyyy-MM-dd") & "'"
            If dtpEntryTo.Checked Then lsCond &= " and p.packet_entryon < '" & Format(DateAdd(DateInterval.Day, 1, dtpEntryTo.Value), "yyyy-MM-dd") & "'"

            If dtpRcvdFrom.Checked Then lsCond &= " and p.packet_receiveddate >= '" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            If dtpRcvdTo.Checked Then lsCond &= " and p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"

            If txtagreementno.Text <> "" Then
                If IsNumeric(txtagreementno.Text) Then
                    lsCond &= " and a.shortagreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%' "
                Else
                    lsCond &= " and a.agreement_no like '" & QuoteFilter(txtagreementno.Text.Trim) & "%'"
                End If
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno ='" & txtgnsarefno.Text.Trim & "'"
            End If

            If txtChqAmt.Text <> "" Then
                lsCond &= " and c.ecsemientry_amount = '" & Val(txtChqAmt.Text) & "' "
            End If

            Select Case cboStatus.Text
                Case "Vault"
                    lsCond &= " and c.ecsemientry_status & " & (GCPULLOUT Or GCPACKETPULLOUT Or GCCLOSURE) & " = 0 "
                    lsCond &= " and (c.ecsemientry_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsCond &= " or c.ecsemientry_status & " & GCBOUNCERECEIVED & " > 0 ) "
                Case "Presentation Pullout"
                    lsCond &= " and c.ecsemientry_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                Case "Presentation DE"
                    lsCond &= " and c.ecsemientry_status & " & GCPRESENTATIONDE & " > 0 "
                Case "Despatch"
                    lsCond &= " and c.ecsemientry_status & " & GCDESPATCH & " > 0 "
                Case "Closure"
                    lsCond &= " and c.ecsemientry_status & " & GCCLOSURE & " > 0 "
                Case "Packet Pullout"
                    lsCond &= " and c.ecsemientry_status & " & GCPACKETPULLOUT & " > 0 "
                Case "Pullout"
                    lsCond &= " and c.ecsemientry_status & " & GCPULLOUT & " > 0 "
                Case "Bounce"
                    lsCond &= " and c.ecsemientry_status & " & GCBOUNCERECEIVED & " > 0 "
                Case "Bounce Pullout"
                    lsCond &= " and c.ecsemientry_status & " & GCBOUNCEPULLOUTENTRY & " > 0 "
            End Select

            If Val(txtPktId.Text) > 0 Then lsCond &= " and p.packet_gid = " & Val(txtPktId.Text) & " "
            If txtMicrCode.Text <> "" Then lsCond &= " and c.ecsemientry_micrcode = '" & QuoteFilter(txtMicrCode.Text) & "' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            ' Pdc
            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Cheque' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            ' Chq Disc
            lsSql = ""
            lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
            lsSql &= " where status_level = 'Disc' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag "

            lsDisc = gfExecuteScalar(lsSql, gOdbcConn)
            lsDisc = "'" & lsDisc.Replace(",", "','") & "'"

            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',c.ecsemientry_pdc_gid as 'Finone Id',"
            lsFld &= " c.ecsemientry_emidate as 'Ecs Date',"
            lsFld &= " c.ecsemientry_amount as 'Ecs Amount',"
            lsFld &= " c.ecsemientry_accno as 'A/C No',"
            lsFld &= " c.ecsemientry_micrcode as 'Micr Code',"
            lsFld &= " c.ecsemientry_paymodedisc as 'Ecs Paymode Disc',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Packet Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " make_set(c.ecsemientry_status," & lsTxt & ") as 'Ecs Status',"
            lsFld &= " c.ecsemientry_status as 'Ecs Status Value',"
            lsFld &= " p.packet_paymodedisc as 'Packet Paymode Disc',p.packet_remarks as 'Remark',p.packet_receiveddate as 'Packet Received Date',p.packet_entryon as 'Packet Entry Date',p.packet_entryby as 'Packet Entry By',"
            lsFld &= " c.ecsemientry_entryon as 'Ecs Entry Date',c.ecsemientry_entryby as 'Ecs Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',c.ecsemientry_gid as 'Ecs Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tecsemientry c "
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid = c.ecsemientry_packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " order by c.ecsemientry_packet_gid,c.ecsemientry_gid"

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            For i = 0 To dgvRpt.Columns.Count - 1
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvRpt.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
End Class