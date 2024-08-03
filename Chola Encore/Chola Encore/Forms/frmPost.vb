Imports System.IO
Public Class frmPost
    Dim mnPostFlag As Integer

    Private Sub frmPost_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFrom.Value = Now
        dtpTo.Value = Now

        CancelButton = btnClose
    End Sub

    Public Sub New(ByVal PostFlag As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        mnPostFlag = PostFlag

        Select Case PostFlag
            Case 1
                Me.Text = "Post Finone Auth Date With Packet"
        End Select
    End Sub

    Private Function PostFinoneAuthDateWithPacket() As Long
        Dim i As Integer
        Dim n As Integer = 0
        Dim ds As New DataSet
        Dim lsSql As String
        Dim lnResult As Long

        Dim lnHeadId As Long
        Dim lsAgmtNo As String
        Dim lsShortAgmtNo As String
        Dim lnInwardId As Long
        Dim lnPktId As Long
        Dim lsAuthDate As String

        ' Patch Update
        lsSql = ""
        lsSql &= " update chola_trn_tpdcfilehead "
        lsSql &= " inner join chola_trn_tpacket on packet_gid = head_packet_gid "
        lsSql &= " set packet_status = packet_status | 1024 "
        lsSql &= " where head_packet_gid > 0 "
        lsSql &= " and packet_status & 1024 = 0 "

        lnResult = gfInsertQry(lsSql, gOdbcConn)

        lsSql = ""
        lsSql &= " select head_gid,head_inward_gid,head_systemauthdate,head_shortagreementno,head_agreementno from chola_trn_tpdcfilehead "
        lsSql &= " where head_systemauthdate >= '" & Format(dtpFrom.Value, "yyyy-MM-dd") & "' "
        lsSql &= " and head_systemauthdate < '" & Format(DateAdd(DateInterval.Day, 1, dtpTo.Value), "yyyy-MM-dd") & "' "
        lsSql &= " and head_packet_gid = 0 "
        lsSql &= " and head_agreement_gid > 0 "

        Call gpDataSet(lsSql, "finone", gOdbcConn, ds)

        With ds.Tables("finone")
            For i = 0 To .Rows.Count - 1
                lnHeadId = .Rows(i).Item("head_gid")
                lnInwardId = .Rows(i).Item("head_inward_gid")
                lsAuthDate = Format(.Rows(i).Item("head_systemauthdate"), "yyyy-MM-dd")
                lsAgmtNo = .Rows(i).Item("head_agreementno").ToString
                lsShortAgmtNo = .Rows(i).Item("head_shortagreementno").ToString

                ' Check with inward
                lsSql = ""
                lsSql &= " select inward_gid,inward_packet_gid from chola_trn_tinward "

                If lnInwardId = 0 Then
                    lsSql &= " where inward_agreementno = '" & lsAgmtNo & "' "
                    lsSql &= " and inward_receiveddate >= '" & Format(DateAdd(DateInterval.Day, -30, CDate(lsAuthDate)), "yyyy-MM-dd") & "' "
                    lsSql &= " and inward_receiveddate <= '" & lsAuthDate & "' "
                Else
                    lsSql &= " where inward_gid = " & lnInwardId
                End If

                Call gpDataSet(lsSql, "inward", gOdbcConn, ds)

                If ds.Tables("inward").Rows.Count = 0 And lnInwardId = 0 Then
                    lsSql = ""
                    lsSql &= " select inward_gid,inward_packet_gid from chola_trn_tinward "
                    lsSql &= " where inward_agreementno = '" & lsAgmtNo & "' "
                    lsSql &= " and inward_userauthdate >= '" & Format(DateAdd(DateInterval.Day, -15, CDate(lsAuthDate)), "yyyy-MM-dd") & "' "
                    lsSql &= " and inward_userauthdate <= '" & Format(DateAdd(DateInterval.Day, 30, CDate(lsAuthDate)), "yyyy-MM-dd") & "' "

                    Call gpDataSet(lsSql, "inward", gOdbcConn, ds)
                End If

                If ds.Tables("inward").Rows.Count = 1 Then
                    lnInwardId = ds.Tables("inward").Rows(0).Item("inward_gid")
                    lnPktId = ds.Tables("inward").Rows(0).Item("inward_packet_gid")
                Else
                    lnInwardId = 0
                    lnPktId = 0
                End If

                ds.Tables("inward").Rows.Clear()

                If lnPktId > 0 Then
                    ' Check with packet
                    lsSql = ""
                    lsSql &= " select packet_gid from chola_trn_tpacket "
                    lsSql &= " where packet_gid = " & lnPktId & " "
                    lsSql &= " and packet_status & " & GCPKTAUTHFINONE & " = 0 "

                    lnResult = Val(gfExecuteScalar(lsSql, gOdbcConn))

                    If lnResult > 0 Then
                        ' Packet update
                        lsSql = ""
                        lsSql &= " update chola_trn_tpacket set "
                        lsSql &= " packet_status = packet_status | " & GCPKTAUTHFINONE & " "
                        lsSql &= " where packet_gid = " & lnPktId & " "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)

                        ' Finone update
                        lsSql = ""
                        lsSql &= " update chola_trn_tpdcfilehead set "
                        lsSql &= " head_packet_gid = " & lnPktId & ","
                        lsSql &= " head_inward_gid = " & lnInwardId & " "
                        lsSql &= " where head_gid = " & lnHeadId & " "

                        lnResult = gfInsertQry(lsSql, gOdbcConn)
                        n += 1
                    End If
                End If

                Application.DoEvents()
                frmMain.lblstatus.Text = "Out of " & .Rows.Count & " record(s) reading " & i & " record... " & IIf(n > 0, n & " posted !", "")
            Next i
            .Rows.Clear()
        End With

        Return n
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Dim c As Long = 0

        btnPost.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        frmMain.lblstatus.Text = "Posting records ..."

        Select Case mnPostFlag
            Case 1
                c = PostFinoneAuthDateWithPacket()
        End Select

        MsgBox(c & " record(s) posted successfully !", MsgBoxStyle.Information, gProjectName)

        frmMain.lblstatus.Text = ""
        Me.Cursor = System.Windows.Forms.Cursors.Default
        btnPost.Enabled = True
    End Sub
End Class