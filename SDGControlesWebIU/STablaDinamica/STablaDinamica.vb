Imports System.Web.UI
Imports System.Web.UI.WebControls

''' <summary>
''' Tabla de datos dinamico con búsqueda, filtros y botones para exportar
''' </summary>
<ToolboxData("<{0}:STablaDinamica ID=std_ runat=server></{0}:STablaDinamica>")>
Public Class STablaDinamica
    Inherits DataGrid

    Private mMostrarFiltros As Boolean = False
    Private mExportar As Boolean = False

    Sub New()
        CssClass += " table table-borderless table-hover tablaP stabladinamica "
        Width = Unit.Parse("100%")
        AutoGenerateColumns = False
        EnableViewState = True
    End Sub

    ''' <summary>
    ''' Esta propiedad habilita los filtros en la tabla, por defecto es verdadero
    ''' </summary>
    ''' <returns></returns>
    Public Property MostrarFiltros As Boolean
        Get
            Return mMostrarFiltros
        End Get
        Set(value As Boolean)
            mMostrarFiltros = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad habilita los botones de exportar en la tabla, por defecto es verdadero
    ''' </summary>
    ''' <returns></returns>
    Public Property Exportar As Boolean
        Get
            Return mExportar
        End Get
        Set(value As Boolean)
            mExportar = value
        End Set
    End Property

    Protected Overrides Sub OnPreRender(e As EventArgs)

        If Not Exportar And Not MostrarFiltros Then
            If Not CssClass.Contains("datatable-simple") Then
                CssClass += " datatable-simple"
            End If
        ElseIf Exportar And Not MostrarFiltros Then
            If Not CssClass.Contains("datatable-con-exportar") Then
                CssClass += " datatable-con-exportar"
            End If
        ElseIf Not Exportar And MostrarFiltros Then
            If Not CssClass.Contains("datatable-con-filtros") Then
                CssClass += " datatable-con-filtros"
            End If
        Else
            If Not CssClass.Contains("datatable-x-defecto") Then
                CssClass += " datatable-x-defecto"
            End If
        End If

        UseAccessibleHeader = True
        If Controls.Count > 0 Then
            Dim table = TryCast(Controls(0), Table)

            If table IsNot Nothing AndAlso table.Rows.Count > 0 Then
                UseAccessibleHeader = True
                table.Rows(0).TableSection = TableRowSection.TableHeader

                If ShowFooter Then
                    table.Rows(table.Rows.Count - 1).TableSection = TableRowSection.TableFooter
                End If
            End If
        End If

        MyBase.OnPreRender(e)
    End Sub

    ''' <summary>
    ''' Está función limpia los datos del datagrid
    ''' </summary>
    Public Sub LimpiarDataGrid()
        DataSource = Nothing
        DataBind()
    End Sub

End Class
