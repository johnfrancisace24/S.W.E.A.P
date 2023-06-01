Imports TheArtOfDevHtmlRenderer.Adapters.Entities

Public Class AdminDashboard


    Private Sub bttnDash_Click(sender As Object, e As EventArgs) Handles bttnDash.Click
        pnlDas.BackColor = Color.DarkRed
        pnlEm.BackColor = Color.Transparent
        pnlFundT.BackColor = Color.Transparent

        pnlDashboard.Visible = True
        pnlEmployees.Visible = False
        pnlFundTransfer.Visible = False

        'For button Dashboard design
        bttnDash.FillColor = Color.FromArgb(251, 197, 49)
        bttnDash.ForeColor = Color.Black
        bttnDash.Image = My.Resources.dashboard__1_
        bttnDash.BorderColor = Color.Black


        'For button Employee design
        bttnEmpl.FillColor = Color.DarkRed
        bttnEmpl.ForeColor = Color.White
        bttnEmpl.Image = My.Resources.employees
        bttnEmpl.BorderColor = Color.White


        'For button Fund Transfer design
        bttnFund.FillColor = Color.DarkRed
        bttnFund.ForeColor = Color.Wheat
        bttnFund.Image = My.Resources.transfer
        bttnFund.BorderColor = Color.White
    End Sub

    Private Sub bttnEmpl_Click(sender As Object, e As EventArgs) Handles bttnEmpl.Click
        pnlDas.BackColor = Color.Transparent
        pnlEm.BackColor = Color.DarkRed
        pnlFundT.BackColor = Color.Transparent

        pnlDashboard.Visible = False
        pnlEmployees.Visible = True
        pnlFundTransfer.Visible = False

        'For button Dashboard design
        bttnDash.FillColor = Color.Transparent
        bttnDash.ForeColor = Color.White
        bttnDash.Image = My.Resources.dash
        bttnDash.BorderColor = Color.White


        'For button Employee design
        bttnEmpl.FillColor = Color.FromArgb(251, 197, 49)
        bttnEmpl.ForeColor = Color.Black
        bttnEmpl.Image = My.Resources.company
        bttnEmpl.BorderColor = Color.Black


        'For button Fund Transfer design
        bttnFund.FillColor = Color.DarkRed
        bttnFund.ForeColor = Color.White
        bttnFund.Image = My.Resources.transfer
        bttnFund.BorderColor = Color.White

    End Sub

    Private Sub bttnFund_Click(sender As Object, e As EventArgs) Handles bttnFund.Click
        pnlDas.BackColor = Color.Transparent
        pnlEm.BackColor = Color.Transparent
        pnlFundT.BackColor = Color.DarkRed

        pnlDashboard.Visible = False
        pnlEmployees.Visible = False
        pnlFundTransfer.Visible = True

        'For button Dashboard design
        bttnDash.FillColor = Color.Transparent
        bttnDash.ForeColor = Color.Black
        bttnDash.Image = My.Resources.dashboard__1_
        bttnDash.BorderColor = Color.Black


        'For button Employee design
        bttnEmpl.FillColor = Color.Transparent
        bttnEmpl.ForeColor = Color.Black
        bttnEmpl.Image = My.Resources.company
        bttnEmpl.BorderColor = Color.Black


        'For button Fund Transfer design
        bttnFund.FillColor = Color.FromArgb(251, 197, 49)
        bttnFund.ForeColor = Color.Black
        bttnFund.Image = My.Resources.fund__1_
        bttnFund.BorderColor = Color.Black
    End Sub

    Private Sub pnlFundTransfer_Paint(sender As Object, e As PaintEventArgs) Handles pnlFundTransfer.Paint

    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click

    End Sub
End Class