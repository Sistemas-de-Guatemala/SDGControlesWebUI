Imports System.ComponentModel
Imports System.Web.UI
''' <summary>
''' Contenedor de radio buttons
''' </summary>
<ToolboxData("<{0}:SRadioLista ID=srl_ runat=server></{0}:SRadioLista>")>
Public Class SRadioLista
    Inherits System.Web.UI.WebControls.RadioButtonList

    Sub New()
        EnableViewState = True
    End Sub

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

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        Dim estaHabilitado = ""
        If Not Enabled Then
            estaHabilitado = "disabled"
        End If

        writer.Write($"<fieldset {estaHabilitado}>")
        If (Titulo.Length > 0) Then
            writer.Write($"<legend>{Titulo}</legend>")
        End If
    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
        writer.Write("</fieldset>")
    End Sub
End Class
