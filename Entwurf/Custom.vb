Imports System
Imports System.ComponentModel
Imports System.Drawing

<ToolboxItem(False)>
Public Class Custom

    Inherits WizardPage

    Private styleField As WizardPageStyle = WizardPageStyle.Custom

    <DefaultValue(WizardPageStyle.Custom)>
    <Category("Wizard")>
    <Description("Gets or sets the style of the wizard page.")>
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
