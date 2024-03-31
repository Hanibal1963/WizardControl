Imports SchlumpfSoft

Public Class Form1

    Private Sub Wizard1_Help(sender As Object, e As EventArgs) Handles _
        Wizard1.Help

        Dim index As Integer = Me.Wizard1.Pages.IndexOf(Me.Wizard1.SelectedPage) + 1

        Dim unused = MessageBox.Show(
            $"Dies ist die Hilfe für die Seite {index} des Assistenten.",
            $"Hilfe",
            MessageBoxButtons.OK,
            MessageBoxIcon.Question)

    End Sub

    Private Sub Wizard1_Finish(sender As Object, e As EventArgs) Handles _
        Wizard1.Finish

        Dim unused = MessageBox.Show(
            $"Sie haben den Assistente abgeschlossen.",
            $"Aktion abgeschlossen",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

    End Sub

    Private Sub Wizard1_AfterSwitchPages(sender As Object, e As WizardControl.AfterSwitchPagesEventArgs) Handles _
        Wizard1.AfterSwitchPages

        Dim unused = MessageBox.Show(
            $"neuer Index: {e.NewIndex} / alter Index: {e.OldIndex}",
            $"Nach dem Seitenwechsel",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

    End Sub

    Private Sub Wizard1_BeforeSwitchPages(sender As Object, e As WizardControl.BeforeSwitchPagesEventArgs) Handles _
        Wizard1.BeforeSwitchPages

        Dim unused = MessageBox.Show(
            $"neuer Index: {e.NewIndex} / alter Index: {e.OldIndex}",
            $"Vor dem Seitenwechsel",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

    End Sub

    Private Sub Wizard1_Cancel(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles _
        Wizard1.Cancel

        Dim unused = MessageBox.Show(
            $"Sie haben den Assistenten abgebrochen.",
            $"Aktion abgebrochen",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

    End Sub

End Class
