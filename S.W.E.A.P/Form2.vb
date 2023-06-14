Imports DocumentFormat.OpenXml.Wordprocessing

Public Class Form2
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

    Private Sub btnSetSched_Click(sender As Object, e As EventArgs) Handles btnSetSched.Click
        dgSchedule.Rows.Clear()
        payment = 1
        selectedDate = dateStart.Value.Date
        extraPayment = numXtraP.Value
        loanAmount = numLamount.Value
        annualInterestRate = numAintRate.Value / 100
        loanPeriodInYears = numPyears.Value
        numberOfPaymentsPerYear = numPayYears.Value
        beginningBalance = loanAmount

        ' Convert the annual interest rate to monthly interest rate
        monthlyInterestRate = annualInterestRate / numberOfPaymentsPerYear

        ' Calculate the total number of payments
        totalNumberOfPayments = loanPeriodInYears * numberOfPaymentsPerYear

        ' Calculate the scheduled payment amount
        scheduledPayment = (monthlyInterestRate * loanAmount) / (1 - (1 + monthlyInterestRate) ^ (-totalNumberOfPayments))

        ' Round the scheduled payment amount to two decimal places
        scheduledPayment = Math.Round(scheduledPayment, 2)

        ' Output the result
        MsgBox("Scheduled payment: $" & scheduledPayment.ToString())
        '--------------------------------------------OVERALL CALCULATION-----------------------------------------------------
        totalPayment = scheduledPayment + extraPayment
        interest = beginningBalance * monthlyInterestRate
        principal = totalPayment - interest
        CumuInterest = CumuInterest + interest
        endBalance = beginningBalance - principal
        While beginningBalance >= 0
            beginningBalance = Math.Round(beginningBalance, 2)
            totalPayment = Math.Round(totalPayment, 2)
            interest = Math.Round(interest, 2)
            principal = Math.Round(principal, 2)
            endBalance = Math.Round(endBalance, 2)
            CumuInterest = Math.Round(CumuInterest, 2)
            If beginningBalance < scheduledPayment Or beginningBalance < totalPayment Then
                extraPayment = 0
                totalPayment = beginningBalance
                principal = totalPayment - interest
                endBalance = 0
            End If
            dgSchedule.Rows.Add(payment, selectedDate, beginningBalance, scheduledPayment, extraPayment, totalPayment, principal, interest, endBalance, CumuInterest)
            If endBalance = 0 Then
                beginningBalance = 0
                Exit While
            End If
            totalEarlyPayment = totalEarlyPayment + extraPayment
            selectedDate = selectedDate.AddMonths(1)
            beginningBalance = beginningBalance - principal
            totalPayment = scheduledPayment + extraPayment
            interest = beginningBalance * monthlyInterestRate
            principal = totalPayment - interest
            endBalance = beginningBalance - principal
            CumuInterest = CumuInterest + interest
            payment = payment + 1
            MsgBox(endBalance)

        End While

        '---------------------------------------------END OF CALCULATION-------------------------------------------------------
        txtSnumberPayment.Text = numberOfPaymentsPerYear * loanPeriodInYears
        txtSpayment.Text = scheduledPayment
        txtActualNumPayment.Text = payment
        txtTotalEarlyPayment.Text = totalEarlyPayment
        txtTotalInterest.Text = CumuInterest

    End Sub

    Private Sub numAintRate_ValueChanged(sender As Object, e As EventArgs) Handles numAintRate.ValueChanged

    End Sub
End Class