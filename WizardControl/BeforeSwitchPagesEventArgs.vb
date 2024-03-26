
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
