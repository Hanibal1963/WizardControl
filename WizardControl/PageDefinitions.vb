' ****************************************************************************************************************
' PageDefinitions.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>Auflistung der verfügbaren Seitenstile</summary>
Public Enum PageStyle

    Standard = 0
    Welcome = 1
    Finish = 2
    Custom = 3

End Enum

''' <summary>Definiert eine Seite des Controls</summary>
<ToolboxItem(False)>
Public Class WizardPage

    Inherits Panel

    Private Const HEADER_AREA_HEIGHT As Integer = 64
    Private Const HEADER_GLYPH_SIZE As Integer = 48
    Private Const HEADER_TEXT_PADDING As Integer = 8
    Private Const WELCOME_GLYPH_WIDTH As Integer = 164

    Private styleField As PageStyle = PageStyle.Standard
    Private titleField As String = String.Empty
    Private descriptionField As String = String.Empty

    <Category("Wizard")>
    <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
    Public Overridable Property Style As PageStyle
        Get
            Return Me.styleField
        End Get
        Set(value As PageStyle)
            If Me.styleField = value Then
                Return
            End If
            Me.styleField = value
            If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then
                Dim wizard As Wizard = CType(Me.Parent, Wizard)
                If wizard.SelectedPage Is Me Then
                    wizard.SelectedPage = Me
                End If
            Else
                Me.Invalidate()
            End If
        End Set
    End Property

    <DefaultValue("")>
    <Category("Wizard")>
    <Description("Ruft den Titel der Assistentenseite ab oder legt diesen fest.")>
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
                Me.Invalidate()
            End If
        End Set
    End Property

    <DefaultValue("")>
    <Category("Wizard")>
    <Description("Ruft die Beschreibung der Assistentenseite ab oder legt diese fest.")>
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
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Sub New()
        Me.InitializeStyles()
    End Sub

    Private Sub InitializeStyles()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If Me.styleField = PageStyle.Custom Then
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
            Case PageStyle.Standard
                clientRectangle.Height = 64
                ControlPaint.DrawBorder3D(e.Graphics, clientRectangle, Border3DStyle.Etched, Border3DSide.Bottom)
                clientRectangle.Height -= SystemInformation.Border3DSize.Height
                e.Graphics.FillRectangle(SystemBrushes.Window, clientRectangle)
                Dim num2 As Integer = CInt(Math.Floor(8.0))
                empty.Location = New Point(Me.Width - 48 - num2, num2)
                empty.Size = New Size(48, 48)
                Dim image2 As Image = Nothing
                Dim font3 = MyBase.Font
                Dim font4 = MyBase.Font
                If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then
                    Dim wizard2 As Wizard = CType(Me.Parent, Wizard)
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
            Case PageStyle.Welcome, PageStyle.Finish
                e.Graphics.FillRectangle(SystemBrushes.Window, clientRectangle)
                empty.Location = Point.Empty
                empty.Size = New Size(164, Me.Height)
                Dim image As Image = Nothing
                Dim font = MyBase.Font
                Dim font2 = MyBase.Font
                If Me.Parent IsNot Nothing AndAlso TypeOf Me.Parent Is Wizard Then
                    Dim wizard As Wizard = CType(Me.Parent, Wizard)
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
                empty2.Width = Me.Width - empty2.Left - 8
                Dim num As Integer = CInt(Math.Ceiling(e.Graphics.MeasureString(Me.titleField, font2, empty2.Width, genericDefault).Height))
                empty3.Location = empty2.Location
                empty3.Y += num + 8
                empty3.Size = New Size(Me.Width - empty3.Left - 8, Me.Height - empty3.Y)
                e.Graphics.DrawString(Me.titleField, font2, SystemBrushes.WindowText, empty2, genericDefault)
                e.Graphics.DrawString(Me.descriptionField, font, SystemBrushes.WindowText, empty3, genericDefault)
                Exit Select
        End Select
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)
    End Sub

End Class

''' <summary>Definiert die Abschlußseite</summary>
<ToolboxItem(False)>
Public Class PageFinish

    Inherits WizardPage

    Private styleField As PageStyle = PageStyle.Finish

    <DefaultValue(PageStyle.Finish)>
    <Category("Wizard")>
    <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
    Public Overrides Property Style As PageStyle
        Get
            Return Me.styleField
        End Get
        Set(value As PageStyle)
            Me.styleField = value
        End Set
    End Property

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.BackColor = SystemColors.ControlDarkDark
        Me.ResumeLayout(False)
    End Sub

End Class

''' <summary>Definiert eine Benutzerdefinierte Seite</summary>
<ToolboxItem(False)>
Public Class PageCustom

    Inherits WizardPage

    Private styleField As PageStyle = PageStyle.Custom

    <DefaultValue(PageStyle.Custom)>
    <Category("Wizard")>
    <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
    Public Overrides Property Style As PageStyle
        Get
            Return Me.styleField
        End Get
        Set(value As PageStyle)
            Me.styleField = value
        End Set
    End Property

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.BackColor = SystemColors.ControlDarkDark
        Me.ResumeLayout(False)
    End Sub

End Class

''' <summary>Definiert eine Standardseite</summary>
<ToolboxItem(False)>
Public Class PageStandard

    Inherits WizardPage

    Private styleField As PageStyle = PageStyle.Standard

    <DefaultValue(PageStyle.Standard)>
    <Category("Wizard")>
    <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
    Public Overrides Property Style As PageStyle
        Get
            Return Me.styleField
        End Get
        Set(value As PageStyle)
            Me.styleField = value
        End Set
    End Property

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.BackColor = SystemColors.ControlDarkDark
        Me.ResumeLayout(False)
    End Sub

End Class

''' <summary>Definiert die Willkommenseite</summary>
<ToolboxItem(False)>
Public Class PageWelcome

    Inherits WizardPage

    Private styleField As PageStyle = PageStyle.Welcome

    <DefaultValue(PageStyle.Welcome)>
    <Category("Wizard")>
    <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
    Public Overrides Property Style As PageStyle
        Get
            Return Me.styleField
        End Get
        Set(value As PageStyle)
            Me.styleField = value
        End Set
    End Property

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.BackColor = SystemColors.ControlDarkDark
        Me.ResumeLayout(False)
    End Sub

End Class

''' <summary>Dient zum anzeigen der Seitenstile im Seitendesigner</summary>
Friend Class PagesCellectionEditor

    Inherits CollectionEditor

    Private Types As Type()

    Public Sub New(type As Type)
        MyBase.New(type)
        Me.Types = New Type(3) {GetType(PageWelcome), GetType(PageStandard), GetType(PageCustom), GetType(PageFinish)}
    End Sub

    Protected Overrides Function CreateNewItemTypes() As Type()
        Return Me.Types
    End Function

End Class

''' <summary>Definiert die Auflistung der Seiten des Assistenten</summary>
Public Class PagesCollection

    Inherits CollectionBase

    Private owner As Wizard = Nothing

    Default Public Property Item(index As Integer) As WizardPage
        Get
            Return CType(List(index), WizardPage)
        End Get
        Set(value As WizardPage)
            List(index) = value
        End Set
    End Property

    Friend Sub New(owner As Wizard)
        Me.owner = owner
    End Sub

    Public Function Add(value As WizardPage) As Integer
        Return List.Add(value)
    End Function

    Public Sub AddRange(pages As WizardPage())
        For Each value As WizardPage In pages
            Me.Add(value)
        Next
    End Sub

    Public Function IndexOf(value As WizardPage) As Integer
        Return List.IndexOf(value)
    End Function

    Public Sub Insert(index As Integer, value As WizardPage)
        List.Insert(index, value)
    End Sub

    Public Sub Remove(value As WizardPage)
        List.Remove(value)
    End Sub

    Public Function Contains(value As WizardPage) As Boolean
        Return List.Contains(value)
    End Function

    Protected Overrides Sub OnInsertComplete(index As Integer, value As Object)
        MyBase.OnInsertComplete(index, value)
        Me.owner.SelectedIndex = index
    End Sub

    Protected Overrides Sub OnRemoveComplete(index As Integer, value As Object)
        MyBase.OnRemoveComplete(index, value)
        If Me.owner.SelectedIndex = index Then
            If index < InnerList.Count Then
                Me.owner.SelectedIndex = index
            Else
                Me.owner.SelectedIndex = InnerList.Count - 1
            End If
        End If
    End Sub

End Class
