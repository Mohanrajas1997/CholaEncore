Public Class frmcyclereport
    Dim lssql As String
    Private Sub frmcyclereport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmcyclereport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmcyclereport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        lssql = " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N' order by type_name"
        gpBindCombo(lssql, "type_name", "type_flag", cboproduct, gOdbcConn)
        cboproduct.SelectedIndex = 0
    End Sub

    Private Sub frmcyclereport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()

        lssql = ""
        lssql &= " select almaraentry_cupboardno as 'CupBoard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No',"
        lssql &= " shortagreement_no as 'Short Agreement No',agreement_no as 'Agreement No',"
        lssql &= " packet_gnsarefno as 'GNSA REF#',type_name as 'Product',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lsSql &= " chq_amount as 'Amount' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_ttype on chq_prodtype=type_flag and type_deleteflag='N'"
        lsSql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " where chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and chq_type=" & GCEXTERNALNORMAL
        lsSql &= " and chq_status & " & GCPULLOUT & " = 0 "
        lsSql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
        lsSql &= " and chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
        lsSql &= " and chq_status & " & GCCLOSURE & " = 0 "

        If cboproduct.Text.Trim <> "" Then
            If cboproduct.SelectedValue < 8 Then
                lsSql &= " and chq_prodtype=" & cboproduct.SelectedValue
            End If
        End If

        lssql &= " order by packet_gnsarefno"

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
           
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub


    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpcycledate.Value = Now()
        dtpcycledate.Checked = False
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

   
End Class