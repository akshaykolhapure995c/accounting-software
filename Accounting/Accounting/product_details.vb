Public Class product_details
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Add Product Name")
        Else
            cmd.Open("Select * from product", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("product_name").Value = TextBox1.Text
            cmd.Update()
            cmd.Close()
            TextBox1.Text = ""
            TextBox1.Focus()
        End If
        
    End Sub

    Private Sub product_details_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub product_details_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.LinkLabel1.Text = "CHIRAG" Then
            con.Open("dsn=chirag", Password:="abc")
        End If
        If Me.LinkLabel1.Text = "SURBHI" Then
            con.Open("dsn=surbhi", Password:="abc")
        End If
    End Sub
End Class