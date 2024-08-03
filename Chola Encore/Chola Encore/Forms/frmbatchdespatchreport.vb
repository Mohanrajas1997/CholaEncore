Imports System.IO
Public Class frmbatchdespatchreport

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim lsSql As String
        Dim lsbatchid As String

        If dtpDate.Checked = False Then
            MsgBox("Please select Cycle Date", MsgBoxStyle.Information, gProjectName)
            dtpDate.Focus()
            Exit Sub
        End If

        If cbotype.Text = "" Then
            MsgBox("Please select Type", MsgBoxStyle.Information, gProjectName)
            cbotype.Focus()
            Exit Sub
        End If


        btnGenerate.Enabled = False
        Me.Cursor = Cursors.WaitCursor

        lsSql = ""
        lsSql &= " select group_concat(cast( batch_gid as char)) from chola_trn_tbatch"
        lsSql &= " where batch_cycledate = '" & Format(dtpDate.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and batch_prodtype=" & cbotype.SelectedValue
        lsSql &= " and batch_istally='Y' "
        lsSql &= " and batch_despatch_gid >0 "

        lsbatchid = gfExecuteScalar(lsSql, gOdbcConn)

        If lsbatchid <> "" Then
            lsSql = ""
            lsSql &= " select batch_displayno 'Batch No',type_name as 'Product',"
            lsSql &= " shortagreement_no 'Short Agreement No',agreement_no 'Agreement No', "
            lsSql &= " chq_no 'Cheque No', date_format(chq_date, '%d-%m-%Y') 'Cheque Date', chq_amount 'Cheque Amount',packet_gnsarefno 'GNSA Ref' "
            lsSql &= " from chola_trn_tpdcentry "
            lsSql &= " inner join chola_mst_tagreement on chq_agreement_gid=agreement_gid "
            lsSql &= " inner join chola_trn_tpacket on packet_agreement_gid=agreement_gid "
            lsSql &= " inner join chola_mst_ttype on type_flag=chq_prodtype and type_deleteflag='N' "
            lsSql &= " inner join chola_trn_tbatch on batch_gid=chq_batch_gid "
            lsSql &= " where chq_batch_gid in (" & lsbatchid & ")"
            lsSql &= " group by chq_agreement_gid,chq_no"
            lsSql &= " order by chq_batchslno,packet_gnsarefno "

            Dim lsfilePath As String
            If Not Directory.Exists(gsReportPath) Then Directory.CreateDirectory(gsReportPath)

            lsfilePath = gsReportPath & Format(Date.Now, "dd-MMM-yyyy")

            If Not Directory.Exists(lsfilePath) Then Directory.CreateDirectory(lsfilePath)

            lsfilePath &= "\" & cbotype.Text & "-" & Format(dtpDate.Value, "yyyyMMdd") & ".xls"

            SqlToXml(lsSql, lsfilePath, "Report", False)
            MsgBox("File generated in the path " & gsReportPath & Format(Date.Now, "dd-MMM-yyyy"), , gProjectName)
        Else
            MsgBox("No Data found", MsgBoxStyle.Critical, gProjectName)
        End If

        btnGenerate.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub frmbatchdespatchreport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lsSql As String
        lsSql = " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N'"
        gpBindCombo(lsSql, "type_name", "type_flag", cbotype, gOdbcConn)
        cbotype.SelectedIndex = -1
    End Sub
End Class