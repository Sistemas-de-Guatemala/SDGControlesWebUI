
Imports System.Web.UI
''' <summary>
''' Este control posee la misma funcionalidad que un CheckBox, solo cambia el diseño
''' </summary>
<ToolboxData("<{0}:SSwitch ID=swt_ runat=server />")>
Public Class SSwitch
    Inherits System.Web.UI.WebControls.CheckBox

    Sub New()
        CssClass += " sswitch form-check form-switch"
        InputAttributes.Add("class", "form-check-input")
        InputAttributes.Add("role", "switch")
        LabelAttributes.Add("class", "form-check-label")
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        If Not Enabled Then
            InputAttributes.Add("disabled", "")
        Else
            InputAttributes.Remove("disabled")
        End If
    End Sub

End Class
