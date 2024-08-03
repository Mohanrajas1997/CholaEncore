Public Class frmFinoneRecon
    Dim objdt As New DataTable

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim i As Integer
        Dim lssql As String
        Dim lncnt As Long

        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        btnRefresh.Enabled = False

        objdt.Rows.Clear()
        objdt.Columns.Clear()

        objdt.Columns.Add("Particulars")
        objdt.Columns.Add("Total Count")
        objdt.Columns.Add("Query")

        i = -1

        'Total Records In Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "As Per Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Not Available In VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entrygid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Post Not Available In VMS"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Available
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entrygid > 0 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Balance"
        objdt.Rows(i)(1) = Val(objdt.Rows(i - 2)(1)) - Val(objdt.Rows(i - 1)(1))
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        i += 1
        objdt.Rows.Add()

        'Total Records In Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "As Per Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Not Available In VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile "
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entry_gid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Pre Not Available In VMS"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        'Available
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entry_gid > 0 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Balance"
        objdt.Rows(i)(1) = Val(objdt.Rows(i - 2)(1)) - Val(objdt.Rows(i - 1)(1))
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        i += 1
        objdt.Rows.Add()

        ' Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Batched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Matched With Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched With Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Not Matched With Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Not Matched With Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        'Matched With Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched With Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Not Matched With Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',batch_displayno as 'Batch No',type_name as 'Product Type' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Not Matched With Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        'Matched With Pre Not Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT Or GCMATCHFINONE) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT Or GCMATCHFINONE) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched With Pre Not Batched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Matched With Post Not Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT Or GCMATCHFINONE) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT Or GCMATCHFINONE) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched With Post Not Batched"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        'Matched With Pre Non Presentable
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT Or GCMATCHFINONE) & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT) & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched With Pre Non Presentable"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        'Matched With Post Non Presentable
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT) & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_type = 1 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',"
        lssql &= " agreement_closeddate as 'Closed Date',"
        lssql &= " pullout_insertdate as 'Pullout Date',"
        lssql &= " packetpullout_postedon as 'Packet Pullout',"
        lssql &= " reason_name as 'Pullout Reason',"
        lssql &= " packetpullout_reason as 'Packet Pullout Reason',"
        lssql &= " chq_desc as 'Description' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tpullout on pullout_entrygid = entry_gid "
        lssql &= " left join chola_mst_tpulloutreason on reason_gid = pullout_reasongid "
        lssql &= " left join chola_trn_tpacketpullout on packetpullout_packet_gid = packet_gid "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONE & " > 0 "
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT) & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Matched With Post Non Presentable"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        ' Available In Post & Not Availble in Pre
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone as f "
        lssql &= " left join chola_trn_tfinonepreconverfile as p on p.finone_entry_gid = f.finone_entrygid "
        lssql &= " where f.finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and f.finone_entrygid > 0 "
        lssql &= " and p.finone_entry_gid is null "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available In VMS Post & Not Availble in Pre"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "f.*")

        i += 1
        objdt.Rows.Add()

        ' Available In Pre & Not Availble in Post
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile as p "
        lssql &= " left join chola_trn_tfinone as f on f.finone_entrygid = p.finone_entry_gid "
        lssql &= " where p.finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and p.finone_entry_gid > 0 "
        lssql &= " and f.finone_entrygid is null "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Available In VMS Pre & Not Availble in Post"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "p.*")

        dgvsummary.DataSource = objdt

        dgvsummary.Columns(0).Width = 300
        dgvsummary.Columns(1).Width = 50
        dgvsummary.Columns(2).Visible = False

        btnRefresh.Enabled = True
    End Sub

    Private Sub LoadDataOld()
        Dim lssql As String
        Dim lncnt As Long

        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        btnRefresh.Enabled = False
        CreateTableOld()

        'Total Records In Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(0)(1) = lncnt
        objdt.Rows(0)(2) = Replace(lssql, "count(*)", "*")

        'Not Available In VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinone"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & 512 > 0 "

        objdt.Rows(4)(2) = Replace(lssql, "count(*)", "*")

        'Not Matched With Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(8)(2) = Replace(lssql, "count(*)", "*")

        'Not Batched
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCCLOSURE Or GCPULLOUT) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(12)(2) = Replace(lssql, "count(*)", "*")

        'Packet Pullout
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(13)(2) = Replace(lssql, "count(*)", "*")


        'Closure
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCCLOSURE & " > 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(14)(2) = Replace(lssql, "count(*)", "*")

        'Pullout
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCDESPATCH & " > 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(18)(2) = Replace(lssql, "count(*)", "*")

        'To be Despatched 
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCDESPATCH & " = 0 "
        lssql &= " and chq_status & " & GCPRESENTATIONDE & " > 0"
        lssql &= " and chq_batch_gid > 0 "
        lssql &= " and chq_type = 1 "

        objdt.Rows(19)(2) = Replace(lssql, "count(*)", "*")

        'To be Data Entered 
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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
        lssql &= " where f.finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
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

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        lsqry = dgvsummary.Rows(e.RowIndex).Cells(2).Value.ToString

        If lsqry <> "" Then
            QuickView(gOdbcConn, lsqry)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
