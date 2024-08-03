<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmupdatepacket
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.txtgnsarefno = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvPdc = New System.Windows.Forms.DataGridView()
        Me.dgvSpdc = New System.Windows.Forms.DataGridView()
        Me.dgvEcs = New System.Windows.Forms.DataGridView()
        Me.grpMain.SuspendLayout()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvEcs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.btnclear)
        Me.grpMain.Controls.Add(Me.btnrefresh)
        Me.grpMain.Controls.Add(Me.btnclose)
        Me.grpMain.Controls.Add(Me.txtgnsarefno)
        Me.grpMain.Controls.Add(Me.Label1)
        Me.grpMain.Location = New System.Drawing.Point(9, 0)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(604, 57)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        '
        'btnclear
        '
        Me.btnclear.Location = New System.Drawing.Point(360, 19)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(87, 23)
        Me.btnclear.TabIndex = 2
        Me.btnclear.Text = "&Clear"
        Me.btnclear.UseVisualStyleBackColor = True
        '
        'btnrefresh
        '
        Me.btnrefresh.Location = New System.Drawing.Point(267, 19)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(87, 23)
        Me.btnrefresh.TabIndex = 1
        Me.btnrefresh.Text = "&Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = True
        '
        'btnclose
        '
        Me.btnclose.Location = New System.Drawing.Point(454, 20)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(87, 23)
        Me.btnclose.TabIndex = 3
        Me.btnclose.Text = "C&lose"
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'txtgnsarefno
        '
        Me.txtgnsarefno.Location = New System.Drawing.Point(103, 20)
        Me.txtgnsarefno.Name = "txtgnsarefno"
        Me.txtgnsarefno.Size = New System.Drawing.Size(157, 21)
        Me.txtgnsarefno.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "GNSA REF#"
        '
        'dgvPdc
        '
        Me.dgvPdc.AllowUserToAddRows = False
        Me.dgvPdc.AllowUserToDeleteRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        Me.dgvPdc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvPdc.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPdc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvPdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPdc.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvPdc.Location = New System.Drawing.Point(12, 63)
        Me.dgvPdc.Name = "dgvPdc"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPdc.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvPdc.RowHeadersVisible = False
        Me.dgvPdc.Size = New System.Drawing.Size(600, 131)
        Me.dgvPdc.TabIndex = 1
        '
        'dgvSpdc
        '
        Me.dgvSpdc.AllowUserToAddRows = False
        Me.dgvSpdc.AllowUserToDeleteRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black
        Me.dgvSpdc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.dgvSpdc.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSpdc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.dgvSpdc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSpdc.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvSpdc.Location = New System.Drawing.Point(12, 200)
        Me.dgvSpdc.Name = "dgvSpdc"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSpdc.RowHeadersDefaultCellStyle = DataGridViewCellStyle15
        Me.dgvSpdc.RowHeadersVisible = False
        Me.dgvSpdc.Size = New System.Drawing.Size(600, 131)
        Me.dgvSpdc.TabIndex = 3
        '
        'dgvEcs
        '
        Me.dgvEcs.AllowUserToAddRows = False
        Me.dgvEcs.AllowUserToDeleteRows = False
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black
        Me.dgvEcs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvEcs.BackgroundColor = System.Drawing.SystemColors.Menu
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvEcs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.dgvEcs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEcs.GridColor = System.Drawing.SystemColors.Desktop
        Me.dgvEcs.Location = New System.Drawing.Point(13, 337)
        Me.dgvEcs.Name = "dgvEcs"
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvEcs.RowHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgvEcs.RowHeadersVisible = False
        Me.dgvEcs.Size = New System.Drawing.Size(600, 131)
        Me.dgvEcs.TabIndex = 4
        '
        'frmupdatepacket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(625, 589)
        Me.Controls.Add(Me.dgvEcs)
        Me.Controls.Add(Me.dgvSpdc)
        Me.Controls.Add(Me.dgvPdc)
        Me.Controls.Add(Me.grpMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmupdatepacket"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Packet Update"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        CType(Me.dgvPdc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSpdc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvEcs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents dgvPdc As System.Windows.Forms.DataGridView
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents txtgnsarefno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvSpdc As System.Windows.Forms.DataGridView
    Friend WithEvents dgvEcs As System.Windows.Forms.DataGridView
End Class
