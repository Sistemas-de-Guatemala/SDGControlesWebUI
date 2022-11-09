
Imports System.Web.UI

''' <summary>
''' Renderiza un boton con estilos para bootstrap 5
''' </summary>
<ToolboxData("<{0}:SBoton ID='sbtn_' runat=server></{0}:SBoton>")>
Public Class SBoton
    Inherits System.Web.UI.WebControls.Button

    Sub New()
        CssClass += " btn"
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
        If Not CssClass.Contains(clase) Then
            CssClass &= " " & clase
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
        End If
    End Sub



End Class
