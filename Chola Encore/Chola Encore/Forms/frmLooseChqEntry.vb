Public Class frmLooseChqEntry
    Dim msSql As String

    Private Sub frmPacketEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmPacketEntry_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As Search
        Try
            SearchDialog = New Search(gOdbcConn, _
                             " select a.loosechqentry_gid as 'Loose Entry Id',a.loosechqentry_sno as 'Serial No',c.chq_no as 'Chq No'," & _
                             " a.loosechqentry_insertdate as 'Entry Date',a.loosechqentry_insertby as 'Entry By' " & _
                             " FROM chola_trn_tloosechqentry as a inner join chola_trn_tpdcentry as c on c.entry_gid = a.loosechqentry_pdcgid", _
                             " a.loosechqentry_gid,a.loosechqentry_sno,c.chq_no,a.loosechqentry_insertdate,a.loosechqentry_insertby", _
                             " 1 = 1 and a.loosechqentry_deleteflag = 'N' ")
            SearchDialog.ShowDialog()

            If txt <> 0 Then
                Call ListAll("select a.*,c.chq_no,c.chq_date,c.chq_amount from chola_trn_tloosechqentry as a " _
                    & "inner join chola_trn_tpdcentry as c on c.entry_gid = a.loosechqentry_pdcgid " _
                    & "where a.loosechqentry_gid = " & txt & " " _
                    & "and a.loosechqentry_deleteflag = 'N' ", gOdbcConn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub ListAll(ByVal gsqry As String, ByVal odbcConn As Odbc.OdbcConnection)
        Dim lobjDataReader As Odbc.OdbcDataReader
        Dim i As Integer = 0

        Try
            lobjDataReader = gfExecuteQry(gsqry, gOdbcConn)

            With lobjDataReader
                If .HasRows Then
                    If .Read Then
                        txtId.Text = .Item("loosechqentry_gid")
                        txtChqEntryId.Text = .Item("loosechqentry_pdcgid")
                        txtSNo.Text = .Item("loosechqentry_sno")
                        txtChqNo.Text = .Item("chq_no").ToString
                        txtChqAmount.Text = Format(.Item("chq_amount"), "0.00")
                        dtpChqDate.Value = .Item("chq_date")
                        txtRemark.Text = .Item("loosechqentry_remarks").ToString

                        msSql = ""
                        msSql &= " select * from chola_mst_tagreement"
                        msSql &= " where agreement_gid = " & .Item("loosechqentry_agreementgid")

                        Call gpBindCombo(msSql, "agreement_no", "agreement_gid", cboAgrNo, gOdbcConn)

                        cboAgrNo.SelectedValue = .Item("loosechqentry_agreementgid").ToString
                    End If
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim lnResult As Long

        Try
            If txtId.Text.Trim = "" Then
                MsgBox("Select the Record", MsgBoxStyle.Information, gProjectName)
                'Calling Find Button to select record
                Call btnFind_Click(sender, e)
            Else
                If MsgBox("Are you sure want to delete this record?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
                    'Check available in pullout
                    msSql = ""
                    msSql &= " select count(*) from chola_trn_tloosechqentry "
                    msSql &= " where loosechqentry_gid=" & txtId.Text
                    msSql &= " and loosechqentry_deleteflag='N'"

                    If Val(gfExecuteScalar(msSql, gOdbcConn)) = 0 Then
                        MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
                        Exit Sub
                    End If

                    'delete from pullout
                    msSql = ""
                    msSql &= " update chola_trn_tloosechqentry set "
                    msSql &= " loosechqentry_deleteflag='Y',loosechqentry_deletedate = sysdate(),loosechqentry_deleteby = '" & QuoteFilter(gUserName) & "' "
                    msSql &= " where loosechqentry_gid = " & txtId.Text.Trim
                    msSql &= " and loosechqentry_deleteflag = 'N' "

                    lnResult = gfInsertQry(msSql, gOdbcConn)

                    'update cheque details
                    msSql = ""
                    msSql &= " update chola_trn_tpdcentry "
                    msSql &= " set chq_status = chq_status ^ " & GCLOOSECHQ & " "
                    msSql &= " where entry_gid=" & Val(txtChqEntryId.Text)
                    msSql &= " and chq_status & " & GCLOOSECHQ & " > 0 "

                    lnResult = gfInsertQry(msSql, gOdbcConn)

                    MsgBox("Record deleted successfully !", MsgBoxStyle.Information, gProjectName)
                Else
                    btnDelete.Focus()
                End If

                Call Clear_Control()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lnSNo As Integer
        Dim lnLooseChqId As Long
        Dim lnChqEntryId As Long
        Dim lnAgrId As Long
        Dim lsChqNo As String
        Dim lsChqDate As String
        Dim lnChqAmt As Double
        Dim lsRemark As String

        Dim ds As New DataSet
        Dim lnResult As Long

        Try
            If Val(txtSNo.Text) = 0 Then
                MsgBox("Invalid serial no !", MsgBoxStyle.Critical, gProjectName)
                txtSNo.Focus()
                Exit Sub
            End If

            If Val(txtChqNo.Text) = 0 Then
                MsgBox("Invalid cheque no !", MsgBoxStyle.Critical, gProjectName)
                txtChqNo.Focus()
                Exit Sub
            End If

            If cboAgrNo.SelectedIndex = -1 Or cboAgrNo.Text.Trim = "" Then
                MsgBox("Please select the reason !", MsgBoxStyle.Information, gProjectName)
                cboAgrNo.Focus()
                Exit Sub
            End If

            lnLooseChqId = Val(txtId.Text)

            lnSNo = Val(txtSNo.Text)
            lsChqNo = txtChqNo.Text
            lnChqAmt = Math.Round(Val(txtChqAmount.Text), 2)
            lsChqDate = Format(dtpChqDate.Value, "yyyy-MM-dd")
            lnAgrId = Val(cboAgrNo.SelectedValue.ToString)
            lsRemark = Mid(QuoteFilter(txtRemark.Text), 1, 255).Replace(vbCrLf, " ").Trim

            If lnLooseChqId = 0 Then
                ' Cheque availability
                msSql = ""
                msSql &= " select c.entry_gid from chola_trn_tpdcentry as c "
                msSql &= " inner join chola_trn_tpacket as p on p.packet_gid = c.chq_packet_gid "
                msSql &= " inner join chola_mst_tagreement as a on a.agreement_gid = p.packet_agreement_gid "
                msSql &= " where c.chq_no = '" & lsChqNo & "' "
                msSql &= " and c.chq_date = '" & lsChqDate & "' "
                msSql &= " and c.chq_amount = " & lnChqAmt & " "
                msSql &= " and p.packet_agreement_gid = '" & lnAgrId & "' "
                msSql &= " and c.chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCBOUNCERECEIVED Or GCLOOSECHQ) & " = 0 "
                msSql &= " and c.chq_status & " & GCPRESENTATIONDE & " > 0 "

                lnChqEntryId = Val(gfExecuteScalar(msSql, gOdbcConn))

                If lnChqEntryId = 0 Then
                    MsgBox("Invalid cheque !", MsgBoxStyle.Critical, gProjectName)
                    txtChqNo.Focus()
                    Exit Sub
                End If

                msSql = ""
                msSql &= " select count(*) from chola_trn_tloosechqentry "
                msSql &= " where loosechqentry_sno = " & lnSNo & " "
                msSql &= " and loosechqentry_deleteflag = 'N' "

                lnResult = Val(gfExecuteScalar(msSql, gOdbcConn))

                If lnResult > 0 Then
                    MsgBox("Duplicate serial no !", MsgBoxStyle.Critical, gProjectName)
                    txtSNo.Focus()
                    Exit Sub
                End If

                'insert in pullout table
                msSql = ""
                msSql &= " insert into chola_trn_tloosechqentry (loosechqentry_sno,loosechqentry_pdcgid,loosechqentry_remarks,"
                msSql &= " loosechqentry_agreementgid,loosechqentry_insertdate,loosechqentry_insertby) values ("
                msSql &= " " & lnSNo & ","
                msSql &= " '" & lnChqEntryId & "',"
                msSql &= " '" & lsRemark & "',"
                msSql &= " " & lnAgrId & ","
                msSql &= " sysdate(),"
                msSql &= " '" & gUserName & "')"

                lnResult = gfInsertQry(msSql, gOdbcConn)

                ' update in chq table
                msSql = ""
                msSql &= " update chola_trn_tpdcentry "
                msSql &= " set chq_status=chq_status | " & GCLOOSECHQ & " "
                msSql &= " where entry_gid=" & lnChqEntryId
                msSql &= " and chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCBOUNCERECEIVED Or GCLOOSECHQ) & " = 0 "

                lnResult = gfInsertQry(msSql, gOdbcConn)
            Else
                msSql = ""
                msSql &= " update chola_trn_tloosechqentry set "
                msSql &= " loosechqentry_remarks = '" & lsRemark & "' "
                msSql &= " where loosechqentry_gid = " & lnLooseChqId & " "
                msSql &= " and loosechqentry_deleteflag = 'N' "

                lnResult = gfInsertQry(msSql, gOdbcConn)
            End If

            MsgBox("Record updated successfully ! ", MsgBoxStyle.Information, gProjectName)

            Call Clear_Control()

            If MsgBox("Do you want to add another record ?", MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                txtSNo.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub Clear_Control()
        Call frmCtrClear(Me)

        txtId.Text = ""

        cboAgrNo.Text = ""
        cboAgrNo.SelectedIndex = -1

        txtSNo.Focus()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Call Clear_Control()
    End Sub

    Private Sub txtRemark_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRemark.GotFocus
        With txtRemark
            .SelectionStart = 0
            .SelectionLength = .TextLength
        End With
    End Sub

    Private Sub txtRemark_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemark.TextChanged

    End Sub

    Private Sub cboAgrNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAgrNo.GotFocus
        Dim lsChqNo As String
        Dim lsChqDate As String
        Dim lnChqAmt As Double

        lsChqNo = txtChqNo.Text
        lnChqAmt = Math.Round(Val(txtChqAmount.Text), 2)
        lsChqDate = Format(dtpChqDate.Value, "yyyy-MM-dd")

        msSql = ""
        msSql &= " select a.* from chola_trn_tpdcentry as c "
        msSql &= " inner join chola_trn_tpacket as p on p.packet_gid = c.chq_packet_gid "
        msSql &= " inner join chola_mst_tagreement as a on a.agreement_gid = p.packet_agreement_gid "
        msSql &= " where c.chq_no = '" & lsChqNo & "' "
        msSql &= " and c.chq_date = '" & lsChqDate & "' "
        msSql &= " and c.chq_amount = " & lnChqAmt & " "
        msSql &= " and c.chq_status & " & (GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL Or GCBOUNCERECEIVED Or GCLOOSECHQ) & " = 0 "
        msSql &= " and c.chq_status & " & GCPRESENTATIONDE & " > 0 "

        Call gpBindCombo(msSql, "agreement_no", "agreement_gid", cboAgrNo, gOdbcConn)

        If cboAgrNo.Items.Count = 1 Then
            cboAgrNo.SelectedIndex = 0
        Else
            cboAgrNo.SelectedIndex = -1
        End If
    End Sub

    Private Sub cboAgrNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgrNo.SelectedIndexChanged

    End Sub
End Class