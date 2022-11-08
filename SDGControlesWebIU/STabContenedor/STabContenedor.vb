Imports System.Web.UI
''' <summary>
''' Es el contenedor de las pestañas
''' </summary>
<ToolboxData("<{0}:STabContenedor ID='stc_' runat=server></{0}:STabContenedor>")>
Public Class STabContenedor
    Inherits System.Web.UI.WebControls.Panel

    Sub New()
        EnableViewState = True
        ClientIDMode = ClientIDMode.Static
    End Sub

    ''' <summary>
    ''' Pestaña seleccionada a mostrar
    ''' </summary>
    ''' <returns>Integer</returns>
    <PersistenceMode(PersistenceMode.Attribute)>
    Public Property IndiceActual As Integer
        Get
            Return IIf(ViewState("indice_actual") <> Nothing, ViewState("indice_actual"), 0)
        End Get
        Set(value As Integer)
            ViewState("indice_actual") = value
        End Set
    End Property

    ''' <summary>
    ''' Esta funcion se renderiza despues de que todos los estados fueron cargados
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        Dim lista_pestanas = ObtenerControles(Of STabPestana)()
        For i = 0 To lista_pestanas.Count - 1
            lista_pestanas(i).Indice = i
            If i = IndiceActual Then
                lista_pestanas(i).Seleccionado = True
            Else
                lista_pestanas(i).Seleccionado = False
            End If

        Next
    End Sub

    Private Sub RenderizarEstilosTabPanel(writer As HtmlTextWriter)
        Dim lista_pestanas = ObtenerControles(Of STabPestana)()

        writer.Write("<style type='text/css'>")
        writer.Write("  .stabcontenedor-" & ClientID & " {")
        writer.Write($"      grid-template-columns: repeat({lista_pestanas.Count}, min-content) 1fr;")

        Dim paneles As String = "'"
        ' Renderizar grid template areas
        For i = 0 To lista_pestanas.Count - 1
            paneles &= "panel" & lista_pestanas(i).ClientID & " "
        Next
        paneles &= " panelaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' ' "
        ' Rendereziar grid template contenidos
        For i = 0 To lista_pestanas.Count - 1
            paneles &= "contenido "
        Next
        paneles &= " contenido';"

        writer.Write($"      grid-template-areas: {paneles}")
        writer.Write("  }")
        writer.Write("</style>")

    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        RenderizarEstilosTabPanel(writer)
        writer.Write($"<div class='control-contenedor-paneles stabcontenedor-{ClientID}' name='{UniqueID}' id='{UniqueID}'>")
    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
        writer.Write("</div>")
    End Sub
End Class
