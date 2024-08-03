Public Class frmpouchnotreceived
    Dim isload As Boolean = True

    Private Sub frmpouchnotreceived_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmpouchnotreceived_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        isload = True
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        isload = False
    End Sub

    Private Sub frmpouchnotreceived_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()
        Dim lssql As String

        lssql = " select inward_gid,inward_product as 'Product',inward_applicationid as 'ApplicationId',inward_applicationformno as 'Application Form No', "
        lssql &= " inward_branch as 'Branch',inward_agreementno as 'Agreement No',inward_shortagreementno as 'Short agreement No',"
        lssql &= " inward_customername as 'Customer Name',inward_paymode as 'Pay Mode',inward_pdc as 'PDC',inward_spdc as 'SPDC',"
        lssql &= " inward_mandate as 'Mandate',date_format(inward_lmsauthdate,'%d-%m-%Y') as 'LMS Auth Date',inward_tenure as 'Tenure',"
        lssql &= " date_format(inward_firstemidate,'%d-%m-%Y') as 'First EMI Date',inward_installmode as 'Install Mode',inward_dumpremarks as 'Remarks',"
        lssql &= " inward_compagr as 'Comp Agr',date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date' "
        lssql &= " from chola_trn_tinward"
        lssql &= " where inward_file_gid=" & Val(cbofilename.SelectedValue)

        If rbtnotreceived.Checked Then
            lssql &= " and inward_status & " & GCNOTRECEIVED & " > 0"
        ElseIf rbtpending.Checked Then
            lssql &= " and inward_status & " & GCNEWFILE & " > 0"
            lssql &= " and inward_status & " & GCNOTRECEIVED & " = 0"
            lssql &= " and inward_status & " & GCRECEIVED & " = 0"
            lssql &= " and inward_status & " & GCCOMBINED & " = 0"
        Else
            lssql &= " and inward_status & " & GCCOMBINED & " > 0"
        End If

        If txtShortAgreementNo.Text.Trim <> "" Then
            lssql &= " and inward_shortagreementno='" & txtShortAgreementNo.Text.Trim & "'"
        End If

        If txtAgreementNo.Text.Trim <> "" Then
            lssql &= " and inward_agreementno='" & txtAgreementNo.Text.Trim & "'"
        End If

        dgvsummary.Columns.Clear()
        gpPopGridView(dgvsummary, lssql, gOdbcConn)

        dgvsummary.Columns(0).Visible = False

        If rbtnotreceived.Checked Then
            Dim dgButtonColumn2 As New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "Pending"
            dgButtonColumn2.Name = "NRPending"
            dgButtonColumn2.ToolTipText = "Pending"
            dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn2.FlatStyle = FlatStyle.System
            dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn2)
        ElseIf rbtcombined.Checked Then
            Dim dgButtonColumn2 As New DataGridViewButtonColumn
            dgButtonColumn2.HeaderText = ""
            dgButtonColumn2.UseColumnTextForButtonValue = True
            dgButtonColumn2.Text = "Pending"
            dgButtonColumn2.Name = "CMBPending"
            dgButtonColumn2.ToolTipText = "Pending"
            dgButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn2.FlatStyle = FlatStyle.System
            dgButtonColumn2.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn2.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn2)
        Else
            Dim dgButtonColumn3 As New DataGridViewButtonColumn
            dgButtonColumn3.HeaderText = ""
            dgButtonColumn3.UseColumnTextForButtonValue = True
            dgButtonColumn3.Text = "Not Received"
            dgButtonColumn3.Name = "NotRcvd"
            dgButtonColumn3.ToolTipText = "Not Received"
            dgButtonColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn3.FlatStyle = FlatStyle.System
            dgButtonColumn3.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn3.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn3)

            Dim dgButtonColumn4 As New DataGridViewButtonColumn
            dgButtonColumn4.HeaderText = ""
            dgButtonColumn4.UseColumnTextForButtonValue = True
            dgButtonColumn4.Text = "Combined"
            dgButtonColumn4.Name = "Combined"
            dgButtonColumn4.ToolTipText = "Combined"
            dgButtonColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            dgButtonColumn4.FlatStyle = FlatStyle.System
            dgButtonColumn4.DefaultCellStyle.BackColor = Color.Gray
            dgButtonColumn4.DefaultCellStyle.ForeColor = Color.White
            dgvsummary.Columns.Add(dgButtonColumn4)
        End If

    End Sub
    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click

        If dtpimportdate.Checked = False Then
            MsgBox("Please Select Import Date", MsgBoxStyle.Critical, gProjectName)
            dtpimportdate.Focus()
            Exit Sub
        End If

        If cbofilename.Text = "" Then
            MsgBox("Please Select File Name", MsgBoxStyle.Critical, gProjectName)
            cbofilename.Focus()
            Exit Sub
        End If

        Call LoadData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpimportdate.Value = Now()
        dtpimportdate.Checked = False
        cbofilename.SelectedIndex = -1
        rbtpending.Checked = True
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick
        Dim dtInward As DataTable
        Dim lsqry As String
        Dim lsAgreementNo As String
        Dim lnInwardGid As Long
        Dim lsParentRcvd As String
        Dim lsRecvdDate As String = ""
        Dim lsinputdate As String

        If e.RowIndex < 0 Then
            Exit Sub
        End If

        Select Case e.ColumnIndex
            Case Is > -1
                If sender.Columns(e.ColumnIndex).Name = "NotRcvd" Then
                    If MsgBox("Are You Sure Want to Mark as Not Received", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    lsqry = ""
                    lsqry &= " update chola_trn_tinward set "
                    lsqry &= " inward_status=inward_status | " & GCNOTRECEIVED
                    lsqry &= " where inward_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)
                ElseIf sender.Columns(e.ColumnIndex).Name = "Combined" Then
                    If MsgBox("Are You Sure Want to Mark as Combined", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    Do
                        lsAgreementNo = InputBox("Enter Parent Agreement No", gProjectName)
                    Loop Until lsAgreementNo <> ""

                    lsqry = ""
                    lsqry &= " select inward_gid,inward_parent_gid,date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date' "
                    lsqry &= " from chola_trn_tinward "
                    lsqry &= " where inward_agreementno='" & lsAgreementNo & "'"
                    lsqry &= " and inward_status & " & GCINWARDENTRY & " > 0"
                    dtInward = GetDataTable(lsqry)

                    Select Case dtInward.Rows.Count
                        Case 1
                            If dtInward.Rows(0).Item("inward_gid").ToString = dtInward.Rows(0).Item("inward_parent_gid").ToString Then
                                lnInwardGid = Val(dtInward.Rows(0).Item("inward_gid").ToString)
                                lsParentRcvd = dtInward.Rows(0).Item("Received Date").ToString
                            Else
                                MsgBox("This is Combined Agreement No..!", MsgBoxStyle.Critical, gProjectName)
                                Exit Sub
                            End If
                        Case 0
                            MsgBox("Invalid Agreement No..!", MsgBoxStyle.Critical, gProjectName)
                            Exit Sub
                        Case Else
                            For i As Integer = 0 To dtInward.Rows.Count - 1
                                lsRecvdDate &= dtInward.Rows(i).Item("Received Date").ToString & ","
                            Next
                            MsgBox("More than one record found !", MsgBoxStyle.Critical, gProjectName)
                            Do
                                lsinputdate = InputBox("Enter One Received Date: " & lsRecvdDate, gProjectName)
                            Loop Until IsDate(lsinputdate) And InStr(lsRecvdDate, lsinputdate) > 0

                            lsqry = ""
                            lsqry &= " select inward_gid,inward_parent_gid,date_format(inward_receiveddate,'%d-%m-%Y') as 'Received Date' "
                            lsqry &= " from chola_trn_tinward "
                            lsqry &= " where inward_agreementno='" & lsAgreementNo & "'"
                            lsqry &= " and inward_status & " & GCINWARDENTRY & " > 0"
                            lsqry &= " and inward_receiveddate ='" & Format(CDate(lsinputdate), "yyyy-MM-dd") & "'"
                            dtInward = GetDataTable(lsqry)

                            If dtInward.Rows.Count > 0 Then
                                lnInwardGid = Val(dtInward.Rows(0).Item("inward_gid").ToString)
                                lsParentRcvd = dtInward.Rows(0).Item("Received Date").ToString
                            Else
                                MsgBox("Invalid Agreement No..!", MsgBoxStyle.Critical, gProjectName)
                                Exit Sub
                            End If
                    End Select

                    If CDate(lsParentRcvd) <> CDate(dgvsummary.Rows(e.RowIndex).Cells("Received Date").Value.ToString) Then
                        MsgBox("Access Denied..!", MsgBoxStyle.Critical, gProjectName)
                        Exit Sub
                    End If

                    lsqry = ""
                    lsqry &= " update chola_trn_tinward set "
                    lsqry &= " inward_status=inward_status | " & GCCOMBINED & ","
                    lsqry &= " inward_parent_gid=" & lnInwardGid
                    lsqry &= " where inward_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)
                ElseIf sender.Columns(e.ColumnIndex).Name = "NRPending" Then
                    If MsgBox("Are You Sure Want to Mark as Pending", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    lsqry = ""
                    lsqry &= " update chola_trn_tinward set "
                    lsqry &= " inward_status=(inward_status | " & GCNOTRECEIVED & " ) ^ " & GCNOTRECEIVED
                    lsqry &= " where inward_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)
                ElseIf sender.Columns(e.ColumnIndex).Name = "CMBPending" Then
                    If MsgBox("Are You Sure Want to Mark as Pending", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.No Then
                        Exit Sub
                    End If

                    lsqry = ""
                    lsqry &= " update chola_trn_tinward set "
                    lsqry &= " inward_parent_gid= inward_gid,"
                    lsqry &= " inward_status=(inward_status | " & GCCOMBINED & " ) ^ " & GCCOMBINED
                    lsqry &= " where inward_gid=" & dgvsummary.Rows(e.RowIndex).Cells(0).Value.ToString
                    gfInsertQry(lsqry, gOdbcConn)
                End If
        End Select
    End Sub

    Private Sub dtpimportdate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpimportdate.ValueChanged
        Dim lssql As String
        If dtpimportdate.Checked = False Then Exit Sub

        lssql = ""
        lssql &= " select distinct file_name,file_gid "
        lssql &= " from chola_mst_tfile "
        lssql &= " inner join chola_trn_tinward on inward_file_gid=file_gid "
        lssql &= " where date_format(inward_receiveddate,'%Y-%m-%d')='" & Format(dtpimportdate.Value, "yyyy-MM-dd") & "'"
        gpBindCombo(lssql, "file_name", "file_gid", cbofilename, gOdbcConn)

        If cbofilename.Items.Count = 0 Then
            cbofilename.SelectedValue = 0
        Else
            cbofilename.SelectedValue = -1
        End If
    End Sub
End Class