Public Class FrmEntry
    Dim lsfilegid As String = ""
    Dim lscolumn1 As String = ""
    Dim lscolumn2 As String = ""
    Dim lscount As String = ""
    Dim lsmandate As String = ""
    Dim lsSql As String = ""
    Dim lsdate As String
    Dim ReentryFlag As Boolean
    Dim lsremarks As String
    Dim lsgnsarefno As String

    Public Sub New(ByVal filegid As String, ByVal lscolumntemp1 As String, ByVal lscolumntemp2 As String, ByVal lstempcount As String, ByVal lstempmandate As String, ByVal gnsarefno As String, Optional ByVal remarks As String = "", Optional ByVal isReentry As Boolean = False)
        InitializeComponent()
        lsfilegid = filegid
        lscolumn1 = lscolumntemp1
        lscolumn2 = lscolumntemp2
        lscount = lstempcount
        lsmandate = lstempmandate
        lsgnsarefno = gnsarefno
        lsremarks = remarks
        ReentryFlag = isReentry
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        If txtcount.Text = "" Then
            MsgBox("Count should not be empty.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtcount.Focus()
            Exit Sub
        End If

        'If txtGnsaRefno.Text.Trim = "" Then
        '    MsgBox("Enter GNSA Reference no.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
        '    txtGnsaRefno.Focus()
        '    Exit Sub
        'Else
        '    If txtGnsaRefno.Text.Trim.Length <> 10 Then
        '        MsgBox("Enter Valid GNSA Reference no.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
        '        txtGnsaRefno.Text = Getrefno()
        '        txtGnsaRefno.Focus()
        '        Exit Sub
        '    End If
        'End If

        If Not IsNumeric(txtcount.Text) Then
            MsgBox("Enter valid numeric count.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtcount.Focus()
            Exit Sub
        End If

        If txtcount.Text = "0" Then
            MsgBox("Enter valid numeric count.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtcount.Focus()
            Exit Sub
        End If

        If txtmandate.Text = "" Then
            MsgBox("Mandate should not be empty.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtmandate.Focus()
            Exit Sub
        End If

        If Not IsNumeric(txtmandate.Text) Then
            MsgBox("Enter valid numeric mandate count.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtmandate.Focus()
            Exit Sub
        End If

        'Update the trn table status and release the lock
        Try

            'lsSql = " select file_gid from chola_trn_tfile where gnsa_refno='" & txtGnsaRefno.Text.Trim & "'"
            'filegid = gfExecuteScalar(lsSql, gOdbcConn)
            'If Val(filegid) > 0 Then
            '    MsgBox("Duplicate GNSA Reference No", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            '    txtGnsaRefno.Focus()
            '    Exit Sub
            'End If

          


            If ReentryFlag Then
                lsSql = ""
                lsSql &= " UPDATE"
                lsSql &= " chola_trn_tspdc"
                lsSql &= " set"
                lsSql &= " spdc_entryspdccnt=" & Val(txtcount.Text.Trim) & ","
                lsSql &= " spdc_entryecsmandatecnt='" & Val(txtmandate.Text) & "',"
                lsSql &= " spdc_entryremarks='" & txtremarks.Text.Trim & "',"
                lsSql &= " file_lock='N',lock_by=null,"
                lsSql &= " spdc_statusflag=spdc_statusflag|4"
                lsSql &= " where 1=1"
                lsSql &= " and spdc_gid='" & lsfilegid & "'"

                gfInsertQry(lsSql, gOdbcConn)

                LogTransaction(lsgnsarefno, 4, gUserName)
            Else
                lsSql = ""
                lsSql &= " UPDATE"
                lsSql &= " chola_trn_tspdc"
                lsSql &= " set"
                lsSql &= " spdc_entryspdccnt=" & Val(txtcount.Text.Trim) & ","
                lsSql &= " spdc_entryecsmandatecnt='" & Val(txtmandate.Text) & "',"
                lsSql &= " spdc_entryremarks='" & txtremarks.Text.Trim & "',"
                lsSql &= " file_lock='N',lock_by=null,"
                lsSql &= " spdc_statusflag=spdc_statusflag|2"
                lsSql &= " where 1=1"
                lsSql &= " and spdc_gid='" & lsfilegid & "'"

                gfExecuteQry(lsSql, gOdbcConn)

                LogTransaction(lsgnsarefno, 2, gUserName)
            End If


        Catch ex As Exception
            MsgBox("Error occured on updation.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            Exit Sub
        End Try

        Me.Close()

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Are you sure you want to close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        lsSql = ""
        lsSql &= " UPDATE"
        lsSql &= " chola_trn_tspdc"
        lsSql &= " set"
        lsSql &= " file_lock='N',lock_by=null"
        lsSql &= " where 1=1"
        lsSql &= " and spdc_gid='" & lsfilegid & "'"

        gfExecuteQry(lsSql, gOdbcConn)

        Me.Close()
    End Sub

    Private Sub FrmEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub FrmEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        KeyPreview = True
        lblmode.Text = lscolumn1
        lblagreementno.Text = lscolumn2
        lblgnsarefno.Text = lsgnsarefno        
        txtcount.Text = ""
        txtcount.Focus()
        If ReentryFlag Then
            txtcount.Text = Val(lscount)
            txtmandate.Text = Val(lsmandate)
            txtremarks.Text = lsremarks
        End If
    End Sub

    Private Sub txtcount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub
    Private Sub txtmandate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmandate.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub
End Class