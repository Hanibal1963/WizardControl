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



End Module
