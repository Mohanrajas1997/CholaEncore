
Public Class frmhandsoffentry
    Dim lsagreementno As String = ""
    Dim lstype As String = ""
    Dim chqno As String = ""
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal Agreementno As String)
        lsagreementno = Agreementno
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lssql As String
        Dim lschqno As String = ""
        Dim lschqnos() As String
        Dim ischqlevel As Boolean = False
        Dim lspdcgnsarefno As String = ""
        Dim lsspdcgnsarefno As String = ""
        Dim listatus As Integer

        If Not IsDate(mtxthandsoffdate.Text) Then
            MsgBox("Please enter Valid Handsoff Date", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            mtxthandsoffdate.Focus()
            Exit Sub
        End If

        If txthandsoffto.Text.Trim = "" Then
            MsgBox("Please enter Handsoff To", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txthandsoffto.Focus()
            Exit Sub
        End If

        If txtdcno.Text.Trim = "" Then
            MsgBox("Please enter DC No", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtdcno.Focus()
            Exit Sub
        End If

        If Val(txtchqcount.Text.Trim) = 0 Then
            MsgBox("Please valid Chq Count", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            txtdcno.Focus()
            Exit Sub
        End If

        lssql = " select status_flag "
        lssql &= " from chola_mst_tstatus"
        lssql &= "  inner join chola_trn_thandsoff on handsoff_type=status_desc "
        lssql &= " where handsoff_shortagreementno='" & lsagreementno & "'"

        listatus = Val(gfExecuteScalar(lssql, gOdbcConn))

        If listatus = 0 Then
            MsgBox("Invalid Status.,Contact Administrator", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            Exit Sub
        End If

        lssql = " select spdc_gnsarefno from chola_trn_tspdc where spdc_shortagreementno='" & lsagreementno & "'"

        lsspdcgnsarefno = gfExecuteScalar(lssql, gOdbcConn)

        lssql = " select pdc_gnsarefno from chola_trn_tpdcfile where pdc_shortpdc_parentcontractno='" & lsagreementno & "'"

        lspdcgnsarefno = gfExecuteScalar(lssql, gOdbcConn)

        If lsspdcgnsarefno <> "" And lspdcgnsarefno <> "" Then
            MsgBox("Agreement No Available in both PDC & SPDC", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            Exit Sub
        ElseIf lsspdcgnsarefno = "" And lspdcgnsarefno = "" Then
            MsgBox("Agreement No not Available in both PDC & SPDC", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
            Exit Sub
        End If


        lssql = " select handsofftype_chqlevel "
        lssql &= " from chola_mst_thandsofftype "
        lssql &= " inner join chola_trn_thandsoff on handsoff_type=handsofftype_name "
        lssql &= " where handsoff_shortagreementno='" & lsagreementno & "'"

        If gfExecuteScalar(lssql, gOdbcConn) = "Y" Then
            ischqlevel = True
        Else
            ischqlevel = False
        End If

        If ischqlevel Then
            chqno = ""
            lschqno = GetChqNo()

            If Replace(lschqno, "|", "").Trim.Length = 0 Then
                MsgBox("Please enter atleast One Chqno", MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, gProjectName)
                lschqno = GetChqNo()
            End If

            While MsgBox("Entered Chqno#" & lschqno & vbCrLf & "Do you want to Reenter?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes
                chqno = ""
                lschqno = GetChqNo()
            End While

            lschqno = lschqno.TrimEnd("|")
            lschqnos = Split(lschqno, "|")

            For i As Integer = 0 To UBound(lschqnos)
                If lschqnos(i).Trim <> "" Then
                    lssql = " insert into chola_trn_thandsoffentry(entry_shortagreementno,entry_chqno,entry_insertby,entry_insertdate) values ("
                    lssql &= "'" & lsagreementno & "',"
                    lssql &= "'" & lschqnos(i).Trim & "',"
                    lssql &= "'" & gUserName & "',sysdate())"

                    gfInsertQry(lssql, gOdbcConn)

                    If lspdcgnsarefno <> "" Then
                        lssql = " update chola_trn_tpdcfile set "
                        lssql &= " pdc_status_flag=pdc_status_flag | " & listatus
                        lssql &= " where pdc_gnsarefno='" & lspdcgnsarefno & "'"
                        lssql &= " and pdc_chqno='" & lschqnos(i).Trim & "'"

                        gfInsertQry(lssql, gOdbcConn)

                    End If
                End If
            Next

            If lsspdcgnsarefno <> "" Then
                lssql = " update chola_trn_tspdc set "
                lssql &= " spdc_statusflag=spdc_statusflag | " & listatus
                lssql &= " where spdc_gnsarefno='" & lsspdcgnsarefno & "'"
                gfInsertQry(lssql, gOdbcConn)

                LogTransaction(lsspdcgnsarefno, listatus, gUserName)
            Else
                LogTransaction(lspdcgnsarefno, listatus, gUserName)
            End If
        Else

            If lsspdcgnsarefno <> "" Then
                lssql = " update chola_trn_tspdc set "
                lssql &= " spdc_statusflag=spdc_statusflag | " & listatus
                lssql &= " where spdc_gnsarefno='" & lsspdcgnsarefno & "'"
                gfInsertQry(lssql, gOdbcConn)

                LogTransaction(lsspdcgnsarefno, listatus, gUserName)
            End If

            If lspdcgnsarefno <> "" Then
                lssql = " update chola_trn_tpdcfile set "
                lssql &= " pdc_status_flag=pdc_status_flag | " & listatus
                lssql &= " where pdc_gnsarefno='" & lspdcgnsarefno & "'"

                gfInsertQry(lssql, gOdbcConn)

                LogTransaction(lspdcgnsarefno, listatus, gUserName)

            End If
        End If

        lssql = " update chola_trn_thandsoff set "
        lssql &= " handsoff_handsoffdate='" & Format(CDate(mtxthandsoffdate.Text), "yyyy-MM-dd") & "',"
        lssql &= " handsoff_to='" & txthandsoffto.Text.Trim & "',"
        lssql &= " handsoff_dcno='" & txtdcno.Text.Trim & "',"
        lssql &= " handsoff_chqcnt=" & Val(txtchqcount.Text.Trim) & ", "
        lssql &= " handsoff_handsoffflag='Y'"
        lssql &= " where handsoff_shortagreementno = '" & lsagreementno & "'"

        gfInsertQry(lssql, gOdbcConn)

        Me.Close()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Function GetChqNo() As String
        Dim lsTemp As String
        lsTemp = InputBox("Enter Chq No.") & "|"

        If InStr(chqno, lsTemp) > 0 Then
            MsgBox("Duplicate Cheque No. Please try again.", MsgBoxStyle.Information, gProjectName)
        Else
            chqno &= lsTemp
        End If

        If MsgBox("Do you want to add another Chq no", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
            GetChqNo()
        End If
        Return chqno
    End Function

    Private Sub txtchqcount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchqcount.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub
End Class