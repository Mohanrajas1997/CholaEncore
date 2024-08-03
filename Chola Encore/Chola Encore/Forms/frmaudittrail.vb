Public Class frmaudittrail
    Dim lsgnsaref As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal GNSAREF As String)
        lsgnsaref = GNSAREF
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub frmaudittrail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lssql As String

        lssql = " select status_desc as 'Status',transaction_transactionby as 'Transaction By',transaction_transactionon as 'Transaction on' from chloa_trn_ttransaction "
        lssql &= " inner join chola_mst_tstatus on status_flag=transaction_statusflag and status_level='Cheque' "
        lssql &= " where transaction_gnsarefno='" & lsgnsaref & "'"
        lssql &= " group by status_desc "

        dgvsummary.DataSource = GetDataTable(lssql)

    End Sub
End Class