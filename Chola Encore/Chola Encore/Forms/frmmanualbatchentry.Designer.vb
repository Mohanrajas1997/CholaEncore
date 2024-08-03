<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmanualbatchentry
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
        Me.gbDetails = New System.Windows.Forms.GroupBox()
        Me.lblamount = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbltotalchq = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblproduct = New System.Windows.Forms.Label()
        Me.lblcycledate = New System.Windows.Forms.Label()
        Me.lblbatchno = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvsummary = New System.Windows.Forms.DataGridView()
        Me.btnsubmit = New System.Windows.Forms.Button()
        Me.txtchequeamt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtchqno = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboagreementno = New System.Windows.Forms.ComboBox()
        Me.gbDetails.SuspendLayout()
        CType(Me.dgvsummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbDetails
        '
        Me.gbDetails.Controls.Add(Me.lblamount)
        Me.gbDetails.Controls.Add(Me.Label6)
        Me.gbDetails.Controls.Add(Me.lbltotalchq)
        Me.gbDetails.Controls.Add(Me.Label5)
        Me.gbDetails.Controls.Add(Me.lblproduct)
        Me.gbDetails.Controls.Add(Me.lblcycledate)
        Me.gbDetails.Controls.Add(Me.lblbatchno)
        Me.gbDetails.Controls.Add(Me.Label3)
        Me.gbDetails.Controls.Add(Me.Label2)
        Me.gbDetails.Controls.Add(Me.Label1)
        Me.gbDetails.Location = New System.Drawing.Point(7, -1)
        Me.gbDetails.Name = "gbDetails"
        Me.gbDetails.Size = New System.Drawing.Size(582, 72)
        Me.gbDetails.TabIndex = 0
        Me.gbDetails.TabStop = False
        '
        'lblamount
        '
        Me.lblamount.AutoSize = True
        Me.lblamount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblamount.ForeColor = System.Drawing.Color.Crimson
        Me.lblamount.Location = New System.Drawing.Point(521, 28)
        Me.lblamount.Name = "lblamount"
        Me.lblamount.Size = New System.Drawing.Size(52, 13)
        Me.lblamount.TabIndex = 7
        Me.lblamount.Text = "Amount"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(470, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Amount:"
        '
        'lbltotalchq
        '
        Me.lbltotalchq.AutoSize = True
        Me.lbltotalchq.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalchq.ForeColor = System.Drawing.Color.Crimson
        Me.lbltotalchq.Location = New System.Drawing.Point(414, 28)
        Me.lbltotalchq.Name = "lbltotalchq"
        Me.lbltotalchq.Size = New System.Drawing.Size(60, 13)
        Me.lbltotalchq.TabIndex = 7
        Me.lbltotalchq.Text = "Total Chq"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(355, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Total Chq:"
        '
        'lblproduct
        '
        Me.lblproduct.AutoSize = True
        Me.lblproduct.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblproduct.ForeColor = System.Drawing.Color.Crimson
        Me.lblproduct.Location = New System.Drawing.Point(300, 28)
        Me.lblproduct.Name = "lblproduct"
        Me.lblproduct.Size = New System.Drawing.Size(51, 13)
        Me.lblproduct.TabIndex = 5
        Me.lblproduct.Text = "Product"
        '
        'lblcycledate
        '
        Me.lblcycledate.AutoSize = True
        Me.lblcycledate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcycledate.ForeColor = System.Drawing.Color.Crimson
        Me.lblcycledate.Location = New System.Drawing.Point(177, 28)
        Me.lblcycledate.Name = "lblcycledate"
        Me.lblcycledate.Size = New System.Drawing.Size(67, 13)
        Me.lblcycledate.TabIndex = 4
        Me.lblcycledate.Text = "Cycle Date"
        '
        'lblbatchno
        '
        Me.lblbatchno.AutoSize = True
        Me.lblbatchno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbatchno.ForeColor = System.Drawing.Color.Crimson
        Me.lblbatchno.Location = New System.Drawing.Point(59, 28)
        Me.lblbatchno.Name = "lblbatchno"
        Me.lblbatchno.Size = New System.Drawing.Size(56, 13)
        Me.lblbatchno.TabIndex = 3
        Me.lblbatchno.Text = "Batch No"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(249, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Product:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(113, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Cycle Date:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Batch No:"
        '
        'dgvsummary
        '
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
        Me.dgvsummary.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvsummary.Location = New System.Drawing.Point(7, 167)
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
        Me.dgvsummary.Size = New System.Drawing.Size(582, 183)
        Me.dgvsummary.TabIndex = 4
        '
        'btnsubmit
        '
        Me.btnsubmit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsubmit.Location = New System.Drawing.Point(511, 138)
        Me.btnsubmit.Name = "btnsubmit"
        Me.btnsubmit.Size = New System.Drawing.Size(69, 23)
        Me.btnsubmit.TabIndex = 3
        Me.btnsubmit.Text = "&Submit"
        Me.btnsubmit.UseVisualStyleBackColor = True
        '
        'txtchequeamt
        '
        Me.txtchequeamt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchequeamt.Location = New System.Drawing.Point(382, 81)
        Me.txtchequeamt.MaxLength = 10
        Me.txtchequeamt.Name = "txtchequeamt"
        Me.txtchequeamt.Size = New System.Drawing.Size(198, 21)
        Me.txtchequeamt.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(325, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Amount"
        '
        'txtchqno
        '
        Me.txtchqno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchqno.Location = New System.Drawing.Point(105, 80)
        Me.txtchqno.MaxLength = 12
        Me.txtchqno.Name = "txtchqno"
        Me.txtchqno.Size = New System.Drawing.Size(198, 21)
        Me.txtchqno.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(33, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Cheque No"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 111)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Agreement No"
        '
        'cboagreementno
        '
        Me.cboagreementno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboagreementno.FormattingEnabled = True
        Me.cboagreementno.Location = New System.Drawing.Point(105, 108)
        Me.cboagreementno.Name = "cboagreementno"
        Me.cboagreementno.Size = New System.Drawing.Size(475, 21)
        Me.cboagreementno.TabIndex = 2
        '
        'frmmanualbatchentry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(598, 362)
        Me.Controls.Add(Me.cboagreementno)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnsubmit)
        Me.Controls.Add(Me.txtchequeamt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtchqno)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dgvsummary)
        Me.Controls.Add(Me.gbDetails)
        Me.KeyPreview = True
        Me.Name = "frmmanualbatchentry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manual Batch Entry"
        Me.gbDetails.ResumeLayout(False)
        Me.gbDetails.PerformLayout()
        CType(Me.dgvsummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbDetails As System.Windows.Forms.GroupBox
    Friend WithEvents lblbatchno As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblcycledate As System.Windows.Forms.Label
    Friend WithEvents lblproduct As System.Windows.Forms.Label
    Friend WithEvents lblamount As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbltotalchq As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgvsummary As System.Windows.Forms.DataGridView
    Friend WithEvents btnsubmit As System.Windows.Forms.Button
    Friend WithEvents txtchequeamt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtchqno As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboagreementno As System.Windows.Forms.ComboBox
End Class
