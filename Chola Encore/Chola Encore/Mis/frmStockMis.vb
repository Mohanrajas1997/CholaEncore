Public Class frmStockMis
    Private Sub frmstockmis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmstockmis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpRcvdFrom.Value = Now
        dtpRcvdTo.Value = Now

        dtpRcvdFrom.Checked = False
        dtpRcvdTo.Checked = False

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
            Case "SPDC"
                Call LoadDataSpdc()
            Case "SPDC HEADER"
                Call LoadDataSpdcHeader()
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
            lsSql &= " a.agreement_no as 'Agreement No',p.packet_mode as 'Pay Mode',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chq_status & " & GCPRESENTATIONDE & " > 0 and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault',"
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

    Private Sub LoadDataSpdc()
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
            lsSql &= " a.agreement_no as 'Agreement No',p.packet_mode as 'Pay Mode',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(chqentry_status & " & GCPRESENTATIONDE & " > 0 and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0,1,0)) as 'Presented',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " > 0,1,0)) as 'Pullout',"
            lsSql &= " sum(if(chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0,1,0)) as 'Bal in Vault',"
            lsSql &= " sum(if(chqentry_status & " & GCBOUNCERECEIVED & " > 0,1,0)) as 'Bounce Received',"
            lsSql &= " sum(if(chqentry_status & " & GCBOUNCEPULLOUTENTRY & " > 0,1,0)) as 'Bounce Pullout',"
            lsSql &= " sum(if(chqentry_status & " & GCBOUNCERECEIVED & " > 0 and chqentry_status & " & GCBOUNCEPULLOUTENTRY & " = 0,1,0)) as 'Bal Bounce in Vault' "
            lsSql &= " from chola_trn_tspdcchqentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.chqentry_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = p.packet_agreement_gid"

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
            lsSql &= " a.agreement_no as 'Agreement No',p.packet_mode as 'Pay Mode',"
            lsSql &= " sum(spdcentry_spdccount) as 'SPDC',"
            lsSql &= " sum(spdcentry_ecsmandatecount) as 'Mandate' "
            lsSql &= " from chola_trn_tspdcentry e"
            lsSql &= " inner join chola_trn_tpacket p on p.packet_gid=e.spdcentry_packet_gid"
            lsSql &= " inner join chola_mst_tagreement a on a.agreement_gid = p.packet_agreement_gid"

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