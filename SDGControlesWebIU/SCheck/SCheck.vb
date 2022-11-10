
Imports System.Web.UI
''' <summary>
''' Control de tipo CheckBox
''' </summary>
<ToolboxData("<{0}:SCheck ID=schk_ runat=server />")>
Public Class SCheck
    Inherits System.Web.UI.WebControls.CheckBox

    Sub New()
        CssClass += " scheck form-check"
        InputAttributes.Add("class", "scheck-input form-check-input")
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
