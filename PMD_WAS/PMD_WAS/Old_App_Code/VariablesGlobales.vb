Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class VariablesGlobales

    ''' <summary>
    ''' Regresa la cantidad de días que se tendrá que esperar para realizar un evento
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property DIAS_ESPERA_EVENTO As Integer
        Get
            Try
                Dim con As New conexion
                Using data As New DB(con.Conectar())

                    Dim ds = data.ObtieneDatos("ObtieneConfiguracion", Nothing)

                    Return ds.Tables(0).Rows(0)("diasSolicitarEvento")
                End Using
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Shared ReadOnly Property CORREO_ALCALDE As String
        Get
            Try
                Dim con As New conexion
                Using data As New DB(con.Conectar())

                    Dim ds = data.ObtieneDatos("ObtieneConfiguracion", Nothing)

                    Return ds.Tables(0).Rows(0)("correoAlcalde")
                End Using
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
End Class
