Public Class frmbatchentry
    Dim lsbatchgid As String
    Dim isload As Boolean
    Dim lssql As String
    Dim lscycledate, lsproduct As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal Cycledate As String, ByVal product As String, ByVal Batchid As String)

        ' This call is required by the designer.
        InitializeComponent()


        ' Add any initialization after the InitializeComponent() call.
        lscycledate = Cycledate
        lsproduct = product
        lsbatchgid = Batchid
    End Sub

    Private Sub frmbatchentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub txtagreementno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = gfIntstrEntryOnly(e)
    End Sub

    Private Sub frmbatchentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isload = True
        lssql = " select type_flag,type_name from chola_mst_ttype where type_deleteflag='N' "
        gpBindCombo(lssql, "type_name", "type_flag", cboproducttype, gOdbcConn)
        cboproducttype.SelectedIndex = -1
        isload = False
        If lscycledate <> "" Then
            dtpcycledate.Value = Format(CDate(lscycledate), "dd-MM-yyyy")
            cboproducttype.SelectedValue = lsproduct
            cbobatchno.SelectedValue = lsbatchgid
            frmbatchentry_Resize(sender, e)
            btnrefresh.PerformClick()
            txtchqno.Focus()
        Else
            frmbatchentry_Resize(sender, e)
        End If
    End Sub

    Private Sub cboproducttype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboproducttype.SelectedIndexChanged
        If isload = True Then Exit Sub

        If dtpcycledate.Checked = False Then
            MsgBox("Please select Cycle Date", MsgBoxStyle.Information)
            Exit Sub
        End If

        lssql = " select batch_gid,batch_displayno "
        lssql &= " from chola_trn_tbatch "
        lssql &= " where batch_prodtype=" & cboproducttype.SelectedValue
        lssql &= " and batch_cycledate='" & Format(dtpcycledate.Value, "yyyy-MM-dd") & "'"
        lssql &= " and batch_istally='N' and batch_deleteflag='N' "
        gpBindCombo(lssql, "batch_displayno", "batch_gid", cbobatchno, gOdbcConn)
        cbobatchno.SelectedIndex = -1
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        If dtpcycledate.Checked = False Then
            MsgBox("Please Select Cycle Date", MsgBoxStyle.Information)
            Exit Sub
        End If

        If cboproducttype.Text = "" Then
            MsgBox("Please Select Product Type", MsgBoxStyle.Information)
            Exit Sub
        End If

        If cbobatchno.Text = "" Then
            MsgBox("Please Select Batch No", MsgBoxStyle.Information)
            Exit Sub
        End If

        lssql = " set @slno:=0 "
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select entry_gid,agreement_gid,@slno := @slno + 1 as 'SL No',packet_gnsarefno as 'GNSA REF#',"
        lssql &= " shortagreement_no as 'Short Agreement No',agreement_no as 'Agreement No',"
        lssql &= " chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',chq_amount as 'Amount',"
        lssql &= " if(chq_papflag='Y','PAP','NON PAP') as 'PAP/NON PAP' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " where chq_batch_gid=" & cbobatchno.SelectedValue
        'lssql &= " group by shortagreement_no,chq_no"
        lssql &= " order by chq_batchslno,packet_gnsarefno "

        gpPopGridView(dgvdumpsummary, lssql, gOdbcConn)
        dgvdumpsummary.Columns("entry_gid").Visible = False
        dgvdumpsummary.Columns("agreement_gid").Visible = False

        FillEntryGrid()
    End Sub

    Private Sub FillEntryGrid()

        lssql = " set @slno:=0 "
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select @slno:= @slno + 1 as 'SL No',packet_gnsarefno as 'GNSA REF#',"
        lssql &= " shortagreement_no as 'Short Agreement No',agreement_no as 'Agreement No',"
        lssql &= " chq_no as 'Cheque No',date_format(chq_date,'%d-%m-%Y') as 'Cheque Date',chq_amount as 'Amount',"
        lssql &= " if(chq_papflag='Y','PAP','NON PAP') as 'PAP/NON PAP' "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " where chq_batch_gid=" & cbobatchno.SelectedValue
        lssql &= " and chq_status & " & GCPRESENTATIONDE & " > 0 "
        'lssql &= " group by shortagreement_no,chq_no"
        lssql &= " order by chq_batchslno,packet_gnsarefno "

        gpPopGridView(dgventrysummary, lssql, gOdbcConn)
        txtslno.Text = dgventrysummary.RowCount + 1

        dtpcycledate.Enabled = False
        cboproducttype.Enabled = False
        cbobatchno.Enabled = False
        txtchqno.Focus()
    End Sub
    Private Sub frmbatchentry_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        GroupBox1.Width = Me.Width - 35
        GroupBox1.Height = Me.Height - GroupBox2.Height - 70
        dgvdumpsummary.Height = (GroupBox1.Height / 2) - 50
        dgvdumpsummary.Width = Me.Width - 50
        GroupBox3.Top = dgvdumpsummary.Height + 25
        dgventrysummary.Top = GroupBox3.Top + 60
        dgventrysummary.Height = (GroupBox1.Height / 2) - 50
        dgventrysummary.Width = Me.Width - 50
    End Sub

    Private Sub btnclose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Are you Sure want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnsubmit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lssql As String
        Dim lschqno As String
        Dim lsagreementgid As String
        Dim lschqamt As String

        If dgvdumpsummary.RowCount = 0 Then Exit Sub

        lsagreementgid = dgvdumpsummary.Rows(dgventrysummary.RowCount).Cells("agreement_gid").Value.ToString()
        lschqno = dgvdumpsummary.Rows(dgventrysummary.RowCount).Cells("Cheque No").Value.ToString()
        lschqamt = dgvdumpsummary.Rows(dgventrysummary.RowCount).Cells("Amount").Value.ToString()

        If lschqno <> txtchqno.Text.Trim Then
            MsgBox("Invalid Cheque No", MsgBoxStyle.Information)
            txtchqno.Focus()
            Exit Sub
        End If

        If Val(lschqamt) <> Val(txtchequeamt.Text) Then
            MsgBox("Invalid Cheque Amount", MsgBoxStyle.Information)
            txtchequeamt.Focus()
            Exit Sub
        End If

        lssql = " select entry_gid from chola_trn_tpdcentry "
        lssql &= " where chq_status &" & GCPACKETPULLOUT & " > 0"
        lssql &= " and chq_agreement_gid=" & lsagreementgid
        lssql &= " and chq_no='" & lschqno & "'"

        If Val(gfExecuteScalar(lssql, gOdbcConn)) > 0 Then
            MsgBox("This Cheque Available in Packet Pullout list..", MsgBoxStyle.Information)
            Exit Sub
        End If

        lssql = " select entry_gid from chola_trn_tpdcentry "
        lssql &= " where chq_status &" & GCCLOSURE & " > 0"
        lssql &= " and chq_agreement_gid=" & lsagreementgid
        lssql &= " and chq_no='" & lschqno & "'"

        If Val(gfExecuteScalar(lssql, gOdbcConn)) > 0 Then
            MsgBox("This Cheque Available in Foreclosure list..", MsgBoxStyle.Information)
            Exit Sub
        End If

        lssql = " update chola_trn_tpdcentry set "
        lssql &= " chq_status=chq_status | " & GCPRESENTATIONDE
        lssql &= " where chq_agreement_gid=" & lsagreementgid
        lssql &= " and chq_no='" & lschqno & "'"

        gfInsertQry(lssql, gOdbcConn)

        lssql = " update chola_trn_tbatch set batch_entrychq=if(batch_entrychq is null,0,batch_entrychq)+1,"
        lssql &= " batch_entrychqamt=if(batch_entrychqamt is null,0,batch_entrychqamt) + " & Val(lschqamt)
        lssql &= " where batch_gid=" & cbobatchno.SelectedValue
        gfInsertQry(lssql, gOdbcConn)

        lssql = " update chola_trn_tbatch set batch_istally='Y' "
        lssql &= " where batch_gid=" & cbobatchno.SelectedValue
        lssql &= " and batch_totalchqamt=batch_entrychqamt "
        lssql &= " and batch_entrychq=batch_totalchq "

        If Val(gfInsertQry(lssql, gOdbcConn)) Then
            MsgBox("Batch Completed...", MsgBoxStyle.Information)
            isload = True
            dtpcycledate.Value = Now()
            dtpcycledate.Checked = False
            dtpcycledate.Enabled = True
            cboproducttype.Enabled = True
            cboproducttype.SelectedIndex = -1
            cbobatchno.Enabled = True
            cbobatchno.SelectedIndex = -1
            dtpcycledate.Focus()
            dgvdumpsummary.Columns.Clear()
            dgventrysummary.Columns.Clear()
            txtchequeamt.Text = ""
            txtchqno.Text = ""
            isload = False
            Exit Sub
        End If

        txtchequeamt.Text = ""
        txtchqno.Text = ""
        FillEntryGrid()


    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        isload = True
        dtpcycledate.Enabled = True
        dtpcycledate.Value = Now
        dtpcycledate.Checked = False
        cboproducttype.SelectedIndex = -1
        cboproducttype.Enabled = True
        cbobatchno.SelectedIndex = -1
        cbobatchno.Enabled = True
        dgvdumpsummary.DataSource = Nothing
        dgventrysummary.DataSource = Nothing
        lscycledate = ""
        lsbatchgid = ""
        lsproduct = ""
        isload = False
    End Sub

    Private Sub dgvdumpsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvdumpsummary.CellDoubleClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgvdumpsummary.Rows(e.RowIndex)
        Dim frmentry As New frmupdateentry(row.Cells("entry_gid").Value.ToString())
        frmentry.ShowDialog()
        btnrefresh.PerformClick()
    End Sub
End Class