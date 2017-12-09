Public Class bank
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Dim x As Integer
    Private Sub bank_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        accounts.Enabled = True
    End Sub

    Private Sub bank_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.Open("dsn=surbhi", Password:="abc")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        cmd.Open("Select * from amount ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        cmd.AddNew()
        cmd.Fields("account_no").Value = TextBox3.Text
        cmd.Fields("da_te").Value = DateTimePicker1.Value.Date
        cmd.Fields("balance").Value = TextBox4.Text
        cmd.Update()
        cmd.Close()
        TextBox4.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        x = 0
        cmd.Open("Select * from amount where account_no='" & TextBox3.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            x = cmd.Fields("balance").Value
            cmd.MoveNext()
        End While
        cmd.Close()

        cmd.Open("Select * from transaction ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        cmd.AddNew()
        cmd.Fields("bank_name").Value = LinkLabel1.Text
        cmd.Fields("account_no").Value = TextBox3.Text
        cmd.Fields("da_te").Value = DateTimePicker2.Value.Date
        If RadioButton1.Checked Then
            cmd.Fields("credit").Value = TextBox2.Text
            cmd.Fields("debit").Value = "-"
            x = x + Val(TextBox2.Text)
        End If
        If RadioButton2.Checked Then
            cmd.Fields("debit").Value = TextBox2.Text
            cmd.Fields("credit").Value = "-"
            x = x - Val(TextBox2.Text)
        End If
        cmd.Fields("balance").Value = x
        cmd.Update()
        cmd.Close()
        TextBox2.Text = ""

        cmd.Open("select * from amount", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        cmd.AddNew()
        cmd.Fields("balance").Value = x
        cmd.Fields("account_no").Value = TextBox3.Text
        cmd.Fields("da_te").Value = DateTimePicker2.Value.Date
        cmd.Update()
        cmd.Close()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        DataGridView1.Rows.Clear()
        cmd.Open("Select * from transaction where account_no='" & TextBox3.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            DataGridView1.Rows.Add(cmd.Fields("da_te").Value, cmd.Fields("credit").Value, cmd.Fields("debit").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
        x = 0
        cmd.Open("Select balance from transaction ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            x = cmd.Fields("balance").Value
            cmd.MoveNext()
        End While
        cmd.Close()
        TextBox1.Text = x
    End Sub
End Class