Public Class accounts
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Dim i As Integer
    Private Sub accounts_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        banking.Enabled = True
    End Sub

    Private Sub accounts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        i = 0
        con.Open("dsn=surbhi", Password:="abc")
        cmd.Open("Select * from bank ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            ComboBox1.Items.Add(cmd.Fields("bank_name").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Bank Name or Account Number Missing")
        Else
            bank.LinkLabel1.Text = ComboBox1.Text
            bank.TextBox3.Text = ComboBox2.Text
            bank.TextBox3.Enabled = False
            bank.Show()
            Me.Enabled = False
        End If
       
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox2.Items.Clear()
        cmd.Open("Select account_no from bank where bank_name='" & ComboBox1.Text & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            ComboBox2.Items.Add(cmd.Fields("account_no").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
    End Sub
End Class