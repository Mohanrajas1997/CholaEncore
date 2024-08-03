Public Class frminwardreport
    Private Sub frminwardreport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frminwardreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frminwardreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtagreementno.Focus()
        txtagreementno.Text = ""
        cbostatus.Items.Add("Inward Completed")
        cbostatus.Items.Add("Auth Completed")
        cbostatus.Items.Add("Entry Completed")
        cbostatus.Items.Add("Vaulting Completed")
        cbostatus.Items.Add("Packet Pullout Completed")
    End Sub

    Private Sub frminwardreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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
        Dim lsstatus As String

        If cbostatus.Text = "Inward Completed" Then
            lsCond &= " and  p.packet_status & " & GCINWARDENTRY & " > 0"
        ElseIf cbostatus.Text = "Auth Completed" Then
            lsCond &= " and ( p.packet_status & " & GCAUTHENTRY & " > 0 "
            lsCond &= " or p.packet_status & " & GCREJECTENTRY & " > 0 )"
        ElseIf cbostatus.Text = "Entry Completed" Then
            lsCond &= " and  p.packet_status & " & GCPACKETCHEQUEENTRY & " > 0"
        ElseIf cbostatus.Text = "Vaulting Completed" Then
            lsCond &= " and  p.packet_status & " & GCPACKETVAULTED & " > 0"
        ElseIf cbostatus.Text = "Packet Pullout Completed" Then
            lsCond &= " and  p.packet_status & " & GCIPACKETPULLOUT & " > 0"
        End If

        lsSql = " select group_concat(concat('\'',status_desc,'\'')) from "
        lsSql &= "(select status_desc FROM chola_mst_tstatus where status_deleteflag='N' and status_level='Packet' order by status_flag) as a"

        lsstatus = gfExecuteScalar(lsSql, gOdbcConn)

        lsSql = ""
        lsSql &= " select i.inward_product as 'Product',i.inward_applicationid as 'ApplicationId',i.inward_applicationformno as 'Application Form No', "
        lsSql &= " i.inward_branch as 'Branch',d.inward_agreementno as 'Parent Agreement No',"
        lsSql &= " i.inward_agreementno as 'Agreement No',i.inward_shortagreementno as 'Short agreement No',i.inward_customername as 'Customer Name',i.inward_paymode as 'Pay Mode',i.inward_pdc as 'PDC',i.inward_spdc as 'SPDC',"
        lsSql &= " i.inward_mandate as 'Mandate',date_format(i.inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',i.inward_tenure as 'Tenure',"
        lsSql &= " date_format(i.inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',i.inward_installmode as 'Install Mode',i.inward_dumpremarks as 'Remarks',"
        lsSql &= " i.inward_compagr as 'Comp Agr',make_set(i.inward_status,'','Received','Not Received','Combined') as 'Received Status',p.packet_gnsarefno as 'GNSA REF#', "
        lsSql &= " p.packet_remarks as 'Packet Remarks',"
        lsSql &= " make_set(packet_status ," & lsstatus & ") as 'Packet Status',i.inward_gid as 'Inward Id',i.inward_parent_gid as 'Parent Inward Id' "
        lsSql &= " from chola_trn_tinward as i "
        lsSql &= " inner join chola_mst_tfile as f on f.file_gid=i.inward_file_gid "
        lsSql &= " left join chola_trn_tpacket as p on p.packet_gid=i.inward_packet_gid "
        lsSql &= " left join chola_trn_tinward as d on d.inward_gid = i.inward_parent_gid and d.inward_gid <> i.inward_gid "
        lsSql &= " where 1=1 "

        If dtpfrom.Checked Then
            lsSql &= " and  i.inward_receiveddate >='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked Then
            lsSql &= " and  i.inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        End If

        If cboreceived.Text = "Received" Then
            lsSql &= " and i.inward_packet_gid>0 "
        ElseIf cboreceived.Text = "Not Received" Then
            lsSql &= " and i.inward_status & " & GCNOTRECEIVED & " > 0"
        ElseIf cboreceived.Text = "Combined" Then
            lsSql &= " and i.inward_status & " & GCCOMBINED & " > 0"
        End If

        If txtagreementno.Text.Trim <> "" Then
            lsSql &= " and i.inward_agreementno='" & txtagreementno.Text.Trim & "'"
        End If

        If txtShortAgreementNo.Text.Trim <> "" Then
            lsSql &= " and i.inward_shortagreementno='" & txtShortAgreementNo.Text.Trim & "'"
        End If

        lsSql &= lsCond

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
        txtagreementno.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
        cboreceived.SelectedIndex = -1
        cbostatus.SelectedIndex = -1
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