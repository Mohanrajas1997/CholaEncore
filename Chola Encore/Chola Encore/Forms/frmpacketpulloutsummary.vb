Public Class frmpacketpulloutsummary
    Dim lssql As String

    Private Sub frmsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtShortAgreementNo.Focus()
        txtShortAgreementNo.Text = ""
    End Sub

    Private Sub frmsummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim dgvccolumn As New DataGridViewCheckBoxColumn
        Dim dgvBtn As New DataGridViewButtonColumn
        Dim i As Integer
        Dim n As Integer

        lsSql = " "
        lsSql &= " SELECT tf.packetpullout_gid,packetpullout_packet_gid,almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No', "
        lsSql &= " packet_gnsarefno as 'GNSA Ref#',shortagreement_no as 'Short Agreement Number',agreement_no as 'Agreement Number',"
        lsSql &= " packetpullout_reason as 'Reason',import_on as 'Import Date',if(packetpullout_postflag ='Y','Posted','Pending') as 'Status' "
        lsSql &= " FROM chola_trn_tpacketpullout as tf"
        lsSql &= " inner join chola_mst_tfile on file_gid=packetpullout_file_gid "
        lsSql &= " inner join chola_trn_tpacket on packet_gid=packetpullout_packet_gid "
        lsSql &= " inner join chola_mst_tagreement on packet_agreement_gid=agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " where 1=1 "

        Select Case True
            Case rdoPosted.Checked
                lsSql &= " and packetpullout_postflag = 'Y' "
            Case rdoNotPosted.Checked
                lsSql &= " and packetpullout_postflag = 'N' "
            Case Else
                lsSql &= " and 1 = 2 "
        End Select

        If dtpfrom.Checked = True Then
            lsSql &= " and import_on >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "' "
        End If

        If dtpto.Checked = True Then
            lsSql &= " and import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "' "
        End If

        If txtShortAgreementNo.Text <> "" Then
            lsSql &= " and shortagreement_no='" & QuoteFilter(txtShortAgreementNo.Text) & "' "
        End If

        If txtAgreementNo.Text <> "" Then
            lsSql &= " and agreement_no='" & QuoteFilter(txtAgreementNo.Text) & "' "
        End If

        If txtPktNo.Text <> "" Then
            lsSql &= " and packet_gnsarefno = '" & QuoteFilter(txtPktNo.Text) & "' "
        End If

        lsSql &= " order by packet_gnsarefno"

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)

            n = .ColumnCount

            dgvccolumn.HeaderText = "Submit"
            .Columns.Add(dgvccolumn)

            dgvBtn.HeaderText = "Action"
            dgvBtn.Text = "Action"
            .Columns.Add(dgvBtn)

            .Columns(0).Visible = False
            .Columns(1).Visible = False

            For i = 0 To .RowCount - 1
                Select Case .Rows(i).Cells("Status").Value.ToString
                    Case "Posted"
                        .Rows(i).Cells(n + 1).Value = "Undo"
                    Case "Pending"
                        .Rows(i).Cells(n + 1).Value = "Post"
                End Select
            Next i

            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()

        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtShortAgreementNo.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnPktId As Long = 0
        Dim licnt As Integer = 0

        If MsgBox("Are You Sure Want to Update", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To dgvsummary.RowCount - 1
            If dgvsummary.Rows(i).Cells(dgvsummary.ColumnCount - 2).Value Then
                licnt += 1

                lnPktId = dgvsummary.Rows(i).Cells("packetpullout_packet_gid").Value

                lssql = " select packet_gid from chola_trn_tpacket "
                lssql &= " where packet_gid = " & lnPktId
                lssql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "

                lnPktId = Val(gfExecuteScalar(lssql, gOdbcConn))

                If lnPktId > 0 Then
                    lssql = " update chola_trn_tpacket set "
                    lssql &= " packet_status=packet_status | " & GCIPACKETPULLOUT & " "
                    lssql &= " where packet_gid = " & lnPktId
                    lssql &= " and packet_status & " & GCIPACKETPULLOUT & " = 0 "

                    gfInsertQry(lssql, gOdbcConn)

                    'Cheque Level
                    lssql = " update chola_trn_tpdcentry set "
                    lssql &= " chq_status=chq_status | " & GCPACKETPULLOUT & ","
                    lssql &= " chq_desc='" & dgvsummary.Rows(i).Cells("Reason").Value.ToString & "'"
                    lssql &= " where chq_packet_gid=" & lnPktId & " and chq_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " update chola_trn_tspdcchqentry set "
                    lssql &= " chqentry_status = chqentry_status | " & GCPACKETPULLOUT & ","
                    lssql &= " chqentry_remarks ='" & dgvsummary.Rows(i).Cells("Reason").Value.ToString & "'"
                    lssql &= " where chqentry_packet_gid=" & lnPktId & " and chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " update chola_trn_tecsemientry set "
                    lssql &= " ecsemientry_status = ecsemientry_status | " & GCPACKETPULLOUT & " "
                    lssql &= " where ecsemientry_packet_gid = " & lnPktId & " and ecsemientry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCPRESENTATIONDE) & " = 0 "

                    gfInsertQry(lssql, gOdbcConn)

                    ' Packet pullout table
                    lssql = " update chola_trn_tpacketpullout set "
                    lssql &= " packetpullout_postflag='Y',packetpullout_postedon = sysdate(),packetpullout_postedby = '" & gUserName & "' "
                    lssql &= " where packetpullout_gid=" & dgvsummary.Rows(i).Cells("packetpullout_gid").Value.ToString
                    lssql &= " and packetpullout_postflag = 'N' "
                    gfInsertQry(lssql, gOdbcConn)
                End If
            End If
GONEXT:
        Next

        If licnt = 0 Then
            MsgBox("No records Selected", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        Else
            MsgBox("Successfully updated", MsgBoxStyle.OkOnly, gProjectName)
        End If
        LoadData()
        chkselectall.Checked = False
    End Sub

    Private Sub UndoPacket(ByVal PktId As Long)
        Dim licnt As Integer = 0

        If MsgBox("Are you sure to undo ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        lssql = " select packet_gid from chola_trn_tpacket "
        lssql &= " where packet_gid = " & PktId
        lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "

        PktId = Val(gfExecuteScalar(lssql, gOdbcConn))

        If PktId > 0 Then
            lssql = " update chola_trn_tpacket set "
            lssql &= " packet_status = (packet_status | " & GCIPACKETPULLOUT & ") ^ " & GCIPACKETPULLOUT
            lssql &= " where packet_gid = " & PktId
            lssql &= " and packet_status & " & GCIPACKETPULLOUT & " > 0 "

            gfInsertQry(lssql, gOdbcConn)

            'Cheque Level
            lssql = " update chola_trn_tpdcentry set "
            lssql &= " chq_status = (chq_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT
            lssql &= " where chq_packet_gid=" & PktId & " and chq_status & " & GCPACKETPULLOUT & " > 0 "

            gfInsertQry(lssql, gOdbcConn)

            lssql = " update chola_trn_tspdcchqentry set "
            lssql &= " chqentry_status = (chqentry_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT
            lssql &= " where chqentry_packet_gid=" & PktId & " and chqentry_status & " & GCPACKETPULLOUT & " > 0 "

            gfInsertQry(lssql, gOdbcConn)

            lssql = " update chola_trn_tecsemientry set "
            lssql &= " ecsemientry_status = (ecsemientry_status | " & GCPACKETPULLOUT & ") ^ " & GCPACKETPULLOUT
            lssql &= " where ecsemientry_packet_gid = " & PktId & " and ecsemientry_status & " & GCPACKETPULLOUT & " > 0 "

            gfInsertQry(lssql, gOdbcConn)

            ' Packet pullout table
            lssql = " update chola_trn_tpacketpullout set "
            lssql &= " packetpullout_undoon = sysdate(),packetpullout_undoby = '" & gUserName & "',packetpullout_postflag = 'U' "
            lssql &= " where packetpullout_packet_gid=" & PktId
            lssql &= " and packetpullout_postflag = 'Y' "

            gfInsertQry(lssql, gOdbcConn)

            MsgBox("Packet undo made successfully !", MsgBoxStyle.Information, gProjectName)
        Else
            MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
        End If
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Dim lnPktId As Long

        With dgvsummary
            If e.RowIndex >= 0 Then
                Select Case e.ColumnIndex
                    Case .Columns.Count - 1
                        Select Case .Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                            Case "Undo"
                                lnPktId = .Rows(e.RowIndex).Cells("packetpullout_packet_gid").Value
                                .Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value = False

                                Call UndoPacket(lnPktId)
                            Case "Post"
                                .Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value = True
                        End Select
                End Select
            End If
        End With
    End Sub

    Private Sub pnlMain_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlMain.Paint

    End Sub

    Private Sub chkselectall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged
        For i As Integer = 0 To dgvsummary.Rows.Count - 1
            dgvsummary.Rows(i).Cells(dgvsummary.ColumnCount - 2).Value = chkselectall.CheckState
        Next
    End Sub
End Class