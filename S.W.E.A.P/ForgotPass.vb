Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Net.Mail
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions
Imports System.IO

Public Class ForgotPass
    Dim conn As New MySqlConnection("server=172.30.206.128;port=3306;username=dswdSweap;password=druguser;database=sweap")
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



    Dim otp As String = generateOTP()
    Dim smtpUsername As String = "condradssalon@gmail.com"
    Dim smtpPassword As String = "ihlmujssjncwlcaq"

    Function generateOTP() As String
        Dim random As New Random()
        Dim otp As Integer = random.Next(1000, 9999)
        Return otp.ToString()
    End Function



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
            txtOtpVerify.Enabled = True
        Else
            MsgBox(message)
            i = 0
            message = ""
            Array.Clear(error_msg, 0, error_msg.Length)
        End If


        '-------------------------Sending Email Otp-----------------------------------'


        Try
            Dim fromAddress As New MailAddress("noreply@sweap.com", "SWEAP")
            Dim toAddress As New MailAddress(txtEmail.Text)
            Dim subject As String = "One-Time Password (OTP)"
            Dim body As String = String.Format("To reset your password use this 4 digits OTP : {0}", otp)


            Dim smtp As New SmtpClient()
            smtp.Host = "smtp.gmail.com"
            smtp.Port = 587
            smtp.EnableSsl = True
            smtp.Credentials = New NetworkCredential(smtpUsername, smtpPassword)
            Dim message As New MailMessage(fromAddress, toAddress)
            message.Subject = subject
            message.Body = body

            smtp.Send(message)

        Catch ex As Exception
            MsgBox("Failed to send OTP code" & ex.Message)
        End Try
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

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        valid_blank(txtOtpVerify.Text, "Username", txtOtpVerify)


        '------------------OTP VERIFICATION----------------------
        Dim entercode As String = txtOtpVerify.Text
        If entercode.Equals(otp, StringComparison.OrdinalIgnoreCase) Then
            MsgBox("One-Time Password is matched!")
            Guna2TabControl1.SelectedTab = Page3
            txtNewPass.Enabled = True
            txtConfirmPass.Enabled = True
        Else
            MsgBox("Please enter a valid OTP!")
        End If
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Guna2TabControl1.SelectedTab = Page1
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Guna2TabControl1.SelectedTab = Page2
    End Sub


    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click

        Try
            If txtNewPass.Text = txtConfirmPass.Text Then
                lblNotmatch.Text = "Password Matched!"
                lblNotmatch.ForeColor = Color.Blue
                conn.Open()
                Dim cmd As New MySqlCommand("UPDATE users SET password = @NewPassword WHERE username = @Username", conn)
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@Username", txtUname.Text)
                cmd.Parameters.AddWithValue("@NewPassword", txtConfirmPass.Text)

                cmd.ExecuteNonQuery()

                MessageBox.Show("Password Updated successfully!", "SUCCESSFULL", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                Form2.Show()
                signups.ClearAllTextboxes(Me)
            Else
                MessageBox.Show("Password cannot be Updated!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                lblNotmatch.Text = "Password didn't matched!"
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub
End Class