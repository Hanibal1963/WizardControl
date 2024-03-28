' ****************************************************************************************************************
' EventArgsDefinitions.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System

Public Class BeforeSwitchPagesEventArgs : Inherits AfterSwitchPagesEventArgs

    Private _Cancel As Boolean = False

    Public Property Cancel As Boolean
        Get
            Return Me._Cancel
        End Get
        Set(value As Boolean)
            Me._Cancel = value
        End Set
    End Property

    Public Overloads Property NewIndex As Integer
        Get
            Return Me._NewIndex
        End Get
        Set(value As Integer)
            Me._NewIndex = value
        End Set
    End Property

    Friend Sub New(OldIndex As Integer, NewIndex As Integer)
        MyBase.New(OldIndex, NewIndex)
    End Sub

End Class

Public Class AfterSwitchPagesEventArgs : Inherits EventArgs

    Protected _NewIndex As Integer
    Private ReadOnly _OldIndex As Integer


    Public ReadOnly Property OldIndex As Integer
        Get
            Return Me._OldIndex
        End Get
    End Property

    Public ReadOnly Property NewIndex As Integer
        Get
            Return Me._NewIndex
        End Get
    End Property

    Friend Sub New(OldIndex As Integer, NewIndex As Integer)
        Me._OldIndex = OldIndex
        Me._NewIndex = NewIndex
    End Sub

End Class

