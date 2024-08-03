Public Class frmAgreementReport
    Private Sub frmcityreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmcityreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtAgrNo.Focus()
        txtAgrNo.Text = ""
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        lsSql = " select agreement_no as 'Agreement No',"
        lsSql &= " shortagreement_no as 'Short Agreement No',"
        lsSql &= " agreement_closeddate as 'Closed Date',"
        lsSql &= " make_set(agreement_statusflag,'Active','Inactive','Closed') as 'Status',"
        lsSql &= " agreement_gid as 'Agreement Id' "
        lsSql &= " from chola_mst_tagreement "
        lsSql &= " where 1=1 "

        If txtAgrNo.Text.Trim <> "" Then
            lsSql &= " and agreement_no like '" & QuoteFilter(txtAgrNo.Text.Trim) & "%' "
        End If

        If txtShortAgrNo.Text.Trim <> "" Then
            lsSql &= " and shortagreement_no like '" & QuoteFilter(txtShortAgrNo.Text.Trim) & "%' "
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

            lblTotRec.Top = pnlMain.Top + pnlMain.Height + 6
            lblTotRec.Left = pnlMain.Left
        End With
    End Sub
End Class