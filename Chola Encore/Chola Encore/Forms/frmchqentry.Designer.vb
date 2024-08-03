<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmchqentry
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvEntry = New System.Windows.Forms.DataGridView()
        Me.gbentry = New System.Windows.Forms.GroupBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPdcId = New System.Windows.Forms.TextBox()
        Me.btncreatebatch = New System.Windows.Forms.Button()
        Me.mtxtchqdate = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtchqno = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtchequeamt = New System.Windows.Forms.TextBox()
        Me.btnsubmit = New System.Windows.Forms.Button()
        Me.cboagreementno = New System.Windows.Forms.ComboBox()
        Me.dgvSummary = New System.Windows.Forms.DataGridView()
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbentry.SuspendLayout()
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvEntry
        '
        Me.dgvEntry.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgvEntry.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvEntry.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvEntry.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntry.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvEntry.Location = New System.Drawing.Point(12, 142)
        Me.dgvEntry.Name = "dgvEntry"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvEntry.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvEntry.RowHeadersVisible = False
        Me.dgvEntry.Size = New System.Drawing.Size(851, 246)
        Me.dgvEntry.TabIndex = 1
        '
        'gbentry
        '
        Me.gbentry.Controls.Add(Me.lblTotal)
        Me.gbentry.Controls.Add(Me.Label2)
        Me.gbentry.Controls.Add(Me.txtPdcId)
        Me.gbentry.Controls.Add(Me.btncreatebatch)
        Me.gbentry.Controls.Add(Me.mtxtchqdate)
        Me.gbentry.Controls.Add(Me.Label1)
        Me.gbentry.Controls.Add(Me.Label7)
        Me.gbentry.Controls.Add(Me.txtchqno)
        Me.gbentry.Controls.Add(Me.Label8)
        Me.gbentry.Controls.Add(Me.Label4)
        Me.gbentry.Controls.Add(Me.txtchequeamt)
        Me.gbentry.Controls.Add(Me.btnsubmit)
        Me.gbentry.Controls.Add(Me.cboagreementno)
        Me.gbentry.Location = New System.Drawing.Point(12, 3)
        Me.gbentry.Name = "gbentry"
        Me.gbentry.Size = New System.Drawing.Size(417, 133)
        Me.gbentry.TabIndex = 0
        Me.gbentry.TabStop = False
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Red
        Me.lblTotal.Location = New System.Drawing.Point(27, 104)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(45, 13)
        Me.lblTotal.TabIndex = 36
        Me.lblTotal.Text = "Total : "
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(245, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Pdc Id"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPdcId
        '
        Me.txtPdcId.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPdcId.Location = New System.Drawing.Point(293, 43)
        Me.txtPdcId.MaxLength = 12
        Me.txtPdcId.Name = "txtPdcId"
        Me.txtPdcId.Size = New System.Drawing.Size(110, 21)
        Me.txtPdcId.TabIndex = 2
        Me.txtPdcId.TabStop = False
        '
        'btncreatebatch
        '
        Me.btncreatebatch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncreatebatch.Location = New System.Drawing.Point(311, 94)
        Me.btncreatebatch.Name = "btncreatebatch"
        Me.btncreatebatch.Size = New System.Drawing.Size(92, 23)
        Me.btncreatebatch.TabIndex = 33
        Me.btncreatebatch.TabStop = False
        Me.btncreatebatch.Text = "&Create Batch"
        Me.btncreatebatch.UseVisualStyleBackColor = True
        '
        'mtxtchqdate
        '
        Me.mtxtchqdate.Location = New System.Drawing.Point(293, 13)
        Me.mtxtchqdate.Mask = "00/00/0000"
        Me.mtxtchqdate.Name = "mtxtchqdate"
        Me.mtxtchqdate.Size = New System.Drawing.Size(110, 20)
        Me.mtxtchqdate.TabIndex = 1
        Me.mtxtchqdate.ValidatingType = GetType(Date)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(208, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Cheque Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "Cheque No"
        '
        'txtchqno
        '
        Me.txtchqno.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchqno.Location = New System.Drawing.Point(78, 13)
        Me.txtchqno.MaxLength = 12
        Me.txtchqno.Name = "txtchqno"
        Me.txtchqno.Size = New System.Drawing.Size(110, 21)
        Me.txtchqno.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(2, 74)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Agreement"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Amount"
        '
        'txtchequeamt
        '
        Me.txtchequeamt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchequeamt.Location = New System.Drawing.Point(78, 43)
        Me.txtchequeamt.MaxLength = 10
        Me.txtchequeamt.Name = "txtchequeamt"
        Me.txtchequeamt.Size = New System.Drawing.Size(110, 21)
        Me.txtchequeamt.TabIndex = 3
        '
        'btnsubmit
        '
        Me.btnsubmit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsubmit.Location = New System.Drawing.Point(236, 94)
        Me.btnsubmit.Name = "btnsubmit"
        Me.btnsubmit.Size = New System.Drawing.Size(69, 23)
        Me.btnsubmit.TabIndex = 5
        Me.btnsubmit.Text = "&Submit"
        Me.btnsubmit.UseVisualStyleBackColor = True
        '
        'cboagreementno
        '
        Me.cboagreementno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboagreementno.FormattingEnabled = True
        Me.cboagreementno.Location = New System.Drawing.Point(78, 70)
        Me.cboagreementno.Name = "cboagreementno"
        Me.cboagreementno.Size = New System.Drawing.Size(325, 21)
        Me.cboagreementno.TabIndex = 4
        '
        'dgvSummary
        '
        Me.dgvSummary.AllowUserToAddRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        Me.dgvSummary.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSummary.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSummary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSummary.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvSummary.Location = New System.Drawing.Point(435, 12)
        Me.dgvSummary.Name = "dgvSummary"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSummary.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvSummary.RowHeadersVisible = False
        Me.dgvSummary.Size = New System.Drawing.Size(428, 124)
        Me.dgvSummary.TabIndex = 2
        '
        'frmchqentry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(870, 394)
        Me.Controls.Add(Me.dgvSummary)
        Me.Controls.Add(Me.gbentry)
        Me.Controls.Add(Me.dgvEntry)
        Me.KeyPreview = True
        Me.Name = "frmchqentry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cheque Entry"
        CType(Me.dgvEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbentry.ResumeLayout(False)
        Me.gbentry.PerformLayout()
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvEntry As System.Windows.Forms.DataGridView
    Friend WithEvents gbentry As System.Windows.Forms.GroupBox
    Friend WithEvents mtxtchqdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtchqno As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtchequeamt As System.Windows.Forms.TextBox
    Friend WithEvents btnsubmit As System.Windows.Forms.Button
    Friend WithEvents cboagreementno As System.Windows.Forms.ComboBox
    Friend WithEvents btncreatebatch As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPdcId As System.Windows.Forms.TextBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents dgvSummary As System.Windows.Forms.DataGridView
End Class
