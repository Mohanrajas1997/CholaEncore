Public Class frmmicrreport
    Private Sub frmmicrreport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmmicrreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmmicrreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtmicrcode.Focus()
        txtmicrcode.Text = ""
    End Sub

    Private Sub frmmicrreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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
        Dim lsCond As String = ""

        lsSql = " select micr_code as 'MICR Code',micr_bankname as 'Bank Name',"
        lsSql &= " micr_bankbranch as 'Bank Branch',micr_micrcentre as 'MICR Centre',"
        lsSql &= " date_format(import_on,'%d-%m-%Y') as 'Import Date',import_by as 'Import By' "
        lsSql &= " from chola_mst_tmicr "
        lsSql &= " left join chola_mst_tfile on file_gid=micr_file_gid"
        lsSql &= " where 1=1 "

        If txtmicrcode.Text.Trim <> "" Then
            lsSql &= " and micr_code like '" & txtmicrcode.Text.Trim & "%'"
        End If

        If txtLocCode.Text.Trim <> "" Then
            lsSql &= " and mid(micr_code,1,3) like '" & QuoteFilter(txtLocCode.Text) & "%' "
        End If

        If txtBankCode.Text.Trim <> "" Then
            lsSql &= " and mid(micr_code,4,3) like '" & QuoteFilter(txtBankCode.Text) & "%' "
        End If

        If txtBrCode.Text.Trim <> "" Then
            lsSql &= " and mid(micr_code,7,3) like '" & QuoteFilter(txtBrCode.Text) & "%' "
        End If

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtmicrcode.Text = ""
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