Public Class frmfinonepreconverrecon
    Dim objdt As New DataTable

    Private Sub LoadDetail()
        Dim lssql As String
        Dim lncnt As Long


        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        btnRefresh.Enabled = False
        CreateTable()

        'Total Records In Finone
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(0)(1) = lncnt
        objdt.Rows(0)(2) = Replace(lssql, "count(*)", "*")

        'Not Available In VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entry_gid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(1)(1) = lncnt
        objdt.Rows(1)(2) = Replace(lssql, "count(*)", "*")

        'Overall Total
        objdt.Rows(2)(1) = Val(objdt.Rows(0)(1)) - Val(objdt.Rows(1)(1))

        'Matched With VMS
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status &" & GCMATCHFINONEPRECOVERFILE & "  > 0 "

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
        lssql &= " and chq_status &" & GCMATCHFINONEPRECOVERFILE & "  > 0 "
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(4)(2) = Replace(lssql, "count(*)", "*")

        'Not Matched With Finone
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status &" & GCMATCHFINONEPRECOVERFILE & "  = 0 "
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
        lssql &= " and chq_status &" & GCMATCHFINONEPRECOVERFILE & "  = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(5)(2) = Replace(lssql, "count(*)", "*")

        'As Per VMS
        objdt.Rows(6)(1) = Val(objdt.Rows(4)(1)) + Val(objdt.Rows(5)(1))

        'Batched
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
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
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(8)(2) = Replace(lssql, "count(*)", "*")

        'Not Batched
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_batch_gid = 0 "
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
        lssql &= " and chq_type = 1 "
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(9)(2) = Replace(lssql, "count(*)", "*")

        'As Per VMS
        objdt.Rows(10)(1) = Val(objdt.Rows(8)(1)) + Val(objdt.Rows(9)(1))


        'To be Batched
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        'lssql &= " and chq_status & " & GCENTRY & " > 0 "
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCCLOSURE Or GCPULLOUT) & " = 0 "
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
        lssql &= " and chq_status & " & (GCPACKETPULLOUT Or GCCLOSURE Or GCPULLOUT) & " = 0 "
        lssql &= " and chq_batch_gid = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(12)(2) = Replace(lssql, "count(*)", "*")

        'Packet Pullout
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
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
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(13)(2) = Replace(lssql, "count(*)", "*")


        'Closure
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
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
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(14)(2) = Replace(lssql, "count(*)", "*")

        'Pullout
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
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
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(15)(2) = Replace(lssql, "count(*)", "*")

        objdt.Rows(16)(1) = Val(objdt.Rows(12)(1)) + Val(objdt.Rows(13)(1)) + Val(objdt.Rows(14)(1)) + Val(objdt.Rows(15)(1))

        'Despatched 
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
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
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(18)(2) = Replace(lssql, "count(*)", "*")

        'To be Despatched 
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
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
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(19)(2) = Replace(lssql, "count(*)", "*")

        'To be Data Entered 
        lssql = ""
        lssql &= " select count(distinct chq_agreement_gid,chq_no) "
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
        lssql &= " group by chq_agreement_gid,chq_no"

        objdt.Rows(20)(2) = Replace(lssql, "count(*)", "*")

        objdt.Rows(21)(1) = Val(objdt.Rows(18)(1)) + Val(objdt.Rows(19)(1)) + Val(objdt.Rows(20)(1))

        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile as p "
        lssql &= " left join chola_trn_tfinone as f on f.finone_entrygid = p.finone_entry_gid "
        lssql &= " where p.finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and p.finone_entry_gid > 0 "
        lssql &= " and f.finone_entrygid is null "
        lssql &= " and p.finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        objdt.Rows(23)(1) = lncnt
        objdt.Rows(23)(2) = Replace(lssql, "count(*)", "p.*")

        dgvsummary.DataSource = objdt

        dgvsummary.Columns(0).Width = 300
        dgvsummary.Columns(1).Width = 50
        dgvsummary.Columns(2).Visible = False

        btnRefresh.Enabled = True
    End Sub
    Private Sub CreateTable()
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

        objdt.Rows(23)(0) = "Available In VMS Pre & Not Availble in Post"
    End Sub

    Private Sub frmFinoneRecon_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With cboRecon
            .Items.Clear()
            .Items.Add("Summary")
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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        btnRefresh.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        Select Case cboRecon.Text.ToUpper
            Case "DETAIL"
                Call LoadDetail()
            Case Else
                Call LoadSummary()
        End Select

        dgvsummary.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Me.Cursor = Cursors.Default
        btnRefresh.Enabled = True
    End Sub

    Private Sub LoadSummary()
        Dim i As Integer
        Dim lssql As String
        Dim lncnt As Long
        Dim lnPreConvCnt As Long
        Dim lnPreDECnt As Long

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

        'Total Records In Finone (PreConv)
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)
        lnPreConvCnt = lncnt

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "As Per Pre Conversion"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        ' As per VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        'lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        'lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "As Per VMS"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "Pre Conversion Break Up"

        ' Available in VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entry_gid > 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Matched With VMS"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        ' Not Available in VMS
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tfinonepreconverfile"
        lssql &= " where finone_chqdate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and finone_deleteflag='N' "
        lssql &= " and finone_entry_gid = 0 "

        lncnt = gfExecuteScalar(lssql, gOdbcConn)

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Not Matched With VMS"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = Replace(lssql, "count(*)", "*")

        ' Total
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Total"
        objdt.Rows(i)(1) = lnPreConvCnt
        objdt.Rows(i)(2) = lssql

        i += 1
        objdt.Rows.Add()

        ' Presentation Breakup
        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "VMS Breakup"

        ' Presentable With Pre Conversion
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Presentable Available Pre Conversion"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Non Presentable with Pre Conversion
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " > 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Non Presentable Available in Pre Conversion (Pullout/Closure)"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Presentable Not Matched With Pre Conversion
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " = 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Presentable Not Available in Pre Conversion"
        objdt.Rows(i)(1) = lncnt
        objdt.Rows(i)(2) = lssql

        ' Non Presentable Not Matched with Pre Conversion
        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
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
        lssql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & (GCCLOSURE Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPACKETPULLOUT) & " > 0 "
        'lssql &= " and chq_status & " & (GCPRESENTATIONDE) & " > 0 "
        lssql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "
        lssql &= " and chq_type = 1 "
        lssql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "

        i += 1
        objdt.Rows.Add()
        objdt.Rows(i)(0) = "    Non Presentable Not Available in Pre Conversion (Pullout/Closure)"
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
End Class