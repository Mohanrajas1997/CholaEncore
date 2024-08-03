<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPdcChqReport
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
        Me.cboQueue = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.cbostatus = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtboxno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboVault = New System.Windows.Forms.ComboBox()
        Me.lblvaulted = New System.Windows.Forms.Label()
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
        Me.dtpRcvdTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpRcvdFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.lblRecCount = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvRpt = New System.Windows.Forms.DataGridView()
        Me.txtchqno = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpChqDate = New System.Windows.Forms.DateTimePicker()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.pnlDisplay.SuspendLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.dtpChqDate)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtchqno)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.cboQueue)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.btnclose)
        Me.Panel1.Controls.Add(Me.btnclear)
        Me.Panel1.Controls.Add(Me.btnrefresh)
        Me.Panel1.Controls.Add(Me.cbostatus)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.txtboxno)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.cboVault)
        Me.Panel1.Controls.Add(Me.lblvaulted)
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
        Me.Panel1.Controls.Add(Me.dtpRcvdTo)
        Me.Panel1.Controls.Add(Me.dtpRcvdFrom)
        Me.Panel1.Location = New System.Drawing.Point(6, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(843, 220)
        Me.Panel1.TabIndex = 0
        '
        'cboQueue
        '
        Me.cboQueue.FormattingEnabled = True
        Me.cboQueue.Items.AddRange(New Object() {"Inward", "Authorized", "Rejected", "Packet Entry", "Packet Re-Entry", "Yet to Vault", "Vault", "Pullout ", "GNSA REF Changed", "AGRMT No Changed", "Retrieved"})
        Me.cboQueue.Location = New System.Drawing.Point(98, 153)
        Me.cboQueue.Name = "cboQueue"
        Me.cboQueue.Size = New System.Drawing.Size(307, 21)
        Me.cboQueue.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(46, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "Queue"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnclose.Location = New System.Drawing.Point(751, 181)
        Me.btnclose.Name = "btnClose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 14
        Me.btnclose.Text = "Close"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnclear.Location = New System.Drawing.Point(670, 181)
        Me.btnclear.Name = "btnClear"
        Me.btnclear.Size = New System.Drawing.Size(75, 23)
        Me.btnclear.TabIndex = 13
        Me.btnclear.Text = "Clear"
        Me.btnclear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(589, 181)
        Me.btnrefresh.Name = "btnRefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnrefresh.TabIndex = 12
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cbostatus.FormattingEnabled = True
        Me.cbostatus.Items.AddRange(New Object() {"Inward", "Authorized", "Rejected", "Packet Entry", "Packet Re-Entry", "Yet to Vault", "Vault", "Pullout ", "GNSA REF Changed", "AGRMT No Changed", "Retrieved"})
        Me.cbostatus.Location = New System.Drawing.Point(533, 153)
        Me.cbostatus.Name = "cboStatus"
        Me.cbostatus.Size = New System.Drawing.Size(293, 21)
        Me.cbostatus.TabIndex = 11
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(481, 155)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(44, 13)
        Me.lblStatus.TabIndex = 46
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtboxno
        '
        Me.txtboxno.Location = New System.Drawing.Point(707, 120)
        Me.txtboxno.Name = "txtboxno"
        Me.txtboxno.Size = New System.Drawing.Size(111, 21)
        Me.txtboxno.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(656, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Box No"
        '
        'cboVault
        '
        Me.cboVault.FormattingEnabled = True
        Me.cboVault.Items.AddRange(New Object() {"Yes", "No"})
        Me.cboVault.Location = New System.Drawing.Point(533, 126)
        Me.cboVault.Name = "cboVault"
        Me.cboVault.Size = New System.Drawing.Size(111, 21)
        Me.cboVault.TabIndex = 8
        '
        'lblvaulted
        '
        Me.lblvaulted.AutoSize = True
        Me.lblvaulted.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvaulted.Location = New System.Drawing.Point(475, 131)
        Me.lblvaulted.Name = "lblvaulted"
        Me.lblvaulted.Size = New System.Drawing.Size(50, 13)
        Me.lblvaulted.TabIndex = 42
        Me.lblvaulted.Text = "Vaulted"
        '
        'cboDisc
        '
        Me.cboDisc.FormattingEnabled = True
        Me.cboDisc.Items.AddRange(New Object() {"Yes", "No"})
        Me.cboDisc.Location = New System.Drawing.Point(294, 126)
        Me.cboDisc.Name = "cboDisc"
        Me.cboDisc.Size = New System.Drawing.Size(111, 21)
        Me.cboDisc.TabIndex = 7
        '
        'lbldisc
        '
        Me.lbldisc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldisc.Location = New System.Drawing.Point(215, 128)
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
        Me.cbopaymode.Location = New System.Drawing.Point(98, 126)
        Me.cbopaymode.Name = "cbopaymode"
        Me.cbopaymode.Size = New System.Drawing.Size(111, 21)
        Me.cbopaymode.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "Pay Mode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtgnsarefno
        '
        Me.txtgnsarefno.Location = New System.Drawing.Point(533, 36)
        Me.txtgnsarefno.Name = "txtgnsarefno"
        Me.txtgnsarefno.Size = New System.Drawing.Size(293, 21)
        Me.txtgnsarefno.TabIndex = 5
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
        Me.Label1.Location = New System.Drawing.Point(250, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "To"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 39)
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
        Me.dtpEntryTo.Location = New System.Drawing.Point(294, 36)
        Me.dtpEntryTo.Name = "dtpEntryTo"
        Me.dtpEntryTo.ShowCheckBox = True
        Me.dtpEntryTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpEntryTo.TabIndex = 4
        '
        'dtpEntryFrom
        '
        Me.dtpEntryFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpEntryFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEntryFrom.Location = New System.Drawing.Point(98, 36)
        Me.dtpEntryFrom.Name = "dtpEntryFrom"
        Me.dtpEntryFrom.ShowCheckBox = True
        Me.dtpEntryFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpEntryFrom.TabIndex = 3
        '
        'txtagreementno
        '
        Me.txtagreementno.Location = New System.Drawing.Point(533, 10)
        Me.txtagreementno.Name = "txtagreementno"
        Me.txtagreementno.Size = New System.Drawing.Size(293, 21)
        Me.txtagreementno.TabIndex = 2
        '
        'lblagreementno
        '
        Me.lblagreementno.AutoSize = True
        Me.lblagreementno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblagreementno.Location = New System.Drawing.Point(437, 13)
        Me.lblagreementno.Name = "lblagreementno"
        Me.lblagreementno.Size = New System.Drawing.Size(88, 13)
        Me.lblagreementno.TabIndex = 25
        Me.lblagreementno.Text = "Agreement No"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(250, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Rcvd From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpRcvdTo
        '
        Me.dtpRcvdTo.CustomFormat = "dd-MM-yyyy"
        Me.dtpRcvdTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRcvdTo.Location = New System.Drawing.Point(294, 10)
        Me.dtpRcvdTo.Name = "dtpRcvdTo"
        Me.dtpRcvdTo.ShowCheckBox = True
        Me.dtpRcvdTo.Size = New System.Drawing.Size(111, 21)
        Me.dtpRcvdTo.TabIndex = 1
        '
        'dtpRcvdFrom
        '
        Me.dtpRcvdFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpRcvdFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRcvdFrom.Location = New System.Drawing.Point(98, 10)
        Me.dtpRcvdFrom.Name = "dtpRcvdFrom"
        Me.dtpRcvdFrom.ShowCheckBox = True
        Me.dtpRcvdFrom.Size = New System.Drawing.Size(111, 21)
        Me.dtpRcvdFrom.TabIndex = 0
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
        Me.dgvRpt.Location = New System.Drawing.Point(6, 269)
        Me.dgvRpt.Name = "dgvRpt"
        Me.dgvRpt.Size = New System.Drawing.Size(843, 77)
        Me.dgvRpt.TabIndex = 1
        '
        'txtChqNo
        '
        Me.txtchqno.Location = New System.Drawing.Point(533, 63)
        Me.txtchqno.Name = "txtChqNo"
        Me.txtchqno.Size = New System.Drawing.Size(111, 21)
        Me.txtchqno.TabIndex = 52
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(480, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Chq No"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(715, 63)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(111, 21)
        Me.TextBox2.TabIndex = 54
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(650, 66)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Amount"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 66)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 57
        Me.Label10.Text = "Cheque Date"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpChqDate
        '
        Me.dtpChqDate.CustomFormat = "dd-MM-yyyy"
        Me.dtpChqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChqDate.Location = New System.Drawing.Point(98, 63)
        Me.dtpChqDate.Name = "dtpChqDate"
        Me.dtpChqDate.ShowCheckBox = True
        Me.dtpChqDate.Size = New System.Drawing.Size(111, 21)
        Me.dtpChqDate.TabIndex = 56
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(294, 63)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(111, 21)
        Me.TextBox1.TabIndex = 58
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(241, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 59
        Me.Label11.Text = "Chq No"
        '
        'frmPdcReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 395)
        Me.Controls.Add(Me.dgvRpt)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPdcReport"
        Me.Text = "Pdc Report"
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
    Friend WithEvents dtpRcvdTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpRcvdFrom As System.Windows.Forms.DateTimePicker
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
    Friend WithEvents lblvaulted As System.Windows.Forms.Label
    Friend WithEvents cboVault As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtboxno As System.Windows.Forms.TextBox
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents lblRecCount As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents dgvRpt As System.Windows.Forms.DataGridView
    Friend WithEvents cboQueue As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtpChqDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtChqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
