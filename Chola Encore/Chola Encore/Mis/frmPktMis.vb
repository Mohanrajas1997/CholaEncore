Public Class frmPktMis
    Dim lssql As String
    Dim objdt As New DataTable
    Private Sub frmctsmis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub Clear()
        dtpFrom.Value = Now()
        dtpFrom.Checked = False
        dtpTo.Value = Now()
        dtpTo.Checked = False
        msfGrid.DataSource = Nothing
    End Sub

    Private Sub GridProperty()
        Dim i As Integer = 0

        With msfGrid
            .Cols = 7
            .Rows = 2

            .FixedRows = 1
            .FixedCols = 1

            'First Header Row
            .set_TextMatrix(0, 0, "SNo")
            .set_TextMatrix(0, 1, "Date")
            .set_TextMatrix(0, 2, "Packet")
            .set_TextMatrix(0, 3, "PDC")
            .set_TextMatrix(0, 4, "SPDC")
            .set_TextMatrix(0, 5, "ECS")
            .set_TextMatrix(0, 6, "Mandate")

            .set_ColWidth(0, 1440 * 0.4)
            .set_ColWidth(1, 1440 * 1)
            .set_ColWidth(2, 1440 * 1)
            .set_ColWidth(3, 1440 * 1)
            .set_ColWidth(4, 1440 * 1)
            .set_ColWidth(5, 1440 * 1)
            .set_ColWidth(6, 1440 * 1)

            .MergeCells = MSFlexGridLib.MergeCellsSettings.flexMergeFree
            .WordWrap = True

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

            With msfGrid
                .Rows = .FixedRows

                lnMonthCnt = DateDiff(DateInterval.Day, dtpFrom.Value, dtpTo.Value) + 1

                For i = 0 To DateDiff(DateInterval.Day, dtpFrom.Value, dtpTo.Value)
                    row = .Rows
                    .Rows = .Rows + 1
                    c += 1

                    'S.No
                    .set_TextMatrix(row, 0, c)
                    'Date
                    .set_TextMatrix(row, 1, Format(DateAdd(DateInterval.Day, i, dtpFrom.Value), "dd-MMM-yyyy"))

                    'PDC Packet
                    lsSql = ""
                    lsSql &= " select count(*) as cnt from chola_trn_tpacket p"
                    lsSql &= " where p.packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpFrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and p.packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpFrom.Value), "yyyy-MM-dd") & "'"

                    Call gpDataSet(lsSql, "cnt", gOdbcConn, ds)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        .set_TextMatrix(row, 2, Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString))
                    End If

                    ds.Tables("cnt").Rows.Clear()
                    ds.Tables("cnt").Columns.Clear()

                    Application.DoEvents()

                    'PDC
                    lsSql = ""
                    lsSql &= " select"
                    lsSql &= " sum(if(chq_type <> " & GCEXTERNALSECURITY & ",1,0)) as pdc,"
                    lsSql &= " sum(if(chq_type = " & GCEXTERNALSECURITY & ",1,0)) as spdc "
                    lsSql &= " from chola_trn_tpdcentry e"
                    lsSql &= " inner join chola_trn_tpacket p on e.chq_packet_gid=p.packet_gid"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpFrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpFrom.Value), "yyyy-MM-dd") & "'"

                    Call gpDataSet(lsSql, "pdc", gOdbcConn, ds)

                    If ds.Tables("pdc").Rows.Count > 0 Then
                        .set_TextMatrix(row, 3, Val(ds.Tables("pdc").Rows(0).Item("pdc").ToString))
                        .set_TextMatrix(row, 4, Val(ds.Tables("pdc").Rows(0).Item("spdc").ToString))
                    End If

                    ds.Tables("pdc").Rows.Clear()
                    ds.Tables("pdc").Columns.Clear()

                    Application.DoEvents()

                    'SPDC Cheque wise
                    lsSql = ""
                    lsSql &= " select count(*) as cnt "
                    lsSql &= " from chola_trn_tspdcchqentry e"
                    lsSql &= " inner join chola_trn_tpacket p on e.chqentry_packet_gid=p.packet_gid"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpFrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpFrom.Value), "yyyy-MM-dd") & "'"

                    Call gpDataSet(lsSql, "spdc", gOdbcConn, ds)

                    If ds.Tables("spdc").Rows.Count > 0 Then
                        .set_TextMatrix(row, 4, (Val(.get_TextMatrix(row, 4)) + Val(ds.Tables("spdc").Rows(0).Item("cnt")).ToString))
                    End If

                    ds.Tables("spdc").Rows.Clear()
                    ds.Tables("spdc").Columns.Clear()

                    Application.DoEvents()

                    'ECS
                    lsSql = ""
                    lsSql &= " select count(*) as cnt "
                    lsSql &= " from chola_trn_tecsemientry e"
                    lsSql &= " inner join chola_trn_tpacket p on e.ecsemientry_packet_gid=p.packet_gid"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpFrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpFrom.Value), "yyyy-MM-dd") & "'"

                    Call gpDataSet(lsSql, "ecs", gOdbcConn, ds)

                    If ds.Tables("ecs").Rows.Count > 0 Then
                        .set_TextMatrix(row, 5, Val(ds.Tables("ecs").Rows(0).Item("cnt").ToString))
                    End If

                    ds.Tables("ecs").Rows.Clear()
                    ds.Tables("ecs").Columns.Clear()

                    Application.DoEvents()

                    'SPDC Header Wise
                    lsSql = ""
                    lsSql &= " SELECT sum(spdcentry_ecsmandatecount) as cnt"
                    lsSql &= " FROM chola_trn_tspdcentry"
                    lsSql &= " inner join chola_trn_tpacket on packet_gid=spdcentry_packet_gid"
                    lsSql &= " where packet_receiveddate>='" & Format(DateAdd(DateInterval.Day, i, dtpFrom.Value), "yyyy-MM-dd") & "'"
                    lsSql &= " and packet_receiveddate<'" & Format(DateAdd(DateInterval.Day, i + 1, dtpFrom.Value), "yyyy-MM-dd") & "'"

                    ds = gfDataSet(lsSql, "cnt", gOdbcConn)

                    If ds.Tables("cnt").Rows.Count > 0 Then
                        .set_TextMatrix(row, 6, Val(ds.Tables("cnt").Rows(0).Item("cnt").ToString))
                    End If

                    ds.Tables("cnt").Rows.Clear()
                    ds.Tables("cnt").Columns.Clear()

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