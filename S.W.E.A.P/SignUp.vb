Imports MySql.Data.MySqlClient
Public Class SignUp
    Dim conn As New MySqlConnection("server=172.30.206.156;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader
    Private Sub SignUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rdiobttnPer.Checked = True
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("insert into users(username, password, first_name, middle_name, last_name, position, image, is_admin, created_at, updated_at)values(@UNAME, @PW, @FNAME, @MNAME, @LNAME, @POS, @IMG, 0, now(), now());
                                         insert into user_info(user_id, address, email, educational, birthdate, office, employment_status, committee)values(last_insert_id(), @ADRS, @EMAIL, @EDUC, @BDAY, @OFFICE, @EMTYPE, @COMM);
                                         insert into beneficiaries(user_id, full_name, relationship, age)values((select id from users where username=@HOOK), @BNAME1, @BREL1, @BAGE1);", conn)
            cmd.Parameters.AddWithValue("@UNAME", txtbxUser.Text)
            cmd.Parameters.AddWithValue("@PW", txtbxPass.Text)
            cmd.Parameters.AddWithValue("@FNAME", txtbxFname.Text)
            cmd.Parameters.AddWithValue("@MNAME", txtbxMname.Text)
            cmd.Parameters.AddWithValue("@LNAME", txtbxLname.Text)
            cmd.Parameters.AddWithValue("@POS", comboPos.SelectedItem)
            cmd.Parameters.AddWithValue("@IMG", "Image.jpg")
            cmd.Parameters.AddWithValue("@ADRS", txtbxAddrs.Text)
            cmd.Parameters.AddWithValue("@EMAIL", txtbxEmail.Text)
            cmd.Parameters.AddWithValue("@EDUC", txtbxEducAt.Text)
            cmd.Parameters.AddWithValue("@BDAY", pickBday.Value)
            cmd.Parameters.AddWithValue("@OFFICE", comboOffice.SelectedItem)
            cmd.Parameters.AddWithValue("@EMTYPE", comboEmployStat.SelectedItem)
            cmd.Parameters.AddWithValue("@COMM", comboCommit.SelectedItem)
            cmd.Parameters.AddWithValue("@BNAME1", txtbxBF1.Text)
            cmd.Parameters.AddWithValue("@BREL1", txtbxBR1.Text)
            cmd.Parameters.AddWithValue("@BAGE1", txtbxBA1.Text)
            cmd.Parameters.AddWithValue("@HOOK", txtbxUser.Text)
            cmd.ExecuteNonQuery()
            MsgBox("napasok na")
        Catch ex As Exception
            MsgBox("doesnt work lmao")
        Finally
            conn.Close()
        End Try
    End Sub


    Private Sub bttnNext_Click(sender As Object, e As EventArgs) Handles bttnNext.Click
        If rdiobttnPer.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = True
            pnlBen.Visible = False
            pnlAcc.Visible = False


            rdiobttnWork.Checked = True
            rdiobttnPer.Checked = False


        ElseIf rdiobttnWork.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = False
            pnlBen.Visible = True
            pnlAcc.Visible = False

            rdiobttnWork.Checked = False
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = True
            rdiobttnAccnt.Checked = False


        ElseIf rdiobttnBene.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = False
            pnlBen.Visible = False
            pnlAcc.Visible = True

            rdiobttnWork.Checked = False
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = False
            rdiobttnAccnt.Checked = True
            btnSubmit.Show()
            bttnNext.Hide()
            'ElseIf (For Saving Account)
        End If
    End Sub
    Private Sub bttnSubmit_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub bttnBck_Click(sender As Object, e As EventArgs) Handles bttnBck.Click
        If rdiobttnPer.Checked = True Then
            Form1.Show()
            Me.Hide()

        ElseIf rdiobttnWork.Checked = True Then
            pnlPer.Visible = True
            pnlWork.Visible = False
            pnlBen.Visible = False
            pnlAcc.Visible = False

            rdiobttnPer.Checked = True
            rdiobttnWork.Checked = False
            rdiobttnBene.Checked = False
            rdiobttnAccnt.Checked = False

        ElseIf rdiobttnBene.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = True
            pnlBen.Visible = False
            pnlAcc.Visible = False

            rdiobttnWork.Checked = True
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = False
            rdiobttnAccnt.Checked = False


        ElseIf rdiobttnAccnt.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = False
            pnlBen.Visible = True
            pnlAcc.Visible = False

            rdiobttnWork.Checked = False
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = True
            rdiobttnAccnt.Checked = False
            bttnNext.Show()
            btnSubmit.Hide()
        End If
    End Sub
End Class