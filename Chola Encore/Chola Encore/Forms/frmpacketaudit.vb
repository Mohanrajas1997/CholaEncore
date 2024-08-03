Public Class frmpacketaudit
    Dim lssql As String
    Dim lnPacketGid As Long
    Public Sub New(ByVal PacketGid As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lnPacketGid = PacketGid
    End Sub

    Private Sub frmpacketaudit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillSPDC()
        FillPDC()
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

        lssql = ""
        lssql &= " select chqentry_gid,chqentry_iscts,chqentry_ismicr, agreement_no as 'Agreement No',shortagreement_no as 'ShortAgr No',packet_gnsarefno as 'GNSAREF#', "
        lssql &= " chqentry_chqno as 'Cheque No',chqentry_micrcode as 'MICR Code'"
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tspdcchqentry on chqentry_packet_gid=packet_gid "
        lssql &= " where packet_gid=" & lnPacketGid

        dtpacket = GetDataTable(lssql)

        dgvspdcchqentry.DataSource = dtpacket

        dgvspdcchqentry.Columns(0).Visible = False
        dgvspdcchqentry.Columns(1).Visible = False
        dgvspdcchqentry.Columns(2).Visible = False
        dgvspdcchqentry.Columns(3).Visible = False
        dgvspdcchqentry.Columns(4).Visible = False

        If dtpacket.Rows.Count = 0 Then Exit Sub

        If dgvpdcchqentry.RowCount = 0 Then
            lbltotal.Text = "Total:" & dtpacket.Rows.Count
            TBCChequeentry.SelectedTab = TPSPDC
        End If


        For i As Integer = 0 To dgvspdcchqentry.Columns.Count - 1
            dgvspdcchqentry.Columns(i).ReadOnly = True
        Next

        'Dim dgvtxtcolumn1 As New DataGridViewTextBoxColumn
        'dgvtxtcolumn1.Name = "Remarks"
        'dgvtxtcolumn1.HeaderText = "Remarks"
        'dgvspdcchqentry.Columns.Add(dgvtxtcolumn1)


        Dim dgvccolumn4 As New DataGridViewCheckBoxColumn
        dgvccolumn4.Name = "MICR"
        dgvccolumn4.HeaderText = "MICR"
        dgvspdcchqentry.Columns.Add(dgvccolumn4)

        Dim dgvccolumn5 As New DataGridViewCheckBoxColumn
        dgvccolumn5.Name = "NONMICR"
        dgvccolumn5.HeaderText = "NONMICR"
        dgvspdcchqentry.Columns.Add(dgvccolumn5)

        Dim dgvccolumn1 As New DataGridViewCheckBoxColumn
        dgvccolumn1.Name = "CTS"
        dgvccolumn1.HeaderText = "CTS"
        dgvspdcchqentry.Columns.Add(dgvccolumn1)

        Dim dgvccolumn2 As New DataGridViewCheckBoxColumn
        dgvccolumn2.Name = "NonCTS"
        dgvccolumn2.HeaderText = "Non CTS"
        dgvspdcchqentry.Columns.Add(dgvccolumn2)

        Dim dgvccolumn3 As New DataGridViewCheckBoxColumn
        dgvccolumn3.Name = "NA"
        dgvccolumn3.HeaderText = "NA"
        dgvspdcchqentry.Columns.Add(dgvccolumn3)

        For i As Integer = 0 To dgvspdcchqentry.RowCount - 1
            If dgvspdcchqentry.Rows(i).Cells("chqentry_iscts").Value.ToString = "Y" Then
                dgvspdcchqentry.Rows(i).Cells("CTS").Value = True
                dgvspdcchqentry.Rows(i).Cells("NonCTS").Value = False
                dgvspdcchqentry.Rows(i).Cells("NA").Value = False
            ElseIf dgvspdcchqentry.Rows(i).Cells("chqentry_iscts").Value.ToString = "N" Then
                dgvspdcchqentry.Rows(i).Cells("CTS").Value = False
                dgvspdcchqentry.Rows(i).Cells("NonCTS").Value = True
                dgvspdcchqentry.Rows(i).Cells("NA").Value = False
            Else
                dgvspdcchqentry.Rows(i).Cells("CTS").Value = False
                dgvspdcchqentry.Rows(i).Cells("NonCTS").Value = False
                dgvspdcchqentry.Rows(i).Cells("NA").Value = True
            End If

            If dgvspdcchqentry.Rows(i).Cells("chqentry_ismicr").Value.ToString = "Y" Then
                dgvspdcchqentry.Rows(i).Cells("MICR").Value = True
                dgvspdcchqentry.Rows(i).Cells("NONMICR").Value = False
            ElseIf dgvspdcchqentry.Rows(i).Cells("chqentry_ismicr").Value.ToString = "N" Then
                dgvspdcchqentry.Rows(i).Cells("MICR").Value = False
                dgvspdcchqentry.Rows(i).Cells("NONMICR").Value = True
            End If
        Next

        dgvspdcchqentry.Columns(9).Width = 65
        dgvspdcchqentry.Columns(10).Width = 65
        dgvspdcchqentry.Columns(11).Width = 65

    End Sub
    Private Sub FillPDC()
        Dim dtpacket As DataTable
        Dim drAgreement As Odbc.OdbcDataReader

        lssql = ""
        lssql &= " select entry_gid,chq_iscts,chq_ismicr, agreement_no as 'Agreement No',shortagreement_no as 'ShortAgr No',packet_gnsarefno as 'GNSAREF#', "
        lssql &= " chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',"
        lssql &= " chq_amount as 'Cheque Amount'"
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

        dgvpdcchqentry.DataSource = dtpacket


        lssql = ""
        lssql &= " select agreement_no as 'Agreement No',shortagreement_no as 'ShortAgr No',packet_gnsarefno as 'GNSAREF#' "
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " where packet_gid=" & lnPacketGid
        drAgreement = gfExecuteQry(lssql, gOdbcConn)

        If drAgreement.HasRows Then
            While drAgreement.Read
                lblshortagreementno.Text = drAgreement.Item("ShortAgr No")
                lblagreementno.Text = drAgreement.Item("Agreement No")
                lblpacketno.Text = drAgreement.Item("GNSAREF#")
            End While
        End If

        dgvpdcchqentry.Columns(0).Visible = False
        dgvpdcchqentry.Columns(1).Visible = False
        dgvpdcchqentry.Columns(2).Visible = False
        dgvpdcchqentry.Columns(3).Visible = False
        dgvpdcchqentry.Columns(4).Visible = False

        If dtpacket.Rows.Count = 0 Then Exit Sub

        lbltotal.Text = "Total:" & dtpacket.Rows.Count

        For i As Integer = 0 To dgvpdcchqentry.Columns.Count - 1
            dgvpdcchqentry.Columns(i).ReadOnly = True
        Next

        'Dim dgvtxtcolumn1 As New DataGridViewTextBoxColumn
        'dgvtxtcolumn1.Name = "Remarks"
        'dgvtxtcolumn1.HeaderText = "Remarks"
        'dgvpdcchqentry.Columns.Add(dgvtxtcolumn1)

        Dim dgvccolumn4 As New DataGridViewCheckBoxColumn
        dgvccolumn4.Name = "MICR"
        dgvccolumn4.HeaderText = "MICR"
        dgvpdcchqentry.Columns.Add(dgvccolumn4)

        Dim dgvccolumn5 As New DataGridViewCheckBoxColumn
        dgvccolumn5.Name = "NONMICR"
        dgvccolumn5.HeaderText = "NONMICR"
        dgvpdcchqentry.Columns.Add(dgvccolumn5)

        Dim dgvccolumn1 As New DataGridViewCheckBoxColumn
        dgvccolumn1.Name = "CTS"
        dgvccolumn1.HeaderText = "CTS"
        dgvpdcchqentry.Columns.Add(dgvccolumn1)

        Dim dgvccolumn2 As New DataGridViewCheckBoxColumn
        dgvccolumn2.Name = "NonCTS"
        dgvccolumn2.HeaderText = "Non CTS"
        dgvpdcchqentry.Columns.Add(dgvccolumn2)

        Dim dgvccolumn3 As New DataGridViewCheckBoxColumn
        dgvccolumn3.Name = "NA"
        dgvccolumn3.HeaderText = "NA"
        dgvpdcchqentry.Columns.Add(dgvccolumn3)

        For i As Integer = 0 To dgvpdcchqentry.RowCount - 1
            If dgvpdcchqentry.Rows(i).Cells("chq_iscts").Value.ToString = "Y" Then
                dgvpdcchqentry.Rows(i).Cells("CTS").Value = True
                dgvpdcchqentry.Rows(i).Cells("NonCTS").Value = False
                dgvpdcchqentry.Rows(i).Cells("NA").Value = False
            ElseIf dgvpdcchqentry.Rows(i).Cells("chq_iscts").Value.ToString = "N" Then
                dgvpdcchqentry.Rows(i).Cells("CTS").Value = False
                dgvpdcchqentry.Rows(i).Cells("NonCTS").Value = True
                dgvpdcchqentry.Rows(i).Cells("NA").Value = False
            Else
                dgvpdcchqentry.Rows(i).Cells("CTS").Value = False
                dgvpdcchqentry.Rows(i).Cells("NonCTS").Value = False
                dgvpdcchqentry.Rows(i).Cells("NA").Value = True
            End If

            If dgvpdcchqentry.Rows(i).Cells("chq_ismicr").Value.ToString = "Y" Then
                dgvpdcchqentry.Rows(i).Cells("MICR").Value = True
                dgvpdcchqentry.Rows(i).Cells("NONMICR").Value = False
            ElseIf dgvpdcchqentry.Rows(i).Cells("chq_ismicr").Value.ToString = "N" Then
                dgvpdcchqentry.Rows(i).Cells("MICR").Value = False
                dgvpdcchqentry.Rows(i).Cells("NONMICR").Value = True
            End If
        Next

        dgvpdcchqentry.Columns(6).Width = 65
        dgvpdcchqentry.Columns(7).Width = 65
        dgvpdcchqentry.Columns(8).Width = 65
        dgvpdcchqentry.Columns(9).Width = 65
        dgvpdcchqentry.Columns(10).Width = 65
        dgvpdcchqentry.Columns(11).Width = 65
        dgvpdcchqentry.Columns(12).Width = 65
    End Sub

    Private Sub dgvchqentry_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvpdcchqentry.CellContentClick
        If e.ColumnIndex < 0 Then
            Exit Sub
        End If

        Select Case dgvpdcchqentry.Columns(e.ColumnIndex).Name
            Case "CTS"
                dgvpdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = True
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = False
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NA").Value = False
            Case "NonCTS"
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = True
                dgvpdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = False
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NA").Value = False
            Case "NA"
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NA").Value = True
                dgvpdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = False
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = False
            Case "MICR"
                dgvpdcchqentry.Rows(e.RowIndex).Cells("MICR").Value = True
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NONMICR").Value = False
            Case "NONMICR"
                dgvpdcchqentry.Rows(e.RowIndex).Cells("MICR").Value = False
                dgvpdcchqentry.Rows(e.RowIndex).Cells("NONMICR").Value = True
        End Select

    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnChequeGid As Long
        Dim lsRemarks As String = ""
        Dim lsiscts As String = ""
        Dim lsisMICR As String = ""
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

        If rbtnno.Checked Then
            lsisMultiple = "'N'"
        ElseIf rbtnyes.Checked Then
            lsisMultiple = "'Y'"
        Else
            lsisMultiple = "null"
        End If


        'PDC
        For i As Integer = 0 To dgvpdcchqentry.Rows.Count - 1
            If (dgvpdcchqentry.Rows(i).Cells("CTS").Value = False And _
                dgvpdcchqentry.Rows(i).Cells("NonCTS").Value = False And _
                dgvpdcchqentry.Rows(i).Cells("NA").Value = False) Or _
                 (dgvpdcchqentry.Rows(i).Cells("MICR").Value = False And _
                  dgvpdcchqentry.Rows(i).Cells("NONMICR").Value = False) Then

                MsgBox("Please Audit Cheque No." & dgvpdcchqentry.Rows(i).Cells("Cheque No").Value.ToString, MsgBoxStyle.Critical, gProjectName)
                dgvpdcchqentry.Rows(i).Selected = True
                TBCChequeentry.SelectedTab = TPPDC
                Exit Sub
            End If
        Next

        'SPDC
        For i As Integer = 0 To dgvspdcchqentry.Rows.Count - 1
            If (dgvspdcchqentry.Rows(i).Cells("CTS").Value = False And _
                dgvspdcchqentry.Rows(i).Cells("NonCTS").Value = False And _
                dgvspdcchqentry.Rows(i).Cells("NA").Value = False) Or _
               (dgvspdcchqentry.Rows(i).Cells("MICR").Value = False And _
                  dgvspdcchqentry.Rows(i).Cells("NONMICR").Value = False) Then

                MsgBox("Please Audit Cheque No." & dgvspdcchqentry.Rows(i).Cells("Cheque No").Value.ToString, MsgBoxStyle.Critical, gProjectName)
                dgvspdcchqentry.Rows(i).Selected = True
                TBCChequeentry.SelectedTab = TPSPDC
                Exit Sub
            End If
        Next

        'PDC
        For i As Integer = 0 To dgvpdcchqentry.Rows.Count - 1
            'Chq Gid
            lnChequeGid = dgvpdcchqentry.Rows(i).Cells("entry_gid").Value.ToString

            'If dgvpdcchqentry.Rows(i).Cells("Remarks").Value = Nothing Then
            '    lsRemarks = ""
            'Else
            '    lsRemarks = dgvpdcchqentry.Rows(i).Cells("Remarks").Value.ToString
            'End If

            'Cheque Status
            If dgvpdcchqentry.Rows(i).Cells("CTS").Value Then
                lsiscts = "'Y'"
            ElseIf dgvpdcchqentry.Rows(i).Cells("NonCTS").Value Then
                lsiscts = "'N'"
            Else
                lsiscts = "null"
            End If

            'Is MICR
            If dgvpdcchqentry.Rows(i).Cells("MICR").Value Then
                lsisMICR = "'Y'"
            ElseIf dgvpdcchqentry.Rows(i).Cells("NONMICR").Value Then
                lsisMICR = "'N'"
            Else
                lsisMICR = "null"
            End If

            lssql = ""
            lssql &= " update chola_trn_tpdcentry set "
            lssql &= " chq_iscts=" & lsiscts & ","
            lssql &= " chq_ismicr=" & lsisMICR
            If lsRemarks <> "" Then
                lssql &= " ,chq_desc=concat(chq_desc,',','" & lsRemarks & "')"
            End If
            lssql &= " where entry_gid=" & lnChequeGid
            gfInsertQry(lssql, gOdbcConn)

            LogCTSAudit(lnChequeGid, "PDC", lsiscts, lsisMICR)
        Next


        liNonCTS = Val(txtspdccount.Text) - Val(txtctscount.Text)

        If liNonCTS < 0 Then
            MsgBox("Invalid CTS Count..!", MsgBoxStyle.Critical, gProjectName)
            txtctscount.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To dgvspdcchqentry.Rows.Count - 1
            If dgvspdcchqentry.Rows(i).Cells("NonCTS").Value Then
                liEntryNonCTS += 1
            End If
        Next

        If liNonCTS <> liEntryNonCTS Then
            MsgBox("Cheque Details Mismatch..!", MsgBoxStyle.Critical, gProjectName)
            txtctscount.Focus()
            Exit Sub
        End If

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
            'Chq Gid
            lnChequeGid = dgvspdcchqentry.Rows(i).Cells("chqentry_gid").Value.ToString

            'If dgvspdcchqentry.Rows(i).Cells("Remarks").Value = Nothing Then
            '    lsRemarks = ""
            'Else
            '    lsRemarks = dgvspdcchqentry.Rows(i).Cells("Remarks").Value.ToString
            'End If

            'Cheque Status
            If dgvspdcchqentry.Rows(i).Cells("CTS").Value Then
                lsiscts = "'Y'"
            ElseIf dgvspdcchqentry.Rows(i).Cells("NonCTS").Value Then
                lsiscts = "'N'"
            Else
                lsiscts = "null"
            End If

            'Is MICR
            If dgvspdcchqentry.Rows(i).Cells("MICR").Value Then
                lsisMICR = "'Y'"
            ElseIf dgvspdcchqentry.Rows(i).Cells("NONMICR").Value Then
                lsisMICR = "'N'"
            Else
                lsisMICR = "null"
            End If

            lssql = ""
            lssql &= " update chola_trn_tspdcchqentry set "
            lssql &= " chqentry_iscts=" & lsiscts & ","
            lssql &= " chqentry_ismicr=" & lsisMICR
            If lsRemarks <> "" Then
                lssql &= " ,chqentry_remarks='" & lsRemarks & "'"
            End If
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
        Dim frmchqentry As New frmchequeentry(lnPacketGid, "", "")
        frmchqentry.ShowDialog()
        FillSPDC()
    End Sub

    Private Sub dgvspdcchqentry_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvspdcchqentry.CellContentClick
        If e.ColumnIndex < 0 Then
            Exit Sub
        End If

        Select Case dgvspdcchqentry.Columns(e.ColumnIndex).Name
            Case "CTS"
                dgvspdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = True
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = False
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NA").Value = False
            Case "NonCTS"
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = True
                dgvspdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = False
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NA").Value = False
            Case "NA"
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NA").Value = True
                dgvspdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = False
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = False
            Case "MICR"
                dgvspdcchqentry.Rows(e.RowIndex).Cells("MICR").Value = True
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NONMICR").Value = False
            Case "NONMICR"
                dgvspdcchqentry.Rows(e.RowIndex).Cells("MICR").Value = False
                dgvspdcchqentry.Rows(e.RowIndex).Cells("NONMICR").Value = True
        End Select
    End Sub

    Private Sub dgvspdcchqentry_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvspdcchqentry.CellFormatting
        'If dgvspdcchqentry.Rows(e.RowIndex).Cells("chqentry_iscts").Value.ToString = "Y" Then
        '    dgvspdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = True
        '    dgvspdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = False
        '    dgvspdcchqentry.Rows(e.RowIndex).Cells("NA").Value = False
        'ElseIf dgvspdcchqentry.Rows(e.RowIndex).Cells("chqentry_iscts").Value.ToString = "N" Then
        '    dgvspdcchqentry.Rows(e.RowIndex).Cells("CTS").Value = False
        '    dgvspdcchqentry.Rows(e.RowIndex).Cells("NonCTS").Value = True
        '    dgvspdcchqentry.Rows(e.RowIndex).Cells("NA").Value = False
        'End If
    End Sub

    Private Sub TBCChequeentry_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TBCChequeentry.SelectedIndexChanged
        If TBCChequeentry.SelectedIndex = 0 Then
            lbltotal.Text = "Total:" & dgvpdcchqentry.RowCount
            chkallcts.Visible = True
            chkallnoncts.Visible = True
            chkmicrall.Visible = True
            chknonmicrall.Visible = True
            lblselectall.Visible = True
        Else
            lbltotal.Text = "Total:" & dgvspdcchqentry.RowCount
            chkallcts.Visible = False
            chkallnoncts.Visible = False
            chkmicrall.Visible = False
            chknonmicrall.Visible = False
            lblselectall.Visible = False
        End If
    End Sub

    Private Sub chkallcts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkallcts.CheckedChanged
        For i As Integer = 0 To dgvpdcchqentry.RowCount - 1
            dgvpdcchqentry.Rows(i).Cells("CTS").Value = chkallcts.Checked
            dgvpdcchqentry.Rows(i).Cells("NonCTS").Value = False
            dgvpdcchqentry.Rows(i).Cells("NA").Value = False
        Next
        chkallnoncts.Checked = False
    End Sub

    Private Sub chkallnoncts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkallnoncts.CheckedChanged
        For i As Integer = 0 To dgvpdcchqentry.RowCount - 1
            dgvpdcchqentry.Rows(i).Cells("CTS").Value = False
            dgvpdcchqentry.Rows(i).Cells("NonCTS").Value = chkallnoncts.Checked
            dgvpdcchqentry.Rows(i).Cells("NA").Value = False
        Next
        chkallcts.Checked = False
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

    Private Sub chkmicrall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkmicrall.CheckedChanged
        For i As Integer = 0 To dgvpdcchqentry.RowCount - 1
            dgvpdcchqentry.Rows(i).Cells("MICR").Value = chkmicrall.Checked
            dgvpdcchqentry.Rows(i).Cells("NONMICR").Value = False
        Next
        chknonmicrall.Checked = False
    End Sub

    Private Sub chknonmicrall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chknonmicrall.CheckedChanged
        For i As Integer = 0 To dgvpdcchqentry.RowCount - 1
            dgvpdcchqentry.Rows(i).Cells("NONMICR").Value = chknonmicrall.Checked
            dgvpdcchqentry.Rows(i).Cells("MICR").Value = False
        Next
        chkmicrall.Checked = False
    End Sub
End Class