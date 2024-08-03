Public Class frmchequeentry
    Dim lnPacketGid As Long
    Public Sub New(ByVal PacketGid As Long, ByVal AccNo As String, ByVal MicrCode As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lnPacketGid = PacketGid
        txtaccountno.Text = AccNo
        txtmicrcode.Text = MicrCode
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtchequeno.Text = Format(Val(txtchequeno.Text) + 1, "000000")
        txtchequegid.Text = ""
        btnadd.Text = "Add"
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim lssql As String
        Dim liresult As Integer
        Dim lsiscts As Char
        Dim lsismicr As Char

        If txtchequeno.Text.Trim = "" Then
            MsgBox("Please Enter Cheque No..!", MsgBoxStyle.Critical, gProjectName)
            txtchequeno.Focus()
            Exit Sub
        End If

        If txtaccountno.Text.Trim = "" Then
            MsgBox("Please Enter Account No..!", MsgBoxStyle.Critical, gProjectName)
            txtaccountno.Focus()
            Exit Sub
        End If

        If txtmicrcode.Text.Trim = "" Then
            MsgBox("Please Enter Micr Code..!", MsgBoxStyle.Critical, gProjectName)
            txtmicrcode.Focus()
            Exit Sub
        End If

        If txtmicrcode.Text.Trim.Length <> 9 Then
            MsgBox("Please Enter Valid Micr Code..!", MsgBoxStyle.Critical, gProjectName)
            txtmicrcode.Focus()
            Exit Sub
        End If

        If txtbankcode.Text.Trim = "" Then
            MsgBox("Please Enter Bank Code..!", MsgBoxStyle.Critical, gProjectName)
            txtbankcode.Focus()
            Exit Sub
        End If

        If txtbankName.Text.Trim = "" Then
            MsgBox("Please Enter Bank Name..!", MsgBoxStyle.Critical, gProjectName)
            txtbankName.Focus()
            Exit Sub
        End If

        If cbospdcbranch.Text.Trim = "" Then
            MsgBox("Please Enter Bank Branch Name..!", MsgBoxStyle.Critical, gProjectName)
            cbospdcbranch.Focus()
            Exit Sub
        End If

        If cbocts.Text = "" Then
            MsgBox("Please Select CTS Cheque or not..!", MsgBoxStyle.Critical, gProjectName)
            Exit Sub
        End If

        If cbocts.Text.ToUpper = "YES" Then
            lsiscts = "Y"
        Else
            lsiscts = "N"
        End If

        If txtmicrcode.Text.Trim <> "" Then
            lsismicr = "Y"
        Else
            lsismicr = "N"
        End If

        For i As Integer = 0 To dgvsummary.RowCount - 1
            If dgvsummary.Rows(i).Cells("Cheque No").Value.ToString = txtchequeno.Text.Trim Then
                MsgBox("Duplicate Cheque No..!", MsgBoxStyle.Critical, gProjectName)
                Exit Sub
            End If
        Next

        If Val(txtchequegid.Text) = 0 Then
            lssql = ""
            lssql &= " insert into chola_trn_tspdcchqentry ("
            lssql &= " chqentry_packet_gid,chqentry_micrcode,"
            lssql &= " chqentry_bankcode,chqentry_bankname,chqentry_branchname,"
            lssql &= " chqentry_accno,chqentry_chqno,chqentry_iscts,chqentry_ismicr,"
            lssql &= " chqentry_entryby,chqentry_entryon ) "
            lssql &= " values ("
            lssql &= "" & lnPacketGid & ","
            lssql &= "'" & txtmicrcode.Text.Trim & "',"
            lssql &= "'" & txtbankcode.Text.Trim & "',"
            lssql &= "'" & txtbankName.Text.Trim & "',"
            lssql &= "'" & cbospdcbranch.Text.Trim & "',"
            lssql &= "'" & txtaccountno.Text.Trim & "',"
            lssql &= "'" & txtchequeno.Text.Trim & "',"
            lssql &= "'" & lsiscts & "',"
            lssql &= "'" & lsismicr & "',"
            lssql &= "'" & gUserName & "',"
            lssql &= "sysdate())"
        Else
            lssql = ""
            lssql &= " update chola_trn_tspdcchqentry set "
            lssql &= " chqentry_micrcode='" & txtmicrcode.Text.Trim & "',"
            lssql &= " chqentry_bankcode='" & txtbankcode.Text.Trim & "',"
            lssql &= " chqentry_bankname='" & txtbankName.Text.Trim & "',"
            lssql &= " chqentry_branchname='" & cbospdcbranch.Text.Trim & "',"
            lssql &= " chqentry_accno='" & txtaccountno.Text.Trim & "',"
            lssql &= " chqentry_chqno='" & txtchequeno.Text.Trim & "',"
            lssql &= " chqentry_iscts='" & lsiscts & "',"
            lssql &= " chqentry_ismicr='" & lsismicr & "',"
            lssql &= " chqentry_entryby='" & gUserName & "',"
            lssql &= " chqentry_entryon=sysdate() "
            lssql &= " where chqentry_gid=" & Val(txtchequegid.Text)
        End If

        liresult = gfInsertQry(lssql, gOdbcConn)

        If liresult = 0 Then
            MsgBox("Some Error occurred", MsgBoxStyle.Critical, gProjectName)
        Else
            MsgBox("Sucessfully Added", MsgBoxStyle.Information, gProjectName)
            btnclear.PerformClick()
            FillGrid()
            txtchequeno.Focus()
        End If
    End Sub

    Private Sub txtchequeno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchequeno.GotFocus
        txtchequeno.SelectAll()
    End Sub

    Private Sub txtchequeno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtchequeno.KeyPress
        e.Handled = gfIntEntryOnly(e)
    End Sub

    Private Sub frmchequeentry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub FillGrid()
        Dim lssql As String

        lssql = " set @slno:=0 "
        gfInsertQry(lssql, gOdbcConn)

        lssql = ""
        lssql &= " select chqentry_gid,@slno := @slno + 1 as 'SL No',chqentry_chqno as 'Cheque No',chqentry_micrcode as 'Micr Code',"
        lssql &= " chqentry_accno as 'Account No',"
        lssql &= " chqentry_bankcode as 'Bank Code',chqentry_bankname as 'Bank Name',chqentry_branchname as 'Bank Branch Name',"
        lssql &= " if(chqentry_iscts='Y','CTS','NONCTS') as 'Cheque Type' "
        lssql &= " from chola_trn_tspdcchqentry "
        lssql &= " where chqentry_packet_gid = " & lnPacketGid

        gpPopGridView(dgvsummary, lssql, gOdbcConn)
        dgvsummary.Columns(0).Visible = False
    End Sub


    Private Sub frmchequeentry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lssql As String

        Call FillGrid()

        lssql = ""
        lssql &= " select city_micrcode,city_name "
        lssql &= " from chola_mst_tcity "
        lssql &= " order by city_name "
        gpBindCombo(lssql, "city_name", "city_micrcode", cbospdcbranch, gOdbcConn)

        txtchequeno.Focus()
    End Sub


    Private Sub dgvsummary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvsummary.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        txtchequegid.Text = dgvsummary.Rows(e.RowIndex).Cells("chqentry_gid").Value.ToString
        txtmicrcode.Text = dgvsummary.Rows(e.RowIndex).Cells("Micr Code").Value.ToString
        txtchequeno.Text = dgvsummary.Rows(e.RowIndex).Cells("Cheque No").Value.ToString
        txtaccountno.Text = dgvsummary.Rows(e.RowIndex).Cells("Account No").Value.ToString
        txtbankcode.Text = dgvsummary.Rows(e.RowIndex).Cells("Bank Code").Value.ToString
        txtbankName.Text = dgvsummary.Rows(e.RowIndex).Cells("Bank Name").Value.ToString
        cbospdcbranch.Text = dgvsummary.Rows(e.RowIndex).Cells("Bank Branch Name").Value.ToString

        If dgvsummary.Rows(e.RowIndex).Cells("Cheque Type").Value.ToString = "CTS" Then
            cbocts.SelectedIndex = 0
        Else
            cbocts.SelectedIndex = 1
        End If
        btnadd.Text = "Update"
        txtchequeno.Focus()
    End Sub
    Private Sub txtmicrcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmicrcode.LostFocus
        Dim lssql As String

        Dim drbank As Odbc.OdbcDataReader
        If txtmicrcode.Text.Trim <> "" Then

            If txtmicrcode.Text.Trim.Length <> 9 Then
                MsgBox("Invalid MICR Code..!", MsgBoxStyle.Critical, gProjectName)
                txtmicrcode.Focus()
                Exit Sub
            End If

            'Bank Name and Code
            lssql = ""
            lssql &= " select bank_bankcode,bank_bankname "
            lssql &= " from chola_mst_tbank "
            lssql &= " where 1=1 "
            lssql &= " and bank_micrcode='" & Mid(txtmicrcode.Text.Trim, 4, 3) & "'"
            drbank = gfExecuteQry(lssql, gOdbcConn)

            If drbank.HasRows Then
                drbank.Read()
                txtbankcode.Text = drbank.Item("bank_bankcode").ToString
                txtbankName.Text = drbank.Item("bank_bankname").ToString
            End If

            'Bank Branch
            lssql = ""
            lssql &= " select city_micrcode "
            lssql &= " from chola_mst_tcity "
            lssql &= " where city_micrcode='" & Microsoft.VisualBasic.Left(txtmicrcode.Text.Trim, 3) & "'"
            cbospdcbranch.SelectedValue = gfExecuteScalar(lssql, gOdbcConn)
        End If
    End Sub

    Private Sub txtbankcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbankcode.LostFocus
        If txtbankcode.Text.ToUpper.Trim = "XXX" Then
            txtbankName.Enabled = True
            txtbankName.Focus()
        Else
            txtbankName.Enabled = False
        End If
    End Sub
    Private Sub FillCombo()
        Dim lssql As String

        lssql = ""
        lssql &= " select city_micrcode,city_name "
        lssql &= " from chola_mst_tcity "
        lssql &= " order by city_name "
        gpBindCombo(lssql, "city_name", "city_micrcode", cbospdcbranch, gOdbcConn)
    End Sub

    Private Sub txtchequeno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtchequeno.TextChanged

    End Sub

    Private Sub txtmicrcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmicrcode.TextChanged

    End Sub
End Class