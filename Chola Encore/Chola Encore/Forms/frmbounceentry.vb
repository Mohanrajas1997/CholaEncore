Public Class frmbounceentry
    Dim lssql As String
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        cboagreement.Text = ""
        txtchequeamt.Text = ""
        txtchqno.Text = ""
        mtxtchqdate.Text = ""
        cboreason.SelectedIndex = -1
        cboagreement.DataSource = Nothing
        cboagreement.SelectedIndex = -1
        txtchqslno.Text = ""
        txtremarks.Text = ""
    End Sub
    Private Sub frmpulloutentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmpulloutentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lssql = " select reason_gid,reason_name from chola_mst_tbouncereason where reason_deleteflag='N' "
        gpBindCombo(lssql, "reason_name", "reason_gid", cboreason, gOdbcConn)
        cboreason.SelectedIndex = -1

    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnStatus As Integer
        Dim lnPdcId As Long
        Dim lnPacketGid As Long
        Dim lnAlmaraGid As Long
        Dim lnBounceGid As Long
        Dim lnAgreementGid As Long
        Dim lnDumpGid As Long

        Dim lsAlmaraDetails As String = ""

        Dim ds As New DataSet

        If Val(txtchqslno.Text) = 0 Then
            MsgBox("please enter Valid Slno", MsgBoxStyle.Critical)
            txtchqslno.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select bounceentry_gid "
        lssql &= " from chola_trn_tbounceentry "
        lssql &= " where bounceentry_slno=" & Val(txtchqslno.Text)

        lnBounceGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnBounceGid > 0 Then
            MsgBox("Duplicate Cheque Slno", MsgBoxStyle.Critical)
            txtchqslno.Focus()
            Exit Sub
        End If

        If txtchqno.Text.Trim = "" Then
            MsgBox("please enter Cheque No", MsgBoxStyle.Critical)
            txtchqno.Focus()
            Exit Sub
        End If

        If Not IsDate(mtxtchqdate.Text) Then
            MsgBox("please enter Valid Cheque Date", MsgBoxStyle.Critical)
            mtxtchqdate.Focus()
            Exit Sub
        End If

        If Val(txtchequeamt.Text) = 0 Then
            MsgBox("please enter Valid Cheque Amount", MsgBoxStyle.Critical)
            txtchequeamt.Focus()
            Exit Sub
        End If

        If cboagreement.Items.Count > 0 Then
            If cboagreement.Text.Trim = "" Then
                MsgBox("please enter Agreementno", MsgBoxStyle.Critical)
                cboagreement.Focus()
                Exit Sub
            Else
                lnAgreementGid = cboagreement.SelectedValue
            End If
        Else
            lnAgreementGid = 0
        End If

        If cboreason.Text = "" Then
            MsgBox("please Select Bounce Reason", MsgBoxStyle.Critical)
            cboreason.Focus()
            Exit Sub
        End If

        lssql = ""
        lssql &= " select bounce_gid "
        lssql &= " from chola_trn_tbounce "
        lssql &= " where bounce_chqno='" & txtchqno.Text.Trim & "'"
        lssql &= " and bounce_chqdate='" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"
        lssql &= " and bounce_chqamount=" & Val(txtchequeamt.Text)
        lssql &= " and bounce_isentry='N'"
        lnDumpGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnDumpGid = 0 Then
            MsgBox("Invalid Details..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        lssql = " select * from chola_trn_tpdcentry "
        lssql &= " where 1 = 1 "
        lssql &= " and chq_agreement_gid=" & lnAgreementGid
        lssql &= " and chq_no = '" & txtchqno.Text.Trim & "' "
        lssql &= " and chq_date = '" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"

        If txtPdcId.Text <> "" Then lssql &= " and entry_gid = " & Val(txtPdcId.Text) & " "

        Call gpDataSet(lssql, "pdc", gOdbcConn, ds)

        With ds.Tables("pdc")
            Select Case .Rows.Count
                Case 1
                    lnStatus = .Rows(0).Item("chq_status")
                    lnPacketGid = .Rows(0).Item("chq_packet_gid")

                    If (lnStatus And GCBOUNCERECEIVED) = 0 And (lnStatus And GCDESPATCH) > 0 Then
                        lnPdcId = .Rows(0).Item("entry_gid")

                        lssql = " update chola_trn_tpdcentry set "
                        lssql &= " chq_status = chq_status | " & GCBOUNCERECEIVED & ""
                        lssql &= " where entry_gid=" & lnPdcId & ""

                        gfInsertQry(lssql, gOdbcConn)
                    End If

                    lssql = ""
                    lssql &= " select packet_box_gid "
                    lssql &= " from chola_trn_tpacket"
                    lssql &= " where packet_gid=" & lnPacketGid

                    lnAlmaraGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                    lssql = ""
                    lssql &= " select Concat('Cupboard No:',almaraentry_cupboardno,' Shelf No:',almaraentry_shelfno,"
                    lssql &= " ' Box No:',almaraentry_boxno) "
                    lssql &= " from chola_trn_almaraentry "
                    lssql &= " where almaraentry_gid=" & lnAlmaraGid

                    lsAlmaraDetails = gfExecuteScalar(lssql, gOdbcConn)

                    lssql = " insert into chola_trn_tbounceentry (bounceentry_bounce_gid,bounceentry_entry_gid,bounceentry_agreement_gid,bounceentry_bouncereason_gid,"
                    lssql &= "bounceentry_almara_gid, bounceentry_slno,bounceentry_chqno,bounceentry_chqdate,bounceentry_chqamount,bounceentry_remarks,"
                    lssql &= "bounceentry_insertdate,bounceentry_insertby) "
                    lssql &= " values ("
                    lssql &= "" & lnDumpGid & ","
                    lssql &= "" & lnPdcId & ","
                    lssql &= "" & lnAgreementGid & ","
                    lssql &= "" & cboreason.SelectedValue & ","
                    lssql &= "" & lnAlmaraGid & ","
                    lssql &= "" & Val(txtchqslno.Text) & ","
                    lssql &= "'" & txtchqno.Text.Trim & "',"
                    lssql &= "'" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "',"
                    lssql &= "" & Val(txtchequeamt.Text) & ","
                    lssql &= "'" & txtremarks.Text.Trim & "',"
                    lssql &= " sysdate(),'" & gUserName & "')"
                    gfInsertQry(lssql, gOdbcConn)

                    UpdateBounceInward(Val(txtchqslno.Text.Trim))
                    UpdateBounce(cboagreement.Text, lnDumpGid)

                    MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)

                    If lsAlmaraDetails <> "" Then
                        MsgBox("Almara Details-" & lsAlmaraDetails, MsgBoxStyle.Information, gProjectName)
                    End If

                Case 0
                    lssql = " insert into chola_trn_tbounceentry (bounceentry_bounce_gid,bounceentry_entry_gid,bounceentry_agreement_gid,bounceentry_bouncereason_gid,"
                    lssql &= "bounceentry_almara_gid, bounceentry_slno,bounceentry_chqno,bounceentry_chqdate,bounceentry_chqamount,bounceentry_remarks,"
                    lssql &= "bounceentry_insertdate,bounceentry_insertby) "
                    lssql &= " values ("
                    lssql &= "" & lnDumpGid & ","
                    lssql &= "" & lnPdcId & ","
                    lssql &= "" & lnAgreementGid & ","
                    lssql &= "" & cboreason.SelectedValue & ","
                    lssql &= "" & lnAlmaraGid & ","
                    lssql &= "" & Val(txtchqslno.Text) & ","
                    lssql &= "'" & txtchqno.Text.Trim & "',"
                    lssql &= "'" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "',"
                    lssql &= "" & Val(txtchequeamt.Text) & ","
                    lssql &= "'" & txtremarks.Text.Trim & "',"
                    lssql &= " sysdate(),'" & gUserName & "')"
                    gfInsertQry(lssql, gOdbcConn)

                    UpdateBounceInward(Val(txtchqslno.Text.Trim))
                    UpdateBounce(cboagreement.Text, lnDumpGid)
                    MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)
                Case Else
                    MsgBox("More than one record found !", MsgBoxStyle.Critical, gProjectName)
            End Select

            .Rows.Clear()
        End With
       
        btnclear.PerformClick()
        txtchqslno.Focus()
    End Sub

    Private Sub txtchequeamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchequeamt.LostFocus
        Dim drbounce As Odbc.OdbcDataReader

        If Val(txtchequeamt.Text) > 0 Then
            If txtchqno.Text.Trim = "" Then
                MsgBox("please enter Cheque No", MsgBoxStyle.Information)
                Exit Sub
            ElseIf Not IsDate(mtxtchqdate.Text) Then
                MsgBox("please enter Valid Cheque Date", MsgBoxStyle.Information)
                Exit Sub
            Else

                lssql = ""
                lssql &= " select bounce_gid,bounce_reason_gid "
                lssql &= " from chola_trn_tbounce "
                lssql &= " where bounce_chqno='" & txtchqno.Text.Trim & "'"
                lssql &= " and bounce_chqdate='" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"
                lssql &= " and bounce_chqamount=" & Val(txtchequeamt.Text)
                lssql &= " and bounce_isentry='N'"
                drbounce = gfExecuteQry(lssql, gOdbcConn)

                If drbounce.HasRows Then
                    While drbounce.Read
                        cboreason.SelectedValue = drbounce.Item("bounce_reason_gid").ToString
                    End While
                Else
                    MsgBox("Invalid Cheque Details", MsgBoxStyle.Information)
                    Exit Sub
                End If

                lssql = " select agreement_gid,concat(shortagreement_no,'-',agreement_no) as agreement_no "
                lssql &= " from chola_trn_tpdcentry "
                lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid"
                lssql &= " where chq_no='" & txtchqno.Text.Trim & "'"
                lssql &= " and chq_amount=" & Val(txtchequeamt.Text.Trim)
                lssql &= " and chq_date='" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"

                gpBindCombo(lssql, "agreement_no", "agreement_gid", cboagreement, gOdbcConn)

                If cboagreement.Items.Count = 1 Then
                    cboagreement.SelectedIndex = 0
                Else
                    cboagreement.SelectedIndex = -1
                End If
            End If
        End If
    End Sub

    Private Sub txtchqslno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqslno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtchqno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub
    Private Sub UpdateBounceInward(ByVal CHQSLNO As Integer)
        Dim lnInwardGid As Long

        Sqlstr = ""
        Sqlstr &= " SELECT inward_gid FROM chola_trn_tbounceinward"
        Sqlstr &= " WHERE (" & CHQSLNO & " BETWEEN inward_chqfromslno AND inward_chqtoslno)"
        Sqlstr &= " AND inward_deleteflag ='N'"

        lnInwardGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

        If lnInwardGid > 0 Then
            Sqlstr = ""
            Sqlstr &= " update chola_trn_tbounceentry "
            Sqlstr &= " set bounceentry_inward_gid=" & lnInwardGid
            Sqlstr &= " where bounceentry_slno='" & CHQSLNO & "'"
            gfInsertQry(Sqlstr, gOdbcConn)
        End If

    End Sub

    Private Sub UpdateBounce(ByVal AgreementNo As String, ByVal BounceGid As Long)
        lssql = ""
        lssql &= " update chola_trn_tbounce "
        lssql &= " set bounce_isentry='Y' "
        If AgreementNo <> "" Then
            lssql &= " , bounce_agreementno='" & AgreementNo & "'"
        End If
        lssql &= " where bounce_gid=" & BounceGid

        gfInsertQry(lssql, gOdbcConn)

    End Sub

    Private Sub cboagreement_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboagreement.SelectedIndexChanged

    End Sub

    Private Sub txtchequeamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtchequeamt.TextChanged

    End Sub
End Class