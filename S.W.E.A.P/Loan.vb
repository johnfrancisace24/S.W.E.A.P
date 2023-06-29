Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Wordprocessing
Imports MySql.Data.MySqlClient
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
    '------------------------------------VARIABLE DECLARATION FOR CONTRIBUTIONS----------------------------------------------
    Dim updatedMonth As Integer
    Dim updatedYear As Integer
    Dim updatedWeek As Integer
    Dim updatedDay As Integer
    '-----------------------------------------------END OF CONTRIBUTION'S VARIABLE-------------------------------------------

    '-----------------------------------END OF VARIABLE DECLARATION-------------------------------------------
    '--------------------------------------FUNCTIONS----------------------------------------------------------
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
            MsgBox("datagrid doesnt wokr!")
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
                counter = counter + 1
            End While
        Catch ex As Exception
            MsgBox("Fetching of data failed from reset_contributions function")
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
        contriEditFields(False)
        reset_contributions()
        contriGrid()
        contriTimer.Start()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("select month(updated_at) as month, year(updated_at) as year, week(updated_at) as week, day(updated_at) as day from contributions", conn)
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
            dgLoans.Rows.Clear()
            dgLoanSchedule.Rows.Clear()
            idSelect = dgEmList.CurrentRow.Cells(0).Value.ToString()
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
            dgLoanSchedule.Rows.Clear()
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("select * from loans where loan_id=@ID", conn)
                cmd.Parameters.AddWithValue("@ID", idSelect)
                rid = cmd.ExecuteReader
                While rid.Read
                    dgLoanSchedule.Rows.Add(rid.Item("pmt_no"), rid.Item("payment_date"), "₱" & rid.Item("beginning_balance"), "₱" & rid.Item("scheduled_payment"), "₱" & rid.Item("extra_payment"), "₱" & rid.Item("total_payment"), "₱" & rid.Item("principal"), "₱" & rid.Item("interest"), "₱" & rid.Item("ending_balance"), "₱" & rid.Item("cumulative_interest"))
                End While
            Catch ex As Exception
                MsgBox("EW error")
            Finally
                conn.Close()
            End Try
        End If
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
            updatedMonth = currentdate.Month
            contriGrid()
        End If

        If updatedYear <> currentdate.Year Then
            For Each contribution As class_contribution In contributions
                If contribution.period = "Annually" Then
                    contribution.insertion()
                End If
            Next
            updatedYear = currentdate.Year
            contriGrid()

        End If

        If updatedWeek <> currentweek Then
            For Each contribution As class_contribution In contributions
                If contribution.period = "Weekly" Then
                    contribution.insertion()
                End If
            Next
            updatedWeek = currentweek
            contriGrid()
        End If

        If updatedDay <> currentdate.DayOfYear Then
            For Each contribution As class_contribution In contributions
                If contribution.period = "Daily" Then
                    contribution.insertion()
                End If
            Next
            updatedDay = currentdate.DayOfYear
            contriGrid()
        End If

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
    End Sub
End Class

