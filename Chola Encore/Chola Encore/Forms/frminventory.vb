Public Class frminventory
    Dim lssql As String
    Private Sub frminventory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frminventory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frminventory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtGNSARefno.Focus()
        txtGNSARefno.Text = ""
        cbotype.SelectedIndex = 0
    End Sub

    Private Sub frminventory_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

        lsSql = ""
        lsSql &= " SELECT almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No',packet_gnsarefno as 'GNSAREF#', "
        lsSql &= " shortagreement_no as 'Short Agreement No#',agreement_no as 'Agreement No#'"
        lsSql &= " FROM chola_trn_tpacket "
        lsSql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid"
        lsSql &= " left join chola_trn_almaraentry box on almaraentry_gid=packet_box_gid "
        lsSql &= " where 1=1 "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(packet_entryon,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(packet_entryon,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtGNSARefno.Text <> "" Then
            lsSql &= " and packet_gnsarefno='" & txtGNSARefno.Text & "'"
        End If

        If txtAgreementNo.Text <> "" Then
            lsSql &= " and agreement_no='" & txtAgreementNo.Text & "'"
        End If

        If txtShortAgreementNo.Text <> "" Then
            lsSql &= " and shortagreement_no='" & txtShortAgreementNo.Text & "'"
        End If

        If cbostatus.Text.Trim <> "" Then
            If cbostatus.Text.Trim <> "Completed" Then
                lsSql &= " and packet_box_gid=0 "
            Else
                lsSql &= " and packet_box_gid > 0 "
            End If
        End If

        If cbotype.Text.Trim <> "" Then
            lsSql &= " and packet_mode ='" & cbotype.Text & "'"
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
        txtGNSARefno.Text = ""
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
    Private Sub txtproposalno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGNSARefno.KeyPress
        e.Handled = gfIntstrEntryOnly(e)
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        LoadData()
    End Sub
End Class