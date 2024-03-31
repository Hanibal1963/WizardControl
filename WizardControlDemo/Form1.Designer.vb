<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Wizard1 = New SchlumpfSoft.WizardControl.Wizard()
        Me.PageFinish1 = New SchlumpfSoft.WizardControl.PageFinish()
        Me.PageCustom1 = New SchlumpfSoft.WizardControl.PageCustom()
        Me.PageStandard1 = New SchlumpfSoft.WizardControl.PageStandard()
        Me.PageWelcome1 = New SchlumpfSoft.WizardControl.PageWelcome()
        Me.Wizard1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Wizard1
        '
        Me.Wizard1.Controls.Add(Me.PageWelcome1)
        Me.Wizard1.Controls.Add(Me.PageFinish1)
        Me.Wizard1.Controls.Add(Me.PageCustom1)
        Me.Wizard1.Controls.Add(Me.PageStandard1)
        Me.Wizard1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.HeaderTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Wizard1.ImageHeader = CType(resources.GetObject("Wizard1.ImageHeader"), System.Drawing.Image)
        Me.Wizard1.ImageWelcome = CType(resources.GetObject("Wizard1.ImageWelcome"), System.Drawing.Image)
        Me.Wizard1.Location = New System.Drawing.Point(0, 0)
        Me.Wizard1.Name = "Wizard1"
        Me.Wizard1.Pages.AddRange(New SchlumpfSoft.WizardControl.WizardPage() {Me.PageWelcome1, Me.PageStandard1, Me.PageCustom1, Me.PageFinish1})
        Me.Wizard1.Size = New System.Drawing.Size(465, 296)
        Me.Wizard1.TabIndex = 0
        Me.Wizard1.VisibleHelp = False
        Me.Wizard1.WelcomeFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Wizard1.WelcomeTitleFont = New System.Drawing.Font("Microsoft Sans Serif", 18.25!, System.Drawing.FontStyle.Bold)
        '
        'PageFinish1
        '
        Me.PageFinish1.Location = New System.Drawing.Point(0, 0)
        Me.PageFinish1.Name = "PageFinish1"
        Me.PageFinish1.Size = New System.Drawing.Size(465, 248)
        Me.PageFinish1.TabIndex = 13
        '
        'PageCustom1
        '
        Me.PageCustom1.Location = New System.Drawing.Point(0, 0)
        Me.PageCustom1.Name = "PageCustom1"
        Me.PageCustom1.Size = New System.Drawing.Size(395, 190)
        Me.PageCustom1.TabIndex = 12
        '
        'PageStandard1
        '
        Me.PageStandard1.Location = New System.Drawing.Point(0, 0)
        Me.PageStandard1.Name = "PageStandard1"
        Me.PageStandard1.Size = New System.Drawing.Size(395, 190)
        Me.PageStandard1.TabIndex = 11
        '
        'PageWelcome1
        '
        Me.PageWelcome1.Location = New System.Drawing.Point(0, 0)
        Me.PageWelcome1.Name = "PageWelcome1"
        Me.PageWelcome1.Size = New System.Drawing.Size(465, 248)
        Me.PageWelcome1.TabIndex = 10
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 296)
        Me.Controls.Add(Me.Wizard1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Wizard1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents Wizard1 As SchlumpfSoft.WizardControl.Wizard
    Friend WithEvents PageFinish1 As SchlumpfSoft.WizardControl.PageFinish
    Friend WithEvents PageCustom1 As SchlumpfSoft.WizardControl.PageCustom
    Friend WithEvents PageStandard1 As SchlumpfSoft.WizardControl.PageStandard
    Friend WithEvents PageWelcome1 As SchlumpfSoft.WizardControl.PageWelcome
End Class
