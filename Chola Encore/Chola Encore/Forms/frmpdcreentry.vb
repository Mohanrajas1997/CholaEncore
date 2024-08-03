Public Class frmpdcreentry
    Dim lssql As String = ""
    Dim lscontractno As String = ""
    Dim lsgnsarefno As String = ""
    Dim dtentry As New DataTable
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal ContractNo As String, ByVal GNSAREFNo As String)

        lscontractno = ContractNo
        lsgnsarefno = GNSAREFNo
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmspdcentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmspdcentry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objdt As DataTable

        lssql = " select pdc_shortpdc_parentcontractno,pdc_contractno,pdc_parentcontractno,pdc_draweename,pdc_mode,pdc_gnsarefno "
        lssql &= " from chola_trn_tpdcfile "
        lssql &= " where pdc_shortpdc_parentcontractno='" & lscontractno & "'"
        lssql &= " and pdc_gnsarefno='" & lsgnsarefno & "'"
        lssql &= " group by pdc_contractno "

        objdt = GetDataTable(lssql)

        If objdt.Rows.Count > 0 Then
            For i As Integer = 0 To objdt.Rows.Count - 1
                lstcontractnos.Items.Add(objdt.Rows(i).Item("pdc_contractno").ToString)
            Next
            lblAgreement.Text = objdt.Rows(0).Item("pdc_shortpdc_parentcontractno").ToString
            lblname.Text = objdt.Rows(0).Item("pdc_draweename").ToString
            lblmode.Text = objdt.Rows(0).Item("pdc_mode").ToString
            lblrefno.Text = objdt.Rows(0).Item("pdc_gnsarefno").ToString
        End If

        'lssql = " select distinct pdc_type as 'type'"
        'lssql &= " from chola_trn_tpdcfile "
        'lssql &= " where pdc_shortpdc_parentcontractno='" & lscontractno & "'"
        'lssql &= " and pdc_gnsarefno='" & lsgnsarefno & "'"

        'objdt = GetDataTable(lssql)
        'cbotype.DataSource = objdt
        'cbotype.DisplayMember = "type"
        'cbotype.ValueMember = "type"


        lssql = " select entry_gid as 'Gid', chq_no as 'Cheque No.',cast(chq_date as char) as 'Cheque Date',chq_amount as 'Cheque Amount',"
        lssql &= " if(chq_type=" & GCEXTERNALNORMAL & ",'EXTERNAL-NORMAL',if(chq_type=" & GCEXTERNALSECURITY & ",'EXTERNAL-SECURITY',if(chq_type=" & GCECSNORMAL & ",'ECS-NORMAL',0))) as 'Type',"
        lssql &= " if(chq_papflag='Y','PAP','NON PAP') as 'PAP/NONPAP' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " where shortagreement_no='" & lscontractno & "'"
        lssql &= " and packet_gnsarefno='" & lsgnsarefno & "'"

        dtentry = GetDataTable(lssql)
        dgventry.DataSource = dtentry

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
        dgventry.Columns.Add(dgButtonColumn)

        dgventry.Columns("Cheque No.").Width = dgventry.Width * 0.2
        dgventry.Columns("Cheque Date").Width = dgventry.Width * 0.2
        dgventry.Columns("Cheque Amount").Width = dgventry.Width * 0.1
        dgventry.Columns("Type").Width = dgventry.Width * 0.3


        txtchqno.Focus()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim ldchqamt As Double
        Dim lspap As String
        Dim lsentrypap As String
        Dim dtecs As DataTable

        If InStr(cbotype.Text.Trim.ToUpper, "ECS") = 0 Then
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

            If txtrowid.Text.Trim <> "" Then
                dtentry.Rows(txtrowid.Text.Trim).Delete()
                dtentry.AcceptChanges()
                txtrowid.Text = ""
                btnadd.Text = "ADD"
            End If


            For i As Integer = 0 To Val(txtchqcount.Text) - 1

                For j As Integer = 0 To dtentry.Rows.Count - 1

                    If dtentry.Rows(j).Item("Cheque No.") = txtchqno.Text Then
                        MsgBox("Cheque No. already exists.", MsgBoxStyle.Critical, gProjectName)
                        txtchqno.Focus()
                        Exit Sub
                    End If

                    If dtentry.Rows(j).Item("Cheque Date").ToString = mstchqdate.Text Then
                        MsgBox("Cheque Date. already exists.", MsgBoxStyle.Critical, gProjectName)
                        txtchqno.Focus()
                        Exit Sub
                    End If

                Next

                If gobjSecurity.LoginUserGroupGID <> "2" Then
                    lssql = " select pdc_gid from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lscontractno & "' and pdc_chqno='" & txtchqno.Text.Trim & "'"
                    If Val(gfExecuteScalar(lssql, gOdbcConn)) = 0 Then
                        MsgBox("Invalid Cheque No.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                        txtchqno.Focus()
                        Exit Sub
                    End If

                    lssql = " select pdc_gid from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lscontractno & "' and pdc_chqno='" & txtchqno.Text.Trim & "'"

                    If IsDate(mstchqdate.Text.Trim) Then
                        lssql &= " and date_format(pdc_chqdate,'%Y-%m-%d')='" & Format(CDate(mstchqdate.Text), "yyyy-MM-dd") & "'"
                    End If

                    If Val(gfExecuteScalar(lssql, gOdbcConn)) = 0 Then
                        MsgBox("Invalid Cheque Date.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                        txtchqno.Focus()
                        Exit Sub
                    End If

                End If

                lssql = " select atpar_flag from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lscontractno & "' and pdc_chqno='" & txtchqno.Text.Trim & "'"
                lspap = gfExecuteScalar(lssql, gOdbcConn)

                If lspap <> lsentrypap Then
                    If lspap = "Y" Then
                        If MsgBox("This Cheque Marked As PAP in Dump..,Do you want to Continue?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If
                    Else
                        If MsgBox("This Cheque Marked As Non PAP in Dump..,Do you want to Continue?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                            Exit Sub
                        End If
                    End If
                End If


                lssql = " select pdc_chqamount from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lscontractno & "' and pdc_chqno='" & txtchqno.Text.Trim & "'"
                ldchqamt = Val(gfExecuteScalar(lssql, gOdbcConn))

                If ldchqamt > 0 Then
                    If ldchqamt <> Val(txtchqamount.Text) Then
                        If MsgBox("Chq Amount mismatch with dump" & vbCrLf & "Are you sure you want to Add?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                            txtchqamount.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                dtentry.Rows.Add()
                dtentry.Rows(dtentry.Rows.Count - 1).Item("Cheque No.") = Val(txtchqno.Text.Trim)

                If Not IsDate(mstchqdate.Text.Trim) Then
                    dtentry.Rows(dtentry.Rows.Count - 1).Item("Cheque Date") = ""
                Else
                    dtentry.Rows(dtentry.Rows.Count - 1).Item("Cheque Date") = Format(CDate(mstchqdate.Text.Trim), "dd-MM-yyyy")
                End If

                dtentry.Rows(dtentry.Rows.Count - 1).Item("Cheque Amount") = Val(txtchqamount.Text.Trim)
                dtentry.Rows(dtentry.Rows.Count - 1).Item("Type") = cbotype.Text.Trim
                dtentry.Rows(dtentry.Rows.Count - 1).Item("PAP/NONPAP") = cbopopnonpop.Text.Trim

                If Val(txtchqcount.Text) > 1 Then
                    txtchqno.Text = Val(txtchqno.Text) + 1
                    If IsDate(mstchqdate.Text) Then
                        mstchqdate.Text = DateAdd(DateInterval.Month, 1, CDate(mstchqdate.Text))
                    End If


                End If
            Next

        Else
            If Val(txtecsamount.Text.Trim) = 0 Then
                MsgBox("Please enter ECS Amount", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                txtecsamount.Focus()
                Exit Sub
            End If

            If Val(txtecscount.Text.Trim) = 0 Then
                MsgBox("Please enter ECS Count", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                txtecscount.Focus()
                Exit Sub
            End If

            lssql = " select pdc_chqamount from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lscontractno & "' and pdc_type='ECS-NORMAL'"
            ldchqamt = Val(gfExecuteScalar(lssql, gOdbcConn))

            If ldchqamt > 0 Then
                If ldchqamt <> Val(txtecsamount.Text) Then
                    If MsgBox("ECS Amount mismatch with dump" & vbCrLf & "Are you sure you want to Add?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                        txtecsamount.Focus()
                        Exit Sub
                    End If
                End If
            End If

            lssql = " select distinct pdc_chqno,pdc_chqdate from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lscontractno & "' and pdc_type='ECS-NORMAL'"
            dtecs = GetDataTable(lssql)

            If dtecs.Rows.Count > 0 Then
                If dtecs.Rows.Count <> Val(txtecscount.Text) Then
                    MsgBox("ECS Count Mismatch With dump", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                    txtecscount.Focus()
                    Exit Sub
                    'If MsgBox("ECF Count mismatch with dump" & vbCrLf & "Are you sure you want to Add?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
                    '    Exit Sub
                    'End If
                End If
            End If

            For i As Integer = 0 To dtecs.Rows.Count - 1
                dtentry.Rows.Add()
                dtentry.Rows(dtentry.Rows.Count - 1)(0) = dtecs.Rows(i)(0).ToString
                dtentry.Rows(dtentry.Rows.Count - 1)(1) = Format(CDate(dtecs.Rows(i)(1).ToString), "dd-MM-yyyy")
                dtentry.Rows(dtentry.Rows.Count - 1)(2) = txtecsamount.Text.Trim
                dtentry.Rows(dtentry.Rows.Count - 1)(3) = cbotype.Text.Trim
            Next
        End If

        FillGrid(dtentry, txtchqcount.Text)

        txtchqamount.Text = ""
        txtchqno.Text = ""
        mstchqdate.Text = ""
        txtecsamount.Text = ""
        txtecscount.Text = ""
        txtchqcount.Text = 1
        cbotype.SelectedIndex = 0
        cbotype.Focus()
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim lsgnsarefno As String
        Dim lsmicrgid As String
        Dim lsentrygid As String
        Dim lsagreementgid As String
        Dim lspdcgid As String = ""
        Dim lspacketgid As String
        Dim drpdc As Odbc.OdbcDataReader
        Dim lschqdisc As String = ""
        Dim lspap As String = ""

        If MsgBox("Are you sure you want to Submit?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        If dtentry.Rows.Count = 0 Then
            MsgBox("Please Enter atleast one record", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtchqno.Focus()
            Exit Sub
        End If

        'lssql = " select distinct pdc_gnsarefno from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lscontractno & "'"
        lsgnsarefno = lblrefno.Text
        'Agreement Gid
        lssql = " select agreement_gid from chola_mst_tagreement where shortagreement_no='" & lscontractno & "'"
        lsagreementgid = gfExecuteScalar(lssql, gOdbcConn)

        'Packet Gid
        lssql = " select packet_gid from chola_trn_tpacket where packet_agreement_gid=" & lsagreementgid
        lssql &= " and packet_gnsarefno='" & lsgnsarefno & "'"
        lspacketgid = gfExecuteScalar(lssql, gOdbcConn)

        lssql = ""
        lssql &= " update chola_trn_tpacket set "
        lssql &= " packet_gnsarefno='" & lsgnsarefno & "',"
        lssql &= " packet_mode='" & lblmode.Text & "',"
        lssql &= " packet_entryby='" & gUserName & "',"
        lssql &= " packet_entryon=sysdate()"
        lssql &= " where packet_gid=" & lspacketgid

        gfInsertQry(lssql, gOdbcConn)



        For i As Integer = 0 To dtentry.Rows.Count - 1
            If dtentry.Rows(i).RowState <> DataRowState.Deleted Then
                If Val(dtentry.Rows(i).Item("Gid").ToString) = 0 Then

                    'Cheque Disc
                    lssql = " select pdc_gid,atpar_flag from chola_trn_tpdcfile where chq_rec_slno=1 "
                    lssql &= "and pdc_chqno='" & dtentry.Rows(i).Item("Cheque No.").ToString & "'"
                    lssql &= " and pdc_shortpdc_parentcontractno='" & lscontractno & "'"
                    drpdc = gfExecuteQry(lssql, gOdbcConn)

                    If drpdc.HasRows Then
                        While drpdc.Read
                            lspdcgid = drpdc.Item("pdc_gid").ToString
                            lspap = drpdc.Item("atpar_flag").ToString
                            lschqdisc = GCZERO
                        End While
                    Else
                        lspdcgid = GCZERO
                        lschqdisc = GCCHQNONOTAVBL
                    End If

                    If Val(lspdcgid) > 0 And dtentry.Rows(i).Item("Cheque Date").ToString <> "" Then
                        lssql = " select pdc_gid from chola_trn_tpdcfile where chq_rec_slno=1 "
                        lssql &= " and pdc_chqno='" & dtentry.Rows(i).Item("Cheque No.").ToString & "'"
                        lssql &= " and pdc_chqdate='" & Format(CDate(dtentry.Rows(i).Item("Cheque Date").ToString), "yyyy-MM-dd") & "'"
                        lssql &= " and pdc_shortpdc_parentcontractno='" & lscontractno & "'"
                        drpdc = gfExecuteQry(lssql, gOdbcConn)

                        If drpdc.HasRows Then
                            lschqdisc = GCZERO
                        Else
                            lschqdisc = GCCHQDATENOTAVBL
                        End If
                    End If

                    If lspap = "Y" And dtentry.Rows(i).Item("PAP/NONPAP").ToString <> "PAP" Then
                        lschqdisc &= "|" & GCPAPCHANGED
                    End If

                    'MICR CODE
                    lssql = " select micr_gid "
                    lssql &= " from chola_mst_tspeedmicr "
                    lssql &= " inner join chola_trn_tpdcfile on pdc_micrcode=micr_code "
                    lssql &= " where 1=1 and chq_rec_slno=1 "
                    lssql &= " and pdc_shortpdc_parentcontractno='" & lscontractno & "'"
                    lssql &= " and pdc_chqno='" & dtentry.Rows(i).Item("Cheque No.").ToString & "'"

                    lsmicrgid = gfExecuteScalar(lssql, gOdbcConn)

                    lssql = " insert into chola_trn_tpdcentry (chq_packet_gid,chq_agreement_gid,"
                    lssql &= "chq_pdc_gid,chq_no,chq_date,chq_amount,chq_type,chq_papflag,chq_prodtype,chq_status,chq_disc_value) values ("
                    lssql &= "" & lspacketgid & ","
                    lssql &= "" & lsagreementgid & ","
                    lssql &= "" & lspdcgid & ","
                    lssql &= "'" & dtentry.Rows(i).Item("Cheque No.").ToString & "',"

                    If dtentry.Rows(i).Item("Cheque Date").ToString <> "" Then
                        lssql &= "'" & Format(CDate(dtentry.Rows(i).Item("Cheque Date").ToString), "yyyy-MM-dd") & "',"
                    Else
                        lssql &= "null,"
                    End If

                    lssql &= "'" & dtentry.Rows(i).Item("Cheque Amount").ToString & "',"

                    If dtentry.Rows(i).Item("Type").ToString.ToUpper = "EXTERNAL-NORMAL" Then
                        lssql &= "" & GCEXTERNALNORMAL & ","
                    ElseIf dtentry.Rows(i).Item("Type").ToString.ToUpper = "EXTERNAL-SECURITY" Then
                        lssql &= "" & GCEXTERNALSECURITY & ","
                    ElseIf dtentry.Rows(i).Item("Type").ToString.ToUpper = "ECS-NORMAL" Then
                        lssql &= "" & GCECSNORMAL & ","
                    Else
                        lssql &= "" & GCZERO & ","
                    End If


                    If dtentry.Rows(i).Item("PAP/NONPAP").ToString = "PAP" Then
                        lssql &= "'Y',"
                    Else
                        lssql &= "'N',"
                    End If

                    If dtentry.Rows(i).Item("PAP/NONPAP").ToString = "PAP" Then
                        lssql &= "" & GCPAP & ","

                    ElseIf Val(lsmicrgid) > 0 Then
                        lssql &= "" & GCSPEED & ","
                    Else
                        lssql &= "" & GCOTHERS & ","
                    End If

                    lssql &= "chq_status|" & GCREENTRY & ","

                    lssql &= "chq_disc_value|" & lschqdisc & ");"

                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " select entry_gid from chola_trn_tpdcentry "
                    lssql &= " where chq_no='" & dtentry.Rows(i).Item("Cheque No.").ToString & "'"
                    lssql &= " and chq_packet_gid=" & lspacketgid & ""

                    lsentrygid = gfExecuteScalar(lssql, gOdbcConn)


                    'Update in Base File
                    lssql = ""
                    lssql &= " UPDATE"
                    lssql &= " chola_trn_tpdcfile"
                    lssql &= " set"
                    lssql &= " entry_gid=" & lsentrygid & ","

                    If dtentry.Rows(i).Item("PAP/NONPAP").ToString = "PAP" Then
                        lssql &= "atpar_flag='Y',"
                    Else
                        lssql &= "atpar_flag='N',"
                    End If


                    If dtentry.Rows(i).Item("PAP/NONPAP").ToString = "PAP" Then
                        lssql &= "prod_type=" & GCPAP & ","

                    ElseIf Val(lsmicrgid) > 0 Then
                        lssql &= "prod_type=" & GCSPEED & ","
                    Else
                        lssql &= "prod_type=" & GCOTHERS & ","
                    End If

                    If dtentry.Rows(i).Item("Cheque Date").ToString = "" Then
                        lssql &= "org_chqdate=null,"
                    Else
                        lssql &= "org_chqdate='" & Format(CDate(dtentry.Rows(i).Item("Cheque Date").ToString), "yyyy-MM-dd") & "',"
                    End If
                    lssql &= " org_chqamount=" & Val(dtentry.Rows(i).Item("Cheque Amount").ToString) & ","
                    lssql &= " pdc_status_flag=pdc_status_flag|" & GCREENTRY & ","
                    lssql &= " file_lock='N',lock_by=null "
                    lssql &= " where 1=1"
                    lssql &= " and pdc_shortpdc_parentcontractno='" & lscontractno & "'"
                    lssql &= " and pdc_chqno='" & dtentry.Rows(i).Item("Cheque No.").ToString & "' and chq_rec_slno=1 "

                    gfExecuteQry(lssql, gOdbcConn)
                End If
            End If
        Next

        lssql = ""
        lssql &= " UPDATE"
        lssql &= " chola_trn_tpdcfile"
        lssql &= " set"
        lssql &= " pdc_status_flag=pdc_status_flag|" & GCREENTRY & ","
        lssql &= " file_lock='N',lock_by=null "
        lssql &= " where 1=1"
        lssql &= " and pdc_shortpdc_parentcontractno='" & lscontractno & "'"
        lssql &= " and pdc_gnsarefno='" & lsgnsarefno & "'"
        lssql &= " and pdc_status_flag=" & GCNEW

        gfExecuteQry(lssql, gOdbcConn)

        LogTransaction(lsgnsarefno, GCREENTRY, gUserName)


        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("Are you sure you want to close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        lssql = ""
        lssql &= " UPDATE"
        lssql &= " chola_trn_tpdcfile"
        lssql &= " set"
        lssql &= " file_lock='N',lock_by=null"
        lssql &= " where 1=1"
        lssql &= " and pdc_shortpdc_parentcontractno='" & lscontractno & "'"

        gfExecuteQry(lssql, gOdbcConn)

        Me.Close()
    End Sub

    Private Sub txtchqamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqamount.KeyPress
        e.Handled = gfAmountEntryOnly(e, txtchqamount)
    End Sub

    Private Sub dgventry_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgventry.CellContentClick
        Try
            Select Case e.ColumnIndex
                Case Is > -1
                    If sender.Columns(e.ColumnIndex).Name = "Delete" Then
                        If Val(dtentry.Rows(e.RowIndex).Item(0).ToString) > 0 Then MsgBox("Access Denied!", MsgBoxStyle.Critical, gProjectName) : Exit Sub
                        dtentry.Rows(e.RowIndex).Delete()
                        dtentry.AcceptChanges()
                        FillGrid(dtentry, 1)
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub FillGrid(ByVal dt As DataTable, ByVal Chqcount As Integer)
        dgventry.DataSource = dt

        'If dtentry.Rows.Count = 1 Or Chqcount > 1 Then
        '    Dim dgButtonColumn As New DataGridViewButtonColumn
        '    dgButtonColumn.HeaderText = ""
        '    dgButtonColumn.UseColumnTextForButtonValue = True
        '    dgButtonColumn.Text = "Delete"
        '    dgButtonColumn.Name = "Delete"
        '    dgButtonColumn.ToolTipText = "Delete Row"
        '    dgButtonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        '    dgButtonColumn.FlatStyle = FlatStyle.System
        '    dgButtonColumn.DefaultCellStyle.BackColor = Color.Gray
        '    dgButtonColumn.DefaultCellStyle.ForeColor = Color.White
        '    dgventry.Columns.Add(dgButtonColumn)

        '    dgventry.Columns("Cheque No.").Width = dgventry.Width * 0.2
        '    dgventry.Columns("Cheque Date").Width = dgventry.Width * 0.2
        '    dgventry.Columns("Cheque Amount").Width = dgventry.Width * 0.1
        '    dgventry.Columns("Type").Width = dgventry.Width * 0.3
        'End If
    End Sub

    Private Sub cbotype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbotype.SelectedIndexChanged
        If cbotype.Text.Trim.ToUpper = "ECS-NORMAL" Then
            txtecsamount.Enabled = True
            txtecscount.Enabled = True
            txtchqamount.Enabled = False
            txtchqno.Enabled = False
            mstchqdate.Enabled = False
        Else
            txtecsamount.Enabled = False
            txtecscount.Enabled = False
            txtchqamount.Enabled = True
            txtchqno.Enabled = True
            mstchqdate.Enabled = True
        End If
    End Sub

    Private Sub dgventry_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgventry.CellDoubleClick
        If dgventry.RowCount = 0 Then Exit Sub
        If Val(dgventry.Rows(e.RowIndex).Cells("Gid").Value.ToString) > 0 Then MsgBox("Access Denied!", MsgBoxStyle.Critical, gProjectName) : Exit Sub
        cbotype.Text = dgventry.Rows(e.RowIndex).Cells("Type").Value
        txtchqno.Text = dgventry.Rows(e.RowIndex).Cells("Cheque No.").Value
        txtchqamount.Text = dgventry.Rows(e.RowIndex).Cells("Cheque Amount").Value
        mstchqdate.Text = dgventry.Rows(e.RowIndex).Cells("Cheque Date").Value
        cbopopnonpop.Text = dgventry.Rows(e.RowIndex).Cells("PAP/NONPAP").Value
        txtrowid.Text = e.RowIndex
        btnadd.Text = "Update"
    End Sub

    Private Sub txtchqno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub
End Class