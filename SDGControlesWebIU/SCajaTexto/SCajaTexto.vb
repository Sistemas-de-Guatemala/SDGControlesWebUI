Imports System.Web.UI

<ToolboxData("<{0}:SCajaTexto ID='stxt_' runat=server />")>
Public Class SCajaTexto
    Inherits System.Web.UI.WebControls.TextBox

    Private Property AutoCompletado As New List(Of String)

    Sub New()
        CssClass += " fuente_aspx form-control"
        EnableViewState = True
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        If Not Enabled Then
            Attributes.Add("disabled", "")
        Else
            Attributes.Remove("disabled")
        End If
    End Sub

    ''' <summary>
    ''' Muestra un icono por defecto a la derecha en la caja de texto
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Icono As String
        Get
            Return IIf(ViewState("icono_txt") <> Nothing, ViewState("icono_txt"), "")
        End Get
        Set(value As String)
            ViewState("icono_txt") = value
        End Set
    End Property

    ''' <summary>
    ''' Este texto va a generar un label arriba del control
    ''' </summary>
    ''' <returns></returns>
    Public Property Titulo As String
        Get
            Return IIf(ViewState("titulo_txt") <> Nothing, ViewState("titulo_txt"), "")
        End Get
        Set(value As String)
            ViewState("titulo_txt") = value
        End Set
    End Property

    Private Sub RenderizarAutocompletado(writer As HtmlTextWriter)
        If AutoCompletado.Count > 0 Then
            writer.Write($"<datalist id='{UniqueID}'>")
            For Each palabra In AutoCompletado
                writer.Write($"<option value='{palabra.Replace("'", "´")}' />")
            Next
            writer.Write("</datalist>")
        End If
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        writer.Write("<div class='m-0 form-label'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label class='fuente_aspx form-label' for='{ClientID}'>{Titulo}</label>")
        End If

        If Icono.Length > 0 Then
            writer.Write("<div class='position-relative'>")

            writer.Write($"<i class='position-absolute ms-3 top-50 start-0 translate-middle-y {Icono}'></i>")

            If Not CssClass.Contains("ps-5") Then
                CssClass += " ps-5"
            End If

            MyBase.RenderBeginTag(writer)
            RenderizarAutocompletado(writer)
            writer.Write("</div>")
        Else
            MyBase.RenderBeginTag(writer)
            RenderizarAutocompletado(writer)
        End If
    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
        writer.Write("</div>")

        MyBase.RenderEndTag(writer)
    End Sub


    Public Sub FijarAutoCompletado(autocompletado As List(Of String))
        Me.AutoCompletado = autocompletado
        Attributes.Remove("list")
        Attributes.Add("list", UniqueID)
    End Sub
End Class
