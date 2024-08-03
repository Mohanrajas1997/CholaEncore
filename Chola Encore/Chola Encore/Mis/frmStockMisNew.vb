Public Class frmStockMisNew
    Private Sub frmstockmis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmstockmis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpRcvdFrom.Value = Now
        dtpRcvdTo.Value = Now

        dtpRcvdFrom.Checked = False
        dtpRcvdTo.Checked = False

        With cboCtsStatus
            .Items.Clear()
            .Items.Add("All")
            .Items.Add("CTS")
            .Items.Add("Non CTS")
            .Items.Add("Unknown")
        End With

        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtgnsarefno.Focus()
        txtgnsarefno.Text = ""
    End Sub

    Private Sub frmstockmis_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            With dgvstockmis
                pnlMain.Width = Me.Width - 30
                pnlMain.Height = Me.Height - pnlButtons.Height - 90
                .Width = pnlMain.Width
                .Height = pnlMain.Height
                pnlDisplay.Width = Me.Width - 30
                pnlDisplay.Top = pnlButtons.Top + pnlButtons.Height + dgvstockmis.Height + 15
                btnExport.Left = pnlDisplay.Width - (btnExport.Width + 10)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor

        Select Case cbopaymode.Text
            Case "PDC"
                Call LoadDataPdc()
            Case "PDC INWARD"
                Call LoadDataPdcInward()
            Case "PDC SUMMARY"
                Call LoadDataPdcSummary()
            Case "PDC RCVD SUMMARY"
                Call LoadDataPdcRcvdSummary()
            Case "PDC INWARD SUMMARY"
                Call LoadDataPdcInwardSummary()
            Case "PDC RCVD TOTAL"
                Call LoadDataPdcRcvdTotal()
            Case "PDC INWARD TOTAL"
                Call LoadDataPdcInwardTotal()
            Case "PDC DETAIL"
                Call LoadDataPdcDetail()
            Case "SPDC"
                Call LoadDataSpdc()
            Case "SPDC INWARD"
                Call LoadDataSpdcInward()
            Case "SPDC RCVD SUMMARY"
                Call LoadDataSpdcRcvdSummary()
            Case "SPDC INWARD SUMMARY"
                Call LoadDataSpdcInwardSummary()
            Case "SPDC RCVD TOTAL"
                Call LoadDataSpdcRcvdTotal()
            Case "SPDC INWARD TOTAL"
                Call LoadDataSpdcInwardTotal()
            Case "SPDC HEADER"
                Call LoadDataSpdcHeader()
            Case "PDC CURRENT STOCK"
                Call LoadPdcCurrentStock()
            Case "SPDC CURRENT STOCK"
                Call LoadSpdcCurrentStock()
        End Select

        btnRefresh.Enabled = True
        If dgvstockmis.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadDataPdc()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No',m.almaraentry_cupboardno as 'Cup Board No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno 'Box No','PDC' as 'Table','PDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts = 'Y',1,0)) as 'CTS in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts = 'N',1,0)) as 'Non-CTS in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts is null,1,0)) as 'Unknown CTS in Vault',"
            lsSql &= " group_concat(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,chq_no,'')) as 'Chq No',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCERECEIVED & " > 0,1,0)) as 'Bounce Received',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCEPULLOUTENTRY & " > 0,1,0)) as 'Bounce Pullout',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCERECEIVED & " > 0 and chq_status & " & GCBOUNCEPULLOUTENTRY & " = 0,1,0)) as 'Bal Bounce in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= " group by 'Date Of Receipt',packet_gnsarefno,agreement_no,packet_mode,almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No',m.almaraentry_cupboardno as 'Cup Board No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno 'Box No','PDC' as 'Table','SPDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts = 'Y',1,0)) as 'CTS in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts = 'N',1,0)) as 'Non-CTS in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts is null,1,0)) as 'Unknown CTS in Vault',"
            lsSql &= " group_concat(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,chq_no,'')) as 'Chq No',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCERECEIVED & " > 0,1,0)) as 'Bounce Received',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCEPULLOUTENTRY & " > 0,1,0)) as 'Bounce Pullout',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCERECEIVED & " > 0 and chq_status & " & GCBOUNCEPULLOUTENTRY & " = 0,1,0)) as 'Bal Bounce in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= " group by 'Date Of Receipt',packet_gnsarefno,agreement_no,packet_mode,almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadSpdcCurrentStock()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chqentry_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chqentry_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chqentry_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " count(*) as 'Current Stock' "
            lsSql &= " from chola_trn_tspdcchqentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chqentry_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = p.packet_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
            lsSql &= " "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
    Private Sub LoadPdcCurrentStock()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = ""

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select sum(a.current_stock) as 'Current Stock' from ("
            lsSql &= " select "
            lsSql &= " count(*) as current_stock "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " count(*) as current_stock "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
            lsSql &= lsGrpCond

            lsSql &= ") as a "

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub


    Private Sub LoadDataPdcSummary()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(e.chq_date,'%d-%m-%Y') as 'Chq Date',"
            lsSql &= " 'PDC' as 'Table','PDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= " group by e.chq_date "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " date_format(e.chq_date,'%d-%m-%Y') as 'Chq Date',"
            lsSql &= " 'PDC' as 'Table','SPDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= " group by e.chq_date "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataPdcRcvdSummary()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',"
            lsSql &= " 'PDC' as 'Table','PDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= " group by date_format(p.packet_receiveddate,'%d-%m-%Y') "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',"
            lsSql &= " 'PDC' as 'Table','SPDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= " group by date_format(p.packet_receiveddate,'%d-%m-%Y') "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataPdcInwardSummary()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  i.inward_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  i.inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " i.inward_receiveddate as 'Inward Date',"
            lsSql &= " 'PDC' as 'Table','PDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tinward as i "
            lsSql &= " inner join chola_trn_tpacket p on i.inward_packet_gid = p.packet_gid "
            lsSql &= " inner join chola_trn_tpdcentry e on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= " group by i.inward_receiveddate "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " i.inward_receiveddate as 'Inward Date',"
            lsSql &= " 'PDC' as 'Table','SPDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tinward as i "
            lsSql &= " inner join chola_trn_tpacket p on i.inward_packet_gid = p.packet_gid "
            lsSql &= " inner join chola_trn_tpdcentry e on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= " group by i.inward_receiveddate "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataPdcInward()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  i.inward_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  i.inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " i.inward_receiveddate as 'Inward Date',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No','PDC' as 'Table','PDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " inner join chola_trn_tinward as i on p.packet_gid = i.inward_packet_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= " group by i.inward_receiveddate,packet_gnsarefno,agreement_no "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " i.inward_receiveddate as 'Inward Date',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No','PDC' as 'Table','SPDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " inner join chola_trn_tinward as i on p.packet_gid = i.inward_packet_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= " group by 'Date Of Receipt',packet_gnsarefno,agreement_no "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
    Private Sub LoadDataPdcInwardTotal()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  i.inward_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  i.inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " 'PDC' as 'Table','PDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tinward as i "
            lsSql &= " inner join chola_trn_tpacket p on i.inward_packet_gid = p.packet_gid "
            lsSql &= " inner join chola_trn_tpdcentry e on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " 'PDC' as 'Table','SPDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tinward as i "
            lsSql &= " inner join chola_trn_tpacket p on i.inward_packet_gid = p.packet_gid "
            lsSql &= " inner join chola_trn_tpdcentry e on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
    Private Sub LoadDataPdcRcvdTotal()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " 'PDC' as 'Table','PDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " 'PDC' as 'Table','SPDC' as 'Type',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
    Private Sub LoadDataPdcDetail()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chq_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chq_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chq_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No',e.chq_no as 'Chq No',e.chq_date as 'Chq Date',e.chq_amount as 'Chq Amount',"
            lsSql &= " m.almaraentry_cupboardno as 'Cup Board No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno 'Box No','PDC' as 'Table','PDC' as 'Type' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALNORMAL & " "
            lsSql &= " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
            'lsSql &= lsGrpCond

            lsSql &= " union all "

            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No',e.chq_no as 'Chq No',e.chq_date as 'Chq Date',e.chq_amount as 'Chq Amount',"
            lsSql &= " m.almaraentry_cupboardno as 'Cup Board No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno 'Box No','PDC' as 'Table','SPDC' as 'Type' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " and e.chq_type = " & GCEXTERNALSECURITY & " "
            lsSql &= " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
            'lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataSpdc()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chqentry_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chqentry_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chqentry_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No',m.almaraentry_cupboardno as 'Cup Board No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno 'Box No','SPDC' as 'Table',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chqentry_status & " & GCPRESENTATIONDE & " > 0 and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chqentry_iscts = 'Y',1,0)) as 'CTS in Vault',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chqentry_iscts = 'N',1,0)) as 'Non-CTS in Vault',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chqentry_iscts is null,1,0)) as 'Unknown CTS in Vault',"
            lsSql &= " group_concat(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,chqentry_chqno,'')) as 'Chq No',"
            lsSql &= " sum(if(chqentry_status & " & GCBOUNCERECEIVED & " > 0,1,0)) as 'Bounce Received',"
            lsSql &= " sum(if(chqentry_status & " & GCBOUNCEPULLOUTENTRY & " > 0,1,0)) as 'Bounce Pullout',"
            lsSql &= " sum(if(chqentry_status & " & GCBOUNCERECEIVED & " > 0 and chqentry_status & " & GCBOUNCEPULLOUTENTRY & " = 0,1,0)) as 'Bal Bounce in Vault' "
            lsSql &= " from chola_trn_tspdcchqentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chqentry_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = p.packet_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= " group by 'Date Of Receipt',packet_gnsarefno,agreement_no,packet_mode,almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno"
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataSpdcInward()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  i.inward_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  i.inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chqentry_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chqentry_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chqentry_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " i.inward_receiveddate as 'Inward Date',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No','SPDC' as 'Table',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chqentry_status & " & GCPRESENTATIONDE & " > 0 and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tspdcchqentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chqentry_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = p.packet_agreement_gid"
            lsSql &= " inner join chola_trn_tinward as i on p.packet_gid = i.inward_packet_gid "

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= " group by i.inward_receiveddate,packet_gnsarefno,agreement_no"
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataSpdcRcvdSummary()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chqentry_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chqentry_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chqentry_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt','SPDC' as 'Table',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chqentry_status & " & GCPRESENTATIONDE & " > 0 and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tspdcchqentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chqentry_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= " group by date_format(p.packet_receiveddate,'%d-%m-%Y')"
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataSpdcInwardSummary()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  i.inward_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  i.inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chqentry_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chqentry_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chqentry_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " i.inward_receiveddate as 'Inward Date','SPDC' as 'Table',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chqentry_status & " & GCPRESENTATIONDE & " > 0 and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tinward as i "
            lsSql &= " inner join chola_trn_tpacket p on i.inward_packet_gid = p.packet_gid "
            lsSql &= " inner join chola_trn_tspdcchqentry e on p.packet_gid=e.chqentry_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= " group by i.inward_receiveddate"
            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataSpdcInwardTotal()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  i.inward_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  i.inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chqentry_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chqentry_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chqentry_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " 'SPDC' as 'Table',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chqentry_status & " & GCPRESENTATIONDE & " > 0 and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tinward as i "
            lsSql &= " inner join chola_trn_tpacket p on i.inward_packet_gid = p.packet_gid "
            lsSql &= " inner join chola_trn_tspdcchqentry e on p.packet_gid=e.chqentry_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
    Private Sub LoadDataSpdcRcvdTotal()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""
        Dim lsGrpCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            lsGrpCond = " having sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) > 0 "

            Select Case cboCtsStatus.Text.ToUpper
                Case "CTS"
                    lsCond &= " and chqentry_iscts = 'Y' "
                Case "NON CTS"
                    lsCond &= " and chqentry_iscts = 'N' "
                Case "UNKNOWN"
                    lsCond &= " and chqentry_iscts is null "
                Case Else
                    lsGrpCond = ""
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " 'SPDC' as 'Table',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chqentry_status & " & GCPRESENTATIONDE & " > 0 and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault' "
            lsSql &= " from chola_trn_tspdcchqentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chqentry_packet_gid"

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= lsGrpCond

            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
    Private Sub LoadDataSpdcHeader()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No',m.almaraentry_cupboardno as 'Cup Board No',m.almaraentry_shelfno as 'Shelf No',m.almaraentry_boxno 'Box No','SPDC Header' as 'Table',"
            lsSql &= " sum(spdcentry_spdccount) as 'SPDC',"
            lsSql &= " sum(spdcentry_ecsmandatecount) as 'Mandate' "
            lsSql &= " from chola_trn_tspdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.spdcentry_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = p.packet_agreement_gid"
            lsSql &= " left join chola_trn_almaraentry m on m.almaraentry_gid = p.packet_box_gid "

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= " group by 'Date Of Receipt',packet_gnsarefno,agreement_no,packet_mode,almaraentry_cupboardno,almaraentry_shelfno,almaraentry_boxno"


            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub LoadDataOld()
        btnRefresh.Enabled = False
        Dim ds As New DataSet
        Dim lsSql As String = ""
        Dim lsCond As String = ""

        Try
            If dtpRcvdFrom.Checked Then
                lsCond &= " and  p.packet_receiveddate >='" & Format(dtpRcvdFrom.Value, "yyyy-MM-dd") & "'"
            End If

            If dtpRcvdTo.Checked Then
                lsCond &= " and  p.packet_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRcvdTo.Value), "yyyy-MM-dd") & "'"
            End If

            If txtgnsarefno.Text <> "" Then
                lsCond &= " and p.packet_gnsarefno = '" & txtgnsarefno.Text.Trim & "' "
            End If

            If Not (cbopaymode.SelectedIndex = -1 Or cbopaymode.Text.Trim = "") Then
                lsCond &= " and p.packet_mode ='" & cbopaymode.Text & "'"
            End If

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " date_format(p.packet_receiveddate,'%d-%m-%Y') as 'Date Of Receipt',p.packet_gnsarefno as 'GNSA Ref No',"
            lsSql &= " a.agreement_no as 'Agreement No',p.packet_mode as 'Pay Mode',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts = 'Y',1,0)) as 'CTS in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts = 'N',1,0)) as 'Non-CTS in Vault',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 and chq_iscts is null,1,0)) as 'Non-CTS in Vault',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCERECEIVED & " > 0,1,0)) as 'Bounce Received',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCEPULLOUTENTRY & " > 0,1,0)) as 'Bounce Pullout',"
            lsSql &= " sum(if(chq_status & " & GCBOUNCERECEIVED & " > 0 and chq_status & " & GCBOUNCEPULLOUTENTRY & " = 0,1,0)) as 'Bal Bounce in Vault' "
            lsSql &= " from chola_trn_tpdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chq_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = e.chq_agreement_gid"

            lsSql &= " where true "
            lsSql &= lsCond

            lsSql &= " group by 'Date Of Receipt',packet_gnsarefno,agreement_no,packet_mode"


            Call gpPopGridView(dgvstockmis, lsSql, gOdbcConn)

            For i = 0 To dgvstockmis.Columns.Count - 1
                dgvstockmis.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Record Count: " & dgvstockmis.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dtpRcvdFrom.Checked = False
        dtpRcvdTo.Checked = False
        txtgnsarefno.Text = ""
        cbopaymode.Text = ""
        dgvstockmis.DataSource = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If dgvstockmis.RowCount = 0 Then
                MsgBox("No Details to Export!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            Call PrintDGViewXML(dgvstockmis, gsReportPath & "Packet Report.xls", "Packet Details")

            MsgBox(" Exported to Excel !!" & Chr(13) & _
                   " Saved Path : " & gsReportPath & "Packet Report", MsgBoxStyle.Information, gProjectName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class