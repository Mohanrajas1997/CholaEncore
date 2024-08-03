Public Class frmSwapListReport
    Dim lsCond As String = ""
    Dim msSql As String = ""

    Private Sub frmInwardReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dtpImportFrom.Value = Now
            dtpImportTo.Value = Now

            dtpImportFrom.Checked = False
            dtpImportTo.Checked = False

            With cboStatus
                .Items.Clear()
                .Items.Add("Retrieved")
                .Items.Add("Yet To Retrieve")
            End With

            dtpImportFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            lsCond = ""

            If dtpImportFrom.Checked = True Then lsCond &= " and f.import_date >='" & Format(dtpImportFrom.Value, "yyyy-MM-dd") & "'"
            If dtpImportTo.Checked = True Then lsCond &= " and f.import_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpImportTo.Value), "yyyy-MM-dd") & "'"

            If cboFile.SelectedIndex <> -1 Then lsCond &= " and f.swapfile_gid = " & Val(cboFile.SelectedValue.ToString)

            Select Case cboStatus.Text
                Case "Retrieved"
                    lsCond &= " and s.oldpacket_count > 0 "
                Case "Yet to Retrieve"
                    lsCond &= " and s.oldpacket_count = 0 "
            End Select

            If txtagreementno.Text.Trim <> "" Then lsCond &= " and (a.agreement_no = '" & QuoteFilter(txtagreementno.Text.Trim) & "' or a.shortagreement_no = '" & QuoteFilter(txtagreementno.Text.Trim) & "') "
            If txtgnsarefno.Text <> "" Then lsCond &= " and p.packet_gnsarefno ='" & txtgnsarefno.Text.Trim & "'"

            If lsCond = "" Then lsCond = " and 1 = 2 "

            msSql = ""
            msSql &= " select s.swap_date as 'Swap Date',s.shortagreement_no as 'Short Agreement No',a.agreement_no as 'AgreementNo',"

            If chkPkt.Checked = True Then
                msSql &= " p.packet_gnsarefno as 'GNSAREF',p.packet_mode as 'Mode',"
                msSql &= " e.almaraentry_cupboardno as 'Cupboard No',e.almaraentry_shelfno as 'Shelf No',e.almaraentry_boxno as 'Box No',p.packet_gid as 'Packet Id',"
            End If

            msSql &= " oldpacket_count as 'Old Packet Count',f.import_date as 'Import Date',f.import_by as 'Import By',"
            msSql &= " f.file_name as 'File Name',f.sheet_name as 'Sheet Name',f.swapfile_gid as 'Swap File Id',s.swap_gid as 'Swap Id',"
            msSql &= " s.agreement_gid as 'Agreement Id' "
            msSql &= " from chola_trn_tswapfile as f "
            msSql &= " left join chola_trn_tswap as s on s.swapfile_gid = f.swapfile_gid "
            msSql &= " left join chola_mst_tagreement as a on a.agreement_gid = s.agreement_gid "

            If txtgnsarefno.Text <> "" Or chkPkt.Checked = True Then
                msSql &= " left join chola_trn_toldswappacket as o on o.swap_gid = s.swap_gid "
                msSql &= " left join chola_trn_tpacket as p on p.packet_gid = o.oldpacket_gid "
                msSql &= " left join chola_trn_almaraentry as e on e.almaraentry_gid = p.packet_box_gid "
            End If

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
        msSql &= " select swapfile_gid,concat(file_name,'-',sheet_name) as file_name from chola_trn_tswapfile "
        msSql &= " where true "
        msSql &= IIf(dtpImportFrom.Checked = True, " and import_date >= '" & Format(dtpImportFrom.Value, "yyyy-MM-dd") & "' ", "")
        msSql &= IIf(dtpImportTo.Checked = True, " and import_date < '" & Format(DateAdd(DateInterval.Day, 1, dtpImportTo.Value), "yyyy-MM-dd") & "' ", "")

        Call gpBindCombo(msSql, "file_name", "swapfile_gid", cboFile, gOdbcConn)
    End Sub

    Private Sub cboFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFile.SelectedIndexChanged

    End Sub
End Class