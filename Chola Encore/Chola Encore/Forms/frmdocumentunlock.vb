Public Class frmdocumentunlock
    Dim lsfiletype As String = ""
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal FileType As String)
        lsfiletype = FileType
        InitializeComponent()
    End Sub

    Private Sub frmdocumentunlock_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If dgvsummary.RowCount > 0 Then Call LoadData()
    End Sub

    Private Sub frmdocumentunlock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmdocumentunlock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.KeyPreview = True
        MyBase.WindowState = FormWindowState.Maximized
        txtproposalno.Focus()
        txtproposalno.Text = ""
    End Sub

    Private Sub frmdocumentunlock_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        With dgvsummary
            pnlMain.Width = Me.Width - 30
            pnlMain.Height = Me.Height - 140
            .Height = pnlMain.Height - 10
            .Width = pnlMain.Width - 10
        End With
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Call LoadData()
        If dgvsummary.Rows.Count <= 1 Then
            MsgBox("No records found", MsgBoxStyle.OkOnly, gProjectName)
            Exit Sub
        End If
    End Sub

    Private Sub LoadData()
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lsCond As String = ""

        Dim i As Integer

      
        If lsfiletype = "PDC" Then
            lsSql = " select tf.pdc_gid,pdc_gnsarefno as 'GNSAREF#',pdc_parentcontractno as 'Agreement No',pdc_draweename as 'Drawee Name' " & _
                " from chola_trn_tpdcfile tf  where file_lock='Y' "

            If txtproposalno.Text <> "" Then
                lsSql &= " and pdc_parentcontractno='" & txtproposalno.Text & "'"
            End If

            lsSql &= " group by pdc_gnsarefno"
        Else

            lsSql = " select tf.spdc_gid,spdc_gnsarefno as 'GNSAREF#',spdc_agreementno as 'Agreement No'" & _
                " from chola_trn_tspdc tf where file_lock='Y' "

            If txtproposalno.Text <> "" Then
                lsSql &= " and spdc_agreementno='" & txtproposalno.Text & "'"
            End If

            lsSql &= " group by spdc_gnsarefno"
        End If
        

            With dgvsummary
                .Columns.Clear()

                gpPopGridView(dgvsummary, lsSql, gOdbcConn)

                For i = 0 To .Columns.Count - 1
                    .Columns(i).ReadOnly = True
                    .Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                Next i
                .Columns(0).Visible = False
            lbltotrec.Text = "Total Records : " & (.RowCount).ToString
            End With

    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtproposalno.Text = ""
        dgvsummary.DataSource = Nothing
        lbltotrec.Text = ""
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If MsgBox("Do you want to Close?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If dgvsummary.Rows.Count <= 0 Then
            Exit Sub
        End If
        Call PrintDGridXML(dgvsummary, gsReportPath & "\Summary.xls", "Summary")
    End Sub

    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        Dim lsSql As String = ""
        If MsgBox("Are you Sure Want to Unlock..?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, gProjectName) = MsgBoxResult.No Then
            Exit Sub
        End If

        If lsfiletype = "SPDC" Then
            lsSql = ""
            lsSql &= " UPDATE"
            lsSql &= " chola_trn_tspdc"
            lsSql &= " set"
            lsSql &= " file_lock='N',"
            lsSql &= " lock_by=null"
            lsSql &= " where 1=1"
            lsSql &= " and spdc_gid='" & dgvsummary.Rows(e.RowIndex).Cells(0).Value & "'"

            gfExecuteQry(lsSql, gOdbcConn)
        ElseIf lsfiletype = "PDC" Then
            lsSql = ""
            lsSql &= " UPDATE"
            lsSql &= " chola_trn_tpdcfile"
            lsSql &= " set"
            lsSql &= " file_lock='N',"
            lsSql &= " lock_by=null"
            lsSql &= " where 1=1"
            lsSql &= " and pdc_parentcontractno='" & dgvsummary.Rows(e.RowIndex).Cells(1).Value & "'"

            gfExecuteQry(lsSql, gOdbcConn)
        End If
        txtproposalno.Text = ""
        LoadData()
    End Sub

    Private Sub txtproposalno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtproposalno.KeyPress
        e.Handled = gfIntstrEntryOnly(e)
    End Sub

    Private Sub dgvsummary_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellContentClick

    End Sub
End Class