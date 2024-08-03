<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmpouchnotreceived
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtShortAgreementNo = New System.Windows.Forms.TextBox()
        Me.rbtcombined = New System.Windows.Forms.RadioButton()
        Me.rbtnotreceived = New System.Windows.Forms.RadioButton()
        Me.rbtpending = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbofilename = New System.Windows.Forms.ComboBox()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.dtpimportdate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbltotrec = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvsummary = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAgreementNo = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvsummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtAgreementNo)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtShortAgreementNo)
        Me.Panel1.Controls.Add(Me.rbtcombined)
        Me.Panel1.Controls.Add(Me.rbtnotreceived)
        Me.Panel1.Controls.Add(Me.rbtpending)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cbofilename)
        Me.Panel1.Controls.Add(Me.btnexport)
        Me.Panel1.Controls.Add(Me.btnclose)
        Me.Panel1.Controls.Add(Me.btnclear)
        Me.Panel1.Controls.Add(Me.btnrefresh)
        Me.Panel1.Controls.Add(Me.dtpimportdate)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(9, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(887, 69)
        Me.Panel1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Short Agreement No"
        '
        'txtShortAgreementNo
        '
        Me.txtShortAgreementNo.Location = New System.Drawing.Point(132, 33)
        Me.txtShortAgreementNo.Name = "txtShortAgreementNo"
        Me.txtShortAgreementNo.Size = New System.Drawing.Size(116, 21)
        Me.txtShortAgreementNo.TabIndex = 5
        '
        'rbtcombined
        '
        Me.rbtcombined.AutoSize = True
        Me.rbtcombined.Location = New System.Drawing.Point(793, 8)
        Me.rbtcombined.Name = "rbtcombined"
        Me.rbtcombined.Size = New System.Drawing.Size(81, 17)
        Me.rbtcombined.TabIndex = 4
        Me.rbtcombined.Text = "Combined"
        Me.rbtcombined.UseVisualStyleBackColor = True
        '
        'rbtnotreceived
        '
        Me.rbtnotreceived.AutoSize = True
        Me.rbtnotreceived.Location = New System.Drawing.Point(688, 8)
        Me.rbtnotreceived.Name = "rbtnotreceived"
        Me.rbtnotreceived.Size = New System.Drawing.Size(99, 17)
        Me.rbtnotreceived.TabIndex = 3
        Me.rbtnotreceived.Text = "Not Received"
        Me.rbtnotreceived.UseVisualStyleBackColor = True
        '
        'rbtpending
        '
        Me.rbtpending.AutoSize = True
        Me.rbtpending.Checked = True
        Me.rbtpending.Location = New System.Drawing.Point(607, 8)
        Me.rbtpending.Name = "rbtpending"
        Me.rbtpending.Size = New System.Drawing.Size(70, 17)
        Me.rbtpending.TabIndex = 2
        Me.rbtpending.TabStop = True
        Me.rbtpending.Text = "Pending"
        Me.rbtpending.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(284, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "File Name"
        '
        'cbofilename
        '
        Me.cbofilename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbofilename.FormattingEnabled = True
        Me.cbofilename.Location = New System.Drawing.Point(351, 6)
        Me.cbofilename.Name = "cbofilename"
        Me.cbofilename.Size = New System.Drawing.Size(214, 21)
        Me.cbofilename.TabIndex = 1
        '
        'btnexport
        '
        Me.btnexport.Location = New System.Drawing.Point(728, 30)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(72, 24)
        Me.btnexport.TabIndex = 9
        Me.btnexport.Text = "&Export"
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(806, 30)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(72, 24)
        Me.btnclose.TabIndex = 10
        Me.btnclose.Text = "E&xit"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnclear
        '
        Me.btnclear.Location = New System.Drawing.Point(650, 30)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(72, 24)
        Me.btnclear.TabIndex = 8
        Me.btnclear.Text = "C&lear"
        Me.btnclear.UseVisualStyleBackColor = True
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(572, 30)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(72, 24)
        Me.btnrefresh.TabIndex = 7
        Me.btnrefresh.Text = "&Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'dtpimportdate
        '
        Me.dtpimportdate.Checked = False
        Me.dtpimportdate.CustomFormat = "dd-MM-yyyy"
        Me.dtpimportdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpimportdate.Location = New System.Drawing.Point(132, 6)
        Me.dtpimportdate.Name = "dtpimportdate"
        Me.dtpimportdate.ShowCheckBox = True
        Me.dtpimportdate.Size = New System.Drawing.Size(116, 21)
        Me.dtpimportdate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Received Date"
        '
        'lbltotrec
        '
        Me.lbltotrec.AutoSize = True
        Me.lbltotrec.ForeColor = System.Drawing.Color.Green
        Me.lbltotrec.Location = New System.Drawing.Point(994, 20)
        Me.lbltotrec.Name = "lbltotrec"
        Me.lbltotrec.Size = New System.Drawing.Size(0, 13)
        Me.lbltotrec.TabIndex = 33
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.dgvsummary)
        Me.pnlMain.Location = New System.Drawing.Point(9, 93)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1316, 648)
        Me.pnlMain.TabIndex = 31
        '
        'dgvsummary
        '
        Me.dgvsummary.AllowUserToAddRows = False
        Me.dgvsummary.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgvsummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvsummary.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvsummary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvsummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvsummary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvsummary.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvsummary.Location = New System.Drawing.Point(6, 7)
        Me.dgvsummary.Name = "dgvsummary"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvsummary.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvsummary.RowHeadersVisible = False
        Me.dgvsummary.Size = New System.Drawing.Size(280, 150)
        Me.dgvsummary.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Green
        Me.Label7.Location = New System.Drawing.Point(994, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(0, 13)
        Me.Label7.TabIndex = 32
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(257, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Agreement No"
        '
        'txtAgreementNo
        '
        Me.txtAgreementNo.Location = New System.Drawing.Point(351, 33)
        Me.txtAgreementNo.Name = "txtAgreementNo"
        Me.txtAgreementNo.Size = New System.Drawing.Size(214, 21)
        Me.txtAgreementNo.TabIndex = 6
        '
        'frmpouchnotreceived
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(924, 343)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lbltotrec)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Label7)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmpouchnotreceived"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pouch Not Received"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvsummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents dtpimportdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbltotrec As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents dgvsummary As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents rbtnotreceived As System.Windows.Forms.RadioButton
    Friend WithEvents rbtpending As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbofilename As System.Windows.Forms.ComboBox
    Friend WithEvents rbtcombined As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtShortAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAgreementNo As System.Windows.Forms.TextBox
End Class
