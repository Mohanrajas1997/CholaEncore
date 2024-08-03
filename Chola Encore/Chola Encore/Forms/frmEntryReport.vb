
Public Class frmEntryReport
    Dim lssql As String = ""
  
    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsCond As String = ""

        LoadPDCData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Report.xls", "Report")
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Call LoadData()
        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub frmEntryReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmEntryReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmEntryReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub btnclear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False

        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
        txtaggrementno.Text = ""
    End Sub

    Private Sub LoadPDCData()
        Dim objdt, objdt1, objdata As DataTable
        objdt1 = New DataTable()

        Dim lsstatus As String

        lssql = " select group_concat(concat('\'',status_desc,'\'')) from "
        lssql &= "(select status_desc FROM chola_mst_tstatus where status_deleteflag='N' and status_level='Cheque' order by status_flag) as a"
        lsstatus = gfExecuteScalar(lssql, gOdbcConn)

        lssql = " select distinct pdc_parentcontractno "
        lssql &= " from chola_trn_tpdcfile a "
        lssql &= " left join chola_mst_tfile as mf on mf.file_gid=a.file_mst_gid"
        lssql &= " where 1=1 "

        If dtpfrom.Checked = True Then
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtaggrementno.Text.Trim <> "" Then
            lssql &= " and pdc_shortpdc_parentcontractno='" & txtaggrementno.Text.Trim & "'"
        End If

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lssql = ""
            lssql &= " select packet_gnsarefno as 'GNSA Ref No#',pdc_contractno as 'Contract No',pdc_shortpdc_parentcontractno as 'Dump Parent No',"
            lssql &= " shortagreement_no as 'Entry Short Parent No',agreement_no as 'Entry Parent No',"
            lssql &= " pdc_draweename as 'Drawee Name',pdc_chqno as 'Dump Chqno', chq_no as 'Entry Chqno',if(pdc_chqno=chq_no,'Matched','Not Matched') as 'Chqno Diff',"
            lssql &= " pdc_chqdate as 'Dump Chqdate', date_format(chq_date,'%d-%m-%Y') as 'Entry Chqdate',if(chq_date=pdc_chqdate,'Matched','Not Matched') as 'Chqdate Diff',"
            lssql &= " cast(pdc_chqamount as char) as 'Dump Chqamt', cast(chq_amount as char) as 'Entry Chqamt',if(pdc_chqamount=chq_amount,'Matched','Not Matched') as 'Chqamt Diff', "
            lssql &= " '' as 'Import By',"
            lssql &= " '' as 'Import Date',"
            lssql &= " packet_entryby as 'Entry By',"
            lssql &= " date_format(packet_entryon,'%d-%m-%Y') as 'Entry On',"
            lssql &= " make_set(if(pdc_status_flag is null,'',pdc_status_flag) ," & lsstatus & ") as 'Status' "
            lssql &= " from chola_trn_tpdcentry  a "
            lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
            lssql &= " left join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
            lssql &= " left join chola_trn_tpdcfile b on agreement_no=pdc_parentcontractno and pdc_parentcontractno='" & objdt.Rows(i)(0).ToString & "' and pdc_chqno=chq_no"
            lssql &= " where pdc_gnsarefno is null "
            lssql &= " and agreement_no='" & objdt.Rows(i)(0).ToString & "'"

            objdata = GetDataTable(lssql)

            If objdata.Rows.Count = 0 Then
                GoTo GNext
            End If

            lssql = ""
            lssql &= " select packet_gnsarefno as 'GNSA Ref No#',pdc_contractno as 'Contract No',pdc_shortpdc_parentcontractno as 'Dump Parent No',"
            lssql &= " shortagreement_no as 'Entry Short Parent No',agreement_no as 'Entry Parent No',"
            lssql &= " pdc_draweename as 'Drawee Name',pdc_chqno as 'Dump Chqno', chq_no as 'Entry Chqno',if(pdc_chqno=chq_no,'Matched','Not Matched') as 'Chqno Diff',"
            lssql &= " date_format(pdc_chqdate,'%d-%m-%Y') as 'Dump Chqdate',if(pdc_type='EXTERNAL-SECURITY' and chq_status>" & GCNEW & ",date_format(pdc_chqdate,'%d-%m-%Y'),date_format(chq_date,'%d-%m-%Y')) as 'Entry Chqdate',if(if(pdc_type='EXTERNAL-SECURITY',date_format(pdc_chqdate, '%d-%m-%Y'),date_format(chq_date, '%d-%m-%Y'))=date_format(pdc_chqdate, '%d-%m-%Y'),'Matched','Not Matched') as 'Chqdate Diff',"
            lssql &= " cast(pdc_chqamount as char) as 'Dump Chqamt', cast(chq_amount as char) as 'Entry Chqamt',if(pdc_chqamount=chq_amount,'Matched','Not Matched') as 'Chqamt Diff', "
            lssql &= " import_by as 'Import By',"
            lssql &= " date_format(import_on,'%d-%m-%Y') as 'Import Date',"
            lssql &= " packet_entryby as 'Entry By',"
            lssql &= " date_format(packet_entryon,'%d-%m-%Y') as 'Entry On',"
            lssql &= " make_set(pdc_status_flag ," & lsstatus & ") as 'Status' "
            lssql &= " from chola_trn_tpdcfile a "
            lssql &= " inner join chola_mst_tagreement on agreement_no=pdc_parentcontractno "
            lssql &= " left join chola_mst_tfile as mf on mf.file_gid=a.file_mst_gid"
            lssql &= " left join chola_trn_tpdcentry b on agreement_gid=chq_agreement_gid and pdc_chqno=chq_no"
            lssql &= " left join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
            lssql &= " where 1=1 "
            lssql &= " and pdc_parentcontractno='" & objdt.Rows(i)(0).ToString() & "'"
            lssql &= " group by pdc_shortpdc_parentcontractno,pdc_contractno,pdc_chqno "
            lssql &= " Union"
            lssql &= " select packet_gnsarefno as 'GNSA Ref No#',pdc_contractno as 'Contract No',pdc_shortpdc_parentcontractno as 'Dump Parent No',"
            lssql &= " shortagreement_no as 'Entry Short Parent No',agreement_no as 'Entry Parent No',"
            lssql &= " pdc_draweename as 'Drawee Name',pdc_chqno as 'Dump Chqno', chq_no as 'Entry Chqno',if(pdc_chqno=chq_no,'Matched','Not Matched') as 'Chqno Diff',"
            lssql &= " pdc_chqdate as 'Dump Chqdate', date_format(chq_date,'%d-%m-%Y') as 'Entry Chqdate',if(chq_date=pdc_chqdate,'Matched','Not Matched') as 'Chqdate Diff',"
            lssql &= " cast(pdc_chqamount as char) as 'Dump Chqamt', cast(chq_amount as char) as 'Entry Chqamt',if(pdc_chqamount=chq_amount,'Matched','Not Matched') as 'Chqamt Diff', "
            lssql &= " '' as 'Import By',"
            lssql &= " '' as 'Import Date',"
            lssql &= " packet_entryby as 'Entry By',"
            lssql &= " date_format(packet_entryon,'%d-%m-%Y') as 'Entry On',"
            lssql &= " make_set(if(pdc_status_flag is null,'',pdc_status_flag) ," & lsstatus & ") as 'Status' "
            lssql &= " from chola_trn_tpdcentry  a "
            lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
            lssql &= " left join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
            lssql &= " left join chola_trn_tpdcfile b on agreement_no=pdc_parentcontractno and pdc_parentcontractno='" & objdt.Rows(i)(0).ToString & "' and pdc_chqno=chq_no"
            lssql &= " where pdc_gnsarefno is null "
            lssql &= " and agreement_no='" & objdt.Rows(i)(0).ToString & "'"

            objdata = GetDataTable(lssql)

            If objdt1.Columns.Count = 0 Then
                For k As Integer = 0 To objdata.Columns.Count - 1
                    objdt1.Columns.Add(objdata.Columns(k).ColumnName.ToString)
                Next
            End If

            For j As Integer = 0 To objdata.Rows.Count - 1
                objdt1.Rows.Add()
                For k As Integer = 0 To objdata.Columns.Count - 1
                    If k = 16 Then
                        objdt1.Rows(objdt1.Rows.Count - 1)(k) = objdata.Rows(0)(16).ToString()
                    ElseIf k = 17 Then
                        objdt1.Rows(objdt1.Rows.Count - 1)(k) = objdata.Rows(0)(17).ToString()
                    Else
                        objdt1.Rows(objdt1.Rows.Count - 1)(k) = objdata.Rows(j)(k).ToString()
                    End If
                Next
            Next

GNext:
        Next
        dgvsummary.Columns.Clear()

        dgvsummary.DataSource = objdt1
       
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class