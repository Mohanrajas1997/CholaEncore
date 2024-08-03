<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmpacketauditnew
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtecscount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblagreementno = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblmode = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblpacketno = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblshortagreementno = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtctscount = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtspdccount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TPSPDC = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cbospdcbranch = New System.Windows.Forms.ComboBox()
        Me.btnspdcupdate = New System.Windows.Forms.Button()
        Me.cbospdccts = New System.Windows.Forms.ComboBox()
        Me.txtspdcaccountno = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtspdcmicrcode = New System.Windows.Forms.TextBox()
        Me.txtspdcbankcode = New System.Windows.Forms.TextBox()
        Me.txtspdcbankname = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dgvspdcchqentry = New System.Windows.Forms.DataGridView()
        Me.rbtnyes = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.TPPDC = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cbopdcbranch = New System.Windows.Forms.ComboBox()
        Me.btnpdcupdate = New System.Windows.Forms.Button()
        Me.cbopdccts = New System.Windows.Forms.ComboBox()
        Me.txtpdcaccountno = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtpdcmicrcode = New System.Windows.Forms.TextBox()
        Me.txtpdcbankcode = New System.Windows.Forms.TextBox()
        Me.txtpdcbankName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgvpdcchqentry = New System.Windows.Forms.DataGridView()
        Me.TBCChequeentry = New System.Windows.Forms.TabControl()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnsubmit = New System.Windows.Forms.Button()
        Me.lbltotal = New System.Windows.Forms.Label()
        Me.rbtnno = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TPSPDC.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgvspdcchqentry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPPDC.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvpdcchqentry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBCChequeentry.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblagreementno)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblmode)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblpacketno)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblshortagreementno)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(864, 43)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblagreementno
        '
        Me.lblagreementno.AutoSize = True
        Me.lblagreementno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblagreementno.ForeColor = System.Drawing.Color.MediumVioletRed
        Me.lblagreementno.Location = New System.Drawing.Point(307, 17)
        Me.lblagreementno.Name = "lblagreementno"
        Me.lblagreementno.Size = New System.Drawing.Size(69, 13)
        Me.lblagreementno.TabIndex = 7
        Me.lblagreementno.Text = "Shortagree"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(218, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(91, 13)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Agreement No:"
        '
        'lblmode
        '
        Me.lblmode.AutoSize = True
        Me.lblmode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmode.ForeColor = System.Drawing.Color.MediumVioletRed
        Me.lblmode.Location = New System.Drawing.Point(759, 17)
        Me.lblmode.Name = "lblmode"
        Me.lblmode.Size = New System.Drawing.Size(38, 13)
        Me.lblmode.TabIndex = 5
        Me.lblmode.Text = "Mode"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(722, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 13)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Mode:"
        '
        'lblpacketno
        '
        Me.lblpacketno.AutoSize = True
        Me.lblpacketno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpacketno.ForeColor = System.Drawing.Color.MediumVioletRed
        Me.lblpacketno.Location = New System.Drawing.Point(81, 16)
        Me.lblpacketno.Name = "lblpacketno"
        Me.lblpacketno.Size = New System.Drawing.Size(67, 13)
        Me.lblpacketno.TabIndex = 1
        Me.lblpacketno.Text = "Packet No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Packet No:"
        '
        'lblshortagreementno
        '
        Me.lblshortagreementno.AutoSize = True
        Me.lblshortagreementno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblshortagreementno.ForeColor = System.Drawing.Color.MediumVioletRed
        Me.lblshortagreementno.Location = New System.Drawing.Point(629, 16)
        Me.lblshortagreementno.Name = "lblshortagreementno"
        Me.lblshortagreementno.Size = New System.Drawing.Size(69, 13)
        Me.lblshortagreementno.TabIndex = 3
        Me.lblshortagreementno.Text = "Shortagree"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(507, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Short Agreement No:"
        '
        'txtctscount
        '
        Me.txtctscount.Location = New System.Drawing.Point(452, 13)
        Me.txtctscount.Name = "txtctscount"
        Me.txtctscount.Size = New System.Drawing.Size(100, 20)
        Me.txtctscount.TabIndex = 4
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
        'TPSPDC
        '
        Me.TPSPDC.Controls.Add(Me.GroupBox4)
        Me.TPSPDC.Controls.Add(Me.GroupBox2)
        Me.TPSPDC.Controls.Add(Me.dgvspdcchqentry)
        Me.TPSPDC.Location = New System.Drawing.Point(4, 22)
        Me.TPSPDC.Name = "TPSPDC"
        Me.TPSPDC.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSPDC.Size = New System.Drawing.Size(864, 457)
        Me.TPSPDC.TabIndex = 1
        Me.TPSPDC.Text = "SPDC"
        Me.TPSPDC.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cbospdcbranch)
        Me.GroupBox4.Controls.Add(Me.btnspdcupdate)
        Me.GroupBox4.Controls.Add(Me.cbospdccts)
        Me.GroupBox4.Controls.Add(Me.txtspdcaccountno)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.txtspdcmicrcode)
        Me.GroupBox4.Controls.Add(Me.txtspdcbankcode)
        Me.GroupBox4.Controls.Add(Me.txtspdcbankname)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 48)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(858, 90)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'cbospdcbranch
        '
        Me.cbospdcbranch.FormattingEnabled = True
        Me.cbospdcbranch.Location = New System.Drawing.Point(83, 46)
        Me.cbospdcbranch.Name = "cbospdcbranch"
        Me.cbospdcbranch.Size = New System.Drawing.Size(121, 21)
        Me.cbospdcbranch.TabIndex = 8
        '
        'btnspdcupdate
        '
        Me.btnspdcupdate.Location = New System.Drawing.Point(437, 46)
        Me.btnspdcupdate.Name = "btnspdcupdate"
        Me.btnspdcupdate.Size = New System.Drawing.Size(75, 23)
        Me.btnspdcupdate.TabIndex = 11
        Me.btnspdcupdate.Text = "&Update"
        Me.btnspdcupdate.UseVisualStyleBackColor = True
        '
        'cbospdccts
        '
        Me.cbospdccts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbospdccts.FormattingEnabled = True
        Me.cbospdccts.Items.AddRange(New Object() {"Yes", "No"})
        Me.cbospdccts.Location = New System.Drawing.Point(301, 46)
        Me.cbospdccts.Name = "cbospdccts"
        Me.cbospdccts.Size = New System.Drawing.Size(100, 21)
        Me.cbospdccts.TabIndex = 10
        '
        'txtspdcaccountno
        '
        Me.txtspdcaccountno.Location = New System.Drawing.Point(86, 17)
        Me.txtspdcaccountno.MaxLength = 32
        Me.txtspdcaccountno.Name = "txtspdcaccountno"
        Me.txtspdcaccountno.Size = New System.Drawing.Size(100, 20)
        Me.txtspdcaccountno.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(21, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(64, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Account No"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(260, 49)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 13)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "Is CTS"
        '
        'txtspdcmicrcode
        '
        Me.txtspdcmicrcode.Location = New System.Drawing.Point(301, 17)
        Me.txtspdcmicrcode.MaxLength = 9
        Me.txtspdcmicrcode.Name = "txtspdcmicrcode"
        Me.txtspdcmicrcode.Size = New System.Drawing.Size(100, 20)
        Me.txtspdcmicrcode.TabIndex = 3
        '
        'txtspdcbankcode
        '
        Me.txtspdcbankcode.Location = New System.Drawing.Point(469, 17)
        Me.txtspdcbankcode.MaxLength = 3
        Me.txtspdcbankcode.Name = "txtspdcbankcode"
        Me.txtspdcbankcode.Size = New System.Drawing.Size(43, 20)
        Me.txtspdcbankcode.TabIndex = 5
        '
        'txtspdcbankname
        '
        Me.txtspdcbankname.Enabled = False
        Me.txtspdcbankname.Location = New System.Drawing.Point(518, 17)
        Me.txtspdcbankname.Name = "txtspdcbankname"
        Me.txtspdcbankname.Size = New System.Drawing.Size(228, 20)
        Me.txtspdcbankname.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(41, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(41, 13)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "Branch"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(403, 20)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 13)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "Bank Name"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(244, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(55, 13)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Micr Code"
        '
        'dgvspdcchqentry
        '
        Me.dgvspdcchqentry.AllowUserToAddRows = False
        Me.dgvspdcchqentry.AllowUserToDeleteRows = False
        Me.dgvspdcchqentry.AllowUserToResizeColumns = False
        Me.dgvspdcchqentry.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgvspdcchqentry.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvspdcchqentry.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvspdcchqentry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvspdcchqentry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvspdcchqentry.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvspdcchqentry.Location = New System.Drawing.Point(3, 144)
        Me.dgvspdcchqentry.Name = "dgvspdcchqentry"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvspdcchqentry.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvspdcchqentry.RowHeadersVisible = False
        Me.dgvspdcchqentry.Size = New System.Drawing.Size(855, 307)
        Me.dgvspdcchqentry.TabIndex = 2
        '
        'rbtnyes
        '
        Me.rbtnyes.AutoSize = True
        Me.rbtnyes.Location = New System.Drawing.Point(372, 49)
        Me.rbtnyes.Name = "rbtnyes"
        Me.rbtnyes.Size = New System.Drawing.Size(43, 17)
        Me.rbtnyes.TabIndex = 12
        Me.rbtnyes.TabStop = True
        Me.rbtnyes.Text = "Yes"
        Me.rbtnyes.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(297, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Multiple Bank"
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(678, 3)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(86, 23)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "&Add Cheque"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'TPPDC
        '
        Me.TPPDC.Controls.Add(Me.GroupBox3)
        Me.TPPDC.Controls.Add(Me.dgvpdcchqentry)
        Me.TPPDC.Location = New System.Drawing.Point(4, 22)
        Me.TPPDC.Name = "TPPDC"
        Me.TPPDC.Padding = New System.Windows.Forms.Padding(3)
        Me.TPPDC.Size = New System.Drawing.Size(864, 457)
        Me.TPPDC.TabIndex = 0
        Me.TPPDC.Text = "PDC"
        Me.TPPDC.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbopdcbranch)
        Me.GroupBox3.Controls.Add(Me.btnpdcupdate)
        Me.GroupBox3.Controls.Add(Me.cbopdccts)
        Me.GroupBox3.Controls.Add(Me.txtpdcaccountno)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtpdcmicrcode)
        Me.GroupBox3.Controls.Add(Me.txtpdcbankcode)
        Me.GroupBox3.Controls.Add(Me.txtpdcbankName)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(858, 90)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'cbopdcbranch
        '
        Me.cbopdcbranch.FormattingEnabled = True
        Me.cbopdcbranch.Location = New System.Drawing.Point(86, 46)
        Me.cbopdcbranch.Name = "cbopdcbranch"
        Me.cbopdcbranch.Size = New System.Drawing.Size(121, 21)
        Me.cbopdcbranch.TabIndex = 8
        '
        'btnpdcupdate
        '
        Me.btnpdcupdate.Location = New System.Drawing.Point(437, 46)
        Me.btnpdcupdate.Name = "btnpdcupdate"
        Me.btnpdcupdate.Size = New System.Drawing.Size(75, 23)
        Me.btnpdcupdate.TabIndex = 11
        Me.btnpdcupdate.Text = "&Update"
        Me.btnpdcupdate.UseVisualStyleBackColor = True
        '
        'cbopdccts
        '
        Me.cbopdccts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbopdccts.FormattingEnabled = True
        Me.cbopdccts.Items.AddRange(New Object() {"Yes", "No"})
        Me.cbopdccts.Location = New System.Drawing.Point(301, 46)
        Me.cbopdccts.Name = "cbopdccts"
        Me.cbopdccts.Size = New System.Drawing.Size(100, 21)
        Me.cbopdccts.TabIndex = 10
        '
        'txtpdcaccountno
        '
        Me.txtpdcaccountno.Location = New System.Drawing.Point(86, 17)
        Me.txtpdcaccountno.MaxLength = 32
        Me.txtpdcaccountno.Name = "txtpdcaccountno"
        Me.txtpdcaccountno.Size = New System.Drawing.Size(100, 20)
        Me.txtpdcaccountno.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(21, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Account No"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(260, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Is CTS"
        '
        'txtpdcmicrcode
        '
        Me.txtpdcmicrcode.Location = New System.Drawing.Point(301, 17)
        Me.txtpdcmicrcode.MaxLength = 9
        Me.txtpdcmicrcode.Name = "txtpdcmicrcode"
        Me.txtpdcmicrcode.Size = New System.Drawing.Size(100, 20)
        Me.txtpdcmicrcode.TabIndex = 3
        '
        'txtpdcbankcode
        '
        Me.txtpdcbankcode.Location = New System.Drawing.Point(469, 17)
        Me.txtpdcbankcode.MaxLength = 3
        Me.txtpdcbankcode.Name = "txtpdcbankcode"
        Me.txtpdcbankcode.Size = New System.Drawing.Size(43, 20)
        Me.txtpdcbankcode.TabIndex = 5
        '
        'txtpdcbankName
        '
        Me.txtpdcbankName.Enabled = False
        Me.txtpdcbankName.Location = New System.Drawing.Point(518, 17)
        Me.txtpdcbankName.Name = "txtpdcbankName"
        Me.txtpdcbankName.Size = New System.Drawing.Size(228, 20)
        Me.txtpdcbankName.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(41, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Branch"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(403, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Bank Name"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(244, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Micr Code"
        '
        'dgvpdcchqentry
        '
        Me.dgvpdcchqentry.AllowUserToAddRows = False
        Me.dgvpdcchqentry.AllowUserToDeleteRows = False
        Me.dgvpdcchqentry.AllowUserToResizeColumns = False
        Me.dgvpdcchqentry.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        Me.dgvpdcchqentry.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvpdcchqentry.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvpdcchqentry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvpdcchqentry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvpdcchqentry.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvpdcchqentry.Location = New System.Drawing.Point(2, 96)
        Me.dgvpdcchqentry.Name = "dgvpdcchqentry"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvpdcchqentry.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvpdcchqentry.RowHeadersVisible = False
        Me.dgvpdcchqentry.Size = New System.Drawing.Size(858, 357)
        Me.dgvpdcchqentry.TabIndex = 1
        '
        'TBCChequeentry
        '
        Me.TBCChequeentry.Controls.Add(Me.TPPDC)
        Me.TBCChequeentry.Controls.Add(Me.TPSPDC)
        Me.TBCChequeentry.Location = New System.Drawing.Point(10, 51)
        Me.TBCChequeentry.Name = "TBCChequeentry"
        Me.TBCChequeentry.SelectedIndex = 0
        Me.TBCChequeentry.Size = New System.Drawing.Size(872, 483)
        Me.TBCChequeentry.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(770, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "&Close"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnclose)
        Me.Panel1.Controls.Add(Me.btnsubmit)
        Me.Panel1.Controls.Add(Me.lbltotal)
        Me.Panel1.Location = New System.Drawing.Point(12, 540)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(866, 32)
        Me.Panel1.TabIndex = 2
        '
        'btnsubmit
        '
        Me.btnsubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsubmit.Location = New System.Drawing.Point(597, 3)
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
        'rbtnno
        '
        Me.rbtnno.AutoSize = True
        Me.rbtnno.Location = New System.Drawing.Point(416, 50)
        Me.rbtnno.Name = "rbtnno"
        Me.rbtnno.Size = New System.Drawing.Size(39, 17)
        Me.rbtnno.TabIndex = 13
        Me.rbtnno.TabStop = True
        Me.rbtnno.Text = "No"
        Me.rbtnno.UseVisualStyleBackColor = True
        '
        'frmpacketauditnew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(889, 580)
        Me.Controls.Add(Me.rbtnno)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.rbtnyes)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TBCChequeentry)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "frmpacketauditnew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Packet Audit"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TPSPDC.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.dgvspdcchqentry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPPDC.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgvpdcchqentry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBCChequeentry.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtecscount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblpacketno As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblshortagreementno As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtctscount As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtspdccount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TPSPDC As System.Windows.Forms.TabPage
    Friend WithEvents dgvspdcchqentry As System.Windows.Forms.DataGridView
    Friend WithEvents rbtnyes As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents TPPDC As System.Windows.Forms.TabPage
    Friend WithEvents dgvpdcchqentry As System.Windows.Forms.DataGridView
    Friend WithEvents TBCChequeentry As System.Windows.Forms.TabControl
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnsubmit As System.Windows.Forms.Button
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    Friend WithEvents rbtnno As System.Windows.Forms.RadioButton
    Friend WithEvents lblmode As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtpdcmicrcode As System.Windows.Forms.TextBox
    Friend WithEvents txtpdcbankcode As System.Windows.Forms.TextBox
    Friend WithEvents txtpdcbankName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnpdcupdate As System.Windows.Forms.Button
    Friend WithEvents cbopdccts As System.Windows.Forms.ComboBox
    Friend WithEvents txtpdcaccountno As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblagreementno As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnspdcupdate As System.Windows.Forms.Button
    Friend WithEvents cbospdccts As System.Windows.Forms.ComboBox
    Friend WithEvents txtspdcaccountno As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtspdcmicrcode As System.Windows.Forms.TextBox
    Friend WithEvents txtspdcbankcode As System.Windows.Forms.TextBox
    Friend WithEvents txtspdcbankname As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbopdcbranch As System.Windows.Forms.ComboBox
    Friend WithEvents cbospdcbranch As System.Windows.Forms.ComboBox
End Class
