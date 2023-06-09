Imports System.Web.UI
Imports System.Web.UI.WebControls

''' <summary>
''' Este componente renderiza una etiqueta en pantalla para bootstrap 5
''' </summary>
<ToolboxData("<{0}:SEtiqueta ID='sta_' runat=server />")>
Public Class SEtiqueta
    Inherits Label

    Public Sub New()
        CssClass += " form-control"
    End Sub

    ''' <summary>
    ''' Este titulo se renderizará en la parte superior del texto
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Titulo As String

    Public Property CssClassContenedor As String
        Get
            Return IIf(ViewState("lbd_cssclasscontenedor_etiqueta") <> Nothing, ViewState("lbd_cssclasscontenedor_etiqueta"), "")
        End Get
        Set(value As String)
            ViewState("lbd_cssclasscontenedor_etiqueta") = value
        End Set
    End Property

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        writer.Write($"<div class='setiqueta mt-1 form-label w-100 {CssClassContenedor}'>")
        If Titulo.Length > 0 Then
            writer.Write($"  <label class='form-label'>{Titulo}</label>")
        End If
        MyBase.RenderControl(writer)
        writer.Write("</div>")
    End Sub

End Class
