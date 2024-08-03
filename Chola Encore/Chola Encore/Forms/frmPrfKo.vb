Public Class frmPrfKo
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents cboLocation As System.Windows.Forms.ComboBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents cboCorp As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboP2pAccNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpUpldTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpUpldFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtExcpAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSecCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSfNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRefNo As System.Windows.Forms.TextBox
    Friend WithEvents cboAccMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPrfDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRecCount As System.Windows.Forms.TextBox
    Friend WithEvents msfGrid As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents chkKo As System.Windows.Forms.CheckBox
    Friend WithEvents msfKo As AxMSFlexGridLib.AxMSFlexGrid
    Public WithEvents imgChkBox As System.Windows.Forms.ImageList
    Friend WithEvents txtDiffAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtCrAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtDrAmt As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBrkpId As System.Windows.Forms.TextBox
    Friend WithEvents pnlKo As System.Windows.Forms.Panel
    Friend WithEvents btnKo As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnClearSele As System.Windows.Forms.Button
    Friend WithEvents btnAll As System.Windows.Forms.Button
    Friend WithEvents chkBrkpId As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTdsInvNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrfKo))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTdsInvNo = New System.Windows.Forms.TextBox
        Me.chkBrkpId = New System.Windows.Forms.CheckBox
        Me.btnExport = New System.Windows.Forms.Button
        Me.chkKo = New System.Windows.Forms.CheckBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtExcpAmt = New System.Windows.Forms.TextBox
        Me.txtSecCode = New System.Windows.Forms.TextBox
        Me.txtSfNo = New System.Windows.Forms.TextBox
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.txtPrfDesc = New System.Windows.Forms.TextBox
        Me.txtBrkpId = New System.Windows.Forms.TextBox
        Me.txtRefNo = New System.Windows.Forms.TextBox
        Me.txtAmt = New System.Windows.Forms.TextBox
        Me.dtpUpldTo = New System.Windows.Forms.DateTimePicker
        Me.Label11 = New System.Windows.Forms.Label
        Me.dtpUpldFrom = New System.Windows.Forms.DateTimePicker
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboAccMode = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.cboP2pAccNo = New System.Windows.Forms.ComboBox
        Me.cboCorp = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboLocation = New System.Windows.Forms.ComboBox
        Me.lblLocation = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlExport = New System.Windows.Forms.Panel
        Me.pnlKo = New System.Windows.Forms.Panel
        Me.btnKo = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.btnClearSele = New System.Windows.Forms.Button
        Me.btnAll = New System.Windows.Forms.Button
        Me.txtDiffAmt = New System.Windows.Forms.TextBox
        Me.txtCrAmt = New System.Windows.Forms.TextBox
        Me.txtRecCount = New System.Windows.Forms.TextBox
        Me.txtDrAmt = New System.Windows.Forms.TextBox
        Me.msfGrid = New AxMSFlexGridLib.AxMSFlexGrid
        Me.msfKo = New AxMSFlexGridLib.AxMSFlexGrid
        Me.imgChkBox = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlExport.SuspendLayout()
        Me.pnlKo.SuspendLayout()
        CType(Me.msfGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.msfKo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.txtTdsInvNo)
        Me.pnlMain.Controls.Add(Me.chkBrkpId)
        Me.pnlMain.Controls.Add(Me.btnExport)
        Me.pnlMain.Controls.Add(Me.chkKo)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.Label16)
        Me.pnlMain.Controls.Add(Me.Label15)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label14)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Controls.Add(Me.txtExcpAmt)
        Me.pnlMain.Controls.Add(Me.txtSecCode)
        Me.pnlMain.Controls.Add(Me.txtSfNo)
        Me.pnlMain.Controls.Add(Me.txtRemark)
        Me.pnlMain.Controls.Add(Me.txtPrfDesc)
        Me.pnlMain.Controls.Add(Me.txtBrkpId)
        Me.pnlMain.Controls.Add(Me.txtRefNo)
        Me.pnlMain.Controls.Add(Me.txtAmt)
        Me.pnlMain.Controls.Add(Me.dtpUpldTo)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.dtpUpldFrom)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.cboAccMode)
        Me.pnlMain.Controls.Add(Me.Label17)
        Me.pnlMain.Controls.Add(Me.cboP2pAccNo)
        Me.pnlMain.Controls.Add(Me.cboCorp)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.cboLocation)
        Me.pnlMain.Controls.Add(Me.lblLocation)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnClear)
        Me.pnlMain.Controls.Add(Me.btnRefresh)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Location = New System.Drawing.Point(6, 7)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(768, 157)
        Me.pnlMain.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(73, 16)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "Tds Inv No"
        '
        'txtTdsInvNo
        '
        Me.txtTdsInvNo.Location = New System.Drawing.Point(87, 66)
        Me.txtTdsInvNo.MaxLength = 100
        Me.txtTdsInvNo.Name = "txtTdsInvNo"
        Me.txtTdsInvNo.Size = New System.Drawing.Size(105, 21)
        Me.txtTdsInvNo.TabIndex = 118
        '
        'chkBrkpId
        '
        Me.chkBrkpId.AutoSize = True
        Me.chkBrkpId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkBrkpId.Location = New System.Drawing.Point(207, 125)
        Me.chkBrkpId.Name = "chkBrkpId"
        Me.chkBrkpId.Size = New System.Drawing.Size(67, 17)
        Me.chkBrkpId.TabIndex = 15
        Me.chkBrkpId.Text = "Brkp Id"
        Me.chkBrkpId.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(603, 120)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 18
        Me.btnExport.Text = "&Export"
        '
        'chkKo
        '
        Me.chkKo.AutoSize = True
        Me.chkKo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkKo.Location = New System.Drawing.Point(291, 125)
        Me.chkKo.Name = "chkKo"
        Me.chkKo.Size = New System.Drawing.Size(115, 17)
        Me.chkKo.TabIndex = 15
        Me.chkKo.Text = "Enable Knockoff"
        Me.chkKo.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(579, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label13.Size = New System.Drawing.Size(63, 16)
        Me.Label13.TabIndex = 117
        Me.Label13.Text = "Exception"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(376, 68)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label16.Size = New System.Drawing.Size(62, 16)
        Me.Label16.TabIndex = 117
        Me.Label16.Text = "Sec Code"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(192, 68)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label15.Size = New System.Drawing.Size(62, 16)
        Me.Label15.TabIndex = 117
        Me.Label15.Text = "SF No"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(376, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(62, 16)
        Me.Label4.TabIndex = 117
        Me.Label4.Text = "Remark"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(5, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(76, 16)
        Me.Label3.TabIndex = 117
        Me.Label3.Text = "Proof Desc"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(19, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(62, 16)
        Me.Label6.TabIndex = 117
        Me.Label6.Text = "Brkp Id"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(580, 42)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label14.Size = New System.Drawing.Size(62, 16)
        Me.Label14.TabIndex = 117
        Me.Label14.Text = "Ref No"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(376, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label12.Size = New System.Drawing.Size(62, 16)
        Me.Label12.TabIndex = 117
        Me.Label12.Text = "Amount"
        '
        'txtExcpAmt
        '
        Me.txtExcpAmt.Location = New System.Drawing.Point(648, 11)
        Me.txtExcpAmt.MaxLength = 100
        Me.txtExcpAmt.Name = "txtExcpAmt"
        Me.txtExcpAmt.Size = New System.Drawing.Size(105, 21)
        Me.txtExcpAmt.TabIndex = 4
        '
        'txtSecCode
        '
        Me.txtSecCode.Location = New System.Drawing.Point(444, 66)
        Me.txtSecCode.MaxLength = 100
        Me.txtSecCode.Name = "txtSecCode"
        Me.txtSecCode.Size = New System.Drawing.Size(105, 21)
        Me.txtSecCode.TabIndex = 11
        '
        'txtSfNo
        '
        Me.txtSfNo.Location = New System.Drawing.Point(260, 66)
        Me.txtSfNo.MaxLength = 100
        Me.txtSfNo.Name = "txtSfNo"
        Me.txtSfNo.Size = New System.Drawing.Size(105, 21)
        Me.txtSfNo.TabIndex = 10
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(444, 93)
        Me.txtRemark.MaxLength = 1000
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(309, 21)
        Me.txtRemark.TabIndex = 14
        '
        'txtPrfDesc
        '
        Me.txtPrfDesc.Location = New System.Drawing.Point(87, 93)
        Me.txtPrfDesc.MaxLength = 1000
        Me.txtPrfDesc.Name = "txtPrfDesc"
        Me.txtPrfDesc.Size = New System.Drawing.Size(278, 21)
        Me.txtPrfDesc.TabIndex = 13
        '
        'txtBrkpId
        '
        Me.txtBrkpId.Location = New System.Drawing.Point(87, 120)
        Me.txtBrkpId.MaxLength = 100
        Me.txtBrkpId.Name = "txtBrkpId"
        Me.txtBrkpId.Size = New System.Drawing.Size(105, 21)
        Me.txtBrkpId.TabIndex = 15
        '
        'txtRefNo
        '
        Me.txtRefNo.Location = New System.Drawing.Point(648, 39)
        Me.txtRefNo.MaxLength = 100
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.Size = New System.Drawing.Size(105, 21)
        Me.txtRefNo.TabIndex = 9
        '
        'txtAmt
        '
        Me.txtAmt.Location = New System.Drawing.Point(444, 12)
        Me.txtAmt.MaxLength = 100
        Me.txtAmt.Name = "txtAmt"
        Me.txtAmt.Size = New System.Drawing.Size(105, 21)
        Me.txtAmt.TabIndex = 3
        '
        'dtpUpldTo
        '
        Me.dtpUpldTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpUpldTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUpldTo.Location = New System.Drawing.Point(260, 12)
        Me.dtpUpldTo.Name = "dtpUpldTo"
        Me.dtpUpldTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpUpldTo.TabIndex = 2
        Me.dtpUpldTo.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(204, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 17)
        Me.Label11.TabIndex = 115
        Me.Label11.Text = "To"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpUpldFrom
        '
        Me.dtpUpldFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpUpldFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUpldFrom.Location = New System.Drawing.Point(87, 12)
        Me.dtpUpldFrom.Name = "dtpUpldFrom"
        Me.dtpUpldFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpUpldFrom.TabIndex = 1
        Me.dtpUpldFrom.Value = New Date(2008, 1, 12, 0, 0, 0, 0)
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(-4, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 17)
        Me.Label10.TabIndex = 115
        Me.Label10.Text = "Upld From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboAccMode
        '
        Me.cboAccMode.FormattingEnabled = True
        Me.cboAccMode.Location = New System.Drawing.Point(648, 66)
        Me.cboAccMode.Name = "cboAccMode"
        Me.cboAccMode.Size = New System.Drawing.Size(105, 21)
        Me.cboAccMode.TabIndex = 12
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(553, 68)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(89, 13)
        Me.Label17.TabIndex = 113
        Me.Label17.Text = "A/C Mode"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboP2pAccNo
        '
        Me.cboP2pAccNo.FormattingEnabled = True
        Me.cboP2pAccNo.Location = New System.Drawing.Point(444, 39)
        Me.cboP2pAccNo.Name = "cboP2pAccNo"
        Me.cboP2pAccNo.Size = New System.Drawing.Size(105, 21)
        Me.cboP2pAccNo.TabIndex = 8
        '
        'cboCorp
        '
        Me.cboCorp.FormattingEnabled = True
        Me.cboCorp.Location = New System.Drawing.Point(87, 39)
        Me.cboCorp.Name = "cboCorp"
        Me.cboCorp.Size = New System.Drawing.Size(105, 21)
        Me.cboCorp.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(5, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 113
        Me.Label2.Text = "Corp Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboLocation
        '
        Me.cboLocation.FormattingEnabled = True
        Me.cboLocation.Location = New System.Drawing.Point(260, 39)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.Size = New System.Drawing.Size(105, 21)
        Me.cboLocation.TabIndex = 7
        '
        'lblLocation
        '
        Me.lblLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocation.Location = New System.Drawing.Point(198, 42)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(55, 13)
        Me.lblLocation.TabIndex = 113
        Me.lblLocation.Text = "Location"
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(681, 120)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 19
        Me.btnClose.Text = "&Close"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(525, 120)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 17
        Me.btnClear.Text = "C&lear"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(447, 120)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 16
        Me.btnRefresh.Text = "&Refresh"
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(360, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 113
        Me.Label5.Text = "P2P A/C No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlExport
        '
        Me.pnlExport.Controls.Add(Me.pnlKo)
        Me.pnlExport.Controls.Add(Me.txtDiffAmt)
        Me.pnlExport.Controls.Add(Me.txtCrAmt)
        Me.pnlExport.Controls.Add(Me.txtRecCount)
        Me.pnlExport.Controls.Add(Me.txtDrAmt)
        Me.pnlExport.Location = New System.Drawing.Point(6, 316)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(827, 33)
        Me.pnlExport.TabIndex = 22
        '
        'pnlKo
        '
        Me.pnlKo.Controls.Add(Me.btnKo)
        Me.pnlKo.Controls.Add(Me.btnImport)
        Me.pnlKo.Controls.Add(Me.btnClearSele)
        Me.pnlKo.Controls.Add(Me.btnAll)
        Me.pnlKo.Location = New System.Drawing.Point(468, 1)
        Me.pnlKo.Name = "pnlKo"
        Me.pnlKo.Size = New System.Drawing.Size(326, 29)
        Me.pnlKo.TabIndex = 26
        '
        'btnKo
        '
        Me.btnKo.Location = New System.Drawing.Point(3, 2)
        Me.btnKo.Name = "btnKo"
        Me.btnKo.Size = New System.Drawing.Size(72, 24)
        Me.btnKo.TabIndex = 27
        Me.btnKo.Text = "&Knockoff"
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(237, 2)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(72, 23)
        Me.btnImport.TabIndex = 29
        Me.btnImport.Text = "Import"
        '
        'btnClearSele
        '
        Me.btnClearSele.Location = New System.Drawing.Point(159, 2)
        Me.btnClearSele.Name = "btnClearSele"
        Me.btnClearSele.Size = New System.Drawing.Size(72, 23)
        Me.btnClearSele.TabIndex = 28
        Me.btnClearSele.Text = "Clear"
        '
        'btnAll
        '
        Me.btnAll.Location = New System.Drawing.Point(81, 2)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(72, 23)
        Me.btnAll.TabIndex = 26
        Me.btnAll.Text = "All"
        '
        'txtDiffAmt
        '
        Me.txtDiffAmt.BackColor = System.Drawing.SystemColors.Control
        Me.txtDiffAmt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDiffAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDiffAmt.Location = New System.Drawing.Point(319, 0)
        Me.txtDiffAmt.MaxLength = 100
        Me.txtDiffAmt.Name = "txtDiffAmt"
        Me.txtDiffAmt.ReadOnly = True
        Me.txtDiffAmt.Size = New System.Drawing.Size(155, 14)
        Me.txtDiffAmt.TabIndex = 15
        Me.txtDiffAmt.TabStop = False
        Me.txtDiffAmt.Text = "Diff Amt : "
        Me.txtDiffAmt.Visible = False
        '
        'txtCrAmt
        '
        Me.txtCrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.txtCrAmt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCrAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCrAmt.Location = New System.Drawing.Point(158, 0)
        Me.txtCrAmt.MaxLength = 100
        Me.txtCrAmt.Name = "txtCrAmt"
        Me.txtCrAmt.ReadOnly = True
        Me.txtCrAmt.Size = New System.Drawing.Size(155, 14)
        Me.txtCrAmt.TabIndex = 15
        Me.txtCrAmt.TabStop = False
        Me.txtCrAmt.Text = "Cr Amt : "
        Me.txtCrAmt.Visible = False
        '
        'txtRecCount
        '
        Me.txtRecCount.BackColor = System.Drawing.SystemColors.Control
        Me.txtRecCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRecCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRecCount.Location = New System.Drawing.Point(6, 16)
        Me.txtRecCount.MaxLength = 100
        Me.txtRecCount.Name = "txtRecCount"
        Me.txtRecCount.ReadOnly = True
        Me.txtRecCount.Size = New System.Drawing.Size(197, 14)
        Me.txtRecCount.TabIndex = 15
        Me.txtRecCount.TabStop = False
        Me.txtRecCount.Text = "Record Count : "
        '
        'txtDrAmt
        '
        Me.txtDrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.txtDrAmt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDrAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDrAmt.Location = New System.Drawing.Point(6, 0)
        Me.txtDrAmt.MaxLength = 100
        Me.txtDrAmt.Name = "txtDrAmt"
        Me.txtDrAmt.ReadOnly = True
        Me.txtDrAmt.Size = New System.Drawing.Size(146, 14)
        Me.txtDrAmt.TabIndex = 15
        Me.txtDrAmt.TabStop = False
        Me.txtDrAmt.Text = "Dr Amt : "
        Me.txtDrAmt.Visible = False
        '
        'msfGrid
        '
        Me.msfGrid.Location = New System.Drawing.Point(6, 170)
        Me.msfGrid.Name = "msfGrid"
        Me.msfGrid.OcxState = CType(resources.GetObject("msfGrid.OcxState"), System.Windows.Forms.AxHost.State)
        Me.msfGrid.Size = New System.Drawing.Size(419, 114)
        Me.msfGrid.TabIndex = 20
        '
        'msfKo
        '
        Me.msfKo.Location = New System.Drawing.Point(455, 180)
        Me.msfKo.Name = "msfKo"
        Me.msfKo.OcxState = CType(resources.GetObject("msfKo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.msfKo.Size = New System.Drawing.Size(227, 114)
        Me.msfKo.TabIndex = 21
        Me.msfKo.Visible = False
        '
        'imgChkBox
        '
        Me.imgChkBox.ImageStream = CType(resources.GetObject("imgChkBox.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgChkBox.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.imgChkBox.Images.SetKeyName(0, "")
        Me.imgChkBox.Images.SetKeyName(1, "")
        '
        'frmPrfKo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(845, 354)
        Me.Controls.Add(Me.msfKo)
        Me.Controls.Add(Me.msfGrid)
        Me.Controls.Add(Me.pnlExport)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPrfKo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "P2P Knockoff"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlExport.ResumeLayout(False)
        Me.pnlExport.PerformLayout()
        Me.pnlKo.ResumeLayout(False)
        CType(Me.msfGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.msfKo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Local Declaration"
    Const mnKoFlag As Integer = 1
#End Region
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Are you sure to close ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtAmt.Text = ""
        txtExcpAmt.Text = ""

        cboCorp.SelectedIndex = -1
        cboLocation.SelectedIndex = -1
        cboP2pAccNo.SelectedIndex = -1

        cboLocation.Text = ""
        cboCorp.Text = ""
        cboP2pAccNo.Text = ""

        txtRefNo.Text = ""
        txtSfNo.Text = ""
        txtSecCode.Text = ""
        cboAccMode.Text = ""
        txtPrfDesc.Text = ""
        txtRemark.Text = ""

        msfGrid.Rows = msfGrid.FixedRows
        msfKo.Rows = msfKo.FixedRows
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        btnRefresh.Enabled = False
        frmMain.lblStatus.Text = "Loading records ..."

        Call LoadData()

        btnRefresh.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default

        btnRefresh.Focus()
    End Sub

    Private Sub LoadData()
        Dim lsSql As String
        Dim lsCond As String
        Dim ds As New DataSet
        Dim n As Integer, m As Integer
        Dim row As Integer
        Dim lnInterval As Long

        Dim lsCorpCd As String = ""
        Dim lsBrCd As String = ""
        Dim lsP2pAccNo As String = ""
        Dim lnDrAmt As Double = 0
        Dim lnCrAmt As Double = 0
        Dim lnDiffAmt As Double = 0
        Dim lnBalAmt As Double = 0
        Dim lnSysBalAmt As Double = 0

        gpAutoFindCombo(cboLocation)
        gpAutoFindCombo(cboP2pAccNo)

        Try
            Select Case ""
                Case cboCorp.Text
                    MsgBox("Please select the corp !", MsgBoxStyle.Information, gProjectName)
                    cboCorp.Focus()
                    Exit Sub
                Case cboLocation.Text
                    MsgBox("Please select the location !", MsgBoxStyle.Information, gProjectName)
                    cboLocation.Focus()
                    Exit Sub
                Case cboP2pAccNo.Text
                    MsgBox("Please select the a/c no !", MsgBoxStyle.Information, gProjectName)
                    cboP2pAccNo.Focus()
                    Exit Sub
            End Select

            Select Case -1
                Case cboCorp.SelectedIndex
                    MsgBox("Please select the corp !", MsgBoxStyle.Information, gProjectName)
                    cboCorp.Focus()
                    Exit Sub
                Case cboLocation.SelectedIndex
                    MsgBox("Please select the location !", MsgBoxStyle.Information, gProjectName)
                    cboLocation.Focus()
                    Exit Sub
                Case cboP2pAccNo.SelectedIndex
                    MsgBox("Please select the a/c no !", MsgBoxStyle.Information, gProjectName)
                    cboP2pAccNo.Focus()
                    Exit Sub
            End Select

            lsCorpCd = cboCorp.SelectedValue.ToString

            lsCond = ""

            If txtBrkpId.Text <> "" Then lsCond &= " and a.proof_gid = '" & Val(txtBrkpId.Text) & "' "
            If txtExcpAmt.Text <> "" Then lsCond &= " and a.excp_amount = '" & Val(txtExcpAmt.Text) & "' "
            If txtAmt.Text <> "" Then lsCond &= " and a.amount = '" & Val(txtAmt.Text) & "' "

            If dtpUpldFrom.Checked = True Then lsCond &= " and a.upload_date >= '" & Format(CDate(dtpUpldFrom.Value), "yyyy-MM-dd") & "' "
            If dtpUpldTo.Checked = True Then lsCond &= " and a.upload_date <= '" & Format(CDate(dtpUpldTo.Value), "yyyy-MM-dd") & "' "

            If cboCorp.SelectedIndex <> -1 And cboCorp.Text <> "" Then lsCond &= " and a.corp_code = '" & cboCorp.SelectedValue.ToString & "' "

            If txtRefNo.Text <> "" Then lsCond &= " and a.ref_no like '" & txtRefNo.Text & "%' "
            If txtTdsInvNo.Text <> "" Then lsCond &= " and a.tdsinv_no like '" & txtTdsInvNo.Text & "%' "
            If txtSfNo.Text <> "" Then lsCond &= " and a.sf_no like '" & txtSfNo.Text & "%' "
            If txtSecCode.Text <> "" Then lsCond &= " and a.section_code like '" & txtSecCode.Text & "%' "
            If txtPrfDesc.Text <> "" Then lsCond &= " and a.proof_desc like '" & txtPrfDesc.Text & "%' "
            If txtRemark.Text <> "" Then lsCond &= " and a.remark like '" & txtRemark.Text & "%' "

            If cboP2pAccNo.Text <> "" Then
                lsSql = ""
                lsSql &= " select * from tds_mst_tp2pacc "
                lsSql &= " where p2pacc_no = '" & cboP2pAccNo.Text & "' "

                If cboLocation.SelectedIndex <> -1 And cboLocation.Text <> "" Then
                    lsSql &= " and branch_code = '" & cboLocation.SelectedValue.ToString & "' "
                Else
                    lsSql &= " and 1 = 2 "
                End If

                lsSql &= " and corp_code = '" & cboCorp.SelectedValue.ToString & "' "
                lsSql &= " and delete_flag = 'N' "

                gpDataSet(lsSql, "p2pacc", gOdbcConn, ds)

                If ds.Tables("p2pacc").Rows.Count > 0 Then
                    lsP2pAccNo = ds.Tables("p2pacc").Rows(0).Item("p2pacc_no").ToString
                    lsBrCd = ds.Tables("p2pacc").Rows(0).Item("branch_code").ToString

                    lsCond &= " and a.p2pacc_no = '" & lsP2pAccNo & "' "
                    lsCond &= " and a.branch_code = '" & lsBrCd & "' "
                Else
                    lsCond &= " and 1 = 2 "
                End If

                ds.Tables("p2pacc").Rows.Clear()
            Else
                lsCond &= " and 1 = 2 "
            End If

            Select Case cboAccMode.Text
                Case "DEBIT"
                    lsCond &= " and a.acc_mode = 'D' "
                Case "CREDIT"
                    lsCond &= " and a.acc_mode = 'C' "
            End Select

            lnBalAmt = P2pExcpBal(lsCorpCd, lsBrCd, lsP2pAccNo, CDate(dtpUpldFrom.Value), CDate(dtpUpldTo.Text))
            'lnSysBalAmt = PrfAcBal(lsBrCd, lsP2pAccNo, CDate(dtpUpldTo.Value))

            'lnBalAmt = Math.Round(lnBalAmt, 2)
            'lnSysBalAmt = Math.Round(lnSysBalAmt, 2)

            'If lnBalAmt <> lnSysBalAmt Then
            '    MsgBox("Proof A/C balance mismatched !", MsgBoxStyle.Critical, gProjectName)
            '    frmMain.lblStatus.Text = "System Balance : " & Format(lnSysBalAmt, "0.00") _
            '        & " (Difference : " & Format(lnDiffAmt, "0.00") & ")"
            'End If

            If chkBrkpId.Checked = True Then
                lsSql = ""
                lsSql &= " select a.* from tds_rpt_tbrkpqry as q "
                lsSql &= " inner join tds_trn_tproof as a on a.proof_gid = q.brkp_id "
                lsSql &= " and a.excp_amount <> 0 "
                lsSql &= " and a.delete_flag = 'N' "
                lsSql &= " left join tds_trn_tprooffile as b on b.prooffile_gid = a.prooffile_gid and b.delete_flag = 'N' "
                lsSql &= " left join tds_mst_tp2pacc as c on c.p2pacc_no = a.p2pacc_no "
                lsSql &= " and c.branch_code = a.branch_code and c.corp_code = a.corp_code and c.delete_flag = 'N' "
                lsSql &= " where q.import_by = '" & gUserName & "' "
                lsSql &= lsCond
                lsSql &= " and q.delete_flag = 'N' order by a.upload_date,a.mult,a.amount"
            Else
                lsSql = ""
                lsSql &= " select a.* from tds_trn_tproof as a "
                lsSql &= " left join tds_trn_tprooffile as b on b.prooffile_gid = a.prooffile_gid and b.delete_flag = 'N' "
                lsSql &= " left join tds_mst_tp2pacc as c on c.p2pacc_no = a.p2pacc_no "
                lsSql &= " and c.branch_code = a.branch_code and c.corp_code = a.corp_code and c.delete_flag = 'N' "
                lsSql &= " where a.excp_amount <> 0 "
                lsSql &= lsCond
                lsSql &= " and a.delete_flag = 'N' order by a.upload_date,a.mult,a.amount"
            End If

            ds = gfDataSet(lsSql, "prf", gOdbcConn)

            With ds.Tables("prf")
                n = .Rows.Count
                msfGrid.Rows = 1
                msfKo.Rows = msfKo.FixedRows

                If .Rows.Count > 0 Then
                    lsBrCd = .Rows(0).Item("branch_code").ToString
                    lsP2pAccNo = .Rows(0).Item("p2pacc_no").ToString

                    txtRecCount.Text = "Total Records : " & .Rows.Count

                    For row = 1 To n
                        m = row - 1

                        msfGrid.Rows = msfGrid.Rows + 1
                        msfGrid.set_TextMatrix(row, 0, row.ToString)
                        msfGrid.set_TextMatrix(row, 1, Format(CDate(.Rows(m).Item("upload_date").ToString), "dd-MM-yyyy"))
                        msfGrid.set_TextMatrix(row, 2, .Rows(m).Item("proof_desc").ToString)

                        If .Rows(m).Item("acc_mode").ToString = "D" Then
                            msfGrid.set_TextMatrix(row, 3, Format(.Rows(m).Item("amount"), "0.00"))
                            msfGrid.set_TextMatrix(row, 6, Format(.Rows(m).Item("excp_amount"), "0.00"))
                        Else
                            msfGrid.set_TextMatrix(row, 4, Format(.Rows(m).Item("amount"), "0.00"))
                            msfGrid.set_TextMatrix(row, 7, Format(.Rows(m).Item("excp_amount"), "0.00"))
                        End If

                        'lnInterval = Ageing(CDate(.Rows(m).Item("upload_date").ToString), gdUpldDate)
                        lnInterval = DateDiff(DateInterval.Day, CDate(.Rows(m).Item("upload_date").ToString), Now)

                        msfGrid.set_TextMatrix(row, 5, Format(.Rows(m).Item("amount") - .Rows(m).Item("excp_amount"), "0.00"))
                        msfGrid.set_TextMatrix(row, 8, .Rows(m).Item("remark").ToString)
                        msfGrid.set_TextMatrix(row, 9, lnInterval)

                        Select Case lnInterval
                            Case 0 To 4
                                msfGrid.set_TextMatrix(row, 10, "< 5 days")
                            Case 5 To 7
                                msfGrid.set_TextMatrix(row, 10, "5-7 days")
                            Case 8 To 10
                                msfGrid.set_TextMatrix(row, 10, "8-10 days")
                            Case 11 To 15
                                msfGrid.set_TextMatrix(row, 10, "11-15 days")
                            Case 16 To 20
                                msfGrid.set_TextMatrix(row, 10, "16-20 days")
                            Case 21 To 30
                                msfGrid.set_TextMatrix(row, 10, "21-30 days")
                            Case 31 To 60
                                msfGrid.set_TextMatrix(row, 10, "31-60 days")
                            Case 61 To 90
                                msfGrid.set_TextMatrix(row, 10, "61-90 days")
                            Case Else
                                msfGrid.set_TextMatrix(row, 10, "> 90 days")
                        End Select

                        msfGrid.set_TextMatrix(row, 11, .Rows(m).Item("proof_gid").ToString)
                        msfGrid.set_TextMatrix(row, 12, .Rows(m).Item("tdsinv_no").ToString)
                        msfGrid.set_TextMatrix(row, 13, .Rows(m).Item("sf_no").ToString)
                        msfGrid.set_TextMatrix(row, 14, .Rows(m).Item("vendor_code").ToString)
                        msfGrid.set_TextMatrix(row, 15, .Rows(m).Item("tdstax_id").ToString)
                        msfGrid.set_TextMatrix(row, 16, .Rows(m).Item("section_code").ToString)

                        frmMain.lblStatus.Text = "Loading " & row & " record ..."
                    Next row
                End If
            End With

            With msfGrid
                .Rows = .Rows + 2

                For row = .FixedRows To .Rows - 3
                    lnDrAmt += Val(.get_TextMatrix(row, 3))
                    lnCrAmt += Val(.get_TextMatrix(row, 4))
                Next row

                row = .Rows - 2
                .set_TextMatrix(row, 3, Format(lnDrAmt, "0.00"))
                .set_TextMatrix(row, 4, Format(lnCrAmt, "0.00"))

                lnDrAmt = 0
                lnCrAmt = 0

                For row = .FixedRows To .Rows - 3
                    lnDrAmt += Val(.get_TextMatrix(row, 6))
                    lnCrAmt += Val(.get_TextMatrix(row, 7))
                Next row

                row = .Rows - 2
                .set_TextMatrix(row, 6, Format(lnDrAmt, "0.00"))
                .set_TextMatrix(row, 7, Format(lnCrAmt, "0.00"))

                lnDiffAmt = lnDrAmt - lnCrAmt

                If lnDiffAmt > 0 Then
                    .set_TextMatrix(.Rows - 1, 7, Format(lnDiffAmt, "0.00"))
                Else
                    .set_TextMatrix(.Rows - 1, 6, Format(Math.Abs(lnDiffAmt), "0.00"))
                End If

                If lnSysBalAmt <> lnBalAmt Then
                    frmMain.lblStatus.Text = "System Balance : " & Format(lnSysBalAmt, "0.00") _
                        & " (Difference : " & Format(lnSysBalAmt - lnBalAmt, "0.00") & ")"
                End If

                'lnBalAmt = Math.Round(-lnDrAmt + lnCrAmt, 2)
                'lnSysBalAmt = PrfAcBal(lsBrCd, lsP2pAccNo, CDate(dtpUpldTo.Value))

                'If lsMirrorAccNo <> "" Then
                '    lnSysBalAmt += PrfAcBal(lsBrCd, lsMirrorAccNo, CDate(dtpUpldTo.Value))
                'End If

                'lnBalAmt = Math.Round(lnBalAmt, 2)
                'lnSysBalAmt = Math.Round(lnSysBalAmt, 2)

                'If lnBalAmt <> lnSysBalAmt Then
                '    MsgBox("Proof A/C balance mismatched !", MsgBoxStyle.Critical, gProjectName)
                '    frmMain.lblStatus.Text = "System Balance : " & Format(lnSysBalAmt, "0.00") _
                '        & " (Difference : " & Format(lnDiffAmt, "0.00") & ")"
                'End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadData(ByVal BrkpId As Long, ByVal Row As Long)
        Dim lsSql As String
        Dim ds As DataSet
        Dim m As Integer

        Try
            lsSql = ""
            lsSql &= " select a.*,b.file_type from tds_trn_tproof as a "
            lsSql &= " left join tds_trn_tprooffile as b on b.prooffile_gid = a.prooffile_gid and b.delete_flag = 'N' "
            lsSql &= " where 1 = 1 "
            lsSql &= " and a.proof_gid = '" & BrkpId & "' "
            lsSql &= " and a.delete_flag = 'N'"

            ds = gfDataSet(lsSql, "prf", gOdbcConn)

            With ds.Tables("prf")
                If .Rows.Count > 0 Then
                    m = 0
                    msfGrid.set_TextMatrix(Row, 8, .Rows(m).Item("remark").ToString)
                    frmMain.lblStatus.Text = "Loading " & Row & " record ..."
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call gpBindCombo("select * from tds_mst_tcorp where delete_flag = 'N' order by corp_name", _
                             "corp_name", "corp_code", cboCorp, gOdbcConn)

            dtpUpldFrom.Value = gdMinUpldDate
            dtpUpldTo.Value = gdUpldDate

            cboAccMode.Items.Add("DEBIT")
            cboAccMode.Items.Add("CREDIT")

            Call GridProperty()

            msfKo.Visible = False
            btnClear.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmPrfReport_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        With msfGrid
            .Left = 8
            .Top = 170
            .Width = Me.Width - 24
            .Height = Math.Abs(Me.Height - (pnlMain.Top + pnlMain.Height) - pnlExport.Height - 42)
            .Tag = .Height

            pnlExport.Left = .Left
            pnlExport.Width = .Width

            pnlKo.Left = Math.Abs(pnlExport.Left + pnlExport.Width - pnlKo.Width)
        End With

        pnlExport.Top = Me.Height - (pnlExport.Height * 2)

        Call chkKo_CheckedChanged(sender, e)
    End Sub

    Private Sub cboLocation_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.GotFocus
        cboLocation.Tag = cboLocation.Text

        Call gpBindCombo("select branch_code,loc_name from tds_mst_tloc " & _
                             "where delete_flag = 'N' order by loc_name", _
                             "loc_name", "branch_code", cboLocation, gOdbcConn)

        cboLocation.Text = cboLocation.Tag
        gpAutoFindCombo(cboLocation)

        If cboLocation.SelectedIndex = -1 Then
            cboLocation.Text = ""
        Else
            cboLocation.SelectionStart = 0
            cboLocation.SelectionLength = cboLocation.Text.Length
        End If

        cboLocation.Tag = ""
    End Sub

    Private Sub cboLocation_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLocation.SelectedIndexChanged

    End Sub

    Private Sub cboLocation_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLocation.TextChanged
        If cboLocation.SelectedIndex = -1 And cboLocation.Text <> "" Then Call gpAutoFillCombo(cboLocation)
    End Sub

    Private Sub cboAccFlag_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCorp.GotFocus
    End Sub

    Private Sub cboAccFlag_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCorp.TextChanged
        If cboCorp.Text <> "" And cboCorp.SelectedIndex = -1 Then Call gpAutoFillCombo(cboCorp)
    End Sub

    Private Sub cboAccNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboP2pAccNo.GotFocus
        Dim lsCond As String

        cboP2pAccNo.Tag = cboP2pAccNo.Text
        If cboCorp.Text <> "" Then gpAutoFindCombo(cboCorp)
        gpAutoFindCombo(cboLocation)

        If cboLocation.SelectedIndex <> -1 And cboLocation.Text <> "" Then
            lsCond = "and corp_code = '" & cboCorp.SelectedValue.ToString & "' "

            Call gpBindCombo("select distinct p2pacc_no from tds_mst_tp2pacc " & _
                             "where branch_code = '" & cboLocation.SelectedValue.ToString & "' " & _
                             lsCond & _
                             "and delete_flag = 'N' " & _
                             "order by p2pacc_no", _
                             "p2pacc_no", "p2pacc_no", cboP2pAccNo, gOdbcConn)
        Else
            Call gpBindCombo("select distinct p2pacc_no from tds_mst_tp2pacc " & _
                             "where 1 = 2 ", _
                             "p2pacc_no", "p2pacc_no", cboP2pAccNo, gOdbcConn)
        End If

        cboP2pAccNo.Text = cboP2pAccNo.Tag
        gpAutoFindCombo(cboP2pAccNo)

        If cboP2pAccNo.SelectedIndex = -1 Then
            cboP2pAccNo.Text = ""
        Else
            cboP2pAccNo.SelectionStart = 0
            cboP2pAccNo.SelectionLength = cboP2pAccNo.Text.Length
        End If

        cboP2pAccNo.Tag = ""
    End Sub

    Private Sub cboAccNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboP2pAccNo.TextChanged
        Dim lsSql As String

        If cboP2pAccNo.SelectedIndex = -1 And cboP2pAccNo.Text <> "" Then Call gpAutoFillCombo(cboP2pAccNo)

        If cboP2pAccNo.Text <> "" And cboLocation.Text <> "" Then
            lsSql = ""
            lsSql &= " select p2pacc_name from tds_mst_tp2pacc "
            lsSql &= " where p2pacc_no = '" & cboP2pAccNo.Text & "' "
            lsSql &= " and branch_code = '" & cboLocation.SelectedValue.ToString & "' "
            lsSql &= " and corp_code = '" & cboCorp.SelectedValue.ToString & "' "
            lsSql &= " and delete_flag = 'N' "

            frmMain.lblStatus.Text = "A/C Name : " & gfExecuteScalar(lsSql, gOdbcConn)
        End If
    End Sub

    Private Sub GridProperty()
        Dim row As Integer, col As Integer
        Dim i As Integer

        ' Set property for show grid
        With msfGrid
            .Rows = 2
            .Cols = 17

            row = 0
            col = 0

            .set_TextMatrix(row, col, "SNo")
            .set_ColWidth(col, 1440 * 0.5)
            .set_ColAlignment(col, 4)

            col += 1
            .set_TextMatrix(row, col, "Date")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 4)

            col += 1
            .set_TextMatrix(row, col, "Text")
            .set_ColWidth(col, 1440 * 2)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Dr Amount")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Cr Amount")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Ko Amount")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Dr Exception")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Cr Exception")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Remark")
            .set_ColWidth(col, 1440 * 2)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Ageing Days")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Ageing")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Brkp Id")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Tds Inv No")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "SF No")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Vendor No")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Tds Tax Id")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Sec Code")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 1)

            ' Set column header alignment
            .Row = 0

            For i = 0 To .Cols - 1
                .Col = i
                .CellAlignment = 4
            Next i

            .RowHeightMin = 315
        End With

        ' Set property for knockoff grid
        With msfKo
            .Rows = 1
            .Cols = 10

            row = 0
            col = 0
            .set_TextMatrix(row, col, "SNo")
            .set_ColWidth(col, 1440 * 0.5)
            .set_ColAlignment(col, 4)

            col += 1
            .set_TextMatrix(row, col, "Date")
            .set_ColWidth(col, 1440 * 0.75)
            .set_ColAlignment(col, 4)

            col += 1
            .set_TextMatrix(row, col, "A/C No")
            .set_ColWidth(col, 1440 * 0.75)
            .set_ColAlignment(col, 4)

            col += 1
            .set_TextMatrix(row, col, "Particulars")
            .set_ColWidth(col, 1440 * 1.5)
            .set_ColAlignment(col, 1)

            col += 1
            .set_TextMatrix(row, col, "Dr Amt")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Cr Amt")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Match")
            .set_ColWidth(col, 1440 * 1)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Excp Amt")
            .set_ColWidth(col, 1440 * 0)
            .set_ColAlignment(col, 7)

            col += 1
            .set_TextMatrix(row, col, "Brkp Id")
            .set_ColWidth(col, 1440 * 0)
            .set_ColAlignment(col, 1)


            col += 1
            .set_TextMatrix(row, col, "A/C Mode")
            .set_ColWidth(col, 1440 * 0)
            .set_ColAlignment(col, 1)

            ' Set column header alignment
            .Row = 0

            For i = 0 To .Cols - 1
                .Col = i
                .CellAlignment = 4
            Next i

            .RowHeightMin = 315
        End With
    End Sub

    Private Sub chkKo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkKo.CheckedChanged
        msfKo.Visible = chkKo.Checked
        txtDrAmt.Visible = msfKo.Visible
        txtCrAmt.Visible = msfKo.Visible
        txtDiffAmt.Visible = msfKo.Visible
        pnlKo.Visible = msfKo.Visible

        If chkKo.Checked = True Then
            msfGrid.Height = Val(msfGrid.Tag) / 2 - 12

            msfKo.Rows = msfKo.FixedRows
            msfKo.Left = msfGrid.Left
            msfKo.Top = msfGrid.Top + msfGrid.Height + 6
            msfKo.Height = msfGrid.Height
            msfKo.Width = msfGrid.Width
        Else
            msfGrid.Height = Val(msfGrid.Tag)
        End If
    End Sub

    Private Sub AddRow(ByVal Row As Integer, ByVal BrkpId As Integer, ByVal DrFlag As Boolean)
        Dim i As Integer
        Dim ds As DataSet

        With msfKo
            If .Rows > 1 Then
                If DrFlag = True Then
                    If Val(.get_TextMatrix(1, 4)) <> 0 Then
                        MsgBox("You are not allowed to select more than one debit ...", MsgBoxStyle.Information, gProjectName)
                        Exit Sub
                    End If
                Else
                    If Val(.get_TextMatrix(1, 5)) <> 0 Then
                        MsgBox("You are not allowed to select more than one credit ...", MsgBoxStyle.Information, gProjectName)
                        Exit Sub
                    End If
                End If
            End If

            ' Find Duplicates
            For i = 1 To .Rows - 1
                If .get_TextMatrix(i, 8).ToString = BrkpId.ToString Then
                    MsgBox("Duplicate entry !", MsgBoxStyle.Critical, gProjectName)
                    Exit Sub
                End If
            Next i

            ds = gfDataSet("select * from tds_trn_tproof " & _
                           "where proof_gid = '" & BrkpId & "' and excp_amount <> 0 and delete_flag = 'N'", _
                           "addrow", gOdbcConn)

            With ds.Tables("addrow")
                If .Rows.Count > 0 Then
                    msfKo.Rows = msfKo.Rows + 1

                    msfKo.set_TextMatrix(Row, 0, msfKo.Rows - 1)
                    msfKo.set_TextMatrix(Row, 1, Format(CDate(.Rows(0).Item("upload_date")), "dd-MM-yyyy"))
                    msfKo.set_TextMatrix(Row, 2, .Rows(0).Item("p2pacc_no").ToString)
                    msfKo.set_TextMatrix(Row, 3, .Rows(0).Item("proof_desc").ToString)

                    If .Rows(0).Item("acc_mode").ToString = "D" Then
                        msfKo.set_TextMatrix(Row, 4, Format(Val(.Rows(0).Item("excp_amount")), "0.00"))
                    Else
                        msfKo.set_TextMatrix(Row, 5, Format(Val(.Rows(0).Item("excp_amount")), "0.00"))
                    End If

                    msfKo.Row = Row
                    msfKo.Col = 6
                    msfKo.CellPicture = imgChkBox.Images(0)
                    msfKo.CellPictureAlignment = 4

                    msfKo.set_TextMatrix(Row, 7, Format(Val(.Rows(0).Item("excp_amount")), "0.00"))
                    msfKo.set_TextMatrix(Row, 8, .Rows(0).Item("proof_gid").ToString)
                    msfKo.set_TextMatrix(Row, 9, .Rows(0).Item("acc_mode").ToString)
                End If
            End With
        End With
    End Sub

    Private Sub msfGrid_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles msfGrid.DblClick
        Dim Row As Integer, BrkpId As Integer

        If msfGrid.Row >= msfGrid.FixedRows And chkKo.Checked = True Then
            Row = Integer.Parse(msfKo.Rows)
            BrkpId = Integer.Parse(msfGrid.get_TextMatrix(msfGrid.Row, 11))

            If Val(msfGrid.get_TextMatrix(msfGrid.Row, 3)) > 0 Then
                Call AddRow(Row, BrkpId, True)
            Else
                Call AddRow(Row, BrkpId, False)
            End If
        End If
    End Sub

    Private Sub msfGrid_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msfGrid.Enter

    End Sub

    Private Sub msfKo_ClickEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles msfKo.ClickEvent
        RowSelect()
    End Sub

    Private Sub msfKo_KeyDownEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyDownEvent) Handles msfKo.KeyDownEvent
        If e.keyCode = 32 Then RowSelect()
    End Sub

    Private Sub RowSelect()
        Dim lsCellTxt As String = ""
        Dim lbResult As Boolean

        With msfKo
            If .Row >= .FixedRows Then
                If .Col = 6 Then
                    lsCellTxt = .get_TextMatrix(.Row, .Col)

                    .set_TextMatrix(.Row, .Col, Space(Math.Abs(.get_TextMatrix(.Row, .Col).Length - 1)))
                    .CellPicture = imgChkBox.Images(.get_TextMatrix(.Row, .Col).Length)

                    lbResult = RefreshTotal()

                    If lbResult = False Then
                        MsgBox("Mapped amount exceeds !", MsgBoxStyle.Critical, gProjectName)

                        .set_TextMatrix(.Row, .Col, Space(lsCellTxt.Length))
                        .CellPicture = imgChkBox.Images(lsCellTxt.Length)
                    End If
                End If
            End If
        End With
    End Sub

    Private Function RefreshTotal() As Boolean
        Dim lnDrAmt As Double = 0
        Dim lnCrAmt As Double = 0
        Dim lnDiffAmt As Double = 0
        Dim i As Integer

        With msfKo
            If .Rows > 1 Then
                For i = 1 To .Rows - 1
                    If .get_TextMatrix(i, 6) <> "" Then
                        Select Case .get_TextMatrix(i, 9)
                            Case "D"
                                lnDrAmt += Val(.get_TextMatrix(i, 4))
                            Case "C"
                                lnCrAmt += Val(.get_TextMatrix(i, 5))
                        End Select
                    End If
                Next i

                ' Find Difference Amount
                Select Case .get_TextMatrix(1, 9)
                    Case "D"
                        lnDiffAmt = lnDrAmt - lnCrAmt
                    Case "C"
                        lnDiffAmt = lnCrAmt - lnDrAmt
                End Select
            End If

            lnDiffAmt = Math.Round(lnDiffAmt, 2)

            If lnDiffAmt >= 0 Then
                txtDrAmt.Text = "Dr Amt : " & Format(lnDrAmt, "0.00")
                txtCrAmt.Text = "Cr Amt : " & Format(lnCrAmt, "0.00")
                txtDiffAmt.Text = "Diff Amt : " & Format(lnDiffAmt, "0.00")

                Return True
            Else
                Return False
            End If
        End With
    End Function

    'Validating for Integer only
    Public Function EntryText(ByVal CellTxt As String, ByVal KeyAscii As Integer) As String
        Dim lsTxt As String = ""
        Dim n As Integer
        Dim lsDecimal As String

        Select Case KeyAscii
            Case 48 To 57, 8, 46
                If KeyAscii = 8 Then            ' Backspace
                    If CellTxt.Length > 0 Then
                        Return CellTxt.Substring(0, CellTxt.Length - 1)
                    Else
                        Return CellTxt
                    End If
                ElseIf KeyAscii = 46 Then       ' Dot operator
                    If CellTxt.IndexOf(".") = -1 Then
                        Return CellTxt & Chr(KeyAscii)
                    Else
                        Return CellTxt & Chr(KeyAscii)
                    End If
                ElseIf CellTxt.IndexOf(".") <> -1 Then  ' Decimal part checking
                    n = CellTxt.IndexOf(".")

                    If n <> CellTxt.Length - 1 Then
                        lsDecimal = CellTxt.Substring(n + 1)
                        If lsDecimal.Length <= 1 Then
                            Return CellTxt & Chr(KeyAscii)
                        Else
                            Return CellTxt
                        End If
                    Else
                        Return CellTxt & Chr(KeyAscii)
                    End If
                Else
                    Return CellTxt & Chr(KeyAscii)
                End If
            Case Else
                Return CellTxt
        End Select
    End Function

    Private Sub msfKo_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles msfKo.KeyPressEvent
        Dim lsAmt As String
        Dim lsExcpAmt As String
        Dim lsCellTxt As String
        Dim lbResult As Boolean

        With msfKo
            If .Visible = True Then
                If .Row >= .FixedRows Then
                    If (.get_TextMatrix(.Row, 9) = "D" And .Col = 4) Or (.get_TextMatrix(.Row, 9) = "C" And .Col = 5) Then

                        lsExcpAmt = .get_TextMatrix(.Row, 7)        ' Exception amount
                        lsCellTxt = .get_TextMatrix(.Row, .Col)
                        lsAmt = EntryText(.get_TextMatrix(.Row, .Col), e.keyAscii)

                        If Val(lsAmt) <= Val(lsExcpAmt) Then
                            .set_TextMatrix(.Row, .Col, lsAmt)
                            lbResult = RefreshTotal()

                            If lbResult = False Then
                                MsgBox("Mapped amount exceeds !", MsgBoxStyle.Critical, gProjectName)
                                .set_TextMatrix(.Row, .Col, lsCellTxt)
                            End If
                        Else
                            MsgBox("Exception : " & lsExcpAmt, MsgBoxStyle.Information)
                        End If
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub msfGrid_KeyDownEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyDownEvent) Handles msfGrid.KeyDownEvent
        Dim frmObj As Form
        Dim BrkpId As Long
        Dim lsSql As String = ""
        Dim lsTxt As String = ""
        Dim lsFindTxt As String = ""
        Dim lnCount As Long
        Dim i As Integer, j As Integer
        Dim lnPrvRow As Integer, lnPrvCol As Integer
        Dim lnStartRow As Integer, lnStartCol As Integer
        Dim lbSearchFlag As Boolean

        With msfGrid
            If .Rows > 1 Then
                BrkpId = Val(.get_TextMatrix(.Row, 11))
                Select Case e.keyCode
                    Case 32
                        If BrkpId <> 0 Then
                            frmObj = New frmPrfRemarkEntry(BrkpId)
                            frmObj.ShowDialog()
                            LoadData(BrkpId, .Row)
                        End If
                    Case 112
                    Case 113
                        If BrkpId <> 0 Then
                            frmObj = New frmP2pKoReport(BrkpId)
                            frmObj.ShowDialog()
                        End If
                    Case 67
                        If e.shift = 2 Then Clipboard.SetText(msfGrid.Text)
                    Case 84
                        If e.shift = 2 Then
                            lsFindTxt = InputBox("File Name", "Tracker", "", 240, 40).ToUpper.Trim

                            If lsFindTxt <> "" Then
                                lsFindTxt = gfExecuteScalar("select proof_gid from tds_trn_tspooltracker " _
                                          & "where upload_filename = '" & lsFindTxt & "' " _
                                          & "and proof_gid <> 0 " _
                                          & "and delete_flag = 'N'", gOdbcConn)

                                If lsFindTxt <> "" Then
                                    With msfGrid
                                        lnPrvRow = .Row
                                        lnPrvCol = .Col
                                        .CellBackColor = Color.White

                                        lnStartRow = lnPrvRow
                                        lnStartCol = .Col + 1

                                        If lnStartCol = .Cols Then
                                            lnStartCol = .FixedCols
                                            lnStartRow = lnPrvRow + 1
                                        End If

                                        For i = lnStartRow To .Rows - 1
                                            For j = lnStartCol To .Cols - 1
                                                lsTxt = .get_TextMatrix(i, j).ToUpper.Trim

                                                If lsTxt.Contains(lsFindTxt) = True Then
                                                    .TopRow = i
                                                    .Row = i
                                                    .Col = j
                                                    .CellBackColor = Color.Yellow
                                                    lbSearchFlag = True
                                                    Exit For
                                                End If
                                            Next j

                                            If lbSearchFlag = True Then Exit For
                                            lnStartCol = .FixedCols
                                        Next i

                                        If lbSearchFlag = False Then
                                            MsgBox("No matches found !", MsgBoxStyle.Critical, gProjectName)
                                        End If
                                    End With
                                End If
                            End If
                        End If
                    Case 70
                        If e.shift = 2 Then
                            lsFindTxt = InputBox("Find", "Search", "", 240, 40).ToUpper.Trim

                            Do
                                If lsFindTxt <> "" Then
                                    lbSearchFlag = False

                                    With msfGrid
                                        lnPrvRow = .Row
                                        lnPrvCol = .Col
                                        .CellBackColor = Color.White

                                        lnStartRow = lnPrvRow
                                        lnStartCol = .Col + 1

                                        If lnStartCol = .Cols Then
                                            lnStartCol = .FixedCols
                                            lnStartRow = lnPrvRow + 1
                                        End If

                                        For i = lnStartRow To .Rows - 1
                                            For j = lnStartCol To .Cols - 1
                                                lsTxt = .get_TextMatrix(i, j).ToUpper.Trim

                                                If lsTxt.Contains(lsFindTxt) = True Then
                                                    .TopRow = i
                                                    .Row = i
                                                    .Col = j
                                                    .CellBackColor = Color.Yellow
                                                    lbSearchFlag = True
                                                    Exit For
                                                End If
                                            Next j

                                            If lbSearchFlag = True Then Exit For
                                            lnStartCol = .FixedCols
                                        Next i

                                        If lbSearchFlag = True Then
                                            lsFindTxt = InputBox("Find", "Search", lsFindTxt, 240, 40).ToUpper.Trim
                                        End If
                                    End With
                                End If
                            Loop Until lbSearchFlag = False Or lsFindTxt = ""

                            .CellBackColor = Color.White
                        End If
                End Select
            End If
        End With
    End Sub

    Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAll.Click
        Dim i As Integer
        Dim lbResult As Boolean

        With msfKo
            If chkKo.Checked = True Then
                .Col = 6

                For i = 1 To .Rows - 1
                    .Row = i

                    .set_TextMatrix(.Row, .Col, " ")
                    .CellPicture = imgChkBox.Images(1)

                    lbResult = RefreshTotal()

                    If lbResult = False Then
                        .set_TextMatrix(.Row, .Col, "")
                        .CellPicture = imgChkBox.Images(0)
                    End If
                Next i
            End If
        End With
    End Sub

    Private Sub btnClearSele_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSele.Click
        Dim lbResult As Boolean
        msfKo.Rows = 1
        lbResult = RefreshTotal()
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim i As Integer
        Dim lsAccMode As String
        Dim lbResult As Boolean

        With msfKo
            If .Rows > 1 And chkKo.Checked = True Then
                .Rows = 2

                lsAccMode = .get_TextMatrix(1, 9)

                For i = 1 To msfGrid.Rows - 1
                    If lsAccMode = "D" And msfGrid.get_TextMatrix(i, 4) <> "" Then
                        AddRow(.Rows, Val(msfGrid.get_TextMatrix(i, 11)), False)
                    ElseIf lsAccMode = "C" And msfGrid.get_TextMatrix(i, 3) <> "" Then
                        AddRow(.Rows, Val(msfGrid.get_TextMatrix(i, 11)), True)
                    End If
                Next i

                lbResult = RefreshTotal()
            End If
        End With
    End Sub

    Private Sub btnKo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKo.Click
        Dim lnDrAmt As Double = 0
        Dim lnCrAmt As Double = 0
        Dim lnKoAmt As Double = 0
        Dim lnDrId As Long = 0
        Dim lnCrId As Long = 0
        Dim lnResult As Long = 0

        Dim i As Integer, c As Integer = 0

        With msfKo
            If .Rows > 1 And chkKo.Checked = True Then
                For i = 1 To .Rows - 1
                    If .get_TextMatrix(i, 6) <> "" Then
                        Select Case .get_TextMatrix(i, 9)
                            Case "D"
                                lnDrAmt += Val(.get_TextMatrix(i, 4))
                            Case "C"
                                lnCrAmt += Val(.get_TextMatrix(i, 5))
                        End Select
                    End If
                Next i

                If lnDrAmt <> 0 And lnCrAmt <> 0 Then
                    Select Case .get_TextMatrix(1, 9)
                        Case "D"
                            lnDrId = Val(.get_TextMatrix(1, 8))
                        Case "C"
                            lnCrId = Val(.get_TextMatrix(1, 8))
                    End Select


                    For i = 2 To .Rows - 1
                        If .get_TextMatrix(i, 6) <> "" Then
                            Select Case .get_TextMatrix(i, 9)
                                Case "D"
                                    lnDrId = Val(.get_TextMatrix(i, 8))
                                Case "C"
                                    lnCrId = Val(.get_TextMatrix(i, 8))
                            End Select

                            lnKoAmt = Val(.get_TextMatrix(i, 4)) + Val(.get_TextMatrix(i, 5))
                            lnResult = Knockoff(lnKoAmt, lnDrId, lnCrId, mnKoFlag, gUserName)

                            If lnResult <> 0 Then c += 1
                        End If
                    Next i

                End If

                MsgBox(c & " records knockoff successfully !", MsgBoxStyle.Information, gProjectName)

                msfKo.Rows = msfKo.FixedRows
                btnRefresh.PerformClick()
            End If
        End With
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            PrintFGridXMLMerge(msfGrid, gsReportPath & "\Report.xls", "Report")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboAccNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboP2pAccNo.SelectedIndexChanged

    End Sub

    Private Sub msfKo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msfKo.Enter

    End Sub
End Class
