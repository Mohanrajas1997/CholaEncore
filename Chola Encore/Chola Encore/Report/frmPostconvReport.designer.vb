<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPostconvReport
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
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.txtAgrmntNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.txtChqAmt = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpChqTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpChqFrom = New System.Windows.Forms.DateTimePicker()
        Me.cboFile = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpImportTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpImportFrom = New System.Windows.Forms.DateTimePicker()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.lblRecCount = New System.Windows.Forms.Label()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlButtons.SuspendLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDisplay.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlButtons.Controls.Add(Me.txtAgrmntNo)
        Me.pnlButtons.Controls.Add(Me.Label5)
        Me.pnlButtons.Controls.Add(Me.txtChqNo)
        Me.pnlButtons.Controls.Add(Me.txtChqAmt)
        Me.pnlButtons.Controls.Add(Me.Label21)
        Me.pnlButtons.Controls.Add(Me.Label4)
        Me.pnlButtons.Controls.Add(Me.Label2)
        Me.pnlButtons.Controls.Add(Me.Label3)
        Me.pnlButtons.Controls.Add(Me.dtpChqTo)
        Me.pnlButtons.Controls.Add(Me.dtpChqFrom)
        Me.pnlButtons.Controls.Add(Me.cboFile)
        Me.pnlButtons.Controls.Add(Me.Label9)
        Me.pnlButtons.Controls.Add(Me.Label1)
        Me.pnlButtons.Controls.Add(Me.Label7)
        Me.pnlButtons.Controls.Add(Me.dtpImportTo)
        Me.pnlButtons.Controls.Add(Me.dtpImportFrom)
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnClear)
        Me.pnlButtons.Controls.Add(Me.btnRefresh)
        Me.pnlButtons.Controls.Add(Me.cboStatus)
        Me.pnlButtons.Controls.Add(Me.lbl1)
        Me.pnlButtons.Location = New System.Drawing.Point(12, 12)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(784, 123)
        Me.pnlButtons.TabIndex = 0
        '
        'txtAgrmntNo
        '
        Me.txtAgrmntNo.Location = New System.Drawing.Point(94, 61)
        Me.txtAgrmntNo.MaxLength = 0
        Me.txtAgrmntNo.Name = "txtAgrmntNo"
        Me.txtAgrmntNo.Size = New System.Drawing.Size(283, 21)
        Me.txtAgrmntNo.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(-1, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 135
        Me.Label5.Text = "Agreement No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqNo
        '
        Me.txtChqNo.Location = New System.Drawing.Point(479, 34)
        Me.txtChqNo.MaxLength = 0
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.Size = New System.Drawing.Size(105, 21)
        Me.txtChqNo.TabIndex = 6
        '
        'txtChqAmt
        '
        Me.txtChqAmt.Location = New System.Drawing.Point(668, 34)
        Me.txtChqAmt.MaxLength = 0
        Me.txtChqAmt.Name = "txtChqAmt"
        Me.txtChqAmt.Size = New System.Drawing.Size(105, 21)
        Me.txtChqAmt.TabIndex = 7
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(395, 38)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(78, 13)
        Me.Label21.TabIndex = 133
        Me.Label21.Text = "Chq No"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(586, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 132
        Me.Label4.Text = "Chq Amount"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(246, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "To"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Chq From"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpChqTo
        '
        Me.dtpChqTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpChqTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChqTo.Location = New System.Drawing.Point(272, 34)
        Me.dtpChqTo.Name = "dtpChqTo"
        Me.dtpChqTo.ShowCheckBox = True
        Me.dtpChqTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpChqTo.TabIndex = 5
        '
        'dtpChqFrom
        '
        Me.dtpChqFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpChqFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChqFrom.Location = New System.Drawing.Point(94, 34)
        Me.dtpChqFrom.Name = "dtpChqFrom"
        Me.dtpChqFrom.ShowCheckBox = True
        Me.dtpChqFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpChqFrom.TabIndex = 3
        '
        'cboFile
        '
        Me.cboFile.FormattingEnabled = True
        Me.cboFile.Location = New System.Drawing.Point(479, 7)
        Me.cboFile.Name = "cboFile"
        Me.cboFile.Size = New System.Drawing.Size(294, 21)
        Me.cboFile.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(412, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "File Name"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(246, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "To"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(7, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Import  From"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpImportTo
        '
        Me.dtpImportTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpImportTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpImportTo.Location = New System.Drawing.Point(272, 7)
        Me.dtpImportTo.Name = "dtpImportTo"
        Me.dtpImportTo.ShowCheckBox = True
        Me.dtpImportTo.Size = New System.Drawing.Size(105, 21)
        Me.dtpImportTo.TabIndex = 1
        '
        'dtpImportFrom
        '
        Me.dtpImportFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpImportFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpImportFrom.Location = New System.Drawing.Point(94, 7)
        Me.dtpImportFrom.Name = "dtpImportFrom"
        Me.dtpImportFrom.ShowCheckBox = True
        Me.dtpImportFrom.Size = New System.Drawing.Size(105, 21)
        Me.dtpImportFrom.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(701, 88)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(623, 88)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(545, 88)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 10
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(479, 61)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(294, 21)
        Me.cboStatus.TabIndex = 9
        '
        'lbl1
        '
        Me.lbl1.Location = New System.Drawing.Point(388, 65)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(86, 13)
        Me.lbl1.TabIndex = 2
        Me.lbl1.Text = "Status"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.Location = New System.Drawing.Point(12, 141)
        Me.dgView.Name = "dgView"
        Me.dgView.ReadOnly = True
        Me.dgView.RowHeadersVisible = False
        Me.dgView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.dgView.Size = New System.Drawing.Size(823, 180)
        Me.dgView.TabIndex = 1
        '
        'lblRecCount
        '
        Me.lblRecCount.AutoSize = True
        Me.lblRecCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblRecCount.Location = New System.Drawing.Point(7, 8)
        Me.lblRecCount.Name = "lblRecCount"
        Me.lblRecCount.Size = New System.Drawing.Size(83, 13)
        Me.lblRecCount.TabIndex = 2
        Me.lblRecCount.Text = "Record Count"
        '
        'pnlDisplay
        '
        Me.pnlDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDisplay.Controls.Add(Me.lblRecCount)
        Me.pnlDisplay.Controls.Add(Me.btnExport)
        Me.pnlDisplay.Location = New System.Drawing.Point(12, 327)
        Me.pnlDisplay.Name = "pnlDisplay"
        Me.pnlDisplay.Size = New System.Drawing.Size(812, 32)
        Me.pnlDisplay.TabIndex = 2
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(725, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "E&xport"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'frmPostconvReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(885, 372)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.dgView)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPostconvReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Postconversion Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlButtons.PerformLayout()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDisplay.ResumeLayout(False)
        Me.pnlDisplay.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents lblRecCount As System.Windows.Forms.Label
    Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents cboFile As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpImportTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpImportFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpChqTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpChqFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAgrmntNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtChqNo As System.Windows.Forms.TextBox
    Friend WithEvents txtChqAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
