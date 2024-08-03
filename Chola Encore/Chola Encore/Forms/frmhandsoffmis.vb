Imports System.Data.Odbc
Public Class frmhandsoffmis

    Dim lssql As String
    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 1 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objdt As DataTable
        Dim dsdata As DataSet
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        Else
            objdt = dgvsummary.DataSource
            dsdata = popDataset()
        End If


        Dim objexl As New XMLExport(gsReportPath & "\Report.xls")
        With objexl
            .NewSheet("Summary")
            .BeginRow()
            For j As Integer = 0 To objdt.Columns.Count - 1
                .AddCellboldgry(objdt.Columns(j).ColumnName)
            Next
            .EndRow()
            For i As Integer = 0 To objdt.Rows.Count - 1
                .BeginRow()
                For j As Integer = 0 To objdt.Columns.Count - 1
                    .AddCell(objdt.Rows(i)(j).ToString)
                Next
                .EndRow()
            Next
            .EndSheet()
            .NewSheet("Dump")
            .BeginRow()
            For j As Integer = 0 To dsdata.Tables(0).Columns.Count - 1
                .AddCellboldgry(dsdata.Tables(0).Columns(j).ColumnName)
            Next
            .EndRow()
            For i As Integer = 0 To dsdata.Tables(0).Rows.Count - 1
                .BeginRow()
                For j As Integer = 0 To dsdata.Tables(0).Columns.Count - 1
                    .AddCell(dsdata.Tables(0).Rows(i)(j).ToString)
                Next
                .EndRow()
            Next
            .EndSheet()            
            .Close()
        End With

        System.Diagnostics.Process.Start(gsReportPath & "\Report.xls")

    End Sub
    Private Function popDataset() As DataSet
        Dim ds As New DataSet
        'SPDC
        lssql = ""
        lssql &= " SELECT 'SPDC' as 'Type',spdc_gnsarefno as 'GNSA REF#',spdc_agreementno as 'Agreement No#',spdc_repaymentmode as 'Mode',handsoff_type as 'Handsoff Type',"
        lssql &= " handsoff_dcno as 'DC No#',handsoff_chqcnt as 'Cheque Count',group_concat(distinct(entry_chqno)) as 'Cheque Nos',handsoff_to as 'Send to',date_format(handsoff_handsoffdate,'%d-%m-%Y') as 'Handsoff Date' "
        lssql &= " FROM chola_trn_tspdc as tf"
        lssql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lssql &= " inner join chola_trn_thandsoff on handsoff_shortagreementno=spdc_shortagreementno "
        lssql &= " left join chola_trn_thandsoffentry on entry_shortagreementno=handsoff_shortagreementno "
        lssql &= " where handsoff_handsoffflag='Y' "

        If dtpfrom.Checked = True Then
            lssql &= " and date_format(spdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lssql &= " and date_format(spdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If cbofilename.SelectedIndex > 0 Then
            lssql &= " and tf.file_mst_gid='" & cbofilename.SelectedValue & "'"
        End If

        lssql &= " group by spdc_agreementno "

        lssql &= " Union "
        lssql &= " SELECT 'PDC' as 'Type',pdc_gnsarefno as 'GNSA REF#',pdc_parentcontractno as 'Agreement No#',`pdc_mode`as 'Mode',handsoff_type as 'Handsoff Type',"
        lssql &= " handsoff_dcno as 'DC No#',handsoff_chqcnt as 'Cheque Count',group_concat(distinct(entry_chqno)) as 'Cheque Nos',handsoff_to as 'Send to',date_format(handsoff_handsoffdate,'%d-%m-%Y') as 'Handsoff Date' "
        lssql &= " FROM chola_trn_tpdcfile as tf"
        lssql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lssql &= " inner join chola_trn_thandsoff on handsoff_shortagreementno=pdc_shortpdc_parentcontractno "
        lssql &= " left join chola_trn_thandsoffentry on entry_shortagreementno=handsoff_shortagreementno "
        lssql &= " where handsoff_handsoffflag='Y' "

        If dtpfrom.Checked = True Then
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If cbofilename.SelectedIndex > 0 Then
            lssql &= " and tf.file_mst_gid='" & cbofilename.SelectedValue & "'"
        End If

        lssql &= " group by pdc_parentcontractno "

        gfDataSet(ds, lssql, "Dump", gOdbcConn)


        Return ds
    End Function

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        cbofilename.DataSource = Nothing
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub LoadData()
        Dim lssql As String
        Dim ds As New DataSet
        Dim lsCond As String = ""

        Dim i As Integer

        lssql = ""
        lssql &= " SELECT 'SPDC' as 'Type',spdc_repaymentmode as 'Mode',handsoff_type as 'Handsoff Type', count(*) as 'Total'"
        lssql &= " FROM chola_trn_tspdc as tf"
        lssql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lssql &= " inner join chola_trn_thandsoff on handsoff_shortagreementno=spdc_shortagreementno "
        lssql &= " where handsoff_handsoffflag='Y' "

        If dtpfrom.Checked = True Then
            lssql &= " and date_format(spdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lssql &= " and date_format(spdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If cbofilename.SelectedIndex > 0 Then
            lssql &= " and tf.file_mst_gid='" & cbofilename.SelectedValue & "'"
        End If

        lssql &= " group by spdc_repaymentmode,handsoff_type "
        lssql &= " Union "
        lssql &= " SELECT 'PDC' as 'Type',`pdc_mode`as 'Mode',handsoff_type as 'Handsoff Type',count(distinct(pdc_shortpdc_parentcontractno)) as 'Total' "
        lssql &= " FROM chola_trn_tpdcfile as tf"
        lssql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lssql &= " inner join chola_trn_thandsoff on handsoff_shortagreementno=pdc_shortpdc_parentcontractno "
        lssql &= " where handsoff_handsoffflag='Y' "

        If dtpfrom.Checked = True Then
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If cbofilename.SelectedIndex > 0 Then
            lssql &= " and tf.file_mst_gid='" & cbofilename.SelectedValue & "'"
        End If

        lssql &= " group by `pdc_mode`,handsoff_type "

        With dgvsummary
            .Columns.Clear()

            gpPopGridView(dgvsummary, lssql, gOdbcConn)

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                .Columns(i).Width = 144 * 0.8
            Next i

            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 144 * 0.2
            lbltotrec.Text = "Total Records : " & (.RowCount - 1).ToString
        End With
    End Sub

    Private Sub frmhandsoffmis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        Call FillCombo()
    End Sub
    Public Sub FillCombo()
        Dim lsSql As String
        Dim dt As New DataTable
        lsSql = ""
        lsSql = " Select"
        lsSql &= " 0 as file_gid,"
        lsSql &= " 'ALL' as file_name"
        lsSql &= " FROM chola_mst_tfile as mf"
        lsSql &= " union Select"
        lsSql &= " distinct mf.file_gid as file_gid,"
        lsSql &= " concat(mf.file_name,' - ',mf.file_sheetname) as file_name"
        lsSql &= " FROM chola_mst_tfile as mf"
        lsSql &= " inner join chola_trn_tfile as tf where tf.file_mst_gid=mf.file_gid"

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(file_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(file_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        dt = GetDataTable(lsSql)

        If dt.Rows.Count > 0 Then
            cbofilename.DataSource = dt
            cbofilename.DisplayMember = "file_name"
            cbofilename.ValueMember = "file_gid"
            cbofilename.SelectedIndex = 0
        End If
    End Sub
    Private Sub dtpfrom_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpfrom.ValueChanged
        Call FillCombo()
    End Sub

    Private Sub dtpto_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpto.ValueChanged
        Call FillCombo()
    End Sub

    Private Sub frmhandsoffmis_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Function gfDataSet(ByVal objDataSet As DataSet, ByVal SQL As String, ByVal tblName As String, ByVal odbcConn As Odbc.OdbcConnection) As DataSet
        Dim objDataAdapter As New OdbcDataAdapter(SQL, odbcConn)
        Try
            objDataAdapter.Fill(objDataSet, tblName)
            Return objDataSet
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function
End Class