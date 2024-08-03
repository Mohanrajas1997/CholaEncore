<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChngPktPayMode
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
        Me.txtGnsaRefNo = New System.Windows.Forms.TextBox()
        Me.lblgnsaref = New System.Windows.Forms.Label()
        Me.cboPayMode = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtGnsaRefNo
        '
        Me.txtGnsaRefNo.Location = New System.Drawing.Point(88, 12)
        Me.txtGnsaRefNo.Name = "txtGnsaRefNo"
        Me.txtGnsaRefNo.Size = New System.Drawing.Size(293, 21)
        Me.txtGnsaRefNo.TabIndex = 0
        '
        'lblgnsaref
        '
        Me.lblgnsaref.AutoSize = True
        Me.lblgnsaref.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgnsaref.Location = New System.Drawing.Point(12, 15)
        Me.lblgnsaref.Name = "lblgnsaref"
        Me.lblgnsaref.Size = New System.Drawing.Size(68, 13)
        Me.lblgnsaref.TabIndex = 39
        Me.lblgnsaref.Text = "GNSA Ref#"
        '
        'cboPayMode
        '
        Me.cboPayMode.FormattingEnabled = True
        Me.cboPayMode.Items.AddRange(New Object() {"PDC", "SPDC"})
        Me.cboPayMode.Location = New System.Drawing.Point(88, 39)
        Me.cboPayMode.Name = "cboPayMode"
        Me.cboPayMode.Size = New System.Drawing.Size(293, 21)
        Me.cboPayMode.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(-7, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Pay Mode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(306, 66)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(225, 66)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmChngPktPayMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 97)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cboPayMode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtGnsaRefNo)
        Me.Controls.Add(Me.lblgnsaref)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChngPktPayMode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Packet Pay Mode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtGnsaRefNo As System.Windows.Forms.TextBox
    Friend WithEvents lblgnsaref As System.Windows.Forms.Label
    Friend WithEvents cboPayMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class
