' ****************************************************************************************************************
' PagesCollection.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System.Collections

Public Class PagesCollection

    Inherits CollectionBase

    Private owner As Wizard = Nothing

    Default Public Property Item(index As Integer) As WizardPage
        Get
            Return CType(List(index), WizardPage)
        End Get
        Set(value As WizardPage)
            List(index) = value
        End Set
    End Property

    Friend Sub New(owner As Wizard)
        Me.owner = owner
    End Sub

    Public Function Add(value As WizardPage) As Integer
        Return List.Add(value)
    End Function

    Public Sub AddRange(pages As WizardPage())
        For Each value As WizardPage In pages
            Me.Add(value)
        Next
    End Sub

    Public Function IndexOf(value As WizardPage) As Integer
        Return List.IndexOf(value)
    End Function

    Public Sub Insert(index As Integer, value As WizardPage)
        List.Insert(index, value)
    End Sub

    Public Sub Remove(value As WizardPage)
        List.Remove(value)
    End Sub

    Public Function Contains(value As WizardPage) As Boolean
        Return List.Contains(value)
    End Function

    Protected Overrides Sub OnInsertComplete(index As Integer, value As Object)
        MyBase.OnInsertComplete(index, value)
        Me.owner.SelectedIndex = index
    End Sub

    Protected Overrides Sub OnRemoveComplete(index As Integer, value As Object)
        MyBase.OnRemoveComplete(index, value)
        If Me.owner.SelectedIndex = index Then
            If index < InnerList.Count Then
                Me.owner.SelectedIndex = index
            Else
                Me.owner.SelectedIndex = InnerList.Count - 1
            End If
        End If
    End Sub

End Class
