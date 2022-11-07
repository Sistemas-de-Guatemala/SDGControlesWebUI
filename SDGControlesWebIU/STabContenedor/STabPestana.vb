Imports System.Text
Imports System.Web.UI

''' <summary>
''' Este componente Renderiza controles de TabPanel (Pestañas)
''' </summary>
<ToolboxData("<{0}:STabPestana runat=server Titulo=stp_{1} />")>
Partial Public Class STabPestana
    Inherits WebControls.Panel
    Implements IPostBackEventHandler
    Public Sub New()
        Enabled = True
        ClientIDMode = ClientIDMode.Static
    End Sub

    ''' <summary>
    ''' Le dice al contenedor padre que numero de contenedor es
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property Indice As Integer

    ''' <summary>
    ''' Si esta propiedad es true entonces se mostrará este TabPanel
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property Seleccionado As Boolean
        Get
            Return IIf(ViewState("esta_seleccionado") <> Nothing, ViewState("esta_seleccionado"), False)
        End Get
        Set(value As Boolean)
            ViewState("esta_seleccionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el titulo que se mostrará en la pestaña
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Titulo As String

    ''' <summary>
    ''' Referencia del contenedor padre
    ''' </summary>
    ''' <returns>String</returns>
    Public Property IDContenedorPadre As String

    ' Declarar el evento click
    Public Delegate Sub _STabPresionado()
    ' Crear Evento
    Public Event STabPresionado As _STabPresionado

    Protected Overrides Sub OnInit(e As EventArgs)
        Page.RegisterRequiresRaiseEvent(Me)
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        RegistrarScript()
    End Sub

    Private Sub RenderizarEstilos(writer As HtmlTextWriter)
        writer.Write("<style type='text/css'>")
        writer.Write(" .stabpestana-" & ClientID & " {")
        writer.Write($"      grid-area: panel{ClientID} ;")
        writer.Write("  }")
        writer.Write("</style>")
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        RenderizarEstilos(writer)

        Dim funcion_click_pestana = ""
        If Enabled Then
            funcion_click_pestana = $"click_stabpestana_{ID}()"
        End If

        Dim clase_activo = "control-pestana-activo"
        If Not Seleccionado Then
            clase_activo = ""
        End If

        writer.Write($"<div onclick='{funcion_click_pestana}' class='control-pestana stabpestana-{ClientID} {clase_activo}'>{Titulo}</div>")

        Dim estaSeleccionado As String = ""
        If Seleccionado Then
            estaSeleccionado = "control-contenido-activo"
        End If

        writer.Write($"<div class='control-contenido {estaSeleccionado}'>")
    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
        writer.Write("</div>")
    End Sub

    ''' <summary>
    ''' Esta función inyecta el script que ejecuta los postbacks
    ''' </summary>
    Private Sub RegistrarScript()
        If (Page.ClientScript.IsStartupScriptRegistered(ClientID)) Then
            Return
        End If

        Dim str_script As New StringBuilder()
        str_script.AppendLine("function click_stabpestana_" & ID & "() {")
        str_script.AppendLine($"    __doPostBack('{UniqueID}', '{Indice}');")
        str_script.AppendLine("}")
        Page.ClientScript.RegisterStartupScript([GetType](), ClientID, str_script.ToString(), True)
    End Sub

    ''' <summary>
    ''' Envia el evento click
    ''' </summary>
    Private Sub GenerarEventoClick()
        RaiseEvent STabPresionado()
    End Sub

    ''' <summary>
    ''' Está función captura los postbacks del control
    ''' En el parametro de eventArgument viene los argumentos para controlar los diferentes tipos de postback
    ''' </summary>
    ''' <param name="eventArgument"></param>
    Public Sub RaisePostBackEvent(eventArgument As String) Implements IPostBackEventHandler.RaisePostBackEvent
        Dim padre As STabContenedor = Parent
        padre.IndiceActual = Page.Request("__EVENTARGUMENT")
        GenerarEventoClick()
    End Sub
End Class
