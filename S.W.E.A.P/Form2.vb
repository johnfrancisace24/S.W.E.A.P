Imports MySql.Data.MySqlClient

Public Class Form2
    Dim conn As New MySqlConnection("server=172.30.205.208;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader
    Public Shared log_id As Integer
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
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
            Catch ex As Exception
                MsgBox("Account doesn't exist.")
            Finally
                conn.Close()
            End Try
            If (status = 1) Then
                txtUsername.Clear()
                txtPassword.Clear()
                admindash.Show()
                Me.Hide()
            ElseIf (status = 0) Then
                Userdash.Show()
                txtUsername.Clear()
                txtPassword.Clear()
                Me.Hide()
            Else
                MsgBox("Invalid username or passowrd.")
            End If
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
End Class