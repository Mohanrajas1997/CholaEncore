<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockMisNew
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
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.cboCtsStatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cbopaymode = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtgnsarefno = New System.Windows.Forms.TextBox()
        Me.lblgnsaref = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpRcvdTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpRcvdFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.lblRecCount = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvstockmis = New System.Windows.Forms.DataGridView()
        Me.pnlButtons.SuspendLayout()
        Me.pnlDisplay.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvstockmis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlButtons.Controls.Add(Me.cboCtsStatus)
        Me.pnlButtons.Controls.Add(Me.Label1)
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnClear)
        Me.pnlButtons.Controls.Add(Me.btnRefresh)
        Me.pnlButtons.Controls.Add(Me.cbopaymode)
        Me.pnlButtons.Controls.Add(Me.Label5)
        Me.pnlButtons.Controls.Add(Me.txtgnsarefno)
        Me.pnlButtons.Controls.Add(Me.lblgnsaref)
        Me.pnlButtons.Controls.Add(Me.Label3)
        Me.pnlButtons.Controls.Add(Me.Label4)
        Me.pnlButtons.Controls.Add(Me.dtpRcvdTo)
        Me.pnlButtons.Controls.Add(Me.dtpRcvdFrom)
        Me.pnlButtons.Location = New System.Drawing.Point(10, 7)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(517, 92)
        Me.pnlButtons.TabIndex = 0
        '
        'cboCtsStatus
        '
        Me.cboCtsStatus.FormattingEnabled = True
        Me.cboCtsStatus.Items.AddRange(New Object() {"PDC", "SPDC", "SPDC HEADER"})
        Me.cboCtsStatus.Location = New System.Drawing.Point(79, 57)
        Me.cboCtsStatus.Name = "cboCtsStatus"
        Me.cboCtsStatus.Size = New System.Drawing.Size(165, 21)
        Me.cboCtsStatus.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(-17, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "CTS Status"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(426, 57)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(345, 57)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(264, 57)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'cbopaymode
        '
        Me.cbopaymode.FormattingEnabled = True
        Me.cbopaymode.Items.AddRange(New Object() {"PDC", "PDC INWARD", "PDC SUMMARY", "PDC RCVD TOTAL", "PDC INWARD TOTAL", "PDC RCVD SUMMARY", "PDC INWARD SUMMARY", "PDC DETAIL", "SPDC", "SPDC INWARD", "SPDC SUMMARY", "SPDC INWARD SUMMARY", "SPDC RCVD TOTAL", "SPDC INWARD TOTAL", "PDC CURRENT STOCK", "SPDC CURRENT STOCK", "SPDC HEADER"})
        Me.cbopaymode.Location = New System.Drawing.Point(341, 30)
        Me.cbopaymode.Name = "cbopaymode"
        Me.cbopaymode.Size = New System.Drawing.Size(160, 21)
        Me.cbopaymode.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(248, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Pay Mode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtgnsarefno
        '
        Me.txtgnsarefno.Location = New System.Drawing.Point(79, 30)
        Me.txtgnsarefno.Name = "txtgnsarefno"
        Me.txtgnsarefno.Size = New System.Drawing.Size(165, 21)
        Me.txtgnsarefno.TabIndex = 2
        '
        'lblgnsaref
        '
        Me.lblgnsaref.AutoSize = True
        Me.lblgnsaref.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgnsaref.Location = New System.Drawing.Point(2, 34)
        Me.lblgnsaref.Name = "lblgnsaref"
        Me.lblgnsaref.Size = New System.Drawing.Size(68, 13)
        Me.lblgnsaref.TabIndex = 39
        Me.lblgnsaref.Text = "GNSA Ref#"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(298, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "To"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(-18, 7)
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
        Me.dtpRcvdTo.Location = New System.Drawing.Point(342, 4)
        Me.dtpRcvdTo.Name = "dtpRcvdTo"
        Me.dtpRcvdTo.ShowCheckBox = True
        Me.dtpRcvdTo.Size = New System.Drawing.Size(159, 21)
        Me.dtpRcvdTo.TabIndex = 1
        '
        'dtpRcvdFrom
        '
        Me.dtpRcvdFrom.CustomFormat = "dd-MM-yyyy"
        Me.dtpRcvdFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRcvdFrom.Location = New System.Drawing.Point(79, 4)
        Me.dtpRcvdFrom.Name = "dtpRcvdFrom"
        Me.dtpRcvdFrom.ShowCheckBox = True
        Me.dtpRcvdFrom.Size = New System.Drawing.Size(165, 21)
        Me.dtpRcvdFrom.TabIndex = 0
        '
        'pnlDisplay
        '
        Me.pnlDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDisplay.Controls.Add(Me.lblRecCount)
        Me.pnlDisplay.Controls.Add(Me.btnExport)
        Me.pnlDisplay.Location = New System.Drawing.Point(8, 488)
        Me.pnlDisplay.Name = "pnlDisplay"
        Me.pnlDisplay.Size = New System.Drawing.Size(928, 32)
        Me.pnlDisplay.TabIndex = 2
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
        Me.btnExport.Location = New System.Drawing.Point(811, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 1
        Me.btnExport.Text = "E&xport"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.dgvstockmis)
        Me.pnlMain.Location = New System.Drawing.Point(5, 105)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(931, 376)
        Me.pnlMain.TabIndex = 1
        '
        'dgvstockmis
        '
        Me.dgvstockmis.AllowUserToAddRows = False
        Me.dgvstockmis.AllowUserToDeleteRows = False
        Me.dgvstockmis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvstockmis.Location = New System.Drawing.Point(2, 8)
        Me.dgvstockmis.Name = "dgvstockmis"
        Me.dgvstockmis.ReadOnly = True
        Me.dgvstockmis.Size = New System.Drawing.Size(926, 360)
        Me.dgvstockmis.TabIndex = 2
        '
        'frmStockMisNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(948, 533)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlDisplay)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmStockMisNew"
        Me.Text = "Stock Report (New)"
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlButtons.PerformLayout()
        Me.pnlDisplay.ResumeLayout(False)
        Me.pnlDisplay.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvstockmis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpRcvdTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpRcvdFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtgnsarefno As System.Windows.Forms.TextBox
    Friend WithEvents lblgnsaref As System.Windows.Forms.Label
    Friend WithEvents cbopaymode As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
    Friend WithEvents lblRecCount As System.Windows.Forms.Label
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents dgvstockmis As System.Windows.Forms.DataGridView
    Friend WithEvents cboCtsStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
