Public Class product
    Dim con As New ADODB.Connection
    Dim cmd, cmd1 As New ADODB.Recordset
    Dim str, str1, str2 As String
    Dim x, y As Integer
    Private Sub product_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Main.Enabled = True
    End Sub

    Private Sub product_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.LinkLabel1.Text = "CHIRAG" Then
            con.Open("dsn=chirag", Password:="abc")
        End If
        If Me.LinkLabel1.Text = "SURBHI" Then
            con.Open("dsn=surbhi", Password:="abc")
        End If
        cmd.Open("Select * from product ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            ComboBox2.Items.Add(cmd.Fields("product_name").Value)
            ComboBox3.Items.Add(cmd.Fields("product_name").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
        cmd.Open("Select * from firm ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            ComboBox1.Items.Add(cmd.Fields("firm_name").Value)
            ComboBox4.Items.Add(cmd.Fields("firm_name").Value)
            ComboBox5.Items.Add(cmd.Fields("firm_name").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Or ComboBox2.Text = "" Or TextBox1.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Something Missing")
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("Select Paid or Unpaid")
        Else

            cmd.Open("Select * from product_purchase ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("company").Value = ComboBox1.Text
            cmd.Fields("product_name").Value = ComboBox2.Text
            cmd.Fields("quantity").Value = TextBox1.Text
            cmd.Fields("rate_per_unit").Value = TextBox5.Text
            cmd.Fields("total").Value = TextBox6.Text
            cmd.Fields("da_te").Value = DateTimePicker1.Value.Date
            If CheckBox1.Checked = True Then
                cmd.Fields("paid_unpaid").Value = "Paid"
                DataGridView5.Rows.Add(DateTimePicker1.Value.Date, ComboBox1.Text, ComboBox2.Text, TextBox1.Text, TextBox6.Text, "Paid")
            End If
            If CheckBox2.Checked = True Then
                cmd.Fields("paid_unpaid").Value = "UnPaid"
                DataGridView5.Rows.Add(DateTimePicker1.Value.Date, ComboBox1.Text, ComboBox2.Text, TextBox1.Text, TextBox6.Text, "UnPaid")
            End If
            cmd.Update()
            cmd.Close()
            ComboBox2.Text = ""
            TextBox1.Text = ""
            TextBox5.Text = ""
            TextBox6.Enabled = True
            TextBox6.Text = ""
        End If

    End Sub

    Private Sub TextBox5_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox5.LostFocus
        TextBox6.Text = Val(TextBox1.Text) * Val(TextBox5.Text)
        TextBox6.Enabled = False
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        str = "UnPaid"
        DataGridView4.Rows.Clear()
        If ComboBox5.Text = "" Then
            MsgBox("select firm")

        Else
            str1 = DateTimePicker4.Value.Date
            str2 = DateTimePicker5.Value.Date
            x = 0
            y = 0
            DataGridView4.Rows.Clear()
            While (Not (str1 = str2))
                cmd.Open("select * from product_purchase where da_te= '" & str1 & "' and company='" & ComboBox5.Text & "' and paid_unpaid='" & str & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                While Not cmd.EOF
                    x = x + cmd.Fields("total").Value
                    cmd.MoveNext()
                End While
                DataGridView4.Rows.Add(str1, x, "Paid")
                cmd.Close()
                y = y + 1
                str1 = DateTimePicker4.Value.Date.AddDays(y)
                x = 0
            End While
            If (str1 = str2) Then
                cmd.Open("select * from product_purchase where da_te= '" & str1 & "'and company='" & ComboBox5.Text & "' and paid_unpaid='" & str & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
                While Not cmd.EOF
                    x = x + cmd.Fields("total").Value
                    cmd.MoveNext()
                End While
                DataGridView4.Rows.Add(str1, x, "Paid")
                cmd.Close()
            End If
        End If
        
        x = 0
        For i As Integer = 0 To DataGridView4.RowCount - 1
            x = x + DataGridView4.Rows(i).Cells(1).Value
        Next
        TextBox8.Text = x

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        str = "Paid"
        str1 = DateTimePicker2.Value.Date
        str2 = DateTimePicker3.Value.Date
        x = 0
        y = 0
        DataGridView1.Rows.Clear()
        While (Not (str1 = str2))
            
            cmd.Open("select * from product_purchase where da_te= '" & str1 & "' and paid_unpaid='" & str & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While Not cmd.EOF
                x = x + cmd.Fields("total").Value
                cmd.MoveNext()
            End While
            DataGridView1.Rows.Add(str1, x)
            cmd.Close()
            y = y + 1
            str1 = DateTimePicker2.Value.Date.AddDays(y)
            x = 0
        End While
        If (str1 = str2) Then
            cmd.Open("select * from product_purchase where da_te= '" & str1 & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While Not cmd.EOF
                x = x + cmd.Fields("total").Value
                cmd.MoveNext()
            End While
            DataGridView1.Rows.Add(str1, x)
            cmd.Close()
        End If
        x = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            x = x + DataGridView1.Rows(i).Cells(1).Value
        Next
        TextBox2.Text = x
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox3.Text = "" Then
            MsgBox("Select Product")
        Else
            DataGridView2.Rows.Clear()
            cmd.Open("select * from product_purchase where da_te  >= '" & DateTimePicker2.Value.Date & "' and da_te<= '" & DateTimePicker3.Value.Date & "' and product_name= '" & ComboBox3.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While Not cmd.EOF
                DataGridView2.Rows.Add(cmd.Fields("da_te").Value(), cmd.Fields("company").Value(), cmd.Fields("quantity").Value(), cmd.Fields("total").Value())
                cmd.MoveNext()
            End While
            cmd.Close()
        End If
        x = 0
        For i As Integer = 0 To DataGridView2.RowCount - 1
            x = x + DataGridView2.Rows(i).Cells(3).Value
        Next
        TextBox3.Text = x
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        str = "Paid"
        str1 = DateTimePicker2.Value.Date
        str2 = DateTimePicker3.Value.Date
        x = 0
        y = 0
        DataGridView3.Rows.Clear()
        While (Not (str1 = str2))

            cmd.Open("select * from product_purchase where da_te= '" & str1 & "' and paid_unpaid='" & str & "' and company='" & ComboBox4.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While Not cmd.EOF
                x = x + cmd.Fields("total").Value
                cmd.MoveNext()
            End While
            DataGridView3.Rows.Add(str1, x)
            cmd.Close()
            y = y + 1
            str1 = DateTimePicker2.Value.Date.AddDays(y)
            x = 0
        End While
        If (str1 = str2) Then
            cmd.Open("select * from product_purchase where da_te= '" & str1 & "' and paid_unpaid='" & str & "' and company='" & ComboBox4.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While Not cmd.EOF
                x = x + cmd.Fields("total").Value
                cmd.MoveNext()
            End While
            DataGridView3.Rows.Add(str1, x)
            cmd.Close()
        End If
        x = 0
        For i As Integer = 0 To DataGridView3.RowCount - 1
            x = x + DataGridView3.Rows(i).Cells(1).Value
        Next
        TextBox4.Text = x


    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        If DataGridView3.CurrentCell.Selected Then
            cmd.Open("select * from product_purchase where da_te= '" & DataGridView3.CurrentRow.Cells(0).Value & "' and paid_unpaid='" & str & "' and company='" & ComboBox4.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While Not cmd.EOF

                details.DataGridView1.Rows.Add(cmd.Fields("product_name").Value, cmd.Fields("quantity").Value, cmd.Fields("rate_per_unit").Value, cmd.Fields("total").Value)
                cmd.MoveNext()

            End While
            cmd.Close()
            details.Show()
        End If
    End Sub

    Private Sub DataGridView4_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick
        str = "UnPaid"
        If DataGridView4.CurrentCell.Selected Then
            cmd.Open("select * from product_purchase where da_te= '" & DataGridView3.CurrentRow.Cells(0).Value & "' and paid_unpaid='" & str & "' and company='" & ComboBox4.Text & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            While Not cmd.EOF

                details.DataGridView1.Rows.Add(cmd.Fields("product_name").Value, cmd.Fields("quantity").Value, cmd.Fields("rate_per_unit").Value, cmd.Fields("total").Value)
                cmd.MoveNext()

            End While
            cmd.Close()
            details.Show()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        DataGridView5.Rows.Clear()
        cmd.Open("select * from product_purchase where da_te= '" & DateTimePicker6.Value.Date & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            DataGridView5.Rows.Add(cmd.Fields("da_te").Value, cmd.Fields("company").Value, cmd.Fields("product_name").Value, cmd.Fields("quantity").Value, cmd.Fields("total").Value, cmd.Fields("paid_unpaid").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
        x = 0
        For i As Integer = 0 To DataGridView5.RowCount - 1
            x = x + DataGridView5.Rows(i).Cells(4).Value
        Next
        TextBox7.Text = x
    End Sub
End Class