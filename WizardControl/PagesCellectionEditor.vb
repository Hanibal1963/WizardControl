' ****************************************************************************************************************
' PagesCellectionEditor.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System
Imports System.ComponentModel.Design

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
