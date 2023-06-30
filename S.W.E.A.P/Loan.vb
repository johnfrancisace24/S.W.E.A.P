Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports OfficeOpenXml
Imports MySql.Data.MySqlClient
Imports DocumentFormat.OpenXml.Office.Word
Imports OfficeOpenXml.Style
Imports System.IO

Public Class Loan
    '-----------------------------------VARIABLE DECLARATION------------------------------------------
    Dim loanAmount As Decimal
    Dim annualInterestRate As Decimal
    Dim loanPeriodInYears As Integer
    Dim numberOfPaymentsPerYear As Integer
    Dim monthlyInterestRate As Decimal
    Dim totalNumberOfPayments As Integer
    Dim scheduledPayment As Decimal
    Dim extraPayment As Integer
    Dim payment As Integer
    Dim beginningBalance As Double
    Dim endBalance As Double
    Dim principal As Double
    Dim totalPayment As Double
    Dim interest As Double
    Dim CumuInterest As Double
    Dim selectedDate As DateTime
    Dim totalEarlyPayment As Integer
    Dim error_msg(0) As String
    Dim random As Integer = 0
    Dim i As Integer = 0
    Dim message As String
    Dim conn As New MySqlConnection("server=172.30.192.162;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader
    Dim selectedId As Integer = 0
    Dim loanSchedId As Integer
    '------------------------------------VARIABLE DECLARATION FOR CONTRIBUTIONS----------------------------------------------
    Dim updatedMonth As Integer
    Dim updatedYear As Integer
    Dim updatedWeek As Integer
    Dim updatedDay As Integer
    '-----------------------------------------------END OF CONTRIBUTION'S VARIABLE-------------------------------------------

    '-----------------------------------END OF VARIABLE DECLARATION-------------------------------------------
    '--------------------------------------FUNCTIONS----------------------------------------------------------

    Public Function IsFileExists(filePath As String) As Boolean
        Return File.Exists(filePath)
    End Function

    Public Sub common_calculation()
        totalPayment = scheduledPayment + extraPayment
        interest = beginningBalance * monthlyInterestRate
        principal = totalPayment - interest
        endBalance = beginningBalance - principal
        CumuInterest = CumuInterest + interest
    End Sub
    Public Sub common_process()
        payment = 1
        selectedDate = dateStart.Value.Date
        extraPayment = numXtraP.Value
        loanAmount = numLamount.Value
        annualInterestRate = numAintRate.Value / 100
        loanPeriodInYears = numPyears.Value
        numberOfPaymentsPerYear = numPayYears.Value
        beginningBalance = loanAmount

        '--------------Convert the annual interest rate to monthly interest rate
        monthlyInterestRate = annualInterestRate / numberOfPaymentsPerYear

        '---------------Calculate the total number of payments
        totalNumberOfPayments = loanPeriodInYears * numberOfPaymentsPerYear

        '----------------Calculate the scheduled payment amount
        scheduledPayment = (monthlyInterestRate * loanAmount) / (1 - (1 + monthlyInterestRate) ^ (-totalNumberOfPayments))

        '------------------Round the scheduled payment amount to two decimal places
        scheduledPayment = Math.Round(scheduledPayment, 2)

        '--------------------------------------------OVERALL CALCULATION-----------------------------------------------------
    End Sub

    Public Sub validation(field, condition, msg)
        If field <= condition Then
            error_msg(random) = msg & vbNewLine
            random = random + 1
            ReDim Preserve error_msg(random)
        End If
    End Sub
    Public Sub reset()
        numXtraP.Value = 0
        numLamount.Value = 0
        numAintRate.Value = 0
        numPyears.Value = 0
        numPayYears.Value = 0
        loanAmount = 0
        btnSetSched.Enabled = True
        txtSnumberPayment.Text = ""
        txtSpayment.Text = ""
        txtActualNumPayment.Text = ""
        txtTotalEarlyPayment.Text = ""
        txtTotalInterest.Text = ""
        CumuInterest = 0
        dgSchedule.Rows.Clear()
        selectedId = 0
        txtLenderName.Text = ""
        txtName.Text = ""
    End Sub
    Public Sub mathing()
        beginningBalance = Math.Round(beginningBalance, 2)
        totalPayment = Math.Round(totalPayment, 2)
        interest = Math.Round(interest, 2)
        principal = Math.Round(principal, 2)
        endBalance = Math.Round(endBalance, 2)
        CumuInterest = Math.Round(CumuInterest, 2)
    End Sub

    Public Sub contriGrid() '--------------------FOR CONTRIBUTION TABLE
        dgContribution.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select user_id, concat(users.first_name, ' ', users.middle_name, ' ', users.last_name) as full_name, users.position, sum(membership_fee) as membership,
                                        sum(union_dues) as union_due, sum(bereavement) as bereavement, sum(contribution4) as con4, sum(contribution5) as con5, contributions.updated_at from contributions left join users
                                            on contributions.user_id = users.id group by contributions.user_id", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                dgContribution.Rows.Add(rid.Item("user_id"), rid.Item("full_name"), rid.Item("position"), rid.Item("membership"), rid.Item("union_due"), rid.Item("bereavement"), rid.Item("con4"), rid.Item("con5"), rid.Item("updated_at"))
            End While
        Catch ex As Exception
            MsgBox("Fetching contribution table doesn't work. Function name contriGrid()")
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub reset_contributions() '---------------------TO RESET OF CONTRIBUTIONS CLASS
        Array.Clear(contributions, 0, contributions.Length)
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select * from contri_types", conn)
            Dim counter As Integer = 0
            rid = cmd.ExecuteReader
            While rid.Read
                contributions(counter) = New class_contribution(rid.Item("contribution_name"), rid.Item("periodity"), rid.Item("amount"))
                dgContribution.Columns(3 + counter).HeaderText = rid.Item("alias")
                counter = counter + 1
            End While
        Catch ex As Exception
            MsgBox("Fetching of data failed from reset_contributions() function")
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub contriEditFields(status)
        btnUpdateContriType.Enabled = status
        pickContriName.Enabled = status
        pickContriEditPeriod.Enabled = status
        txtNewContriName.Enabled = status
        numContriEditAmount.Enabled = status
    End Sub


    '------------------------------------END OF FUNCTIONS-----------------------------------------------------

    Private Sub btnSetSched_Click(sender As Object, e As EventArgs) Handles btnSetSched.Click '-------SET SCHEDULE BUTTON
        validation(numLamount.Value, 999, "Loan can't be less than 1000.")
        validation(numAintRate.Value, 0, "Anual Interest rate can't be less than or equal to 0.")
        validation(numPyears.Value, 0, "Loan Period in years can't be less than or equal to 0.")
        validation(numPayYears.Value, 11, "Number of payment per year can't be less than 12.")
        If selectedId = 0 Then
            error_msg(random) = "Lender name can't be blank." & vbNewLine
            random = random + 1
            ReDim Preserve error_msg(random)
        End If
        While i < error_msg.Length
            message = message & error_msg(i)
            i = i + 1
        End While

        If message = "" Then
            dgSchedule.Rows.Clear()
            common_process()
            common_calculation()
            While beginningBalance >= 0
                mathing()
                If beginningBalance < scheduledPayment Or beginningBalance < totalPayment Then
                    extraPayment = 0
                    totalPayment = beginningBalance
                    principal = totalPayment - interest
                    endBalance = 0
                End If
                dgSchedule.Rows.Add(payment, selectedDate, "₱" & beginningBalance, "₱" & scheduledPayment, "₱" & extraPayment, "₱" & totalPayment, "₱" & principal, "₱" & interest, "₱" & endBalance, "₱" & CumuInterest)
                If endBalance = 0 Then
                    beginningBalance = 0
                    Exit While
                End If
                totalEarlyPayment = totalEarlyPayment + extraPayment
                selectedDate = selectedDate.AddMonths(1)
                beginningBalance = beginningBalance - principal
                common_calculation()
                payment = payment + 1

            End While

            '---------------------------------------------END OF CALCULATION-------------------------------------------------------

            '--------------------------------------RESULT-------------------------------------------------------
            txtSnumberPayment.Text = numberOfPaymentsPerYear * loanPeriodInYears
            txtSpayment.Text = scheduledPayment
            txtActualNumPayment.Text = payment
            txtTotalEarlyPayment.Text = totalEarlyPayment
            txtTotalInterest.Text = CumuInterest
            txtName.Text = txtLenderName.Text
            '-----------------------------------END OF RESULT--------------------------------------------------
            btnSetSched.Enabled = False
            btnApprove.Enabled = True
        Else
            MessageBox.Show(message, "Invalid Input")
            i = 0
            message = ""
            Array.Clear(error_msg, 0, error_msg.Length)
        End If

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
        btnApprove.Enabled = False
    End Sub


    Private Sub btnSelectName_Click(sender As Object, e As EventArgs) Handles btnSelectName.Click
        dgSelectEm.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select concat(first_name, ' ', middle_name, ' ', last_name) as full_name, username, id from users", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                dgSelectEm.Rows.Add(rid.Item("id"), rid.Item("full_name"), rid.Item("username"))
            End While
        Catch ex As Exception
            MsgBox("doesn't work lmao")
        Finally
            conn.Close()
        End Try
        pnlSelectLender.Visible = True
    End Sub

    Private Sub btnBackPanel_Click(sender As Object, e As EventArgs) Handles btnBackPanel.Click
        pnlSelectLender.Visible = False
    End Sub
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to proceed?", "Confirmation", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            CumuInterest = 0
            common_process()
            common_calculation()
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("insert into loan_info(user_id, loan_amount, anual_interest_rate, loan_period_years, 
                                            no_payments_per_year, start_date_of_loan, optional_xtra) values(@ID, @ALOAN, @ARATE, @LYEARS, @NOPYEAR, @SDATE, @XTRA);", conn)
                cmd.Parameters.AddWithValue("@ID", selectedId)
                cmd.Parameters.AddWithValue("@ALOAN", numLamount.Value)
                cmd.Parameters.AddWithValue("@ARATE", numAintRate.Value)
                cmd.Parameters.AddWithValue("@LYEARS", numPyears.Value)
                cmd.Parameters.AddWithValue("@NOPYEAR", numPayYears.Value)
                cmd.Parameters.AddWithValue("@SDATE", dateStart.Value.Date)
                cmd.Parameters.AddWithValue("@XTRA", numXtraP.Value)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Loan_info doesn't work")
            Finally
                conn.Close()
            End Try
            While beginningBalance >= 0
                mathing()
                If beginningBalance < scheduledPayment Or beginningBalance < totalPayment Then
                    extraPayment = 0
                    totalPayment = beginningBalance
                    principal = totalPayment - interest
                    endBalance = 0
                End If

                Try
                    conn.Open()
                    Dim cmd As New MySqlCommand("INSERT INTO loans(user_id, loan_id, pmt_no, payment_date, beginning_balance, scheduled_payment, extra_payment,
                                                total_payment, principal, interest, ending_balance, cumulative_interest) VALUES(@ID, (select id from loan_info where loan_amount=@ALOAN and user_id=@UID),@PAYMENT,
                                                    @DATE, @BEGBAL, @SCHEDP, @XTRA, @TPAYMENT, @PRINCIPAL, @INTEREST, @ENDBAL, @CUMINTEREST)", conn)
                    cmd.Parameters.AddWithValue("@ALOAN", numLamount.Value)
                    cmd.Parameters.AddWithValue("@UID", selectedId)
                    cmd.Parameters.AddWithValue("@ID", selectedId)
                    cmd.Parameters.AddWithValue("@PAYMENT", payment)
                    cmd.Parameters.AddWithValue("@DATE", selectedDate)
                    cmd.Parameters.AddWithValue("@BEGBAL", beginningBalance)
                    cmd.Parameters.AddWithValue("@SCHEDP", scheduledPayment)
                    cmd.Parameters.AddWithValue("@XTRA", extraPayment)
                    cmd.Parameters.AddWithValue("@TPAYMENT", totalPayment)
                    cmd.Parameters.AddWithValue("@PRINCIPAL", principal)
                    cmd.Parameters.AddWithValue("@INTEREST", interest)
                    cmd.Parameters.AddWithValue("@ENDBAL", endBalance)
                    cmd.Parameters.AddWithValue("@CUMINTEREST", CumuInterest)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("Failed to insert data: " & ex.Message)
                Finally
                    conn.Close()
                End Try
                If endBalance = 0 Then
                    beginningBalance = 0
                    Exit While
                End If
                totalEarlyPayment = totalEarlyPayment + extraPayment
                selectedDate = selectedDate.AddMonths(1)
                beginningBalance = beginningBalance - principal
                common_calculation()
                payment = payment + 1

            End While

            MsgBox("Loan added successfully!")
            reset()
        End If


    End Sub

    Private Sub Form2_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load '------------AUTOLOAD
        btnLoanToExcel.Enabled = False
        contriEditFields(False)
        reset_contributions()
        contriGrid()
        contriTimer.Start()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select month(updated_at) as month, year(updated_at) as year, week(updated_at) as week, day(updated_at) as day from contributions order by updated_at DESC limit 1", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                updatedMonth = rid.GetInt32("month")
                updatedYear = rid.GetInt32("year")
                updatedWeek = rid.GetInt32("week")
                updatedDay = rid.GetInt32("day")
            End While
        Catch ex As Exception
        Finally
            conn.Close()
        End Try
        dgSelectEm.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select concat(first_name, ' ', middle_name, ' ', last_name) as full_name, username, id from users", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                dgEmList.Rows.Add(rid.Item("id"), rid.Item("full_name"), rid.Item("username"))
            End While
        Catch ex As Exception
            MsgBox("doesn't work lmao")
        Finally
            conn.Close()
        End Try

        btnApprove.Enabled = False
        pnlSelectLender.Visible = False
        Guna2Button1.Enabled = False '-------------LOCK BUTTON
    End Sub

    Private Sub Guna2CircleButton1_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton1.Click
        admindash.Show()
        Me.Close()
    End Sub

    Private Sub dgSelectEm_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgSelectEm.CellClick
        If e.ColumnIndex = 3 AndAlso e.RowIndex >= 0 Then '----------------TO SELECT
            If String.IsNullOrEmpty(dgSelectEm.CurrentRow.Cells(0).Value.ToString()) = False Then
                selectedId = dgSelectEm.CurrentRow.Cells(0).Value.ToString()
                txtLenderName.Text = dgSelectEm.CurrentRow.Cells(1).Value.ToString()
                pnlSelectLender.Visible = False
            End If
        End If
    End Sub

    '-------------------------------------------------------VIEW LOANS---------------------------------------------------------------------------
    Private Sub dgEmList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgEmList.CellClick
        Dim idSelect As Integer
        If e.ColumnIndex = 3 AndAlso e.RowIndex >= 0 Then '----------------TO SELECT
            btnLoanToExcel.Enabled = False
            dgLoans.Rows.Clear()
            dgLoanSchedule.Rows.Clear()
            idSelect = dgEmList.CurrentRow.Cells(0).Value.ToString()
            loanSchedId = idSelect
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("select * from loan_info where user_id=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", idSelect)
                rid = cmd.ExecuteReader
                While rid.Read
                    dgLoans.Rows.Add(rid.Item("id"), rid.Item("loan_amount"), rid.Item("anual_interest_rate"), rid.Item("loan_period_years"), rid.Item("no_payments_per_year"), rid.Item("start_date_of_loan"), rid.Item("optional_xtra"))
                End While
            Catch ex As Exception
                MsgBox("EW error")
            Finally
                conn.Close()
            End Try
        End If
    End Sub

    Private Sub dgLoans_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgLoans.CellClick
        Dim idSelect As Integer

        If e.ColumnIndex = 7 AndAlso e.RowIndex >= 0 Then '----------------TO SELECT
            idSelect = dgLoans.CurrentRow.Cells(0).Value.ToString()
            btnLoanToExcel.Enabled = True
            dgLoanSchedule.Rows.Clear()
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("select * from loans where loan_id=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", idSelect)
                rid = cmd.ExecuteReader
                While rid.Read
                    dgLoanSchedule.Rows.Add(rid.Item("pmt_no"), rid.Item("payment_date"), rid.Item("beginning_balance"), rid.Item("scheduled_payment"), rid.Item("extra_payment"), rid.Item("total_payment"), rid.Item("principal"), rid.Item("interest"), rid.Item("ending_balance"), rid.Item("cumulative_interest"))
                End While
            Catch ex As Exception
                MsgBox("EW error")
            Finally
                conn.Close()
            End Try
        End If
    End Sub


    Private Sub btnLoanToExcel_Click(sender As Object, e As EventArgs) Handles btnLoanToExcel.Click '---------CREATE EXCEL FILE
        Dim filePath As String
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial

        Using package As New ExcelPackage()
            Dim workbook As ExcelWorkbook = package.Workbook
            Dim worksheet As ExcelWorksheet = workbook.Worksheets.Add("Sheet1") ' Add a worksheet
            Dim counter As Integer = 13
            Dim totalXtraP As Integer

            worksheet.Cells("A2").Value = "ENTERED VALUES"
            worksheet.Cells("A3").Value = "Loan amount"
            worksheet.Cells("A4").Value = "Annual interest rate"
            worksheet.Cells("A5").Value = "Loan period in years"
            worksheet.Cells("A6").Value = "Loan amount"
            worksheet.Cells("A7").Value = "Annual interest rate"
            worksheet.Cells("A9").Value = "Loan period in years"


            For columnIndex As Integer = 1 To 8 ' Columns A to H
                worksheet.Column(columnIndex).Width = 50
            Next

            Dim columnRangeTops As ExcelRange = worksheet.Cells("A2:A9")
            columnRangeTops.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            columnRangeTops.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)
            columnRangeTops.Style.Font.Color.SetColor(System.Drawing.Color.Black)
            columnRangeTops.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
            columnRangeTops.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black)
            columnRangeTops.Style.Font.Bold = True


            worksheet.Cells("G2").Value = "LOAN SUMMARY"
            worksheet.Cells("G3").Value = "Scheduled Payment"
            worksheet.Cells("G4").Value = "Scheduled number of payments"
            worksheet.Cells("G5").Value = "Actual number of payments"
            worksheet.Cells("G6").Value = "Total early payments"
            worksheet.Cells("G7").Value = "Total Interest"
            worksheet.Cells("G9").Value = "LENDER NAME"

            Dim columnRangeG As ExcelRange = worksheet.Cells("G2:G9")
            columnRangeG.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            columnRangeG.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)
            columnRangeG.Style.Font.Color.SetColor(System.Drawing.Color.Black)
            columnRangeG.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
            columnRangeG.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black)
            columnRangeG.Style.Font.Bold = True

            Try
                conn.Open()
                Dim cmd As New MySqlCommand("select loan_info.*, concat(first_name, ' ', middle_name, ' ', last_name) as fullname, username from loan_info left join users on loan_info.user_id = users.id where user_id=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", loanSchedId)
                rid = cmd.ExecuteReader
                While rid.Read
                    worksheet.Cells("B3").Value = rid.Item("loan_amount")
                    worksheet.Cells("B4").Value = rid.Item("anual_interest_rate")
                    worksheet.Cells("B5").Value = rid.Item("loan_period_years")
                    worksheet.Cells("B6").Value = rid.Item("no_payments_per_year")
                    worksheet.Cells("B8").Value = rid.Item("start_date_of_loan")
                    worksheet.Cells("B9").Value = rid.Item("optional_xtra")
                    worksheet.Cells("H4").Value = rid.Item("no_payments_per_year") * rid.Item("loan_period_years")
                    worksheet.Cells("H9").Value = rid.Item("fullname")
                    filePath = rid.Item("username")
                End While
            Catch ex As Exception
                MsgBox("doesntwork")
            Finally
                conn.Close()
            End Try

            Dim columnRangeB As ExcelRange = worksheet.Cells("B3:H9")
            columnRangeB.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            columnRangeB.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)
            columnRangeB.Style.Font.Color.SetColor(System.Drawing.Color.Black)
            columnRangeB.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
            columnRangeB.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black)
            columnRangeB.Style.Font.Bold = True

            worksheet.Cells("H5").Value = dgLoanSchedule.RowCount
            worksheet.Cells("A11").Value = dgLoanSchedule.Columns(0).HeaderText
            worksheet.Cells("B11").Value = dgLoanSchedule.Columns(1).HeaderText
            worksheet.Cells("C11").Value = dgLoanSchedule.Columns(2).HeaderText
            worksheet.Cells("D11").Value = dgLoanSchedule.Columns(3).HeaderText
            worksheet.Cells("E11").Value = dgLoanSchedule.Columns(4).HeaderText
            worksheet.Cells("F11").Value = dgLoanSchedule.Columns(5).HeaderText
            worksheet.Cells("G11").Value = dgLoanSchedule.Columns(6).HeaderText
            worksheet.Cells("H11").Value = dgLoanSchedule.Columns(7).HeaderText
            worksheet.Cells("J11").Value = dgLoanSchedule.Columns(8).HeaderText
            worksheet.Cells("I11").Value = dgLoanSchedule.Columns(9).HeaderText

            Dim columnRange As ExcelRange = worksheet.Cells("A11:J11")
            columnRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            columnRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green)
            columnRange.Style.Font.Color.SetColor(System.Drawing.Color.White)
            columnRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
            columnRange.Style.Font.Bold = True


            ' Set column height
            worksheet.Row(11).Height = 20 ' Set the height of the header row

            For Each row As DataGridViewRow In dgLoanSchedule.Rows
                worksheet.Cells("A" & counter).Value = row.Cells(0).Value
                worksheet.Cells("B" & counter).Value = row.Cells(1).Value.ToString()
                worksheet.Cells("C" & counter).Value = row.Cells(2).Value
                worksheet.Cells("D" & counter).Value = row.Cells(3).Value
                worksheet.Cells("E" & counter).Value = row.Cells(4).Value
                worksheet.Cells("F" & counter).Value = row.Cells(5).Value
                worksheet.Cells("G" & counter).Value = row.Cells(6).Value
                worksheet.Cells("H" & counter).Value = row.Cells(7).Value
                worksheet.Cells("I" & counter).Value = row.Cells(8).Value
                worksheet.Cells("J" & counter).Value = row.Cells(9).Value
                totalXtraP = totalXtraP + row.Cells(4).Value
                counter = counter + 1
                worksheet.Cells("H7").Value = row.Cells(9).Value
                worksheet.Cells("H3").Value = row.Cells(3).Value
            Next
            worksheet.Cells("H6").Value = totalXtraP

            ' Set fill color and font color for all data rows
            Dim dataRange As ExcelRange = worksheet.Cells("A13:J" & (counter - 1))
            Dim fill As ExcelFill = dataRange.Style.Fill
            fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            fill.BackgroundColor.SetColor(System.Drawing.Color.White)
            dataRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin
            dataRange.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black)
            Dim font As ExcelFont = dataRange.Style.Font
            font.Color.SetColor(System.Drawing.Color.Black)
            Dim locateProject As String = My.Application.Info.DirectoryPath
            Dim indext As Integer = locateProject.IndexOf("bin\Debug\net6.0-windows")
            Dim location As String = locateProject.Substring(0, indext)

            For columnIndex As Integer = 1 To 2
                worksheet.Column(columnIndex).Width = 30
            Next
            For columnIndex As Integer = 3 To 6
                worksheet.Column(columnIndex).Width = 20
            Next
            For columnIndex As Integer = 7 To 8
                worksheet.Column(columnIndex).Width = 30
            Next
            For columnIndex As Integer = 9 To 10
                worksheet.Column(columnIndex).Width = 20
            Next

            filePath = location & "\Resources\Exported_file\" & filePath & "_loan_Schedule.xlsx"

            If IsFileExists(filePath) Then
                MsgBox("File already exists.")
            Else
                package.SaveAs(New System.IO.FileInfo(filePath))
                MsgBox("File saved to " & filePath)
            End If

        End Using
    End Sub

    '------------------------------------------------------------CONTRIBUTIONS--------------------------------------------------------------------

    Dim contriCounter As Integer = 0
    Public Shared contributions(4) As class_contribution

    Public Class class_contribution
        Public contriName As String
        Public period As String
        Public amount As Integer
        Public Sub New(name As String, period As String, amount As Integer)
            Me.contriName = name
            Me.period = period
            Me.amount = amount
        End Sub
        Public Sub insertion()
            For Each row As DataGridViewRow In Loan.dgContribution.Rows
                If Not row.IsNewRow Then
                    Try
                        Loan.conn.Open()
                        Dim columnName As String = Me.contriName
                        Dim query As String = "insert into contributions(user_id, " & columnName & ", updated_at)values(@ID, @AMOUNT, now())"
                        Dim cmd As New MySqlCommand(query, Loan.conn)
                        cmd.Parameters.AddWithValue("@ID", row.Cells(0).Value.ToString())
                        cmd.Parameters.AddWithValue("@AMOUNT", Me.amount)
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                    Finally
                        Loan.conn.Close()
                    End Try
                End If
            Next
        End Sub

    End Class

    Private Sub contriTimer_Tick(sender As Object, e As EventArgs) Handles contriTimer.Tick
        Dim timezone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("singapore standard time")
        Dim currenttime As DateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timezone)
        Dim currentdate As DateTime = currenttime
        Dim currentweek As Integer = currentdate.DayOfYear / 7
        lblTime.Text = currentdate.Hour & " : " & currentdate.Minute & " : " & currentdate.Second

        If updatedMonth <> currentdate.Month Then
            For Each contribution As class_contribution In contributions
                If contribution.period = "Monthly" Then
                    contribution.insertion()
                End If
            Next
            contriGrid()
        End If

        If updatedYear <> currentdate.Year Then
            For Each contribution As class_contribution In contributions
                If contribution.period = "Annually" Then
                    contribution.insertion()
                End If
            Next
            contriGrid()
        End If

        If updatedWeek <> currentweek Then
            For Each contribution As class_contribution In contributions
                If contribution.period = "Weekly" Then
                    contribution.insertion()
                End If
            Next
            contriGrid()
        End If

        If updatedDay <> currentdate.Day Then
            For Each contribution As class_contribution In contributions
                If contribution.period = "Daily" Then
                    contribution.insertion()
                End If
            Next
            contriGrid()
        End If

        updatedMonth = currentdate.Month
        updatedYear = currentdate.Year
        updatedWeek = currentweek
        updatedDay = currentdate.Day

    End Sub

    Private Sub btnUpdateContriType_Click(sender As Object, e As EventArgs) Handles btnUpdateContriType.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("update contri_types set alias = @CN, amount = @AMOUNT, periodity = @PER, updated_at = now() where alias=@OCN;", conn)
            cmd.Parameters.AddWithValue("@CN", txtNewContriName.Text)
            cmd.Parameters.AddWithValue("@AMOUNT", numContriEditAmount.Value)
            cmd.Parameters.AddWithValue("@PER", pickContriEditPeriod.SelectedItem)
            cmd.Parameters.AddWithValue("@OCN", pickContriName.SelectedItem)
            cmd.ExecuteNonQuery()
            MsgBox("Update Successfully")
            dgContribution.Columns(pickContriName.SelectedIndex + 3).HeaderText = txtNewContriName.Text
        Catch ex As Exception
            MsgBox("Update doesn't work")
        Finally
            conn.Close()
        End Try
        contriEditFields(False)
    End Sub

    Private Sub btnOpenEdit_Click(sender As Object, e As EventArgs) Handles btnOpenEdit.Click
        pickContriName.Items.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select alias from contri_types", conn)
            rid = cmd.ExecuteReader
            While rid.Read
                pickContriName.Items.Add(rid.Item("alias"))
            End While
        Catch ex As Exception
        Finally
            conn.Close()
        End Try
        contriEditFields(True)
        btnOpenEdit.Enabled = False
        Guna2Button1.Enabled = True
    End Sub

    Private Sub pickContriName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles pickContriName.SelectedIndexChanged
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select * from contri_types where alias=@NAME", conn)
            cmd.Parameters.AddWithValue("@NAME", pickContriName.SelectedItem)
            rid = cmd.ExecuteReader
            While rid.Read
                pickContriEditPeriod.SelectedItem = rid.Item("periodity")
                numContriEditAmount.Value = rid.Item("amount")
            End While
        Catch ex As Exception
            MsgBox("Fetching data failed at pickContriName")
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click '-----------LOCK BUTTON FROM CONTRIBUTION
        contriEditFields(False)
        Guna2Button1.Enabled = False
        btnOpenEdit.Enabled = True
    End Sub

End Class

