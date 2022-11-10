Imports System.Web.UI
Imports System.Web.UI.WebControls

''' <summary>
''' Tabla de datos dinamico con búsqueda, filtros y botones para exportar
''' </summary>
<ToolboxData("<{0}:STablaDinamica ID=std_ runat=server></{0}:STablaDinamica>")>
Public Class STablaDinamica
    Inherits System.Web.UI.WebControls.DataGrid

    Sub New()
        CssClass += " table table-striped table-bordered datatable"
        Width = Unit.Parse("100%")
    End Sub

    Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
        UseAccessibleHeader = True

        If Controls.Count > 0 Then
            Dim table As Table = TryCast(Controls(0), Table)

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
