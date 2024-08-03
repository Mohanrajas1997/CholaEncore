Public Class frmclosurereport
    Dim lssql As String
    Dim msReport As String

    Private Sub frmclosurereport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmclosurereport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmclosurereport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtAgreementNo.Focus()
        txtAgreementNo.Text = ""

        With cboReport
            .Items.Clear()
            .Items.Add("CLOSURE FILE")
            .Items.Add("CLOSURE MIS")
        End With
    End Sub

    Private Sub frmclosurereport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        If dtpfrom.Checked = True Then
            lsCond &= " and closure_date >= '" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "' "
        End If

        If dtpto.Checked = True Then
            lsCond &= " and closure_date < '" & Format(DateAdd(DateInterval.Day, 1, CDate(dtpto.Text)), "yyyy-MM-dd") & "' "
        End If

        If txtShortAgreementNo.Text <> "" Then
            lsCond &= " and shortagreement_no = '" & QuoteFilter(txtShortAgreementNo.Text) & "' "
        End If

        If txtAgreementNo.Text <> "" Then
            lsCond &= " and agreement_no = '" & QuoteFilter(txtAgreementNo.Text) & "' "
        End If

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = " "
        lsSql &= " SELECT date_format(closure_date,'%d-%m-%Y') as 'Closure Date',almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No', "
        lsSql &= " packet_gnsarefno as 'GNSA Ref#',agreement_no as 'Agreement Number',shortagreement_no as 'Short Agreement Number',"
        lsSql &= " bounceentry_slno as 'Bounce Slno',bounceentry_chqno as 'Bounce Cheque No',date_format(bounceentry_chqdate,'%d-%m-%Y') as 'Bounce Cheque Date',"
        lsSql &= " bounceentry_chqamount as 'Bounce Cheque Amount',bounceentry_remarks as 'Bounce Remarks',"
        lsSql &= " if(closure_postflag='N','Not Posted','Posted') as 'Post Status'"
        lsSql &= " FROM chola_trn_tclosure as tf"
        lsSql &= " inner join chola_mst_tagreement on closure_agreementgid=agreement_gid "
        lsSql &= " left join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " left join chola_trn_tbounceentry on bounceentry_agreement_gid=closure_agreementgid "
        lsSql &= " where 1=1 "
        lsSql &= lsCond
        lsSql &= " order by packet_gnsarefno"

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        msReport = cboReport.Text.ToUpper

        Select Case msReport
            Case "CLOSURE MIS"
                Call ClosureMis()
            Case Else
                Call LoadData()
        End Select
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Report.xls", "Report")
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtAgreementNo.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub ClosureMis()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        If dtpfrom.Checked = True Then lsCond &= " and agreement_closeddate >= '" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "' "
        If dtpto.Checked = True Then lsCond &= " and agreement_closeddate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(dtpto.Text)), "yyyy-MM-dd") & "' "

        If txtShortAgreementNo.Text <> "" Then lsCond &= " and shortagreement_no = '" & QuoteFilter(txtShortAgreementNo.Text) & "' "
        If txtAgreementNo.Text <> "" Then lsCond &= " and agreement_no = '" & QuoteFilter(txtAgreementNo.Text) & "' "

        If lsCond = "" Then lsCond &= " and 1 = 2 "

        lsSql = ""
        lsSql &= " SELECT agreement_closeddate as 'Closed Date',"
        lsSql &= " count(distinct agreement_gid) as 'Agreement Count', "
        lsSql &= " sum(if(packet_gid is null,0,1)) as 'Packet Count',"
        lsSql &= " sum(if(ifnull(packet_status,0) & " & GCIPACKETPULLOUT & " > 0,1,0)) as 'Packet Pullout Count',"
        lsSql &= " sum(if(ifnull(packet_status," & GCIPACKETPULLOUT & ") & " & GCIPACKETPULLOUT & " = 0,1,0)) as 'Packet Available Count' "
        lsSql &= " FROM chola_mst_tagreement "
        lsSql &= " left join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
        lsSql &= " where 1=1 "
        lsSql &= lsCond
        lsSql &= " and agreement_closeddate is not null "
        lsSql &= " group by agreement_closeddate"

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        Dim frm As frmQuickView

        With dgvsummary
            If msReport = "CLOSURE MIS" And e.RowIndex >= 0 Then
                lssql = " "
                lssql &= " SELECT date_format(agreement_closeddate,'%d-%m-%Y') as 'Closure Date',almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No', "
                lssql &= " packet_gnsarefno as 'GNSA Ref#',agreement_no as 'Agreement No',shortagreement_no as 'Short Agreement Number' "
                lssql &= " FROM chola_mst_tagreement "
                lssql &= " inner join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
                lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
                lssql &= " where agreement_closeddate = '" & Format(.Item(0, e.RowIndex).Value, "yyyy-MM-dd") & "' "
                lssql &= lsCond

                Select Case e.ColumnIndex
                    Case 2
                    Case 3
                        lssql &= " and  packet_status & " & GCIPACKETPULLOUT & " > 0 "
                    Case 4
                        lssql &= " and  packet_status & " & GCIPACKETPULLOUT & " = 0 "
                    Case Else
                        lssql &= " and 1 = 2 "
                End Select

                lssql &= " order by packet_gnsarefno"

                frm = New frmQuickView(gOdbcConn, lssql)
                frm.ShowDialog()
            End If
        End With
    End Sub
End Class