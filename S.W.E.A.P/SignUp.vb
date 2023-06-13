Imports System.IO
Imports MySql.Data.MySqlClient
Public Class SignUp
    Dim conn As New MySqlConnection("server=172.30.205.208;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader
    Dim error_msg(0) As String
    Dim random As Integer = 0
    Dim i As Integer = 0
    Dim message As String
    Dim sourceFilePath As String
    Dim getExtension As String
    '-------FUNCTIONS-------------------------------------------------------------------------------------------------------------------------------------
    Public Sub valid_blank(field, name, fieldname)
        If field = "" Then
            fieldname.bordercolor = Color.FromArgb(255, 0, 0)
            fieldname.borderthickness = 1.5
            error_msg(random) = name & " can't be blank." & vbNewLine
            random = random + 1
            ReDim Preserve error_msg(random)
        End If
    End Sub

    Public Sub add_benefi(hook, bname, brel, bage)
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("insert into beneficiaries(user_id, full_name, relationship, age)values((select id from users where username=@HOOK1), @BNAME, @BREL, @BAGE);", conn)
            cmd.Parameters.AddWithValue("@HOOK1", hook)
            cmd.Parameters.AddWithValue("@BNAME", bname)
            cmd.Parameters.AddWithValue("@BREL", brel)
            cmd.Parameters.AddWithValue("@BAGE", bage)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("beneficiaryy function doesn't work.")
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub invalid_reset(fieldname)
        fieldname.bordercolor = Color.Gray
        fieldname.borderthickness = 1
    End Sub
    Public Sub reset_all()
        invalid_reset(txtbxUser)
        invalid_reset(txtbxPass)
        invalid_reset(txtbxFname)
        invalid_reset(txtbxMname)
        invalid_reset(txtbxLname)
        invalid_reset(comboPos)
        invalid_reset(txtbxAddrs)
        invalid_reset(txtbxCntct)
        invalid_reset(txtbxEmail)
        invalid_reset(txtbxEducAt)
        invalid_reset(comboOffice)
        invalid_reset(comboEmployStat)
        invalid_reset(comboCommit)
        invalid_reset(txtbxBF1)
        invalid_reset(txtbxBR1)
        invalid_reset(txtbxBA1)
    End Sub
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------


    '----------------------------------------------------------------BODY-----------------------------------------------------------------------------------------

    Private Sub SignUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rdiobttnPer.Checked = True
    End Sub

    Private Sub bttnUpload_Click(sender As Object, e As EventArgs) Handles bttnUpload.Click
        Dim opf As New OpenFileDialog

        opf.Filter = "Choose Image(*.jpg; *.png; *.gif) | * .jpg; *.png; *.gif"
        If opf.ShowDialog = DialogResult.OK Then
            'imageInput = System.IO.Path.GetFullPath(opf.FileName)
            sourceFilePath = Path.GetFullPath(opf.FileName)
            pBoxProfile.BackgroundImage = Image.FromFile(sourceFilePath)
            getExtension = Path.GetExtension(opf.FileName)
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        valid_blank(txtbxUser.Text, "Username", txtbxUser)
        valid_blank(txtbxPass.Text, "Password", txtbxPass)
        valid_blank(txtbxFname.Text, "First name", txtbxFname)
        valid_blank(txtbxMname.Text, "Middle name", txtbxMname)
        valid_blank(txtbxLname.Text, "Last name", txtbxLname)
        valid_blank(comboPos.SelectedItem, "Position", comboPos)
        valid_blank(txtbxAddrs.Text, "Address", txtbxAddrs)
        valid_blank(txtbxCntct.Text, "Contact", txtbxCntct)
        valid_blank(txtbxEmail.Text, "Email", txtbxEmail)
        valid_blank(txtbxEducAt.Text, "Educational Attainment", txtbxEducAt)
        valid_blank(comboOffice.SelectedItem, "Office", comboOffice)
        valid_blank(comboEmployStat.SelectedItem, "Employment type", comboEmployStat)
        valid_blank(comboCommit.SelectedItem, "Committee", comboCommit)
        valid_blank(txtbxBF1.Text, "Beneficiarie's name", txtbxBF1)
        valid_blank(txtbxBR1.Text, "Beneficiarie's relationship", txtbxBR1)
        valid_blank(txtbxBA1.Text, "Beneficiarie's age", txtbxBA1)
        While i < error_msg.Length
            message = message & error_msg(i)
            i = i + 1
        End While
        If message = "" Then
            '----------------------------GETTING IMAGE-------------------------------------------------
            Dim locateProject As String = My.Application.Info.DirectoryPath
            Dim indext As Integer = locateProject.IndexOf("bin\Debug\net6.0-windows")
            Dim location As String = locateProject.Substring(0, indext)
            Dim opf As New OpenFileDialog

            Dim destinationPath As String = location & "\Resources\user_profile\" & txtbxUser.Text & getExtension
            File.Copy(sourceFilePath, destinationPath, True)
            Dim imageInput As String = "\" & txtbxUser.Text & getExtension
            '------------------------------------------------------------------------------------------
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("insert into users(username, password, first_name, middle_name, last_name, position, image, is_admin, created_at, updated_at)values(@UNAME, @PW, @FNAME, @MNAME, @LNAME, @POS, @IMG, 0, now(), now());
                                         insert into user_info(user_id, address, contact, email, educational, birthdate, office, employment_status, committee)values(last_insert_id(), @ADRS, @CONTACT, @EMAIL, @EDUC, @BDAY, @OFFICE, @EMTYPE, @COMM);
                                         insert into beneficiaries(user_id, full_name, relationship, age)values((select id from users where username=@HOOK), @BNAME1, @BREL1, @BAGE1);", conn)
                cmd.Parameters.AddWithValue("@UNAME", txtbxUser.Text)
                cmd.Parameters.AddWithValue("@PW", txtbxPass.Text)
                cmd.Parameters.AddWithValue("@FNAME", txtbxFname.Text)
                cmd.Parameters.AddWithValue("@MNAME", txtbxMname.Text)
                cmd.Parameters.AddWithValue("@LNAME", txtbxLname.Text)
                cmd.Parameters.AddWithValue("@POS", comboPos.SelectedItem)
                cmd.Parameters.AddWithValue("@IMG", imageInput)
                cmd.Parameters.AddWithValue("@ADRS", txtbxAddrs.Text)
                cmd.Parameters.AddWithValue("@CONTACT", txtbxCntct.Text)
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
                MsgBox("Added successfully!")
            Catch ex As Exception
                MsgBox("Create account failed.")
            Finally
                conn.Close()
            End Try
            If txtbxBF2.Text <> "" Then
                add_benefi(txtbxUser.Text, txtbxBF2.Text, txtbxBR2.Text, txtbxBA2.Text)
            End If
            If txtbxBF3.Text <> "" Then
                add_benefi(txtbxUser.Text, txtbxBF3.Text, txtbxBR3.Text, txtbxBA3.Text)
            End If
            If txtbxBF4.Text <> "" Then
                add_benefi(txtbxUser.Text, txtbxBF4.Text, txtbxBR4.Text, txtbxBA4.Text)
            End If
            If txtbxBF5.Text <> "" Then
                add_benefi(txtbxUser.Text, txtbxBF5.Text, txtbxBR5.Text, txtbxBA5.Text)
            End If
            Form1.Visible = True
            Me.Visible = False
        Else

            MsgBox(message)
            i = 0
            message = ""
            Array.Clear(error_msg, 0, error_msg.Length)

        End If

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
            reset_all()
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