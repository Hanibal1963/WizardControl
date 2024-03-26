
Imports System
Imports System.ComponentModel
Imports System.Drawing

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
