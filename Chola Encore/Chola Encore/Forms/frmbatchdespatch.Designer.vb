<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbatchdespatch
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
        Me.dGBatch = New System.Windows.Forms.DataGridView()
        Me.chkSelect = New System.Windows.Forms.CheckBox()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.btnload = New System.Windows.Forms.Button()
        Me.cboproducttype = New System.Windows.Forms.ComboBox()
        Me.dtpcycledate = New System.Windows.Forms.DateTimePicker()
        Me.lblproducttype = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtsentto = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtairwaybillno = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtdespatchno = New System.Windows.Forms.TextBox()
        Me.lbldespatchno = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.pnlSave.SuspendLayout()
        CType(Me.dGBatch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtRemarks
        '
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Location = New System.Drawing.Point(86, 34)
        Me.txtRemarks.MaxLength = 255
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(419, 20)
        Me.txtRemarks.TabIndex = 3
        '
        'pnlSave
        '
        Me.pnlSave.Controls.Add(Me.btnSave)
        Me.pnlSave.Controls.Add(Me.btnCancel)
        Me.pnlSave.Location = New System.Drawing.Point(192, 292)
        Me.pnlSave.Name = "pnlSave"
        Me.pnlSave.Size = New System.Drawing.Size(155, 28)
        Me.pnlSave.TabIndex = 8
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1, 1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 24)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(79, 1)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'dGBatch
        '
        Me.dGBatch.AllowUserToAddRows = False
        Me.dGBatch.AllowUserToDeleteRows = False
        Me.dGBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dGBatch.Location = New System.Drawing.Point(8, 100)
        Me.dGBatch.Name = "dGBatch"
        Me.dGBatch.ReadOnly = True
        Me.dGBatch.Size = New System.Drawing.Size(523, 186)
        Me.dGBatch.TabIndex = 6
        '
        'chkSelect
        '
        Me.chkSelect.AutoSize = True
        Me.chkSelect.Location = New System.Drawing.Point(435, 61)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(70, 17)
        Me.chkSelect.TabIndex = 7
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.UseVisualStyleBackColor = True
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(31, 36)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 117
        Me.lblRemarks.Text = "Remarks"
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.btnload)
        Me.pnlMain.Controls.Add(Me.cboproducttype)
        Me.pnlMain.Controls.Add(Me.dtpcycledate)
        Me.pnlMain.Controls.Add(Me.lblproducttype)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.txtsentto)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.txtairwaybillno)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.chkSelect)
        Me.pnlMain.Controls.Add(Me.txtRemarks)
        Me.pnlMain.Controls.Add(Me.lblRemarks)
        Me.pnlMain.Controls.Add(Me.txtdespatchno)
        Me.pnlMain.Controls.Add(Me.lbldespatchno)
        Me.pnlMain.Location = New System.Drawing.Point(8, 8)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(523, 86)
        Me.pnlMain.TabIndex = 0
        '
        'btnload
        '
        Me.btnload.Location = New System.Drawing.Point(367, 56)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(62, 23)
        Me.btnload.TabIndex = 6
        Me.btnload.Text = "Load"
        Me.btnload.UseVisualStyleBackColor = True
        '
        'cboproducttype
        '
        Me.cboproducttype.FormattingEnabled = True
        Me.cboproducttype.Location = New System.Drawing.Point(259, 58)
        Me.cboproducttype.Name = "cboproducttype"
        Me.cboproducttype.Size = New System.Drawing.Size(102, 21)
        Me.cboproducttype.TabIndex = 5
        '
        'dtpcycledate
        '
        Me.dtpcycledate.Checked = False
        Me.dtpcycledate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpcycledate.Location = New System.Drawing.Point(86, 58)
        Me.dtpcycledate.Name = "dtpcycledate"
        Me.dtpcycledate.ShowCheckBox = True
        Me.dtpcycledate.Size = New System.Drawing.Size(91, 20)
        Me.dtpcycledate.TabIndex = 4
        '
        'lblproducttype
        '
        Me.lblproducttype.AutoSize = True
        Me.lblproducttype.Location = New System.Drawing.Point(185, 62)
        Me.lblproducttype.Name = "lblproducttype"
        Me.lblproducttype.Size = New System.Drawing.Size(71, 13)
        Me.lblproducttype.TabIndex = 123
        Me.lblproducttype.Text = "Product Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 122
        Me.Label3.Text = "Cycle Date"
        '
        'txtsentto
        '
        Me.txtsentto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsentto.Location = New System.Drawing.Point(387, 10)
        Me.txtsentto.MaxLength = 25
        Me.txtsentto.Name = "txtsentto"
        Me.txtsentto.Size = New System.Drawing.Size(118, 20)
        Me.txtsentto.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(338, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 121
        Me.Label2.Text = "Sent to"
        '
        'txtairwaybillno
        '
        Me.txtairwaybillno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtairwaybillno.Location = New System.Drawing.Point(216, 8)
        Me.txtairwaybillno.MaxLength = 25
        Me.txtairwaybillno.Name = "txtairwaybillno"
        Me.txtairwaybillno.Size = New System.Drawing.Size(118, 20)
        Me.txtairwaybillno.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(154, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "Airwaybillno"
        '
        'txtdespatchno
        '
        Me.txtdespatchno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdespatchno.Location = New System.Drawing.Point(86, 8)
        Me.txtdespatchno.MaxLength = 10
        Me.txtdespatchno.Name = "txtdespatchno"
        Me.txtdespatchno.Size = New System.Drawing.Size(60, 20)
        Me.txtdespatchno.TabIndex = 0
        '
        'lbldespatchno
        '
        Me.lbldespatchno.AutoSize = True
        Me.lbldespatchno.Location = New System.Drawing.Point(10, 10)
        Me.lbldespatchno.Name = "lbldespatchno"
        Me.lbldespatchno.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbldespatchno.Size = New System.Drawing.Size(70, 13)
        Me.lbldespatchno.TabIndex = 117
        Me.lbldespatchno.Text = "Despatch No"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(235, 1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(72, 24)
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
        Me.pnlButtons.Location = New System.Drawing.Point(82, 292)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(388, 28)
        Me.pnlButtons.TabIndex = 7
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(313, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(72, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "C&lose"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(157, 1)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(72, 24)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1, 1)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(72, 24)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(79, 1)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(72, 24)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "E&dit"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'txtId
        '
        Me.txtId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtId.Location = New System.Drawing.Point(48, 296)
        Me.txtId.MaxLength = 10
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(18, 20)
        Me.txtId.TabIndex = 4
        Me.txtId.Visible = False
        '
        'frmbatchdespatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(542, 333)
        Me.Controls.Add(Me.pnlSave)
        Me.Controls.Add(Me.dGBatch)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.txtId)
        Me.Name = "frmbatchdespatch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Batch Despatch"
        Me.pnlSave.ResumeLayout(False)
        CType(Me.dGBatch, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents dGBatch As System.Windows.Forms.DataGridView
    Friend WithEvents chkSelect As System.Windows.Forms.CheckBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtdespatchno As System.Windows.Forms.TextBox
    Friend WithEvents lbldespatchno As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents txtsentto As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtairwaybillno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboproducttype As System.Windows.Forms.ComboBox
    Friend WithEvents dtpcycledate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblproducttype As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnload As System.Windows.Forms.Button
End Class
