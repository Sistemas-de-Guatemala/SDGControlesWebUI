Imports System.Web.UI
Imports System.Globalization
Imports System.ComponentModel

<ToolboxData("<{0}:SCajaTexto ID='stxt_' runat=server />")>
Public Class SCajaTexto
    Inherits WebControls.TextBox

    Private _SimboloMoneda As String
    Private Autocompletado As New List(Of String)
    Private uuid As New Guid
    Sub New()
        EnableViewState = True
        _SimboloMoneda = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol
        CssClass += " form-control"
    End Sub

    ''' <summary>
    ''' Esta propiedad renderiza un estilo en el CSS del contenedor del input que es un div
    ''' </summary>
    ''' <returns></returns>
    <Category("Diseño")>
    Public Property CssClassContenedor As String
        Get
            Return IIf(ViewState($"txt_cssclasscontenedor_{uuid}") <> Nothing, ViewState($"txt_cssclasscontenedor_{uuid}"), "")
        End Get
        Set(value As String)
            ViewState($"txt_cssclasscontenedor_{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Estilos del icono de la izquierda
    ''' </summary>
    ''' <returns>String</returns>
    <Category("Diseño")>
    Public Property CssClassIconoIzquierda As String
        Get
            Return IIf(ViewState($"txt_cssclassiconoizquierda_{uuid}") <> Nothing, ViewState($"txt_cssclassiconoizquierda_{uuid}"), "")
        End Get
        Set(value As String)
            ViewState($"txt_cssclassiconoizquierda_{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Estilos del icono de la derecha
    ''' </summary>
    ''' <returns>String</returns>
    <Category("Diseño")>
    Public Property CssClassIconoDerecha As String
        Get
            Return IIf(ViewState($"txt_cssclassiconoderecha_{uuid}") <> Nothing, ViewState($"txt_cssclassiconoderecha_{uuid}"), "")
        End Get
        Set(value As String)
            ViewState($"txt_cssclassiconoderecha_{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el simbolo de la moneda
    ''' </summary>
    ''' <returns></returns>
    Public Function ObtenerSimboloMoneda() As String
        Return IIf(SimboloMoneda.Length > 0, SimboloMoneda, _SimboloMoneda)
    End Function

    ''' <summary>
    ''' Muestra el simbolo de la moneda y si es falso no lo muestra
    ''' </summary>
    ''' <returns></returns>
    <Category("Diseño")>
    Public Property MostrarSimboloMoneda As Boolean
        Get
            Return IIf(ViewState($"txt_mostrarsimbolomoneda_{uuid}") <> Nothing, ViewState($"txt_mostrarsimbolomoneda_{uuid}"), False)
        End Get
        Set(value As Boolean)
            ViewState($"txt_mostrarsimbolomoneda_{uuid}") = value
        End Set
    End Property

    <Category("Diseño")>
    Public Property SimboloMoneda As String
        Get
            Return IIf(ViewState($"txt_simbolo_moneda_{uuid}") <> Nothing, ViewState($"txt_simbolo_moneda_{uuid}"), "")
        End Get
        Set(value As String)
            ViewState($"txt_simbolo_moneda_{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Coloca un icono a la izquierda del input
    ''' </summary>
    ''' <returns>String</returns>
    <Category("Diseño")>
    Public Property IconoIzquierda As String
        Get
            Return IIf(ViewState($"txt_iconoizquierda{uuid}") <> Nothing, ViewState($"txt_iconoizquierda{uuid}"), "")
        End Get
        Set(value As String)
            ViewState($"txt_iconoizquierda{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Coloca un icono a la derecha del input
    ''' </summary>
    ''' <returns>String</returns>
    <Category("Diseño")>
    Public Property IconoDerecha As String
        Get
            Return IIf(ViewState($"txt_iconoderecha_{uuid}") <> Nothing, ViewState($"txt_iconoderecha_{uuid}"), "")
        End Get
        Set(value As String)
            ViewState($"txt_iconoderecha_{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Pone un titulo sobre el campo de texto
    ''' </summary>
    ''' <returns>String</returns>
    <Category("Diseño")>
    Public Property Titulo As String
        Get
            Return IIf(ViewState($"txt_titulo_{uuid}") <> Nothing, ViewState($"txt_titulo_{uuid}"), "")
        End Get
        Set(value As String)
            ViewState($"txt_titulo_{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si el input es requerido  no
    ''' </summary>
    ''' <returns>Boolean</returns>
    <Category("Comportamiento")>
    Public Property Requerido As Boolean
        Get
            Return IIf(ViewState($"txt_requerido_{uuid}") <> Nothing, ViewState($"txt_requerido_{uuid}"), False)
        End Get
        Set(value As Boolean)
            ViewState($"txt_requerido_{uuid}") = value
        End Set
    End Property

    ''' <summary>
    ''' Está función configura todos los atributos del input durante la renderización del objeto, si hay que añadir atributos los añade, de lo contrario no hace nada
    ''' </summary>
    Public Overridable Sub ConfiguracionDeAtributos()
        If Requerido Then
            If Not ExisteAtributo("required") Then
                Attributes.Add("required", "true")
            End If
        Else
            If ExisteAtributo("required") Then
                Attributes.Remove("required")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Renderiza una lista para autocompletado en el input
    ''' </summary>
    ''' <param name="writer"></param>
    Public Overridable Sub RenderizarAutoCompletado(writer As HtmlTextWriter)
        If Autocompletado.Count > 0 Then

            If Attributes("list") = Nothing Then
                Attributes.Add("list", $"txt_autocompletado_{uuid}")
            End If

            writer.Write($"<datalist id='txt_autocompletado_{uuid}'>")
            For Each palabra In Autocompletado
                writer.Write($"<option value='{palabra.Replace("'", "´")}' />")
            Next
            writer.Write("</datalist>")
        End If
    End Sub

    ''' <summary>
    ''' Renderiza el input con iconos o simbolo de moneda
    ''' </summary>
    ''' <param name="writer"></param>
    Public Overridable Sub RenderizarControlConIconosOMoneda(writer As HtmlTextWriter)
        writer.Write($"<div class='input-group {CssClassContenedor}'>")

        If MostrarSimboloMoneda Then
            ' Renderizar Moneda
            writer.Write($"<span class='input-group-text {CssClassIconoIzquierda}'>{IIf(SimboloMoneda.Length > 0, SimboloMoneda, _SimboloMoneda)}</span>")
        End If

        If Not MostrarSimboloMoneda And IconoIzquierda.Length > 0 Then
            ' Renderizar el icono de la izquierda si el simbolo de moneda no está activo
            writer.Write($"<span class='input-group-text {CssClassIconoIzquierda}'>{IconoIzquierda}</span>")
        End If

        If Not CssClass.Contains("form-control") Then
            CssClass &= " form-control"
        End If

        ' Renderizar Input
        MyBase.RenderBeginTag(writer)

        If IconoDerecha.Length > 0 Then
            ' Renderziar el icono de la derecha
            writer.Write($"<span class='input-group-text {CssClassIconoDerecha}'>{IconoDerecha}</span>")
        End If
        writer.Write("</div>")
    End Sub

    ''' <summary>
    ''' Está función renderiza el input normalmente si ninguna alteración
    ''' </summary>
    ''' <param name="writer"></param>
    Public Overridable Sub RenderizacionNormal(writer As HtmlTextWriter)
        If Not CssClass.Contains("form-control") Then
            CssClass &= " form-control"
        End If

        MyBase.RenderBeginTag(writer)
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        writer.Write($"<div class='scajatexto mt-1 {CssClassContenedor}'>")

        If Titulo.Length > 0 Then
            writer.Write($"<label for='{ClientID}'>{Titulo}</label>")
        End If

        ' Configuración de atributos
        ConfiguracionDeAtributos()

        If MostrarSimboloMoneda Or (IconoDerecha.Length > 0) Or (IconoIzquierda.Length > 0) Then
            RenderizarControlConIconosOMoneda(writer)
        Else
            RenderizacionNormal(writer)
        End If

        RenderizarAutoCompletado(writer)
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

    ''' <summary>
    ''' Está función verifica si el atributo existe, si no existe devuelve falso
    ''' </summary>
    ''' <param name="atributo">por ejemplo class, required, list, etc</param>
    ''' <returns>Boolean</returns>
    Private Function ExisteAtributo(atributo As String) As Boolean
        Return Attributes(atributo) <> Nothing
    End Function
End Class
