Public Class frmMain
    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TestFlag As Boolean = True
        Dim lsSql As String = ""
        Dim lsFolder As String = ""

        Me.Visible = False

        TestFlag = False

        If TestFlag = False Then
            '' Searches the START-UP File Path....
            If Not FileIO.FileSystem.FileExists(Application.StartupPath & "\AppConfig.ini") Then
                MessageBox.Show("Configuration File is Missing", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End
            Else
                Call main()
                gobjSecurity.SetUserRights(Me.MenuStrip.Items)
            End If
        Else
            MsgBox("Test Database !", MsgBoxStyle.Information, gProjectName)
            'MsgBox("Post Almara Entry", MsgBoxStyle.Information, gProjectName)

            ServerDetails = "Driver={Mysql odbc 3.51 Driver};Server=localhost;port=3306;DataBase=Chola;uid=root;pwd=root"
            'ServerDetails = "Driver={Mysql odbc 3.51 Driver};Server=192.168.250.124;port=4001;DataBase=Chola;uid=dba;pwd=Dba@Flexi246"
            'ServerDetails = "Driver={Mysql odbc 3.51 Driver};Server=192.168.250.19;port=3306;DataBase=Chola;uid=root;pwd=gnsa"
            'ServerDetails = "Driver={Mysql odbc 3.51 Driver};Server=192.168.0.127;port=3306;DataBase=Chola;uid=root;pwd=asng"
            gobjSecurity.LoginUserGroupGID = "2"
            'Live

            Call ConOpenOdbc(ServerDetails)
            'SPDCPatchUpdate.ShowDialog()
            'End
        End If

        Me.Visible = True

        lblstatus.Text = ""
        lblUser.Text = "Module : " & gProjectName & " ...        User Name : " & gUserName

        MyBase.WindowState = FormWindowState.Maximized

        Call Patch()

        lsFolder = Application.StartupPath

        If gUserName <> "" Then
            lsFolder &= "\" & gUserName
            If IO.Directory.Exists(lsFolder) = False Then IO.Directory.CreateDirectory(lsFolder)
        End If

        gsReportPath = lsFolder & "\Report"
        If IO.Directory.Exists(gsReportPath) = False Then IO.Directory.CreateDirectory(gsReportPath)
        gsReportPath &= "\"

        gsAsciiFilePath = lsFolder & "\Ascii"
        If IO.Directory.Exists(gsAsciiFilePath) = False Then IO.Directory.CreateDirectory(gsAsciiFilePath)
    End Sub

    Private Sub Patch()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lnResult As Long

        lsSql = "show fields from chola_trn_tspdcpullout where Field = 'spdcpullout_remarks'"

        Call gpDataSet(lsSql, "tab", gOdbcConn, ds)

        If ds.Tables("tab").Rows.Count = 0 Then
            lsSql = "alter table chola_trn_tspdcpullout add spdcpullout_remarks varchar(255) default null after spdcpullout_reasongid"
            lnResult = gfInsertQry(lsSql, gOdbcConn)
        End If

        ds.Tables("tab").Rows.Clear()

        ' 09-04-2014
        lsSql = "show fields from chola_rpt_tchqqry where Field = 'chq_amount'"

        Call gpDataSet(lsSql, "tab", gOdbcConn, ds)

        If ds.Tables("tab").Rows.Count = 0 Then
            lsSql = "alter table chola_rpt_tchqqry add chq_amount double(15,2) not null default 0 after chq_no"
            lnResult = gfInsertQry(lsSql, gOdbcConn)
        End If

        ds.Tables("tab").Rows.Clear()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        If MsgBox("Do you want to Quit?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            End
        End If
    End Sub

    Private Sub FileImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileImport.Click
        Dim objfrm As New frmmstfile
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuHandsoffType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHandsoffType.Click
        Dim objfrm As New frmmstpulloutreason
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuALMARAReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuALMARAReport.Click
        Dim objfrm As New frminventory
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub PDCToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDCToolStripMenuItem1.Click
        Dim objfrm As New frmFileReport("PDC")
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub SPDCToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPDCToolStripMenuItem1.Click
        Dim objfrm As New frmFileReport("SPDC")
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Dim objfrm As New frmEntryReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub CholaMISToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CholaMISToolStripMenuItem.Click
        Dim objfrm As New frmcholaMIS
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuClosureDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClosureDump.Click
        Dim objfrm As New frmclosureimport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPacketPullout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPacketPullout.Click
        Dim objfrm As New frmPacketPullout
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuFinoneDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFinoneDump.Click
        Dim objfrm As New frmfinoneimport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBatchReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchReport.Click
        Dim objfrm As New frmBatchSummReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuFinoneMIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFinoneMIS.Click
        Dim objfrm As New frmFinoneReconNew
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuCycleReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCycleReport.Click
        Dim objfrm As New frmcyclereport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub


    Private Sub mnuInwardDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInwardDump.Click
        Dim objfrm As New frminwardimport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuInwardReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInwardReport.Click
        Dim objfrm As New frminwardreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPouchInward_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPouchInward.Click
        Dim objfrm As New frmpouchinward
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuauthentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuauthentry.Click
        Dim objfrm As New frmauthsummary
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPacketEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPacketEntry.Click
        Dim objfrm As New frminwardsummary
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuchqpulloutentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuchqpulloutentry.Click
        Dim objfrm As New frmpulloutentry
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPulloutSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPulloutSummary.Click
        Dim objfrm As New frmPulloutReverseSummary
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPulloutReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPulloutReverse.Click
        Dim objfrm As New frmUndoPullout
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnupacketlevelpullout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupacketlevelpullout.Click
        Dim objfrm As New frmpacketpulloutsummary
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuclosure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuclosure.Click
        Dim objfrm As New frmclosuresummary
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub AuthDumpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AuthDumpToolStripMenuItem.Click
        Dim objfrm As New frmauthimport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnupacketreentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupacketreentry.Click
        Dim objfrm As New frmreinwardsummary
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBatchCreation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchCreation.Click
        Dim objfrm As New frmBatchCreation
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBatchDataEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchDataEntry.Click
        Dim objfrm As New frmbatchentry
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBatchDespatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchDespatch.Click
        Dim objfrm As New frmbatchdespatch
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBatchRebatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchRebatch.Click
        Dim objfrm As New frmrebatch
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBatchPulloutSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchPulloutSummary.Click
        Dim objfrm As New frmbatchsummary
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuSingleDataEntrys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSingleDataEntrys.Click
        Dim objfrm As New frmBatchSingleEntry
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuManualBatchs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManualBatchs.Click
        Dim objfrm As New frmmanualbatch
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuChequeEntrys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChequeEntrys.Click
        Dim objfrm As New frmchqentry
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuRemoveChequeEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoveChequeEntry.Click
        Dim objfrm As New frmchqentrydelete
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnualmaraPDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnualmaraPDC.Click
        Dim objfrm As New frmalmaraentry("PDC")
        objfrm.ShowDialog()
    End Sub

    Private Sub frmalmaraSPDC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmalmaraSPDC.Click
        Dim objfrm As New frmalmaraentry("SPDC")
        objfrm.ShowDialog()
    End Sub

    Private Sub DespatchReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DespatchReportToolStripMenuItem.Click
        Dim objfrm As New frmbatchdespatchreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBounceReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBounceReason.Click
        Dim objfrm As New frmbouncereason
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBounceEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBounceEntry.Click
        Dim objfrm As New frmbounceentry
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBouncePulloutEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBouncePulloutEntry.Click
        Dim objfrm As New frmbouncepulloutentry
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuInwardMIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInwardMIS.Click
        Dim objfrm As New frmInwardMisNew
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnupouchnotrecvd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupouchnotrecvd.Click
        Dim objfrm As New frmpouchnotreceived
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnupacketupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupacketupdate.Click
        Dim objfrm As New frmupdatepacket
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnupouchrejectreverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupouchrejectreverse.Click
        Dim objfrm As New frmpouchrejectreverse
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuGetGNSAREF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGetGNSAREF.Click
        Dim objfrm As New frmgetrefno
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuupdaterefno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuupdaterefno.Click
        Dim objfrm As New frmupdaterefno
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuProductUpdateDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProductUpdateDump.Click
        Dim objfrm As New frmproductupdate
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPostProductupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostProductupdate.Click
        Dim objfrm As New frmpostproductupdate
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPreConverFinoneDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreConverFinoneDump.Click
        Dim objfrm As New frmfinonepreconverfileimport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuFinonePreconversionMIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFinonePreconversionMIS.Click
        Dim objfrm As New frmfinonepreconverrecon
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnubounceinward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubounceinward.Click
        Dim objfrm As New frmbounceinward
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuSOSMIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSOSMIS.Click
        Dim objfrm As New frmsosmis
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuWarehousingMIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWarehousingMIS.Click
        Dim objfrm As New frmwarehouseMIS
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuupdateagreement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuupdateagreement.Click
        Dim objfrm As New frmupdateagreementno
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuCTSAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCTSAudit.Click
        Dim objfrm As New frmctsauditsummary
        objfrm.MdiParent = Me
        objfrm.Tag = "Old"
        objfrm.Show()
    End Sub

    Private Sub mnuCTSReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCTSReport.Click
        Dim objfrm As New frmctsreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBounceDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBounceDump.Click
        Dim objfrm As New frmbounceimport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuRebatchProductWise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRebatchProductWise.Click
        Dim objfrm As New frmrebacthprodwise
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnentryreverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnentryreverse.Click
        Dim objfrm As New frmauthremovalpostentry
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuRejectReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRejectReason.Click
        Dim objfrm As New frmAuthrejectreason
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuRemoveFinoneData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoveFinoneData.Click
        Dim objfrm As New frmremovefinone
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnupostpreconvdisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupostpreconvdisc.Click
        Dim objfrm As New frmgenerateprecondisc
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPreconversionDisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreconversionDisc.Click
        Dim objfrm As New frmpreconvdiscreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPostconversionDiscReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostconversionDiscReport.Click
        Dim objfrm As New frmpostconvdiscreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnupostpostconversiondisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnupostpostconversiondisc.Click
        Dim objfrm As New frmgeneratepostcondisc
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnureverseprecondisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnureverseprecondisc.Click
        Dim objfrm As New frmreversepreconversiondisc
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuremovepreconversion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremovepreconversion.Click
        Dim objfrm As New frmremovefinonepreconversion
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuMismatchReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMismatchReport.Click
        Dim objfrm As New frmmismatchreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuremovefinoneunmatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremovefinoneunmatched.Click
        Dim objfrm As New frmUndoFinoneMapping
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuCTSSummaryReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCTSSummaryReport.Click
        Dim objfrm As New frmctssummaryreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuHolidayMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHolidayMaster.Click
        Dim objfrm As New frmholidayreason
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuForeClosureReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuForeClosureReport.Click
        Dim objfrm As New frmclosurereport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuFinoneReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFinoneReport.Click
        Dim objfrm As New frmfinonereport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuauthdateupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuauthdateupdate.Click
        Dim objfrm As New frmauthdateupdate
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBankMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBankMaster.Click
        Dim objfrm As New frmbankmaster
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub CityMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CityMasterToolStripMenuItem.Click
        Dim objfrm As New frmcitymaster
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBankReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBankReport.Click
        Dim objfrm As New frmbankreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuCityReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCityReport.Click
        Dim objfrm As New frmcityreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPacketAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPacketAudit.Click
        Dim objfrm As New frmctsauditsummary
        objfrm.MdiParent = Me
        objfrm.Tag = "New"
        objfrm.Show()
    End Sub

    Private Sub mnuAgrmtRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAgrmtRpt.Click
        Dim objfrm As New frmAgreementReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuQryRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQryRpt.Click
        Dim objfrm As New frmQryReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuQryEngine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQryEngine.Click
        Dim objfrm As New frmReportQryMaking
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuSearchEngine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSearchEngine.Click
        Dim objfrm As New frmSearchEngine
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuMisCts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMisCts.Click
        Dim objfrm As New frmPktMis
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuStockMis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStockMis.Click
        Dim objfrm As New frmStockMis
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPostFinoneWithPacket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostFinoneWithPacket.Click
        Dim objfrm As New frmPost(1)
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuDelInward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelInward.Click
        Dim objfrm As New frmDelInwardFile
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuBnceDumpRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBnceDumpRpt.Click
        Dim objfrm As New frmBounceDumpReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub BounceEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BounceEntryToolStripMenuItem.Click
        Dim objfrm As New frmBounceEntryReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuImpChqQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImpChqQuery.Click
        Dim frm As New frmImport(1)
        frm.ShowDialog()
    End Sub

    Private Sub mnuBnceOldRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBnceOldRpt.Click
        Dim objfrm As New frmbouncereport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBnceInwdRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBnceInwdRpt.Click
        Dim objfrm As New frmbounceinwardreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuBnceMis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBnceMis.Click
        Dim objfrm As New frmBounceMis
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub BounceInwardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BounceInwardToolStripMenuItem.Click
        Dim objfrm As New frmBounceInwardMis
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuOldPktEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOldPktEntry.Click
        Dim frm As New frmOldSwapPktEntry
        frm.ShowDialog()
    End Sub

    Private Sub mnuOldPktCancelEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOldPktCancelEntry.Click
        Dim frm As New frmOldSwapPktCancelEntry(GCOLDSWAPPKTCANCELLED)
        frm.ShowDialog()
    End Sub

    Private Sub OldPacketPulloutEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OldPacketPulloutEntryToolStripMenuItem.Click
        Dim frm As New frmOldSwapPktCancelEntry(GCOLDSWAPPKTPULLOUT)
        frm.ShowDialog()
    End Sub

    Private Sub mnuOldPktChqTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOldPktChqTransfer.Click
        Dim frm As New frmTransferCheque
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuRevOldPktChqTran_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRevOldPktChqTran.Click
        Dim frm As New frmRevTransferCheque
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuImpShortAgmntQry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImpShortAgmntQry.Click
        Dim frm As New frmSwapList
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuRevOldPktPullout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRevOldPktPullout.Click
        Dim frm As New frmOldSwapPktCancelEntry(GCOLDSWAPPKTPULLOUTUNDO)
        frm.ShowDialog()
    End Sub

    Private Sub mnuFindSwapOldPkt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFindSwapOldPkt.Click
        Dim frm As New frmFindOldSwapPkt
        frm.ShowDialog()
    End Sub

    Private Sub mnuDepSwap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDepSwap.Click
        Dim objfrm As New frmDelSwapFile
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuImpSwap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImpSwap.Click
        Dim frm As New frmImport(3)
        frm.ShowDialog()
    End Sub

    Private Sub mnuSwapOldPktReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSwapOldPktReport.Click
        Dim objfrm As New frmSwapReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuSwapListReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSwapListReport.Click
        Dim objfrm As New frmSwapListReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPostWithFinone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostWithFinone.Click
        Dim objfrm As New frmpostwithfinone
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuPulloutReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPulloutReport.Click
        Dim objfrm As New frmPulloutReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuSpdcPullout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSpdcPullout.Click
        Dim objfrm As New frmSpdcPulloutEntry
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuSpdcPulloutReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSpdcPulloutReport.Click
        Dim objfrm As New frmSPDCPulloutReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuSearchEngineChq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSearchEngineChq.Click
        Dim objfrm As New frmChqSearchEngine
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuLooseChqEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLooseChqEntry.Click
        Dim objfrm As New frmLooseChqEntry
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuLooseChqReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLooseChqReport.Click
        Dim objfrm As New frmLooseEntryReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuDelBounceEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelBounceEntry.Click
        Dim objfrm As New frmBounceEntryDelete
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPouchInwardAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPouchInwardAuth.Click
        Dim objfrm As New frmPouchInwardAuth
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuStockNewMis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStockNewMis.Click
        Dim objfrm As New frmStockMisNew
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPktPulloutRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPktPulloutRpt.Click
        Dim objfrm As New frmPacketPulloutReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPktContentRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPktContentRpt.Click
        Dim objfrm As New frmPktDetailRpt
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPktRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPktRpt.Click
        Dim objfrm As New frmPacketReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPdcSpdcPullout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPdcSpdcPullout.Click
        Dim objfrm As New frmPdcSpdcPulloutEntry
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuPktPulloutNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPktPulloutNew.Click
        Dim objfrm As New frmPacketPulloutNew
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuUpdPktPayMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUpdPktPayMode.Click
        Dim objfrm As New frmChngPktPayMode
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuImpPktPayMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImpPktPayMode.Click
        Dim frm As New frmImport(4)
        frm.ShowDialog()
    End Sub

    Private Sub mnuChqNoAmtQry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuChqNoAmtQry.Click
        Dim frm As New frmImport(5)
        frm.ShowDialog()
    End Sub

    Private Sub mnuPostPostConvDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostPostConvDump.Click
        Dim objfrm As New frmpostfinone(False)
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuUndoPostConvDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUndoPostConvDump.Click
        Dim objfrm As New frmpostfinone(True)
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuPostPreConvDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostPreConvDump.Click
        Dim objfrm As New frmpostfinonepreconver(False)
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuUndoPreConvDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUndoPreConvDump.Click
        Dim objfrm As New frmpostfinonepreconver(True)
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuDelPreConvDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelPreConvDump.Click
        Dim frmObj As New frmDeleteFile("chola_mst_tfile", "file_gid", "import_on", "concat(file_name,ifnull(concat('-',file_sheetname),''))", "", _
                                        "chola_trn_tfinonepreconverfile", "finone_file_gid", "true", "and finone_entry_gid > 0", _
                                        "Delete Preconversion File", True, False)

        frmObj.ShowDialog()
    End Sub

    Private Sub mnuDelPostConvDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelPostConvDump.Click
        Dim frmObj As New frmDeleteFile("chola_mst_tfile", "file_gid", "import_on", "concat(file_name,ifnull(concat('-',file_sheetname),''))", "", _
                                        "chola_trn_tfinone", "finone_file_gid", "true", "and finone_entrygid > 0", _
                                        "Delete Postconversion File", True, False)

        frmObj.ShowDialog()
    End Sub

    Private Sub mnuImpFileReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImpFileReport.Click
        Dim objfrm As New frmImpFileReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPostconvRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPostconvRpt.Click
        Dim objfrm As New frmPostconvReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPreconvRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreconvRpt.Click
        Dim objfrm As New frmPreconvReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPktBlockRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPktBlockRpt.Click
        Dim objfrm As New frmPacketBlockReport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuPktBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPktBlock.Click
        Dim objfrm As New frmPacketBlock
        objfrm.ShowDialog()
    End Sub

    Private Sub mnuBatchTally_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBatchTally.Click
        Dim objfrm As New frmbatchreport
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuProductivity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProductivity.Click
        Dim objfrm As New frmProductivityMIS
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub mnuInwardMisRevised_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuInwardMisRevised.Click
        Dim objfrm As New frmInwardMisNew(True)
        objfrm.MdiParent = Me
        objfrm.Show()
    End Sub

    Private Sub MnuReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuReport.Click

    End Sub
End Class
