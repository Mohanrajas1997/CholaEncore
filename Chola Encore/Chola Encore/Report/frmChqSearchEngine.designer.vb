<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChqSearchEngine
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
        Me.dgvPdcPullout = New System.Windows.Forms.DataGridView()
        Me.dgvSpdc = New System.Windows.Forms.DataGridView()
        Me.dgvSpdcPullout = New System.Windows.Forms.DataGridView()
        Me.dgvPdc = New System.Windows.Forms.DataGridView()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.lblPDCPullout = New System.Windows.Forms.Label()
        Me.lblPdc = New System.Windows.Forms.Label()
        Me.lblSpdcPullout = New System.Windows.Forms.Label()
        Me.lblSpdc = New System.Windows.Forms.Label()
        Me.dgvBounce = New System.Windows.Forms.DataGridView()
        Me.lblBounce = New System.Windows.Forms.Label()
        Me.lblPdcCount = New System.Windows.Forms.Label()
        Me.lblBounceCount = New System.Windows.Forms.Label()
        Me.lblSpdcCount = New System.Windows.Forms.Label()
        Me.dgvLooseChq = New System.Windows.Forms.DataGridView()
        Me.lblLooseChq = New System.Windows.Forms.Label()
        CType(Me.dgvPdcPullout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSpdcPullout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMain.SuspendLayout()
        CType(Me.dgvBounce, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLooseChq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPdcPullout
        '
        Me.dgvPdcPullout.AllowUserToAddRows = False
        Me.dgvPdcPullout.AllowUserToDeleteRows = False
        Me.dgvPdcPullout.BackgroundColor = System.Drawing.Color.White
        Me.dgvPdcPullout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPdcPullout.Location = New System.Drawing.Point(10, 81)
        Me.dgvPdcPullout.Name = "dgvPdcPullout"
        Me.dgvPdcPullout.ReadOnly = True
        Me.dgvPdcPullout.Size = New System.Drawing.Size(811, 86)
        Me.dgvPdcPullout.TabIndex = 11
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
        'dgvSpdcPullout
        '
        Me.dgvSpdcPullout.AllowUserToAddRows = False
        Me.dgvSpdcPullout.AllowUserToDeleteRows = False
        Me.dgvSpdcPullout.BackgroundColor = System.Drawing.Color.White
        Me.dgvSpdcPullout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSpdcPullout.Location = New System.Drawing.Point(10, 259)
        Me.dgvSpdcPullout.Name = "dgvSpdcPullout"
        Me.dgvSpdcPullout.ReadOnly = True
        Me.dgvSpdcPullout.Size = New System.Drawing.Size(811, 86)
        Me.dgvSpdcPullout.TabIndex = 13
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
        Me.grpMain.Controls.Add(Me.btnClose)
        Me.grpMain.Controls.Add(Me.btnClear)
        Me.grpMain.Controls.Add(Me.btnExport)
        Me.grpMain.Controls.Add(Me.btnSearch)
        Me.grpMain.Controls.Add(Me.Label2)
        Me.grpMain.Controls.Add(Me.txtChqNo)
        Me.grpMain.Location = New System.Drawing.Point(11, 1)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(763, 52)
        Me.grpMain.TabIndex = 12
        Me.grpMain.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(676, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 23)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(596, 16)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(74, 23)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(516, 16)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(74, 23)
        Me.btnExport.TabIndex = 8
        Me.btnExport.Text = "&Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(436, 16)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "&Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Cheque No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqNo
        '
        Me.txtChqNo.Location = New System.Drawing.Point(83, 18)
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.Size = New System.Drawing.Size(116, 21)
        Me.txtChqNo.TabIndex = 1
        '
        'lblPDCPullout
        '
        Me.lblPDCPullout.AutoSize = True
        Me.lblPDCPullout.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPDCPullout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPDCPullout.Location = New System.Drawing.Point(17, 61)
        Me.lblPDCPullout.Name = "lblPDCPullout"
        Me.lblPDCPullout.Size = New System.Drawing.Size(71, 13)
        Me.lblPDCPullout.TabIndex = 16
        Me.lblPDCPullout.Text = "PDC Pullout"
        '
        'lblPdc
        '
        Me.lblPdc.AutoSize = True
        Me.lblPdc.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPdc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPdc.Location = New System.Drawing.Point(91, 61)
        Me.lblPdc.Name = "lblPdc"
        Me.lblPdc.Size = New System.Drawing.Size(29, 13)
        Me.lblPdc.TabIndex = 17
        Me.lblPdc.Text = "PDC"
        '
        'lblSpdcPullout
        '
        Me.lblSpdcPullout.AutoSize = True
        Me.lblSpdcPullout.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpdcPullout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSpdcPullout.Location = New System.Drawing.Point(168, 61)
        Me.lblSpdcPullout.Name = "lblSpdcPullout"
        Me.lblSpdcPullout.Size = New System.Drawing.Size(78, 13)
        Me.lblSpdcPullout.TabIndex = 18
        Me.lblSpdcPullout.Text = "SPDC Pullout"
        '
        'lblSpdc
        '
        Me.lblSpdc.AutoSize = True
        Me.lblSpdc.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpdc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSpdc.Location = New System.Drawing.Point(247, 61)
        Me.lblSpdc.Name = "lblSpdc"
        Me.lblSpdc.Size = New System.Drawing.Size(36, 13)
        Me.lblSpdc.TabIndex = 19
        Me.lblSpdc.Text = "SPDC"
        '
        'dgvBounce
        '
        Me.dgvBounce.AllowUserToAddRows = False
        Me.dgvBounce.AllowUserToDeleteRows = False
        Me.dgvBounce.BackgroundColor = System.Drawing.Color.White
        Me.dgvBounce.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBounce.Location = New System.Drawing.Point(10, 529)
        Me.dgvBounce.Name = "dgvBounce"
        Me.dgvBounce.ReadOnly = True
        Me.dgvBounce.Size = New System.Drawing.Size(811, 86)
        Me.dgvBounce.TabIndex = 21
        '
        'lblBounce
        '
        Me.lblBounce.AutoSize = True
        Me.lblBounce.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBounce.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblBounce.Location = New System.Drawing.Point(444, 61)
        Me.lblBounce.Name = "lblBounce"
        Me.lblBounce.Size = New System.Drawing.Size(48, 13)
        Me.lblBounce.TabIndex = 22
        Me.lblBounce.Text = "Bounce"
        '
        'lblPdcCount
        '
        Me.lblPdcCount.AutoSize = True
        Me.lblPdcCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPdcCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPdcCount.Location = New System.Drawing.Point(530, 61)
        Me.lblPdcCount.Name = "lblPdcCount"
        Me.lblPdcCount.Size = New System.Drawing.Size(0, 13)
        Me.lblPdcCount.TabIndex = 23
        '
        'lblBounceCount
        '
        Me.lblBounceCount.AutoSize = True
        Me.lblBounceCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBounceCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBounceCount.Location = New System.Drawing.Point(616, 61)
        Me.lblBounceCount.Name = "lblBounceCount"
        Me.lblBounceCount.Size = New System.Drawing.Size(0, 13)
        Me.lblBounceCount.TabIndex = 24
        '
        'lblSpdcCount
        '
        Me.lblSpdcCount.AutoSize = True
        Me.lblSpdcCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpdcCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSpdcCount.Location = New System.Drawing.Point(788, 60)
        Me.lblSpdcCount.Name = "lblSpdcCount"
        Me.lblSpdcCount.Size = New System.Drawing.Size(0, 13)
        Me.lblSpdcCount.TabIndex = 26
        '
        'dgvLooseChq
        '
        Me.dgvLooseChq.AllowUserToAddRows = False
        Me.dgvLooseChq.AllowUserToDeleteRows = False
        Me.dgvLooseChq.BackgroundColor = System.Drawing.Color.White
        Me.dgvLooseChq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLooseChq.Location = New System.Drawing.Point(10, 440)
        Me.dgvLooseChq.Name = "dgvLooseChq"
        Me.dgvLooseChq.ReadOnly = True
        Me.dgvLooseChq.Size = New System.Drawing.Size(811, 86)
        Me.dgvLooseChq.TabIndex = 27
        '
        'lblLooseChq
        '
        Me.lblLooseChq.AutoSize = True
        Me.lblLooseChq.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLooseChq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblLooseChq.Location = New System.Drawing.Point(509, 61)
        Me.lblLooseChq.Name = "lblLooseChq"
        Me.lblLooseChq.Size = New System.Drawing.Size(85, 13)
        Me.lblLooseChq.TabIndex = 28
        Me.lblLooseChq.Text = "Loose Cheque"
        '
        'frmChqSearchEngine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 687)
        Me.Controls.Add(Me.lblLooseChq)
        Me.Controls.Add(Me.dgvLooseChq)
        Me.Controls.Add(Me.lblSpdcCount)
        Me.Controls.Add(Me.lblBounceCount)
        Me.Controls.Add(Me.lblPdcCount)
        Me.Controls.Add(Me.lblBounce)
        Me.Controls.Add(Me.dgvBounce)
        Me.Controls.Add(Me.lblSpdc)
        Me.Controls.Add(Me.lblSpdcPullout)
        Me.Controls.Add(Me.lblPdc)
        Me.Controls.Add(Me.lblPDCPullout)
        Me.Controls.Add(Me.grpMain)
        Me.Controls.Add(Me.dgvPdc)
        Me.Controls.Add(Me.dgvSpdcPullout)
        Me.Controls.Add(Me.dgvSpdc)
        Me.Controls.Add(Me.dgvPdcPullout)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmChqSearchEngine"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cheque Search Engine"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgvPdcPullout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSpdcPullout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        CType(Me.dgvBounce, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLooseChq, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Friend WithEvents dgvPdcPullout As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSpdc As System.Windows.Forms.DataGridView
    Friend WithEvents dgvSpdcPullout As System.Windows.Forms.DataGridView
    Friend WithEvents dgvPdc As System.Windows.Forms.DataGridView
    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtChqNo As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents lblPDCPullout As System.Windows.Forms.Label
    Friend WithEvents lblPdc As System.Windows.Forms.Label
    Friend WithEvents lblSpdcPullout As System.Windows.Forms.Label
    Friend WithEvents lblSpdc As System.Windows.Forms.Label
    Friend WithEvents dgvBounce As System.Windows.Forms.DataGridView
    Friend WithEvents lblBounce As System.Windows.Forms.Label
    Friend WithEvents lblPdcCount As System.Windows.Forms.Label
    Friend WithEvents lblBounceCount As System.Windows.Forms.Label
    Friend WithEvents lblSpdcCount As System.Windows.Forms.Label
    Friend WithEvents dgvLooseChq As System.Windows.Forms.DataGridView
    Friend WithEvents lblLooseChq As System.Windows.Forms.Label
End Class
