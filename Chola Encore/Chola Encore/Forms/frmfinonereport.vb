Public Class frmfinonereport
    Dim lssql As String
    Private Sub frmfinonereport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmfinonereport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmfinonereport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtAgreementNo.Focus()
        txtAgreementNo.Text = ""
    End Sub

    Private Sub frmfinonereport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub
    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        lsSql = " "
        lsSql &= " select pdc_clientcode as 'Client Code',pdc_contractno as 'Contract No',pdc_parentcontractno as 'Parent Contract No',"
        lsSql &= " pdc_shortpdc_parentcontractno as 'Short Parent No',pdc_draweename as 'Drawee Name',pdc_chqno as 'Cheque No',"
        lsSql &= " date_format(pdc_chqdate,'%d-%m-%Y') as 'Cheque Date',pdc_chqamount as 'Cheque Amount',pdc_cycle as 'Cycle',"
        lsSql &= " pdc_contractamount as 'Contract Amount',pdc_micrcode as 'Micr Code',pdc_bankname as 'Bank Name',"
        lsSql &= " pdc_bankbranch as 'Bank Branch',pdc_payablelocation as 'Payable Location',pdc_pickuplocation as 'Pickup Location',"
        lsSql &= " pdc_mode as 'Mode',pdc_type as 'Type',pdc_branchname as 'Branch Name',date_format(pdc_importdate,'%d-%m-%Y') as 'Auth Date'"
        lsSql &= " from chola_trn_tpdcfile "
        lsSql &= " where 1=1 "

        If dtpfrom.Checked = True Then
            lsSql &= " and date_format(pdc_importdate,'%Y-%m-%d')>='" & Format(CDate(dtpfrom.Text), "yyyy-MM-dd") & "'"
        End If

        If dtpto.Checked = True Then
            lsSql &= " and date_format(pdc_importdate,'%Y-%m-%d')<='" & Format(CDate(dtpto.Text), "yyyy-MM-dd") & "'"
        End If

        If txtShortAgreementNo.Text <> "" Then
            If InStr(txtShortAgreementNo.Text, ",") = 0 Then
                lsSql &= " and pdc_shortpdc_parentcontractno='" & txtShortAgreementNo.Text & "'"
            Else
                lsSql &= " and pdc_shortpdc_parentcontractno in ('" & Replace(txtShortAgreementNo.Text, ",", "','") & "')"
            End If
        End If

        If txtAgreementNo.Text <> "" Then
            If InStr(txtAgreementNo.Text, ",") = 0 Then
                lsSql &= " and pdc_parentcontractno='" & txtAgreementNo.Text & "'"
            Else
                lsSql &= " and pdc_parentcontractno in ('" & Replace(txtAgreementNo.Text, ",", "','") & "')"
            End If
        End If

        If txtchequeno.Text.Trim <> "" Then
            lsSql &= " and pdc_chqno='" & txtchequeno.Text.Trim & "'"
        End If

        If Val(txtchqamount.Text) > 0 Then
            lsSql &= " and pdc_chqamount=" & Val(txtchqamount.Text.Trim) & ""
        End If

        If dtpchqdate.Checked Then
            lsSql &= " and pdc_chqdate='" & Format(dtpchqdate.Value, "yyyy-MM-dd") & "'"
        End If

        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub


    Private Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click

        If dtpfrom.Checked = False Then
            MsgBox("Please Select Auth From..!", MsgBoxStyle.Critical, gProjectName)
            dtpfrom.Focus()
            Exit Sub
        End If


        If dtpto.Checked = False Then
            MsgBox("Please Select Auth To..!", MsgBoxStyle.Critical, gProjectName)
            dtpto.Focus()
            Exit Sub
        End If

        Call LoadData()

        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Report.xls", "Report")
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpfrom.Value = Now()
        dtpfrom.Checked = False
        dtpto.Value = Now()
        dtpto.Checked = False
        txtAgreementNo.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
        txtchequeno.Text = ""
        txtchqamount.Text = ""
        dtpchqdate.Value = Now
        dtpchqdate.Checked = False
    End Sub
End Class