Public Class login

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "Abid" And TextBox2.Text = "abid" Then
            Main.Show()
            Me.Visible = False
        Else
            MsgBox("Wrong Username or Password")
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub TextBox2_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles TextBox2.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then
            If TextBox1.Text = "Abid" And TextBox2.Text = "abid" Then
                Main.Show()
                Me.Visible = False
            Else
                MsgBox("Wrong Username or Password")
                TextBox1.Text = ""
                TextBox2.Text = ""
            End If
        End If
        
    End Sub
End Class