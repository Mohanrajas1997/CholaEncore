Public Class frmreinward
    Dim dtpdcentry As New DataTable
    Dim dtspdcentry As New DataTable
    Dim dtecsentry As New DataTable
    Dim lssql As String
    Dim lsagreementno As String = ""
    Dim lsgnsarefno As String = ""
    Public Sub New(ByVal AgreementNo As String, ByVal GNSAREFNO As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lsagreementno = AgreementNo
        lsgnsarefno = GNSAREFNO
    End Sub

    Private Sub frminward_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
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
    Private Sub frminward_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillCombo()
        Dim dgButtonColumn As New DataGridViewButtonColumn
        dgButtonColumn.HeaderText = ""
        dgButtonColumn.UseColumnTextForButtonValue = True
        dgButtonColumn.Text = "Delete"
        dgButtonColumn.Name = "Delete"
        dgButtonColumn.ToolTipText = "Delete Row"
        dgButtonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgButtonColumn.FlatStyle = FlatStyle.System
        dgButtonColumn.DefaultCellStyle.BackColor = Color.Gray
        dgButtonColumn.DefaultCellStyle.ForeColor = Color.White
        dgvpdcentry.Columns.Add(dgButtonColumn)

        Dim dgButtonColumn1 As New DataGridViewButtonColumn
        dgButtonColumn1.HeaderText = ""
        dgButtonColumn1.UseColumnTextForButtonValue = True
        dgButtonColumn1.Text = "Delete"
        dgButtonColumn1.Name = "Delete"
        dgButtonColumn1.ToolTipText = "Delete Row"
        dgButtonColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgButtonColumn1.FlatStyle = FlatStyle.System
        dgButtonColumn1.DefaultCellStyle.BackColor = Color.Gray
        dgButtonColumn1.DefaultCellStyle.ForeColor = Color.White
        dgvspdcentry.Columns.Add(dgButtonColumn1)

        Dim dgButtonColumn2 As New DataGridViewButtonColumn
        dgButtonColumn2.HeaderText = ""
        dgButtonColumn2.UseColumnTextForButtonValue = True
        dgButtonColumn2.Text = "Delete"
        dgButtonColumn2.Name = "Delete"
        dgButtonColumn2.ToolTipText = "Delete Row"
        dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgButtonColumn2.FlatStyle = FlatStyle.System
        dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
        dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
        dgvecsentry.Columns.Add(dgButtonColumn2)

        FillGrid()
        GbECS.Enabled = False
        GbPDC.Enabled = False
        lblAgreement.Text = lsagreementno
        lblrefno.Text = lsgnsarefno
        cbomode.Focus()
    End Sub
    Private Sub FillGrid()
        Dim drentry As Odbc.OdbcDataReader
        lssql = ""
        lssql = " select entry_gid as 'Gid',chq_no as 'Cheque No.',cast(date_format(chq_date,'%d-%m-%Y') as char) as 'Cheque Date',chq_amount as 'Cheque Amount',"
        lssql &= " if(chq_type=" & GCEXTERNALNORMAL & ",'EXTERNAL-NORMAL',if(chq_type=" & GCEXTERNALSECURITY & ",'EXTERNAL-SECURITY',if(chq_type=" & GCECSNORMAL & ",'ECS-NORMAL',0))) as 'Type',"
        lssql &= " chq_accno as 'Account No',chq_micrcode as 'MICR No',chq_bankcode as 'Bank Code',chq_bankname as 'Bank Name',chq_bankbranch as 'Bank Branch',"
        lssql &= " if(chq_papflag='Y','PAP','NON PAP') as 'PAP/NONPAP',if(chq_iscts = 'Y' ,'CTS','Non CTS') as 'CTS/Non CTS',if(chq_ismicr = 'Y' ,'YES','NO') as 'MICR/Non MICR'"
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " where agreement_no='" & lsagreementno & "'"
        lssql &= " and packet_gnsarefno='" & lsgnsarefno & "'"
        dtpdcentry = GetDataTable(lssql)
        dgvpdcentry.DataSource = dtpdcentry

        lssql = ""
        lssql &= " select chqentry_gid as 'Gid',chqentry_chqno as 'Cheque No.',chqentry_micrcode as 'MICR Code',"
        lssql &= " chqentry_accno as 'Account No', chqentry_bankcode as 'Bank Code',chqentry_bankname as 'Bank Name',chqentry_branchname as 'Bank Branch',"
        lssql &= " if(chqentry_iscts = 'Y' ,'CTS','Non CTS') as 'CTS/Non CTS', if(chqentry_ismicr = 'Y' ,'YES','NO') as 'MICR/Non MICR'"
        lssql &= " from chola_trn_tspdcchqentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chqentry_packet_gid "
        lssql &= " where packet_gnsarefno='" & lsgnsarefno & "'"
        dtspdcentry = GetDataTable(lssql)
        dgvspdcentry.DataSource = dtspdcentry

        lssql = ""
        lssql &= " select ecsemientry_gid as 'Gid',ecsemientry_accno as 'Account No',ecsemientry_micrcode as 'MICR Code',ecsemientry_emidate as 'EMI Date',ecsemientry_amount as 'Amount' "
        lssql &= " from chola_trn_tecsemientry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=ecsemientry_packet_gid "
        lssql &= " where packet_gnsarefno='" & lsgnsarefno & "'"
        dtecsentry = GetDataTable(lssql)
        dgvecsentry.DataSource = dtecsentry

        lssql = " select spdcentry_gid as 'Gid',spdcentry_spdccount as 'SPDC Count',spdcentry_ecsmandatecount as 'ECS Mandate Count',spdcentry_remarks,"
        lssql &= " spdcentry_accountno,spdcentry_micrcode "
        lssql &= " from chola_trn_tspdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=spdcentry_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where agreement_no='" & lsagreementno & "'"
        lssql &= " and packet_gnsarefno='" & lsgnsarefno & "'"
        drentry = gfExecuteQry(lssql, gOdbcConn)

        If drentry.HasRows Then
            While drentry.Read
                txtecsmandatecount.Text = Val(drentry.Item("ECS Mandate Count").ToString)
                txtspdccount.Text = Val(drentry.Item("SPDC Count").ToString)
                txtremarks.Text = drentry.Item("spdcentry_remarks").ToString
                txtspdcgid.Text = drentry.Item("Gid").ToString
                txtaccno.Text = drentry.Item("spdcentry_accountno").ToString
                txtecsmicrcode.Text = drentry.Item("spdcentry_micrcode").ToString
            End While
        End If

    End Sub
    Private Sub cbomode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbomode.SelectedIndexChanged
        If cbomode.Text = "PDC" Then
            GbPDC.Enabled = True
            GbECS.Enabled = True
            cbotype.Focus()
        ElseIf cbomode.Text = "SPDC" Then
            GbPDC.Enabled = True
            GbECS.Enabled = True
            txtspdccount.Focus()
        Else
            GbECS.Enabled = False
            GbPDC.Enabled = False
            cbomode.Focus()
        End If
    End Sub

    Private Sub btnpdcadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdcadd.Click
        Dim ldchqamt As Double
        Dim lschqno As String = ""
        Dim ldchqdate As Date
        Dim lspap As String
        Dim lsMicrCode As String = ""
        Dim lsentrypap As String = ""
        Dim drpdc As Odbc.OdbcDataReader
        Dim lsInstrType As String = ""
        Dim lsismicr As String = ""
        Dim lsAccNo As String = ""
        Dim lbAccMatchFlag As Boolean = False
        Dim lbMicrInsertFlag As Boolean = False

        gbAccValidationFlag = True

        If txtchqno.Text.Trim = "" Then
            MsgBox("Please enter Cheque No", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtchqno.Focus()
            Exit Sub
        End If

        If cbotype.Text.Trim = "" Then
            MsgBox("Please Select Type", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            cbotype.Focus()
            Exit Sub
        End If

        If cbopopnonpop.Text.Trim = "" Then
            MsgBox("Please Select PAP/NONPAP", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            cbopopnonpop.Focus()
            Exit Sub
        ElseIf cbopopnonpop.Text = "PAP" Then
            lsentrypap = "Y"
        Else
            lsentrypap = "N"
        End If

        If InStr(cbotype.Text.Trim.ToUpper, "SECURITY") = 0 Then

            If Not IsDate(mstchqdate.Text) Then
                MsgBox("Please enter Valid Cheque Date", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                mstchqdate.Focus()
                Exit Sub
            End If

            If txtchqamount.Text.Trim = "" Then
                MsgBox("Please enter Cheque Amount", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                txtchqamount.Focus()
                Exit Sub
            End If

        End If

        If Val(txtchqamount.Text) = 0 Then
            If InStr(cbotype.Text.Trim.ToUpper, "SECURITY") = 0 Then
                MsgBox("Please enter Valid Cheque Amount", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                txtchqamount.Focus()
                Exit Sub
            End If
        End If

        If (InStr(cbotype.Text.Trim.ToUpper, "SECURITY") <> 0) And Val(txtchqamount.Text) <> 0 Then
            MsgBox("Cheque Amount Should be ZERO for Type EXTERNAL-SECURITY", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtchqamount.Focus()
            Exit Sub
        End If


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

        If txtrowid.Text.Trim <> "" Then
            dtpdcentry.Rows(txtrowid.Text.Trim).Delete()
            dtpdcentry.AcceptChanges()
            txtrowid.Text = ""
            btnpdcadd.Text = "ADD"
        End If

        If cbopdccts.Text = "" Then
            MsgBox("Please Select CTS/Non CTS", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            cbopdccts.Focus()
            Exit Sub
        ElseIf cbopdccts.Text = "YES" Then
            lsInstrType = "CTS"
        Else
            lsInstrType = "Non CTS"
        End If


        For i As Integer = 0 To Val(txtchqcount.Text) - 1
            For j As Integer = 0 To dtpdcentry.Rows.Count - 1
                If dtpdcentry.Rows(j).Item("Cheque No.") = txtchqno.Text Then
                    MsgBox("Cheque No. already exists.", MsgBoxStyle.Critical, gProjectName)
                    txtchqno.Focus()
                    Exit Sub
                End If

                If IsDate(mstchqdate.Text) Then
                    If dtpdcentry.Rows(j).Item("Cheque Date").ToString = mstchqdate.Text Then
                        MsgBox("Cheque Date already exists.", MsgBoxStyle.Critical, gProjectName)
                        mstchqdate.Focus()
                        Exit Sub
                    End If
                End If
            Next

            For j As Integer = 0 To dtspdcentry.Rows.Count - 1
                If dtspdcentry.Rows(j).Item("Cheque No.") = txtchqno.Text Then
                    MsgBox("Cheque No. already exists.", MsgBoxStyle.Critical, gProjectName)
                    txtchqno.Focus()
                    Exit Sub
                End If
            Next

            lsMicrCode = ""
            lspap = ""

            lssql = " select * from chola_trn_tpdcfile "
            lssql &= " where pdc_parentcontractno='" & lsagreementno & "'"
            lssql &= " and pdc_chqno='" & txtchqno.Text.Trim & "'"

            drpdc = gfExecuteQry(lssql, gOdbcConn)

            If drpdc.HasRows Then
                While drpdc.Read
                    lschqno = drpdc.Item("pdc_chqno").ToString
                    ldchqamt = drpdc.Item("pdc_chqamount").ToString
                    ldchqdate = Format(CDate(drpdc.Item("pdc_chqdate").ToString), "dd-MM-yyyy")
                    lspap = drpdc.Item("atpar_flag").ToString
                    lsMicrCode = drpdc.Item("pdc_micrcode").ToString
                    lsAccNo = drpdc.Item("pdc_acc_no").ToString
                End While
            Else
                If gobjSecurity.LoginUserGroupGID <> "2" Then
                    If lschqno <> txtchqno.Text.Trim Then
                        MsgBox("Invalid Cheque No.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                        txtchqno.Focus()
                        Exit Sub
                    End If
                End If
            End If

            If gobjSecurity.LoginUserGroupGID <> "2" Then
                If IsDate(mstchqdate.Text) = True Then
                    If ldchqdate <> Format(CDate(mstchqdate.Text.Trim), "yyyy-MM-dd") Then
                        MsgBox("Invalid Cheque Date.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                        mstchqdate.Focus()
                        Exit Sub
                    End If
                End If

                If ldchqamt > 0 Then
                    If ldchqamt <> Val(txtchqamount.Text) Then
                        If MsgBox("Chq Amount mismatch with dump" & vbCrLf & "Are you sure you want to Add?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                            txtchqamount.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                lbAccMatchFlag = True

                If lsAccNo <> txtpdcaccountno.Text Then
                    If IsNumeric(lsAccNo) = True Then
                        If Val(lsAccNo) <> Val(txtpdcaccountno.Text) Then
                            lbAccMatchFlag = False
                        End If
                    Else
                        lbAccMatchFlag = False
                    End If

                    If lbAccMatchFlag = False And gbAccValidationFlag = True Then
                        If MsgBox("Chq a/c no mismatch with dump !" & vbCrLf & "Are you sure to add?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton1, gProjectName) = MsgBoxResult.No Then
                            txtpdcaccountno.Focus()
                            Exit Sub
                        Else
                            gbAccValidationFlag = False
                        End If
                    End If
                End If

                If lsMicrCode <> txtpdcmicrcode.Text And lbMicrInsertFlag = False Then
                    If MsgBox("Micr code mismatch with dump !" & vbCrLf & "Are you sure to add ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton1, gProjectName) = MsgBoxResult.No Then
                        txtpdcmicrcode.Focus()
                        Exit Sub
                    Else
                        lbMicrInsertFlag = True
                    End If
                End If
            End If

            ' PAP validation is removed - Confirmation from Mr.Nandakumar over phone on 16-08-2013
            'If lspap <> lsentrypap Then
            '    If lspap = "Y" Then
            '        If MsgBox("This Cheque Marked As PAP in Dump..,Do you want to Continue?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            '            Exit Sub
            '        End If
            '    ElseIf lspap = "N" Then
            '        If MsgBox("This Cheque Marked As Non PAP in Dump..,Do you want to Continue?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            '            Exit Sub
            '        End If
            '    End If
            'End If

            If txtpdcmicrcode.Text.Trim <> "" Then
                lsismicr = "YES"
            Else
                lsismicr = "NO"
            End If

            dtpdcentry.Rows.Add()

            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Cheque No.") = Val(txtchqno.Text.Trim)

            If Not IsDate(mstchqdate.Text.Trim) Then
                dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Cheque Date") = ""
            Else
                dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Cheque Date") = Format(CDate(mstchqdate.Text.Trim), "dd-MM-yyyy")
            End If

            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Cheque Amount") = Val(txtchqamount.Text.Trim)
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Type") = cbotype.Text.Trim
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Account No") = txtpdcaccountno.Text.Trim
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("MICR No") = txtpdcmicrcode.Text.Trim
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Bank Code") = txtpdcbankcode.Text.Trim
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Bank Name") = txtpdcbankName.Text.Trim
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("Bank Branch") = cbopdcbranch.Text.Trim
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("PAP/NONPAP") = cbopopnonpop.Text.Trim
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("CTS/Non CTS") = lsInstrType
            dtpdcentry.Rows(dtpdcentry.Rows.Count - 1).Item("MICR/Non MICR") = lsismicr

            If Val(txtchqcount.Text) > 1 Then
                If chkpdcreverse.Checked Then
                    txtchqno.Text = Val(txtchqno.Text) - 1
                    If IsDate(mstchqdate.Text) Then
                        mstchqdate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(mstchqdate.Text)), "dd-MM-yyyy")
                    End If
                Else
                    txtchqno.Text = Val(txtchqno.Text) + 1
                    If IsDate(mstchqdate.Text) Then
                        mstchqdate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(mstchqdate.Text)), "dd-MM-yyyy")
                    End If
                End If
            End If
        Next
        dgvpdcentry.DataSource = dtpdcentry
        clear()
        txtchqno.Focus()
    End Sub
    Private Sub clear()
        txtchqamount.Text = ""
        txtchqcount.Text = 1
        txtspdcchqcnt.Text = 1
        mstchqdate.Text = ""
        cbotype.SelectedIndex = -1
        cbopopnonpop.SelectedIndex = -1
        txtchqno.Text = ""
        txtspdcchqno.Text = ""
        chkpdcreverse.Checked = False
        mtxtstartdate.Text = ""
        mtxtenddate.Text = ""
        txtecsamount.Text = ""
    End Sub

    Private Sub dgventry_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvpdcentry.CellDoubleClick
        If dgvpdcentry.RowCount = 0 Then Exit Sub
        If Val(dtpdcentry.Rows(e.RowIndex).Item(0).ToString) > 0 Then MsgBox("Access Denied!", MsgBoxStyle.Critical, gProjectName) : Exit Sub
        clear()

        cbotype.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Type").Value
        txtchqno.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Cheque No.").Value
        txtchqamount.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Cheque Amount").Value
        mstchqdate.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Cheque Date").Value
        txtpdcaccountno.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Account No").Value
        txtpdcmicrcode.Text = dgvpdcentry.Rows(e.RowIndex).Cells("MICR No").Value
        txtpdcbankcode.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Bank Code").Value
        txtpdcbankName.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Bank Name").Value
        cbopdcbranch.Text = dgvpdcentry.Rows(e.RowIndex).Cells("Bank Branch").Value
        cbopopnonpop.Text = dgvpdcentry.Rows(e.RowIndex).Cells("PAP/NONPAP").Value
        cbomode.SelectedIndex = 0
        If dgvpdcentry.Rows(e.RowIndex).Cells("CTS/Non CTS").Value = "CTS" Then
            cbopdccts.SelectedIndex = 1
        Else
            cbopdccts.SelectedIndex = 0
        End If
        btnpdcadd.Text = "Update"
        GbPDC.Enabled = True
        GbECS.Enabled = False
        txtrowid.Text = e.RowIndex
    End Sub

    Private Sub txtchqno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtecsmandatecount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtecsmandatecount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub txtspdccount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtspdccount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub
    Private Sub txtchqamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqamount.KeyPress
        e.Handled = gfAmountEntryOnly(e, txtchqamount)
    End Sub

    Private Sub dgventry_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvpdcentry.CellContentClick

        Try
            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Delete" Then
                        If Val(dtpdcentry.Rows(e.RowIndex).Item(0).ToString) > 0 Then MsgBox("Access Denied!", MsgBoxStyle.Critical, gProjectName) : Exit Sub
                        dtpdcentry.Rows(e.RowIndex).Delete()
                        dtpdcentry.AcceptChanges()
                        dgvpdcentry.DataSource = dtpdcentry
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub dgvecsentry_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvecsentry.CellContentClick

        Try
            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Delete" Then
                        If Val(dtecsentry.Rows(e.RowIndex).Item(0).ToString) > 0 Then MsgBox("Access Denied!", MsgBoxStyle.Critical, gProjectName) : Exit Sub
                        dtecsentry.Rows(e.RowIndex).Delete()
                        dtecsentry.AcceptChanges()
                        dgvecsentry.DataSource = dtecsentry
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim drpacket As Odbc.OdbcDataReader
        Dim drpdc As Odbc.OdbcDataReader

        Dim lnAgreementGid As Long
        Dim lnPacketGid As Long
        Dim lnpdcgid As Long
        Dim lnMicrGid As Long
        Dim lnEntryGid As Long
        Dim lsiscts As Char = ""
        Dim lschqdisc As String = ""
        Dim liProduct As Integer
        Dim liType As Integer
        Dim liNonCTS As Integer
        Dim liEntryNonCTS As Integer

        Dim lsEntryMode As String = ""
        Dim lsChqMode As String = ""

        Dim lcPacketDisc As String = ""
        Dim lcChqDisc As String = ""

        Dim lspap As String = ""
        Dim lsChequeDate As String = ""
        Dim lsMicrCode As String = ""

        Dim lsismicr As String = ""
        Dim lsismultiple As String = ""

        If MsgBox("Are you sure you want to Submit?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        If rbtnno.Checked = False And rbtnyes.Checked = False Then
            MsgBox("Please Select Multiple Cheque Yes/No..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        ElseIf rbtnyes.Checked Then
            lsismultiple = "Y"
        Else
            lsismultiple = "N"
        End If

        If Val(txtecsmandatecount.Text) = 0 And Val(txtspdccount.Text) = 0 Then
            If dtpdcentry.Rows.Count = 0 Then
                MsgBox("Please Enter atleast one record", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                txtchqno.Focus()
                Exit Sub
            End If
        Else

            If dtecsentry.Rows.Count > 0 Then
                If txtaccno.Text.Trim = "" Then
                    MsgBox("Please Enter Account No..!", MsgBoxStyle.Critical, gProjectName)
                    txtaccno.Focus()
                    Exit Sub
                End If

                If txtecsmicrcode.Text.Trim = "" Then
                    MsgBox("Please Enter Micr Code..!", MsgBoxStyle.Critical, gProjectName)
                    txtecsmicrcode.Focus()
                    Exit Sub
                End If
            End If

            liNonCTS = Val(txtspdccount.Text) - Val(txtctscount.Text)

            If liNonCTS < 0 Then
                MsgBox("Invalid CTS Count..!", MsgBoxStyle.Critical, gProjectName)
                txtctscount.Focus()
                Exit Sub
            End If

            For i As Integer = 0 To dgvspdcentry.Rows.Count - 1
                If dgvspdcentry.Rows(i).Cells("CTS/Non CTS").Value.ToString.ToUpper = "Non CTS".ToUpper Then
                    liEntryNonCTS += 1
                End If
            Next

            If liNonCTS <> liEntryNonCTS Then
                MsgBox("SPDC NonCTS Cheque Details Mismatch..!", MsgBoxStyle.Critical, gProjectName)
                txtctscount.Focus()
                Exit Sub
            End If

            If Val(txtspdccount.Text) <> dgvspdcentry.RowCount Then
                MsgBox("SPDC Count and Cheque Details Count Mismatch..!", MsgBoxStyle.Critical, gProjectName)
                txtspdccount.Focus()
                Exit Sub
            End If

        End If


        lssql = " select * from chola_trn_tpacket"
        lssql &= " where packet_gnsarefno='" & lsgnsarefno & "'"

        drpacket = gfExecuteQry(lssql, gOdbcConn)

        If drpacket.HasRows Then
            While drpacket.Read
                lnAgreementGid = Val(drpacket.Item("packet_agreement_gid").ToString)
                lnPacketGid = Val(drpacket.Item("packet_gid").ToString)
            End While
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            Exit Sub
        End If

        lssql = ""
        lssql &= " select agreement_prodtype "
        lssql &= " from chola_mst_tagreement "
        lssql &= " where agreement_gid=" & lnAgreementGid

        liProduct = Val(gfExecuteScalar(lssql, gOdbcConn))

        lssql = ""
        lssql &= " select pdc_mode "
        lssql &= " from chola_trn_tpdcfile "
        lssql &= " where pdc_parentcontractno='" & lsagreementno & "'"

        lsEntryMode = Val(gfExecuteScalar(lssql, gOdbcConn))

        If (lsEntryMode = "PDC" Or lsEntryMode = "RPDC") And cbomode.Text = "PDC" Then
            lcPacketDisc = "N"
        ElseIf (lsEntryMode = "ECS" Or lsEntryMode = "NPDC") And cbomode.Text = "SPDC" Then
            lcPacketDisc = "N"
        Else
            lcPacketDisc = "Y"
        End If

        If cbomode.Text = "" Then
            MsgBox("Please select the pay mode !", MsgBoxStyle.Information, gProjectName)
            cbomode.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To dtpdcentry.Rows.Count - 1
            If dtpdcentry.Rows(i).RowState <> DataRowState.Deleted Then
                If Val(dtpdcentry.Rows(i).Item(0).ToString) = 0 Then

                    'If dtpdcentry.Rows(i).Item("Mode").ToString = "PDC" Then

                    'Cheque Disc
                    lssql = " select pdc_gid,atpar_flag,pdc_chqdate,pdc_micrcode,pdc_mode,pdc_type from chola_trn_tpdcfile where chq_rec_slno=1 "
                    lssql &= " and pdc_chqno = " & Val(dtpdcentry.Rows(i).Item("Cheque No.").ToString)
                    lssql &= " and pdc_parentcontractno='" & lsagreementno & "'"

                    lssql &= " and pdc_ecsentry_gid = 0 "
                    lssql &= " and (entry_gid is null or entry_gid = 0) "
                    lssql &= " and pdc_spdcentry_gid = 0 "
                    lssql &= " and chq_rec_slno = 1 "

                    drpdc = gfExecuteQry(lssql, gOdbcConn)

                    If drpdc.HasRows Then
                        While drpdc.Read
                            lnpdcgid = drpdc.Item("pdc_gid").ToString
                            lspap = drpdc.Item("atpar_flag").ToString
                            lsMicrCode = drpdc.Item("pdc_micrcode").ToString
                            lsChqMode = drpdc.Item("pdc_type").ToString

                            If dtpdcentry.Rows(i).Item("Cheque Date").ToString <> "" Then
                                If Format(CDate(drpdc.Item("pdc_chqdate").ToString), "yyyy-MM-dd") <> Format(CDate(dtpdcentry.Rows(i).Item("Cheque Date").ToString), "yyyy-MM-dd") Then
                                    lschqdisc = GCCHQDATENOTAVBL
                                End If
                            Else
                                lschqdisc = GCZERO
                            End If

                        End While
                    Else
                        lnpdcgid = GCZERO
                        lschqdisc = GCCHQNONOTAVBL
                    End If

                    If dtpdcentry.Rows(i).Item("Type").ToString.ToUpper <> lsChqMode.ToUpper Then
                        lcChqDisc = "Y"
                    Else
                        lcChqDisc = "N"
                    End If

                    If lspap = "Y" And dtpdcentry.Rows(i).Item("PAP/NONPAP").ToString <> "PAP" Then
                        lschqdisc &= "|" & GCPAPCHANGED
                    End If

                    If lschqdisc = "" Then
                        lschqdisc = GCZERO
                    End If

                    If dtpdcentry.Rows(i).Item("CTS/Non CTS").ToString.ToUpper = "Non CTS".ToUpper Then
                        lsiscts = "N"
                    Else
                        lsiscts = "Y"
                    End If

                    'MICR CODE
                    lssql = " select micr_gid "
                    lssql &= " from chola_mst_tspeedmicr "
                    lssql &= " where micr_code='" & lsMicrCode & "'"

                    lnMicrGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                    'Type
                    If dtpdcentry.Rows(i).Item("Type").ToString.ToUpper = "EXTERNAL-NORMAL" Then
                        liType = GCEXTERNALNORMAL
                    ElseIf dtpdcentry.Rows(i).Item("Type").ToString.ToUpper = "EXTERNAL-SECURITY" Then
                        liType = GCEXTERNALSECURITY
                    ElseIf dtpdcentry.Rows(i).Item("Type").ToString.ToUpper = "ECS-NORMAL" Then
                        liType = GCECSNORMAL
                    Else
                        MsgBox("Invalid Type", MsgBoxStyle.Critical, gProjectName)
                        Exit Sub
                    End If

                    'PAP
                    If dtpdcentry.Rows(i).Item("PAP/NONPAP").ToString = "PAP" Then
                        lspap = "Y"
                    Else
                        lspap = "N"
                    End If

                    'Product
                    If liProduct = 0 Then
                        liProduct = GCOTHERS
                    End If

                    'If dtpdcentry.Rows(i).Item("PAP/NONPAP").ToString = "PAP" Then
                    '    liProduct = GCPAP
                    'ElseIf Val(lnMicrGid) > 0 Then
                    '    liProduct = GCSPEED
                    'Else
                    '    liProduct = GCOTHERS
                    'End If

                    'Cheque Date
                    If dtpdcentry.Rows(i).Item("Cheque Date").ToString <> "" Then
                        lsChequeDate = "'" & Format(CDate(dtpdcentry.Rows(i).Item("Cheque Date").ToString), "yyyy-MM-dd") & "'"
                    Else
                        lsChequeDate = "null"
                    End If

                    lssql = " insert into chola_trn_tpdcentry (chq_packet_gid,chq_agreement_gid,"
                    lssql &= "chq_pdc_gid,chq_no,chq_date,chq_amount,chq_type,chq_papflag,chq_prodtype,"
                    lssql &= "chq_iscts,chq_ismicr,chq_paymodedisc,chq_micrcode,chq_bankcode,chq_bankname,"
                    lssql &= "chq_bankbranch,chq_accno,chq_status,chq_disc_value) values ("
                    lssql &= "" & lnPacketGid & ","
                    lssql &= "" & lnAgreementGid & ","
                    lssql &= "" & lnpdcgid & ","
                    lssql &= "'" & dtpdcentry.Rows(i).Item("Cheque No.").ToString & "',"
                    lssql &= "" & lsChequeDate & ","
                    lssql &= "'" & dtpdcentry.Rows(i).Item("Cheque Amount").ToString & "',"
                    lssql &= "" & liType & ","
                    lssql &= "'" & lspap & "',"
                    lssql &= "" & liProduct & ","
                    lssql &= "'" & lsiscts & "',"
                    lssql &= "'" & lsismicr & "',"
                    lssql &= "'" & lcChqDisc & "',"
                    lssql &= "'" & dtpdcentry.Rows(i).Item("MICR No").ToString & "',"
                    lssql &= "'" & dtpdcentry.Rows(i).Item("Bank Code").ToString & "',"
                    lssql &= "'" & dtpdcentry.Rows(i).Item("Bank Name").ToString & "',"
                    lssql &= "'" & dtpdcentry.Rows(i).Item("Bank Branch").ToString & "',"
                    lssql &= "'" & dtpdcentry.Rows(i).Item("Account No").ToString & "',"
                    lssql &= "chq_status|" & GCENTRY & ","
                    lssql &= "chq_disc_value|" & lschqdisc & ");"

                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " select entry_gid from chola_trn_tpdcentry "
                    lssql &= " where chq_no='" & dtpdcentry.Rows(i).Item("Cheque No.").ToString & "'"
                    lssql &= " and chq_packet_gid=" & lnPacketGid & ""

                    lnEntryGid = Val(gfExecuteScalar(lssql, gOdbcConn))


                    'Update in Base File
                    lssql = ""
                    lssql &= " UPDATE"
                    lssql &= " chola_trn_tpdcfile"
                    lssql &= " set"
                    lssql &= " entry_gid=" & lnEntryGid & ","
                    lssql &= " atpar_flag='" & lspap & "',"
                    lssql &= " prod_type=" & liProduct & ","
                    lssql &= " org_chqdate=" & lsChequeDate & ","
                    lssql &= " org_chqamount=" & Val(dtpdcentry.Rows(i).Item("Cheque Amount").ToString) & ","
                    lssql &= " pdc_status_flag=pdc_status_flag|" & GCREENTRY & ","
                    lssql &= " file_lock='N',lock_by=null "
                    lssql &= " where pdc_gid = " & lnpdcgid
                    lssql &= " and pdc_ecsentry_gid = 0 "
                    lssql &= " and (entry_gid is null or entry_gid = 0) "
                    lssql &= " and pdc_spdcentry_gid = 0 "

                    gfExecuteQry(lssql, gOdbcConn)
                End If
            End If
        Next

        If dtspdcentry.Rows.Count > 0 Then
            For i As Integer = 0 To dtspdcentry.Rows.Count - 1
                If dtspdcentry.Rows(i).RowState <> DataRowState.Deleted Then
                    If Val(dtspdcentry.Rows(i).Item(0).ToString) = 0 Then

                        Dim lnEPdcGid As Long
                        Dim lnEentryGid As Long

                        lssql = ""
                        lssql &= " select pdc_gid,pdc_type "
                        lssql &= " from chola_trn_tpdcfile "
                        lssql &= " where pdc_parentcontractno='" & lsagreementno & "'"
                        lssql &= " and pdc_chqno = " & Val(dtspdcentry.Rows(i).Item("Cheque No.").ToString)

                        lssql &= " and pdc_ecsentry_gid = 0 "
                        lssql &= " and (entry_gid is null or entry_gid = 0) "
                        lssql &= " and pdc_spdcentry_gid = 0 "
                        lssql &= " and chq_rec_slno = 1 "

                        'lssql &= " and upper(pdc_type)='EXTERNAL-SECURITY'"

                        drpdc = gfExecuteQry(lssql, gOdbcConn)

                        If drpdc.HasRows Then
                            While drpdc.Read
                                lnEPdcGid = drpdc.Item("pdc_gid").ToString
                                lsChqMode = drpdc.Item("pdc_type").ToString
                            End While
                        Else
                            lnEPdcGid = 0
                            lsChqMode = ""
                        End If

                        If lsChqMode.ToUpper <> "EXTERNAL-SECURITY" Then
                            lcChqDisc = "Y"
                        Else
                            lcChqDisc = "N"
                        End If

                        If dtspdcentry.Rows(i).Item("CTS/Non CTS").ToString.ToUpper = "Non CTS".ToUpper Then
                            lsiscts = "N"
                        Else
                            lsiscts = "Y"
                        End If


                        If dtspdcentry.Rows(i).Item("MICR/Non MICR").ToString.ToUpper.Trim = "YES" Then
                            lsismicr = "Y"
                        Else
                            lsismicr = "N"
                        End If
                        lssql = ""
                        lssql &= " insert into chola_trn_tspdcchqentry ("
                        lssql &= " chqentry_packet_gid,chqentry_pdc_gid,chqentry_chqno,chqentry_micrcode,"
                        lssql &= " chqentry_bankcode,chqentry_bankname,chqentry_branchname,chqentry_accno,"
                        lssql &= " chqentry_iscts,chqentry_ismicr,chqentry_paymodedisc,"
                        lssql &= " chqentry_entryby,chqentry_entryon)"
                        lssql &= " values ("
                        lssql &= "" & lnPacketGid & ","
                        lssql &= "" & lnEPdcGid & ","
                        lssql &= "'" & dtspdcentry.Rows(i).Item("Cheque No.").ToString & "',"
                        lssql &= "'" & dtspdcentry.Rows(i).Item("MICR Code").ToString & "',"
                        lssql &= "'" & dtspdcentry.Rows(i).Item("Bank Code").ToString & "',"
                        lssql &= "'" & dtspdcentry.Rows(i).Item("Bank Name").ToString & "',"
                        lssql &= "'" & dtspdcentry.Rows(i).Item("Bank Branch").ToString & "',"
                        lssql &= "'" & dtspdcentry.Rows(i).Item("Account No").ToString & "',"
                        lssql &= "'" & lsiscts & "',"
                        lssql &= "'" & lsismicr & "',"
                        lssql &= "'" & lcChqDisc & "',"
                        lssql &= "'" & gUserName & "',"
                        lssql &= " sysdate())"

                        gfInsertQry(lssql, gOdbcConn)

                        lssql = ""
                        lssql &= " select chqentry_gid "
                        lssql &= " from chola_trn_tspdcchqentry "
                        lssql &= " where chqentry_packet_gid=" & lnPacketGid
                        lssql &= " and chqentry_chqno='" & dtspdcentry.Rows(i).Item("Cheque No.").ToString & "'"
                        lssql &= " and chqentry_pdc_gid=" & lnEPdcGid

                        lnEentryGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                        lssql = ""
                        lssql &= " update chola_trn_tpdcfile "
                        lssql &= " set pdc_spdcentry_gid=" & lnEentryGid
                        lssql &= " where pdc_gid=" & lnEPdcGid
                        gfInsertQry(lssql, gOdbcConn)

                    End If
                End If
            Next
        End If

        If dtecsentry.Rows.Count > 0 Then
            For i As Integer = 0 To dtecsentry.Rows.Count - 1
                If dtecsentry.Rows(i).RowState <> DataRowState.Deleted Then
                    If Val(dtecsentry.Rows(i).Item(0).ToString) = 0 Then
                        Dim lnEPdcGid As Long
                        Dim lnEentryGid As Long

                        lssql = ""
                        lssql &= " select pdc_gid,pdc_type "
                        lssql &= " from chola_trn_tpdcfile "
                        lssql &= " where pdc_parentcontractno='" & lsagreementno & "'"
                        lssql &= " and pdc_chqdate='" & Format(CDate(dtecsentry.Rows(i).Item("EMI Date").ToString), "yyyy-MM-dd") & "'"
                        lssql &= " and pdc_chqamount = " & Math.Round(Val(dtecsentry.Rows(i).Item("Amount").ToString), 2) & " "
                        lssql &= " and pdc_ecsentry_gid = 0 "
                        lssql &= " and (entry_gid is null or entry_gid = 0) "
                        lssql &= " and pdc_spdcentry_gid = 0 "
                        lssql &= " and chq_rec_slno = 1 "

                        'lssql &= " and upper(pdc_type)='ECS-NORMAL'"
                        'lnEPdcGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                        drpdc = gfExecuteQry(lssql, gOdbcConn)

                        If drpdc.HasRows Then
                            While drpdc.Read
                                lnEPdcGid = drpdc.Item("pdc_gid").ToString
                                lsChqMode = drpdc.Item("pdc_type").ToString
                            End While
                        Else
                            lnEPdcGid = 0
                            lsChqMode = ""
                        End If

                        If lsChqMode.ToUpper <> "ECS-NORMAL" Then
                            lcChqDisc = "Y"
                        Else
                            lcChqDisc = "N"
                        End If

                        lssql = ""
                        lssql &= " insert into chola_trn_tecsemientry ("
                        lssql &= " ecsemientry_packet_gid,ecsemientry_pdc_gid,"
                        lssql &= " ecsemientry_accno,ecsemientry_micrcode,ecsemientry_emidate,"
                        lssql &= " ecsemientry_amount,ecsemientry_paymodedisc,ecsemientry_entryby,ecsemientry_entryon)"
                        lssql &= " values ("
                        lssql &= "" & lnPacketGid & ","
                        lssql &= "" & lnEPdcGid & ","
                        lssql &= "'" & dtecsentry.Rows(i).Item("Account No").ToString & "',"
                        lssql &= "'" & dtecsentry.Rows(i).Item("MICR Code").ToString & "',"
                        lssql &= "'" & Format(CDate(dtecsentry.Rows(i).Item("EMI Date").ToString), "yyyy-MM-dd") & "',"
                        lssql &= "" & Val(dtecsentry.Rows(i).Item("Amount").ToString) & ","
                        lssql &= "'" & lcChqDisc & "',"
                        lssql &= "'" & gUserName & "',"
                        lssql &= "sysdate())"
                        gfInsertQry(lssql, gOdbcConn)

                        lssql = ""
                        lssql &= " select ecsemientry_gid "
                        lssql &= " from chola_trn_tecsemientry "
                        lssql &= " where ecsemientry_packet_gid=" & lnPacketGid
                        lssql &= " and ecsemientry_emidate='" & Format(CDate(dtecsentry.Rows(i).Item("EMI Date").ToString), "yyyy-MM-dd") & "'"
                        lssql &= " and ecsemientry_pdc_gid=" & lnEPdcGid

                        lnEentryGid = Val(gfExecuteScalar(lssql, gOdbcConn))

                        lssql = ""
                        lssql &= " update chola_trn_tpdcfile "
                        lssql &= " set pdc_ecsentry_gid=" & lnEentryGid
                        lssql &= " where pdc_gid=" & lnEPdcGid
                        gfInsertQry(lssql, gOdbcConn)
                    End If
                End If
            Next
        End If

        If (Val(txtecsmandatecount.Text) > 0 Or Val(txtspdccount.Text) > 0) And Val(txtspdcgid.Text) = 0 Then
            Dim drecs As Odbc.OdbcDataReader
            Dim lsstartdate As String = ""
            Dim lsenddate As String = ""

            lssql = ""
            lssql &= " select min(ecsemientry_emidate) as 'Start', max(ecsemientry_emidate) as 'end'"
            lssql &= " from chola_trn_tecsemientry "
            lssql &= " where ecsemientry_packet_gid=" & lnPacketGid
            lssql &= " having `Start` is not null "
            drecs = gfExecuteQry(lssql, gOdbcConn)

            If drecs.HasRows Then
                While drecs.Read
                    lsstartdate = "'" & Format(CDate(drecs.Item("Start").ToString), "yyyy-MM-dd") & "'"
                    lsenddate = "'" & Format(CDate(drecs.Item("end").ToString), "yyyy-MM-dd") & "'"
                End While
            Else
                lsstartdate = "null"
                lsenddate = "null"
            End If

            lssql = ""
            lssql &= "insert into chola_trn_tspdcentry ("
            lssql &= " spdcentry_packet_gid,spdcentry_spdccount,spdcentry_ecsmandatecount,"
            lssql &= " spdcentry_ctschqcount,spdcentry_nonctschqcount,spdcentry_remarks,"
            lssql &= " spdcentry_accountno,spdcentry_micrcode,spdcentry_startdate,spdcentry_enddate,spdcentry_ecsamount)"
            lssql &= " values ("
            lssql &= "" & lnPacketGid & ","
            lssql &= "" & Val(txtspdccount.Text) & ","
            lssql &= "" & Val(txtecsmandatecount.Text) & ","
            lssql &= "" & Val(txtctscount.Text) & ","
            lssql &= "" & Val(txtspdccount.Text) - Val(txtctscount.Text) & ","
            lssql &= "'" & txtremarks.Text & "',"
            lssql &= "'" & txtaccno.Text.Trim & "',"
            lssql &= "'" & txtecsmicrcode.Text.Trim & "',"
            lssql &= "" & lsstartdate & ","
            lssql &= "" & lsenddate & ","
            lssql &= "" & Val(txtecsamount.Text.Trim) & ")"
            gfInsertQry(lssql, gOdbcConn)

        ElseIf (Val(txtecsmandatecount.Text) > 0 Or Val(txtspdccount.Text) > 0) And Val(txtspdcgid.Text) > 0 Then

            Dim drecs As Odbc.OdbcDataReader
            Dim lsstartdate As String = ""
            Dim lsenddate As String = ""

            lssql = ""
            lssql &= " select min(ecsemientry_emidate) as 'Start', max(ecsemientry_emidate) as 'end'"
            lssql &= " from chola_trn_tecsemientry "
            lssql &= " where ecsemientry_packet_gid=" & lnPacketGid
            lssql &= " having `Start` is not null "
            drecs = gfExecuteQry(lssql, gOdbcConn)

            If drecs.HasRows Then
                While drecs.Read
                    lsstartdate = "'" & Format(CDate(drecs.Item("Start").ToString), "yyyy-MM-dd") & "'"
                    lsenddate = "'" & Format(CDate(drecs.Item("end").ToString), "yyyy-MM-dd") & "'"
                End While
            Else
                lsstartdate = "null"
                lsenddate = "null"
            End If

            lssql = ""
            lssql &= " update chola_trn_tspdcentry "
            lssql &= " set spdcentry_spdccount=" & Val(txtspdccount.Text) & ","
            lssql &= " spdcentry_ecsmandatecount=" & Val(txtecsmandatecount.Text) & ","
            lssql &= " spdcentry_ctschqcount=" & Val(txtctscount.Text) & ","
            lssql &= " spdcentry_nonctschqcount=" & Val(txtspdccount.Text) - Val(txtctscount.Text) & ","
            lssql &= " spdcentry_remarks='" & txtremarks.Text & "',"
            lssql &= " spdcentry_accountno='" & txtaccno.Text.Trim & "',"
            lssql &= " spdcentry_micrcode='" & txtecsmicrcode.Text.Trim & "',"
            lssql &= " spdcentry_startdate=" & lsstartdate & ","
            lssql &= " spdcentry_enddate=" & lsenddate & ","
            lssql &= " spdcentry_ecsamount=null"
            lssql &= " where spdcentry_gid=" & Val(txtspdcgid.Text)

            gfInsertQry(lssql, gOdbcConn)

        End If

        LogPacket("", GCPACKETCHEQUEREENTRY, lnPacketGid, cbomode.Text, , , lcPacketDisc, lsismultiple)
        UpdateAlmara(lsgnsarefno, cbomode.Text)
        LogPacketHistory("", GCPACKETCHEQUEREENTRY, lnPacketGid)

        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("Are you sure you want to close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If
        Me.Close()
    End Sub
    Private Sub btnaddspdc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddspdc.Click
        Dim lsCTS As String = ""
        Dim lsismicr As String = ""

        gbAccValidationFlag = True

        If txtspdcchqno.Text = "" Then
            MsgBox("Please Enter Cheque Number..!", MsgBoxStyle.Critical, gProjectName)
            txtspdcchqno.Focus()
            Exit Sub
        End If

        'If txtmicrcode.Text = "" Then
        '    MsgBox("Please Enter MICR Code..!", MsgBoxStyle.Critical, gProjectName)
        '    txtmicrcode.Focus()
        '    Exit Sub
        'End If

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
            MsgBox("Please Select CTS/Non CTS", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            cbospdccts.Focus()
            Exit Sub
        ElseIf cbospdccts.Text = "YES" Then
            lsCTS = "CTS"
        Else
            lsCTS = "Non CTS"
        End If

        If txtmicrcode.Text.Trim = "" Then
            lsismicr = "NO"
        Else
            lsismicr = "YES"
        End If

        For i As Integer = 0 To Val(txtspdcchqcnt.Text) - 1
            For j As Integer = 0 To dtspdcentry.Rows.Count - 1
                If dtspdcentry.Rows(j).Item("Cheque No.") = txtspdcchqno.Text Then
                    MsgBox("Cheque No. already exists.", MsgBoxStyle.Critical, gProjectName)
                    txtspdcchqno.Focus()
                    Exit Sub
                End If
            Next

            For j As Integer = 0 To dtpdcentry.Rows.Count - 1
                If dtpdcentry.Rows(j).Item("Cheque No.") = txtspdcchqno.Text Then
                    MsgBox("Cheque No. already exists.", MsgBoxStyle.Critical, gProjectName)
                    txtspdcchqno.Focus()
                    Exit Sub
                End If
            Next

            If gobjSecurity.LoginUserGroupGID <> "2" Then
                If ValidateSPDC(lsagreementno, txtspdcchqno.Text, txtmicrcode.Text, txtspdcaccountno.Text) = False Then
                    MsgBox("Not available in Finone !", MsgBoxStyle.Critical, gProjectName)
                    txtspdcchqno.Focus()
                    Exit Sub
                End If
            End If

            dtspdcentry.Rows.Add()
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("Cheque No.") = Val(txtspdcchqno.Text.Trim)
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("MICR Code") = txtmicrcode.Text.Trim
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("Account No") = txtspdcaccountno.Text.Trim
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("Bank Code") = txtspdcbankcode.Text.Trim
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("Bank Name") = txtspdcbankname.Text.Trim
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("Bank Branch") = cbospdcbranch.Text.Trim
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("CTS/Non CTS") = lsCTS
            dtspdcentry.Rows(dtspdcentry.Rows.Count - 1).Item("MICR/Non MICR") = lsismicr

            If Val(txtspdcchqcnt.Text) > 1 Then
                txtspdcchqno.Text = Val(txtspdcchqno.Text) + 1
            End If
        Next
        dgvspdcentry.DataSource = dtspdcentry
        tbsummary.SelectedTab = TPSPDC
        clear()
        txtchqno.Focus()
    End Sub

    Private Sub dgvspdcentry_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvspdcentry.CellContentClick
        Try
            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Delete" Then
                        If Val(dtspdcentry.Rows(e.RowIndex).Item(0).ToString) > 0 Then MsgBox("Access Denied!", MsgBoxStyle.Critical, gProjectName) : Exit Sub
                        dtspdcentry.Rows(e.RowIndex).Delete()
                        dtspdcentry.AcceptChanges()
                        dgvspdcentry.DataSource = dtspdcentry
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub txtctscount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtctscount.LostFocus
        If Val(txtecsmandatecount.Text) = 0 Then
            txtspdcchqno.Focus()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnecsadd.Click
        Dim n As Integer

        gbAccValidationFlag = True

        If txtaccno.Text.Trim = "" Then
            MsgBox("Please Enter Account No..!", MsgBoxStyle.Critical, gProjectName)
            txtaccno.Focus()
            Exit Sub
        End If

        If txtecsmicrcode.Text.Trim = "" Then
            MsgBox("Please Enter Micr Code..!", MsgBoxStyle.Critical, gProjectName)
            txtecsmicrcode.Focus()
            Exit Sub
        End If

        If Not (IsDate(mtxtstartdate.Text.Trim)) Then
            MsgBox("Please Enter Valid Start Date..!", MsgBoxStyle.Critical, gProjectName)
            mtxtstartdate.Focus()
            Exit Sub
        End If

        If Not (IsDate(mtxtenddate.Text.Trim)) Then
            MsgBox("Please Enter Valid End Date..!", MsgBoxStyle.Critical, gProjectName)
            mtxtenddate.Focus()
            Exit Sub
        End If

        If Val(txtecsamount.Text.Trim) <= 0 Then
            MsgBox("Please Enter Valid ECS Amount..!", MsgBoxStyle.Critical, gProjectName)
            txtecsamount.Focus()
            Exit Sub
        End If

        n = DateDiff(DateInterval.Month, CDate(mtxtstartdate.Text.Trim), CDate(mtxtenddate.Text.Trim)) + 1

        If n > 0 Then
            If MsgBox("Total Months : " & n & ". Are you sure to generate !", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                Exit Sub
            End If
        Else
            MsgBox("Invalid date range !", MsgBoxStyle.Critical, gProjectName)
            mtxtstartdate.Focus()
            Exit Sub
        End If

        While CDate(mtxtstartdate.Text.Trim) <= CDate(mtxtenddate.Text.Trim)
            For i As Integer = 0 To dtecsentry.Rows.Count - 1
                If DateDiff(DateInterval.Day, dtecsentry.Rows(i).Item("EMI Date"), CDate(mtxtstartdate.Text.Trim)) = 0 Then
                    MsgBox("EMI Date Duplicate..!", MsgBoxStyle.Critical, gProjectName)
                    mtxtstartdate.Focus()
                    Exit Sub
                End If
            Next

            For i As Integer = 0 To dtpdcentry.Rows.Count - 1
                If IsDate(dtpdcentry.Rows(i).Item("Cheque Date").ToString) = True Then
                    If DateDiff(DateInterval.Day, dtpdcentry.Rows(i).Item("Cheque Date"), CDate(mtxtstartdate.Text.Trim)) = 0 Then
                        MsgBox("EMI Date Duplicate..!", MsgBoxStyle.Critical, gProjectName)
                        mtxtstartdate.Focus()
                        Exit Sub
                    End If
                End If
            Next

            If gobjSecurity.LoginUserGroupGID <> "2" Then
                If ValidateECS(lsagreementno, CDate(mtxtstartdate.Text.Trim), Math.Round(Val(txtecsamount.Text), 2), txtecsmicrcode.Text, txtaccno.Text) = False Then
                    MsgBox("Not available in Finone !", MsgBoxStyle.Critical, gProjectName)
                    mtxtstartdate.Focus()
                    Exit Sub
                End If
            End If

            dtecsentry.Rows.Add()
            dtecsentry.Rows(dtecsentry.Rows.Count - 1).Item("Account No") = txtaccno.Text.Trim
            dtecsentry.Rows(dtecsentry.Rows.Count - 1).Item("MICR Code") = txtecsmicrcode.Text.Trim
            dtecsentry.Rows(dtecsentry.Rows.Count - 1).Item("EMI Date") = Format(CDate(mtxtstartdate.Text.Trim), "dd-MM-yyyy")
            dtecsentry.Rows(dtecsentry.Rows.Count - 1).Item("Amount") = Math.Round(Val(txtecsamount.Text.Trim), 2)
            mtxtstartdate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(mtxtstartdate.Text.Trim)), "dd-MM-yyyy")
        End While

        dgvecsentry.DataSource = dtecsentry
        tbsummary.SelectedTab = TPECS
        clear()
        mtxtstartdate.Focus()
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

    Private Sub txtmicrcode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmicrcode.GotFocus
        txtmicrcode.SelectAll()
    End Sub
    Private Sub txtspdcmicrcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmicrcode.LostFocus
        Dim drbank As Odbc.OdbcDataReader
        If txtmicrcode.Text.Trim <> "" Then

            If txtmicrcode.Text.Trim.Length <> 9 Then
                MsgBox("Invalid MICR Code..!", MsgBoxStyle.Critical, gProjectName)
                txtmicrcode.Focus()
                Exit Sub
            End If

            'Bank Name and Code
            lssql = ""
            lssql &= " select bank_bankcode,bank_bankname "
            lssql &= " from chola_mst_tbank "
            lssql &= " where 1=1 "
            lssql &= " and bank_micrcode='" & Mid(txtmicrcode.Text.Trim, 4, 3) & "'"
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
            lssql &= " where city_micrcode='" & Microsoft.VisualBasic.Left(txtmicrcode.Text.Trim, 3) & "'"
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

    Private Sub txtaccno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaccno.GotFocus
        txtaccno.SelectAll()
    End Sub

    Private Sub txtaccno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtaccno.TextChanged
        txtspdcaccountno.Text = txtaccno.Text
    End Sub

    Private Sub txtecsmicrcode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtecsmicrcode.GotFocus
        txtecsmicrcode.SelectAll()
    End Sub

    Private Sub txtecsmicrcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtecsmicrcode.TextChanged
        txtmicrcode.Text = txtecsmicrcode.Text
    End Sub

    Private Sub txtmicrcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmicrcode.TextChanged

    End Sub

    Private Sub txtspdcaccountno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtspdcaccountno.GotFocus
        txtspdcaccountno.SelectAll()
    End Sub

    Private Sub txtspdcaccountno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtspdcaccountno.TextChanged

    End Sub

    Private Sub txtpdcaccountno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpdcaccountno.TextChanged
        txtaccno.Text = txtpdcaccountno.Text
    End Sub

    Private Sub txtpdcmicrcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpdcmicrcode.TextChanged
        txtecsmicrcode.Text = txtpdcmicrcode.Text
    End Sub

    Private Sub cbopdcbranch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbopdcbranch.SelectedIndexChanged

    End Sub

    Private Sub txtpdcbankcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpdcbankcode.TextChanged

    End Sub
End Class