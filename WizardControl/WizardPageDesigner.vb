
Imports System.Windows.Forms.Design

Friend Class WizardPageDesigner

    Inherits ParentControlDesigner

    Public Overrides ReadOnly Property SelectionRules As SelectionRules
        Get
            Return SelectionRules.Locked Or SelectionRules.Visible
        End Get
    End Property

End Class
