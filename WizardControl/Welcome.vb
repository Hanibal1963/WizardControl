﻿
Imports System
Imports System.ComponentModel
Imports System.Drawing

<ToolboxItem(False)>
Public Class Welcome

    Inherits WizardPage

    Private styleField As WizardPageStyle = WizardPageStyle.Welcome

    <DefaultValue(WizardPageStyle.Welcome)>
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
        Me.Style = WizardPageStyle.Welcome
        MyBase.ResumeLayout(False)
    End Sub

End Class
