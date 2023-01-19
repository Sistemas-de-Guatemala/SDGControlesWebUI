Imports System.Web.UI
Imports System.Globalization

<ToolboxData("<{0}:SCajaTexto ID='stxt_' runat=server />")>
Public Class SCajaTexto
    Inherits System.Web.UI.WebControls.TextBox

    Private _SimboloMoneda As String
    Private Property AutoCompletado As New List(Of String)

    Sub New()
        EnableViewState = True
        _SimboloMoneda = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol
    End Sub

    Private Function ObtenerSimboloMoneda() As String
        If SimboloMoneda.Length > 0 Then
            Return SimboloMoneda
        Else
            Return _SimboloMoneda
        End If
    End Function

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        If Not Enabled Then
            Attributes.Add("disabled", "")
        Else
            Attributes.Remove("disabled")
        End If
    End Sub

    ''' <summary>
    ''' Si la variable esta es True entonces se mostrara la moneda local, Si se desea mostrar un simbolo personalizado usar la propiedad SimboloMoneda
    ''' </summary>
    ''' <returns></returns>
    Public Property MostrarMonedaLocal As Boolean
        Get
            Return IIf(ViewState("txt_mostrar_moneda_local") <> Nothing, ViewState("txt_mostrar_moneda_local"), False)
        End Get
        Set(value As Boolean)
            ViewState("txt_mostrar_moneda_local") = value
        End Set
    End Property

    ''' <summary>
    ''' Muestra un simbolo de moneda personalizado a la par del Input
    ''' </summary>
    ''' <returns></returns>
    Public Property SimboloMoneda As String
        Get
            Return IIf(ViewState("txt_simbolo_moneda") <> Nothing, ViewState("txt_simbolo_moneda"), "")
        End Get
        Set(value As String)
            ViewState("txt_simbolo_moneda") = value
        End Set
    End Property

    ''' <summary>
    ''' Muestra un icono por defecto a la derecha en la caja de texto
    ''' Se le tiene que pasar el Icono completo de Bootstrap por ejemplo <i class='bi bi-arrow-bar-up'></i>
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
    
    ''' <summary>
    ''' Si el campo es obligatorio se agrega una clase css para cambiar el borde del control
    ''' </summary>
    ''' <returns></returns>
    Public Property Obligatorio As Boolean
        Get
            Return IIf(ViewState("obligatorio_txt") <> Nothing, ViewState("obligatorio_txt"), False)
        End Get
        Set(value As Boolean)
            ViewState("obligatorio_txt") = value
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

    Private Sub RenderizarObligatoriedad()
        If Obligatorio And Not CssClass.Contains("input_obligatorio") Then
            CssClass += " input_obligatorio"
        End If
    End Sub

    ''' <summary>
    ''' Esta función Renderiza el control normalmente, se le pude incluir un icono con autocompletado
    ''' el icono siempre quedara a la izquierda
    ''' </summary>
    ''' <param name="writer"></param>
    Public Overridable Sub RenderNormalControl(writer As HtmlTextWriter)

        If Not CssClass.Contains("fuente_aspx form-control") Then
            CssClass += " fuente_aspx form-control"
        End If

        writer.Write("<div class='m-0 form-label'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label class='fuente_aspx form-label' for='{ClientID}'>{Titulo}</label>")
        End If

        MyBase.RenderBeginTag(writer)
        RenderizarAutocompletado(writer)
    End Sub

    Public Overridable Sub RenderizarConIcono(writer As HtmlTextWriter)
        writer.Write("<div class='m-0 form-label'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label class='fuente_aspx form-label' for='{ClientID}'>{Titulo}</label>")
        End If

        If Not CssClass.Contains("form-control py-2 border-left-0 border") Then
            CssClass &= " form-control py-2 border-left-0 border"
        End If

        writer.Write("<div class='input-group'>")
        writer.Write($"<div class='input-group-text bg-transparent border-right-0'>{Icono}</div>")
        MyBase.RenderBeginTag(writer)
        RenderizarAutocompletado(writer)
        writer.Write("</div>")
    End Sub

    Public Overridable Sub RenderizarConMoneda(writer As HtmlTextWriter)
        writer.Write("<div class='m-0 form-label'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label class='fuente_aspx form-label' for='{ClientID}'>{Titulo}</label>")
        End If

        If Not CssClass.Contains("form-control py-2 border-left-0 border") Then
            CssClass &= " form-control py-2 border-left-0 border"
        End If

        writer.Write("<div class='input-group'>")
        writer.Write($"<div class='input-group-text bg-transparent border-right-0'>{ObtenerSimboloMoneda()}.</div>")
        MyBase.RenderBeginTag(writer)
        RenderizarAutocompletado(writer)
        writer.Write("</div>")
    End Sub

    ''' <summary>
    ''' Esta función renderiza un Icono a la derecha con el simbolo de moneda a la izquierda
    ''' también puede incluir un autocompletado
    ''' </summary>
    ''' <param name="writer"></param>
    Public Overridable Sub RenderizarIconoConMoneda(writer As HtmlTextWriter)
        writer.Write("<div class='m-0 form-label'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label class='fuente_aspx form-label' for='{ClientID}'>{Titulo}</label>")
        End If

        If Not CssClass.Contains("form-control py-2 border-left-0 border") Then
            CssClass &= " form-control py-2 border-left-0 border"
        End If

        writer.Write("<div class='input-group'>")
        writer.Write($"<div class='input-group-text bg-transparent border-right-0'>{ObtenerSimboloMoneda()}.</div>")
        MyBase.RenderBeginTag(writer)
        RenderizarAutocompletado(writer)
        writer.Write($"<div class='input-group-text bg-transparent border-left-0'>{Icono}</div>")
        writer.Write("</div>")

    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        RenderizarObligatoriedad()

        If (Icono.Length > 0) And (MostrarMonedaLocal) Then
            RenderizarIconoConMoneda(writer)
        ElseIf (Icono.Length > 0) Then
            RenderizarConIcono(writer)
        ElseIf (MostrarMonedaLocal) Then
            RenderizarConMoneda(writer)
        Else
            RenderNormalControl(writer)
        End If

    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
        If TextMode <> WebControls.TextBoxMode.MultiLine Then
            writer.Write("</div>")
            MyBase.RenderEndTag(writer)
        Else
            MyBase.RenderEndTag(writer)
            writer.Write("</div>")
        End If
    End Sub


    Public Sub FijarAutoCompletado(autocompletado As List(Of String))
        Me.AutoCompletado = autocompletado
        Attributes.Remove("list")
        Attributes.Add("list", UniqueID)
    End Sub
End Class
