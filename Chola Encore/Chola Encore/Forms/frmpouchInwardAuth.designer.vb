<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPouchInwardAuth
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
        Me.dgvRpt = New System.Windows.Forms.DataGridView()
        Me.cboAgreementNo = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btnsubmit = New System.Windows.Forms.Button()
        Me.txtgnsarefno = New System.Windows.Forms.TextBox()
        Me.txtShortAgreementNo = New System.Windows.Forms.TextBox()
        Me.lblAgreementNo = New System.Windows.Forms.Label()
        Me.lblGNSARefno = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvRpt)
        Me.GroupBox1.Controls.Add(Me.cboAgreementNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Controls.Add(Me.btnclear)
        Me.GroupBox1.Controls.Add(Me.btnsubmit)
        Me.GroupBox1.Controls.Add(Me.txtgnsarefno)
        Me.GroupBox1.Controls.Add(Me.txtShortAgreementNo)
        Me.GroupBox1.Controls.Add(Me.lblAgreementNo)
        Me.GroupBox1.Controls.Add(Me.lblGNSARefno)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(547, 306)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'dgvRpt
        '
        Me.dgvRpt.AllowUserToAddRows = False
        Me.dgvRpt.AllowUserToDeleteRows = False
        Me.dgvRpt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRpt.Location = New System.Drawing.Point(12, 107)
        Me.dgvRpt.Name = "dgvRpt"
        Me.dgvRpt.ReadOnly = True
        Me.dgvRpt.Size = New System.Drawing.Size(514, 159)
        Me.dgvRpt.TabIndex = 7
        Me.dgvRpt.TabStop = False
        '
        'cboAgreementNo
        '
        Me.cboAgreementNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAgreementNo.FormattingEnabled = True
        Me.cboAgreementNo.Location = New System.Drawing.Point(145, 53)
        Me.cboAgreementNo.Name = "cboAgreementNo"
        Me.cboAgreementNo.Size = New System.Drawing.Size(380, 21)
        Me.cboAgreementNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(51, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Agreement No"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(439, 272)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(87, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnclear
        '
        Me.btnclear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.Location = New System.Drawing.Point(344, 272)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(87, 23)
        Me.btnclear.TabIndex = 4
        Me.btnclear.Text = "&Clear"
        Me.btnclear.UseVisualStyleBackColor = True
        '
        'btnsubmit
        '
        Me.btnsubmit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsubmit.Location = New System.Drawing.Point(250, 272)
        Me.btnsubmit.Name = "btnsubmit"
        Me.btnsubmit.Size = New System.Drawing.Size(87, 23)
        Me.btnsubmit.TabIndex = 3
        Me.btnsubmit.Text = "&Submit"
        Me.btnsubmit.UseVisualStyleBackColor = True
        '
        'txtgnsarefno
        '
        Me.txtgnsarefno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgnsarefno.Location = New System.Drawing.Point(145, 80)
        Me.txtgnsarefno.MaxLength = 12
        Me.txtgnsarefno.Name = "txtgnsarefno"
        Me.txtgnsarefno.Size = New System.Drawing.Size(380, 21)
        Me.txtgnsarefno.TabIndex = 2
        '
        'txtShortAgreementNo
        '
        Me.txtShortAgreementNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortAgreementNo.Location = New System.Drawing.Point(145, 26)
        Me.txtShortAgreementNo.MaxLength = 7
        Me.txtShortAgreementNo.Name = "txtShortAgreementNo"
        Me.txtShortAgreementNo.Size = New System.Drawing.Size(380, 21)
        Me.txtShortAgreementNo.TabIndex = 0
        '
        'lblAgreementNo
        '
        Me.lblAgreementNo.AutoSize = True
        Me.lblAgreementNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgreementNo.Location = New System.Drawing.Point(17, 29)
        Me.lblAgreementNo.Name = "lblAgreementNo"
        Me.lblAgreementNo.Size = New System.Drawing.Size(122, 13)
        Me.lblAgreementNo.TabIndex = 1
        Me.lblAgreementNo.Text = "Short Agreement No"
        '
        'lblGNSARefno
        '
        Me.lblGNSARefno.AutoSize = True
        Me.lblGNSARefno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGNSARefno.Location = New System.Drawing.Point(73, 83)
        Me.lblGNSARefno.Name = "lblGNSARefno"
        Me.lblGNSARefno.Size = New System.Drawing.Size(66, 13)
        Me.lblGNSARefno.TabIndex = 2
        Me.lblGNSARefno.Text = "GNSAREF#"
        '
        'frmPouchInwardAuth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(570, 318)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPouchInwardAuth"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pouch Inward and Auth"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvRpt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtgnsarefno As System.Windows.Forms.TextBox
    Friend WithEvents txtShortAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents lblAgreementNo As System.Windows.Forms.Label
    Friend WithEvents lblGNSARefno As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btnsubmit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboAgreementNo As System.Windows.Forms.ComboBox
    Friend WithEvents dgvRpt As System.Windows.Forms.DataGridView
End Class
