
Imports System.ComponentModel
Imports System.Web.UI

''' <summary>
''' Renderiza un boton con estilos para bootstrap 5
''' </summary>
<ToolboxData("<{0}:SBoton ID='sbtn_' runat=server></{0}:SBoton>")>
Public Class SBoton
    Inherits WebControls.Button

    Sub New()
        EnableViewState = True
    End Sub

    ''' <summary>
    ''' Configura el botón para que tenga diferentes diseños de bootstrap
    ''' </summary>
    ''' <returns>ESBotonColores</returns>
    <Category("Diseño")>
    Public Property TipoBotonColor As ESBotonColores
        Get
            Return IIf(ViewState($"btn_{ID}_tipoBotonColor") <> Nothing, ViewState($"btn_{ID}_tipoBotonColor"), ESBotonColores.POR_DEFECTO)
        End Get
        Set(value As ESBotonColores)
            ViewState($"btn_{ID}_tipoBotonColor") = value
        End Set
    End Property

    ''' <summary>
    ''' Icono que se muestra a la par del texto
    ''' </summary>
    ''' <returns>String</returns>
    <Category("Diseño")>
    Public Property Icono As String
        Get
            Return IIf(ViewState($"btn_icono_{ID}") <> Nothing, ViewState($"btn_icono_{ID}"), "")
        End Get
        Set(value As String)
            ViewState($"btn_icono_{ID}") = value
        End Set
    End Property

    ''' <summary>
    ''' Toma el texto como centro y se posiciona a la izquierda o a la derecha del texto
    ''' </summary>
    ''' <returns>EPosicionIconos</returns>
    <Category("Diseño")>
    Public Property PosIcono As EPosicionIconos
        Get
            Return IIf(ViewState($"btn_posicono_{ID}") <> Nothing, ViewState($"btn_posicono_{ID}"), EPosicionIconos.IZQUIERDA)
        End Get
        Set(value As EPosicionIconos)
            ViewState($"btn_posicono_{ID}") = value
        End Set
    End Property

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        If Not Enabled Then
            Attributes.Add("disabled", "")
        Else
            Attributes.Remove("disabled")
        End If
    End Sub

    Public Overridable Sub ReescribirColorTextoConIcono(color As String)
        If Not ExisteEstilo("color") Then
            Style.Add("color", color)
        End If
    End Sub

    ''' <summary>
    ''' Está función obtiene un string con el tipo de boton
    ''' </summary>
    ''' <returns>String</returns>
    Public Overridable Function ObtenerTipoBotonColor() As String
        Dim tipo_boton As String = ""
        Select Case TipoBotonColor
            Case ESBotonColores.PRIMARIO
                tipo_boton = "btn-primary"
            Case ESBotonColores.PRIMARIO_SIN_RELLENO
                tipo_boton = "btn-outline-primary"
            Case ESBotonColores.SECUNDARIO
                tipo_boton = "btn-secondary"
            Case ESBotonColores.SECUNDARIO_SIN_RELLENO
                tipo_boton = "btn-outline-secondary"
            Case ESBotonColores.OK
                tipo_boton = "btn-success"
            Case ESBotonColores.OK_SIN_RELLENO
                tipo_boton = "btn-outline-success"
            Case ESBotonColores.PELIGRO
                tipo_boton = "btn-danger"
            Case ESBotonColores.PELIGRO_SIN_RELLENO
                tipo_boton = "btn-outline-danger"
            Case ESBotonColores.ADVERTENCIA
                tipo_boton = "btn-warning"
            Case ESBotonColores.ADVERTENCIA_SIN_RELLENO
                tipo_boton = "btn-outline-warning"
            Case ESBotonColores.INFO
                tipo_boton = "btn-info"
            Case ESBotonColores.INFO_SIN_RELLENO
                tipo_boton = "btn-outline-info"
            Case ESBotonColores.LUZ
                tipo_boton = "btn-light"
            Case ESBotonColores.LUZ_SIN_RELLENO
                tipo_boton = "btn-outline-light"
            Case ESBotonColores.OSCURO
                tipo_boton = "btn-dark"
            Case ESBotonColores.OSCURO_SIN_RELLENO
                tipo_boton = "btn-outline-dark"
            Case ESBotonColores.LINK
                tipo_boton = "btn-link"
            Case ESBotonColores.POR_DEFECTO
                tipo_boton = "btn-primary"
        End Select

        Return tipo_boton
    End Function

    ''' <summary>
    ''' Renderiza el botón con un icono
    ''' </summary>
    ''' <param name="writer"></param>
    Public Overridable Sub RenderizarConIcono(writer As HtmlTextWriter)

        If Not ExisteEstilo("background") Then
            Style.Add("background", "none")
            Style.Add("border", "none")
            Style.Add("outline", "none")
        End If

        writer.Write($"<div class='sboton btn {ObtenerTipoBotonColor()}'>")
        If PosIcono = EPosicionIconos.IZQUIERDA Then
            writer.Write($"{Icono} ")

            If Not CssClass.Contains(ObtenerTipoBotonColor()) Then
                CssClass &= $" {ObtenerTipoBotonColor()}"
            End If

            MyBase.RenderBeginTag(writer)
        Else
            MyBase.RenderBeginTag(writer)
            writer.Write($" {Icono}")
        End If
        writer.Write("</div>")
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        If Icono.Length > 0 Then
            RenderizarConIcono(writer)
        Else
            If Not CssClass.Contains("sboton") Then
                CssClass &= $" sboton"
            End If

            If Not CssClass.Contains("btn") Then
                CssClass &= $" btn {ObtenerTipoBotonColor()}"
            End If

            MyBase.RenderBeginTag(writer)
        End If
    End Sub

    ''' <summary>
    ''' Está función verifica si el estilo existe, si no existe devuelve falso
    ''' </summary>
    ''' <param name="estilo">por ejemplo background, border, etc</param>
    ''' <returns>Boolean</returns>
    Private Function ExisteEstilo(estilo As String) As Boolean
        Return Style(estilo) <> Nothing
    End Function
End Class
