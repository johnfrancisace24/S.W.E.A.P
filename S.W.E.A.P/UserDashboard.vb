Imports MySql.Data.MySqlClient

Public Class UserDashboard

    Dim conn As New MySqlConnection("server=172.30.206.156;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim dr As MySqlDataReader

    Public Sub Get_info()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT users.*, CONCAT(users.first_name, ' ', users.middle_name, ' ', users.last_name) AS fullName, user_info.*" &
                            "FROM users " &
                            "INNER JOIN user_info ON users.id = user_info.user_id " &
                            "WHERE users.id = @ID", conn)

            cmd.Parameters.AddWithValue("@ID", Form1.log_id)
            dr = cmd.ExecuteReader
            If dr.Read() Then
                Dim Gooday As String = "Good day, " + dr.GetString("first_name")
                lblFname.Text = dr.GetString("fullName")
                lblPosition.Text = dr.GetString("position")
                lblFirst.Text = Gooday
                lnkFname.Text = dr.GetString("fullName")
                lnkaddress.Text = dr.GetString("address")
                lnkemail.Text = dr.GetString("email")
                'lnkbdate.Text = dr.GetString("birthdate")
                lnkeduc.Text = dr.GetString("educational")
                lnkemply.Text = dr.GetString("employment_status")
                lnkoffice.Text = dr.GetString("office")
                lnkcomm.Text = dr.GetString("committee")
                lnkcontact.Text = dr.GetString("contact")
                ' Dito mo ilalagay ang logic para gamitin ang retrieved data
            End If
        Catch ex As Exception
            MsgBox("Doesn't work. LOL!")
        Finally
            conn.Close()
        End Try


    End Sub

    Private Sub UserDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Get_info()
    End Sub

    Private Sub bttnEmpl_Click(sender As Object, e As EventArgs) Handles bttnEmpl.Click
        Guna2CustomGradientPanel1.Visible = True
        Guna2Panel1.Visible = False
    End Sub

    Private Sub bttnDash_Click(sender As Object, e As EventArgs) Handles bttnDash.Click
        Guna2Panel1.Visible = True
        Guna2CustomGradientPanel1.Visible = False
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim AnswerYes As String
        AnswerYes = MsgBox("Are you sure you want to Log out", vbQuestion + vbYesNo, "User Repsonse")

        If AnswerYes = vbYes Then
            Form1.Show()
            Me.Hide()
        End If
    End Sub
End Class