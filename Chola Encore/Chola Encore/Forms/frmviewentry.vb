Public Class frmviewentry
    Dim lnPacketgid As Long
    Dim lsPktNo As String
    Dim lsPktMode As String
    Dim lssql As String

    Public Sub New(ByVal PktId As Long, ByVal PktNo As String, ByVal PktMode As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lnPacketgid = PktId
        lsPktNo = PktNo
        lsPktMode = PktMode
    End Sub
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub frmviewentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lsTxt As String

        lblPkt.Text = "Packet No : " & lsPktNo & "    Mode : " & lsPktMode

        lssql = ""
        lssql &= " select distinct packet_gid from chola_trn_tpacket as a "
        lssql &= " inner join chola_trn_tpdcfilehead as b on b.head_packet_gid = a.packet_gid "
        lssql &= " inner join chola_mst_tpdcmode as c on c.pdc_mode = b.head_mode and c.new_pdc_mode <> a.packet_mode "
        lssql &= " where a.packet_gid = " & lnPacketgid

        lsTxt = gfExecuteScalar(lssql, gOdbcConn)

        If lsTxt <> "" Then
            lblPkt.Text = lblPkt.Text & "   (Packet Pay Mode Disc)"
            lblPkt.ForeColor = Color.Red
        Else
            lblPkt.ForeColor = Color.Blue
        End If

        Call LoadData()
    End Sub

    Private Sub LoadData()
        Dim dttable As DataTable
        Dim objdt As New DataTable
        Dim drpdc As Odbc.OdbcDataReader
        Dim liGreen As Integer = 0
        Dim liRed As Integer = 0
        Dim lsPdcMode As String
        Dim lnFinoneId As Integer
        Dim lsChqNo As String
        Dim lnChqAmt As Double
        Dim lsChqDate As String
        Dim lbMatch As Boolean

        objdt.Columns.Add("Sl No")
        objdt.Columns.Add("Type")
        objdt.Columns.Add("Agreement No")
        objdt.Columns.Add("GNSA REF No")
        objdt.Columns.Add("Chq No")
        objdt.Columns.Add("Chq Date")
        objdt.Columns.Add("Chq Amt")
        objdt.Columns.Add("Mode")
        objdt.Columns.Add("Flag")

        lssql = ""
        lssql &= " select packet_gnsarefno,agreement_no,shortagreement_no,"
        lssql &= " chq_no,if(chq_type=2,'',cast(date_format(chq_date,'%d-%b-%Y') as char)) as chq_date,chq_amount,"
        lssql &= " '' as chqentry_chqno,"
        lssql &= " '' as ecsemientry_emidate,0 as ecsemientry_amount,"
        lssql &= " entry_gid,0 as chqentry_gid,0 as ecsemientry_gid,chq_pdc_gid as finone_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tpdcentry on packet_gid=chq_packet_gid "
        lssql &= " where packet_gid =" & lnPacketgid

        lssql &= " union all "
        lssql &= " select packet_gnsarefno,agreement_no,shortagreement_no,"
        lssql &= " '' as chq_no,'' as chq_date,0 as chq_amount,"
        lssql &= " chqentry_chqno,"
        lssql &= " '' ecsemientry_emidate,0 as ecsemientry_amount,"
        lssql &= " 0 as entry_gid,chqentry_gid,0 as ecsemientry_gid,chqentry_pdc_gid as finone_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tspdcchqentry on packet_gid=chqentry_packet_gid "
        lssql &= " where packet_gid =" & lnPacketgid

        lssql &= " union all "
        lssql &= " select packet_gnsarefno,agreement_no,shortagreement_no,"
        lssql &= " '' as chq_no,cast(date_format(ecsemientry_emidate,'%d-%b-%Y') as char) as chq_date,0 as chq_amount,"
        lssql &= " '' as chqentry_chqno,"
        lssql &= " ecsemientry_emidate,ecsemientry_amount,"
        lssql &= " 0 as entry_gid,0 as chqentry_gid,ecsemientry_gid,ecsemientry_pdc_gid as finone_gid "
        lssql &= " from chola_trn_tpacket "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid "
        lssql &= " inner join chola_trn_tecsemientry on packet_gid=ecsemientry_packet_gid "
        lssql &= " where packet_gid =" & lnPacketgid

        dttable = GetDataTable(lssql)

        For i As Integer = 0 To dttable.Rows.Count - 1
            lsChqDate = ""
            lsChqNo = ""
            lnChqAmt = 0
            lsPdcMode = ""

            lnFinoneId = Val(dttable.Rows(i).Item("finone_gid").ToString)

            If dttable.Rows(i).Item("entry_gid") > 0 Then
                lsChqNo = dttable.Rows(i).Item("chq_no").ToString
                lsChqDate = dttable.Rows(i).Item("chq_date").ToString
                lnChqAmt = dttable.Rows(i).Item("chq_amount")

                If lsChqDate <> "" Then
                    lsPdcMode = "PDC"
                Else
                    lsPdcMode = "SPDC"
                End If
            ElseIf dttable.Rows(i).Item("chqentry_gid") > 0 Then
                lsPdcMode = "SPDC"

                lsChqNo = dttable.Rows(i).Item("chqentry_chqno").ToString
            ElseIf dttable.Rows(i).Item("ecsemientry_gid") > 0 Then
                lsPdcMode = "ECS"

                lsChqDate = dttable.Rows(i).Item("chq_date").ToString
                lnChqAmt = dttable.Rows(i).Item("ecsemientry_amount")
            End If

            objdt.Rows.Add()
            objdt.Rows(objdt.Rows.Count - 1).Item("sl No") = i + 1
            objdt.Rows(objdt.Rows.Count - 1).Item("Type") = "VMS-" & lsPdcMode
            objdt.Rows(objdt.Rows.Count - 1).Item("Agreement No") = dttable.Rows(i).Item("agreement_no").ToString
            objdt.Rows(objdt.Rows.Count - 1).Item("GNSA REF No") = dttable.Rows(i).Item("packet_gnsarefno").ToString
            objdt.Rows(objdt.Rows.Count - 1).Item("Chq No") = lsChqNo
            objdt.Rows(objdt.Rows.Count - 1).Item("Chq Date") = lsChqDate
            objdt.Rows(objdt.Rows.Count - 1).Item("Chq Amt") = Format(lnChqAmt, "0.00")
            objdt.Rows(objdt.Rows.Count - 1).Item("Mode") = lsPdcMode

            If lnFinoneId = 0 Then
                objdt.Rows(objdt.Rows.Count - 1).Item("Flag") = "False"
            Else
                objdt.Rows(objdt.Rows.Count - 1).Item("Flag") = "True"
            End If

            If lnFinoneId = 0 Then
                lssql = " select a.pdc_chqno,a.pdc_chqamount,if(upper(a.pdc_type)='EXTERNAL-SECURITY','',cast(date_format(a.pdc_chqdate,'%d-%b-%Y') as char)) as pdc_chqdate, "
                lssql &= " ifnull(b.new_pdc_type,a.pdc_type) as pdc_type,a.pdc_mode from chola_trn_tpdcfile as a "
                lssql &= " left join chola_mst_tpdctype as b on b.pdc_type = a.pdc_type "
                lssql &= " where a.chq_rec_slno=1 "
                lssql &= " and a.pdc_parentcontractno='" & dttable.Rows(i).Item("agreement_no").ToString & "'"

                If lsPdcMode = "ECS" And lsChqDate <> "" Then
                    lssql &= " and a.pdc_chqdate >= '" & Format(Format(CDate(lsChqDate), "yyyy-MM-01"), "yyyy-MM-dd") & "'"
                    lssql &= " and a.pdc_chqdate < '" & Format(Format(DateAdd(DateInterval.Month, 1, CDate(lsChqDate)), "yyyy-MM-01"), "yyyy-MM-dd") & "'"
                Else
                    If Val(lsChqNo) > 0 Then
                        lssql &= " and a.pdc_chqno = " & Val(lsChqNo)
                    Else
                        lssql &= " and 1 = 2 "
                    End If
                End If

                drpdc = gfExecuteQry(lssql, gOdbcConn)

                If drpdc.HasRows = False Then
                    If IsDate(dttable.Rows(i).Item("chq_date").ToString) Then
                        lssql = " select a.pdc_chqno,a.pdc_chqamount,if(upper(a.pdc_type)='EXTERNAL-SECURITY','',cast(date_format(a.pdc_chqdate,'%d-%b-%Y') as char)) as pdc_chqdate, "
                        lssql &= " ifnull(b.new_pdc_type,a.pdc_type) as pdc_type,a.pdc_mode from chola_trn_tpdcfile as a "
                        lssql &= " left join chola_mst_tpdctype as b on b.pdc_type = a.pdc_type "
                        lssql &= " where a.chq_rec_slno=1 "
                        lssql &= " and a.pdc_parentcontractno='" & dttable.Rows(i).Item("agreement_no").ToString & "'"
                        lssql &= " and a.pdc_chqdate ='" & Format(CDate(dttable.Rows(i).Item("chq_date").ToString), "yyyy-MM-dd") & "'"

                        drpdc = gfExecuteQry(lssql, gOdbcConn)
                    End If
                End If
            Else
                lssql = " select a.pdc_chqno,a.pdc_chqamount,if(upper(a.pdc_type)='EXTERNAL-SECURITY','',cast(date_format(a.pdc_chqdate,'%d-%b-%Y') as char)) as pdc_chqdate, "
                lssql &= " ifnull(b.new_pdc_type,a.pdc_type) as pdc_type,a.pdc_mode from chola_trn_tpdcfile as a "
                lssql &= " left join chola_mst_tpdctype as b on b.pdc_type = a.pdc_type "
                lssql &= " where a.pdc_gid = " & lnFinoneId & " "

                drpdc = gfExecuteQry(lssql, gOdbcConn)
            End If

            If drpdc.HasRows Then
                drpdc.Read()
                objdt.Rows.Add()
                objdt.Rows(objdt.Rows.Count - 1).Item("sl No") = i + 1
                objdt.Rows(objdt.Rows.Count - 1).Item("Type") = "FINONE-" & drpdc.Item("pdc_type").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("Agreement No") = dttable.Rows(i).Item("agreement_no").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("GNSA REF No") = dttable.Rows(i).Item("packet_gnsarefno").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("Chq No") = drpdc.Item("pdc_chqno").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("Chq Date") = drpdc.Item("pdc_chqdate").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("Chq Amt") = Format(drpdc.Item("pdc_chqamount"), "0.00")
                objdt.Rows(objdt.Rows.Count - 1).Item("Mode") = drpdc.Item("pdc_type").ToString

                If lnFinoneId = 0 Then
                    objdt.Rows(objdt.Rows.Count - 1).Item("Flag") = "False"
                Else
                    objdt.Rows(objdt.Rows.Count - 1).Item("Flag") = "True"
                End If
            End If
        Next

        dgvsummary.DataSource = objdt

        For i As Integer = 0 To dgvsummary.ColumnCount - 1
            dgvsummary.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next i

        For i As Integer = 0 To dgvsummary.RowCount - 1

            Dim row As DataGridViewRow = dgvsummary.Rows(i)

            If dgvsummary.RowCount - 1 = i Then
                If liRed = 0 Then
                    row.DefaultCellStyle.BackColor = Color.PaleVioletRed
                    liRed = 1
                ElseIf liRed = 1 Then
                    row.DefaultCellStyle.BackColor = Color.Red
                    liRed = 0
                End If
                Exit Sub
            End If

            Dim row1 As DataGridViewRow = dgvsummary.Rows(i + 1)

            If row.Cells("sl No").Value.ToString() = row1.Cells("sl No").Value.ToString() Then
                lsPdcMode = row.Cells("Mode").Value.ToString()
                lbMatch = False

                If row.Cells("Mode").Value.ToString() = row1.Cells("Mode").Value.ToString() Then
                    Select Case lsPdcMode
                        Case "PDC"
                            If row.Cells("Chq No").Value.ToString() = row1.Cells("Chq No").Value.ToString() And _
                                row.Cells("Chq Date").Value.ToString() = row1.Cells("Chq Date").Value.ToString() And _
                                row.Cells("Chq Amt").Value.ToString() = row1.Cells("Chq Amt").Value.ToString Then
                                lbMatch = True
                            End If
                        Case "SPDC"
                            If row.Cells("Chq No").Value.ToString() = row1.Cells("Chq No").Value.ToString() Then
                                lbMatch = True
                            End If
                        Case "ECS"
                            If row.Cells("Chq Date").Value.ToString() = row1.Cells("Chq Date").Value.ToString() And _
                                row.Cells("Chq Amt").Value.ToString() = row1.Cells("Chq Amt").Value.ToString Then
                                lbMatch = True
                            End If
                    End Select
                Else
                    lbMatch = False
                End If

                If lbMatch = True Then
                    If liGreen = 0 Then
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                        row1.DefaultCellStyle.BackColor = Color.LightGreen
                        liGreen = 1
                    ElseIf liGreen = 1 Then
                        row.DefaultCellStyle.BackColor = Color.GreenYellow
                        row1.DefaultCellStyle.BackColor = Color.GreenYellow
                        liGreen = 0
                    End If
                Else
                    If liRed = 0 Then
                        row.DefaultCellStyle.BackColor = Color.PaleVioletRed
                        row1.DefaultCellStyle.BackColor = Color.PaleVioletRed
                        liRed = 1
                    ElseIf liRed = 1 Then
                        row.DefaultCellStyle.BackColor = Color.Red
                        row1.DefaultCellStyle.BackColor = Color.Red
                        liRed = 0
                    End If
                End If
                i += 1
            Else
                If liRed = 0 Then
                    row.DefaultCellStyle.BackColor = Color.PaleVioletRed
                    liRed = 1
                ElseIf liRed = 1 Then
                    row.DefaultCellStyle.BackColor = Color.Red
                    liRed = 0
                End If
            End If
        Next
    End Sub

    Private Sub LoadDataOld()
        Dim dttable As DataTable
        Dim objdt As New DataTable
        Dim drpdc As Odbc.OdbcDataReader
        Dim liGreen As Integer = 0
        Dim liRed As Integer = 0

        objdt.Columns.Add("Sl No")
        objdt.Columns.Add("Type")
        objdt.Columns.Add("Agreement No")
        objdt.Columns.Add("GNSA REF No")
        objdt.Columns.Add("cheque No")
        objdt.Columns.Add("cheque Date")
        objdt.Columns.Add("cheque Amount")

        lssql = " select packet_gnsarefno,agreement_no,chq_pdc_gid,chq_no,if(chq_type=2,'',cast(date_format(chq_date,'%d-%m-%Y') as char)) as chq_date,chq_amount,shortagreement_no "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=chq_agreement_gid "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " where chq_packet_gid =" & lnPacketgid
        dttable = GetDataTable(lssql)

        For i As Integer = 0 To dttable.Rows.Count - 1
            objdt.Rows.Add()
            objdt.Rows(objdt.Rows.Count - 1).Item("sl No") = i + 1
            objdt.Rows(objdt.Rows.Count - 1).Item("Type") = "VMS"
            objdt.Rows(objdt.Rows.Count - 1).Item("Agreement No") = dttable.Rows(i).Item("agreement_no").ToString
            objdt.Rows(objdt.Rows.Count - 1).Item("GNSA REF No") = dttable.Rows(i).Item("packet_gnsarefno").ToString
            objdt.Rows(objdt.Rows.Count - 1).Item("cheque No") = dttable.Rows(i).Item("Chq_no").ToString
            objdt.Rows(objdt.Rows.Count - 1).Item("cheque Date") = dttable.Rows(i).Item("chq_date").ToString
            objdt.Rows(objdt.Rows.Count - 1).Item("cheque Amount") = dttable.Rows(i).Item("chq_amount").ToString


            lssql = " select pdc_chqno,pdc_chqamount,if(upper(pdc_type)='EXTERNAL-SECURITY','',cast(date_format(pdc_chqdate,'%d-%m-%Y') as char)) as pdc_chqdate "
            lssql &= " from chola_trn_tpdcfile "
            lssql &= " where chq_rec_slno=1 "
            lssql &= " and pdc_parentcontractno='" & dttable.Rows(i).Item("agreement_no").ToString & "'"
            lssql &= " and pdc_chqno = " & dttable.Rows(i).Item("Chq_no").ToString & ""
            drpdc = gfExecuteQry(lssql, gOdbcConn)

            If drpdc.HasRows Then
                drpdc.Read()
                objdt.Rows.Add()
                objdt.Rows(objdt.Rows.Count - 1).Item("sl No") = i + 1
                objdt.Rows(objdt.Rows.Count - 1).Item("Type") = "FINONE"
                objdt.Rows(objdt.Rows.Count - 1).Item("Agreement No") = dttable.Rows(i).Item("agreement_no").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("GNSA REF No") = dttable.Rows(i).Item("packet_gnsarefno").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("cheque No") = drpdc.Item("pdc_chqno").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("cheque Date") = drpdc.Item("pdc_chqdate").ToString
                objdt.Rows(objdt.Rows.Count - 1).Item("cheque Amount") = drpdc.Item("pdc_chqamount").ToString
            Else
                If IsDate(dttable.Rows(i).Item("chq_date").ToString) Then
                    lssql = " select pdc_chqno,pdc_chqamount,if(upper(pdc_type)='EXTERNAL-SECURITY','',cast(date_format(pdc_chqdate,'%d-%m-%Y') as char)) as pdc_chqdate "
                    lssql &= " from chola_trn_tpdcfile "
                    lssql &= " where chq_rec_slno=1 "
                    lssql &= " and pdc_parentcontractno='" & dttable.Rows(i).Item("agreement_no").ToString & "'"
                    lssql &= " and pdc_chqdate ='" & Format(CDate(dttable.Rows(i).Item("chq_date").ToString), "yyyy-MM-dd") & "'"
                    drpdc = gfExecuteQry(lssql, gOdbcConn)
                    If drpdc.HasRows Then
                        drpdc.Read()
                        objdt.Rows.Add()
                        objdt.Rows(objdt.Rows.Count - 1).Item("sl No") = i + 1
                        objdt.Rows(objdt.Rows.Count - 1).Item("Type") = "FINONE"
                        objdt.Rows(objdt.Rows.Count - 1).Item("Agreement No") = dttable.Rows(i).Item("agreement_no").ToString
                        objdt.Rows(objdt.Rows.Count - 1).Item("GNSA REF No") = dttable.Rows(i).Item("packet_gnsarefno").ToString
                        objdt.Rows(objdt.Rows.Count - 1).Item("cheque No") = drpdc.Item("pdc_chqno").ToString
                        objdt.Rows(objdt.Rows.Count - 1).Item("cheque Date") = drpdc.Item("pdc_chqdate").ToString
                        objdt.Rows(objdt.Rows.Count - 1).Item("cheque Amount") = drpdc.Item("pdc_chqamount").ToString
                    End If
                End If
            End If
        Next

        dgvsummary.DataSource = objdt

        For i As Integer = 0 To dgvsummary.RowCount - 1

            Dim row As DataGridViewRow = dgvsummary.Rows(i)

            If dgvsummary.RowCount - 1 = i Then
                If liRed = 0 Then
                    row.DefaultCellStyle.BackColor = Color.OrangeRed
                    liRed = 1
                ElseIf liRed = 1 Then
                    row.DefaultCellStyle.BackColor = Color.Red
                    liRed = 0
                End If
                Exit Sub
            End If

            Dim row1 As DataGridViewRow = dgvsummary.Rows(i + 1)

            If row.Cells("sl No").Value.ToString() = row1.Cells("sl No").Value.ToString() Then
                If row.Cells("cheque No").Value.ToString() = row1.Cells("cheque No").Value.ToString() And _
                    row.Cells("cheque Date").Value.ToString() = row1.Cells("cheque Date").Value.ToString() And _
                    row.Cells("cheque Amount").Value.ToString() = row1.Cells("cheque Amount").Value.ToString Then

                    If liGreen = 0 Then
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                        row1.DefaultCellStyle.BackColor = Color.LightGreen
                        liGreen = 1
                    ElseIf liGreen = 1 Then
                        row.DefaultCellStyle.BackColor = Color.GreenYellow
                        row1.DefaultCellStyle.BackColor = Color.GreenYellow
                        liGreen = 0
                    End If
                Else
                    If liRed = 0 Then
                        row.DefaultCellStyle.BackColor = Color.OrangeRed
                        row1.DefaultCellStyle.BackColor = Color.OrangeRed
                        liRed = 1
                    ElseIf liRed = 1 Then
                        row.DefaultCellStyle.BackColor = Color.Red
                        row1.DefaultCellStyle.BackColor = Color.Red
                        liRed = 0
                    End If
                End If
                i += 1
            Else
                If liRed = 0 Then
                    row.DefaultCellStyle.BackColor = Color.OrangeRed
                    liRed = 1
                ElseIf liRed = 1 Then
                    row.DefaultCellStyle.BackColor = Color.Red
                    liRed = 0
                End If
            End If
        Next
    End Sub

    Private Sub frmviewentry_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        lblPkt.Top = 6
        lblPkt.Left = 6

        With dgvsummary
            .Left = 6
            .Top = lblPkt.Top + lblPkt.Height + 6
            .Width = Me.Width - 20
            .Height = Me.Height - 90

            btnExport.Top = dgvsummary.Top + dgvsummary.Height + 6
            btnExport.Left = (Me.Width - btnExport.Width) \ 2
        End With
    End Sub
End Class