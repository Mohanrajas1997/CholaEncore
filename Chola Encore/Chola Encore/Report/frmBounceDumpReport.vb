Public Class frmBounceDumpReport
    Private Sub frmPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lsSql As String

        Try
            dtpChqFrom.Value = Now
            dtpChqTo.Value = Now

            dtpChqFrom.Checked = False
            dtpChqTo.Checked = False

            dtpImpFrom.Value = Now
            dtpImpTo.Value = Now

            dtpImpFrom.Checked = False
            dtpImpTo.Checked = False

            dtpRtnFrom.Value = Now
            dtpRtnTo.Value = Now

            dtpRtnFrom.Checked = False
            dtpRtnTo.Checked = False

            lsSql = ""
            lsSql &= " select * from chola_mst_tbouncereason "
            lsSql &= " where true "
            lsSql &= " order by reason_name "

            Call gpBindCombo(lsSql, "reason_name", "reason_gid", cboReason, gOdbcConn)

            With cboStatus
                .Items.Clear()
                .Items.Add("Received")
                .Items.Add("Not Received")
            End With

            dtpChqFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim lsSql As String
        Dim lsCond As String

        Try
            lsCond = ""

            If dtpImpFrom.Checked = True Then lsCond &= " and f.import_on >='" & Format(dtpImpFrom.Value, "yyyy-MM-dd") & "'"
            If dtpImpTo.Checked = True Then lsCond &= " and f.import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImpTo.Value), "yyyy-MM-dd") & "'"

            If dtpChqFrom.Checked = True Then lsCond &= " and b.bounce_chqdate >='" & Format(dtpChqFrom.Value, "yyyy-MM-dd") & "'"
            If dtpChqTo.Checked = True Then lsCond &= " and b.bounce_chqdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpChqTo.Value), "yyyy-MM-dd") & "'"

            If dtpRtnFrom.Checked = True Then lsCond &= " and b.bounce_returndate >='" & Format(dtpRtnFrom.Value, "yyyy-MM-dd") & "'"
            If dtpRtnTo.Checked = True Then lsCond &= " and b.bounce_returndate < '" & Format(DateAdd(DateInterval.Day, 1, dtpRtnTo.Value), "yyyy-MM-dd") & "'"

            If cboFile.SelectedIndex <> -1 Then lsCond &= " and f.file_gid = " & cboFile.SelectedValue
            If cboReason.SelectedIndex <> -1 Then lsCond &= " and r.reason_gid = '" & QuoteFilter(cboReason.SelectedValue) & "'"

            If txtChqNo.Text.Trim <> "" Then
                lsCond &= " and b.bounce_chqno like '" & QuoteFilter(txtChqNo.Text) & "%'"
            End If

            If txtChqAmt.Text.Trim <> "" Then
                lsCond &= " and b.bounce_chqamount =" & Val(txtChqAmt.Text)
            End If

            If txtAWBNo.Text <> "" Then lsCond &= " and b.bounce_awbno like '" & QuoteFilter(txtAWBNo.Text) & "%' "
            If txtAgmtNo.Text.Trim <> "" Then lsCond &= " and b.bounce_agreementno like '" & QuoteFilter(txtAgmtNo.Text) & "%'"
            If txtBounceId.Text <> "" Then lsCond &= " and b.bounce_gid = " & Val(txtBounceId.Text) & " "

            Select Case cboStatus.Text.ToUpper
                Case "RECEIVED"
                    lsCond &= " and e.bounceentry_bounce_gid > 0 "
                Case "NOT RECEIVED"
                    lsCond &= " and e.bounceentry_bounce_gid is null "
            End Select

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select b.bounce_returndate as 'Return Date', "
            lsSql &= " b.bounce_chqno as 'Chq No',b.bounce_chqdate as 'Chq Date',b.bounce_chqamount as 'Chq Amount',"
            lsSql &= " b.bounce_agreementno as 'Agreement No',r.reason_name,b.bounce_awbno as 'AWB No',"
            lsSql &= " f.import_on as 'Import Date', f.file_name as 'File Name',f.file_sheetname as 'Sheet Name',"
            lsSql &= " f.import_by as 'Import By',f.file_gid as 'File Id',b.bounce_gid as 'Bounce Id',e.bounceentry_gid as 'Bounce Entry Id' "
            lsSql &= " from chola_trn_tbounce as b "
            lsSql &= " left join chola_mst_tfile as f on f.file_gid = b.bounce_file_gid "
            lsSql &= " left join chola_mst_tbouncereason as r on r.reason_gid = b.bounce_reason_gid "
            lsSql &= " left join chola_trn_tbounceentry as e on e.bounceentry_bounce_gid = b.bounce_gid "
            lsSql &= " where true " & lsCond & " "

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            dgvRpt.AutoResizeColumns()

            lblRecCount.Text = "Record Count: " & dgvRpt.RowCount
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        MyBase.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmPaymentReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            With dgvRpt
                .Width = Me.Width - 30
                .Height = Me.Height - pnlButtons.Height - 90
                pnlDisplay.Width = Me.Width - 30
                pnlDisplay.Top = pnlButtons.Top + pnlButtons.Height + dgvRpt.Height + 15
                btnExport.Left = pnlDisplay.Width - (btnExport.Width + 10)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dtpImpFrom.Checked = False
        dtpImpTo.Checked = False
        dtpChqFrom.Checked = False
        dtpChqTo.Checked = False

        cboFile.Text = ""
        cboFile.SelectedIndex = -1

        cboReason.Text = ""
        cboReason.SelectedIndex = -1

        Call frmCtrClear(Me)

        dgvRpt.DataSource = Nothing
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If dgvRpt.RowCount = 0 Then
                MsgBox("No Details to Export!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
            PrintDGViewXML(dgvRpt, gsReportPath & "Payment Report.xls", "Payment Details")

            MsgBox(" Exported to Excel !!" & Chr(13) & _
                   " Saved Path : " & gsReportPath & "Payment Report", MsgBoxStyle.Information, gProjectName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtChqNo_keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        If gfIntEntryOnly(e) Then e.Handled = True
    End Sub

    Private Sub txtChqAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChqAmt.KeyPress
        If gfAmtEntryOnly(e) Then e.Handled = True
    End Sub

    Private Sub cboFile_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFile.GotFocus
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select file_gid,concat(file_name,'-',file_sheetname) as file_name from chola_mst_tfile "
        lsSql &= " where true "
        lsSql &= IIf(dtpImpFrom.Checked = True, " and import_on >= '" & Format(dtpImpFrom.Value, "yyyy-MM-dd") & "' ", "")
        lsSql &= IIf(dtpImpTo.Checked, " and import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImpTo.Value), "yyyy-MM-dd") & "' ", "")
        lsSql &= " order by file_gid"

        Call gpBindCombo(lsSql, "file_name", "file_gid", cboFile, gOdbcConn)
    End Sub

    Private Sub cboFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFile.SelectedIndexChanged

    End Sub
End Class