Public Class frmcholaMIS
    Dim lssql As String
    Private Sub frmsummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
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
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select `Received Date`,sum(`No of Pouches`) as 'No of Pouches',sum(`Not Recd Pouches`) as 'Not Recd Pouches',"
        lsSql &= " sum(PDC) as 'PDC',sum(`External Security`) as 'External Security',"
        lsSql &= " sum(`Sub Contract No`) as 'Sub Contract No',sum(NPDC) as 'NPDC' ,sum(`Ecs SPDC`) as 'Ecs SPDC',"
        lsSql &= " sum(`Not Recd Cheques`) as 'Not Recd Cheques',sum(`Ecs Mandate`) as 'Ecs Mandate' from"
        lsSql &= " (SELECT date_format(pdc_importdate,'%d-%m-%Y') as 'Received Date',"
        lsSql &= " count(distinct pdc_shortpdc_parentcontractno) as 'No of Pouches',"
        lsSql &= " (select  count(distinct pdc_shortpdc_parentcontractno)"
        lsSql &= " from chola_trn_tpdcfile where pdc_status_flag=1 and pdc_importdate=a.pdc_importdate) as 'Not Recd Pouches',"
        lsSql &= " sum(if(instr(upper(pdc_type),'EXTERNAL-NORMAL')>0,1,0)) as 'PDC',"
        lsSql &= " sum(if(instr(upper(pdc_type),'EXTERNAL-SECURITY')>0,1,0)) as 'External Security',"
        lsSql &= " (select  count(*)"
        lsSql &= " from chola_trn_tpdcfile where pdc_importdate=a.pdc_importdate) - count(*) as 'Sub Contract No',"
        lsSql &= " 0 as 'NPDC',"
        lsSql &= " 0 as 'Ecs SPDC',"
        lsSql &= " (select  count(distinct pdc_chqno)"
        lsSql &= " from chola_trn_tpdcfile where pdc_status_flag=1 and pdc_importdate=a.pdc_importdate) as 'Not Recd Cheques',"
        lsSql &= " 0 as 'Ecs Mandate'"
        lsSql &= " FROM ( select * from chola_trn_tpdcfile Where 1=1 "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(pdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(pdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        lsSql &= " group by pdc_shortpdc_parentcontractno,pdc_chqno) as a"
        lsSql &= " group by pdc_importdate"
        lsSql &= " union all"
        lsSql &= " SELECT date_format(spdc_importdate,'%d-%m-%Y') as 'Received Date',"
        lsSql &= " count(*) as 'No of Pouches',"
        lsSql &= " (select  count(distinct spdc_gid)"
        lsSql &= " from chola_trn_tspdc where spdc_statusflag=1 and spdc_importdate=a.spdc_importdate) as 'Not Recd Pouches',"
        lsSql &= " 0 as 'PDC',"
        lsSql &= " 0 as 'External Security',"
        lsSql &= " 0 as 'Sub Contract No',"
        lsSql &= " sum(if(spdc_repaymentmode='NPDC',(spdc_entryspdccnt),0)) as 'NPDC',"
        lsSql &= " sum(if(spdc_repaymentmode='ECS',(spdc_entryspdccnt),0)) as 'Ecs SPDC',"
        lsSql &= " (select  sum(spdc_entryspdccnt)"
        lsSql &= " from chola_trn_tspdc where spdc_statusflag=1 and spdc_importdate=a.spdc_importdate) as 'Not Recd Cheques',"
        lsSql &= " sum(spdc_entryecsmandatecnt) as 'Ecs Mandate'"
        lsSql &= " FROM chola_trn_tspdc as a where 1=1 "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(spdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(spdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        lsSql &= " group by spdc_importdate) b"
        lsSql &= " group by `Received Date`"


        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub


    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
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
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub



End Class