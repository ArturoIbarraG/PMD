Imports Class1
Imports System.Data
Imports System.Data.SqlClient
Partial Class Password
    Inherits System.Web.UI.Page
    Dim conx As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("paso") = "0"
        Session("Clave_empl") = "0"
        Session("clave_empl") = "0"

        TextBox2.Text = "9L4nE4Ci0N_04"

        'If Not IsPostBack Then
        '    Using helper As New IntelipolisEngine.Eventos.EventoHelper()

        '        helper.ValidaEtapasEventos()

        '    End Using

        'End If
    End Sub
    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Sent As New System.Data.SqlClient.SqlCommand("select * from [PMD].Usuarios where Clave_empl='" & Me.TextBox1.Text & "' and Password= '" + Me.TextBox2.Text + "'", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = Sent.ExecuteReader
        If exe.Read = Nothing Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "Proceso();", True)
        Else
            'Verifica La Seguridad(Usuario o Administrador) y lo pone en una variable sesion
            Dim Sent2 As New System.Data.SqlClient.SqlCommand("select * from [PMD].Usuarios where Clave_empl='" & Me.TextBox1.Text & "'and Password= '" + Me.TextBox2.Text + "'", conx.conectar)
            Dim exe2 As System.Data.SqlClient.SqlDataReader
            exe2 = Sent2.ExecuteReader
            exe2.Read()

            Session("P4$W0r0_S354mo") = TextBox2.Text
            Session("NombreEmpl") = exe2(1)
            Session("seguridad") = exe2(3)
            Session("Puesto") = exe2(4)
            Session("clave_depe") = exe2(9)

            exe2.Close()
            ''''''''
            Session("paso") = "1"
            Session("Clave_empl") = Me.TextBox1.Text
            Session("clave_empl") = Me.TextBox1.Text


            Dim dataInfo As New DataInfo()

            'Carga las secretarias
            dataInfo.CargaSecretariasWS()

            'Carga las dependencias
            dataInfo.CargaDependenciasWS()

            'Carga los empleados
            dataInfo.CargaEmpleadosWS()

            'Carga Puestos
            dataInfo.CargaPuestosWS()

            'Carga Vehiculos
            dataInfo.CargaVehiculosWS()

            ''Carga las Claves de Gasto
            'dataInfo.CargaClavesGastoWS()

            'Antes de Redireccionar al Menu:
            'Obtenemos la Fecha del Servidor
            Dim sent4 As New System.Data.SqlClient.SqlCommand("select convert(char, getdate(), 120)", conx.conectar)
            Dim exe4 As System.Data.SqlClient.SqlDataReader
            exe4 = sent4.ExecuteReader
            exe4.Read()
            Dim fecha = exe4(0)
            exe4.Close()

            'y Hacemos un insert a la Bitacora de Sesion
            Dim sent3 As New System.Data.SqlClient.SqlCommand("insert into [PMD].Bitacora_Sesion(Clave_empl,Fecha) values ('" & Me.TextBox1.Text & "',GETDATE())", conx.conectar)
            Dim exe3 As System.Data.SqlClient.SqlDataReader
            exe3 = sent3.ExecuteReader

            Try
                'CREDENCIALES DE EL PROYECTO DE EVENTOS
                Session("paso") = "0"
                Session("seguridad") = "0"


                Dim tabla As DataTable
                Dim stry = "SELECT clave_empl,pass_empl,clave_secr,privilegio,seguridad FROM [eventos].usuarios " &
                       " WHERE activo = 1 and clave_empl = '" & Me.TextBox1.Text & "'"

                tabla = conexion.sqlcon(stry)

                'Items.Add(tabla.Rows(i)("producto").ToString())
                Session("paso") = "1"
                Session("privilegio") = tabla.Rows(0)("privilegio")
                Session("id_secr") = tabla.Rows(0)("clave_secr")

                Session("clave_empl") = tabla.Rows(0)("clave_empl")
                Session("pass_empl") = tabla.Rows(0)("pass_empl")
                Session("seguridad") = tabla.Rows(0)("seguridad")

                Session("UsuarioAdmin") = 0

                'Redireccionamos
                Response.Redirect("Bienvenido.aspx")
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "Proceso();", True)
            End Try
        End If
        exe.Close()
    End Sub
End Class
