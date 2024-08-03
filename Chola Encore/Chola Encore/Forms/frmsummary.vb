Public Class frmsummary
    Dim lsfiletype As String = ""
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal FileType As String)
        lsfiletype = FileType
        InitializeComponent()
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

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 1 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
        If txtproposalno.Text <> "" Then
            If lsfiletype = "SPDC" Then
                Dim lsfilegid As String = dgvsummary.Rows(0).Cells(0).Value
                Dim lscolumn1 As String = dgvsummary.Rows(0).Cells(1).Value
                Dim lscolumn2 As String = dgvsummary.Rows(0).Cells(2).Value
                Dim lscount As String = dgvsummary.Rows(0).Cells(3).Value
                Dim lsmandate As String = dgvsummary.Rows(0).Cells(4).Value
                Dim lsgnsaref As String = dgvsummary.Rows(0).Cells(5).Value
                SPDCEntry(lsfilegid, lscolumn1, lscolumn2, lscount, lsmandate, lsgnsaref)
                txtproposalno.Text = ""
                dgvsummary.Columns.Clear()
                'LoadData()
            ElseIf lsfiletype = "PDC" Then
                pdcEntry(dgvsummary.Rows(0).Cells(2).Value)
                txtproposalno.Text = ""
                dgvsummary.Columns.Clear()
                'LoadData()
            End If

        End If
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim i As Integer

        If lsfiletype = "SPDC" Then

            lsSql = ""
            lsSql &= " SELECT tf.spdc_gid,"
            lsSql &= " spdc_shortagreementno as 'Agreement Number',"
            lsSql &= " spdc_repaymentmode as Mode,"
            lsSql &= " spdc_dumpspdccnt as 'SPDC Count',"
            lsSql &= " spdc_ecsmandatecnt as 'Mandate Count',"
            lsSql &= " spdc_gnsarefno as 'GNSA Ref no',"
            lsSql &= " spdc_fineonerecords as 'Finnone Count',"
            lsSql &= " date_format(spdc_handsoffdate,'%d-%m-%Y') as 'Handsoff Date',"
            lsSql &= " spdc_dumpremarks1 as 'Remarks',"
            lsSql &= " spdc_dumpremarks as 'Combined Remarks',"
            lsSql &= " import_by as 'Import By',"
            lsSql &= " date_format(import_on,'%d-%m-%Y') as 'Import Date'"
            lsSql &= " FROM chola_trn_tspdc as tf"
            lsSql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
            lsSql &= " where 1=1 and spdc_statusflag=1"

            If dtpfrom.Checked = True Then
                lsSql &= " and date_format(spdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
            End If

            If dtpto.Checked = True Then
                lsSql &= " and date_format(spdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
            End If

            If txtproposalno.Text <> "" Then
                lsSql &= " and spdc_shortagreementno='" & txtproposalno.Text & "'"
            End If


            With dgvsummary
                .Columns.Clear()

                gpPopGridView(dgvsummary, lsSql, gOdbcConn)

                For i = 0 To .Columns.Count - 1
                    .Columns(i).ReadOnly = True
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                    .Columns(i).Width = 144 * 0.8
                Next i

                .Columns(0).Visible = False
                .Columns(1).Width = 144 * 1.2

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .ColumnHeadersHeight = 144 * 0.2
                lbltotrec.Text = "Total Records : " & (.RowCount).ToString
            End With

        ElseIf lsfiletype = "PDC" Then

            lsSql = " select tf.pdc_gid,pdc_parentcontractno as 'Contract No',pdc_shortpdc_parentcontractno as 'Short Contract No',pdc_draweename as 'Drawee Name' " & _
                    " from chola_trn_tpdcfile tf " & _
                    " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid" & _
                    " where 1=1 and pdc_status_flag=1 "

            If dtpfrom.Checked = True Then
                lsSql &= " and date_format(pdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
            End If

            If dtpto.Checked = True Then
                lsSql &= " and date_format(pdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
            End If

            If txtproposalno.Text <> "" Then
                lsSql &= " and pdc_shortpdc_parentcontractno='" & txtproposalno.Text & "'"
            End If

            lsSql &= " group by pdc_shortpdc_parentcontractno,pdc_draweename"

            With dgvsummary
                .Columns.Clear()

                gpPopGridView(dgvsummary, lsSql, gOdbcConn)

                For i = 0 To .Columns.Count - 1
                    .Columns(i).ReadOnly = True
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
                .Columns(0).Visible = False
                .Columns(1).Width = 122 * 2
                .Columns(2).Width = 122 * 2
                .Columns(3).Width = 244 * 2

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .ColumnHeadersHeight = 144 * 0.2
                lbltotrec.Text = "Total Records : " & (.RowCount).ToString
            End With
        End If
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
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If lsfiletype = "SPDC" Then
            Dim lsfilegid As String = dgvsummary.Rows(e.RowIndex).Cells(0).Value
            Dim lscolumn1 As String = dgvsummary.Rows(e.RowIndex).Cells(1).Value
            Dim lscolumn2 As String = dgvsummary.Rows(e.RowIndex).Cells(2).Value
            Dim lscount As String = dgvsummary.Rows(e.RowIndex).Cells(3).Value
            Dim lsmandate As String = dgvsummary.Rows(e.RowIndex).Cells(4).Value
            Dim lsgnsaref As String = dgvsummary.Rows(e.RowIndex).Cells(5).Value.ToString
            SPDCEntry(lsfilegid, lscolumn1, lscolumn2, lscount, lsmandate, lsgnsaref)
        ElseIf lsfiletype = "PDC" Then
            pdcEntry(dgvsummary.Rows(e.RowIndex).Cells(2).Value)
        End If
        txtproposalno.Text = ""
        LoadData()
    End Sub

    Public Sub SPDCEntry(ByVal lsfilegid As String, ByVal lscolumn1 As String, ByVal lscolumn2 As String, ByVal lscount As String, ByVal lsmandate As String, ByVal lsgnsaref As String)

        Dim lsSql As String = ""
        Dim lslock As String = ""
        Dim lsproposalno As String = ""

        lsSql = ""
        lsSql &= " SELECT"
        lsSql &= " spdc_shortagreementno"
        lsSql &= " FROM chola_trn_tspdc"
        lsSql &= " where 1=1"
        lsSql &= " and file_lock='Y'"
        lsSql &= " and lock_by='" & gUserName & "'"
        lsSql &= " and spdc_gid<>'" & lsfilegid & "'" 'No correction

        lsproposalno = gfExecuteScalar(lsSql, gOdbcConn)

        If lsproposalno <> "" Then
            MsgBox("Already Lock " & lsproposalno, MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtproposalno.Focus()
            Exit Sub
        End If

        lsSql = ""
        lsSql &= " SELECT"
        lsSql &= " lock_by"
        lsSql &= " FROM chola_trn_tspdc"
        lsSql &= " where 1=1"
        lsSql &= " and file_lock='Y'"
        lsSql &= " and spdc_gid='" & lsfilegid & "'"
        lsSql &= " and lock_by<>'" & gUserName & "'"
        lslock = gfExecuteScalar(lsSql, gOdbcConn)

        If lslock = "" Then
            lsSql = ""
            lsSql &= " UPDATE"
            lsSql &= " chola_trn_tspdc"
            lsSql &= " set"
            lsSql &= " file_lock='Y',"
            lsSql &= " lock_by='" & gUserName & "'"
            lsSql &= " where 1=1"
            lsSql &= " and spdc_gid='" & lsfilegid & "'"

            gfExecuteQry(lsSql, gOdbcConn)

            Dim frmObj As New FrmEntry(lsfilegid, lscolumn1, lscolumn2, lscount, lsmandate, lsgnsaref)
            frmObj.ShowDialog()
            frmObj.MdiParent = frmMain
            txtproposalno.Focus()
        Else
            MsgBox("Lock by - " & lslock, MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            Exit Sub
        End If
    End Sub
    Public Sub pdcEntry(ByVal ContractNo As String)

        Dim lsSql As String = ""
        Dim lslock As String = ""
        Dim lsproposalno As String = ""

        lsSql = ""
        lsSql &= " SELECT"
        lsSql &= " pdc_shortpdc_parentcontractno "
        lsSql &= " FROM chola_trn_tpdcfile"
        lsSql &= " where 1=1"
        lsSql &= " and file_lock='Y'"
        lsSql &= " and lock_by='" & gUserName & "'"
        lsSql &= " and pdc_shortpdc_parentcontractno<>'" & ContractNo & "'" 'No correction

        'lsproposalno = gfExecuteScalar(lsSql, gOdbcConn)

        'If lsproposalno <> "" Then
        '    MsgBox("Already Lock " & lsproposalno, MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
        '    txtproposalno.Focus()
        '    Exit Sub
        'End If

        lsSql = ""
        lsSql &= " SELECT"
        lsSql &= " lock_by"
        lsSql &= " FROM chola_trn_tpdcfile"
        lsSql &= " where 1=1"
        lsSql &= " and file_lock='Y'"
        lsSql &= " and pdc_shortpdc_parentcontractno='" & ContractNo & "'"
        lsSql &= " and lock_by<>'" & gUserName & "'"
        lslock = gfExecuteScalar(lsSql, gOdbcConn)

        If lslock = "" Then
            lsSql = ""
            lsSql &= " UPDATE"
            lsSql &= " chola_trn_tpdcfile"
            lsSql &= " set"
            lsSql &= " file_lock='Y',"
            lsSql &= " lock_by='" & gUserName & "'"
            lsSql &= " where 1=1"
            lsSql &= " and pdc_shortpdc_parentcontractno='" & ContractNo & "'"

            gfExecuteQry(lsSql, gOdbcConn)

            Dim frmObj As New frmspdcentry(ContractNo)
            frmObj.ShowDialog()
            txtproposalno.Focus()
        Else
            MsgBox("Lock by - " & lslock, MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub txtproposalno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtproposalno.KeyPress
        e.Handled = gfIntstrEntryOnly(e)
    End Sub
End Class