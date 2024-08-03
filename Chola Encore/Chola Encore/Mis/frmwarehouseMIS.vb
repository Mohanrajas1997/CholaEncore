Public Class frmwarehouseMIS

    Private Sub btngenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngenerate.Click

        Dim lssql As String
        Dim objdt As DataTable

        If dtpreceiveddate.Checked = False Then
            MsgBox("Please Select Received Date..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        lssql = " set @slno:=0 "
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select @slno:= @slno + 1 as 'SL No',inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
        lssql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
        lssql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
        lssql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
        lssql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
        lssql &= " if(packet_status & " & GCAUTHENTRY & " > 0 ,'Auther Done',if(packet_status & " & GCREJECTENTRY & " > 0 ,packet_remarks,if(packet_gid is null ,'Not Received/Combined','Auth Pending'))) as 'Finnone Status',"
        lssql &= " date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date',"
        lssql &= " inward_compagr as 'Comp Agr' "
        lssql &= " from chola_trn_tinward"
        lssql &= " left join chola_trn_tpacket on packet_gid=inward_packet_gid "
        lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"

        objdt = GetDataTable(lssql)

        If objdt.Rows.Count = 0 Then MsgBox("No Record Found..!", MsgBoxStyle.Information, gProjectName) : Me.Cursor = Cursors.Default : Exit Sub

        Dim xlxport As New XMLExport(gsReportPath & "WarehouseMIS.xls")
        With xlxport
            .NewSheet("Handsoff")
            .BeginRow()
            For i As Integer = 0 To objdt.Columns.Count - 1
                .AddCellboldgry(objdt.Columns(i).ColumnName)
            Next
            .EndRow()

            For i As Integer = 0 To objdt.Rows.Count - 1
                .BeginRow()
                For j As Integer = 0 To objdt.Columns.Count - 1
                    .AddCell(objdt.Rows(i)(j).ToString)
                Next
                .EndRow()
            Next
            .EndSheet()

            'Handsoff tally
            lssql = ""
            lssql &= " select agreement_no as 'Agreement No',packet_gnsarefno as 'GNSAREF#',pdc_mode as 'Mode',"
            lssql &= " inward_spdc as 'SPDC Count', "
            lssql &= " inward_mandate as 'Mandate Count', "
            lssql &= " inward_pdc as 'PDC Count', "
            lssql &= " sum(if(pdc_type='External-Normal' or pdc_type='External-Security',1,0)) as 'Finone Count',"
            lssql &= " spdcentry_spdccount as 'GNSA SPDC Count',"
            lssql &= " spdcentry_ecsmandatecount as 'GNSA Mandate Count',"
            lssql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid=packet_gid) as 'GNSA PDC Count', "
            lssql &= " if(inward_spdc=spdcentry_spdccount,'Matched','Not Matched') as 'SPDC Status',"
            lssql &= " if(inward_mandate=spdcentry_ecsmandatecount,'Matched','Not Matched') as 'Mandate Status',"
            lssql &= " spdcentry_remarks as 'GNSA Remarks'"
            lssql &= " from  chola_trn_tinward "
            lssql &= " left join chola_trn_tpdcfile b on pdc_parentcontractno=inward_agreementno "
            lssql &= " inner join chola_mst_tagreement on agreement_no=inward_agreementno "
            lssql &= " inner join chola_trn_tpacket on inward_packet_gid=packet_gid  "
            lssql &= " left join chola_trn_tspdcentry on spdcentry_packet_gid = packet_gid "
            lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
            lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"
            lssql &= " group by agreement_no "
            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Handsoff tally")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
                Next
                .EndRow()

                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'ECS
            lssql = ""
            lssql &= " select agreement_no as 'Agreement No',packet_gnsarefno as 'GNSAREF#',pdc_mode as 'Mode',"
            lssql &= " inward_spdc as 'SPDC Count', "
            lssql &= " inward_mandate as 'Mandate Count', "
            lssql &= " inward_pdc as 'PDC Count', "
            lssql &= " sum(if(pdc_type='External-Normal' or pdc_type='External-Security',1,0)) as 'Finone Count',"
            lssql &= " spdcentry_spdccount as 'GNSA SPDC Count',"
            lssql &= " spdcentry_ecsmandatecount as 'GNSA Mandate Count',"
            lssql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid=packet_gid) as 'GNSA PDC Count', "
            lssql &= " if(inward_spdc=spdcentry_spdccount,'Matched','Not Matched') as 'SPDC Status',"
            lssql &= " if(inward_mandate=spdcentry_ecsmandatecount,'Matched','Not Matched') as 'Mandate Status',"
            lssql &= " spdcentry_remarks as 'GNSA Remarks'"
            lssql &= " from  chola_trn_tinward "
            lssql &= " left join chola_trn_tpdcfile b on pdc_parentcontractno=inward_agreementno and chq_rec_slno=1 and pdc_mode<>'PDC' "
            lssql &= " inner join chola_mst_tagreement on agreement_no=inward_agreementno"
            lssql &= " inner join chola_trn_tpacket on inward_packet_gid=packet_gid and packet_mode='SPDC' "
            lssql &= " left join chola_trn_tspdcentry on spdcentry_packet_gid = packet_gid "
            lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
            lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"
            lssql &= " group by agreement_no "
            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("ECS")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
                Next
                .EndRow()

                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'PDC
            lssql = ""
            lssql &= " select packet_gnsarefno as 'GNSAREF#',pdc_contractno as 'Contract No',"
            lssql &= " pdc_shortpdc_parentcontractno as 'Entry Parent No',pdc_draweename as 'Drawee Name',"
            lssql &= " pdc_chqno as 'Dump Chqno',chq_no as 'Entry Chqno',if(pdc_chqno=chq_no,'Matched','Not Matched') as 'Chqno Diff',"
            lssql &= " date_format(pdc_chqdate,'%d-%m-%Y') as 'Dump Chqdate',date_format(chq_date,'%d-%m-%Y') as 'Entry Chqdate',"
            lssql &= " if(pdc_chqdate=chq_date,'Matched','Not Matched') as 'Chqdate Diff',pdc_chqamount as 'Dump Chqamt',"
            lssql &= " chq_amount as 'Entry Chqamt',if(pdc_chqamount=chq_amount,'Matched','Not Matched') as 'Chqamt Diff' "
            lssql &= " from  chola_trn_tinward "
            lssql &= " inner join chola_trn_tpdcfile b on pdc_parentcontractno=inward_agreementno  and pdc_mode='PDC'"
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')=inward_userauthdate "
            lssql &= " inner join chola_mst_tagreement on agreement_no=inward_agreementno "
            lssql &= " inner join chola_trn_tpacket on inward_packet_gid=packet_gid "
            lssql &= " left join chola_trn_tpdcentry on chq_packet_gid=packet_gid and pdc_chqno=chq_no and chq_pdc_gid>0 "
            lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
            lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"


            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("PDC")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
                Next
                .EndRow()

                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Add PDC
            lssql = ""
            lssql &= " select packet_gnsarefno as 'GNSAREF#',agreement_no as 'Contract No',"
            lssql &= " shortagreement_no as 'Entry Parent No',pdc_draweename as 'Drawee Name',"
            lssql &= " '' as 'Dump Chqno',chq_no as 'Entry Chqno','Not Matched' as 'Chqno Diff',"
            lssql &= " '' as 'Dump Chqdate',date_format(chq_date,'%d-%m-%Y') as 'Entry Chqdate',"
            lssql &= " 'Not Matched' as 'Chqdate Diff','' as 'Dump Chqamt',chq_amount as 'Entry Chqamt',"
            lssql &= " 'Not Matched' as 'Chqamt Diff'"
            lssql &= " from  chola_trn_tinward "
            lssql &= " inner join chola_mst_tagreement on agreement_no=inward_agreementno "
            lssql &= " inner join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
            lssql &= " inner join chola_trn_tpdcentry on chq_packet_gid=packet_gid "
            lssql &= " left join chola_trn_tpdcfile b on pdc_parentcontractno=agreement_no and pdc_chqno=chq_no "
            lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
            lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"
            lssql &= " and pdc_gid is null "

            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Add PDC")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
                Next
                .EndRow()

                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            ' New
            ' New ECS
            lssql = ""
            lssql &= " select agreement_no as 'Agreement No',packet_gnsarefno as 'GNSAREF#',pdc_mode as 'Mode',"
            lssql &= " inward_spdc as 'SPDC Count', "
            lssql &= " inward_mandate as 'Mandate Count', "
            lssql &= " inward_pdc as 'PDC Count', "
            lssql &= " sum(if(pdc_type='External-Normal' or pdc_type='External-Security',1,0)) as 'Finone Count',"
            lssql &= " spdcentry_spdccount as 'GNSA SPDC Count',"
            lssql &= " spdcentry_ecsmandatecount as 'GNSA Mandate Count',"
            lssql &= " (select count(*) from chola_trn_tpdcentry where chq_packet_gid=packet_gid) as 'GNSA PDC Count', "
            lssql &= " if(inward_spdc=spdcentry_spdccount,'Matched','Not Matched') as 'SPDC Status',"
            lssql &= " if(inward_mandate=spdcentry_ecsmandatecount,'Matched','Not Matched') as 'Mandate Status',"
            lssql &= " spdcentry_remarks as 'GNSA Remarks',if(inward_parent_gid=inward_gid,'','Combined') as 'Combined' "
            lssql &= " from  chola_trn_tinward "
            lssql &= " left join chola_trn_tpdcfile b on pdc_parentcontractno=inward_agreementno and chq_rec_slno=1 and pdc_mode<>'PDC' "
            lssql &= " inner join chola_mst_tagreement on agreement_no=inward_agreementno"
            lssql &= " inner join chola_trn_tpacket on inward_parent_gid=packet_inward_gid and packet_mode='SPDC' "
            lssql &= " left join chola_trn_tspdcentry on spdcentry_packet_gid = packet_gid "
            lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
            lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"
            lssql &= " group by agreement_no "
            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("ECS NEW")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
                Next
                .EndRow()

                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'PDC
            lssql = ""
            lssql &= " select packet_gnsarefno as 'GNSAREF#',pdc_contractno as 'Contract No',"
            lssql &= " pdc_shortpdc_parentcontractno as 'Entry Parent No',pdc_draweename as 'Drawee Name',"
            lssql &= " pdc_chqno as 'Dump Chqno',chq_no as 'Entry Chqno',if(pdc_chqno=chq_no,'Matched','Not Matched') as 'Chqno Diff',"
            lssql &= " date_format(pdc_chqdate,'%d-%m-%Y') as 'Dump Chqdate',date_format(chq_date,'%d-%m-%Y') as 'Entry Chqdate',"
            lssql &= " if(pdc_chqdate=chq_date,'Matched','Not Matched') as 'Chqdate Diff',pdc_chqamount as 'Dump Chqamt',"
            lssql &= " chq_amount as 'Entry Chqamt',if(pdc_chqamount=chq_amount,'Matched','Not Matched') as 'Chqamt Diff',"
            lssql &= " if(inward_parent_gid=inward_gid,'','Combined') as 'Combined' from  chola_trn_tinward "
            lssql &= " inner join chola_trn_tpdcfile b on pdc_parentcontractno=inward_agreementno  and pdc_mode='PDC'"
            lssql &= " and date_format(pdc_importdate,'%Y-%m-%d')=inward_userauthdate "
            lssql &= " inner join chola_mst_tagreement on agreement_no=inward_agreementno "
            lssql &= " inner join chola_trn_tpacket on inward_parent_gid=packet_inward_gid "
            lssql &= " left join chola_trn_tpdcentry on chq_packet_gid=packet_gid and pdc_chqno=chq_no and chq_pdc_gid>0 "
            lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
            lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"


            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("PDC NEW")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
                Next
                .EndRow()

                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            'Add PDC
            lssql = ""
            lssql &= " select packet_gnsarefno as 'GNSAREF#',agreement_no as 'Contract No',"
            lssql &= " shortagreement_no as 'Entry Parent No',pdc_draweename as 'Drawee Name',"
            lssql &= " '' as 'Dump Chqno',chq_no as 'Entry Chqno','Not Matched' as 'Chqno Diff',"
            lssql &= " '' as 'Dump Chqdate',date_format(chq_date,'%d-%m-%Y') as 'Entry Chqdate',"
            lssql &= " 'Not Matched' as 'Chqdate Diff','' as 'Dump Chqamt',chq_amount as 'Entry Chqamt',"
            lssql &= " 'Not Matched' as 'Chqamt Diff',if(inward_parent_gid=inward_gid,'','Combined') as 'Combined'"
            lssql &= " from  chola_trn_tinward "
            lssql &= " inner join chola_mst_tagreement on agreement_no=inward_agreementno "
            lssql &= " inner join chola_trn_tpacket on inward_parent_gid=packet_inward_gid "
            lssql &= " inner join chola_trn_tpdcentry on chq_packet_gid=packet_gid "
            lssql &= " left join chola_trn_tpdcfile b on pdc_parentcontractno=agreement_no and pdc_chqno=chq_no "
            lssql &= " where inward_receiveddate >= '" & Format(dtpreceiveddate.Value, "yyyy-MM-dd") & "'"
            lssql &= " and inward_receiveddate < '" & Format(DateAdd(DateInterval.Day, 1, dtpreceiveddate.Value), "yyyy-MM-dd") & "'"
            lssql &= " and pdc_gid is null "

            objdt = GetDataTable(lssql)

            If objdt.Rows.Count > 0 Then
                .NewSheet("Add PDC NEW")
                .BeginRow()
                For i As Integer = 0 To objdt.Columns.Count - 1
                    .AddCellboldgry(objdt.Columns(i).ColumnName)
                Next
                .EndRow()

                For i As Integer = 0 To objdt.Rows.Count - 1
                    .BeginRow()
                    For j As Integer = 0 To objdt.Columns.Count - 1
                        .AddCell(objdt.Rows(i)(j).ToString)
                    Next
                    .EndRow()
                Next
                .EndSheet()
            End If

            .Close()
        End With
        Me.Cursor = Cursors.Default
        MsgBox("Report Generated..!", MsgBoxStyle.Information, gProjectName)
        System.Diagnostics.Process.Start(gsReportPath & "WarehouseMIS.xls")
    End Sub
End Class