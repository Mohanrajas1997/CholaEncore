Public Class frmhandsoffreport
    Dim lssql As String
    Private Sub frmhandsoffreport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmhandsoffreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmhandsoffreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtagreementno.Focus()
        txtagreementno.Text = ""
    End Sub

    Private Sub frmhandsoffreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

        Dim i As Integer

        lsSql = ""
        lsSql &= " SELECT handsoff_shortagreementno as 'Agreement No#',handsoff_customername as 'Customer Name',handsoff_repaymentmode as 'Repayment Mode', "
        lsSql &= " handsoff_type as 'Type',handsoff_remarks as 'Remarks',handsoff_dcno as 'DC No#',handsoff_chqcnt as 'Cheque Count',group_concat(entry_chqno) as 'Cheque nos',handsoff_to as 'Send To',date_format(handsoff_handsoffdate,'%d-%m-%Y') as 'Handsoff Date' "
        lsSql &= " FROM chola_trn_thandsoff tf"
        lsSql &= " left join chola_trn_thandsoffentry cup on entry_shortagreementno=handsoff_shortagreementno "
        lsSql &= " where 1=1 and handsoff_handsoffflag='Y' "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(handsoff_handsoffdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(handsoff_handsoffdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtagreementno.Text <> "" Then
            lsSql &= " and handsoff_shortagreementno='" & txtagreementno.Text & "'"
        End If

        lsSql &= " group by handsoff_shortagreementno "

        With dgvsummary
            .Columns.Clear()

            gpPopGridView(dgvsummary, lsSql, gOdbcConn)

            lbltotrec.Text = "Total Records : " & (.RowCount - 1).ToString
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
    Private Sub txtproposalno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtagreementno.KeyPress
        e.Handled = gfIntstrEntryOnly(e)
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        LoadData()
    End Sub
End Class