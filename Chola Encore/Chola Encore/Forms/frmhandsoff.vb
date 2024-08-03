Public Class frmhandsoff
    Dim lsfiletype As String = ""
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmsummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtproposalno.Focus()
        txtproposalno.Text = ""
    End Sub

    Private Sub frmsummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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
        lsSql &= " SELECT tf.handsoff_gid,"
        lsSql &= " handsoff_shortagreementno as 'Agreement Number',spdc_gnsarefno as 'GNSA Ref#',"
        lsSql &= " handsoff_repaymentmode as 'Repayment Mode',"
        lsSql &= " handsoff_customername as 'Customer Name',"
        lsSql &= " handsoff_type as 'Type',"
        lsSql &= " handsoff_remarks as 'Remarks',"
        lsSql &= " handsoff_importdate as 'Import Date'"
        lsSql &= " FROM chola_trn_thandsoff as tf"
        lsSql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lsSql &= " left join chola_trn_tspdc on spdc_shortagreementno=handsoff_shortagreementno"
        lsSql &= " where 1=1 and handsoff_handsoffflag='N' "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtproposalno.Text <> "" Then
            lsSql &= " and handsoff_shortagreementno='" & txtproposalno.Text & "'"
        End If

        lsSql &= " union "
        lsSql &= " SELECT tf.handsoff_gid,"
        lsSql &= " handsoff_shortagreementno as 'Agreement Number',pdc_gnsarefno as 'GNSA Ref#',"
        lsSql &= " handsoff_repaymentmode as 'Repayment Mode',"
        lsSql &= " handsoff_customername as 'Customer Name',"
        lsSql &= " handsoff_type as 'Type',"
        lsSql &= " handsoff_remarks as 'Remarks',"
        lsSql &= " handsoff_importdate as 'Import Date'"
        lsSql &= " FROM chola_trn_thandsoff as tf"
        lsSql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lsSql &= " left join chola_trn_tpdcfile on pdc_shortpdc_parentcontractno=handsoff_shortagreementno"
        lsSql &= " where 1=1 and handsoff_handsoffflag='N' "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtproposalno.Text <> "" Then
            lsSql &= " and handsoff_shortagreementno='" & txtproposalno.Text & "'"
        End If

        With dgvsummary
            .Columns.Clear()

            gpPopGridView(dgvsummary, lsSql, gOdbcConn)

        

            .Columns(0).Visible = False


         
            lbltotrec.Text = "Total Records : " & (.RowCount - 1).ToString
        End With

        
    End Sub


    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 1 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
        If txtproposalno.Text <> "" Then
            Dim objfrm As New frmhandsoffentry(dgvsummary.Rows(0).Cells(1).Value)
            objfrm.ShowDialog()
            txtproposalno.Text = ""
            LoadData()
        End If
    End Sub

    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        Dim objfrm As New frmhandsoffentry(dgvsummary.Rows(e.RowIndex).Cells(1).Value)
        objfrm.ShowDialog()
        txtproposalno.Text = ""
        LoadData()        
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
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtproposalno.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub
End Class