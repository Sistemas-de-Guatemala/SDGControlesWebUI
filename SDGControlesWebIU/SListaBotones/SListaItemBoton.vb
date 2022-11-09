
Imports System.Web.UI

<ToolboxData("<{0}:SListaItemBoton ID='sibtn_' runat=server Titulo='Botón Item' />")>
Public Class SListaItemBoton
    Inherits System.Web.UI.WebControls.Button

    Sub New()
        CssClass += " dropdown-item"
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        writer.Write("<li>")
        MyBase.RenderBeginTag(writer)
        writer.Write("</li>")
    End Sub

End Class
