<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatchupdate
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
        Me.btnupdatepatch = New System.Windows.Forms.Button()
        Me.lbltotalrecords = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbltotalrecords)
        Me.GroupBox1.Controls.Add(Me.btnupdatepatch)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(301, 106)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnupdatepatch
        '
        Me.btnupdatepatch.Location = New System.Drawing.Point(78, 32)
        Me.btnupdatepatch.Name = "btnupdatepatch"
        Me.btnupdatepatch.Size = New System.Drawing.Size(145, 23)
        Me.btnupdatepatch.TabIndex = 1
        Me.btnupdatepatch.Text = "Update Patch"
        Me.btnupdatepatch.UseVisualStyleBackColor = True
        '
        'lbltotalrecords
        '
        Me.lbltotalrecords.AutoSize = True
        Me.lbltotalrecords.Location = New System.Drawing.Point(75, 72)
        Me.lbltotalrecords.Name = "lbltotalrecords"
        Me.lbltotalrecords.Size = New System.Drawing.Size(77, 13)
        Me.lbltotalrecords.TabIndex = 1
        Me.lbltotalrecords.Text = "Total Records:"
        '
        'frmPatchupdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(319, 118)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frmPatchupdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patch update"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbltotalrecords As System.Windows.Forms.Label
    Friend WithEvents btnupdatepatch As System.Windows.Forms.Button
End Class
