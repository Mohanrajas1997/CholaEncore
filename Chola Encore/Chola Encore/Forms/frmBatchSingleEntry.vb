Public Class frmBatchSingleEntry
    Dim lssql As String
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        cboagreement.Text = ""
        txtchequeamt.Text = ""
        txtchqno.Text = ""
        mtxtchqdate.Text = ""
        cboagreement.DataSource = Nothing
        cboagreement.SelectedIndex = -1

    End Sub
    Private Sub frmpulloutentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnStatus As Integer
        Dim lnTempStatus As Integer
        Dim lnValidStatus As Integer
        Dim lnPdcId As Long
        Dim lnBatchId As Long
        Dim lnChqAmt As Double
        Dim lsGNSARef As String
        Dim lnslno As Long

        Dim ds As New DataSet

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

        If cboagreement.Text.Trim = "" Then
            MsgBox("please enter Agreementno", MsgBoxStyle.Critical)
            cboagreement.Focus()
            Exit Sub
        End If


        lssql = " select * from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " where 1 = 1 "
        lssql &= " and chq_agreement_gid=" & cboagreement.SelectedValue
        lssql &= " and chq_no = '" & txtchqno.Text.Trim & "' "
        lssql &= " and chq_amount = " & Val(txtchequeamt.Text.Trim) & " "
        lssql &= " and chq_date = '" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"

        Call gpDataSet(lssql, "pdc", gOdbcConn, ds)

        With ds.Tables("pdc")
            Select Case .Rows.Count
                Case 1
                    lnStatus = .Rows(0).Item("chq_status")
                    lnBatchId = .Rows(0).Item("chq_batch_gid")
                    lnPdcId = .Rows(0).Item("entry_gid")
                    lnChqAmt = Math.Round(.Rows(0).Item("chq_amount"), 2)
                    lsGNSARef = .Rows(0).Item("packet_gnsarefno")

                    ' Check pullout or despatch or Presentation DE or Packet Pullout
                    lnValidStatus = GCPULLOUT Or GCDESPATCH Or GCPRESENTATIONDE Or GCPACKETPULLOUT
                    lnTempStatus = lnStatus And lnValidStatus

                    If lnTempStatus Then MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName) : Exit Sub

                    If lnBatchId > 0 Then

                        lssql = " update chola_trn_tpdcentry set "
                        lssql &= " chq_status=chq_status | " & GCPRESENTATIONDE
                        lssql &= " where entry_gid = " & lnPdcId
                        gfInsertQry(lssql, gOdbcConn)

                        lssql = " update chola_trn_tbatch set batch_entrychq = if(batch_entrychq is null,0,batch_entrychq) + 1,"
                        lssql &= " batch_entrychqamt = if(batch_entrychqamt is null,0,batch_entrychqamt) + " & lnChqAmt & " "
                        lssql &= " where batch_gid =" & lnBatchId
                        gfInsertQry(lssql, gOdbcConn)

                        lssql = " select count(*) "
                        lssql &= " from chola_trn_tpdcentry "
                        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
                        lssql &= " where chq_batch_gid = " & lnBatchId
                        lssql &= " and packet_gnsarefno <= '" & lsGNSARef & "'"
                        lnslno = gfExecuteScalar(lssql, gOdbcConn)
                    End If

                    MsgBox("Batch No:" & lnBatchId & ",SLNo:" & lnslno, MsgBoxStyle.Information, gProjectName)
                Case 0
                    MsgBox("Invalid record !", MsgBoxStyle.Critical, gProjectName)
                Case Else
                    MsgBox("More than one record found !", MsgBoxStyle.Critical, gProjectName)
            End Select

            .Rows.Clear()
        End With
        btnclear.PerformClick()
        txtchqno.Focus()
    End Sub

    Private Sub txtchequeamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchequeamt.LostFocus
        If Val(txtchequeamt.Text) > 0 Then
            If txtchqno.Text.Trim = "" Then
                MsgBox("please enter Cheque No", MsgBoxStyle.Information)
                Exit Sub
            ElseIf Not IsDate(mtxtchqdate.Text) Then
                MsgBox("please enter Valid Cheque Date", MsgBoxStyle.Information)
                Exit Sub
            Else
                lssql = " select agreement_gid,concat(shortagreement_no,'-',agreement_no) as agreement_no "
                lssql &= " from chola_trn_tpdcentry "
                lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid"
                lssql &= " where chq_no='" & txtchqno.Text.Trim & "'"
                lssql &= " and chq_amount=" & Val(txtchequeamt.Text.Trim)
                lssql &= " and chq_date='" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"
                gpBindCombo(lssql, "agreement_no", "agreement_gid", cboagreement, gOdbcConn)
                cboagreement.SelectedIndex = -1
            End If
        End If
    End Sub
End Class