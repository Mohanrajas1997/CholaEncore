<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRetrievalReport
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
        Me.cboFile = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpImpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpImpFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtWorkItemNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboDisc = New System.Windows.Forms.ComboBox()
        Me.lbldisc = New System.Windows.Forms.Label()
        Me.cboRetrieveMode = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtgnsarefno = New System.Windows.Forms.TextBox()
        Me.lblgnsaref = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpRtrTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpRtrFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtagreementno = New System.Windows.Forms.TextBox()
        Me.lblagreementno = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpReqTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpReqFrom = New System.Windows.Forms.DateTimePicker()
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
        Me.Panel1.Controls.Add(Me.cboFile)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.dtpImpTo)
        Me.Panel1.Controls.Add(Me.dtpImpFrom)
        Me.Panel1.Controls.Add(Me.txtReason)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtWorkItemNo)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.cboStatus)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.txtChqNo)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.cboDisc)
        Me.Panel1.Controls.Add(Me.lbldisc)
        Me.Panel1.Controls.Add(Me.cboRetrieveMode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtgnsarefno)
        Me.Panel1.Controls.Add(Me.lblgnsaref)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtpRtrTo)
        Me.Panel1.Controls.Add(Me.dtpRtrFrom)
        Me.Panel1.Controls.Add(Me.txtagreementno)
        Me.Panel1.Controls.Add(Me.lblagreementno)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dtpReqTo)
        Me.Panel1.Controls.Add(Me.dtpReqFrom)
        Me.Panel1.Location = New System.Drawing.Point(6, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(843, 176)
        Me.Panel1.TabIndex = 0
        '
        'cboFile
        '
        Me.cboFile.FormattingEnabled = True
        Me.cboFile.Location = New System.Drawing.Point(533, 10)
        Me.cboFile.Name = "cboFile"
        Me.cboFile.Size = New System.Drawing.Size(294, 21)
        Me.cboFile.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(466, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "File Name"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(266, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(21, 13)
        Me.Label10.TabIndex = 53
        Me.Label10.Text = "To"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(13, 13)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 13)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "Import  From"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpImpTo
        '
        Me.dtpImpTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpImpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpImpTo.Location = New System.Drawing.Point(294, 10)
        Me.dtpImpTo.Name = "dtpImpTo"
        Me.dtpImpTo.ShowCheckBox = True
        Me.dtpImpTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpImpTo.TabIndex = 1
        '
        'dtpImpFrom
        '
        Me.dtpImpFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpImpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpImpFrom.Location = New System.Drawing.Point(98, 10)
        Me.dtpImpFrom.Name = "dtpImpFrom"
        Me.dtpImpFrom.ShowCheckBox = True
        Me.dtpImpFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpImpFrom.TabIndex = 0
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(533, 116)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(293, 21)
        Me.txtReason.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(476, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Reason"
        '
        'txtWorkItemNo
        '
        Me.txtWorkItemNo.Location = New System.Drawing.Point(533, 89)
        Me.txtWorkItemNo.Name = "txtWorkItemNo"
        Me.txtWorkItemNo.Size = New System.Drawing.Size(111, 21)
        Me.txtWorkItemNo.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(440, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Work Item No"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(751, 143)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 17
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(670, 143)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 16
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(589, 143)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 15
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"Inward", "Authorized", "Rejected", "Packet Entry", "Packet Re-Entry", "Yet to Vault", "Vault", "Pullout ", "GNSA REF Changed", "AGRMT No Changed", "Retrieved"})
        Me.cboStatus.Location = New System.Drawing.Point(98, 116)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(307, 21)
        Me.cboStatus.TabIndex = 13
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(51, 120)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(44, 13)
        Me.lblStatus.TabIndex = 46
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqNo
        '
        Me.txtChqNo.Location = New System.Drawing.Point(715, 89)
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.Size = New System.Drawing.Size(111, 21)
        Me.txtChqNo.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(664, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Chq No"
        '
        'cboDisc
        '
        Me.cboDisc.FormattingEnabled = True
        Me.cboDisc.Items.AddRange(New Object() {"Yes", "No"})
        Me.cboDisc.Location = New System.Drawing.Point(294, 89)
        Me.cboDisc.Name = "cboDisc"
        Me.cboDisc.Size = New System.Drawing.Size(111, 21)
        Me.cboDisc.TabIndex = 10
        '
        'lbldisc
        '
        Me.lbldisc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldisc.Location = New System.Drawing.Point(215, 91)
        Me.lbldisc.Name = "lbldisc"
        Me.lbldisc.Size = New System.Drawing.Size(72, 13)
        Me.lbldisc.TabIndex = 1
        Me.lbldisc.Text = "Disc"
        Me.lbldisc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboRetrieveMode
        '
        Me.cboRetrieveMode.FormattingEnabled = True
        Me.cboRetrieveMode.Items.AddRange(New Object() {"PACKET", "PDC", "SPDC"})
        Me.cboRetrieveMode.Location = New System.Drawing.Point(98, 89)
        Me.cboRetrieveMode.Name = "cboRetrieveMode"
        Me.cboRetrieveMode.Size = New System.Drawing.Size(111, 21)
        Me.cboRetrieveMode.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Retrieve Mode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtgnsarefno
        '
        Me.txtgnsarefno.Location = New System.Drawing.Point(533, 62)
        Me.txtgnsarefno.Name = "txtgnsarefno"
        Me.txtgnsarefno.Size = New System.Drawing.Size(293, 21)
        Me.txtgnsarefno.TabIndex = 8
        '
        'lblgnsaref
        '
        Me.lblgnsaref.AutoSize = True
        Me.lblgnsaref.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgnsaref.Location = New System.Drawing.Point(457, 64)
        Me.lblgnsaref.Name = "lblgnsaref"
        Me.lblgnsaref.Size = New System.Drawing.Size(68, 13)
        Me.lblgnsaref.TabIndex = 37
        Me.lblgnsaref.Text = "GNSA Ref#"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(250, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "To"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-4, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Retrieved From"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpRtrTo
        '
        Me.dtpRtrTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpRtrTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRtrTo.Location = New System.Drawing.Point(294, 62)
        Me.dtpRtrTo.Name = "dtpRtrTo"
        Me.dtpRtrTo.ShowCheckBox = True
        Me.dtpRtrTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpRtrTo.TabIndex = 7
        '
        'dtpRtrFrom
        '
        Me.dtpRtrFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpRtrFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRtrFrom.Location = New System.Drawing.Point(98, 62)
        Me.dtpRtrFrom.Name = "dtpRtrFrom"
        Me.dtpRtrFrom.ShowCheckBox = True
        Me.dtpRtrFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpRtrFrom.TabIndex = 6
        '
        'txtagreementno
        '
        Me.txtagreementno.Location = New System.Drawing.Point(533, 36)
        Me.txtagreementno.Name = "txtagreementno"
        Me.txtagreementno.Size = New System.Drawing.Size(293, 21)
        Me.txtagreementno.TabIndex = 5
        '
        'lblagreementno
        '
        Me.lblagreementno.AutoSize = True
        Me.lblagreementno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblagreementno.Location = New System.Drawing.Point(437, 39)
        Me.lblagreementno.Name = "lblagreementno"
        Me.lblagreementno.Size = New System.Drawing.Size(88, 13)
        Me.lblagreementno.TabIndex = 25
        Me.lblagreementno.Text = "Agreement No"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(250, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Req From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpReqTo
        '
        Me.dtpReqTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpReqTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReqTo.Location = New System.Drawing.Point(294, 36)
        Me.dtpReqTo.Name = "dtpReqTo"
        Me.dtpReqTo.ShowCheckBox = True
        Me.dtpReqTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpReqTo.TabIndex = 4
        '
        'dtpReqFrom
        '
        Me.dtpReqFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpReqFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReqFrom.Location = New System.Drawing.Point(98, 36)
        Me.dtpReqFrom.Name = "dtpReqFrom"
        Me.dtpReqFrom.ShowCheckBox = True
        Me.dtpReqFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpReqFrom.TabIndex = 3
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
        Me.dgvRpt.Location = New System.Drawing.Point(6, 189)
        Me.dgvRpt.Name = "dgvRpt"
        Me.dgvRpt.ReadOnly = True
        Me.dgvRpt.Size = New System.Drawing.Size(843, 157)
        Me.dgvRpt.TabIndex = 1
        '
        'frmRetrievalReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 395)
        Me.Controls.Add(Me.dgvRpt)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmRetrievalReport"
        Me.Text = "Packet Report"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlDisplay.ResumeLayout(False)
        Me.pnlDisplay.PerformLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpReqTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpReqFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtagreementno As System.Windows.Forms.TextBox
    Friend WithEvents lblagreementno As System.Windows.Forms.Label
    Friend WithEvents lblgnsaref As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpRtrTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpRtrFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtgnsarefno As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboRetrieveMode As System.Windows.Forms.ComboBox
    Friend WithEvents lbldisc As System.Windows.Forms.Label
    Friend WithEvents cboDisc As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtChqNo As System.Windows.Forms.TextBox
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents lblRecCount As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvRpt As System.Windows.Forms.DataGridView
    Friend WithEvents txtWorkItemNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboFile As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpImpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpImpFrom As System.Windows.Forms.DateTimePicker
End Class
