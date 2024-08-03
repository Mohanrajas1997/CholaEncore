Public Class frmcityreport
    Private Sub frmcityreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmcityreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtCityMicr.Focus()
        txtCityMicr.Text = ""
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        lsSql = " select city_micrcode as 'City Micr',"
        lsSql &= " city_name as 'City Name',"
        lsSql &= " import_on as 'Import Date',import_by as 'Import By' "
        lsSql &= " from chola_mst_tcity "
        lsSql &= " left join chola_mst_tfile on file_gid=city_file_gid"
        lsSql &= " where 1=1 "

        If txtCityMicr.Text.Trim <> "" Then
            lsSql &= " and city_micrcode like '" & txtCityMicr.Text.Trim & "%' "
        End If

        If txtCityName.Text.Trim <> "" Then
            lsSql &= " and city_name like '" & QuoteFilter(txtCityName.Text.Trim) & "%' "
        End If

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lblTotRec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click

    End Sub

    Private Sub frmcityreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
End Class