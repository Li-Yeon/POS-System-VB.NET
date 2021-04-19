Imports MySql.Data.MySqlClient

Public Class Login
    Dim MysqlConn As MySqlConnection
    Dim Command As MySqlCommand


    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        roundCorners(Me)
        Me.CenterToScreen()
    End Sub

    Private Sub roundCorners(obj As Form)
        obj.FormBorderStyle = FormBorderStyle.None
        Dim DGP As New Drawing2D.GraphicsPath
        DGP.StartFigure()
        'top left corner
        DGP.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        DGP.AddLine(40, 0, obj.Width - 40, 0)
        'top right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, 0, 40, 40), -90, 90)
        DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40)
        'buttom right corner
        DGP.AddArc(New Rectangle(obj.Width - 40, obj.Height - 40, 40, 40), 0, 90)
        DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height)
        'buttom left corner
        DGP.AddArc(New Rectangle(0, obj.Height - 40, 40, 40), 90, 90)
        DGP.CloseFigure()
        obj.Region = New Region(DGP)
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        MysqlConn = New MySqlConnection
        MysqlConn.ConnectionString = "server=localhost;port=3306;database=pos_system;username=root;password=;"
        Dim Reader As MySqlDataReader
        Try
            MysqlConn.Open()
            Dim loginID As String
            loginID = "SELECT Username, Password FROM users where Username='" & txtUser.Text & "' and Password='" & txtPass.Text & "'"
            Dim count As Integer
            count = 0
            Command = New MySqlCommand(loginID, MysqlConn)
            Reader = Command.ExecuteReader
            While Reader.Read
                count = count + 1
            End While
            Reader.Close()
            If count = 1 Then
                Me.Hide()
                Home.Show()
            ElseIf count > 1 Then
                MessageBox.Show("Duplicate Detected")
            Else
                MessageBox.Show("Incorrect Username or Password")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        txtUser.Text = ""
        txtPass.Text = ""

    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Application.Exit()
    End Sub
End Class
