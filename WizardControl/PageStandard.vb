﻿' ****************************************************************************************************************
' PageStandard.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System
Imports System.ComponentModel
Imports System.Drawing

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