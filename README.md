# Draw Arc

This interactive application provides a visual demonstration of arcs drawn with various starting and sweep angles using GDI (Graphics Device Interface) graphics.

It aims to help users gain a deeper understanding of how angles affect the rendering of arcs in graphical applications.




![003](https://github.com/user-attachments/assets/bde01867-02e5-4cb1-af6d-0ab1265832dd)







## Features

- **Interactive Visualization**: Adjust starting and sweep angles dynamically to see real-time changes in arc rendering.
- **Intuitive Controls**: User-friendly interface for manipulating angles.
- **Educational Tool**: Designed to help users grasp the concepts of angles in graphics programming.

---

## Code Walkthrough

#### 1. **Imports**
```vb
Imports System.Drawing.Drawing2D
```
- **Explanation**: This line imports the `System.Drawing.Drawing2D` namespace, which provides advanced 2D graphics functionality. It includes classes for drawing shapes, such as arcs and curves.

#### 2. **Class Definition**
```vb
Public Class Form1
```
- **Explanation**: This line defines a new class called `Form1`. In VB.NET, a class is a blueprint for creating objects. Here, `Form1` is likely a form (window) in the application.

#### 3. **Variable Declarations**
```vb
Private Context As BufferedGraphicsContext
Private Buffer As BufferedGraphics
```
- **Explanation**: 
  - `Context`: This variable will hold the graphics context for buffered graphics, which helps in rendering graphics smoothly.
  - `Buffer`: This variable is used to store the graphics buffer, which allows for double buffering to reduce flickering during drawing.

#### 4. **Structure Definition**
```vb
Private Structure DisplayStructure
    Public Location As Point
    Public Text As String
    Public Font As Font
End Structure
```
- **Explanation**: 
  - This defines a structure called `DisplayStructure` that holds information about where to display text (`Location`), the text itself (`Text`), and the font used (`Font`).

#### 5. **More Variable Declarations**
```vb
Private CodeDisplay As DisplayStructure
Private CircleOfProgress As Rectangle
Private CircleOfProgressPen As Pen
Private CircleOfProgressBackgroundPen As Pen
```
- **Explanation**: 
  - `CodeDisplay`: An instance of `DisplayStructure` to display code-related information.
  - `CircleOfProgress`: A rectangle that defines the area where the arc will be drawn.
  - `CircleOfProgressPen` and `CircleOfProgressBackgroundPen`: Pens used to draw the arc and its background.

#### 6. **Constants**
```vb
Private Const startAngle As Single = 0.0F
Private sweepAngle As Single
```
- **Explanation**: 
  - `startAngle`: A constant representing the starting angle for the arc (0 degrees).
  - `sweepAngle`: A variable that will hold the sweep angle, which determines how far the arc extends.

#### 7. **String Format for Centering Text**
```vb
Private ReadOnly AlineCenterMiddle As New StringFormat With {
    .Alignment = StringAlignment.Center,
    .LineAlignment = StringAlignment.Center}
```
- **Explanation**: This creates a `StringFormat` object that centers text both horizontally and vertically.

#### 8. **Form Load Event**
```vb
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    InitializeApp()
    Debug.Print($"Program running... {Now.ToShortTimeString}")
End Sub
```
- **Explanation**: 
  - This method is called when the form loads. It initializes the application by calling `InitializeApp()`.
  - It also prints a message to the debug console indicating that the program is running, along with the current time.

#### 9. **Form Resize Event**
```vb
Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    If Not WindowState = FormWindowState.Minimized Then
        ResizeCodeDisplay()
        ResizeControls()
        ResizeCircleOfProgress()
        DisposeBuffer()
    End If
End Sub
```
- **Explanation**: 
  - This method handles the form's resize event. If the window is not minimized, it resizes various components of the form (like text display and controls) and disposes of the buffer to prepare for redrawing.

#### 10. **Timer Tick Event**
```vb
Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    If Not WindowState = FormWindowState.Minimized Then
        UpdateCodeDisplay()
        Invalidate() ' Calls OnPaint Sub
    End If
End Sub
```
- **Explanation**: 
  - This method is called every time the timer ticks (every 30 milliseconds, as set later). It updates the code display and triggers a repaint of the form.

#### 11. **OnPaint Method**
```vb
Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
    AllocateBuffer(e)
    DrawDisplays()
    Buffer.Render(e.Graphics)
    MyBase.OnPaint(e)
End Sub
```
- **Explanation**: 
  - This method is overridden to perform custom painting. It allocates a graphics buffer, draws the displays, and then renders the buffer onto the form.

#### 12. **TrackBar Scroll Events**
```vb
Private Sub StartAngleTrackBar_Scroll(sender As Object, e As EventArgs) Handles StartAngleTrackBar.Scroll
    StartAngleLabel.Text = $"Start Angle: {StartAngleTrackBar.Value}°"
End Sub

Private Sub SweepAngleTrackBar_Scroll(sender As Object, e As EventArgs) Handles SweepAngleTrackBar.Scroll
    SweepAngleLabel.Text = $"Sweep Angle: {SweepAngleTrackBar.Value}°"
End Sub
```
- **Explanation**: 
  - These methods handle the scrolling of two trackbars (sliders) for adjusting the start angle and sweep angle. They update the corresponding labels to show the current angle values.

#### 13. **Update Code Display Method**
```vb
Private Sub UpdateCodeDisplay()
    CodeDisplay.Text = $"Graphics.DrawArc(Pen, Rectangle, {StartAngleTrackBar.Value}, {SweepAngleTrackBar.Value})"
End Sub
```
- **Explanation**: 
  - This method updates the text displayed in the `CodeDisplay` structure to show the current code that would be used to draw the arc with the selected angles.

#### 14. **OnPaintBackground Method**
```vb
Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
    'Intentionally left blank. Do not remove.
End Sub
```
- **Explanation**: 
  - This method is overridden but intentionally left blank to prevent flickering during painting.

#### 15. **Allocate Buffer Method**
```vb
Private Sub AllocateBuffer(e As PaintEventArgs)
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
```
- **Explanation**: 
  - This method checks if the buffer has been allocated. If not, it allocates a new buffer and sets various rendering properties to ensure high-quality graphics.

#### 16. **Draw Displays Method**
```vb
Private Sub DrawDisplays()
    If Buffer IsNot Nothing Then
        Try
            With Buffer.Graphics
                .Clear(SystemColors.Control)
                .DrawEllipse(CircleOfProgressBackgroundPen, CircleOfProgress)
                .DrawArc(CircleOfProgressPen, CircleOfProgress, StartAngleTrackBar.Value, SweepAngleTrackBar.Value)
                .DrawString(CodeDisplay.Text, CodeDisplay.Font, New SolidBrush(SystemColors.ControlText), CodeDisplay.Location, AlineCenterMiddle)
            End With
        Catch ex As Exception
            Debug.Print("Draw error: " & ex.Message)
        End Try
    Else
        Debug.Print("Buffer is not initialized.")
    End If
End Sub
```
- **Explanation**: 
  - This method draws the background ellipse, the arc based on the current angles, and the code display text on the buffer. It also handles any potential drawing errors.

#### 17. **Dispose Buffer Method**
```vb
Private Sub DisposeBuffer()
    If Buffer IsNot Nothing Then
        Buffer.Dispose()
        Buffer = Nothing ' Set to Nothing to avoid using a disposed object
    End If
End Sub
```
- **Explanation**: 
  - This method disposes of the buffer if it exists and sets it to `Nothing` to avoid using a disposed object.

#### 18. **Initialize App Method**
```vb
Private Sub InitializeApp()
    InitializeForm()
    InitializeBuffer()
    InitializeLabels()
    InitializeTimer()
End Sub
```
- **Explanation**: 
  - This method initializes the application by calling several initialization methods for the form, buffer, labels, and timer.

#### 19. **Initialize Form Method**
```vb
Private Sub InitializeForm()
    CenterToScreen()
    SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint, True)
    SetStyle(ControlStyles.UserPaint, True)
    Text = "Draw Arc - Code with Joe"
    Me.WindowState = FormWindowState.Maximized
End Sub
```
- **Explanation**: 
  - This method centers the form on the screen, sets styles for double buffering (to reduce flickering), sets the window title, and maximizes the window.

#### 20. **Initialize Buffer Method**
```vb
Private Sub InitializeBuffer()
    Context = BufferedGraphicsManager.Current
    Context.MaximumBuffer = Screen.PrimaryScreen.WorkingArea.Size
    Buffer = Context.Allocate(CreateGraphics(), ClientRectangle)
    With Buffer.Graphics
        .CompositingMode = Drawing2D.CompositingMode.SourceOver
        .TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        .PixelOffsetMode = Drawing2D.PixelOffsetMode.None
        .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
    End With
End Sub
```
- **Explanation**: 
  - This method initializes the graphics buffer context, sets the maximum buffer size, and allocates the buffer with high-quality rendering settings.

#### 21. **Initialize Timer Method**
```vb
Private Sub InitializeTimer()
    Timer1.Interval = 30
    Timer1.Enabled = True
End Sub
```
- **Explanation**: 
  - This method sets the timer interval to 30 milliseconds and enables the timer, which will trigger periodic updates.

#### 22. **Initialize Labels Method**
```vb
Private Sub InitializeLabels()
    StartAngleLabel.Text = $"Start Angle: {StartAngleTrackBar.Value}°"
    SweepAngleLabel.Text = $"Sweep Angle: {SweepAngleTrackBar.Value}°"
End Sub
```
- **Explanation**: 
  - This method initializes the text for the angle labels based on the initial values of the trackbars.

#### 23. **Resize Methods**
- **ResizeCodeDisplay, ResizeControls, ResizeCircleOfProgress**
```vb
Private Sub ResizeCodeDisplay()
    ' Resizes the code display area based on form size
End Sub

Private Sub ResizeControls()
    ' Resizes and positions the trackbars and labels based on form size
End Sub

Private Sub ResizeCircleOfProgress()
    ' Resizes the circle and pens based on form size
End Sub
```
- **Explanation**: 
  - These methods adjust the size and position of the code display, controls (like trackbars), and the drawing area (circle) based on the current size of the form.


This code creates an interactive application that allows users to visualize how arcs are drawn based on different starting and sweep angles. It uses VB.NET's GDI+ for graphics rendering and provides a user-friendly interface for manipulating the angles. Each section of the code is designed to handle specific aspects of the application, from initialization to drawing and resizing components. 





![AnglesDiagram](https://github.com/user-attachments/assets/9964ef31-4ba1-459d-9e5c-5f53d95393f4)

