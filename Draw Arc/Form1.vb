' Draw Arc

' This interactive application provides a visual demonstration of arcs drawn
' with various starting and sweep angles using GDI (Graphics Device Interface)
' graphics.

' It aims to help users gain a deeper understanding of how angles affect the
' rendering of arcs in graphical applications.

' Features
'  Interactive Visualization: Adjust starting and sweep angles dynamically to
'  see real-time changes in arc rendering.
'  Intuitive Controls: User-friendly interface for manipulating angles.
'  Educational Tool: Designed to help users grasp the concepts of angles in
'  graphics programming.

' MIT License
' Copyright(c) 2024 Joseph W. Lumbley

' Permission Is hereby granted, free Of charge, to any person obtaining a copy
' of this software And associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
' copies of the Software, And to permit persons to whom the Software Is
' furnished to do so, subject to the following conditions:

' The above copyright notice And this permission notice shall be included In all
' copies Or substantial portions of the Software.

' THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
' IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
' LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
' OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
' SOFTWARE.

' https://github.com/JoeLumbley/Draw-Arc

Imports System.Drawing.Drawing2D

Public Class Form1

    Private Context As BufferedGraphicsContext
    Private Buffer As BufferedGraphics

    Private Structure DisplayObject
        Public Location As Point
        Public Text As String
        Public Font As Font
    End Structure

    Private CodeDisplay As DisplayObject

    Private CircleOfProgress As Rectangle

    Private CircleOfProgressPen As Pen

    Private CircleOfProgressBackgroundPen As Pen

    ' Define the start angle in degrees at 0 or 3 o'clock.
    Private Const startAngle As Single = 0.0F

    Private sweepAngle As Single

    Private ReadOnly AlineCenterMiddle As New StringFormat With
        {.Alignment = StringAlignment.Center,
        .LineAlignment = StringAlignment.Center}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeApp()

        Debug.Print($"Program running... {Now.ToShortTimeString}")

    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If Not WindowState = FormWindowState.Minimized Then

            ResizeCodeDisplay()

            ResizeControls()

            ResizeCircleOfProgress()

            DisposeBuffer()

        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If Not WindowState = FormWindowState.Minimized Then

            UpdateCodeDisplay()

            Invalidate() ' Calls OnPaint Sub

        End If

    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

        AllocateBuffer(e)

        DrawDisplays()

        Buffer.Render(e.Graphics)

        MyBase.OnPaint(e)

    End Sub

    Private Sub StartAngleTrackBar_Scroll(sender As Object, e As EventArgs) Handles StartAngleTrackBar.Scroll

        StartAngleLabel.Text = $"Start Angle: {StartAngleTrackBar.Value}°"

    End Sub

    Private Sub SweepAngleTrackBar_Scroll(sender As Object, e As EventArgs) Handles SweepAngleTrackBar.Scroll

        SweepAngleLabel.Text = $"Sweep Angle: {SweepAngleTrackBar.Value}°"

    End Sub

    Private Sub UpdateCodeDisplay()

        CodeDisplay.Text = $".DrawArc(Pen, Rectangle, {StartAngleTrackBar.Value}, {SweepAngleTrackBar.Value})"

    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)

        'Intentionally left blank. Do not remove.

    End Sub

    Private Sub AllocateBuffer(e As PaintEventArgs)

        ' Allocate the buffer if it hasn't been allocated yet
        If Buffer Is Nothing Then

            Buffer = Context.Allocate(e.Graphics, ClientRectangle)

            With Buffer.Graphics

                .CompositingMode = Drawing2D.CompositingMode.SourceOver
                .TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
                .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                .PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                .InterpolationMode = InterpolationMode.HighQualityBicubic
                .TextContrast = SmoothingMode.HighQuality

            End With

        End If

    End Sub

    Private Sub DrawDisplays()

        If Buffer IsNot Nothing Then

            Try

                With Buffer.Graphics

                    .Clear(SystemColors.Control)

                    .DrawEllipse(CircleOfProgressBackgroundPen,
                                 CircleOfProgress)

                    .DrawArc(CircleOfProgressPen,
                             CircleOfProgress,
                             StartAngleTrackBar.Value,
                             SweepAngleTrackBar.Value)

                    .DrawString(CodeDisplay.Text,
                                CodeDisplay.Font,
                                New SolidBrush(SystemColors.ControlText),
                                CodeDisplay.Location,
                                AlineCenterMiddle)

                End With

            Catch ex As Exception

                Debug.Print("Draw error: " & ex.Message)

            End Try

        Else

            Debug.Print("Buffer is not initialized.")

        End If

    End Sub

    Private Sub DisposeBuffer()

        If Buffer IsNot Nothing Then

            Buffer.Dispose()

            Buffer = Nothing ' Set to Nothing to avoid using a disposed object

            ' The buffer will be reallocated in OnPaint

        End If

    End Sub

    Private Sub InitializeApp()

        InitializeForm()

        InitializeBuffer()

        InitializeLabels()

        InitializeTimer()

    End Sub

    Private Sub InitializeForm()

        CenterToScreen()

        SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint, True)

        SetStyle(ControlStyles.UserPaint, True)

        Text = "Draw Arc - Code with Joe"

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub InitializeBuffer()

        ' Set context to the context of this app.
        Context = BufferedGraphicsManager.Current

        Context.MaximumBuffer = Screen.PrimaryScreen.WorkingArea.Size

        ' Allocate the buffer initially using the current client rectangle
        Buffer = Context.Allocate(CreateGraphics(), ClientRectangle)

        With Buffer.Graphics

            .CompositingMode = Drawing2D.CompositingMode.SourceOver
            .TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            .PixelOffsetMode = Drawing2D.PixelOffsetMode.None
            .CompositingQuality = Drawing2D.CompositingQuality.HighQuality

        End With

    End Sub

    Private Sub InitializeTimer()

        Timer1.Interval = 30

        Timer1.Enabled = True

    End Sub

    Private Sub InitializeLabels()

        StartAngleLabel.Text = $"Start Angle: {StartAngleTrackBar.Value}°"

        SweepAngleLabel.Text = $"Sweep Angle: {SweepAngleTrackBar.Value}°"

    End Sub

    Private Sub ResizeCodeDisplay()

        Dim FontSize As Single

        If ClientSize.Height / 35 > 13 Then

            FontSize = ClientSize.Height / 35

        Else

            FontSize = 13

        End If

        Dim YPosition As Integer

        If ClientSize.Height / 2.5 > 175 Then

            YPosition = ClientSize.Height / 2.5

        Else

            YPosition = 175

        End If

        CodeDisplay.Font = New Font("Segoe UI",
                                    FontSize,
                                    FontStyle.Regular)

        CodeDisplay.Location.X = ClientSize.Width / 2

        CodeDisplay.Location.Y = ClientSize.Height / 2 - YPosition

    End Sub

    Private Sub ResizeControls()

        Dim TrackBarWidth As Integer

        If ClientSize.Height / 2 > 225 Then

            TrackBarWidth = ClientSize.Height / 2

        Else

            TrackBarWidth = 225

        End If

        StartAngleTrackBar.Width = TrackBarWidth

        StartAngleTrackBar.Left = ClientSize.Width / 2 - StartAngleTrackBar.Width / 2

        StartAngleTrackBar.Top = ClientSize.Height / 2 - StartAngleTrackBar.Height

        StartAngleLabel.Left = StartAngleTrackBar.Left + 8

        StartAngleLabel.Top = StartAngleTrackBar.Top + 40

        SweepAngleTrackBar.Width = TrackBarWidth

        SweepAngleTrackBar.Left = ClientSize.Width / 2 - SweepAngleTrackBar.Width / 2

        SweepAngleTrackBar.Top = ClientSize.Height / 2 + SweepAngleTrackBar.Height * 0.1

        SweepAngleLabel.Left = SweepAngleTrackBar.Left + 8

        SweepAngleLabel.Top = SweepAngleTrackBar.Top + 40

    End Sub

    Private Sub ResizeCircleOfProgress()

        Dim PenSize As Single

        If ClientSize.Height / 30 > 15 Then

            PenSize = ClientSize.Height / 30

        Else

            PenSize = 15

        End If

        CircleOfProgressBackgroundPen = New Pen(SystemColors.ControlLightLight,
                                                PenSize)

        If ClientSize.Height / 40 > 10 Then

            PenSize = ClientSize.Height / 40

        Else

            PenSize = 10

        End If

        CircleOfProgressPen = New Pen(SystemColors.MenuHighlight, PenSize) With {
            .EndCap = LineCap.Round,
            .StartCap = LineCap.Round
        }

        Dim CircleSize As Single

        If ClientSize.Height / 1.5 > 300 Then

            CircleSize = ClientSize.Height / 1.5

        Else

            CircleSize = 300

        End If

        CircleOfProgress.Width = CircleSize
        CircleOfProgress.Height = CircleSize

        CircleOfProgress.X = ClientSize.Width / 2 - CircleOfProgress.Width / 2
        CircleOfProgress.Y = ClientSize.Height / 2 - CircleOfProgress.Height / 2

    End Sub

End Class

' Monica is our an AI assistant.
' https://monica.im/