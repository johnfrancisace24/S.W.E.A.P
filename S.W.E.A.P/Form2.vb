Public Class Form2

    Private Sub btnSetSched_Click(sender As Object, e As EventArgs) Handles btnSetSched.Click
        Dim loanAmount As Decimal = numLamount.Value
        Dim annualInterestRate As Decimal = numAintRate.Value / 100
        Dim loanPeriodInYears As Integer = numPyears.Value
        Dim numberOfPaymentsPerYear As Integer = numPayYears.Value

        ' Convert the annual interest rate to monthly interest rate
        Dim monthlyInterestRate As Decimal = annualInterestRate / numberOfPaymentsPerYear

        ' Calculate the total number of payments
        Dim totalNumberOfPayments As Integer = loanPeriodInYears * numberOfPaymentsPerYear

        ' Calculate the scheduled payment amount
        Dim scheduledPayment As Decimal = (monthlyInterestRate * loanAmount) / (1 - (1 + monthlyInterestRate) ^ (-totalNumberOfPayments))

        ' Round the scheduled payment amount to two decimal places
        scheduledPayment = Math.Round(scheduledPayment, 2)

        ' Output the result
        MsgBox("Scheduled payment: $" & scheduledPayment.ToString())
    End Sub

    Private Sub numAintRate_ValueChanged(sender As Object, e As EventArgs) Handles numAintRate.ValueChanged

    End Sub
End Class