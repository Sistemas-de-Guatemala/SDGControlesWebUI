Imports System.Runtime.CompilerServices
Imports System.Web.UI

Public Module SExtensiones
    <Extension()>
    Public Function ObtenerControles(Of T As Control)(ByRef contenedor As Control) As List(Of T)
        Dim controles As List(Of T) = New List(Of T)
        For Each c As Control In contenedor.Controls
            If TypeOf c Is T Then
                controles.Add(CType(c, T))
            End If

            controles.AddRange(ObtenerControles(Of T)(c))
        Next

        Return controles
    End Function
End Module
