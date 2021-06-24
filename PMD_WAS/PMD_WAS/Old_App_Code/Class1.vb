Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Odbc
Imports System



Public Class Class1
    Public cn1 As New Data.SqlClient.SqlConnection
    Public cn2 As New Data.Odbc.OdbcConnection

    Public conexionCasa = "Data Source=DESKTOP-HMM88BG\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"
    Public conexionOficina = "Data Source=DESKTOP-3J6NM66\SQLEXPRESS;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"
    Public conexionServidor = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
    Public conexionActual = conexionCasa

    Public Function conectar() As SqlClient.SqlConnection
        cn1.Close()
        'cn1.ConnectionString = "server=WIN-VS5TZOS43BN;uid=sa;pwd=Dl160G6;DATABASE=PMD"   'Conexion servidor .8
        'cn1.ConnectionString = "server=WIN-IR0ZLK22UR6;uid=sa;pwd=WEBtesocl150;DATABASE=PMD" 'Conexion servidor .11
        'cn1.ConnectionString = "server=TAB\SQLEXPRESS;User ID=TAB\Administrador;database=PMD;Trusted_Connection=Yes"
        'cn1.ConnectionString = "Data Source=eslavadeveloper.database.windows.net,1433;Initial Catalog=PMD;User ID=eslavaDB;Password=Razus 1589"
        'cn1.ConnectionString = "Data Source=WIN-IR0ZLK22UR6\SQLEXPRESS;Initial Catalog=eventos;User ID=user_intelipolis;Password=intelipolis_2020"
        'cn1.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        cn1.ConnectionString = conexionServidor

        cn1.Open()
        Return cn1
    End Function
    Public Function ConxPDC3() As OdbcConnection
        cn2.Close()
        'cn2.ConnectionString = "PWD=mju4mkyt;DSN=iso9002;UID=isooper;HOST=162.198.100.2162.198.100.2;PROTOCOL=onsoctcp;SERVICE=1529;DATABASE=iso9002;SERVER=pdc3"
        'cn2.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        cn2.ConnectionString = conexionServidor
        cn2.Open()
        Return cn2
    End Function
    Public Function ConxFINAN() As OdbcConnection
        cn2.Close()
        'cn2.ConnectionString = "PWD=informix;DSN=infofix;UID=informix;HOST=162.198.100.5;PROTOCOL=onsoctcp;SERVICE=1533;DATABASE=dbfinan;SERVER=informatica"
        'cn2.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        cn2.ConnectionString = conexionServidor
        cn2.Open()
        Return cn2
    End Function
    Public Function conectarNomina() As OdbcConnection
        cn2.Close()
        'cn2.ConnectionString = "PWD=mju42m3w;DSN=finan;UID=informix;HOST=162.198.100.251;PROTOCOL=onsoctcp;SERVICE=1530;DATABASE=nomina;SERVER=finan"
        'cn2.ConnectionString = "Data Source=eslavadeveloper.database.windows.net,1433;Initial Catalog=PMD;User ID=eslavaDB;Password=Razus 1589"
        'cn2.ConnectionString = "Data Source=(local)\SQL2012;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;"
        'cn2.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        cn2.ConnectionString = conexionServidor
        cn2.Open()
        Return cn2
    End Function
    Public Function conectarContratos() As OdbcConnection
        cn2.Close()
        'cn2.ConnectionString = "PWD=informix;DSN=infofix;UID=informix;HOST=162.198.100.5;PROTOCOL=onsoctcp;SERVICE=1533;DATABASE=dbfinan;SERVER=informatica"
        'cn2.ConnectionString = "Data Source=187.176.54.246;Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion"
        cn2.ConnectionString = conexionServidor
        cn2.Open()
        Return cn2
    End Function
End Class
