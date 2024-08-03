<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBoxEntry
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtBoxNo = New System.Windows.Forms.TextBox()
        Me.txtDocCount = New System.Windows.Forms.TextBox()
        Me.txtGNSAREfNoTo = New System.Windows.Forms.TextBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.txtGNSAREfNoFrom = New System.Windows.Forms.TextBox()
        Me.txtBoxBarcode = New System.Windows.Forms.TextBox()
        Me.lblDocCount = New System.Windows.Forms.Label()
        Me.lblDocNoTo = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.lblBoxNo = New System.Windows.Forms.Label()
        Me.lblDocNoFrom = New System.Windows.Forms.Label()
        Me.lblBoxBarcode = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.txtBoxNo)
        Me.pnlMain.Controls.Add(Me.txtDocCount)
        Me.pnlMain.Controls.Add(Me.txtGNSAREfNoTo)
        Me.pnlMain.Controls.Add(Me.txtRemark)
        Me.pnlMain.Controls.Add(Me.txtGNSAREfNoFrom)
        Me.pnlMain.Controls.Add(Me.txtBoxBarcode)
        Me.pnlMain.Controls.Add(Me.lblDocCount)
        Me.pnlMain.Controls.Add(Me.lblDocNoTo)
        Me.pnlMain.Controls.Add(Me.lblRemark)
        Me.pnlMain.Controls.Add(Me.lblBoxNo)
        Me.pnlMain.Controls.Add(Me.lblDocNoFrom)
        Me.pnlMain.Controls.Add(Me.lblBoxBarcode)
        Me.pnlMain.Location = New System.Drawing.Point(9, 9)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(465, 129)
        Me.pnlMain.TabIndex = 0
        '
        'txtBoxNo
        '
        Me.txtBoxNo.Location = New System.Drawing.Point(96, 13)
        Me.txtBoxNo.MaxLength = 10
        Me.txtBoxNo.Name = "txtBoxNo"
        Me.txtBoxNo.Size = New System.Drawing.Size(137, 21)
        Me.txtBoxNo.TabIndex = 0
        '
        'txtDocCount
        '
        Me.txtDocCount.Location = New System.Drawing.Point(96, 67)
        Me.txtDocCount.MaxLength = 20
        Me.txtDocCount.Name = "txtDocCount"
        Me.txtDocCount.ReadOnly = True
        Me.txtDocCount.Size = New System.Drawing.Size(137, 21)
        Me.txtDocCount.TabIndex = 4
        '
        'txtGNSAREfNoTo
        '
        Me.txtGNSAREfNoTo.Location = New System.Drawing.Point(310, 41)
        Me.txtGNSAREfNoTo.MaxLength = 20
        Me.txtGNSAREfNoTo.Name = "txtGNSAREfNoTo"
        Me.txtGNSAREfNoTo.Size = New System.Drawing.Size(137, 21)
        Me.txtGNSAREfNoTo.TabIndex = 3
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(96, 94)
        Me.txtRemark.MaxLength = 255
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(351, 21)
        Me.txtRemark.TabIndex = 5
        '
        'txtGNSAREfNoFrom
        '
        Me.txtGNSAREfNoFrom.Location = New System.Drawing.Point(96, 40)
        Me.txtGNSAREfNoFrom.MaxLength = 20
        Me.txtGNSAREfNoFrom.Name = "txtGNSAREfNoFrom"
        Me.txtGNSAREfNoFrom.Size = New System.Drawing.Size(137, 21)
        Me.txtGNSAREfNoFrom.TabIndex = 2
        '
        'txtBoxBarcode
        '
        Me.txtBoxBarcode.Location = New System.Drawing.Point(310, 14)
        Me.txtBoxBarcode.MaxLength = 64
        Me.txtBoxBarcode.Name = "txtBoxBarcode"
        Me.txtBoxBarcode.Size = New System.Drawing.Size(137, 21)
        Me.txtBoxBarcode.TabIndex = 1
        Me.txtBoxBarcode.Visible = False
        '
        'lblDocCount
        '
        Me.lblDocCount.AutoSize = True
        Me.lblDocCount.Location = New System.Drawing.Point(26, 71)
        Me.lblDocCount.Name = "lblDocCount"
        Me.lblDocCount.Size = New System.Drawing.Size(64, 13)
        Me.lblDocCount.TabIndex = 0
        Me.lblDocCount.Text = "Doc Count"
        '
        'lblDocNoTo
        '
        Me.lblDocNoTo.AutoSize = True
        Me.lblDocNoTo.Location = New System.Drawing.Point(286, 45)
        Me.lblDocNoTo.Name = "lblDocNoTo"
        Me.lblDocNoTo.Size = New System.Drawing.Size(21, 13)
        Me.lblDocNoTo.TabIndex = 0
        Me.lblDocNoTo.Text = "To"
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(32, 97)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(58, 13)
        Me.lblRemark.TabIndex = 0
        Me.lblRemark.Text = "Remarks"
        '
        'lblBoxNo
        '
        Me.lblBoxNo.AutoSize = True
        Me.lblBoxNo.Location = New System.Drawing.Point(45, 17)
        Me.lblBoxNo.Name = "lblBoxNo"
        Me.lblBoxNo.Size = New System.Drawing.Size(45, 13)
        Me.lblBoxNo.TabIndex = 2
        Me.lblBoxNo.Text = "Box No"
        '
        'lblDocNoFrom
        '
        Me.lblDocNoFrom.AutoSize = True
        Me.lblDocNoFrom.Location = New System.Drawing.Point(3, 44)
        Me.lblDocNoFrom.Name = "lblDocNoFrom"
        Me.lblDocNoFrom.Size = New System.Drawing.Size(92, 13)
        Me.lblDocNoFrom.TabIndex = 0
        Me.lblDocNoFrom.Text = "GNSA REF From"
        '
        'lblBoxBarcode
        '
        Me.lblBoxBarcode.AutoSize = True
        Me.lblBoxBarcode.Location = New System.Drawing.Point(250, 18)
        Me.lblBoxBarcode.Name = "lblBoxBarcode"
        Me.lblBoxBarcode.Size = New System.Drawing.Size(57, 13)
        Me.lblBoxBarcode.TabIndex = 0
        Me.lblBoxBarcode.Text = "Bar Code"
        Me.lblBoxBarcode.Visible = False
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(23, 145)
        Me.txtId.MaxLength = 16
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(25, 21)
        Me.txtId.TabIndex = 1
        Me.txtId.Visible = False
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Location = New System.Drawing.Point(54, 144)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(386, 26)
        Me.pnlButtons.TabIndex = 1
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(157, 0)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(72, 24)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(72, 24)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(313, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(235, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 24)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(79, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(72, 24)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "E&dit"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'pnlSave
        '
        Me.pnlSave.CausesValidation = False
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Location = New System.Drawing.Point(163, 144)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(154, 26)
        Me.pnlSave.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.Location = New System.Drawing.Point(79, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmBoxEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(504, 180)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBoxEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Box Entry"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlSave.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblBoxBarcode As System.Windows.Forms.Label
    Friend WithEvents lblBoxNo As System.Windows.Forms.Label
    Friend WithEvents txtBoxBarcode As System.Windows.Forms.TextBox
    Friend WithEvents txtBoxNo As System.Windows.Forms.TextBox
    Friend WithEvents txtGNSAREfNoFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblDocNoFrom As System.Windows.Forms.Label
    Friend WithEvents txtGNSAREfNoTo As System.Windows.Forms.TextBox
    Friend WithEvents lblDocNoTo As System.Windows.Forms.Label
    Friend WithEvents txtDocCount As System.Windows.Forms.TextBox
    Friend WithEvents lblDocCount As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtId As System.Windows.Forms.TextBox
End Class
