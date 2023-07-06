<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim CustomizableEdges21 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges22 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges23 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges24 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges17 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges18 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges19 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges20 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges15 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges16 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(Form2))
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        txtPassword = New Guna.UI2.WinForms.Guna2TextBox()
        PictureBox1 = New PictureBox()
        Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Label5 = New Label()
        lblRegister = New Label()
        Label2 = New Label()
        lblForgot = New Label()
        checkShowPw = New Guna.UI2.WinForms.Guna2CheckBox()
        btnLogin = New Guna.UI2.WinForms.Guna2Button()
        txtUsername = New Guna.UI2.WinForms.Guna2TextBox()
        Panel1 = New Panel()
        Label4 = New Label()
        Label3 = New Label()
        Guna2PictureBox1 = New Guna.UI2.WinForms.Guna2PictureBox()
        btnChangeServer = New Guna.UI2.WinForms.Guna2Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Guna2Panel1.SuspendLayout()
        Panel1.SuspendLayout()
        CType(Guna2PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 30
        Guna2Elipse1.TargetControl = Me
        ' 
        ' txtPassword
        ' 
        txtPassword.BorderRadius = 10
        txtPassword.CustomizableEdges = CustomizableEdges21
        txtPassword.DefaultText = ""
        txtPassword.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtPassword.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtPassword.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPassword.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtPassword.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        txtPassword.ForeColor = Color.Black
        txtPassword.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtPassword.Location = New Point(34, 169)
        txtPassword.Margin = New Padding(4, 4, 4, 4)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = ChrW(0)
        txtPassword.PlaceholderForeColor = Color.Gray
        txtPassword.PlaceholderText = "PASSWORD"
        txtPassword.SelectedText = ""
        txtPassword.ShadowDecoration.CustomizableEdges = CustomizableEdges22
        txtPassword.Size = New Size(340, 45)
        txtPassword.TabIndex = 0
        txtPassword.TextAlign = HorizontalAlignment.Center
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackgroundImage = My.Resources.Resources.image_removebg_preview__1_
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.Location = New Point(13, 6)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(140, 135)
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        ' 
        ' Guna2Panel1
        ' 
        Guna2Panel1.BackColor = Color.FromArgb(CByte(54), CByte(69), CByte(94))
        Guna2Panel1.BorderRadius = 100
        Guna2Panel1.Controls.Add(btnChangeServer)
        Guna2Panel1.Controls.Add(Label5)
        Guna2Panel1.Controls.Add(lblRegister)
        Guna2Panel1.Controls.Add(Label2)
        Guna2Panel1.Controls.Add(lblForgot)
        Guna2Panel1.Controls.Add(checkShowPw)
        Guna2Panel1.Controls.Add(btnLogin)
        Guna2Panel1.Controls.Add(txtUsername)
        Guna2Panel1.Controls.Add(txtPassword)
        Guna2Panel1.CustomizableEdges = CustomizableEdges23
        Guna2Panel1.Location = New Point(28, 202)
        Guna2Panel1.Name = "Guna2Panel1"
        Guna2Panel1.ShadowDecoration.CustomizableEdges = CustomizableEdges24
        Guna2Panel1.Size = New Size(408, 404)
        Guna2Panel1.TabIndex = 2
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point)
        Label5.ForeColor = SystemColors.ControlLightLight
        Label5.Location = New Point(144, 18)
        Label5.Name = "Label5"
        Label5.Size = New Size(118, 37)
        Label5.TabIndex = 3
        Label5.Text = "SIGN IN"
        ' 
        ' lblRegister
        ' 
        lblRegister.AutoSize = True
        lblRegister.Cursor = Cursors.Hand
        lblRegister.ForeColor = Color.Cyan
        lblRegister.Location = New Point(247, 365)
        lblRegister.Name = "lblRegister"
        lblRegister.Size = New Size(47, 15)
        lblRegister.TabIndex = 3
        lblRegister.Text = "Sign up"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = SystemColors.ControlLightLight
        Label2.Location = New Point(116, 365)
        Label2.Name = "Label2"
        Label2.Size = New Size(131, 15)
        Label2.TabIndex = 3
        Label2.Text = "Don't have an account?"
        ' 
        ' lblForgot
        ' 
        lblForgot.AutoSize = True
        lblForgot.Cursor = Cursors.Hand
        lblForgot.ForeColor = SystemColors.ControlLightLight
        lblForgot.Location = New Point(250, 221)
        lblForgot.Name = "lblForgot"
        lblForgot.Size = New Size(127, 15)
        lblForgot.TabIndex = 3
        lblForgot.Text = "Forgot your password?"
        ' 
        ' checkShowPw
        ' 
        checkShowPw.AutoSize = True
        checkShowPw.CheckedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        checkShowPw.CheckedState.BorderRadius = 0
        checkShowPw.CheckedState.BorderThickness = 0
        checkShowPw.CheckedState.FillColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        checkShowPw.ForeColor = SystemColors.ControlLightLight
        checkShowPw.Location = New Point(34, 221)
        checkShowPw.Name = "checkShowPw"
        checkShowPw.Size = New Size(108, 19)
        checkShowPw.TabIndex = 2
        checkShowPw.Text = "Show password"
        checkShowPw.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        checkShowPw.UncheckedState.BorderRadius = 0
        checkShowPw.UncheckedState.BorderThickness = 0
        checkShowPw.UncheckedState.FillColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        ' 
        ' btnLogin
        ' 
        btnLogin.BorderRadius = 10
        btnLogin.CustomizableEdges = CustomizableEdges17
        btnLogin.DisabledState.BorderColor = Color.DarkGray
        btnLogin.DisabledState.CustomBorderColor = Color.DarkGray
        btnLogin.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnLogin.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnLogin.Font = New Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point)
        btnLogin.ForeColor = Color.White
        btnLogin.Location = New Point(34, 260)
        btnLogin.Name = "btnLogin"
        btnLogin.ShadowDecoration.CustomizableEdges = CustomizableEdges18
        btnLogin.Size = New Size(340, 44)
        btnLogin.TabIndex = 1
        btnLogin.Text = "LOGIN"
        ' 
        ' txtUsername
        ' 
        txtUsername.BorderRadius = 10
        txtUsername.CustomizableEdges = CustomizableEdges19
        txtUsername.DefaultText = ""
        txtUsername.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtUsername.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtUsername.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtUsername.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtUsername.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point)
        txtUsername.ForeColor = Color.DimGray
        txtUsername.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtUsername.Location = New Point(34, 97)
        txtUsername.Margin = New Padding(4, 4, 4, 4)
        txtUsername.Name = "txtUsername"
        txtUsername.PasswordChar = ChrW(0)
        txtUsername.PlaceholderForeColor = Color.Gray
        txtUsername.PlaceholderText = "USERNAME"
        txtUsername.SelectedText = ""
        txtUsername.ShadowDecoration.CustomizableEdges = CustomizableEdges20
        txtUsername.Size = New Size(340, 45)
        txtUsername.TabIndex = 0
        txtUsername.TextAlign = HorizontalAlignment.Center
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(54), CByte(69), CByte(94))
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Location = New Point(28, 32)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(408, 150)
        Panel1.TabIndex = 3
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point)
        Label4.ForeColor = SystemColors.ControlLightLight
        Label4.Location = New Point(177, 68)
        Label4.Name = "Label4"
        Label4.Size = New Size(205, 37)
        Label4.TabIndex = 3
        Label4.Text = "PARA SA BAYAN"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point)
        Label3.ForeColor = SystemColors.ControlLightLight
        Label3.Location = New Point(167, 35)
        Label3.Name = "Label3"
        Label3.Size = New Size(224, 37)
        Label3.TabIndex = 3
        Label3.Text = "PARA SA KAWANI"
        ' 
        ' Guna2PictureBox1
        ' 
        Guna2PictureBox1.CustomizableEdges = CustomizableEdges13
        Guna2PictureBox1.Image = My.Resources.Resources.close
        Guna2PictureBox1.ImageRotate = 0F
        Guna2PictureBox1.Location = New Point(434, 6)
        Guna2PictureBox1.Name = "Guna2PictureBox1"
        Guna2PictureBox1.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        Guna2PictureBox1.Size = New Size(24, 24)
        Guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Guna2PictureBox1.TabIndex = 4
        Guna2PictureBox1.TabStop = False
        ' 
        ' btnChangeServer
        ' 
        btnChangeServer.BorderRadius = 10
        btnChangeServer.CustomizableEdges = CustomizableEdges15
        btnChangeServer.DisabledState.BorderColor = Color.DarkGray
        btnChangeServer.DisabledState.CustomBorderColor = Color.DarkGray
        btnChangeServer.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnChangeServer.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnChangeServer.FillColor = Color.Transparent
        btnChangeServer.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        btnChangeServer.ForeColor = Color.White
        btnChangeServer.HoverState.ForeColor = Color.FromArgb(CByte(33), CByte(42), CByte(57))
        btnChangeServer.Location = New Point(34, 310)
        btnChangeServer.Name = "btnChangeServer"
        btnChangeServer.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        btnChangeServer.Size = New Size(108, 37)
        btnChangeServer.TabIndex = 4
        btnChangeServer.Text = "Change server"
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(33), CByte(42), CByte(57))
        ClientSize = New Size(465, 630)
        Controls.Add(Guna2PictureBox1)
        Controls.Add(Panel1)
        Controls.Add(Guna2Panel1)
        FormBorderStyle = FormBorderStyle.None
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form2"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form2"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Guna2Panel1.ResumeLayout(False)
        Guna2Panel1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(Guna2PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents checkShowPw As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents btnLogin As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txtUsername As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblForgot As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblRegister As Label
    Friend WithEvents Guna2PictureBox1 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents btnChangeServer As Guna.UI2.WinForms.Guna2Button
End Class
