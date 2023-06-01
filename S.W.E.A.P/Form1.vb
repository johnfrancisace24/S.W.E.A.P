Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography

Public Class Form1
    Dim conn As New MySqlConnection("server=172.30.206.156;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader

    Public Shared log_id As Integer
    Private Sub LinkLabelSignUp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelSignUp.LinkClicked
        Me.Hide()
        SignUp.Show()
    End Sub
    Private Sub CheckBoxShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowPass.CheckedChanged
        If CheckBoxShowPass.Checked = False Then
            txtbxPassword.PasswordChar = "*"
        Else
            txtbxPassword.PasswordChar = ""
        End If
    End Sub

    Public Sub Login()
        If (txtbxUsername.Text = "") Then
            MsgBox("Username can't be blank.")
        ElseIf (txtbxPassword.Text = "") Then
            MsgBox("Password can't be blank.")
        Else
            Dim status As Integer = 3
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT * FROM users WHERE username=@NAME AND password=@PASS", conn)
                cmd.Parameters.AddWithValue("@NAME", txtbxUsername.Text)
                cmd.Parameters.AddWithValue("@PASS", txtbxPassword.Text)
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
                AdminDashboard.Show()
                Me.Hide()
            ElseIf (status = 0) Then
                UserDashboard.Show()
                txtbxUsername.Clear()
                txtbxPassword.Clear()
                Me.Hide()
            Else
                MsgBox("Invalid username or passowrd.")
            End If
        End If
    End Sub
    Private Sub bttLogin_Click(sender As Object, e As EventArgs) Handles bttLogin.Click
        Login()
    End Sub
End Class
