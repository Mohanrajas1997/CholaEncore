Imports FlexiLibrary
Imports System.Data.Odbc
Module Module1
    Public gobjConnection As New iODBCconnection                                                        ' Connection Object 
    Public gobjSecurity As New iSecurity                                                                ' Security Object
    Public gbAccValidationFlag As Boolean = True

    Public Sub main()
        With gobjSecurity
            .LoginCaption = "ENCORE"                                                                      ' Login Caption       
            .LoginSoftCode = "ENCORE"                                                                     ' Login Software Code.
            .LoginSoftVersion = "1.0.0"                                                                 ' Login Software Version.
            .GetConfig(Application.StartupPath & "\AppConfig.ini")
            If Environment.GetCommandLineArgs.Length = 2 Then
                .LoginFromProcess = Environment.GetCommandLineArgs(1)
            End If
            .ShowLoginDialog()

            If Not .LoginState Then
                End
            Else
                If .DbApplication <> "" Then gobjConnection.OpenConnection(gobjSecurity)
            End If

            ServerDetails = "Driver={Mysql odbc 3.51 Driver};Server=" & .DbIP & ";DataBase=" & .DbName & ";uid=" & .DbUserName & ";pwd=" & .DbPassword
            ServerDetailsQry = "Driver={Mysql odbc 3.51 Driver};Server=" & .DbIP & ";DataBase=" & .DbName & ";uid=query;pwd=query"
            Call ConOpenOdbc(ServerDetails)

            If Environment.GetCommandLineArgs.Length = 2 Then
                .LoginFromProcess = Environment.GetCommandLineArgs(1)
                gUserName = iRoutines.Decryption(Command)
            Else
                gUserName = .LoginUserCode
            End If

            gUid = .LoginUserGID
            gUserFullName = .LoginUserName
            gUserRights = .LoginUserDesignationGID
            frmMain.Text = "Encore Vault Management System - Version " & .LoginSoftVersion
        End With
    End Sub

    Public Function ValidateSPDC(ByVal AgrNo As String, ByVal ChqNo As String, ByVal MicrCode As String, ByVal AccNo As String) As Boolean
        Dim ds As New DataSet
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select pdc_gid,pdc_micrcode,pdc_acc_no from chola_trn_tpdcfile "
        lsSql &= " where pdc_parentcontractno = '" & AgrNo & "' "
        lsSql &= " and pdc_chqno = " & Val(ChqNo) & " "
        lsSql &= " and (entry_gid is null or entry_gid = 0) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "
        lsSql &= " and chq_rec_slno = 1 "

        Call gpDataSet(lsSql, "spdc", gOdbcConn, ds)

        With ds.Tables("spdc")
            If .Rows.Count > 0 Then
                If AccNo <> "" And gbAccValidationFlag = True Then
                    If AccNo <> .Rows(0).Item("pdc_acc_no").ToString Then
                        If MsgBox("A/C no mismatch ! Are you sure to add ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, gProjectName) = MsgBoxResult.No Then
                            Return False
                        Else
                            gbAccValidationFlag = False
                        End If
                    End If
                End If

                If .Rows(0).Item("pdc_micrcode").ToString = MicrCode Then
                    Return True
                Else
                    If MsgBox("SPDC micrcode mismatch ! Are you sure to add ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gProjectName) = MsgBoxResult.Yes Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If

            .Rows.Clear()
        End With
    End Function

    Public Function ValidateECS(ByVal AgrNo As String, ByVal EcsDate As Date, ByVal EcsAmt As Double, ByVal MicrCode As String, ByVal AccNo As String) As Boolean
        Dim ds As New DataSet
        Dim lsSql As String

        lsSql = ""
        lsSql &= " select pdc_gid,pdc_acc_no from chola_trn_tpdcfile "
        lsSql &= " where pdc_parentcontractno = '" & AgrNo & "' "
        lsSql &= " and pdc_chqdate = '" & Format(EcsDate, "yyyy-MM-dd") & "' "
        lsSql &= " and pdc_chqamount = " & EcsAmt & " "
        lsSql &= " and pdc_micrcode = '" & MicrCode & "' "

        'If IsNumeric(AccNo) = True Then
        '    lsSql &= " and (pdc_acc_no = '" & AccNo & "' "
        '    lsSql &= " or pdc_acc_no = '" & Val(AccNo) & "') "
        'Else
        '    lsSql &= " and pdc_acc_no = '" & AccNo & "' "
        'End If

        lsSql &= " and (entry_gid is null or entry_gid = 0) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "
        lsSql &= " and chq_rec_slno = 1 "

        Call gpDataSet(lsSql, "ecs", gOdbcConn, ds)

        With ds.Tables("ecs")
            If .Rows.Count > 0 Then
                If AccNo <> "" And gbAccValidationFlag = True Then
                    If .Rows(0).Item("pdc_acc_no").ToString <> AccNo Then
                        If MsgBox("A/C no mismatch ! Are you sure to add ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, gProjectName) = MsgBoxResult.No Then
                            Return False
                        Else
                            gbAccValidationFlag = False
                            Return True
                        End If
                    Else
                        Return True
                    End If
                End If
            End If

            .Rows.Clear()
        End With

        Return False
    End Function

    Public Sub UpdatePacket(ByVal PktId As Long)
        Dim lssql As String
        Dim drpdc As Odbc.OdbcDataReader
        Dim objdt As DataTable
        Dim lnPdcGid As Long

        lssql = ""
        lssql &= " select entry_gid,shortagreement_no,agreement_no,agreement_gid,chq_no,chq_date,chq_amount,chq_type  "
        lssql &= " from chola_trn_tpdcentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chq_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid"
        lssql &= " where packet_gid = " & PktId
        lssql &= " and chq_pdc_gid=0"

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lssql = " select pdc_gid from chola_trn_tpdcfile "
            lssql &= " where chq_rec_slno=1 "
            lssql &= " and pdc_chqno = " & Val(objdt.Rows(i).Item("chq_no").ToString) & " "
            lssql &= " and pdc_parentcontractno='" & objdt.Rows(i).Item("agreement_no").ToString & "' "

            If objdt.Rows(i).Item("chq_type") = GCEXTERNALNORMAL Then
                lssql &= " and pdc_chqamount = " & objdt.Rows(i).Item("chq_amount") & " "
                lssql &= " and pdc_chqdate = '" & Format(objdt.Rows(i).Item("chq_date"), "yyyy-MM-dd") & "' "
            End If

            lssql &= " and (entry_gid = 0 or entry_gid is null) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "

            drpdc = gfExecuteQry(lssql, gOdbcConn)

            If drpdc.HasRows Then
                While drpdc.Read
                    lnPdcGid = drpdc.Item("pdc_gid").ToString
                End While
            Else
                lnPdcGid = GCZERO
            End If

            drpdc.Close()

            If lnPdcGid > 0 Then
                lssql = ""
                lssql &= " update chola_trn_tpdcentry set "
                lssql &= " chq_pdc_gid=" & lnPdcGid & ","
                lssql &= " chq_agreement_gid=" & objdt.Rows(i).Item("agreement_gid").ToString & " "
                lssql &= " where entry_gid=" & objdt.Rows(i).Item("entry_gid").ToString
                lssql &= " and chq_pdc_gid=0"

                gfInsertQry(lssql, gOdbcConn)

                lssql = ""
                lssql &= " UPDATE"
                lssql &= " chola_trn_tpdcfile"
                lssql &= " set"
                lssql &= " entry_gid=" & objdt.Rows(i).Item("entry_gid").ToString & " "
                lssql &= " where 1=1"
                lssql &= " and pdc_gid=" & lnPdcGid
                lssql &= " and (entry_gid = 0 or entry_gid is null) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "

                gfInsertQry(lssql, gOdbcConn)
            End If
        Next i

        objdt.Rows.Clear()

        ' SPDC
        lssql = ""
        lssql &= " select chqentry_gid,shortagreement_no,agreement_no,agreement_gid,chqentry_chqno  "
        lssql &= " from chola_trn_tspdcchqentry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=chqentry_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid"
        lssql &= " where packet_gid = " & PktId
        lssql &= " and chqentry_pdc_gid=0"

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lssql = " select pdc_gid from chola_trn_tpdcfile "
            lssql &= " where chq_rec_slno=1 "
            lssql &= " and pdc_chqno = " & Val(objdt.Rows(i).Item("chqentry_chqno").ToString) & " "
            lssql &= " and pdc_parentcontractno='" & objdt.Rows(i).Item("agreement_no").ToString & "' "
            lssql &= " and (entry_gid = 0 or entry_gid is null) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "

            drpdc = gfExecuteQry(lssql, gOdbcConn)

            If drpdc.HasRows Then
                While drpdc.Read
                    lnPdcGid = drpdc.Item("pdc_gid").ToString
                End While
            Else
                lnPdcGid = GCZERO
            End If

            drpdc.Close()

            If lnPdcGid > 0 Then
                lssql = ""
                lssql &= " update chola_trn_tspdcchqentry set "
                lssql &= " chqentry_pdc_gid=" & lnPdcGid & " "
                lssql &= " where chqentry_gid = " & objdt.Rows(i).Item("chqentry_gid").ToString
                lssql &= " and chqentry_pdc_gid = 0"

                gfInsertQry(lssql, gOdbcConn)

                lssql = ""
                lssql &= " UPDATE"
                lssql &= " chola_trn_tpdcfile"
                lssql &= " set"
                lssql &= " pdc_spdcentry_gid =" & objdt.Rows(i).Item("chqentry_gid").ToString & " "
                lssql &= " where 1=1"
                lssql &= " and pdc_gid=" & lnPdcGid
                lssql &= " and (entry_gid = 0 or entry_gid is null) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "

                gfInsertQry(lssql, gOdbcConn)
            End If
        Next i

        objdt.Rows.Clear()

        ' ECS
        lssql = ""
        lssql &= " select ecsemientry_gid,shortagreement_no,agreement_no,agreement_gid,ecsemientry_emidate,ecsemientry_amount  "
        lssql &= " from chola_trn_tecsemientry "
        lssql &= " inner join chola_trn_tpacket on packet_gid=ecsemientry_packet_gid "
        lssql &= " inner join chola_mst_tagreement on agreement_gid=packet_agreement_gid"
        lssql &= " where packet_gid = " & PktId
        lssql &= " and ecsemientry_pdc_gid = 0"

        objdt = GetDataTable(lssql)

        For i As Integer = 0 To objdt.Rows.Count - 1
            lssql = " select pdc_gid from chola_trn_tpdcfile "
            lssql &= " where chq_rec_slno=1 "
            lssql &= " and pdc_parentcontractno='" & objdt.Rows(i).Item("agreement_no").ToString & "' "
            lssql &= " and pdc_chqdate = '" & Format(objdt.Rows(i).Item("ecsemientry_emidate"), "yyyy-MM-dd") & "' "
            lssql &= " and pdc_chqamount = " & objdt.Rows(i).Item("ecsemientry_amount") & " "
            lssql &= " and (entry_gid = 0 or entry_gid is null) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "

            drpdc = gfExecuteQry(lssql, gOdbcConn)

            If drpdc.HasRows Then
                While drpdc.Read
                    lnPdcGid = drpdc.Item("pdc_gid").ToString
                End While
            Else
                lnPdcGid = GCZERO
            End If

            drpdc.Close()

            If lnPdcGid > 0 Then
                lssql = ""
                lssql &= " update chola_trn_tecsemientry set "
                lssql &= " ecsemientry_pdc_gid = " & lnPdcGid & " "
                lssql &= " where ecsemientry_gid = " & objdt.Rows(i).Item("ecsemientry_gid").ToString
                lssql &= " and ecsemientry_pdc_gid = 0"

                gfInsertQry(lssql, gOdbcConn)

                lssql = ""
                lssql &= " UPDATE"
                lssql &= " chola_trn_tpdcfile"
                lssql &= " set"
                lssql &= " pdc_ecsentry_gid =" & objdt.Rows(i).Item("ecsemientry_gid").ToString & " "
                lssql &= " where 1=1"
                lssql &= " and pdc_gid=" & lnPdcGid
                lssql &= " and (entry_gid = 0 or entry_gid is null) and pdc_spdcentry_gid = 0 and pdc_ecsentry_gid = 0 "

                gfInsertQry(lssql, gOdbcConn)
            End If
        Next i

        objdt.Rows.Clear()
    End Sub
End Module

