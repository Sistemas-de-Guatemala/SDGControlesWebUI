Imports System.ComponentModel
Imports System.Web.UI
''' <summary>
''' Componente de Radio botón personalizado
''' </summary>
<ToolboxData("<{0}:SRadio ID=srd_ runat=server />")>
Public Class SRadio
    Inherits System.Web.UI.WebControls.RadioButton

    Sub New()
        InputAttributes.Add("class", "form-check-input")
        LabelAttributes.Add("class", "form-check-label")
    End Sub

    ''' <summary>
    ''' Está propiedad almacena un datos especifico
    ''' </summary>
    ''' <returns></returns>
    <DefaultValue("00")>
    <Category("Datos")>
    <Localizable(True)>
    Public Property Valor As String
        Get
            Return IIf(ViewState("valor_radio") <> Nothing, ViewState("valor_radio"), "")
        End Get
        Set(value As String)
            ViewState("valor_radio") = value
        End Set
    End Property

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        If Not Enabled Then
            InputAttributes.Add("disabled", "")
        Else
            InputAttributes.Remove("disabled")
        End If
    End Sub

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        writer.Write("<div class='form-check'>")
        MyBase.RenderControl(writer)
        writer.Write("</div>")
    End Sub

End Class
