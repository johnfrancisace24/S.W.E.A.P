Public Class serverChange
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
        Form2.conn.Close()
    End Sub
End Class