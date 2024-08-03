<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmanualbatch
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnopen = New System.Windows.Forms.Button()
        Me.cboproduct = New System.Windows.Forms.ComboBox()
        Me.dtpcycledate = New System.Windows.Forms.DateTimePicker()
        Me.txtbatchno = New System.Windows.Forms.TextBox()
        Me.lblcycledate = New System.Windows.Forms.Label()
        Me.lblproduct = New System.Windows.Forms.Label()
        Me.lblbatchno = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnopen)
        Me.GroupBox1.Controls.Add(Me.cboproduct)
        Me.GroupBox1.Controls.Add(Me.dtpcycledate)
        Me.GroupBox1.Controls.Add(Me.txtbatchno)
        Me.GroupBox1.Controls.Add(Me.lblcycledate)
        Me.GroupBox1.Controls.Add(Me.lblproduct)
        Me.GroupBox1.Controls.Add(Me.lblbatchno)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(539, 72)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnopen
        '
        Me.btnopen.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnopen.Location = New System.Drawing.Point(458, 28)
        Me.btnopen.Name = "btnopen"
        Me.btnopen.Size = New System.Drawing.Size(75, 23)
        Me.btnopen.TabIndex = 6
        Me.btnopen.Text = "Open"
        Me.btnopen.UseVisualStyleBackColor = True
        '
        'cboproduct
        '
        Me.cboproduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboproduct.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboproduct.FormattingEnabled = True
        Me.cboproduct.Location = New System.Drawing.Point(345, 30)
        Me.cboproduct.Name = "cboproduct"
        Me.cboproduct.Size = New System.Drawing.Size(107, 21)
        Me.cboproduct.TabIndex = 5
        '
        'dtpcycledate
        '
        Me.dtpcycledate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpcycledate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpcycledate.Location = New System.Drawing.Point(202, 31)
        Me.dtpcycledate.Name = "dtpcycledate"
        Me.dtpcycledate.Size = New System.Drawing.Size(92, 21)
        Me.dtpcycledate.TabIndex = 4
        '
        'txtbatchno
        '
        Me.txtbatchno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbatchno.Location = New System.Drawing.Point(67, 30)
        Me.txtbatchno.MaxLength = 10
        Me.txtbatchno.Name = "txtbatchno"
        Me.txtbatchno.Size = New System.Drawing.Size(62, 21)
        Me.txtbatchno.TabIndex = 3
        '
        'lblcycledate
        '
        Me.lblcycledate.AutoSize = True
        Me.lblcycledate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcycledate.Location = New System.Drawing.Point(136, 35)
        Me.lblcycledate.Name = "lblcycledate"
        Me.lblcycledate.Size = New System.Drawing.Size(67, 13)
        Me.lblcycledate.TabIndex = 2
        Me.lblcycledate.Text = "Cycle Date"
        '
        'lblproduct
        '
        Me.lblproduct.AutoSize = True
        Me.lblproduct.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblproduct.Location = New System.Drawing.Point(293, 35)
        Me.lblproduct.Name = "lblproduct"
        Me.lblproduct.Size = New System.Drawing.Size(51, 13)
        Me.lblproduct.TabIndex = 1
        Me.lblproduct.Text = "Product"
        '
        'lblbatchno
        '
        Me.lblbatchno.AutoSize = True
        Me.lblbatchno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbatchno.Location = New System.Drawing.Point(9, 33)
        Me.lblbatchno.Name = "lblbatchno"
        Me.lblbatchno.Size = New System.Drawing.Size(56, 13)
        Me.lblbatchno.TabIndex = 0
        Me.lblbatchno.Text = "Batch No"
        '
        'frmmanualbatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(553, 79)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmmanualbatch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manual Batch"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblcycledate As System.Windows.Forms.Label
    Friend WithEvents lblproduct As System.Windows.Forms.Label
    Friend WithEvents lblbatchno As System.Windows.Forms.Label
    Friend WithEvents txtbatchno As System.Windows.Forms.TextBox
    Friend WithEvents dtpcycledate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboproduct As System.Windows.Forms.ComboBox
    Friend WithEvents btnopen As System.Windows.Forms.Button
End Class
