Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class ForgotPass
    Dim conn As New MySqlConnection("server=172.30.207.132;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim error_msg(0) As String
    Dim random As Integer = 0
    Dim i As Integer = 0
    Dim message As String
    Dim rid As MySqlDataReader



    Private Sub ForgotPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub bttnbckLogin_Click(sender As Object, e As EventArgs) Handles bttnbckLogin.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click
        Form2.Show()
        Me.Hide()

    End Sub



    Public Sub valid_blank(field, name, fieldname) 'Para sa validation kapag walang laman ang textbox
        If field = "" Then
            fieldname.BorderColor = Color.FromArgb(255, 0, 0)
            fieldname.BorderThickness = 1.5
            error_msg(random) = name & " can't be blank." & vbNewLine
            random = random + 1
            ReDim Preserve error_msg(random)
        End If
    End Sub


    Private Sub bttnNextCred_Click(sender As Object, e As EventArgs) Handles bttnNextCred.Click
        valid_blank(txtUname.Text, "Username", txtUname)
        valid_blank(txtEmail.Text, "Email", txtUname)
        While i < error_msg.Length
            message = message & error_msg(i)
            i = i + 1
        End While

        If message = "" Then
            '----------------------------NEXT-FORM-------2------------------------------------------'
            Guna2TabControl1.SelectedTab = Page2
            Page2.Enabled = True
        Else
            MsgBox(message)
            i = 0
            message = ""
            Array.Clear(error_msg, 0, error_msg.Length)
        End If
    End Sub

    Private Sub txtUname_TextChanged(sender As Object, e As EventArgs) Handles txtUname.TextChanged
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select username, email from users left join user_info on users.id = user_info.user_id where username =@username", conn)
            cmd.Parameters.AddWithValue("@username", txtUname.Text)
            rid = cmd.ExecuteReader()
            While rid.Read
                txtEmail.Text = rid.GetString("email")
            End While
            If txtUname.Text = "" Then
                txtEmail.Text = ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
End Class