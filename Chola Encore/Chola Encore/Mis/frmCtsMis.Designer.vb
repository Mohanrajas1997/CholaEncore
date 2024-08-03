<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCtsMis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCtsMis))
        Me.pnlCtrl = New System.Windows.Forms.Panel()
        Me.cbochqstatus = New System.Windows.Forms.ComboBox()
        Me.lblchqstatus = New System.Windows.Forms.Label()
        Me.cboctsstatus = New System.Windows.Forms.ComboBox()
        Me.lblctsstatus = New System.Windows.Forms.Label()
        Me.cbotype = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.dtpto = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.msfGrid = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.pnlCtrl.SuspendLayout()
        CType(Me.msfGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlCtrl
        '
        Me.pnlCtrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCtrl.Controls.Add(Me.cbochqstatus)
        Me.pnlCtrl.Controls.Add(Me.lblchqstatus)
        Me.pnlCtrl.Controls.Add(Me.cboctsstatus)
        Me.pnlCtrl.Controls.Add(Me.lblctsstatus)
        Me.pnlCtrl.Controls.Add(Me.cbotype)
        Me.pnlCtrl.Controls.Add(Me.Label28)
        Me.pnlCtrl.Controls.Add(Me.btnexport)
        Me.pnlCtrl.Controls.Add(Me.btnclose)
        Me.pnlCtrl.Controls.Add(Me.btnrefresh)
        Me.pnlCtrl.Controls.Add(Me.dtpto)
        Me.pnlCtrl.Controls.Add(Me.Label2)
        Me.pnlCtrl.Controls.Add(Me.dtpfrom)
        Me.pnlCtrl.Controls.Add(Me.Label1)
        Me.pnlCtrl.Location = New System.Drawing.Point(7, 7)
        Me.pnlCtrl.Name = "pnlCtrl"
        Me.pnlCtrl.Size = New System.Drawing.Size(760, 77)
        Me.pnlCtrl.TabIndex = 0
        '
        'cbochqstatus
        '
        Me.cbochqstatus.FormattingEnabled = True
        Me.cbochqstatus.Location = New System.Drawing.Point(333, 43)
        Me.cbochqstatus.Name = "cbochqstatus"
        Me.cbochqstatus.Size = New System.Drawing.Size(114, 21)
        Me.cbochqstatus.TabIndex = 4
        '
        'lblchqstatus
        '
        Me.lblchqstatus.AutoSize = True
        Me.lblchqstatus.Location = New System.Drawing.Point(259, 46)
        Me.lblchqstatus.Name = "lblchqstatus"
        Me.lblchqstatus.Size = New System.Drawing.Size(68, 13)
        Me.lblchqstatus.TabIndex = 12
        Me.lblchqstatus.Text = "Chq Status"
        '
        'cboctsstatus
        '
        Me.cboctsstatus.FormattingEnabled = True
        Me.cboctsstatus.Location = New System.Drawing.Point(114, 43)
        Me.cboctsstatus.Name = "cboctsstatus"
        Me.cboctsstatus.Size = New System.Drawing.Size(114, 21)
        Me.cboctsstatus.TabIndex = 3
        '
        'lblctsstatus
        '
        Me.lblctsstatus.AutoSize = True
        Me.lblctsstatus.Location = New System.Drawing.Point(40, 46)
        Me.lblctsstatus.Name = "lblctsstatus"
        Me.lblctsstatus.Size = New System.Drawing.Size(68, 13)
        Me.lblctsstatus.TabIndex = 10
        Me.lblctsstatus.Text = "CTS Status"
        '
        'cbotype
        '
        Me.cbotype.FormattingEnabled = True
        Me.cbotype.Location = New System.Drawing.Point(514, 12)
        Me.cbotype.Name = "cbotype"
        Me.cbotype.Size = New System.Drawing.Size(231, 21)
        Me.cbotype.TabIndex = 2
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(473, 15)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(35, 13)
        Me.Label28.TabIndex = 8
        Me.Label28.Text = "Type"
        '
        'btnexport
        '
        Me.btnexport.Location = New System.Drawing.Point(597, 41)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(72, 24)
        Me.btnexport.TabIndex = 6
        Me.btnexport.Text = "&Export"
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(675, 41)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(72, 24)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(519, 41)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnrefresh.TabIndex = 5
        Me.btnrefresh.Text = "&Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'dtpto
        '
        Me.dtpto.Checked = False
        Me.dtpto.CustomFormat = "dd-MM-yyyy"
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpto.Location = New System.Drawing.Point(333, 12)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.ShowCheckBox = True
        Me.dtpto.Size = New System.Drawing.Size(114, 21)
        Me.dtpto.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(306, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To"
        '
        'dtpfrom
        '
        Me.dtpfrom.Checked = False
        Me.dtpfrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfrom.Location = New System.Drawing.Point(114, 12)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.ShowCheckBox = True
        Me.dtpfrom.Size = New System.Drawing.Size(114, 21)
        Me.dtpfrom.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Received From"
        '
        'msfGrid
        '
        Me.msfGrid.Location = New System.Drawing.Point(7, 90)
        Me.msfGrid.Name = "msfGrid"
        Me.msfGrid.OcxState = CType(resources.GetObject("msfGrid.OcxState"), System.Windows.Forms.AxHost.State)
        Me.msfGrid.Size = New System.Drawing.Size(298, 195)
        Me.msfGrid.TabIndex = 0
        '
        'frmCtsMis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(803, 361)
        Me.Controls.Add(Me.msfGrid)
        Me.Controls.Add(Me.pnlCtrl)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCtsMis"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CTS - MIS"
        Me.pnlCtrl.ResumeLayout(False)
        Me.pnlCtrl.PerformLayout()
        CType(Me.msfGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlCtrl As System.Windows.Forms.Panel
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboctsstatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblctsstatus As System.Windows.Forms.Label
    Friend WithEvents cbotype As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cbochqstatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblchqstatus As System.Windows.Forms.Label
    Friend WithEvents msfGrid As AxMSFlexGridLib.AxMSFlexGrid
End Class
