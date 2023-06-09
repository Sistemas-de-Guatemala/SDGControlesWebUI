﻿Imports System.ComponentModel
Imports System.Web.UI

<ToolboxData("<{0}:SDesplegable ID='sddl_' runat=server></{0}:SDesplegable>")>
Public Class SDesplegable
    Inherits WebControls.DropDownList

    Private uuid As New Guid

    Sub New()
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
    ''' Este texto va a generar un label arriba del control
    ''' </summary>
    ''' <returns></returns>
    <DefaultValue("SDesplegable")>
    <Category("Datos")>
    <Localizable(True)>
    Public Property Titulo As String
        Get
            Return IIf(ViewState("titulo_desplegable") <> Nothing, ViewState("titulo_desplegable"), "")
        End Get
        Set(value As String)
            ViewState("titulo_desplegable") = value
        End Set
    End Property

    ''' <summary>
    ''' Está clase se renderizará en el contenedor del desplegable
    ''' </summary>
    ''' <returns>String</returns>
    <Category("Diseño")>
    Public Property CssClassContenedor As String
        Get
            Return IIf(ViewState("ddl_cssclasscontenedor_desplegable") <> Nothing, ViewState("ddl_cssclasscontenedor_desplegable"), "")
        End Get
        Set(value As String)
            ViewState("ddl_cssclasscontenedor_desplegable") = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad si es verdadera convierte el desplegable en un desplegable con filtro, de lo contrario será un simple desplegable
    ''' </summary>
    ''' <returns>Boolean</returns>
    <DefaultValue(False)>
    <Category("Datos")>
    <Localizable(True)>
    Public Property MostrarFiltro As Boolean
        Get
            Return IIf(ViewState("mostrar_filtro") <> Nothing, ViewState("mostrar_filtro"), False)
        End Get
        Set(value As Boolean)
            ViewState("mostrar_filtro") = value
        End Set
    End Property

    Private Sub RenderizarBeginTagSinFiltro(writer As HtmlTextWriter)
        writer.Write($"<div class='sdesplegable mt-1 {CssClassContenedor}'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label for='{ClientID}' class='form-label m-0'>{Titulo}</label>")
        End If

        If Not CssClass.Contains("form-select") Then
            CssClass &= " form-select"
        End If

        MyBase.RenderBeginTag(writer)

        writer.Write("</div>")
    End Sub

    Private Sub RenderizarBeginTagConFiltro(writer As HtmlTextWriter)
        writer.Write($"<div class='sdesplegable {CssClassContenedor}' id='{ClientID}'>")
        If Titulo.Length > 0 Then
            writer.Write($"<label for='{ClientID}' class='form-label m-0'>{Titulo}</label>")
        End If

        If Not CssClass.Contains("select2 w-100") Then
            CssClass &= $" select2 w-100"
        End If

        MyBase.RenderBeginTag(writer)

        writer.Write("</div>")
    End Sub

    Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
        If Not MostrarFiltro Then
            RenderizarBeginTagSinFiltro(writer)
        Else
            RenderizarBeginTagConFiltro(writer)
        End If
    End Sub

    Public Overrides Sub RenderEndTag(writer As HtmlTextWriter)
        MyBase.RenderEndTag(writer)
        writer.Write("</div>")
    End Sub

    ''' <summary>
    ''' Está función selecciona una opción, si no existe dentro del listado de opciones entonces no hace nada
    ''' </summary>
    ''' <param name="valor"></param>
    Public Sub AsignarValor(valor As String)
        If Items.FindByValue(valor.ToString()) IsNot Nothing Then
            SelectedValue = valor.ToString()
        End If
    End Sub

    ''' <summary>
    ''' Está función selecciona una opción, si no existe dentro del listado de opciones entonces no hace nada
    ''' </summary>
    ''' <param name="valor"></param>
    Public Sub FijarValor(valor As String)
        If Items.FindByValue(valor.ToString()) IsNot Nothing Then
            SelectedValue = valor.ToString()
        End If
    End Sub

End Class
