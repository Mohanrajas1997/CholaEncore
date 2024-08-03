Public Class frmbounceinwardreport

    Private Sub frmbounceinwardreport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmbounceinwardreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmbounceinwardreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtawbno.Focus()
        txtawbno.Text = ""
    End Sub

    Private Sub frmbounceinwardreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select inward_couriername as 'Courier Name',inward_awbno as 'AWB No'" ',inward_chqfromslno as 'From Slno',inward_chqtoslno as 'To Slno'
        lsSql &= ", date_format(inward_insertdate,'%d-%m-%Y') as 'Insert date',inward_insertby as 'Insert by' "
        lsSql &= " from chola_trn_tbounceinward "
        lsSql &= " where inward_deleteflag='N' "

        If dtpfrom.Checked Then
            lsSql &= " and  date_format(inward_insertdate,'%Y-%m-%d') >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked Then
            lsSql &= " and  date_format(inward_insertdate,'%Y-%m-%d') <='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
        End If

        If txtawbno.Text.Trim <> "" Then
            lsSql &= " and inward_awbno='" & txtawbno.Text.Trim & "'"
        End If

        lsSql &= " order by inward_insertdate"
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
        txtawbno.Text = ""
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