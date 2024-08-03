Public Class frminwardMIS
    Dim lssql As String
    Dim objdt As New DataTable
    Private Sub frminwardMIS_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frminwardMIS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frminwardMIS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frminwardMIS_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()
        Dim lsSql As String
        Dim objdt As DataTable

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
        lsSql &= " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Date',count(*) as 'Hands off',"
        lsSql &= " sum(if(inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Inward Recd',"
        lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " > 0,1,0)) as 'Not Recd',"
        lsSql &= " sum(if(inward_status & " & GCCOMBINED & " > 0,1,0)) as 'Combined',"
        lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " = 0 and inward_status & " & GCRECEIVED & " = 0 and inward_status & " & GCCOMBINED & " = 0,1,0)) as 'Pending Inward',"
        lsSql &= " sum(if(packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Auth',"
        lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " > 0,1,0)) as 'Rejected',"
        lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " = 0 and packet_status & " & GCAUTHENTRY & " = 0 and inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Auth Pending',"
        lsSql &= " sum(if(packet_status & " & GCPACKETCHEQUEENTRY & " > 0,1,0)) as 'Chq DE Completed',"
        lsSql &= " sum(if(packet_status & " & GCPACKETCHEQUEENTRY & " = 0 and packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Chq DE pending'"
        lsSql &= " from chola_trn_tinward "
        lsSql &= " left join chola_trn_tpacket on packet_gid=inward_packet_gid "
        lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
        lsSql &= " group by inward_receiveddate"

        objdt = GetDataTable(lsSql)
        objdt.Rows.Add()
        objdt.Rows(objdt.Rows.Count - 1)(0) = "Total:"
        For i As Integer = 0 To objdt.Rows.Count - 2
            For j As Integer = 1 To objdt.Columns.Count - 1
                objdt.Rows(objdt.Rows.Count - 1)(j) = Val(objdt.Rows(objdt.Rows.Count - 1)(j).ToString) + Val(objdt.Rows(i)(j).ToString)
            Next
        Next

        dgvsummary.DataSource = objdt

        btnrefresh.Enabled = True
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
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
    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        Dim lsqry As String
        Dim lsdate As String
        Dim lscolumn As String

        If e.RowIndex < 0 Then
            Exit Sub
        End If



        lsdate = dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
        lscolumn = dgvsummary.Columns(dgvsummary.CurrentCell.ColumnIndex).Name

        If Not IsDate(lsdate) Then Exit Sub
        Select Case lscolumn
            Case "Hands off"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
            Case "Inward Recd"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and inward_status & " & GCRECEIVED & " > 0 "
            Case "Not Recd"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and inward_packet_gid=0 "
                lsqry &= " and inward_status & " & GCNOTRECEIVED & " > 0 "
            Case "Combined"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and inward_packet_gid=0 "
                lsqry &= " and inward_status & " & GCCOMBINED & " > 0 "
            Case "Pending Inward"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and inward_packet_gid=0 "
                lsqry &= " and inward_status & " & GCRECEIVED & " = 0 "
                lsqry &= " and inward_status & " & GCNOTRECEIVED & " = 0 "
                lsqry &= " and inward_status & " & GCCOMBINED & " = 0 "
            Case "Auth"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and packet_status & " & GCAUTHENTRY & " > 0 "
            Case "Rejected"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',packet_remarks as 'Remarks' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and packet_status & " & GCREJECTENTRY & " > 0 "
            Case "Auth Pending"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and packet_status & " & GCREJECTENTRY & " = 0 "
                lsqry &= " and packet_status & " & GCAUTHENTRY & " = 0 "
                lsqry &= " and inward_status & " & GCRECEIVED & " > 0 "
            Case "Chq DE Completed"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and packet_status & " & GCAUTHENTRY & " > 0 "
                lsqry &= " and packet_status & " & GCPACKETCHEQUEENTRY & " > 0 "
            Case "Chq DE pending"
                lsqry = " select inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
                lsqry &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
                lsqry &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
                lsqry &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
                lsqry &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
                lsqry &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#' "
                lsqry &= " from chola_trn_tinward"
                lsqry &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
                lsqry &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(CDate(lsdate), "yyyy-MM-dd") & "'"
                lsqry &= " and packet_status & " & GCAUTHENTRY & " > 0 "
                lsqry &= " and packet_status & " & GCPACKETCHEQUEENTRY & " = 0 "
            Case Else
                lsqry = ""
        End Select

        If lsqry <> "" Then
            QuickView(gOdbcConn, lsqry)
        End If
    End Sub

    Private Sub btnexportmis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportmis.Click
        Dim lsSql As String
        Dim objdt As DataTable
        Dim lsFileName As String

        If dtpfrom.Checked = False Then
            MsgBox("Please Select Received From Date", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        If dtpto.Checked = False Then
            MsgBox("Please Select Received To Date", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        lsFileName = "Inward MIS" & Format(Now, "ddMMyyyyhhmmss") & ".xls"
        Dim objxlexport As New XMLExport(gsReportPath & lsFileName)

        With objxlexport

            'Summary Sheet
            lsSql = ""
            lsSql &= " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Date',count(*) as 'Hands off',"
            lsSql &= " sum(if(inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Inward Recd',"
            lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " > 0,1,0)) as 'Not Recd',"
            lsSql &= " sum(if(inward_status & " & GCCOMBINED & " > 0,1,0)) as 'Combined',"
            lsSql &= " sum(if(inward_status & " & GCNOTRECEIVED & " = 0 and inward_status & " & GCRECEIVED & " = 0 and inward_status & " & GCCOMBINED & " = 0,1,0)) as 'Pending Inward',"
            lsSql &= " sum(if(packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Auth',"
            lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " > 0,1,0)) as 'Rejected',"
            lsSql &= " sum(if(packet_status & " & GCREJECTENTRY & " = 0 and packet_status & " & GCAUTHENTRY & " = 0 and inward_status & " & GCRECEIVED & " > 0,1,0)) as 'Auth Pending',"
            lsSql &= " sum(if(packet_status & " & GCPACKETCHEQUEENTRY & " > 0,1,0)) as 'Chq DE Completed',"
            lsSql &= " sum(if(packet_status & " & GCPACKETCHEQUEENTRY & " = 0 and packet_status & " & GCAUTHENTRY & " > 0,1,0)) as 'Chq DE pending'"
            lsSql &= " from chola_trn_tinward "
            lsSql &= " left join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " group by inward_receiveddate"
            lsSql &= " having "
            lsSql &= " (`Pending Inward` <> 0 or `Auth Pending` <> 0 or `Chq DE pending` <> 0 or `Rejected` <> 0)"

            objdt = GetDataTable(lsSql)
            objdt.Rows.Add()
            objdt.Rows(objdt.Rows.Count - 1)(0) = "Total:"
            For i As Integer = 0 To objdt.Rows.Count - 2
                For j As Integer = 1 To objdt.Columns.Count - 1
                    objdt.Rows(objdt.Rows.Count - 1)(j) = Val(objdt.Rows(objdt.Rows.Count - 1)(j).ToString) + Val(objdt.Rows(i)(j).ToString)
                Next
            Next
            If objdt.Rows.Count > 0 Then
                .NewSheet("Summary")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
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

            'Pouch Not Received
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_packet_gid=0 "
            lsSql &= " and inward_status & " & GCNOTRECEIVED & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pouch Not Recd")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
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

            'Pending Inward
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and inward_packet_gid=0 "
            lsSql &= " and inward_status & " & GCRECEIVED & " = 0 "
            lsSql &= " and inward_status & " & GCNOTRECEIVED & " = 0 "
            lsSql &= " and inward_status & " & GCCOMBINED & " = 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Inward")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
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
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCREJECTENTRY & " = 0 "
            lsSql &= " and packet_status & " & GCAUTHENTRY & " = 0 "
            lsSql &= " and inward_status & " & GCRECEIVED & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending Auth")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
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

            'Rejects
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#',packet_remarks as 'Remarks', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCREJECTENTRY & " > 0 "
            objdt = GetDataTable(lsSql)
            If objdt.Rows.Count > 0 Then
                .NewSheet("Rejects")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
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

            'Pending DE
            lsSql = " select date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
            lsSql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
            lsSql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
            lsSql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
            lsSql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
            lsSql &= " inward_compagr as 'Comp Agr',packet_gnsarefno as 'GNSAREF#', "
            lsSql &= " datediff(current_date(),inward_receiveddate)- "
            lsSql &= " (SELECT count(*) FROM chola_mst_tholiday "
            lsSql &= " where holiday_date between  inward_receiveddate and current_date() ) as 'Ageing Days'"
            lsSql &= " from chola_trn_tinward"
            lsSql &= " inner join chola_trn_tpacket on packet_gid=inward_packet_gid "
            lsSql &= " where date_format(inward_receiveddate,'%Y-%m-%d')>='" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and date_format(inward_receiveddate,'%Y-%m-%d')<='" & Format(dtpto.Value, "yyyy-MM-dd") & "'"
            lsSql &= " and packet_status & " & GCAUTHENTRY & " > 0 "
            lsSql &= " and packet_status & " & GCPACKETCHEQUEENTRY & " = 0 "
            objdt = GetDataTable(lsSql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Pending DE")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName.ToString)
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
        Me.Cursor = Cursors.Default
        MsgBox("Report Generated..!", MsgBoxStyle.Information, gProjectName)
        System.Diagnostics.Process.Start(gsReportPath & lsFileName)

    End Sub
End Class