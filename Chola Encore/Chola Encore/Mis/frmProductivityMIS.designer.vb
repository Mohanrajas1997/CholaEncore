<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductivityMIS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductivityMIS))
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.cboUserName = New System.Windows.Forms.ComboBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.msfGrid = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.grpMain.SuspendLayout()
        CType(Me.msfGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.BackColor = System.Drawing.SystemColors.Control
        Me.grpMain.Controls.Add(Me.cboUserName)
        Me.grpMain.Controls.Add(Me.btnClose)
        Me.grpMain.Controls.Add(Me.btnClear)
        Me.grpMain.Controls.Add(Me.btnExport)
        Me.grpMain.Controls.Add(Me.btnRefresh)
        Me.grpMain.Controls.Add(Me.dtpToDate)
        Me.grpMain.Controls.Add(Me.lblToDate)
        Me.grpMain.Controls.Add(Me.dtpFromDate)
        Me.grpMain.Controls.Add(Me.lblUserName)
        Me.grpMain.Controls.Add(Me.lblFromDate)
        Me.grpMain.Location = New System.Drawing.Point(8, 5)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(737, 83)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        '
        'cboUserName
        '
        Me.cboUserName.FormattingEnabled = True
        Me.cboUserName.Location = New System.Drawing.Point(490, 16)
        Me.cboUserName.Name = "cboUserName"
        Me.cboUserName.Size = New System.Drawing.Size(231, 21)
        Me.cboUserName.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(649, 43)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(571, 43)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(493, 43)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 4
        Me.btnExport.Text = "&Export"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(415, 43)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = ""
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(282, 16)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(108, 21)
        Me.dtpToDate.TabIndex = 1
        Me.dtpToDate.Value = New Date(2008, 12, 22, 0, 0, 0, 0)
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(255, 19)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(21, 13)
        Me.lblToDate.TabIndex = 2
        Me.lblToDate.Text = "To"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = ""
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(84, 16)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(108, 21)
        Me.dtpFromDate.TabIndex = 0
        Me.dtpFromDate.Value = New Date(2008, 12, 22, 0, 0, 0, 0)
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(419, 19)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(68, 13)
        Me.lblUserName.TabIndex = 4
        Me.lblUserName.Text = "User Name"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(12, 19)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(69, 13)
        Me.lblFromDate.TabIndex = 0
        Me.lblFromDate.Text = "Entry From"
        '
        'msfGrid
        '
        Me.msfGrid.Location = New System.Drawing.Point(6, 94)
        Me.msfGrid.Name = "msfGrid"
        Me.msfGrid.OcxState = CType(resources.GetObject("msfGrid.OcxState"), System.Windows.Forms.AxHost.State)
        Me.msfGrid.Size = New System.Drawing.Size(908, 509)
        Me.msfGrid.TabIndex = 10
        '
        'frmProductivityMIS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(919, 627)
        Me.Controls.Add(Me.msfGrid)
        Me.Controls.Add(Me.grpMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmProductivityMIS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Productivity MIS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        CType(Me.msfGrid, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents msfGrid As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cboUserName As System.Windows.Forms.ComboBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
End Class
