Public Class frmmanualbatchentry
    Dim liBatchNo As Integer
    Dim ldCycleDate As DateTime
    Dim lsDispProduct As String
    Dim liValProduct As String
    Dim lssql As String
    Dim lnBatchGid As Long
    Public Sub New(ByVal BatchID As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lnBatchGid = BatchID
    End Sub

    Private Sub frmmanualbatchentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmmanualbatchentry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim drbatch As Odbc.OdbcDataReader

        lssql = ""
        lssql &= " select * "
        lssql &= " from chola_trn_tbatch "
        lssql &= " inner join chola_mst_ttype on type_flag=batch_prodtype and type_deleteflag='N' "
        lssql &= " where batch_deleteflag='N' "
        lssql &= " and batch_gid = " & lnBatchGid

        drbatch = gfExecuteQry(lssql, gOdbcConn)

        If drbatch.HasRows Then
            While drbatch.Read
                liBatchNo = drbatch.Item("batch_no").ToString
                ldCycleDate = CDate(drbatch.Item("batch_cycledate").ToString)
                lsDispProduct = drbatch.Item("type_name").ToString
                liValProduct = drbatch.Item("type_flag").ToString
            End While
        End If

        lblbatchno.Text = Format(liBatchNo, "0000")
        lblcycledate.Text = Format(ldCycleDate, "dd-MM-yyyy")
        lblproduct.Text = lsDispProduct
        lblamount.Text = 0
        lbltotalchq.Text = 0

        lssql = ""
        lssql &= " select batch_gid "
        lssql &= " from chola_trn_tbatch "
        lssql &= " where batch_no = " & liBatchNo
        lssql &= " and batch_deleteflag='N' "

        lnBatchGid = Val(gfExecuteScalar(lssql, gOdbcConn))

        FillEntryGrid()
        txtchqno.Focus()
    End Sub

    Private Sub txtchequeamt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchequeamt.LostFocus
        If Val(txtchequeamt.Text) > 0 Then
            If txtchqno.Text.Trim = "" Then
                MsgBox("please enter Cheque No", MsgBoxStyle.Information)
                Exit Sub
            Else
                lssql = " select agreement_gid,concat(shortagreement_no,'-',agreement_no) as agreement_no "
                lssql &= " from chola_trn_tpdcentry "
                lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid"
                lssql &= " where chq_no='" & txtchqno.Text.Trim & "'"
                lssql &= " and chq_amount=" & Val(txtchequeamt.Text.Trim)
                lssql &= " and chq_date='" & Format(ldCycleDate, "yyyy-MM-dd") & "'"

                gpBindCombo(lssql, "agreement_no", "agreement_gid", cboagreementno, gOdbcConn)

                If cboagreementno.Items.Count = 0 Then Exit Sub
                If cboagreementno.Items.Count > 1 Then
                    cboagreementno.SelectedIndex = -1
                Else
                    cboagreementno.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Sub btnsubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim lnStatus As Integer
        Dim lnTempStatus As Integer
        Dim lnValidStatus As Integer
        Dim lnPdcId As Long

        Dim liBatchSlno As Integer
        Dim lnChqAmt As Double

        Dim ds As New DataSet

        If txtchqno.Text.Trim = "" Then
            MsgBox("please enter Cheque No", MsgBoxStyle.Critical)
            txtchqno.Focus()
            Exit Sub
        End If

        If Val(txtchequeamt.Text) = 0 Then
            MsgBox("please enter Valid Cheque Amount", MsgBoxStyle.Critical)
            txtchequeamt.Focus()
            Exit Sub
        End If

        If cboagreementno.Text.Trim = "" Then
            MsgBox("please Select Agreement No", MsgBoxStyle.Critical)
            cboagreementno.Focus()
            Exit Sub
        End If


        lssql = " select * from chola_trn_tpdcentry "
        lssql &= " where 1 = 1 "
        lssql &= " and chq_agreement_gid=" & cboagreementno.SelectedValue
        lssql &= " and chq_no = '" & txtchqno.Text.Trim & "' "
        lssql &= " and chq_date = '" & Format(ldCycleDate, "yyyy-MM-dd") & "'"
        lssql &= " and chq_type = 1 "

        If liValProduct < 8 Then
            lssql &= " and chq_prodtype=" & liValProduct
        End If

        Call gpDataSet(lssql, "pdc", gOdbcConn, ds)

        lssql = ""
        lssql &= " select count(*) "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " where chq_batch_gid = " & lnBatchGid
        liBatchSlno = gfExecuteScalar(lssql, gOdbcConn)
        liBatchSlno += 1

        With ds.Tables("pdc")
            Select Case .Rows.Count
                Case 1
                    lnStatus = .Rows(0).Item("chq_status")
                    lnPdcId = .Rows(0).Item("entry_gid")
                    lnChqAmt = Math.Round(.Rows(0).Item("chq_amount"), 2)

                    ' Check pullout or despatch or Packet Pullout or Presentation Pullout or Presentation Dataentry
                    lnValidStatus = GCPULLOUT Or GCDESPATCH Or GCPACKETPULLOUT Or GCPRESENTATIONDE Or GCPRESENTATIONPULLOUT
                    lnTempStatus = lnStatus And lnValidStatus


                    If lnTempStatus Then MsgBox("Access denied !", MsgBoxStyle.Critical, gProjectName) : Exit Sub

                    lssql = " update chola_trn_tpdcentry set "
                    lssql &= " chq_status = chq_status | " & GCPRESENTATIONPULLOUT & " | " & GCPRESENTATIONDE & ","
                    lssql &= " chq_batch_gid = " & lnBatchGid & ","
                    lssql &= " chq_batchslno = " & liBatchSlno
                    lssql &= " where entry_gid=" & lnPdcId & ""
                    gfInsertQry(lssql, gOdbcConn)

                    lssql = " update chola_trn_tbatch set batch_totalchq = if(batch_totalchq is null,0,batch_totalchq) + 1,"
                    lssql &= " batch_totalchqamt= if (batch_totalchqamt is null,0,batch_totalchqamt) + " & lnChqAmt & ","
                    lssql &= " batch_entrychq=if(batch_entrychq is null,0,batch_entrychq) + 1 ,"
                    lssql &= " batch_entrychqamt=if(batch_entrychqamt is null,0,batch_entrychqamt) + " & lnChqAmt
                    lssql &= " where batch_gid=" & lnBatchGid
                    gfInsertQry(lssql, gOdbcConn)

                    FillEntryGrid()
                    txtchqno.Text = ""
                    txtchequeamt.Text = ""
                    cboagreementno.DataSource = Nothing

                    'MsgBox("Record updated successfully !", MsgBoxStyle.Information, gProjectName)
                Case 0
                    MsgBox("Invalid record !", MsgBoxStyle.Critical, gProjectName)
                Case Else
                    MsgBox("More than one record found !", MsgBoxStyle.Critical, gProjectName)
            End Select

            .Rows.Clear()
        End With
        txtchqno.Focus()
    End Sub
    Private Sub FillEntryGrid()

        Dim ds As New DataSet

        lssql = ""
        lssql &= " select batch_entrychq,batch_entrychqamt "
        lssql &= " from chola_trn_tbatch "
        lssql &= " where batch_deleteflag='N' "
        lssql &= " and batch_gid = " & lnBatchGid

        Call gpDataSet(lssql, "batch", gOdbcConn, ds)

        If ds.Tables("batch").Rows.Count > 0 Then
            lbltotalchq.Text = Val(ds.Tables("batch").Rows(0).Item("batch_entrychq").ToString)
            lblamount.Text = Val(ds.Tables("batch").Rows(0).Item("batch_entrychqamt").ToString)
        End If
        

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
        lssql &= " where chq_batch_gid=" & lnBatchGid
        lssql &= " and chq_status & " & GCPRESENTATIONDE & " > 0 "
        'lssql &= " group by shortagreement_no,chq_no"
        lssql &= " order by chq_batchslno,packet_gnsarefno "

        gpPopGridView(dgvsummary, lssql, gOdbcConn)
        txtchqno.Focus()
    End Sub

    Private Sub frmmanualbatchentry_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        dgvsummary.Top = gbDetails.Height + 50
        dgvsummary.Width = Me.Width - 20
        dgvsummary.Height = Me.Height - gbDetails.Height - 100
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick

    End Sub
End Class