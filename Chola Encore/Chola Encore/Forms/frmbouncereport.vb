Public Class frmbouncereport
    Private Sub frmbouncereport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmbouncereport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmbouncereport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtAgreementNo.Focus()
        txtAgreementNo.Text = ""
        lssql = " select reason_gid,reason_name from chola_mst_tbouncereason where reason_deleteflag='N' "
        gpBindCombo(lssql, "reason_name", "reason_gid", cboreason, gOdbcConn)
        cboreason.SelectedIndex = -1
    End Sub

    Private Sub frmbouncereport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

        If cbostatus.Text <> "Not in Dump" Then
            lsSql = ""
            lsSql &= " select bounceentry_slno as 'Slno',almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',"
            lsSql &= " almaraentry_boxno as 'Box No',agreement_no as 'Agreement No',bounce_chqno as 'Cheque No',"
            lsSql &= "date_format(bounce_chqdate,'%d-%m-%Y') as 'Cheque Date',"
            lsSql &= " bounce_chqamount as 'Cheque Amount',b.reason_name as 'Dump Bounce Reason',bounce_awbno as 'AWB No',"
            lsSql &= "a.reason_name as 'Entry Bounce Reason',bounceentry_remarks as 'Remarks',if(bounceentry_entry_gid=0,'Not Matched With PDC',"
            lsSql &= "if(bounceentry_entry_gid is null,'','Matched With PDC')) as 'PDC Remarks',"
            lsSql &= " if(bounceentry_pullout_gid=0,'Pullout Not Done',if(bounceentry_pullout_gid is null,'','Pullout Done'))"
            lsSql &= " as 'Pullout Remarks',date_format(bounceentry_insertdate,'%d-%m-%Y') as 'Received Date',bounceentry_insertby as 'Received By', "
            lsSql &= " if(bounce_isentry ='Y','Entry Completed','Entry Pending') as 'Entry Status' "
            lsSql &= " from chola_trn_tbounce "
            lsSql &= " left join chola_trn_tbounceentry on bounceentry_bounce_gid=bounce_gid "
            lsSql &= " left join chola_mst_tbouncereason a on a.reason_gid=bounceentry_bouncereason_gid "
            lsSql &= " inner join chola_mst_tbouncereason b on b.reason_gid=bounce_reason_gid "
            lsSql &= " left join chola_mst_tagreement on agreement_gid=bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_tbounceinward on inward_gid=bounceentry_inward_gid "
            lsSql &= " left join chola_trn_tpacket on packet_agreement_gid=bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
            lsSql &= " where 1=1 "

            If dtpfrom.Checked Then
                lsSql &= " and  date_format(bounce_returndate,'%Y-%m-%d') >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpto.Checked Then
                lsSql &= " and  date_format(bounce_returndate,'%Y-%m-%d') <='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            End If

            If txtAgreementNo.Text.Trim <> "" Then
                lsSql &= " and agreement_no='" & txtAgreementNo.Text.Trim & "'"
            End If

            If txtShortAgreementNo.Text.Trim <> "" Then
                lsSql &= " and shortagreement_no='" & txtShortAgreementNo.Text.Trim & "'"
            End If

            If txtchqno.Text.Trim <> "" Then
                lsSql &= " and bounce_chqno='" & txtchqno.Text.Trim & "'"
            End If

            If Val(txtamount.Text) > 0 Then
                lsSql &= " and bounce_chqamount='" & Val(txtamount.Text.Trim) & "'"
            End If

            If cboreason.Text.Trim <> "" Then
                lsSql &= " and bounceentry_bouncereason_gid=" & cboreason.SelectedValue
            End If

            If txtairwaybillno.Text.Trim <> "" Then
                lsSql &= " and bounce_awbno='" & txtairwaybillno.Text.Trim & "'"
            End If

            If cbostatus.Text.Trim <> "" Then
                If cbostatus.Text.Trim = "Pending" Then
                    lsSql &= " and bounce_isentry='N' "
                Else
                    lsSql &= " and bounce_isentry='Y' "
                End If
            End If

            lsSql &= " union "
            lsSql &= " select bounceentry_slno as 'Slno',almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No',agreement_no as 'Agreement No',bounceentry_chqno as 'Cheque No',date_format(bounceentry_chqdate,'%d-%m-%Y') as 'Cheque Date',"
            lsSql &= " bounceentry_chqamount as 'Cheque Amount','' as 'Dump Bounce Reason',inward_awbno as 'AWB No',reason_name as 'Entry Bounce Reason',bounceentry_remarks as 'Remarks',if(bounceentry_entry_gid=0,'Not Matched With PDC','Matched With PDC') as 'PDC Remarks',"
            lsSql &= " if(bounceentry_pullout_gid=0,'Pullout Not Done','Pullout Done') as 'Pullout Remarks',date_format(bounceentry_insertdate,'%d-%m-%Y') as 'Received Date',bounceentry_insertby as 'Received By','Not in Dump' as 'Entry Status' "
            lsSql &= " from chola_trn_tbounceentry "
            lsSql &= " inner join chola_mst_tbouncereason on reason_gid=bounceentry_bouncereason_gid "
            lsSql &= " left join chola_mst_tagreement on agreement_gid=bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_tbounceinward on inward_gid=bounceentry_inward_gid "
            lsSql &= " left join chola_trn_tpacket on packet_agreement_gid=bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
            lsSql &= " where 1=1 and bounceentry_bounce_gid=0 "

            If dtpfrom.Checked Then
                lsSql &= " and  date_format(bounceentry_insertdate,'%Y-%m-%d') >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpto.Checked Then
                lsSql &= " and  date_format(bounceentry_insertdate,'%Y-%m-%d') <='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            End If

            If txtShortAgreementNo.Text.Trim <> "" Then
                lsSql &= " and shortagreement_no='" & txtShortAgreementNo.Text.Trim & "'"
            End If

            If txtAgreementNo.Text.Trim <> "" Then
                lsSql &= " and agreement_no='" & txtAgreementNo.Text.Trim & "'"
            End If

            If txtchqno.Text.Trim <> "" Then
                lsSql &= " and bounceentry_chqno='" & txtchqno.Text.Trim & "'"
            End If

            If Val(txtamount.Text) > 0 Then
                lsSql &= " and bounceentry_chqamount='" & Val(txtamount.Text.Trim) & "'"
            End If

            If cboreason.Text.Trim <> "" Then
                lsSql &= " and bounceentry_bouncereason_gid=" & cboreason.SelectedValue
            End If

            If txtairwaybillno.Text.Trim <> "" Then
                lsSql &= " and inward_awbno='" & txtairwaybillno.Text.Trim & "'"
            End If

        Else
            lsSql = ""
            lsSql &= " select bounceentry_slno as 'Slno',almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No',agreement_no as 'Agreement No',bounceentry_chqno as 'Cheque No',date_format(bounceentry_chqdate,'%d-%m-%Y') as 'Cheque Date',"
            lsSql &= " bounceentry_chqamount as 'Cheque Amount',reason_name as 'Bounce Reason',bounceentry_remarks as 'Remarks',if(bounceentry_entry_gid=0,'Not Matched With PDC','Matched With PDC') as 'PDC Remarks',"
            lsSql &= " inward_couriername as 'Courier Name',inward_awbno as 'AWB No',"
            lsSql &= " if(bounceentry_pullout_gid=0,'Pullout Not Done','Pullout Done') as 'Pullout Remarks',date_format(bounceentry_insertdate,'%d-%m-%Y') as 'Received Date',bounceentry_insertby as 'Received By' "
            lsSql &= " from chola_trn_tbounceentry "
            lsSql &= " inner join chola_mst_tbouncereason on reason_gid=bounceentry_bouncereason_gid "
            lsSql &= " left join chola_mst_tagreement on agreement_gid=bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_tbounceinward on inward_gid=bounceentry_inward_gid "
            lsSql &= " left join chola_trn_tpacket on packet_agreement_gid=bounceentry_agreement_gid "
            lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
            lsSql &= " where 1=1 and bounceentry_bounce_gid=0 "

            If dtpfrom.Checked Then
                lsSql &= " and  date_format(bounceentry_insertdate,'%Y-%m-%d') >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpto.Checked Then
                lsSql &= " and  date_format(bounceentry_insertdate,'%Y-%m-%d') <='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            End If

            If txtAgreementNo.Text.Trim <> "" Then
                lsSql &= " and agreement_no='" & txtAgreementNo.Text.Trim & "'"
            End If

            If txtShortAgreementNo.Text.Trim <> "" Then
                lsSql &= " and shortagreement_no='" & txtShortAgreementNo.Text.Trim & "'"
            End If

            If txtchqno.Text.Trim <> "" Then
                lsSql &= " and bounceentry_chqno='" & txtchqno.Text.Trim & "'"
            End If

            If Val(txtamount.Text) > 0 Then
                lsSql &= " and bounceentry_chqamount='" & Val(txtamount.Text.Trim) & "'"
            End If

            If cboreason.Text.Trim <> "" Then
                lsSql &= " and bounceentry_bouncereason_gid=" & cboreason.SelectedValue
            End If

            If txtairwaybillno.Text.Trim <> "" Then
                lsSql &= " and inward_awbno='" & txtairwaybillno.Text.Trim & "'"
            End If

            lsSql &= " order by bounceentry_slno"
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

        lsSql = ""
        lsSql &= " select bounceentry_slno as 'Slno',almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No',agreement_no as 'Agreement No',bounceentry_chqno as 'Cheque No',date_format(bounceentry_chqdate,'%d-%m-%Y') as 'Cheque Date',"
        lsSql &= " bounceentry_chqamount as 'Cheque Amount',reason_name as 'Bounce Reason',bounceentry_remarks as 'Remarks',if(bounceentry_entry_gid=0,'Not Matched With PDC','Matched With PDC') as 'PDC Remarks',"
        lsSql &= " inward_couriername as 'Courier Name',inward_awbno as 'AWB No',"
        lsSql &= " if(bounceentry_pullout_gid=0,'Pullout Not Done','Pullout Done') as 'Pullout Remarks',date_format(bounceentry_insertdate,'%d-%m-%Y') as 'Received Date',bounceentry_insertby as 'Received By' "
        lsSql &= " from chola_trn_tbounceentry "
        lsSql &= " inner join chola_mst_tbouncereason on reason_gid=bounceentry_bouncereason_gid "
        lsSql &= " left join chola_mst_tagreement on agreement_gid=bounceentry_agreement_gid "
        lsSql &= " left join chola_trn_tbounceinward on inward_gid=bounceentry_inward_gid "
        lsSql &= " left join chola_trn_tpacket on packet_agreement_gid=bounceentry_agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " where 1=1  "

        lsSql &= " and  bounceentry_insertdate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and  bounceentry_insertdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"

        If txtAgreementNo.Text.Trim <> "" Then
            lsSql &= " and agreement_no='" & txtAgreementNo.Text.Trim & "'"
        End If

        If txtShortAgreementNo.Text.Trim <> "" Then
            lsSql &= " and shortagreement_no='" & txtShortAgreementNo.Text.Trim & "'"
        End If

        If txtchqno.Text.Trim <> "" Then
            lsSql &= " and bounceentry_chqno='" & txtchqno.Text.Trim & "'"
        End If

        If Val(txtamount.Text) > 0 Then
            lsSql &= " and bounceentry_chqamount='" & Val(txtamount.Text.Trim) & "'"
        End If

        If cboreason.Text.Trim <> "" Then
            lsSql &= " and bounceentry_bouncereason_gid=" & cboreason.SelectedValue
        End If

        If txtairwaybillno.Text.Trim <> "" Then
            lsSql &= " and inward_awbno='" & txtairwaybillno.Text.Trim & "'"
        End If

        Select Case cbostatus.Text
            Case "Received With Dump"
                lsSql &= " and bounceentry_bounce_gid > 0 "
            Case "Additionally Received"
                lsSql &= " and bounceentry_bounce_gid = 0 "
            Case "Pullout"
                lsSql &= " and bounceentry_pullout_gid > 0 "
            Case "Matched With Vault"
                lsSql &= " and bounceentry_entry_gid > 0 "
            Case "Not Matched With Vault"
                lsSql &= " and bounceentry_entry_gid = 0 "
        End Select

        lsSql &= " order by bounceentry_slno"

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
        txtAgreementNo.Text = ""
        txtchqno.Text = ""
        txtamount.Text = ""
        cboreason.SelectedIndex = -1
        cbostatus.SelectedIndex = -1
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
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