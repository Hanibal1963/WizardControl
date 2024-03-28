' ****************************************************************************************************************
' EventArgsDefinitions.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System

Public Class BeforeSwitchPagesEventArgs

    Inherits AfterSwitchPagesEventArgs

    Private cancelField As Boolean = False

    Public Property Cancel As Boolean
        Get
            Return Me.cancelField
        End Get
        Set(value As Boolean)
            Me.cancelField = value
        End Set
    End Property

    Public Overloads Property NewIndex As Integer
        Get
            Return Me.newIndexField
        End Get
        Set(value As Integer)
            Me.newIndexField = value
        End Set
    End Property

    Friend Sub New(oldIndex As Integer, newIndex As Integer)
        MyBase.New(oldIndex, newIndex)
    End Sub

End Class

Public Class AfterSwitchPagesEventArgs

    Inherits EventArgs

    Private ReadOnly oldIndexField As Integer

    Protected newIndexField As Integer

    Public ReadOnly Property OldIndex As Integer
        Get
            Return Me.oldIndexField
        End Get
    End Property

    Public ReadOnly Property NewIndex As Integer
        Get
            Return Me.newIndexField
        End Get
    End Property

    Friend Sub New(oldIndex As Integer, newIndex As Integer)
        Me.oldIndexField = oldIndex
        Me.newIndexField = newIndex
    End Sub

End Class

