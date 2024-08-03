
Public Class frmhandsoffbulkuploadsummary
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
        txtproposalno.Focus()
        txtproposalno.Text = ""
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

        lsSql = ""
        lsSql &= " SELECT tf.handsoff_gid,"
        lsSql &= " handsoff_shortagreementno as 'Agreement Number',spdc_gnsarefno as 'GNSA Ref#',"
        lsSql &= " group_concat(distinct(entry_chqno)) as 'Cheque Nos',"
        lsSql &= " handsoff_repaymentmode as 'Repayment Mode',"
        lsSql &= " handsoff_customername as 'Customer Name',"
        lsSql &= " handsoff_type as 'Type',"
        lsSql &= " handsoff_remarks as 'Remarks',"
        lsSql &= " handsoff_dcno as 'DC No',"
        lsSql &= " handsoff_chqcnt as 'Cheque Count',"
        lsSql &= " handsoff_to as 'Sent To',"
        lsSql &= " handsoff_handsoffdate as 'Sent On',"
        lsSql &= " handsoff_importdate as 'Import Date'"
        lsSql &= " FROM chola_trn_thandsoff as tf"
        lsSql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lsSql &= " left join chola_trn_tspdc on spdc_shortagreementno=handsoff_shortagreementno"
        lsSql &= " left join chola_trn_thandsoffentry on entry_shortagreementno=handsoff_shortagreementno "
        lsSql &= " where 1=1 and handsoff_handsoffflag='N' and handsoff_dcno is not null and spdc_gnsarefno is not null "


        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtproposalno.Text <> "" Then
            lsSql &= " and handsoff_shortagreementno='" & txtproposalno.Text & "'"
        End If

        lsSql &= " group by handsoff_shortagreementno "

        lsSql &= " union "
        lsSql &= " SELECT tf.handsoff_gid,"
        lsSql &= " handsoff_shortagreementno as 'Agreement Number',pdc_gnsarefno as 'GNSA Ref#',"
        lsSql &= " group_concat(distinct(entry_chqno)) as 'Cheque Nos',"
        lsSql &= " handsoff_repaymentmode as 'Repayment Mode',"
        lsSql &= " handsoff_customername as 'Customer Name',"
        lsSql &= " handsoff_type as 'Type',"
        lsSql &= " handsoff_remarks as 'Remarks',"
        lsSql &= " handsoff_dcno as 'DC No',"
        lsSql &= " handsoff_chqcnt as 'Cheque Count',"
        lsSql &= " handsoff_to as 'Sent To',"
        lsSql &= " handsoff_handsoffdate as 'Sent On',"
        lsSql &= " handsoff_importdate as 'Import Date'"
        lsSql &= " FROM chola_trn_thandsoff as tf"
        lsSql &= " inner join chola_mst_tfile as mf on mf.file_gid=tf.file_mst_gid"
        lsSql &= " left join chola_trn_tpdcfile on pdc_shortpdc_parentcontractno=handsoff_shortagreementno"
        lsSql &= " left join chola_trn_thandsoffentry on entry_shortagreementno=handsoff_shortagreementno "
        lsSql &= " where 1=1 and handsoff_handsoffflag='N' and handsoff_dcno is not null  and pdc_gnsarefno is not null "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(handsoff_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtproposalno.Text <> "" Then
            lsSql &= " and handsoff_shortagreementno='" & txtproposalno.Text & "'"
        End If

        lsSql &= " group by handsoff_shortagreementno "

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            Dim dgvccolumn As New DataGridViewCheckBoxColumn
            dgvccolumn.HeaderText = "Submit"
            .Columns.Add(dgvccolumn)
            .Columns(0).Visible = False
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
        txtproposalno.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim listatus As Integer
        Dim licheques As String()

        If MsgBox("Are You Sure Want to Update", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        For i As Integer = 0 To dgvsummary.RowCount - 1
            If dgvsummary.Rows(i).Cells(13).Value Then
                lssql = " select status_flag "
                lssql &= " from chola_mst_tstatus"
                lssql &= "  inner join chola_trn_thandsoff on handsoff_type=status_desc "
                lssql &= " where handsoff_gid='" & dgvsummary.Rows(i).Cells("handsoff_gid").Value.ToString & "'"

                listatus = Val(gfExecuteScalar(lssql, gOdbcConn))

                If listatus = 0 Then
                    MsgBox("Invalid Status.,Contact Administrator", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                    Exit Sub
                End If

                If Microsoft.VisualBasic.Left(dgvsummary.Rows(i).Cells("GNSA Ref#").Value.ToString, 1) = "P" Then

                    If dgvsummary.Rows(i).Cells("Cheque Nos").Value.ToString.Trim = "" Then
                        lssql = " update chola_trn_tpdcfile set "
                        lssql &= " pdc_status_flag=pdc_status_flag | " & listatus
                        lssql &= " where pdc_gnsarefno='" & dgvsummary.Rows(i).Cells("GNSA Ref#").Value.ToString & "'"

                        gfInsertQry(lssql, gOdbcConn)
                    Else
                        licheques = Split(dgvsummary.Rows(i).Cells("Cheque Nos").Value.ToString, ",")
                        For j As Integer = 0 To UBound(licheques)
                            If licheques(j).ToString <> "" Then
                                lssql = " update chola_trn_tpdcfile set "
                                lssql &= " pdc_status_flag=pdc_status_flag | " & listatus
                                lssql &= " where pdc_gnsarefno='" & dgvsummary.Rows(i).Cells("GNSA Ref#").Value.ToString & "'"
                                lssql &= " and pdc_chqno='" & licheques(j).ToString & "'"

                                gfInsertQry(lssql, gOdbcConn)
                            End If
                        Next
                    End If

                    LogTransaction(dgvsummary.Rows(i).Cells("GNSA Ref#").Value.ToString, listatus, gUserName)
                Else
                    lssql = " update chola_trn_tspdc set "
                    lssql &= " spdc_statusflag=spdc_statusflag | " & listatus
                    lssql &= " where spdc_gnsarefno='" & dgvsummary.Rows(i).Cells("GNSA Ref#").Value.ToString & "'"
                    gfInsertQry(lssql, gOdbcConn)

                    LogTransaction(dgvsummary.Rows(i).Cells("GNSA Ref#").Value.ToString, listatus, gUserName)
                End If


                lssql = " update chola_trn_thandsoff set "
                lssql &= " handsoff_handsoffflag='Y'"
                lssql &= " where handsoff_gid = '" & dgvsummary.Rows(i).Cells("handsoff_gid").Value.ToString & "'"

                gfInsertQry(lssql, gOdbcConn)

            End If
        Next
        LoadData()
        chkselectall.Checked = False
        
    End Sub

    Private Sub chkselectall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkselectall.Click
        For i As Integer = 0 To dgvsummary.Rows.Count - 1
            dgvsummary.Rows(i).Cells("").Value = chkselectall.Checked
        Next
    End Sub
End Class