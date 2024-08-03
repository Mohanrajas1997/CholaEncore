<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPacketBlockReport
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cboDisc = New System.Windows.Forms.ComboBox()
        Me.lbldisc = New System.Windows.Forms.Label()
        Me.cbopaymode = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtgnsarefno = New System.Windows.Forms.TextBox()
        Me.lblgnsaref = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpEntryTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpEntryFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtagreementno = New System.Windows.Forms.TextBox()
        Me.lblagreementno = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpPulloutTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpPulloutFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.lblRecCount = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvRpt = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpDelTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpDelFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtPktBlockNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.pnlDisplay.SuspendLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtPktBlockNo)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.dtpDelTo)
        Me.Panel1.Controls.Add(Me.dtpDelFrom)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.cboDisc)
        Me.Panel1.Controls.Add(Me.lbldisc)
        Me.Panel1.Controls.Add(Me.cbopaymode)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtgnsarefno)
        Me.Panel1.Controls.Add(Me.lblgnsaref)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtpEntryTo)
        Me.Panel1.Controls.Add(Me.dtpEntryFrom)
        Me.Panel1.Controls.Add(Me.txtagreementno)
        Me.Panel1.Controls.Add(Me.lblagreementno)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dtpPulloutTo)
        Me.Panel1.Controls.Add(Me.dtpPulloutFrom)
        Me.Panel1.Location = New System.Drawing.Point(6, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(843, 123)
        Me.Panel1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(751, 89)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(670, 89)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 13
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(589, 89)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 12
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
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
        Me.lbldisc.Location = New System.Drawing.Point(215, 92)
        Me.lbldisc.Name = "lbldisc"
        Me.lbldisc.Size = New System.Drawing.Size(72, 13)
        Me.lbldisc.TabIndex = 1
        Me.lbldisc.Text = "Pay Disc"
        Me.lbldisc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbopaymode
        '
        Me.cbopaymode.FormattingEnabled = True
        Me.cbopaymode.Items.AddRange(New Object() {"PDC", "SPDC"})
        Me.cbopaymode.Location = New System.Drawing.Point(98, 89)
        Me.cbopaymode.Name = "cbopaymode"
        Me.cbopaymode.Size = New System.Drawing.Size(111, 21)
        Me.cbopaymode.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 92)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Pay Mode"
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
        Me.lblgnsaref.Location = New System.Drawing.Point(457, 65)
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
        Me.Label4.Location = New System.Drawing.Point(3, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Pullout From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpPulloutTo
        '
        Me.dtpPulloutTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpPulloutTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPulloutTo.Location = New System.Drawing.Point(294, 36)
        Me.dtpPulloutTo.Name = "dtpPulloutTo"
        Me.dtpPulloutTo.ShowCheckBox = True
        Me.dtpPulloutTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpPulloutTo.TabIndex = 4
        '
        'dtpPulloutFrom
        '
        Me.dtpPulloutFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpPulloutFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPulloutFrom.Location = New System.Drawing.Point(98, 36)
        Me.dtpPulloutFrom.Name = "dtpPulloutFrom"
        Me.dtpPulloutFrom.ShowCheckBox = True
        Me.dtpPulloutFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpPulloutFrom.TabIndex = 3
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
        Me.dgvRpt.Location = New System.Drawing.Point(6, 136)
        Me.dgvRpt.Name = "dgvRpt"
        Me.dgvRpt.ReadOnly = True
        Me.dgvRpt.Size = New System.Drawing.Size(843, 210)
        Me.dgvRpt.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(250, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "To"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Delete From"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpDelTo
        '
        Me.dtpDelTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpDelTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDelTo.Location = New System.Drawing.Point(294, 62)
        Me.dtpDelTo.Name = "dtpDelTo"
        Me.dtpDelTo.ShowCheckBox = True
        Me.dtpDelTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpDelTo.TabIndex = 7
        '
        'dtpDelFrom
        '
        Me.dtpDelFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpDelFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDelFrom.Location = New System.Drawing.Point(98, 62)
        Me.dtpDelFrom.Name = "dtpDelFrom"
        Me.dtpDelFrom.ShowCheckBox = True
        Me.dtpDelFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpDelFrom.TabIndex = 6
        '
        'txtPktBlockNo
        '
        Me.txtPktBlockNo.Location = New System.Drawing.Point(533, 10)
        Me.txtPktBlockNo.Name = "txtPktBlockNo"
        Me.txtPktBlockNo.Size = New System.Drawing.Size(293, 21)
        Me.txtPktBlockNo.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(429, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(96, 13)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "Packet Block No"
        '
        'frmPacketBlockReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 395)
        Me.Controls.Add(Me.dgvRpt)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPacketBlockReport"
        Me.Text = "Packet Block Report"
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
    Friend WithEvents dtpPulloutTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPulloutFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtagreementno As System.Windows.Forms.TextBox
    Friend WithEvents lblagreementno As System.Windows.Forms.Label
    Friend WithEvents lblgnsaref As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpEntryTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEntryFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtgnsarefno As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbopaymode As System.Windows.Forms.ComboBox
    Friend WithEvents lbldisc As System.Windows.Forms.Label
    Friend WithEvents cboDisc As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents lblRecCount As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvRpt As System.Windows.Forms.DataGridView
    Friend WithEvents txtPktBlockNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpDelTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDelFrom As System.Windows.Forms.DateTimePicker
End Class
