Imports System.IO

Public Class frmBatchCreation
    Dim lsSql As String

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim n As Integer
        Dim dtPdc As DataTable
        Dim lnBatchNo As Integer
        Dim llBatchID As Long

        Dim lnChqCnt As Integer
        Dim ldChqAmount As Double

        If dtpDate.Checked = False Then
            MsgBox("Please select Cycle Date", MsgBoxStyle.Information, gProjectName)
            dtpDate.Focus()
            Exit Sub
        End If

        If cbotype.Text = "" Then
            MsgBox("Please select Type", MsgBoxStyle.Information, gProjectName)
            cbotype.Focus()
            Exit Sub
        End If


        btnGenerate.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        lsSql = ""
        lsSql &= " select entry_gid, chq_agreement_gid,chq_no,chq_amount"
        lsSql &= " from chola_trn_tpdcentry p"
        lsSql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lsSql &= " where chq_date= '" & Format(dtpDate.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and (chq_status | " & GCMATCHFINONE & "|" & GCMATCHFINONEPRECOVERFILE & ") ^ (" & GCMATCHFINONE & "|" & GCMATCHFINONEPRECOVERFILE & ") <= " & GCINPROCESS
        lsSql &= " and chq_type=" & GCEXTERNALNORMAL
        lsSql &= " and chq_prodtype =" & cbotype.SelectedValue
        lsSql &= " and packet_box_gid > 0 "
        lsSql &= " group by chq_agreement_gid,chq_no"
        lsSql &= " order by packet_gnsarefno "

        dtPdc = GetDataTable(lsSql)
        For i As Integer = 0 To dtPdc.Rows.Count - 1 Step 200
            lnBatchNo = GenerateBatchNo()

            lsSql = ""
            lsSql &= " insert into chola_trn_tbatch(batch_no, batch_displayno, batch_prodtype, batch_cycledate, batch_inserdate, batch_insertby) "
            lsSql &= " values("
            lsSql &= " " & lnBatchNo & ", "
            lsSql &= " '" & Format(lnBatchNo, "0000") & "', "
            lsSql &= "" & cbotype.SelectedValue & ","
            lsSql &= " '" & Format(dtpDate.Value, "yyyy-MM-dd") & "',"
            lsSql &= " sysdate(), "
            lsSql &= " '" & gUserName & "') "

            gfInsertQry(lsSql, gOdbcConn)

            lsSql = ""
            lsSql &= " select max(batch_gid) from chola_trn_tbatch"

            llBatchID = gfExecuteScalar(lsSql, gOdbcConn)

            n = i + 200

            If n > dtPdc.Rows.Count - 1 Then
                n = dtPdc.Rows.Count - 1
            Else
                n -= 1
            End If

            lnChqCnt = 0
            ldChqAmount = 0

            For j As Integer = i To n

                lsSql = ""
                lsSql &= " update chola_trn_tpdcentry set"
                lsSql &= " chq_batch_gid = " & llBatchID & ","
                lsSql &= " chq_status = chq_status | " & GCPRESENTATIONPULLOUT

                lsSql &= " where entry_gid =" & dtPdc.Rows(j).Item("entry_gid").ToString & ""
                gfInsertQry(lsSql, gOdbcConn)

                'lsSql = ""
                'lsSql &= " update chola_trn_tpdcfile set batch_gid =" & llBatchID & ", "
                'lsSql &= " pdc_status_flag = pdc_status_flag | " & GCPRESENTATIONPULLOUT

                'lsSql &= " where entry_gid=" & dtPdc.Rows(j).Item("entry_gid").ToString

                'gfInsertQry(lsSql, gOdbcConn)

                lnChqCnt += 1
                ldChqAmount += Val(dtPdc.Rows(j).Item("chq_amount").ToString)
            Next j

            lsSql = ""
            lsSql &= " update chola_trn_tbatch set batch_totalchq = " & lnChqCnt & ", "
            lsSql &= " batch_totalchqamt =" & ldChqAmount
            lsSql &= " where batch_gid = " & llBatchID

            gfInsertQry(lsSql, gOdbcConn)
            lnChqCnt = 0
            ldChqAmount = 0

        Next i

        Dim drBatch As Odbc.OdbcDataReader

        lsSql = ""
        lsSql &= " select batch_gid, batch_displayno from chola_trn_tbatch"
        lsSql &= " where batch_cycledate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and batch_prodtype=" & cbotype.SelectedValue

        drBatch = gfExecuteQry(lsSql, gOdbcConn)

        If drBatch.HasRows Then
            While drBatch.Read()

                lsSql = ""
                lsSql &= " select almaraentry_cupboardno 'Cupboard No' , almaraentry_shelfno 'Shelf No', almaraentry_boxno 'Box No', "
                lsSql &= " packet_gnsarefno 'GNSA Ref', shortagreement_no 'Short Agreement No',agreement_no 'Agreement No', "
                lsSql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount' "
                lsSql &= " from chola_trn_tpdcentry "
                lsSql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
                lsSql &= " inner join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
                lsSql &= " left join chola_trn_almaraentry  on almaraentry_gid=packet_box_gid "
                lsSql &= " where chq_batch_gid = " & drBatch.Item("batch_gid").ToString
                lsSql &= " group by chq_agreement_gid,chq_no"
                lsSql &= " order by chq_batchslno,packet_gnsarefno "

                Dim lsfilePath As String
                If Not Directory.Exists(gsReportPath) Then Directory.CreateDirectory(gsReportPath)

                lsfilePath = gsReportPath & Format(Date.Now, "dd-MMM-yyyy")

                If Not Directory.Exists(lsfilePath) Then Directory.CreateDirectory(lsfilePath)

                lsfilePath &= "\" & cbotype.Text & "-" & drBatch.Item("batch_displayno").ToString & ".xls"

                SqlToXml(lsSql, lsfilePath, drBatch.Item("batch_gid").ToString, False)
            End While

            MsgBox("Batch File generated in the path " & gsReportPath & Format(Date.Now, "dd-MMM-yyyy"), , gProjectName)
        Else
            MsgBox("No Data found", MsgBoxStyle.Critical, gProjectName)
        End If

        btnGenerate.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Function GenerateBatchNo()
        lsSql = ""
        lsSql &= " select max(batch_no) from chola_trn_tbatch"

        Return Val(gfExecuteScalar(lsSql, gOdbcConn)) + 1
    End Function

    Private Sub frmBatchCreation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lsSql = " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N' order by type_name "
        gpBindCombo(lsSql, "type_name", "type_flag", cbotype, gOdbcConn)
        cbotype.SelectedIndex = -1
    End Sub
End Class