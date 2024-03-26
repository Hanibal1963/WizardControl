Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

<ToolboxItem(False)>
Public Class WizardPage

    Inherits Panel

    Friend Class WizardPageDesigner

        Inherits ParentControlDesigner

        Public Overrides ReadOnly Property SelectionRules As SelectionRules
            Get
                Return SelectionRules.Locked Or SelectionRules.Visible
            End Get
        End Property

    End Class

    Private Const HEADER_AREA_HEIGHT As Integer = 64

    Private Const HEADER_GLYPH_SIZE As Integer = 48

    Private Const HEADER_TEXT_PADDING As Integer = 8

    Private Const WELCOME_GLYPH_WIDTH As Integer = 164

    Private styleField As WizardPageStyle = WizardPageStyle.Standard

    Private titleField As String = String.Empty

    Private descriptionField As String = String.Empty

    <Category("Wizard")>
    <Description("Gets or sets the style of the wizard page.")>
    Public Overridable Property Style As WizardPageStyle
        Get
            Return Me.styleField
        End Get
        Set(value As WizardPageStyle)
            If Me.styleField = value Then
                Return
            End If
            Me.styleField = value
            If Parent IsNot Nothing AndAlso TypeOf Parent Is Wizard Then
                Dim wizard As Wizard = CType(Parent, Wizard)
                If wizard.SelectedPage Is Me Then
                    wizard.SelectedPage = Me
                End If
            Else
                Invalidate()
            End If
        End Set
    End Property

    <DefaultValue("")>
    <Category("Wizard")>
    <Description("Gets or sets the title of the wizard page.")>
    Public Overridable Property Title As String
        Get
            Return Me.titleField
        End Get
        Set(value As String)
            If Equals(value, Nothing) Then
                value = String.Empty
            End If
            If Not Equals(Me.titleField, value) Then
                Me.titleField = value
                Invalidate()
            End If
        End Set
    End Property

    <DefaultValue("")>
    <Category("Wizard")>
    <Description("Gets or sets the description of the wizard page.")>
    Public Overridable Property Description As String
        Get
            Return Me.descriptionField
        End Get
        Set(value As String)
            If Equals(value, Nothing) Then
                value = String.Empty
            End If
            If Not Equals(Me.descriptionField, value) Then
                Me.descriptionField = value
                Invalidate()
            End If
        End Set
    End Property

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserPaint, True)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If Me.styleField = WizardPageStyle.Custom Then
            Return
        End If
        Dim clientRectangle = MyBase.ClientRectangle
        Dim empty = Rectangle.Empty
        Dim empty2 = Rectangle.Empty
        Dim empty3 = Rectangle.Empty
        Dim genericDefault = StringFormat.GenericDefault
        genericDefault.LineAlignment = StringAlignment.Near
        genericDefault.Alignment = StringAlignment.Near
        genericDefault.Trimming = StringTrimming.EllipsisCharacter
        Select Case Me.Style
            Case WizardPageStyle.Standard
                clientRectangle.Height = 64
                ControlPaint.DrawBorder3D(e.Graphics, clientRectangle, Border3DStyle.Etched, Border3DSide.Bottom)
                clientRectangle.Height -= SystemInformation.Border3DSize.Height
                e.Graphics.FillRectangle(SystemBrushes.Window, clientRectangle)
                Dim num2 As Integer = CInt(Math.Floor(8.0))
                empty.Location = New Point(Width - 48 - num2, num2)
                empty.Size = New Size(48, 48)
                Dim image2 As Image = Nothing
                Dim font3 = MyBase.Font
                Dim font4 = MyBase.Font
                If Parent IsNot Nothing AndAlso TypeOf Parent Is Wizard Then
                    Dim wizard2 As Wizard = CType(Parent, Wizard)
                    image2 = wizard2.ImageHeader
                    If image2 Is Nothing Then
                        empty.Size = New Size(0, 0)
                    End If
                    font3 = wizard2.HeaderFont
                    font4 = wizard2.HeaderTitleFont
                End If
                If image2 Is Nothing Then
                    ControlPaint.DrawFocusRectangle(e.Graphics, empty)
                Else
                    e.Graphics.DrawImage(image2, empty)
                End If
                Dim num3 As Integer = CInt(Math.Ceiling(e.Graphics.MeasureString(Me.titleField, font4, 0, genericDefault).Height))
                empty2.Location = New Point(8, 8)
                empty2.Size = New Size(empty.Left - 8, num3)
                empty3.Location = empty2.Location
                empty3.Y += num3 + 4
                empty3.Size = New Size(empty2.Width, 64 - empty3.Y)
                e.Graphics.DrawString(Me.titleField, font4, SystemBrushes.WindowText, empty2, genericDefault)
                e.Graphics.DrawString(Me.descriptionField, font3, SystemBrushes.WindowText, empty3, genericDefault)
                Exit Select
            Case WizardPageStyle.Welcome, WizardPageStyle.Finish
                e.Graphics.FillRectangle(SystemBrushes.Window, clientRectangle)
                empty.Location = Point.Empty
                empty.Size = New Size(164, Height)
                Dim image As Image = Nothing
                Dim font = MyBase.Font
                Dim font2 = MyBase.Font
                If Parent IsNot Nothing AndAlso TypeOf Parent Is Wizard Then
                    Dim wizard As Wizard = CType(Parent, Wizard)
                    image = wizard.ImageWelcome
                    font = wizard.WelcomeFont
                    font2 = wizard.WelcomeTitleFont
                End If
                If image Is Nothing Then
                    ControlPaint.DrawFocusRectangle(e.Graphics, empty)
                Else
                    e.Graphics.DrawImage(image, empty)
                End If
                empty2.Location = New Point(172, 8)
                empty2.Width = Width - empty2.Left - 8
                Dim num As Integer = CInt(Math.Ceiling(e.Graphics.MeasureString(Me.titleField, font2, empty2.Width, genericDefault).Height))
                empty3.Location = empty2.Location
                empty3.Y += num + 8
                empty3.Size = New Size(Width - empty3.Left - 8, Height - empty3.Y)
                e.Graphics.DrawString(Me.titleField, font2, SystemBrushes.WindowText, empty2, genericDefault)
                e.Graphics.DrawString(Me.descriptionField, font, SystemBrushes.WindowText, empty3, genericDefault)
                Exit Select
        End Select
    End Sub

    Private Sub InitializeComponent()
        SuspendLayout()
        ResumeLayout(False)
    End Sub

End Class
