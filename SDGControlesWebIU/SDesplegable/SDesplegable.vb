Imports System.Web.UI

<ToolboxData("<{0}:SDesplegable ID='sddl_' runat=server></{0}:SDesplegable>")>
Public Class SDesplegable
    Inherits System.Web.UI.WebControls.DropDownList

    Sub New()
        CssClass += " fuente_aspx form-control"
        EnableViewState = True
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        If Not Enabled Then
            Attributes.Add("disabled", "")
        End If
    End Sub

    ''' <summary>
    ''' Este texto va a generar un label arriba del control
    ''' </summary>
    ''' <returns></returns>
    Public Property Titulo As String
        Get
            Return IIf(ViewState("titulo_desplegable") <> Nothing, ViewState("titulo_desplegable"), "")
        End Get
        Set(value As String)
            ViewState("titulo_desplegable") = value
        End Set
    End Property
    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        writer.Write("<div class='m-0 form-label'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label class='fuente_aspx form-label' for='{ClientID}'>{Titulo}</label>")
        End If

        MyBase.RenderBeginTag(writer)
    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
        writer.Write("</div>")

        MyBase.RenderEndTag(writer)
    End Sub


End Class
