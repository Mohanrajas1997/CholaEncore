<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmpdcreentry
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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblrefno = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblmode = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblname = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblAgreement = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GbEntry = New System.Windows.Forms.GroupBox()
        Me.txtrowid = New System.Windows.Forms.TextBox()
        Me.cbopopnonpop = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.txtecsamount = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtchqcount = New System.Windows.Forms.TextBox()
        Me.txtchqamount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mstchqdate = New System.Windows.Forms.MaskedTextBox()
        Me.lblecsamount = New System.Windows.Forms.Label()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.lblchqdate = New System.Windows.Forms.Label()
        Me.cbotype = New System.Windows.Forms.ComboBox()
        Me.txtecscount = New System.Windows.Forms.TextBox()
        Me.txtchqno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblecscount = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lstcontractnos = New System.Windows.Forms.ListBox()
        Me.dgventry = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GbEntry.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgventry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GbEntry)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.dgventry)
        Me.GroupBox1.Location = New System.Drawing.Point(4, -3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(675, 403)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblrefno)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.lblmode)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.lblname)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.lblAgreement)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 9)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(657, 40)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'lblrefno
        '
        Me.lblrefno.AutoSize = True
        Me.lblrefno.ForeColor = System.Drawing.Color.Crimson
        Me.lblrefno.Location = New System.Drawing.Point(256, 16)
        Me.lblrefno.Name = "lblrefno"
        Me.lblrefno.Size = New System.Drawing.Size(58, 13)
        Me.lblrefno.TabIndex = 7
        Me.lblrefno.Text = "GNSAREF"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(230, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "REF:"
        '
        'lblmode
        '
        Me.lblmode.AutoSize = True
        Me.lblmode.ForeColor = System.Drawing.Color.Crimson
        Me.lblmode.Location = New System.Drawing.Point(188, 16)
        Me.lblmode.Name = "lblmode"
        Me.lblmode.Size = New System.Drawing.Size(34, 13)
        Me.lblmode.TabIndex = 5
        Me.lblmode.Text = "Mode"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(155, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Mode:"
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.ForeColor = System.Drawing.Color.Crimson
        Me.lblname.Location = New System.Drawing.Point(417, 16)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(35, 13)
        Me.lblname.TabIndex = 3
        Me.lblname.Text = "Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(339, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Drawee Name:"
        '
        'lblAgreement
        '
        Me.lblAgreement.AutoSize = True
        Me.lblAgreement.ForeColor = System.Drawing.Color.Crimson
        Me.lblAgreement.Location = New System.Drawing.Point(100, 16)
        Me.lblAgreement.Name = "lblAgreement"
        Me.lblAgreement.Size = New System.Drawing.Size(58, 13)
        Me.lblAgreement.TabIndex = 1
        Me.lblAgreement.Text = "Agreement"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Parent Contract No."
        '
        'GbEntry
        '
        Me.GbEntry.Controls.Add(Me.txtrowid)
        Me.GbEntry.Controls.Add(Me.cbopopnonpop)
        Me.GbEntry.Controls.Add(Me.Label5)
        Me.GbEntry.Controls.Add(Me.Label7)
        Me.GbEntry.Controls.Add(Me.Label11)
        Me.GbEntry.Controls.Add(Me.btnSubmit)
        Me.GbEntry.Controls.Add(Me.txtecsamount)
        Me.GbEntry.Controls.Add(Me.Button2)
        Me.GbEntry.Controls.Add(Me.txtchqcount)
        Me.GbEntry.Controls.Add(Me.txtchqamount)
        Me.GbEntry.Controls.Add(Me.Label2)
        Me.GbEntry.Controls.Add(Me.mstchqdate)
        Me.GbEntry.Controls.Add(Me.lblecsamount)
        Me.GbEntry.Controls.Add(Me.btnadd)
        Me.GbEntry.Controls.Add(Me.lblchqdate)
        Me.GbEntry.Controls.Add(Me.cbotype)
        Me.GbEntry.Controls.Add(Me.txtecscount)
        Me.GbEntry.Controls.Add(Me.txtchqno)
        Me.GbEntry.Controls.Add(Me.Label6)
        Me.GbEntry.Controls.Add(Me.lblecscount)
        Me.GbEntry.Location = New System.Drawing.Point(7, 53)
        Me.GbEntry.Name = "GbEntry"
        Me.GbEntry.Size = New System.Drawing.Size(660, 131)
        Me.GbEntry.TabIndex = 0
        Me.GbEntry.TabStop = False
        Me.GbEntry.Text = "Entry"
        '
        'txtrowid
        '
        Me.txtrowid.Location = New System.Drawing.Point(91, 97)
        Me.txtrowid.Name = "txtrowid"
        Me.txtrowid.Size = New System.Drawing.Size(100, 20)
        Me.txtrowid.TabIndex = 1
        Me.txtrowid.Visible = False
        '
        'cbopopnonpop
        '
        Me.cbopopnonpop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbopopnonpop.FormattingEnabled = True
        Me.cbopopnonpop.Items.AddRange(New Object() {"PAP", "NON PAP"})
        Me.cbopopnonpop.Location = New System.Drawing.Point(554, 42)
        Me.cbopopnonpop.Name = "cbopopnonpop"
        Me.cbopopnonpop.Size = New System.Drawing.Size(100, 21)
        Me.cbopopnonpop.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(465, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "PAP/NON PAP"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(276, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Cheque Amount"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(471, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 13)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Cheque Count"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(239, 95)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 9
        Me.btnSubmit.Text = "&Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'txtecsamount
        '
        Me.txtecsamount.Location = New System.Drawing.Point(365, 69)
        Me.txtecsamount.MaxLength = 15
        Me.txtecsamount.Name = "txtecsamount"
        Me.txtecsamount.Size = New System.Drawing.Size(100, 20)
        Me.txtecsamount.TabIndex = 7
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(320, 95)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "&Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtchqcount
        '
        Me.txtchqcount.Location = New System.Drawing.Point(554, 16)
        Me.txtchqcount.MaxLength = 15
        Me.txtchqcount.Name = "txtchqcount"
        Me.txtchqcount.Size = New System.Drawing.Size(100, 20)
        Me.txtchqcount.TabIndex = 2
        Me.txtchqcount.Text = "1"
        '
        'txtchqamount
        '
        Me.txtchqamount.Location = New System.Drawing.Point(365, 42)
        Me.txtchqamount.MaxLength = 15
        Me.txtchqamount.Name = "txtchqamount"
        Me.txtchqamount.Size = New System.Drawing.Size(100, 20)
        Me.txtchqamount.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(295, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Cheque No."
        '
        'mstchqdate
        '
        Me.mstchqdate.Location = New System.Drawing.Point(91, 42)
        Me.mstchqdate.Mask = "00/00/0000"
        Me.mstchqdate.Name = "mstchqdate"
        Me.mstchqdate.Size = New System.Drawing.Size(100, 20)
        Me.mstchqdate.TabIndex = 3
        Me.mstchqdate.ValidatingType = GetType(Date)
        '
        'lblecsamount
        '
        Me.lblecsamount.AutoSize = True
        Me.lblecsamount.Location = New System.Drawing.Point(295, 72)
        Me.lblecsamount.Name = "lblecsamount"
        Me.lblecsamount.Size = New System.Drawing.Size(67, 13)
        Me.lblecsamount.TabIndex = 9
        Me.lblecsamount.Text = "ECS Amount"
        '
        'btnadd
        '
        Me.btnadd.Location = New System.Drawing.Point(487, 67)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(76, 23)
        Me.btnadd.TabIndex = 8
        Me.btnadd.Text = "&Add"
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'lblchqdate
        '
        Me.lblchqdate.AutoSize = True
        Me.lblchqdate.Location = New System.Drawing.Point(15, 45)
        Me.lblchqdate.Name = "lblchqdate"
        Me.lblchqdate.Size = New System.Drawing.Size(70, 13)
        Me.lblchqdate.TabIndex = 1
        Me.lblchqdate.Text = "Cheque Date"
        '
        'cbotype
        '
        Me.cbotype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbotype.FormattingEnabled = True
        Me.cbotype.Items.AddRange(New Object() {"EXTERNAL-NORMAL", "EXTERNAL-SECURITY", "EXTERNAL-SHADOW", "ECS-NORMAL"})
        Me.cbotype.Location = New System.Drawing.Point(91, 15)
        Me.cbotype.Name = "cbotype"
        Me.cbotype.Size = New System.Drawing.Size(153, 21)
        Me.cbotype.TabIndex = 0
        '
        'txtecscount
        '
        Me.txtecscount.Location = New System.Drawing.Point(91, 67)
        Me.txtecscount.MaxLength = 15
        Me.txtecscount.Name = "txtecscount"
        Me.txtecscount.Size = New System.Drawing.Size(100, 20)
        Me.txtecscount.TabIndex = 6
        '
        'txtchqno
        '
        Me.txtchqno.Location = New System.Drawing.Point(365, 16)
        Me.txtchqno.MaxLength = 15
        Me.txtchqno.Name = "txtchqno"
        Me.txtchqno.Size = New System.Drawing.Size(100, 20)
        Me.txtchqno.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(54, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Type"
        '
        'lblecscount
        '
        Me.lblecscount.AutoSize = True
        Me.lblecscount.Location = New System.Drawing.Point(21, 70)
        Me.lblecscount.Name = "lblecscount"
        Me.lblecscount.Size = New System.Drawing.Size(59, 13)
        Me.lblecscount.TabIndex = 7
        Me.lblecscount.Text = "ECS Count"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstcontractnos)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 183)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(179, 213)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Contract Nos"
        '
        'lstcontractnos
        '
        Me.lstcontractnos.FormattingEnabled = True
        Me.lstcontractnos.Location = New System.Drawing.Point(6, 19)
        Me.lstcontractnos.Name = "lstcontractnos"
        Me.lstcontractnos.Size = New System.Drawing.Size(166, 186)
        Me.lstcontractnos.TabIndex = 1
        '
        'dgventry
        '
        Me.dgventry.AllowUserToAddRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black
        Me.dgventry.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgventry.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgventry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.dgventry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgventry.DefaultCellStyle = DataGridViewCellStyle15
        Me.dgventry.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgventry.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgventry.Location = New System.Drawing.Point(188, 190)
        Me.dgventry.Name = "dgventry"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgventry.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgventry.Size = New System.Drawing.Size(478, 206)
        Me.dgventry.TabIndex = 2
        '
        'frmpdcreentry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(686, 406)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmpdcreentry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pdc Reentry"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GbEntry.ResumeLayout(False)
        Me.GbEntry.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgventry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblrefno As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblmode As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblname As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblAgreement As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GbEntry As System.Windows.Forms.GroupBox
    Friend WithEvents txtrowid As System.Windows.Forms.TextBox
    Friend WithEvents cbopopnonpop As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents txtecsamount As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtchqcount As System.Windows.Forms.TextBox
    Friend WithEvents txtchqamount As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mstchqdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblecsamount As System.Windows.Forms.Label
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents lblchqdate As System.Windows.Forms.Label
    Friend WithEvents cbotype As System.Windows.Forms.ComboBox
    Friend WithEvents txtecscount As System.Windows.Forms.TextBox
    Friend WithEvents txtchqno As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblecscount As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstcontractnos As System.Windows.Forms.ListBox
    Friend WithEvents dgventry As System.Windows.Forms.DataGridView
End Class
