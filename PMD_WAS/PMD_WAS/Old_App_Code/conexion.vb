Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc

Public Class conexion


    Public con As New Data.SqlClient.SqlConnection

    Public conexionCasa = "Data Source=DESKTOP-HMM88BG\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"
    Public conexionOficina = "Data Source=DESKTOP-3J6NM66\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"

    'EQUIPO1\SQLEXPRESS

    Public Function Conectar() As SqlClient.SqlConnection
        con.Close()
        'con.ConnectionString = "DATABASE=eventos;SERVER=LAU-B6BF32A13EF\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
        'con.ConnectionString = "server=WIN-VS5TZOS43BN; uid=user_envio; pwd=mexico78; DATABASE=eventos"
        ' con.ConnectionString = "Data Source=192.168.100.8;Initial Catalog=eventos;User ID=user_envio;Password=mexico78"
        'con.ConnectionString = "DATABASE=eventos;SERVER=WIN-VS5TZOS43BN;TRUSTED_CONNECTION=YES;"
        'con.ConnectionString = "Data Source=(local)\SQL2012;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"
        'con.ConnectionString = "Data Source=WIN-IR0ZLK22UR6\SQLEXPRESS;Initial Catalog=eventos;User ID=user_intelipolis;Password=intelipolis_2020"
        'con.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        con.ConnectionString = conexionCasa
        con.Open()
        Return con
    End Function


    Public Sub Desconectar()
        con.Close()
    End Sub


    Public Shared Function sqlcon(ByVal str As String) As DataTable   ' recibe una variable con el query

        Dim Cn1 As SqlConnection  ' se crea un objeto en donde se hara la conexion a la bd
        Dim dr As SqlDataReader = Nothing ' se crea el objeto data reader que sera para leer los registros en la bd
        Dim cmd As SqlCommand ' se crea el objeto command para realizar los comandos
        Dim tabla As DataTable = New DataTable() ' se crea el objeto datatable para vaciar la informacion del query

        Dim conexionCasa = "Data Source=DESKTOP-HMM88BG\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"
        Dim conexionOficina = "Data Source=DESKTOP-3J6NM66\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"

        Cn1 = New SqlConnection() ' se crea un objeto conexion en donde se pone el servidor, user, pass y bd a la que accesara la aplicacion
        'Cn1.ConnectionString = "DATABASE=pruebas;SERVER=LAU-B6BF32A13EF\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
        'Cn1.ConnectionString = "DATABASE=eventos;SERVER=LAU-B6BF32A13EF\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
        'Cn1.ConnectionString = "Data Source=(local)\SQL2012;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;" 'Cn1.ConnectionString = "server=WIN-VS5TZOS43BN; uid=user_envio; pwd=mexico78; DATABASE=eventos2" '"DATABASE=eventos;SERVER=EQUIPO1\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
        'Cn1.ConnectionString = "DATABASE=eventos;SERVER=WIN-VS5TZOS43BN;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
        'Cn1.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        Cn1.ConnectionString = conexionCasa

        '----------ORIGINAL
        'Cn1.ConnectionString = "server=TESOSVRX\SISTEMASV; uid=sa; pwd=CL160web; DATABASE=eventos"

        Try
            Cn1.Open() ' se abre la conexion
            cmd = New SqlCommand() ' se crea un cmd
            cmd.CommandText = str ' se le manda el query que se creo en la aplicacion
            cmd.Connection = Cn1 ' se le da la cadena de conexion
            dr = cmd.ExecuteReader() ' al objeto datareader se le manda la cadena de conexion ademas de el query que realizara y ejecuta la lectura a la bd
            tabla.Load(dr) ' se cargan los datos a el objeto tabla con lo que el objeto dr obtuvo

        Catch mierror As SqlException ' se crea una excepcion por si la aplicacion no pudo conectarse a la bd
            MsgBox("Error de conexion a la base de datos: " & mierror.Message)
        Finally
            If Not dr Is Nothing Then
                dr.Close() ' se cierra el objeto dr
            End If
            Cn1.Close() ' se cierra la conexion a la bd
        End Try
        Return tabla ' retorna el objeto tabla con los datos encontrados
    End Function

    Public Shared Function sqlcambios(ByVal str As String) As String   ' recibe una variable con el query

        Dim Cn1 As SqlConnection ' se crea un objeto en donde se hara la conexion a la bd
        Dim cmd As SqlCommand ' se crea el objeto command para realizar los comandos
        Cn1 = New SqlConnection() ' se crea un objeto conexion en donde se pone el servidor, user, pass y bd a la que accesara la aplicacion

        Dim conexionCasa = "Data Source=DESKTOP-HMM88BG\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"
        Dim conexionOficina = "Data Source=DESKTOP-3J6NM66\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"

        'Cn1.ConnectionString = "DATABASE=eventos;SERVER=LAU-B6BF32A13EF\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
        'Cn1.ConnectionString = "Data Source=(local)\SQL2012;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;" 'Cn1.ConnectionString = "server=WIN-VS5TZOS43BN; uid=user_envio; pwd=mexico78; DATABASE=eventos" '"DATABASE=eventos;SERVER=EQUIPO1\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
        'Cn1.ConnectionString = "server=TESOSVRX\SISTEMASV; uid=sa; pwd=CL160web; DATABASE=eventos"
        'Cn1.ConnectionString = "server=WIN-VS5TZOS43BN; uid=sa; pwd=CL160web; DATABASE=eventos"
        'Cn1.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        Cn1.ConnectionString = conexionCasa

        Dim resultado As Integer    ' se crea una variable en la cual se pondra un valor integer el cual servira para saber si se realizo la accion ' en la cual se hayan afectado registros en la bd

        Try
            Cn1.Open() ' se abre la conexion
            cmd = New SqlCommand()  ' se crea un cmd
            cmd.CommandText = str ' se le manda el query que se creo en la aplicacion
            cmd.Connection = Cn1 ' se le da la cadena de conexion

            resultado = cmd.ExecuteNonQuery   ' a la variable resultado se le asignara el valor si es que realizo la accion de cambio en la bd
        Catch mierror As SqlException ' se crea la excepcion por si la aplicacion no pudo accesar a la bd
            resultado = -1 ' si resultado es igual a -1 quiere decir que la accion no se pudo realizar por un problema en la instruccion query
            MsgBox("Error de conexion a la base de datos: " & mierror.Message)
        Finally
            Cn1.Close() ' se cierra la conexion a la bd
        End Try
        Return resultado
        'se retorna la variable resultado con el valor obtenido
    End Function

    'Public Shared Function infoconingre(ByVal str As String) As DataTable   ' recibe una variable con el query
    '    Dim Cn2 As OdbcConnection   ' se crea un objeto en donde se hara la conexion a la bd
    '    Dim dr As OdbcDataReader = Nothing ' se crea el objeto data reader que sera para leer los registros en la bd
    '    Dim cmd As OdbcCommand  ' se crea el objeto command para realizar los comandos
    '    Dim tabla As DataTable = New DataTable() ' se crea el objeto datatable para vaciar la informacion del query

    '    Cn2 = New OdbcConnection() ' se crea un objeto conexion en donde se pone el servidor, user, pass y bd a la que accesara la aplicacion
    '    'Cn1.ConnectionString = "DATABASE=pruebas;SERVER=LAU-B6BF32A13EF\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
    '    Cn2.ConnectionString = "DATABASE=dbgeneral;SERVER=ingresos;PWD=dbgenera;UID=dgeneral;DSN=general;HOST=162.198.100.10;PROTOCOL=onsoctcp;SERVICE=1531" ' despues se le da la cadena de conexion al objeto creado
    '    ' '.ConnectionString = ("PWD=mju42m3w;DSN=info284;UID=informix;HOST=162.198.100.251;PROTOCOL=onsoctcp;SERVICE=1530;DATABASE=transito;SERVER=finan")

    '    Try
    '        Cn2.Open() ' se abre la conexion
    '        cmd = New OdbcCommand() ' se crea un cmd
    '        cmd.CommandText = str ' se le manda el query que se creo en la aplicacion
    '        cmd.Connection = Cn2 ' se le da la cadena de conexion
    '        dr = cmd.ExecuteReader() ' al objeto datareader se le manda la cadena de conexion ademas de el query que realizara y ejecuta la lectura a la bd
    '        tabla.Load(dr) ' se cargan los datos a el objeto tabla con lo que el objeto dr obtuvo

    '    Catch mierror As SqlException ' se crea una excepcion por si la aplicacion no pudo conectarse a la bd
    '        MsgBox("Error de conexion a la base de datos: " & mierror.Message)
    '    Finally
    '        If Not dr Is Nothing Then
    '            dr.Close() ' se cierra el objeto dr
    '        End If
    '        Cn2.Close() ' se cierra la conexion a la bd
    '    End Try
    '    Return tabla ' retorna el objeto tabla con los datos encontrados
    'End Function

    'Public Shared Function infoconpdc3(ByVal str As String) As DataTable   ' recibe una variable con el query
    '    Dim Cn3 As OdbcConnection   ' se crea un objeto en donde se hara la conexion a la bd
    '    Dim dr As OdbcDataReader = Nothing ' se crea el objeto data reader que sera para leer los registros en la bd
    '    Dim cmd As OdbcCommand  ' se crea el objeto command para realizar los comandos
    '    Dim tabla As DataTable = New DataTable() ' se crea el objeto datatable para vaciar la informacion del query

    '    Cn3 = New OdbcConnection() ' se crea un objeto conexion en donde se pone el servidor, user, pass y bd a la que accesara la aplicacion
    '    'Cn1.ConnectionString = "DATABASE=pruebas;SERVER=LAU-B6BF32A13EF\SQLEXPRESS;TRUSTED_CONNECTION=YES;" ' despues se le da la cadena de conexion al objeto creado
    '    Cn3.ConnectionString = "DATABASE=dbcalidad;SERVER=pdc3;PWD=mju4mkyt;UID=isooper;DSN=dbcalidad;HOST=162.198.100.2;PROTOCOL=olsoctcp;SERVICE=1529" ' despues se le da la cadena de conexion al objeto creado


    '    Try
    '        Cn3.Open() ' se abre la conexion
    '        cmd = New OdbcCommand() ' se crea un cmd
    '        cmd.CommandText = str ' se le manda el query que se creo en la aplicacion
    '        cmd.Connection = Cn3 ' se le da la cadena de conexion
    '        dr = cmd.ExecuteReader() ' al objeto datareader se le manda la cadena de conexion ademas de el query que realizara y ejecuta la lectura a la bd
    '        tabla.Load(dr) ' se cargan los datos a el objeto tabla con lo que el objeto dr obtuvo

    '    Catch mierror As SqlException ' se crea una excepcion por si la aplicacion no pudo conectarse a la bd
    '        MsgBox("Error de conexion a la base de datos: " & mierror.Message)
    '    Finally
    '        If Not dr Is Nothing Then
    '            dr.Close() ' se cierra el objeto dr
    '        End If
    '        Cn3.Close() ' se cierra la conexion a la bd
    '    End Try
    '    Return tabla ' retorna el objeto tabla con los datos encontrados
    'End Function
End Class
