Imports MySql.Data.MySqlClient
Public Class SignUp
    Dim conn As New MySqlConnection("server=172.30.206.156;port=3306;username=sweapp;password=druguser;database=sweap")
    Dim rid As MySqlDataReader
    Private Sub SignUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rdiobttnPer.Checked = True
    End Sub

    Private Sub bttnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("")
        Catch ex As Exception

        End Try
    End Sub


    Private Sub bttnNext_Click(sender As Object, e As EventArgs) Handles bttnNext.Click
        If rdiobttnPer.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = True
            pnlBen.Visible = False
            pnlAcc.Visible = False

            If txtbxFname.Text = "" Then
                MsgBox("Fields can't be blank")
            Else
                rdiobttnWork.Checked = True
                rdiobttnPer.Checked = False
            End If


        ElseIf rdiobttnWork.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = False
            pnlBen.Visible = True
            pnlAcc.Visible = False

            rdiobttnWork.Checked = False
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = True
            rdiobttnAccnt.Checked = False


        ElseIf rdiobttnBene.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = False
            pnlBen.Visible = False
            pnlAcc.Visible = True

            rdiobttnWork.Checked = False
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = False
            rdiobttnAccnt.Checked = True
            btnSubmit.Show()
            bttnNext.Hide()
            'ElseIf (For Saving Account)
        End If
    End Sub
    Private Sub bttnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

    End Sub

    Private Sub bttnBck_Click(sender As Object, e As EventArgs) Handles bttnBck.Click
        If rdiobttnPer.Checked = True Then
            Form1.Show()
            Me.Hide()

        ElseIf rdiobttnWork.Checked = True Then
            pnlPer.Visible = True
            pnlWork.Visible = False
            pnlBen.Visible = False
            pnlAcc.Visible = False

            rdiobttnPer.Checked = True
            rdiobttnWork.Checked = False
            rdiobttnBene.Checked = False
            rdiobttnAccnt.Checked = False

        ElseIf rdiobttnBene.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = True
            pnlBen.Visible = False
            pnlAcc.Visible = False

            rdiobttnWork.Checked = True
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = False
            rdiobttnAccnt.Checked = False


        ElseIf rdiobttnAccnt.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = False
            pnlBen.Visible = True
            pnlAcc.Visible = False

            rdiobttnWork.Checked = False
            rdiobttnPer.Checked = False
            rdiobttnBene.Checked = True
            rdiobttnAccnt.Checked = False
            bttnNext.Show()
            btnSubmit.Hide()
        End If
    End Sub
End Class