Imports System.Data.Odbc
Imports System.Data
Imports System.IO
Imports System.Web

Public Class XMLExport
    Private xlWriter As StreamWriter
    Private sExcelHeader As String
    Private sSheetFooter As String
    Public Sub New(ByVal sFileName As String)
        xlWriter = New StreamWriter(sFileName)
        sExcelHeader = "<?xml version=""1.0""?>" & vbCrLf
        sExcelHeader += "<?mso-application progid=""Excel.Sheet""?>" & vbCrLf
        sExcelHeader += "<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""" & vbCrLf
        sExcelHeader += "xmlns:o=""urn:schemas-microsoft-com:office:office""" & vbCrLf
        sExcelHeader += "xmlns:x=""urn:schemas-microsoft-com:office:excel""" & vbCrLf
        sExcelHeader += "xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""" & vbCrLf
        sExcelHeader += "xmlns:html=""http://www.w3.org/TR/REC-html40"">" & vbCrLf
        sExcelHeader += "<DocumentProperties xmlns=""urn:schemas-microsoft-com:office:office"">" & vbCrLf
        sExcelHeader += "<Version>11.5606</Version>" & vbCrLf
        sExcelHeader += "</DocumentProperties>" & vbCrLf
        sExcelHeader += "<OfficeDocumentSettings xmlns=""urn:schemas-microsoft-com:office:office"">" & vbCrLf
        sExcelHeader += "<DownloadComponents/>" & vbCrLf
        sExcelHeader += "</OfficeDocumentSettings>" & vbCrLf
        sExcelHeader += "<ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">" & vbCrLf
        sExcelHeader += "<ActiveSheet>1</ActiveSheet>" & vbCrLf
        sExcelHeader += "<ProtectStructure>False</ProtectStructure>" & vbCrLf
        sExcelHeader += "<ProtectWindows>False</ProtectWindows>" & vbCrLf
        sExcelHeader += "</ExcelWorkbook>" & vbCrLf 'test

        sExcelHeader += "<Styles>" & vbCrLf
        sExcelHeader += "<Style ss:ID=""Default"" ss:Name=""Normal"">" & vbCrLf
        sExcelHeader += "<Alignment ss:Vertical=""Bottom"" />" & vbCrLf
        sExcelHeader += "<Borders />" & vbCrLf
        sExcelHeader += "<Font />" & vbCrLf
        sExcelHeader += "<Interior />" & vbCrLf
        sExcelHeader += "<NumberFormat />" & vbCrLf
        sExcelHeader += "<Protection />" & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s21"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Font x:Family=""Swiss"" ss:Bold=""1"" />" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""sB"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Font x:Family=""Swiss"" ss:Bold=""1"" />" & vbCrLf
        'sExcelHeader += "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""sHead"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Font x:Family=""Swiss"" ss:Bold=""1"" />" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#00FFFF"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s24"">" & vbCrLf
        sExcelHeader += "<Alignment ss:Horizontal=""Center"" ss:Vertical=""Bottom""/>" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Font x:Family=""Swiss"" ss:Bold=""1"" />" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""sMer"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s22"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""red"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#FF0000"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""green"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#008000"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""yellow"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#FFFF00"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s16"" ss:Name=""Comma"">" & vbCrLf
        sExcelHeader += "<NumberFormat ss:Format=""_(* ###0.00_);_(* \(###0.00\);_(* &quot;-&quot;??_);_(@_)""/>" & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s23"" ss:Parent=""s16"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<NumberFormat ss:Format=""_(* ###0_);_(* \(###0\);_(* &quot;-&quot;??_);_(@_)""/>" & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s25"" ss:Parent=""s16"">" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Font x:Family=""Swiss"" ss:Bold=""1"" />" & vbCrLf
        sExcelHeader += "<NumberFormat ss:Format=""_(* ###0_);_(* \(###0\);_(* &quot;-&quot;??_);_(@_)""/>" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s26"" >" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<NumberFormat ss:Format=""Fixed""/>" & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s27"" >" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Font x:Family=""Swiss"" ss:Bold=""1"" />" & vbCrLf
        sExcelHeader += "<NumberFormat ss:Format=""Fixed""/>" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#C0C0C0"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf

        sExcelHeader += "<Style ss:ID=""s28"" >" & vbCrLf
        sExcelHeader += "<Borders>" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1"" /> " & vbCrLf
        sExcelHeader += "<Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "<Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1"" />" & vbCrLf
        sExcelHeader += "</Borders>" & vbCrLf
        sExcelHeader += "<Font x:Family=""Swiss"" ss:Bold=""1"" />" & vbCrLf
        sExcelHeader += "<Interior ss:Color=""#FF0000"" ss:Pattern=""Solid"" /> " & vbCrLf
        sExcelHeader += "</Style>" & vbCrLf
        sExcelHeader += "</Styles>" & vbCrLf
        xlWriter.Write(sExcelHeader)
    End Sub
    Public Sub NewSheet(ByVal sSheetName As String)
        With xlWriter
            .WriteLine("<Worksheet ss:Name=" & """" & sSheetName & """" & ">")
            .WriteLine("<Table>")
        End With
    End Sub
    Public Sub EndSheet()
        With xlWriter
            .WriteLine("</Table>")
            .WriteLine("<WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">")
            .WriteLine("<ProtectObjects>False</ProtectObjects>")
            .WriteLine("<ProtectScenarios>False</ProtectScenarios>")
            .WriteLine("</WorksheetOptions>")
            .WriteLine("</Worksheet>")
        End With
    End Sub
    Public Sub BeginRow()
        With xlWriter
            .WriteLine("<Row>")
        End With
    End Sub

    Public Sub BeginRowHide()
        With xlWriter
            .WriteLine("<Row ss:Hidden = ""1"">")
        End With
    End Sub

    Public Sub AddCell(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s22""><Data ss:Type=""String"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellred(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""red""><Data ss:Type=""String"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellgreen(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""green""><Data ss:Type=""String"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellYellow(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""yellow""><Data ss:Type=""String"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellm(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:Index=""2"" ss:StyleID=""s22""><Data ss:Type=""String"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub

    Public Sub AddCellwithpopup(ByVal sCellValue As String, ByVal popup As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s28""><Data ss:Type=""String"" x:Ticked=""1"">" & sCellValue & "</Data>")
            .WriteLine("<Comment ss:Author=""gnsa""><ss:Data xmlns=""http://www.w3.org/TR/REC-html40""><Font html:Face=""Tahoma"" html:Size=""8""  html:Color=""#000000"">" & popup & "&#10;</Font></ss:Data></Comment>")
            .WriteLine("</Cell>")
        End With
    End Sub
    Public Sub AddCellnemeric(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s23""><Data ss:Type=""Number"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCelldouble(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s26""><Data ss:Type=""Number"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCelldoublegry(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s27""><Data ss:Type=""Number"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellempty(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell><Data ss:Type=""String"" x:Ticked=""1"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellboldYel(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s21""><Data ss:Type=""String"" >" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellBold(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""sB""><Data ss:Type=""String"" >" & sCellValue & "</Data></Cell>")
        End With
    End Sub

    Public Sub AddCellHeadingBold(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""sHead""><Data ss:Type=""String"" >" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellboldgry(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s24""><Data ss:Type=""String"" >" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellboldnumericgry(ByVal sCellValue As String)
        With xlWriter
            .WriteLine("<Cell ss:StyleID=""s25""><Data ss:Type=""Number"" >" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub AddCellboldgryrowspan(ByVal sCellValue As String, ByVal lnSpanCount As Integer)
        With xlWriter
            .WriteLine("<Cell ss:Index=""" & lnSpanCount & """ ss:StyleID=""s24""><Data ss:Type=""String"" >" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub MergeAddCellcol(ByVal sCellValue As String, ByVal lnSpanCount As Integer)
        With xlWriter
            .WriteLine("<Cell ss:MergeAcross=""" & lnSpanCount - 2 & """ ss:StyleID=""s24"" ><Data ss:Type=""String"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub

    Public Sub MergeAddCellcolWithoutAlign(ByVal sCellValue As String, ByVal lnSpanCount As Integer)
        With xlWriter
            .WriteLine("<Cell ss:MergeAcross=""" & lnSpanCount - 2 & """ ss:StyleID=""sMer"" ><Data ss:Type=""String"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub MergeAddCellrow(ByVal sCellValue As String, ByVal lnSpanCount As Integer)
        With xlWriter
            .WriteLine("<Cell ss:MergeDown=""" & lnSpanCount - 2 & """ ss:StyleID=""s24"" ><Data ss:Type=""String"">" & sCellValue & "</Data></Cell>")
        End With
    End Sub
    Public Sub EndRow()
        With xlWriter
            .WriteLine("</Row>")
        End With
    End Sub
    Public Sub Close()
        With xlWriter
            .WriteLine("</Workbook>")
            .Close()
        End With
    End Sub
End Class


