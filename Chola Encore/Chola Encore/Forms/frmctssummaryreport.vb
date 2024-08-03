Public Class frmctssummaryreport
    Private Sub frmctssummaryreport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmctssummaryreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmctssummaryreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtgnsarefno.Focus()
        txtgnsarefno.Text = ""
        cbotype.SelectedIndex = 0
    End Sub

    Private Sub frmctssummaryreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub LoadDataOld()
        Dim ds As New DataSet
        Dim lsSql As String

        Select Case cbotype.Text
            Case "PDC"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',pdc_branchname as 'Branch Name',packet_mode as 'Mode',pdc_bankname as 'Bank Name',"
                lsSql &= " pdc_bankbranch as 'Bank Branch',pdc_micrcode as 'MICR No',packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chq_iscts='Y',1,0)) as 'CTS',sum(if(chq_iscts='N',1,0)) as 'Non CTS',sum(if(chq_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tpdcentry  c on chq_packet_gid=packet_gid "
                lsSql &= " inner join chola_trn_tpdcfile d on d.pdc_gid=c.chq_pdc_gid "
                lsSql &= " where true "

                'lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
                'lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                'lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
                'lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                'lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no,pdc_branchname,packet_mode,pdc_bankname,pdc_bankbranch,pdc_micrcode "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "SPDC"
                lsSql = ""
                lsSql &= " select almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',"
                lsSql &= " almaraentry_boxno as 'Box No',packet_gnsarefno as 'GNSA REF#',spdcentry_spdccount as 'Total Cheque',"
                lsSql &= " if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount) as 'CTS',spdcentry_nonctschqcount as 'Non CTS',"
                lsSql &= " if(if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount)=0 and spdcentry_nonctschqcount=0,spdcentry_spdccount,0) as 'Audit Pending' "
                lsSql &= " from chola_trn_tpacket "
                lsSql &= " inner join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
                lsSql &= " inner join chola_trn_tspdcentry on spdcentry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                lsSql &= " group by packet_gnsarefno,almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,spdcentry_spdccount "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If

                lsSql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "
            Case "SPDC NEW"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',pdc_branchname as 'Branch Name',packet_mode as 'Mode',pdc_bankname as 'Bank Name',"
                lsSql &= " pdc_bankbranch as 'Bank Branch',pdc_micrcode as 'MICR No',packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chqentry_iscts='Y',1,0)) as 'CTS',sum(if(chqentry_iscts='N',1,0)) as 'Non CTS',sum(if(chqentry_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tspdcchqentry c on chqentry_packet_gid=packet_gid "
                lsSql &= " inner join chola_trn_tpdcfile d on d.pdc_gid=c.chqentry_pdc_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no,pdc_branchname,packet_mode,pdc_bankname,pdc_bankbranch,pdc_micrcode "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case Else
                Exit Sub
        End Select

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String

        Select Case cbotype.Text
            Case "PDC"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',"
                lsSql &= " packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',sum(if(chq_iscts='Y',1,0)) as 'CTS',sum(if(chq_iscts='N',1,0)) as 'Non CTS',sum(if(chq_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tpdcentry  c on chq_packet_gid=packet_gid "
                lsSql &= " where true "

                lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
                lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
                lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
                    lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no"

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "PDC NEW"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',chq_bankbranch as 'Branch Name',packet_mode as 'Mode',chq_bankname as 'Bank Name',"
                lsSql &= " chq_bankbranch as 'Bank Branch',chq_accno as 'A/C No',chq_micrcode as 'MICR No',"
                lsSql &= " packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',sum(if(chq_iscts='Y',1,0)) as 'CTS',sum(if(chq_iscts='N',1,0)) as 'Non CTS',sum(if(chq_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tpdcentry  c on chq_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
                    lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no,chq_bankbranch,chq_bankname,chq_accno,chq_micrcode "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "PDC DATEWISE"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " date_format(packet_receiveddate,'%d-%m-%Y') as 'Received Date',"
                lsSql &= " count(distinct packet_gid) as 'Pouch Count',"
                lsSql &= " count(*) as 'Total Cheque',sum(if(chq_iscts='Y',1,0)) as 'CTS',sum(if(chq_iscts='N',1,0)) as 'Non CTS',sum(if(chq_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_trn_tpdcentry  c on chq_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
                    lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
                End If

                lsSql &= " group by date_format(packet_receiveddate,'%d-%m-%Y') "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "PDC SUMMARY"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(*) as 'Total Cheque',sum(if(chq_iscts='Y',1,0)) as 'CTS',sum(if(chq_iscts='N',1,0)) as 'Non CTS',sum(if(chq_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tpdcentry  c on chq_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
                    lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
                End If

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "PDC OLD"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',pdc_branchname as 'Branch Name',packet_mode as 'Mode',pdc_bankname as 'Bank Name',"
                lsSql &= " pdc_bankbranch as 'Bank Branch',pdc_micrcode as 'MICR No',packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chq_iscts='Y',1,0)) as 'CTS',sum(if(chq_iscts='N',1,0)) as 'Non CTS',sum(if(chq_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tpdcentry  c on chq_packet_gid=packet_gid "
                lsSql &= " inner join chola_trn_tpdcfile d on d.pdc_gid=c.chq_pdc_gid "
                lsSql &= " where true "

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
                    lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
                End If

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no,pdc_branchname,packet_mode,pdc_bankname,pdc_bankbranch,pdc_micrcode "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "SPDC"
                lsSql = ""
                lsSql &= " select almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',"
                lsSql &= " almaraentry_boxno as 'Box No',packet_gnsarefno as 'GNSA REF#',spdcentry_spdccount as 'Total Cheque',"
                lsSql &= " if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount) as 'CTS',spdcentry_nonctschqcount as 'Non CTS',"
                lsSql &= " if(if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount)=0 and spdcentry_nonctschqcount=0,spdcentry_spdccount,0) as 'Audit Pending' "
                lsSql &= " from chola_trn_tpacket "
                lsSql &= " inner join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
                lsSql &= " inner join chola_trn_tspdcentry on spdcentry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCCLOSURE & " = 0 "
                End If

                lsSql &= " group by packet_gnsarefno,almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,spdcentry_spdccount "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If

                lsSql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno "
            Case "SPDC NEW"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chqentry_iscts='Y',1,0)) as 'CTS',sum(if(chqentry_iscts='N',1,0)) as 'Non CTS',sum(if(chqentry_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tspdcchqentry c on chqentry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCCLOSURE & " = 0 "
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no"

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "SPDC DATEWISE"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " date_format(packet_receiveddate,'%d-%m-%Y') as 'Received Date',"
                lsSql &= " count(distinct packet_gid) as 'Pouch Count',count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chqentry_iscts='Y',1,0)) as 'CTS',sum(if(chqentry_iscts='N',1,0)) as 'Non CTS',sum(if(chqentry_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_trn_tspdcchqentry c on chqentry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCCLOSURE & " = 0 "
                End If

                lsSql &= " group by date_format(packet_receiveddate,'%d-%m-%Y')"

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "SPDC ECS DATEWISE"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " date_format(packet_receiveddate,'%d-%m-%Y') as 'Received Date',"
                lsSql &= " count(distinct packet_gid) as 'Pouch Count',count(*) as 'Total Ecs' "
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_trn_tecsemientry c on ecsemientry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                lsSql &= " group by date_format(packet_receiveddate,'%d-%m-%Y')"
            Case "SPDC ECS SUMMARY"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(distinct packet_gid) as 'Pouch Count',count(*) as 'Total Ecs' "
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_trn_tecsemientry c on ecsemientry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If
            Case "SPDC SUMMARY"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chqentry_iscts='Y',1,0)) as 'CTS',sum(if(chqentry_iscts='N',1,0)) as 'Non CTS',sum(if(chqentry_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tspdcchqentry c on chqentry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCCLOSURE & " = 0 "
                End If

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "SPDC SUMM"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',max(chqentry_branchname) as 'Branch Name',packet_mode as 'Mode',max(chqentry_bankname) as 'Bank Name',"
                lsSql &= " max(chqentry_branchname) as 'Bank Branch',max(chqentry_micrcode) as 'MICR No',packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chqentry_iscts='Y',1,0)) as 'CTS',sum(if(chqentry_iscts='N',1,0)) as 'Non CTS',sum(if(chqentry_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tspdcchqentry c on chqentry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no"

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case "SPDC DETAIL"
                lsSql = ""
                lsSql &= " select "
                lsSql &= " agreement_no as 'Agreement No',chqentry_branchname as 'Branch Name',packet_mode as 'Mode',chqentry_bankname as 'Bank Name',"
                lsSql &= " chqentry_branchname as 'Bank Branch',chqentry_accno as 'A/C No',chqentry_micrcode as 'MICR No',packet_gnsarefno as 'GNSA REF#',count(*) as 'Total Cheque',"
                lsSql &= " sum(if(chqentry_iscts='Y',1,0)) as 'CTS',sum(if(chqentry_iscts='N',1,0)) as 'Non CTS',sum(if(chqentry_iscts is null,1,0)) as 'Audit Pending'"
                lsSql &= " from chola_trn_tpacket a "
                lsSql &= " inner join chola_mst_tagreement b on agreement_gid=packet_agreement_gid "
                lsSql &= " inner join chola_trn_tspdcchqentry c on chqentry_packet_gid=packet_gid "
                lsSql &= " where true "

                If dtpfrom.Checked Then
                    lsSql &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
                End If

                If dtpto.Checked Then
                    lsSql &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
                End If

                If txtgnsarefno.Text.Trim <> "" Then
                    lsSql &= " and packet_gnsarefno='" & txtgnsarefno.Text.Trim & "'"
                End If

                If chkCurrStock.Checked = True Then
                    lsSql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                    lsSql &= " and chqentry_status & " & GCCLOSURE & " = 0 "
                End If

                lsSql &= " group by packet_gnsarefno,agreement_no,chqentry_branchname,chqentry_bankname,chqentry_accno,chqentry_micrcode "

                If cbostatus.Text = "Audit Completed" Then
                    lsSql &= " having `Audit Pending`=0 "
                ElseIf cbostatus.Text = "Audit Pending" Then
                    lsSql &= " having `Audit Pending`>0 "
                End If
            Case Else
                Exit Sub
        End Select

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtgnsarefno.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
        cbostatus.SelectedIndex = -1
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub
End Class