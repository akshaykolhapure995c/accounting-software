Public Class firm
    Dim con As New ADODB.Connection
    Dim cmd As New ADODB.Recordset
    Private Sub firm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.LinkLabel1.Text = "CHIRAG" Then
            con.Open("dsn=chirag", Password:="abc")
        End If
        If Me.LinkLabel1.Text = "SURBHI" Then
            con.Open("dsn=surbhi", Password:="abc")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Add Firm Name")
        Else
            cmd.Open("Select * from firm", con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)
            cmd.AddNew()
            cmd.Fields("firm_name").Value = TextBox1.Text
            cmd.Update()
            cmd.Close()
            TextBox1.Text = ""
            TextBox1.Focus()
        End If
        
    End Sub
End Class