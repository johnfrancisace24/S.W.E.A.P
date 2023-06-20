Imports System.IO
Imports MySql.Data.MySqlClient
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Public Class Userdash

    Dim conn As New MySqlConnection("server=172.30.207.132;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim dr As MySqlDataReader

    Private isPanelVisible As Boolean = False
    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        If isPanelVisible Then
            Panel6.Visible = False
            isPanelVisible = False
        Else
            Panel6.Visible = True
            isPanelVisible = True
        End If
    End Sub
    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        pnlAccount.Visible = True
        pnlProfile.Hide()
        pnlDash.Hide()
        pnlContribute.Hide()
        Panel6.Hide()
    End Sub
    Private Sub Userdash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlDash.Visible = True
        pnlProfile.Hide()
        pnlAccount.Hide()
        pnlContribute.Hide()
        Panel6.Hide()

        DG_Load()
    End Sub
    Private Sub bttnDash_Click(sender As Object, e As EventArgs) Handles bttnDash.Click
        pnlDash.Visible = True
        pnlProfile.Hide()
        pnlAccount.Hide()
        pnlContribute.Hide()
        Panel6.Hide()
    End Sub
    Private Sub bttnProf_Click(sender As Object, e As EventArgs) Handles bttnProf.Click
        pnlProfile.Visible = True
        pnlAccount.Hide()
        pnlDash.Hide()
        pnlContribute.Hide()
        Panel6.Hide()

        Get_info()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        pnlContribute.Visible = True
        pnlProfile.Hide()
        pnlDash.Hide()
        pnlAccount.Hide()
        Panel6.Hide()


        DG_Load()
    End Sub

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

    Public Sub DG_Load()
        BeneficiariesDGV.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM users 
                            INNER JOIN beneficiaries ON users.id = beneficiaries.user_id
                            WHERE users.id = @ID", conn)

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

    Public Sub Get_info()
        Dim locateProject As String = My.Application.Info.DirectoryPath
        Dim indext As Integer = locateProject.IndexOf("bin\Debug\net6.0-windows")
        Dim location As String = locateProject.Substring(0, indext)
        Dim destinationPath As String = location & "\Resources\user_profile"

        Try

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT *, CONCAT(users.first_name, ' ', users.last_name) AS fullName
                            FROM users 
                            INNER JOIN user_info ON users.id = user_info.user_id  
                            WHERE users.id = @ID", conn)

            cmd.Parameters.AddWithValue("@ID", Form2.log_id)
            dr = cmd.ExecuteReader
            If dr.Read() Then
                Dim imagePath As String = dr.GetString("image")
                Dim imagePathInResources As String = (destinationPath + imagePath)
                Dim Gooday As String = "Hello! " + dr.GetString("first_name") + " Welcome back"


                lblFname.Text = dr.GetString("fullName")
                lblPosition.Text = dr.GetString("position")
                lblFirsts.Text = Gooday

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
                Pgender.Text = dr.GetString("sex")

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

    Public Sub SetEPPlusLicenseContext()
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial
    End Sub
    Public Sub ExportToExcel(BenefeciariesDGV As DataGridView, filePath As String)

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial
        ' Create a new Excel package
        Using package As New ExcelPackage()
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.Add("Employees")

            ' Add headers
            For j = 0 To BenefeciariesDGV.Columns.Count - 1
                worksheet.Cells(1, j + 1).Value = BenefeciariesDGV.Columns(j).HeaderText
            Next

            ' Add data rows
            For i = 0 To BenefeciariesDGV.Rows.Count - 1
                For j = 0 To BenefeciariesDGV.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1).Value = BenefeciariesDGV.Rows(i).Cells(j).Value
                Next
            Next

            ' Apply auto-design
            Dim range As ExcelRange = worksheet.Cells(1, 1, BenefeciariesDGV.Rows.Count + 1, BenefeciariesDGV.Columns.Count)
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            range.Style.Font.Bold = True
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin

            ' Set background color for header
            Dim headerRange As ExcelRange = worksheet.Cells(1, 1, 1, BenefeciariesDGV.Columns.Count)
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGreen)
            headerRange.Style.Font.Color.SetColor(Color.Black)

            ' Set background color for rows
            Dim dataRange As ExcelRange = worksheet.Cells(2, 1, BenefeciariesDGV.Rows.Count + 1, BenefeciariesDGV.Columns.Count)
            dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid
            dataRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray)
            dataRange.Style.Font.Color.SetColor(Color.Black)

            'worksheet.Cells.AutoFitColumns()

            ' Set custom column width
            worksheet.Column(1).Width = 7.43 ' Column A
            worksheet.Column(2).Width = 32 ' Column B
            worksheet.Column(3).Width = 8 ' Column C
            worksheet.Column(4).Width = 18.57 ' Column D
            worksheet.Column(5).Width = 20 ' Column E
            worksheet.Column(6).Width = 24 ' Column F


            ' Save the Excel package to a file
            Dim fileInfo As New FileInfo(filePath)
            package.SaveAs(fileInfo)
        End Using

        ' Open the file
        Dim processStartInfo As New ProcessStartInfo()
        processStartInfo.FileName = filePath
        processStartInfo.UseShellExecute = True
        Process.Start(processStartInfo)
    End Sub

    Private Sub OpenFile(filePath As String)
        Dim fileName As String = Path.GetFileName(filePath)

        Dim pStartInfo As New ProcessStartInfo()
        pStartInfo.FileName = "explorer.exe"
        pStartInfo.Arguments = "/open," & filePath

        Dim p As Process = Process.Start(pStartInfo)
    End Sub
    Private Const SW_SHOWDEFAULT As Integer = 10

    Public Sub Update()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE users " &
                                        "INNER JOIN user_info ON users.id = user_info.user_id " &
                                        "SET users.username = @username, users.password = @password, users.first_name = @first, users.middle_name = @mid, users.last_name = @last, users.position = @pos, user_info.address = @adds, user_info.contact = @contact, user_info.email = @email, user_info.educational = @educ, user_info.birthdate = @birthdate, user_info.office = @office, user_info.employment_status = @employ, user_info.committee = @comm " &
                                        "WHERE users.id     = @ID", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@ID", Form2.log_id)
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

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Try
            Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            Dim filePath As String = Path.Combine(documentsPath, "employee.xlsx")

            ExportToExcel(BeneficiariesDGV, filePath)
            MessageBox.Show("Export complete.", "Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Dim AnswerYes As String
        AnswerYes = MsgBox("Are you sure you want to Log out", vbQuestion + vbYesNo, "User Repsonse")

        If AnswerYes = vbYes Then
            Form2.Show()
            Me.Hide()
        End If
    End Sub


End Class