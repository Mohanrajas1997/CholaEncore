Public Class frmPdcChqReport
    Dim lssql As String
    Private Sub frmpdcreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lssql = " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N' "
        gpBindCombo(lssql, "type_name", "type_flag", cboproduct, gOdbcConn)
        cboproduct.SelectedIndex = -1

        'lssql = " select status_flag,status_desc from chola_mst_tstatus where status_deleteflag='N' "
        'gpBindCombo(lssql, "status_desc", "status_flag", cbostatus, gOdbcConn)

        cbostatus.Items.Add("")
        'cbostatus.Items.Add("Matched With Dump")
        'cbostatus.Items.Add("Not Matched With Dump")
        cbostatus.Items.Add("Packet Pullout")
        cbostatus.Items.Add("Pullout")
        cbostatus.Items.Add("Closed")
        cbostatus.Items.Add("Batched")
        cbostatus.Items.Add("Batched-Entry Not Done")
        cbostatus.Items.Add("Batched-Entry Done")
        cbostatus.Items.Add("Batched-Closed")
        cbostatus.Items.Add("Batched-Packet Pullout")
        cbostatus.Items.Add("Batched-Pullout")
        cbostatus.Items.Add("Entry Done-Not Despatched")
        cbostatus.Items.Add("Entry Done-Despatched")

        cbostatus.SelectedIndex = 0

        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtagreementno.Focus()
        txtagreementno.Text = ""
    End Sub

    Private Sub frmpdcreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Dim lsstatus As String
        Dim lscond As String = ""

        Select Case cbostatus.Text
            'Case "Matched With Dump"

            'Case "Not Matched With Dump"

            Case "Packet Pullout"
                lscond &= " and chq_status & " & GCPACKETPULLOUT & " > 0 "
            Case "Pullout"
                lscond &= " and chq_status & " & GCPULLOUT & " > 0 "
            Case "Closed"
                lscond &= " and chq_status & " & GCCLOSURE & " > 0 "
            Case "Batched"
                lscond &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
            Case "Batched-Entry Not Done"
                lscond &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                lscond &= " and chq_status & " & GCPRESENTATIONDE & " = 0 "
            Case "Batched-Entry Done"
                lscond &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                lscond &= " and chq_status & " & GCPRESENTATIONDE & " > 0 "
            Case "Batched-Closed"
                lscond &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                lscond &= " and chq_status & " & GCCLOSURE & " > 0 "
            Case "Batched-Packet Pullout"
                lscond &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                lscond &= " and chq_status & " & GCPACKETPULLOUT & " > 0 "
            Case "Batched-Pullout"
                lscond &= " and chq_status & " & GCPRESENTATIONPULLOUT & " > 0 "
                lscond &= " and chq_status & " & GCPULLOUT & " > 0 "
            Case "Entry Done-Not Despatched"
                lscond &= " and chq_status & " & GCPRESENTATIONDE & " > 0 "
                lscond &= " and chq_status & " & GCDESPATCH & " = 0 "
            Case "Entry Done-Despatched"
                lscond &= " and chq_status & " & GCPRESENTATIONDE & " > 0 "
                lscond &= " and chq_status & " & GCDESPATCH & " > 0 "
        End Select


        If txtagreementno.Text.Trim <> "" Then
            lscond &= " and shortagreement_no='" & txtagreementno.Text.Trim & "'"
        End If

        If dtpcycledate.Checked Then
            lscond &= " and chq_date='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        End If

        If cboproduct.Text.Trim <> "" Then
            lscond &= " and chq_prodtype=" & cboproduct.SelectedValue
        End If

        If txtchqno.Text.Trim <> "" Then
            lscond &= " and chq_no='" & txtchqno.Text.Trim & "'"
        End If

        lssql = " select group_concat(concat('\'',status_desc,'\'')) from "
        lssql &= "(select status_desc FROM chola_mst_tstatus where status_deleteflag='N' and status_level='Cheque' order by status_flag) as a"
        lsstatus = gfExecuteScalar(lssql, gOdbcConn)

        lssql = " select date_format(chq_date,'%d-%m-%Y') as 'Cycle Date',type_name as 'Product Type',shortagreement_no as 'Agreement No', "
        lssql &= " packet_gnsarefno as 'GNSA REF#',"
        lssql &= " chq_no as 'Cheque No',chq_amount as 'Cheque Amount', "
        lssql &= " make_set(chq_status ," & lsstatus & ") as 'Status',"
        lssql &= " batch_no as 'Batch No' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N'"
        lssql &= " left join chola_trn_tbatch on batch_gid = chq_batch_gid "
        lssql &= " where 1=1 "
        lssql &= lscond

        gpPopGridView(dgvsummary, lssql, gOdbcConn)

        lbltotrec.Text = "Total : " & dgvsummary.RowCount
    End Sub
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpcycledate.Value = Now()
        dtpcycledate.Checked = False
        txtagreementno.Text = ""
        cboproduct.SelectedIndex = -1
        cbostatus.SelectedIndex = -1
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