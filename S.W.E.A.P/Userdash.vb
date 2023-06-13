Imports System.IO
Imports MySql.Data.MySqlClient

Public Class Userdash

    Dim conn As New MySqlConnection("server=172.30.205.208;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim dr As MySqlDataReader

    Private Sub Userdash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlDash.Visible = True
        pnlProfile.Hide()
        pnlAccount.Hide()
        pnlContribute.Hide()
        DG_Load()
    End Sub
    Private Sub bttnDash_Click(sender As Object, e As EventArgs) Handles bttnDash.Click
        pnlDash.Visible = True
        pnlProfile.Hide()
        pnlAccount.Hide()
        pnlContribute.Hide()
    End Sub
    Private Sub bttnProf_Click(sender As Object, e As EventArgs) Handles bttnProf.Click
        pnlProfile.Visible = True
        pnlAccount.Hide()
        pnlDash.Hide()
        pnlContribute.Hide()
        Get_info()
    End Sub

    Private Sub bttnAcc_Click(sender As Object, e As EventArgs) Handles bttnAcc.Click
        pnlAccount.Visible = True
        pnlProfile.Hide()
        pnlDash.Hide()
        pnlContribute.Hide()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        pnlContribute.Visible = True
        pnlProfile.Hide()
        pnlDash.Hide()
        pnlAccount.Hide()

        DG_Load()
    End Sub
    Private Sub bttnLogout_Click(sender As Object, e As EventArgs) Handles bttnLogout.Click
        Dim AnswerYes As String
        AnswerYes = MsgBox("Are you sure you want to Log out", vbQuestion + vbYesNo, "User Repsonse")

        If AnswerYes = vbYes Then
            Form1.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub search_TextChanged(sender As Object, e As EventArgs) Handles search.TextChanged
        BeneficiariesDGV.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM beneficiaries WHERE full_name LIKE '%" & search.Text & "%' AND user_id = @ID", conn)
            cmd.Parameters.AddWithValue("@ID", Form1.log_id)
            dr = cmd.ExecuteReader

            While dr.Read
                BeneficiariesDGV.Rows.Add(dr.Item("user_id"), dr.Item("full_name"), dr.Item("age"), dr.Item("relationship"))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub DG_Load()
        BeneficiariesDGV.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM users 
                            INNER JOIN beneficiaries ON users.id = beneficiaries.user_id
                            WHERE users.id = @ID", conn)

            cmd.Parameters.AddWithValue("@ID", Form1.log_id)
            dr = cmd.ExecuteReader
            While dr.Read
                BeneficiariesDGV.Rows.Add(dr.Item("user_id"), dr.Item("full_name"), dr.Item("age"), dr.Item("relationship"))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub Get_info()
        Dim locateProject As String = My.Application.Info.DirectoryPath
        Dim indext As Integer = locateProject.IndexOf("bin\Debug\net6.0-windows")
        Dim location As String = locateProject.Substring(0, indext)
        Dim destinationPath As String = location & "\Resources\user_profile"

        Try

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT *, CONCAT(users.first_name, ' ', users.middle_name, ' ', users.last_name) AS fullName
                            FROM users 
                            INNER JOIN user_info ON users.id = user_info.user_id  
                            WHERE users.id = @ID", conn)

            cmd.Parameters.AddWithValue("@ID", Form1.log_id)
            dr = cmd.ExecuteReader
            If dr.Read() Then
                Dim imagePath As String = dr.GetString("image")
                Dim imagePathInResources As String = (destinationPath + imagePath)
                Dim Gooday As String = "Good day, Ma'am/Sir " + dr.GetString("first_name")


                lblFname.Text = dr.GetString("fullName")
                lblPosition.Text = dr.GetString("position")
                lblFirst.Text = Gooday

                Pfname.Text = dr.GetString("fullName")
                Padd.Text = dr.GetString("address")
                Pcntact.Text = dr.GetString("contact")
                Pemail.Text = dr.GetString("email")
                Pbdate.Text = dr.GetDateTime("birthdate")

                Peducational.Text = dr.GetString("educational")
                Pemployment.Text = dr.GetString("employment_status")
                Poffice.Text = dr.GetString("office")
                Pposition.Text = dr.GetString("position")
                Pcommittee.Text = dr.GetString("committee")


                txtbxusername.Text = dr.GetString("username")
                txtbxpassword.Text = dr.GetString("password")
                txtbxfname.Text = dr.GetString("first_name")
                txtbxmname.Text = dr.GetString("middle_name")
                txtbxlname.Text = dr.GetString("last_name")
                txtbxadds.Text = dr.GetString("address")
                txtbxcontact.Text = dr.GetString("contact")
                txtbxemail.Text = dr.GetString("email")
                txtbxeduc.Text = dr.GetString("educational")
                txtbxbdate.Value = dr.GetString("birthdate")
                cmbxposition.SelectedItem = dr.GetString("position")
                cmbxemployment.SelectedItem = dr.GetString("employment_status")
                cmbxoffice.SelectedItem = dr.GetString("office")
                cmbxcomm.SelectedItem = dr.GetString("committee")
                cmbxcomm.SelectedItem = dr.GetString("committee")

                If File.Exists(imagePathInResources) Then

                    ImgProfile.Image = Image.FromFile(imagePathInResources)
                Else
                    ImgProfile.Image = Nothing
                End If
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
                                        "WHERE users.id     = @ID", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ID", Form1.log_id)
            cmd.Parameters.AddWithValue("@username", txtbxusername.Text)
            cmd.Parameters.AddWithValue("@password", txtbxpassword.Text)
            cmd.Parameters.AddWithValue("@first", txtbxfname.Text)
            cmd.Parameters.AddWithValue("@mid", txtbxmname.Text)
            cmd.Parameters.AddWithValue("@last", txtbxlname.Text)
            cmd.Parameters.AddWithValue("@pos", cmbxposition.Text)

            cmd.Parameters.AddWithValue("@adds", txtbxadds.Text)
            cmd.Parameters.AddWithValue("@contact", txtbxcontact.Text)
            cmd.Parameters.AddWithValue("@email", txtbxemail.Text)
            cmd.Parameters.AddWithValue("@educ", txtbxeduc.Text)
            cmd.Parameters.AddWithValue("@birthdate", txtbxbdate.Value)
            cmd.Parameters.AddWithValue("@office", cmbxoffice.Text)
            cmd.Parameters.AddWithValue("@employ", cmbxemployment.Text)
            cmd.Parameters.AddWithValue("@comm", cmbxcomm.Text)

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

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
        Get_info()
    End Sub


End Class