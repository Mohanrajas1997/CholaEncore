<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPktMis
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPktMis))
        Me.pnlCtrl = New System.Windows.Forms.Panel()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.msfGrid = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.pnlCtrl.SuspendLayout()
        CType(Me.msfGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlCtrl
        '
        Me.pnlCtrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCtrl.Controls.Add(Me.btnexport)
        Me.pnlCtrl.Controls.Add(Me.btnclose)
        Me.pnlCtrl.Controls.Add(Me.btnrefresh)
        Me.pnlCtrl.Controls.Add(Me.dtpTo)
        Me.pnlCtrl.Controls.Add(Me.Label2)
        Me.pnlCtrl.Controls.Add(Me.dtpFrom)
        Me.pnlCtrl.Controls.Add(Me.Label1)
        Me.pnlCtrl.Location = New System.Drawing.Point(7, 7)
        Me.pnlCtrl.Name = "pnlCtrl"
        Me.pnlCtrl.Size = New System.Drawing.Size(636, 44)
        Me.pnlCtrl.TabIndex = 0
        '
        'btnexport
        '
        Me.btnexport.Location = New System.Drawing.Point(472, 9)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(72, 24)
        Me.btnexport.TabIndex = 6
        Me.btnexport.Text = "&Export"
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(550, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(72, 24)
        Me.btnclose.TabIndex = 7
        Me.btnclose.Text = "Close"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(394, 9)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnrefresh.TabIndex = 5
        Me.btnrefresh.Text = "&Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(282, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(101, 21)
        Me.dtpTo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(255, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To"
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(114, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(101, 21)
        Me.dtpFrom.TabIndex = 0
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
        'frmPktMis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(803, 361)
        Me.Controls.Add(Me.msfGrid)
        Me.Controls.Add(Me.pnlCtrl)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPktMis"
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
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents msfGrid As AxMSFlexGridLib.AxMSFlexGrid
End Class
