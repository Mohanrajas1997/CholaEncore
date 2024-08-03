<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmretrievalmissingentry
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
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btnsubmit = New System.Windows.Forms.Button()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rbtnmissing = New System.Windows.Forms.RadioButton()
        Me.rbtnrequested = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboretrievalmode = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtworkitemno = New System.Windows.Forms.TextBox()
        Me.txtagreementno = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbtnCancel = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnCancel)
        Me.GroupBox1.Controls.Add(Me.btnclose)
        Me.GroupBox1.Controls.Add(Me.btnclear)
        Me.GroupBox1.Controls.Add(Me.btnsubmit)
        Me.GroupBox1.Controls.Add(Me.txtremarks)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.rbtnmissing)
        Me.GroupBox1.Controls.Add(Me.rbtnrequested)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboretrievalmode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtworkitemno)
        Me.GroupBox1.Controls.Add(Me.txtagreementno)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(355, 232)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(221, 199)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 9
        Me.btnclose.Text = "C&lose"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnclear
        '
        Me.btnclear.Location = New System.Drawing.Point(140, 199)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(75, 23)
        Me.btnclear.TabIndex = 8
        Me.btnclear.Text = "&Clear"
        Me.btnclear.UseVisualStyleBackColor = True
        '
        'btnsubmit
        '
        Me.btnsubmit.Location = New System.Drawing.Point(59, 199)
        Me.btnsubmit.Name = "btnsubmit"
        Me.btnsubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnsubmit.TabIndex = 7
        Me.btnsubmit.Text = "&Submit"
        Me.btnsubmit.UseVisualStyleBackColor = True
        '
        'txtremarks
        '
        Me.txtremarks.Location = New System.Drawing.Point(130, 144)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(183, 44)
        Me.txtremarks.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(75, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Remarks"
        '
        'rbtnmissing
        '
        Me.rbtnmissing.AutoSize = True
        Me.rbtnmissing.Location = New System.Drawing.Point(130, 119)
        Me.rbtnmissing.Name = "rbtnmissing"
        Me.rbtnmissing.Size = New System.Drawing.Size(60, 17)
        Me.rbtnmissing.TabIndex = 3
        Me.rbtnmissing.TabStop = True
        Me.rbtnmissing.Text = "Missing"
        Me.rbtnmissing.UseVisualStyleBackColor = True
        '
        'rbtnrequested
        '
        Me.rbtnrequested.AutoSize = True
        Me.rbtnrequested.Location = New System.Drawing.Point(196, 119)
        Me.rbtnrequested.Name = "rbtnrequested"
        Me.rbtnrequested.Size = New System.Drawing.Size(77, 17)
        Me.rbtnrequested.TabIndex = 4
        Me.rbtnrequested.TabStop = True
        Me.rbtnrequested.Text = "Requested"
        Me.rbtnrequested.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(87, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Status"
        '
        'cboretrievalmode
        '
        Me.cboretrievalmode.Enabled = False
        Me.cboretrievalmode.FormattingEnabled = True
        Me.cboretrievalmode.Items.AddRange(New Object() {"Packet", "PDC", "SPDC"})
        Me.cboretrievalmode.Location = New System.Drawing.Point(128, 87)
        Me.cboretrievalmode.Name = "cboretrievalmode"
        Me.cboretrievalmode.Size = New System.Drawing.Size(121, 21)
        Me.cboretrievalmode.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Retrieval Mode"
        '
        'txtworkitemno
        '
        Me.txtworkitemno.Location = New System.Drawing.Point(128, 59)
        Me.txtworkitemno.Name = "txtworkitemno"
        Me.txtworkitemno.Size = New System.Drawing.Size(100, 20)
        Me.txtworkitemno.TabIndex = 1
        '
        'txtagreementno
        '
        Me.txtagreementno.Location = New System.Drawing.Point(128, 31)
        Me.txtagreementno.MaxLength = 7
        Me.txtagreementno.Name = "txtagreementno"
        Me.txtagreementno.Size = New System.Drawing.Size(100, 20)
        Me.txtagreementno.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(51, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Work Item No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Agreement No"
        '
        'rbtnCancel
        '
        Me.rbtnCancel.AutoSize = True
        Me.rbtnCancel.Location = New System.Drawing.Point(279, 119)
        Me.rbtnCancel.Name = "rbtnCancel"
        Me.rbtnCancel.Size = New System.Drawing.Size(58, 17)
        Me.rbtnCancel.TabIndex = 5
        Me.rbtnCancel.TabStop = True
        Me.rbtnCancel.Text = "Cancel"
        Me.rbtnCancel.UseVisualStyleBackColor = True
        '
        'frmretrievalmissingentry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(370, 246)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmretrievalmissingentry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Retrieval Missing Entry"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtworkitemno As System.Windows.Forms.TextBox
    Friend WithEvents txtagreementno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rbtnmissing As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnrequested As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboretrievalmode As System.Windows.Forms.ComboBox
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btnsubmit As System.Windows.Forms.Button
    Friend WithEvents txtremarks As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rbtnCancel As System.Windows.Forms.RadioButton
End Class
