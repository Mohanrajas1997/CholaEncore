Public Class frmQuickView
    Dim mDbCon As Odbc.OdbcConnection
    Dim msSql As String
    Dim mbCancelButton As Boolean

    Private Sub frmQuickView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ds As DataSet

        If msSql <> "" Then
            ds = gfDataSet(msSql, "quickview", mDbCon)

            With dgvQuickView
                .DataSource = ds.Tables("quickview")
                lblTotRec.Text = "Total Records : " & .RowCount.ToString
            End With
        End If

        If mbCancelButton = True Then CancelButton = btnClose
    End Sub

    Private Sub frmQuickView_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvQuickView
            .Left = 6
            .Width = Me.Width - 18
            .Height = Math.Abs(Me.Height - 36 - pnlReport.Height)

            pnlReport.Left = 6
            pnlReport.Width = .Width
            pnlReport.Top = dgvQuickView.Height + 6

            lblTotRec.Left = 6

            btnClose.Left = Math.Abs(pnlReport.Width - btnClose.Width - 8)
            btnExport.Left = Math.Abs(btnClose.Left - btnClose.Width - 6)
        End With
    End Sub

    Private Sub dgvQuickView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvQuickView.CellContentClick

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Sub New(ByVal dbCon As Odbc.OdbcConnection, ByVal sqlstr As String, Optional ByVal CancelButtonFlag As Boolean = False)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mDbCon = dbCon
        msSql = sqlstr
        mbCancelButton = CancelButtonFlag
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Call PrintDGridXML(dgvQuickView, gsReportPath & "\Report.xls", "Report")
    End Sub
End Class