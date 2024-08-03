<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSwapReport
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtOldPktSNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtgnsarefno = New System.Windows.Forms.TextBox()
        Me.lblgnsaref = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpEntryTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpEntryFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtagreementno = New System.Windows.Forms.TextBox()
        Me.lblagreementno = New System.Windows.Forms.Label()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.lblRecCount = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvRpt = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.pnlDisplay.SuspendLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtOldPktSNo)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.cboStatus)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.txtgnsarefno)
        Me.Panel1.Controls.Add(Me.lblgnsaref)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtpEntryTo)
        Me.Panel1.Controls.Add(Me.dtpEntryFrom)
        Me.Panel1.Controls.Add(Me.txtagreementno)
        Me.Panel1.Controls.Add(Me.lblagreementno)
        Me.Panel1.Location = New System.Drawing.Point(6, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(843, 97)
        Me.Panel1.TabIndex = 0
        '
        'txtOldPktSNo
        '
        Me.txtOldPktSNo.Location = New System.Drawing.Point(533, 10)
        Me.txtOldPktSNo.Name = "txtOldPktSNo"
        Me.txtOldPktSNo.Size = New System.Drawing.Size(293, 21)
        Me.txtOldPktSNo.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(434, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Old Packet SNo"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(751, 65)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(670, 65)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(589, 65)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"Inward", "Authorized", "Rejected", "Packet Entry", "Packet Re-Entry", "Yet to Vault", "Vault", "Pullout ", "GNSA REF Changed", "AGRMT No Changed", "Retrieved"})
        Me.cboStatus.Location = New System.Drawing.Point(98, 63)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(307, 21)
        Me.cboStatus.TabIndex = 5
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(46, 65)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(44, 13)
        Me.lblStatus.TabIndex = 46
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtgnsarefno
        '
        Me.txtgnsarefno.Location = New System.Drawing.Point(533, 36)
        Me.txtgnsarefno.Name = "txtgnsarefno"
        Me.txtgnsarefno.Size = New System.Drawing.Size(293, 21)
        Me.txtgnsarefno.TabIndex = 4
        '
        'lblgnsaref
        '
        Me.lblgnsaref.AutoSize = True
        Me.lblgnsaref.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgnsaref.Location = New System.Drawing.Point(457, 39)
        Me.lblgnsaref.Name = "lblgnsaref"
        Me.lblgnsaref.Size = New System.Drawing.Size(68, 13)
        Me.lblgnsaref.TabIndex = 37
        Me.lblgnsaref.Text = "GNSA Ref#"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(250, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "To"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Entry From"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpEntryTo
        '
        Me.dtpEntryTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpEntryTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEntryTo.Location = New System.Drawing.Point(294, 10)
        Me.dtpEntryTo.Name = "dtpEntryTo"
        Me.dtpEntryTo.ShowCheckBox = True
        Me.dtpEntryTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpEntryTo.TabIndex = 1
        '
        'dtpEntryFrom
        '
        Me.dtpEntryFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpEntryFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEntryFrom.Location = New System.Drawing.Point(98, 10)
        Me.dtpEntryFrom.Name = "dtpEntryFrom"
        Me.dtpEntryFrom.ShowCheckBox = True
        Me.dtpEntryFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpEntryFrom.TabIndex = 0
        '
        'txtagreementno
        '
        Me.txtagreementno.Location = New System.Drawing.Point(98, 36)
        Me.txtagreementno.Name = "txtagreementno"
        Me.txtagreementno.Size = New System.Drawing.Size(307, 21)
        Me.txtagreementno.TabIndex = 3
        '
        'lblagreementno
        '
        Me.lblagreementno.AutoSize = True
        Me.lblagreementno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblagreementno.Location = New System.Drawing.Point(2, 39)
        Me.lblagreementno.Name = "lblagreementno"
        Me.lblagreementno.Size = New System.Drawing.Size(88, 13)
        Me.lblagreementno.TabIndex = 25
        Me.lblagreementno.Text = "Agreement No"
        '
        'pnlDisplay
        '
        Me.pnlDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDisplay.Controls.Add(Me.lblRecCount)
        Me.pnlDisplay.Controls.Add(Me.btnExport)
        Me.pnlDisplay.Location = New System.Drawing.Point(6, 352)
        Me.pnlDisplay.Name = "pnlDisplay"
        Me.pnlDisplay.Size = New System.Drawing.Size(843, 32)
        Me.pnlDisplay.TabIndex = 3
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
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(746, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "E&xport"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'dgvRpt
        '
        Me.dgvRpt.AllowUserToAddRows = False
        Me.dgvRpt.AllowUserToDeleteRows = False
        Me.dgvRpt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRpt.Location = New System.Drawing.Point(6, 110)
        Me.dgvRpt.Name = "dgvRpt"
        Me.dgvRpt.ReadOnly = True
        Me.dgvRpt.Size = New System.Drawing.Size(843, 236)
        Me.dgvRpt.TabIndex = 1
        '
        'frmSwapReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 395)
        Me.Controls.Add(Me.dgvRpt)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSwapReport"
        Me.Text = "Swap Packet Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlDisplay.ResumeLayout(False)
        Me.pnlDisplay.PerformLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtagreementno As System.Windows.Forms.TextBox
    Friend WithEvents lblagreementno As System.Windows.Forms.Label
    Friend WithEvents lblgnsaref As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpEntryTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEntryFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtgnsarefno As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents lblRecCount As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvRpt As System.Windows.Forms.DataGridView
    Friend WithEvents txtOldPktSNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
