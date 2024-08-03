<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmpacketaudit
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblpacketno = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblshortagreementno = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblagreementno = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvpdcchqentry = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblselectall = New System.Windows.Forms.Label()
        Me.chknonmicrall = New System.Windows.Forms.CheckBox()
        Me.chkmicrall = New System.Windows.Forms.CheckBox()
        Me.chkallnoncts = New System.Windows.Forms.CheckBox()
        Me.chkallcts = New System.Windows.Forms.CheckBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnsubmit = New System.Windows.Forms.Button()
        Me.lbltotal = New System.Windows.Forms.Label()
        Me.TBCChequeentry = New System.Windows.Forms.TabControl()
        Me.TPPDC = New System.Windows.Forms.TabPage()
        Me.TPSPDC = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtecscount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtctscount = New System.Windows.Forms.TextBox()
        Me.txtspdccount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvspdcchqentry = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.rbtnyes = New System.Windows.Forms.RadioButton()
        Me.rbtnno = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvpdcchqentry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TBCChequeentry.SuspendLayout()
        Me.TPPDC.SuspendLayout()
        Me.TPSPDC.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvspdcchqentry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblpacketno)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblshortagreementno)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblagreementno)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(657, 43)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblpacketno
        '
        Me.lblpacketno.AutoSize = True
        Me.lblpacketno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpacketno.ForeColor = System.Drawing.Color.MediumVioletRed
        Me.lblpacketno.Location = New System.Drawing.Point(525, 16)
        Me.lblpacketno.Name = "lblpacketno"
        Me.lblpacketno.Size = New System.Drawing.Size(87, 13)
        Me.lblpacketno.TabIndex = 11
        Me.lblpacketno.Text = "Agreement No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(457, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Packet No:"
        '
        'lblshortagreementno
        '
        Me.lblshortagreementno.AutoSize = True
        Me.lblshortagreementno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblshortagreementno.ForeColor = System.Drawing.Color.MediumVioletRed
        Me.lblshortagreementno.Location = New System.Drawing.Point(378, 16)
        Me.lblshortagreementno.Name = "lblshortagreementno"
        Me.lblshortagreementno.Size = New System.Drawing.Size(69, 13)
        Me.lblshortagreementno.TabIndex = 9
        Me.lblshortagreementno.Text = "Shortagree"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(256, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Short Agreement No:"
        '
        'lblagreementno
        '
        Me.lblagreementno.AutoSize = True
        Me.lblagreementno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblagreementno.ForeColor = System.Drawing.Color.MediumVioletRed
        Me.lblagreementno.Location = New System.Drawing.Point(94, 16)
        Me.lblagreementno.Name = "lblagreementno"
        Me.lblagreementno.Size = New System.Drawing.Size(87, 13)
        Me.lblagreementno.TabIndex = 7
        Me.lblagreementno.Text = "Agreement No"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Agreement No:"
        '
        'dgvpdcchqentry
        '
        Me.dgvpdcchqentry.AllowUserToAddRows = False
        Me.dgvpdcchqentry.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        Me.dgvpdcchqentry.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvpdcchqentry.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvpdcchqentry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvpdcchqentry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvpdcchqentry.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvpdcchqentry.Location = New System.Drawing.Point(2, 3)
        Me.dgvpdcchqentry.Name = "dgvpdcchqentry"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvpdcchqentry.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvpdcchqentry.RowHeadersVisible = False
        Me.dgvpdcchqentry.Size = New System.Drawing.Size(643, 293)
        Me.dgvpdcchqentry.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblselectall)
        Me.Panel1.Controls.Add(Me.chknonmicrall)
        Me.Panel1.Controls.Add(Me.chkmicrall)
        Me.Panel1.Controls.Add(Me.chkallnoncts)
        Me.Panel1.Controls.Add(Me.chkallcts)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnclose)
        Me.Panel1.Controls.Add(Me.btnsubmit)
        Me.Panel1.Controls.Add(Me.lbltotal)
        Me.Panel1.Location = New System.Drawing.Point(12, 378)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(653, 32)
        Me.Panel1.TabIndex = 2
        '
        'lblselectall
        '
        Me.lblselectall.AutoSize = True
        Me.lblselectall.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblselectall.Location = New System.Drawing.Point(77, 9)
        Me.lblselectall.Name = "lblselectall"
        Me.lblselectall.Size = New System.Drawing.Size(61, 13)
        Me.lblselectall.TabIndex = 8
        Me.lblselectall.Text = "Select All"
        '
        'chknonmicrall
        '
        Me.chknonmicrall.AutoSize = True
        Me.chknonmicrall.Location = New System.Drawing.Point(311, 9)
        Me.chknonmicrall.Name = "chknonmicrall"
        Me.chknonmicrall.Size = New System.Drawing.Size(76, 17)
        Me.chknonmicrall.TabIndex = 7
        Me.chknonmicrall.Text = "Non MICR"
        Me.chknonmicrall.UseVisualStyleBackColor = True
        '
        'chkmicrall
        '
        Me.chkmicrall.AutoSize = True
        Me.chkmicrall.Location = New System.Drawing.Point(260, 9)
        Me.chkmicrall.Name = "chkmicrall"
        Me.chkmicrall.Size = New System.Drawing.Size(53, 17)
        Me.chkmicrall.TabIndex = 6
        Me.chkmicrall.Text = "MICR"
        Me.chkmicrall.UseVisualStyleBackColor = True
        '
        'chkallnoncts
        '
        Me.chkallnoncts.AutoSize = True
        Me.chkallnoncts.Location = New System.Drawing.Point(192, 8)
        Me.chkallnoncts.Name = "chkallnoncts"
        Me.chkallnoncts.Size = New System.Drawing.Size(70, 17)
        Me.chkallnoncts.TabIndex = 5
        Me.chkallnoncts.Text = "Non CTS"
        Me.chkallnoncts.UseVisualStyleBackColor = True
        '
        'chkallcts
        '
        Me.chkallcts.AutoSize = True
        Me.chkallcts.Location = New System.Drawing.Point(144, 8)
        Me.chkallcts.Name = "chkallcts"
        Me.chkallcts.Size = New System.Drawing.Size(47, 17)
        Me.chkallcts.TabIndex = 4
        Me.chkallcts.Text = "CTS"
        Me.chkallcts.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(471, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(86, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "&Add Cheque"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnclose
        '
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(563, 4)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 2
        Me.btnclose.Text = "&Close"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnsubmit
        '
        Me.btnsubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsubmit.Location = New System.Drawing.Point(390, 4)
        Me.btnsubmit.Name = "btnsubmit"
        Me.btnsubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnsubmit.TabIndex = 1
        Me.btnsubmit.Text = "&Submit"
        Me.btnsubmit.UseVisualStyleBackColor = True
        '
        'lbltotal
        '
        Me.lbltotal.AutoSize = True
        Me.lbltotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotal.Location = New System.Drawing.Point(3, 8)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(40, 13)
        Me.lbltotal.TabIndex = 0
        Me.lbltotal.Text = "Total:"
        '
        'TBCChequeentry
        '
        Me.TBCChequeentry.Controls.Add(Me.TPPDC)
        Me.TBCChequeentry.Controls.Add(Me.TPSPDC)
        Me.TBCChequeentry.Location = New System.Drawing.Point(8, 49)
        Me.TBCChequeentry.Name = "TBCChequeentry"
        Me.TBCChequeentry.SelectedIndex = 0
        Me.TBCChequeentry.Size = New System.Drawing.Size(657, 323)
        Me.TBCChequeentry.TabIndex = 3
        '
        'TPPDC
        '
        Me.TPPDC.Controls.Add(Me.dgvpdcchqentry)
        Me.TPPDC.Location = New System.Drawing.Point(4, 22)
        Me.TPPDC.Name = "TPPDC"
        Me.TPPDC.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPDC.Size = New System.Drawing.Size(649, 297)
        Me.TPPDC.TabIndex = 0
        Me.TPPDC.Text = "PDC"
        Me.TPPDC.UseVisualStyleBackColor = True
        '
        'TPSPDC
        '
        Me.TPSPDC.Controls.Add(Me.GroupBox2)
        Me.TPSPDC.Controls.Add(Me.dgvspdcchqentry)
        Me.TPSPDC.Location = New System.Drawing.Point(4, 22)
        Me.TPSPDC.Name = "TPSPDC"
        Me.TPSPDC.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSPDC.Size = New System.Drawing.Size(649, 297)
        Me.TPSPDC.TabIndex = 1
        Me.TPSPDC.Text = "SPDC"
        Me.TPSPDC.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtecscount)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtctscount)
        Me.GroupBox2.Controls.Add(Me.txtspdccount)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(636, 42)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'txtecscount
        '
        Me.txtecscount.Location = New System.Drawing.Point(273, 13)
        Me.txtecscount.Name = "txtecscount"
        Me.txtecscount.Size = New System.Drawing.Size(100, 20)
        Me.txtecscount.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(208, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "ECS Count"
        '
        'txtctscount
        '
        Me.txtctscount.Location = New System.Drawing.Point(452, 13)
        Me.txtctscount.Name = "txtctscount"
        Me.txtctscount.Size = New System.Drawing.Size(100, 20)
        Me.txtctscount.TabIndex = 4
        '
        'txtspdccount
        '
        Me.txtspdccount.Location = New System.Drawing.Point(91, 13)
        Me.txtspdccount.Name = "txtspdccount"
        Me.txtspdccount.Size = New System.Drawing.Size(100, 20)
        Me.txtspdccount.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(387, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "CTS Count"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SPDC Count"
        '
        'dgvspdcchqentry
        '
        Me.dgvspdcchqentry.AllowUserToAddRows = False
        Me.dgvspdcchqentry.AllowUserToDeleteRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        Me.dgvspdcchqentry.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvspdcchqentry.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvspdcchqentry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvspdcchqentry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvspdcchqentry.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvspdcchqentry.Location = New System.Drawing.Point(3, 51)
        Me.dgvspdcchqentry.Name = "dgvspdcchqentry"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvspdcchqentry.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvspdcchqentry.RowHeadersVisible = False
        Me.dgvspdcchqentry.Size = New System.Drawing.Size(643, 243)
        Me.dgvspdcchqentry.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(204, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Multiple Bank"
        '
        'rbtnyes
        '
        Me.rbtnyes.AutoSize = True
        Me.rbtnyes.Location = New System.Drawing.Point(279, 48)
        Me.rbtnyes.Name = "rbtnyes"
        Me.rbtnyes.Size = New System.Drawing.Size(43, 17)
        Me.rbtnyes.TabIndex = 6
        Me.rbtnyes.TabStop = True
        Me.rbtnyes.Text = "Yes"
        Me.rbtnyes.UseVisualStyleBackColor = True
        '
        'rbtnno
        '
        Me.rbtnno.AutoSize = True
        Me.rbtnno.Location = New System.Drawing.Point(323, 49)
        Me.rbtnno.Name = "rbtnno"
        Me.rbtnno.Size = New System.Drawing.Size(39, 17)
        Me.rbtnno.TabIndex = 7
        Me.rbtnno.TabStop = True
        Me.rbtnno.Text = "No"
        Me.rbtnno.UseVisualStyleBackColor = True
        '
        'frmpacketaudit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(677, 416)
        Me.Controls.Add(Me.rbtnno)
        Me.Controls.Add(Me.rbtnyes)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TBCChequeentry)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmpacketaudit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Packet Audit"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvpdcchqentry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TBCChequeentry.ResumeLayout(False)
        Me.TPPDC.ResumeLayout(False)
        Me.TPSPDC.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.dgvspdcchqentry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblagreementno As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgvpdcchqentry As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnsubmit As System.Windows.Forms.Button
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblpacketno As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblshortagreementno As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TBCChequeentry As System.Windows.Forms.TabControl
    Friend WithEvents TPPDC As System.Windows.Forms.TabPage
    Friend WithEvents TPSPDC As System.Windows.Forms.TabPage
    Friend WithEvents dgvspdcchqentry As System.Windows.Forms.DataGridView
    Friend WithEvents chkallnoncts As System.Windows.Forms.CheckBox
    Friend WithEvents chkallcts As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtctscount As System.Windows.Forms.TextBox
    Friend WithEvents txtspdccount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtecscount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rbtnyes As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnno As System.Windows.Forms.RadioButton
    Friend WithEvents chknonmicrall As System.Windows.Forms.CheckBox
    Friend WithEvents chkmicrall As System.Windows.Forms.CheckBox
    Friend WithEvents lblselectall As System.Windows.Forms.Label
End Class
