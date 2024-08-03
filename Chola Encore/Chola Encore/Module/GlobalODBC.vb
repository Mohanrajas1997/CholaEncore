Imports System.IO
Imports System.IO.FileStream
Imports System.Data.Odbc
Imports System.Data
Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Module GlobalODBC
#Region "Global Declaration"

    Public Const GCZERO As Integer = 0

    'Inward
    Public Const GCNEWFILE As Integer = 1
    Public Const GCRECEIVED As Integer = 2
    Public Const GCNOTRECEIVED As Integer = 4
    Public Const GCCOMBINED As Integer = 8

    'Packet
    Public Const GCINWARDENTRY As Integer = 1
    Public Const GCAUTHENTRY As Integer = 2
    Public Const GCREJECTENTRY As Integer = 4
    Public Const GCPACKETCHEQUEENTRY As Integer = 8
    Public Const GCPACKETCHEQUEREENTRY As Integer = 16
    Public Const GCPACKETVAULTED As Integer = 32
    Public Const GCIPACKETPULLOUT As Integer = 64
    Public Const GCGNSAREFCHANGED As Integer = 128
    Public Const GCAGREEMENTNOCHANGED As Integer = 256
    Public Const GCPACKETRETRIEVAL As Integer = 512
    Public Const GCPKTAUTHFINONE As Integer = 1024
    Public Const GCPKTREPROCESS As Integer = 2048
    Public Const GCPKTOLDSWAP As Integer = 4096
    Public Const GCPKTOLDSWAPCHQRCVD As Integer = 8192
    Public Const GCPKTOLDSWAPCHQTRAN As Integer = 16384
    Public Const GCPACKETBLOCK As Integer = 32768

    'Document Status
    Public Const GCNEW As Integer = 1
    Public Const GCENTRY As Integer = 2
    Public Const GCREENTRY As Integer = 4
    Public Const GCINPROCESS As Integer = 7
    Public Const GCCLOSURE As Integer = 8
    Public Const GCPACKETPULLOUT As Integer = 16
    Public Const GCPRESENTATIONPULLOUT As Integer = 32
    Public Const GCPRESENTATIONDE As Integer = 64
    Public Const GCDESPATCH As Integer = 128
    Public Const GCPULLOUT As Integer = 256
    Public Const GCMATCHFINONE As Integer = 512
    Public Const GCBOUNCERECEIVED As Integer = 1024
    Public Const GCBOUNCEPULLOUTENTRY As Integer = 2048
    Public Const GCMATCHFINONEPRECOVERFILE As Integer = 4096
    Public Const GCCHQRETRIEVAL As Integer = 8192
    Public Const GCLOOSECHQ As Integer = 16384

    'Finone VS GNSA
    Public Const GCDISCAGREEMENT As Integer = 1
    Public Const GCDUPLICATEENTRY As Integer = 2
    Public Const GCDISCCHQNO As Integer = 4
    Public Const GCDISCCHQDATE As Integer = 8
    Public Const GCDISCCHQAMOUNT As Integer = 16
    Public Const GCDISCCHQDETAILS As Integer = 32

    'Product Type
    Public Const GCCSPD As Integer = 1
    Public Const GCPAP As Integer = 2
    Public Const GCOTHERS As Integer = 4
    Public Const GCALL As Integer = 8
    Public Const GCCCLR As Integer = 16
    Public Const GCCCHK As Integer = 32

    'Agreement Status
    Public Const GCACTIVE As Integer = 1
    Public Const GCINACTIVE As Integer = 2
    Public Const GCCLOSED As Integer = 4
    Public Const GCCLOSEDSTATUS As Integer = 5

    'Cheque Type
    Public Const GCEXTERNALNORMAL As Integer = 1
    Public Const GCEXTERNALSECURITY As Integer = 2
    Public Const GCECSNORMAL As Integer = 4

    'Cheque Disc
    Public Const GCCHQNONOTAVBL As Integer = 1
    Public Const GCCHQDATENOTAVBL As Integer = 2
    Public Const GCPAPCHANGED As Integer = 4
    Public Const GCNONCTS As Integer = 8

    'Retrieval
    Public Const GCREQUESTED As Integer = 1
    Public Const GCRETRIEVED As Integer = 2
    Public Const GCMISSING As Integer = 4
    Public Const GCRTRCANCEL As Integer = 8

    ' Swap
    Public Const GCOLDSWAPCHQTRANSFERED As Integer = 1
    Public Const GCOLDSWAPPKTCANCELLED As Integer = 2
    Public Const GCOLDSWAPPKTPULLOUT As Integer = 4
    Public Const GCOLDSWAPPKTPULLOUTUNDO As Integer = 8

    Public ServerDetails As String
    Public ServerDetailsQry As String
    Public ServerDetailsBIZ As String

    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long

    Public gProjectName As String = "Encore Vault Management System"
    Public softcode As String = "ENCORE"
    Public gUid As Integer
    Public gUserName As String = "ADMIN"
    Public gUserFullName As String = ""
    Public gUserRights As String
    Public GFormName As String
    Public GRejectReason As String

    Public gOdbcConn As New OdbcConnection
    Public gOdbcConnBIZ As New OdbcConnection
    Public gOdbcDAdp As New OdbcDataAdapter
    Public gOdbcCmd As New OdbcCommand
    Public gOdbcCmdBIZ As New OdbcCommand

    Public gFso As New FileIO.FileSystem

    Public gsReportPath As String = "C:\Execute\"
    Public gsAsciiFilePath As String = "c:\temp"

    Public glYearGid As Long = 0
    Public glCompanyGid As Long = 0

    Public txt As Long
    Public gnRefId As Long

    Public gsPacketStatus As String
    Public Sqlstr As String
    Public lsCond As String

    Public gbVerification As Boolean
    Public gbAddFlag As Boolean
    Public gbMChqFlag As Boolean
    Public gbEditFlag As Boolean
    Public glPacketCheckList As Long
    Public glAuditCheckList As Long
    Public lsPayMode As String
    Public gsPacketNo As String
    Public gsEnteredBy As String
    Public gsEntryDtFrom As String
    Public gsEntryDtTo As String
    Public gsSpoolMonth As String

    Dim RCount As Integer
    Dim empid As Integer
    Dim empname As String

    Public gnEvenColor As Long = RGB(220, 200, 100)
    Public gnOddColor As Long = RGB(175, 210, 175)

    'Public gnEvenColor As Long = RGB(220, 200, 500)
    'Public gnOddColor As Long = RGB(175, 210, 675)

    Public Const gnAuth As Integer = 1
    Public Const gnReject As Integer = 2

    'Mail Information'

    Public RA_From As String = ""
    Public Sys_To As String = ""
    Public Mail_Subject As String = "Error Message"

    Public gnInvFlag As Integer = 1
    Public gnPymtFlag As Integer = 2
    Public gnTdsFlag As Integer = 4
    Public gnDiscountFlag As Integer = 8
#End Region
    'For calling the Main form
    '' ''Public Sub Main()
    '' ''    Call ConOpenOdbc(ServerDetails)
    '' ''    Call ConOpenOdbcBIZ(ServerDetailsBIZ)
    '' ''    Try

    '' ''        Dim Security As New GNSASecurity.clsForm

    '' ''        'If UCase(Application.LocalUserAppDataPath) <> UCase("C:\EXEC\QA") Then
    '' ''        '    MsgBox("File path Error!" & Chr(13) & _
    '' ''        '     "File Only from C:\EXEC\QA can access", vbInformation, gProjectName)
    '' ''        '    Exit Sub
    '' ''        'End If

    '' ''        Security.clsDbIP = DbIP             ' commented for testing
    '' ''        Security.clsDbUID = DbUId           ' commented for testing
    '' ''        Security.clsDbPWD = DbPwd           ' commented for testing
    '' ''        Security.clsDbName = DbName         ' commented for testing

    '' ''        If Command() <> "" Then Security.clsEmpId = Val(Command)

    '' ''        Security.LoadLogin()                   ' commented for testing

    '' ''        gUserName = UCase(Security.clsEmpShortName)
    '' ''        gUserRights = UCase(Security.ModuleRights("RECEIPT"))
    '' ''        gUId = UCase(Security.clsEmpName)
    '' ''        If gUserName <> "" Then
    '' ''frmMain.ShowDialog()
    '' ''        End If
    '' ''    Catch ex As Exception
    '' ''        objMail.GF_Mail(RA_From, Sys_To, Mail_Subject, ex.Message, GFormName, "Query")
    '' ''        MsgBox(ex.Message)
    '' ''    End Try
    '' ''End Sub
    'To open the Connection
    Public Sub ConOpenOdbc(ByVal ServerDetails As String)
        If gOdbcConn.State = ConnectionState.Closed Then
            gOdbcConn.ConnectionString = ServerDetails
            gOdbcConn.Open()
            gOdbcCmd.Connection = gOdbcConn
        End If
        'empid = Security.clsEmpId
    End Sub
    'To open the Connection
    Public Sub ConOpenOdbcBIZ(ByVal ServerDetailsBIZ As String)
        If gOdbcConnBIZ.State = ConnectionState.Closed Then
            gOdbcConnBIZ.ConnectionString = ServerDetailsBIZ
            gOdbcConnBIZ.Open()
            gOdbcCmdBIZ.Connection = gOdbcConnBIZ
        End If
        'empid = Security.clsEmpId
    End Sub
    'To Close the Connection
    Public Sub ConCloseOdbc(ByVal ServerDetails As String)
        If gOdbcConn.State = ConnectionState.Open Then
            gOdbcConn.Close()
        End If
    End Sub
    'To Execute Query and return as datareader
    Public Function gfExecuteQry(ByVal strsql As String, ByVal odbcConn As OdbcConnection)
        Dim objCommand As OdbcCommand
        Dim objDataReader As OdbcDataReader
        objCommand = New OdbcCommand(strsql, odbcConn)
        Try
            objDataReader = objCommand.ExecuteReader()
            objCommand.Dispose()
            Return objDataReader
        Catch ex As Exception
            MsgBox(ex.Message)
            Return (0)
        End Try

    End Function
    'To Execute Query and return value as boolean
    Public Function gfExecuteQryBln(ByVal strsql As String, ByVal odbcConn As OdbcConnection) As Boolean
        gOdbcCmd = New OdbcCommand(strsql, odbcConn)
        Dim objDataReader As OdbcDataReader
        Try
            objDataReader = gOdbcCmd.ExecuteReader()
            If objDataReader.HasRows Then
                gfExecuteQryBln = True
            Else
                gfExecuteQryBln = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            gfExecuteQryBln = False
        End Try
    End Function
    'To Execute Query and return value as string
    Public Function gfExecuteScalar(ByVal strsql As String, ByVal odbcConn As OdbcConnection) As String
        Dim StrVal As String
        Dim objCommand As OdbcCommand
        objCommand = New OdbcCommand(strsql, odbcConn)

        Try
            If IsDBNull(objCommand.ExecuteScalar()) Or IsNothing(objCommand.ExecuteScalar()) Then
                StrVal = ""
            Else
                StrVal = objCommand.ExecuteScalar()
            End If

            objCommand.Dispose()
            Return StrVal

        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        End Try
    End Function

    'To Execute Query and return value as integer
    Public Function gfInsertQry(ByVal strsql As String, ByVal odbcConn As OdbcConnection) As Integer
        Dim recAffected As Long
        gOdbcCmd = New OdbcCommand(strsql, odbcConn)
        gOdbcCmd.CommandType = CommandType.Text
        Try
            recAffected = gOdbcCmd.ExecuteNonQuery()
            Return recAffected
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(strsql)
            LogQuery(strsql)
            End
            Return 0
        End Try
    End Function

    Private Sub LogQuery(ByVal Qry As String)
        Dim lsFileName As String

        lsFileName = Application.StartupPath & "\Qry.txt"

        Call FileOpen(1, lsFileName, OpenMode.Append)
        PrintLine(1, Qry)
        FileClose(1)
    End Sub

    'To Bind values to Datagrid
    Public Sub gpPopGrid(ByVal GridName As DataGrid, ByVal Qry As String, ByVal odbcConn As OdbcConnection)
        Dim lobjDataTable As New DataTable
        Dim lobjDataView As New DataView
        Dim lobjDataSet As New DataSet
        Dim lobjDataAdapter As New Odbc.OdbcDataAdapter
        Try
            lobjDataAdapter = New OdbcDataAdapter(Qry, odbcConn)
            lobjDataSet = New DataSet("TBL")
            lobjDataAdapter.Fill(lobjDataSet, "TBL")
            lobjDataTable = lobjDataSet.Tables(0)
            lobjDataView = New DataView(lobjDataTable)
            GridName.DataSource = lobjDataView
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'To Bind values to Datagrid
    Public Sub gpPopGridView(ByVal GridName As DataGridView, ByVal Qry As String, ByVal odbcConn As OdbcConnection)
        Dim lda As New Odbc.OdbcDataAdapter(Qry, odbcConn)
        Dim lds As New DataSet
        Dim ldt As DataTable
        Try
            lda.Fill(lds, "tbl")
            ldt = lds.Tables("tbl")
            GridName.DataSource = ldt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'To filter single quote in the give text
    Public Function QuoteFilter(ByVal txt As String) As String
        QuoteFilter = Trim(Replace(Replace(Replace(txt, "'", "''"), """", """"""), "\", "/"))
    End Function
    'To Clear control in a form
    Public Sub frmCtrClear(ByVal ctrlBag As Object)
        Dim ctrl As Control

        For Each ctrl In ctrlBag.Controls
            If ctrl.Tag <> "*" Then
                If TypeOf ctrl Is TextBox Then
                    ctrl.Text = ""
                ElseIf TypeOf ctrl Is ComboBox Then
                    ctrl.Text = ""
                ElseIf ctrl.Controls.Count > 0 Then
                    frmCtrClear(ctrl)
                End If
            End If
        Next
    End Sub

    'To get Dataset
    Public Function gfDataSet(ByVal SQL As String, ByVal tblName As String, ByVal odbcConn As Odbc.OdbcConnection) As DataSet
        Dim objDataAdapter As New OdbcDataAdapter(SQL, odbcConn)
        Dim objDataSet As New DataSet
        Try
            objDataAdapter.Fill(objDataSet, tblName)
            Return objDataSet
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function
    'Binding combo
    Public Sub gpBindCombo(ByVal SQL As String, ByVal Dispfld As String, _
                               ByVal Valfld As String, ByRef ComboName As ComboBox, _
                                ByVal odbcConn As Odbc.OdbcConnection)

        Dim objDataAdapter As New OdbcDataAdapter
        Dim objCommand As New OdbcCommand
        Dim objDataTable As New Data.DataTable
        Try
            objCommand.Connection = odbcConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = SQL
            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(objDataTable)
            ComboName.DataSource = objDataTable
            ComboName.DisplayMember = Dispfld
            ComboName.ValueMember = Valfld
            ComboName.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
            objDataTable.Dispose()
            objCommand.Dispose()
            objDataAdapter.Dispose()
        End Try
    End Sub
    'Validating for Integer only
    Public Function gfIntEntryOnly(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8, 47
                gfIntEntryOnly = False
            Case Else
                gfIntEntryOnly = True
        End Select
    End Function
    Public Function gfIntstrEntryOnly(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Select Case Asc(e.KeyChar)
            Case 47 To 57, 8, 60 To 90, 32, 97 To 122
                If Asc(e.KeyChar) = 64 Then
                    gfIntstrEntryOnly = True
                Else
                    gfIntstrEntryOnly = False
                End If

            Case Else
                gfIntstrEntryOnly = True
        End Select
    End Function
    Public Function gfstrEntryOnly(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Select Case Asc(e.KeyChar)
            Case 8, 60 To 90, 32, 46, 97 To 122
                If Asc(e.KeyChar) = 64 Then
                    gfstrEntryOnly = True
                Else
                    gfstrEntryOnly = False
                End If

            Case Else
                gfstrEntryOnly = True
        End Select
    End Function
    Public Function gfAmountEntryOnly(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txt As TextBox) As Boolean
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8, 46
                If Asc(e.KeyChar) = 46 Then
                    If InStr(txt.Text, ".") = 0 Then
                        gfAmountEntryOnly = False
                    Else
                        gfAmountEntryOnly = True
                    End If
                Else
                    gfAmountEntryOnly = False
                End If
            Case Else
                gfAmountEntryOnly = True
        End Select
    End Function
    'To Get the DataTable
    Public Function GetDataTable(ByVal SqlQry As String) As DataTable
        Dim lobjDataTable As New DataTable
        Dim lobjDataView As New DataView
        Dim lobjDataSet As New DataSet
        Dim lobjDataAdapter As New Odbc.OdbcDataAdapter
        GetDataTable = Nothing
        Try

            gOdbcDAdp = New OdbcDataAdapter(SqlQry, gOdbcConn)
            lobjDataSet = New DataSet("TBL")
            gOdbcDAdp.Fill(lobjDataSet, "TBL")
            lobjDataTable = lobjDataSet.Tables(0)
            lobjDataView = New DataView(lobjDataTable)
            Return lobjDataTable

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    'Disables the addition of rows in the given DataGrid
    Public Sub DisableAddNew(ByRef dg As DataGrid, _
                                    ByRef Frm As Form)
        ' Disable addnew capability on the grid.
        ' Note that AllowEdit and AllowDelete can be disabled
        ' by adding or changing the "AllowNew" property to
        ' AllowDelete or AllowEdit.
        Dim cm As CurrencyManager = _
           CType(Frm.BindingContext(dg.DataSource, dg.DataMember),  _
                 CurrencyManager)
        CType(cm.List, DataView).AllowNew = False
    End Sub
    ' Aligns the given text in specified format
    Public Function AlignTxt(ByVal txt As String, ByVal Length As Integer, ByVal Alignment As Integer) As String
        Dim X As String = ""

        Select Case Alignment
            Case 1
                Return LSet(txt, Length)
            Case 4
                Return CSet(txt, Length)
            Case 7
                Return RSet(txt, Length)
            Case Else
                Return (0)
        End Select
    End Function
    ' Center Align the Given Text
    Public Function CSet(ByVal txt As String, ByVal PaperChrWidth As Integer) As String
        Dim s As String                 ' Temporary String Variable
        Dim l As Integer                ' Length of the String
        If Len(txt) > PaperChrWidth Then
            CSet = Left(txt, PaperChrWidth)
        Else
            l = (PaperChrWidth - Len(txt)) / 2
            s = RSet(txt, l + Len(txt))
            CSet = Space(PaperChrWidth - Len(s))
            CSet = s + CSet
        End If
    End Function
    Public Function SwapChkSum(ByVal txt As String) As Double
        Dim TempTxt As String
        Dim TempChkSum As Double
        Dim i As Long

        TempTxt = txt
        TempChkSum = 0

        For i = 1 To Len(TempTxt)
            TempChkSum = TempChkSum + Asc(Mid(TempTxt, i, 1)) + (i - 1)
        Next i

        SwapChkSum = TempChkSum
    End Function
    Public Function SwapChkSumNew(ByVal txt As String) As Double
        Dim TempTxt As String
        Dim TempChkSum As Double
        Dim i As Long

        TempTxt = txt
        TempChkSum = 0

        For i = 1 To Len(TempTxt)
            TempChkSum = TempChkSum + Asc(Mid(TempTxt, i, 1)) + (i)
        Next i

        SwapChkSumNew = TempChkSum
    End Function
    Public Function ConvUcase(ByVal keychar As String) As String
        Select Case keychar
            Case "a" To "z"
                ConvUcase = keychar.ToUpper
            Case Else
                ConvUcase = keychar
        End Select
    End Function

    Public Sub Kill_Excel()
        Dim proc As System.Diagnostics.Process
        For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
            proc.Kill()
        Next
    End Sub
    'For Getting Loan Type 
    Public Function gfLoanType(ByVal lsLoanNo As String)
        Dim lsLoanType As String = ""
        Dim lnLoanLen As Integer

        lnLoanLen = lsLoanNo.Length
        If IsNumeric(lsLoanNo) Then
            lsLoanType = "H"
        Else
            lsLoanType = Mid(lsLoanNo, 1, 1)
            If lsLoanType = "A" Then
                lsLoanType = "A"
            ElseIf lsLoanType = "C" Or lsLoanType = "D" Then
                lsLoanType = "C"
            Else
                lsLoanType = "P"
            End If

        End If
        Return lsLoanType
    End Function
    'For Zip A File
    Public Sub gp_WinZip(ByVal password As String, ByVal DirPath As String, ByVal ZipPath As String)
        Dim FileName As String
        Dim X As String
        Dim lb_Flag As Boolean
        Try
            Const ZIPEXE = "C:\Program Files\WinZip\WINZIP32.EXE "

            'DirPath = txtAttachment1.Text
            'ZipPath = "c:\WinZip"

            'Password = "Citibank" & Mid(Format(Now, "dd-MM-yyyy"), 1, 2)
            If Dir(ZipPath, vbDirectory) = "" Then
                MkDir(ZipPath)
            End If
            FileName = Dir(DirPath, vbNormal)
            While FileName <> ""
                X = ZIPEXE & " -a -s" & password & " " & ZipPath & "\" & Mid(FileName, 1, Len(FileName) - 4) & ".zip " & FileName
                Shell(X)
                FileName = Dir()
                lb_Flag = True
            End While

            If lb_Flag = True Then
                MsgBox("Successfully Created at " & ZipPath, MsgBoxStyle.Information, gProjectName)
            Else
                MsgBox("ZIP Procedd Faild", MsgBoxStyle.Information, gProjectName)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ''Excel To DS :Created Date :23-02-2009 :Created By :Ilaya
    'Public Function gpExcelDataset(ByVal Qry As String, ByVal Excelpath As String) As DataTable
    '    Dim fOleDbConString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" & Excelpath & ";" + "Extended Properties=Excel 8.0;"
    '    Dim lobjDataTable As New DataTable
    '    Dim lobjDataSet As New DataSet
    '    Dim lobjDataAdapter As New OleDbDataAdapter

    '    lobjDataAdapter = New OleDbDataAdapter(Qry, fOleDbConString)
    '    lobjDataSet = New DataSet("TBL")
    '    lobjDataAdapter.Fill(lobjDataSet, "TBL")
    '    lobjDataTable = lobjDataSet.Tables(0)
    '    Return lobjDataTable

    'End Function

    Public Function gpExcelDataset(ByVal Qry As String, ByVal Excelpath As String) As DataTable
        Dim fOleDbConString As String = ""
        Dim lobjDataTable As New DataTable
        Dim lobjDataSet As New DataSet
        Dim lobjDataAdapter As New OleDbDataAdapter
        Dim n As Integer

        n = Excelpath.Split(".").Length

        If Excelpath.Split(".")(n - 1).ToLower = "xlsx" Then
            fOleDbConString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" & Excelpath & ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES';"
        Else
            fOleDbConString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" & Excelpath & ";" + "Extended Properties='Excel 12.0;HDR=YES';"
        End If

        lobjDataAdapter = New OleDbDataAdapter(Qry, fOleDbConString)
        lobjDataSet = New DataSet("TBL")
        lobjDataAdapter.Fill(lobjDataSet, "TBL")
        lobjDataTable = lobjDataSet.Tables(0)

        Return lobjDataTable
    End Function

    'LoadExcelSheet
    Public Sub gfLoadXLSheet(ByVal FileName As String, ByVal objCbo As ComboBox)
        Dim objXL As New Excel.Application
        Dim i As Integer

        objCbo.Items.Clear()
        objXL.Workbooks.Open(FileName)

        For i = 1 To objXL.ActiveWorkbook.Worksheets.Count
            objCbo.Items.Add(objXL.ActiveWorkbook.Worksheets(i).name)
        Next i

        objXL.Workbooks.Close()

        GC.Collect()
        GC.WaitForPendingFinalizers()
        objXL.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(objXL)
        objXL = Nothing
    End Sub

    'AutoFillCombo :Created Date :24-02-2009 :Created By :Ilaya
    Public Sub gpAutoFillCombo(ByVal cboBox As ComboBox)

        Dim lnLenght As Long

        With cboBox

            lnLenght = .Text.Length

            .SelectedIndex = .FindString(.Text)

            .SelectionStart = lnLenght

            .SelectionLength = Math.Abs(.Text.Length - lnLenght)

        End With

    End Sub
    'AutoFillCombo :Created Date :24-02-2009 Created By :Ilaya
    Public Sub gpAutoFindCombo(ByVal cboBox As ComboBox)
        cboBox.SelectedIndex = cboBox.FindString(cboBox.Text)
    End Sub

    'Public Sub RowColor(ByVal ctrl As AxMSFlexGridLib.AxMSFlexGrid, ByVal StartRow As Integer, ByVal EndRow As Integer, ByVal BkColor As Long)
    '    Dim i As Integer, j As Integer

    '    Try
    '        With ctrl
    '            For i = StartRow To EndRow
    '                .Row = i

    '                For j = .FixedCols To .Cols - 1
    '                    .Col = j
    '                    .CellBackColor = ColorTranslator.FromWin32(BkColor)
    '                Next j
    '            Next i
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, gProjectName)
    '    End Try
    'End Sub

    Public Function gfAmtEntryOnly(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Select Case Asc(e.KeyChar)
            Case 48 To 57, 8, 46
                gfAmtEntryOnly = False
            Case Else
                gfAmtEntryOnly = True
        End Select
    End Function

    'Binding combo
    Public Sub gpBindDGridCombo(ByVal SQL As String, ByVal Dispfld As String, _
                               ByVal Valfld As String, ByRef ComboName As DataGridViewComboBoxColumn, _
                                ByVal odbcConn As Odbc.OdbcConnection)

        Dim objDataAdapter As New OdbcDataAdapter
        Dim objCommand As New OdbcCommand
        Dim objDataTable As New Data.DataTable
        Try
            objCommand.Connection = odbcConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = SQL
            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(objDataTable)
            ComboName.DataSource = objDataTable
            ComboName.DisplayMember = Dispfld
            ComboName.ValueMember = Valfld
            'ComboName.SelectedIndex = -1

        Catch ex As Exception
            MsgBox(ex.Message)
            objDataTable.Dispose()
            objCommand.Dispose()
            objDataAdapter.Dispose()
        End Try
    End Sub

    'To get Dataset
    Public Sub gpDataSet(ByVal SQL As String, ByVal tblName As String, ByVal odbcConn As Odbc.OdbcConnection, ByVal ds As DataSet)
        Dim objDataAdapter As New OdbcDataAdapter(SQL, odbcConn)

        Try
            objDataAdapter.Fill(ds, tblName)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gProjectName)
        End Try
    End Sub

    'To trim all control in a form
    Public Sub gpTrimAll(ByVal frmName As Form)
        Dim ctrl As Control
        For Each ctrl In frmName.Controls
            If TypeOf ctrl Is TextBox Then ctrl.Text = ctrl.Text.Trim
        Next
    End Sub

    Public Function TrimDblSpace(ByRef Txt As String) As String
        Txt = Txt.Replace("  ", " ")

        If Txt.IndexOf("  ") > 0 Then
            Return TrimDblSpace(Txt)
        Else
            Return Txt
        End If
    End Function

    Public Function GetLoanType(ByVal LoanNo As String) As String
        If Microsoft.VisualBasic.Left(LoanNo, 1) = "A" And Len(LoanNo) = "13" Then
            Return "A"
        ElseIf Microsoft.VisualBasic.Left(LoanNo, 1) = "C" And Len(LoanNo) = "13" Then
            Return "C"
        ElseIf Microsoft.VisualBasic.Left(LoanNo, 1) = "D" And Len(LoanNo) = "13" Then
            Return "D"
        ElseIf Len(LoanNo) = "13" Then
            Return "P"
        Else
            Return "H"
        End If
    End Function

    Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Function AmtInWords(ByRef amt As Double, ByRef Rupees As String, ByRef Paise As String, ByRef Only As String) As String
        Dim m As Integer
        Dim n As Short
        Dim b As String = ""
        Dim a As String = ""
        Dim C As String = ""

        m = Int(amt)
        n = Int(System.Math.Round(amt - m, 2) * 100)

        If m <> 0 Then
            a = English(m)
            'If n <> 0 Then
            b = " and "
        End If
        'If n <> 0 Then
        C = Paise & " " & English(n)

        AmtInWords = Rupees & " " & a & b & C & " " & Only
    End Function
    Function English(ByVal n As Integer) As String
        Const Thousand As Long = 1000
        Const Lakh As Long = Thousand * 100
        Const Crore As Integer = Thousand * 10000
        'Const Trillion = Thousand * Crore

        Dim Buf As String : Buf = ""

        If (n = 0) Then English = "zero" : Exit Function

        If (n < 0) Then Buf = "negative " : n = -n

        If (n >= Crore) Then
            Buf = Buf & EnglishDigitGroup(n \ Crore) & " crore"
            n = n Mod Crore
            If (n) Then Buf = Buf & " "
        End If

        If (n >= Lakh) Then
            Buf = Buf & EnglishDigitGroup(n \ Lakh) & " lakh"
            n = n Mod Lakh
            If (n) Then Buf = Buf & " "
        End If

        If (n >= Thousand) Then
            Buf = Buf & EnglishDigitGroup(n \ Thousand) & " thousand"
            n = n Mod Thousand
            If (n) Then Buf = Buf & " "
        End If

        If (n > 0) Then
            Buf = Buf & EnglishDigitGroup(n)
        End If

        English = Buf
    End Function
    'Mari Muthu 07-12-2011
    Public Sub gpListView(ByVal SQL As String, ByVal Dispfld As String, _
                                ByVal Valfld As String, ByRef ListName As ListView, _
                                 ByVal odbcConn As Odbc.OdbcConnection)

        Dim objDataAdapter As New OdbcDataAdapter
        Dim objDataReader As OdbcDataReader
        Dim objCommand As New OdbcCommand
        Dim objDataTable As New Data.DataTable
        Try
            objCommand.Connection = odbcConn
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = SQL
            objDataReader = objCommand.ExecuteReader
            While objDataReader.Read
                ListName.Items.Add(objDataReader.Item(0).ToString)
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
            objDataTable.Dispose()
            objCommand.Dispose()
            objDataAdapter.Dispose()
        End Try
    End Sub
    ' Support function to be used only by English()
    Function EnglishDigitGroup(ByVal n As Short) As String
        Const Hundred As String = " hundred"
        Const One As String = "one"
        Const Two As String = "two"
        Const Three As String = "three"
        Const Four As String = "four"
        Const Five As String = "five"
        Const Six As String = "six"
        Const Seven As String = "seven"
        Const Eight As String = "eight"
        Const Nine As String = "nine"
        Dim Buf As String : Buf = ""
        Dim Flag As Short : Flag = False

        'Do hundreds
        Select Case (n \ 100)
            Case 0 : Buf = "" : Flag = False
            Case 1 : Buf = One & Hundred : Flag = True
            Case 2 : Buf = Two & Hundred : Flag = True
            Case 3 : Buf = Three & Hundred : Flag = True
            Case 4 : Buf = Four & Hundred : Flag = True
            Case 5 : Buf = Five & Hundred : Flag = True
            Case 6 : Buf = Six & Hundred : Flag = True
            Case 7 : Buf = Seven & Hundred : Flag = True
            Case 8 : Buf = Eight & Hundred : Flag = True
            Case 9 : Buf = Nine & Hundred : Flag = True
        End Select

        If (Flag) Then n = n Mod 100
        If (n) Then
            If (Flag) Then Buf = Buf & " "
        Else
            EnglishDigitGroup = Buf
            Exit Function
        End If

        'Do tens (except teens)
        Select Case (n \ 10)
            Case 0, 1 : Flag = False
            Case 2 : Buf = Buf & "twenty" : Flag = True
            Case 3 : Buf = Buf & "thirty" : Flag = True
            Case 4 : Buf = Buf & "forty" : Flag = True
            Case 5 : Buf = Buf & "fifty" : Flag = True
            Case 6 : Buf = Buf & "sixty" : Flag = True
            Case 7 : Buf = Buf & "seventy" : Flag = True
            Case 8 : Buf = Buf & "eighty" : Flag = True
            Case 9 : Buf = Buf & "ninety" : Flag = True
        End Select

        If (Flag) Then n = n Mod 10
        If (n) Then
            If (Flag) Then Buf = Buf & "-"
        Else
            EnglishDigitGroup = Buf
            Exit Function
        End If

        'Do ones and teens
        Select Case (n)
            Case 0 ' do nothing
            Case 1 : Buf = Buf & One
            Case 2 : Buf = Buf & Two
            Case 3 : Buf = Buf & Three
            Case 4 : Buf = Buf & Four
            Case 5 : Buf = Buf & Five
            Case 6 : Buf = Buf & Six
            Case 7 : Buf = Buf & Seven
            Case 8 : Buf = Buf & Eight
            Case 9 : Buf = Buf & Nine
            Case 10 : Buf = Buf & "ten"
            Case 11 : Buf = Buf & "eleven"
            Case 12 : Buf = Buf & "twelve"
            Case 13 : Buf = Buf & "thirteen"
            Case 14 : Buf = Buf & "fourteen"
            Case 15 : Buf = Buf & "fifteen"
            Case 16 : Buf = Buf & "sixteen"
            Case 17 : Buf = Buf & "seventeen"
            Case 18 : Buf = Buf & "eighteen"
            Case 19 : Buf = Buf & "nineteen"
        End Select

        EnglishDigitGroup = Buf
    End Function
    Public Sub LogTransaction(ByVal GNSAREF As String, ByVal StatusFlag As Integer, ByVal TransactionBy As String)
        Dim lssql As String
        Dim liseqno As Integer

        lssql = " select max(transaction_seqno) as mncount from chloa_trn_ttransaction where " & _
               " transaction_gnsarefno = '" & GNSAREF & "'"
        liseqno = Val(gfExecuteScalar(lssql, gOdbcConn))
        liseqno += 1

        lssql = " insert into chloa_trn_ttransaction (transaction_gnsarefno,transaction_statusflag,transaction_transactionby,transaction_transactionon,"
        lssql &= " transaction_seqno) values ("
        lssql &= "'" & GNSAREF & "',"
        lssql &= "" & StatusFlag & ","
        lssql &= "'" & TransactionBy & "',"
        lssql &= "now(),"
        lssql &= "" & liseqno & ")"

        gfInsertQry(lssql, gOdbcConn)

    End Sub

    Public Sub QuickView(ByVal Cn As OdbcConnection, ByVal Sqlstr As String)
        Dim frm As New frmQuickView(Cn, Sqlstr)
        frm.ShowDialog()
    End Sub
    'The IPAddresses method obtains the selected server IP address information.
    'It then displays the type of address family supported by the server and 
    'its IP address in standard and byte format.
    Public Function IPAddresses(ByVal server As String) As String
        Try
            Dim ASCII As New System.Text.ASCIIEncoding()

            ' Get server related information.
            Dim heserver As Net.IPHostEntry = Net.Dns.Resolve(server)

            ' Loop on the AddressList
            Dim curAdd As Net.IPAddress
            For Each curAdd In heserver.AddressList
                ' Display the server IP address in the standard format. In 
                ' IPv4 the format will be dotted-quad notation, in IPv6 it will be
                ' in in colon-hexadecimal notation.
                Return curAdd.ToString()
            Next curAdd

            Return ""
        Catch e As Exception
            Console.WriteLine(("[DoResolve] Exception: " + e.ToString()))
            Return ""
        End Try
    End Function 'IPAddresses

    Public Sub UpdateAlmara(ByVal GNSAREFNO As String, ByVal Mode As String)
        Dim lnAlmaraGid As Long

        Sqlstr = ""
        Sqlstr &= " SELECT almaraentry_gid FROM chola_trn_almaraentry"
        Sqlstr &= " WHERE (" & Replace(Replace(Replace(GNSAREFNO.ToUpper, "P", ""), "N", ""), "E", "") & " BETWEEN almaraentry_refnofrom AND almaraentry_refnoto)"
        Sqlstr &= " AND almaraentry_deleteflag ='N'"
        Sqlstr &= " AND almaraentry_type='" & Mode & "'"

        lnAlmaraGid = Val(gfExecuteScalar(Sqlstr, gOdbcConn))

        If lnAlmaraGid > 0 Then
            Sqlstr = ""
            Sqlstr &= " update chola_trn_tpacket "
            Sqlstr &= " set packet_box_gid=" & lnAlmaraGid
            Sqlstr &= " ,packet_status= packet_status | " & GCPACKETVAULTED
            Sqlstr &= " where packet_gnsarefno='" & GNSAREFNO & "'"
            gfInsertQry(Sqlstr, gOdbcConn)
        End If

    End Sub
    Public Sub LogPacketHistory(ByVal GNSAREFNO As String, ByVal StatusFlag As Integer, Optional ByVal PacketGid As Long = 0, Optional ByVal EntryBy As String = "", Optional ByVal EntryOn As String = "")
        Dim lssql As String
        Dim lnPacketGid As Long

        If PacketGid = 0 Then
            lssql = ""
            lssql &= " select packet_gid from chola_trn_tpacket "
            lssql &= " where packet_gnsarefno='" & GNSAREFNO & "'"
            lnPacketGid = gfExecuteScalar(lssql, gOdbcConn)
        Else
            lnPacketGid = PacketGid
        End If

        lssql = ""
        lssql &= " insert into chola_trn_tpackethistory("
        lssql &= " history_packet_gid,history_statusflag,history_entryby,history_entryon)"
        lssql &= " values ("
        lssql &= "" & lnPacketGid & ","
        lssql &= "" & StatusFlag & ","

        If EntryBy = "" Then
            lssql &= "'" & gUserName & "',"
        Else
            lssql &= "'" & EntryBy & "',"
        End If

        If EntryOn = "" Then
            lssql &= "sysdate())"
        Else
            lssql &= "'" & Format(CDate(EntryOn), "yyyy-MM-dd") & "')"
        End If


        gfInsertQry(lssql, gOdbcConn)
    End Sub
    Public Sub LogPacket(ByVal GNSAREFNO As String, ByVal StatusFlag As Integer, Optional ByVal PacketGid As Long = 0, _
                         Optional ByVal Mode As String = "", Optional ByVal EntryBy As String = "", _
                         Optional ByVal EntryOn As String = "", Optional ByVal PacketDisc As String = "", Optional ByVal MultipleChq As String = "")
        Dim lssql As String
        Dim lnPacketGid As Long

        If PacketGid = 0 Then
            lssql = ""
            lssql &= " select packet_gid from chola_trn_tpacket "
            lssql &= " where packet_gnsarefno='" & GNSAREFNO & "'"
            lnPacketGid = gfExecuteScalar(lssql, gOdbcConn)
        Else
            lnPacketGid = PacketGid
        End If

        lssql = ""
        lssql &= " update chola_trn_tpacket set "
        lssql &= " packet_status=packet_status | " & StatusFlag & ","

        If EntryBy = "" Then
            lssql &= "packet_entryby='" & gUserName & "',"
        Else
            lssql &= "packet_entryby='" & EntryBy & "',"
        End If

        If PacketDisc.Trim <> "" Then
            lssql &= " packet_paymodedisc='" & PacketDisc & "',"
        End If

        If Mode <> "" Then
            lssql &= " packet_mode='" & Mode & "',"
        End If

        If MultipleChq <> "" Then
            lssql &= " packet_ismultiplebank='" & MultipleChq & "',"
        End If

        If EntryOn = "" Then
            lssql &= "packet_entryon=sysdate()"
        Else
            lssql &= "packet_entryon='" & Format(CDate(EntryOn), "yyyy-MM-dd") & "'"
        End If


        lssql &= " where packet_gid=" & lnPacketGid & ""

        gfInsertQry(lssql, gOdbcConn)
    End Sub

    'Public Sub UpdatePacket(ByVal GNSAREF As String)
    '    Dim objdt As DataTable
    '    Dim lssql As String
    '    Dim drpdc As Odbc.OdbcDataReader
    '    Dim lnpdcgid As Long
    '    Dim lschqdisc As String = ""

    '    lssql = ""
    '    lssql &= " select entry_gid,shortagreement_no,agreement_gid,chq_no,chq_date,chq_amount,chq_disc_value,chq_prodtype,chq_papflag,chq_type  "
    '    lssql &= " from chola_trn_tpdcentry "
    '    lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
    '    lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid"
    '    lssql &= " where packet_gnsarefno='" & GNSAREF & "'"
    '    lssql &= " and chq_pdc_gid=0"
    '    objdt = GetDataTable(lssql)

    '    If objdt.Rows.Count = 0 Then
    '        MsgBox("Invalid Reference No..!", MsgBoxStyle.Critical, gProjectName)
    '        Exit Sub
    '    End If

    '    For i As Integer = 0 To objdt.Rows.Count - 1
    '        'Cheque Disc
    '        lssql = " select pdc_gid,atpar_flag,pdc_chqdate,pdc_micrcode from chola_trn_tpdcfile where chq_rec_slno=1 "
    '        lssql &= " and pdc_chqno = " & Val(objdt.Rows(i).Item("chq_no").ToString) & " "
    '        lssql &= " and pdc_shortpdc_parentcontractno='" & objdt.Rows(i).Item("shortagreement_no").ToString & "'"

    '        drpdc = gfExecuteQry(lssql, gOdbcConn)

    '        If drpdc.HasRows Then
    '            While drpdc.Read
    '                lnpdcgid = drpdc.Item("pdc_gid").ToString
    '                If Val(objdt.Rows(i).Item("chq_type").ToString) <> GCEXTERNALSECURITY Then
    '                    If objdt.Rows(i).Item("chq_date").ToString <> "" Then
    '                        If Format(CDate(drpdc.Item("pdc_chqdate").ToString), "yyyy-MM-dd") <> Format(CDate(objdt.Rows(i).Item("chq_date").ToString), "yyyy-MM-dd") Then
    '                            lschqdisc = GCCHQDATENOTAVBL
    '                        Else
    '                            lschqdisc = GCZERO
    '                        End If
    '                    Else
    '                        lschqdisc = GCZERO
    '                    End If
    '                Else
    '                    lschqdisc = GCZERO
    '                End If
    '            End While
    '        Else
    '            lnpdcgid = GCZERO
    '            lschqdisc = GCCHQNONOTAVBL
    '        End If

    '        lssql = ""
    '        lssql &= " update chola_trn_tpdcentry set "
    '        lssql &= " chq_pdc_gid=" & lnpdcgid & ","
    '        lssql &= " chq_agreement_gid=" & objdt.Rows(i).Item("agreement_gid").ToString & ","
    '        lssql &= " chq_disc_value=(chq_disc_value | (" & GCCHQNONOTAVBL & "|" & GCCHQDATENOTAVBL & ") ) ^ (" & GCCHQNONOTAVBL & "|" & GCCHQDATENOTAVBL & ") | " & lschqdisc
    '        lssql &= " where entry_gid=" & objdt.Rows(i).Item("entry_gid").ToString
    '        gfInsertQry(lssql, gOdbcConn)

    '        lssql = ""
    '        lssql &= " UPDATE"
    '        lssql &= " chola_trn_tpdcfile"
    '        lssql &= " set"
    '        lssql &= " entry_gid=" & objdt.Rows(i).Item("entry_gid").ToString & ","
    '        lssql &= " atpar_flag='" & objdt.Rows(i).Item("chq_papflag").ToString & "',"
    '        lssql &= " prod_type=" & objdt.Rows(i).Item("chq_prodtype").ToString & ","
    '        If objdt.Rows(i).Item("chq_date").ToString <> "" Then
    '            lssql &= " org_chqdate='" & Format(CDate(objdt.Rows(i).Item("chq_date").ToString), "yyyy-MM-dd") & "',"
    '        End If
    '        lssql &= " org_chqamount=" & Val(objdt.Rows(i).Item("chq_amount").ToString) & ","
    '        lssql &= " pdc_status_flag=pdc_status_flag|" & GCENTRY & ","
    '        lssql &= " file_lock='N',lock_by=null "
    '        lssql &= " where 1=1"
    '        lssql &= " and pdc_gid=" & lnpdcgid
    '    Next
    'End Sub

    Public Sub LogCTSAudit(ByVal EntryGid As Long, ByVal Mode As String, ByVal IsCTS As String, ByVal IsMICR As String)
        Dim lssql As String
        lssql = ""
        lssql &= " insert into chola_trn_tctsaudit( "
        lssql &= " ctsaudit_entry_gid,ctsaudit_mode,ctsaudit_iscts,ctsaudit_ismicr,ctsaudit_entryby,ctsaudit_entryon)"
        lssql &= " values ("
        lssql &= "" & EntryGid & ","
        lssql &= "'" & Mode & "',"
        lssql &= "" & IIf(Mid(IsCTS, 1, 1) = "'", IsCTS, "'" & IsCTS & "'") & ","
        lssql &= "" & If(Mid(IsMICR, 1, 1) = "'", IsMICR, "'" & IsMICR & "'") & ","
        lssql &= "'" & gUserName & "',"
        lssql &= " sysdate())"
        gfInsertQry(lssql, gOdbcConn)
    End Sub

    Public Sub gpSelectColumnGrid(ByVal Grid As DataGridView, ByVal SelectColumnName As String, ByVal SelectFlag As Boolean)
        Dim i As Integer

        With Grid
            For i = 0 To .RowCount - 1
                .Rows(i).Cells(SelectColumnName).Value = SelectFlag
            Next i
        End With
    End Sub
End Module
