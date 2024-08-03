<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmalmaraentry
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtGNSAREfNoTo = New System.Windows.Forms.TextBox()
        Me.txtboxno = New System.Windows.Forms.TextBox()
        Me.txtshelfno = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtcupboardno = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDocCount = New System.Windows.Forms.TextBox()
        Me.txtGNSAREfNoFrom = New System.Windows.Forms.TextBox()
        Me.lblDocCount = New System.Windows.Forms.Label()
        Me.lblDocNoTo = New System.Windows.Forms.Label()
        Me.lblBoxNo = New System.Windows.Forms.Label()
        Me.lblDocNoFrom = New System.Windows.Forms.Label()
        Me.pnlSave = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.pnlMain.SuspendLayout()
        Me.pnlSave.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.txtGNSAREfNoTo)
        Me.pnlMain.Controls.Add(Me.txtboxno)
        Me.pnlMain.Controls.Add(Me.txtshelfno)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.txtcupboardno)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.txtDocCount)
        Me.pnlMain.Controls.Add(Me.txtGNSAREfNoFrom)
        Me.pnlMain.Controls.Add(Me.lblDocCount)
        Me.pnlMain.Controls.Add(Me.lblDocNoTo)
        Me.pnlMain.Controls.Add(Me.lblBoxNo)
        Me.pnlMain.Controls.Add(Me.lblDocNoFrom)
        Me.pnlMain.Location = New System.Drawing.Point(14, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(542, 101)
        Me.pnlMain.TabIndex = 0
        '
        'txtGNSAREfNoTo
        '
        Me.txtGNSAREfNoTo.Location = New System.Drawing.Point(338, 38)
        Me.txtGNSAREfNoTo.MaxLength = 20
        Me.txtGNSAREfNoTo.Name = "txtGNSAREfNoTo"
        Me.txtGNSAREfNoTo.Size = New System.Drawing.Size(173, 21)
        Me.txtGNSAREfNoTo.TabIndex = 4
        '
        'txtboxno
        '
        Me.txtboxno.Location = New System.Drawing.Point(338, 15)
        Me.txtboxno.MaxLength = 10
        Me.txtboxno.Name = "txtboxno"
        Me.txtboxno.Size = New System.Drawing.Size(63, 21)
        Me.txtboxno.TabIndex = 2
        '
        'txtshelfno
        '
        Me.txtshelfno.Location = New System.Drawing.Point(222, 16)
        Me.txtshelfno.MaxLength = 10
        Me.txtshelfno.Name = "txtshelfno"
        Me.txtshelfno.Size = New System.Drawing.Size(63, 21)
        Me.txtshelfno.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(159, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Shelf No"
        '
        'txtcupboardno
        '
        Me.txtcupboardno.Location = New System.Drawing.Point(112, 14)
        Me.txtcupboardno.MaxLength = 10
        Me.txtcupboardno.Name = "txtcupboardno"
        Me.txtcupboardno.Size = New System.Drawing.Size(44, 21)
        Me.txtcupboardno.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Cupboard No"
        '
        'txtDocCount
        '
        Me.txtDocCount.Location = New System.Drawing.Point(112, 67)
        Me.txtDocCount.MaxLength = 20
        Me.txtDocCount.Name = "txtDocCount"
        Me.txtDocCount.ReadOnly = True
        Me.txtDocCount.Size = New System.Drawing.Size(44, 21)
        Me.txtDocCount.TabIndex = 5
        '
        'txtGNSAREfNoFrom
        '
        Me.txtGNSAREfNoFrom.Location = New System.Drawing.Point(112, 40)
        Me.txtGNSAREfNoFrom.MaxLength = 20
        Me.txtGNSAREfNoFrom.Name = "txtGNSAREfNoFrom"
        Me.txtGNSAREfNoFrom.Size = New System.Drawing.Size(173, 21)
        Me.txtGNSAREfNoFrom.TabIndex = 3
        '
        'lblDocCount
        '
        Me.lblDocCount.AutoSize = True
        Me.lblDocCount.Location = New System.Drawing.Point(30, 71)
        Me.lblDocCount.Name = "lblDocCount"
        Me.lblDocCount.Size = New System.Drawing.Size(64, 13)
        Me.lblDocCount.TabIndex = 0
        Me.lblDocCount.Text = "Doc Count"
        '
        'lblDocNoTo
        '
        Me.lblDocNoTo.AutoSize = True
        Me.lblDocNoTo.Location = New System.Drawing.Point(307, 45)
        Me.lblDocNoTo.Name = "lblDocNoTo"
        Me.lblDocNoTo.Size = New System.Drawing.Size(21, 13)
        Me.lblDocNoTo.TabIndex = 0
        Me.lblDocNoTo.Text = "To"
        '
        'lblBoxNo
        '
        Me.lblBoxNo.AutoSize = True
        Me.lblBoxNo.Location = New System.Drawing.Point(288, 19)
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
        'pnlSave
        '
        Me.pnlSave.CausesValidation = False
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Location = New System.Drawing.Point(194, 121)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(180, 26)
        Me.pnlSave.TabIndex = 6
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(84, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.Location = New System.Drawing.Point(92, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(92, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(84, 24)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "E&dit"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(274, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(84, 24)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(365, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(84, 24)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(183, 0)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(84, 24)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = False
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(30, 122)
        Me.txtId.MaxLength = 16
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(28, 21)
        Me.txtId.TabIndex = 4
        Me.txtId.Visible = False
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnFind)
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnDelete)
        Me.pnlButtons.Controls.Add(Me.btnEdit)
        Me.pnlButtons.Location = New System.Drawing.Point(66, 121)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(450, 26)
        Me.pnlButtons.TabIndex = 5
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(290, 38)
        Me.TextBox4.MaxLength = 20
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(149, 20)
        Me.TextBox4.TabIndex = 10
        '
        'frmalmaraentry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(576, 156)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlSave)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmalmaraentry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Almara Entry"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlSave.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtDocCount As System.Windows.Forms.TextBox
    Friend WithEvents txtGNSAREfNoFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblDocCount As System.Windows.Forms.Label
    Friend WithEvents lblDocNoTo As System.Windows.Forms.Label
    Friend WithEvents lblBoxNo As System.Windows.Forms.Label
    Friend WithEvents lblDocNoFrom As System.Windows.Forms.Label
    Friend WithEvents pnlSave As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents txtboxno As System.Windows.Forms.TextBox
    Friend WithEvents txtshelfno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcupboardno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtGNSAREfNoTo As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
End Class
