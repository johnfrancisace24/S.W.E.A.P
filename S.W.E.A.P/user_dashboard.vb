﻿Imports System.IO
Imports MySql.Data.MySqlClient
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Text.RegularExpressions

Public Class user_dashboard
    Dim conn As New MySqlConnection("server=172.30.206.128;port=3306;username=dswd;password=sweapdswd;database=sweap")
    Dim dr As MySqlDataReader

    Dim sourceFilePath As String
    Dim getExtension As String
    Dim locateProject As String = My.Application.Info.DirectoryPath
    Dim indext As Integer = locateProject.IndexOf("bin\Debug\net6.0-windows")
    Dim location As String = locateProject.Substring(0, indext)
    Dim destinationPath As String = location & "\Resources\user_profile"
    Dim destinationIconPath As String = location & "\Resources\"

    Dim dashPath As String = "dashboard (3).png"
    Dim profPath As String = "man.png"
    Dim benefPath As String = "beneficiary (2).png"
    Dim settingPath As String = "settings.png"
    Dim Home As String = "house (1).png"

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick '--------------------TIME
        Dim timezone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("singapore standard time")
        Dim currenttime As DateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timezone)
        Dim currentdate As DateTime = currenttime
        Dim remainer As Integer
        lblTime.Text = currentdate.Hour & " : " & currentdate.Minute & " : " & currentdate.Second
    End Sub

    Private Sub user_dashboard_load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Get_info()
        DG_Load()
        Dashboard()
    End Sub

    'FETCHED SAVING, UNION DUES, AND CONTRIBUTIONS
    Public Sub Dashboard()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT users.*, SUM(contributions.union_dues) as union_d, SUM(contributions.bereavement) as bereav FROM users 
                                LEFT JOIN contributions on users.id = contributions.user_id 
                                WHERE users.id = @id", conn)
            cmd.Parameters.AddWithValue("@id", Form2.log_id)
            dr = cmd.ExecuteReader
            While dr.Read
                If Not dr.IsDBNull(dr.GetOrdinal("balance")) Then
                    txtSaving.Text = dr.GetString("balance")
                Else
                    txtSaving.Text = 0
                End If

                If Not dr.IsDBNull(dr.GetOrdinal("union_d")) Then
                    txtUdues.Text = dr.GetString("union_d")
                Else
                    txtUdues.Text = 0
                End If

                If Not dr.IsDBNull(dr.GetOrdinal("bereav")) Then
                    txtBreavement.Text = dr.GetString("bereav")
                Else
                    txtBreavement.Text = 0
                End If
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    'LOG OUT-------------------------
    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Dim AnswerYes As String
        AnswerYes = MessageBox.Show("Are you sure you want to Log out?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If AnswerYes = vbYes Then
            Guna2TabControl1.SelectedTab = tabDashboard
            Me.Hide()
            Form2.Show()
            lblFromTitle.Text = "Home"
            iconFromtitle.Image = Image.FromFile(destinationIconPath + Home)
        End If
    End Sub


    Private Sub Guna2TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles Guna2TabControl1.Selected
        If Guna2TabControl1.SelectedTab Is tabDashboard Then
            lblFromTitle.Text = "Dashboard"
            iconFromtitle.Image = Image.FromFile(destinationIconPath + dashPath)
            Dashboard()
        ElseIf Guna2TabControl1.SelectedTab Is tabProfile Then
            lblFromTitle.Text = "Profile"
            iconFromtitle.Image = Image.FromFile(destinationIconPath + profPath)
            Get_info()
        ElseIf Guna2TabControl1.SelectedTab Is tabBeneficiary Then
            lblFromTitle.Text = "Beneficiary"
            iconFromtitle.Image = Image.FromFile(destinationIconPath + benefPath)
            DG_Load()
        ElseIf Guna2TabControl1.SelectedTab Is tabSetting Then
            lblFromTitle.Text = "Account Setting"
            iconFromtitle.Image = Image.FromFile(destinationIconPath + settingPath)
        Else
            lblFromTitle.Text = "Home"
            iconFromtitle.Image = Image.FromFile(destinationIconPath + Home)
        End If
    End Sub


    'FETCHED ALL BENEFICIARY OF USER-------------------------------
    Public Sub DG_Load()
        BeneficiariesDGV.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT beneficiaries.* FROM users 
                                INNER JOIN beneficiaries ON users.id = beneficiaries.user_id
                                WHERE users.id = @ID", conn)

            cmd.Parameters.AddWithValue("@ID", Form2.log_id)
            dr = cmd.ExecuteReader()
            While dr.Read()
                BeneficiariesDGV.Rows.Add(dr.Item("user_id"), dr.Item("full_name"), dr.Item("age"), dr.Item("relationship"))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    'FETCH ALL INFO OF THE USER------------------------
    Public Sub Get_info()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT *, CONCAT(first_name, ' ', middle_name, ' ', last_name) AS fullName FROM users 
                            INNER JOIN user_info ON users.id = user_info.user_id  
                            WHERE users.id = @ID", conn)

            cmd.Parameters.AddWithValue("@ID", Form2.log_id)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim imagepathResources As String = destinationPath + dr.GetString("image")
                If File.Exists(imagepathResources) Then
                    userProfile.Image = Image.FromFile(imagepathResources)
                    user_Profile.Image = Image.FromFile(imagepathResources)
                Else
                    userProfile.Image = Nothing
                    user_Profile.Image = Nothing
                End If

                If cmboSex.SelectedIndex = 0 Then
                    lblDateTime.Text = "Mr. " + dr.GetString("first_name") + " Your last log in was " + dr.GetString("last_logout")
                ElseIf cmboSex.SelectedIndex = 1 Then
                    lblDateTime.Text = "Ms. " + dr.GetString("first_name") + " Your last log in was " + dr.GetString("last_logout")
                ElseIf cmboSex.SelectedIndex = 2 Then
                    lblDateTime.Text = "Hi " + dr.GetString("first_name") + " Your last log in was " + dr.GetString("last_logout")
                End If
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
                PSex.Text = dr.GetString("sex")

                txtbxusername.Text = dr.GetString("username")
                txtbxpassword.Text = dr.GetString("password")
                txtbxfname.Text = dr.GetString("first_name")
                txtbxmname.Text = dr.GetString("middle_name")
                txtbxlname.Text = dr.GetString("last_name")
                txtbxadds.Text = dr.GetString("address")
                txtbxcontact.Text = dr.GetString("contact")
                txtbxemail.Text = dr.GetString("email")
                cmboSex.SelectedItem = dr.GetString("sex")
                txtbxeduc.Text = dr.GetString("educational")
                txtbxbdate.Value = dr.GetString("birthdate")
                cmbxposition.SelectedItem = dr.GetString("position")
                cmbxemployment.SelectedItem = dr.GetString("employment_status")
                cmbxoffice.SelectedItem = dr.GetString("office")
                cmbxcomm.SelectedItem = dr.GetString("committee")
                cmbxcomm.SelectedItem = dr.GetString("committee")
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub



    'SEARCH FUNCTION-------------------------------------------
    Private Sub search_TextChanged(sender As Object, e As EventArgs) Handles search.TextChanged
        BeneficiariesDGV.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM beneficiaries WHERE full_name LIKE '%" & search.Text & "%' AND user_id = @ID", conn)
            cmd.Parameters.AddWithValue("@ID", Form2.log_id)
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



    'UPDATE FUNCTION---------------------------------------------
    Public Sub Update()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE users " &
                                        "INNER JOIN user_info ON users.id = user_info.user_id " &
                                        "SET users.username = @username, users.password = @password, users.first_name = @first, users.middle_name = @mid, users.last_name = @last, users.position = @pos, users.sex = @sex, user_info.address = @adds, user_info.contact = @contact, user_info.email = @email, user_info.educational = @educ, user_info.birthdate = @birthdate, user_info.office = @office, user_info.employment_status = @employ, user_info.committee = @comm " &
                                        "WHERE users.id = @ID", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ID", Form2.log_id)
            cmd.Parameters.AddWithValue("@username", txtbxusername.Text)
            cmd.Parameters.AddWithValue("@password", txtbxpassword.Text)
            cmd.Parameters.AddWithValue("@first", txtbxfname.Text)
            cmd.Parameters.AddWithValue("@mid", txtbxmname.Text)
            cmd.Parameters.AddWithValue("@last", txtbxlname.Text)
            cmd.Parameters.AddWithValue("@pos", cmbxposition.Text)

            cmd.Parameters.AddWithValue("@adds", txtbxadds.Text)
            cmd.Parameters.AddWithValue("@sex", cmboSex.SelectedItem)
            cmd.Parameters.AddWithValue("@contact", txtbxcontact.Text)
            cmd.Parameters.AddWithValue("@email", txtbxemail.Text)
            cmd.Parameters.AddWithValue("@educ", txtbxeduc.Text)
            cmd.Parameters.AddWithValue("@birthdate", txtbxbdate.Value)
            cmd.Parameters.AddWithValue("@office", cmbxoffice.SelectedItem)
            cmd.Parameters.AddWithValue("@employ", cmbxemployment.SelectedItem)
            cmd.Parameters.AddWithValue("@comm", cmbxcomm.SelectedItem)

            cmd.ExecuteNonQuery()
            MessageBox.Show("Updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            signups.ClearAllTextboxes(Me) 'clear all textboxes
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
        Get_info()
    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Update()
    End Sub




    'EXPORT TO EXCEL-------------------------------------------------------------------------
    Public Sub SetEPPlusLicenseContext()
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial
    End Sub
    Public Sub ExportToExcel(BenefeciariesDGV As DataGridView, filePath As String)

        SetEPPlusLicenseContext()
        Using package As New ExcelPackage()
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.Add("Employees")

            For j = 0 To BenefeciariesDGV.Columns.Count - 1
                worksheet.Cells(1, j + 1).Value = BenefeciariesDGV.Columns(j).HeaderText
            Next

            For i = 0 To BenefeciariesDGV.Rows.Count - 1
                For j = 0 To BenefeciariesDGV.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1).Value = BenefeciariesDGV.Rows(i).Cells(j).Value
                Next
            Next

            Dim range As ExcelRange = worksheet.Cells(1, 1, BenefeciariesDGV.Rows.Count + 1, BenefeciariesDGV.Columns.Count)
            range.Style.Font.Bold = True

            'background color, font color, and border for header
            Dim headerRange As ExcelRange = worksheet.Cells(1, 1, 1, BenefeciariesDGV.Columns.Count)
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGreen)
            headerRange.Style.Font.Color.SetColor(Color.Black)
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin

            'background color, font color, and border for rows
            Dim dataRange As ExcelRange = worksheet.Cells(2, 1, BenefeciariesDGV.Rows.Count + 1, BenefeciariesDGV.Columns.Count)
            dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid
            dataRange.Style.Font.Color.SetColor(Color.Black)
            dataRange.Style.Fill.BackgroundColor.SetColor(Color.White)
            dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin


            worksheet.Column(1).Width = 7.43 ' Column A
            worksheet.Column(2).Width = 42 ' Column B
            worksheet.Column(3).Width = 8 ' Column C
            worksheet.Column(4).Width = 18.57 ' Column D

            Dim fileInfo As New FileInfo(filePath)
            package.SaveAs(fileInfo)
        End Using

        ' Open the folder location of the exported Excel file
        Dim processStartInfo As New ProcessStartInfo()
        processStartInfo.FileName = "explorer.exe"
        processStartInfo.Arguments = "/select, """ & filePath & """"
        processStartInfo.UseShellExecute = True
        Process.Start(processStartInfo)
    End Sub

    'EXPORT TO EXCEL BUTTON--------------------------
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM users WHERE id=@id", conn)
            cmd.Parameters.AddWithValue("@id", Form2.log_id)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                Dim filePath As String = Path.Combine(documentsPath, dr.GetString("first_name") & "_beneficiary.xlsx")
                Dim fileName As String = dr.GetString("first_name") & "_beneficiary.xlsx"
                If File.Exists(filePath) Then 'IF FILE EXIST-------
                    MessageBox.Show("The file already exists; this is the file location" & vbCrLf & filePath, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    ExportToExcel(BeneficiariesDGV, filePath)
                    MessageBox.Show("Export completed. The file name is " & fileName, "Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '---------NUMBER ONLY AND LETTER ONLY------------'
    Private Sub txtbxcontact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbxcontact.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Shared Sub txtbxfname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbxfname.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) AndAlso Not Char.IsPunctuation(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtbxmname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbxmname.KeyPress
        txtbxfname_KeyPress(sender, e)
    End Sub
    Private Sub txtbxlname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbxlname.KeyPress
        txtbxfname_KeyPress(sender, e)
    End Sub


    ''Email Edit Validation
    Private Function IsValidEmail(email As String) As Boolean
        Dim emailRegex As New Regex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
        Return emailRegex.IsMatch(email)
    End Function

    Private Sub txtEmail_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtbxemail.Validating
        Dim inputEmail As String = txtbxemail.Text.Trim()
        If txtbxemail.Text = "" Then
            txtbxemail.Text = txtbxemail.Text

        ElseIf Not IsValidEmail(inputEmail) Then
            MessageBox.Show("Invalid email address." & vbCrLf & "Please enter a valid email address.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            e.Cancel = True
        End If
    End Sub

End Class