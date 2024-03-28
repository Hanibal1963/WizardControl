' ****************************************************************************************************************
' Wizard.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

''' <summary>Ein Control zum erstellen eines Assistenen</summary>
<ProvideToolboxControl("SchlumpfSoft Controls", False)>
<Description("Ein Control zum erstellen eines Assistenen")>
<ToolboxItem(True)>
<ToolboxBitmap(GetType(Wizard), "Wizard.bmp")>
<Designer(GetType(WizardDesigner))>
Public Class Wizard : Inherits UserControl

    'Private Const FOOTER_AREA_HEIGHT As Integer = 48

    Public Delegate Sub BeforeSwitchPagesEventHandler(sender As Object, e As BeforeSwitchPagesEventArgs)
    Public Delegate Sub AfterSwitchPagesEventHandler(sender As Object, e As AfterSwitchPagesEventArgs)

    <Category("Wizard")>
    <Description("Tritt auf, bevor die Seiten des Assistenten gewechselt werden, um dem Benutzer die Möglichkeit zur Validierung zu geben.")>
    Public Event BeforeSwitchPages As BeforeSwitchPagesEventHandler

    <Category("Wizard")>
    <Description("Tritt auf, nachdem die Seiten des Assistenten gewechselt wurden, und gibt dem Benutzer die Möglichkeit, die neue Seite einzurichten.")>
    Public Event AfterSwitchPages As AfterSwitchPagesEventHandler

    <Category("Wizard")>
    <Description("Tritt ein, wenn der Assistent abgebrochen wird, und gibt dem Benutzer die Möglichkeit zur Validierung.")>
    Public Event Cancel As CancelEventHandler

    <Category("Wizard")>
    <Description("Tritt auf, wenn der Assistent abgeschlossen ist, und gibt dem Benutzer die Möglichkeit, zusätzliche Aufgaben zu erledigen.")>
    Public Event Finish As EventHandler

    <Category("Wizard")>
    <Description("Tritt auf, wenn der Benutzer auf die Hilfeschaltfläche klickt.")>
    Public Event Help As EventHandler

    <Browsable(True)>
    <Category("Wizard")>
    <Description("Ruft die Sichtbarkeit Status der Hilfeschaltfläche ab oder legt diesen fest.")>
    <DefaultValue(True)>
    Public Property VisibleHelp As Boolean
        Get
            Return Me.ButtonHelp.Visible
        End Get
        Set(value As Boolean)
            Me.ButtonHelp.Visible = value
            Try
                Dim e As EventArgs = Nothing
                If Not Me.ButtonHelp.Visible Then
                    Me.Controls.Remove(Me.ButtonHelp)
                    Me.OnResize(e)
                Else
                    Me.Controls.Add(Me.ButtonHelp)
                    Me.OnResize(e)
                End If

            Catch
            End Try
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    <Category("Wizard")>
    <Description("Ruft die Auflistung der Assistentenseiten in diesem Registerkartensteuerelement ab.")>
    <Editor(GetType(PagesCollectionEditor), GetType(UITypeEditor))>
    Public ReadOnly Property Pages As PagesCollection
        Get
            Return _Pages
        End Get
    End Property

    <Browsable(True)>
    <Category("Wizard")>
    <Description("Ruft das in der Kopfzeile der Standardseiten angezeigte Bild ab oder legt dieses fest.")>
    Public Property ImageHeader As Image
        Get
            Return _ImageHeader
        End Get
        Set(value As Image)
            If _ImageHeader IsNot value Then
                _ImageHeader = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    <Category("Wizard")>
    <Description("Ruft das auf den Begrüßungs- und Abschlussseiten angezeigte Bild ab oder legt es fest.")>
    Public Property ImageWelcome As Image
        Get
            Return _ImageWelcome
        End Get
        Set(value As Image)
            If _ImageWelcome IsNot value Then
                _ImageWelcome = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("Layout")>
    <Description("Ruft ab oder legt fest, an welcher Kante des übergeordneten Containers ein Steuerelement angedockt ist.")>
    <DefaultValue(DockStyle.Fill)>
    Public Overloads Property Dock As DockStyle
        Get
            Return MyBase.Dock
        End Get
        Set(value As DockStyle)
            MyBase.Dock = value
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedPage As WizardPage
        Get
            Return _SelectedPage
        End Get
        Set(value As WizardPage)
            Me.ActivatePage(value)
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Friend Property SelectedIndex As Integer
        Get
            Return _Pages.IndexOf(_SelectedPage)
        End Get
        Set(value As Integer)
            If _Pages.Count = 0 Then
                Me.ActivatePage(-1)
                Return
            End If
            If value < -1 OrElse value >= _Pages.Count Then
                Throw New ArgumentOutOfRangeException(
                    $"SelectedIndex", value,
                    $"Der Seitenindex muss zwischen 0 und {_Pages.Count - 1} liegen ")
            End If
            Me.ActivatePage(value)
        End Set
    End Property

    <Category("Appearance")>
    <Description("Ruft die Schriftart ab, die zum Anzeigen der Beschreibung einer Standardseite verwendet wird, oder legt diese fest.")>
    Public Property HeaderFont As Font
        Get
            Return If(_HeaderFont, MyBase.Font)
        End Get
        Set(value As Font)
            If _HeaderFont IsNot value Then
                _HeaderFont = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <Description("Ruft die Schriftart ab, die zum Anzeigen des Titels einer Standardseite verwendet wird, oder legt diese fest.")>
    Public Property HeaderTitleFont As Font
        Get
            Return If(
                _HeaderTitleFont,
                New Font(
                MyBase.Font.FontFamily,
                MyBase.Font.Size + 2.0F,
                FontStyle.Bold))
        End Get
        Set(value As Font)
            If _HeaderTitleFont IsNot value Then
                _HeaderTitleFont = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <Description("Ruft die Schriftart ab, die zum Anzeigen der Beschreibung einer Begrüßungs- oder Abschlussseite verwendet wird, oder legt diese fest.")>
    Public Property WelcomeFont As Font
        Get
            Return If(_WelcomeFont, MyBase.Font)
        End Get
        Set(value As Font)
            If _WelcomeFont IsNot value Then
                _WelcomeFont = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <Description("Ruft die Schriftart ab, die zum Anzeigen des Titels einer Begrüßungs- oder Abschlussseite verwendet wird, oder legt diese fest.")>
    Public Property WelcomeTitleFont As Font
        Get
            Return If(
                _WelcomeTitleFont,
                New Font(
                MyBase.Font.FontFamily,
                MyBase.Font.Size + 10.0F,
                FontStyle.Bold))
        End Get
        Set(value As Font)
            If _WelcomeTitleFont IsNot value Then
                _WelcomeTitleFont = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property NextEnabled As Boolean
        Get
            Return Me.ButtonNext.Enabled
        End Get
        Set(value As Boolean)
            Me.ButtonNext.Enabled = value
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BackEnabled As Boolean
        Get
            Return Me.ButtonBack.Enabled
        End Get
        Set(value As Boolean)
            Me.ButtonBack.Enabled = value
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property NextText As String
        Get
            Return Me.ButtonNext.Text
        End Get
        Set(value As String)
            Me.ButtonNext.Text = value
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BackText As String
        Get
            Return Me.ButtonBack.Text
        End Get
        Set(value As String)
            Me.ButtonBack.Text = value
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CancelText As String
        Get
            Return Me.ButtonCancel.Text
        End Get
        Set(value As String)
            Me.ButtonCancel.Text = value
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property HelpText As String
        Get
            Return Me.ButtonHelp.Text
        End Get
        Set(value As String)
            Me.ButtonHelp.Text = value
        End Set
    End Property

    Public Sub New()

        Me.InitializeComponent()
        Me.InitializeStyles()
        MyBase.Dock = DockStyle.Fill
        _Pages = New PagesCollection(Me)

    End Sub

    Private Sub InitializeStyles()

        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.UserPaint, True)

    End Sub

    Protected Function ShouldSerializeHeaderFont() As Boolean
        Return _HeaderFont IsNot Nothing
    End Function

    Protected Function ShouldSerializeHeaderTitleFont() As Boolean
        Return _HeaderTitleFont IsNot Nothing
    End Function

    Protected Function ShouldSerializeWelcomeFont() As Boolean
        Return _WelcomeFont IsNot Nothing
    End Function

    Protected Function ShouldSerializeWelcomeTitleFont() As Boolean
        Return _WelcomeTitleFont IsNot Nothing
    End Function

    Public Sub [Next]()
        If Me.SelectedIndex = _Pages.Count - 1 Then
            Me.ButtonNext.Enabled = False
        Else
            Me.OnBeforeSwitchPages(
                New BeforeSwitchPagesEventArgs(
                Me.SelectedIndex,
                Me.SelectedIndex + 1))
        End If

    End Sub

    Public Sub Back()

        If Me.SelectedIndex = 0 Then
            Me.ButtonBack.Enabled = False
        Else
            Me.OnBeforeSwitchPages(
                New BeforeSwitchPagesEventArgs(
                Me.SelectedIndex,
                Me.SelectedIndex - 1))
        End If

    End Sub

    Private Sub ActivatePage(index As Integer)

        If index >= 0 AndAlso index < _Pages.Count Then
            Dim page As WizardPage = _Pages(index)
            Me.ActivatePage(page)
        End If

    End Sub

    Private Sub ActivatePage(page As WizardPage)

        If Not _Pages.Contains(page) Then
            Return
        End If

        If _SelectedPage IsNot Nothing Then
            _SelectedPage.Visible = False
        End If

        _SelectedPage = page

        If _SelectedPage IsNot Nothing Then

            _SelectedPage.Parent = Me

            If Not MyBase.Contains(_SelectedPage) Then
                Me.Container.Add(_SelectedPage)
            End If


            '---------------------------------------------------

            'TODO: Hier auf die verschiedenen Sprachen reagieren


            If _SelectedPage.Style = PageStyle.Finish Then
                Me.ButtonCancel.Text = "Fertigstellen"
                Me.ButtonCancel.DialogResult = DialogResult.OK
            Else
                Me.ButtonCancel.Text = "Abbruch"
                Me.ButtonCancel.DialogResult = DialogResult.Cancel
            End If

            If _SelectedPage.Style = PageStyle.Custom And _SelectedPage Is _Pages(_Pages.Count - 1) Then
                Me.ButtonCancel.Text = "OK"
                Me.ButtonCancel.DialogResult = DialogResult.OK
            End If

            '----------------------------------------------------


            _SelectedPage.SetBounds(0, 0, Me.Width, Me.Height - 48)
            _SelectedPage.Visible = True
            _SelectedPage.BringToFront()
            Me.FocusFirstTabIndex(_SelectedPage)

        End If

        Me.ButtonBack.Enabled = Me.SelectedIndex > 0

        If Me.SelectedIndex < _Pages.Count - 1 Then
            Me.ButtonNext.Enabled = True
        Else
            If Not Me.DesignMode Then
            End If
            Me.ButtonNext.Enabled = False
        End If

        If _SelectedPage IsNot Nothing Then
            _SelectedPage.Invalidate()
        Else
            Me.Invalidate()
        End If

    End Sub

    Private Sub FocusFirstTabIndex(container As Control)
        Dim control As Control = Nothing

        For Each control2 As Control In container.Controls

            If control2.CanFocus AndAlso (control Is Nothing OrElse control2.TabIndex < control.TabIndex) Then
                control = control2
            End If

        Next

        If control IsNot Nothing Then
            Dim unused = control.Focus()
        Else
            Dim unused1 = container.Focus()
        End If

    End Sub

    Protected Overridable Sub OnBeforeSwitchPages(e As BeforeSwitchPagesEventArgs)

        RaiseEvent BeforeSwitchPages(Me, e)

        If Not e.Cancel Then
            Me.ActivatePage(e.NewIndex)
            Me.OnAfterSwitchPages(e)
        End If

    End Sub

    Protected Overridable Sub OnAfterSwitchPages(e As AfterSwitchPagesEventArgs)
        RaiseEvent AfterSwitchPages(Me, e)
    End Sub

    Protected Overridable Sub OnCancel(e As CancelEventArgs)
        RaiseEvent Cancel(Me, e)
        If e.Cancel Then
            Me.ParentForm.DialogResult = DialogResult.None
        Else
            Me.ParentForm.Close()
        End If
    End Sub

    Protected Overridable Sub OnFinish(e As EventArgs)
        RaiseEvent Finish(Me, e)
        Me.ParentForm.Close()
    End Sub

    Protected Overridable Sub OnHelp(e As EventArgs)
        RaiseEvent Help(Me, e)
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)

        MyBase.OnLoad(e)
        If _Pages.Count > 0 Then
            Me.ActivatePage(0)
        End If

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)

        MyBase.OnResize(e)
        If _SelectedPage IsNot Nothing Then
            _SelectedPage.SetBounds(0, 0, Me.Width, Me.Height - 48)
        End If
        Me.ButtonHelp.Location = New Point(Me.ButtonHelp.Location.X, Me.Height - _OffsetBack.Y)
        Me.ButtonBack.Location = New Point(Me.Width - _OffsetBack.X, Me.Height - _OffsetBack.Y)
        Me.ButtonNext.Location = New Point(Me.Width - _OffsetNext.X, Me.Height - _OffsetNext.Y)
        Me.ButtonCancel.Location = New Point(Me.Width - _OffsetCancel.X, Me.Height - _OffsetCancel.Y)
        MyBase.Refresh()

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        MyBase.OnPaint(e)
        Dim clientRectangle = MyBase.ClientRectangle
        clientRectangle.Y = Me.Height - 48
        clientRectangle.Height = 48
        ControlPaint.DrawBorder3D(
            e.Graphics,
            clientRectangle,
            Border3DStyle.Etched,
            Border3DSide.Top)

    End Sub

    Protected Overrides Sub OnControlAdded(e As ControlEventArgs)

        If Not (TypeOf e.Control Is WizardPage) AndAlso e.Control IsNot Me.ButtonCancel AndAlso e.Control IsNot Me.ButtonNext AndAlso e.Control IsNot Me.ButtonBack Then
            If _SelectedPage IsNot Nothing Then
                _SelectedPage.Controls.Add(e.Control)
            End If
        Else
            MyBase.OnControlAdded(e)
        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles _
        ButtonNext.Click, ButtonBack.Click,
        ButtonCancel.Click, ButtonHelp.Click

        Select Case True

            Case sender Is Me.ButtonNext : Me.Next()
            Case sender Is Me.ButtonBack : Me.Back()
            Case sender Is Me.ButtonHelp : Me.OnHelp(EventArgs.Empty)
            Case sender Is Me.ButtonCancel
                If Me.ButtonCancel.DialogResult = DialogResult.Cancel Then
                    Me.OnCancel(New CancelEventArgs())
                ElseIf Me.ButtonCancel.DialogResult = DialogResult.OK Then
                    Me.OnFinish(EventArgs.Empty)
                End If

        End Select

    End Sub

End Class
