Option Strict On
Option Explicit On

Public Class Search
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal cnstring As String, ByVal sqlstr As String, ByVal rawfld As String, ByVal cond As String)
        MyBase.New()

        cnstr = cnstring
        sql = sqlstr
        rawFldName = rawfld
        condition = cond

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        FillCombo()
        txt = 0
        RefreshGrid(sql & " where " & cond)
    End Sub

    Public Sub New(ByVal cn As Odbc.OdbcConnection, ByVal sqlstr As String, ByVal rawfld As String, ByVal cond As String)
        MyBase.New()

        con = cn
        sql = sqlstr
        rawFldName = rawfld
        condition = cond

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        FillCombo()
        txt = 0
        RefreshGrid(sql & " where " & cond)
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents cboFld As System.Windows.Forms.ComboBox
    Friend WithEvents cboCondition As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlInput As System.Windows.Forms.Panel
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlDecision As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents lblFld As System.Windows.Forms.Label
    Friend WithEvents txtTotRec As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlInput = New System.Windows.Forms.Panel
        Me.lblFld = New System.Windows.Forms.Label
        Me.btnExport = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cboCondition = New System.Windows.Forms.ComboBox
        Me.cboFld = New System.Windows.Forms.ComboBox
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel
        Me.pnlDecision = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.dgvReport = New System.Windows.Forms.DataGridView
        Me.txtTotRec = New System.Windows.Forms.TextBox
        Me.pnlInput.SuspendLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDecision.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlInput
        '
        Me.pnlInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInput.Controls.Add(Me.lblFld)
        Me.pnlInput.Controls.Add(Me.btnExport)
        Me.pnlInput.Controls.Add(Me.btnSearch)
        Me.pnlInput.Controls.Add(Me.txtSearch)
        Me.pnlInput.Controls.Add(Me.cboCondition)
        Me.pnlInput.Controls.Add(Me.cboFld)
        Me.pnlInput.Location = New System.Drawing.Point(10, 5)
        Me.pnlInput.Name = "pnlInput"
        Me.pnlInput.Size = New System.Drawing.Size(715, 51)
        Me.pnlInput.TabIndex = 0
        '
        'lblFld
        '
        Me.lblFld.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFld.AutoSize = True
        Me.lblFld.Location = New System.Drawing.Point(16, 16)
        Me.lblFld.Name = "lblFld"
        Me.lblFld.Size = New System.Drawing.Size(63, 13)
        Me.lblFld.TabIndex = 17
        Me.lblFld.Text = "Search by"
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(630, 13)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(72, 24)
        Me.btnExport.TabIndex = 5
        Me.btnExport.Text = "Export"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(552, 13)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(72, 24)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "&Search"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(326, 13)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(210, 21)
        Me.txtSearch.TabIndex = 3
        '
        'cboCondition
        '
        Me.cboCondition.Location = New System.Drawing.Point(232, 13)
        Me.cboCondition.Name = "cboCondition"
        Me.cboCondition.Size = New System.Drawing.Size(85, 21)
        Me.cboCondition.TabIndex = 2
        '
        'cboFld
        '
        Me.cboFld.Location = New System.Drawing.Point(80, 13)
        Me.cboFld.Name = "cboFld"
        Me.cboFld.Size = New System.Drawing.Size(144, 21)
        Me.cboFld.TabIndex = 1
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        '
        'pnlDecision
        '
        Me.pnlDecision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDecision.Controls.Add(Me.btnCancel)
        Me.pnlDecision.Controls.Add(Me.btnOk)
        Me.pnlDecision.Location = New System.Drawing.Point(462, 392)
        Me.pnlDecision.Name = "pnlDecision"
        Me.pnlDecision.Size = New System.Drawing.Size(173, 36)
        Me.pnlDecision.TabIndex = 7
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(90, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(12, 5)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(72, 24)
        Me.btnOk.TabIndex = 8
        Me.btnOk.Text = "&Ok"
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.Location = New System.Drawing.Point(54, 106)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.Size = New System.Drawing.Size(634, 119)
        Me.dgvReport.TabIndex = 6
        '
        'txtTotRec
        '
        Me.txtTotRec.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtTotRec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotRec.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotRec.Location = New System.Drawing.Point(55, 341)
        Me.txtTotRec.Name = "txtTotRec"
        Me.txtTotRec.ReadOnly = True
        Me.txtTotRec.Size = New System.Drawing.Size(222, 14)
        Me.txtTotRec.TabIndex = 17
        Me.txtTotRec.TabStop = False
        Me.txtTotRec.Text = "Total Records : "
        '
        'Search
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(734, 437)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlDecision)
        Me.Controls.Add(Me.pnlInput)
        Me.Controls.Add(Me.txtTotRec)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Search"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search"
        Me.pnlInput.ResumeLayout(False)
        Me.pnlInput.PerformLayout()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDecision.ResumeLayout(False)
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Dim cnstr As String
    Dim sql As String
    Dim rawFldName As String
    Dim condition As String
    Dim con As Odbc.OdbcConnection

    Private Sub FillCombo()
        ' Fill Condition
        With cboCondition
            .Items.Clear()
            .Items.Add("Like")
            .Items.Add("Not Like")
            .Items.Add("=")
            .Items.Add(">")
            .Items.Add(">=")
            .Items.Add("<")
            .Items.Add("<=")
            .Items.Add("<>")
            .SelectedIndex = 0
        End With
    End Sub

    'Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
    '    With dgvReport
    '        pnlInput.Top = 12
    '        pnlInput.Left = 12

    '        .Left = 12

    '        .Top = Math.Abs(pnlInput.Top + pnlInput.Height + 14)
    '        .Height = Math.Abs(Me.Height - .Top - 65)
    '        .Width = Math.Abs(Me.Width - 24)
    '    End With
    'End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim cond As String = " where " & condition
        Dim fld() As String = Split(rawFldName, ",")

        txtSearch.Text = txtSearch.Text.Trim()

        If cboFld.Text <> "" And txtSearch.Text <> "" And cboCondition.Text <> "" Then
            If condition = "" Then
                cond = " where " & fld(cboFld.Items.IndexOf(cboFld.Text)) & " "
            Else
                cond &= " and " & fld(cboFld.Items.IndexOf(cboFld.Text)) & " "
            End If

            Select Case UCase(cboCondition.Text)
                Case "LIKE", "NOT LIKE"
                    cond = cond & " " & cboCondition.Text & " '" & txtSearch.Text & "%' "
                Case ""
                    cond = ""
                Case Else
                    cond = cond & " " & cboCondition.Text & " '" & txtSearch.Text & "' "
            End Select
        End If

        RefreshGrid(sql & cond)
    End Sub

    Private Sub RefreshGrid(ByVal sqlstr As String)
        Dim cn As Odbc.OdbcConnection
        Dim cmd As New Odbc.OdbcCommand
        Dim adp As New Odbc.OdbcDataAdapter
        Dim ds As New DataSet

        If con.State = ConnectionState.Open Then
            cn = con
        ElseIf cnstr <> "" Then
            cn = New Odbc.OdbcConnection(cnstr)
            cn.Open()
        Else
            Exit Sub
        End If

        gpDataSet(sqlstr, "search", cn, ds)
        dgvReport.DataSource = ds.Tables("search")

        txtTotRec.Text = "Total Records : " & dgvReport.Rows.Count

        ' Add column name
        If cboFld.Items.Count = 0 Then
            For i As Integer = 0 To ds.Tables("search").Columns.Count - 1
                cboFld.Items.Add(ds.Tables("search").Columns(i).ColumnName)
            Next

            cboFld.SelectedIndex = 0
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        btnSearch.PerformClick()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim i As Integer

        If dgvReport.RowCount > 0 Then
            i = dgvReport.CurrentRow.Index
            txt = Convert.ToInt64(dgvReport.Item(0, i).Value)
            txt = CLng(txt)
        Else
            txt = 0
        End If

        MyBase.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        MyBase.Close()
    End Sub

    Private Sub dgvReport_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvReport.DoubleClick
        btnOk.PerformClick()
    End Sub

    Private Sub dgvReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvReport.KeyDown
        If e.KeyCode = 13 Then
            btnOk.PerformClick()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvReport.Rows.Count > 0 Then
            PrintDGridXML(dgvReport, gsReportPath & "\report.xls", "Report")
        End If
    End Sub

    Private Sub Search_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        pnlInput.Top = 6
        pnlInput.Left = 6

        dgvReport.Top = pnlInput.Top + pnlInput.Height + 6
        dgvReport.Left = 6
        dgvReport.Height = Math.Abs(Me.Height - dgvReport.Top - pnlDecision.Height - 36)
        dgvReport.Width = Me.Width - 18

        pnlDecision.Top = dgvReport.Top + dgvReport.Height + 6
        pnlDecision.Left = Math.Abs(dgvReport.Left + dgvReport.Width \ 2 - pnlDecision.Width \ 2)

        txtTotRec.Top = Math.Abs(pnlDecision.Top + pnlDecision.Height \ 2 - txtTotRec.Height \ 2)
        txtTotRec.Left = 6
    End Sub

    Private Sub Search_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
