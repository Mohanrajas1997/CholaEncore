<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferCheque
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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.txtShortAgreementNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPayMode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAgmntNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtOldSwapNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvPdc = New System.Windows.Forms.DataGridView()
        Me.dgvSpdc = New System.Windows.Forms.DataGridView()
        Me.grpNewPacket = New System.Windows.Forms.GroupBox()
        Me.cboNewPkt = New System.Windows.Forms.ComboBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpMain.SuspendLayout()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNewPacket.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.txtShortAgreementNo)
        Me.grpMain.Controls.Add(Me.txtPayMode)
        Me.grpMain.Controls.Add(Me.Label3)
        Me.grpMain.Controls.Add(Me.txtAgmntNo)
        Me.grpMain.Controls.Add(Me.Label4)
        Me.grpMain.Controls.Add(Me.txtPktNo)
        Me.grpMain.Controls.Add(Me.Label5)
        Me.grpMain.Controls.Add(Me.btnClear)
        Me.grpMain.Controls.Add(Me.btnRefresh)
        Me.grpMain.Controls.Add(Me.btnClose)
        Me.grpMain.Controls.Add(Me.txtOldSwapNo)
        Me.grpMain.Controls.Add(Me.Label1)
        Me.grpMain.Controls.Add(Me.Label6)
        Me.grpMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grpMain.Location = New System.Drawing.Point(9, 0)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(673, 104)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        Me.grpMain.Text = "Old Packet Details"
        '
        'txtShortAgreementNo
        '
        Me.txtShortAgreementNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtShortAgreementNo.Location = New System.Drawing.Point(437, 74)
        Me.txtShortAgreementNo.Name = "txtShortAgreementNo"
        Me.txtShortAgreementNo.Size = New System.Drawing.Size(223, 21)
        Me.txtShortAgreementNo.TabIndex = 12
        Me.txtShortAgreementNo.TabStop = False
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(322, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 15)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Short Agreement No"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPayMode
        '
        Me.txtPayMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPayMode.Location = New System.Drawing.Point(437, 47)
        Me.txtPayMode.Name = "txtPayMode"
        Me.txtPayMode.Size = New System.Drawing.Size(223, 21)
        Me.txtPayMode.TabIndex = 9
        Me.txtPayMode.TabStop = False
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(339, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 15)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Pay Mode"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAgmntNo
        '
        Me.txtAgmntNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAgmntNo.Location = New System.Drawing.Point(103, 74)
        Me.txtAgmntNo.Name = "txtAgmntNo"
        Me.txtAgmntNo.Size = New System.Drawing.Size(223, 21)
        Me.txtAgmntNo.TabIndex = 8
        Me.txtAgmntNo.TabStop = False
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(5, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Agreement No"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPktNo
        '
        Me.txtPktNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPktNo.Location = New System.Drawing.Point(103, 47)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(223, 21)
        Me.txtPktNo.TabIndex = 7
        Me.txtPktNo.TabStop = False
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(19, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Packet No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Location = New System.Drawing.Point(510, 16)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnRefresh.Location = New System.Drawing.Point(432, 16)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(588, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtOldSwapNo
        '
        Me.txtOldSwapNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.txtOldSwapNo.Location = New System.Drawing.Point(103, 20)
        Me.txtOldSwapNo.Name = "txtOldSwapNo"
        Me.txtOldSwapNo.Size = New System.Drawing.Size(223, 21)
        Me.txtOldSwapNo.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Old Swap No"
        '
        'dgvPdc
        '
        Me.dgvPdc.AllowUserToAddRows = False
        Me.dgvPdc.AllowUserToDeleteRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black
        Me.dgvPdc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvPdc.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPdc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.dgvPdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPdc.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvPdc.Location = New System.Drawing.Point(9, 110)
        Me.dgvPdc.Name = "dgvPdc"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPdc.RowHeadersDefaultCellStyle = DataGridViewCellStyle15
        Me.dgvPdc.RowHeadersVisible = False
        Me.dgvPdc.Size = New System.Drawing.Size(603, 157)
        Me.dgvPdc.TabIndex = 1
        '
        'dgvSpdc
        '
        Me.dgvSpdc.AllowUserToAddRows = False
        Me.dgvSpdc.AllowUserToDeleteRows = False
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black
        Me.dgvSpdc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvSpdc.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSpdc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.dgvSpdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSpdc.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvSpdc.Location = New System.Drawing.Point(12, 273)
        Me.dgvSpdc.Name = "dgvSpdc"
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSpdc.RowHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgvSpdc.RowHeadersVisible = False
        Me.dgvSpdc.Size = New System.Drawing.Size(600, 131)
        Me.dgvSpdc.TabIndex = 3
        '
        'grpNewPacket
        '
        Me.grpNewPacket.Controls.Add(Me.cboNewPkt)
        Me.grpNewPacket.Controls.Add(Me.btnUpdate)
        Me.grpNewPacket.Controls.Add(Me.Label2)
        Me.grpNewPacket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grpNewPacket.Location = New System.Drawing.Point(12, 410)
        Me.grpNewPacket.Name = "grpNewPacket"
        Me.grpNewPacket.Size = New System.Drawing.Size(604, 57)
        Me.grpNewPacket.TabIndex = 4
        Me.grpNewPacket.TabStop = False
        Me.grpNewPacket.Text = "New Packet Details"
        '
        'cboNewPkt
        '
        Me.cboNewPkt.FormattingEnabled = True
        Me.cboNewPkt.Location = New System.Drawing.Point(103, 20)
        Me.cboNewPkt.Name = "cboNewPkt"
        Me.cboNewPkt.Size = New System.Drawing.Size(223, 21)
        Me.cboNewPkt.TabIndex = 2
        '
        'btnUpdate
        '
        Me.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnUpdate.Location = New System.Drawing.Point(339, 17)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(72, 24)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(23, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Gnsa Ref No"
        '
        'frmTransferCheque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(800, 479)
        Me.Controls.Add(Me.grpNewPacket)
        Me.Controls.Add(Me.dgvSpdc)
        Me.Controls.Add(Me.dgvPdc)
        Me.Controls.Add(Me.grpMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmTransferCheque"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Cheque From One Packet To Another"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpNewPacket.ResumeLayout(False)
        Me.grpNewPacket.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents dgvPdc As System.Windows.Forms.DataGridView
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtOldSwapNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvSpdc As System.Windows.Forms.DataGridView
    Friend WithEvents grpNewPacket As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboNewPkt As System.Windows.Forms.ComboBox
    Friend WithEvents txtPayMode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAgmntNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtShortAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
