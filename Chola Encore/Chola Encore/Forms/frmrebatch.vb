Imports System.IO
Public Class frmrebatch
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

        lssql = " select batch_gid,batch_prodtype,batch_displayno as 'Batch No',date_format(batch_cycledate,'%d-%m-%Y') as 'Cycle Date',type_name as 'Product Type',"
        lssql &= " batch_totalchq as 'Total Cheque',batch_totalchqamt as 'Total Amount',batch_entrychq as 'Total Enterd Cheque', "
        lssql &= "batch_entrychqamt as 'Entry Total Amount',batch_istally as 'Batch Tally'"
        lssql &= " from chola_trn_tbatch "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where 1=1 "
        lssql &= " and batch_istally='N' "
        lssql &= " and batch_despatch_gid = 0 "
        lssql &= " and batch_entrychq > 0 "


        If dtpcycledate.Checked Then
            lssql &= " and batch_cycledate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        End If

        If cboproduct.Text.Trim <> "" Then
            lssql &= " and batch_prodtype=" & cboproduct.SelectedValue
        End If

        lssql &= " order by batch_gid "

        dgvsummary.Columns.Clear()
        gpPopGridView(dgvsummary, lssql, gOdbcConn)
        dgvsummary.Columns("batch_gid").Visible = False
        dgvsummary.Columns("batch_prodtype").Visible = False


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
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgvsummary.Rows(e.RowIndex)
        If row.Cells("Batch Tally").Value.ToString() = "N" Then
            Dim frmentry As New frmbatchentry(row.Cells("Cycle Date").Value.ToString(), row.Cells("batch_prodtype").Value.ToString(), row.Cells("batch_gid").Value.ToString())
            frmentry.ShowDialog()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbatchtally.Click
        If cboproduct.Text.Trim = "" Then
            MsgBox("Please Select Product", MsgBoxStyle.Critical, gProjectName)
            cboproduct.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To dgvsummary.Rows.Count - 1
            If dgvsummary.Rows(i).Cells("Select").Value Then
                If MsgBox("Are You Sure Want to ReBatch " & dgvsummary.Rows(i).Cells("Batch No").Value, MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then

                    'Update in Entry Table
                    lssql = ""
                    lssql &= " update chola_trn_tpdcentry "
                    lssql &= " set chq_batch_gid=0, "
                    lssql &= " chq_status=(chq_status | " & GCPRESENTATIONPULLOUT & " ) ^ " & GCPRESENTATIONPULLOUT
                    lssql &= " where chq_status & " & GCPRESENTATIONDE & " = 0 "
                    lssql &= " and chq_batch_gid = " & dgvsummary.Rows(i).Cells("batch_gid").Value.ToString
                    gfInsertQry(lssql, gOdbcConn)

                    'Update in Batch table
                    lssql = ""
                    lssql &= " update chola_trn_tbatch as a "
                    lssql &= " inner join ( "
                    lssql &= " select chq_batch_gid,count(*) as cnt,sum(chq_amount) as "
                    lssql &= " amt,b.batch_totalchq,b.batch_entrychq from chola_trn_tpdcentry as p "
                    lssql &= " inner join chola_trn_tbatch as b on b.batch_gid = p.chq_batch_gid "
                    lssql &= " where chq_batch_gid = " & dgvsummary.Rows(i).Cells("batch_gid").Value.ToString
                    lssql &= " group by chq_batch_gid) as j on j.chq_batch_gid = a.batch_gid "
                    lssql &= " set a.batch_totalchq = j.cnt,a.batch_totalchqamt = j.amt, "
                    lssql &= " a.batch_entrychq = j.cnt,a.batch_entrychqamt = j.amt "
                    gfInsertQry(lssql, gOdbcConn)

                End If
            End If
        Next

        MsgBox("Process Completed", MsgBoxStyle.Information, gProjectName)
        btnrefresh.PerformClick()
    End Sub
End Class