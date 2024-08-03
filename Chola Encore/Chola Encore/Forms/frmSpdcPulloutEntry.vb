Public Class frmSpdcPulloutEntry
    Dim msSql As String

    Private Sub frmPacketEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmPacketEntry_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub frmPacketEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        msSql = ""
        msSql &= " select * from chola_mst_tpulloutreason"
        msSql &= " where true "
        msSql &= " and reason_deleteflag='N' "
        msSql &= " order by reason_name"

        Call gpBindCombo(msSql, "reason_name", "reason_gid", cboReason, gOdbcConn)
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim SearchDialog As Search
        Try
            SearchDialog = New Search(gOdbcConn, _
                             " select a.spdcpullout_gid as 'SPDC Pullout Id',a.spdcpullout_shortagreementno as 'Short Agreement No',a.spdcpullout_chqno as 'Chq No'," & _
                             " a.spdcpullout_insertdate as 'Entry Date',a.spdcpullout_insertby as 'Entry By' " & _
                             " FROM chola_trn_tspdcpullout as a ", _
                             " a.spdcpullout_gid,a.spdcpullout_shortagreementnoa.spdcpullout_chqno,a.spdcpullout_insertdate,a.spdcpullout_insertby", _
                             " 1 = 1 and a.spdcpullout_deleteflag = 'N' ")
            SearchDialog.ShowDialog()

            If txt <> 0 Then
                Call ListAll("select a.* from chola_trn_tspdcpullout as a " _
                    & "where a.spdcpullout_gid = " & txt & " " _
                    & "and a.spdcpullout_deleteflag = 'N' ", gOdbcConn)
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
                        txtId.Text = .Item("spdcpullout_gid")
                        txtChqEntryId.Text = .Item("spdcpullout_chqentrygid")
                        txtShortAgreementNo.Text = .Item("spdcpullout_shortagreementno")
                        txtChqNo.Text = .Item("spdcpullout_chqno").ToString
                        txtRemark.Text = .Item("spdcpullout_remarks").ToString
                        cboReason.SelectedValue = .Item("spdcpullout_reasongid").ToString
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
                    msSql &= " select count(*) from chola_trn_tspdcpullout "
                    msSql &= " where spdcpullout_gid=" & txtId.Text
                    msSql &= " and spdcpullout_deleteflag='N'"

                    If Val(gfExecuteScalar(msSql, gOdbcConn)) = 0 Then
                        MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)
                        Exit Sub
                    End If

                    'delete from pullout
                    msSql = ""
                    msSql &= " update chola_trn_tspdcpullout set "
                    msSql &= " spdcpullout_deleteflag='Y',spdcpullout_deletedate = sysdate(),spdcpullout_deleteby = '" & QuoteFilter(gUserName) & "' "
                    msSql &= " where spdcpullout_gid = " & txtId.Text.Trim
                    msSql &= " and spdcpullout_deleteflag = 'N' "

                    lnResult = gfInsertQry(msSql, gOdbcConn)

                    'update cheque details
                    msSql = ""
                    msSql &= " update chola_trn_tspdcchqentry "
                    msSql &= " set chqentry_status = chqentry_status ^ " & GCPULLOUT & ",chqentry_remarks = '' "
                    msSql &= " where chqentry_gid=" & Val(txtChqEntryId.Text)
                    msSql &= " and chqentry_status & " & GCCHQRETRIEVAL & " = 0 "
                    msSql &= " and chqentry_status & " & GCPULLOUT & " > 0 "
                    msSql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                    msSql &= " and chqentry_status & " & GCPRESENTATIONPULLOUT & " = 0 "

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
        Dim lnPulloutId As Long
        Dim lnChqEntryId As Long
        Dim lsShortAgreementNo As String
        Dim lsChqNo As String
        Dim lsReason As String
        Dim lsRemark As String
        Dim lnReasonId As Long

        Dim ds As New DataSet
        Dim lnResult As Long

        Try
            If Val(txtShortAgreementNo.Text) = 0 Then
                MsgBox("Invalid agreement no !", MsgBoxStyle.Critical, gProjectName)
                txtShortAgreementNo.Focus()
                Exit Sub
            End If

            If cboAgreementNo.Text = "" Or cboAgreementNo.SelectedIndex = -1 Then
                MsgBox("Invalid agreement no !", MsgBoxStyle.Critical, gProjectName)
                cboAgreementNo.Focus()
                Exit Sub
            End If

            If Val(txtChqNo.Text) = 0 Then
                MsgBox("Invalid cheque no !", MsgBoxStyle.Critical, gProjectName)
                txtChqNo.Focus()
                Exit Sub
            End If

            If cboReason.SelectedIndex = -1 Or cboReason.Text.Trim = "" Then
                MsgBox("Please select the reason !", MsgBoxStyle.Information, gProjectName)
                cboReason.Focus()
                Exit Sub
            End If

            lnPulloutId = Val(txtId.Text)

            lsShortAgreementNo = Format(Val(txtShortAgreementNo.Text), "000000")
            lsChqNo = txtChqNo.Text
            lsReason = QuoteFilter(cboReason.Text)
            lnReasonId = Val(cboReason.SelectedValue.ToString)
            lsRemark = Mid(QuoteFilter(txtRemark.Text), 1, 255)

            If lnPulloutId = 0 Then
                ' Cheque availability
                msSql = ""
                msSql &= " select c.chqentry_gid from chola_trn_tspdcchqentry as c "
                msSql &= " inner join chola_trn_tpacket as p on p.packet_gid = c.chqentry_packet_gid "
                msSql &= " inner join chola_mst_tagreement as a on a.agreement_gid = p.packet_agreement_gid "
                msSql &= " where c.chqentry_chqno = '" & lsChqNo & "' "
                msSql &= " and a.shortagreement_no = '" & lsShortAgreementNo & "' "
                msSql &= " and a.agreement_no = '" & cboAgreementNo.Text & "' "
                msSql &= " and c.chqentry_status & " & (GCCLOSURE Or GCPACKETPULLOUT Or GCPULLOUT Or GCCHQRETRIEVAL) & " = 0 "

                lnChqEntryId = Val(gfExecuteScalar(msSql, gOdbcConn))

                If lnChqEntryId = 0 Then
                    MsgBox("Invalid cheque !", MsgBoxStyle.Critical, gProjectName)
                    txtChqNo.Focus()
                    Exit Sub
                End If

                'insert in pullout table
                msSql = ""
                msSql &= " insert into chola_trn_tspdcpullout (spdcpullout_chqentrygid,spdcpullout_shortagreementno,spdcpullout_chqno,"
                msSql &= " spdcpullout_remarks,spdcpullout_reasongid,spdcpullout_insertdate,spdcpullout_insertby) values ("
                msSql &= " '" & lnChqEntryId & "',"
                msSql &= " '" & lsShortAgreementNo & "',"
                msSql &= " '" & lsChqNo & "',"
                msSql &= " '" & lsRemark & "',"
                msSql &= " '" & lnReasonId & "',"
                msSql &= " sysdate(),"
                msSql &= " '" & gUserName & "')"

                lnResult = gfInsertQry(msSql, gOdbcConn)

                ' update in chq table
                msSql = ""
                msSql &= " update chola_trn_tspdcchqentry "
                msSql &= " set chqentry_status=chqentry_status | " & GCPULLOUT & ",chqentry_remarks = '" & lsReason & "' "
                msSql &= " where chqentry_gid=" & lnChqEntryId
                msSql &= " and chqentry_status & " & GCCHQRETRIEVAL & " = 0 "
                msSql &= " and chqentry_status & " & GCPULLOUT & " = 0 "
                msSql &= " and chqentry_status & " & GCPACKETPULLOUT & " = 0 "
                msSql &= " and chqentry_status & " & GCPRESENTATIONPULLOUT & " = 0 "

                lnResult = gfInsertQry(msSql, gOdbcConn)
            Else
                msSql = ""
                msSql &= " update chola_trn_tspdcpullout set "
                msSql &= " spdcpullout_remarks = '" & lsRemark & "' "
                msSql &= " where spdcpullout_gid = " & lnPulloutId & " "
                msSql &= " and spdcpullout_deleteflag = 'N' "

                lnResult = gfInsertQry(msSql, gOdbcConn)
            End If

            MsgBox("Record updated successfully ! ", MsgBoxStyle.Information, gProjectName)

            Call Clear_Control()

            If MsgBox("Do you want to add another record ?", MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                txtShortAgreementNo.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub Clear_Control()
        Call frmCtrClear(Me)

        txtId.Text = ""

        cboReason.Text = ""
        cboReason.SelectedIndex = -1

        txtShortAgreementNo.Focus()
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

    Private Sub cboAgreementNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAgreementNo.GotFocus
        Dim lsSql As String

        If txtShortAgreementNo.Text <> "" Then
            lsSql = ""
            lsSql &= " select * from chola_mst_tagreement "
            lsSql &= " where shortagreement_no = '" & txtShortAgreementNo.Text & "' "

            Call gpBindCombo(lsSql, "agreement_no", "agreement_gid", cboAgreementNo, gOdbcConn)

            If cboAgreementNo.Items.Count = 1 Then
                cboAgreementNo.SelectedIndex = 0
            Else
                cboAgreementNo.SelectedIndex = -1
                cboAgreementNo.Text = ""
            End If
        End If
    End Sub

    Private Sub cboAgreementNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgreementNo.SelectedIndexChanged

    End Sub
End Class