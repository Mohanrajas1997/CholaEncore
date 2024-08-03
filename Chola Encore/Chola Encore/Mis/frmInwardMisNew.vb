Public Class frmInwardMisNew
    Dim lssql As String
    Dim objdt As New DataTable
    Dim mbNewColVisible As Boolean = False

    Private Sub frminwardMIS_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frminwardMIS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frminwardMIS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frminwardMIS_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()
        Dim lsSql As String
        Dim objdt As DataTable

        btnrefresh.Enabled = False

        lsSql = ""
        lsSql &= " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Date',count(*) as 'Hands off',"

        If mbNewColVisible = True Then
            lsSql &= " sum(inward_pdc) as 'Inward Pdc',"
            lsSql &= " sum(inward_spdc) as 'Inward Spdc',"
            lsSql &= " sum(if(inward_mandate>0,1,0)) as 'Inward Mandate',"
            lsSql &= " sum(if(inward_packet_gid = 0,inward_pdc,0)) as 'Inward Pdc (Packet Not Received)',"
            lsSql &= " sum(if(inward_packet_gid = 0,inward_spdc,0)) as 'Inward Spdc (Packet Not Received)',"
            lsSql &= " sum(if(inward_packet_gid = 0,if(inward_mandate>0,1,0),0)) as 'Inward Mandate (Packet Not Received)',"
        End If

        lsSql &= " sum(if(inward_packet_gid > 0,1,0)) as 'Inward Recd',"
        lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " > 0,1,0)) as 'Not Recd',"
        lsSql &= " sum(if(inward_status & " & GCCOMBINED & " > 0,1,0)) as 'Combined',"
        lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " = 0 and inward_packet_gid = 0 and inward_status & " & GCCOMBINED & " = 0,1,0)) as 'Pending Inward',"
        'lsSql &= " sum(if(ifnull(packet_status,0) & " & GCPKTAUTHFINONE & " > 0,1,0)) as 'Finone Auth',"
        lsSql &= " sum(if(ifnull(packet_status,0) & " & GCREJECTENTRY & " > 0,1,0)) as 'Rejected',"
        'lsSql &= " sum(if(ifnull(packet_status,0) & " & (GCREJECTENTRY Or GCPKTAUTHFINONE) & " = 0 and inward_packet_gid > 0,1,0)) as 'Finone Auth Pending',"
        lsSql &= " sum(if(ifnull(packet_status,0) & " & GCPACKETCHEQUEENTRY & " > 0,1,0)) as 'Chq DE Completed',"
        lsSql &= " sum(if(ifnull(packet_status,0) & " & (GCPACKETCHEQUEENTRY Or GCREJECTENTRY Or GCPKTREPROCESS) & " = 0 and inward_packet_gid > 0,1,0)) as 'Chq DE pending',"
        lsSql &= " sum(if(ifnull(packet_status,0) & " & GCPACKETCHEQUEENTRY & " = 0 and packet_status & " & GCPKTREPROCESS & " > 0,1,0)) as 'Reject DE pending' "

        If mbNewColVisible = True Then
            lsSql &= " ,sum((select count(*) from chola_trn_tpdcentry where chq_packet_gid = inward_packet_gid)) as 'PDC Entered',"
            lsSql &= " sum((select count(*) from chola_trn_tspdcchqentry where chqentry_packet_gid = inward_packet_gid)) as 'SPDC Entered' "
        End If

        lsSql &= " from chola_trn_tinward "
        lsSql &= " left join chola_trn_tpacket on packet_gid=inward_packet_gid "
        lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        lsSql &= " group by inward_receiveddate"

        objdt = GetDataTable(lsSql)
        objdt.Rows.Add()
        objdt.Rows(objdt.Rows.Count - 1)(0) = "Total:"
        For i As Integer = 0 To objdt.Rows.Count - 2
            For j As Integer = 1 To objdt.Columns.Count - 1
                objdt.Rows(objdt.Rows.Count - 1)(j) = Val(objdt.Rows(objdt.Rows.Count - 1)(j).ToString) + Val(objdt.Rows(i)(j).ToString)
            Next
        Next

        dgvsummary.DataSource = objdt

        btnrefresh.Enabled = True
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub btnDay2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDay2.Click
        Dim lsSql As String
        Dim lnResult As Long
        Dim lnRptId As Long
        Dim lnPktId As Long
        Dim lnRptInwardId As Long

        Dim lnTotCnt As Long
        Dim lnPdcCnt As Long
        Dim lnPdcMapped As Long
        Dim lnSpdcCnt As Long
        Dim lnSpdcMapped As Long
        Dim lnEmiCnt As Long
        Dim lnEmiMapped As Long
        Dim lnTypeDiscCnt As Long

        Dim objdt As DataTable
        Dim lsFileName As String

        Dim lsDayZero As String = ""
        Dim lsDayOne As String = ""
        Dim lsDayTwo As String = ""
        Dim lsPktStatus As String = ""
        Dim lsInwardStatus As String = ""
        Dim n As Integer = 0
        Dim ds As New DataSet

        ' day check
        n = DateDiff(DateInterval.Day, dtpfrom.Value, dtpto.Value) + 1

        If n > 10 Then
            If MsgBox("You are trying to take " & n & " day(s) data ! This will slow entire process ! Are you sure to continue ?" _
                      , MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        lsSql = ""
        lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
        lsSql &= " where status_level = 'Packet' "
        lsSql &= " and status_deleteflag = 'N' "
        lsSql &= " order by status_flag "

        lsPktStatus = gfExecuteScalar(lsSql, gOdbcConn)
        lsPktStatus = "'" & lsPktStatus.Replace(",", "','") & "'"

        lsInwardStatus = "'','Received','Not Received','Combined'"

        Me.Cursor = Cursors.WaitCursor

        lsFileName = "Day2MIS" & Format(Now, "ddMMyyyyhhmmss") & ".xls"
        Dim objxlexport As New XMLExport(gsReportPath & lsFileName)

        With objxlexport
            ' Day Two By Inward Date
            lsSql = "insert into chola_rpt_treport (entry_date,entry_by) values (sysdate(),'" & gUserName & "')"
            lnResult = gfInsertQry(lsSql, gOdbcConn)

            lnRptId = Val(gfExecuteScalar("select max(rpt_gid) from chola_rpt_treport", gOdbcConn))

            ' insert in packet report table
            lsSql = ""
            lsSql &= " insert into chola_rpt_tinward (rpt_gid,inward_gid,inward_date,inward_pdc_mode,inward_pdc_cnt,inward_spdc_cnt,inward_mandate_cnt,"
            lsSql &= " inward_status,gnsa_refno,packet_gid,agreement_no,shortagreement_no,agreement_gid,"
            lsSql &= " cust_name,auth_date,pkt_pdc_mode,pkt_mandate_cnt,packet_status) "
            lsSql &= " SELECT " & lnRptId & ","
            lsSql &= " inward_gid,inward_receiveddate,inward_paymode,inward_pdc,inward_spdc,inward_mandate,inward_status,"
            lsSql &= " packet_gnsarefno,ifnull(packet_gid,0),inward_agreementno,inward_shortagreementno,"
            lsSql &= " ifnull(packet_agreement_gid,0),inward_customername,inward_userauthdate,packet_mode,"
            lsSql &= " ifnull(spdcentry_ecsmandatecount,0) as mandate_cnt,ifnull(packet_status,0) "
            lsSql &= " FROM chola_trn_tinward"
            lsSql &= " left join chola_trn_tpacket on packet_gid = inward_packet_gid"
            lsSql &= " left join chola_trn_tspdcentry on spdcentry_packet_gid = packet_gid"
            lsSql &= " where true"
            lsSql &= " and inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " group by inward_gid,inward_paymode,inward_pdc,inward_spdc,inward_mandate,inward_status,"
            lsSql &= " packet_gnsarefno,packet_gid,inward_agreementno,inward_shortagreementno,"
            lsSql &= " packet_agreement_gid,inward_customername,inward_userauthdate,packet_mode,spdcentry_ecsmandatecount"

            lnResult = gfInsertQry(lsSql, gOdbcConn)

            lsSql = ""
            lsSql &= " select rptinward_gid,packet_gid from chola_rpt_tinward "
            lsSql &= " where rpt_gid = " & lnRptId & " "
            lsSql &= " and packet_gid > 0 "

            Call gpDataSet(lsSql, "rpt", gOdbcConn, ds)

            For i = 0 To ds.Tables("rpt").Rows.Count - 1
                lnPktId = ds.Tables("rpt").Rows(i).Item("packet_gid")
                lnRptInwardId = ds.Tables("rpt").Rows(i).Item("rptinward_gid")

                lnTotCnt = 0
                lnPdcCnt = 0
                lnPdcMapped = 0
                lnSpdcCnt = 0
                lnSpdcMapped = 0
                lnEmiCnt = 0
                lnEmiMapped = 0
                lnTypeDiscCnt = 0

                ' pdc
                lsSql = ""
                lsSql &= " select count(*) as cnt,sum(if(chq_pdc_gid > 0,1,0)) as mapped from chola_trn_tpdcentry "
                lsSql &= " where chq_packet_gid = " & lnPktId & " "
                lsSql &= " and chq_type <> " & GCEXTERNALSECURITY & " "

                Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                If ds.Tables("cnt").Rows.Count > 0 Then
                    lnPdcCnt = Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                    lnPdcMapped = Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                End If

                ds.Tables("cnt").Rows.Clear()

                lsSql = ""
                lsSql &= " select count(*) as cnt,sum(if(chq_pdc_gid > 0,1,0)) as mapped from chola_trn_tpdcentry "
                lsSql &= " where chq_packet_gid = " & lnPktId & " "
                lsSql &= " and chq_type = " & GCEXTERNALSECURITY & " "

                Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                If ds.Tables("cnt").Rows.Count > 0 Then
                    lnSpdcCnt += Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                    lnSpdcMapped += Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                End If

                ds.Tables("cnt").Rows.Clear()

                ' spdc
                lsSql = ""
                lsSql &= " select count(*) as cnt,sum(if(chqentry_pdc_gid > 0,1,0)) as mapped from chola_trn_tspdcchqentry "
                lsSql &= " where chqentry_packet_gid = " & lnPktId & " "

                Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                If ds.Tables("cnt").Rows.Count > 0 Then
                    lnSpdcCnt += Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                    lnSpdcMapped += Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                End If

                ds.Tables("cnt").Rows.Clear()

                ' emi
                lsSql = ""
                lsSql &= " select count(*) as cnt,sum(if(ecsemientry_pdc_gid > 0,1,0)) as mapped from chola_trn_tecsemientry "
                lsSql &= " where ecsemientry_packet_gid = " & lnPktId & " "

                Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                If ds.Tables("cnt").Rows.Count > 0 Then
                    lnEmiCnt = Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                    lnEmiMapped = Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                End If

                ds.Tables("cnt").Rows.Clear()

                ' Disc Count
                lsSql = ""
                lsSql &= " select count(*) from chola_trn_tpdcentry as a "
                lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chq_pdc_gid "
                lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                lsSql &= " and c.new_pdc_type <> 'PDC' "
                lsSql &= " where a.chq_packet_gid = " & lnPktId & " "
                lsSql &= " and a.chq_type <> " & GCEXTERNALSECURITY & " "

                lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                lsSql = ""
                lsSql &= " select count(*) from chola_trn_tpdcentry as a "
                lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chq_pdc_gid "
                lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                lsSql &= " and c.new_pdc_type <> 'SPDC' "
                lsSql &= " where a.chq_packet_gid = " & lnPktId & " "
                lsSql &= " and a.chq_type = " & GCEXTERNALSECURITY & " "

                lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                lsSql = ""
                lsSql &= " select count(*) from chola_trn_tspdcchqentry as a "
                lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chqentry_pdc_gid "
                lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                lsSql &= " and c.new_pdc_type <> 'SPDC' "
                lsSql &= " where a.chqentry_packet_gid = " & lnPktId & " "

                lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                lsSql = ""
                lsSql &= " select count(*) from chola_trn_tecsemientry as a "
                lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.ecsemientry_pdc_gid "
                lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                lsSql &= " and c.new_pdc_type <> 'ECS' "
                lsSql &= " where a.ecsemientry_packet_gid = " & lnPktId & " "

                lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                lnTotCnt = lnPdcCnt + lnSpdcCnt + lnEmiCnt

                If lnTotCnt > 0 Then
                    lsSql = ""
                    lsSql &= " update chola_rpt_tinward set "
                    lsSql &= " pkt_tot_cnt = " & lnTotCnt & ","
                    lsSql &= " pkt_pdc_cnt = " & lnPdcCnt & ","
                    lsSql &= " pkt_pdc_mapped = " & lnPdcMapped & ","
                    lsSql &= " pkt_spdc_cnt = " & lnSpdcCnt & ","
                    lsSql &= " pkt_spdc_mapped = " & lnSpdcMapped & ","
                    lsSql &= " pkt_emi_cnt = " & lnEmiCnt & ","
                    lsSql &= " pkt_emi_mapped = " & lnEmiMapped & ","
                    lsSql &= " pkt_disc_mapped = " & lnTypeDiscCnt & " "
                    lsSql &= " where rptinward_gid = " & lnRptInwardId

                    lnResult = gfInsertQry(lsSql, gOdbcConn)
                End If
            Next i

            ds.Tables("rpt").Rows.Clear()

            ' insert in finone report table
            lsSql = ""
            lsSql &= " insert into chola_rpt_tfinone (rpt_gid,inward_gid,packet_gid,short_agreementno,agreement_gid,"
            lsSql &= " auth_date,pdc_mode,tot_cnt,pdc_cnt,spdc_cnt,emi_cnt,pdc_mapped,spdc_mapped,emi_mapped) "
            lsSql &= " select " & lnRptId & ",ifnull(h.head_inward_gid,0),ifnull(h.head_packet_gid,0),"
            lsSql &= " h.head_shortagreementno,"
            lsSql &= " ifnull(h.head_agreement_gid,0),"
            lsSql &= " h.head_systemauthdate,"
            lsSql &= " m.new_pdc_mode,"
            lsSql &= " count(*) as tot_cnt,"
            lsSql &= " ifnull(sum(if(t.new_pdc_type = 'PDC',1,0)),0) as pdc_cnt,"
            lsSql &= " ifnull(sum(if(t.new_pdc_type = 'SPDC',1,0)),0) as spdc_cnt,"
            lsSql &= " ifnull(sum(if(t.new_pdc_type = 'ECS',1,0)),0) as emi_cnt,"
            lsSql &= " ifnull(sum(if(entry_gid > 0,1,0)),0) as pdc_mapped,"
            lsSql &= " ifnull(sum(if(pdc_spdcentry_gid > 0,1,0)),0) as spdc_mapped,"
            lsSql &= " ifnull(sum(if(pdc_ecsentry_gid > 0,1,0)),0) as emi_mapped "
            lsSql &= " from chola_rpt_tinward as r "
            lsSql &= " inner join chola_trn_tpdcfilehead as h on h.head_packet_gid = r.packet_gid and h.head_agreement_gid = r.agreement_gid "
            lsSql &= " inner join chola_trn_tpdcfile as p on p.file_mst_gid = h.head_file_gid and p.pdc_parentcontractno = h.head_agreementno "
            lsSql &= " left join chola_mst_tpdctype as t on t.pdc_type = p.pdc_type "
            lsSql &= " and p.chq_rec_slno = 1 "
            lsSql &= " left join chola_mst_tpdcmode as m on m.pdc_mode = h.head_mode "
            lsSql &= " where r.rpt_gid = " & lnRptId & " "
            lsSql &= " and r.packet_gid > 0 "
            lsSql &= " group by h.head_inward_gid,h.head_packet_gid,h.head_shortagreementno,h.head_agreement_gid,"
            lsSql &= " h.head_systemauthdate, h.head_mode"

            lnResult = gfInsertQry(lsSql, gOdbcConn)

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(i.inward_date,'%d-%m-%Y') as 'Received Date',"
            lsSql &= " i.agreement_no as 'Agreement No',"
            lsSql &= " i.shortagreement_no as 'Short Agreement No',"
            lsSql &= " i.cust_name as 'Customer Name',"
            lsSql &= " i.inward_pdc_mode as 'PDC Mode',"
            lsSql &= " i.gnsa_refno as 'GNSAREF#',"

            lsSql &= " case "
            lsSql &= " when i.packet_gid > 0 then 'Received' "
            lsSql &= " when i.inward_status & " & GCCOMBINED & " > 0 then 'Combined' "
            lsSql &= " when i.inward_status & " & GCNOTRECEIVED & " > 0 then 'Not Received' "
            lsSql &= " else 'Pending' end as 'Inward Status',"

            lsSql &= " case "
            lsSql &= " when i.packet_gid = 0 then '' "
            lsSql &= " when i.packet_status & " & GCREJECTENTRY & " > 0 then 'Rejected' "
            lsSql &= " when i.packet_status & " & GCPACKETCHEQUEENTRY & " > 0 then 'DE Completed' "
            lsSql &= " when i.packet_status & " & (GCREJECTENTRY Or GCPACKETCHEQUEENTRY) & " = 0 then 'DE Pending' "
            lsSql &= " else '' end as 'Packet Status',"

            lsSql &= " case "
            lsSql &= " when i.packet_gid = 0 or (i.packet_status & " & GCREJECTENTRY & " > 0 and i.packet_status & " & GCPKTAUTHFINONE & " = 0) then '' "
            lsSql &= " when i.packet_status & " & GCPKTAUTHFINONE & " > 0 then 'Authorized' "
            lsSql &= " else 'Pending' end as 'Finone Status',"

            lsSql &= " i.inward_pdc_cnt as 'Inward PDC',"
            lsSql &= " i.inward_spdc_cnt as 'Inward SPDC',"
            lsSql &= " i.inward_mandate_cnt as 'Inward Mandate',"
            lsSql &= " f.pdc_cnt as 'Finone PDC',"
            lsSql &= " f.spdc_cnt as 'Finone SPDC',"
            lsSql &= " f.emi_cnt as 'Finone ECS',"
            lsSql &= " i.pkt_pdc_cnt as 'GNSA PDC',"
            lsSql &= " i.pkt_spdc_cnt as 'GNSA SPDC',"
            lsSql &= " i.pkt_emi_cnt as 'GNSA ECS',"
            lsSql &= " i.pkt_mandate_cnt as 'GNSA Mandate',"
            lsSql &= " if(i.pkt_tot_cnt= 0,'',"
            lsSql &= " if(ifnull(f.pdc_cnt,0) = i.pkt_pdc_cnt and i.inward_pdc_cnt = i.pkt_pdc_cnt and ifnull(f.spdc_cnt,0) = i.pkt_spdc_cnt and i.inward_spdc_cnt = i.pkt_spdc_cnt and ifnull(f.emi_cnt,0) = i.pkt_emi_cnt,'No','Yes')) as 'Count Mismatch',"

            lsSql &= " case "
            lsSql &= " when ifnull(f.packet_gid,0) = 0 then '' "
            lsSql &= " when i.pkt_tot_cnt = 0 then '' "
            lsSql &= " when i.pkt_disc_mapped > 0 then 'Yes' "
            lsSql &= " when i.pkt_pdc_mode <> ifnull(f.pdc_mode,'') then 'Yes' "
            lsSql &= " when i.pkt_tot_cnt = i.pkt_pdc_mapped+i.pkt_spdc_mapped+i.pkt_emi_mapped then 'No' "
            lsSql &= " else 'Yes' end as 'Field Mismatch'"

            lsSql &= " from chola_rpt_tinward as i "
            lsSql &= " left join chola_rpt_tfinone as f on f.inward_gid = i.inward_gid and f.rpt_gid = i.rpt_gid "
            lsSql &= " where i.rpt_gid = " & lnRptId & " "

            objdt = GetDataTable(lsSql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Day2")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            lsSql = "delete from chola_rpt_tinward where rpt_gid = " & lnRptId & " "
            lnResult = gfInsertQry(lsSql, gOdbcConn)

            lsSql = "delete from chola_rpt_tfinone where rpt_gid = " & lnRptId & " "
            lnResult = gfInsertQry(lsSql, gOdbcConn)

            .Close()
        End With

        Me.Cursor = Cursors.Default
        MsgBox("Report Generated..!", MsgBoxStyle.Information, gProjectName)
        System.Diagnostics.Process.Start(gsReportPath & lsFileName)
    End Sub
    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        Dim lsqry As String
        Dim lsdate As String
        Dim lsFrom As String
        Dim lsTo As String
        Dim lscolumn As String

        If e.RowIndex < 0 Then
            Exit Sub
        End If



        lsdate = dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
        lscolumn = dgvsummary.Columns(dgvsummary.CurrentCell.ColumnIndex).Name

        If Not IsDate(lsdate) Then
            lsFrom = Format(dtpfrom.Value, "yyyy-MM-dd")
            lsTo = Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd")
        Else
            lsFrom = Format(CDate(lsdate), "yyyy-MM-dd")
            lsTo = Format(DateAdd(DateInterval.Day, 1, CDate(lsdate)), "yyyy-MM-dd")
        End If

        Select Case lscolumn
            Case "Hands off"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
            Case "Inward Recd"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and inward_packet_gid > 0 "
            Case "Not Recd"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and inward_packet_gid=0 "
                lsqry &= " and inward_status & " & GCNOTRECEIVED & " > 0 "
            Case "Combined"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and inward_packet_gid=0 "
                lsqry &= " and inward_status & " & GCCOMBINED & " > 0 "
            Case "Pending Inward"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and inward_packet_gid=0 "
                lsqry &= " and inward_status & " & GCNOTRECEIVED & " = 0 "
                lsqry &= " and inward_status & " & GCCOMBINED & " = 0 "
            Case "Finone Auth"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and packet_status & " & GCPKTAUTHFINONE & " > 0 "
            Case "Rejected"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',packet_remarks as 'Remarks' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and packet_status & " & GCREJECTENTRY & " > 0 "
            Case "Finone Auth Pending"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and packet_status & " & GCREJECTENTRY & " = 0 "
                lsqry &= " and packet_status & " & GCPKTAUTHFINONE & " = 0 "
                lsqry &= " and inward_status & " & GCRECEIVED & " > 0 "
            Case "Chq DE Completed"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
            Case "Chq DE pending"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and packet_status & " & (GCPACKETCHEQUEENTRY Or GCREJECTENTRY Or GCPKTREPROCESS) & " = 0 "
            Case "Reject DE pending"
                lsqry = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where inward_receiveddate >= '" & lsFrom & "'"
                lsqry &= " and inward_receiveddate < '" & lsTo & "'"
                lsqry &= " and packet_status & " & GCPACKETCHEQUEENTRY & " = 0 "
                lsqry &= " and packet_status & " & GCPKTREPROCESS & " > 0 "
            Case Else
                lsqry = ""
        End Select

        If lsqry <> "" Then
            QuickView(gOdbcConn, lsqry)
        End If
    End Sub

    Private Sub btnexportmis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportmis.Click
        Dim lsSql As String
        Dim lnResult As Long
        Dim lnRptId As Long
        Dim lnPktId As Long
        Dim lnRptInwardId As Long

        Dim lnTotCnt As Long
        Dim lnPdcCnt As Long
        Dim lnPdcMapped As Long
        Dim lnSpdcCnt As Long
        Dim lnSpdcMapped As Long
        Dim lnEmiCnt As Long
        Dim lnEmiMapped As Long
        Dim lnTypeDiscCnt As Long

        Dim objdt As DataTable
        Dim lsFileName As String

        Dim lsDayZero As String = ""
        Dim lsDayOne As String = ""
        Dim lsDayTwo As String = ""
        Dim lsPktStatus As String = ""
        Dim lsInwardStatus As String = ""
        Dim n As Integer = 0
        Dim ds As New DataSet


        lsSql = ""
        lsSql &= " select group_concat(status_desc) from chola_mst_tstatus "
        lsSql &= " where status_level = 'Packet' "
        lsSql &= " and status_deleteflag = 'N' "
        lsSql &= " order by status_flag "

        lsPktStatus = gfExecuteScalar(lsSql, gOdbcConn)
        lsPktStatus = "'" & lsPktStatus.Replace(",", "','") & "'"

        lsInwardStatus = "'','Received','Not Received','Combined'"

        Me.Cursor = Cursors.WaitCursor

        lsFileName = "Inward MIS" & Format(Now, "ddMMyyyyhhmmss") & ".xls"
        Dim objxlexport As New XMLExport(gsReportPath & lsFileName)

        With objxlexport
            'Summary Sheet
            lsSql = ""
            lsSql &= " select date_format(inward_receiveddate,'%d-%b-%Y') as 'Date',count(*) as 'Hands off',"
            lsSql &= " sum(if(inward_packet_gid > 0,1,0)) as 'Inward Recd',"
            lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " > 0,1,0)) as 'Not Recd',"
            lsSql &= " sum(if(inward_status & " & GCCOMBINED & " > 0,1,0)) as 'Combined',"
            lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " = 0 and inward_packet_gid = 0 and inward_status & " & GCCOMBINED & " = 0,1,0)) as 'Pending Inward',"
            lsSql &= " sum(if(ifnull(packet_status,0) & " & GCPKTAUTHFINONE & " > 0,1,0)) as 'Finone Auth',"
            lsSql &= " sum(if(ifnull(packet_status,0) & " & GCREJECTENTRY & " > 0,1,0)) as 'Rejected',"
            lsSql &= " sum(if(ifnull(packet_status,0) & " & (GCREJECTENTRY Or GCPKTAUTHFINONE) & " = 0 and inward_packet_gid > 0,1,0)) as 'Finone Auth Pending',"
            lsSql &= " sum(if(ifnull(packet_status,0) & " & GCPACKETCHEQUEENTRY & " > 0,1,0)) as 'Chq DE Completed',"
            lsSql &= " sum(if(ifnull(packet_status,0) & " & (GCPACKETCHEQUEENTRY Or GCREJECTENTRY Or GCPKTREPROCESS) & " = 0 and inward_packet_gid > 0,1,0)) as 'Chq DE pending',"
            lsSql &= " sum(if(ifnull(packet_status,0) & " & GCPACKETCHEQUEENTRY & " = 0 and packet_status & " & GCPKTREPROCESS & " > 0,1,0)) as 'Reject DE pending' "
            lsSql &= " from chola_trn_tinward "
            lsSql &= " left join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " group by inward_receiveddate"
            lsSql &= " having "
            lsSql &= " (`Pending Inward` <> 0 or `Finone Auth Pending` <> 0 or `Chq DE pending` <> 0 or `Rejected` <> 0)"

            objdt = GetDataTable(lsSql)

            For i As Integer = objdt.Rows.Count - 1 To 0 Step -1
                Select Case n
                    Case 0
                        lsDayZero = Format(CDate(objdt.Rows(i).Item("Date").ToString), "yyyy-MM-dd")
                    Case 1
                        lsDayOne = Format(CDate(objdt.Rows(i).Item("Date").ToString), "yyyy-MM-dd")
                    Case 2
                        lsDayTwo = Format(CDate(objdt.Rows(i).Item("Date").ToString), "yyyy-MM-dd")
                    Case Else
                        Exit For
                End Select

                n += 1
            Next i

            objdt.Rows.Add()
            objdt.Rows(objdt.Rows.Count - 1)(0) = "Total:"
            For i As Integer = 0 To objdt.Rows.Count - 2
                For j As Integer = 1 To objdt.Columns.Count - 1
                    objdt.Rows(objdt.Rows.Count - 1)(j) = Val(objdt.Rows(objdt.Rows.Count - 1)(j).ToString) + Val(objdt.Rows(i)(j).ToString)
                Next
            Next
            If objdt.Rows.Count > 0 Then
                .NewSheet("Summary")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            If lsDayZero <> "" Then
                'Day Zero
                lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',"

                lsSql &= " case "
                lsSql &= " when inward_packet_gid > 0 then 'Received' "
                lsSql &= " when inward_status & " & GCCOMBINED & " > 0 then 'Combined' "
                lsSql &= " when inward_status & " & GCNOTRECEIVED & " > 0 then 'Not Received' "
                lsSql &= " else 'Pending' end as 'Inward Status',"

                lsSql &= " case "
                lsSql &= " when ifnull(packet_gid,0) = 0 then '' "
                lsSql &= " when packet_status & " & GCREJECTENTRY & " > 0 then 'Rejected' "
                lsSql &= " when packet_status & " & GCPACKETCHEQUEENTRY & " > 0 then 'DE Completed' "
                lsSql &= " when packet_status & " & (GCREJECTENTRY Or GCPACKETCHEQUEENTRY) & " = 0 then 'DE Pending' "
                lsSql &= " else '' end as 'Packet Status' "

                lsSql &= " from chola_trn_tinward"
                lsSql &= " left join chola_trn_tpacket on packet_gid = inward_packet_gid "
                lsSql &= " where inward_receiveddate >= '" & lsDayZero & "'"
                lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lsDayZero)), "yyyy-MM-dd") & "'"
                objdt = GetDataTable(lsSql)

                If objdt.Rows.Count > 0 Then
                    .NewSheet("Day 0 (Inward for " & Format(CDate(lsDayZero), "dd-MM") & ")")
                    .BeginRow()
                    For i As Integer = 0 To objdt.Columns.Count - 1
                        .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                    Next
                    .EndRow()
                    For i As Integer = 0 To objdt.Rows.Count - 1
                        .BeginRow()
                        For j As Integer = 0 To objdt.Columns.Count - 1
                            .AddCell(objdt.Rows(i)(j).ToString)
                        Next
                        .EndRow()
                    Next
                    .EndSheet()
                End If
            End If

            If lsDayOne <> "" Then
                'Day One
                lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',"

                lsSql &= " case "
                lsSql &= " when inward_packet_gid > 0 then 'Received' "
                lsSql &= " when inward_status & " & GCCOMBINED & " > 0 then 'Combined' "
                lsSql &= " when inward_status & " & GCNOTRECEIVED & " > 0 then 'Not Received' "
                lsSql &= " else 'Pending' end as 'Inward Status',"

                lsSql &= " case "
                lsSql &= " when ifnull(packet_gid,0) = 0 then '' "
                lsSql &= " when packet_status & " & GCREJECTENTRY & " > 0 then 'Rejected' "
                lsSql &= " when packet_status & " & GCPACKETCHEQUEENTRY & " > 0 then 'DE Completed' "
                lsSql &= " when packet_status & " & (GCREJECTENTRY Or GCPACKETCHEQUEENTRY) & " = 0 then 'DE Pending' "
                lsSql &= " else '' end as 'Packet Status' "

                lsSql &= " from chola_trn_tinward"
                lsSql &= " left join chola_trn_tpacket on packet_gid = inward_packet_gid "
                lsSql &= " where inward_receiveddate >= '" & lsDayOne & "'"
                lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lsDayOne)), "yyyy-MM-dd") & "'"
                objdt = GetDataTable(lsSql)

                If objdt.Rows.Count > 0 Then
                    .NewSheet("Day 1 (Inward for " & Format(CDate(lsDayOne), "dd-MM") & ")")
                    .BeginRow()
                    For i As Integer = 0 To objdt.Columns.Count - 1
                        .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                    Next
                    .EndRow()
                    For i As Integer = 0 To objdt.Rows.Count - 1
                        .BeginRow()
                        For j As Integer = 0 To objdt.Columns.Count - 1
                            .AddCell(objdt.Rows(i)(j).ToString)
                        Next
                        .EndRow()
                    Next
                    .EndSheet()
                End If
            End If

            If lsDayTwo <> "" Then
                ' Day Two By Inward Date
                lsSql = "insert into chola_rpt_treport (entry_date,entry_by) values (sysdate(),'" & gUserName & "')"
                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lnRptId = Val(gfExecuteScalar("select max(rpt_gid) from chola_rpt_treport", gOdbcConn))

                ' insert in packet report table
                lsSql = ""
                lsSql &= " insert into chola_rpt_tinward (rpt_gid,inward_gid,inward_date,inward_pdc_mode,inward_pdc_cnt,inward_spdc_cnt,inward_mandate_cnt,"
                lsSql &= " inward_status,gnsa_refno,packet_gid,agreement_no,shortagreement_no,agreement_gid,"
                lsSql &= " cust_name,auth_date,pkt_pdc_mode,pkt_mandate_cnt,packet_status) "
                lsSql &= " SELECT " & lnRptId & ","
                lsSql &= " inward_gid,inward_receiveddate,inward_paymode,inward_pdc,inward_spdc,inward_mandate,inward_status,"
                lsSql &= " packet_gnsarefno,ifnull(packet_gid,0),inward_agreementno,inward_shortagreementno,"
                lsSql &= " ifnull(packet_agreement_gid,0),inward_customername,inward_userauthdate,packet_mode,"
                lsSql &= " ifnull(spdcentry_ecsmandatecount,0) as mandate_cnt,ifnull(packet_status,0) "
                lsSql &= " FROM chola_trn_tinward"
                lsSql &= " left join chola_trn_tpacket on packet_gid = inward_packet_gid"
                lsSql &= " left join chola_trn_tspdcentry on spdcentry_packet_gid = packet_gid"
                lsSql &= " where true"
                lsSql &= " and inward_receiveddate >= '" & lsDayTwo & "'"
                lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lsDayTwo)), "yyyy-MM-dd") & "'"
                lsSql &= " group by inward_gid,inward_paymode,inward_pdc,inward_spdc,inward_mandate,inward_status,"
                lsSql &= " packet_gnsarefno,packet_gid,inward_agreementno,inward_shortagreementno,"
                lsSql &= " packet_agreement_gid,inward_customername,inward_userauthdate,packet_mode,spdcentry_ecsmandatecount"

                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lsSql = ""
                lsSql &= " select rptinward_gid,packet_gid from chola_rpt_tinward "
                lsSql &= " where rpt_gid = " & lnRptId & " "
                lsSql &= " and packet_gid > 0 "

                Call gpDataSet(lsSql, "rpt", gOdbcConn, ds)

                For i = 0 To ds.Tables("rpt").Rows.Count - 1
                    lnPktId = ds.Tables("rpt").Rows(i).Item("packet_gid")
                    lnRptInwardId = ds.Tables("rpt").Rows(i).Item("rptinward_gid")

                    lnTotCnt = 0
                    lnPdcCnt = 0
                    lnPdcMapped = 0
                    lnSpdcCnt = 0
                    lnSpdcMapped = 0
                    lnEmiCnt = 0
                    lnEmiMapped = 0
                    lnTypeDiscCnt = 0

                    ' pdc
                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(chq_pdc_gid > 0,1,0)) as mapped from chola_trn_tpdcentry "
                    lsSql &= " where chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and chq_type <> " & GCEXTERNALSECURITY & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnPdcCnt = Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnPdcMapped = Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(chq_pdc_gid > 0,1,0)) as mapped from chola_trn_tpdcentry "
                    lsSql &= " where chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and chq_type = " & GCEXTERNALSECURITY & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnSpdcCnt += Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnSpdcMapped += Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    ' spdc
                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(chqentry_pdc_gid > 0,1,0)) as mapped from chola_trn_tspdcchqentry "
                    lsSql &= " where chqentry_packet_gid = " & lnPktId & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnSpdcCnt += Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnSpdcMapped += Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    ' emi
                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(ecsemientry_pdc_gid > 0,1,0)) as mapped from chola_trn_tecsemientry "
                    lsSql &= " where ecsemientry_packet_gid = " & lnPktId & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnEmiCnt = Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnEmiMapped = Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    ' Disc Count
                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tpdcentry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chq_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'PDC' "
                    lsSql &= " where a.chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and a.chq_type <> " & GCEXTERNALSECURITY & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tpdcentry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chq_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'SPDC' "
                    lsSql &= " where a.chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and a.chq_type = " & GCEXTERNALSECURITY & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tspdcchqentry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chqentry_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'SPDC' "
                    lsSql &= " where a.chqentry_packet_gid = " & lnPktId & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tecsemientry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.ecsemientry_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'ECS' "
                    lsSql &= " where a.ecsemientry_packet_gid = " & lnPktId & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lnTotCnt = lnPdcCnt + lnSpdcCnt + lnEmiCnt

                    If lnTotCnt > 0 Then
                        lsSql = ""
                        lsSql &= " update chola_rpt_tinward set "
                        lsSql &= " pkt_tot_cnt = " & lnTotCnt & ","
                        lsSql &= " pkt_pdc_cnt = " & lnPdcCnt & ","
                        lsSql &= " pkt_pdc_mapped = " & lnPdcMapped & ","
                        lsSql &= " pkt_spdc_cnt = " & lnSpdcCnt & ","
                        lsSql &= " pkt_spdc_mapped = " & lnSpdcMapped & ","
                        lsSql &= " pkt_emi_cnt = " & lnEmiCnt & ","
                        lsSql &= " pkt_emi_mapped = " & lnEmiMapped & ","
                        lsSql &= " pkt_disc_mapped = " & lnTypeDiscCnt & " "
                        lsSql &= " where rptinward_gid = " & lnRptInwardId

                        lnResult = gfInsertQry(lsSql, gOdbcConn)
                    End If
                Next i

                ds.Tables("rpt").Rows.Clear()

                ' insert in finone report table
                lsSql = ""
                lsSql &= " insert into chola_rpt_tfinone (rpt_gid,inward_gid,packet_gid,short_agreementno,agreement_gid,"
                lsSql &= " auth_date,pdc_mode,tot_cnt,pdc_cnt,spdc_cnt,emi_cnt,pdc_mapped,spdc_mapped,emi_mapped) "
                lsSql &= " select " & lnRptId & ",ifnull(h.head_inward_gid,0),ifnull(h.head_packet_gid,0),"
                lsSql &= " h.head_shortagreementno,"
                lsSql &= " ifnull(h.head_agreement_gid,0),"
                lsSql &= " h.head_systemauthdate,"
                lsSql &= " m.new_pdc_mode,"
                lsSql &= " count(*) as tot_cnt,"
                lsSql &= " ifnull(sum(if(t.new_pdc_type = 'PDC',1,0)),0) as pdc_cnt,"
                lsSql &= " ifnull(sum(if(t.new_pdc_type = 'SPDC',1,0)),0) as spdc_cnt,"
                lsSql &= " ifnull(sum(if(t.new_pdc_type = 'ECS',1,0)),0) as emi_cnt,"
                lsSql &= " ifnull(sum(if(entry_gid > 0,1,0)),0) as pdc_mapped,"
                lsSql &= " ifnull(sum(if(pdc_spdcentry_gid > 0,1,0)),0) as spdc_mapped,"
                lsSql &= " ifnull(sum(if(pdc_ecsentry_gid > 0,1,0)),0) as emi_mapped "
                lsSql &= " from chola_rpt_tinward as r "
                lsSql &= " inner join chola_trn_tpdcfilehead as h on h.head_packet_gid = r.packet_gid and h.head_agreement_gid = r.agreement_gid "
                lsSql &= " inner join chola_trn_tpdcfile as p on p.file_mst_gid = h.head_file_gid and p.pdc_parentcontractno = h.head_agreementno "
                lsSql &= " left join chola_mst_tpdctype as t on t.pdc_type = p.pdc_type "
                lsSql &= " and p.chq_rec_slno = 1 "
                lsSql &= " left join chola_mst_tpdcmode as m on m.pdc_mode = h.head_mode "
                lsSql &= " where r.rpt_gid = " & lnRptId & " "
                lsSql &= " and r.packet_gid > 0 "
                lsSql &= " group by h.head_inward_gid,h.head_packet_gid,h.head_shortagreementno,h.head_agreement_gid,"
                lsSql &= " h.head_systemauthdate, h.head_mode"

                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lsSql = ""
                lsSql &= " select "
                lsSql &= " date_format(i.inward_date,'%d-%m-%Y') as 'Received Date',"
                lsSql &= " i.agreement_no as 'Agreement No',"
                lsSql &= " i.shortagreement_no as 'Short Agreement No',"
                lsSql &= " i.cust_name as 'Customer Name',"
                lsSql &= " i.inward_pdc_mode as 'PDC Mode',"
                lsSql &= " i.gnsa_refno as 'GNSAREF#',"

                lsSql &= " case "
                lsSql &= " when i.packet_gid > 0 then 'Received' "
                lsSql &= " when i.inward_status & " & GCCOMBINED & " > 0 then 'Combined' "
                lsSql &= " when i.inward_status & " & GCNOTRECEIVED & " > 0 then 'Not Received' "
                lsSql &= " else 'Pending' end as 'Inward Status',"

                lsSql &= " case "
                lsSql &= " when i.packet_gid = 0 then '' "
                lsSql &= " when i.packet_status & " & GCREJECTENTRY & " > 0 then 'Rejected' "
                lsSql &= " when i.packet_status & " & GCPACKETCHEQUEENTRY & " > 0 then 'DE Completed' "
                lsSql &= " when i.packet_status & " & (GCREJECTENTRY Or GCPACKETCHEQUEENTRY) & " = 0 then 'DE Pending' "
                lsSql &= " else '' end as 'Packet Status',"

                lsSql &= " case "
                lsSql &= " when i.packet_gid = 0 or (i.packet_status & " & GCREJECTENTRY & " > 0 and i.packet_status & " & GCPKTAUTHFINONE & " = 0) then '' "
                lsSql &= " when i.packet_status & " & GCPKTAUTHFINONE & " > 0 then 'Authorized' "
                lsSql &= " else 'Pending' end as 'Finone Status',"

                lsSql &= " i.inward_pdc_cnt as 'Inward PDC',"
                lsSql &= " i.inward_spdc_cnt as 'Inward SPDC',"
                lsSql &= " i.inward_mandate_cnt as 'Inward Mandate',"
                lsSql &= " f.pdc_cnt as 'Finone PDC',"
                lsSql &= " f.spdc_cnt as 'Finone SPDC',"
                lsSql &= " f.emi_cnt as 'Finone ECS',"
                lsSql &= " i.pkt_pdc_cnt as 'GNSA PDC',"
                lsSql &= " i.pkt_spdc_cnt as 'GNSA SPDC',"
                lsSql &= " i.pkt_emi_cnt as 'GNSA ECS',"
                lsSql &= " i.pkt_mandate_cnt as 'GNSA Mandate',"
                lsSql &= " if(i.pkt_tot_cnt= 0,'',"
                lsSql &= " if(ifnull(f.pdc_cnt,0) = i.pkt_pdc_cnt and i.inward_pdc_cnt = i.pkt_pdc_cnt and ifnull(f.spdc_cnt,0) = i.pkt_spdc_cnt and i.inward_spdc_cnt = i.pkt_spdc_cnt and ifnull(f.emi_cnt,0) = i.pkt_emi_cnt,'No','Yes')) as 'Count Mismatch',"

                lsSql &= " case "
                lsSql &= " when ifnull(f.packet_gid,0) = 0 then '' "
                lsSql &= " when i.pkt_tot_cnt = 0 then '' "
                lsSql &= " when i.pkt_disc_mapped > 0 then 'Yes' "
                lsSql &= " when i.pkt_pdc_mode <> ifnull(f.pdc_mode,'') then 'Yes' "
                lsSql &= " when i.pkt_tot_cnt = i.pkt_pdc_mapped+i.pkt_spdc_mapped+i.pkt_emi_mapped then 'No' "
                lsSql &= " else 'Yes' end as 'Field Mismatch'"

                lsSql &= " from chola_rpt_tinward as i "
                lsSql &= " left join chola_rpt_tfinone as f on f.inward_gid = i.inward_gid and f.rpt_gid = i.rpt_gid "
                lsSql &= " where i.rpt_gid = " & lnRptId & " "

                objdt = GetDataTable(lsSql)

                If objdt.Rows.Count > 0 Then
                    .NewSheet("Day 2 (Inward " & Format(CDate(lsDayTwo), "dd-MM") & ")")
                    .BeginRow()
                    For i As Integer = 0 To objdt.Columns.Count - 1
                        .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                    Next
                    .EndRow()
                    For i As Integer = 0 To objdt.Rows.Count - 1
                        .BeginRow()
                        For j As Integer = 0 To objdt.Columns.Count - 1
                            .AddCell(objdt.Rows(i)(j).ToString)
                        Next
                        .EndRow()
                    Next
                    .EndSheet()
                End If

                lsSql = "delete from chola_rpt_tinward where rpt_gid = " & lnRptId & " "
                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lsSql = "delete from chola_rpt_tfinone where rpt_gid = " & lnRptId & " "
                lnResult = gfInsertQry(lsSql, gOdbcConn)

                ' Day Two By Auth Date
                lsSql = "insert into chola_rpt_treport (entry_date,entry_by) values (sysdate(),'" & gUserName & "')"
                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lnRptId = Val(gfExecuteScalar("select max(rpt_gid) from chola_rpt_treport", gOdbcConn))

                ' insert in packet report table
                lsSql = ""
                lsSql &= " insert into chola_rpt_tinward (rpt_gid,inward_gid,inward_date,inward_pdc_mode,inward_pdc_cnt,inward_spdc_cnt,inward_mandate_cnt,"
                lsSql &= " inward_status,gnsa_refno,packet_gid,agreement_no,shortagreement_no,agreement_gid,"
                lsSql &= " cust_name,auth_date,pkt_pdc_mode,pkt_mandate_cnt,packet_status) "
                lsSql &= " SELECT " & lnRptId & ","
                lsSql &= " inward_gid,inward_receiveddate,inward_paymode,inward_pdc,inward_spdc,inward_mandate,inward_status,"
                lsSql &= " packet_gnsarefno,ifnull(packet_gid,0),inward_agreementno,inward_shortagreementno,"
                lsSql &= " ifnull(packet_agreement_gid,0),inward_customername,inward_userauthdate,packet_mode,"
                lsSql &= " ifnull(spdcentry_ecsmandatecount,0) as mandate_cnt,ifnull(packet_status,0) "
                lsSql &= " FROM chola_trn_tinward"
                lsSql &= " inner join chola_trn_tpdcfilehead on head_inward_gid = inward_gid "
                lsSql &= " left join chola_trn_tpacket on packet_gid = inward_packet_gid"
                lsSql &= " left join chola_trn_tspdcentry on spdcentry_packet_gid = packet_gid"
                lsSql &= " where true"

                lsSql &= " and (inward_receiveddate >= '" & lsDayTwo & "'"
                lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lsDayTwo)), "yyyy-MM-dd") & "') "

                lsSql &= " or (head_systemauthdate >= '" & lsDayTwo & "'"
                lsSql &= " and head_systemauthdate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lsDayTwo)), "yyyy-MM-dd") & "') "

                lsSql &= " and packet_status & " & GCPKTAUTHFINONE & " > 0 "
                lsSql &= " group by inward_gid,inward_paymode,inward_pdc,inward_spdc,inward_mandate,inward_status,"
                lsSql &= " packet_gnsarefno,packet_gid,inward_agreementno,inward_shortagreementno,"
                lsSql &= " packet_agreement_gid,inward_customername,inward_userauthdate,packet_mode,spdcentry_ecsmandatecount"

                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lsSql = ""
                lsSql &= " select rptinward_gid,packet_gid from chola_rpt_tinward "
                lsSql &= " where rpt_gid = " & lnRptId & " "
                lsSql &= " and packet_gid > 0 "

                Call gpDataSet(lsSql, "rpt", gOdbcConn, ds)

                For i = 0 To ds.Tables("rpt").Rows.Count - 1
                    lnPktId = ds.Tables("rpt").Rows(i).Item("packet_gid")
                    lnRptInwardId = ds.Tables("rpt").Rows(i).Item("rptinward_gid")

                    lnTotCnt = 0
                    lnPdcCnt = 0
                    lnPdcMapped = 0
                    lnSpdcCnt = 0
                    lnSpdcMapped = 0
                    lnEmiCnt = 0
                    lnEmiMapped = 0
                    lnTypeDiscCnt = 0

                    ' pdc
                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(chq_pdc_gid > 0,1,0)) as mapped from chola_trn_tpdcentry "
                    lsSql &= " where chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and chq_type <> " & GCEXTERNALSECURITY & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnPdcCnt = Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnPdcMapped = Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(chq_pdc_gid > 0,1,0)) as mapped from chola_trn_tpdcentry "
                    lsSql &= " where chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and chq_type = " & GCEXTERNALSECURITY & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnSpdcCnt += Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnSpdcMapped += Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    ' spdc
                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(chqentry_pdc_gid > 0,1,0)) as mapped from chola_trn_tspdcchqentry "
                    lsSql &= " where chqentry_packet_gid = " & lnPktId & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnSpdcCnt += Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnSpdcMapped += Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    ' emi
                    lsSql = ""
                    lsSql &= " select count(*) as cnt,sum(if(ecsemientry_pdc_gid > 0,1,0)) as mapped from chola_trn_tecsemientry "
                    lsSql &= " where ecsemientry_packet_gid = " & lnPktId & " "

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        lnEmiCnt = Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString)
                        lnEmiMapped = Val(ds.Tables("cnt").Rows(0).Item("mapped").ToString)
                    End If

                    ds.Tables("cnt").Rows.Clear()

                    ' Disc Count
                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tpdcentry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chq_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'PDC' "
                    lsSql &= " where a.chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and a.chq_type <> " & GCEXTERNALSECURITY & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tpdcentry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chq_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'SPDC' "
                    lsSql &= " where a.chq_packet_gid = " & lnPktId & " "
                    lsSql &= " and a.chq_type = " & GCEXTERNALSECURITY & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tspdcchqentry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.chqentry_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'SPDC' "
                    lsSql &= " where a.chqentry_packet_gid = " & lnPktId & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lsSql = ""
                    lsSql &= " select count(*) from chola_trn_tecsemientry as a "
                    lsSql &= " inner join chola_trn_tpdcfile as b on b.pdc_gid = a.ecsemientry_pdc_gid "
                    lsSql &= " inner join chola_mst_tpdctype as c on c.pdc_type = b.pdc_type "
                    lsSql &= " and c.new_pdc_type <> 'ECS' "
                    lsSql &= " where a.ecsemientry_packet_gid = " & lnPktId & " "

                    lnTypeDiscCnt += Val(gfExecuteScalar(lsSql, gOdbcConn))

                    lnTotCnt = lnPdcCnt + lnSpdcCnt + lnEmiCnt

                    If lnTotCnt > 0 Then
                        lsSql = ""
                        lsSql &= " update chola_rpt_tinward set "
                        lsSql &= " pkt_tot_cnt = " & lnTotCnt & ","
                        lsSql &= " pkt_pdc_cnt = " & lnPdcCnt & ","
                        lsSql &= " pkt_pdc_mapped = " & lnPdcMapped & ","
                        lsSql &= " pkt_spdc_cnt = " & lnSpdcCnt & ","
                        lsSql &= " pkt_spdc_mapped = " & lnSpdcMapped & ","
                        lsSql &= " pkt_emi_cnt = " & lnEmiCnt & ","
                        lsSql &= " pkt_emi_mapped = " & lnEmiMapped & ","
                        lsSql &= " pkt_disc_mapped = " & lnTypeDiscCnt & " "
                        lsSql &= " where rptinward_gid = " & lnRptInwardId

                        lnResult = gfInsertQry(lsSql, gOdbcConn)
                    End If
                Next i

                ds.Tables("rpt").Rows.Clear()

                ' insert in finone report table
                lsSql = ""
                lsSql &= " insert into chola_rpt_tfinone (rpt_gid,inward_gid,packet_gid,short_agreementno,agreement_gid,"
                lsSql &= " auth_date,pdc_mode,tot_cnt,pdc_cnt,spdc_cnt,emi_cnt,pdc_mapped,spdc_mapped,emi_mapped) "
                lsSql &= " select " & lnRptId & ",ifnull(h.head_inward_gid,0),ifnull(h.head_packet_gid,0),"
                lsSql &= " h.head_shortagreementno,"
                lsSql &= " ifnull(h.head_agreement_gid,0),"
                lsSql &= " h.head_systemauthdate,"
                lsSql &= " m.new_pdc_mode,"
                lsSql &= " count(*) as tot_cnt,"
                lsSql &= " ifnull(sum(if(t.new_pdc_type = 'PDC',1,0)),0) as pdc_cnt,"
                lsSql &= " ifnull(sum(if(t.new_pdc_type = 'SPDC',1,0)),0) as spdc_cnt,"
                lsSql &= " ifnull(sum(if(t.new_pdc_type = 'ECS',1,0)),0) as emi_cnt,"
                lsSql &= " ifnull(sum(if(entry_gid > 0,1,0)),0) as pdc_mapped,"
                lsSql &= " ifnull(sum(if(pdc_spdcentry_gid > 0,1,0)),0) as spdc_mapped,"
                lsSql &= " ifnull(sum(if(pdc_ecsentry_gid > 0,1,0)),0) as emi_mapped "
                lsSql &= " from chola_rpt_tinward as r "
                lsSql &= " inner join chola_trn_tpdcfilehead as h on h.head_packet_gid = r.packet_gid and h.head_agreement_gid = r.agreement_gid "
                lsSql &= " inner join chola_trn_tpdcfile as p on p.file_mst_gid = h.head_file_gid and p.pdc_parentcontractno = h.head_agreementno "
                lsSql &= " left join chola_mst_tpdctype as t on t.pdc_type = p.pdc_type "
                lsSql &= " and p.chq_rec_slno = 1 "
                lsSql &= " left join chola_mst_tpdcmode as m on m.pdc_mode = h.head_mode "
                lsSql &= " where r.rpt_gid = " & lnRptId & " "
                lsSql &= " and r.packet_gid > 0 "
                lsSql &= " group by h.head_inward_gid,h.head_packet_gid,h.head_shortagreementno,h.head_agreement_gid,"
                lsSql &= " h.head_systemauthdate, h.head_mode"

                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lsSql = ""
                lsSql &= " select "
                lsSql &= " date_format(i.inward_date,'%d-%m-%Y') as 'Received Date',"
                lsSql &= " i.agreement_no as 'Agreement No',"
                lsSql &= " i.shortagreement_no as 'Short Agreement No',"
                lsSql &= " i.cust_name as 'Customer Name',"
                lsSql &= " i.inward_pdc_mode as 'PDC Mode',"
                lsSql &= " i.gnsa_refno as 'GNSAREF#',"

                lsSql &= " case "
                lsSql &= " when i.packet_gid > 0 then 'Received' "
                lsSql &= " when i.inward_status & " & GCCOMBINED & " > 0 then 'Combined' "
                lsSql &= " when i.inward_status & " & GCNOTRECEIVED & " > 0 then 'Not Received' "
                lsSql &= " else 'Pending' end as 'Inward Status',"

                lsSql &= " case "
                lsSql &= " when i.packet_gid = 0 then '' "
                lsSql &= " when i.packet_status & " & GCREJECTENTRY & " > 0 then 'Rejected' "
                lsSql &= " when i.packet_status & " & GCPACKETCHEQUEENTRY & " > 0 then 'DE Completed' "
                lsSql &= " when i.packet_status & " & (GCREJECTENTRY Or GCPACKETCHEQUEENTRY) & " = 0 then 'DE Pending' "
                lsSql &= " else '' end as 'Packet Status',"

                lsSql &= " case "
                lsSql &= " when i.packet_gid = 0 or (i.packet_status & " & GCREJECTENTRY & " > 0 and i.packet_status & " & GCPKTAUTHFINONE & " = 0) then '' "
                lsSql &= " when i.packet_status & " & GCPKTAUTHFINONE & " > 0 then 'Authorized' "
                lsSql &= " else 'Pending' end as 'Finone Status',"

                lsSql &= " i.inward_pdc_cnt as 'Inward PDC',"
                lsSql &= " i.inward_spdc_cnt as 'Inward SPDC',"
                lsSql &= " i.inward_mandate_cnt as 'Inward Mandate',"
                lsSql &= " f.pdc_cnt as 'Finone PDC',"
                lsSql &= " f.spdc_cnt as 'Finone SPDC',"
                lsSql &= " f.emi_cnt as 'Finone ECS',"
                lsSql &= " i.pkt_pdc_cnt as 'GNSA PDC',"
                lsSql &= " i.pkt_spdc_cnt as 'GNSA SPDC',"
                lsSql &= " i.pkt_emi_cnt as 'GNSA ECS',"
                lsSql &= " i.pkt_mandate_cnt as 'GNSA Mandate',"

                lsSql &= " if(i.pkt_tot_cnt= 0,'',"
                lsSql &= " if(ifnull(f.pdc_cnt,0) = i.pkt_pdc_cnt and i.inward_pdc_cnt = i.pkt_pdc_cnt and ifnull(f.spdc_cnt,0) = i.pkt_spdc_cnt and i.inward_spdc_cnt = i.pkt_spdc_cnt and ifnull(f.emi_cnt,0) = i.pkt_emi_cnt,'No','Yes')) as 'Count Mismatch',"

                'lsSql &= " case "
                'lsSql &= " when ifnull(f.packet_gid,0) = 0 then '' "
                'lsSql &= " when i.pkt_tot_cnt = 0 then '' "
                'lsSql &= " when i.pkt_tot_cnt = i.pkt_pdc_mapped+i.pkt_spdc_mapped+i.pkt_emi_mapped then 'No' "
                'lsSql &= " else 'Yes' end as 'Field Mismatch'"

                lsSql &= " case "
                lsSql &= " when ifnull(f.packet_gid,0) = 0 then '' "
                lsSql &= " when i.pkt_tot_cnt = 0 then '' "
                lsSql &= " when i.pkt_disc_mapped > 0 then 'Yes' "
                lsSql &= " when i.pkt_pdc_mode <> ifnull(f.pdc_mode,'') then 'Yes' "
                lsSql &= " when i.pkt_tot_cnt = i.pkt_pdc_mapped+i.pkt_spdc_mapped+i.pkt_emi_mapped then 'No' "
                lsSql &= " else 'Yes' end as 'Field Mismatch'"

                lsSql &= " from chola_rpt_tinward as i "
                lsSql &= " left join chola_rpt_tfinone as f on f.inward_gid = i.inward_gid and f.rpt_gid = i.rpt_gid "
                lsSql &= " where i.rpt_gid = " & lnRptId & " "

                objdt = GetDataTable(lsSql)

                If objdt.Rows.Count > 0 Then
                    .NewSheet("Day 2 (Auth " & Format(CDate(lsDayTwo), "dd-MM") & ")")
                    .BeginRow()
                    For i As Integer = 0 To objdt.Columns.Count - 1
                        .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                    Next
                    .EndRow()
                    For i As Integer = 0 To objdt.Rows.Count - 1
                        .BeginRow()
                        For j As Integer = 0 To objdt.Columns.Count - 1
                            .AddCell(objdt.Rows(i)(j).ToString)
                        Next
                        .EndRow()
                    Next
                    .EndSheet()
                End If

                lsSql = "delete from chola_rpt_tinward where rpt_gid = " & lnRptId & " "
                lnResult = gfInsertQry(lsSql, gOdbcConn)

                lsSql = "delete from chola_rpt_tfinone where rpt_gid = " & lnRptId & " "
                lnResult = gfInsertQry(lsSql, gOdbcConn)
            End If

            'Pouch Not Received
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',"
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " left join chola_trn_tpacket on packet_gid = inward_packet_gid "
            lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " and inward_packet_gid=0 "
            lsSql &= " and inward_status & " & GCNOTRECEIVED & " > 0 "

            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pouch Not Recd")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Pending Inward
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " and inward_packet_gid=0 "
            lsSql &= " and inward_status & " & GCNOTRECEIVED & " = 0 "
            lsSql &= " and inward_status & " & GCCOMBINED & " = 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Inward")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Pending Finone Auth
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCREJECTENTRY & " = 0 "
            lsSql &= " and packet_status & " & GCPKTAUTHFINONE & " = 0 "
            lsSql &= " and inward_status & " & GCRECEIVED & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Auth")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Rejects
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',packet_remarks as 'Remarks', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCREJECTENTRY & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Rejects")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Pending DE
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " and inward_status & " & GCRECEIVED & " > 0 "
            lsSql &= " and packet_status & " & (GCPACKETCHEQUEENTRY Or GCREJECTENTRY Or GCPKTREPROCESS) & " = 0 "
            objdt = GetDataTable(lsSql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("DE Pending")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Rejected Pending DE
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where inward_receiveddate >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
            lsSql &= " and inward_status & " & GCRECEIVED & " > 0 "
            lsSql &= " and packet_status & " & GCPKTREPROCESS & " > 0 "
            lsSql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " = 0 "
            objdt = GetDataTable(lsSql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Reject DE Pending")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            .Close()
        End With
        Me.Cursor = Cursors.Default
        MsgBox("Report Generated..!", MsgBoxStyle.Information, gProjectName)
        System.Diagnostics.Process.Start(gsReportPath & lsFileName)

    End Sub

    Private Sub ExportMisOld()
        Dim lsSql As String
        Dim objdt As DataTable
        Dim lsFileName As String

        If dtpfrom.Checked = False Then
            MsgBox("Please Select Received From Date", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        If dtpto.Checked = False Then
            MsgBox("Please Select Received To Date", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        lsFileName = "Inward MIS" & Format(Now, "ddMMyyyyhhmmss") & ".xls"
        Dim objxlexport As New XMLExport(gsReportPath & lsFileName)

        With objxlexport
            'Summary Sheet
            lsSql = ""
            lsSql &= " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Date',count(*) as 'Hands off',"
            lsSql &= " sum(if(inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Inward Recd',"
            lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " > 0,1,0)) as 'Not Recd',"
            lsSql &= " sum(if(inward_status & " & GCCOMBINED & " > 0,1,0)) as 'Combined',"
            lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " = 0 and inward_status & " & GCRECEIVED & " = 0 and inward_status & " & GCCOMBINED & " = 0,1,0)) as 'Pending Inward',"
            lsSql &= " sum(if(packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Auth',"
            lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " > 0,1,0)) as 'Rejected',"
            lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " = 0 and packet_status & " & GCAUTHENTRY & " = 0 and inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Auth Pending',"
            lsSql &= " sum(if(packet_status & " & GCPACKETCHEQUEENTRY & " > 0,1,0)) as 'Chq DE Completed',"
            lsSql &= " sum(if(packet_status & " & GCPACKETCHEQUEENTRY & " = 0 and packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Chq DE pending'"
            lsSql &= " from chola_trn_tinward "
            lsSql &= " left join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " group by inward_receiveddate"
            lsSql &= " having "
            lsSql &= " (`Pending Inward` <> 0 or `Auth Pending` <> 0 or `Chq DE pending` <> 0 or `Rejected` <> 0)"

            objdt = GetDataTable(lsSql)
            objdt.Rows.Add()
            objdt.Rows(objdt.Rows.Count - 1)(0) = "Total:"
            For i As Integer = 0 To objdt.Rows.Count - 2
                For j As Integer = 1 To objdt.Columns.Count - 1
                    objdt.Rows(objdt.Rows.Count - 1)(j) = Val(objdt.Rows(objdt.Rows.Count - 1)(j).ToString) + Val(objdt.Rows(i)(j).ToString)
                Next
            Next
            If objdt.Rows.Count > 0 Then
                .NewSheet("Summary")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Pouch Not Received
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_packet_gid=0 "
            lsSql &= " and inward_status & " & GCNOTRECEIVED & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pouch Not Recd")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Pending Inward
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_packet_gid=0 "
            lsSql &= " and inward_status & " & GCRECEIVED & " = 0 "
            lsSql &= " and inward_status & " & GCNOTRECEIVED & " = 0 "
            lsSql &= " and inward_status & " & GCCOMBINED & " = 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Inward")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Pending Auth
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCREJECTENTRY & " = 0 "
            lsSql &= " and packet_status & " & GCAUTHENTRY & " = 0 "
            lsSql &= " and inward_status & " & GCRECEIVED & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Auth")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Rejects
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',packet_remarks as 'Remarks', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCREJECTENTRY & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Rejects")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Pending DE
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCAUTHENTRY & " > 0 "
            lsSql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " = 0 "
            objdt = GetDataTable(lsSql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending DE")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
                Next
                .EndRow()
                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If
            .Close()
        End With
        Me.Cursor = Cursors.Default
        MsgBox("Report Generated..!", MsgBoxStyle.Information, gProjectName)
        System.Diagnostics.Process.Start(gsReportPath & lsFileName)

    End Sub

    Public Sub New(Optional ByVal NewColVisible As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mbNewColVisible = NewColVisible
    End Sub
End Class