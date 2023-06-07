Imports TheArtOfDevHtmlRenderer.Adapters.Entities
Imports MySql.Data.MySqlClient
Public Class AdminDashboard
    Dim conn As New MySqlConnection("server=172.30.192.29;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader
    Dim selectedId As Integer = 0
    '-------------------------------FUNCTIONSS--------------------------------------------------------------------------------------
    Public Sub viewMembers(query) '-----------------PARA SA EMPLOYEES TABLE
        dgMembers.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand(query, conn)
            rid = cmd.ExecuteReader
            While rid.Read
                dgMembers.Rows.Add(rid.Item("id"), rid.Item("full_name"), rid.Item("office"), rid.Item("position"), rid.Item("employment_status"), rid.Item("email"))
            End While
        Catch ex As Exception
        Finally
            conn.Close()
        End Try
    End Sub
    Public Sub btnFx1(button) '-------------------FOR BUTTON EFFECTS
        button.FillColor = Color.FromArgb(251, 197, 49)
        button.ForeColor = Color.Black
        button.BorderColor = Color.Black
    End Sub
    Public Sub btnFx2(button) '--------------------FOR BUTTON EFFECTS FILL COLOR DARKRED
        button.FillColor = Color.DarkRed
        button.ForeColor = Color.White
        button.BorderColor = Color.White
    End Sub
    Public Sub panelVisible(pnldash, pnlEm, pnlf) '--------------------FOR PANEL VISIBILITY
        pnlEmployees.Visible = pnldash
        pnlFundTransfer.Visible = pnlEm
        pnlDashboard.Visible = pnlf
    End Sub
    '---------------------------------------------------END OF FUNCTIONS----------------------------------------------------------------------------
    Private Sub bttnDash_Click(sender As Object, e As EventArgs) Handles bttnDash.Click
        pnlDas.BackColor = Color.DarkRed
        pnlEm.BackColor = Color.Transparent
        pnlFundT.BackColor = Color.Transparent

        'To view panel
        pnlHome.Visible = False
        pnlDashboard.Visible = True
        pnlEmployees.Visible = False
        pnlFundTransfer.Visible = False

        btnFx1(bttnDash)
        bttnDash.Image = My.Resources.dashboard__1_

        btnFx2(bttnEmpl)
        bttnEmpl.Image = My.Resources.employees

        btnFx2(bttnFund)
        bttnFund.Image = My.Resources.transfer
    End Sub

    Private Sub bttnEmpl_Click(sender As Object, e As EventArgs) Handles bttnEmpl.Click
        pnlDas.BackColor = Color.Transparent
        pnlEm.BackColor = Color.DarkRed
        pnlFundT.BackColor = Color.Transparent

        'To view panel
        pnlHome.Visible = False
        pnlDashboard.Visible = False
        pnlEmployees.Visible = True
        pnlFundTransfer.Visible = False

        'For button Dashboard design
        bttnDash.FillColor = Color.Transparent
        bttnDash.ForeColor = Color.White
        bttnDash.Image = My.Resources.dash
        bttnDash.BorderColor = Color.White


        btnFx1(bttnEmpl)
        bttnEmpl.Image = My.Resources.company

        btnFx2(bttnFund)
        bttnFund.Image = My.Resources.transfer

    End Sub

    Private Sub bttnFund_Click(sender As Object, e As EventArgs) Handles bttnFund.Click
        pnlDas.BackColor = Color.Transparent
        pnlEm.BackColor = Color.Transparent
        pnlFundT.BackColor = Color.DarkRed

        'To view panel
        pnlHome.Visible = False
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

        btnFx1(bttnFund)
        bttnFund.Image = My.Resources.fund__1_
    End Sub

    Private Sub pnlFundTransfer_Paint(sender As Object, e As PaintEventArgs) Handles pnlDashboard.Paint

    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click

    End Sub

    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load '----------------------AUTOLOAD
        viewMembers("select users.id, concat(first_name, ' ', middle_name, ' ', last_name) as full_name, office, position, employment_status, 
                                            email from users left join user_info on users.id = user_info.user_id")
        dgMembers.ReadOnly = True

        pnlHome.Visible = True
    End Sub



    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        viewMembers("select users.id, concat(first_name, ' ', middle_name, ' ', last_name) as full_name, office, position, employment_status, 
                                            email from users left join user_info on users.id = user_info.user_id where first_name like '%" & txtSearch.Text & "%' or
                                            middle_name like '%" & txtSearch.Text & "%' or last_name like '%" & txtSearch.Text & "%' or office like '%" & txtSearch.Text &
                                            "%' or position like '%" & txtSearch.Text & "%' or employment_status like '%" & txtSearch.Text & "%' or email like '%" &
                                             txtSearch.Text & "%'")
    End Sub

    Private Sub bttnLogout_Click(sender As Object, e As EventArgs) Handles bttnLogout.Click
        Dim AnswerYes As String
        AnswerYes = MsgBox("Are you sure you want to Log out", vbQuestion + vbYesNo, "User Repsonse")

        If AnswerYes = vbYes Then
            Form1.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub btnEditMember_Click(sender As Object, e As EventArgs) Handles btnEditMember.Click
        tabEditMember.Show()
        pnlEmployee.Hide()

    End Sub

    Private Sub dgMembers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgMembers.CellContentClick
        btnEditMember.Enabled = True
        selectedId = dgMembers.CurrentRow.Cells(0).Value.ToString()
        lblId.Text = selectedId
    End Sub

    Private Sub btnEditNext_Click(sender As Object, e As EventArgs) Handles btnEditNext.Click
        tabEditMember.SelectedIndex = 1
    End Sub

    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click
        'To view panel
        pnlHome.Visible = True
        pnlDashboard.Visible = False
        pnlEmployees.Visible = False
        pnlFundTransfer.Visible = False



        pnlDas.BackColor = Color.Transparent
        pnlEm.BackColor = Color.Transparent
        pnlFundT.BackColor = Color.Transparent


        'For button Dashboard design
        bttnDash.FillColor = Color.Transparent
        bttnDash.ForeColor = Color.White
        bttnDash.Image = My.Resources.dash
        bttnDash.BorderColor = Color.Black


        'For button Employee design
        bttnEmpl.FillColor = Color.Transparent
        bttnEmpl.ForeColor = Color.White
        bttnEmpl.Image = My.Resources.employees
        bttnEmpl.BorderColor = Color.Black

        'For button Fundtransfer design
        bttnFund.FillColor = Color.Transparent
        bttnFund.ForeColor = Color.White
        bttnFund.Image = My.Resources.transfer
        bttnFund.BorderColor = Color.Black




    End Sub

    Private Sub Guna2CircleProgressBar2_ValueChanged(sender As Object, e As EventArgs) Handles Guna2CircleProgressBar2.ValueChanged

    End Sub

    Private Sub Guna2CircleProgressBar1_ValueChanged(sender As Object, e As EventArgs) Handles Guna2CircleProgressBar1.ValueChanged
    End Sub

    Private Sub Guna2Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel3.Paint
    End Sub

    Private Sub pickOffice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles pickOffice.SelectedIndexChanged
        ' viewMembers("select users.id, concat(first_name, ' ', middle_name, ' ', last_name) as full_name, office, position, employment_status, 
        '                                    email from users left join user_info on users.id = user_info.user_id where office=" & pickOffice.SelectedItem)
        If pickOffice.SelectedIndex = 0 Then
            viewMembers("select users.id, concat(first_name, ' ', middle_name, ' ', last_name) as full_name, office, position, employment_status, 
                                            email from users left join user_info on users.id = user_info.user_id")
        Else
            dgMembers.Rows.Clear()
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("select users.id, concat(first_name, ' ', middle_name, ' ', last_name) as full_name, office, position, employment_status, 
                                            email from users left join user_info on users.id = user_info.user_id where office=@OFC", conn)
                cmd.Parameters.AddWithValue("@OFC", pickOffice.SelectedItem)
                rid = cmd.ExecuteReader
                While rid.Read
                    dgMembers.Rows.Add(rid.Item("id"), rid.Item("full_name"), rid.Item("office"), rid.Item("position"), rid.Item("employment_status"), rid.Item("email"))
                End While
            Catch ex As Exception
                MsgBox("doesn't work lmao")
            Finally
                conn.Close()
            End Try
        End If

    End Sub
End Class