Public Class frmclosuresummary
    Dim lssql As String
    Private Sub frmsummary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmsummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmsummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtAgreementNo.Focus()
        txtAgreementNo.Text = ""
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


        lsSql = " "
        lsSql &= " SELECT tf.closure_gid,agreement_gid,almaraentry_cupboardno as 'Cupboard No',almaraentry_shelfno as 'Shelf No',almaraentry_boxno as 'Box No', "
        lsSql &= " packet_gnsarefno as 'GNSA Ref#',shortagreement_no as 'Short Agreement Number',agreement_no as 'Agreement Number',"
        lsSql &= " date_format(closure_date,'%d-%m-%Y') as 'Closure Date'"
        lsSql &= " FROM chola_trn_tclosure as tf"
        lsSql &= " inner join chola_mst_tagreement on closure_agreementgid=agreement_gid "
        lsSql &= " left join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
        lsSql &= " left join chola_trn_almaraentry on almaraentry_gid=packet_box_gid "
        lsSql &= " where 1=1 and closure_postflag='N' "

        If dtpfrom.Checked = True Then
            lsSql &= " and closure_date >= '" & Format(dtpfrom.Value, "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and closure_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpto.Value), "yyyy-MM-dd") & "'"
        End If

        If txtAgreementNo.Text <> "" Then
            lsSql &= " and agreement_no='" & txtAgreementNo.Text & "'"
        End If

        If txtShortAgreementNo.Text <> "" Then
            lsSql &= " and shortagreement_no='" & txtShortAgreementNo.Text & "'"
        End If

        lsSql &= " order by packet_gnsarefno"

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            Dim dgvccolumn As New DataGridViewCheckBoxColumn
            dgvccolumn.HeaderText = "Submit"
            .Columns.Add(dgvccolumn)
            .Columns(0).Visible = False
            .Columns(1).Visible = False
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

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtAgreementNo.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim licnt As Integer = 0

        If MsgBox("Are You Sure Want to Update", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To dgvsummary.RowCount - 1
            If dgvsummary.Rows(i).Cells(8).Value Then
                licnt += 1
                lssql = " select agreement_statusflag from chola_mst_tagreement "
                lssql &= " where agreement_gid=" & dgvsummary.Rows(i).Cells("agreement_gid").Value.ToString

                If Val(gfExecuteScalar(lssql, gOdbcConn)) = GCCLOSEDSTATUS Then
                    MsgBox("Agreement Already in Closed Status", MsgBoxStyle.Information)
                    GoTo GONEXT
                End If

                If dgvsummary.Rows(i).Cells("GNSA Ref#").Value.ToString = "" Then
                    'MsgBox("Packet Not Received-" & dgvsummary.Rows(i).Cells("Agreement Number").Value.ToString, MsgBoxStyle.Information)
                    GoTo GONEXT
                End If


                'Cheque Level
                lssql = " update chola_trn_tpdcentry set "
                lssql &= " chq_status=chq_status | " & GCCLOSURE
                lssql &= " where chq_agreement_gid=" & dgvsummary.Rows(i).Cells("agreement_gid").Value.ToString & " and chq_status < " & GCPRESENTATIONDE

                gfInsertQry(lssql, gOdbcConn)

                'Security Cheque Level
                lssql = " update chola_trn_tspdcchqentry "
                lssql &= " inner join chola_trn_tpacket on packet_gid = chqentry_packet_gid "
                lssql &= " set chqentry_status=chqentry_status | " & GCCLOSURE
                lssql &= " where packet_agreement_gid=" & dgvsummary.Rows(i).Cells("agreement_gid").Value.ToString & " "

                gfInsertQry(lssql, gOdbcConn)

                'Agreement Level
                lssql = " update chola_mst_tagreement set "
                lssql &= " agreement_statusflag=agreement_statusflag|" & GCCLOSED & ","
                lssql &= " agreement_closeddate='" & Format(CDate(dgvsummary.Rows(i).Cells("Closure Date").Value.ToString), "yyyy-MM-dd") & "'"
                lssql &= " where agreement_gid=" & dgvsummary.Rows(i).Cells("agreement_gid").Value.ToString & ""
                gfInsertQry(lssql, gOdbcConn)

                lssql = " update chola_trn_tclosure set "
                lssql &= " closure_postflag='Y' "
                lssql &= " where closure_gid=" & dgvsummary.Rows(i).Cells("closure_gid").Value.ToString
                gfInsertQry(lssql, gOdbcConn)

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

    Private Sub chkselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkselectall.Click
        For i As Integer = 0 To dgvsummary.Rows.Count - 1
            dgvsummary.Rows(i).Cells(8).Value = chkselectall.Checked
        Next
    End Sub
End Class