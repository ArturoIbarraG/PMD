Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DB : Implements IDisposable

    Public conn As SqlConnection

    Public Sub New(ByVal con As SqlConnection)


        conn = con
    End Sub

    Public Sub DB(ByVal con As SqlConnection)
        conn = con
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        conn.Close()
    End Sub

    ''' <summary>
    ''' Regresa la informacion de un sp
    ''' </summary>
    ''' <param name="sp"></param>
    ''' <param name="parametros"></param>
    ''' <returns></returns>
    Public Function ObtieneDatos(ByVal sp As String, ByVal parametros As SqlParameter()) As DataSet
        'Crea las variables
        Dim ds As New DataSet
        Dim command As New SqlCommand(sp, conn)
        command.CommandType = CommandType.StoredProcedure

        'Agrega los parametros si existe
        If Not parametros Is Nothing Then
            For Each p As SqlParameter In parametros
                command.Parameters.Add(p)
            Next
        End If

        Dim adapter As New SqlDataAdapter(command)
        adapter.Fill(ds)

        Return ds

    End Function

    Public Function EjecutaCommand(ByVal sp As String, ByVal parametros As SqlParameter()) As Object
        'Crea las variables
        Dim ds As New DataSet
        Dim command As New SqlCommand(sp, conn)
        command.CommandType = CommandType.StoredProcedure

        'Agrega los parametros si existe
        If Not parametros Is Nothing Then
            For Each p As SqlParameter In parametros
                command.Parameters.Add(p)
            Next
        End If

        Return command.ExecuteScalar()

    End Function

End Class
