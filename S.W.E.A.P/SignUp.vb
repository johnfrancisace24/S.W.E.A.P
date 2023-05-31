Public Class SignUp
    Private Sub SignUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rdiobttnPer.Checked = True
    End Sub

    Private Sub bttnNext_Click(sender As Object, e As EventArgs) Handles bttnNext.Click
        If rdiobttnPer.Checked = True Then
            pnlPer.Visible = False
            pnlWork.Visible = True
            pnlBen.Visible = False
            pnlAcc.Visible = False


            rdiobttnWork.Checked = True
            rdiobttnPer.Checked = False

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

            'ElseIf (For Saving Account)
        End If
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
        End If
    End Sub
End Class