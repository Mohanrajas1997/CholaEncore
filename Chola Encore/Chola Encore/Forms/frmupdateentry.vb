Public Class frmupdateentry
    Dim lsentrygid As String
    Dim lssql As String
    Public Sub New(ByVal EntryGid As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lsentrygid = EntryGid
    End Sub
    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtamount.Text = ""
        txtchqno.Text = ""
        mtxtdate.Text = ""
        cbotype.SelectedIndex = -1
        txtchqno.Focus()
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnbatchid As Long
        Dim ldoldamt As Double
        Dim ldnewamt As Double
        Dim ldamtdiff As Double
        Dim lnchqtype As Long
        Dim lspap As String
        Dim lschqdisc As String
        Dim lscts As String

        If cbotype.Text.Trim = "" Then
            MsgBox("Please Select Cheque Type", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        If txtchqno.Text.Trim = "" Then
            MsgBox("Please enter Cheque No", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        If cbotype.Text <> "EXTERNAL-SECURITY" Then
            If Not IsDate(mtxtdate.Text) Then
                MsgBox("Please enter Valid Cheque Date", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If

            If Val(txtamount.Text) = 0 Then
                MsgBox("Please enter Cheque Amount", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
        Else
            If Val(txtamount.Text) > 0 Then
                MsgBox("Amount Should be Zero for security Cheque", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
            If IsDate(mtxtdate.Text.Trim) Then
                MsgBox("Date Should be Null for security Cheque", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
        End If

        If cbopopnonpop.Text = "" Then
            MsgBox("Please Select PAP/Non PAP..!", MsgBoxStyle.Critical, gProjectName)
            cbopopnonpop.Focus()
            Exit Sub
        ElseIf cbopopnonpop.Text = "PAP" Then
            lspap = "Y"
        Else
            lspap = "N"
        End If

        If rbtncts.Checked Then
            lschqdisc = "(chq_disc_value|" & GCNONCTS & ") ^ " & GCNONCTS
            lscts = "Y"
        Else
            lschqdisc = "chq_disc_value|" & GCNONCTS
            lscts = "N"
        End If

        Dim drentry As Odbc.OdbcDataReader
        lssql = " select chq_batch_gid,chq_no,chq_amount,shortagreement_no,agreement_gid "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " where entry_gid = " & lsentrygid
        lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 "
        lssql &= " and chq_status & " & GCPULLOUT & " = 0 "
        lssql &= " and chq_status & " & GCDESPATCH & " = 0 "

        drentry = gfExecuteQry(lssql, gOdbcConn)

        If drentry.HasRows Then
            While drentry.Read

                lnbatchid = drentry.Item("chq_batch_gid").ToString
                ldoldamt = Val(drentry.Item("chq_amount").ToString)
                ldnewamt = Val(txtamount.Text.Trim)

                ldamtdiff = ldnewamt - ldoldamt

                If cbotype.Text = "EXTERNAL-NORMAL" Then
                    lnchqtype = GCEXTERNALNORMAL
                ElseIf cbotype.Text = "EXTERNAL-SECURITY" Then
                    lnchqtype = GCEXTERNALSECURITY
                End If

                If lnbatchid > 0 Then
                    lssql = " update chola_trn_tpdcentry set "
                    lssql &= " chq_no='" & txtchqno.Text & "',"
                    lssql &= " chq_amount=" & Val(txtamount.Text) & ","
                    lssql &= " chq_papflag='" & lspap & "',"
                    lssql &= " chq_iscts='" & lscts & "',"
                    lssql &= " chq_disc_value=" & lschqdisc
                    lssql &= " where entry_gid = " & lsentrygid
                    gfInsertQry(lssql, gOdbcConn)

                    lssql = ""
                    lssql &= " update chola_trn_tbatch "
                    lssql &= " set batch_totalchqamt = batch_totalchqamt + " & ldamtdiff
                    lssql &= " where batch_deleteflag='N' "
                    lssql &= " and batch_gid=" & lnbatchid
                    gfInsertQry(lssql, gOdbcConn)
                Else
                    lssql = " update chola_trn_tpdcentry set "
                    lssql &= " chq_no='" & txtchqno.Text & "',"

                    If IsDate(mtxtdate.Text.Trim) Then
                        lssql &= " chq_date='" & Format(CDate(mtxtdate.Text.Trim), "yyyy-MM-dd") & "',"
                    Else
                        lssql &= " chq_date=null,"
                    End If
                    lssql &= " chq_iscts='" & lscts & "',"
                    lssql &= " chq_type=" & lnchqtype & ","
                    lssql &= " chq_amount=" & Val(txtamount.Text) & ","
                    lssql &= " chq_papflag='" & lspap & "',"
                    lssql &= " chq_disc_value=" & lschqdisc
                    lssql &= " where entry_gid = " & lsentrygid
                    gfInsertQry(lssql, gOdbcConn)
                End If

            End While
        Else
            MsgBox("Update Failed", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub frmupdateentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmupdateentry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim drentry As Odbc.OdbcDataReader
        lssql = " select chq_no,date_format(chq_date,'%d-%m-%Y') as chq_date,chq_iscts,chq_papflag,chq_type,chq_amount,agreement_no,shortagreement_no,agreement_gid "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " where entry_gid = " & lsentrygid

        drentry = gfExecuteQry(lssql, gOdbcConn)

        If drentry.HasRows Then
            While drentry.Read
                lblchqno.Text = drentry.Item("chq_no").ToString()
                lblagreement.Text = drentry.Item("agreement_no").ToString()
                lblamount.Text = drentry.Item("chq_amount").ToString()
                txtagreementid.Text = drentry.Item("agreement_gid").ToString()
                txtchqno.Text = drentry.Item("chq_no").ToString()
                txtamount.Text = drentry.Item("chq_amount").ToString()
                mtxtdate.Text = drentry.Item("chq_date").ToString()
                If drentry.Item("chq_papflag").ToString() = "Y" Then
                    cbopopnonpop.SelectedIndex = 0
                Else
                    cbopopnonpop.SelectedIndex = 1
                End If
                cbotype.SelectedIndex = drentry.Item("chq_type").ToString()
                If drentry.Item("chq_iscts").ToString() = "Y" Then
                    rbtncts.Checked = True
                Else
                    rbtncts.Checked = False
                End If
            End While
            txtchqno.Focus()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub txtamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtamount.KeyPress
        e.Handled = gfAmtEntryOnly(e)
    End Sub
End Class