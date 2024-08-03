Public Class frmpostfinone
    Dim mbUndoFlag As Boolean = False

    Public Sub FillCombo()
        Dim lsSql As String
        Dim dt As New DataTable
        lsSql = ""
        lsSql = " Select"
        lsSql &= " distinct mf.file_gid as file_gid,"
        lsSql &= " mf.file_name as file_name"
        lsSql &= " FROM chola_mst_tfile as mf"
        lsSql &= " inner join chola_trn_tfinone as tf on tf.finone_file_gid=mf.file_gid"
        lsSql &= " where import_on >= '" & Format(dtpDate.Value, "yyyy-MM-dd") & "'"
        lsSql &= " and import_on < '" & Format(DateAdd(DateInterval.Day, 1, dtpDate.Value), "yyyy-MM-dd") & "'"

        dt = GetDataTable(lsSql)

        If dt.Rows.Count > 0 Then
            cbofilename.DataSource = dt
            cbofilename.DisplayMember = "file_name"
            cbofilename.ValueMember = "file_gid"
            cbofilename.SelectedIndex = -1
        Else
            cbofilename.DataSource = Nothing
            cbofilename.SelectedIndex = -1
        End If
    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged
        FillCombo()
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Select Case mbUndoFlag
            Case True
                Call UndoFinone()
            Case False
                Call PostFinone()
        End Select
    End Sub

    Private Sub PostFinone()
        Dim i As Integer
        Dim lsSql As String
        Dim lnResult As Integer
        Dim ds As New DataSet
        Dim lnAgrId As Long = 0
        Dim lnPdcId As Long = 0
        Dim lnFinoneId As Long = 0

        If dtpDate.Checked = False Then
            MsgBox("Please Select import Date", MsgBoxStyle.Information)
            Exit Sub
        End If

        If cbofilename.Text.Trim = "" Then
            MsgBox("Please Select File Name", MsgBoxStyle.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        ' select preconversion file
        lsSql = ""
        lsSql &= " select * from chola_trn_tfinone "
        lsSql &= " where true "
        lsSql &= " and finone_file_gid=" & cbofilename.SelectedValue
        lsSql &= " and finone_entrygid=0 "

        Call gpDataSet(lsSql, "rec", gOdbcConn, ds)

        With ds.Tables("rec")
            For i = 0 To .Rows.Count - 1
                lnFinoneId = .Rows(i).Item("finone_gid")

                ' find agreement_gid
                lsSql = ""
                lsSql &= " select agreement_gid from chola_mst_tagreement "
                lsSql &= " where agreement_no = '" & QuoteFilter(.Rows(i).Item("finone_agreementno").ToString) & "' "

                lnAgrId = Val(gfExecuteScalar(lsSql, gOdbcConn))

                If lnAgrId > 0 Then
                    ' find in pdc table
                    lsSql = ""
                    lsSql &= " select entry_gid from chola_trn_tpdcentry "
                    lsSql &= " where chq_agreement_gid = " & lnAgrId & " "
                    lsSql &= " and chq_date = '" & Format(.Rows(i).Item("finone_chqdate"), "yyyy-MM-dd") & "' "
                    lsSql &= " and (chq_no = '" & Format(Val(QuoteFilter(.Rows(i).Item("finone_chqno").ToString)), "000000") & "' "
                    lsSql &= " or chq_no = '" & Val(.Rows(i).Item("finone_chqno").ToString) & "') "
                    lsSql &= " and chq_status & " & GCMATCHFINONE & " = 0 "

                    lnPdcId = Val(gfExecuteScalar(lsSql, gOdbcConn))

                    If lnPdcId > 0 Then
                        ' update in preconv table
                        lsSql = ""
                        lsSql &= " update chola_trn_tfinone set "
                        lsSql &= " finone_entrygid = " & lnPdcId & " "
                        lsSql &= " where finone_gid = " & lnFinoneId & " "
                        lsSql &= " and finone_entrygid = 0 "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        ' update in pdc table
                        lsSql = ""
                        lsSql &= " update chola_trn_tpdcentry set "
                        lsSql &= " chq_status = chq_status | " & GCMATCHFINONE
                        lsSql &= " where entry_gid = " & lnPdcId & " "
                        lsSql &= " and chq_status & " & GCMATCHFINONE & " = 0 "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)
                    End If
                End If

                Application.DoEvents()
                frmMain.lblstatus.Text = "Out of " & .Rows.Count & " record(s) reading " & (i + 1) & " record..."
            Next i

            .Rows.Clear()
        End With

        MsgBox("process Completed", MsgBoxStyle.Information)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PostFinoneOld()
        Dim lssql As String
        Dim liresult As Integer

        If dtpDate.Checked = False Then
            MsgBox("Please Select Cycle Date", MsgBoxStyle.Information)
            Exit Sub
        End If

        If cbofilename.Text.Trim = "" Then
            MsgBox("Please Select File Name", MsgBoxStyle.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        lssql = ""
        lssql &= " update chola_trn_tfinone "
        lssql &= " inner join chola_mst_tagreement on agreement_no=finone_agreementno "
        lssql &= " inner join chola_trn_tpdcentry on chq_agreement_gid=agreement_gid "
        lssql &= " and chq_no=finone_chqno and chq_date=finone_chqdate and chq_amount=finone_chqamount "
        'lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 and chq_status & " & GCPULLOUT & " = 0 "
        'lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " set finone_entrygid=entry_gid, chq_status = chq_status | " & GCMATCHFINONE
        lssql &= " where finone_entrygid=0 "
        lssql &= " and finone_file_gid=" & cbofilename.SelectedValue

        liresult = gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " update chola_trn_tfinone "
        lssql &= " inner join chola_mst_tagreement on agreement_no=finone_agreementno "
        lssql &= " inner join chola_trn_tpdcentry on chq_agreement_gid=agreement_gid "
        lssql &= " and cast(chq_no as unsigned)=finone_chqno and chq_date=finone_chqdate and chq_amount=finone_chqamount "
        'lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 and chq_status & " & GCPULLOUT & " = 0 "
        'lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " set finone_entrygid=entry_gid, chq_status = chq_status | " & GCMATCHFINONE
        lssql &= " where finone_entrygid=0 "
        lssql &= " and finone_file_gid=" & cbofilename.SelectedValue

        liresult = gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " update chola_trn_tfinone "
        lssql &= " inner join chola_mst_tagreement on agreement_no=finone_agreementno "
        lssql &= " inner join chola_trn_tpdcentry on chq_agreement_gid=agreement_gid "
        lssql &= " and chq_date=finone_chqdate and chq_amount=finone_chqamount "
        lssql &= " set finone_entrygid=entry_gid, chq_status = chq_status | " & GCMATCHFINONE
        lssql &= " where finone_entrygid=0 "
        lssql &= " and finone_file_gid=" & cbofilename.SelectedValue

        liresult = gfInsertQry(lssql, gOdbcConn)


        MsgBox("process Completed", MsgBoxStyle.Information)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub frmpostfinone_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpDate.Value = DateAdd(DateInterval.Day, -1, Now)
        dtpDate.Value = Now

        Select Case mbUndoFlag
            Case True
                Me.Text = "Undo Postconversion Dump"
                btnpost.Text = "Undo"
            Case False
                Me.Text = "Post Postconversion Dump"
                btnpost.Text = "Post"
        End Select
    End Sub

    Public Sub New(ByVal UndoFlag As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mbUndoFlag = UndoFlag
    End Sub

    Private Sub UndoFinone()
        Dim lssql As String
        Dim liresult As Integer

        If cbofilename.Text.Trim = "" Then
            MsgBox("Please Select File Name", MsgBoxStyle.Information)
            Exit Sub
        End If

        If MsgBox("Are you sure to undo ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
            Me.Cursor = Cursors.WaitCursor

            lssql = ""
            lssql &= " update chola_trn_tfinone "
            lssql &= " inner join chola_trn_tpdcentry on finone_entrygid=entry_gid "
            lssql &= " set finone_entrygid=0, chq_status = (chq_status | " & GCMATCHFINONE & ") ^ " & GCMATCHFINONE
            lssql &= " where finone_file_gid=" & cbofilename.SelectedValue

            liresult = gfInsertQry(lssql, gOdbcConn)

            MsgBox("Undo Completed !", MsgBoxStyle.Information)
            Me.Cursor = Cursors.Default
        End If
    End Sub
End Class