Public Class home
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Dim i As Integer
    Private Sub home_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Main.Enabled = True
    End Sub

    Private Sub home_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.Open("dsn=surbhi", Password:="abc")
        cmd.Open("Select * from product", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            ComboBox1.Items.Add(cmd.Fields("product_name").Value)
            ComboBox2.Items.Add(cmd.Fields("product_name").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Something is Missing")
        Else
            cmd.Open("Select * from home", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("da_te").Value = DateTimePicker1.Value.Date
            cmd.Fields("product_name").Value = ComboBox1.Text
            cmd.Fields("quantity").Value = TextBox1.Text
            cmd.Fields("rate").Value = TextBox2.Text
            cmd.Fields("cost").Value = TextBox3.Text
            cmd.Update()
            cmd.Close()
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
        End If

       
    End Sub

    Private Sub TextBox2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.LostFocus
        TextBox3.Text = Val(TextBox1.Text) * Val(TextBox2.Text)
        TextBox3.Enabled = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        cmd.Open("select * from home where da_te  >= '" & DateTimePicker2.Value.Date & "' and da_te<= '" & DateTimePicker3.Value.Date & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        i = 0
        While Not cmd.EOF
            i = i + cmd.Fields("cost").Value
            DataGridView1.Rows.Add(cmd.Fields("da_te").Value, cmd.Fields("product_name").Value, cmd.Fields("quantity").Value, cmd.Fields("cost").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
        TextBox4.Text = i
        TextBox4.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox2.Text = "" Then
            MsgBox("Select Product")
        Else
            DataGridView1.Rows.Clear()
            cmd.Open("select * from home where da_te  >= '" & DateTimePicker2.Value.Date & "' and da_te<= '" & DateTimePicker3.Value.Date & "' and product_name='" & ComboBox2.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            i = 0

            While Not cmd.EOF
                i = i + cmd.Fields("cost").Value
                DataGridView1.Rows.Add(cmd.Fields("da_te").Value, cmd.Fields("product_name").Value, cmd.Fields("quantity").Value, cmd.Fields("cost").Value)
                cmd.MoveNext()
            End While
            cmd.Close()
            TextBox4.Text = i
            TextBox4.Enabled = False
        End If

    End Sub
End Class