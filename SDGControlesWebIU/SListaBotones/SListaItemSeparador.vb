Imports System.Web.UI

''' <summary>
''' Este control no genera ninguna acción solo dibuja una línea
''' </summary>
<ToolboxData("<{0}:SListaItemSeparador ID='sls_' runat=server />")>
Public Class SListaItemSeparador
    Inherits System.Web.UI.WebControls.WebControl

    Sub New()

    End Sub

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        writer.Write("<li><hr class='dropdown-divider'></li>")
    End Sub

End Class
