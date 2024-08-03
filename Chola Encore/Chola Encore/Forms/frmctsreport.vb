Public Class frmctsreport
    Private Sub frmctsreport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmctsreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmctsreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        With cbostatus
            .Items.Clear()
            .Items.Add("All")
            .Items.Add("Available")
            .Items.Add("Audit Completed")
            .Items.Add("Audit Pending")
        End With

        With cbotype
            .Items.Clear()
            .Items.Add("PDC")
            .Items.Add("SPDC")
        End With

        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtgnsarefno.Focus()
        txtgnsarefno.Text = ""

        cbotype.SelectedIndex = -1
    End Sub

    Private Sub frmctsreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

    Private Sub LoadData1()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lnResult As Long = 0

        lsCond = ""

        If dtpfrom.Checked Then lsCond &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "' "
        If dtpto.Checked Then lsCond &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        If txtgnsarefno.Text.Trim <> "" Then lsCond &= " and packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
        If cbotype.Text <> "" Then lsCond &= " and packet_mode = '" & cbotype.Text & "' "

        lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

        lsSql = ""
        lsSql &= " select @sno := @sno + 1 as 'SNo',"
        lsSql &= " a.almaraentry_cupboardno as 'Cupboard No',a.almaraentry_shelfno as 'Shelf No',"
        lsSql &= " a.almaraentry_boxno as 'Box No',a.packet_gnsarefno as 'GNSA REF#',a.agreement_no 'Agreement No',a.shortagreement_no 'Short Agreement No',"
        lsSql &= " sum(a.tot_chq) as 'Total Cheque',"
        lsSql &= " sum(a.cts_count) as 'CTS',"
        lsSql &= " sum(a.noncts_count) as 'Non CTS',"
        lsSql &= " sum(a.audit_pending) as 'Audit Pending',"
        lsSql &= " if(sum(a.tot_chq)=sum(a.cts_count),'CTS',if(sum(a.tot_chq)=sum(a.noncts_count),'Non CTS','Partial')) as 'Status' "
        lsSql &= " from ("
        lsSql &= " select almaraentry_cupboardno,almaraentry_shelfno,"
        lsSql &= " almaraentry_boxno,packet_gnsarefno,agreement_no,shortagreement_no,count(*) as tot_chq,"
        lsSql &= " sum(if(chq_iscts='Y',1,0)) as cts_count,sum(if(chq_iscts='N',1,0)) as noncts_count,sum(if(chq_iscts is null,1,0)) as audit_pending "
        lsSql &= " from chola_trn_tpacket "
        lsSql &= " inner join chola_mst_tagreement on packet_agreement_gid = agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " inner join chola_trn_tpdcentry on chq_packet_gid=packet_gid "
        lsSql &= " where true "
        lsSql &= lsCond

        If Not (cbostatus.Text = "All" Or cbostatus.Text = "") Then
            lsSql &= " and packet_status & " & GCAUTHENTRY & " > 0 "
            lsSql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
            lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
            lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
            lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
            lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
            lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
        End If

        lsSql &= " group by packet_gnsarefno "

        lsSql &= " union all "

        lsSql &= " select almaraentry_cupboardno,almaraentry_shelfno,"
        lsSql &= " almaraentry_boxno,packet_gnsarefno,agreement_no,shortagreement_no,spdcentry_spdccount as tot_chq,"
        lsSql &= " if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount) as cts_count,spdcentry_nonctschqcount as noncts_count,if(if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount)=0 and spdcentry_nonctschqcount=0,spdcentry_spdccount,0) as audit_pending "
        lsSql &= " from chola_trn_tpacket "
        lsSql &= " inner join chola_mst_tagreement on packet_agreement_gid = agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " inner join chola_trn_tspdcentry on spdcentry_packet_gid=packet_gid "
        lsSql &= " where true "

        If Not (cbostatus.Text = "All" Or cbostatus.Text = "") Then
            lsSql &= " and packet_status & " & GCAUTHENTRY & " > 0 "
            lsSql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
        End If

        lsSql &= lsCond
        lsSql &= " group by packet_gnsarefno"
        lsSql &= " ) as a group by packet_gnsarefno "

        'lsSql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno) as a "

        If cbostatus.Text = "Audit Completed" Then
            lsSql &= " having sum(a.audit_pending) = 0 "
        ElseIf cbostatus.Text = "Audit Pending" Then
            lsSql &= " having sum(a.audit_pending) > 0 "
        End If

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lnResult As Long = 0

        lsCond = ""

        If dtpfrom.Checked Then lsCond &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "' "
        If dtpto.Checked Then lsCond &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        If txtgnsarefno.Text.Trim <> "" Then lsCond &= " and packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
        If cbotype.Text <> "" Then lsCond &= " and packet_mode = '" & cbotype.Text & "' "

        lsSql = ""
        lsSql &= " select "
        lsSql &= " date_format(a.packet_receiveddate,'%d-%m-%Y') as 'Rcvd Date',"
        lsSql &= " a.packet_gnsarefno as 'GNSA REF#',a.packet_mode as 'Mode',a.agreement_no 'Agreement No',"
        lsSql &= " sum(a.tot_chq) as 'Total Cheque',"
        lsSql &= " sum(a.cts_count) as 'CTS',"
        lsSql &= " sum(a.noncts_count) as 'Non CTS',"
        lsSql &= " sum(a.audit_pending) as 'Audit Pending',"
        lsSql &= " if(sum(a.tot_chq)=sum(a.cts_count),'CTS',if(sum(a.tot_chq)=sum(a.noncts_count),'Non CTS','Partial')) as 'Status' "
        lsSql &= " from ("
        lsSql &= " select packet_receiveddate,packet_gnsarefno,packet_mode,agreement_no,count(*) as tot_chq,"
        lsSql &= " sum(if(chq_iscts='Y',1,0)) as cts_count,sum(if(chq_iscts='N',1,0)) as noncts_count,sum(if(chq_iscts is null,1,0)) as audit_pending "
        lsSql &= " from chola_trn_tpacket "
        lsSql &= " inner join chola_mst_tagreement on packet_agreement_gid = agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " inner join chola_trn_tpdcentry on chq_packet_gid=packet_gid "
        lsSql &= " where true "
        lsSql &= lsCond

        If Not (cbostatus.Text = "All" Or cbostatus.Text = "") Then
            lsSql &= " and packet_status & " & GCAUTHENTRY & " > 0 "
            lsSql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
            lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
            lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
            lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "
            lsSql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
            lsSql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
        End If

        lsSql &= " group by packet_gnsarefno "

        lsSql &= " union all "

        lsSql &= " select packet_receiveddate,packet_gnsarefno,packet_mode,agreement_no,spdcentry_spdccount as tot_chq,"
        lsSql &= " if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount) as cts_count,spdcentry_nonctschqcount as noncts_count,if(if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount)=0 and spdcentry_nonctschqcount=0,spdcentry_spdccount,0) as audit_pending "
        lsSql &= " from chola_trn_tpacket "
        lsSql &= " inner join chola_mst_tagreement on packet_agreement_gid = agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " inner join chola_trn_tspdcentry on spdcentry_packet_gid=packet_gid "
        lsSql &= " where true "

        If Not (cbostatus.Text = "All" Or cbostatus.Text = "") Then
            lsSql &= " and packet_status & " & GCAUTHENTRY & " > 0 "
            lsSql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
        End If

        lsSql &= lsCond
        lsSql &= " group by packet_gnsarefno"
        lsSql &= " ) as a group by packet_receiveddate,packet_gnsarefno "

        If cbostatus.Text = "Audit Completed" Then
            lsSql &= " having sum(a.audit_pending) = 0 "
        ElseIf cbostatus.Text = "Audit Pending" Then
            lsSql &= " having sum(a.audit_pending) > 0 "
        End If

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub LoadDataOld()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lnResult As Long = 0

        lsCond = ""

        If dtpfrom.Checked Then lsCond &= " and packet_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "' "
        If dtpto.Checked Then lsCond &= " and packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        If txtgnsarefno.Text.Trim <> "" Then lsCond &= " and packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "

        Select Case cbostatus.Text
            Case "All"
            Case Else
                If cbotype.Text = "PDC" Then
                    lsCond &= " and packet_status & " & GCAUTHENTRY & " > 0 "
                    lsCond &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
                    lsCond &= " and chq_status & " & GCPULLOUT & " = 0 "
                    lsCond &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
                    lsCond &= " and chq_status & " & GCCLOSURE & " = 0 "
                    lsCond &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
                    lsCond &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"
                Else
                    lsCond &= " and packet_status & " & GCAUTHENTRY & " > 0 "
                    lsCond &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
                End If
        End Select

        lnResult = gfInsertQry("set @sno := 0", gOdbcConn)

        If cbotype.Text = "PDC" Then
            lsSql = ""
            lsSql &= " select @sno := @sno + 1 as 'SNo',a.*, "
            lsSql &= " if(a.`Total Cheque`= a.CTS,'CTS',if(a.`Total Cheque`=a.`Non CTS`,'Non CTS','Partial')) as 'Status' "
            lsSql &= " from (select almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',"
            lsSql &= " almaraentry_boxno as 'Box No',packet_gnsarefno as 'GNSA REF#',shortagreement_no 'Short Agreement No',agreement_no 'Agreement No',count(*) as 'Total Cheque',"
            lsSql &= " sum(if(chq_iscts='Y',1,0)) as 'CTS',sum(if(chq_iscts='N',1,0)) as 'Non CTS',sum(if(chq_iscts is null,1,0)) as 'Audit Pending' "
            lsSql &= " from chola_trn_tpacket "
            lsSql &= " inner join chola_mst_tagreement on packet_agreement_gid = agreement_gid "
            lsSql &= " inner join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
            lsSql &= " inner join chola_trn_tpdcentry on chq_packet_gid=packet_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " group by packet_gnsarefno "
            lsSql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno) as a "
            lsSql &= " where true "

            If cbostatus.Text = "Audit Completed" Then
                lsSql &= " and `Audit Pending`=0 "
            ElseIf cbostatus.Text = "Audit Pending" Then
                lsSql &= " and `Audit Pending`>0 "
            End If
        Else
            lsSql = ""
            lsSql &= " select @sno := @sno + 1 as 'SNo',a.*,"
            lsSql &= " if(a.`Total Cheque`= a.CTS,'CTS',if(a.`Total Cheque`=a.`Non CTS`,'Non CTS','Partial')) as 'Status' "
            lsSql &= " from (select almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',"
            lsSql &= " almaraentry_boxno as 'Box No',packet_gnsarefno as 'GNSA REF#',shortagreement_no 'Short Agreement No',agreement_no 'Agreement No',spdcentry_spdccount as 'Total Cheque',"
            lsSql &= " if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount) as 'CTS',spdcentry_nonctschqcount as 'Non CTS',if(if(spdcentry_ctschqcount is null,0,spdcentry_ctschqcount)=0 and spdcentry_nonctschqcount=0,spdcentry_spdccount,0) as 'Audit Pending'"
            lsSql &= " from chola_trn_tpacket "
            lsSql &= " inner join chola_mst_tagreement on packet_agreement_gid = agreement_gid "
            lsSql &= " inner join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
            lsSql &= " inner join chola_trn_tspdcentry on spdcentry_packet_gid=packet_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " group by packet_gnsarefno "
            lsSql &= " order by almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno,packet_gnsarefno) as a "
            lsSql &= " where true "

            If cbostatus.Text = "Audit Completed" Then
                lsSql &= " and `Audit Pending`=0 "
            ElseIf cbostatus.Text = "Audit Pending" Then
                lsSql &= " and `Audit Pending`>0 "
            End If
        End If

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