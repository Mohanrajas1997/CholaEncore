<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmwarehouseMIS
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpreceiveddate = New System.Windows.Forms.DateTimePicker()
        Me.btngenerate = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btngenerate)
        Me.GroupBox1.Controls.Add(Me.dtpreceiveddate)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(313, 64)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Received Date"
        '
        'dtpreceiveddate
        '
        Me.dtpreceiveddate.Checked = False
        Me.dtpreceiveddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpreceiveddate.Location = New System.Drawing.Point(103, 24)
        Me.dtpreceiveddate.Name = "dtpreceiveddate"
        Me.dtpreceiveddate.ShowCheckBox = True
        Me.dtpreceiveddate.Size = New System.Drawing.Size(96, 20)
        Me.dtpreceiveddate.TabIndex = 1
        '
        'btngenerate
        '
        Me.btngenerate.Location = New System.Drawing.Point(215, 23)
        Me.btngenerate.Name = "btngenerate"
        Me.btngenerate.Size = New System.Drawing.Size(75, 23)
        Me.btngenerate.TabIndex = 2
        Me.btngenerate.Text = "&Generate"
        Me.btngenerate.UseVisualStyleBackColor = True
        '
        'frmwarehouseMIS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(330, 74)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmwarehouseMIS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ware House MIS"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btngenerate As System.Windows.Forms.Button
    Friend WithEvents dtpreceiveddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
