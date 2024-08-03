<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.lblstatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.mnuadmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGetGNSAREF = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuimport = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuClosureDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPacketPullout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFinoneDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInwardDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.AuthDumpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProductUpdateDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPreConverFinoneDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBounceDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBankMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.CityMasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImpSwap = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImpPktPayMode = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImpChqQuery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuChqNoAmtQry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDelInward = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDepSwap = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem17 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDelPreConvDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDelPostConvDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnumaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHandsoffType = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBounceReason = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRejectReason = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHolidayMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuprocesss = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPouchInward = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPouchInwardAuth = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuupdaterefno = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuupdateagreement = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdPktPayMode = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnupouchnotrecvd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnupouchrejectreverse = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnentryreverse = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuauthentry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuauthdateupdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPacketEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnupacketreentry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnupacketupdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnualmara = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnualmaraPDC = New System.Windows.Forms.ToolStripMenuItem()
        Me.frmalmaraSPDC = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPullout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuchqpulloutentry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPulloutSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPulloutReverse = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSpdcPullout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPdcSpdcPullout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnupacketlevelpullout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPktPulloutNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPktBlock = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem19 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLooseChqEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuclosure = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPresentation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchCreation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchDataEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchDespatch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchRebatch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchPulloutSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSingleDataEntrys = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuManualBatchs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuChequeEntrys = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveChequeEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRebatchProductWise = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuremovepreconversion = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnupostpostconversiondisc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnupostpreconvdisc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnureverseprecondisc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPosting = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPostFinoneWithPacket = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPostWithFinone = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuremovefinoneunmatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPostPreConvDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUndoPreConvDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPostPostConvDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUndoPostConvDump = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBounce = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnubounceinward = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBounceEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDelBounceEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuBouncePulloutEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPostProductupdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCTSAudit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveFinoneData = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSwap = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImpShortAgmntQry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuOldPktEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFindSwapOldPkt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOldPktChqTransfer = New System.Windows.Forms.ToolStripMenuItem()
        Me.OldPacketPulloutEntryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOldPktCancelEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuRevOldPktPullout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRevOldPktChqTran = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPacketAudit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.PDCToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SPDCToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuALMARAReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.CholaMISToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBatchReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFinoneMIS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFinonePreconversionMIS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCycleReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInwardReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.DespatchReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInwardMIS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSOSMIS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWarehousingMIS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCTSReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPreconversionDisc = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPostconversionDiscReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMismatchReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCTSSummaryReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuForeClosureReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFinoneReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPacketReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPktRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPktContentRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPktPulloutRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPktBlockRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMisCts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStockMis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStockNewMis = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuBnceMis = New System.Windows.Forms.ToolStripMenuItem()
        Me.BounceInwardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProductivity = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuInwardMisRevised = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuBnceRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.BounceEntryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBnceDumpRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuBnceInwdRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBnceOldRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSwapReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSwapListReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSwapOldPktReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMastRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAgrmtRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuBankReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCityReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOthersReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPulloutReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSpdcPulloutReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLooseChqReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuImpFileReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem18 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPreconvRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPostconvRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSearchEngine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSearchEngineChq = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuQryRpt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuQryEngine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuwindows = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblstatus, Me.lblUser})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 698)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip.Size = New System.Drawing.Size(1028, 22)
        Me.StatusStrip.TabIndex = 0
        Me.StatusStrip.Text = "StatusStrip"
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = False
        Me.lblstatus.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.lblstatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(500, 17)
        Me.lblstatus.Text = "Status"
        Me.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUser
        '
        Me.lblUser.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(33, 17)
        Me.lblUser.Text = "User"
        '
        'MenuStrip
        '
        Me.MenuStrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuadmin, Me.mnuimport, Me.mnumaster, Me.mnuprocesss, Me.MnuReport, Me.mnuwindows, Me.mnuExit})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.MdiWindowListItem = Me.mnuwindows
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Padding = New System.Windows.Forms.Padding(7, 2, 0, 2)
        Me.MenuStrip.Size = New System.Drawing.Size(1028, 24)
        Me.MenuStrip.TabIndex = 1
        Me.MenuStrip.Text = "MenuStrip"
        '
        'mnuadmin
        '
        Me.mnuadmin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGetGNSAREF})
        Me.mnuadmin.Name = "mnuadmin"
        Me.mnuadmin.Size = New System.Drawing.Size(55, 20)
        Me.mnuadmin.Text = "&Admin"
        '
        'mnuGetGNSAREF
        '
        Me.mnuGetGNSAREF.Name = "mnuGetGNSAREF"
        Me.mnuGetGNSAREF.Size = New System.Drawing.Size(159, 22)
        Me.mnuGetGNSAREF.Text = "&Get GNSA REF#"
        '
        'mnuimport
        '
        Me.mnuimport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileImport, Me.mnuClosureDump, Me.mnuPacketPullout, Me.mnuFinoneDump, Me.mnuInwardDump, Me.AuthDumpToolStripMenuItem, Me.mnuProductUpdateDump, Me.mnuPreConverFinoneDump, Me.mnuBounceDump, Me.mnuBankMaster, Me.CityMasterToolStripMenuItem, Me.mnuImpSwap, Me.mnuImpPktPayMode, Me.mnuImpChqQuery, Me.mnuChqNoAmtQry, Me.ToolStripMenuItem7, Me.mnuDelete})
        Me.mnuimport.Name = "mnuimport"
        Me.mnuimport.Size = New System.Drawing.Size(59, 20)
        Me.mnuimport.Text = "&Import"
        '
        'FileImport
        '
        Me.FileImport.Name = "FileImport"
        Me.FileImport.Size = New System.Drawing.Size(213, 22)
        Me.FileImport.Text = "&File Import"
        Me.FileImport.Visible = False
        '
        'mnuClosureDump
        '
        Me.mnuClosureDump.Name = "mnuClosureDump"
        Me.mnuClosureDump.Size = New System.Drawing.Size(213, 22)
        Me.mnuClosureDump.Text = "&Closure Dump"
        '
        'mnuPacketPullout
        '
        Me.mnuPacketPullout.Name = "mnuPacketPullout"
        Me.mnuPacketPullout.Size = New System.Drawing.Size(213, 22)
        Me.mnuPacketPullout.Text = "&Packet Pullout"
        '
        'mnuFinoneDump
        '
        Me.mnuFinoneDump.Name = "mnuFinoneDump"
        Me.mnuFinoneDump.Size = New System.Drawing.Size(213, 22)
        Me.mnuFinoneDump.Text = "&Final Finone Dump"
        '
        'mnuInwardDump
        '
        Me.mnuInwardDump.Name = "mnuInwardDump"
        Me.mnuInwardDump.Size = New System.Drawing.Size(213, 22)
        Me.mnuInwardDump.Text = "&Inward Dump"
        '
        'AuthDumpToolStripMenuItem
        '
        Me.AuthDumpToolStripMenuItem.Name = "AuthDumpToolStripMenuItem"
        Me.AuthDumpToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.AuthDumpToolStripMenuItem.Text = "&Auth Dump"
        '
        'mnuProductUpdateDump
        '
        Me.mnuProductUpdateDump.Name = "mnuProductUpdateDump"
        Me.mnuProductUpdateDump.Size = New System.Drawing.Size(213, 22)
        Me.mnuProductUpdateDump.Text = "Product &Update Dump"
        '
        'mnuPreConverFinoneDump
        '
        Me.mnuPreConverFinoneDump.Name = "mnuPreConverFinoneDump"
        Me.mnuPreConverFinoneDump.Size = New System.Drawing.Size(213, 22)
        Me.mnuPreConverFinoneDump.Text = "Pr&eConver Finone Dump"
        '
        'mnuBounceDump
        '
        Me.mnuBounceDump.Name = "mnuBounceDump"
        Me.mnuBounceDump.Size = New System.Drawing.Size(213, 22)
        Me.mnuBounceDump.Text = "&Bounce Dump"
        '
        'mnuBankMaster
        '
        Me.mnuBankMaster.Name = "mnuBankMaster"
        Me.mnuBankMaster.Size = New System.Drawing.Size(213, 22)
        Me.mnuBankMaster.Text = "&Bank Master"
        '
        'CityMasterToolStripMenuItem
        '
        Me.CityMasterToolStripMenuItem.Name = "CityMasterToolStripMenuItem"
        Me.CityMasterToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
        Me.CityMasterToolStripMenuItem.Text = "City Master"
        '
        'mnuImpSwap
        '
        Me.mnuImpSwap.Name = "mnuImpSwap"
        Me.mnuImpSwap.Size = New System.Drawing.Size(213, 22)
        Me.mnuImpSwap.Text = "Swap"
        '
        'mnuImpPktPayMode
        '
        Me.mnuImpPktPayMode.Name = "mnuImpPktPayMode"
        Me.mnuImpPktPayMode.Size = New System.Drawing.Size(213, 22)
        Me.mnuImpPktPayMode.Text = "Packet Pay Mode"
        '
        'mnuImpChqQuery
        '
        Me.mnuImpChqQuery.Name = "mnuImpChqQuery"
        Me.mnuImpChqQuery.Size = New System.Drawing.Size(213, 22)
        Me.mnuImpChqQuery.Text = "Cheque Query"
        '
        'mnuChqNoAmtQry
        '
        Me.mnuChqNoAmtQry.Name = "mnuChqNoAmtQry"
        Me.mnuChqNoAmtQry.Size = New System.Drawing.Size(213, 22)
        Me.mnuChqNoAmtQry.Text = "Cheque && Amount Query"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(210, 6)
        '
        'mnuDelete
        '
        Me.mnuDelete.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDelInward, Me.mnuDepSwap, Me.ToolStripMenuItem17, Me.mnuDelPreConvDump, Me.mnuDelPostConvDump})
        Me.mnuDelete.Name = "mnuDelete"
        Me.mnuDelete.Size = New System.Drawing.Size(213, 22)
        Me.mnuDelete.Text = "Delete"
        '
        'mnuDelInward
        '
        Me.mnuDelInward.Name = "mnuDelInward"
        Me.mnuDelInward.Size = New System.Drawing.Size(197, 22)
        Me.mnuDelInward.Text = "Inward"
        '
        'mnuDepSwap
        '
        Me.mnuDepSwap.Name = "mnuDepSwap"
        Me.mnuDepSwap.Size = New System.Drawing.Size(197, 22)
        Me.mnuDepSwap.Text = "Swap"
        '
        'ToolStripMenuItem17
        '
        Me.ToolStripMenuItem17.Name = "ToolStripMenuItem17"
        Me.ToolStripMenuItem17.Size = New System.Drawing.Size(194, 6)
        '
        'mnuDelPreConvDump
        '
        Me.mnuDelPreConvDump.Name = "mnuDelPreConvDump"
        Me.mnuDelPreConvDump.Size = New System.Drawing.Size(197, 22)
        Me.mnuDelPreConvDump.Text = "Preconversion Dump"
        '
        'mnuDelPostConvDump
        '
        Me.mnuDelPostConvDump.Name = "mnuDelPostConvDump"
        Me.mnuDelPostConvDump.Size = New System.Drawing.Size(197, 22)
        Me.mnuDelPostConvDump.Text = "Postconversion Dump"
        '
        'mnumaster
        '
        Me.mnumaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHandsoffType, Me.mnuBounceReason, Me.mnuRejectReason, Me.mnuHolidayMaster})
        Me.mnumaster.Name = "mnumaster"
        Me.mnumaster.Size = New System.Drawing.Size(59, 20)
        Me.mnumaster.Text = "&Master"
        '
        'mnuHandsoffType
        '
        Me.mnuHandsoffType.Name = "mnuHandsoffType"
        Me.mnuHandsoffType.Size = New System.Drawing.Size(160, 22)
        Me.mnuHandsoffType.Text = "&Pullout Reason"
        '
        'mnuBounceReason
        '
        Me.mnuBounceReason.Name = "mnuBounceReason"
        Me.mnuBounceReason.Size = New System.Drawing.Size(160, 22)
        Me.mnuBounceReason.Text = "&Bounce Reason"
        '
        'mnuRejectReason
        '
        Me.mnuRejectReason.Name = "mnuRejectReason"
        Me.mnuRejectReason.Size = New System.Drawing.Size(160, 22)
        Me.mnuRejectReason.Text = "&Reject Reason"
        '
        'mnuHolidayMaster
        '
        Me.mnuHolidayMaster.Name = "mnuHolidayMaster"
        Me.mnuHolidayMaster.Size = New System.Drawing.Size(160, 22)
        Me.mnuHolidayMaster.Text = "&Holiday Master"
        '
        'mnuprocesss
        '
        Me.mnuprocesss.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPouchInward, Me.mnuPouchInwardAuth, Me.mnuupdaterefno, Me.mnuupdateagreement, Me.mnuUpdPktPayMode, Me.mnupouchnotrecvd, Me.mnupouchrejectreverse, Me.mnentryreverse, Me.mnuauthentry, Me.mnuauthdateupdate, Me.mnuPacketEntry, Me.mnupacketreentry, Me.mnupacketupdate, Me.mnualmara, Me.mnuPullout, Me.mnuPresentation, Me.mnuremovepreconversion, Me.mnupostpostconversiondisc, Me.mnupostpreconvdisc, Me.mnureverseprecondisc, Me.mnuPosting, Me.mnuBounce, Me.mnuPostProductupdate, Me.mnuCTSAudit, Me.mnuRemoveFinoneData, Me.ToolStripMenuItem6, Me.mnuSwap, Me.mnuPacketAudit})
        Me.mnuprocesss.Name = "mnuprocesss"
        Me.mnuprocesss.Size = New System.Drawing.Size(63, 20)
        Me.mnuprocesss.Text = "&Process"
        '
        'mnuPouchInward
        '
        Me.mnuPouchInward.Name = "mnuPouchInward"
        Me.mnuPouchInward.Size = New System.Drawing.Size(231, 22)
        Me.mnuPouchInward.Text = "&Pouch Inward"
        '
        'mnuPouchInwardAuth
        '
        Me.mnuPouchInwardAuth.Name = "mnuPouchInwardAuth"
        Me.mnuPouchInwardAuth.Size = New System.Drawing.Size(231, 22)
        Me.mnuPouchInwardAuth.Text = "Pouch Inward and Auth"
        '
        'mnuupdaterefno
        '
        Me.mnuupdaterefno.Name = "mnuupdaterefno"
        Me.mnuupdaterefno.Size = New System.Drawing.Size(231, 22)
        Me.mnuupdaterefno.Text = "Update Refn&o"
        '
        'mnuupdateagreement
        '
        Me.mnuupdateagreement.Name = "mnuupdateagreement"
        Me.mnuupdateagreement.Size = New System.Drawing.Size(231, 22)
        Me.mnuupdateagreement.Text = "Up&date Agreement"
        '
        'mnuUpdPktPayMode
        '
        Me.mnuUpdPktPayMode.Name = "mnuUpdPktPayMode"
        Me.mnuUpdPktPayMode.Size = New System.Drawing.Size(231, 22)
        Me.mnuUpdPktPayMode.Text = "Update Packet Pay Mode"
        '
        'mnupouchnotrecvd
        '
        Me.mnupouchnotrecvd.Name = "mnupouchnotrecvd"
        Me.mnupouchnotrecvd.Size = New System.Drawing.Size(231, 22)
        Me.mnupouchnotrecvd.Text = "Pouch &Not Received"
        '
        'mnupouchrejectreverse
        '
        Me.mnupouchrejectreverse.Name = "mnupouchrejectreverse"
        Me.mnupouchrejectreverse.Size = New System.Drawing.Size(231, 22)
        Me.mnupouchrejectreverse.Text = "Auth &Reverse"
        '
        'mnentryreverse
        '
        Me.mnentryreverse.Name = "mnentryreverse"
        Me.mnentryreverse.Size = New System.Drawing.Size(231, 22)
        Me.mnentryreverse.Text = "Entry Reverse"
        '
        'mnuauthentry
        '
        Me.mnuauthentry.Name = "mnuauthentry"
        Me.mnuauthentry.Size = New System.Drawing.Size(231, 22)
        Me.mnuauthentry.Text = "&Auth Entry"
        '
        'mnuauthdateupdate
        '
        Me.mnuauthdateupdate.Name = "mnuauthdateupdate"
        Me.mnuauthdateupdate.Size = New System.Drawing.Size(231, 22)
        Me.mnuauthdateupdate.Text = "AuthDate Update"
        '
        'mnuPacketEntry
        '
        Me.mnuPacketEntry.Name = "mnuPacketEntry"
        Me.mnuPacketEntry.Size = New System.Drawing.Size(231, 22)
        Me.mnuPacketEntry.Text = "Pa&cket Entry"
        '
        'mnupacketreentry
        '
        Me.mnupacketreentry.Name = "mnupacketreentry"
        Me.mnupacketreentry.Size = New System.Drawing.Size(231, 22)
        Me.mnupacketreentry.Text = "Packet &ReEntry"
        '
        'mnupacketupdate
        '
        Me.mnupacketupdate.Name = "mnupacketupdate"
        Me.mnupacketupdate.Size = New System.Drawing.Size(231, 22)
        Me.mnupacketupdate.Text = "Packet &Update"
        '
        'mnualmara
        '
        Me.mnualmara.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnualmaraPDC, Me.frmalmaraSPDC})
        Me.mnualmara.Name = "mnualmara"
        Me.mnualmara.Size = New System.Drawing.Size(231, 22)
        Me.mnualmara.Text = "Al&mara Entry"
        '
        'mnualmaraPDC
        '
        Me.mnualmaraPDC.Name = "mnualmaraPDC"
        Me.mnualmaraPDC.Size = New System.Drawing.Size(103, 22)
        Me.mnualmaraPDC.Text = "&PDC"
        '
        'frmalmaraSPDC
        '
        Me.frmalmaraSPDC.Name = "frmalmaraSPDC"
        Me.frmalmaraSPDC.Size = New System.Drawing.Size(103, 22)
        Me.frmalmaraSPDC.Text = "&SPDC"
        '
        'mnuPullout
        '
        Me.mnuPullout.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuchqpulloutentry, Me.mnuPulloutSummary, Me.mnuPulloutReverse, Me.mnuSpdcPullout, Me.mnuPdcSpdcPullout, Me.ToolStripMenuItem11, Me.mnupacketlevelpullout, Me.mnuPktPulloutNew, Me.mnuPktBlock, Me.ToolStripMenuItem19, Me.mnuLooseChqEntry, Me.mnuclosure})
        Me.mnuPullout.Name = "mnuPullout"
        Me.mnuPullout.Size = New System.Drawing.Size(231, 22)
        Me.mnuPullout.Text = "Pu&llout"
        '
        'mnuchqpulloutentry
        '
        Me.mnuchqpulloutentry.Name = "mnuchqpulloutentry"
        Me.mnuchqpulloutentry.Size = New System.Drawing.Size(214, 22)
        Me.mnuchqpulloutentry.Text = "Pullout &Entry"
        '
        'mnuPulloutSummary
        '
        Me.mnuPulloutSummary.Name = "mnuPulloutSummary"
        Me.mnuPulloutSummary.Size = New System.Drawing.Size(214, 22)
        Me.mnuPulloutSummary.Text = "Pullout &Summary"
        '
        'mnuPulloutReverse
        '
        Me.mnuPulloutReverse.Name = "mnuPulloutReverse"
        Me.mnuPulloutReverse.Size = New System.Drawing.Size(214, 22)
        Me.mnuPulloutReverse.Text = "Pullout &Reverse"
        '
        'mnuSpdcPullout
        '
        Me.mnuSpdcPullout.Name = "mnuSpdcPullout"
        Me.mnuSpdcPullout.Size = New System.Drawing.Size(214, 22)
        Me.mnuSpdcPullout.Text = "SPDC Pullout"
        '
        'mnuPdcSpdcPullout
        '
        Me.mnuPdcSpdcPullout.Name = "mnuPdcSpdcPullout"
        Me.mnuPdcSpdcPullout.Size = New System.Drawing.Size(214, 22)
        Me.mnuPdcSpdcPullout.Text = "SPDC Pullout (PDC Table)"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(211, 6)
        '
        'mnupacketlevelpullout
        '
        Me.mnupacketlevelpullout.Name = "mnupacketlevelpullout"
        Me.mnupacketlevelpullout.Size = New System.Drawing.Size(214, 22)
        Me.mnupacketlevelpullout.Text = "P&acket Pullout"
        '
        'mnuPktPulloutNew
        '
        Me.mnuPktPulloutNew.Name = "mnuPktPulloutNew"
        Me.mnuPktPulloutNew.Size = New System.Drawing.Size(214, 22)
        Me.mnuPktPulloutNew.Text = "Packet Pullout (New)"
        '
        'mnuPktBlock
        '
        Me.mnuPktBlock.Name = "mnuPktBlock"
        Me.mnuPktBlock.Size = New System.Drawing.Size(214, 22)
        Me.mnuPktBlock.Text = "Packet Block"
        '
        'ToolStripMenuItem19
        '
        Me.ToolStripMenuItem19.Name = "ToolStripMenuItem19"
        Me.ToolStripMenuItem19.Size = New System.Drawing.Size(211, 6)
        '
        'mnuLooseChqEntry
        '
        Me.mnuLooseChqEntry.Name = "mnuLooseChqEntry"
        Me.mnuLooseChqEntry.Size = New System.Drawing.Size(214, 22)
        Me.mnuLooseChqEntry.Text = "Loose Entry"
        '
        'mnuclosure
        '
        Me.mnuclosure.Name = "mnuclosure"
        Me.mnuclosure.Size = New System.Drawing.Size(214, 22)
        Me.mnuclosure.Text = "&Closure"
        '
        'mnuPresentation
        '
        Me.mnuPresentation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBatchCreation, Me.mnuBatchDataEntry, Me.mnuBatchDespatch, Me.mnuBatchRebatch, Me.mnuBatchPulloutSummary, Me.mnuSingleDataEntrys, Me.mnuManualBatchs, Me.mnuChequeEntrys, Me.mnuRemoveChequeEntry, Me.mnuRebatchProductWise, Me.mnuBatchTally})
        Me.mnuPresentation.Name = "mnuPresentation"
        Me.mnuPresentation.Size = New System.Drawing.Size(231, 22)
        Me.mnuPresentation.Text = "Pr&esentation"
        '
        'mnuBatchCreation
        '
        Me.mnuBatchCreation.Name = "mnuBatchCreation"
        Me.mnuBatchCreation.Size = New System.Drawing.Size(206, 22)
        Me.mnuBatchCreation.Text = "&Batch Creation"
        '
        'mnuBatchDataEntry
        '
        Me.mnuBatchDataEntry.Name = "mnuBatchDataEntry"
        Me.mnuBatchDataEntry.Size = New System.Drawing.Size(206, 22)
        Me.mnuBatchDataEntry.Text = "&Batch Data Entry"
        '
        'mnuBatchDespatch
        '
        Me.mnuBatchDespatch.Name = "mnuBatchDespatch"
        Me.mnuBatchDespatch.Size = New System.Drawing.Size(206, 22)
        Me.mnuBatchDespatch.Text = "Batch &Despatch"
        '
        'mnuBatchRebatch
        '
        Me.mnuBatchRebatch.Name = "mnuBatchRebatch"
        Me.mnuBatchRebatch.Size = New System.Drawing.Size(206, 22)
        Me.mnuBatchRebatch.Text = "&Rebatch"
        '
        'mnuBatchPulloutSummary
        '
        Me.mnuBatchPulloutSummary.Name = "mnuBatchPulloutSummary"
        Me.mnuBatchPulloutSummary.Size = New System.Drawing.Size(206, 22)
        Me.mnuBatchPulloutSummary.Text = "Batch &Pullout Summary"
        '
        'mnuSingleDataEntrys
        '
        Me.mnuSingleDataEntrys.Name = "mnuSingleDataEntrys"
        Me.mnuSingleDataEntrys.Size = New System.Drawing.Size(206, 22)
        Me.mnuSingleDataEntrys.Text = "&Single Data Entry"
        '
        'mnuManualBatchs
        '
        Me.mnuManualBatchs.Name = "mnuManualBatchs"
        Me.mnuManualBatchs.Size = New System.Drawing.Size(206, 22)
        Me.mnuManualBatchs.Text = "&Manual Batch"
        '
        'mnuChequeEntrys
        '
        Me.mnuChequeEntrys.Name = "mnuChequeEntrys"
        Me.mnuChequeEntrys.Size = New System.Drawing.Size(206, 22)
        Me.mnuChequeEntrys.Text = "&Cheque Entry"
        '
        'mnuRemoveChequeEntry
        '
        Me.mnuRemoveChequeEntry.Name = "mnuRemoveChequeEntry"
        Me.mnuRemoveChequeEntry.Size = New System.Drawing.Size(206, 22)
        Me.mnuRemoveChequeEntry.Text = "Remo&ve Cheque Entry"
        '
        'mnuRebatchProductWise
        '
        Me.mnuRebatchProductWise.Name = "mnuRebatchProductWise"
        Me.mnuRebatchProductWise.Size = New System.Drawing.Size(206, 22)
        Me.mnuRebatchProductWise.Text = "Rebatch &Product Wise"
        '
        'mnuBatchTally
        '
        Me.mnuBatchTally.Name = "mnuBatchTally"
        Me.mnuBatchTally.Size = New System.Drawing.Size(206, 22)
        Me.mnuBatchTally.Text = "Batch Tally"
        '
        'mnuremovepreconversion
        '
        Me.mnuremovepreconversion.Name = "mnuremovepreconversion"
        Me.mnuremovepreconversion.Size = New System.Drawing.Size(231, 22)
        Me.mnuremovepreconversion.Text = "Remove Preconversion File"
        '
        'mnupostpostconversiondisc
        '
        Me.mnupostpostconversiondisc.Name = "mnupostpostconversiondisc"
        Me.mnupostpostconversiondisc.Size = New System.Drawing.Size(231, 22)
        Me.mnupostpostconversiondisc.Text = "Post Postconversion Disc"
        '
        'mnupostpreconvdisc
        '
        Me.mnupostpreconvdisc.Name = "mnupostpreconvdisc"
        Me.mnupostpreconvdisc.Size = New System.Drawing.Size(231, 22)
        Me.mnupostpreconvdisc.Text = "Post Preconversion Disc"
        '
        'mnureverseprecondisc
        '
        Me.mnureverseprecondisc.Name = "mnureverseprecondisc"
        Me.mnureverseprecondisc.Size = New System.Drawing.Size(231, 22)
        Me.mnureverseprecondisc.Text = "Reverse Preconversion Disc"
        '
        'mnuPosting
        '
        Me.mnuPosting.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPostFinoneWithPacket, Me.mnuPostWithFinone, Me.mnuremovefinoneunmatched, Me.ToolStripMenuItem15, Me.mnuPostPreConvDump, Me.mnuUndoPreConvDump, Me.ToolStripMenuItem16, Me.mnuPostPostConvDump, Me.mnuUndoPostConvDump})
        Me.mnuPosting.Name = "mnuPosting"
        Me.mnuPosting.Size = New System.Drawing.Size(231, 22)
        Me.mnuPosting.Text = "Posting"
        '
        'mnuPostFinoneWithPacket
        '
        Me.mnuPostFinoneWithPacket.Name = "mnuPostFinoneWithPacket"
        Me.mnuPostFinoneWithPacket.Size = New System.Drawing.Size(229, 22)
        Me.mnuPostFinoneWithPacket.Text = "Finone With Packet"
        '
        'mnuPostWithFinone
        '
        Me.mnuPostWithFinone.Name = "mnuPostWithFinone"
        Me.mnuPostWithFinone.Size = New System.Drawing.Size(229, 22)
        Me.mnuPostWithFinone.Text = "Po&st With Finone"
        '
        'mnuremovefinoneunmatched
        '
        Me.mnuremovefinoneunmatched.Name = "mnuremovefinoneunmatched"
        Me.mnuremovefinoneunmatched.Size = New System.Drawing.Size(229, 22)
        Me.mnuremovefinoneunmatched.Text = "Undo Finone Mapping"
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(226, 6)
        '
        'mnuPostPreConvDump
        '
        Me.mnuPostPreConvDump.Name = "mnuPostPreConvDump"
        Me.mnuPostPreConvDump.Size = New System.Drawing.Size(229, 22)
        Me.mnuPostPreConvDump.Text = "Post Preconversion Dump"
        '
        'mnuUndoPreConvDump
        '
        Me.mnuUndoPreConvDump.Name = "mnuUndoPreConvDump"
        Me.mnuUndoPreConvDump.Size = New System.Drawing.Size(229, 22)
        Me.mnuUndoPreConvDump.Text = "Undo Preconversion Dump"
        '
        'ToolStripMenuItem16
        '
        Me.ToolStripMenuItem16.Name = "ToolStripMenuItem16"
        Me.ToolStripMenuItem16.Size = New System.Drawing.Size(226, 6)
        '
        'mnuPostPostConvDump
        '
        Me.mnuPostPostConvDump.Name = "mnuPostPostConvDump"
        Me.mnuPostPostConvDump.Size = New System.Drawing.Size(229, 22)
        Me.mnuPostPostConvDump.Text = "Post Postconversion Dump"
        '
        'mnuUndoPostConvDump
        '
        Me.mnuUndoPostConvDump.Name = "mnuUndoPostConvDump"
        Me.mnuUndoPostConvDump.Size = New System.Drawing.Size(229, 22)
        Me.mnuUndoPostConvDump.Text = "Undo Postconversion Dump"
        '
        'mnuBounce
        '
        Me.mnuBounce.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnubounceinward, Me.mnuBounceEntry, Me.mnuDelBounceEntry, Me.ToolStripMenuItem13, Me.mnuBouncePulloutEntry})
        Me.mnuBounce.Name = "mnuBounce"
        Me.mnuBounce.Size = New System.Drawing.Size(231, 22)
        Me.mnuBounce.Text = "&Bounce"
        '
        'mnubounceinward
        '
        Me.mnubounceinward.Name = "mnubounceinward"
        Me.mnubounceinward.Size = New System.Drawing.Size(190, 22)
        Me.mnubounceinward.Text = "Bounce &Inward"
        '
        'mnuBounceEntry
        '
        Me.mnuBounceEntry.Name = "mnuBounceEntry"
        Me.mnuBounceEntry.Size = New System.Drawing.Size(190, 22)
        Me.mnuBounceEntry.Text = "Bounce &Entry"
        '
        'mnuDelBounceEntry
        '
        Me.mnuDelBounceEntry.Name = "mnuDelBounceEntry"
        Me.mnuDelBounceEntry.Size = New System.Drawing.Size(190, 22)
        Me.mnuDelBounceEntry.Text = "Bounce Entry Delete"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(187, 6)
        '
        'mnuBouncePulloutEntry
        '
        Me.mnuBouncePulloutEntry.Name = "mnuBouncePulloutEntry"
        Me.mnuBouncePulloutEntry.Size = New System.Drawing.Size(190, 22)
        Me.mnuBouncePulloutEntry.Text = "Bounce &Pullout Entry"
        '
        'mnuPostProductupdate
        '
        Me.mnuPostProductupdate.Name = "mnuPostProductupdate"
        Me.mnuPostProductupdate.Size = New System.Drawing.Size(231, 22)
        Me.mnuPostProductupdate.Text = "Post Product &update"
        '
        'mnuCTSAudit
        '
        Me.mnuCTSAudit.Name = "mnuCTSAudit"
        Me.mnuCTSAudit.Size = New System.Drawing.Size(231, 22)
        Me.mnuCTSAudit.Text = "&CTS Audit"
        '
        'mnuRemoveFinoneData
        '
        Me.mnuRemoveFinoneData.Name = "mnuRemoveFinoneData"
        Me.mnuRemoveFinoneData.Size = New System.Drawing.Size(231, 22)
        Me.mnuRemoveFinoneData.Text = "Remove &Finone Data"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(228, 6)
        '
        'mnuSwap
        '
        Me.mnuSwap.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuImpShortAgmntQry, Me.ToolStripMenuItem12, Me.mnuOldPktEntry, Me.mnuFindSwapOldPkt, Me.mnuOldPktChqTransfer, Me.OldPacketPulloutEntryToolStripMenuItem, Me.mnuOldPktCancelEntry, Me.ToolStripMenuItem10, Me.mnuRevOldPktPullout, Me.mnuRevOldPktChqTran})
        Me.mnuSwap.Name = "mnuSwap"
        Me.mnuSwap.Size = New System.Drawing.Size(231, 22)
        Me.mnuSwap.Text = "Swap"
        '
        'mnuImpShortAgmntQry
        '
        Me.mnuImpShortAgmntQry.Name = "mnuImpShortAgmntQry"
        Me.mnuImpShortAgmntQry.Size = New System.Drawing.Size(286, 22)
        Me.mnuImpShortAgmntQry.Text = "Swap Short Agreement Query"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(283, 6)
        '
        'mnuOldPktEntry
        '
        Me.mnuOldPktEntry.Name = "mnuOldPktEntry"
        Me.mnuOldPktEntry.Size = New System.Drawing.Size(286, 22)
        Me.mnuOldPktEntry.Text = "Old Packet Entry"
        '
        'mnuFindSwapOldPkt
        '
        Me.mnuFindSwapOldPkt.Name = "mnuFindSwapOldPkt"
        Me.mnuFindSwapOldPkt.Size = New System.Drawing.Size(286, 22)
        Me.mnuFindSwapOldPkt.Text = "Find Swap Old Packet"
        '
        'mnuOldPktChqTransfer
        '
        Me.mnuOldPktChqTransfer.Name = "mnuOldPktChqTransfer"
        Me.mnuOldPktChqTransfer.Size = New System.Drawing.Size(286, 22)
        Me.mnuOldPktChqTransfer.Text = "Old Packet Cheques Transfer"
        '
        'OldPacketPulloutEntryToolStripMenuItem
        '
        Me.OldPacketPulloutEntryToolStripMenuItem.Name = "OldPacketPulloutEntryToolStripMenuItem"
        Me.OldPacketPulloutEntryToolStripMenuItem.Size = New System.Drawing.Size(286, 22)
        Me.OldPacketPulloutEntryToolStripMenuItem.Text = "Old Packet Pullout Entry"
        '
        'mnuOldPktCancelEntry
        '
        Me.mnuOldPktCancelEntry.Name = "mnuOldPktCancelEntry"
        Me.mnuOldPktCancelEntry.Size = New System.Drawing.Size(286, 22)
        Me.mnuOldPktCancelEntry.Text = "Old Packet Cancellation Entry"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(283, 6)
        '
        'mnuRevOldPktPullout
        '
        Me.mnuRevOldPktPullout.Name = "mnuRevOldPktPullout"
        Me.mnuRevOldPktPullout.Size = New System.Drawing.Size(286, 22)
        Me.mnuRevOldPktPullout.Text = "Reverse Old Packet Pullout"
        '
        'mnuRevOldPktChqTran
        '
        Me.mnuRevOldPktChqTran.Name = "mnuRevOldPktChqTran"
        Me.mnuRevOldPktChqTran.Size = New System.Drawing.Size(286, 22)
        Me.mnuRevOldPktChqTran.Text = "Reverse Old Packet Cheques Transfer"
        '
        'mnuPacketAudit
        '
        Me.mnuPacketAudit.Name = "mnuPacketAudit"
        Me.mnuPacketAudit.Size = New System.Drawing.Size(231, 22)
        Me.mnuPacketAudit.Text = "Packet Audit"
        '
        'MnuReport
        '
        Me.MnuReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileReport, Me.ToolStripMenuItem1, Me.mnuALMARAReport, Me.CholaMISToolStripMenuItem, Me.mnuBatchReport, Me.mnuFinoneMIS, Me.mnuFinonePreconversionMIS, Me.mnuCycleReport, Me.mnuInwardReport, Me.DespatchReportToolStripMenuItem, Me.mnuInwardMIS, Me.mnuSOSMIS, Me.mnuWarehousingMIS, Me.mnuCTSReport, Me.mnuPreconversionDisc, Me.mnuPostconversionDiscReport, Me.mnuMismatchReport, Me.mnuCTSSummaryReport, Me.mnuForeClosureReport, Me.mnuFinoneReport, Me.ToolStripMenuItem2, Me.mnuPacketReport, Me.mnuMis, Me.ToolStripMenuItem4, Me.mnuBnceRpt, Me.mnuSwapReport, Me.mnuMastRpt, Me.mnuOthersReport, Me.ToolStripMenuItem5, Me.mnuSearchEngine, Me.mnuSearchEngineChq, Me.mnuQryRpt, Me.mnuQryEngine})
        Me.MnuReport.Name = "MnuReport"
        Me.MnuReport.Size = New System.Drawing.Size(58, 20)
        Me.MnuReport.Text = "&Report"
        '
        'mnuFileReport
        '
        Me.mnuFileReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PDCToolStripMenuItem1, Me.SPDCToolStripMenuItem1})
        Me.mnuFileReport.Name = "mnuFileReport"
        Me.mnuFileReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuFileReport.Text = "File &wise Report"
        '
        'PDCToolStripMenuItem1
        '
        Me.PDCToolStripMenuItem1.Name = "PDCToolStripMenuItem1"
        Me.PDCToolStripMenuItem1.Size = New System.Drawing.Size(103, 22)
        Me.PDCToolStripMenuItem1.Text = "&PDC"
        '
        'SPDCToolStripMenuItem1
        '
        Me.SPDCToolStripMenuItem1.Name = "SPDCToolStripMenuItem1"
        Me.SPDCToolStripMenuItem1.Size = New System.Drawing.Size(103, 22)
        Me.SPDCToolStripMenuItem1.Text = "&SPDC"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(229, 22)
        Me.ToolStripMenuItem1.Text = "PDC Entry NotMatching"
        '
        'mnuALMARAReport
        '
        Me.mnuALMARAReport.Name = "mnuALMARAReport"
        Me.mnuALMARAReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuALMARAReport.Text = "&ALMARA Report"
        '
        'CholaMISToolStripMenuItem
        '
        Me.CholaMISToolStripMenuItem.Name = "CholaMISToolStripMenuItem"
        Me.CholaMISToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.CholaMISToolStripMenuItem.Text = "Chola &MIS"
        Me.CholaMISToolStripMenuItem.Visible = False
        '
        'mnuBatchReport
        '
        Me.mnuBatchReport.Name = "mnuBatchReport"
        Me.mnuBatchReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuBatchReport.Text = "&Batch Report"
        '
        'mnuFinoneMIS
        '
        Me.mnuFinoneMIS.Name = "mnuFinoneMIS"
        Me.mnuFinoneMIS.Size = New System.Drawing.Size(229, 22)
        Me.mnuFinoneMIS.Text = "Finone MIS"
        '
        'mnuFinonePreconversionMIS
        '
        Me.mnuFinonePreconversionMIS.Name = "mnuFinonePreconversionMIS"
        Me.mnuFinonePreconversionMIS.Size = New System.Drawing.Size(229, 22)
        Me.mnuFinonePreconversionMIS.Text = "Finone Preconversion MIS"
        '
        'mnuCycleReport
        '
        Me.mnuCycleReport.Name = "mnuCycleReport"
        Me.mnuCycleReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuCycleReport.Text = "&Cycle Report"
        '
        'mnuInwardReport
        '
        Me.mnuInwardReport.Name = "mnuInwardReport"
        Me.mnuInwardReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuInwardReport.Text = "&Inward Report"
        '
        'DespatchReportToolStripMenuItem
        '
        Me.DespatchReportToolStripMenuItem.Name = "DespatchReportToolStripMenuItem"
        Me.DespatchReportToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.DespatchReportToolStripMenuItem.Text = "&Despatch Report"
        '
        'mnuInwardMIS
        '
        Me.mnuInwardMIS.Name = "mnuInwardMIS"
        Me.mnuInwardMIS.Size = New System.Drawing.Size(229, 22)
        Me.mnuInwardMIS.Text = "Inwa&rd MIS"
        '
        'mnuSOSMIS
        '
        Me.mnuSOSMIS.Name = "mnuSOSMIS"
        Me.mnuSOSMIS.Size = New System.Drawing.Size(229, 22)
        Me.mnuSOSMIS.Text = "&SOS MIS"
        '
        'mnuWarehousingMIS
        '
        Me.mnuWarehousingMIS.Name = "mnuWarehousingMIS"
        Me.mnuWarehousingMIS.Size = New System.Drawing.Size(229, 22)
        Me.mnuWarehousingMIS.Text = "&Warehousing MIS"
        '
        'mnuCTSReport
        '
        Me.mnuCTSReport.Name = "mnuCTSReport"
        Me.mnuCTSReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuCTSReport.Text = "CTS &Report"
        '
        'mnuPreconversionDisc
        '
        Me.mnuPreconversionDisc.Name = "mnuPreconversionDisc"
        Me.mnuPreconversionDisc.Size = New System.Drawing.Size(229, 22)
        Me.mnuPreconversionDisc.Text = "Preconversion Disc Report"
        '
        'mnuPostconversionDiscReport
        '
        Me.mnuPostconversionDiscReport.Name = "mnuPostconversionDiscReport"
        Me.mnuPostconversionDiscReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuPostconversionDiscReport.Text = "Postconversion Disc Report"
        '
        'mnuMismatchReport
        '
        Me.mnuMismatchReport.Name = "mnuMismatchReport"
        Me.mnuMismatchReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuMismatchReport.Text = "Mismatch Report"
        '
        'mnuCTSSummaryReport
        '
        Me.mnuCTSSummaryReport.Name = "mnuCTSSummaryReport"
        Me.mnuCTSSummaryReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuCTSSummaryReport.Text = "CTS Summary Report"
        '
        'mnuForeClosureReport
        '
        Me.mnuForeClosureReport.Name = "mnuForeClosureReport"
        Me.mnuForeClosureReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuForeClosureReport.Text = "ForeClosure Report"
        '
        'mnuFinoneReport
        '
        Me.mnuFinoneReport.Name = "mnuFinoneReport"
        Me.mnuFinoneReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuFinoneReport.Text = "Finone Report"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(226, 6)
        '
        'mnuPacketReport
        '
        Me.mnuPacketReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPktRpt, Me.mnuPktContentRpt, Me.ToolStripMenuItem14, Me.mnuPktPulloutRpt, Me.mnuPktBlockRpt})
        Me.mnuPacketReport.Name = "mnuPacketReport"
        Me.mnuPacketReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuPacketReport.Text = "&Packet"
        '
        'mnuPktRpt
        '
        Me.mnuPktRpt.Name = "mnuPktRpt"
        Me.mnuPktRpt.Size = New System.Drawing.Size(161, 22)
        Me.mnuPktRpt.Text = "Packet"
        '
        'mnuPktContentRpt
        '
        Me.mnuPktContentRpt.Name = "mnuPktContentRpt"
        Me.mnuPktContentRpt.Size = New System.Drawing.Size(161, 22)
        Me.mnuPktContentRpt.Text = "Packet Content"
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        Me.ToolStripMenuItem14.Size = New System.Drawing.Size(158, 6)
        '
        'mnuPktPulloutRpt
        '
        Me.mnuPktPulloutRpt.Name = "mnuPktPulloutRpt"
        Me.mnuPktPulloutRpt.Size = New System.Drawing.Size(161, 22)
        Me.mnuPktPulloutRpt.Text = "Packet Pullout"
        '
        'mnuPktBlockRpt
        '
        Me.mnuPktBlockRpt.Name = "mnuPktBlockRpt"
        Me.mnuPktBlockRpt.Size = New System.Drawing.Size(161, 22)
        Me.mnuPktBlockRpt.Text = "Packet Block"
        '
        'mnuMis
        '
        Me.mnuMis.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMisCts, Me.mnuStockMis, Me.mnuStockNewMis, Me.ToolStripMenuItem9, Me.mnuBnceMis, Me.BounceInwardToolStripMenuItem, Me.mnuProductivity, Me.mnuInwardMisRevised})
        Me.mnuMis.Name = "mnuMis"
        Me.mnuMis.Size = New System.Drawing.Size(229, 22)
        Me.mnuMis.Text = "MIS"
        '
        'mnuMisCts
        '
        Me.mnuMisCts.Name = "mnuMisCts"
        Me.mnuMisCts.Size = New System.Drawing.Size(158, 22)
        Me.mnuMisCts.Text = "Packet"
        '
        'mnuStockMis
        '
        Me.mnuStockMis.Name = "mnuStockMis"
        Me.mnuStockMis.Size = New System.Drawing.Size(158, 22)
        Me.mnuStockMis.Text = "Stock"
        '
        'mnuStockNewMis
        '
        Me.mnuStockNewMis.Name = "mnuStockNewMis"
        Me.mnuStockNewMis.Size = New System.Drawing.Size(158, 22)
        Me.mnuStockNewMis.Text = "Stock New"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(155, 6)
        '
        'mnuBnceMis
        '
        Me.mnuBnceMis.Name = "mnuBnceMis"
        Me.mnuBnceMis.Size = New System.Drawing.Size(158, 22)
        Me.mnuBnceMis.Text = "Bounce"
        '
        'BounceInwardToolStripMenuItem
        '
        Me.BounceInwardToolStripMenuItem.Name = "BounceInwardToolStripMenuItem"
        Me.BounceInwardToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.BounceInwardToolStripMenuItem.Text = "Bounce Inward"
        '
        'mnuProductivity
        '
        Me.mnuProductivity.Name = "mnuProductivity"
        Me.mnuProductivity.Size = New System.Drawing.Size(158, 22)
        Me.mnuProductivity.Text = "Productivity"
        '
        'mnuInwardMisRevised
        '
        Me.mnuInwardMisRevised.Name = "mnuInwardMisRevised"
        Me.mnuInwardMisRevised.Size = New System.Drawing.Size(158, 22)
        Me.mnuInwardMisRevised.Text = "Inward"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(226, 6)
        '
        'mnuBnceRpt
        '
        Me.mnuBnceRpt.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BounceEntryToolStripMenuItem, Me.mnuBnceDumpRpt, Me.ToolStripMenuItem8, Me.mnuBnceInwdRpt, Me.mnuBnceOldRpt})
        Me.mnuBnceRpt.Name = "mnuBnceRpt"
        Me.mnuBnceRpt.Size = New System.Drawing.Size(229, 22)
        Me.mnuBnceRpt.Text = "Bounce"
        '
        'BounceEntryToolStripMenuItem
        '
        Me.BounceEntryToolStripMenuItem.Name = "BounceEntryToolStripMenuItem"
        Me.BounceEntryToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.BounceEntryToolStripMenuItem.Text = "Bounce Entry"
        '
        'mnuBnceDumpRpt
        '
        Me.mnuBnceDumpRpt.Name = "mnuBnceDumpRpt"
        Me.mnuBnceDumpRpt.Size = New System.Drawing.Size(158, 22)
        Me.mnuBnceDumpRpt.Text = "Bounce Dump"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(155, 6)
        '
        'mnuBnceInwdRpt
        '
        Me.mnuBnceInwdRpt.Name = "mnuBnceInwdRpt"
        Me.mnuBnceInwdRpt.Size = New System.Drawing.Size(158, 22)
        Me.mnuBnceInwdRpt.Text = "Bounce Inward"
        '
        'mnuBnceOldRpt
        '
        Me.mnuBnceOldRpt.Name = "mnuBnceOldRpt"
        Me.mnuBnceOldRpt.Size = New System.Drawing.Size(158, 22)
        Me.mnuBnceOldRpt.Text = "Bounce Old"
        '
        'mnuSwapReport
        '
        Me.mnuSwapReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSwapListReport, Me.mnuSwapOldPktReport})
        Me.mnuSwapReport.Name = "mnuSwapReport"
        Me.mnuSwapReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuSwapReport.Text = "Swap"
        '
        'mnuSwapListReport
        '
        Me.mnuSwapListReport.Name = "mnuSwapListReport"
        Me.mnuSwapListReport.Size = New System.Drawing.Size(167, 22)
        Me.mnuSwapListReport.Text = "Swap List"
        '
        'mnuSwapOldPktReport
        '
        Me.mnuSwapOldPktReport.Name = "mnuSwapOldPktReport"
        Me.mnuSwapOldPktReport.Size = New System.Drawing.Size(167, 22)
        Me.mnuSwapOldPktReport.Text = "Swap Old Packet"
        '
        'mnuMastRpt
        '
        Me.mnuMastRpt.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAgrmtRpt, Me.ToolStripMenuItem3, Me.mnuBankReport, Me.mnuCityReport})
        Me.mnuMastRpt.Name = "mnuMastRpt"
        Me.mnuMastRpt.Size = New System.Drawing.Size(229, 22)
        Me.mnuMastRpt.Text = "Master"
        '
        'mnuAgrmtRpt
        '
        Me.mnuAgrmtRpt.Name = "mnuAgrmtRpt"
        Me.mnuAgrmtRpt.Size = New System.Drawing.Size(144, 22)
        Me.mnuAgrmtRpt.Text = "Agreement"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(141, 6)
        '
        'mnuBankReport
        '
        Me.mnuBankReport.Name = "mnuBankReport"
        Me.mnuBankReport.Size = New System.Drawing.Size(144, 22)
        Me.mnuBankReport.Text = "Bank Report"
        '
        'mnuCityReport
        '
        Me.mnuCityReport.Name = "mnuCityReport"
        Me.mnuCityReport.Size = New System.Drawing.Size(144, 22)
        Me.mnuCityReport.Text = "City Report"
        '
        'mnuOthersReport
        '
        Me.mnuOthersReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPulloutReport, Me.mnuSpdcPulloutReport, Me.mnuLooseChqReport, Me.mnuImpFileReport, Me.ToolStripMenuItem18, Me.mnuPreconvRpt, Me.mnuPostconvRpt})
        Me.mnuOthersReport.Name = "mnuOthersReport"
        Me.mnuOthersReport.Size = New System.Drawing.Size(229, 22)
        Me.mnuOthersReport.Text = "Others"
        '
        'mnuPulloutReport
        '
        Me.mnuPulloutReport.Name = "mnuPulloutReport"
        Me.mnuPulloutReport.Size = New System.Drawing.Size(197, 22)
        Me.mnuPulloutReport.Text = "Pullout"
        '
        'mnuSpdcPulloutReport
        '
        Me.mnuSpdcPulloutReport.Name = "mnuSpdcPulloutReport"
        Me.mnuSpdcPulloutReport.Size = New System.Drawing.Size(197, 22)
        Me.mnuSpdcPulloutReport.Text = "SPDC Pullout"
        '
        'mnuLooseChqReport
        '
        Me.mnuLooseChqReport.Name = "mnuLooseChqReport"
        Me.mnuLooseChqReport.Size = New System.Drawing.Size(197, 22)
        Me.mnuLooseChqReport.Text = "Loose Cheque"
        '
        'mnuImpFileReport
        '
        Me.mnuImpFileReport.Name = "mnuImpFileReport"
        Me.mnuImpFileReport.Size = New System.Drawing.Size(197, 22)
        Me.mnuImpFileReport.Text = "Imported File"
        '
        'ToolStripMenuItem18
        '
        Me.ToolStripMenuItem18.Name = "ToolStripMenuItem18"
        Me.ToolStripMenuItem18.Size = New System.Drawing.Size(194, 6)
        '
        'mnuPreconvRpt
        '
        Me.mnuPreconvRpt.Name = "mnuPreconvRpt"
        Me.mnuPreconvRpt.Size = New System.Drawing.Size(197, 22)
        Me.mnuPreconvRpt.Text = "Preconversion Dump"
        '
        'mnuPostconvRpt
        '
        Me.mnuPostconvRpt.Name = "mnuPostconvRpt"
        Me.mnuPostconvRpt.Size = New System.Drawing.Size(197, 22)
        Me.mnuPostconvRpt.Text = "Postconversion Dump"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(226, 6)
        '
        'mnuSearchEngine
        '
        Me.mnuSearchEngine.Name = "mnuSearchEngine"
        Me.mnuSearchEngine.Size = New System.Drawing.Size(229, 22)
        Me.mnuSearchEngine.Text = "Search Engine"
        '
        'mnuSearchEngineChq
        '
        Me.mnuSearchEngineChq.Name = "mnuSearchEngineChq"
        Me.mnuSearchEngineChq.Size = New System.Drawing.Size(229, 22)
        Me.mnuSearchEngineChq.Text = "Search Engine (Cheque)"
        '
        'mnuQryRpt
        '
        Me.mnuQryRpt.Name = "mnuQryRpt"
        Me.mnuQryRpt.Size = New System.Drawing.Size(229, 22)
        Me.mnuQryRpt.Text = "Query"
        '
        'mnuQryEngine
        '
        Me.mnuQryEngine.Name = "mnuQryEngine"
        Me.mnuQryEngine.Size = New System.Drawing.Size(229, 22)
        Me.mnuQryEngine.Text = "Query Engine"
        '
        'mnuwindows
        '
        Me.mnuwindows.Name = "mnuwindows"
        Me.mnuwindows.Size = New System.Drawing.Size(69, 20)
        Me.mnuwindows.Text = "&Windows"
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(40, 20)
        Me.mnuExit.Text = "E&xit"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(1028, 720)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "frmMain"
        Me.Text = "Encore Vault Management System"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblstatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MnuReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuimport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnumaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHandsoffType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuALMARAReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PDCToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SPDCToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuadmin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CholaMISToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuClosureDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPacketPullout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFinoneDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFinoneMIS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCycleReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInwardDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInwardReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuprocesss As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPouchInward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuauthentry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPacketEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPullout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuchqpulloutentry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPulloutSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPulloutReverse As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupacketlevelpullout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuclosure As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnualmara As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AuthDumpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupacketreentry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPresentation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchCreation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchDataEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchDespatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchRebatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchPulloutSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSingleDataEntrys As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuManualBatchs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuChequeEntrys As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveChequeEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DespatchReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnualmaraPDC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents frmalmaraSPDC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBounceReason As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBounce As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBounceEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBouncePulloutEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInwardMIS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupouchnotrecvd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupacketupdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupouchrejectreverse As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGetGNSAREF As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuupdaterefno As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProductUpdateDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPostProductupdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPreConverFinoneDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFinonePreconversionMIS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnubounceinward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSOSMIS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWarehousingMIS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuwindows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuupdateagreement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCTSAudit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCTSReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBounceDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRebatchProductWise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPostWithFinone As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnentryreverse As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRejectReason As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveFinoneData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupostpreconvdisc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPreconversionDisc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPostconversionDiscReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPacketReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupostpostconversiondisc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnureverseprecondisc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuremovepreconversion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMismatchReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuremovefinoneunmatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCTSSummaryReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHolidayMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuForeClosureReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFinoneReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuauthdateupdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBankMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CityMasterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBankReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCityReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPacketAudit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMastRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAgrmtRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuQryRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuQryEngine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSearchEngine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMisCts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStockMis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPosting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPostFinoneWithPacket As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblUser As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDelInward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBnceRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBnceDumpRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BounceEntryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImpChqQuery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBnceOldRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBnceInwdRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBnceMis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BounceInwardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSwap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOldPktEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOldPktCancelEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OldPacketPulloutEntryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOldPktChqTransfer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRevOldPktChqTran As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImpShortAgmntQry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSwapReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuRevOldPktPullout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFindSwapOldPkt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuImpSwap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDepSwap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSwapListReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSwapOldPktReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOthersReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPulloutReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSpdcPullout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSpdcPulloutReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSearchEngineChq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLooseChqEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLooseChqReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDelBounceEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPouchInwardAuth As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStockNewMis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPktRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPktContentRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPktPulloutRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPdcSpdcPullout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPktPulloutNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdPktPayMode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImpPktPayMode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuChqNoAmtQry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPostPostConvDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUndoPostConvDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPostPreConvDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUndoPreConvDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDelPreConvDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDelPostConvDump As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuImpFileReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPreconvRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPostconvRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPktBlock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPktBlockRpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBatchTally As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProductivity As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuInwardMisRevised As System.Windows.Forms.ToolStripMenuItem

End Class
