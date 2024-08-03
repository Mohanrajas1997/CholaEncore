Imports System.IO
Public Class frmbatchreport
    Dim lssql As String
    Private Sub frmpdcreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lssql = " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N' "
        gpBindCombo(lssql, "type_name", "type_flag", cboproduct, gOdbcConn)
        cboproduct.SelectedIndex = -1

        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        dtpcycledate.Focus()

    End Sub

    Private Sub frmpdcreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Dim i As Integer

        lssql = " select batch_gid,batch_prodtype,batch_displayno as 'Batch No',date_format(batch_cycledate,'%d-%m-%Y') as 'Cycle Date',type_name as 'Product Type',"
        lssql &= " batch_totalchq as 'Total Cheque',batch_totalchqamt as 'Total Amount',batch_entrychq as 'Total Enterd Cheque', "
        lssql &= "batch_entrychqamt as 'Total Amount',batch_istally as 'Batch isTally',despatch_no as 'Despatch No',"
        lssql &= " despatch_awbno as 'AWB No',despatch_sentto as 'Sent to',date_format(despatch_senton,'%d-%m-%Y') as 'Sent on',despatch_remarks as 'Despatch Remarks',"
        lssql &= " sum(if(chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0,1,0)) as 'Pre Matched',"
        lssql &= " sum(if(chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0,1,0)) as 'Pre Not Matched',"
        lssql &= " sum(if(chq_status & " & GCMATCHFINONE & " > 0,1,0)) as 'Post Matched',"
        lssql &= " sum(if(chq_status & " & GCMATCHFINONE & " = 0,1,0)) as 'Post Not Matched' "
        lssql &= " from chola_trn_tbatch "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " inner join chola_trn_tpdcentry on chq_batch_gid = batch_gid "
        lssql &= " left join chola_trn_tdespatch on despatch_gid=batch_despatch_gid "
        lssql &= " where 1=1 "


        If dtpcycledate.Checked Then
            lssql &= " and batch_cycledate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        End If

        If cboproduct.Text.Trim <> "" Then
            lssql &= " and batch_prodtype=" & cboproduct.SelectedValue
        End If

        If cboistally.Text.Trim <> "" Then
            lssql &= " and batch_istally='" & cboistally.Text & "'"
        End If

        If cbodespatch.Text.Trim = "Y" Then
            lssql &= " and batch_despatch_gid > 0"
        ElseIf cbodespatch.Text.Trim = "N" Then
            lssql &= " and batch_despatch_gid = 0"
        End If

        'lssql &= " order by batch_gid "
        lssql &= " group by batch_gid,batch_prodtype,batch_displayno,batch_cycledate,type_name,"
        lssql &= " batch_totalchq,batch_totalchqamt,batch_entrychq,batch_entrychqamt,batch_istally,"
        lssql &= " despatch_no,despatch_awbno,despatch_sentto,despatch_senton,despatch_remarks "

        dgvsummary.Columns.Clear()
        gpPopGridView(dgvsummary, lssql, gOdbcConn)
        dgvsummary.Columns("batch_gid").Visible = False
        dgvsummary.Columns("batch_prodtype").Visible = False

        For i = 0 To dgvsummary.RowCount - 1
            Dim row As DataGridViewRow = dgvsummary.Rows(i)
            If row.Cells("Batch isTally").Value.ToString() = "N" Then
                row.DefaultCellStyle.BackColor = Color.IndianRed
            Else
                row.DefaultCellStyle.BackColor = Color.GreenYellow
            End If
        Next

        For i = 0 To dgvsummary.ColumnCount - 1
            dgvsummary.Columns(i).ReadOnly = True
        Next i

        Dim dgvccolumn As New DataGridViewCheckBoxColumn
        dgvccolumn.HeaderText = "Select"
        dgvccolumn.Name = "Select"
        dgvsummary.Columns.Add(dgvccolumn)

        lbltotrec.Text = "Total : " & dgvsummary.RowCount
    End Sub
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpcycledate.Value = Now()
        dtpcycledate.Checked = False
        cboproduct.SelectedIndex = -1
        dgvsummary.DataSource = Nothing
        dgvsummary.Columns.Clear()
        lbltotrec.Text = ""
        cboistally.SelectedIndex = -1
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        Dim lnBatchId As Long = 0
        Dim lsSql As String = ""
        Dim frm As frmQuickView

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        With dgvsummary
            lnBatchId = .Rows(e.RowIndex).Cells("batch_gid").Value

            lsSql = ""
            lsSql &= " select * from chola_trn_tpdcentry "
            lsSql &= " where chq_batch_gid = " & lnBatchId & " "

            Select Case dgvsummary.Columns(e.ColumnIndex).Name
                Case "Pre Matched"
                    lsSql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " > 0 "

                    frm = New frmQuickView(gOdbcConn, lsSql)
                    frm.ShowDialog()
                Case "Pre Not Matched"
                    lsSql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "

                    frm = New frmQuickView(gOdbcConn, lsSql)
                    frm.ShowDialog()
                Case "Post Matched"
                    lsSql &= " and chq_status & " & GCMATCHFINONE & " > 0 "

                    frm = New frmQuickView(gOdbcConn, lsSql)
                    frm.ShowDialog()
                Case "Post Not Matched"
                    lsSql &= " and chq_status & " & GCMATCHFINONE & " = 0 "

                    frm = New frmQuickView(gOdbcConn, lsSql)
                    frm.ShowDialog()
                Case Else
                    Dim row As DataGridViewRow = dgvsummary.Rows(e.RowIndex)
                    If row.Cells("Batch isTally").Value.ToString() = "N" Then
                        Dim frmentry As New frmbatchentry(row.Cells("Cycle Date").Value.ToString(), row.Cells("batch_prodtype").Value.ToString(), row.Cells("batch_gid").Value.ToString())
                        frmentry.ShowDialog()
                    End If
            End Select
        End With
    End Sub
    Private Sub dgv_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dgvsummary.CellFormatting
        If e.ColumnIndex = 0 Then
            Dim row As DataGridViewRow = dgvsummary.Rows(e.RowIndex)
            If row.Cells("Batch isTally").Value.ToString() = "N" Then
                row.DefaultCellStyle.BackColor = Color.Red
            Else
                row.DefaultCellStyle.BackColor = Color.GreenYellow
            End If
        End If
    End Sub


    Private Sub btnbatchexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatchUntally.Click
        Dim i As Integer

        If MsgBox("Are You Sure Want to Untally Batch", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        With dgvsummary
            For i = 0 To .Rows.Count - 1

                If .Rows(i).Cells("Select").Value = True Then
                    lssql = ""
                    lssql &= " update chola_trn_tbatch "
                    lssql &= " set batch_istally = 'N' "
                    lssql &= " where batch_gid = '" & .Rows(i).Cells("batch_gid").Value & "'"
                    lssql &= " and batch_totalchq = batch_entrychq "
                    lssql &= " and batch_totalchqamt = batch_entrychqamt "
                    lssql &= " and batch_istally = 'Y'"

                    Call gfInsertQry(lssql, gOdbcConn)
                End If


                Application.DoEvents()
            Next i
        End With

        MsgBox("Process Completed", MsgBoxStyle.Information, gProjectName)
    End Sub

    Private Sub chkselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkselectall.CheckedChanged
        For i As Integer = 0 To dgvsummary.Rows.Count - 1
            dgvsummary.Rows(i).Cells("Select").Value = chkselectall.Checked
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbatchtally.Click
        Dim i As Integer

        If MsgBox("Are You Sure Want to Tally Batch", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        With dgvsummary
            For i = 0 To .Rows.Count - 1
                If .Rows(i).Cells("Select").Value = True Then
                    lssql = ""
                    lssql &= " update chola_trn_tbatch "
                    lssql &= " set batch_istally = 'Y' "
                    lssql &= " where batch_gid = '" & .Rows(i).Cells("batch_gid").Value & "'"
                    lssql &= " and batch_totalchq = batch_entrychq "
                    lssql &= " and batch_totalchqamt = batch_entrychqamt "
                    lssql &= " and batch_istally = 'N'"

                    Call gfInsertQry(lssql, gOdbcConn)
                End If

                Application.DoEvents()
            Next i
        End With

        MsgBox("Process Completed", MsgBoxStyle.Information, gProjectName)
    End Sub
End Class