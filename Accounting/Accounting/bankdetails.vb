Public Class bankdetails
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Bank Name or Account Number Missing")
        Else
            cmd.Open("Select * from bank ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("bank_name").Value = TextBox1.Text
            cmd.Fields("account_no").Value = TextBox2.Text
            cmd.Update()
            cmd.Close()
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If
        
    End Sub

    Private Sub bankdetails_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        banking.Enabled = True
    End Sub

    Private Sub bankdetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.Open("dsn=surbhi", Password:="abc")
    End Sub
End Class