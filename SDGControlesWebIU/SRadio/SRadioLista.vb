Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls
''' <summary>
''' Contenedor de radio buttons
''' </summary>
<ParseChildren(True)>
<PersistChildren(False)>
<ValidationProperty("ValorSeleccionado")>
<ToolboxData("<{0}:SRadioLista ID=srl_ runat=server></{0}:SRadioLista>")>
Public Class SRadioLista
    Inherits Control
    Implements IPostBackEventHandler

    Sub New()
        EnableViewState = True
    End Sub


    <PersistenceMode(PersistenceMode.InnerProperty)>
    <TemplateContainer(GetType(SRadio))>
    <TemplateInstance(TemplateInstance.Single)>
    Public Property SRadios As List(Of SRadio)

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

        writer.Write($"<fieldset {estaHabilitado}>")
        If (Titulo.Length > 0) Then
            writer.Write($"<legend>{Titulo}</legend>")
        End If

        MyBase.RenderControl(writer)

        writer.Write("</fieldset>")
    End Sub

    Protected Overrides Sub DataBind(raiseOnDataBinding As Boolean)
        For i = 0 To SRadios.Count - 1
            Dim sradio = SRadios(i)
            If sradio.Checked Then
                ValorSeleccionado = sradio.Valor
            End If
        Next
    End Sub

    Protected Overrides Sub CreateChildControls()
        Controls.Clear()

        Dim valor_unico_radios As String = ID & "_" & UniqueID

        For i = 0 To SRadios.Count - 1
            Dim sradio = SRadios(i)
            sradio.ClientIDMode = ClientIDMode.Static
            sradio.ID = valor_unico_radios
            If ValorSeleccionado = sradio.Valor Then
                sradio.Checked = True
            Else
                sradio.Checked = False
            End If

            Controls.Add(sradio)
        Next
    End Sub

    Public Sub RaisePostBackEvent(eventArgument As String) Implements IPostBackEventHandler.RaisePostBackEvent
        Dim algo = eventArgument
        Console.WriteLine(algo)
    End Sub

End Class
