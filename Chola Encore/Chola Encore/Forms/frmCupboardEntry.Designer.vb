<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCupboardEntry
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
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.dGBox = New System.Windows.Forms.DataGridView()
        Me.chkSelect = New System.Windows.Forms.CheckBox()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtShelfNo = New System.Windows.Forms.TextBox()
        Me.lblCartonNo = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.pnlSave.SuspendLayout()
        CType(Me.dGBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Location = New System.Drawing.Point(100, 35)
        Me.txtRemarks.MaxLength = 255
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(488, 21)
        Me.txtRemarks.TabIndex = 2
        '
        'pnlSave
        '
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Location = New System.Drawing.Point(229, 296)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(181, 28)
        Me.pnlSave.TabIndex = 8
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(84, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(92, 1)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'dGBox
        '
        Me.dGBox.AllowUserToAddRows = False
        Me.dGBox.AllowUserToDeleteRows = False
        Me.dGBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dGBox.Location = New System.Drawing.Point(14, 104)
        Me.dGBox.Name = "dGBox"
        Me.dGBox.ReadOnly = True
        Me.dGBox.Size = New System.Drawing.Size(610, 186)
        Me.dGBox.TabIndex = 6
        '
        'chkSelect
        '
        Me.chkSelect.AutoSize = True
        Me.chkSelect.Location = New System.Drawing.Point(100, 62)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(78, 17)
        Me.chkSelect.TabIndex = 3
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.UseVisualStyleBackColor = True
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(26, 37)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRemarks.Size = New System.Drawing.Size(58, 13)
        Me.lblRemarks.TabIndex = 117
        Me.lblRemarks.Text = "Remarks"
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.chkSelect)
        Me.pnlMain.Controls.Add(Me.txtRemarks)
        Me.pnlMain.Controls.Add(Me.lblRemarks)
        Me.pnlMain.Controls.Add(Me.txtShelfNo)
        Me.pnlMain.Controls.Add(Me.lblCartonNo)
        Me.pnlMain.Location = New System.Drawing.Point(14, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(610, 86)
        Me.pnlMain.TabIndex = 5
        '
        'txtShelfNo
        '
        Me.txtShelfNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShelfNo.Location = New System.Drawing.Point(100, 8)
        Me.txtShelfNo.MaxLength = 10
        Me.txtShelfNo.Name = "txtShelfNo"
        Me.txtShelfNo.Size = New System.Drawing.Size(488, 21)
        Me.txtShelfNo.TabIndex = 0
        '
        'lblCartonNo
        '
        Me.lblCartonNo.AutoSize = True
        Me.lblCartonNo.Location = New System.Drawing.Point(12, 10)
        Me.lblCartonNo.Name = "lblCartonNo"
        Me.lblCartonNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCartonNo.Size = New System.Drawing.Size(78, 13)
        Me.lblCartonNo.TabIndex = 117
        Me.lblCartonNo.Text = "Cupboard No"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(274, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(84, 24)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Location = New System.Drawing.Point(100, 296)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(453, 28)
        Me.pnlButtons.TabIndex = 7
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(365, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(183, 1)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(84, 24)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(84, 24)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(92, 1)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(84, 24)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "E&dit"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'txtId
        '
        Me.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtId.Location = New System.Drawing.Point(61, 300)
        Me.txtId.MaxLength = 10
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(21, 21)
        Me.txtId.TabIndex = 4
        Me.txtId.Visible = False
        '
        'frmCupboardEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(640, 334)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.dGBox)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.txtId)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCupboardEntry"
        Me.Text = "Cupboard Entry"
        Me.pnlSave.ResumeLayout(False)
        CType(Me.dGBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents dGBox As System.Windows.Forms.DataGridView
    Friend WithEvents chkSelect As System.Windows.Forms.CheckBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtShelfNo As System.Windows.Forms.TextBox
    Friend WithEvents lblCartonNo As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents txtId As System.Windows.Forms.TextBox
End Class
