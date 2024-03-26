Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

<ToolboxBitmap("D:\Data\Dokumente\Visual Studio 2019\ToolboxBitmap\RuWizard\Wizard16x16.ico")>
<ProvideToolboxControl("SchlumpfSoft Controls", False)>
<Designer(GetType(Wizard.WizardDesigner))>
Public Class Wizard

    Inherits UserControl

    Public Delegate Sub BeforeSwitchPagesEventHandler(sender As Object, e As BeforeSwitchPagesEventArgs)

    Public Delegate Sub AfterSwitchPagesEventHandler(sender As Object, e As AfterSwitchPagesEventArgs)

    Friend Class WizardDesigner

        Inherits ParentControlDesigner

        Protected Overrides Property DrawGrid As Boolean
            Get
                Return MyBase.DrawGrid
            End Get
            Set(value As Boolean)
                MyBase.DrawGrid = value
            End Set
        End Property

        Protected Overrides Sub WndProc(ByRef msg As Message)
            If msg.Msg = 513 OrElse msg.Msg = 515 Then
                Dim selectionService = CType(MyBase.GetService(GetType(ISelectionService)), ISelectionService)
                Dim wizard As Wizard = TryCast(selectionService.PrimarySelection, Wizard)
                If wizard IsNot Nothing Then
                    Dim x As Integer = CShort(CInt(msg.LParam) And &HFFFF)
                    Dim y As Integer = CShort(CUInt(CInt(msg.LParam) And -65536) >> 16)
                    Dim pt As Point = New Point(x, y)
                    If msg.HWnd = wizard.buttonNext.Handle Then
                        If wizard.buttonNext.Enabled AndAlso wizard.buttonNext.ClientRectangle.Contains(pt) Then
                            wizard.Next()
                        End If
                    ElseIf msg.HWnd = wizard.ButtonBack.Handle AndAlso wizard.ButtonBack.Enabled AndAlso wizard.ButtonBack.ClientRectangle.Contains(pt) Then
                        wizard.Back()
                    End If
                    Return
                End If
            End If
            MyBase.WndProc(msg)
        End Sub

    End Class

    Public Class AfterSwitchPagesEventArgs

        Inherits EventArgs

        Private ReadOnly oldIndexField As Integer

        Protected newIndexField As Integer

        Public ReadOnly Property OldIndex As Integer
            Get
                Return Me.oldIndexField
            End Get
        End Property

        Public ReadOnly Property NewIndex As Integer
            Get
                Return Me.newIndexField
            End Get
        End Property

        Friend Sub New(oldIndex As Integer, newIndex As Integer)
            Me.oldIndexField = oldIndex
            Me.newIndexField = newIndex
        End Sub
    End Class

    Public Class BeforeSwitchPagesEventArgs

        Inherits AfterSwitchPagesEventArgs

        Private cancelField As Boolean = False

        Public Property Cancel As Boolean
            Get
                Return Me.cancelField
            End Get
            Set(value As Boolean)
                Me.cancelField = value
            End Set
        End Property

        Public Overloads Property NewIndex As Integer
            Get
                Return newIndexField
            End Get
            Set(value As Integer)
                newIndexField = value
            End Set
        End Property

        Friend Sub New(oldIndex As Integer, newIndex As Integer)
            MyBase.New(oldIndex, newIndex)
        End Sub
    End Class

    Private Const FOOTER_AREA_HEIGHT As Integer = 48

    Private ReadOnly offsetCancel As Point = New Point(84, 36)

    Private ReadOnly offsetNext As Point = New Point(164, 36)

    Private ReadOnly offsetBack As Point = New Point(244, 36)

    Private selectedPageField As WizardPage = Nothing

    Private ReadOnly pagesField As WizardPagesCollection = Nothing

    Private imageHeaderField As Image = My.Resources.HeaderImage

    Private imageWelcomeField As Image = My.Resources.WelcomeImage

    Private headerFontField As Font = Nothing

    Private headerTitleFontField As Font = Nothing

    Private welcomeFontField As Font = Nothing

    Private welcomeTitleFontField As Font = Nothing

    Private ButtonHelp As Button

    Private ButtonBack As Button

    Private buttonNext As Button

    Private ButtonCancel As Button

    Private ReadOnly components As Container = Nothing

    <Browsable(True)>
    <Category("Wizard")>
    <DefaultValue(True)>
    <Description("Gets or sets the visible state of the help button.## ")>
    Public Property VisibleHelp As Boolean
        Get
            Return Me.ButtonHelp.Visible
        End Get
        Set(value As Boolean)
            Me.ButtonHelp.Visible = value
            Try
                Dim e As EventArgs = Nothing
                If Not Me.ButtonHelp.Visible Then
                    Controls.Remove(Me.ButtonHelp)
                    Me.OnResize(e)
                Else
                    Controls.Add(Me.ButtonHelp)
                    Me.OnResize(e)
                End If

            Catch
            End Try
        End Set
    End Property

    <Category("Wizard")>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    <ComponentModel.EditorAttribute(GetType(PagesCellectionEditor), GetType(UITypeEditor))>
    <Description("Gets the collection of wizard pages in this tab control.")>
    Public ReadOnly Property Pages As WizardPagesCollection
        Get
            Return Me.pagesField
        End Get
    End Property

    <Browsable(True)>
    <Category("Wizard")>
    <Description("Gets or sets the image displayed on the header of the standard pages.")>
    Public Property ImageHeader As Image
        Get
            Return Me.imageHeaderField
        End Get
        Set(value As Image)
            If Me.imageHeaderField IsNot value Then
                Me.imageHeaderField = value
                Invalidate()
            End If
        End Set
    End Property

    <Category("Wizard")>
    <Description("Gets or sets the image displayed on the welcome and finish pages.")>
    Public Property ImageWelcome As Image
        Get
            Return Me.imageWelcomeField
        End Get
        Set(value As Image)
            If Me.imageWelcomeField IsNot value Then
                Me.imageWelcomeField = value
                Invalidate()
            End If
        End Set
    End Property

    <DefaultValue(DockStyle.Fill)>
    <Category("Layout")>
    <Description("Gets or sets which edge of the parent container a control is docked to.")>
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
            Return Me.selectedPageField
        End Get
        Set(value As WizardPage)
            Me.ActivatePage(value)
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Friend Property SelectedIndex As Integer
        Get
            Return Me.pagesField.IndexOf(Me.selectedPageField)
        End Get
        Set(value As Integer)
            If Me.pagesField.Count = 0 Then
                Me.ActivatePage(-1)
                Return
            End If
            If value < -1 OrElse value >= Me.pagesField.Count Then
                Throw New ArgumentOutOfRangeException("SelectedIndex", value, "The page index must be between 0 and " & Convert.ToString(Me.pagesField.Count - 1))
            End If
            Me.ActivatePage(value)
        End Set
    End Property

    <Category("Appearance")>
    <Description("Gets or sets the font used to display the description of a standard page.")>
    Public Property HeaderFont As Font
        Get
            If Me.headerFontField Is Nothing Then
                Return MyBase.Font
            End If
            Return Me.headerFontField
        End Get
        Set(value As Font)
            If Me.headerFontField IsNot value Then
                Me.headerFontField = value
                Invalidate()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <Description("Gets or sets the font used to display the title of a standard page.")>
    Public Property HeaderTitleFont As Font
        Get
            If Me.headerTitleFontField Is Nothing Then
                Return New Font(MyBase.Font.FontFamily, MyBase.Font.Size + 2.0F, FontStyle.Bold)
            End If
            Return Me.headerTitleFontField
        End Get
        Set(value As Font)
            If Me.headerTitleFontField IsNot value Then
                Me.headerTitleFontField = value
                Invalidate()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <Description("Gets or sets the font used to display the description of a welcome of finish page.")>
    Public Property WelcomeFont As Font
        Get
            If Me.welcomeFontField Is Nothing Then
                Return MyBase.Font
            End If
            Return Me.welcomeFontField
        End Get
        Set(value As Font)
            If Me.welcomeFontField IsNot value Then
                Me.welcomeFontField = value
                Invalidate()
            End If
        End Set
    End Property

    <Category("Appearance")>
    <Description("Gets or sets the font used to display the title of a welcome of finish page.")>
    Public Property WelcomeTitleFont As Font
        Get
            If Me.welcomeTitleFontField Is Nothing Then
                Return New Font(MyBase.Font.FontFamily, MyBase.Font.Size + 10.0F, FontStyle.Bold)
            End If
            Return Me.welcomeTitleFontField
        End Get
        Set(value As Font)
            If Me.welcomeTitleFontField IsNot value Then
                Me.welcomeTitleFontField = value
                Invalidate()
            End If
        End Set
    End Property

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property NextEnabled As Boolean
        Get
            Return Me.buttonNext.Enabled
        End Get
        Set(value As Boolean)
            Me.buttonNext.Enabled = value
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
            Return Me.buttonNext.Text
        End Get
        Set(value As String)
            Me.buttonNext.Text = value
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

    <Category("Wizard")>
    <Description("Occurs before the wizard pages are switched, giving the user a chance to validate.")>
    Public Event BeforeSwitchPages As BeforeSwitchPagesEventHandler

    <Category("Wizard")>
    <Description("Occurs after the wizard pages are switched, giving the user a chance to setup the new page.")>
    Public Event AfterSwitchPages As AfterSwitchPagesEventHandler

    <Category("Wizard")>
    <Description("Occurs when wizard is canceled, giving the user a chance to validate.")>
    Public Event Cancel As CancelEventHandler

    <Category("Wizard")>
    <Description("Occurs when wizard is finished, giving the user a chance to do extra stuff.")>
    Public Event Finish As EventHandler

    <Category("Wizard")>
    <Description("Occurs when the user clicks the help button.")>
    Public Event Help As EventHandler

    Public Sub New()
        Me.InitializeComponent()
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserPaint, True)
        MyBase.Dock = DockStyle.Fill
        Me.pagesField = New WizardPagesCollection(Me)
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso Me.components IsNot Nothing Then
            Me.components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Me.ButtonHelp = New Button()
        Me.ButtonBack = New Button()
        Me.buttonNext = New Button()
        Me.ButtonCancel = New Button()
        SuspendLayout()
        Me.ButtonHelp.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Me.ButtonHelp.FlatStyle = FlatStyle.System
        Me.ButtonHelp.Location = New Point(8, 224)
        Me.ButtonHelp.Name = "ButtonHelp"
        Me.ButtonHelp.Size = New Size(75, 23)
        Me.ButtonHelp.TabIndex = 9
        Me.ButtonHelp.Text = "&Help"
        AddHandler Me.ButtonHelp.Click, New EventHandler(AddressOf Me.ButtonHelp_Click)
        Me.ButtonBack.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.ButtonBack.FlatStyle = FlatStyle.System
        Me.ButtonBack.Location = New Point(184, 224)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New Size(75, 23)
        Me.ButtonBack.TabIndex = 6
        Me.ButtonBack.Text = "< &Back"
        AddHandler Me.ButtonBack.Click, New EventHandler(AddressOf Me.ButtonBack_Click)
        Me.buttonNext.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.buttonNext.FlatStyle = FlatStyle.System
        Me.buttonNext.Location = New Point(264, 224)
        Me.buttonNext.Name = "buttonNext"
        Me.buttonNext.Size = New Size(75, 23)
        Me.buttonNext.TabIndex = 7
        Me.buttonNext.Text = "&Next >"
        AddHandler Me.buttonNext.Click, New EventHandler(AddressOf Me.ButtonNext_Click)
        Me.ButtonCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.ButtonCancel.DialogResult = DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = FlatStyle.System
        Me.ButtonCancel.Location = New Point(344, 224)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New Size(75, 23)
        Me.ButtonCancel.TabIndex = 8
        Me.ButtonCancel.Text = "Cancel"
        AddHandler Me.ButtonCancel.VisibleChanged, New EventHandler(AddressOf Me.ButtonCancel_VisibleChanged)
        AddHandler Me.ButtonCancel.Click, New EventHandler(AddressOf Me.ButtonCancel_Click)
        Controls.Add(Me.ButtonHelp)
        Controls.Add(Me.ButtonBack)
        Controls.Add(Me.buttonNext)
        Controls.Add(Me.ButtonCancel)
        Name = "Wizard"
        Size = New Size(428, 256)
        ResumeLayout(False)
    End Sub

    Protected Function ShouldSerializeHeaderFont() As Boolean
        Return Me.headerFontField IsNot Nothing
    End Function

    Protected Function ShouldSerializeHeaderTitleFont() As Boolean
        Return Me.headerTitleFontField IsNot Nothing
    End Function

    Protected Function ShouldSerializeWelcomeFont() As Boolean
        Return Me.welcomeFontField IsNot Nothing
    End Function

    Protected Function ShouldSerializeWelcomeTitleFont() As Boolean
        Return Me.welcomeTitleFontField IsNot Nothing
    End Function

    Public Sub [Next]()
        If Me.SelectedIndex = Me.pagesField.Count - 1 Then
            Me.buttonNext.Enabled = False
        Else
            Me.OnBeforeSwitchPages(New BeforeSwitchPagesEventArgs(Me.SelectedIndex, Me.SelectedIndex + 1))
        End If
    End Sub

    Public Sub Back()
        If Me.SelectedIndex = 0 Then
            Me.ButtonBack.Enabled = False
        Else
            Me.OnBeforeSwitchPages(New BeforeSwitchPagesEventArgs(Me.SelectedIndex, Me.SelectedIndex - 1))
        End If
    End Sub

    Private Sub ActivatePage(index As Integer)
        If index >= 0 AndAlso index < Me.pagesField.Count Then
            Dim page As WizardPage = Me.pagesField(index)
            Me.ActivatePage(page)
        End If
    End Sub

    Private Sub ActivatePage(page As WizardPage)
        If Not Me.pagesField.Contains(page) Then
            Return
        End If
        If Me.selectedPageField IsNot Nothing Then
            Me.selectedPageField.Visible = False
        End If
        Me.selectedPageField = page
        If Me.selectedPageField IsNot Nothing Then
            Me.selectedPageField.Parent = Me
            If Not MyBase.Contains(Me.selectedPageField) Then
                Container.Add(Me.selectedPageField)
            End If
            If Me.selectedPageField.Style = WizardPageStyle.Finish Then
                Me.ButtonCancel.Text = "OK"
                Me.ButtonCancel.DialogResult = DialogResult.OK
            Else
                Me.ButtonCancel.Text = "Cancel"
                Me.ButtonCancel.DialogResult = DialogResult.Cancel
            End If
            If Me.selectedPageField.Style = WizardPageStyle.Custom And Me.selectedPageField Is Me.pagesField(Me.pagesField.Count - 1) Then
                Me.ButtonCancel.Text = "OK"
                Me.ButtonCancel.DialogResult = DialogResult.OK
            End If
            Me.selectedPageField.SetBounds(0, 0, Width, Height - 48)
            Me.selectedPageField.Visible = True
            Me.selectedPageField.BringToFront()
            Me.FocusFirstTabIndex(Me.selectedPageField)
        End If
        If Me.SelectedIndex > 0 Then
            Me.ButtonBack.Enabled = True
        Else
            Me.ButtonBack.Enabled = False
        End If
        If Me.SelectedIndex < Me.pagesField.Count - 1 Then
            Me.buttonNext.Enabled = True
        Else
            If Not DesignMode Then
            End If
            Me.buttonNext.Enabled = False
        End If
        If Me.selectedPageField IsNot Nothing Then
            Me.selectedPageField.Invalidate()
        Else
            Invalidate()
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
            control.Focus()
        Else
            container.Focus()
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
            ParentForm.DialogResult = DialogResult.None
        Else
            ParentForm.Close()
        End If
    End Sub

    Protected Overridable Sub OnFinish(e As EventArgs)
        RaiseEvent Finish(Me, e)
        ParentForm.Close()
    End Sub

    Protected Overridable Sub OnHelp(e As EventArgs)
        RaiseEvent Help(Me, e)
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        If Me.pagesField.Count > 0 Then
            Me.ActivatePage(0)
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If Me.selectedPageField IsNot Nothing Then
            Me.selectedPageField.SetBounds(0, 0, Width, Height - 48)
        End If
        Me.ButtonHelp.Location = New Point(Me.ButtonHelp.Location.X, Height - Me.offsetBack.Y)
        Me.ButtonBack.Location = New Point(Width - Me.offsetBack.X, Height - Me.offsetBack.Y)
        Me.buttonNext.Location = New Point(Width - Me.offsetNext.X, Height - Me.offsetNext.Y)
        Me.ButtonCancel.Location = New Point(Width - Me.offsetCancel.X, Height - Me.offsetCancel.Y)
        MyBase.Refresh()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim clientRectangle = MyBase.ClientRectangle
        clientRectangle.Y = Height - 48
        clientRectangle.Height = 48
        ControlPaint.DrawBorder3D(e.Graphics, clientRectangle, Border3DStyle.Etched, Border3DSide.Top)
    End Sub

    Protected Overrides Sub OnControlAdded(e As ControlEventArgs)
        If Not (TypeOf e.Control Is WizardPage) AndAlso e.Control IsNot Me.ButtonCancel AndAlso e.Control IsNot Me.buttonNext AndAlso e.Control IsNot Me.ButtonBack Then
            If Me.selectedPageField IsNot Nothing Then
                Me.selectedPageField.Controls.Add(e.Control)
            End If
        Else
            MyBase.OnControlAdded(e)
        End If
    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs)
        Me.Next()
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs)
        Me.Back()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs)
        If Me.ButtonCancel.DialogResult = DialogResult.Cancel Then
            Me.OnCancel(New CancelEventArgs())
        ElseIf Me.ButtonCancel.DialogResult = DialogResult.OK Then
            Me.OnFinish(EventArgs.Empty)
        End If
    End Sub

    Private Sub ButtonCancel_VisibleChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs)
        Me.OnHelp(EventArgs.Empty)
    End Sub
End Class
