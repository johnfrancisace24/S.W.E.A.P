Imports System.IO
Imports MySql.Data.MySqlClient
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Text.RegularExpressions

Public Class user_dashboard
    '
    Dim conn As New MySqlConnection("server=172.30.206.180;port=3306;username=dswd;password=sweap123;database=sweap")
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
            conn.Open()  ' Opens a connection to the database.
            Dim cmd As New MySqlCommand("SELECT users.*, SUM(contributions.union_dues) as union_d, SUM(contributions.bereavement) as bereav FROM users 
                                LEFT JOIN contributions on users.id = contributions.user_id 
                                WHERE users.id = @id", conn)  ' Creates a new MySqlCommand object to retrieve data from the "users" table and calculate the sum of union dues and bereavement contributions using a LEFT JOIN with the "contributions" table. The query filters the results based on the user's id (Form2.log_id).
            cmd.Parameters.AddWithValue("@id", Form2.log_id) ' Passes the log_id from Form2 as a parameter to the query.
            dr = cmd.ExecuteReader ' Executes the query and populates the DataReader object "dr" with the results.
            While dr.Read ' While reading the results from the DataReader...
                If Not dr.IsDBNull(dr.GetOrdinal("balance")) Then ' Checks if the "balance" column is not null for the current row.
                    txtSaving.Text = dr.GetString("balance")  ' Sets the text of txtSaving TextBox to the value of the "balance" column.
                Else
                    txtSaving.Text = 0 ' If the "balance" column is null, sets the text of txtSaving TextBox to 0.
                End If

                If Not dr.IsDBNull(dr.GetOrdinal("union_d")) Then ' Checks if the "union_d" column is not null for the current row.
                    txtUdues.Text = dr.GetString("union_d") ' Sets the text of txtUdues TextBox to the value of the "union_d" column.
                Else
                    txtUdues.Text = 0 ' If the "union_d" column is null, sets the text of txtUdues TextBox to 0.
                End If

                If Not dr.IsDBNull(dr.GetOrdinal("bereav")) Then ' Checks if the "bereav" column is not null for the current row.
                    txtBreavement.Text = dr.GetString("bereav") ' Sets the text of txtBreavement TextBox to the value of the "bereav" column.
                Else
                    txtBreavement.Text = 0 ' If the "bereav" column is null, sets the text of txtBreavement TextBox to 0.
                End If
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close() ' Closes the database connection.
        End Try
    End Sub

    'LOG OUT-------------------------
    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Dim AnswerYes As String
        AnswerYes = MessageBox.Show("Are you sure you want to Log out?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If AnswerYes = vbYes Then
            Guna2TabControl1.SelectedTab = tabDashboard ' Selects the "tabDashboard" tab in the Guna2TabControl.
            Me.Hide() ' Hides the current form.
            Form2.Show() ' Shows the Form2.
            lblFromTitle.Text = "Home" ' Sets the text of lblFromTitle label to "Home".
            iconFromtitle.Image = Image.FromFile(destinationIconPath + Home) ' Sets the image of iconFromtitle PictureBox to the image located at the destinationIconPath concatenated with the "Home" file name.
        End If

    End Sub


    Private Sub Guna2TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles Guna2TabControl1.Selected
        If Guna2TabControl1.SelectedTab Is tabDashboard Then 'If the selected tab is "tabDashboard"...
            lblFromTitle.Text = "Dashboard" ' Sets the text of lblFromTitle label to "Dashboard"
            iconFromtitle.Image = Image.FromFile(destinationIconPath + dashPath) ' Sets the image of iconFromtitle PictureBox to the image located at the destinationIconPath concatenated with the dashPath file name.
            Dashboard() ' Calls the Dashboard() function to populate the dashboard information.
        ElseIf Guna2TabControl1.SelectedTab Is tabProfile Then ' If the selected tab is "tabProfile"...
            lblFromTitle.Text = "Profile" ' Sets the text of lblFromTitle label to "Profile".
            iconFromtitle.Image = Image.FromFile(destinationIconPath + profPath) ' Sets the image of iconFromtitle PictureBox to the image located at the destinationIconPath concatenated with the profPath file name.
            Get_info() ' Calls the Get_info() function to retrieve and display the user's profile information.
        ElseIf Guna2TabControl1.SelectedTab Is tabBeneficiary Then ' If the selected tab is "tabBeneficiary"...
            lblFromTitle.Text = "Beneficiary" ' Sets the text of lblFromTitle label to "Beneficiary".
            iconFromtitle.Image = Image.FromFile(destinationIconPath + benefPath) ' Sets the image of iconFromtitle PictureBox to the image located at the destinationIconPath concatenated with the benefPath file name.
            DG_Load() ' Calls the DG_Load() function to load and display the beneficiary data in the DataGridView.
        ElseIf Guna2TabControl1.SelectedTab Is tabSetting Then ' If the selected tab is "tabSetting"...
            lblFromTitle.Text = "Account Setting" ' Sets the text of lblFromTitle label to "Account Setting".
            iconFromtitle.Image = Image.FromFile(destinationIconPath + settingPath) ' Sets the image of iconFromtitle PictureBox to the image located at the destinationIconPath concatenated with the settingPath file name.
        Else ' For any other tab...
            lblFromTitle.Text = "Home" ' Sets the text of lblFromTitle label to "Home".
            iconFromtitle.Image = Image.FromFile(destinationIconPath + Home) ' Sets the image of iconFromtitle PictureBox to the image located at the destinationIconPath concatenated with the Home file name.
        End If
    End Sub


    'FETCHED ALL BENEFICIARY OF USER-------------------------------
    Public Sub DG_Load()
        BeneficiariesDGV.Rows.Clear()
        Try
            conn.Open() ' Opens a connection to the database.
            Dim cmd As New MySqlCommand("SELECT beneficiaries.* FROM users 
                                INNER JOIN beneficiaries ON users.id = beneficiaries.user_id
                                WHERE users.id = @ID", conn)  ' Creates a new MySqlCommand object to retrieve data from the "beneficiaries"
            ' table by joining it with the "users" table based on the user_id. The query filters the results based on the user's id (Form2.log_id).
            cmd.Parameters.AddWithValue("@ID", Form2.log_id)  'Passes the log_id from Form2 as a parameter to the query.
            dr = cmd.ExecuteReader() ' Executes the query and populates the DataReader object "dr" with the results.
            While dr.Read() ' While reading the results from the DataReader...
                BeneficiariesDGV.Rows.Add(dr.Item("user_id"), dr.Item("full_name"), dr.Item("age"), dr.Item("relationship"))
                ' Adds a new row to the BeneficiariesDGV DataGridView and populates it with the values from the current row of the DataReader.
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()  'Closes the database connection.
        End Try
    End Sub


    'FETCH ALL INFO OF THE USER------------------------
    Public Sub Get_info()
        Try
            conn.Open() ' Opens a connection to the database.
            Dim cmd As New MySqlCommand("SELECT *, CONCAT(first_name, ' ', middle_name, ' ', last_name) AS fullName FROM users 
                            INNER JOIN user_info ON users.id = user_info.user_id  
                            WHERE users.id = @ID", conn)  ' Creates a new MySqlCommand object to retrieve all information of the user by joining the "users" table with the "user_info" table
            ' based on the user_id. The query filters the results based on the user's id (Form2.log_id).
            cmd.Parameters.AddWithValue("@ID", Form2.log_id) ' Passes the log_id from Form2 as a parameter to the query.
            dr = cmd.ExecuteReader ' Executes the query and populates the DataReader object "dr" with the results.
            While dr.Read ' While reading the results from the DataReader...
                Dim imagepathResources As String = destinationPath + dr.GetString("image") ' Retrieves the image path from the result and concatenates it with the destinationPath to get the complete image path.

                If File.Exists(imagepathResources) Then ' If the image file exists at the specified path...
                    userProfile.Image = Image.FromFile(imagepathResources) ' Sets the image of the userProfile PictureBox to the image located at the imagepathResources.
                    user_Profile.Image = Image.FromFile(imagepathResources) ' Sets the image of the user_Profile PictureBox to the image located at the imagepathResources.
                Else ' If the image file does not exist...
                    userProfile.Image = Nothing ' Clears the image of the userProfile PictureBox.
                    user_Profile.Image = Nothing ' Clears the image of the user_Profile PictureBox.
                End If

                ' Determines the appropriate greeting based on the selected value of cmboSex ComboBox.
                If cmboSex.SelectedIndex = 0 Then
                    lblDateTime.Text = "Mr. " + dr.GetString("first_name") + " Your last log in was " + dr.GetString("last_logout")
                ElseIf cmboSex.SelectedIndex = 1 Then
                    lblDateTime.Text = "Ms. " + dr.GetString("first_name") + " Your last log in was " + dr.GetString("last_logout")
                ElseIf cmboSex.SelectedIndex = 2 Then
                    lblDateTime.Text = "Hi " + dr.GetString("first_name") + " Your last log in was " + dr.GetString("last_logout")
                End If

                ' Populates the various labels and text boxes with the retrieved information.
                Pfname.Text = dr.GetString("fullName")
                Padd.Text = dr.GetString("address")
                Pcntact.Text = dr.GetString("contact")
                Pemail.Text = dr.GetString("email")
                Pbdate.Text = dr.GetDateTime("birthdate").ToString()

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
                txtbxbdate.Value = dr.GetDateTime("birthdate")

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
            conn.Open() ' Opens a connection to the database.
            Dim cmd As New MySqlCommand("SELECT * FROM beneficiaries WHERE full_name LIKE '%" & search.Text & "%' AND user_id = @ID", conn) ' Creates a new MySqlCommand object to search for beneficiaries whose full_name matches the search text and belong to the current user (Form2.log_id).
            cmd.Parameters.AddWithValue("@ID", Form2.log_id) ' Passes the log_id from Form2 as a parameter to the query.
            dr = cmd.ExecuteReader ' Executes the query and populates the DataReader object "dr" with the results.
            While dr.Read ' While reading the results from the DataReader...
                BeneficiariesDGV.Rows.Add(dr.Item("user_id"), dr.Item("full_name"), dr.Item("age"), dr.Item("relationship")) ' Adds a new row to the BeneficiariesDGV DataGridView and populates it with the values from the current row of the DataReader.
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close() ' Closes the database connection.
        End Try
    End Sub



    'UPDATE FUNCTION---------------------------------------------
    Public Sub Update()
        Try
            conn.Open() ' Opens a connection to the database.
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
            conn.Close() ' Closes the connection to the database.
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
        ' Set the EPPlus license context to NonCommercial
        ' This allows the use of EPPlus library in a non-commercial context
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial
    End Sub
    Public Sub ExportToExcel(BenefeciariesDGV As DataGridView, filePath As String)
        ' Set the EPPlus license context
        SetEPPlusLicenseContext()

        ' Create a new Excel package
        Using package As New ExcelPackage()

            ' Add a worksheet to the package
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets.Add("Employees")

            ' Write column headers to the worksheet
            For j = 0 To BenefeciariesDGV.Columns.Count - 1
                worksheet.Cells(1, j + 1).Value = BenefeciariesDGV.Columns(j).HeaderText
            Next

            ' Write data rows to the worksheet
            For i = 0 To BenefeciariesDGV.Rows.Count - 1
                For j = 0 To BenefeciariesDGV.Columns.Count - 1
                    worksheet.Cells(i + 2, j + 1).Value = BenefeciariesDGV.Rows(i).Cells(j).Value
                Next
            Next

            ' Define a range for styling the header
            Dim headerRange As ExcelRange = worksheet.Cells(1, 1, 1, BenefeciariesDGV.Columns.Count)

            ' Set background color, font color, and border for the header
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGreen)
            headerRange.Style.Font.Color.SetColor(Color.Black)
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin

            ' Define a range for styling the data rows
            Dim dataRange As ExcelRange = worksheet.Cells(2, 1, BenefeciariesDGV.Rows.Count + 1, BenefeciariesDGV.Columns.Count)

            ' Set background color, font color, and border for the data rows
            dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid
            dataRange.Style.Font.Color.SetColor(Color.Black)
            dataRange.Style.Fill.BackgroundColor.SetColor(Color.White)
            dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin

            ' Set column widths for specific columns
            worksheet.Column(1).Width = 7.43 ' Column A
            worksheet.Column(2).Width = 42 ' Column B
            worksheet.Column(3).Width = 8 ' Column C
            worksheet.Column(4).Width = 18.57 ' Column D

            ' Save the Excel package to the specified file path
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
            conn.Open() ' Opens a connection to the database.
            Dim cmd As New MySqlCommand("SELECT * FROM users WHERE id=@id", conn) ' Creates a new MySqlCommand object to retrieve data from the "users" table where the id matches Form2.log_id.
            cmd.Parameters.AddWithValue("@id", Form2.log_id) ' Passes the log_id from Form2 as a parameter to the query.
            dr = cmd.ExecuteReader ' Executes the query and populates the DataReader object "dr" with the results.
            While dr.Read ' While reading the results from the DataReader...
                Dim documentsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) ' Retrieves the path for the My Documents folder.
                Dim filePath As String = Path.Combine(documentsPath, dr.GetString("first_name") & "_beneficiary.xlsx") ' Combines the My Documents folder path with the user's first name from the query results. This will be the location and name of the output Excel file.
                Dim fileName As String = dr.GetString("first_name") & "_beneficiary.xlsx" ' The name of the output Excel file based on the user's first name from the query results.
                If File.Exists(filePath) Then ' IF THE FILE ALREADY EXISTS -------
                    ' If yes, displays an alert box indicating that the file already exists along with its location.
                    MessageBox.Show("The file already exists; this is the file location" & vbCrLf & filePath, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    ExportToExcel(BeneficiariesDGV, filePath) ' If the file doesn't exist yet, performs the export to Excel using the ExportToExcel function. Passes BeneficiariesDGV (DataGridView) and the path of the output file as arguments.
                    MessageBox.Show("Export completed. The file name is " & fileName, "Excel file", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ' Displays an alert box indicating that the export is completed and also shows the name of the output file.
                End If
            End While
        Catch ex As Exception
            MsgBox(ex.Message) ' Closes the database connection.
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