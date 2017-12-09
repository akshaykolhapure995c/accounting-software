Public Class sale
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Dim a, b, c, d, f, g, amt As Integer
    Dim s As String
    Private Sub sale_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Main.Enabled = True
    End Sub

    Private Sub sale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.LinkLabel1.Text = "CHIRAG" Then
            con.Open("dsn=chirag", Password:="abc")
        End If
        If Me.LinkLabel1.Text = "SURBHI" Then
            con.Open("dsn=surbhi", Password:="abc")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Enter Amount")
        Else
            cmd.Open("Select amount from nonpaid where da_te='" & DateTimePicker1.Value.Date & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            a = 0
            While Not cmd.EOF
                a = a + cmd.Fields("amount").Value
                cmd.MoveNext()
            End While
            cmd.Close()

            cmd.Open("Select amount from tax where da_te='" & DateTimePicker1.Value.Date & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            b = 0
            While Not cmd.EOF
                b = b + cmd.Fields("amount").Value
                cmd.MoveNext()
            End While
            cmd.Close()

            cmd.Open("Select amount from pos where da_te='" & DateTimePicker1.Value.Date & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            c = 0
            While Not cmd.EOF
                c = c + cmd.Fields("amount").Value
                cmd.MoveNext()
            End While
            cmd.Close()
            s = "Paid"
            cmd.Open("Select total from product_purchase where da_te='" & DateTimePicker1.Value.Date & "' and paid_unpaid='" & s & "'", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            d = 0
            While Not cmd.EOF
                d = d + cmd.Fields("total").Value
                cmd.MoveNext()
            End While
            cmd.Close()

            cmd.Open("Select cost from home where da_te='" & DateTimePicker1.Value.Date & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            f = 0
            While Not cmd.EOF
                f = f + cmd.Fields("cost").Value
                cmd.MoveNext()
            End While
            cmd.Close()

            cmd.Open("Select labour_sal from [" & DateTimePicker1.Value.Month & "] where date_='" & DateTimePicker1.Value.Date & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            g = 0
            While Not cmd.EOF
                g = g + cmd.Fields("labour_sal").Value
                cmd.MoveNext()
            End While
            cmd.Close()

            cmd.Open("Select * from collection ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("collection").Value = TextBox1.Text
            cmd.Fields("date_").Value = DateTimePicker1.Value.Date
            cmd.Fields("labour").Value = g
            cmd.Fields("non_paid").Value = a
            cmd.Fields("home_expenditure").Value = f
            cmd.Fields("tax").Value = b
            cmd.Fields("product_purchased").Value = d
            cmd.Fields("pos").Value = c
            cmd.Update()
            cmd.Close()
        End If
        
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        a = 0
        b = 0
        c = 0
        d = 0
        f = 0
        g = 0
        cmd.Open("select * from collection where date_  >= '" & DateTimePicker2.Value.Date & "' and date_<= '" & DateTimePicker3.Value.Date & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            a = a + cmd.Fields("labour").Value
            b = b + cmd.Fields("non_paid").Value
            c = c + cmd.Fields("tax").Value
            d = d + cmd.Fields("pos").Value
            f = f + cmd.Fields("product_purchased").Value
            g = g + cmd.Fields("collection").Value
            DataGridView1.Rows.Add(cmd.Fields("labour").Value, cmd.Fields("non_paid").Value, cmd.Fields("tax").Value, cmd.Fields("pos").Value, cmd.Fields("product_purchased").Value, cmd.Fields("date_").Value, (0 + cmd.Fields("labour").Value + cmd.Fields("non_paid").Value + cmd.Fields("tax").Value + cmd.Fields("pos").Value + cmd.Fields("product_purchased").Value), cmd.Fields("collection").Value)
            cmd.MoveNext()
        End While
        cmd.Close()
        TextBox3.Text = a
        TextBox4.Text = b
        TextBox5.Text = c
        TextBox6.Text = d
        TextBox7.Text = f

        
        amt = 0
        cmd.Open("select * from collection where date_  >= '" & DateTimePicker2.Value.Date & "' and date_<= '" & DateTimePicker3.Value.Date & "' ", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
        While Not cmd.EOF
            amt = amt + cmd.Fields("collection").Value
            cmd.MoveNext()
        End While
        cmd.Close()
        TextBox9.Text = amt

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox2.Text = Val(TextBox9.Text) - (Val(TextBox3.Text) + Val(TextBox4.Text) + Val(TextBox5.Text) + Val(TextBox6.Text) + Val(TextBox7.Text))
    End Sub
End Class