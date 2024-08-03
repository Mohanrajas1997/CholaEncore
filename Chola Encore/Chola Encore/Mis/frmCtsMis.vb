Public Class frmCtsMis
    Dim lssql As String
    Dim objdt As New DataTable
    Private Sub frmctsmis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub FillCombo()
        'Fill Type
        cbotype.Items.Insert(0, "All")
        cbotype.Items.Insert(1, "PDC")
        cbotype.Items.Insert(2, "SPDC")

        'Fill CTS Status
        cboctsstatus.Items.Insert(0, "All")
        cboctsstatus.Items.Insert(1, "CTS")
        cboctsstatus.Items.Insert(2, "Non-CTS")
        cboctsstatus.Items.Insert(3, "NA")

        'Fill Cheque Status
        cbochqstatus.Items.Insert(0, "All")
        cbochqstatus.Items.Insert(1, "Available")
        cbochqstatus.Items.Insert(2, "Not Available")

    End Sub

    Private Sub Clear()
        cbotype.SelectedIndex = -1
        cboctsstatus.SelectedIndex = -1
        cbochqstatus.SelectedIndex = -1
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        msfGrid.DataSource = Nothing
    End Sub

    Private Sub GridProperty()
        Dim i As Integer = 0

        With msfGrid
            .Cols = 13
            .Rows = 2

            'First Header Row
            .set_TextMatrix(0, 0, "SNo")
            .set_TextMatrix(0, 1, "Date")
            .set_TextMatrix(0, 2, "PDC")     'PDC Packet
            .set_TextMatrix(0, 3, "PDC")     'PDC CTS
            .set_TextMatrix(0, 4, "PDC")     'PDC Non CTS
            .set_TextMatrix(0, 5, "PDC")     'PDC NA
            .set_TextMatrix(0, 6, "SPDC Cheque Wise")    'SPDC Packet
            .set_TextMatrix(0, 7, "SPDC Cheque Wise")    'SPDC CTS
            .set_TextMatrix(0, 8, "SPDC Cheque Wise")    'SPDC Non CTS
            .set_TextMatrix(0, 9, "SPDC Cheque Wise")    'SPDC NA
            .set_TextMatrix(0, 10, "SPDC Header Wise")    'SPDC Packet
            .set_TextMatrix(0, 11, "SPDC Header Wise")    'SPDC CTS
            .set_TextMatrix(0, 12, "SPDC Header Wise")    'SPDC Non CTS

            'Second Header Row
            .set_TextMatrix(1, 0, "SNo")
            .set_TextMatrix(1, 1, "Date")
            .set_TextMatrix(1, 2, "PKT")     'PDC Packet
            .set_TextMatrix(1, 3, "CTS")     'PDC CTS
            .set_TextMatrix(1, 4, "Non CTS") 'PDC Non CTS
            .set_TextMatrix(1, 5, "NA")      'PDC NA
            .set_TextMatrix(1, 6, "PKT")     'SPDC Packet
            .set_TextMatrix(1, 7, "CTS")     'SPDC CTS
            .set_TextMatrix(1, 8, "Non CTS") 'SPDC Non CTS
            .set_TextMatrix(1, 9, "NA")      'SPDC NA
            .set_TextMatrix(1, 10, "PKT")     'SPDC Packet
            .set_TextMatrix(1, 11, "CTS")     'SPDC CTS
            .set_TextMatrix(1, 12, "Non CTS") 'SPDC Non CTS

            .set_ColWidth(0, 1440 * 0.4)
            .set_ColWidth(1, 1440 * 1)
            .set_ColWidth(2, 1440 * 1)
            .set_ColWidth(3, 1440 * 1)
            .set_ColWidth(4, 1440 * 1)
            .set_ColWidth(5, 1440 * 1)
            .set_ColWidth(6, 1440 * 1)
            .set_ColWidth(7, 1440 * 1)
            .set_ColWidth(8, 1440 * 1)
            .set_ColWidth(9, 1440 * 1)
            .set_ColWidth(10, 1440 * 1)
            .set_ColWidth(11, 1440 * 1)
            .set_ColWidth(12, 1440 * 1)

            .MergeCells = MSFlexGridLib.MergeCellsSettings.flexMergeFree
            .WordWrap = True

            .set_MergeRow(0, True)
            .set_MergeRow(1, True)

            .set_MergeCol(0, True)
            .set_MergeCol(1, True)

            ' Set column header alignment

            For i = 0 To .FixedRows - 1
                .Row = i

                For j = 0 To .Cols - 1
                    .Col = j
                    .CellAlignment = 4
                Next j
            Next i

            .RowHeightMin = 315

        End With
    End Sub

    Private Sub FillGrid()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lsTbCond As String = ""

        Dim lnMonthCnt As Integer

        Dim lnTot As Double
        Dim i As Integer, j As Integer
        Dim row As Integer, c As Integer = 0

        Try
            lsCond = ""

            Select Case cbotype.Text
                Case "PDC"
                    lsCond &= "  and packet_mode = 'PDC' "
                Case "SPDC"
                    lsCond &= "  and packet_mode = 'SPDC' "
            End Select

            With msfGrid
                .Rows = .FixedRows

                lnMonthCnt = DateDiff(DateInterval.Day, dtpfrom.Value, dtpto.Value) + 1

                For i = 0 To lnMonthCnt - 1
                    row = .Rows
                    .Rows = .Rows + 1
                    c += 1

                    'S.No
                    .set_TextMatrix(row, 0, c)
                    'Date
                    .set_TextMatrix(row, 1, Format(DateAdd(DateInterval.Day, i, dtpfrom.Value), "dd-MMM-yyyy"))

                    'PDC Packet
                    lsSql = ""
                    lsSql &= " select count(packet_gid) 'pdccnt' from chola_trn_tpacket p"
                    lsSql &= " where p.packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and p.packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and p.packet_mode = 'PDC' "
                    lsSql &= lsCond
                    lsSql &= IIf(cbotype.Text = "SPDC", " and 1 = 2", "")

                    ds = gfDataSet(lsSql, "PDC", gOdbcConn)

                    If ds.Tables("PDC").Rows.Count > 0 Then
                        .set_TextMatrix(row, 2, Val(ds.Tables("PDC").Rows(0).Item("pdccnt").ToString))
                    End If

                    Application.DoEvents()

                    'PDC
                    lsSql = ""
                    lsSql &= " select"
                    lsSql &= " sum(if(chq_iscts='Y',1,0)) as 'cts',"
                    lsSql &= " sum(if(chq_iscts='N',1,0)) as 'noncts',"
                    lsSql &= " sum(if(chq_iscts='' or chq_iscts is null,1,0)) as 'na'"
                    lsSql &= " from chola_trn_tpdcentry e"
                    lsSql &= " inner join chola_trn_tpacket p on e.chq_packet_gid=p.packet_gid"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and p.packet_mode = 'PDC' "
                    lsSql &= lsCond
                    lsSql &= IIf(cbotype.Text = "SPDC", " and 1 = 2", "")

                    Select Case cboctsstatus.Text
                        Case "CTS"
                            lsSql &= " and chq_iscts='Y'"
                        Case "Non-CTS"
                            lsSql &= " and chq_iscts='N'"
                        Case "NA"
                            lsSql &= " and (chq_iscts='' or chq_iscts is null)"
                    End Select

                    ds = gfDataSet(lsSql, "PDCcnt", gOdbcConn)

                    If ds.Tables("PDCcnt").Rows.Count > 0 Then
                        .set_TextMatrix(row, 3, Val(ds.Tables("PDCcnt").Rows(0).Item("cts").ToString))
                        .set_TextMatrix(row, 4, Val(ds.Tables("PDCcnt").Rows(0).Item("noncts").ToString))
                        .set_TextMatrix(row, 5, Val(ds.Tables("PDCcnt").Rows(0).Item("na").ToString))
                    End If

                    Application.DoEvents()

                    'SPDC Cheque wise Packet
                    lsSql = ""
                    lsSql &= " select count(packet_gid) 'spdccnt' from chola_trn_tpacket"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_mode='SPDC' "
                    lsSql &= IIf(cbotype.Text = "PDC", " and 1=2", "")
                    lsSql &= lsCond

                    ds = gfDataSet(lsSql, "SPDC", gOdbcConn)

                    If ds.Tables("SPDC").Rows.Count > 0 Then
                        .set_TextMatrix(row, 6, Val(ds.Tables("SPDC").Rows(0).Item("spdccnt").ToString))
                    End If

                    Application.DoEvents()

                    'SPDC Cheque wise
                    lsSql = ""
                    lsSql &= " select"
                    lsSql &= " sum(if(chqentry_iscts='Y',1,0)) as 'cts',"
                    lsSql &= " sum(if(chqentry_iscts='N',1,0)) as 'noncts',"
                    lsSql &= " sum(if(chqentry_iscts='' or chqentry_iscts is null,1,0)) as 'na'"
                    lsSql &= " from chola_trn_tspdcchqentry e"
                    lsSql &= " inner join chola_trn_tpacket p on e.chqentry_packet_gid=p.packet_gid"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= IIf(cbotype.Text = "PDC", " and 1=2", "")

                    Select Case cboctsstatus.Text
                        Case "CTS"
                            lsSql &= " and chqentry_iscts='Y'"
                        Case "Non-CTS"
                            lsSql &= " and chqentry_iscts='N'"
                        Case "NA"
                            lsSql &= " and (chqentry_iscts='' or chqentry_iscts is null)"
                    End Select

                    ds = gfDataSet(lsSql, "SPDCcnt", gOdbcConn)

                    If ds.Tables("SPDCcnt").Rows.Count > 0 Then
                        .set_TextMatrix(row, 7, Val(ds.Tables("SPDCcnt").Rows(0).Item("cts").ToString))
                        .set_TextMatrix(row, 8, Val(ds.Tables("SPDCcnt").Rows(0).Item("noncts").ToString))
                        .set_TextMatrix(row, 9, Val(ds.Tables("SPDCcnt").Rows(0).Item("na").ToString))
                    End If

                    Application.DoEvents()

                    'SPDC Header Wise
                    lsSql = ""
                    lsSql &= " SELECT count(packet_gid) as 'pkt',sum(spdcentry_ctschqcount) as 'cts',sum(spdcentry_nonctschqcount) as 'noncts'"
                    lsSql &= " FROM chola_trn_tspdcentry"
                    lsSql &= " inner join chola_trn_tpacket on packet_gid=spdcentry_packet_gid"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpfrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_mode='SPDC' "
                    lsSql &= IIf(cbotype.Text = "PDC", " and 1=2", "")
                    lsSql &= lsCond

                    ds = gfDataSet(lsSql, "SPDCHeader", gOdbcConn)

                    If ds.Tables("SPDCHeader").Rows.Count > 0 Then
                        .set_TextMatrix(row, 10, Val(ds.Tables("SPDCHeader").Rows(0).Item("pkt").ToString))
                        .set_TextMatrix(row, 11, Val(ds.Tables("SPDCHeader").Rows(0).Item("cts").ToString))
                        .set_TextMatrix(row, 12, Val(ds.Tables("SPDCHeader").Rows(0).Item("noncts").ToString))
                    End If

                    ds.Tables.Clear()

                    Application.DoEvents()
                Next i

                row = .Rows
                .Rows = .Rows + 1
                .set_TextMatrix(row, 1, "Total")

                .set_ColAlignment(0, 4)
                .set_ColAlignment(1, 4)

                For i = 2 To .Cols - 1
                    lnTot = 0

                    For j = .FixedRows To .Rows - 2
                        lnTot += Val(.get_TextMatrix(j, i))
                    Next j

                    .set_TextMatrix(row, i, lnTot)
                Next i
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gProjectName)
        End Try
    End Sub

    Private Sub frmctsmis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        Call FillCombo()
        Call Clear()
        Call GridProperty()
    End Sub

    Private Sub frmctsmis_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With msfGrid
            .Left = pnlCtrl.Left
            .Top = pnlCtrl.Top + pnlCtrl.Height + 6
            .Width = Me.Width - 30
            .Height = Me.Height - 140
        End With
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
            If dtpfrom.Checked = False Then
                MsgBox("Please Select Received From Date", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            If dtpto.Checked = False Then
                MsgBox("Please Select Received To Date", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            btnrefresh.Enabled = False

            Call FillGrid()

            btnrefresh.Enabled = True
            Me.Cursor = System.Windows.Forms.Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
            btnrefresh.Enabled = True
        End Try
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Try
            PrintFGridXMLMerge(msfGrid, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class