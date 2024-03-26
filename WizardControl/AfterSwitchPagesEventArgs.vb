' ****************************************************************************************************************
' AfterSwitchPagesEventArgs.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System

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
