
Imports System.Web.UI

''' <summary>
''' Renderiza un boton con estilos para bootstrap 5
''' </summary>
<ToolboxData("<{0}:SBoton ID='sbtn_' runat=server></{0}:SBoton>")>
Public Class SBoton
    Inherits System.Web.UI.WebControls.Button

    Private _Icono As String
    Private _PosicionIcono As EPosicionIconos
    Private _ColorBotonConIcono As String

    Sub New()
        EnableViewState = True
    End Sub

    Public Property TipoBotonColor As ESBotonColores
        Get
            Return IIf(ViewState("tipo_boton_color") <> Nothing, ViewState("tipo_boton_color"), ESBotonColores.PRIMARIO)
        End Get
        Set(value As ESBotonColores)
            ViewState("tipo_boton_color") = value
        End Set
    End Property

    Private Sub VerificarSiYaExisteLaClase(clase As String)
        If Icono.Length > 0 Then
            _ColorBotonConIcono = clase
            Style.Add("background", "none")
            Style.Add("border", "none")
            Style.Add("outline", "none")
        Else
            If Not CssClass.Contains("btn") Then
                CssClass &= " btn"
            End If

            If Not CssClass.Contains(clase) Then
                CssClass &= " " & clase
            End If
        End If
    End Sub

    Private Sub RenderizarTipoBotonColor()
        Select Case TipoBotonColor
            Case ESBotonColores.PRIMARIO
                VerificarSiYaExisteLaClase("btn-primary")
            Case ESBotonColores.SECUNDARIO
                VerificarSiYaExisteLaClase("btn-secondary")
            Case ESBotonColores.OK
                VerificarSiYaExisteLaClase("btn-success")
            Case ESBotonColores.PELIGRO
                VerificarSiYaExisteLaClase("btn-danger")
            Case ESBotonColores.ADVERTENCIA
                VerificarSiYaExisteLaClase("btn-warning")
            Case ESBotonColores.INFO
                VerificarSiYaExisteLaClase("btn-info")
            Case ESBotonColores.LUZ
                VerificarSiYaExisteLaClase("btn-light")
            Case ESBotonColores.OSCURO
                VerificarSiYaExisteLaClase("btn-dark")
            Case ESBotonColores.LINK
                VerificarSiYaExisteLaClase("btn-link")
        End Select
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        RenderizarTipoBotonColor()
        If Not Enabled Then
            Attributes.Add("disabled", "")
        Else
            Attributes.Remove("disabled")
        End If
    End Sub

    ''' <summary>
    ''' El icono se mostrará a la izquierda por defecto del texto
    ''' </summary>
    ''' <returns></returns>
    Public Property Icono As String
        Get
            Return IIf(_Icono <> Nothing, _Icono, "")
        End Get
        Set(value As String)
            _Icono = value
        End Set
    End Property

    ''' <summary>
    ''' Posiciona el icono a la izquierda o a la derecha del texto, por defecto es a la Izquierda
    ''' </summary>
    ''' <returns></returns>
    Public Property PosicionIcono As EPosicionIconos
        Get
            Return IIf(_PosicionIcono <> Nothing, _PosicionIcono, EPosicionIconos.IZQUIERDA)
        End Get
        Set(value As EPosicionIconos)
            _PosicionIcono = value
        End Set
    End Property

    ''' <summary>
    ''' Cuando se llama RenderBeginTag e Icono.Length > 0 está función se ejecuta
    ''' </summary>
    ''' <param name="writer"></param>
    Public Overridable Sub RenderizarIcono(writer As HtmlTextWriter)
        writer.Write($"<div class='btn {_ColorBotonConIcono}'>")

        If PosicionIcono = EPosicionIconos.IZQUIERDA Then
            writer.Write(Icono)
            MyBase.RenderBeginTag(writer)
        Else
            MyBase.RenderBeginTag(writer)
            writer.Write(Icono)
        End If
        writer.Write("</div>")
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        If Icono.Length > 0 Then
            RenderizarIcono(writer)
        Else
            MyBase.RenderBeginTag(writer)
        End If
    End Sub

End Class
