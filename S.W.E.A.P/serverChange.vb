Imports MySql.Data.MySqlClient

Public Class serverChange
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Form2.query = "server=" & txtIp.Text & ";port=" & txtPort.Text & ";username=" & txtUsername.Text & ";password=" & txtPassword.Text & ";database=" & txtDatabase.Text
        Form2.conn.ConnectionString = Form2.query

        Try
            Form2.conn.Open()
            Dim cmd As New MySqlCommand("insert into connection(ip, port, username, password, db_name, updated_at)values(@IP, @PORT, @UNAME, @PW, @DB, now());", Form2.conn)
            cmd.Parameters.AddWithValue("@IP", txtIp.Text)
            cmd.Parameters.AddWithValue("@PORT", txtPort.Text)
            cmd.Parameters.AddWithValue("@UNAME", txtUsername.Text)
            cmd.Parameters.AddWithValue("@PW", txtPassword.Text)
            cmd.Parameters.AddWithValue("@DB", txtDatabase.Text)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Connected Successfully!", "Response", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Connection Failed!", "Response", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Form2.conn.Close()
        End Try

        Form2.Show()
        Me.Close()
    End Sub

    Private Sub serverChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPort.Text = "3306"
        txtDatabase.Text = "sweap"
    End Sub
End Class