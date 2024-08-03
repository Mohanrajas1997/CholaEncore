<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUndoPullout
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
        Me.chkChqQry = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dtpClosedTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpClosedFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtPulloutId = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPdcId = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpEntryTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpEntryFrom = New System.Windows.Forms.DateTimePicker()
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
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dgvRpt = New System.Windows.Forms.DataGridView()
        Me.lblRecCount = New System.Windows.Forms.Label()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.txtShortAgreementNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlButtons.SuspendLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDisplay.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlButtons.Controls.Add(Me.txtShortAgreementNo)
        Me.pnlButtons.Controls.Add(Me.chkChqQry)
        Me.pnlButtons.Controls.Add(Me.Label16)
        Me.pnlButtons.Controls.Add(Me.Label17)
        Me.pnlButtons.Controls.Add(Me.dtpClosedTo)
        Me.pnlButtons.Controls.Add(Me.dtpClosedFrom)
        Me.pnlButtons.Controls.Add(Me.txtPulloutId)
        Me.pnlButtons.Controls.Add(Me.Label15)
        Me.pnlButtons.Controls.Add(Me.txtPdcId)
        Me.pnlButtons.Controls.Add(Me.Label6)
        Me.pnlButtons.Controls.Add(Me.Label7)
        Me.pnlButtons.Controls.Add(Me.dtpEntryTo)
        Me.pnlButtons.Controls.Add(Me.dtpEntryFrom)
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
        Me.pnlButtons.Controls.Add(Me.Label13)
        Me.pnlButtons.Controls.Add(Me.Label2)
        Me.pnlButtons.Location = New System.Drawing.Point(12, 12)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(861, 119)
        Me.pnlButtons.TabIndex = 0
        '
        'chkChqQry
        '
        Me.chkChqQry.AutoSize = True
        Me.chkChqQry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.chkChqQry.Location = New System.Drawing.Point(519, 89)
        Me.chkChqQry.Name = "chkChqQry"
        Me.chkChqQry.Size = New System.Drawing.Size(84, 17)
        Me.chkChqQry.TabIndex = 13
        Me.chkChqQry.Text = "Chq Query"
        Me.chkChqQry.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(276, 62)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(21, 13)
        Me.Label16.TabIndex = 60
        Me.Label16.Text = "To"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(10, 62)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 13)
        Me.Label17.TabIndex = 59
        Me.Label17.Text = "Closed  From"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpClosedTo
        '
        Me.dtpClosedTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpClosedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpClosedTo.Location = New System.Drawing.Point(303, 59)
        Me.dtpClosedTo.Name = "dtpClosedTo"
        Me.dtpClosedTo.ShowCheckBox = True
        Me.dtpClosedTo.Size = New System.Drawing.Size(116, 21)
        Me.dtpClosedTo.TabIndex = 9
        '
        'dtpClosedFrom
        '
        Me.dtpClosedFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpClosedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpClosedFrom.Location = New System.Drawing.Point(96, 59)
        Me.dtpClosedFrom.Name = "dtpClosedFrom"
        Me.dtpClosedFrom.ShowCheckBox = True
        Me.dtpClosedFrom.Size = New System.Drawing.Size(116, 21)
        Me.dtpClosedFrom.TabIndex = 8
        '
        'txtPulloutId
        '
        Me.txtPulloutId.Location = New System.Drawing.Point(96, 86)
        Me.txtPulloutId.MaxLength = 8
        Me.txtPulloutId.Name = "txtPulloutId"
        Me.txtPulloutId.Size = New System.Drawing.Size(116, 21)
        Me.txtPulloutId.TabIndex = 11
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(5, 89)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(85, 13)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "Pullout Id"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPdcId
        '
        Me.txtPdcId.Location = New System.Drawing.Point(732, 32)
        Me.txtPdcId.MaxLength = 8
        Me.txtPdcId.Name = "txtPdcId"
        Me.txtPdcId.Size = New System.Drawing.Size(116, 21)
        Me.txtPdcId.TabIndex = 7
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
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Entry  From"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpEntryTo
        '
        Me.dtpEntryTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpEntryTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEntryTo.Location = New System.Drawing.Point(303, 5)
        Me.dtpEntryTo.Name = "dtpEntryTo"
        Me.dtpEntryTo.ShowCheckBox = True
        Me.dtpEntryTo.Size = New System.Drawing.Size(116, 21)
        Me.dtpEntryTo.TabIndex = 1
        '
        'dtpEntryFrom
        '
        Me.dtpEntryFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpEntryFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEntryFrom.Location = New System.Drawing.Point(96, 5)
        Me.dtpEntryFrom.Name = "dtpEntryFrom"
        Me.dtpEntryFrom.ShowCheckBox = True
        Me.dtpEntryFrom.Size = New System.Drawing.Size(116, 21)
        Me.dtpEntryFrom.TabIndex = 0
        '
        'txtAgmtNo
        '
        Me.txtAgmtNo.Location = New System.Drawing.Point(732, 5)
        Me.txtAgmtNo.MaxLength = 255
        Me.txtAgmtNo.Name = "txtAgmtNo"
        Me.txtAgmtNo.Size = New System.Drawing.Size(116, 21)
        Me.txtAgmtNo.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(638, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Agreement No"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqAmt
        '
        Me.txtChqAmt.Location = New System.Drawing.Point(519, 32)
        Me.txtChqAmt.MaxLength = 15
        Me.txtChqAmt.Name = "txtChqAmt"
        Me.txtChqAmt.Size = New System.Drawing.Size(116, 21)
        Me.txtChqAmt.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(430, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Chq Amt"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqNo
        '
        Me.txtChqNo.Location = New System.Drawing.Point(519, 5)
        Me.txtChqNo.MaxLength = 8
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.Size = New System.Drawing.Size(116, 21)
        Me.txtChqNo.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(448, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 13)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Chq No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(276, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(10, 35)
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
        Me.dtpChqTo.Location = New System.Drawing.Point(303, 32)
        Me.dtpChqTo.Name = "dtpChqTo"
        Me.dtpChqTo.ShowCheckBox = True
        Me.dtpChqTo.Size = New System.Drawing.Size(116, 21)
        Me.dtpChqTo.TabIndex = 5
        '
        'dtpChqFrom
        '
        Me.dtpChqFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpChqFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChqFrom.Location = New System.Drawing.Point(96, 32)
        Me.dtpChqFrom.Name = "dtpChqFrom"
        Me.dtpChqFrom.ShowCheckBox = True
        Me.dtpChqFrom.Size = New System.Drawing.Size(116, 21)
        Me.dtpChqFrom.TabIndex = 4
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(776, 86)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(698, 86)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 15
        Me.btnClear.Text = "C&lear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(620, 86)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'cboReason
        '
        Me.cboReason.FormattingEnabled = True
        Me.cboReason.Location = New System.Drawing.Point(519, 59)
        Me.cboReason.Name = "cboReason"
        Me.cboReason.Size = New System.Drawing.Size(329, 21)
        Me.cboReason.TabIndex = 10
        '
        'lbl1
        '
        Me.lbl1.Location = New System.Drawing.Point(429, 62)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(88, 13)
        Me.lbl1.TabIndex = 2
        Me.lbl1.Text = "Reason"
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(658, 35)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "PDC Id"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvRpt
        '
        Me.dgvRpt.AllowUserToAddRows = False
        Me.dgvRpt.Location = New System.Drawing.Point(12, 137)
        Me.dgvRpt.Name = "dgvRpt"
        Me.dgvRpt.RowHeadersVisible = False
        Me.dgvRpt.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.dgvRpt.Size = New System.Drawing.Size(861, 168)
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
        'txtShortAgreementNo
        '
        Me.txtShortAgreementNo.Location = New System.Drawing.Point(303, 86)
        Me.txtShortAgreementNo.MaxLength = 255
        Me.txtShortAgreementNo.Name = "txtShortAgreementNo"
        Me.txtShortAgreementNo.Size = New System.Drawing.Size(116, 21)
        Me.txtShortAgreementNo.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(209, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "Short Agr No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmUndoPullout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(915, 372)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.dgvRpt)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmUndoPullout"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Remove Pullout"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlButtons.PerformLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDisplay.ResumeLayout(False)
        Me.pnlDisplay.PerformLayout()
        Me.ResumeLayout(False)

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
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpEntryTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEntryFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboReason As System.Windows.Forms.ComboBox
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents txtPdcId As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPulloutId As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents dtpClosedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpClosedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkChqQry As System.Windows.Forms.CheckBox
    Friend WithEvents txtShortAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
