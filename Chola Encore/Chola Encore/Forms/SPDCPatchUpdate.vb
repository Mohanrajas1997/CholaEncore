Public Class SPDCPatchUpdate

    Private Sub btnupdatepatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdatepatch.Click
        Dim lssql As String
        Dim objdt As DataTable
        Dim lnAgreementGid As Long
        Dim lnPacketGid As Long
        Dim lnInwardGid As Long

        Me.Cursor = Cursors.WaitCursor
        btnupdatepatch.Enabled = False

        lssql = " select file_mst_gid,spdc_importdate,spdc_dumpspdccnt,spdc_ecsmandatecnt,spdc_shortagreementno,spdc_gnsarefno,spdc_agreementno,spdc_entryspdccnt,spdc_entryecsmandatecnt,spdc_entryremarks,spdc_repaymentmode,"
        lssql &= " if(transaction_transactionby is null,'" & gUserName & "',transaction_transactionby) as 'Entry by',if(transaction_transactionon is null,sysdate(),transaction_transactionon) as 'Entry on'"
        lssql &= " from chola_trn_tspdc "
        lssql &= " left join chloa_trn_ttransaction on transaction_gnsarefno=spdc_gnsarefno and transaction_statusflag=2 "
        lssql &= " where spdc_statusflag >= 3 "

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lbltotalrecords.Text = "Processing " & objdt.Rows.Count & "/" & i + 1
            Application.DoEvents()

            'Agreement
            lssql = " select agreement_gid"
            lssql &= " from chola_mst_tagreement"
            lssql &= " where agreement_no='" & objdt.Rows(i).Item("spdc_agreementno").ToString & "'"

            lnAgreementGid = Val(gfExecuteScalar(lssql, gOdbcConn))

            If Val(lnAgreementGid) = 0 Then

                lssql = " insert into chola_mst_tagreement"
                lssql &= "(agreement_no,shortagreement_no) values ("
                lssql &= "'" & objdt.Rows(i).Item("spdc_agreementno").ToString & "',"
                lssql &= "'" & Format(Val(Microsoft.VisualBasic.Right(objdt.Rows(i).Item("spdc_agreementno").ToString, 7)), "000000") & "')"
                gfInsertQry(lssql, gOdbcConn)

                lssql = " select max(agreement_gid) "
                lssql &= " from chola_mst_tagreement"
                'lssql &= " where agreement_no='" & objdt.Rows(i).Item("spdc_agreementno").ToString & "'"
                lnAgreementGid = gfExecuteScalar(lssql, gOdbcConn)

            End If

            'Packet
            lssql = ""
            lssql &= " select packet_gid "
            lssql &= " from chola_trn_tpacket "
            lssql &= " where packet_gnsarefno='" & objdt.Rows(i).Item("spdc_gnsarefno").ToString & "'"

            lnPacketGid = Val(gfExecuteScalar(lssql, gOdbcConn))

            If lnPacketGid = 0 Then
                'Insert into Inward Table
                lssql = ""
                lssql &= " insert into chola_trn_tinward ("
                lssql &= " inward_file_gid,inward_agreementno,inward_shortagreementno,inward_spdc,inward_paymode,inward_mandate,"
                lssql &= " inward_userauthdate,inward_receiveddate) "
                lssql &= " values ( "
                lssql &= "" & objdt.Rows(i).Item("file_mst_gid").ToString & ","
                lssql &= "'" & objdt.Rows(i).Item("spdc_agreementno").ToString & "',"
                lssql &= "'" & objdt.Rows(i).Item("spdc_shortagreementno").ToString & "',"
                lssql &= "" & Val(objdt.Rows(i).Item("spdc_dumpspdccnt").ToString) & ","
                lssql &= "'" & objdt.Rows(i).Item("spdc_repaymentmode").ToString & "',"
                lssql &= "" & Val(objdt.Rows(i).Item("spdc_ecsmandatecnt").ToString) & ","
                lssql &= "'" & Format(CDate(objdt.Rows(i).Item("Entry on").ToString), "yyyy-MM-dd") & "',"
                lssql &= "'" & Format(CDate(objdt.Rows(i).Item("spdc_importdate").ToString), "yyyy-MM-dd") & "')"

                gfInsertQry(lssql, gOdbcConn)

                'Fetching Inward Gid
                lssql = ""
                lssql &= " select max(inward_gid) from chola_trn_tinward "
                'lssql &= " where inward_file_gid=" & objdt.Rows(i).Item("file_mst_gid").ToString & ""
                'lssql &= " and inward_agreementno='" & objdt.Rows(i).Item("spdc_agreementno").ToString & "'"
                lnInwardGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                'Insert into Packet
                lssql = ""
                lssql &= " insert into chola_trn_tpacket("
                lssql &= " packet_agreement_gid,packet_inward_gid, packet_gnsarefno, packet_mode, packet_entryby,packet_entryon,packet_receiveddate)"
                lssql &= " values ("
                lssql &= "" & lnAgreementGid & ","
                lssql &= "" & lnInwardGid & ","
                lssql &= "'" & objdt.Rows(i).Item("spdc_gnsarefno").ToString & "',"
                lssql &= "'SPDC',"
                lssql &= "'" & objdt.Rows(i).Item("Entry by").ToString & "',"
                lssql &= "'" & Format(CDate(objdt.Rows(i).Item("Entry on").ToString), "yyyy-MM-dd") & "',"
                lssql &= "'" & Format(CDate(objdt.Rows(i).Item("Entry on").ToString), "yyyy-MM-dd") & "')"

                gfInsertQry(lssql, gOdbcConn)

                'Fetching Packet Gid
                lssql = " select max(packet_gid) "
                lssql &= " from chola_trn_tpacket "
                'lssql &= " where packet_agreement_gid=" & lnAgreementGid
                'lssql &= " and packet_gnsarefno='" & objdt.Rows(i).Item("spdc_gnsarefno").ToString & "'"
                lnPacketGid = gfExecuteScalar(lssql, gOdbcConn)

                'Update into Inward Table
                lssql = ""
                lssql &= " update chola_trn_tinward "
                lssql &= " set inward_packet_gid=" & lnPacketGid
                lssql &= " where inward_gid=" & lnInwardGid
                gfInsertQry(lssql, gOdbcConn)

                'SPDC Entry
                lssql = ""
                lssql &= " insert into chola_trn_tspdcentry ("
                lssql &= " spdcentry_packet_gid,spdcentry_spdccount,spdcentry_ecsmandatecount,spdcentry_remarks )"
                lssql &= " values ( "
                lssql &= "" & lnPacketGid & ","
                lssql &= "" & Val(objdt.Rows(i).Item("spdc_entryspdccnt").ToString) & ","
                lssql &= "" & Val(objdt.Rows(i).Item("spdc_entryecsmandatecnt").ToString) & ","
                lssql &= "'" & objdt.Rows(i).Item("spdc_entryremarks").ToString & "')"
                gfInsertQry(lssql, gOdbcConn)

                LogPacket("", GCAUTHENTRY, lnPacketGid, , objdt.Rows(i).Item("Entry by").ToString, objdt.Rows(i).Item("Entry on").ToString)
                LogPacket("", GCPACKETCHEQUEENTRY, lnPacketGid, , objdt.Rows(i).Item("Entry by").ToString, objdt.Rows(i).Item("Entry on").ToString)

                LogPacketHistory("", GCINWARDENTRY, lnPacketGid, objdt.Rows(i).Item("Entry by").ToString, objdt.Rows(i).Item("Entry on").ToString)
                LogPacketHistory("", GCAUTHENTRY, lnPacketGid, objdt.Rows(i).Item("Entry by").ToString, objdt.Rows(i).Item("Entry on").ToString)
                LogPacketHistory("", GCPACKETCHEQUEENTRY, lnPacketGid, objdt.Rows(i).Item("Entry by").ToString, objdt.Rows(i).Item("Entry on").ToString)
            End If
        Next

        MsgBox("Process Completed", MsgBoxStyle.Information)
        btnupdatepatch.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub
End Class