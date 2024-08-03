Public Class frmBounceInwardMis
    Dim lsCond As String = ""

    Private Sub frmPacketReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dtpFrom.Value = Now
            dtpTo.Value = Now

            dtpFrom.Checked = False
            dtpTo.Checked = False

            With cboMisBy
                .Items.Clear()
                .Items.Add("Return")
                .Items.Add("Cheque")
            End With

            dtpFrom.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, gProjectName)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim i As Integer
        Dim lsSql As String
        Dim lsFld As String = ""
        Dim lsFldDesc As String = ""

        Try
            lsCond = ""

            Select Case cboMisBy.Text
                Case "Return"
                    lsFld = "e.bounce_returndate"
                    lsFldDesc = "Return Date"
                Case "Cheque"
                    lsFld = "e.bounce_chqdate"
                    lsFldDesc = "Chq Date"
                Case Else
                    MsgBox("Please select mis type !", MsgBoxStyle.Information, gProjectName)
                    cboMisBy.Focus()
                    Exit Sub
            End Select

            If dtpFrom.Checked = True Then lsCond &= " and " & lsFld & " >='" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
            If dtpTo.Checked = True Then lsCond &= " and " & lsFld & " < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "

            If lsCond = "" Then lsCond &= " and 1 = 2 "

            lsSql = ""
            lsSql &= " select "
            lsSql &= " " & lsFld & " as '" & lsFldDesc & "',"
            lsSql &= " count(*) as 'Total',"
            lsSql &= " sum(if(ifnull(bounceentry_gid,0) > 0,1,0)) as 'Received',"
            lsSql &= " sum(if(ifnull(bounceentry_gid,0) = 0,1,0)) as 'Not Received' "
            lsSql &= " from chola_trn_tbounce as e "
            lsSql &= " left join chola_trn_tbounceentry as b on e.bounce_gid = b.bounceentry_bounce_gid "
            lsSql &= " where true "
            lsSql &= lsCond
            lsSql &= " group by " & lsFld & " "

            Call gpPopGridView(dgvRpt, lsSql, gOdbcConn)

            For i = 0 To dgvRpt.Columns.Count - 1
                dgvRpt.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next i

            lblRecCount.Text = "Total Records : " & dgvRpt.Rows.Count
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

    Private Sub frmPacketReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            With pnlButtons
                .Left = 6
                .Top = 6
            End With

            With dgvRpt
                .Left = pnlButtons.Left
                .Top = pnlButtons.Top + pnlButtons.Height + 6

                .Width = Me.Width - 24
                .Height = Me.Height - pnlButtons.Height - 80
                pnlDisplay.Left = .Left
                pnlDisplay.Width = .Width
                pnlDisplay.Top = .Top + .Height + 6
                btnExport.Left = pnlDisplay.Width - (btnExport.Width + 10)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Call frmCtrClear(Me)
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintDGridXML(dgvRpt, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class