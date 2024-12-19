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
        components = New ComponentModel.Container()
        Timer1 = New Timer(components)
        StartAngleTrackBar = New TrackBar()
        StartAngleLabel = New Label()
        SweepAngleTrackBar = New TrackBar()
        SweepAngleLabel = New Label()
        CType(StartAngleTrackBar, ComponentModel.ISupportInitialize).BeginInit()
        CType(SweepAngleTrackBar, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        ' 
        ' StartAngleTrackBar
        ' 
        StartAngleTrackBar.BackColor = SystemColors.Control
        StartAngleTrackBar.Location = New Point(267, 263)
        StartAngleTrackBar.Maximum = 360
        StartAngleTrackBar.Minimum = -360
        StartAngleTrackBar.Name = "StartAngleTrackBar"
        StartAngleTrackBar.Size = New Size(479, 69)
        StartAngleTrackBar.TabIndex = 0
        StartAngleTrackBar.TickFrequency = 3
        StartAngleTrackBar.TickStyle = TickStyle.TopLeft
        ' 
        ' StartAngleLabel
        ' 
        StartAngleLabel.AutoSize = True
        StartAngleLabel.BackColor = SystemColors.Control
        StartAngleLabel.ForeColor = SystemColors.ControlText
        StartAngleLabel.Location = New Point(348, 293)
        StartAngleLabel.Name = "StartAngleLabel"
        StartAngleLabel.Size = New Size(63, 25)
        StartAngleLabel.TabIndex = 2
        StartAngleLabel.Text = "Label1"
        ' 
        ' SweepAngleTrackBar
        ' 
        SweepAngleTrackBar.BackColor = SystemColors.Control
        SweepAngleTrackBar.Location = New Point(169, 52)
        SweepAngleTrackBar.Maximum = 360
        SweepAngleTrackBar.Minimum = -360
        SweepAngleTrackBar.Name = "SweepAngleTrackBar"
        SweepAngleTrackBar.Size = New Size(479, 69)
        SweepAngleTrackBar.TabIndex = 1
        SweepAngleTrackBar.TickFrequency = 3
        SweepAngleTrackBar.TickStyle = TickStyle.TopLeft
        SweepAngleTrackBar.Value = 45
        ' 
        ' SweepAngleLabel
        ' 
        SweepAngleLabel.AutoSize = True
        SweepAngleLabel.BackColor = SystemColors.Control
        SweepAngleLabel.ForeColor = SystemColors.ControlText
        SweepAngleLabel.Location = New Point(291, 82)
        SweepAngleLabel.Name = "SweepAngleLabel"
        SweepAngleLabel.Size = New Size(63, 25)
        SweepAngleLabel.TabIndex = 3
        SweepAngleLabel.Text = "Label1"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(SweepAngleLabel)
        Controls.Add(StartAngleLabel)
        Controls.Add(StartAngleTrackBar)
        Controls.Add(SweepAngleTrackBar)
        Name = "Form1"
        Text = "Form1"
        CType(StartAngleTrackBar, ComponentModel.ISupportInitialize).EndInit()
        CType(SweepAngleTrackBar, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents StartAngleTrackBar As TrackBar
    Friend WithEvents SweepAngleTrackBar As TrackBar
    Friend WithEvents StartAngleLabel As Label
    Friend WithEvents SweepAngleLabel As Label

End Class
