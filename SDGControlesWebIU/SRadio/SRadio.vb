Imports System.Web.UI
''' <summary>
''' Componente de Radio botón personalizado
''' </summary>
<ToolboxData("<{0}:SRadio ID=srd_ runat=server />")>
Public Class SRadio
    Inherits System.Web.UI.WebControls.RadioButton

    Sub New()
        InputAttributes.Add("class", "form-check-input")
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

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        writer.Write("<div class='form-check'>")
        MyBase.RenderControl(writer)
        writer.Write("</div>")
    End Sub

End Class
