<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        PB_Sobreviviente = New PictureBox()
        PB_Lancha = New PictureBox()
        PB_Tiburon = New PictureBox()
        PB_Buque = New PictureBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        CType(PB_Sobreviviente, ComponentModel.ISupportInitialize).BeginInit()
        CType(PB_Lancha, ComponentModel.ISupportInitialize).BeginInit()
        CType(PB_Tiburon, ComponentModel.ISupportInitialize).BeginInit()
        CType(PB_Buque, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PB_Sobreviviente
        ' 
        PB_Sobreviviente.Image = My.Resources.Resources.sobreviviente
        PB_Sobreviviente.Location = New Point(0, 0)
        PB_Sobreviviente.Name = "PB_Sobreviviente"
        PB_Sobreviviente.Size = New Size(125, 61)
        PB_Sobreviviente.TabIndex = 0
        PB_Sobreviviente.TabStop = False
        ' 
        ' PB_Lancha
        ' 
        PB_Lancha.Image = My.Resources.Resources.Lancha
        PB_Lancha.Location = New Point(0, 77)
        PB_Lancha.Name = "PB_Lancha"
        PB_Lancha.Size = New Size(125, 61)
        PB_Lancha.TabIndex = 1
        PB_Lancha.TabStop = False
        ' 
        ' PB_Tiburon
        ' 
        PB_Tiburon.Image = My.Resources.Resources.Tiburon
        PB_Tiburon.Location = New Point(0, 156)
        PB_Tiburon.Name = "PB_Tiburon"
        PB_Tiburon.Size = New Size(125, 61)
        PB_Tiburon.TabIndex = 2
        PB_Tiburon.TabStop = False
        ' 
        ' PB_Buque
        ' 
        PB_Buque.Image = My.Resources.Resources.Buque
        PB_Buque.Location = New Point(0, 224)
        PB_Buque.Margin = New Padding(3, 4, 3, 4)
        PB_Buque.Name = "PB_Buque"
        PB_Buque.Size = New Size(125, 56)
        PB_Buque.TabIndex = 3
        PB_Buque.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(640, 77)
        Label1.Name = "Label1"
        Label1.Size = New Size(115, 20)
        Label1.TabIndex = 4
        Label1.Text = "Antonio Estrada"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(617, 112)
        Label2.Name = "Label2"
        Label2.Size = New Size(138, 20)
        Label2.TabIndex = 5
        Label2.Text = "Cedula: 3-752-1360"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(651, 132)
        Label3.Name = "Label3"
        Label3.Size = New Size(104, 20)
        Label3.TabIndex = 6
        Label3.Text = "Grupo: 1LS232"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1251, 619)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(PB_Buque)
        Controls.Add(PB_Tiburon)
        Controls.Add(PB_Lancha)
        Controls.Add(PB_Sobreviviente)
        Name = "Form1"
        Text = "Form1"
        CType(PB_Sobreviviente, ComponentModel.ISupportInitialize).EndInit()
        CType(PB_Lancha, ComponentModel.ISupportInitialize).EndInit()
        CType(PB_Tiburon, ComponentModel.ISupportInitialize).EndInit()
        CType(PB_Buque, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PB_Sobreviviente As PictureBox
    Friend WithEvents PB_Lancha As PictureBox
    Friend WithEvents PB_Tiburon As PictureBox
    Friend WithEvents PB_Buque As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label

End Class
