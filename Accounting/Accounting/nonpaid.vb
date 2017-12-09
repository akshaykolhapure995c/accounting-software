Public Class nonpaid
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Dim i As Integer
    Private Sub nonpaid_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Main.Enabled = True
    End Sub

    Private Sub nonpaid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.LinkLabel1.Text = "CHIRAG" Then
            con.Open("dsn=chirag", Password:="abc")
        End If
        If Me.LinkLabel1.Text = "SURBHI" Then
            con.Open("dsn=surbhi", Password:="abc")
        End If

        cmd.Open("Select name from nonpaid", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            i = 0
            While (i < ComboBox2.Items.Count)
                If (Not ComboBox2.Items.IndexOf(i).ToString = cmd.Fields("name").Value) Then
                    i = i + 1
                End If
            End While

            If (i = ComboBox2.Items.Count) Then
                ComboBox2.Items.Add(cmd.Fields("name").Value)
                ComboBox1.Items.Add(cmd.Fields("name").Value)
            End If
            cmd.MoveNext()
        End While
        cmd.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        cmd.Open("select * from nonpaid  where da_te  >= '" & DateTimePicker2.Value.Date & "' and da_te<= '" & DateTimePicker3.Value.Date & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        i = 0
        While Not cmd.EOF
            i = i + cmd.Fields("amount").Value
            DataGridView1.Rows.Add(cmd.Fields("da_te").Value, cmd.Fields("name").Value, cmd.Fields("amount").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
        TextBox2.Text = i
        TextBox2.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox1.Text = "" Then
            MsgBox("Select Name")
        Else
            DataGridView1.Rows.Clear()
            cmd.Open("select * from nonpaid  where da_te  >= '" & DateTimePicker2.Value.Date & "' and da_te<= '" & DateTimePicker3.Value.Date & "' and name='" & ComboBox1.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            i = 0
            While Not cmd.EOF
                i = i + cmd.Fields("amount").Value
                DataGridView1.Rows.Add(cmd.Fields("da_te").Value, cmd.Fields("name").Value, cmd.Fields("amount").Value)
                cmd.MoveNext()
            End While
            cmd.Close()
            TextBox2.Text = i
            TextBox2.Enabled = False
        End If
        
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox2.Text = "" Or TextBox1.Text = "" Then
            MsgBox("Something Missing")
        Else
            cmd.Open("Select * from nonpaid", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("da_te").Value = DateTimePicker1.Value.Date
            cmd.Fields("name").Value = ComboBox2.Text
            cmd.Fields("amount").Value = TextBox1.Text
            cmd.Update()
            cmd.Close()
            TextBox1.Text = ""
        End If
        
    End Sub
End Class