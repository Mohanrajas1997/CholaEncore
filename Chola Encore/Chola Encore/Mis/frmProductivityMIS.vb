Imports System.Data.Odbc

Public Class frmProductivityMIS

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

#Region "Form Level Declaration"
    Dim fsSql As String = ""
#End Region

    Private Sub GridProperty()
        Dim lnRow As Integer = 0
        Dim lnCol As Integer = 0
        Dim i As Integer = 0
        Dim j As Integer = 0

        With msfGrid
            .Rows = 3
            .FixedRows = 2

            .Cols = 3
            .FixedCols = 2

            .Rows = 2
            .Cols = 2

            lnRow = 0
            lnCol = 0

            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_TextMatrix(lnRow, lnCol, "SNo")
            .set_TextMatrix(lnRow + 1, lnCol, "SNo")

            lnCol = lnCol + 1
            .set_ColWidth(lnCol, 1440 * 1.5)
            .set_ColAlignment(lnCol, 1)
            .set_TextMatrix(lnRow, lnCol, "User")
            .set_TextMatrix(lnRow + 1, lnCol, "User")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.75)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Packet Entry")
            .set_TextMatrix(lnRow + 1, lnCol, "Count")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_ColWidth(lnCol, 1440 * 0.75)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Packet Entry")
            .set_TextMatrix(lnRow + 1, lnCol, "PDC")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_ColWidth(lnCol, 1440 * 0.75)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Packet Entry")
            .set_TextMatrix(lnRow + 1, lnCol, "SPDC")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_ColWidth(lnCol, 1440 * 0.75)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Packet Entry")
            .set_TextMatrix(lnRow + 1, lnCol, "Mandate")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_ColWidth(lnCol, 1440 * 1)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Presentation")
            .set_TextMatrix(lnRow + 1, lnCol, "Presentation")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_ColWidth(lnCol, 1440 * 0.75)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Packet Pullout")
            .set_TextMatrix(lnRow + 1, lnCol, "Packet Pullout")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_ColWidth(lnCol, 1440 * 0.75)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Chq Pullout")
            .set_TextMatrix(lnRow + 1, lnCol, "Chq Pullout")

            lnCol = lnCol + 1
            .Cols = .Cols + 1
            .set_ColWidth(lnCol, 1440 * 0.5)
            .set_ColAlignment(lnCol, 4)
            .set_ColWidth(lnCol, 1440 * 0.75)
            .set_ColAlignment(lnCol, 7)
            .set_TextMatrix(lnRow, lnCol, "Bounce")
            .set_TextMatrix(lnRow + 1, lnCol, "Bounce")

            .Row = 0

            For i = 0 To .FixedRows - 1
                .Row = i

                For j = 0 To .Cols - 1
                    .Col = j
                    .CellAlignment = 4
                Next j
            Next i

            .RowHeightMin = 310

            .MergeCells = MSFlexGridLib.MergeCellsSettings.flexMergeFree
            .set_MergeRow(0, True)

            .set_MergeCol(0, True)
            .set_MergeCol(1, True)
            .set_MergeCol(.Cols - 4, True)
            .set_MergeCol(.Cols - 3, True)
            .set_MergeCol(.Cols - 2, True)
            .set_MergeCol(.Cols - 1, True)

            .WordWrap = True
        End With
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        cboUserName.Text = ""
        dtpFromDate.Value = Now
        dtpToDate.Value = Now
        msfGrid.Rows = 1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadData()
        'Declarations
        Dim ds As New DataSet

        Dim lsUserName As String = ""
        Dim lsFrom As String = ""
        Dim lsTo As String = ""
        Dim lsSql As String
        Dim i As Integer, j As Integer
        Dim lnRow As Integer
        Dim lnCol As Integer
        Dim n As Integer

        msfGrid.Rows = msfGrid.FixedRows

        'Condition
        With msfGrid
            If cboUserName.Text = "" Then
                For i = 0 To cboUserName.Items.Count - 1
                    .Rows = .Rows + 1
                    .Row = .Rows - 1

                    .set_TextMatrix(.Row, 0, i + 1)
                    .set_TextMatrix(.Row, 1, cboUserName.Items(i))
                Next i
            Else
                .Rows = .Rows + 1
                .Row = .Rows - 1

                .set_TextMatrix(.Row, 0, "1")
                .set_TextMatrix(.Row, 1, cboUserName.Text)
            End If

            For i = .FixedRows To .Rows - 1
                lsUserName = .get_TextMatrix(i, 1)
                lsUserName = lsUserName.Split("-")(0).Trim

                lsFrom = Format(dtpFromDate.Value, "yyyy-MM-dd")
                lsTo = Format(DateAdd(DateInterval.Day, 1, dtpToDate.Value), "yyyy-MM-dd")

                lnRow = i
                lnCol = 2

                ' packet pdc
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(distinct p.packet_gid) as pkt_count,"
                lsSql &= " count(*) as chq_count "
                lsSql &= " from chola_trn_tpacket as p "
                lsSql &= " inner join chola_trn_tpdcentry as c on c.chq_packet_gid = p.packet_gid "
                lsSql &= " where p.packet_entryon >= '" & lsFrom & "' "
                lsSql &= " and p.packet_entryon <= '" & lsTo & "' "
                lsSql &= " and p.packet_entryby = '" & lsUserName & "' "

                Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

                If ds.Tables("pkt").Rows.Count > 0 Then
                    .set_TextMatrix(lnRow, lnCol, ds.Tables("pkt").Rows(0).Item("pkt_count").ToString)
                    .set_TextMatrix(lnRow, lnCol + 1, ds.Tables("pkt").Rows(0).Item("chq_count").ToString)
                End If

                ds.Tables("pkt").Rows.Clear()
                Application.DoEvents()

                ' packet spdc
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(distinct p.packet_gid) as pkt_count,"
                lsSql &= " count(*) as chq_count "
                lsSql &= " from chola_trn_tpacket as p "
                lsSql &= " inner join chola_trn_tspdcchqentry as c on c.chqentry_packet_gid = p.packet_gid "
                lsSql &= " where p.packet_entryon >= '" & lsFrom & "' "
                lsSql &= " and p.packet_entryon <= '" & lsTo & "' "
                lsSql &= " and p.packet_entryby = '" & lsUserName & "' "

                Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

                If ds.Tables("pkt").Rows.Count > 0 Then
                    .set_TextMatrix(lnRow, lnCol + 2, ds.Tables("pkt").Rows(0).Item("chq_count").ToString)
                End If

                ds.Tables("pkt").Rows.Clear()
                Application.DoEvents()

                ' packet mandate
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(distinct p.packet_gid) as pkt_count,"
                lsSql &= " count(*) as chq_count "
                lsSql &= " from chola_trn_tpacket as p "
                lsSql &= " inner join chola_trn_tspdcentry as c on c.spdcentry_packet_gid = p.packet_gid "
                lsSql &= " where p.packet_entryon >= '" & lsFrom & "' "
                lsSql &= " and p.packet_entryon <= '" & lsTo & "' "
                lsSql &= " and p.packet_entryby = '" & lsUserName & "' "

                Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

                If ds.Tables("pkt").Rows.Count > 0 Then
                    .set_TextMatrix(lnRow, lnCol + 3, ds.Tables("pkt").Rows(0).Item("chq_count").ToString)
                End If

                ds.Tables("pkt").Rows.Clear()
                Application.DoEvents()

                ' presentation
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(*) as chq_count "
                lsSql &= " from chola_trn_chqentry as c "
                lsSql &= " where c.chqentry_entryon >= '" & lsFrom & "' "
                lsSql &= " and c.chqentry_entryon <= '" & lsTo & "' "
                lsSql &= " and c.chqentry_entryby = '" & lsUserName & "' "

                Call gpDataSet(lsSql, "batch", gOdbcConn, ds)

                lnCol += 4

                If ds.Tables("batch").Rows.Count > 0 Then
                    .set_TextMatrix(lnRow, lnCol, ds.Tables("batch").Rows(0).Item("chq_count").ToString)
                End If

                ds.Tables("batch").Rows.Clear()
                Application.DoEvents()

                ' packet pullout
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(*) as pkt_count "
                lsSql &= " from chola_trn_tpacketpullout as b "
                lsSql &= " where b.packetpullout_postedon >= '" & lsFrom & "' "
                lsSql &= " and b.packetpullout_postedon <= '" & lsTo & "' "
                lsSql &= " and b.packetpullout_postedby = '" & lsUserName & "' "

                Call gpDataSet(lsSql, "pkt", gOdbcConn, ds)

                lnCol += 1

                If ds.Tables("pkt").Rows.Count > 0 Then
                    .set_TextMatrix(lnRow, lnCol, ds.Tables("pkt").Rows(0).Item("pkt_count").ToString)
                End If

                ds.Tables("pkt").Rows.Clear()
                Application.DoEvents()

                ' Chq pullout
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(*) as chq_count "
                lsSql &= " from chola_trn_tpullout as b "
                lsSql &= " where b.pullout_insertdate >= '" & lsFrom & "' "
                lsSql &= " and b.pullout_insertdate <= '" & lsTo & "' "
                lsSql &= " and b.pullout_insertby = '" & lsUserName & "' "

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, gfExecuteScalar(lsSql, gOdbcConn).ToString)
                Application.DoEvents()

                ' Bounce
                lsSql = ""
                lsSql &= " select "
                lsSql &= " count(*) as chq_count "
                lsSql &= " from chola_trn_tbounceentry as b "
                lsSql &= " where b.bounceentry_insertdate >= '" & lsFrom & "' "
                lsSql &= " and b.bounceentry_insertdate <= '" & lsTo & "' "
                lsSql &= " and b.bounceentry_insertby = '" & lsUserName & "' "

                lnCol += 1
                .set_TextMatrix(lnRow, lnCol, gfExecuteScalar(lsSql, gOdbcConn).ToString)
                Application.DoEvents()
            Next i

            .Rows = .Rows + 1
            .Row = .Rows - 1

            .set_TextMatrix(.Row, 1, "Total")

            For i = .FixedCols To .Cols - 1
                n = 0

                For j = .FixedRows To .Rows - 2
                    n += Val(.get_TextMatrix(j, i))
                Next j

                .set_TextMatrix(.Row, i, n)
            Next i
        End With
    End Sub

    Private Sub frmUserEntryMIS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ds As New DataSet
        Dim i As Integer

        Try
            dtpToDate.Value = Now
            dtpFromDate.Value = Now

            Call GridProperty()

            fsSql = ""
            fsSql &= " select concat(e.employee_code,'-',e.employee_name) as employee_name,e.employee_code "
            fsSql &= " from egateway.info_mst_temployee as e "
            fsSql &= " inner join egateway.info_mst_tsoftvsemp as s on softvsemp_employee_gid = employee_gid "
            fsSql &= " and s.softvsemp_software_code = '" & softcode & "' "
            fsSql &= " and s.softvsemp_status = 'A' "
            fsSql &= " and e.employee_status in ('A','L') "
            fsSql &= " order by e.employee_code"

            Call gpDataSet(fsSql, "user", gOdbcConn, ds)

            With ds.Tables("user")
                cboUserName.Items.Clear()

                For i = 0 To .Rows.Count - 1
                    cboUserName.Items.Add(.Rows(i).Item("employee_name").ToString)
                Next i

                .Rows.Clear()
            End With

            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim lsTxt As String
        Try
            lsTxt = Format(dtpFromDate.Value, "dd-MM-yy") & " to " & Format(dtpToDate.Value, "dd-MM-yy")

            PrintFGridXMLMerge(msfGrid, gsReportPath & "\Report.xls", lsTxt)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmUserEntryMIS_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        grpMain.Top = 6
        grpMain.Left = 6

        With msfGrid
            .Top = grpMain.Top + grpMain.Height + 6
            .Left = 6
            .Width = Me.Width - 18
            .Height = Math.Abs(Me.Height - (grpMain.Top + grpMain.Height) - 36)
        End With
    End Sub

    Private Sub cboUserName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUserName.SelectedIndexChanged

    End Sub

    Private Sub cboUserName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserName.TextChanged
        'Call gpAutoFillCombo(cboUserName)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
End Class