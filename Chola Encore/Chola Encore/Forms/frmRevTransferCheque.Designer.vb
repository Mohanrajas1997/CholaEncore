<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRevTransferCheque
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpNewPkt = New System.Windows.Forms.GroupBox()
        Me.txtPayMode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAgmntNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.dgvPdc = New System.Windows.Forms.DataGridView()
        Me.dgvSpdc = New System.Windows.Forms.DataGridView()
        Me.grpOldPacket = New System.Windows.Forms.GroupBox()
        Me.cboOldPkt = New System.Windows.Forms.ComboBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpNewPkt.SuspendLayout()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOldPacket.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpNewPkt
        '
        Me.grpNewPkt.Controls.Add(Me.txtPayMode)
        Me.grpNewPkt.Controls.Add(Me.Label3)
        Me.grpNewPkt.Controls.Add(Me.txtAgmntNo)
        Me.grpNewPkt.Controls.Add(Me.Label4)
        Me.grpNewPkt.Controls.Add(Me.txtPktNo)
        Me.grpNewPkt.Controls.Add(Me.Label5)
        Me.grpNewPkt.Controls.Add(Me.btnClear)
        Me.grpNewPkt.Controls.Add(Me.btnRefresh)
        Me.grpNewPkt.Controls.Add(Me.btnClose)
        Me.grpNewPkt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grpNewPkt.Location = New System.Drawing.Point(9, 0)
        Me.grpNewPkt.Name = "grpNewPkt"
        Me.grpNewPkt.Size = New System.Drawing.Size(673, 77)
        Me.grpNewPkt.TabIndex = 0
        Me.grpNewPkt.TabStop = False
        Me.grpNewPkt.Text = "New Packet Details"
        '
        'txtPayMode
        '
        Me.txtPayMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPayMode.Location = New System.Drawing.Point(437, 46)
        Me.txtPayMode.Name = "txtPayMode"
        Me.txtPayMode.Size = New System.Drawing.Size(223, 21)
        Me.txtPayMode.TabIndex = 9
        Me.txtPayMode.TabStop = False
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(339, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 15)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Pay Mode"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAgmntNo
        '
        Me.txtAgmntNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAgmntNo.Location = New System.Drawing.Point(103, 46)
        Me.txtAgmntNo.Name = "txtAgmntNo"
        Me.txtAgmntNo.Size = New System.Drawing.Size(223, 21)
        Me.txtAgmntNo.TabIndex = 8
        Me.txtAgmntNo.TabStop = False
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(5, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Agreement No"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPktNo
        '
        Me.txtPktNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPktNo.Location = New System.Drawing.Point(103, 19)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(223, 21)
        Me.txtPktNo.TabIndex = 7
        Me.txtPktNo.TabStop = False
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "New Packet No"
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
        'dgvPdc
        '
        Me.dgvPdc.AllowUserToAddRows = False
        Me.dgvPdc.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgvPdc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPdc.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPdc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPdc.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvPdc.Location = New System.Drawing.Point(9, 110)
        Me.dgvPdc.Name = "dgvPdc"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPdc.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPdc.RowHeadersVisible = False
        Me.dgvPdc.Size = New System.Drawing.Size(603, 157)
        Me.dgvPdc.TabIndex = 1
        '
        'dgvSpdc
        '
        Me.dgvSpdc.AllowUserToAddRows = False
        Me.dgvSpdc.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        Me.dgvSpdc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSpdc.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSpdc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvSpdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSpdc.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvSpdc.Location = New System.Drawing.Point(12, 273)
        Me.dgvSpdc.Name = "dgvSpdc"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSpdc.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvSpdc.RowHeadersVisible = False
        Me.dgvSpdc.Size = New System.Drawing.Size(600, 131)
        Me.dgvSpdc.TabIndex = 3
        '
        'grpOldPacket
        '
        Me.grpOldPacket.Controls.Add(Me.cboOldPkt)
        Me.grpOldPacket.Controls.Add(Me.btnUpdate)
        Me.grpOldPacket.Controls.Add(Me.Label2)
        Me.grpOldPacket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grpOldPacket.Location = New System.Drawing.Point(12, 410)
        Me.grpOldPacket.Name = "grpOldPacket"
        Me.grpOldPacket.Size = New System.Drawing.Size(604, 57)
        Me.grpOldPacket.TabIndex = 4
        Me.grpOldPacket.TabStop = False
        Me.grpOldPacket.Text = "Old Packet Details"
        '
        'cboOldPkt
        '
        Me.cboOldPkt.FormattingEnabled = True
        Me.cboOldPkt.Location = New System.Drawing.Point(103, 20)
        Me.cboOldPkt.Name = "cboOldPkt"
        Me.cboOldPkt.Size = New System.Drawing.Size(223, 21)
        Me.cboOldPkt.TabIndex = 2
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
        Me.Label2.Location = New System.Drawing.Point(15, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Old Swap SNo"
        '
        'frmRevTransferCheque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(800, 479)
        Me.Controls.Add(Me.grpOldPacket)
        Me.Controls.Add(Me.dgvSpdc)
        Me.Controls.Add(Me.dgvPdc)
        Me.Controls.Add(Me.grpNewPkt)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmRevTransferCheque"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reverse Transfer Cheque From New To Old"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpNewPkt.ResumeLayout(False)
        Me.grpNewPkt.PerformLayout()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOldPacket.ResumeLayout(False)
        Me.grpOldPacket.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpNewPkt As System.Windows.Forms.GroupBox
    Friend WithEvents dgvPdc As System.Windows.Forms.DataGridView
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dgvSpdc As System.Windows.Forms.DataGridView
    Friend WithEvents grpOldPacket As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboOldPkt As System.Windows.Forms.ComboBox
    Friend WithEvents txtPayMode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAgmntNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
