Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Odbc
Imports System

Public Class OPConexion
    Public cn1 As New Data.SqlClient.SqlConnection
    Public connectionString = "Data Source=187.176.54.246;Initial Catalog=obrap;User ID=usrPlaneacion;Password=usrPlaneacion"
    Public Function conectar() As SqlClient.SqlConnection
        cn1.Close()
        cn1.ConnectionString = connectionString
        cn1.Open()
        Return cn1
    End Function

End Class
