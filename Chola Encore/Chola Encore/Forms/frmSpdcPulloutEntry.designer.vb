<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpdcPulloutEntry
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlPktInfo = New System.Windows.Forms.Panel()
        Me.cboAgreementNo = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtChqEntryId = New System.Windows.Forms.TextBox()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtShortAgreementNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.cboReason = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pnlPktInfo.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPktInfo
        '
        Me.pnlPktInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPktInfo.Controls.Add(Me.cboAgreementNo)
        Me.pnlPktInfo.Controls.Add(Me.Label4)
        Me.pnlPktInfo.Controls.Add(Me.txtRemark)
        Me.pnlPktInfo.Controls.Add(Me.Label3)
        Me.pnlPktInfo.Controls.Add(Me.txtChqEntryId)
        Me.pnlPktInfo.Controls.Add(Me.txtChqNo)
        Me.pnlPktInfo.Controls.Add(Me.Label1)
        Me.pnlPktInfo.Controls.Add(Me.txtShortAgreementNo)
        Me.pnlPktInfo.Controls.Add(Me.Label8)
        Me.pnlPktInfo.Controls.Add(Me.txtId)
        Me.pnlPktInfo.Controls.Add(Me.cboReason)
        Me.pnlPktInfo.Controls.Add(Me.Label2)
        Me.pnlPktInfo.Location = New System.Drawing.Point(6, 6)
        Me.pnlPktInfo.Name = "pnlPktInfo"
        Me.pnlPktInfo.Size = New System.Drawing.Size(531, 179)
        Me.pnlPktInfo.TabIndex = 0
        '
        'cboAgreementNo
        '
        Me.cboAgreementNo.FormattingEnabled = True
        Me.cboAgreementNo.Location = New System.Drawing.Point(139, 34)
        Me.cboAgreementNo.Name = "cboAgreementNo"
        Me.cboAgreementNo.Size = New System.Drawing.Size(378, 21)
        Me.cboAgreementNo.TabIndex = 92
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(45, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = "Agreement No"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(139, 115)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(378, 51)
        Me.txtRemark.TabIndex = 3
        Me.txtRemark.Tag = "*"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(54, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 91
        Me.Label3.Text = "Remark"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChqEntryId
        '
        Me.txtChqEntryId.Location = New System.Drawing.Point(5, 41)
        Me.txtChqEntryId.Name = "txtChqEntryId"
        Me.txtChqEntryId.Size = New System.Drawing.Size(14, 21)
        Me.txtChqEntryId.TabIndex = 89
        Me.txtChqEntryId.Visible = False
        '
        'txtChqNo
        '
        Me.txtChqNo.Location = New System.Drawing.Point(139, 61)
        Me.txtChqNo.MaxLength = 16
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.Size = New System.Drawing.Size(378, 21)
        Me.txtChqNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(54, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "Cheque No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShortAgreementNo
        '
        Me.txtShortAgreementNo.Location = New System.Drawing.Point(139, 7)
        Me.txtShortAgreementNo.MaxLength = 8
        Me.txtShortAgreementNo.Name = "txtShortAgreementNo"
        Me.txtShortAgreementNo.Size = New System.Drawing.Size(378, 21)
        Me.txtShortAgreementNo.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 13)
        Me.Label8.TabIndex = 78
        Me.Label8.Text = "Short Agreement No"
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(11, 7)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(14, 21)
        Me.txtId.TabIndex = 65
        Me.txtId.Visible = False
        '
        'cboReason
        '
        Me.cboReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboReason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboReason.FormattingEnabled = True
        Me.cboReason.Location = New System.Drawing.Point(139, 88)
        Me.cboReason.Name = "cboReason"
        Me.cboReason.Size = New System.Drawing.Size(378, 21)
        Me.cboReason.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(63, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Reason"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlSave
        '
        Me.pnlSave.CausesValidation = False
        Me.pnlSave.Controls.Add(Me.btnClear)
        Me.pnlSave.Controls.Add(Me.btnClose)
        Me.pnlSave.Controls.Add(Me.btnFind)
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Controls.Add(Me.btnDelete)
        Me.pnlSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSave.Location = New System.Drawing.Point(149, 191)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(388, 28)
        Me.pnlSave.TabIndex = 1
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.SystemColors.Control
        Me.btnClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClear.Location = New System.Drawing.Point(236, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(72, 24)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(314, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.BackColor = System.Drawing.SystemColors.Control
        Me.btnFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFind.Location = New System.Drawing.Point(80, 1)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(72, 24)
        Me.btnFind.TabIndex = 1
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.SystemColors.Control
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSave.Location = New System.Drawing.Point(2, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDelete.Location = New System.Drawing.Point(158, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 24)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'frmSpdcPulloutEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 225)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.pnlPktInfo)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSpdcPulloutEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SPDC Pullout Entry"
        Me.pnlPktInfo.ResumeLayout(False)
        Me.pnlPktInfo.PerformLayout()
        Me.pnlSave.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlPktInfo As System.Windows.Forms.Panel
    Friend WithEvents cboReason As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents txtChqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtShortAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtChqEntryId As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboAgreementNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
