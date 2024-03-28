' ****************************************************************************************************************
' VariableDefinitions.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System.Drawing

Module VariableDefinitions

    ''' <summary>speichert das Bild für den Headerbereich</summary>
    Friend _ImageHeader As Image = My.Resources.HeaderImage

    ''' <summary>speichert das Bild für den linken Bereich der Willkommens und der Abschlußseite</summary>
    Friend _ImageWelcome As Image = My.Resources.WelcomeImage

    ''' <summary>speichert die Schriftart für die Beschreibung der Willkommens und Abschlußseite</summary>
    Friend _WelcomeFont As Font = Nothing

    ''' <summary>speichert die Schriftart für den Titel der Willkommens und Abschlußseite</summary>
    Friend _WelcomeTitleFont As Font = Nothing

    ''' <summary>speichert die Schriftart für die Beschreibung einer Standardeite</summary>
    Friend _HeaderFont As Font = Nothing

    ''' <summary>speichert die Schriftart für den Titel einer Standardeite</summary>
    Friend _HeaderTitleFont As Font = Nothing

    ''' <summary>speichert die aktuelle Seite</summary>
    Friend _SelectedPage As WizardPage = Nothing

    ''' <summary>speichert die Seiten des Assistenten</summary>
    Friend _Pages As PagesCollection = Nothing


    Friend ReadOnly _OffsetCancel As New Point(84, 36)

    Friend ReadOnly _OffsetNext As New Point(164, 36)

    Friend ReadOnly _OffsetBack As New Point(244, 36)

End Module
