Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class AjustaSubActividad
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                Response.Redirect("~/Password.aspx")
            End If

            If Session("Puesto") <> "1" Then
                Response.Redirect("~/Bienvenido.aspx")
            End If

            Using data As New DB(con.conectar())
                'Carga Administracion
                ddlAdmon.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmon.DataTextField = "Nombr_admon"
                ddlAdmon.DataValueField = "Cve_admon"
                ddlAdmon.DataBind()
                ddlAdmon.Items.Insert(0, New ListItem("Selecciona la administración", "0"))

                'Carga Meses
                ddlMes.DataSource = data.ObtieneDatos("ObtieneMeses", Nothing)
                ddlMes.DataTextField = "Mes"
                ddlMes.DataValueField = "Cve_mes"
                ddlMes.DataBind()

                ddlMesPersonas.DataSource = data.ObtieneDatos("ObtieneMeses", Nothing)
                ddlMesPersonas.DataTextField = "Mes"
                ddlMesPersonas.DataValueField = "Cve_mes"
                ddlMesPersonas.DataBind()

                ddlMesMateriales.DataSource = data.ObtieneDatos("ObtieneMeses", Nothing)
                ddlMesMateriales.DataTextField = "Mes"
                ddlMesMateriales.DataValueField = "Cve_mes"
                ddlMesMateriales.DataBind()

                ddlMesVehiculo.DataSource = data.ObtieneDatos("ObtieneMeses", Nothing)
                ddlMesVehiculo.DataTextField = "Mes"
                ddlMesVehiculo.DataValueField = "Cve_mes"
                ddlMesVehiculo.DataBind()

                ddlMesBeneficiados.DataSource = data.ObtieneDatos("ObtieneMeses", Nothing)
                ddlMesBeneficiados.DataTextField = "Mes"
                ddlMesBeneficiados.DataValueField = "Cve_mes"
                ddlMesBeneficiados.DataBind()

                'Carga Tipo de Tareas
                ddlTipoTarea.DataSource = data.ObtieneDatos("ObtieneTipoTarea", Nothing)
                ddlTipoTarea.DataTextField = "nombre"
                ddlTipoTarea.DataValueField = "id"
                ddlTipoTarea.DataBind()

                'Carga Materiales
                ddlMaterial.DataSource = data.ObtieneDatos("ObtieneMateriales", Nothing)
                ddlMaterial.DataTextField = "Nombre_Material"
                ddlMaterial.DataValueField = "Id_Material"
                ddlMaterial.DataBind()

                'Carga colonias
                ddlColonias.DataSource = data.ObtieneDatos("ObtieneColonias", Nothing)
                ddlColonias.DataTextField = "NombrColonia"
                ddlColonias.DataValueField = "CveColonia"
                ddlColonias.DataBind()

                'Clave gastos
                ddlClave.DataSource = data.ObtieneDatos("ObteneClaveGastos", Nothing)
                ddlClave.DataTextField = "Descripcion"
                ddlClave.DataValueField = "Clave"
                ddlClave.DataBind()

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
        'ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))
        'Using data As New DB(con.conectar())
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
        'Carga Direcciones
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
        ddlDireccion.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
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

        '
        'Using dataInfo As New DataInfo()

        '    'Dim ds = dataInfo.ObtienePuestos(ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue)
        '    Dim ds2 = dataInfo.ListarEmpleados(ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue)
        'End Using
    End Sub

    Protected Sub ddlLinea_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Tareas de la Linea
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue),
                New SqlParameter("@idLinea", ddlLinea.SelectedValue),
                New SqlParameter("@idEstatus", ddlEstatus.SelectedValue)
            }

            grdTareas.DataSource = data.ObtieneDatos("ObtieneSubActividadesLineasAjuste", params)
            grdTareas.DataBind()

            'Muestra estatus Linea
            Dim paramsLin() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAño", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue)
            }

            pnlAceptado.Visible = False
            pnlRechazado.Visible = False
            pnlReducido.Visible = False
            'Dim dt = data.ObtieneDatos("ObtieneEstatusLinea", paramsLin).Tables(0)
            'If dt.Rows.Count > 0 Then
            '    Dim estatus = dt.Rows(0)("Id_Estatus")
            '    Select Case estatus
            '        Case 1
            '            pnlAceptado.Visible = True
            '            lblComentariosAceptado.Text = dt.Rows(0)("Comentarios")
            '        Case 2
            '            pnlReducido.Visible = True
            '            lblComentariosReducido.Text = dt.Rows(0)("Comentarios")
            '        Case 0
            '            pnlRechazado.Visible = True
            '            lblComentariosRechazado.Text = dt.Rows(0)("Comentarios")
            '    End Select

            'End If


            '    'Carga los empleados
            '    Dim params2() As SqlParameter = New SqlParameter() _
            '{
            '    New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
            '    New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue)
            '}
            '    ddlPersona.DataSource = data.ObtieneDatos("ObtieneEmpleadosCombo", params2)
            '    ddlPersona.DataTextField = "nombr_empl"
            '    ddlPersona.DataValueField = "clave_empl"
            '    ddlPersona.DataBind()
        End Using

    End Sub

    Protected Sub chkTodosIgual_CheckedChanged(sender As Object, e As EventArgs)
        ddlMes.SelectedIndex = 0
        ddlMes.Enabled = Not chkTodosIgual.Checked
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Try
            If chkTodosIgual.Checked Then
                For i As Integer = 0 To ddlMes.Items.Count - 1
                    RegistraTarea(ddlMes.Items(i).Value)
                Next
            Else
                RegistraTarea(ddlMes.SelectedValue)
            End If

            'Vuelva  cargar las tareas
            ddlLinea_SelectedIndexChanged(Nothing, Nothing)

            'Limpia los campos
            Limpia_Campos()

            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", "ocultaModalLinea();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try

    End Sub

    Private Sub RegistraTarea(ByVal mes As Integer)
        'Crea variables
        Dim idLinea As String = ddlLinea.SelectedValue
        Dim idAdmon As Integer = Integer.Parse(ddlAdmon.SelectedValue)
        Dim año As Integer = Integer.Parse(ddlAnio.SelectedValue)
        Dim idSecretaria As Integer = Integer.Parse(ddlSecretaria.SelectedValue)
        Dim idDireccion As Integer = Integer.Parse(ddlDireccion.SelectedValue)
        Dim comentarios As String = txtComentarios.Text
        Dim asignado As String = "" 'ddlPersona.SelectedValue
        Dim emplCreacion As Integer = Integer.Parse(Session("Clave_empl").ToString())
        Dim claveGasto As Integer = Integer.Parse(ddlClave.SelectedValue)
        Dim montoDir As Decimal = 0 ' Decimal.Parse(txtMonto.Text)
        Dim metaDir As Integer = Integer.Parse(txtMeta.Text)

        Dim provider As CultureInfo = New System.Globalization.CultureInfo("es-MX")
        Dim fechaCompromiso As DateTime = DateTime.ParseExact(txtFechaCompromiso.Text, "dd/MM/yyyy", provider) 'Dim fechaCompromiso As DateTime = DateTime.Parse(txtFechaCompromiso.Text)
        Dim nombreTarea As String = txtNombreTarea.Text
        Dim idTipoMeta As Integer = Integer.Parse(ddlTipoTarea.SelectedValue)
        'Crea parametros
        Dim params() As SqlParameter = New SqlParameter() _
           {
               New SqlParameter("@Id_Linea", idLinea),
               New SqlParameter("@Id_Admon", idAdmon),
               New SqlParameter("@Año", año),
               New SqlParameter("@Id_Secretaria", idSecretaria),
               New SqlParameter("@Id_Direccion", idDireccion),
               New SqlParameter("@Nombre_Tarea", nombreTarea),
               New SqlParameter("@Mes", mes),
               New SqlParameter("@Clave_Gasto", claveGasto),
               New SqlParameter("@Fecha_Compromiso_Dir", fechaCompromiso),
               New SqlParameter("@Monto_Dir", montoDir),
               New SqlParameter("@Meta_Dir", metaDir),
               New SqlParameter("@Comentarios_Dir", comentarios),
               New SqlParameter("@Asignado1", asignado),
               New SqlParameter("@Empl_Creacion", emplCreacion),
               New SqlParameter("@Tipo_Tarea", idTipoMeta)
           }
        Using data As New DB(con.conectar())
            data.EjecutaCommand("AgregaTarea", params)
        End Using

    End Sub

    Protected Sub btnEnero_Command(sender As Object, e As CommandEventArgs)
        ddlMes.SelectedValue = e.CommandArgument.ToString().Split("|")(0)
        ddlTipoTarea.SelectedValue = e.CommandArgument.ToString().Split("|")(1)
        txtNombreTarea.Text = e.CommandName.ToString()
        ddlClave.Enabled = False
        txtNombreTarea.Enabled = False
        chkTodosIgual.Enabled = False
        ddlMes.Enabled = False
        ddlTipoTarea.Enabled = False
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "abre_modal", "abreModalTareas();", True)
    End Sub

    Protected Sub btnReinicia_Click(sender As Object, e As EventArgs)
        'Limpia los campos
        Limpia_Campos()
        ScriptManager.RegisterStartupScript(updAgregaMateriales, updAgregaMateriales.GetType(), "abre_modal", "abreModalTareas();", True)
    End Sub

    Private Sub Limpia_Campos()
        ddlMes.SelectedIndex = 0
        'ddlPersona.SelectedIndex = 0
        chkTodosIgual.Checked = False
        txtComentarios.Text = ""
        txtFechaCompromiso.Text = ""
        txtMeta.Text = ""
        txtMonto.Text = ""
        txtNombreTarea.Text = ""
        ddlClave.SelectedIndex = 0
        ddlClave.Enabled = True
        txtNombreTarea.Enabled = True
        chkTodosIgual.Enabled = True
        ddlMes.Enabled = True
        ddlTipoTarea.Enabled = True
    End Sub

    Protected Sub lnkModals_Command(sender As Object, e As CommandEventArgs)
        Dim tarea As String = e.CommandArgument.ToString().Split("|")(0)
        Dim mes As String = e.CommandArgument.ToString().Split("|")(1)
        lblNombreTareaActual.Text = tarea
        lblMesTareaActual.Text = mes
        If e.CommandName = "materiales" Then
            grdMateriales.PageIndex = 0
            ddlMaterial.SelectedIndex = 0
            txtCantidadMaterial.Text = ""
            RecargaMateriales(tarea, mes)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_material", "abreModalMateriales();", True)
        ElseIf e.CommandName = "personas" Then
            CargaPersonasDependientes(tarea, mes)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_personas", "abreModalPersonas();", True)
        ElseIf e.CommandName = "vehiculos" Then
            CargaVehiculos(tarea, mes)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_personas", "abreModalVehiculo();", True)
        ElseIf e.CommandName = "beneficiados" Then
            ddlColonias.SelectedIndex = 0
            ddlColonias.Enabled = True
            chkTodasColonias.Checked = False
            txtHombresBeneficiados.Text = ""
            txtMujeresBeneficiadas.Text = ""
            grdColoniasAsignadas.PageIndex = 0
            CargaBeneficiados(tarea, mes)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_personas", "abreModalBeneficiados();", True)
        ElseIf e.CommandName = "evento" Then
            btnLimpiar_Click(Nothing, Nothing)
            Dim Id As Integer = e.CommandArgument.ToString().Split("|")(2)
            lblIdSubActividad.Text = Id
            CargaEventosSubActividad(Id)
        End If
    End Sub

#Region "MATERIALES"

    Private Sub RecargaMateriales(tarea As String, mes As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
            New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
            New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
            New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
            New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
            New SqlParameter("@NombreTarea", tarea),
            New SqlParameter("@Mes", mes)
        }
            grdMateriales.DataSource = data.ObtieneDatos("ObtieneMaterialSubActividad", params)
            grdMateriales.DataBind()
        End Using
    End Sub

    Protected Sub btnAgregaMaterial_Click(sender As Object, e As EventArgs)
        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkTodosMesesMateriales.Checked Then
            For i As Integer = 0 To ddlMesMateriales.Items.Count - 1
                AgregaMaterial(tarea, ddlMes.Items(i).Value)
            Next
        Else
            AgregaMaterial(tarea, mes)
        End If

        'Limpia campos materiales
        ddlMaterial.SelectedIndex = 0
        txtCantidadMaterial.Text = ""

        RecargaMateriales(tarea, mes)
        ddlLinea_SelectedIndexChanged(Nothing, Nothing)

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_material", "abreModalMateriales();", True)
    End Sub

    Private Sub AgregaMaterial(tarea As String, mes As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
            New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
            New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
            New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
            New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
            New SqlParameter("@NombreTarea", tarea),
            New SqlParameter("@Mes", mes),
            New SqlParameter("@IdMaterial", ddlMaterial.SelectedValue),
            New SqlParameter("@Cantidad", txtCantidadMaterial.Text)
        }

            data.EjecutaCommand("AgregaMaterialSubActividad", params)
        End Using
    End Sub

    Protected Sub grdMateriales_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdColoniasAsignadas.PageIndex = e.NewPageIndex
        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text
        RecargaMateriales(tarea, mes)
    End Sub

    Protected Sub ddlMesMateriales_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesMateriales.SelectedValue
        Dim tarea As String = lblNombreTareaActual.Text
        RecargaMateriales(tarea, Integer.Parse(ddlMesMateriales.SelectedValue))
    End Sub

    Protected Sub chkTodosMesesMateriales_CheckedChanged(sender As Object, e As EventArgs)
        ddlMesMateriales.SelectedIndex = 0
        ddlMesMateriales.Enabled = Not chkTodosMesesMateriales.Checked
    End Sub

    Protected Sub btnQuitaMaterial_Click(sender As Object, e As EventArgs)
        Dim id As Integer = DirectCast(sender, Button).CommandArgument
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@id", id)
            }

            data.EjecutaCommand("EliminaMaterial", params)
        End Using

        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text
        RecargaMateriales(tarea, mes)
    End Sub

#End Region

#Region "PERSONAS"

    Protected Sub chkMesPersonas_CheckedChanged(sender As Object, e As EventArgs)
        ddlMesPersonas.SelectedIndex = 0
        ddlMesPersonas.Enabled = Not chkMesPersonas.Checked
    End Sub

    Protected Sub ddlMesPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesPersonas.SelectedValue
        Dim tarea As String = lblNombreTareaActual.Text
        CargaPersonasDependientes(tarea, Integer.Parse(ddlMesPersonas.SelectedValue))
    End Sub

    Protected Sub btnGuardaPersonas_Click(sender As Object, e As EventArgs)
        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesPersonas.Checked Then
            For i As Integer = 0 To ddlMesPersonas.Items.Count - 1
                GuardaPersona(tarea, ddlMesPersonas.Items(i).Value)
            Next
        Else
            GuardaPersona(tarea, mes)
        End If

        ddlLinea_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub GuardaPersona(tarea As String, mes As Integer)
        For Each item As ListViewDataItem In lstPersonas.Items
            Dim clave_empl As Integer = DirectCast(item.FindControl("hdnClave"), HiddenField).Value
            Dim porcentaje As Integer = 0
            Integer.TryParse(DirectCast(item.FindControl("txtPorcentaje"), TextBox).Text, porcentaje)

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
                New SqlParameter("@NombreTarea", tarea),
                New SqlParameter("@Mes", mes),
                New SqlParameter("@Clave_Empl", clave_empl),
                New SqlParameter("@Horas", porcentaje)
            }
                data.EjecutaCommand("AgregaEmpleadoTarea", params)

            End Using
        Next

        CargaPersonasDependientes(tarea, mes)
    End Sub

    Private Sub CargaPersonasDependientes(tarea As String, mes As Integer)
        Using d As New DataInfo()
            d.ObtienePuestos(ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue)
        End Using

        'Carga las Personas Dependientes
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@Clave_Jefe", Session("Clave_empl")),
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
                New SqlParameter("@NombreTarea", tarea),
                New SqlParameter("@Mes", mes)
            }

            lstPersonas.DataSource = data.ObtieneDatos("ObtieneEmpleadosTareas", params)
            lstPersonas.DataBind()
        End Using
    End Sub

#End Region

#Region "VEHICULOS"

    Private Sub CargaVehiculos(tarea As String, mes As Integer)
        'Carga los Vehiculos
        Using dataInfo As New DataInfo()
            lstVehiculos.DataSource = dataInfo.ObtieneVehiculos(ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue, ddlAdmon.SelectedValue, ddlAnio.SelectedValue, ddlLinea.SelectedValue, tarea, mes)
            lstVehiculos.DataBind()
        End Using


        'Using data As New DB(con.conectar())
        '    Dim params() As SqlParameter = New SqlParameter() _
        '    {
        '        New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
        '        New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
        '        New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
        '        New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
        '        New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
        '        New SqlParameter("@NombreTarea", tarea),
        '        New SqlParameter("@Mes", mes)
        '    }

        '    Dim dataVehiculos = data.ObtieneDatos("ObtieneVehiculos", params)

        '    Using dataInfo As New DataInfo()

        '        lstVehiculos.DataSource = Nothing
        '        lstVehiculos.DataBind()

        '    End Using

        'End Using
    End Sub

    Protected Sub btnAgregaVehiculos_Click(sender As Object, e As EventArgs)
        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesesVehiculos.Checked Then
            For i As Integer = 0 To ddlMesVehiculo.Items.Count - 1
                AgregaVehiculos(tarea, ddlMesVehiculo.Items(i).Value)
            Next
        Else
            AgregaVehiculos(tarea, mes)
        End If

        ddlLinea_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub AgregaVehiculos(tarea As String, mes As Integer)
        For Each item As ListViewDataItem In lstVehiculos.Items
            Dim vehiculo As Integer = DirectCast(item.FindControl("hdnIdVehiculo"), HiddenField).Value
            Dim kilometros As Integer = 0
            Integer.TryParse(DirectCast(item.FindControl("txtPorcentajeVehiculo"), TextBox).Text, kilometros)

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
                New SqlParameter("@NombreTarea", tarea),
                New SqlParameter("@Mes", mes),
                New SqlParameter("@IdVehiculo", vehiculo),
                New SqlParameter("@Kilometros", kilometros)
            }
                data.EjecutaCommand("AgregaVehiculoTarea", params)
                CargaVehiculos(tarea, mes)
            End Using
        Next
    End Sub

    Protected Sub ddlMesVehiculo_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesVehiculo.SelectedValue
        Dim tarea As String = lblNombreTareaActual.Text
        CargaVehiculos(tarea, Integer.Parse(ddlMesVehiculo.SelectedValue))
    End Sub

    Protected Sub chkMesesVehiculos_CheckedChanged(sender As Object, e As EventArgs)
        ddlMesVehiculo.SelectedIndex = 0
        ddlMesVehiculo.Enabled = Not chkMesesVehiculos.Checked
    End Sub

#End Region

#Region "BENEFICIADOS"

    Protected Sub ddlMesBeneficiados_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesBeneficiados.SelectedValue
        Dim tarea As String = lblNombreTareaActual.Text
        CargaBeneficiados(tarea, Integer.Parse(ddlMesBeneficiados.SelectedValue))
    End Sub

    Private Sub CargaBeneficiados(tarea As String, mes As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
                New SqlParameter("@NombreTarea", tarea),
                New SqlParameter("@Mes", mes)
            }
            Dim ds As DataSet = data.ObtieneDatos("ObtieneBeneficiados", params)
            Try
                txtHombresBeneficiados.Text = ds.Tables(0).Rows(0)("Beneficiados_Hombres")
                txtMujeresBeneficiadas.Text = ds.Tables(0).Rows(0)("Beneficiados_Muejres")
            Catch ex As Exception
                txtHombresBeneficiados.Text = 0
                txtMujeresBeneficiadas.Text = 0
            End Try

            Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
                New SqlParameter("@NombreTarea", tarea),
                New SqlParameter("@Mes", mes)
            }

            grdColoniasAsignadas.DataSource = data.ObtieneDatos("ObtieneColoniasBeneficiados", params2)
            grdColoniasAsignadas.DataBind()
        End Using
    End Sub

    Protected Sub btnGuardaBeneficiados_Click(sender As Object, e As EventArgs)
        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesBeneficiados.Checked Then
            For i As Integer = 0 To ddlMesBeneficiados.Items.Count - 1
                GuardaBeneficiados(tarea, ddlMesBeneficiados.Items(i).Value)
            Next
        Else
            GuardaBeneficiados(tarea, mes)
        End If
    End Sub

    Private Sub GuardaBeneficiados(tarea As String, mes As Integer)
        Dim hombres As Integer = 0
        Integer.TryParse(txtHombresBeneficiados.Text, hombres)

        Dim mujeres As Integer = 0
        Integer.TryParse(txtMujeresBeneficiadas.Text, mujeres)

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
                New SqlParameter("@NombreTarea", tarea),
                New SqlParameter("@Mes", mes),
                New SqlParameter("@Hombres", hombres),
                New SqlParameter("@Mujeres", mujeres)
            }

            data.EjecutaCommand("ActualizaBeneficiados", params)

        End Using
    End Sub

    Protected Sub btnAgregaColonias_Click(sender As Object, e As EventArgs)
        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesBeneficiados.Checked Then
            For i As Integer = 0 To ddlMesBeneficiados.Items.Count - 1
                GuardaColonias(tarea, ddlMesBeneficiados.Items(i).Value)
            Next
        Else
            GuardaColonias(tarea, mes)
        End If

        CargaBeneficiados(tarea, mes)
    End Sub

    Private Sub GuardaColonias(tarea As String, mes As Integer)
        Dim claveCol As Integer = Integer.Parse(ddlColonias.SelectedValue)
        Dim todasColonias As Boolean = chkTodasColonias.Checked

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue),
                New SqlParameter("@NombreTarea", tarea),
                New SqlParameter("@Mes", mes),
                New SqlParameter("@Clave_Col", claveCol),
                New SqlParameter("@Todas", todasColonias)
            }

            data.EjecutaCommand("GuardaColonias", params)

        End Using
    End Sub

    Protected Sub chkTodasColonias_CheckedChanged(sender As Object, e As EventArgs)
        Dim todasColonias As Boolean = chkTodasColonias.Checked
        ddlColonias.Enabled = Not todasColonias
        ddlColonias.SelectedIndex = 0
    End Sub

    Protected Sub grdColoniasAsignadas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdColoniasAsignadas.PageIndex = e.NewPageIndex
        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text
        CargaBeneficiados(tarea, mes)
    End Sub

    Protected Sub chkMesBeneficiados_CheckedChanged(sender As Object, e As EventArgs)
        ddlMesBeneficiados.SelectedIndex = 0
        ddlMesBeneficiados.Enabled = Not chkMesBeneficiados.Checked
    End Sub

    Protected Sub btnQuitaColonia_Click(sender As Object, e As EventArgs)

        Dim id As Integer = DirectCast(sender, Button).CommandArgument
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@id", id)
            }

            data.EjecutaCommand("EliminaColonia", params)
        End Using

        Dim tarea As String = lblNombreTareaActual.Text
        Dim mes As String = lblMesTareaActual.Text
        CargaBeneficiados(tarea, mes)
    End Sub

#End Region

    Protected Sub ddlEstatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Tareas de la Linea
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
              New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue),
                New SqlParameter("@idLinea", ddlLinea.SelectedValue),
                New SqlParameter("@idEstatus", ddlEstatus.SelectedValue)
            }

            grdTareas.DataSource = data.ObtieneDatos("ObtieneSubActividadesLineasAjuste", params)
            grdTareas.DataBind()

            'Muestra estatus Linea
            Dim paramsLin() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue),
                New SqlParameter("@IdAño", ddlAnio.SelectedValue),
                New SqlParameter("@IdLinea", ddlLinea.SelectedValue)
            }

            pnlAceptado.Visible = False
            pnlRechazado.Visible = False
            pnlReducido.Visible = False
            'Dim dt = data.ObtieneDatos("ObtieneEstatusLinea", paramsLin).Tables(0)
            'If dt.Rows.Count > 0 Then
            '    Dim estatus = dt.Rows(0)("Id_Estatus")
            '    Select Case estatus
            '        Case 1
            '            pnlAceptado.Visible = True
            '            lblComentariosAceptado.Text = dt.Rows(0)("Comentarios")
            '        Case 2
            '            pnlReducido.Visible = True
            '            lblComentariosReducido.Text = dt.Rows(0)("Comentarios")
            '        Case 0
            '            pnlRechazado.Visible = True
            '            lblComentariosRechazado.Text = dt.Rows(0)("Comentarios")
            '    End Select

            'End If


            '    'Carga los empleados
            '    Dim params2() As SqlParameter = New SqlParameter() _
            '{
            '    New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
            '    New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue)
            '}
            '    ddlPersona.DataSource = data.ObtieneDatos("ObtieneEmpleadosCombo", params2)
            '    ddlPersona.DataTextField = "nombr_empl"
            '    ddlPersona.DataValueField = "clave_empl"
            '    ddlPersona.DataBind()
        End Using

    End Sub


#Region "EVENTOS"

    Protected Sub lnkRecargaMaterialesEvento_Click(sender As Object, e As EventArgs)
        Dim idEvento As Integer = 0
        Try
            Integer.TryParse(Session("IdEventoSeleccionado").ToString(), idEvento)
        Catch ex As Exception
        End Try

        Dim abierto As Boolean = False
        abierto = ddlTipoEvento.SelectedValue.ToString() Is "1"
        Dim aforo As Decimal = 0
        Try
            Decimal.TryParse(txtAforoEstimado.Text, aforo)
        Catch ex As Exception
        End Try

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdEvento", idEvento),
                New SqlParameter("@Abierto", abierto),
                New SqlParameter("@AforoEstimado", aforo)
            }

            grdMaterialesEvento.DataSource = data.ObtieneDatos("CargaMaterialesPorTipoEvento", params)
            grdMaterialesEvento.DataBind()

        End Using

        ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "modal_evento", "MuestraEditarEvento();", True)
    End Sub

    Protected Sub btnGuardaEvento_Click(sender As Object, e As EventArgs)
        Dim idEvento As Integer = 0
        Try
            Integer.TryParse(Session("IdEventoSeleccionado").ToString(), idEvento)
        Catch ex As Exception
        End Try

        'Guarda evento
        Dim provider As CultureInfo = New System.Globalization.CultureInfo("es-MX")
        Dim fechaEvento As DateTime = DateTime.ParseExact(txtFechaEvento.Text, "dd/MM/yyyy", provider)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdEvento", idEvento),
                New SqlParameter("@IdSubActividad", lblIdSubActividad.Text),
                New SqlParameter("@Nombre", txtNombreEvento.Text),
                New SqlParameter("@Fecha", fechaEvento),
                New SqlParameter("@Aforo", txtAforoEstimado.Text),
                New SqlParameter("@AireLibre", ddlTipoEvento.SelectedValue Is "1")
            }

            idEvento = data.ObtieneDatos("GuardaEvento", params).Tables(0).Rows(0)(0)
            Session("IdEventoSeleccionado") = idEvento

        End Using


        For Each row As GridViewRow In grdMaterialesEvento.Rows
            Dim id As Integer = DirectCast(row.FindControl("hdnIdMaterial"), HiddenField).Value
            Dim cantidad As Decimal = DirectCast(row.FindControl("txtCantidad"), TextBox).Text
            GuardaMaterialEvento(idEvento, id, cantidad)
        Next

        btnLimpiar_Click(Nothing, Nothing)

        CargaEventosSubActividad(Integer.Parse(lblIdSubActividad.Text))
    End Sub

    Private Sub GuardaMaterialEvento(idEvento As Integer, idMaterial As Integer, cantidad As Decimal)

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdEvento", idEvento),
                New SqlParameter("@IdMaterial", idMaterial),
                New SqlParameter("@Cantidad", cantidad)
            }

            data.EjecutaCommand("GuardaMaterialesEvento", params)

        End Using

    End Sub

    Private Sub CargaEventosSubActividad(idSubActividad As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubACtividad", idSubActividad)
            }

            Try
                Dim dt = data.ObtieneDatos("ObtieneEventosSubActividad", params).Tables(0)
                grdEventosActuales.DataSource = dt
                grdEventosActuales.DataBind()

            Catch ex As Exception

            End Try
        End Using
        grdMaterialesEvento.DataSource = Nothing
        grdMaterialesEvento.DataBind()
        Session("IdEventoSeleccionado") = 0
        ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "modal_evento", "MuestraModalEvento();", True)
    End Sub

    Private Sub CargaEvento(IdEvento As Integer)

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdEvento", IdEvento)
            }

            Try
                Dim dt = data.ObtieneDatos("CargaEventoById", params).Tables(0)
                Session("IdEventoSeleccionado") = dt.Rows(0)("Id")
                txtNombreEvento.Text = dt.Rows(0)("Nombre")
                txtFechaEvento.Text = dt.Rows(0)("Fecha")
                txtAforoEstimado.Text = dt.Rows(0)("Aforo")
                Dim aireLibre As Boolean = dt.Rows(0)("AireLibre")
                If aireLibre Then
                    ddlTipoEvento.SelectedValue = "1"
                Else
                    ddlTipoEvento.SelectedValue = "2"
                End If
            Catch ex As Exception
                txtNombreEvento.Text = ""
                txtFechaEvento.Text = ""
                txtAforoEstimado.Text = ""
                ddlTipoEvento.SelectedIndex = 0
            End Try
        End Using

        lnkRecargaMaterialesEvento_Click(Nothing, Nothing)
        ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "edita_evento", "MuestraEditarEvento();", True)

    End Sub

#End Region

    Protected Sub btnEditar_Command(sender As Object, e As CommandEventArgs)
        Dim IdEvento As Integer = Integer.Parse(e.CommandArgument.ToString())
        CargaEvento(IdEvento)
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        Session("IdEventoSeleccionado") = 0
        txtNombreEvento.Text = ""
        txtFechaEvento.Text = ""
        txtAforoEstimado.Text = ""
        ddlTipoEvento.SelectedIndex = 0

    End Sub
End Class
