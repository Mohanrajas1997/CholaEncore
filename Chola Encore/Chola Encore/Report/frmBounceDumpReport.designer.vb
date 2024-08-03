<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBounceDumpReport
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpRtnTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpRtnFrom = New System.Windows.Forms.DateTimePicker()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtAWBNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtBounceId = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboFile = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpImpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpImpFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtAgmtNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtChqAmt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpChqTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpChqFrom = New System.Windows.Forms.DateTimePicker()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cboReason = New System.Windows.Forms.ComboBox()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.dgvRpt = New System.Windows.Forms.DataGridView()
        Me.lblRecCount = New System.Windows.Forms.Label()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlButtons.SuspendLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDisplay.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlButtons.Controls.Add(Me.Label2)
        Me.pnlButtons.Controls.Add(Me.Label14)
        Me.pnlButtons.Controls.Add(Me.dtpRtnTo)
        Me.pnlButtons.Controls.Add(Me.dtpRtnFrom)
        Me.pnlButtons.Controls.Add(Me.cboStatus)
        Me.pnlButtons.Controls.Add(Me.Label12)
        Me.pnlButtons.Controls.Add(Me.txtAWBNo)
        Me.pnlButtons.Controls.Add(Me.Label10)
        Me.pnlButtons.Controls.Add(Me.txtBounceId)
        Me.pnlButtons.Controls.Add(Me.Label11)
        Me.pnlButtons.Controls.Add(Me.cboFile)
        Me.pnlButtons.Controls.Add(Me.Label9)
        Me.pnlButtons.Controls.Add(Me.Label6)
        Me.pnlButtons.Controls.Add(Me.Label7)
        Me.pnlButtons.Controls.Add(Me.dtpImpTo)
        Me.pnlButtons.Controls.Add(Me.dtpImpFrom)
        Me.pnlButtons.Controls.Add(Me.txtAgmtNo)
        Me.pnlButtons.Controls.Add(Me.Label5)
        Me.pnlButtons.Controls.Add(Me.txtChqAmt)
        Me.pnlButtons.Controls.Add(Me.Label1)
        Me.pnlButtons.Controls.Add(Me.txtChqNo)
        Me.pnlButtons.Controls.Add(Me.Label8)
        Me.pnlButtons.Controls.Add(Me.Label3)
        Me.pnlButtons.Controls.Add(Me.Label4)
        Me.pnlButtons.Controls.Add(Me.dtpChqTo)
        Me.pnlButtons.Controls.Add(Me.dtpChqFrom)
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnClear)
        Me.pnlButtons.Controls.Add(Me.btnRefresh)
        Me.pnlButtons.Controls.Add(Me.cboReason)
        Me.pnlButtons.Controls.Add(Me.lbl1)
        Me.pnlButtons.Location = New System.Drawing.Point(12, 12)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(861, 151)
        Me.pnlButtons.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(276, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "To"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(8, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 49
        Me.Label14.Text = "Return From"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpRtnTo
        '
        Me.dtpRtnTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpRtnTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRtnTo.Location = New System.Drawing.Point(303, 32)
        Me.dtpRtnTo.Name = "dtpRtnTo"
        Me.dtpRtnTo.ShowCheckBox = True
        Me.dtpRtnTo.Size = New System.Drawing.Size(116, 21)
        Me.dtpRtnTo.TabIndex = 4
        '
        'dtpRtnFrom
        '
        Me.dtpRtnFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpRtnFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRtnFrom.Location = New System.Drawing.Point(96, 32)
        Me.dtpRtnFrom.Name = "dtpRtnFrom"
        Me.dtpRtnFrom.ShowCheckBox = True
        Me.dtpRtnFrom.Size = New System.Drawing.Size(116, 21)
        Me.dtpRtnFrom.TabIndex = 3
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(96, 113)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(323, 21)
        Me.cboStatus.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(46, 117)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 13)
        Me.Label12.TabIndex = 45
        Me.Label12.Text = "Status"
        '
        'txtAWBNo
        '
        Me.txtAWBNo.Location = New System.Drawing.Point(96, 86)
        Me.txtAWBNo.MaxLength = 15
        Me.txtAWBNo.Name = "txtAWBNo"
        Me.txtAWBNo.Size = New System.Drawing.Size(116, 21)
        Me.txtAWBNo.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(3, 89)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "AWB No"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBounceId
        '
        Me.txtBounceId.Location = New System.Drawing.Point(303, 86)
        Me.txtBounceId.MaxLength = 8
        Me.txtBounceId.Name = "txtBounceId"
        Me.txtBounceId.Size = New System.Drawing.Size(116, 21)
        Me.txtBounceId.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(228, 89)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 13)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Bounce Id"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboFile
        '
        Me.cboFile.FormattingEnabled = True
        Me.cboFile.Location = New System.Drawing.Point(519, 5)
        Me.cboFile.Name = "cboFile"
        Me.cboFile.Size = New System.Drawing.Size(329, 21)
        Me.cboFile.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(452, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "File Name"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(276, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(21, 13)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "To"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Import  From"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpImpTo
        '
        Me.dtpImpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpImpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpImpTo.Location = New System.Drawing.Point(303, 5)
        Me.dtpImpTo.Name = "dtpImpTo"
        Me.dtpImpTo.ShowCheckBox = True
        Me.dtpImpTo.Size = New System.Drawing.Size(116, 21)
        Me.dtpImpTo.TabIndex = 1
        '
        'dtpImpFrom
        '
        Me.dtpImpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpImpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpImpFrom.Location = New System.Drawing.Point(96, 5)
        Me.dtpImpFrom.Name = "dtpImpFrom"
        Me.dtpImpFrom.ShowCheckBox = True
        Me.dtpImpFrom.Size = New System.Drawing.Size(116, 21)
        Me.dtpImpFrom.TabIndex = 0
        '
        'txtAgmtNo
        '
        Me.txtAgmtNo.Location = New System.Drawing.Point(519, 86)
        Me.txtAgmtNo.MaxLength = 255
        Me.txtAgmtNo.Name = "txtAgmtNo"
        Me.txtAgmtNo.Size = New System.Drawing.Size(329, 21)
        Me.txtAgmtNo.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(425, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Agreement No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqAmt
        '
        Me.txtChqAmt.Location = New System.Drawing.Point(519, 59)
        Me.txtChqAmt.MaxLength = 15
        Me.txtChqAmt.Name = "txtChqAmt"
        Me.txtChqAmt.Size = New System.Drawing.Size(116, 21)
        Me.txtChqAmt.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(426, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Chq Amt"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqNo
        '
        Me.txtChqNo.Location = New System.Drawing.Point(732, 59)
        Me.txtChqNo.MaxLength = 8
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.Size = New System.Drawing.Size(116, 21)
        Me.txtChqNo.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(657, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 13)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Chq No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(276, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(10, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Chq  From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpChqTo
        '
        Me.dtpChqTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpChqTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChqTo.Location = New System.Drawing.Point(303, 59)
        Me.dtpChqTo.Name = "dtpChqTo"
        Me.dtpChqTo.ShowCheckBox = True
        Me.dtpChqTo.Size = New System.Drawing.Size(116, 21)
        Me.dtpChqTo.TabIndex = 7
        '
        'dtpChqFrom
        '
        Me.dtpChqFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpChqFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChqFrom.Location = New System.Drawing.Point(96, 59)
        Me.dtpChqFrom.Name = "dtpChqFrom"
        Me.dtpChqFrom.ShowCheckBox = True
        Me.dtpChqFrom.Size = New System.Drawing.Size(116, 21)
        Me.dtpChqFrom.TabIndex = 6
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(776, 115)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(698, 115)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 15
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(620, 115)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'cboReason
        '
        Me.cboReason.FormattingEnabled = True
        Me.cboReason.Location = New System.Drawing.Point(519, 32)
        Me.cboReason.Name = "cboReason"
        Me.cboReason.Size = New System.Drawing.Size(329, 21)
        Me.cboReason.TabIndex = 5
        '
        'lbl1
        '
        Me.lbl1.Location = New System.Drawing.Point(425, 35)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(88, 13)
        Me.lbl1.TabIndex = 2
        Me.lbl1.Text = "Reason"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvRpt
        '
        Me.dgvRpt.AllowUserToAddRows = False
        Me.dgvRpt.Location = New System.Drawing.Point(12, 169)
        Me.dgvRpt.Name = "dgvRpt"
        Me.dgvRpt.ReadOnly = True
        Me.dgvRpt.RowHeadersVisible = False
        Me.dgvRpt.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.dgvRpt.Size = New System.Drawing.Size(861, 136)
        Me.dgvRpt.TabIndex = 1
        '
        'lblRecCount
        '
        Me.lblRecCount.AutoSize = True
        Me.lblRecCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblRecCount.Location = New System.Drawing.Point(7, 8)
        Me.lblRecCount.Name = "lblRecCount"
        Me.lblRecCount.Size = New System.Drawing.Size(83, 13)
        Me.lblRecCount.TabIndex = 0
        Me.lblRecCount.Text = "Record Count"
        '
        'pnlDisplay
        '
        Me.pnlDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDisplay.Controls.Add(Me.lblRecCount)
        Me.pnlDisplay.Controls.Add(Me.btnExport)
        Me.pnlDisplay.Location = New System.Drawing.Point(12, 327)
        Me.pnlDisplay.Name = "pnlDisplay"
        Me.pnlDisplay.Size = New System.Drawing.Size(861, 32)
        Me.pnlDisplay.TabIndex = 2
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(778, 2)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "E&xport"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'frmBounceDumpReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(915, 372)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.dgvRpt)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmBounceDumpReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bounce Dump Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlButtons.PerformLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDisplay.ResumeLayout(False)
        Me.pnlDisplay.PerformLayout()
    End Sub

    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents dgvRpt As System.Windows.Forms.DataGridView
    Friend WithEvents lblRecCount As System.Windows.Forms.Label
    Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpChqTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpChqFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtChqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtChqAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAgmtNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboFile As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpImpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpImpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAWBNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtBounceId As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpRtnTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpRtnFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboReason As System.Windows.Forms.ComboBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
End Class
