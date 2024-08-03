
Public Class frmchqentry
    Dim lssql As String
    Dim fsSystemIp As String

    Private Sub frmmanualbatchentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmmanualbatchentry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fsSystemIp = IPAddresses(Net.Dns.GetHostName)
        FillEntryGrid()
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
                lssql = " select distinct agreement_gid,concat(shortagreement_no,'-',agreement_no,'-',ifnull(chq_accno,'')) as shortagreement_no "
                lssql &= " from chola_trn_tpdcentry "
                lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid"
                lssql &= " where chq_no='" & txtchqno.Text.Trim & "'"
                lssql &= " and chq_amount=" & Val(txtchequeamt.Text.Trim)
                lssql &= " and chq_date='" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"

                gpBindCombo(lssql, "shortagreement_no", "agreement_gid", cboagreementno, gOdbcConn)

                If cboagreementno.Items.Count = 0 Then Exit Sub
                If cboagreementno.Items.Count > 1 Then
                    cboagreementno.SelectedIndex = -1
                Else
                    cboagreementno.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Sub FillEntryGrid()
        Dim ds As New DataSet

        lssql = " set @slno:=0 "
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select @slno:= @slno + 1 as 'SL No',packet_gnsarefno as 'GNSA REF#',"
        lssql &= " shortagreement_no as 'Short Agreement No',agreement_no as 'Agreement No',"
        lssql &= " chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',chq_amount as 'Amount',"
        lssql &= " if(chq_papflag='Y','PAP','NON PAP') as 'PAP/NON PAP',type_name as 'Product',chqentry_isactive as 'Is Active' "
        lssql &= " from chola_trn_chqentry  "
        lssql &= " inner join chola_trn_tpdcentry on entry_gid=chqentry_entry_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_mst_ttype on chola_mst_ttype.type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " where chqentry_systemip='" & fsSystemIp & "'"
        lssql &= " and chqentry_entryby='" & gUserName & "'"
        lssql &= " and chqentry_batch_gid = 0 "
        lssql &= " order by chqentry_gid "

        gpPopGridView(dgvEntry, lssql, gOdbcConn)

        lblTotal.Text = "Total : " & dgvEntry.Rows.Count.ToString

        'Select Case dgvEntry.Rows.Count
        '    Case 199
        '        MsgBox("Batch reached 199 cheques !", MsgBoxStyle.Information, gProjectName)
        '    Case 200
        '        MsgBox("Batch reached 200 cheques !", MsgBoxStyle.Information, gProjectName)
        '    Case Is > 200
        '        MsgBox("Batch reached more than 200 cheques !", MsgBoxStyle.Information, gProjectName)
        'End Select

        lssql = ""
        lssql &= " select chq_prodtype,date_format(chq_date,'%d-%m-%Y') as 'Cycle Date', "
        lssql &= " type_name as 'Product',"
        lssql &= " count(*) as 'Cheque Count',sum(chq_amount) as 'Total Amount' "
        lssql &= " from chola_trn_chqentry "
        lssql &= " inner join chola_trn_tpdcentry on entry_gid=chqentry_entry_gid "
        lssql &= " inner join chola_mst_ttype on chola_mst_ttype.type_flag=chq_prodtype and type_deleteflag='N'"
        lssql &= " where chqentry_systemip='" & fsSystemIp & "'"
        lssql &= " and chqentry_isactive='Y' "
        lssql &= " and chqentry_batch_gid=0 "
        lssql &= " and chqentry_entryby='" & gUserName & "' "
        lssql &= " group by chq_date,type_name,chqentry_systemip "
        lssql &= " order by sum(chq_amount) desc "

        dgvSummary.Columns.Clear()
        gpPopGridView(dgvSummary, lssql, gOdbcConn)
        dgvSummary.Columns(0).Visible = False

        txtchqno.Focus()
    End Sub

    Private Sub frmmanualbatchentry_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        dgvEntry.Top = gbentry.Top + gbentry.Height + 6
        dgvEntry.Width = Me.Width - 36
        dgvEntry.Height = Me.Height - (gbentry.Top + gbentry.Height + 48)
    End Sub

    Private Sub btncreatebatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreatebatch.Click
        Dim frmbatch As New frmchqentrybatch
        frmbatch.ShowDialog()
        FillEntryGrid()
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnStatus As Integer
        Dim lnTempStatus As Integer
        Dim lnValidStatus As Integer
        Dim lnPdcId As Long
        Dim lnResult As Long
        Dim lsproduct As String

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

        If cboagreementno.Text.Trim = "" Then
            MsgBox("please Select Agreement No", MsgBoxStyle.Critical)
            cboagreementno.Focus()
            Exit Sub
        End If

        lssql = " select * from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
        lssql &= " where 1 = 1 "
        lssql &= " and chq_agreement_gid=" & cboagreementno.SelectedValue
        lssql &= " and chq_no = '" & txtchqno.Text.Trim & "' "
        lssql &= " and chq_date = '" & Format(CDate(mtxtchqdate.Text), "yyyy-MM-dd") & "'"
        lssql &= " and chq_amount=" & Val(txtchequeamt.Text)
        lssql &= " and chq_type = 1 "
        lssql &= " and chq_status & " & (GCPULLOUT Or GCDESPATCH Or GCPACKETPULLOUT Or GCPRESENTATIONDE Or GCPRESENTATIONPULLOUT) & " = 0 "

        If Val(txtPdcId.Text) > 0 Then lssql &= " and entry_gid = " & Val(txtPdcId.Text) & " "

        Call gpDataSet(lssql, "pdc", gOdbcConn, ds)

        With ds.Tables("pdc")
            Select Case .Rows.Count
                Case 1
                    lnStatus = .Rows(0).Item("chq_status")
                    lnPdcId = .Rows(0).Item("entry_gid")
                    lsproduct = .Rows(0).Item("type_flag").ToString & "-" & .Rows(0).Item("type_name").ToString

                    ' Check pullout or despatch or Packet Pullout or Presentation Pullout or Presentation Dataentry
                    lnValidStatus = GCPULLOUT Or GCDESPATCH Or GCPACKETPULLOUT Or GCPRESENTATIONDE Or GCPRESENTATIONPULLOUT Or GCCHQRETRIEVAL
                    lnTempStatus = lnStatus And lnValidStatus


                    If lnTempStatus Then MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName) : Exit Sub

                    lssql = ""
                    lssql &= " select chqentry_entry_gid from chola_trn_chqentry "
                    lssql &= " where chqentry_entry_gid = " & lnPdcId & " "

                    lnResult = Val(gfExecuteScalar(lssql, gOdbcConn))

                    If lnResult > 0 Then
                        MsgBox("Cheque already entered !", MsgBoxStyle.Information, gProjectName)
                        Exit Sub
                    End If

                    lssql = " insert into chola_trn_chqentry ("
                    lssql &= " chqentry_entry_gid,chqentry_entryby,chqentry_entryon,chqentry_systemip)"
                    lssql &= " values ("
                    lssql &= "" & lnPdcId & ","
                    lssql &= "'" & gUserName & "',"
                    lssql &= " sysdate(),"
                    lssql &= "'" & fsSystemIp & "')"
                    gfInsertQry(lssql, gOdbcConn)

                    MsgBox("Product is " & lsproduct & "..!", MsgBoxStyle.Information, gProjectName)

                    FillEntryGrid()
                    txtchqno.Text = ""
                    txtchequeamt.Text = ""
                    cboagreementno.DataSource = Nothing

                Case 0
                    MsgBox("Invalid record !", MsgBoxStyle.Critical, gProjectName)
                Case Else
                    MsgBox("More than one record found !", MsgBoxStyle.Critical, gProjectName)
            End Select

            .Rows.Clear()
        End With
        txtchqno.Focus()
    End Sub

    Private Sub cboagreementno_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboagreementno.SelectedIndexChanged

    End Sub
End Class