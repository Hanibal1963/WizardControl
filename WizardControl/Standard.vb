
Imports System
Imports System.ComponentModel
Imports System.Drawing

<ToolboxItem(False)>
Public Class Standard

    Inherits WizardPage

    Private styleField As WizardPageStyle = WizardPageStyle.Standard

    <DefaultValue(WizardPageStyle.Standard)>
    <Category("Wizard")>
    <Description("Ruft den Stil der Assistentenseite ab oder legt diesen fest.")>
    Public Overrides Property Style As WizardPageStyle
        Get
            Return Me.styleField
        End Get
        Set(value As WizardPageStyle)
            Me.styleField = value
        End Set
    End Property

    Private Sub InitializeComponent()
        MyBase.SuspendLayout()
        Me.BackColor = SystemColors.ControlDarkDark
        MyBase.ResumeLayout(False)
    End Sub

End Class
