Public Class labour
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Dim x As Integer
    Private Sub labour_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Main.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Something Missing")
        Else
            cmd.Open("Select * from labour ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("labour_name").Value = TextBox1.Text
            cmd.Fields("number").Value = TextBox2.Text
            cmd.Update()
            cmd.Close()
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If
        
    End Sub

    Private Sub labour_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.LinkLabel1.Text = "CHIRAG" Then
            con.Open("dsn=chirag", Password:="abc")
        End If
        If Me.LinkLabel1.Text = "SURBHI" Then
            con.Open("dsn=surbhi", Password:="abc")
        End If
        cmd.Open("Select * from labour", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            ComboBox1.Items.Add(cmd.Fields("labour_name").Value)
            ComboBox2.Items.Add(cmd.Fields("labour_name").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Something Missing")
        Else
            DataGridView1.Rows.Add(DateTimePicker1.Value.Date, ComboBox1.Text, TextBox3.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text)
            cmd.Open("Select * from [" & DateTimePicker1.Value.Month & "] ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("labour_name").Value = ComboBox1.Text
            cmd.Fields("labour_sal").Value = TextBox3.Text
            cmd.Fields("advance_taken").Value = TextBox5.Text
            cmd.Fields("advance_paidback").Value = TextBox6.Text
            cmd.Fields("advance_remain").Value = TextBox7.Text
            cmd.Fields("date_").Value = DateTimePicker1.Value.Date
            cmd.Update()
            cmd.Close()
            
        End If
        TextBox4.Enabled = True
        TextBox7.Enabled = True
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(2).Value
        Next
        TextBox8.Text = x
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(3).Value
        Next
        TextBox9.Text = x
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(4).Value
        Next
        TextBox10.Text = x
        x = 0
        TextBox11.Text = Val(TextBox9.Text) - Val(TextBox10.Text)
    End Sub

    Private Sub TextBox3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.LostFocus
        If DateTimePicker1.Value.Day = "1" Then
            cmd.Open("select advance_remain from [" & Date.Today.AddMonths(-1).Month & "]  where labour_name='" & ComboBox1.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While (Not cmd.EOF)
                TextBox4.Text = cmd.Fields("advance_remain").Value
                cmd.MoveNext()
            End While
            TextBox4.Enabled = False
            cmd.Close()
        Else
            cmd.Open("select advance_remain from [" & DateTimePicker1.Value.Month & "]  where labour_name='" & ComboBox1.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While (Not cmd.EOF)
                TextBox4.Text = cmd.Fields("advance_remain").Value
                cmd.MoveNext()
            End While
            TextBox4.Enabled = False
            cmd.Close()
        End If
    End Sub

    Private Sub TextBox6_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.LostFocus
        TextBox7.Text = (Val(TextBox4.Text) + Val(TextBox5.Text)) - Val(TextBox6.Text)
        TextBox7.Enabled = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        
        DataGridView1.Rows.Clear()
        cmd.Open("select * from [" & DateTimePicker2.Value.Month & "]  where date_  >= '" & DateTimePicker2.Value.Date & "' and date_<= '" & DateTimePicker3.Value.Date & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While (Not cmd.EOF)
            DataGridView1.Rows.Add(cmd.Fields("date_").Value, cmd.Fields("labour_name").Value, cmd.Fields("labour_sal").Value, cmd.Fields("advance_taken").Value, cmd.Fields("advance_paidback").Value, cmd.Fields("advance_remain").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(2).Value
        Next
        TextBox8.Text = x
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(3).Value
        Next
        TextBox9.Text = x
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(4).Value
        Next
        TextBox10.Text = x
        x = 0
         TextBox11.Text = Val(TextBox9.Text) - Val(TextBox10.Text)
        
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ComboBox2.Text = "" Then
            MsgBox("Select labour name")
        Else
            DataGridView1.Rows.Clear()
            cmd.Open("select * from [" & DateTimePicker2.Value.Month & "]  where date_  >= '" & DateTimePicker2.Value.Date & "' and date_<= '" & DateTimePicker3.Value.Date & "' and labour_name='" & ComboBox2.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While (Not cmd.EOF)
                DataGridView1.Rows.Add(cmd.Fields("date_").Value, cmd.Fields("labour_name").Value, cmd.Fields("labour_sal").Value, cmd.Fields("advance_taken").Value, cmd.Fields("advance_paidback").Value, cmd.Fields("advance_remain").Value)
                cmd.MoveNext()
            End While
            cmd.Close()
        End If
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(2).Value
        Next
        TextBox8.Text = x
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(3).Value
        Next
        TextBox9.Text = x
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(4).Value
        Next
        TextBox10.Text = x
        x = 0
        
        TextBox11.Text = Val(TextBox9.Text) - Val(TextBox10.Text)
    End Sub
End Class