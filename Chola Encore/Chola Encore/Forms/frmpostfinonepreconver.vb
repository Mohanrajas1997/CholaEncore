Public Class frmpostfinonepreconver
    Dim mbUndoFlag As Boolean

    Public Sub FillCombo()
        Dim lsSql As String
        Dim dt As New DataTable
        lsSql = ""
        lsSql = " Select"
        lsSql &= " distinct mf.file_gid as file_gid,"
        lsSql &= " mf.file_name as file_name"
        lsSql &= " FROM chola_mst_tfile as mf"
        lsSql &= " inner join chola_trn_tfinonepreconverfile as tf where tf.finone_file_gid=mf.file_gid"
        lsSql &= " and date_format(import_on,'%Y-%m-%d')='" & Format(CDate(dtpDate.Text), "yyyy-MM-dd") & "'"

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
                Call UndoPreconversion()
            Case False
                Call PostPreconversion()
        End Select
    End Sub

    Private Sub PostPreconversionOld()
        Dim lssql As String
        Dim liresult As Integer

        If dtpDate.Checked = False Then
            MsgBox("Please Select import Date", MsgBoxStyle.Information)
            Exit Sub
        End If

        If cbofilename.Text.Trim = "" Then
            MsgBox("Please Select File Name", MsgBoxStyle.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        lssql = ""
        lssql &= " update chola_trn_tfinonepreconverfile "
        lssql &= " inner join chola_mst_tagreement on agreement_no=finone_agreementno "
        lssql &= " inner join chola_trn_tpdcentry on chq_agreement_gid=agreement_gid "
        lssql &= " and chq_no=finone_chqno and chq_date=finone_chqdate and chq_amount=finone_chqamount "
        'lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 and chq_status & " & GCPULLOUT & " = 0 "
        'lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " set finone_entry_gid=entry_gid, chq_status = chq_status | " & GCMATCHFINONEPRECOVERFILE
        lssql &= " where finone_entry_gid=0 "
        lssql &= " and finone_file_gid=" & cbofilename.SelectedValue

        liresult = gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " update chola_trn_tfinonepreconverfile "
        lssql &= " inner join chola_mst_tagreement on agreement_no=finone_agreementno "
        lssql &= " inner join chola_trn_tpdcentry on chq_agreement_gid=agreement_gid "
        lssql &= " and cast(chq_no as unsigned)=finone_chqno and chq_date=finone_chqdate and chq_amount=finone_chqamount "
        'lssql &= " and chq_status & " & GCPACKETPULLOUT & " = 0 and chq_status & " & GCPULLOUT & " = 0 "
        'lssql &= " and chq_status & " & GCMATCHFINONE & " = 0 "
        lssql &= " set finone_entry_gid=entry_gid, chq_status = chq_status | " & GCMATCHFINONEPRECOVERFILE
        lssql &= " where finone_entry_gid=0 "
        lssql &= " and finone_file_gid=" & cbofilename.SelectedValue

        liresult = gfInsertQry(lssql, gOdbcConn)

        'lssql = ""
        'lssql &= " update chola_trn_tfinonepreconverfile "
        'lssql &= " inner join chola_mst_tagreement on shortagreement_no=finone_shortagreementno "
        'lssql &= " inner join chola_trn_tpdcentry on chq_agreement_gid=agreement_gid "
        'lssql &= " and chq_date=finone_chqdate and chq_amount=finone_chqamount "
        'lssql &= " set finone_entry_gid=entry_gid, chq_status = chq_status | " & GCMATCHFINONEPRECOVERFILE
        'lssql &= " where finone_entry_gid=0 "
        'lssql &= " and finone_file_gid=" & cbofilename.SelectedValue

        'liresult = gfInsertQry(lssql, gOdbcConn)


        MsgBox("process Completed", MsgBoxStyle.Information)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UndoPreconversion()
        Dim lssql As String
        Dim liresult As Integer

        If cbofilename.Text.Trim = "" Then
            MsgBox("Please Select File Name", MsgBoxStyle.Information)
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor

        lssql = ""
        lssql &= " update chola_trn_tfinonepreconverfile "
        lssql &= " inner join chola_trn_tpdcentry on finone_entry_gid=entry_gid "
        lssql &= " set finone_entry_gid=0, chq_status = (chq_status | " & GCMATCHFINONEPRECOVERFILE & ") ^ " & GCMATCHFINONEPRECOVERFILE
        lssql &= " where finone_entry_gid=0 "
        lssql &= " and finone_file_gid=" & cbofilename.SelectedValue

        liresult = gfInsertQry(lssql, gOdbcConn)

        MsgBox("Record undo made successfully !", MsgBoxStyle.Information)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub frmpostfinonepreconver_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select mbUndoFlag
            Case True
                Me.Text = "Undo Preconversion File"
                btnpost.Text = "Undo"
            Case False
                Me.Text = "Post Preconversion File"
                btnpost.Text = "Post"
        End Select
    End Sub

    Public Sub New(ByVal UndoFlag As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mbUndoFlag = UndoFlag
    End Sub

    Private Sub PostPreconversion()
        Dim i As Integer
        Dim lsSql As String
        Dim lnResult As Integer
        Dim ds As New DataSet
        Dim lnAgrId As Long = 0
        Dim lnPdcId As Long = 0
        Dim lnPreConvId As Long = 0

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
        lsSql &= " select * from chola_trn_tfinonepreconverfile "
        lsSql &= " where true "
        lsSql &= " and finone_file_gid=" & cbofilename.SelectedValue
        lsSql &= " and finone_entry_gid=0 "

        Call gpDataSet(lsSql, "rec", gOdbcConn, ds)

        With ds.Tables("rec")
            For i = 0 To .Rows.Count - 1
                lnPreConvId = .Rows(i).Item("finone_gid")

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
                    lsSql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "

                    lnPdcId = Val(gfExecuteScalar(lsSql, gOdbcConn))

                    If lnPdcId > 0 Then
                        ' update in preconv table
                        lsSql = ""
                        lsSql &= " update chola_trn_tfinonepreconverfile set "
                        lsSql &= " finone_entry_gid = " & lnPdcId & " "
                        lsSql &= " where finone_gid = " & lnPreConvId & " "
                        lsSql &= " and finone_entry_gid = 0 "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        ' update in pdc table
                        lsSql = ""
                        lsSql &= " update chola_trn_tpdcentry set "
                        lsSql &= " chq_status = chq_status | " & GCMATCHFINONEPRECOVERFILE
                        lsSql &= " where entry_gid = " & lnPdcId & " "
                        lsSql &= " and chq_status & " & GCMATCHFINONEPRECOVERFILE & " = 0 "

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
End Class