Public Class frmFinoneReconNew
    Dim objdt As New DataTable
    Dim gsCondDate As String
    Dim gsCondFinoneDate As String

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        btnRefresh.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        Select Case True
            Case rdoDaily.Checked
                gsCondDate = " and chq_date = '" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "' "
            Case rdoMonthly.Checked
                gsCondDate = ""
                gsCondDate &= " and chq_date >= '" & Format(dtpcycledate.Value, "yyyy-MM-01") & "' "
                gsCondDate &= " and chq_date < '" & Format(DateAdd(DateInterval.Month, 1, dtpcycledate.Value), "yyyy-MM-01") & "' "
        End Select

        gsCondFinoneDate = gsCondDate.Replace("chq_date", "finone_chqdate")

        Select Case cboRecon.Text.ToUpper
            Case "DETAIL"
                Call LoadDetail()
            Case "RECON"
                Call LoadRecon()
            Case Else
                Call LoadSummary()
        End Select

        dgvsummary.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Me.Cursor = Cursors.Default
        btnRefresh.Enabled = True
    End Sub

    Private Sub LoadDetail()
        Dim i As Integer
        Dim lssql As String
        Dim lncnt As Long

        btnRefresh.Enabled = False

        objdt.Rows.Clear()
        objdt.Columns.Clear()

        objdt.Columns.Add("Particulars")
        objdt.Columns.Add("Total Count")
        objdt.Columns.Add("Query")

        i = -1

        ' Presentable
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Total Presentable"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Presentable Data Entry Done
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Presentation Data Entry Done"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Presentable Data Entry Not Done
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',chq_accno as 'A/C No',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Presentation Data Entry Not Done"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Presentable Not Available in Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Not Available in Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Presentable Not Available in Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Not Available in Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        'Total Records In Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "As Per Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Available In VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile "
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entry_gid > 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched With Precon"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Not Available in VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile "
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entry_gid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Not Matched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        i += 1
        objdt.Rows.Add()

        'Total Records In Finone (Post)
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "As Per Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Matched With VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entrygid > 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched with Postcon"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Not Matched With VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entrygid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Not Matched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        i += 1
        objdt.Rows.Add()

        ' Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " left join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " left join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',chq_accno as 'A/C No',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Batched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        ' Matched with Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " left join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Batched Matched With Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Matched not available in Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " left join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',chq_accno as 'A/C No',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Batched Not Matched With Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Available in Pre Not Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',"
        lssql &= " agreement_closeddate as 'Closed Date',"
        lssql &= " pullout_insertdate as 'Pullout Date',"
        lssql &= " packetpullout_postedon as 'Packet Pullout',"
        lssql &= " reason_name as 'Pullout Reason',"
        lssql &= " packetpullout_reason as 'Packet Pullout Reason',"
        lssql &= " date_format(oldentry_date,'%d-%m-%Y') as 'Swap Date',"
        lssql &= " chq_desc as 'Description' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tpullout on pullout_entrygid = entry_gid "
        lssql &= " left join chola_mst_tpulloutreason on reason_gid = pullout_reasongid "
        lssql &= " left join chola_trn_tpacketpullout on packetpullout_packet_gid = packet_gid "
        lssql &= " left join chola_trn_toldswappacket on oldpacket_gid = packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',chq_accno as 'A/C No',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available in Pre Not Batched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        ' Matched with Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " left join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',chq_accno as 'A/C No',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Batched Matched With Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Batched Not Matched with Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " left join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',chq_accno as 'A/C No',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Batched Not Matched With Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        ' Batched Not Matched with Pre and Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " left join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "

        lssql = ""
        lssql &= " select batch_displayno 'Batch No',type_name as 'Product',"
        lssql &= " packet_gnsarefno 'GNSA Ref',agreement_no 'Agreement No', "
        lssql &= " chq_iscts as 'CTS Flag',"
        lssql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',chq_accno as 'A/C No',pdc_micrcode as 'Micr Code', "
        lssql &= " pdc_bankname as 'Bank Name',pdc_bankbranch as 'Branch',pdc_payablelocation as 'Payable Location',"
        lssql &= " pdc_pickuplocation as 'Pickup Location',pdc_mode as 'Mode',pdc_type as 'Type',"
        lssql &= " finone_cust_bank_account as 'Customer Account No',if(chq_status &  " & GCPRESENTATIONDE & " > 0 ,'Entry Completed','Entry Not Completed') as 'Status' "
        lssql &= " from chola_trn_tpdcentry a "
        lssql &= " left join chola_trn_tfinone on finone_entrygid=a.entry_gid "
        lssql &= " left join chola_trn_tpdcfile b on a.entry_gid=b.entry_gid "
        lssql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lssql &= " left join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " left join chola_trn_tbatch c on c.batch_gid=chq_batch_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by batch_no,type_name,chq_batchslno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Batched Not Matched With Pre and Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Available in Post Not Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',"
        lssql &= " agreement_closeddate as 'Closed Date',"
        lssql &= " pullout_insertdate as 'Pullout Date',"
        lssql &= " packetpullout_postedon as 'Packet Pullout',"
        lssql &= " reason_name as 'Pullout Reason',"
        lssql &= " packetpullout_reason as 'Packet Pullout Reason',"
        lssql &= " date_format(oldentry_date,'%d-%m-%Y') as 'Swap Date',"
        lssql &= " chq_desc as 'Description' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tpullout on pullout_entrygid = entry_gid "
        lssql &= " left join chola_mst_tpulloutreason on reason_gid = pullout_reasongid "
        lssql &= " left join chola_trn_tpacketpullout on packetpullout_packet_gid = packet_gid "
        lssql &= " left join chola_trn_toldswappacket on oldpacket_gid = packet_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available in Post Not Batched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        ' Available In Pre Not Availble in Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile as p "
        lssql &= " left join chola_trn_tfinone as f on f.finone_agreementno = p.finone_agreementno "
        lssql &= " and f.finone_chqno = p.finone_chqno "
        lssql &= " and f.finone_chqdate = p.finone_chqdate "
        lssql &= " and f.finone_chqamount = p.finone_chqamount "
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate.Replace("finone_chqdate", "p.finone_chqdate") & " "
        lssql &= " and f.finone_gid is null "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available In Pre & Not Availble in Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "p.*")

        ' Available In Post & Not Availble in Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone as f "
        lssql &= " left join chola_trn_tfinonepreconverfile as p on p.finone_agreementno = f.finone_agreementno "
        lssql &= " and p.finone_chqno = f.finone_chqno "
        lssql &= " and p.finone_chqdate = f.finone_chqdate "
        lssql &= " and p.finone_chqamount = f.finone_chqamount "
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate.Replace("finone_chqdate", "f.finone_chqdate") & " "
        lssql &= " and p.finone_entry_gid is null "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available In Post & Not Availble in Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "f.*")

        i += 1
        objdt.Rows.Add()

        'Pullout
        'Packet Pullout
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid = chq_packet_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
        lssql &= " and packet_status & " & GCPKTOLDSWAP & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No',agreement_closeddate as 'Closed Date',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',packetpullout_postedon as 'Packet Pullout Date',packetpullout_reason as Remark "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tpacketpullout on packetpullout_gid = packet_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
        lssql &= " and packet_status & " & GCPKTOLDSWAP & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Packet Pullout"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Packet Block
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid = chq_packet_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and packet_status & " & GCPACKETBLOCK & " > 0 "
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No',agreement_closeddate as 'Closed Date',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',packetblock_entryon as 'Packet Block Date',packetblock_reason as Remark "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tpacketblock on packetblock_packet_gid = packet_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and packet_status & " & GCPACKETBLOCK & " > 0 "
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Packet Block"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Closure
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid = chq_packet_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCCLOSURE Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCCLOSURE Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
        lssql &= " and packet_status & " & GCPKTOLDSWAP & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No',agreement_closeddate as 'Closed Date',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount','Closure' as 'Remark' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCCLOSURE Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCCLOSURE Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
        lssql &= " and packet_status & " & GCPKTOLDSWAP & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Closure"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Closure Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " and agreement_closeddate is not null "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No',agreement_closeddate as 'Closed Date',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " left join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " and agreement_closeddate is not null "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Closure Batched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Pullout
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        'lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
        lssql &= " and chq_status & " & (GCPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No',agreement_closeddate as 'Closed Date',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount',pullout_insertdate as 'Pullout Date',reason_name as Remark "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tpullout on pullout_entrygid = entry_gid "
        lssql &= " left join chola_mst_trejectreason on reason_gid = pullout_reasongid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        'lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
        lssql &= " and chq_status & " & (GCPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Pullout"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Swap
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid = chq_packet_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
        lssql &= " and packet_status & " & GCPKTOLDSWAP & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No',agreement_closeddate as 'Closed Date',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount','Swap' as Remark "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE) & " = " & (GCPACKETPULLOUT Or GCMATCHFINONEPRECOVERFILE)
        lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "
        lssql &= " and packet_status & " & GCPKTOLDSWAP & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Swap"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        dgvsummary.DataSource = objdt

        dgvsummary.Columns(0).Width = 300
        dgvsummary.Columns(1).Width = 50
        dgvsummary.Columns(2).Visible = False

        btnRefresh.Enabled = True
    End Sub

    Private Sub LoadSummary()
        Dim i As Integer
        Dim lssql As String
        Dim lncnt As Long
        Dim lnPostCnt As Long
        Dim lnPreDECnt As Long

        btnRefresh.Enabled = False

        objdt.Rows.Clear()
        objdt.Columns.Clear()

        objdt.Columns.Add("Particulars")
        objdt.Columns.Add("Total Count")
        objdt.Columns.Add("Query")

        i = -1

        'Total Records In Finone (Post)
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)
        lnPostCnt = lncnt

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "As Per Post Conversion"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        ' Presentable Data Entry Done
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)
        lnPreDECnt = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Presentation Data Entry Done"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Post Conversion Break Up"

        ' Presentation Not Available in Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Available in VMS and Matched in Presentation Data Entry"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Presentation Not Available in Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        'lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        'lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Available in VMS and Not Matched With Presentation Data Entry"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Not Available in VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entrygid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Not Available in VMS"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        ' Total
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Total"
        objdt.Rows(i)(1) = lnPostCnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        ' Presentation Breakup
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Presentation Data Entry Breakup"

        ' Presentable Available in Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Available in Post Conversion"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Presentable Not Available in Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Not Available in Post Conversion"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Total"
        objdt.Rows(i)(1) = lnPreDECnt
        objdt.Rows(i)(2) = lssql


        dgvsummary.DataSource = objdt

        dgvsummary.Columns(0).Width = 300
        dgvsummary.Columns(1).Width = 50
        dgvsummary.Columns(2).Visible = False

        btnRefresh.Enabled = True
    End Sub

    Private Sub LoadRecon()
        Dim i As Integer
        Dim lssql As String
        Dim lncnt As Long
        Dim lnTotVMSBaseCnt As Long = 0

        btnRefresh.Enabled = False

        objdt.Rows.Clear()
        objdt.Columns.Clear()

        objdt.Columns.Add("Description")
        objdt.Columns.Add("DE Base")
        objdt.Columns.Add("DE Base Query")
        objdt.Columns.Add("Post Conv Base")
        objdt.Columns.Add("Post Conv Base Query")
        objdt.Columns.Add("VMS Base")
        objdt.Columns.Add("VMS Base Query")

        i = -1

        'Available in all 3
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available in all 3"

        ' DE Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Post Conv Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(3) = lncnt
        objdt.Rows(i)(4) = lssql

        ' VMS Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(5) = lncnt
        objdt.Rows(i)(6) = lssql

        lnTotVMSBaseCnt += lncnt

        ' Available in DE & VMS
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available in DE & VMS"

        ' DE Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' VMS Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(5) = lncnt
        objdt.Rows(i)(6) = lssql

        lnTotVMSBaseCnt += lncnt

        ' Available in Post Conv & VMS (Same Cycle Date)
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available in Post Conv & VMS (Same Cycle Date)"

        ' Post Conv Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(3) = lncnt
        objdt.Rows(i)(4) = lssql

        ' VMS Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(5) = lncnt
        objdt.Rows(i)(6) = lssql

        lnTotVMSBaseCnt += lncnt

        ' Available in Post Conv & VMS
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available in Post Conv & VMS"

        ' Post Conv Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_entrygid = 0 "
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(i)(3) = lncnt
        objdt.Rows(i)(4) = Replace(lssql, "count(*)", "*")

        ' Available in Post Conv & VMS
        i += 1
        objdt.Rows.Add()

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Total"

        ' DE Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " almaraentry_cupboardno as 'Cup Board No',"
        lssql &= " almaraentry_shelfno as 'Shelf No',"
        lssql &= " almaraentry_boxno as 'Box No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',chq_accno as 'A/C No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_iscts as 'CTS Flag',chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid = packet_box_gid"
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Post Conv Base
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(i)(3) = lncnt
        objdt.Rows(i)(4) = Replace(lssql, "count(*)", "*")

        ' VMS Base
        objdt.Rows(i)(5) = lnTotVMSBaseCnt

        ' grid property settings
        dgvsummary.DataSource = objdt

        dgvsummary.Columns(0).Width = 300

        dgvsummary.Columns(1).Width = 150
        dgvsummary.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgvsummary.Columns(2).Visible = False

        dgvsummary.Columns(3).Width = 150
        dgvsummary.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgvsummary.Columns(4).Visible = False

        dgvsummary.Columns(5).Width = 150
        dgvsummary.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgvsummary.Columns(6).Visible = False

        btnRefresh.Enabled = True
    End Sub

    Private Sub LoadDataOld()
        Dim lssql As String
        Dim lncnt As Long

        btnRefresh.Enabled = False
        CreateTableOld()

        'Total Records In Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(0)(1) = lncnt
        objdt.Rows(0)(2) = Replace(lssql, "count(*)", "*")

        'Not Available In VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entrygid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(1)(1) = lncnt
        objdt.Rows(1)(2) = Replace(lssql, "count(*)", "*")

        'Overall Total
        objdt.Rows(2)(1) = Val(objdt.Rows(0)(1)) - Val(objdt.Rows(1)(1))

        'Matched With VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & 512 > 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(4)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & 512 > 0 "

        objdt.Rows(4)(2) = Replace(lssql, "count(*)", "*")

        'Not Matched With Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT Or GCMATCHFINONE) & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(5)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        'lssql &= " and chq_status & 512 = 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT Or GCMATCHFINONE) & " = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(5)(2) = Replace(lssql, "count(*)", "*")

        'As Per VMS
        objdt.Rows(6)(1) = Val(objdt.Rows(4)(1)) + Val(objdt.Rows(5)(1))

        'Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(8)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(8)(2) = Replace(lssql, "count(*)", "*")

        'Not Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(9)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT) & " = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(9)(2) = Replace(lssql, "count(*)", "*")

        'As Per VMS
        objdt.Rows(10)(1) = Val(objdt.Rows(8)(1)) + Val(objdt.Rows(9)(1))


        'To be Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        'lssql &= " and chq_status & " & GCENTRY & " > 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCCLOSURE Or GCPULLOUT) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(12)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCCLOSURE Or GCPULLOUT) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(12)(2) = Replace(lssql, "count(*)", "*")

        'Packet Pullout
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(13)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(13)(2) = Replace(lssql, "count(*)", "*")


        'Closure
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCCLOSURE & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(14)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCCLOSURE & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(14)(2) = Replace(lssql, "count(*)", "*")

        'Pullout
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
        lssql &= " and chq_status & " & GCPULLOUT & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(15)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
        lssql &= " and chq_status & " & GCPULLOUT & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(15)(2) = Replace(lssql, "count(*)", "*")

        objdt.Rows(16)(1) = Val(objdt.Rows(12)(1)) + Val(objdt.Rows(13)(1)) + Val(objdt.Rows(14)(1)) + Val(objdt.Rows(15)(1))

        'Despatched 
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCDESPATCH & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(18)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCDESPATCH & " > 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(18)(2) = Replace(lssql, "count(*)", "*")

        'To be Despatched 
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCDESPATCH & " = 0 "
        lssql &= " and chq_status & " & GCPRESENTATIONDE & " > 0"
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(19)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCDESPATCH & " = 0 "
        lssql &= " and chq_status & " & GCPRESENTATIONDE & " > 0"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(19)(2) = Replace(lssql, "count(*)", "*")

        'To be Data Entered 
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
        lssql &= " and chq_status & " & GCPRESENTATIONDE & " = 0"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(20)(1) = lncnt

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where true "
        lssql &= " " & gsCondDate & " "
        lssql &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
        lssql &= " and chq_status & " & GCPRESENTATIONDE & " = 0"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(20)(2) = Replace(lssql, "count(*)", "*")

        objdt.Rows(21)(1) = Val(objdt.Rows(18)(1)) + Val(objdt.Rows(19)(1)) + Val(objdt.Rows(20)(1))

        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone as f "
        lssql &= " left join chola_trn_tfinonepreconverfile as p on p.finone_entry_gid = f.finone_entrygid "
        lssql &= " where true "
        lssql &= " " & gsCondFinoneDate & " "
        lssql &= " and f.finone_entrygid > 0 "
        lssql &= " and p.finone_entry_gid is null "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(23)(1) = lncnt
        objdt.Rows(23)(2) = Replace(lssql, "count(*)", "f.*")

        dgvsummary.DataSource = objdt

        dgvsummary.Columns(0).Width = 300
        dgvsummary.Columns(1).Width = 50
        dgvsummary.Columns(2).Visible = False

        btnRefresh.Enabled = True
    End Sub

    Private Sub CreateTableOld()
        objdt.Rows.Clear()
        objdt.Columns.Clear()

        objdt.Columns.Add("Particulars")
        objdt.Columns.Add("Total Count")
        objdt.Columns.Add("Query")

        For i As Integer = 0 To 25
            objdt.Rows.Add()
        Next

        objdt.Rows(0)(0) = "As Per Finone"
        objdt.Rows(1)(0) = "Not Available In VMS"
        objdt.Rows(2)(0) = "Balance"

        objdt.Rows(3)(0) = ""
        objdt.Rows(4)(0) = "Matched With VMS"
        objdt.Rows(5)(0) = "Not Available in Finone"
        objdt.Rows(6)(0) = "As Per VMS"

        objdt.Rows(7)(0) = ""
        objdt.Rows(8)(0) = "Batched"
        objdt.Rows(9)(0) = "Not Batched"
        objdt.Rows(10)(0) = "As Per VMS"

        objdt.Rows(11)(0) = ""
        objdt.Rows(12)(0) = "To be Batched"
        objdt.Rows(13)(0) = "Packet Pullout"
        objdt.Rows(14)(0) = "Closure"
        objdt.Rows(15)(0) = "Pullout"

        objdt.Rows(17)(0) = ""
        objdt.Rows(18)(0) = "Despatched"
        objdt.Rows(19)(0) = "To be Despatched"
        objdt.Rows(20)(0) = "To be Data Entered"
        objdt.Rows(21)(0) = ""

        objdt.Rows(23)(0) = "Available In VMS Post & Not Availble in Pre"
    End Sub

    Private Sub frmFinoneRecon_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With cboRecon
            .Items.Clear()
            .Items.Add("Summary")
            .Items.Add("Recon")
            .Items.Add("Detail")
        End With

        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub frmFinoneRecon_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            .Left = pnlMain.Left
            .Top = pnlMain.Top + pnlMain.Height + 6
            .Height = Me.Height - .Top - 40
            .Width = Me.Width - 20
        End With
    End Sub

    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        Dim lsqry As String

        If e.RowIndex < 0 And e.ColumnIndex < 1 Then
            Exit Sub
        End If

        lsqry = dgvsummary.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value.ToString

        If lsqry <> "" Then
            QuickView(gOdbcConn, lsqry)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rdoDaily_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDaily.CheckedChanged
        dtpcycledate.CustomFormat = "dd-MM-yyyy"
    End Sub

    Private Sub rdoMonthly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoMonthly.CheckedChanged
        dtpcycledate.CustomFormat = "MMM-yyyy"
    End Sub
End Class
