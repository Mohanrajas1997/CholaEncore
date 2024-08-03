<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchEngine
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvPkt = New System.Windows.Forms.DataGridView()
        Me.dgvSpdc = New System.Windows.Forms.DataGridView()
        Me.dgvSpdcHeader = New System.Windows.Forms.DataGridView()
        Me.dgvPdc = New System.Windows.Forms.DataGridView()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.cboAgreementNo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtShortAgreementNo = New System.Windows.Forms.TextBox()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.dgvEcsEmi = New System.Windows.Forms.DataGridView()
        Me.lblPacket = New System.Windows.Forms.Label()
        Me.lblPdc = New System.Windows.Forms.Label()
        Me.lblSpdcHeader = New System.Windows.Forms.Label()
        Me.lblSpdc = New System.Windows.Forms.Label()
        Me.lblEcsEmi = New System.Windows.Forms.Label()
        Me.dgvFinone = New System.Windows.Forms.DataGridView()
        Me.lblFin = New System.Windows.Forms.Label()
        Me.lblPdcCount = New System.Windows.Forms.Label()
        Me.lblFinCount = New System.Windows.Forms.Label()
        Me.chkDisc = New System.Windows.Forms.CheckBox()
        Me.lblSpdcCount = New System.Windows.Forms.Label()
        Me.lblEcsEmiCount = New System.Windows.Forms.Label()
        CType(Me.dgvPkt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSpdcHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMain.SuspendLayout()
        CType(Me.dgvEcsEmi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvFinone, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPkt
        '
        Me.dgvPkt.AllowUserToAddRows = False
        Me.dgvPkt.AllowUserToDeleteRows = False
        Me.dgvPkt.BackgroundColor = System.Drawing.Color.White
        Me.dgvPkt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPkt.Location = New System.Drawing.Point(10, 125)
        Me.dgvPkt.Name = "dgvPkt"
        Me.dgvPkt.ReadOnly = True
        Me.dgvPkt.Size = New System.Drawing.Size(811, 42)
        Me.dgvPkt.TabIndex = 11
        '
        'dgvSpdc
        '
        Me.dgvSpdc.AllowUserToAddRows = False
        Me.dgvSpdc.AllowUserToDeleteRows = False
        Me.dgvSpdc.BackgroundColor = System.Drawing.Color.White
        Me.dgvSpdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSpdc.Location = New System.Drawing.Point(10, 348)
        Me.dgvSpdc.Name = "dgvSpdc"
        Me.dgvSpdc.ReadOnly = True
        Me.dgvSpdc.Size = New System.Drawing.Size(811, 86)
        Me.dgvSpdc.TabIndex = 14
        '
        'dgvSpdcHeader
        '
        Me.dgvSpdcHeader.AllowUserToAddRows = False
        Me.dgvSpdcHeader.AllowUserToDeleteRows = False
        Me.dgvSpdcHeader.BackgroundColor = System.Drawing.Color.White
        Me.dgvSpdcHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSpdcHeader.Location = New System.Drawing.Point(10, 259)
        Me.dgvSpdcHeader.Name = "dgvSpdcHeader"
        Me.dgvSpdcHeader.ReadOnly = True
        Me.dgvSpdcHeader.Size = New System.Drawing.Size(811, 86)
        Me.dgvSpdcHeader.TabIndex = 13
        '
        'dgvPdc
        '
        Me.dgvPdc.AllowUserToAddRows = False
        Me.dgvPdc.AllowUserToDeleteRows = False
        Me.dgvPdc.BackgroundColor = System.Drawing.Color.White
        Me.dgvPdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPdc.Location = New System.Drawing.Point(10, 170)
        Me.dgvPdc.Name = "dgvPdc"
        Me.dgvPdc.ReadOnly = True
        Me.dgvPdc.Size = New System.Drawing.Size(811, 86)
        Me.dgvPdc.TabIndex = 12
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.cboAgreementNo)
        Me.grpMain.Controls.Add(Me.Label1)
        Me.grpMain.Controls.Add(Me.btnClose)
        Me.grpMain.Controls.Add(Me.btnClear)
        Me.grpMain.Controls.Add(Me.btnExport)
        Me.grpMain.Controls.Add(Me.btnSearch)
        Me.grpMain.Controls.Add(Me.Label5)
        Me.grpMain.Controls.Add(Me.Label2)
        Me.grpMain.Controls.Add(Me.txtShortAgreementNo)
        Me.grpMain.Controls.Add(Me.txtPktNo)
        Me.grpMain.Location = New System.Drawing.Point(11, 1)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(772, 75)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        '
        'cboAgreementNo
        '
        Me.cboAgreementNo.FormattingEnabled = True
        Me.cboAgreementNo.Location = New System.Drawing.Point(107, 45)
        Me.cboAgreementNo.Name = "cboAgreementNo"
        Me.cboAgreementNo.Size = New System.Drawing.Size(259, 21)
        Me.cboAgreementNo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Agreement No"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(685, 43)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(605, 43)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(74, 23)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(525, 43)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(74, 23)
        Me.btnExport.TabIndex = 4
        Me.btnExport.Text = "&Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(445, 43)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 23)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(372, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Short Agreement No"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(30, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Packet No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShortAgreementNo
        '
        Me.txtShortAgreementNo.Location = New System.Drawing.Point(500, 18)
        Me.txtShortAgreementNo.Name = "txtShortAgreementNo"
        Me.txtShortAgreementNo.Size = New System.Drawing.Size(259, 21)
        Me.txtShortAgreementNo.TabIndex = 1
        '
        'txtPktNo
        '
        Me.txtPktNo.Location = New System.Drawing.Point(107, 18)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(259, 21)
        Me.txtPktNo.TabIndex = 0
        '
        'dgvEcsEmi
        '
        Me.dgvEcsEmi.AllowUserToAddRows = False
        Me.dgvEcsEmi.AllowUserToDeleteRows = False
        Me.dgvEcsEmi.BackgroundColor = System.Drawing.Color.White
        Me.dgvEcsEmi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEcsEmi.Location = New System.Drawing.Point(10, 437)
        Me.dgvEcsEmi.Name = "dgvEcsEmi"
        Me.dgvEcsEmi.ReadOnly = True
        Me.dgvEcsEmi.Size = New System.Drawing.Size(811, 86)
        Me.dgvEcsEmi.TabIndex = 15
        '
        'lblPacket
        '
        Me.lblPacket.AutoSize = True
        Me.lblPacket.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPacket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPacket.Location = New System.Drawing.Point(17, 103)
        Me.lblPacket.Name = "lblPacket"
        Me.lblPacket.Size = New System.Drawing.Size(46, 13)
        Me.lblPacket.TabIndex = 16
        Me.lblPacket.Text = "Packet"
        '
        'lblPdc
        '
        Me.lblPdc.AutoSize = True
        Me.lblPdc.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPdc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPdc.Location = New System.Drawing.Point(91, 103)
        Me.lblPdc.Name = "lblPdc"
        Me.lblPdc.Size = New System.Drawing.Size(71, 13)
        Me.lblPdc.TabIndex = 17
        Me.lblPdc.Text = "PDC Details"
        '
        'lblSpdcHeader
        '
        Me.lblSpdcHeader.AutoSize = True
        Me.lblSpdcHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpdcHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSpdcHeader.Location = New System.Drawing.Point(168, 103)
        Me.lblSpdcHeader.Name = "lblSpdcHeader"
        Me.lblSpdcHeader.Size = New System.Drawing.Size(80, 13)
        Me.lblSpdcHeader.TabIndex = 18
        Me.lblSpdcHeader.Text = "SPDC Header"
        '
        'lblSpdc
        '
        Me.lblSpdc.AutoSize = True
        Me.lblSpdc.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpdc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSpdc.Location = New System.Drawing.Point(247, 103)
        Me.lblSpdc.Name = "lblSpdc"
        Me.lblSpdc.Size = New System.Drawing.Size(78, 13)
        Me.lblSpdc.TabIndex = 19
        Me.lblSpdc.Text = "SPDC Details"
        '
        'lblEcsEmi
        '
        Me.lblEcsEmi.AutoSize = True
        Me.lblEcsEmi.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEcsEmi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblEcsEmi.Location = New System.Drawing.Point(347, 103)
        Me.lblEcsEmi.Name = "lblEcsEmi"
        Me.lblEcsEmi.Size = New System.Drawing.Size(93, 13)
        Me.lblEcsEmi.TabIndex = 20
        Me.lblEcsEmi.Text = "ECS EMI Details"
        '
        'dgvFinone
        '
        Me.dgvFinone.AllowUserToAddRows = False
        Me.dgvFinone.AllowUserToDeleteRows = False
        Me.dgvFinone.BackgroundColor = System.Drawing.Color.White
        Me.dgvFinone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFinone.Location = New System.Drawing.Point(10, 529)
        Me.dgvFinone.Name = "dgvFinone"
        Me.dgvFinone.ReadOnly = True
        Me.dgvFinone.Size = New System.Drawing.Size(811, 86)
        Me.dgvFinone.TabIndex = 21
        '
        'lblFin
        '
        Me.lblFin.AutoSize = True
        Me.lblFin.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblFin.Location = New System.Drawing.Point(444, 103)
        Me.lblFin.Name = "lblFin"
        Me.lblFin.Size = New System.Drawing.Size(44, 13)
        Me.lblFin.TabIndex = 22
        Me.lblFin.Text = "Finone"
        '
        'lblPdcCount
        '
        Me.lblPdcCount.AutoSize = True
        Me.lblPdcCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPdcCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPdcCount.Location = New System.Drawing.Point(530, 103)
        Me.lblPdcCount.Name = "lblPdcCount"
        Me.lblPdcCount.Size = New System.Drawing.Size(0, 13)
        Me.lblPdcCount.TabIndex = 23
        '
        'lblFinCount
        '
        Me.lblFinCount.AutoSize = True
        Me.lblFinCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFinCount.Location = New System.Drawing.Point(616, 103)
        Me.lblFinCount.Name = "lblFinCount"
        Me.lblFinCount.Size = New System.Drawing.Size(0, 13)
        Me.lblFinCount.TabIndex = 24
        '
        'chkDisc
        '
        Me.chkDisc.AutoSize = True
        Me.chkDisc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkDisc.Location = New System.Drawing.Point(518, 102)
        Me.chkDisc.Name = "chkDisc"
        Me.chkDisc.Size = New System.Drawing.Size(188, 17)
        Me.chkDisc.TabIndex = 25
        Me.chkDisc.Text = "Show PDC/Finone Discrepant"
        Me.chkDisc.UseVisualStyleBackColor = True
        '
        'lblSpdcCount
        '
        Me.lblSpdcCount.AutoSize = True
        Me.lblSpdcCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpdcCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpdcCount.Location = New System.Drawing.Point(788, 102)
        Me.lblSpdcCount.Name = "lblSpdcCount"
        Me.lblSpdcCount.Size = New System.Drawing.Size(36, 13)
        Me.lblSpdcCount.TabIndex = 26
        Me.lblSpdcCount.Text = "SPDC"
        '
        'lblEcsEmiCount
        '
        Me.lblEcsEmiCount.AutoSize = True
        Me.lblEcsEmiCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEcsEmiCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEcsEmiCount.Location = New System.Drawing.Point(724, 102)
        Me.lblEcsEmiCount.Name = "lblEcsEmiCount"
        Me.lblEcsEmiCount.Size = New System.Drawing.Size(50, 13)
        Me.lblEcsEmiCount.TabIndex = 27
        Me.lblEcsEmiCount.Text = "ECS Emi"
        '
        'frmSearchEngine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 687)
        Me.Controls.Add(Me.lblEcsEmiCount)
        Me.Controls.Add(Me.lblSpdcCount)
        Me.Controls.Add(Me.chkDisc)
        Me.Controls.Add(Me.lblFinCount)
        Me.Controls.Add(Me.lblPdcCount)
        Me.Controls.Add(Me.lblFin)
        Me.Controls.Add(Me.dgvFinone)
        Me.Controls.Add(Me.lblEcsEmi)
        Me.Controls.Add(Me.lblSpdc)
        Me.Controls.Add(Me.lblSpdcHeader)
        Me.Controls.Add(Me.lblPdc)
        Me.Controls.Add(Me.lblPacket)
        Me.Controls.Add(Me.grpMain)
        Me.Controls.Add(Me.dgvPdc)
        Me.Controls.Add(Me.dgvSpdcHeader)
        Me.Controls.Add(Me.dgvEcsEmi)
        Me.Controls.Add(Me.dgvSpdc)
        Me.Controls.Add(Me.dgvPkt)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSearchEngine"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Engine"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvPkt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSpdcHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        CType(Me.dgvEcsEmi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvFinone, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PerformLayout()
    End Sub

    Friend WithEvents dgvPkt As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSpdc As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSpdcHeader As System.Windows.Forms.DataGridView
    Friend WithEvents dgvPdc As System.Windows.Forms.DataGridView
    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtShortAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dgvEcsEmi As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents lblPacket As System.Windows.Forms.Label
    Friend WithEvents lblPdc As System.Windows.Forms.Label
    Friend WithEvents lblSpdcHeader As System.Windows.Forms.Label
    Friend WithEvents lblSpdc As System.Windows.Forms.Label
    Friend WithEvents lblEcsEmi As System.Windows.Forms.Label
    Friend WithEvents dgvFinone As System.Windows.Forms.DataGridView
    Friend WithEvents lblFin As System.Windows.Forms.Label
    Friend WithEvents lblPdcCount As System.Windows.Forms.Label
    Friend WithEvents lblFinCount As System.Windows.Forms.Label
    Friend WithEvents chkDisc As System.Windows.Forms.CheckBox
    Friend WithEvents lblSpdcCount As System.Windows.Forms.Label
    Friend WithEvents lblEcsEmiCount As System.Windows.Forms.Label
    Friend WithEvents cboAgreementNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
