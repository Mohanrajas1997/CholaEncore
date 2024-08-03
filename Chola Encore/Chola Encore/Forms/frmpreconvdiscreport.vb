Public Class frmpreconvdiscreport
    Private Sub frmpreconvdiscreport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmpreconvdiscreport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmpreconvdiscreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtagreementno.Focus()
        txtagreementno.Text = ""
        cbodiscremarks.Items.Add("Agreement Not Available")
        cbodiscremarks.Items.Add("Cheque No Mismatch")
        cbodiscremarks.Items.Add("Cheque Date Mismatch")
        cbodiscremarks.Items.Add("Cheque Amount Mismatch")
        cbodiscremarks.Items.Add("Cheque Details Mismatch")
        cbodiscremarks.Items.Add("Duplicate Records")
    End Sub

    Private Sub frmpreconvdiscreport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If
        Call LoadData()
        If dgvsummary.Rows.Count <= 0 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""
        Dim lsCond1 As String = ""

        If cbodiscremarks.Text = "Agreement Not Available" Then
            lsCond &= " and  finone_disc & " & GCDISCAGREEMENT & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque No Mismatch" Then
            lsCond &= " and  finone_disc & " & GCDISCCHQNO & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque Date Mismatch" Then
            lsCond &= " and  finone_disc & " & GCDISCCHQDATE & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque Amount Mismatch" Then
            lsCond &= " and  finone_disc & " & GCDISCCHQAMOUNT & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque Details Mismatch" Then
            lsCond &= " and  finone_disc & " & GCDISCCHQDETAILS & " > 0"
        ElseIf cbodiscremarks.Text = "Duplicate Records" Then
            lsCond &= " and  finone_disc & " & GCDUPLICATEENTRY & " > 0"
        End If

        If cbodiscremarks.Text = "Agreement Not Available" Then
            lsCond1 &= " and  chq_predisc & " & GCDISCAGREEMENT & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque No Mismatch" Then
            lsCond1 &= " and  chq_predisc & " & GCDISCCHQNO & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque Date Mismatch" Then
            lsCond1 &= " and  chq_predisc & " & GCDISCCHQDATE & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque Amount Mismatch" Then
            lsCond1 &= " and  chq_predisc & " & GCDISCCHQAMOUNT & " > 0"
        ElseIf cbodiscremarks.Text = "Cheque Details Mismatch" Then
            lsCond1 &= " and  chq_predisc & " & GCDISCCHQDETAILS & " > 0"
        ElseIf cbodiscremarks.Text = "Duplicate Records" Then
            lsCond1 &= " and  chq_predisc & " & GCDUPLICATEENTRY & " > 0"
        End If

        Dim lsstatus As String

        lsSql = " select group_concat(concat('\'',status_desc,'\'')) from "
        lsSql &= "(select status_desc FROM chola_mst_tstatus where status_deleteflag='N' and status_level='Disc' order by status_flag) as a"
        lsstatus = gfExecuteScalar(lsSql, gOdbcConn)

        lsSql = ""
        If cbotype.Text = "FINONE" Or cbotype.Text = "" Then
            lsSql &= " select finone_agreementno as 'Agreement No',if (a.packet_gnsarefno is null,cast(group_concat(distinct b.packet_gnsarefno) as char),a.packet_gnsarefno) as 'GNSA REF#',"
            lsSql &= " if( aa.almaraentry_cupboardno is null,cast(group_concat(distinct bb.almaraentry_cupboardno) as char),cast(aa.almaraentry_cupboardno as char)) as 'Cupboard No',"
            lsSql &= " if( aa.almaraentry_shelfno is null,cast(group_concat(distinct bb.almaraentry_shelfno) as char),cast(aa.almaraentry_shelfno as char)) as 'Shelf No',"
            lsSql &= " if( aa.almaraentry_boxno is null,cast(group_concat(distinct bb.almaraentry_boxno) as char),cast(aa.almaraentry_boxno as char)) as 'Box No',"
            lsSql &= " finone_chqno as 'Finone Cheque No',chq_no as 'GNSA Cheque No',"
            lsSql &= " date_format(finone_chqdate,'%d-%m-%Y') as 'Finone Cheque Date',date_format(chq_date,'%d-%m-%Y') as 'GNSA  Cheque Date', "
            lsSql &= " finone_chqamount as 'Finone Amount',chq_amount as 'GNSA Amount',"
            lsSql &= " make_set(finone_disc," & lsstatus & ") as 'Disc','Finone' as 'Type'"
            lsSql &= " from chola_trn_tfinonepreconverfile "
            lsSql &= " left join chola_mst_tagreement on agreement_no=finone_agreementno "
            lsSql &= " left join chola_trn_tpdcentry on chq_agreement_gid=agreement_gid and (chq_no=finone_chqno or "
            lsSql &= " (chq_amount=finone_chqamount and chq_date=finone_chqdate) or chq_date=finone_chqdate) "
            lsSql &= " left join chola_trn_tpacket a  on a.packet_gid=chq_packet_gid "
            lsSql &= " left join chola_trn_tpacket b on b.packet_agreement_gid=agreement_gid "
            lsSql &= " left join chola_trn_almaraentry aa on aa.almaraentry_gid=a.packet_box_gid "
            lsSql &= " left join chola_trn_almaraentry bb on bb.almaraentry_gid=b.packet_box_gid "
            lsSql &= " where 1=1 "
            lsSql &= " and finone_entry_gid=0 "

            If dtpcycledate.Checked Then
                lsSql &= " and  date_format(finone_chqdate,'%Y-%m-%d') ='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
            End If

            If txtagreementno.Text.Trim <> "" Then
                lsSql &= " and finone_shortagreementno='" & txtagreementno.Text.Trim & "'"
            End If

            lsSql &= lsCond

            lsSql &= " group by finone_gid "
        End If

        If cbotype.Text = "" Then
            lsSql &= " union ALL "
        End If

        If cbotype.Text = "GNSA" Or cbotype.Text = "" Then
            lsSql &= " select agreement_no as 'Agreement No',a.packet_gnsarefno  as 'GNSA REF#',"
            lsSql &= " cast(aa.almaraentry_cupboardno as char) as 'Cupboard No',"
            lsSql &= " cast(aa.almaraentry_shelfno as char) as 'Shelf No',"
            lsSql &= " cast(aa.almaraentry_boxno as char) as 'Box No',"
            lsSql &= " finone_chqno as 'Finone Cheque No',chq_no as 'GNSA Cheque No',"
            lsSql &= " date_format(finone_chqdate,'%d-%m-%Y') as 'Finone Cheque Date',date_format(chq_date,'%d-%m-%Y') as 'GNSA  Cheque Date',"
            lsSql &= " finone_chqamount as 'Finone Amount',chq_amount as 'GNSA Amount',"
            lsSql &= " make_set(chq_predisc," & lsstatus & ") as 'Disc','GNSA' as 'Type'"
            lsSql &= " from chola_trn_tpdcentry "
            lsSql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
            lsSql &= " left join chola_trn_tpacket a  on a.packet_gid=chq_packet_gid "
            lsSql &= " left join chola_trn_almaraentry aa on aa.almaraentry_gid=a.packet_box_gid "
            lsSql &= " left  join chola_trn_tfinonepreconverfile on finone_agreementno=agreement_no and (chq_no=finone_chqno or"
            lsSql &= " (chq_amount=finone_chqamount and chq_date=finone_chqdate) or chq_date=finone_chqdate) and  finone_entry_gid=0"
            lsSql &= " and  date_format(finone_chqdate,'%Y-%m-%d') ='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
            lsSql &= " where 1=1 "
            lsSql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0"

            If dtpcycledate.Checked Then
                lsSql &= " and  date_format(chq_date,'%Y-%m-%d') ='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
            End If

            If txtagreementno.Text.Trim <> "" Then
                lsSql &= " and agreement_no='" & txtagreementno.Text.Trim & "'"
            End If

            lsSql &= lsCond1
        End If
        With dgvsummary
            .Columns.Clear()
            gpPopGridView(dgvsummary, lsSql, gOdbcConn)
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
        End With
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        dtpcycledate.Value = Now()
        dtpcycledate.Checked = False
        txtagreementno.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
        cbodiscremarks.SelectedIndex = -1
        cbodiscremarks.SelectedIndex = -1
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Report.xls", "Report")
    End Sub
End Class