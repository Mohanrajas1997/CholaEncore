Public Class frmPostconvReport
    Dim lsCond As String = ""
    Dim msSql As String = ""

    Private Sub frmInwardReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dtpImportFrom.Value = Now
            dtpImportTo.Value = Now

            dtpImportFrom.Checked = False
            dtpImportTo.Checked = False

            dtpChqFrom.Value = Now
            dtpChqTo.Value = Now

            dtpChqFrom.Checked = False
            dtpChqTo.Checked = False

            With cboStatus
                .Items.Clear()
                .Items.Add("Posted")
                .Items.Add("Not Posted")
                .Items.Add("Disc")
            End With

            dtpImportFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            lsCond = ""

            If dtpImportFrom.Checked = True Then lsCond &= " and f.import_on >='" & Format(dtpImportFrom.Value, "yyyy-MM-dd") & "'"
            If dtpImportTo.Checked = True Then lsCond &= " and f.import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImportTo.Value), "yyyy-MM-dd") & "'"

            If cboFile.SelectedIndex <> -1 Then lsCond &= " and f.file_gid = " & Val(cboFile.SelectedValue.ToString)

            If dtpChqFrom.Checked = True Then lsCond &= " and p.finone_chqdate >='" & Format(dtpChqFrom.Value, "yyyy-MM-dd") & "'"
            If dtpChqTo.Checked = True Then lsCond &= " and p.finone_chqdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpChqTo.Value), "yyyy-MM-dd") & "'"

            If txtChqNo.Text <> "" Then lsCond &= " and p.finone_chqno like '" & QuoteFilter(txtChqNo.Text) & "%' "
            If Val(txtChqAmt.Text) > 0 Then lsCond &= " and p.finone_chqamount = " & Val(txtChqAmt.Text) & " "
            If txtAgrmntNo.Text <> "" Then lsCond &= " and p.finone_shortagreementno = '" & QuoteFilter(txtAgrmntNo.Text) & "' "

            Select Case cboStatus.Text
                Case "Posted"
                    lsCond &= " and p.finone_entrygid > 0 "
                Case "Not Posted"
                    lsCond &= " and p.finone_entrygid = 0 "
                Case "Disc"
                    lsCond &= " and p.finone_disc > 0 "
            End Select

            If lsCond = "" Then lsCond = " and 1 = 2 "

            msSql = ""
            msSql &= " select "
            msSql &= " p.finone_agreementno as 'Agreement No',"
            msSql &= " p.finone_shortagreementno as 'Short Agreement No',"
            msSql &= " p.finone_chqdate as 'Chq Date',"
            msSql &= " p.finone_chqno as 'Chq No',"
            msSql &= " p.finone_chqamount as 'Chq Amount',"
            msSql &= " p.finone_customername as 'Cust Name',"
            msSql &= " p.finone_bankdetails as 'Bank Name',"
            msSql &= " p.finone_micrcode as 'Micr Code',"
            msSql &= " p.finone_entrygid as 'Pdc Id',"
            msSql &= " p.finone_disc as 'Disc',"
            msSql &= " make_set(p.finone_disc,'Agreement','Duplicate','Chq No','Chq Date','Chq Amount','Details') as 'Disc Desc',"
            msSql &= " f.file_gid as 'File Id',"
            msSql &= " f.file_name as 'File Name',"
            msSql &= " f.file_sheetname as 'Sheet Name',"
            msSql &= " f.import_on as 'Import On',"
            msSql &= " f.import_by as 'Import By' "
            msSql &= " from chola_mst_tfile as f "
            msSql &= " inner join chola_trn_tfinone as p on p.finone_file_gid = f.file_gid "
            msSql &= " where true " & lsCond & " "

            Call gpPopGridView(dgView, msSql, gOdbcConn)

            dgView.AutoResizeColumns()

            lblRecCount.Text = "Record Count: " & dgView.RowCount
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

    Private Sub frmInwardReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            With pnlButtons
                .Top = 6
                .Left = 6
            End With

            With dgView
                .Top = pnlButtons.Top + pnlButtons.Height + 6
                .Left = pnlButtons.Left
                .Width = Me.Width - 20
                .Height = Me.Height - pnlButtons.Height - 90
                pnlDisplay.Top = pnlButtons.Top + pnlButtons.Height + dgView.Height + 15
                pnlDisplay.Left = .Left
                pnlDisplay.Width = .Width
                btnExport.Left = pnlDisplay.Width - (btnExport.Width + 10)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        dtpImportFrom.Checked = False
        dtpImportTo.Checked = False

        cboFile.Text = ""
        cboFile.SelectedIndex = -1

        cboStatus.Text = ""
        cboStatus.SelectedIndex = -1

        dgView.DataSource = Nothing
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If dgView.RowCount = 0 Then
                MsgBox("No Details to Export!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
            PrintDGViewXML(dgView, gsReportPath & "Inward Report.xls", "Inward Details")

            MsgBox(" Exported to Excel !!" & Chr(13) & _
                   " Saved Path : " & gsReportPath & "Report", MsgBoxStyle.Information, gProjectName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboFile_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFile.GotFocus
        msSql = ""
        msSql &= " select file_gid,concat(file_name,'-',file_sheetname) as file_name from chola_mst_tfile "
        msSql &= " where true "
        msSql &= IIf(dtpImportFrom.Checked = True, " and import_on >= '" & Format(dtpImportFrom.Value, "yyyy-MM-dd") & "' ", "")
        msSql &= IIf(dtpImportTo.Checked = True, " and import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpImportTo.Value), "yyyy-MM-dd") & "' ", "")

        Call gpBindCombo(msSql, "file_name", "file_gid", cboFile, gOdbcConn)
    End Sub

    Private Sub cboFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFile.SelectedIndexChanged

    End Sub
End Class