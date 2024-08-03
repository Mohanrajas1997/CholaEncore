Public Class frmPdcSpdcPulloutEntry
    Dim lssql As String
    Dim lnentrygid As Long
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal EntryGid As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lnentrygid = EntryGid
    End Sub
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        cboagreement.Text = ""
        txtchqno.Text = ""
        cboreason.SelectedIndex = -1
        cboagreement.DataSource = Nothing
        cboagreement.SelectedIndex = -1
    End Sub
    Private Sub frmpulloutentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmpulloutentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim drpdcentry As Odbc.OdbcDataReader

        lssql = " select reason_gid,reason_name from chola_mst_tpulloutreason where reason_deleteflag='N' "
        gpBindCombo(lssql, "reason_name", "reason_gid", cboreason, gOdbcConn)
        cboreason.SelectedIndex = -1

        If lnentrygid > 0 Then
            lssql = " select chq_no,chq_amount,chq_date "
            lssql &= " from chola_trn_tpdcentry "
            lssql &= " where entry_gid=" & lnentrygid

            drpdcentry = gfExecuteQry(lssql, gOdbcConn)

            If drpdcentry.HasRows Then
                txtchqno.Text = drpdcentry.Item("chq_no")
                txtchequeamt_LostFocus(sender, e)
                cboagreement.SelectedIndex = 0
                cboreason.Focus()
            End If
        End If
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnStatus As Integer
        Dim lnTempStatus As Integer
        Dim lnValidStatus As Integer
        Dim lnPdcId As Long

        Dim ds As New DataSet

        If txtchqno.Text.Trim = "" Then
            MsgBox("please enter Cheque No", MsgBoxStyle.Critical)
            txtchqno.Focus()
            Exit Sub
        End If

        If cboagreement.Text.Trim = "" Then
            MsgBox("please enter Agreementno", MsgBoxStyle.Critical)
            cboagreement.Focus()
            Exit Sub
        End If

        If cboreason.Text = "" Then
            MsgBox("please Select Pullout Reason", MsgBoxStyle.Critical)
            cboreason.Focus()
            Exit Sub
        End If

        lssql = " select * from chola_trn_tpdcentry "
        lssql &= " where 1 = 1 "
        lssql &= " and chq_agreement_gid=" & cboagreement.SelectedValue
        lssql &= " and chq_no = '" & txtchqno.Text.Trim & "' "
        lssql &= " and chq_type = 2 "

        Call gpDataSet(lssql, "pdc", gOdbcConn, ds)

        With ds.Tables("pdc")
            Select Case .Rows.Count
                Case 1
                    lnStatus = .Rows(0).Item("chq_status")
                    lnPdcId = .Rows(0).Item("entry_gid")

                    ' Check pullout or despatch
                    lnValidStatus = GCPULLOUT Or GCDESPATCH
                    lnTempStatus = lnStatus And lnValidStatus

                    If lnTempStatus Then MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName)

                    lssql = " update chola_trn_tpdcentry set "
                    lssql &= " chq_status = chq_status | " & GCPULLOUT & ","
                    lssql &= " chq_batch_gid = 0 "
                    lssql &= " where entry_gid=" & lnPdcId & ""

                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " insert into chola_trn_tpullout (pullout_shortagreementno,pullout_chqno,"
                    lssql &= "pullout_reasongid,pullout_entrygid,pullout_insertdate,pullout_insertby) "
                    lssql &= " values ("
                    lssql &= "'" & cboagreement.Text.Trim & "',"
                    lssql &= "'" & txtchqno.Text.Trim & "',"
                    lssql &= "" & cboreason.SelectedValue & ","
                    lssql &= "" & lnPdcId & ","
                    lssql &= " sysdate(),'" & gUserName & "')"
                    gfInsertQry(lssql, gOdbcConn)

                    MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)
                Case 0
                    MsgBox("Invalid record !", MsgBoxStyle.Critical, gProjectName)
                Case Else
                    MsgBox("More than one record found !", MsgBoxStyle.Critical, gProjectName)
            End Select

            .Rows.Clear()
        End With
        If lnentrygid > 0 Then
            Me.Close()
            Exit Sub
        End If
        btnclear.PerformClick()
        txtchqno.Focus()
    End Sub

    Private Sub txtchequeamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub cboagreement_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboagreement.SelectedIndexChanged

    End Sub

    Private Sub txtchqno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchqno.LostFocus
        If txtchqno.Text.Trim = "" Then
            cboagreement.DataSource = Nothing
            Exit Sub
        Else
            lssql = " select agreement_gid,concat(shortagreement_no,'-',agreement_no) as agreement_no "
            lssql &= " from chola_trn_tpdcentry "
            lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid"
            lssql &= " where chq_no='" & txtchqno.Text.Trim & "'"
            lssql &= " and chq_type=2"
            gpBindCombo(lssql, "agreement_no", "agreement_gid", cboagreement, gOdbcConn)
            cboagreement.SelectedIndex = -1
        End If
    End Sub

    Private Sub txtchqno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtchqno.TextChanged

    End Sub
End Class