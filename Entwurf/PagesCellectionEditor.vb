Imports System
Imports System.ComponentModel.Design

Friend Class PagesCellectionEditor
    Inherits CollectionEditor
    Private Types As Type()

    Public Sub New(type As Type)
        MyBase.New(type)
        Me.Types = New Type(3) {GetType(Welcome), GetType(Standard), GetType(Custom), GetType(Finish)}
    End Sub

    Protected Overrides Function CreateNewItemTypes() As Type()
        Return Me.Types
    End Function
End Class
