Public Class frmChqSearchEngine
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsFld As String
        Dim lsChqNo As String
        Dim lsTxt As String
        Dim lsDisc As String
        Dim lsCond As String

        Dim i As Integer = 0
        Dim n As Integer = 0
        Dim lnResult As Long

        Dim dgvRow As DataGridViewRow = Nothing

        Try
            ' Find Packet Id
            If txtChqNo.Text <> "" Then
                lsChqNo = QuoteFilter(txtChqNo.Text)
            Else
                Exit Sub
            End If

            dgvSpdc.DataSource = Nothing
            dgvSpdcPullout.DataSource = Nothing
            dgvPdcPullout.DataSource = Nothing
            dgvPdc.DataSource = Nothing
            dgvBounce.DataSource = Nothing

            ' Pdc Pullout
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " p.pullout_insertdate as 'Entry Date',p.pullout_shortagreementno as 'Short Agreement No',"
            lsFld &= " p.pullout_chqdate as 'Chq Date',p.pullout_chqno as 'Chq No',p.pullout_chqamount as 'Chq Amount',"
            lsFld &= " r.reason_name as 'Pullout Reason',"
            lsFld &= " a.agreement_no as 'Agreement No',"
            lsFld &= " p.pullout_entrygid as 'Pdc Id',p.pullout_gid as 'Pullout Id',"
            lsFld &= " p.pullout_reasongid as 'Pullout Reason Id',p.pullout_insertby as 'Entry By',"
            lsFld &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsFld &= " a.agreement_closeddate as 'Closed Date'"

            lsCond = ""

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tpullout as p "
            lsSql &= " left join chola_mst_tpulloutreason as r on r.reason_gid = p.pullout_reasongid "
            lsSql &= " left join chola_trn_tpdcentry as c on c.entry_gid = p.pullout_entrygid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = k.packet_agreement_gid "
            lsSql &= " where true "
            lsSql &= " and p.pullout_chqno = '" & QuoteFilter(txtChqNo.Text) & "' "

            gpPopGridView(dgvPdcPullout, lsSql, gOdbcConn)

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
            lsSql &= " and c.chq_no = '" & QuoteFilter(txtChqNo.Text) & "' "
            lsSql &= " order by c.chq_packet_gid,c.chq_date"

            gpPopGridView(dgvPdc, lsSql, gOdbcConn)

            ' SPDC Pullout
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " p.spdcpullout_insertdate as 'Entry Date',p.spdcpullout_shortagreementno as 'Short Agreement No',"
            lsFld &= " p.spdcpullout_chqno as 'Chq No',p.spdcpullout_remarks as 'Pullout Remark',"
            lsFld &= " r.reason_name as 'Pullout Reason',"
            lsFld &= " a.agreement_no as 'Agreement No',"
            lsFld &= " p.spdcpullout_chqentrygid as 'Spdc Id',p.spdcpullout_gid as 'SPDC Pullout Id',"
            lsFld &= " p.spdcpullout_reasongid as 'Pullout Reason Id',p.spdcpullout_insertby as 'Entry By',"
            lsFld &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsFld &= " a.agreement_closeddate as 'Closed Date'"

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tspdcpullout as p "
            lsSql &= " left join chola_mst_tpulloutreason as r on r.reason_gid = p.spdcpullout_reasongid "
            lsSql &= " left join chola_trn_tspdcchqentry as c on c.chqentry_gid = p.spdcpullout_chqentrygid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chqentry_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = k.packet_agreement_gid "
            lsSql &= " where true "
            lsSql &= " and p.spdcpullout_chqno = '" & QuoteFilter(txtChqNo.Text) & "' "
            lsSql &= " and spdcpullout_deleteflag = 'N'"

            gpPopGridView(dgvSpdcPullout, lsSql, gOdbcConn)

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
            lsSql &= " and s.chqentry_chqno = '" & QuoteFilter(txtChqNo.Text) & "' "
            lsSql &= " order by s.chqentry_chqno"

            gpPopGridView(dgvSpdc, lsSql, gOdbcConn)

            ' Bounce
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " e.bounceentry_slno as 'Bounce SNo',e.bounceentry_insertdate as 'Entry Date',"
            lsFld &= " e.bounceentry_chqno as 'Chq No',e.bounceentry_chqdate as 'Chq Date',e.bounceentry_chqamount as 'Chq Amount',"
            lsFld &= " b.bounce_agreementno as 'Bounce Agreement No',a.agreement_no as 'Agreement No',b.bounce_returndate as 'Return Date',r.reason_name,b.bounce_awbno as 'AWB No',"
            lsFld &= " e.bounceentry_remarks as 'Bounce Remark',e.bounceentry_insertby as 'Entry By',"
            lsFld &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsFld &= " p.bouncepullout_insertdate as 'Pullout Date',p.bouncepullout_insertby as 'Pullout By',p.bouncepullout_remarks as 'Pullout Remark',"
            lsFld &= " a.agreement_closeddate as 'Closed Date',b.bounce_gid as 'Bounce Id',e.bounceentry_gid as 'Bounce Entry Id',e.bounceentry_pullout_gid as 'Pullout Id',"
            lsFld &= " e.bounceentry_entry_gid as 'Pdc Id',bounceentry_inward_gid as 'Bounce Inward Id' "

            lsCond = ""

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tbounceentry as e "
            lsSql &= " left join chola_trn_tbounce as b on  b.bounce_gid = e.bounceentry_bounce_gid  "
            lsSql &= " left join chola_trn_tbouncepullout as p on p.bouncepullout_gid = e.bounceentry_pullout_gid "
            lsSql &= " left join chola_mst_tbouncereason as r on r.reason_gid = e.bounceentry_bouncereason_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = e.bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_tpdcentry as c on c.entry_gid = e.bounceentry_entry_gid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " where true "
            lsSql &= " and e.bounceentry_chqno = '" & QuoteFilter(txtChqNo.Text) & "' "

            gpPopGridView(dgvBounce, lsSql, gOdbcConn)

            ' Loose Cheque
            lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

            lsFld = ""
            lsFld &= " @sno := @sno + 1 as 'SNo',"
            lsFld &= " l.loosechqentry_sno as 'Loose Chq SNo',c.chq_date as 'Chq Date',c.chq_no as 'Chq No',c.chq_amount as 'Chq Amount',"
            lsFld &= " a.shortagreement_no as 'Short Agreement No',l.loosechqentry_remarks as 'Remark',"
            lsFld &= " make_set(c.chq_status," & lsTxt & ") as 'Chq Status',"
            lsFld &= " c.chq_status as 'Chq Status Value',"
            lsFld &= " a.agreement_no as 'Agreement No',"
            lsFld &= " l.loosechqentry_insertdate as 'Entry Date',l.loosechqentry_insertby as 'Entry By',"
            lsFld &= " l.loosechqentry_pdcgid as 'Pdc Id',l.loosechqentry_gid as 'Loose Chq Id',"
            lsFld &= " k.packet_gnsarefno as 'GNSA Ref No',m.almaraentry_cupboardno as 'Cupboard No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno as 'Box No',"
            lsFld &= " a.agreement_closeddate as 'Closed Date'"

            lsCond = ""

            lsSql = ""
            lsSql &= " select "
            lsSql &= lsFld
            lsSql &= " from chola_trn_tloosechqentry as l "
            lsSql &= " left join chola_trn_tpdcentry as c on c.entry_gid = l.loosechqentry_pdcgid "
            lsSql &= " left join chola_trn_tpacket as k on k.packet_gid = c.chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry as m on m.almaraentry_gid = k.packet_box_gid "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_gid = k.packet_agreement_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and l.loosechqentry_deleteflag = 'N' "

            gpPopGridView(dgvLooseChq, lsSql, gOdbcConn)

            ' Count
            lblPdcCount.Text = "Total : " & dgvPdc.RowCount
            lblSpdcCount.Text = "Total :" & dgvSpdc.RowCount
            lblBounceCount.Text = "Total : " & dgvBounce.RowCount

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

            With dgvPdcPullout
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvSpdcPullout
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvLooseChq
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With

            With dgvBounce
                For i = 0 To .ColumnCount - 1
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtChqNo.Text = ""

        dgvSpdc.DataSource = Nothing
        dgvSpdcPullout.DataSource = Nothing
        dgvPdcPullout.DataSource = Nothing
        dgvPdc.DataSource = Nothing
        dgvBounce.DataSource = Nothing
        txtChqNo.Focus()
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
        txtChqNo.Focus()
    End Sub

    Private Sub frmSearchEngine_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If txtChqNo.Text.Trim <> "" Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub frmSearchEngine_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim llHeight As Long
        Dim llWidth As Long
        Dim llTop As Long

        llHeight = Math.Abs(Me.Height - (grpMain.Top + grpMain.Height) - 16)
        llHeight = (llHeight - 48 - (lblPDCPullout.Height * 4) - 6 * 4) \ 4

        llWidth = Me.Width - 30

        llTop = grpMain.Top + grpMain.Height + 6

        lblPdc.Left = grpMain.Left
        lblPdc.Top = llTop

        lblPdcCount.Left = lblPdc.Left + lblPdc.Width + 12
        lblPdcCount.Top = llTop

        llTop = llTop + lblPdc.Height + 6

        dgvPdc.Height = llHeight
        dgvPdc.Width = llWidth
        dgvPdc.Left = grpMain.Left
        dgvPdc.Top = llTop
        llTop = llTop + llHeight + 6

        lblSpdc.Left = grpMain.Left
        lblSpdc.Top = llTop

        lblSpdcCount.Top = lblSpdc.Top
        lblSpdcCount.Left = lblSpdc.Left + lblSpdc.Width + 12

        llTop = llTop + lblPdc.Height + 6

        dgvSpdc.Height = llHeight
        dgvSpdc.Left = grpMain.Left
        dgvSpdc.Top = llTop
        dgvSpdc.Width = llWidth

        llTop = llTop + llHeight + 6
        llWidth = (llWidth - 6) \ 2

        lblBounce.Top = llTop
        lblBounce.Left = lblPdc.Left

        lblBounceCount.Top = lblBounce.Top
        lblBounceCount.Left = lblBounce.Left + lblBounce.Width + 12

        lblLooseChq.Top = llTop
        lblLooseChq.Left = llWidth + 10

        llTop = llTop + lblPDCPullout.Height + 6

        dgvBounce.Height = llHeight
        dgvBounce.Left = grpMain.Left
        dgvBounce.Top = llTop
        dgvBounce.Width = llWidth

        dgvLooseChq.Height = llHeight
        dgvLooseChq.Left = grpMain.Left + dgvBounce.Width + 2
        dgvLooseChq.Top = llTop
        dgvLooseChq.Width = llWidth

        llTop = llTop + llHeight + 6

        lblPDCPullout.Left = grpMain.Left
        lblPDCPullout.Top = llTop

        lblSpdcPullout.Left = llWidth + 10
        lblSpdcPullout.Top = llTop

        llTop = llTop + lblPdc.Height + 6

        dgvPdcPullout.Height = llHeight
        dgvPdcPullout.Left = grpMain.Left
        dgvPdcPullout.Top = llTop
        dgvPdcPullout.Width = llWidth

        dgvSpdcPullout.Height = llHeight
        dgvSpdcPullout.Top = llTop
        dgvSpdcPullout.Left = dgvPdcPullout.Left + dgvPdcPullout.Width + 2
        dgvSpdcPullout.Width = dgvPdcPullout.Width
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGViewXML(dgvPdc, gsReportPath & "Report.xls", "PDC", True, True)
            PrintDGViewXML(dgvSpdc, gsReportPath & "Report.xls", "SPDC", False, True)
            PrintDGViewXML(dgvBounce, gsReportPath & "Report.xls", "Bounce", False, True)
            PrintDGViewXML(dgvLooseChq, gsReportPath & "Report.xls", "Loose", False, True)
            PrintDGViewXML(dgvPdcPullout, gsReportPath & "Report.xls", "PDC Pullout", False, True)
            PrintDGViewXML(dgvSpdcPullout, gsReportPath & "Report.xls", "SPDC Pullout", False, False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class