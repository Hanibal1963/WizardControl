' ****************************************************************************************************************
' PageDesigner.vb
' © 2024 by Andreas Sauer
' ****************************************************************************************************************
'

Imports System.Windows.Forms.Design

''' <summary>Designer für die Seiten des Controls</summary>
Friend Class PageDesigner

    Inherits ParentControlDesigner

    Public Overrides ReadOnly Property SelectionRules As SelectionRules
        Get
            Return SelectionRules.Locked Or SelectionRules.Visible
        End Get
    End Property

End Class
