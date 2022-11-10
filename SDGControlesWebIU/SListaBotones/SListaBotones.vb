Imports System.Web.UI

''' <summary>
''' Este control agrupa botones llamados SListaItemBoton
''' </summary>
<ToolboxData("<{0}:SListaBotones ID='slb_' runat=server></{0}:SListaBotones>")>
Public Class SListaBotones
    Inherits System.Web.UI.WebControls.Panel

    Sub New()
    End Sub

    ''' <summary>
    ''' Este texto va a generar un label dentro del botón
    ''' </summary>
    ''' <returns></returns>
    Public Property Titulo As String
        Get
            Return IIf(ViewState("titulo_boton") <> Nothing, ViewState("titulo_boton"), "Botón")
        End Get
        Set(value As String)
            ViewState("titulo_boton") = value
        End Set
    End Property

    Public Property TipoBotonColor As ESBotonColores
        Get
            Return IIf(ViewState("tipo_boton_color") <> Nothing, ViewState("tipo_boton_color"), ESBotonColores.PRIMARIO)
        End Get
        Set(value As ESBotonColores)
            ViewState("tipo_boton_color") = value
        End Set
    End Property

    Private Sub RenderizarTipoBotonColor(ByRef btn_color As String)
        Select Case TipoBotonColor
            Case ESBotonColores.PRIMARIO
                btn_color = "btn-primary"
            Case ESBotonColores.SECUNDARIO
                btn_color = "btn-secondary"
            Case ESBotonColores.OK
                btn_color = "btn-success"
            Case ESBotonColores.PELIGRO
                btn_color = "btn-danger"
            Case ESBotonColores.ADVERTENCIA
                btn_color = "btn-warning"
            Case ESBotonColores.INFO
                btn_color = "btn-info"
            Case ESBotonColores.LUZ
                btn_color = "btn-light"
            Case ESBotonColores.OSCURO
                btn_color = "btn-dark"
            Case ESBotonColores.LINK
                btn_color = "btn-link"
        End Select
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        If Not Enabled Then
            Attributes.Add("disabled", "")
        Else
            Attributes.Remove("disabled")
        End If
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
    End Sub

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        writer.Write($"<div id='{ID}' class='btn-group' role='group'>")

        Dim btn_color As String = "btn-primary"
        RenderizarTipoBotonColor(btn_color)

        writer.Write($"<button id='{ID}' name='{UniqueID}' type='button' class='btn {btn_color} dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'>{Titulo}</button>")
        writer.Write($"<ul class='dropdown-menu' aria-labelledby='btnGroupDrop{ID}'>")
        MyBase.RenderControl(writer)
        writer.Write("</ul>")
        writer.Write("</div>")
    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
    End Sub

End Class
