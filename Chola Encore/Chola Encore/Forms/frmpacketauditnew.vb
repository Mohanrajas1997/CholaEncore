Public Class frmpacketauditnew
    Dim lssql As String
    Dim lnPacketGid As Long
    Public Sub New(ByVal PacketGid As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lnPacketGid = PacketGid
    End Sub

    Private Sub frmpacketauditnew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmpacketaudit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillCombo()
        FillHeader()
        FillSPDC()
        FillPDC()
    End Sub
    Private Sub FillCombo()
        lssql = ""
        lssql &= " select city_micrcode,city_name "
        lssql &= " from chola_mst_tcity "
        lssql &= " order by city_name "
        gpBindCombo(lssql, "city_name", "city_micrcode", cbopdcbranch, gOdbcConn)

        lssql = ""
        lssql &= " select city_micrcode,city_name "
        lssql &= " from chola_mst_tcity "
        lssql &= " order by city_name "
        gpBindCombo(lssql, "city_name", "city_micrcode", cbospdcbranch, gOdbcConn)
    End Sub
    Private Sub FillHeader()
        Dim drAgreement As Odbc.OdbcDataReader

        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'ShortAgr No',packet_gnsarefno as 'GNSAREF#',packet_mode,packet_ismultiplebank "
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where packet_gid=" & lnPacketGid
        drAgreement = gfExecuteQry(lssql, gOdbcConn)

        If drAgreement.HasRows Then
            While drAgreement.Read
                lblshortagreementno.Text = drAgreement.Item("ShortAgr No").ToString
                lblmode.Text = drAgreement.Item("packet_mode").ToString
                lblpacketno.Text = drAgreement.Item("GNSAREF#").ToString
                lblagreementno.Text = drAgreement.Item("Agreement No").ToString
                If drAgreement.Item("packet_ismultiplebank").ToString = "Y" Then
                    rbtnyes.Checked = True
                    rbtnno.Checked = False
                ElseIf drAgreement.Item("packet_ismultiplebank").ToString = "N" Then
                    rbtnyes.Checked = False
                    rbtnno.Checked = True
                End If
            End While
        End If
    End Sub
    Private Sub FillSPDC()
        Dim dtpacket As DataTable

        lssql = ""
        lssql &= " select spdcentry_spdccount,spdcentry_ctschqcount,spdcentry_ecsmandatecount "
        lssql &= " from chola_trn_tspdcentry "
        lssql &= " where spdcentry_packet_gid=" & lnPacketGid

        dtpacket = GetDataTable(lssql)

        If dtpacket.Rows.Count > 0 Then
            txtspdccount.Text = Val(dtpacket.Rows(0).Item("spdcentry_spdccount").ToString)
            txtctscount.Text = Val(dtpacket.Rows(0).Item("spdcentry_ctschqcount").ToString)
            txtecscount.Text = Val(dtpacket.Rows(0).Item("spdcentry_ecsmandatecount").ToString)
        End If

        Dim dgvccolumn4 As New DataGridViewCheckBoxColumn
        dgvccolumn4.Name = "Select"
        dgvccolumn4.HeaderText = ""
        dgvspdcchqentry.Columns.Add(dgvccolumn4)

        lssql = ""
        lssql &= " select chqentry_gid,  "
        lssql &= " chqentry_chqno as 'Cheque No',chqentry_micrcode as 'MICR Code',"
        lssql &= " chqentry_accno as 'Account No',chqentry_bankcode as 'Bank Code',"
        lssql &= " chqentry_bankname as 'Bank Name',chqentry_branchname as 'Bank Branch',"
        lssql &= " chqentry_iscts as 'Is CTS',chqentry_ismicr as 'Is Micr'"
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tspdcchqentry on chqentry_packet_gid=packet_gid "
        lssql &= " where packet_gid=" & lnPacketGid

        dtpacket = GetDataTable(lssql)

        dgvspdcchqentry.DataSource = dtpacket

        dgvspdcchqentry.Columns(1).Visible = False

        'If dtpacket.Rows.Count = 0 Then Exit Sub

        If dgvpdcchqentry.RowCount = 0 Then
            lbltotal.Text = "Total:" & dtpacket.Rows.Count
            TBCChequeentry.SelectedTab = TPSPDC
            txtspdcaccountno.Focus()
        End If


        For i As Integer = 1 To dgvspdcchqentry.Columns.Count - 1
            dgvspdcchqentry.Columns(i).ReadOnly = True
        Next

        dgvspdcchqentry.Columns(0).Width = 30
        dgvspdcchqentry.Columns(2).Width = 50
        dgvspdcchqentry.Columns(3).Width = 120
        dgvspdcchqentry.Columns(4).Width = 120
        dgvspdcchqentry.Columns(5).Width = 40
        dgvspdcchqentry.Columns(6).Width = 280
        dgvspdcchqentry.Columns(7).Width = 130
        dgvspdcchqentry.Columns(8).Width = 30
        dgvspdcchqentry.Columns(9).Width = 30


    End Sub
    Private Sub FillPDC()
        Dim dtpacket As DataTable

        lssql = ""
        lssql &= " select entry_gid, "
        lssql &= " chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount',chq_accno as 'Account No',chq_micrcode as 'MICR Code',"
        lssql &= " chq_bankcode as 'Bank Code',chq_bankname as 'Bank Name',chq_bankbranch as 'Bank Branch',"
        lssql &= " chq_iscts as 'Is CTS',chq_ismicr as 'Is Micr'"
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " left join chola_trn_tpdcentry on chq_packet_gid=packet_gid "
        lssql &= " where packet_gid=" & lnPacketGid
        lssql &= " and chq_status & " & GCPULLOUT & " = 0 "
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
        lssql &= " and chq_status & " & GCCLOSURE & " = 0 "
        lssql &= " and (chq_status & " & GCPRESENTATIONPULLOUT & " = 0 "
        lssql &= " or chq_status & " & GCBOUNCERECEIVED & " > 0 )"

        dtpacket = GetDataTable(lssql)

        Dim dgvccolumn4 As New DataGridViewCheckBoxColumn
        dgvccolumn4.Name = "Select"
        dgvccolumn4.HeaderText = ""
        dgvpdcchqentry.Columns.Add(dgvccolumn4)

        dgvpdcchqentry.DataSource = dtpacket


        dgvpdcchqentry.Columns(1).Visible = False

        'If dtpacket.Rows.Count = 0 Then Exit Sub

        lbltotal.Text = "Total:" & dtpacket.Rows.Count

        For i As Integer = 1 To dgvpdcchqentry.Columns.Count - 1
            dgvpdcchqentry.Columns(i).ReadOnly = True
        Next
        txtpdcaccountno.Focus()

        dgvpdcchqentry.Columns(0).Width = 30
        dgvpdcchqentry.Columns(2).Width = 50
        dgvpdcchqentry.Columns(3).Width = 70
        dgvpdcchqentry.Columns(4).Width = 60
        dgvpdcchqentry.Columns(5).Width = 120
        dgvpdcchqentry.Columns(6).Width = 70
        dgvpdcchqentry.Columns(7).Width = 40
        dgvpdcchqentry.Columns(8).Width = 200
        dgvpdcchqentry.Columns(9).Width = 130
        dgvpdcchqentry.Columns(10).Width = 30
        dgvpdcchqentry.Columns(11).Width = 30
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnChequeGid As Long

        Dim lsiscts As String = ""
        Dim lsisMICR As String = ""
        Dim lsAccountNo As String = ""
        Dim lsMicrCode As String = ""
        Dim lsBankCode As String = ""
        Dim lsBankName As String = ""
        Dim lsBankBranch As String = ""

        Dim lsisMultiple As String = ""
        Dim liNonCTS As Integer = 0
        Dim liEntryNonCTS As Integer = 0
        Dim lnEntryGid As Long


        If dgvspdcchqentry.Rows.Count > 0 And Val(txtspdccount.Text) = 0 Then
            MsgBox("Please Enter SPDC Count..!", MsgBoxStyle.Critical, gProjectName)
            txtspdccount.Focus()
            Exit Sub
        End If

        If dgvspdcchqentry.Rows.Count > 0 And txtctscount.Text = "" Then
            MsgBox("Please Enter CTS Count..!", MsgBoxStyle.Critical, gProjectName)
            txtspdccount.Focus()
            Exit Sub
        End If

        'Multiple

        If rbtnno.Checked = False And rbtnyes.Checked = False Then
            MsgBox("Please Multiple Cheque Yes/No..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If


        liNonCTS = Val(txtspdccount.Text) - Val(txtctscount.Text)

        If liNonCTS < 0 Then
            MsgBox("Invalid CTS Count..!", MsgBoxStyle.Critical, gProjectName)
            txtctscount.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To dgvspdcchqentry.Rows.Count - 1
            If dgvspdcchqentry.Rows(i).Cells("Is CTS").Value.ToString.Trim = "N" Then
                liEntryNonCTS += 1
            End If
        Next

        If liNonCTS <> liEntryNonCTS Then
            MsgBox("Cheque Details Mismatch..!", MsgBoxStyle.Critical, gProjectName)
            txtctscount.Focus()
            Exit Sub
        End If

        If rbtnno.Checked Then
            lsisMultiple = "'N'"
        ElseIf rbtnyes.Checked Then
            lsisMultiple = "'Y'"
        Else
            lsisMultiple = "null"
        End If


        'PDC
        For i As Integer = 0 To dgvpdcchqentry.Rows.Count - 1
            If dgvpdcchqentry.Rows(i).Cells("Is CTS").Value.ToString.Trim = "" Or _
                dgvpdcchqentry.Rows(i).Cells("Account No").Value.ToString.Trim = "" Then

                MsgBox("Please Audit Cheque No." & dgvpdcchqentry.Rows(i).Cells("Cheque No").Value.ToString, MsgBoxStyle.Critical, gProjectName)
                dgvpdcchqentry.Rows(i).Selected = True
                TBCChequeentry.SelectedTab = TPPDC
                Exit Sub

            End If
        Next

        'SPDC
        For i As Integer = 0 To dgvspdcchqentry.Rows.Count - 1
            If dgvspdcchqentry.Rows(i).Cells("Is CTS").Value.ToString.Trim = "" Or _
                dgvspdcchqentry.Rows(i).Cells("Account No").Value.ToString.Trim = "" Then

                MsgBox("Please Audit Cheque No." & dgvspdcchqentry.Rows(i).Cells("Cheque No").Value.ToString, MsgBoxStyle.Critical, gProjectName)
                dgvspdcchqentry.Rows(i).Selected = True
                TBCChequeentry.SelectedTab = TPSPDC
                Exit Sub

            End If
        Next

        'PDC
        For i As Integer = 0 To dgvpdcchqentry.Rows.Count - 1

            lnChequeGid = dgvpdcchqentry.Rows(i).Cells("entry_gid").Value.ToString
            lsAccountNo = dgvpdcchqentry.Rows(i).Cells("Account No").Value.ToString
            lsMicrCode = dgvpdcchqentry.Rows(i).Cells("MICR Code").Value.ToString
            lsBankCode = dgvpdcchqentry.Rows(i).Cells("Bank Code").Value.ToString
            lsBankName = dgvpdcchqentry.Rows(i).Cells("Bank Name").Value.ToString
            lsBankBranch = dgvpdcchqentry.Rows(i).Cells("Bank Branch").Value.ToString
            lsiscts = dgvpdcchqentry.Rows(i).Cells("Is CTS").Value.ToString

            If lsMicrCode <> "" Then
                lsisMICR = "Y"
            Else
                lsisMICR = "N"
            End If

            lssql = ""
            lssql &= " update chola_trn_tpdcentry set "
            lssql &= " chq_micrcode='" & lsMicrCode & "',"
            lssql &= " chq_bankcode='" & lsBankCode & "',"
            lssql &= " chq_bankname='" & lsBankName & "',"
            lssql &= " chq_bankbranch='" & lsBankBranch & "',"
            lssql &= " chq_accno='" & lsAccountNo & "',"
            lssql &= " chq_iscts='" & lsiscts & "',"
            lssql &= " chq_ismicr='" & lsisMICR & "'"
            lssql &= " where entry_gid=" & lnChequeGid
            gfInsertQry(lssql, gOdbcConn)

            LogCTSAudit(lnChequeGid, "PDC", lsiscts, lsisMICR)
        Next


        lssql = ""
        lssql &= " select spdcentry_gid "
        lssql &= " from chola_trn_tspdcentry "
        lssql &= " where spdcentry_packet_gid=" & lnPacketGid
        lnEntryGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        If lnEntryGid > 0 Then
            lssql = ""
            lssql &= " update chola_trn_tspdcentry set "
            lssql &= " spdcentry_ecsmandatecount=" & Val(txtecscount.Text) & ","
            lssql &= " spdcentry_spdccount=" & Val(txtspdccount.Text) & ","
            lssql &= " spdcentry_ctschqcount=" & Val(txtctscount.Text) & ","
            lssql &= " spdcentry_nonctschqcount=" & liNonCTS
            lssql &= " where spdcentry_packet_gid=" & lnPacketGid
            gfInsertQry(lssql, gOdbcConn)
        Else
            lssql = ""
            lssql &= " insert into chola_trn_tspdcentry ( "
            lssql &= " spdcentry_ecsmandatecount,spdcentry_spdccount,spdcentry_ctschqcount,spdcentry_nonctschqcount)"
            lssql &= " values ("
            lssql &= "" & Val(txtecscount.Text) & ","
            lssql &= "" & Val(txtspdccount.Text) & ","
            lssql &= "" & Val(txtctscount.Text) & ","
            lssql &= "" & liNonCTS & ")"

            gfInsertQry(lssql, gOdbcConn)
        End If


        'SPDC
        For i As Integer = 0 To dgvspdcchqentry.Rows.Count - 1
            lnChequeGid = dgvspdcchqentry.Rows(i).Cells("chqentry_gid").Value.ToString
            lsAccountNo = dgvspdcchqentry.Rows(i).Cells("Account No").Value.ToString
            lsMicrCode = dgvspdcchqentry.Rows(i).Cells("MICR Code").Value.ToString
            lsBankCode = dgvspdcchqentry.Rows(i).Cells("Bank Code").Value.ToString
            lsBankName = dgvspdcchqentry.Rows(i).Cells("Bank Name").Value.ToString
            lsBankBranch = dgvspdcchqentry.Rows(i).Cells("Bank Branch").Value.ToString
            lsiscts = dgvspdcchqentry.Rows(i).Cells("Is CTS").Value.ToString

            If lsMicrCode <> "" Then
                lsisMICR = "Y"
            Else
                lsisMICR = "N"
            End If
           

            lssql = ""
            lssql &= " update chola_trn_tspdcchqentry set "
            lssql &= " chqentry_micrcode='" & lsMicrCode & "',"
            lssql &= " chqentry_bankcode='" & lsBankCode & "',"
            lssql &= " chqentry_bankname='" & lsBankName & "',"
            lssql &= " chqentry_branchname='" & lsBankBranch & "',"
            lssql &= " chqentry_accno='" & lsAccountNo & "',"
            lssql &= " chqentry_iscts='" & lsiscts & "',"
            lssql &= " chqentry_ismicr='" & lsisMICR & "'"
            lssql &= " where chqentry_gid=" & lnChequeGid
            gfInsertQry(lssql, gOdbcConn)

            LogCTSAudit(lnChequeGid, "SPDC", lsiscts, lsisMICR)
        Next

        'Update Packet
        lssql = ""
        lssql &= " update chola_trn_tpacket set "
        lssql &= " packet_ismultiplebank=" & lsisMultiple & ","
        lssql &= " packet_entryby='" & gUserName & "',"
        lssql &= " packet_entryon=sysdate()"
        lssql &= " where packet_gid=" & lnPacketGid
        gfInsertQry(lssql, gOdbcConn)

        Me.Close()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim frmchqentry As frmchequeentry
        Dim lsMicrCode As String = ""
        Dim lsAccNo As String = ""

        If txtpdcaccountno.Text <> "" Then lsAccNo = txtpdcaccountno.Text
        If txtspdcaccountno.Text <> "" Then lsAccNo = txtspdcaccountno.Text

        If txtpdcmicrcode.Text <> "" Then lsMicrCode = txtpdcmicrcode.Text
        If txtspdcmicrcode.Text <> "" Then lsMicrCode = txtspdcmicrcode.Text

        frmchqentry = New frmchequeentry(lnPacketGid, lsAccNo, lsMicrCode)
        frmchqentry.ShowDialog()
        FillSPDC()
    End Sub


    Private Sub TBCChequeentry_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBCChequeentry.SelectedIndexChanged
        If TBCChequeentry.SelectedIndex = 0 Then
            lbltotal.Text = "Total:" & dgvpdcchqentry.RowCount          
        Else
            lbltotal.Text = "Total:" & dgvspdcchqentry.RowCount            
        End If
    End Sub


    Private Sub txtspdccount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtspdccount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtctscount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtctscount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtecscount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtecscount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtmicrcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpdcmicrcode.LostFocus
        Dim drbank As Odbc.OdbcDataReader
        If txtpdcmicrcode.Text.Trim <> "" Then

            If txtpdcmicrcode.Text.Trim.Length <> 9 Then
                MsgBox("Invalid MICR Code..!", MsgBoxStyle.Critical, gProjectName)
                txtpdcmicrcode.Focus()
                Exit Sub
            End If

            'Bank Name and Code
            lssql = ""
            lssql &= " select bank_bankcode,bank_bankname "
            lssql &= " from chola_mst_tbank "
            lssql &= " where 1=1 "
            lssql &= " and bank_micrcode='" & Mid(txtpdcmicrcode.Text.Trim, 4, 3) & "'"
            drbank = gfExecuteQry(lssql, gOdbcConn)

            If drbank.HasRows Then
                drbank.Read()
                txtpdcbankcode.Text = drbank.Item("bank_bankcode").ToString
                txtpdcbankName.Text = drbank.Item("bank_bankname").ToString
            End If

            'Bank Branch
            lssql = ""
            lssql &= " select city_micrcode "
            lssql &= " from chola_mst_tcity "
            lssql &= " where city_micrcode='" & Microsoft.VisualBasic.Left(txtpdcmicrcode.Text.Trim, 3) & "'"
            cbopdcbranch.SelectedValue = gfExecuteScalar(lssql, gOdbcConn)
        End If
    End Sub

    Private Sub txtbankcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpdcbankcode.LostFocus
        If txtpdcbankcode.Text.ToUpper.Trim = "XXX" Then
            txtpdcbankName.Enabled = True
            txtpdcbankName.Focus()
        Else
            txtpdcbankName.Enabled = False
        End If
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdcupdate.Click
        Dim lirow As Integer = 0

        If txtpdcaccountno.Text.Trim = "" Then
            MsgBox("Please Enter Account No..!", MsgBoxStyle.Critical, gProjectName)
            txtpdcaccountno.Focus()
            Exit Sub
        End If

        If txtpdcbankcode.Text.Trim = "" Then
            MsgBox("Please Enter Bank Code..!", MsgBoxStyle.Critical, gProjectName)
            txtpdcbankcode.Focus()
            Exit Sub
        End If

        If txtpdcbankName.Text.Trim = "" Then
            MsgBox("Please Enter Bank Name..!", MsgBoxStyle.Critical, gProjectName)
            txtpdcbankName.Focus()
            Exit Sub
        End If

        If cbopdcbranch.Text.Trim = "" Then
            MsgBox("Please Enter Bank Branch..!", MsgBoxStyle.Critical, gProjectName)
            cbopdcbranch.Focus()
            Exit Sub
        End If

        If cbopdccts.Text = "" Then
            MsgBox("Please Select CTS Cheque or not..!", MsgBoxStyle.Critical, gProjectName)
            cbopdccts.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To dgvpdcchqentry.RowCount - 1
            If dgvpdcchqentry.Rows(i).Cells(0).Value Then
                lirow += 1
                dgvpdcchqentry.Rows(i).Cells(0).Value = False
                dgvpdcchqentry.Rows(i).Cells("Account No").Value = txtpdcaccountno.Text.Trim
                dgvpdcchqentry.Rows(i).Cells("MICR Code").Value = txtpdcmicrcode.Text.Trim
                dgvpdcchqentry.Rows(i).Cells("Bank Code").Value = txtpdcbankcode.Text.Trim
                dgvpdcchqentry.Rows(i).Cells("Bank Name").Value = txtpdcbankName.Text.Trim
                dgvpdcchqentry.Rows(i).Cells("Bank Branch").Value = cbopdcbranch.Text.Trim

                If cbopdccts.Text.ToUpper = "YES" Then
                    dgvpdcchqentry.Rows(i).Cells("Is CTS").Value = "Y"
                Else
                    dgvpdcchqentry.Rows(i).Cells("Is CTS").Value = "N"
                End If

                If txtpdcmicrcode.Text.Trim <> "" Then
                    dgvpdcchqentry.Rows(i).Cells("Is Micr").Value = "Y"
                Else
                    dgvpdcchqentry.Rows(i).Cells("Is Micr").Value = "N"
                End If
            End If
        Next

        If lirow = 0 Then
            MsgBox("Please Select Atleast one Record..!", MsgBoxStyle.Critical, gProjectName)
        Else
            txtpdcaccountno.Text = ""
            txtpdcbankcode.Text = ""
            txtpdcmicrcode.Text = ""
            txtpdcbankName.Text = ""
            cbopdcbranch.Text = ""
            cbopdccts.SelectedIndex = -1
            txtpdcaccountno.Focus()
        End If

    End Sub

    Private Sub dgvpdcchqentry_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvpdcchqentry.CellDoubleClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        txtpdcaccountno.Text = dgvpdcchqentry.Rows(e.RowIndex).Cells("Account No").Value.ToString
        txtpdcmicrcode.Text = dgvpdcchqentry.Rows(e.RowIndex).Cells("MICR Code").Value.ToString
        txtpdcbankcode.Text = dgvpdcchqentry.Rows(e.RowIndex).Cells("Bank Code").Value.ToString
        txtpdcbankName.Text = dgvpdcchqentry.Rows(e.RowIndex).Cells("Bank Name").Value.ToString
        cbopdcbranch.Text = dgvpdcchqentry.Rows(e.RowIndex).Cells("Bank Branch").Value.ToString

        If dgvpdcchqentry.Rows(e.RowIndex).Cells("Is CTS").Value.ToString = "Y" Then
            cbopdccts.SelectedIndex = 0
        ElseIf dgvpdcchqentry.Rows(e.RowIndex).Cells("Is CTS").Value.ToString = "N" Then
            cbopdccts.SelectedIndex = 1
        Else
            cbopdccts.SelectedIndex = -1
        End If
        'dgvpdcchqentry.Rows(e.RowIndex).Cells(0).Value = True
        txtpdcaccountno.Focus()
    End Sub

    Private Sub btnspdcupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnspdcupdate.Click
        Dim lirow As Integer = 0

        If txtspdcaccountno.Text.Trim = "" Then
            MsgBox("Please Enter Account No..!", MsgBoxStyle.Critical, gProjectName)
            txtspdcaccountno.Focus()
            Exit Sub
        End If

        If txtspdcbankcode.Text.Trim = "" Then
            MsgBox("Please Enter Bank Code..!", MsgBoxStyle.Critical, gProjectName)
            txtspdcbankcode.Focus()
            Exit Sub
        End If

        If txtspdcbankname.Text.Trim = "" Then
            MsgBox("Please Enter Bank Name..!", MsgBoxStyle.Critical, gProjectName)
            txtspdcbankname.Focus()
            Exit Sub
        End If

        If cbospdcbranch.Text.Trim = "" Then
            MsgBox("Please Enter Bank Branch..!", MsgBoxStyle.Critical, gProjectName)
            cbospdcbranch.Focus()
            Exit Sub
        End If

        If cbospdccts.Text = "" Then
            MsgBox("Please Select CTS Cheque or not..!", MsgBoxStyle.Critical, gProjectName)
            cbospdccts.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To dgvspdcchqentry.RowCount - 1
            If dgvspdcchqentry.Rows(i).Cells(0).Value Then
                lirow += 1
                dgvspdcchqentry.Rows(i).Cells(0).Value = False
                dgvspdcchqentry.Rows(i).Cells("Account No").Value = txtspdcaccountno.Text.Trim
                dgvspdcchqentry.Rows(i).Cells("MICR Code").Value = txtspdcmicrcode.Text.Trim
                dgvspdcchqentry.Rows(i).Cells("Bank Code").Value = txtspdcbankcode.Text.Trim
                dgvspdcchqentry.Rows(i).Cells("Bank Name").Value = txtspdcbankname.Text.Trim
                dgvspdcchqentry.Rows(i).Cells("Bank Branch").Value = cbospdcbranch.Text.Trim

                If cbospdccts.Text.ToUpper = "YES" Then
                    dgvspdcchqentry.Rows(i).Cells("Is CTS").Value = "Y"
                Else
                    dgvspdcchqentry.Rows(i).Cells("Is CTS").Value = "N"
                End If

                If txtspdcmicrcode.Text.Trim <> "" Then
                    dgvspdcchqentry.Rows(i).Cells("Is Micr").Value = "Y"
                Else
                    dgvspdcchqentry.Rows(i).Cells("Is Micr").Value = "N"
                End If
            End If
        Next

        If lirow = 0 Then
            MsgBox("Please Select Atleast one Record..!", MsgBoxStyle.Critical, gProjectName)
        Else
            txtspdcaccountno.Text = ""
            txtspdcbankcode.Text = ""
            txtspdcmicrcode.Text = ""
            txtspdcbankname.Text = ""
            cbospdcbranch.Text = ""
            cbospdccts.SelectedIndex = -1
            txtspdcaccountno.Focus()
        End If
    End Sub

    Private Sub dgvspdcchqentry_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvspdcchqentry.CellDoubleClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        txtspdcaccountno.Text = dgvspdcchqentry.Rows(e.RowIndex).Cells("Account No").Value.ToString
        txtspdcmicrcode.Text = dgvspdcchqentry.Rows(e.RowIndex).Cells("MICR Code").Value.ToString
        txtspdcbankcode.Text = dgvspdcchqentry.Rows(e.RowIndex).Cells("Bank Code").Value.ToString
        txtspdcbankname.Text = dgvspdcchqentry.Rows(e.RowIndex).Cells("Bank Name").Value.ToString
        cbospdcbranch.Text = dgvspdcchqentry.Rows(e.RowIndex).Cells("Bank Branch").Value.ToString

        If dgvspdcchqentry.Rows(e.RowIndex).Cells("Is CTS").Value.ToString = "Y" Then
            cbospdccts.SelectedIndex = 0
        ElseIf dgvspdcchqentry.Rows(e.RowIndex).Cells("Is CTS").Value.ToString = "N" Then
            cbospdccts.SelectedIndex = 1
        Else
            cbospdccts.SelectedIndex = -1
        End If
        'dgvspdcchqentry.Rows(e.RowIndex).Cells(0).Value = True
    End Sub

    Private Sub txtspdcmicrcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtspdcmicrcode.LostFocus
        Dim drbank As Odbc.OdbcDataReader
        If txtspdcmicrcode.Text.Trim <> "" Then

            If txtspdcmicrcode.Text.Trim.Length <> 9 Then
                MsgBox("Invalid MICR Code..!", MsgBoxStyle.Critical, gProjectName)
                txtspdcmicrcode.Focus()
                Exit Sub
            End If

            'Bank Name and Code
            lssql = ""
            lssql &= " select bank_bankcode,bank_bankname "
            lssql &= " from chola_mst_tbank "
            lssql &= " where 1=1 "
            lssql &= " and bank_micrcode='" & Mid(txtspdcmicrcode.Text.Trim, 4, 3) & "'"
            drbank = gfExecuteQry(lssql, gOdbcConn)

            If drbank.HasRows Then
                drbank.Read()
                txtspdcbankcode.Text = drbank.Item("bank_bankcode").ToString
                txtspdcbankname.Text = drbank.Item("bank_bankname").ToString
            End If

            'Bank Branch
            lssql = ""
            lssql &= " select city_micrcode "
            lssql &= " from chola_mst_tcity "
            lssql &= " where city_micrcode='" & Microsoft.VisualBasic.Left(txtspdcmicrcode.Text.Trim, 3) & "'"
            cbospdcbranch.SelectedValue = gfExecuteScalar(lssql, gOdbcConn)
        End If
    End Sub

    Private Sub txtspdcbankcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtspdcbankcode.LostFocus
        If txtspdcbankcode.Text.ToUpper.Trim = "XXX" Then
            txtspdcbankname.Enabled = True
            txtspdcbankname.Focus()
        Else
            txtspdcbankname.Enabled = False
        End If
    End Sub
End Class