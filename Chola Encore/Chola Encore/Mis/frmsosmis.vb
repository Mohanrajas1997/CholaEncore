Public Class frmsosmis
    Dim lssql As String
    Dim objdt As New DataTable
    Private Sub frmsosmis_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmsosmis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmsosmis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmsosmis_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()
        Dim lsSql As String

        If dtpfrom.Checked = False Then
            MsgBox("Please Select Received From Date", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        If dtpto.Checked = False Then
            MsgBox("Please Select Received To Date", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        btnrefresh.Enabled = False

        lsSql = ""
        lsSql &= " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Date',count(*) as 'Recd',"
        lsSql &= " sum(if(inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Inward',"
        lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " > 0,1,0)) as 'Not Recd',"
        lsSql &= " sum(if(inward_status & " & GCCOMBINED & " > 0,1,0)) as 'Combined',"
        lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " = 0 and inward_status & " & GCRECEIVED & " = 0 and inward_status & " & GCCOMBINED & " = 0,1,0)) as 'Pending Inward',"
        lsSql &= " sum(if(packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Auth',"
        lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " > 0,1,0)) as 'Rejected',"
        lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " = 0 and packet_status & " & GCAUTHENTRY & " = 0 and inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Pending Auth',"
        lsSql &= " sum(if(packet_status & " & GCPACKETVAULTED & " > 0,1,0)) as 'Vaulted',"
        lsSql &= " sum(if(packet_status & " & GCPACKETVAULTED & " = 0 and packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Not Vaulted'"
        lsSql &= " from chola_trn_tinward "
        lsSql &= " left join chola_trn_tpacket on packet_gid=inward_packet_gid "
        lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
        lsSql &= " group by inward_receiveddate "
        lsSql &= " having "
        lsSql &= " (`Pending Inward` <> 0 or `Pending Auth` <> 0 or `Not Vaulted` <> 0 )"

        gpPopGridView(dgvsummary, lsSql, gOdbcConn)
        btnrefresh.Enabled = True
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub btnexport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim objdt As DataTable
        objdt = dgvsummary.DataSource

        If objdt.Rows.Count = 0 Then MsgBox("No Records Found..!", MsgBoxStyle.Information, gProjectName) : Exit Sub

        Dim xlexport As New XMLExport(gsReportPath & "Report.xls")
        With xlexport
            'Summary Sheet
            .NewSheet("Summary")

            .BeginRow()
            For i As Integer = 0 To objdt.Columns.Count - 1
                .AddCellboldgry(objdt.Columns(i).ColumnName)
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
            'Pending Inward
            lssql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lssql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lssql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lssql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lssql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lssql &= " inward_compagr as 'Comp Agr' "
            lssql &= " from chola_trn_tinward"
            lssql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Value), "yyyy-MM-dd") & "'"
            lssql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Value), "yyyy-MM-dd") & "'"
            lssql &= " and inward_packet_gid=0 "
            lssql &= " and inward_status & " & GCRECEIVED & " = 0 "
            lssql &= " and inward_status & " & GCNOTRECEIVED & " = 0 "
            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Inward")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
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
            End If

            'Pending Auth
            lssql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lssql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lssql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lssql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lssql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lssql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
            lssql &= " from chola_trn_tinward"
            lssql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lssql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Value), "yyyy-MM-dd") & "'"
            lssql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Value), "yyyy-MM-dd") & "'"
            lssql &= " and packet_status & " & GCREJECTENTRY & " = 0 "
            lssql &= " and packet_status & " & GCAUTHENTRY & " = 0 "
            lssql &= " and inward_status & " & GCRECEIVED & " > 0 "
            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Auth")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
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
            End If

            'Rejected
            lssql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lssql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lssql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lssql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lssql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lssql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',packet_remarks as 'Reject Remarks' "
            lssql &= " from chola_trn_tinward"
            lssql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lssql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Value), "yyyy-MM-dd") & "'"
            lssql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Value), "yyyy-MM-dd") & "'"
            lssql &= " and packet_status & " & GCREJECTENTRY & " > 0 "
            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Rejected")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
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
            End If

            'Not Vaulted
            lssql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lssql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lssql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lssql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lssql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lssql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',packet_remarks as 'Reject Remarks' "
            lssql &= " from chola_trn_tinward"
            lssql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lssql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Value), "yyyy-MM-dd") & "'"
            lssql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Value), "yyyy-MM-dd") & "'"
            lssql &= " and packet_status & " & GCPACKETVAULTED & " = 0 and packet_status & " & GCAUTHENTRY & " > 0"
            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Not Vaulted")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
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
            End If
            .Close()
        End With

        System.Diagnostics.Process.Start(gsReportPath & "Report.xls")
        MsgBox("Report Generated", MsgBoxStyle.Information, gProjectName)

    End Sub
End Class