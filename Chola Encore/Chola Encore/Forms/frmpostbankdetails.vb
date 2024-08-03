Public Class frmpostbankdetails

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim lssql As String
        Dim objdt As DataTable
        Dim drpdc As Odbc.OdbcDataReader

        Dim lsMicrCode As String
        Dim lsBankCode As String
        Dim lsBankName As String
        Dim lsBankBranch As String
        Dim lsAccountNo As String
        Dim lsisMicr As String

        'PDC
        lssql = ""
        lssql &= " select entry_gid,chq_pdc_gid,chq_no,chq_date,chq_amount "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where 1=1 "
        lssql &= " and chq_micrcode is null "
        lssql &= " and chq_pdc_gid > 0 "

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lbltotal.Text = "Processing PDC " & i + 1 & "/" & objdt.Rows.Count
            Application.DoEvents()

            lssql = ""
            lssql &= " select pdc_micrcode,pdc_bankname,pdc_bankbranch "
            lssql &= " from chola_trn_tpdcfile "
            lssql &= " where pdc_gid=" & Val(objdt.Rows(i).Item("chq_pdc_gid").ToString)
            drpdc = gfExecuteQry(lssql, gOdbcConn)
            If drpdc.HasRows Then
                drpdc.Read()
                lsMicrCode = QuoteFilter(drpdc.Item("pdc_micrcode").ToString)
                lsBankName = QuoteFilter(drpdc.Item("pdc_bankname").ToString)
                lsBankBranch = QuoteFilter(drpdc.Item("pdc_bankbranch").ToString)
                lsBankCode = "XXX"

                If lsMicrCode <> "" Then
                    lsisMicr = "Y"
                Else
                    lsisMicr = "N"
                End If

                lssql = ""
                lssql &= " update chola_trn_tpdcentry set "
                lssql &= " chq_micrcode='" & lsMicrCode & "',"
                lssql &= " chq_bankcode='" & lsBankCode & "',"
                lssql &= " chq_bankname='" & lsBankName & "',"
                lssql &= " chq_ismicr='" & lsisMicr & "',"
                lssql &= " chq_bankbranch='" & lsBankBranch & "'"
                lssql &= " where entry_gid=" & Val(objdt.Rows(i).Item("entry_gid").ToString)
                gfInsertQry(lssql, gOdbcConn)
            End If
        Next

        'SPDC
        lssql = ""
        lssql &= " select chqentry_gid,chqentry_pdc_gid,chqentry_chqno,chqentry_micrcode,shortagreement_no "
        lssql &= " from chola_trn_tspdcchqentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chqentry_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where 1=1 "
        lssql &= " and chqentry_bankcode is null "
        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lbltotal.Text = "Processing SPDC " & i + 1 & "/" & objdt.Rows.Count
            Application.DoEvents()

            lssql = ""
            lssql &= " select pdc_micrcode,pdc_bankname,pdc_bankbranch "
            lssql &= " from chola_trn_tpdcfile "
            lssql &= " where 1=1 "
            If Val(objdt.Rows(i).Item("chqentry_pdc_gid").ToString) > 0 Then
                lssql &= " and pdc_gid=" & Val(objdt.Rows(i).Item("chqentry_pdc_gid").ToString)
            Else
                lssql &= " and pdc_shortpdc_parentcontractno='" & objdt.Rows(i).Item("shortagreement_no").ToString & "'"
            End If

            drpdc = gfExecuteQry(lssql, gOdbcConn)
            If drpdc.HasRows Then
                drpdc.Read()
                lsMicrCode = QuoteFilter(drpdc.Item("pdc_micrcode").ToString)
                lsBankName = QuoteFilter(drpdc.Item("pdc_bankname").ToString)
                lsBankBranch = QuoteFilter(drpdc.Item("pdc_bankbranch").ToString)
                lsBankCode = "XXX"

                If lsMicrCode <> "" Then
                    lsisMicr = "Y"
                Else
                    lsisMicr = "N"
                End If

                lssql = ""
                lssql &= " update chola_trn_tspdcchqentry set "
                If objdt.Rows(i).Item("chqentry_micrcode").ToString = "" Then
                    lssql &= " chqentry_micrcode='" & lsMicrCode & "',"
                    lssql &= " chqentry_ismicr='" & lsisMicr & "',"
                End If
                lssql &= " chqentry_bankcode='" & lsBankCode & "',"
                lssql &= " chqentry_bankname='" & lsBankName & "',"
                lssql &= " chqentry_branchname='" & lsBankBranch & "'"
                lssql &= " where chqentry_gid=" & Val(objdt.Rows(i).Item("chqentry_gid").ToString)
                gfInsertQry(lssql, gOdbcConn)
            End If
        Next

        'PDC Account Update
        lssql = ""
        lssql &= " select chq_agreement_gid,shortagreement_no "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " where 1=1 "
        lssql &= " and chq_accno is null "
        lssql &= " group by agreement_gid "

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lbltotal.Text = "Processing PDC Account Update " & i + 1 & "/" & objdt.Rows.Count
            Application.DoEvents()

            lssql = ""
            lssql &= " select finone_cust_bank_account,finone_shortagreementno "
            lssql &= " from chola_trn_tfinone "
            lssql &= " where finone_shortagreementno='" & objdt.Rows(i).Item("shortagreement_no").ToString & "'"
            drpdc = gfExecuteQry(lssql, gOdbcConn)
            If drpdc.HasRows Then
                drpdc.Read()
                lsAccountNo = QuoteFilter(drpdc.Item("finone_cust_bank_account").ToString)

                lssql = ""
                lssql &= " update chola_trn_tpdcentry set "
                lssql &= " chq_accno='" & lsAccountNo & "'"
                lssql &= " where chq_agreement_gid=" & Val(objdt.Rows(i).Item("chq_agreement_gid").ToString)
                gfInsertQry(lssql, gOdbcConn)
            End If
        Next

        'SPDC Account Update
        lssql = ""
        lssql &= " select chqentry_packet_gid,shortagreement_no "
        lssql &= " from chola_trn_tspdcchqentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chqentry_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where 1=1 "
        lssql &= " and chqentry_accno is null "
        lssql &= " group by chqentry_packet_gid"

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lbltotal.Text = "Processing SPDC Account Update " & i + 1 & "/" & objdt.Rows.Count
            Application.DoEvents()

            lssql = ""
            lssql &= " select finone_cust_bank_account,finone_shortagreementno "
            lssql &= " from chola_trn_tfinone "
            lssql &= " where finone_shortagreementno='" & objdt.Rows(i).Item("shortagreement_no").ToString & "'"
            drpdc = gfExecuteQry(lssql, gOdbcConn)
            If drpdc.HasRows Then
                drpdc.Read()
                lsAccountNo = QuoteFilter(drpdc.Item("finone_cust_bank_account").ToString)

                lssql = ""
                lssql &= " update chola_trn_tspdcchqentry set "
                lssql &= " chqentry_accno='" & lsAccountNo & "'"
                lssql &= " where chqentry_packet_gid=" & Val(objdt.Rows(i).Item("chqentry_packet_gid").ToString)
                gfInsertQry(lssql, gOdbcConn)
            End If
        Next
        MsgBox("Process Completed..!", MsgBoxStyle.Information, gProjectName)
    End Sub
End Class