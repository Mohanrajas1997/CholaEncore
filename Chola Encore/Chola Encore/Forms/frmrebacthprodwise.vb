Public Class frmrebacthprodwise
    Dim lssql As String
    Private Sub frmrebacthprodwise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lssql = ""
        lssql &= " select batch_displayno,batch_gid "
        lssql &= " from chola_trn_tbatch "
        lssql &= " where batch_istally='Y' "
        lssql &= " and batch_deleteflag='N' "
        gpBindCombo(lssql, "batch_displayno", "batch_gid", cbobatchno, gOdbcConn)
        cbobatchno.SelectedIndex = -1

    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        cbobatchno.SelectedIndex = -1
    End Sub

    Private Sub btnrebatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrebatch.Click
        Dim dtpdc As DataTable
        Dim lnBatchNo As Long
        Dim lsBatchNos As String = ""
        Dim lnBatchID As Long
        Dim lsCycleDate As String

        If MsgBox("Are You Sure Want to ReBatch " & cbobatchno.Text, MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        'Update Product
        lssql = ""
        lssql &= " update chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " set chq_prodtype=if(agreement_prodtype=0," & GCOTHERS & ",agreement_prodtype)"
        lssql &= " where chq_batch_gid=" & cbobatchno.SelectedValue
        gfInsertQry(lssql, gOdbcConn)

        'Get Product Count
        lssql = ""
        lssql &= " select distinct chq_prodtype "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_batch_gid=" & cbobatchno.SelectedValue
        dtpdc = GetDataTable(lssql)

        For i As Integer = 0 To dtpdc.Rows.Count - 1
            Dim dtpdcchq As DataTable
            Dim lislno As Integer = 0


            lssql = ""
            lssql &= " select date_format(batch_cycledate,'%Y-%m-%d')  "
            lssql &= " from chola_trn_tbatch "
            lssql &= " where batch_gid = " & cbobatchno.SelectedValue
            lsCycleDate = gfExecuteScalar(lssql, gOdbcConn)

            lnBatchNo = GenerateBatchNo()

            lsBatchNos &= lnBatchNo & ","

            lssql = ""
            lssql &= " insert into chola_trn_tbatch(batch_no, batch_displayno, batch_prodtype, batch_cycledate, batch_inserdate, batch_insertby) "
            lssql &= " values("
            lssql &= " " & lnBatchNo & ", "
            lssql &= " '" & Format(lnBatchNo, "0000") & "', "
            lssql &= "" & dtpdc.Rows(i).Item("chq_prodtype").ToString & ","
            lssql &= " '" & lsCycleDate & "',"
            lssql &= " sysdate(), "
            lssql &= " '" & gUserName & "') "

            gfInsertQry(lssql, gOdbcConn)

            lssql = ""
            lssql &= " select batch_gid from chola_trn_tbatch"
            lssql &= " where batch_no=" & lnBatchNo

            lnBatchID = gfExecuteScalar(lssql, gOdbcConn)


            lssql = ""
            lssql &= " select entry_gid,chq_amount "
            lssql &= " from chola_trn_tpdcentry "
            lssql &= " where chq_batch_gid=" & cbobatchno.SelectedValue
            lssql &= " and chq_prodtype=" & dtpdc.Rows(i).Item("chq_prodtype").ToString
            lssql &= " order by chq_batchslno "
            dtpdcchq = GetDataTable(lssql)

            For j As Integer = 0 To dtpdcchq.Rows.Count - 1
                lislno += 1

                lssql = ""
                lssql &= " update chola_trn_tpdcentry set "
                lssql &= " chq_batch_gid=" & lnBatchID & ","
                lssql &= " chq_batchslno=" & lislno
                lssql &= " where entry_gid=" & dtpdcchq.Rows(j).Item("entry_gid").ToString
                gfInsertQry(lssql, gOdbcConn)

                lssql = " update chola_trn_tbatch set batch_totalchq = batch_totalchq - 1,"
                lssql &= " batch_totalchqamt=batch_totalchqamt - " & Val(dtpdcchq.Rows(j).Item("chq_amount").ToString) & ", "
                lssql &= " batch_entrychq=batch_entrychq - 1 ,"
                lssql &= " batch_entrychqamt=batch_entrychqamt - " & Val(dtpdcchq.Rows(j).Item("chq_amount").ToString)
                lssql &= " where batch_gid=" & cbobatchno.SelectedValue
                gfInsertQry(lssql, gOdbcConn)

                lssql = " update chola_trn_tbatch set batch_totalchq = if(batch_entrychq is null,0,batch_entrychq)+1 ,"
                lssql &= " batch_totalchqamt=if(batch_entrychqamt is null,0,batch_entrychqamt) + " & Val(dtpdcchq.Rows(j).Item("chq_amount").ToString) & ", "
                lssql &= " batch_entrychq=if(batch_entrychq is null,0,batch_entrychq)+1 ,"
                lssql &= " batch_entrychqamt=if(batch_entrychqamt is null,0,batch_entrychqamt) + " & Val(dtpdcchq.Rows(j).Item("chq_amount").ToString)
                lssql &= " where batch_gid=" & lnBatchID
                gfInsertQry(lssql, gOdbcConn)
            Next

        Next
        MsgBox("Batch No" & cbobatchno.Text & "  Splited into " & lsBatchNos & "..!", MsgBoxStyle.Information, gProjectName)
        btnclear.PerformClick()
    End Sub
    Private Function GenerateBatchNo()
        lsSql = ""
        lsSql &= " select max(batch_no) from chola_trn_tbatch"

        Return Val(gfExecuteScalar(lsSql, gOdbcConn)) + 1
    End Function
End Class