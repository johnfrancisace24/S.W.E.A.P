Imports MySql.Data.MySqlClient

Public Class Form2
    Dim conn As New MySqlConnection("server=172.30.192.162;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader
    Public Shared log_id As Integer
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        PerformLogin()
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            PerformLogin()
            e.Handled = True
        End If
    End Sub

    Private Sub PerformLogin()
        Dim DateAndTime As String = DateTime.Now.ToString()

        If (txtUsername.Text = "") Then
            MsgBox("Username can't be blank.")
        ElseIf (txtPassword.Text = "") Then
            MsgBox("Password can't be blank.")
        Else
            Dim status As Integer = 3
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM users WHERE username=@NAME AND password=@PASS", conn)
                cmd.Parameters.AddWithValue("@NAME", txtUsername.Text)
                cmd.Parameters.AddWithValue("@PASS", txtPassword.Text)
                rid = cmd.ExecuteReader
                While rid.Read
                    status = rid.GetInt32("is_admin")
                    log_id = rid.GetInt32("id")
                End While
                If (status = 1) Then
                    txtUsername.Clear()
                    txtPassword.Clear()
                    admindash.Show()
                    Me.Hide()
                ElseIf (status = 0) Then
                    txtUsername.Clear()
                    txtPassword.Clear()
                    Me.Hide()
                    user_dashboard.Show()
                Else
                    MsgBox("Invalid username or password.")
                End If
                rid.Close() ' Close the data reader

                ' Update the last_logout column for the logged-in user
                Dim cmdd As New MySqlCommand("UPDATE users SET last_logout=@now WHERE id = @ID", conn)
                cmdd.Parameters.AddWithValue("@ID", log_id)
                cmdd.Parameters.AddWithValue("@now", DateAndTime)
                cmdd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                conn.Close()
            End Try
        End If

    End Sub



    Private Sub checkShowPw_CheckedChanged(sender As Object, e As EventArgs) Handles checkShowPw.CheckedChanged
        If checkShowPw.Checked = False Then
            txtPassword.PasswordChar = "*"
        Else
            txtPassword.PasswordChar = ""
        End If
    End Sub

    Private Sub lblRegister_Click(sender As Object, e As EventArgs) Handles lblRegister.Click
        Me.Hide()
        signups.Show()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.PasswordChar = "*"
    End Sub

    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub lblForgot_Click(sender As Object, e As EventArgs) Handles lblForgot.Click
        ForgotPass.Show()
        Me.Hide()
    End Sub
End Class