Public Class frmupdatepacket
    Dim lsgnsarefno As String
    Dim lssql As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal GNSAREFNo As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lsgnsarefno = GNSAREFNo
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Dim lnPktId As Long

        lssql = ""
        lssql &= " select packet_gid from chola_trn_tpacket "
        lssql &= " where packet_gnsarefno = '" & txtgnsarefno.Text & "' "

        lnPktId = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnPktId > 0 Then
            Call LoadPdc(lnPktId)
            Call LoadSpdc(lnPktId)
            Call LoadEcs(lnPktId)
        End If
    End Sub

    Private Sub LoadPdc(ByVal PktId As Long)
        lssql = " set @slno:=0 "
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select entry_gid,@slno:= @slno + 1 as 'SL No',packet_gnsarefno as 'GNSAREF#',chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date', "
        lssql &= " chq_amount  as 'Amount',chq_accno as 'A/C No' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " where packet_gid = " & PktId & " "
        lssql &= " and chq_pdc_gid = 0 "

        dgvPdc.Columns.Clear()
        gpPopGridView(dgvPdc, lssql, gOdbcConn)
        dgvPdc.Columns(0).Visible = False
        Dim dgButtonColumn2 As New DataGridViewButtonColumn
        dgButtonColumn2.HeaderText = ""
        dgButtonColumn2.UseColumnTextForButtonValue = True
        dgButtonColumn2.Text = "Update"
        dgButtonColumn2.Name = "Update"
        dgButtonColumn2.HeaderText = "Update"
        dgButtonColumn2.ToolTipText = "Update"
        dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgButtonColumn2.FlatStyle = FlatStyle.System
        dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
        dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
        dgvPdc.Columns.Add(dgButtonColumn2)
    End Sub

    Private Sub LoadSpdc(ByVal PktId As Long)
        Dim i As Integer
        Dim n As Integer
        Dim lsSql As String
        Dim lobjTxt As DataGridViewTextBoxColumn
        Dim lobjBtn As DataGridViewButtonColumn

        n = gfInsertQry("set @a := 0", gOdbcConn)

        lsSql = ""
        lsSql &= " select @a:=@a+1 as 'SNo',chqentry_gid as 'Spdc Id',chqentry_chqno as 'Chq No',chqentry_accno as 'A/C No' from chola_trn_tspdcchqentry "
        lsSql &= " where chqentry_packet_gid = " & PktId & " "
        lsSql &= " and chqentry_pdc_gid = 0 "
        lsSql &= " order by chqentry_gid "

        dgvSpdc.Columns.Clear()

        Call gpPopGridView(dgvSpdc, lsSql, gOdbcConn)

        With dgvSpdc

            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            n = .ColumnCount

            lobjTxt = New DataGridViewTextBoxColumn
            lobjTxt.HeaderText = "New Chq No"
            .Columns.Insert(3, lobjTxt)

            lobjTxt = New DataGridViewTextBoxColumn
            lobjTxt.HeaderText = "New A/C No"
            .Columns.Add(lobjTxt)

            lobjBtn = New DataGridViewButtonColumn
            lobjBtn.HeaderText = "Update"
            .Columns.Add(lobjBtn)

            lobjBtn = New DataGridViewButtonColumn
            lobjBtn.HeaderText = "Delete"
            .Columns.Add(lobjBtn)

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n + 2).Value = "Update"
                .Rows(i).Cells(n + 3).Value = "Delete"
            Next i

            .Columns(0).Width = 48
            If gobjSecurity.LoginUserGroupGID <> "2" Then .Columns(n + 2).Visible = False
        End With
    End Sub

    Private Sub LoadEcs(ByVal PktId As Long)
        Dim i As Integer
        Dim n As Integer
        Dim lsSql As String
        Dim lobjTxt As DataGridViewTextBoxColumn
        Dim lobjBtn As DataGridViewButtonColumn

        n = gfInsertQry("set @a := 0", gOdbcConn)

        lsSql = ""
        lsSql &= " select @a:=@a+1 as 'SNo',ecsemientry_gid as 'Ecs Id',ecsemientry_emidate as 'Ecs Date',ecsemientry_amount as 'Ecs Amount' from chola_trn_tecsemientry "
        lsSql &= " where ecsemientry_packet_gid = " & PktId & " "
        lsSql &= " and ecsemientry_pdc_gid = 0 "
        lsSql &= " order by ecsemientry_gid "

        dgvEcs.Columns.Clear()

        Call gpPopGridView(dgvEcs, lsSql, gOdbcConn)

        With dgvEcs
            For i = 0 To .Columns.Count - 1
                .Columns(i).ReadOnly = True
                .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            n = .ColumnCount

            lobjTxt = New DataGridViewTextBoxColumn
            lobjTxt.HeaderText = "New Date"
            .Columns.Add(lobjTxt)

            lobjTxt = New DataGridViewTextBoxColumn
            lobjTxt.HeaderText = "New Amount"
            .Columns.Add(lobjTxt)

            lobjBtn = New DataGridViewButtonColumn
            lobjBtn.HeaderText = "Update"
            .Columns.Add(lobjBtn)

            lobjBtn = New DataGridViewButtonColumn
            lobjBtn.HeaderText = "Delete"
            .Columns.Add(lobjBtn)

            For i = 0 To .RowCount - 1
                .Rows(i).Cells(n + 2).Value = "Update"
                .Rows(i).Cells(n + 3).Value = "Delete"
            Next i

            .Columns(0).Width = 48
            .Columns(n + 3).Visible = False
        End With
    End Sub

    Private Sub frmupdatepacket_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MyBase.WindowState = FormWindowState.Maximized
        If lsgnsarefno <> "" Then
            txtgnsarefno.Text = lsgnsarefno
            btnrefresh.PerformClick()
        Else
            txtgnsarefno.Focus()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtgnsarefno.Text = ""
        dgvPdc.DataSource = Nothing
        dgvPdc.Columns.Clear()
        txtgnsarefno.Focus()
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPdc.CellContentClick
        Try
            Dim lnPdcId As Long
            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Update" Then
                        If MsgBox("Are You Sure Want to Update", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        lnPdcId = dgvPdc.CurrentRow.Cells("entry_gid").Value.ToString

                        Dim frmentry As New frmupdateentry(lnPdcId)
                        frmentry.ShowDialog()

                        btnrefresh.PerformClick()
                        dgvPdc.Rows(e.RowIndex).Selected = True
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub frmupdatepacket_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim lnHeight As Long

        grpMain.Top = 0
        grpMain.Left = 6

        With dgvPdc
            .Top = grpMain.Top + grpMain.Height + 6
            .Left = grpMain.Left

            lnHeight = (Me.Height - .Top - 6 * 3 - 27) \ 3
            .Height = lnHeight
            .Width = Me.Width - 30

            dgvSpdc.Top = .Top + .Height + 6
            dgvSpdc.Left = .Left
            dgvSpdc.Height = .Height
            dgvSpdc.Width = .Width

            dgvEcs.Top = dgvSpdc.Top + dgvSpdc.Height + 6
            dgvEcs.Left = .Left
            dgvEcs.Height = .Height
            dgvEcs.Width = .Width
        End With
    End Sub

    Private Sub dgvSpdc_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSpdc.CellContentClick
        Dim lnSpdcId As Long
        Dim lsNewChqNo As String = ""
        Dim lsNewAccNo As String = ""
        Dim lsSql As String
        Dim lnResult As Long

        With dgvSpdc
            If e.RowIndex >= 0 Then
                lnSpdcId = .Rows(e.RowIndex).Cells.Item("Spdc Id").Value

                Select Case e.ColumnIndex
                    Case .Columns.Count - 2
                        If .Rows(e.RowIndex).Cells(.Columns.Count - 5).Value Is Nothing Then
                            'MsgBox("Please enter new cheque no !", MsgBoxStyle.Critical, gProjectName)
                            'Exit Sub
                        Else
                            lsNewChqNo = .Rows(e.RowIndex).Cells(.Columns.Count - 5).Value.ToString
                        End If

                        'If Val(lsNewChqNo) = 0 Then
                        '    MsgBox("Invalid new cheque no !", MsgBoxStyle.Critical, gProjectName)
                        '    Exit Sub
                        'End If

                        If Not (.Rows(e.RowIndex).Cells(.Columns.Count - 3).Value Is Nothing) Then
                            lsNewAccNo = Mid(QuoteFilter(.Rows(e.RowIndex).Cells(.Columns.Count - 3).Value.ToString), 1, 15)
                        End If

                        lsSql = ""
                        lsSql &= " update chola_trn_tspdcchqentry set "
                        lsSql &= " chqentry_gid = " & lnSpdcId & " "

                        If lsNewChqNo <> "" Then
                            lsSql &= " chqentry_chqno = '" & Mid(lsNewChqNo, 1, 12) & "',"
                        End If

                        If lsNewAccNo <> "" Then
                            lsSql &= " ,chqentry_accno = '" & lsNewAccNo & "' "
                        End If

                        lsSql &= " where chqentry_gid = " & lnSpdcId & " "
                        lsSql &= " and chqentry_pdc_gid = 0 "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)
                    Case .Columns.Count - 1
                        If MsgBox("Are you sure to delete ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                            lsSql = ""
                            lsSql &= " insert into chola_trn_tspdcchqentrydelete "
                            lsSql &= " select * from chola_trn_tspdcchqentry "
                            lsSql &= " where chqentry_gid = " & lnSpdcId & " "
                            lsSql &= " and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
                            lsSql &= " and chqentry_pdc_gid = 0 "

                            lnResult = gfInsertQry(lsSql, gOdbcConn)

                            lsSql = ""
                            lsSql &= " delete from chola_trn_tspdcchqentry "
                            lsSql &= " where chqentry_gid = " & lnSpdcId & " "
                            lsSql &= " and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "
                            lsSql &= " and chqentry_pdc_gid = 0 "

                            lnResult = gfInsertQry(lsSql, gOdbcConn)

                            MsgBox("Record deleted successfully !", MsgBoxStyle.Information, gProjectName)
                            .Rows.RemoveAt(e.RowIndex)
                        End If
                End Select
            End If
        End With
    End Sub

    Private Sub dgvEcs_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEcs.CellContentClick
        Dim lnEcsId As Long
        Dim lsNewChqDate As String = ""
        Dim lsNewChqAmt As String = ""
        Dim lnNewChqAmt As Double = 0
        Dim lsSql As String
        Dim lbFlag As Boolean
        Dim lnResult As Long

        With dgvEcs
            If e.RowIndex >= 0 Then
                lnEcsId = .Rows(e.RowIndex).Cells.Item("Ecs Id").Value

                Select Case e.ColumnIndex
                    Case .Columns.Count - 2
                        lbFlag = True

                        If .Rows(e.RowIndex).Cells(.Columns.Count - 3).Value Is Nothing And .Rows(e.RowIndex).Cells(.Columns.Count - 4).Value Is Nothing Then
                            MsgBox("Please enter new ecs amount/date !", MsgBoxStyle.Critical, gProjectName)
                            Exit Sub
                        End If

                        If Not (.Rows(e.RowIndex).Cells(.Columns.Count - 4).Value Is Nothing) Then
                            lsNewChqDate = .Rows(e.RowIndex).Cells(.Columns.Count - 4).Value.ToString
                        End If

                        If Not (.Rows(e.RowIndex).Cells(.Columns.Count - 3).Value Is Nothing) Then
                            lsNewChqAmt = .Rows(e.RowIndex).Cells(.Columns.Count - 3).Value.ToString
                        End If

                        If (lsNewChqAmt <> "" And Val(lsNewChqAmt) = 0) Or (IsDate(lsNewChqDate) = False And lsNewChqDate <> "") Then
                            MsgBox("Please enter valid new ecs amount/date !", MsgBoxStyle.Critical, gProjectName)
                            Exit Sub
                        End If

                        If lsNewChqDate <> "" Then
                            If IsDate(lsNewChqDate) = False Then
                                MsgBox("Please enter valid new esc date !", MsgBoxStyle.Critical, gProjectName)
                                Exit Sub
                            Else
                                lsNewChqDate = Format(CDate(lsNewChqDate), "yyyy-MM-dd")
                            End If
                        End If

                        lnNewChqAmt = Math.Round(Val(lsNewChqAmt), 2)

                        lsSql = ""
                        lsSql &= " update chola_trn_tecsemientry set "

                        If lnNewChqAmt <> 0 Then
                            lsSql &= " ecsemientry_amount = " & lnNewChqAmt & ","
                        End If

                        If lsNewChqDate <> "" Then
                            lsSql &= " ecsemientry_emidate = '" & lsNewChqDate & "',"
                        End If

                        lsSql &= " ecsemientry_pdc_gid = 0 "
                        lsSql &= " where ecsemientry_gid = " & lnEcsId & " "
                        lsSql &= " and ecsemientry_pdc_gid = 0 "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)
                    Case .Columns.Count - 1
                        If MsgBox("Are you sure to delete ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                            lsSql = ""
                            lsSql &= " insert into chola_trn_tecsemientrydelete "
                            lsSql &= " select * from chola_trn_tecsemientry "
                            lsSql &= " where ecsemientry_gid = " & lnEcsId & " "
                            lsSql &= " and ecsemientry_pdc_gid = 0 "

                            lnResult = gfInsertQry(lsSql, gOdbcConn)

                            lsSql = ""
                            lsSql &= " delete from chola_trn_tecsemientry "
                            lsSql &= " where ecsemientry_gid = " & lnEcsId & " "
                            lsSql &= " and ecsemientry_pdc_gid = 0 "

                            lnResult = gfInsertQry(lsSql, gOdbcConn)

                            MsgBox("Record deleted successfully !", MsgBoxStyle.Information, gProjectName)
                        End If
                End Select
            End If
        End With
    End Sub
End Class