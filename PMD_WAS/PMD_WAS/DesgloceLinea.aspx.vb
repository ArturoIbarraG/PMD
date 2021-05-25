Imports System.Data.SqlClient
Imports System.Globalization

Partial Class DesgloceLinea
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                Response.Redirect("~/Password.aspx")
            End If
            'Dim dataInfo = New DataInfo()
            'Dim sec As String = ""
            'Dim dep As String = ""
            ''Dim d = dataInfo.ObtieneVehiculos(sec, dep)

            'Dim ws As New WsService.Service
            'Dim ds2 As Data.DataSet = ws.ListaEmpleados("304", "243")
            ''Dim ds3 As New Data.DataSet
            ''ds3 = ws.ConsXPuestos("304​", "243")

            'Dim ds4 As Data.DataSet = ws.ConsultaVehiculos("1", "Aq333c_jfnrRRQ!", "304", "243")

            'Dim wsInfofin As New WsInfoFin.Service
            'Dim key As String = "fg_3311$.2212v?​"
            'Dim ds5 As Data.DataSet = wsInfofin.ConsultaArticulos(key, "2532​​", "2")

            ''If Session("Puesto") <> "2" Then
            ''    Response.Redirect("~/Bienvenido.aspx")
            ''End If

            Using data As New DB(con.conectar())
                'Carga Administracion
                ddlAdmon.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmon.DataTextField = "Nombr_admon"
                ddlAdmon.DataValueField = "Cve_admon"
                ddlAdmon.DataBind()
                ddlAdmon.Items.Insert(0, New ListItem("Selecciona la administración", "0"))

                ''Carga empleados
                'Dim claveEmpleado As Integer = Integer.Parse(Session("Clave_empl"))
                'ddlPersona.DataSource = data.ObtieneDatos("ObtieneEmpleados", New SqlParameter() {New SqlParameter("@Clave_Jefe", claveEmpleado)})
                'ddlPersona.DataTextField = "Nombr_empl"
                'ddlPersona.DataValueField = "Clave_empl"
                'ddlPersona.DataBind()

            End Using

        End If

    End Sub
    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''Carga Secretarias
        'Using data As New DataInfo()

        '    ddlSecretaria.DataSource = data.ObtieneSecretarias
        '    ddlSecretaria.DataTextField = "nombr_secr"
        '    ddlSecretaria.DataValueField = "clave_secr"
        '    ddlSecretaria.DataBind()
        'End Using

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

        'Carga el año
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue)
            }

            ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params)
            ddlAnio.DataTextField = "Año"
            ddlAnio.DataValueField = "Año"
            ddlAnio.DataBind()
        End Using

    End Sub

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''Carga Direcciones
        'Using data As New DataInfo()

        '    ddlDireccion.DataSource = data.ObtieneDependencias(ddlSecretaria.SelectedValue)
        '    ddlDireccion.DataTextField = "nombr_depe"
        '    ddlDireccion.DataValueField = "clave_depe"
        '    ddlDireccion.DataBind()
        'End Using
        'ddlDireccion.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlDireccion.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDireccion.DataTextField = "Nombr_direccion"
            ddlDireccion.DataValueField = "IdDireccion"
            ddlDireccion.DataBind()
        End Using
        'ddlDireccion.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Líneas
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue)
            }

            ddlLinea.DataSource = data.ObtieneDatos("ObtieneLineas", params)
            ddlLinea.DataTextField = "Nombr_linea"
            ddlLinea.DataValueField = "ID"
            ddlLinea.DataBind()
        End Using
        ddlLinea.Items.Insert(0, New ListItem("Selecciona la Línea", "0"))
    End Sub
    Protected Sub ddlLinea_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Tareas de la Linea
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue)
            }
            grdTareas.DataSource = data.ObtieneDatos("ObtieneTareasLineasDesgloce", params)
            grdTareas.DataBind()

            Dim paramsResumen() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue),
                New SqlParameter("@idLinea", ddlLinea.SelectedValue)
            }
            grdLineaResumen.DataSource = data.ObtieneDatos("ObtieneTareasLineasResumen", paramsResumen)
            grdLineaResumen.DataBind()
        End Using
    End Sub
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        'Crea variables
        Dim idLinea As Integer = Integer.Parse(ddlLinea.SelectedValue)
        Dim idAdmon As Integer = Integer.Parse(ddlAdmon.SelectedValue)
        Dim año As Integer = Integer.Parse(ddlAnio.SelectedValue)
        Dim idSecretaria As Integer = Integer.Parse(ddlSecretaria.SelectedValue)
        Dim idDireccion As Integer = Integer.Parse(ddlDireccion.SelectedValue)
        Dim comentarios As String = txtComentarios.Text
        Dim asignado As String = ddlPersona.SelectedItem.Text
        Dim emplCreacion As Integer = Integer.Parse(Session("Clave_empl").ToString())
        Dim metaDir As Integer = Integer.Parse(txtMeta.Text)
        Dim provider As CultureInfo = New System.Globalization.CultureInfo("es-MX")
        Dim fechaCompromiso As DateTime = DateTime.ParseExact(txtFechaCompromiso.Text, "dd/MM/yyyy", provider)
        'Dim fechaCompromiso As DateTime = DateTime.Parse(txtFechaCompromiso.Text)
        Dim metaCoordinador As Integer = Integer.Parse(txtMeta.Text)
        Dim nombreTarea As String = hdnNombreTarea.Value

        'Crea parametros
        Dim params() As SqlParameter = New SqlParameter() _
           {
               New SqlParameter("@Id_Linea", idLinea),
               New SqlParameter("@Id_Admon", idAdmon),
               New SqlParameter("@Año", año),
               New SqlParameter("@Id_Secretaria", idSecretaria),
               New SqlParameter("@Id_Direccion", idDireccion),
               New SqlParameter("@Fecha_Compromiso_Coor", fechaCompromiso),
               New SqlParameter("@Meta_Coor", metaCoordinador),
               New SqlParameter("@Asignado2", asignado),
               New SqlParameter("@Comentarios_Coor", comentarios),
               New SqlParameter("@Nombre_Tarea", nombreTarea)
           }
        Using data As New DB(con.conectar())
            data.EjecutaCommand("CompletaInformacionTarea", params)
        End Using

        'Vuelva  cargar las tareas
        ddlLinea_SelectedIndexChanged(Nothing, Nothing)

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", "ocultaAsignadoCoordinador();", True)
    End Sub

    Protected Sub lnkCargaInfoLinea_Click(sender As Object, e As EventArgs)

    End Sub
    Protected Sub btnAsignar_Command(sender As Object, e As CommandEventArgs)
        hdnNombreTarea.Value = e.CommandArgument
        Using helper As New DataInfo()
            helper.ListarEmpleados(ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue)
        End Using
        Using data As New DB(con.conectar())
            'Crea variables
            Dim idLinea As Integer = Integer.Parse(ddlLinea.SelectedValue)
            Dim idAdmon As Integer = Integer.Parse(ddlAdmon.SelectedValue)
            Dim año As Integer = Integer.Parse(ddlAnio.SelectedValue)
            Dim idSecretaria As Integer = Integer.Parse(ddlSecretaria.SelectedValue)
            Dim idDireccion As Integer = Integer.Parse(ddlDireccion.SelectedValue)
            Dim nombreTarea As String = hdnNombreTarea.Value

            'Crea parametros
            Dim params() As SqlParameter = New SqlParameter() _
           {
               New SqlParameter("@IdSecretaria", idSecretaria),
               New SqlParameter("@IdDireccion", idDireccion),
               New SqlParameter("@IdAdmon", idAdmon),
                New SqlParameter("@IdAnio", año),
                New SqlParameter("@IdLinea", idLinea),
               New SqlParameter("@NombreTarea", nombreTarea)
           }
            'Carga empleados
            Dim claveEmpleado As Integer = Integer.Parse(Session("Clave_empl"))
            ddlPersona.DataSource = data.ObtieneDatos("ObtieneEmpleados", params)
            ddlPersona.DataTextField = "nombr_empl"
            ddlPersona.DataValueField = "clave_empl"
            ddlPersona.DataBind()

        End Using


    End Sub
End Class
