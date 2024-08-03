Public Class frmretrievalsummary
    Private Sub frmretrievalsummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmretrievalsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmretrievalsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtagreementno.Focus()
        txtagreementno.Text = ""
    End Sub

    Private Sub frmretrievalsummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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
        If txtagreementno.Text <> "" Then
            'Dim frmentry As New frminward(dgvsummary.Rows(0).Cells("Short Agreement No").Value.ToString, dgvsummary.Rows(0).Cells("GNSA REF#").Value.ToString)
            'frmentry.ShowDialog()
            'txtgnsarefno.Text = ""
            dgvsummary.DataSource = Nothing
            lbltotrec.Text = ""
            txtagreementno.Focus()
        End If
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        lsSql = ""
        lsSql &= " select date_format(retrieval_requestdate,'%d-%m-%Y') as 'Request Date',retrieval_agreementno as 'Agreement No',retrieval_shortagreementno as 'Short Agreement No', "
        lsSql &= " retrieval_mode,retrieval_reason,retrieval_chqno,retrieval_gnsarefno,"
        lsSql &= " make_set(retrieval_status,'Requested','Retrieved','Missing') as 'Status' "
        lsSql &= " from chola_trn_tretrieval "

        If dtpfrom.Checked Then
            lsSql &= " and  date_format(retrieval_requestdate,'%Y-%m-%d') >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked Then
            lsSql &= " and  date_format(retrieval_requestdate,'%Y-%m-%d') <='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
        End If

        If txtagreementno.Text.Trim <> "" Then
            lsSql &= " and retrieval_agreementno='" & txtagreementno.Text.Trim & "'"
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
        txtagreementno.Text = ""
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

    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        'If e.RowIndex < 0 Then
        '    Exit Sub
        'End If
        'Dim frmentry As New frminward(dgvsummary.Rows(e.RowIndex).Cells("Short Agreement No").Value.ToString, dgvsummary.Rows(e.RowIndex).Cells("GNSA REF#").Value.ToString)
        'frmentry.ShowDialog()
        ''txtgnsarefno.Text = ""
        'LoadData()
    End Sub
End Class