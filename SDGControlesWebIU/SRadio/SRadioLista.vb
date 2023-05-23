Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls
''' <summary>
''' Contenedor de radio buttons
''' </summary>
<ToolboxData("<{0}:SRadioLista ID=srl_ runat=server></{0}:SRadioLista>")>
Public Class SRadioLista
    Inherits RadioButtonList

    Sub New()
        EnableViewState = True
        RepeatLayout = RepeatLayout.Flow
        CssClass += " form-check"
    End Sub

    Private Enum EVALORES
        TITULO
        VALOR_SELECCIONADO
        HABILITADO
        ESTADOS_INTERNOS
    End Enum

    ''' <summary>
    ''' Este texto va a generar un label en el contenedor
    ''' </summary>
    ''' <returns></returns>
    <DefaultValue("SRadioLista")>
    <Category("Datos")>
    <Localizable(True)>
    Public Property Titulo As String
        Get
            Return IIf(ViewState("titulo_sradiolista") <> Nothing, ViewState("titulo_sradiolista"), "")
        End Get
        Set(value As String)
            ViewState("titulo_sradiolista") = value
        End Set
    End Property

    ''' <summary>
    ''' Valor actual de la lista de Radio botones
    ''' </summary>
    ''' <returns>String</returns>
    <DefaultValue("0")>
    <Category("Datos")>
    <Localizable(True)>
    Public Property ValorSeleccionado As String
        Get
            Return IIf(ViewState("valor_seleccionado") <> Nothing, ViewState("valor_seleccionado"), "")
        End Get
        Set(value As String)
            ViewState("valor_seleccionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Está variable si es falso inhabilita el control
    ''' </summary>
    ''' <returns>Boolean</returns>
    <DefaultValue("True")>
    <Category("Datos")>
    <Localizable(True)>
    Public Property Habilitado As Boolean
        Get
            Return IIf(ViewState("habilitado") <> Nothing, ViewState("habilitado"), True)
        End Get
        Set(value As Boolean)
            ViewState("habilitado") = value
        End Set
    End Property

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        Dim estaHabilitado = ""
        If Not Habilitado Then
            estaHabilitado = "disabled"
        End If

        writer.Write($"<fieldset {estaHabilitado} class='sradio'>")
        If (Titulo.Length > 0) Then
            writer.Write($"<legend>{Titulo}</legend>")
        End If

        MyBase.RenderControl(writer)

        writer.Write("</fieldset>")
    End Sub

End Class
