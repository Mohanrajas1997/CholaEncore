Public Class frmgeneratepostcondisc
    Dim lssql As String
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpcycledate.Checked = False
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date..!", MsgBoxStyle.Critical, gProjectName)
            dtpcycledate.Focus()
        End If
        Me.Cursor = Cursors.WaitCursor
        PostFinoneVSGNSA(dtpcycledate.Value)
        PostGNSAVSFinone(dtpcycledate.Value)
        MsgBox("Process Completed..!", MsgBoxStyle.Information, gProjectName)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PostFinoneVSGNSA(ByVal CycleDate As Date)
        Dim objdt As DataTable
        Dim lnAgreementId As Long
        Dim lnDisc As Integer = 0
        Dim dtentry As DataTable
        Dim i As Integer

        'Finone Vs GNSA
        lssql = ""
        lssql &= " select "
        lssql &= " finone_gid,finone_agreementno,finone_shortagreementno,finone_chqno,finone_chqdate,finone_chqamount "
        lssql &= " from chola_trn_tfinone "
        lssql &= " where 1=1 "
        lssql &= " and finone_chqdate='" & Format(CycleDate, "yyyy-MM-dd") & "'"
        lssql &= " and finone_entrygid=0"

        objdt = GetDataTable(lssql)

        For i = 0 To objdt.Rows.Count - 1
            lnDisc = 0
            'Checking Agreement No
            lssql = ""
            lssql &= " select agreement_gid "
            lssql &= " from chola_mst_tagreement "
            lssql &= " inner join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
            lssql &= " where agreement_no='" & objdt.Rows(i).Item("finone_agreementno").ToString & "'"
            lnAgreementId = Val(gfExecuteScalar(lssql, gOdbcConn))

            If lnAgreementId > 0 Then
                'Checking Cheque Date
                lssql = ""
                lssql &= " select entry_gid,chq_no,chq_date,chq_amount "
                lssql &= " from chola_trn_tpdcentry "
                lssql &= " where 1=1 "
                lssql &= " and chq_agreement_gid = " & lnAgreementId
                lssql &= " and chq_date='" & Format(CDate(objdt.Rows(i).Item("finone_chqdate")), "yyyy-MM-dd") & "'"

                dtentry = GetDataTable(lssql)

                Select Case dtentry.Rows.Count
                    Case 1
                        If objdt.Rows(i).Item("finone_chqno").ToString <> dtentry.Rows(0).Item("chq_no").ToString Then
                            lnDisc = lnDisc Or GCDISCCHQNO
                        End If

                        If Val(objdt.Rows(i).Item("finone_chqamount").ToString) <> Val(dtentry.Rows(0).Item("chq_amount").ToString) Then
                            lnDisc = lnDisc Or GCDISCCHQAMOUNT
                        End If
                    Case Else
                        'Checking Cheque No
                        lssql = ""
                        lssql &= " select entry_gid,chq_no,chq_date,chq_amount "
                        lssql &= " from chola_trn_tpdcentry "
                        lssql &= " where 1=1 "
                        lssql &= " and chq_agreement_gid = " & lnAgreementId
                        lssql &= IIf(dtentry.Rows.Count = 0, "", " and chq_date='" & Format(CDate(objdt.Rows(i).Item("finone_chqdate")), "yyyy-MM-dd") & "'")
                        lssql &= " and chq_no='" & objdt.Rows(i).Item("finone_chqno") & "'"
                        dtentry = GetDataTable(lssql)

                        Select Case dtentry.Rows.Count
                            Case 1
                                If dtentry.Rows(0).Item("chq_date").ToString <> "" Then
                                    If Format(CDate(objdt.Rows(i).Item("finone_chqdate").ToString), "yyyy-MM-dd") <> Format(CDate(dtentry.Rows(0).Item("chq_date").ToString), "yyyy-MM-dd") Then
                                        lnDisc = lnDisc Or GCDISCCHQDATE
                                    End If
                                Else
                                    lnDisc = lnDisc Or GCDISCCHQDATE
                                End If

                                If Val(objdt.Rows(i).Item("finone_chqamount").ToString) <> Val(dtentry.Rows(0).Item("chq_amount").ToString) Then
                                    lnDisc = lnDisc Or GCDISCCHQAMOUNT
                                End If
                            Case Else
                                'Checking Cheque Amount
                                lssql = ""
                                lssql &= " select entry_gid,chq_no,chq_date,chq_amount "
                                lssql &= " from chola_trn_tpdcentry "
                                lssql &= " where 1=1 "
                                lssql &= " and chq_agreement_gid = " & lnAgreementId
                                lssql &= " and chq_amount='" & objdt.Rows(i).Item("finone_chqamount") & "'"
                                lssql &= " and chq_date='" & Format(CDate(objdt.Rows(i).Item("finone_chqdate")), "yyyy-MM-dd") & "'"
                                lssql &= IIf(dtentry.Rows.Count = 0, "", " and chq_no='" & objdt.Rows(i).Item("finone_chqno") & "'")

                                dtentry = GetDataTable(lssql)

                                Select Case dtentry.Rows.Count
                                    Case 1
                                        If objdt.Rows(i).Item("finone_chqno").ToString <> dtentry.Rows(0).Item("chq_no").ToString Then
                                            lnDisc = lnDisc Or GCDISCCHQNO
                                        End If

                                        If dtentry.Rows(0).Item("chq_date").ToString <> "" Then
                                            If Format(CDate(objdt.Rows(i).Item("finone_chqdate").ToString), "yyyy-MM-dd") <> Format(CDate(dtentry.Rows(0).Item("chq_date").ToString), "yyyy-MM-dd") Then
                                                lnDisc = lnDisc Or GCDISCCHQDATE
                                            End If
                                        Else
                                            lnDisc = lnDisc Or GCDISCCHQDATE
                                        End If
                                    Case 0
                                        lnDisc = lnDisc Or GCDISCCHQDETAILS
                                    Case Else
                                        lnDisc = lnDisc Or GCDUPLICATEENTRY
                                End Select
                        End Select
                End Select

                dtentry.Rows.Clear()
            Else
                lnDisc = lnDisc Or GCDISCAGREEMENT
            End If

            lssql = ""
            lssql &= " update chola_trn_tfinone "
            lssql &= " set finone_disc=((finone_disc | finone_disc ) ^ finone_disc ) | " & lnDisc
            lssql &= " where finone_gid=" & objdt.Rows(i).Item("finone_gid").ToString
            gfInsertQry(lssql, gOdbcConn)
        Next i

        objdt.Rows.Clear()
    End Sub
    Private Sub PostGNSAVSFinone(ByVal CycleDate As Date)
        Dim objdt As DataTable
        Dim lnAgreementid As String
        Dim lnDisc As Integer = 0
        Dim dtentry As DataTable
        Dim i As Integer

        'GNSA VS Finone
        lssql = ""
        lssql &= " select "
        lssql &= " entry_gid,agreement_no,chq_no,chq_date,chq_amount "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " where 1=1 "
        lssql &= " and chq_date='" & Format(CycleDate, "yyyy-MM-dd") & "'"
        lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "

        objdt = GetDataTable(lssql)

        For i = 0 To objdt.Rows.Count - 1
            lnDisc = 0
            'Checking Agreement No
            lssql = ""
            lssql &= " select finone_agreementno "
            lssql &= " from chola_trn_tfinone "
            lssql &= " where finone_agreementno='" & objdt.Rows(i).Item("agreement_no").ToString & "'"
            lssql &= " and finone_chqdate='" & Format(CycleDate, "yyyy-MM-dd") & "'"

            lnAgreementid = gfExecuteScalar(lssql, gOdbcConn)

            If lnAgreementid <> "" Then
                'Checking Cheque Date
                lssql = ""
                lssql &= " select finone_chqno,finone_chqdate,finone_chqamount "
                lssql &= " from chola_trn_tfinone "
                lssql &= " where 1=1 "
                lssql &= " and finone_agreementno = '" & lnAgreementid & "'"
                lssql &= " and finone_chqdate='" & Format(CDate(objdt.Rows(i).Item("chq_date")), "yyyy-MM-dd") & "'"
                dtentry = GetDataTable(lssql)

                Select Case dtentry.Rows.Count
                    Case 1
                        If objdt.Rows(i).Item("chq_no").ToString <> dtentry.Rows(0).Item("finone_chqno").ToString Then
                            lnDisc = lnDisc Or GCDISCCHQNO
                        End If

                        If Val(objdt.Rows(i).Item("chq_amount").ToString) <> Val(dtentry.Rows(0).Item("finone_chqamount").ToString) Then
                            lnDisc = lnDisc Or GCDISCCHQAMOUNT
                        End If
                    Case Else
                        'Checking Cheque No
                        lssql = ""
                        lssql &= " select finone_chqno,finone_chqdate,finone_chqamount "
                        lssql &= " from chola_trn_tfinone "
                        lssql &= " where 1=1 "
                        lssql &= " and finone_agreementno = " & lnAgreementid
                        lssql &= IIf(dtentry.Rows.Count = 0, "", " and finone_chqdate='" & Format(CDate(objdt.Rows(i).Item("chq_date")), "yyyy-MM-dd") & "'")
                        lssql &= " and finone_chqno='" & objdt.Rows(i).Item("chq_no") & "'"
                        dtentry = GetDataTable(lssql)

                        Select Case dtentry.Rows.Count
                            Case 1
                                If dtentry.Rows(0).Item("finone_chqdate").ToString <> "" Then
                                    If Format(CDate(objdt.Rows(i).Item("chq_date").ToString), "yyyy-MM-dd") <> Format(CDate(dtentry.Rows(0).Item("finone_chqdate").ToString), "yyyy-MM-dd") Then
                                        lnDisc = lnDisc Or GCDISCCHQDATE
                                    End If
                                Else
                                    lnDisc = lnDisc Or GCDISCCHQDATE
                                End If

                                If Val(objdt.Rows(i).Item("chq_amount").ToString) <> Val(dtentry.Rows(0).Item("finone_chqamount").ToString) Then
                                    lnDisc = lnDisc Or GCDISCCHQAMOUNT
                                End If
                            Case Else
                                'Checking Cheque Amount
                                lssql = ""
                                lssql &= " select finone_chqno,finone_chqdate,finone_chqamount "
                                lssql &= " from chola_trn_tfinone "
                                lssql &= " where 1=1 "
                                lssql &= " and finone_agreementno = " & lnAgreementid
                                lssql &= " and finone_chqamount='" & objdt.Rows(i).Item("chq_amount") & "'"
                                lssql &= " and finone_chqdate='" & Format(CDate(objdt.Rows(i).Item("chq_date")), "yyyy-MM-dd") & "'"
                                lssql &= IIf(dtentry.Rows.Count = 0, "", " and finone_chqno='" & objdt.Rows(i).Item("chq_no") & "'")

                                dtentry = GetDataTable(lssql)

                                Select Case dtentry.Rows.Count
                                    Case 1
                                        If objdt.Rows(i).Item("chq_no").ToString <> dtentry.Rows(0).Item("finone_chqno").ToString Then
                                            lnDisc = lnDisc Or GCDISCCHQNO
                                        End If

                                        If dtentry.Rows(0).Item("finone_chqdate").ToString <> "" Then
                                            If Format(CDate(objdt.Rows(i).Item("chq_date").ToString), "yyyy-MM-dd") <> Format(CDate(dtentry.Rows(0).Item("finone_chqdate").ToString), "yyyy-MM-dd") Then
                                                lnDisc = lnDisc Or GCDISCCHQDATE
                                            End If
                                        Else
                                            lnDisc = lnDisc Or GCDISCCHQDATE
                                        End If
                                    Case 0
                                        lnDisc = lnDisc Or GCDISCCHQDETAILS
                                    Case Else
                                        lnDisc = lnDisc Or GCDUPLICATEENTRY
                                End Select
                        End Select
                End Select

                dtentry.Rows.Clear()
            Else
                lnDisc = lnDisc Or GCDISCAGREEMENT
            End If

            lssql = ""
            lssql &= " update chola_trn_tpdcentry "
            lssql &= " set chq_postdisc=((chq_postdisc |chq_postdisc)^ chq_postdisc ) | " & lnDisc
            lssql &= " where entry_gid=" & objdt.Rows(i).Item("entry_gid").ToString
            gfInsertQry(lssql, gOdbcConn)
        Next i

        objdt.Rows.Clear()
    End Sub
End Class