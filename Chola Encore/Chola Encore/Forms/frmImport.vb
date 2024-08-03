Imports System.Data.Odbc
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Public Class frmImport

    Inherits System.Windows.Forms.Form
#Region "Local Declaration"
    Dim lnImportFlag As Integer
    Dim fsSql As String
    Dim lnResult As Long

    Dim fsFilePath As String = ""
    Dim fsFileName As String
    Dim fExcelDatatable As New DataTable

#End Region
    Public Sub New(ByVal importFlag)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        lnImportFlag = importFlag
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        'User Selected Browse file 
        With OpenFileDialog1

            .Filter = "Excel Files|*.xls|Text Files|*.*|DBF Files|*.dbf|Text Files|*.txt|Word Files|*.doc"
            .Title = "Select Files to Import"
            .RestoreDirectory = True
            .ShowDialog()
            If .FileName <> "" And .FileName <> "OpenFileDialog1" Then
                txtFileName.Text = .FileName
            End If
            .FileName = ""
        End With

        If (InStr(1, LCase(Trim(txtFileName.Text)), ".xls")) > 0 Then
            cboSheetName.Enabled = True

            Call LoadSheet()

            cboSheetName.Focus()
        Else
            cboSheetName.Enabled = False
        End If

        Exit Sub

    End Sub

    Private Sub LoadSheet()
        Dim objXls As New Excel.Application
        Dim objBook As Excel.Workbook

        If Trim(txtFileName.Text) <> "" Then
            If File.Exists(txtFileName.Text) Then
                objBook = objXls.Workbooks.Open(txtFileName.Text)
                cboSheetName.Items.Clear()
                For i As Integer = 1 To objXls.ActiveWorkbook.Worksheets.Count
                    cboSheetName.Items.Add(objXls.ActiveWorkbook.Worksheets(i).Name)
                Next i
                objXls.Workbooks.Close()

            End If
        End If

        objXls.Workbooks.Close()

        GC.Collect()
        GC.WaitForPendingFinalizers()

        objXls.Quit()

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(objXls)
        objXls = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            MyBase.Close()
        End If
    End Sub

    Private Sub frmImportP2P_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case lnImportFlag
            Case 1
                Me.Text = "Import Chq No Query File"
            Case 2
                Me.Text = "Import Swap Short Agreement No Query File"
            Case 3
                Me.Text = "Import Swap Agreement No"
            Case 4
                Me.Text = "Import Packet Pay Mode Updation"
            Case 5
                Me.Text = "Import Chq No and Amount Query File"
        End Select
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            If txtFileName.Text = "" Then
                MsgBox("Select File Name", MsgBoxStyle.Information, gProjectName)
                txtFileName.Focus()
                Exit Sub
            End If

            Panel1.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            fsFilePath = txtFileName.Text.Trim
            fsFileName = fsFilePath.Substring(fsFilePath.LastIndexOf("\") + 1)

            If txtFileName.Text <> "" Then
                If cboSheetName.Text <> "" Then

                    Call FormatSheet(txtFileName.Text, cboSheetName.Text)

                    Select Case lnImportFlag
                        Case 1
                            Call ChequeNoQry()
                        Case 2
                            Call ShortAgreementNoQry()
                        Case 3
                            Call ImpSwapShortAgreementFile()
                        Case 4
                            Call ImpPacketPaymodeUpdation()
                        Case 5
                            Call ChqNoAmtQry()
                    End Select
                Else
                    MsgBox("Select Sheet Name!", MsgBoxStyle.Information, gProjectName)
                    Exit Sub
                End If
            Else
                MsgBox("Select File to Import!", MsgBoxStyle.Information, gProjectName)
                Exit Sub
            End If

            Panel1.Enabled = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ChequeNoQry()
        Dim fsCorrectFormat As String = ""
        Dim lnTotalRecords As Integer = 0

        Dim lsFileName As String = ""

        Dim lsChqNo As String = ""

        Dim i As Integer

        lsFileName = QuoteFilter(fsFileName)

        Try
            fExcelDatatable = gpExcelDataset("select * from [" & cboSheetName.Text & "$]", fsFilePath)

            If fExcelDatatable.Columns(0).ColumnName.ToUpper.Trim <> "CHQNO" Then
                MsgBox("Excel Column Setting is wrong, Correct format is " & vbCrLf & "CHQNO", vbOKOnly + vbExclamation, gProjectName)
                Exit Sub
            End If

            btnImport.Enabled = False

            With fExcelDatatable
                i = 0

                fsSql = " delete from chola_rpt_tchqqry where import_by = '" & gUserName & "' "
                gfInsertQry(fsSql, gOdbcConn)

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        Me.Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        lsChqNo = QuoteFilter(.Item("CHQNO").ToString().Trim)
                        If Val(lsChqNo) = 0 Then lsChqNo = ""
                        'If lsChqNo <> "" Then lsChqNo = Format(Val(lsChqNo), StrDup(6, "0"))

                        If lsChqNo <> "" Then
                            fsSql = ""
                            fsSql &= " select chqqry_gid from chola_rpt_tchqqry "
                            fsSql &= " where chq_no = '" & lsChqNo & "' "
                            fsSql &= " and import_by = '" & gUserName & "' "

                            lnResult = Val(gfExecuteScalar(fsSql, gOdbcConn))

                            If lnResult = 0 Then
                                fsSql = " insert into chola_rpt_tchqqry (chq_no, import_by, import_date "
                                fsSql &= " ) values ( "
                                fsSql &= " '" & lsChqNo & "',"
                                fsSql &= " '" & gUserName & "',"

                                fsSql &= " SYSDATE())"

                                lnResult = gfInsertQry(fsSql, gOdbcConn)

                                lnTotalRecords += 1

                                frmMain.lblstatus.Text = " Imported Records Count:" & lnTotalRecords
                                Application.DoEvents()
                            End If
                        End If
                    End With

                    i += 1
                End While
            End With

            MsgBox(lnTotalRecords & " record(s) imported successfully ! ", MsgBoxStyle.Information, gProjectName)

            btnImport.Enabled = True
            Application.DoEvents()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gProjectName)
            btnImport.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ChqNoAmtQry()
        Dim fsCorrectFormat As String = ""
        Dim lnTotalRecords As Integer = 0

        Dim lsFileName As String = ""

        Dim lsChqNo As String = ""
        Dim lnAmt As Double = 0

        Dim i As Integer

        lsFileName = QuoteFilter(fsFileName)

        Try
            fExcelDatatable = gpExcelDataset("select * from [" & cboSheetName.Text & "$]", fsFilePath)

            If fExcelDatatable.Columns(0).ColumnName.ToUpper.Trim <> "CHQNO" Then
                MsgBox("Excel Column Setting is wrong, Correct format is " & vbCrLf & "CHQNO", vbOKOnly + vbExclamation, gProjectName)
                Exit Sub
            End If

            If fExcelDatatable.Columns(1).ColumnName.ToUpper.Trim <> "AMOUNT" Then
                MsgBox("Excel Column Setting is wrong, Correct format is " & vbCrLf & "AMOUNT", vbOKOnly + vbExclamation, gProjectName)
                Exit Sub
            End If

            btnImport.Enabled = False

            With fExcelDatatable
                i = 0

                fsSql = " delete from chola_rpt_tchqqry where import_by = '" & gUserName & "' "
                gfInsertQry(fsSql, gOdbcConn)

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        Me.Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        lsChqNo = QuoteFilter(.Item("CHQNO").ToString().Trim)
                        If Val(lsChqNo) = 0 Then lsChqNo = ""
                        If lsChqNo <> "" Then lsChqNo = Format(Val(lsChqNo), "000000")

                        lnAmt = Math.Round(Val(QuoteFilter(.Item("AMOUNT").ToString().Trim)), 2)

                        If lsChqNo <> "" And lnAmt > 0 Then
                            fsSql = ""
                            fsSql &= " select chqqry_gid from chola_rpt_tchqqry "
                            fsSql &= " where chq_no = '" & lsChqNo & "' "
                            fsSql &= " and import_by = '" & gUserName & "' "

                            lnResult = Val(gfExecuteScalar(fsSql, gOdbcConn))

                            If lnResult = 0 Then
                                fsSql = " insert into chola_rpt_tchqqry (chq_no,chq_amount,import_by, import_date "
                                fsSql &= " ) values ( "
                                fsSql &= " '" & lsChqNo & "',"
                                fsSql &= " " & lnAmt & ","
                                fsSql &= " '" & gUserName & "',"

                                fsSql &= " SYSDATE())"

                                lnResult = gfInsertQry(fsSql, gOdbcConn)

                                lnTotalRecords += 1

                                frmMain.lblstatus.Text = " Imported Records Count:" & lnTotalRecords
                                Application.DoEvents()
                            End If
                        End If
                    End With

                    i += 1
                End While
            End With

            MsgBox(lnTotalRecords & " record(s) imported successfully ! ", MsgBoxStyle.Information, gProjectName)

            btnImport.Enabled = True
            Application.DoEvents()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gProjectName)
            btnImport.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub ShortAgreementNoQry()
        Dim fsCorrectFormat As String = ""
        Dim lnTotalRecords As Integer = 0

        Dim lsFileName As String = ""

        Dim lsShortAgmntNo As String = ""

        Dim i As Integer
        Dim lsSql As String
        Dim frm As frmQuickView

        lsFileName = QuoteFilter(fsFileName)

        Try
            fExcelDatatable = gpExcelDataset("select * from [" & cboSheetName.Text & "$]", fsFilePath)

            If fExcelDatatable.Columns(0).ColumnName.ToUpper.Trim <> "SHORT AGREEMENT NO" Then
                MsgBox("Excel Column Setting is wrong, Correct format is " & vbCrLf & "SHORT AGREEMENT NO", vbOKOnly + vbExclamation, gProjectName)
                Exit Sub
            End If

            btnImport.Enabled = False

            With fExcelDatatable
                i = 0

                fsSql = " delete from chola_rpt_tagreementqry where import_by = '" & gUserName & "' "
                gfInsertQry(fsSql, gOdbcConn)

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        Me.Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        lsShortAgmntNo = Format(Val(QuoteFilter(.Item("SHORT AGREEMENT NO").ToString().Trim)), "000000")
                        If Val(lsShortAgmntNo) = 0 Then lsShortAgmntNo = ""

                        If lsShortAgmntNo <> "" Then
                            fsSql = ""
                            fsSql &= " select agmntqry_gid from chola_rpt_tagreementqry "
                            fsSql &= " where short_agreement_no = '" & lsShortAgmntNo & "' "
                            fsSql &= " and import_by = '" & gUserName & "' "

                            lnResult = Val(gfExecuteScalar(fsSql, gOdbcConn))

                            If lnResult = 0 Then
                                fsSql = " insert into chola_rpt_tagreementqry (short_agreement_no, import_by, import_date "
                                fsSql &= " ) values ( "
                                fsSql &= " '" & lsShortAgmntNo & "',"
                                fsSql &= " '" & gUserName & "',"

                                fsSql &= " SYSDATE())"

                                lnResult = gfInsertQry(fsSql, gOdbcConn)

                                lnTotalRecords += 1

                                frmMain.lblstatus.Text = " Imported Records Count:" & lnTotalRecords
                                Application.DoEvents()
                            End If
                        End If
                    End With

                    i += 1
                End While
            End With

            MsgBox(lnTotalRecords & " record(s) imported successfully ! ", MsgBoxStyle.Information, gProjectName)

            lsSql = ""
            lsSql &= " select q.short_agreement_no as 'Short Agreement No',a.agreement_no as 'AgreementNo',p.packet_gnsarefno as 'GNSAREF',p.packet_mode as 'Mode',"
            lsSql &= " e.almaraentry_cupboardno as 'Cupboard No',e.almaraentry_shelfno as 'Shelf No',e.almaraentry_boxno as 'Box No' "
            lsSql &= " from chola_rpt_tagreementqry as q "
            lsSql &= " left join chola_mst_tagreement as a on a.agreement_no = q.agreement_no "
            lsSql &= " left join chola_trn_tpacket as p on p.packet_agreement_gid = a.agreement_gid "
            lsSql &= " and p.packet_status & " & (GCPACKETCHEQUEENTRY Or GCPACKETCHEQUEREENTRY Or GCPACKETVAULTED) & " > 0 "
            lsSql &= " and p.packet_status & " & (GCPACKETRETRIEVAL Or GCIPACKETPULLOUT Or GCPKTOLDSWAP) & " = 0 "
            lsSql &= " left join chola_trn_almaraentry as e on e.almaraentry_gid = p.packet_box_gid "
            lsSql &= " where q.import_by = '" & gUserName & "' "

            frm = New frmQuickView(gOdbcConn, lsSql)
            frm.ShowDialog()

            btnImport.Enabled = True
            Application.DoEvents()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gProjectName)
            btnImport.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Sub ImpSwapShortAgreementFile()
        Dim i As Integer
        Dim lsFldName(3) As String
        Dim lsFldFormat As String = ""
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lsFileName As String = ""
        Dim lnFileId As Long

        Dim lnAgmntId As Long = 0
        Dim lsSwapDate As String = ""
        Dim lsAgmntNo As String = ""
        Dim lsShortAgmntNo As String = ""

        Dim lbInsertFlag As Boolean = False
        Dim lnResult As Long
        Dim lsErrFileName As String = ""
        Dim lsDiscRemark As String = ""
        Dim ds As New DataSet

        lsFldName(1) = "SNO"
        lsFldName(2) = "SWAP DATE"
        lsFldName(3) = "AGREEMENT NO"

        lsFileName = QuoteFilter(fsFileName)

        Try
            '---------------------------------
            Try
                fExcelDatatable = gpExcelDataset("select * from [" & cboSheetName.Text & "$]", fsFilePath)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try

            For i = 1 To 3
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 3
                If lsFldName(i).Trim <> fExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    MsgBox("Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & fExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat, vbOKOnly + vbExclamation, gProjectName)
                    Exit Sub
                End If
            Next i

            'File Name Duplicate
            fsSql = ""
            fsSql &= " select swapfile_gid from chola_trn_tswapfile "
            fsSql &= " where 1=1 "
            fsSql &= " and file_name = '" & lsFileName & "'"
            fsSql &= " and sheet_name='" & cboSheetName.Text & "'"

            lnFileId = Val(gfExecuteScalar(fsSql, gOdbcConn))

            If lnFileId > 0 Then
                MsgBox("File already imported !", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gProjectName)
                txtFileName.Focus()
                Exit Sub
            Else
                fsSql = ""
                fsSql &= " insert into chola_trn_tswapfile "
                fsSql &= " (import_date, file_name, sheet_name,import_by) values ("
                fsSql &= " sysdate(),"
                fsSql &= " '" & lsFileName & "','" & cboSheetName.Text.Trim & "',"
                fsSql &= " '" & gUserName & "')"

                lnResult = gfInsertQry(fsSql, gOdbcConn)

                fsSql = " select max(swapfile_gid) from chola_trn_tswapfile "
                lnFileId = gfExecuteScalar(fsSql, gOdbcConn)
            End If

            lsErrFileName = gsReportPath & "err.txt"
            If File.Exists(lsErrFileName) = True Then File.Delete(lsErrFileName)

            Call FileOpen(1, lsErrFileName, OpenMode.Output)
            Call PrintLine(1, "SNo;Swap Date;Short Agreement No;Error Desc")

            btnImport.Enabled = False

            With fExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        Me.Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        lbInsertFlag = True
                        lsDiscRemark = ""

                        lsSwapDate = QuoteFilter(.Item("SWAP DATE").ToString)
                        lsAgmntNo = QuoteFilter(.Item("AGREEMENT NO").ToString)

                        If IsDate(lsSwapDate) = True Then
                            lsSwapDate = Format(CDate(lsSwapDate), "yyyy-MM-dd")

                            If DateDiff(DateInterval.Day, Now, CDate(lsSwapDate)) > 0 Then
                                lbInsertFlag = False
                                lsDiscRemark &= "Future Swap Date,"
                            ElseIf DateDiff(DateInterval.Day, CDate(lsSwapDate), Now) > 10 Then
                                lbInsertFlag = False
                                lsDiscRemark &= "Old Swap Date More Than 10 Days Old,"
                            End If
                        Else
                            lbInsertFlag = False
                            lsDiscRemark &= "Invalid Swap Date,"
                        End If

                        If lsAgmntNo = "" Then
                            lbInsertFlag = False
                            lsDiscRemark &= "Blank Agreement No,"
                        End If

                        ' Find agreement_gid
                        lnAgmntId = 0
                        lsShortAgmntNo = ""

                        fsSql = ""
                        fsSql &= " select agreement_gid,shortagreement_no from chola_mst_tagreement "
                        fsSql &= " where agreement_no= '" & lsAgmntNo & "' "

                        Call gpDataSet(fsSql, "agreement", gOdbcConn, ds)

                        With ds.Tables("agreement")
                            If .Rows.Count = 1 Then
                                lnAgmntId = .Rows(0).Item("agreement_gid")
                                lsShortAgmntNo = .Rows(0).Item("shortagreement_no").ToString
                            End If

                            .Rows.Clear()
                        End With

                        If lnAgmntId = 0 Then
                            lbInsertFlag = False
                            lsDiscRemark &= "Invalid Short Agreement No,"
                        End If

                        If lbInsertFlag = True Then
                            ' Duplicate check
                            fsSql = ""
                            fsSql &= " select agreement_gid from chola_trn_tswap "
                            fsSql &= " where agreement_gid = '" & lnAgmntId & "' "
                            fsSql &= " and swap_date = '" & lsSwapDate & "' "

                            lnResult = Val(gfExecuteScalar(fsSql, gOdbcConn))

                            If lnResult > 0 Then
                                lbInsertFlag = False
                                lsDiscRemark &= "Duplicate Record,"
                            End If
                        End If

                        If lbInsertFlag = True Then
                            fsSql = ""
                            fsSql &= " insert into chola_trn_tswap (swapfile_gid,swap_date,shortagreement_no,agreement_gid) values ("
                            fsSql &= " '" & lnFileId & "',"
                            fsSql &= " '" & lsSwapDate & "',"
                            fsSql &= " '" & lsShortAgmntNo & "',"
                            fsSql &= " '" & lnAgmntId & "')"

                            lnResult = gfInsertQry(fsSql, gOdbcConn)

                            c += 1

                            frmMain.lblstatus.Text = "Imported Records Count : " & c
                            Application.DoEvents()
                        Else
                            Call PrintLine(1, (i + 1).ToString & ";" & lsSwapDate & ";" & lsShortAgmntNo & ";" & lsDiscRemark)
                            d += 1
                            frmMain.lblstatus.Text = "Error Records Count : " & d
                            Application.DoEvents()
                        End If
                    End With

                    i += 1
                End While
            End With

            Call FileClose(1)

            MsgBox("Out of " & i & " record(s) " & c & " record(s) imported successfully ! ", MsgBoxStyle.Information, gProjectName)

            If d > 0 Then
                MsgBox(d & " record(s) failed to import !", MsgBoxStyle.Critical, gProjectName)
                Call gpOpenFile(lsErrFileName)
            End If

            btnImport.Enabled = True
            Application.DoEvents()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gProjectName)
            btnImport.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Sub ImpPacketPaymodeUpdation()
        Dim i As Integer
        Dim lsFldName(3) As String
        Dim lsFldFormat As String = ""
        Dim c As Integer = 0
        Dim d As Integer = 0

        Dim lsFileName As String = ""
        Dim lnFileId As Long

        Dim lsGnsaRefNo As String = ""
        Dim lsPayMode As String = ""
        Dim lnPktId As Long = 0

        Dim lbInsertFlag As Boolean = False
        Dim lnResult As Long
        Dim lsErrFileName As String = ""
        Dim lsDiscRemark As String = ""

        lsFldName(1) = "SNO"
        lsFldName(2) = "GNSA REF NO"
        lsFldName(3) = "PAY MODE"

        lsFileName = QuoteFilter(fsFileName)

        Try
            '---------------------------------
            Try
                fExcelDatatable = gpExcelDataset("select * from [" & cboSheetName.Text & "$]", fsFilePath)
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try

            For i = 1 To 3
                lsFldFormat &= lsFldName(i) & "|"
            Next

            For i = 1 To 3
                If lsFldName(i).Trim <> fExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim Then
                    MsgBox("Excel Column Setting is wrong (" & i & ")" & vbCrLf & vbCrLf _
                    & lsFldName(i).ToUpper.Trim & " : " & fExcelDatatable.Columns(i - 1).ColumnName.ToUpper.Trim & ":" & vbCrLf & vbCrLf _
                    & "Correct format is " & vbCrLf & vbCrLf & lsFldFormat, vbOKOnly + vbExclamation, gProjectName)
                    Exit Sub
                End If
            Next i

            'File Name Duplicate
            fsSql = ""
            fsSql &= " select swapfile_gid from chola_trn_tswapfile "
            fsSql &= " where 1=1 "
            fsSql &= " and file_name = '" & lsFileName & "'"
            fsSql &= " and sheet_name='" & cboSheetName.Text & "'"

            lnFileId = Val(gfExecuteScalar(fsSql, gOdbcConn))

            If lnFileId > 0 Then
                MsgBox("File already imported !", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, gProjectName)
                txtFileName.Focus()
                Exit Sub
            Else
                fsSql = ""
                fsSql &= " insert into chola_trn_tswapfile "
                fsSql &= " (import_date, file_name, sheet_name,import_by) values ("
                fsSql &= " sysdate(),"
                fsSql &= " '" & lsFileName & "','" & cboSheetName.Text.Trim & "',"
                fsSql &= " '" & gUserName & "')"

                lnResult = gfInsertQry(fsSql, gOdbcConn)

                fsSql = " select max(swapfile_gid) from chola_trn_tswapfile "
                lnFileId = gfExecuteScalar(fsSql, gOdbcConn)
            End If

            lsErrFileName = gsReportPath & "err.txt"
            If File.Exists(lsErrFileName) = True Then File.Delete(lsErrFileName)

            Call FileOpen(1, lsErrFileName, OpenMode.Output)
            Call PrintLine(1, "SNo;Gnsa Ref No;Pay Mode;Error Desc")

            btnImport.Enabled = False

            With fExcelDatatable
                i = 0

                While i <= .Rows.Count - 1
                    With .Rows(i)
                        Me.Cursor = Cursors.WaitCursor
                        Application.DoEvents()

                        lbInsertFlag = True
                        lsDiscRemark = ""

                        lsGnsaRefNo = QuoteFilter(.Item("GNSA REF NO").ToString)
                        lsPayMode = QuoteFilter(.Item("PAY MODE").ToString).ToUpper

                        fsSql = ""
                        fsSql &= " select packet_gid from chola_trn_tpacket "
                        fsSql &= " where packet_gnsarefno='" & lsGnsaRefNo & "'"

                        lnPktId = Val(gfExecuteScalar(fsSql, gOdbcConn))

                        If lsGnsaRefNo = "" Then
                            lbInsertFlag = False
                            lsDiscRemark &= "Blank gnsa ref no,"
                        ElseIf lnPktId = 0 Then
                            lbInsertFlag = False
                            lsDiscRemark &= "Invalid gnsa ref no,"
                        End If

                        If lsPayMode = "" Then
                            lbInsertFlag = False
                            lsDiscRemark &= "Blank pay mode,"
                        Else
                            Select Case lsPayMode
                                Case "PDC", "SPDC"
                                Case Else
                                    lbInsertFlag = False
                                    lsDiscRemark &= "Invalid pay mode,"
                            End Select
                        End If

                        If lbInsertFlag = True Then
                            fsSql = ""
                            fsSql &= " update chola_trn_tpacket set "
                            fsSql &= " packet_mode = '" & lsPayMode & "' "
                            fsSql &= " where packet_gid = " & lnPktId & " "

                            lnResult = gfInsertQry(fsSql, gOdbcConn)

                            c += 1

                            frmMain.lblstatus.Text = "Imported Records Count : " & c
                            Application.DoEvents()
                        Else
                            Call PrintLine(1, (i + 1).ToString & ";" & lsGnsaRefNo & ";" & lsPayMode & ";" & lsDiscRemark)
                            d += 1
                            frmMain.lblstatus.Text = "Error Records Count : " & d
                            Application.DoEvents()
                        End If
                    End With

                    i += 1
                End While
            End With

            Call FileClose(1)

            MsgBox("Out of " & i & " record(s) " & c & " record(s) imported successfully ! ", MsgBoxStyle.Information, gProjectName)

            If d > 0 Then
                MsgBox(d & " record(s) failed to import !", MsgBoxStyle.Critical, gProjectName)
                Call gpOpenFile(lsErrFileName)
            End If

            btnImport.Enabled = True
            Application.DoEvents()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, gProjectName)
            btnImport.Enabled = True
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub
End Class