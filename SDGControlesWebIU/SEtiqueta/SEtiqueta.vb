Imports System.Web.UI
Imports System.Web.UI.WebControls

''' <summary>
''' Este componente renderiza una etiqueta en pantalla para bootstrap 5
''' </summary>
<ToolboxData("<{0}:SEtiqueta ID='sta_' runat=server />")>
Public Class SEtiqueta
    Inherits Label

    Public Sub New()
        CssClass += " fuente_aspx form-control"
    End Sub

    ''' <summary>
    ''' Este titulo se renderizará en la parte superior del texto
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Titulo As String

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        writer.Write("<div class='m-0 form-label'>")
        If Titulo.Length > 0 Then
            writer.Write($"  <p class='fuente_aspx form-label'>{Titulo}</p>")
        End If
        MyBase.RenderControl(writer)
        writer.Write("</div>")
    End Sub

End Class
