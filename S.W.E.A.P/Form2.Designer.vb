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
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        Guna2TabControl1 = New Guna.UI2.WinForms.Guna2TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        Guna2TabControl1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.TargetControl = Me
        ' 
        ' Guna2TabControl1
        ' 
        Guna2TabControl1.Alignment = TabAlignment.Left
        Guna2TabControl1.Controls.Add(TabPage1)
        Guna2TabControl1.Controls.Add(TabPage2)
        Guna2TabControl1.ItemSize = New Size(180, 40)
        Guna2TabControl1.Location = New Point(0, 0)
        Guna2TabControl1.Name = "Guna2TabControl1"
        Guna2TabControl1.SelectedIndex = 0
        Guna2TabControl1.Size = New Size(1190, 764)
        Guna2TabControl1.TabButtonHoverState.BorderColor = Color.Empty
        Guna2TabControl1.TabButtonHoverState.FillColor = Color.FromArgb(CByte(40), CByte(52), CByte(70))
        Guna2TabControl1.TabButtonHoverState.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point)
        Guna2TabControl1.TabButtonHoverState.ForeColor = Color.White
        Guna2TabControl1.TabButtonHoverState.InnerColor = Color.FromArgb(CByte(40), CByte(52), CByte(70))
        Guna2TabControl1.TabButtonIdleState.BorderColor = Color.Empty
        Guna2TabControl1.TabButtonIdleState.FillColor = Color.FromArgb(CByte(33), CByte(42), CByte(57))
        Guna2TabControl1.TabButtonIdleState.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point)
        Guna2TabControl1.TabButtonIdleState.ForeColor = Color.FromArgb(CByte(156), CByte(160), CByte(167))
        Guna2TabControl1.TabButtonIdleState.InnerColor = Color.FromArgb(CByte(33), CByte(42), CByte(57))
        Guna2TabControl1.TabButtonSelectedState.BorderColor = Color.Empty
        Guna2TabControl1.TabButtonSelectedState.FillColor = Color.FromArgb(CByte(29), CByte(37), CByte(49))
        Guna2TabControl1.TabButtonSelectedState.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point)
        Guna2TabControl1.TabButtonSelectedState.ForeColor = Color.White
        Guna2TabControl1.TabButtonSelectedState.InnerColor = Color.FromArgb(CByte(76), CByte(132), CByte(255))
        Guna2TabControl1.TabButtonSize = New Size(180, 40)
        Guna2TabControl1.TabIndex = 0
        Guna2TabControl1.TabMenuBackColor = Color.FromArgb(CByte(33), CByte(42), CByte(57))
        ' 
        ' TabPage1
        ' 
        TabPage1.Location = New Point(184, 4)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1002, 756)
        TabPage1.TabIndex = 0
        TabPage1.Text = "TabPage1"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Location = New Point(184, 4)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1002, 756)
        TabPage2.TabIndex = 1
        TabPage2.Text = "TabPage2"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1190, 764)
        Controls.Add(Guna2TabControl1)
        FormBorderStyle = FormBorderStyle.None
        Name = "Form2"
        Text = "Form2"
        Guna2TabControl1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2TabControl1 As Guna.UI2.WinForms.Guna2TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
End Class
