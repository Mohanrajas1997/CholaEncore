Public Class frmSearchEngine
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsFld As String
        Dim lsTxt As String
        Dim lsDisc As String
        Dim lsCond As String
        Dim lnPktId As Long = 0
        Dim lnAgrId As Long = 0

        Dim i As Integer = 0
        Dim n As Integer = 0
        Dim lnPdc As Integer = 0
        Dim lnSpdc As Integer = 0
        Dim lnEcs As Integer = 0
        Dim lnResult As Long

        Dim dgvRow As DataGridViewRow = Nothing

        Try
            ' Find Agreement Id
            If cboAgreementNo.Text = "" Or cboAgreementNo.SelectedIndex = -1 Then
                MsgBox("Please select agreement no !", MsgBoxStyle.Information, gProjectName)
                cboAgreementNo.Focus()
                Exit Sub
            End If

            If cboAgreementNo.Text <> "" And cboAgreementNo.SelectedIndex <> -1 Then
                lsSql = ""
                lsSql &= " select agreement_gid from chola_mst_tagreement "
                lsSql &= " where agreement_no = '" & QuoteFilter(cboAgreementNo.Text) & "' "

                lnAgrId = Val(gfExecuteScalar(lsSql, gOdbcConn))
            End If

            ' Find Packet Id
            If txtPktNo.Text <> "" Then
                lsSql = ""
                lsSql &= " select packet_gid,packet_agreement_gid from chola_trn_tpacket "
                lsSql &= " where packet_gnsarefno = '" & QuoteFilter(txtPktNo.Text) & "' "

                Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

                With ds.Tables("pkt")
                    If .Rows.Count > 0 Then
                        lnPktId = .Rows(0).Item("packet_gid")
                        If lnAgrId = 0 Then lnAgrId = .Rows(0).Item("packet_agreement_gid")
                    Else
                        MsgBox("Invalid packet no !", MsgBoxStyle.Critical, gProjectName)
                        Exit Sub
                    End If
                    .Rows.Clear()
                End With
            End If

            If lnAgrId = 0 Then
                MsgBox("Invalid agreement no !", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            dgvSpdc.DataSource = Nothing
            dgvSpdcHeader.DataSource = Nothing
            dgvPkt.DataSource = Nothing
            dgvEcsEmi.DataSource = Nothing
            dgvPdc.DataSource = Nothing
            dgvFinone.DataSource = Nothing

            ' Packet
            lsSql = ""
            lsSql &= " select group_concat(a.status_desc) from (select status_desc from chola_mst_tstatus "
            lsSql &= " where status_level = 'Packet' "
            lsSql &= " and status_deleteflag = 'N' "
            lsSql &= " order by status_flag) as a "

            lsTxt = gfExecuteScalar(lsSql, gOdbcConn)
            lsTxt = "'" & lsTxt.Replace(",", "','") & "'"

            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " p.packet_receiveddate as 'Received Date',p.packet_entryon as 'Entry Date',i.inward_userauthdate as 'User Auth Date',p.packet_gnsarefno as 'GNSA Ref No',"
            lsFld &= " a.agreement_no as 'Agreement No',a.shortagreement_no as 'Short Agreement No',p.packet_mode as 'Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',make_set(p.packet_status," & lsTxt & ") as 'Packet',"
            lsFld &= " p.packet_paymodedisc as 'Paymode Disc',p.packet_remarks as 'Remark',p.packet_entryby as 'Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id'"

            lsCond = ""
            If lnPktId > 0 Then lsCond &= " and p.packet_gid = " & lnPktId & " "
            If lnAgrId > 0 Then lsCond &= " and a.agreement_gid = " & lnAgrId & " "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tpacket p"
            lsSql &= " left join chola_trn_tinward i on i.inward_packet_gid = p.packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid"
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " order by p.packet_receiveddate,p.packet_entryon"

            gpPopGridView(dgvPkt, lsSql, gOdbcConn)

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
            lsFld &= " c.chq_paymodedisc as 'Paymode Disc',"
            lsFld &= " c.chq_contractno as 'Contract No',"
            lsFld &= " make_set(c.chq_predisc," & lsDisc & ") as 'Pre Disc',"
            lsFld &= " make_set(c.chq_postdisc," & lsDisc & ") as 'Post Disc',"
            lsFld &= " c.chq_predisc as 'Pre Disc Value',"
            lsFld &= " c.chq_postdisc as 'Post Disc Value',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " p.packet_paymodedisc as 'Paymode Disc',p.packet_remarks as 'Remark',p.packet_entryon as 'Entry Date',p.packet_entryby as 'Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',c.entry_gid as 'Pdc Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tpdcentry c "
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= IIf(chkDisc.Checked = True, "and c.chq_pdc_gid = 0", "")
            lsSql &= " order by c.chq_packet_gid,c.chq_date"

            gpPopGridView(dgvPdc, lsSql, gOdbcConn)

            ' SPDC Header
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " h.spdcentry_accountno as 'A/C No',"
            lsFld &= " h.spdcentry_micrcode as 'Micr Code',"
            lsFld &= " h.spdcentry_startdate as 'Start Date',"
            lsFld &= " h.spdcentry_enddate as 'End Date',"
            lsFld &= " h.spdcentry_ecsamount as 'Ecs Amount',"
            lsFld &= " h.spdcentry_spdccount as 'SPDC Count',"
            lsFld &= " h.spdcentry_ecsmandatecount as 'Mandate Count',"
            lsFld &= " h.spdcentry_remarks as 'SPDC Remark',"
            lsFld &= " h.spdcentry_ctschqcount as 'CTS Count',"
            lsFld &= " h.spdcentry_nonctschqcount as 'Non CTS Count',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " p.packet_paymodedisc as 'Paymode Disc',p.packet_remarks as 'Remark',p.packet_entryon as 'Entry Date',p.packet_entryby as 'Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',h.spdcentry_gid as 'SPDC Header Id' "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tspdcentry h "
            lsSql &= " inner join chola_trn_tpacket p on h.spdcentry_packet_gid = p.packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " order by h.spdcentry_packet_gid"

            gpPopGridView(dgvSpdcHeader, lsSql, gOdbcConn)

            ' SPDC
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " s.chqentry_pdc_gid as 'Finone Id',"
            lsFld &= " s.chqentry_chqno as 'Chq No',"
            lsFld &= " s.chqentry_accno as 'A/C No',"
            lsFld &= " s.chqentry_micrcode as 'Micr Code',"
            lsFld &= " s.chqentry_bankcode as 'Bank Code',"
            lsFld &= " s.chqentry_bankname as 'Bank Name',"
            lsFld &= " s.chqentry_iscts as 'CTS Flag',"
            lsFld &= " s.chqentry_entryby as 'SPDC Entry By',"
            lsFld &= " s.chqentry_entryon as 'SPDC Entry On',"
            lsFld &= " s.chqentry_remarks as 'SPDC Remarks',"
            lsFld &= " s.chqentry_status as 'SPDC Status',"
            lsFld &= " make_set(s.chqentry_status," & lsTxt & ") as 'SPDC Status',"
            lsFld &= " s.chqentry_paymodedisc as 'SPDC Paymode Disc',"
            lsFld &= " s.chqentry_ismicr as 'Micr Flag',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " p.packet_paymodedisc as 'Paymode Disc',p.packet_remarks as 'Remark',p.packet_entryon as 'Entry Date',p.packet_entryby as 'Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',s.chqentry_gid as 'SPDC Id'"

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tspdcchqentry s "
            lsSql &= " inner join chola_trn_tpacket p on s.chqentry_packet_gid = p.packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= IIf(chkDisc.Checked = True, "and s.chqentry_pdc_gid = 0", "")
            lsSql &= " order by s.chqentry_chqno"

            gpPopGridView(dgvSpdc, lsSql, gOdbcConn)

            ' ECS Emi
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " e.ecsemientry_pdc_gid as 'Finone Id',"
            lsFld &= " e.ecsemientry_accno as 'A/C No',"
            lsFld &= " e.ecsemientry_micrcode as 'Micr Code',"
            lsFld &= " e.ecsemientry_emidate as 'EMI Date',"
            lsFld &= " e.ecsemientry_amount as 'EMI Amount',"
            lsFld &= " e.ecsemientry_entryby as 'EMI Entry By',"
            lsFld &= " e.ecsemientry_entryon as 'EMI Entry On',"
            lsFld &= " e.ecsemientry_status as 'EMI Status',"
            lsFld &= " e.ecsemientry_paymodedisc as 'EMI Paymode Disc',"
            lsFld &= " e.ecsemientry_isactive as 'Active Flag',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " p.packet_paymodedisc as 'Paymode Disc',p.packet_remarks as 'Remark',p.packet_entryon as 'Entry Date',p.packet_entryby as 'Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',e.ecsemientry_gid as 'EMI Id'"

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tecsemientry e "
            lsSql &= " inner join chola_trn_tpacket p on e.ecsemientry_packet_gid = p.packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " left join chola_mst_tagreement a on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= IIf(chkDisc.Checked = True, "and e.ecsemientry_pdc_gid = 0", "")
            lsSql &= " order by e.ecsemientry_emidate"

            gpPopGridView(dgvEcsEmi, lsSql, gOdbcConn)

            ' Finone
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',f.entry_gid as 'Pdc Id',"
            lsFld &= " f.pdc_importdate as 'Import Date',"
            lsFld &= " f.pdc_gnsarefno as 'Finone Ref No',"
            lsFld &= " f.pdc_acc_no as 'Bank A/C No',"
            lsFld &= " f.pdc_chqno as 'Chq No',"
            lsFld &= " f.pdc_chqdate as 'Chq Date',"
            lsFld &= " f.org_chqdate as 'Org Chq Date',"
            lsFld &= " f.pdc_draweename as 'Drawee Name',"
            lsFld &= " f.pdc_chqamount as 'Chq Amount',"
            lsFld &= " f.org_chqamount as 'Org Chq Amount',"
            lsFld &= " f.pdc_clientcode as 'Client Code',"
            lsFld &= " f.pdc_contractno as 'Contract No',"
            lsFld &= " f.pdc_micrcode as 'Micr Code',"
            lsFld &= " f.pdc_bankname as 'Bank Name',"
            lsFld &= " f.pdc_bankbranch as 'Bank Branch',"
            lsFld &= " f.pdc_payablelocation as 'Pay Loc',"
            lsFld &= " f.pdc_pickuplocation as 'Pick Loc',"
            lsFld &= " f.pdc_mode as 'Pay Mode',"
            lsFld &= " f.pdc_type as 'Pay Type',"
            lsFld &= " f.prod_type as 'Prod Type',"
            lsFld &= " f.atpar_flag as 'ATPAR Flag',"
            lsFld &= " f.pdc_branchname as 'Pdc Branch Name',"
            lsFld &= " f.pdc_status_flag as 'Pdc Status Flag',"
            lsFld &= " p.packet_gnsarefno as 'GNSA Ref No',a.agreement_no as 'Agreement No',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',"
            lsFld &= " p.packet_mode as 'Packet Pay Mode', "
            lsFld &= " p.packet_status as 'Packet Status',"
            lsFld &= " p.packet_paymodedisc as 'Paymode Disc',p.packet_remarks as 'Remark',p.packet_entryon as 'Entry Date',p.packet_entryby as 'Entry By',"
            lsFld &= " b.box_no as 'Box No',p.packet_ismultiplebank as 'Multiple Bank',"
            lsFld &= " p.packet_gid as 'Packet Id',p.packet_inward_gid as 'Inward Id',a.agreement_gid as 'Agreement Id',"
            lsFld &= " b.box_gid as 'Box Id',f.pdc_gid as 'Finone Id',f.pdc_spdcentry_gid as 'Spdc Id',f.pdc_ecsentry_gid as 'Ecs Id' "

            lsCond = ""
            If lnAgrId > 0 Then lsCond &= " and a.agreement_gid = " & lnAgrId & " "

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tpdcfile f "
            lsSql &= " left join chola_mst_tagreement a on f.pdc_parentcontractno = a.agreement_no "
            lsSql &= " left join chola_trn_tpdcfilehead h on h.head_file_gid = f.file_mst_gid and h.head_agreementno = f.pdc_parentcontractno "
            lsSql &= " left join chola_trn_tpacket p on p.packet_gid = h.head_packet_gid "
            lsSql &= " left join chola_trn_tbox b on p.packet_box_gid = b.box_gid "
            lsSql &= " where true "
            lsSql &= " and f.chq_rec_slno = 1 "
            lsSql &= IIf(chkDisc.Checked = True, "and f.entry_gid = 0", "")
            lsSql &= lsCond
            lsSql &= " order by f.pdc_gid"

            gpPopGridView(dgvFinone, lsSql, gOdbcConn)

            ' Count
            With dgvPdc
                n = 0

                For i = 0 To .Rows.Count - 1
                    dgvRow = .Rows(i)

                    If Val(.Rows(i).DataBoundItem("Finone Id").ToString) > 0 Then
                        n += 1
                    Else
                        dgvRow.DefaultCellStyle.BackColor = Color.IndianRed
                    End If
                Next i

                lblPdcCount.Text = "Total : " & .RowCount & " / Matched With Finone : " & n.ToString
            End With

            With dgvFinone
                lnPdc = 0
                lnSpdc = 0
                lnEcs = 0

                For i = 0 To .Rows.Count - 1
                    dgvRow = .Rows(i)

                    If Val(.Rows(i).DataBoundItem("Pdc Id").ToString) > 0 Then
                        lnPdc += 1
                    ElseIf Val(.Rows(i).DataBoundItem("Spdc Id").ToString) > 0 Then
                        lnSpdc += 1
                    ElseIf Val(.Rows(i).DataBoundItem("Ecs Id").ToString) > 0 Then
                        lnEcs += 1
                    Else
                        dgvRow.DefaultCellStyle.BackColor = Color.IndianRed
                    End If
                Next i

                lblFinCount.Text = "Total : " & .RowCount & "/Pdc : " & lnPdc.ToString & "/Spdc : " & lnSpdc.ToString & "/Ecs : " & lnEcs.ToString
            End With

            With dgvSpdc
                n = 0

                For i = 0 To .Rows.Count - 1
                    dgvRow = .Rows(i)

                    If Val(.Rows(i).DataBoundItem("Finone Id").ToString) > 0 Then
                        n += 1
                    Else
                        dgvRow.DefaultCellStyle.BackColor = Color.IndianRed
                    End If
                Next i

                lblSpdcCount.Text = "Tot:" & .RowCount & "/Matched:" & n.ToString
            End With

            With dgvEcsEmi
                n = 0

                For i = 0 To .Rows.Count - 1
                    dgvRow = .Rows(i)

                    If Val(.Rows(i).DataBoundItem("Finone Id").ToString) > 0 Then
                        n += 1
                    Else
                        dgvRow.DefaultCellStyle.BackColor = Color.IndianRed
                    End If
                Next i

                lblEcsEmiCount.Text = "Tot:" & .RowCount & "/Matched:" & n.ToString
            End With

            ' Sorting Remove
            With dgvPdc
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvSpdc
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvSpdcHeader
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvEcsEmi
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvFinone
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtShortAgreementNo.Text = ""
        txtPktNo.Text = ""

        dgvSpdc.DataSource = Nothing
        dgvSpdcHeader.DataSource = Nothing
        dgvPkt.DataSource = Nothing
        dgvEcsEmi.DataSource = Nothing
        dgvPdc.DataSource = Nothing
        dgvFinone.DataSource = Nothing
        txtPktNo.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmSearchEngine_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtPktNo.Focus()
    End Sub

    Private Sub frmSearchEngine_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If txtPktNo.Text.Trim <> "" Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub frmSearchEngine_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim llHeight As Long
        Dim llWidth As Long
        Dim llTop As Long

        llHeight = Math.Abs(Me.Height - (grpMain.Top + grpMain.Height) - 16)
        llHeight = (llHeight - 48 - (lblPacket.Height * 4) - 6 * 4) \ 4

        llWidth = Me.Width - 30

        llTop = grpMain.Top + grpMain.Height + 6

        lblPacket.Left = grpMain.Left
        lblPacket.Top = llTop
        llTop = llTop + lblPacket.Height + 6

        dgvPkt.Height = llHeight
        dgvPkt.Width = llWidth
        dgvPkt.Left = grpMain.Left
        dgvPkt.Top = llTop
        llTop = llTop + llHeight + 6

        lblPdc.Left = grpMain.Left
        lblPdc.Top = llTop

        lblPdcCount.Top = lblPdc.Top
        lblPdcCount.Left = lblPdc.Left + lblPdc.Width + 12

        llTop = llTop + lblPacket.Height + 6

        dgvPdc.Height = llHeight
        dgvPdc.Left = grpMain.Left
        dgvPdc.Top = llTop
        dgvPdc.Width = llWidth

        llTop = llTop + llHeight + 6

        lblFin.Top = llTop
        lblFin.Left = lblPacket.Left

        lblFinCount.Top = lblFin.Top
        lblFinCount.Left = lblFin.Left + lblFin.Width + 12

        llTop = llTop + lblPacket.Height + 6

        dgvFinone.Height = llHeight
        dgvFinone.Left = grpMain.Left
        dgvFinone.Top = llTop
        dgvFinone.Width = dgvPkt.Width

        llTop = llTop + llHeight + 6
        llWidth = (llWidth - 6) \ 3

        lblSpdcHeader.Left = grpMain.Left
        lblSpdcHeader.Top = llTop

        lblEcsEmi.Left = llWidth + 10
        lblEcsEmi.Top = llTop

        lblEcsEmiCount.Top = lblEcsEmi.Top
        lblEcsEmiCount.Left = lblEcsEmi.Left + lblEcsEmi.Width + 12

        lblSpdc.Left = llWidth * 2 + 16
        lblSpdc.Top = llTop

        lblSpdcCount.Top = lblSpdc.Top
        lblSpdcCount.Left = lblSpdc.Left + lblSpdc.Width + 12

        llTop = llTop + lblPacket.Height + 6

        dgvSpdcHeader.Height = llHeight
        dgvSpdcHeader.Left = grpMain.Left
        dgvSpdcHeader.Top = llTop
        dgvSpdcHeader.Width = llWidth

        dgvEcsEmi.Height = llHeight
        dgvEcsEmi.Left = dgvSpdcHeader.Left + dgvSpdcHeader.Width + 2
        dgvEcsEmi.Top = llTop
        dgvEcsEmi.Width = dgvSpdcHeader.Width

        dgvSpdc.Height = llHeight
        dgvSpdc.Top = llTop
        dgvSpdc.Left = dgvEcsEmi.Left + dgvEcsEmi.Width + 2
        dgvSpdc.Width = dgvSpdcHeader.Width

        chkDisc.Top = lblPacket.Top
        chkDisc.Left = grpMain.Left + btnSearch.Left
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGViewXML(dgvPkt, gsReportPath & "Report.xls", "Packet", True, True)
            PrintDGViewXML(dgvPdc, gsReportPath & "Report.xls", "PDC", False, True)
            PrintDGViewXML(dgvSpdcHeader, gsReportPath & "Report.xls", "SPDC Header", False, True)
            PrintDGViewXML(dgvSpdc, gsReportPath & "Report.xls", "SPDC", False, True)
            PrintDGViewXML(dgvEcsEmi, gsReportPath & "Report.xls", "ECS EMI", False, True)
            PrintDGViewXML(dgvFinone, gsReportPath & "Report.xls", "Finone", False, False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPdcCount.Click

    End Sub

    Private Sub chkDisc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisc.CheckedChanged

    End Sub

    Private Sub cboAgreementNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAgreementNo.GotFocus
        Dim lsSql As String

        If txtShortAgreementNo.Text <> "" Then
            lsSql = ""
            lsSql &= " select * from chola_mst_tagreement "
            lsSql &= " where shortagreement_no = '" & txtShortAgreementNo.Text & "' "

            Call gpBindCombo(lsSql, "agreement_no", "agreement_gid", cboAgreementNo, gOdbcConn)

            If cboAgreementNo.Items.Count = 1 Then
                cboAgreementNo.SelectedIndex = 0
            Else
                cboAgreementNo.SelectedIndex = -1
                cboAgreementNo.Text = ""
            End If
        End If
    End Sub

    Private Sub cboAgreementNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgreementNo.SelectedIndexChanged

    End Sub
End Class