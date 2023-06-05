Imports MySql.Data.MySqlClient

Public Class UserDashboard

    Dim conn As New MySqlConnection("server=172.30.192.29;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim dr As MySqlDataReader

    Public Sub Get_info()


    End Sub

    Public Sub Clear()
        lblFirst.Text = ""
        lblFname.Text = ""
        lblPosition.Text = ""
        lnkFname.Text = ""
        lnkaddress.Text = ""
        lnkemail.Text = ""
        lnkbdate.Text = ""
        lnkeduc.Text = ""
        lnkemply.Text = ""
        lnkoffice.Text = ""
        lnkcomm.Text = ""
        lnkcontact.Text = ""

    End Sub

    Private Sub bttnEmpl_Click(sender As Object, e As EventArgs) Handles bttnEmpl.Click
        Guna2CustomGradientPanel1.Visible = True
        Guna2Panel1.Visible = False
        Guna2Panel2.Visible = False
    End Sub

    Private Sub bttnDash_Click(sender As Object, e As EventArgs) Handles bttnDash.Click
        Guna2Panel1.Visible = True
        Guna2CustomGradientPanel1.Visible = False
        Guna2Panel2.Visible = False
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim AnswerYes As String
        AnswerYes = MsgBox("Are you sure you want to Log out", vbQuestion + vbYesNo, "User Repsonse")

        If AnswerYes = vbYes Then
            Form1.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub UserDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Get_info()
    End Sub

    Private Sub Guna2GradientPanel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2GradientPanel1.Paint
        Dim locateProject As String = My.Application.Info.DirectoryPath
        Dim indext As Integer = locateProject.IndexOf("bin\Debug\net6.0-windows")
        Dim location As String = locateProject.Substring(0, indext)
        Dim destinationPath As String = location & "\Resources\user_profile"
        Try

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT users.*, CONCAT(users.first_name, ' ', users.middle_name, ' ', users.last_name) AS fullName, user_info.*" &
                            "FROM users " &
                            "INNER JOIN user_info ON users.id = user_info.user_id " &
                            "WHERE users.id = @ID", conn)

            cmd.Parameters.AddWithValue("@ID", Form1.log_id)
            dr = cmd.ExecuteReader
            If dr.Read() Then
                Dim imagePath As String = dr.GetString("image")
                Dim imagePathInResources As String = (destinationPath + imagePath)
                Dim Gooday As String = "Good day, " + dr.GetString("first_name")
                imgProfile.Image = Image.FromFile(imagePathInResources)
                lblFname.Text = dr.GetString("fullName")
                lblPosition.Text = dr.GetString("position")
                lblFirst.Text = Gooday

                lnkFname.Text = dr.GetString("fullName")
                lnkaddress.Text = dr.GetString("address")
                lnkemail.Text = dr.GetString("email")
                lnkbdate.Text = dr.GetDateTime("birthdate")
                lnkeduc.Text = dr.GetString("educational")
                lnkemply.Text = dr.GetString("employment_status")
                lnkoffice.Text = dr.GetString("office")
                lnkcomm.Text = dr.GetString("committee")
                lnkcontact.Text = dr.GetString("contact")

                txtbxusername.Text = dr.GetString("username")
                txtbxpassword.Text = dr.GetString("password")
                txtbxfname.Text = dr.GetString("first_name")
                txtbxmname.Text = dr.GetString("middle_name")
                txtbxlname.Text = dr.GetString("last_name")
                txtbxposition.Text = dr.GetString("position")
                cmbxemply.Text = dr.GetString("employment_status")
                txtbxadds.Text = dr.GetString("address")
                txtbxcontact.Text = dr.GetString("contact")
                txtbxemail.Text = dr.GetString("email")
                txtbxeduc.Text = dr.GetString("educational")
                txtbxbdate.Value = dr.GetString("birthdate")
                cmbxoffice.Text = dr.GetString("office")
                cmbxcomittee.Text = dr.GetString("committee")
                ' Dito mo ilalagay ang logic para gamitin ang retrieved data
            End If
        Catch ex As Exception
            MsgBox("Doesn't work. LOL!")
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub Update()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE users " &
                                        "INNER JOIN user_info ON users.id = user_info.user_id " &
                                        "SET users.username = @username, users.password = @password, users.first_name = @first, users.middle_name = @mid, users.last_name = @last, users.position = @pos, user_info.address = @adds, user_info.contact = @contact, user_info.email = @email, user_info.educational = @educ, user_info.birthdate = @birthdate, user_info.office = @office, user_info.employment_status = @employ, user_info.committee = @comm " &
                                        "WHERE users.id = @ID", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ID", Form1.log_id)
            cmd.Parameters.AddWithValue("@username", txtbxusername.Text)
            cmd.Parameters.AddWithValue("@password", txtbxpassword.Text)
            cmd.Parameters.AddWithValue("@first", txtbxfname.Text)
            cmd.Parameters.AddWithValue("@mid", txtbxmname.Text)
            cmd.Parameters.AddWithValue("@last", txtbxlname.Text)
            cmd.Parameters.AddWithValue("@pos", txtbxposition.Text)

            cmd.Parameters.AddWithValue("@adds", txtbxadds.Text)
            cmd.Parameters.AddWithValue("@contact", txtbxcontact.Text)
            cmd.Parameters.AddWithValue("@email", txtbxemail.Text)
            cmd.Parameters.AddWithValue("@educ", txtbxeduc.Text)
            cmd.Parameters.AddWithValue("@birthdate", txtbxbdate.Value)
            cmd.Parameters.AddWithValue("@office", cmbxoffice.Text)
            cmd.Parameters.AddWithValue("@employ", cmbxemply.Text)
            cmd.Parameters.AddWithValue("@comm", cmbxcomittee.Text)

            cmd.ExecuteNonQuery()
            MessageBox.Show("Updated successfully!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Update()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2CustomGradientPanel1.Visible = False
        Guna2Panel1.Visible = False
        Guna2Panel2.Visible = True
    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click

    End Sub
End Class