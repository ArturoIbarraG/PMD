Imports Class1
Imports System.Data.SqlClient
Imports System.Data
Partial Class AltaDeAdministradores
    Inherits System.Web.UI.Page
    Dim conx As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("paso") = "1" Then
        Else
            Response.Redirect("Password.aspx")
        End If
        If IsPostBack = False Then
            CargaUsuarios()
            LlenardropSeguridad()
        End If
    End Sub
    Private Sub CargaUsuarios()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''Llena drop de usuarios de tabla de Nomina
        Dim RsGen3 As New System.Data.Odbc.OdbcCommand("select clave_empl,nombr_empl from nomempleadoz where clave_tipr<>4 or clave_tipr<>9 or clave_tipr<>10  order by 2", conx.conectarNomina)
        Dim Drsx3 As System.Data.Odbc.OdbcDataReader
        Drsx3 = RsGen3.ExecuteReader
        Drsx3.Read()
        Try
            If Not Drsx3 Is Nothing Then
                DropUsuarios.DataSource = Drsx3
                DropUsuarios.Font.Size = 12
                DropUsuarios.DataTextField = "nombr_empl"
                DropUsuarios.DataValueField = "clave_empl"
                DropUsuarios.DataBind()
                Drsx3.NextResult()
            End If
        Finally
            Drsx3.Close()
        End Try
    End Sub
    Public Sub LlenardropSeguridad()
        'LLENAR EL DROP SEGURIDAD
        Dim sent As New System.Data.SqlClient.SqlCommand("Select Cve_seguridad,Descripcion from Seguridad", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropSeguridad.DataSource = exe
                DropSeguridad.DataTextField = "Descripcion"
                DropSeguridad.DataValueField = "Cve_seguridad"
                DropSeguridad.DataBind()
                exe.NextResult()
            End If
            exe.Close()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Guardar.Click
        Dim Stry5 As String
        Dim Rs5 As Odbc.OdbcDataReader

        Stry5 = "select nombr_empl from nomempleadoz where Clave_empl='" & Me.DropUsuarios.Text & "' and (clave_tipr<>4 or clave_tipr<>9 or clave_tipr<>10 ) "

        Dim cmd5 As New Data.Odbc.OdbcCommand(Stry5, conx.conectarNomina())
        Rs5 = cmd5.ExecuteReader()
        Rs5.Read()

        Dim Nombre As String = Rs5(0)

        '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
        Dim Stry1 As String
        Dim Rs1 As SqlDataReader

        Stry1 = "select Clave_empl from [PMD].Usuarios where Clave_empl='" & Me.DropUsuarios.Text & "' "

        Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
        Rs1 = cmd1.ExecuteReader()
        '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Año Solo Updatea la contraseña y nivel de seguridad
        If Rs1.Read() = True Then

            Dim Stry3 As String
            Dim Rs3 As SqlDataReader

            Stry3 = "update Usuarios set Password='" & Me.txtPassword.Text & "',Nivel_seguridad=' " & Me.DropSeguridad.Text & " ' where Clave_empl='" & Me.DropUsuarios.Text & "' "
            Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
            Rs3 = cmd3.ExecuteReader()

            Rs3.Read()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
            ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
        Else
            Dim Stry4 As String
            Dim Rs4 As SqlDataReader

            Stry4 = "insert into Usuarios(Clave_empl,Nombr_empl,Password,Nivel_seguridad) values ('" & Me.DropUsuarios.Text & "','" & Nombre & "','" & Me.txtPassword.Text & "','" & Me.DropSeguridad.Text & "')"
            Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
            Rs4 = cmd4.ExecuteReader()

            Rs4.Read()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
        End If
    End Sub

    
End Class
