Public Class Form1
    Private Sub LinkLabelSignUp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelSignUp.LinkClicked
        Me.Hide()
        SignUp.Show()
    End Sub
    Private Sub CheckBoxShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowPass.CheckedChanged
        If CheckBoxShowPass.Checked = False Then
            txtbxPassword.PasswordChar = "*"
        Else
            txtbxPassword.PasswordChar = ""
        End If
    End Sub
End Class
