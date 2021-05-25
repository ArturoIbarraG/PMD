Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class AsignarRecursosMetas
    Inherits System.Web.UI.Page

    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Asignar recursos y metas")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                Response.Redirect("~/Password.aspx")
            End If

            'If Session("Puesto") <> "1" Then
            '    Response.Redirect("~/Bienvenido.aspx")
            'End If

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
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@empleado", Session("clave_empl")),
                New SqlParameter("@tipo", "-1")}
                ddlMaterial.DataSource = data.ObtieneDatos("CargaRequerimientosDirector", params)
                ddlMaterial.DataTextField = "requerimiento"
                ddlMaterial.DataValueField = "id"
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

            btnMetasLinea.Visible = False

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

        LimpiaInformacionLineas()
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

        LimpiaInformacionLineas()
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

        LimpiaInformacionLineas()
    End Sub

    Private Sub CargaPresupuesto()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue),
                New SqlParameter("@idLinea", ddlLinea.SelectedValue)
            }

            Dim ds = data.ObtieneDatos("ObtieneSubActividadesLineas", params)
            grdTareas.DataSource = ds
            grdTareas.DataBind()
        End Using
    End Sub

    Protected Sub ddlLinea_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Tareas de la Linea
        Using data As New DB(con.conectar())

            CargaPresupuesto()

            'Muestra informacion linea
            Try
                Dim paramsLinea() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@Linea", ddlLinea.SelectedValue)
            }
                Dim dtInfo = data.ObtieneDatos("ObtieneInformacionLinea", paramsLinea).Tables(0)

                'INDICADOR
                pnlIndicadorLinea.Visible = True
                lblIndicadorLinea.Text = dtInfo.Rows(0)("Indicador").ToString()
                lblIndicadorMeta.Text = dtInfo.Rows(0)("Indicador").ToString()

                'PROGRAMA
                pnlProgramaLinea.Visible = True
                lblProgramaLinea.Text = dtInfo.Rows(0)("ProgramaPresupuestal").ToString()

                'COMPONENTE
                pnlComponenteLinea.Visible = True
                lblComponenteLinea.Text = dtInfo.Rows(0)("Componente").ToString()


                'CARGA METAS BENEFICIADOS
                Dim paramsMetas() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@Linea", ddlLinea.SelectedValue)
            }
                'metas
                Dim dtMetas As DataTable = data.ObtieneDatos("CargaMetasBeneficiadosLineas", paramsMetas).Tables(0)
                txtMeta1.Text = dtMetas.Rows(0)("Meta")
                txtMeta2.Text = dtMetas.Rows(1)("Meta")
                txtMeta3.Text = dtMetas.Rows(2)("Meta")
                txtMeta4.Text = dtMetas.Rows(3)("Meta")
                txtMeta5.Text = dtMetas.Rows(4)("Meta")
                txtMeta6.Text = dtMetas.Rows(5)("Meta")
                txtMeta7.Text = dtMetas.Rows(6)("Meta")
                txtMeta8.Text = dtMetas.Rows(7)("Meta")
                txtMeta9.Text = dtMetas.Rows(8)("Meta")
                txtMeta10.Text = dtMetas.Rows(9)("Meta")
                txtMeta11.Text = dtMetas.Rows(10)("Meta")
                txtMeta12.Text = dtMetas.Rows(11)("Meta")

                'beneficiados
                txtBenH1.Text = dtMetas.Rows(0)("Beneficiadios_Hombre")
                txtBenH2.Text = dtMetas.Rows(1)("Beneficiadios_Hombre")
                txtBenH3.Text = dtMetas.Rows(2)("Beneficiadios_Hombre")
                txtBenH4.Text = dtMetas.Rows(3)("Beneficiadios_Hombre")
                txtBenH5.Text = dtMetas.Rows(4)("Beneficiadios_Hombre")
                txtBenH6.Text = dtMetas.Rows(5)("Beneficiadios_Hombre")
                txtBenH7.Text = dtMetas.Rows(6)("Beneficiadios_Hombre")
                txtBenH8.Text = dtMetas.Rows(7)("Beneficiadios_Hombre")
                txtBenH9.Text = dtMetas.Rows(8)("Beneficiadios_Hombre")
                txtBenH10.Text = dtMetas.Rows(9)("Beneficiadios_Hombre")
                txtBenH11.Text = dtMetas.Rows(10)("Beneficiadios_Hombre")
                txtBenH12.Text = dtMetas.Rows(11)("Beneficiadios_Hombre")

                'mujeres
                txtBenM1.Text = dtMetas.Rows(0)("Beneficiados_Mujeres")
                txtBenM2.Text = dtMetas.Rows(1)("Beneficiados_Mujeres")
                txtBenM3.Text = dtMetas.Rows(2)("Beneficiados_Mujeres")
                txtBenM4.Text = dtMetas.Rows(3)("Beneficiados_Mujeres")
                txtBenM5.Text = dtMetas.Rows(4)("Beneficiados_Mujeres")
                txtBenM6.Text = dtMetas.Rows(5)("Beneficiados_Mujeres")
                txtBenM7.Text = dtMetas.Rows(6)("Beneficiados_Mujeres")
                txtBenM8.Text = dtMetas.Rows(7)("Beneficiados_Mujeres")
                txtBenM9.Text = dtMetas.Rows(8)("Beneficiados_Mujeres")
                txtBenM10.Text = dtMetas.Rows(9)("Beneficiados_Mujeres")
                txtBenM11.Text = dtMetas.Rows(10)("Beneficiados_Mujeres")
                txtBenM12.Text = dtMetas.Rows(11)("Beneficiados_Mujeres")

                'Carga titulo
                lblTituloMetaLineas.Text = String.Format("<h2>Metas de la Actividad: <b>{0}</b></h2><br />", ddlLinea.SelectedItem.Text)
            Catch ex As Exception
                pnlIndicadorLinea.Visible = False
                pnlIndicadorLinea.Visible = False
            End Try

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

            btnMetasLinea.Visible = True

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
        hdnSubActividadNombre.Value = e.CommandArgument.ToString().Split("|")(2)
        'lblNombreTareaActual.Text = tarea
        lblMesTareaActual.Text = mes
        If e.CommandName = "materiales" Then
            Dim idSubActividad As Integer = e.CommandArgument.ToString().Split("|")(0)
            lblIdSubActividad.Text = idSubActividad
            grdMateriales.PageIndex = 0
            ddlMaterial.SelectedIndex = 0
            txtCantidadMaterial.Text = ""
            RecargaMateriales(idSubActividad, mes)
            lblTituloMateriales.Text = String.Format("<h2>Materiales de la Subactividad: <b>{0}</b></h2><br />", e.CommandArgument.ToString().Split("|")(2))
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_material", "abreModalMateriales();", True)
        ElseIf e.CommandName = "personas" Then
            Dim idSubActividad As Integer = e.CommandArgument.ToString().Split("|")(0)
            lblIdSubActividad.Text = idSubActividad
            CargaPersonasDependientes(idSubActividad, mes)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_personas", "abreModalPersonas();", True)
        ElseIf e.CommandName = "vehiculos" Then
            CargaVehiculos(tarea, mes)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_personas", "abreModalVehiculo();", True)
        ElseIf e.CommandName = "beneficiados" Then
            Dim idSubActividad As Integer = e.CommandArgument.ToString().Split("|")(0)
            lblIdSubActividad.Text = idSubActividad
            ddlColonias.SelectedIndex = 0
            ddlColonias.Enabled = True
            chkTodasColonias.Checked = False
            txtHombresBeneficiados.Text = ""
            txtMujeresBeneficiadas.Text = ""
            grdColoniasAsignadas.PageIndex = 0
            CargaBeneficiados(idSubActividad, mes)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_personas", "abreModalBeneficiados();", True)
        ElseIf e.CommandName = "evento" Then
            btnLimpiar_Click(Nothing, Nothing)
            Dim Id As Integer = e.CommandArgument.ToString().Split("|")(0)
            lblIdSubActividad.Text = Id
            lblIdEvento.Text = Id
            lblTituloEvento.Text = String.Format("<h5>Eventos de la Subactividad: <b>{0}</b></h5>", e.CommandArgument.ToString().Split("|")(2))

            'ASIGNA VARIABLES
            hdnActividad.Value = e.CommandArgument.ToString().Split("|")(1)
            hdnDireccion.Value = ddlDireccion.SelectedValue
            hdnSecretaria.Value = ddlSecretaria.SelectedValue
            hdnSubActividad.Value = e.CommandArgument.ToString().Split("|")(0)
            hdnAdministracion.Value = ddlAdmon.SelectedValue
            hdnAnio.Value = ddlAnio.SelectedValue

            CargaEventosSubActividad(Id)
        ElseIf e.CommandName = "meta" Then
            Dim Id As Integer = e.CommandArgument.ToString().Split("|")(0)
            lblIdSubActividad.Text = Id
            lblTituloSubActividad.Text = String.Format("<h2>Metas de la Subactividad: <b>{0}</b></h2><br />", e.CommandArgument.ToString().Split("|")(1))
            CargaMetasBeneficiadosSubActividad(Id)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_personas", "abreModalBeneficiadosSubActividad();", True)
        End If
    End Sub



#Region "Metas Subactividad"

    Private Sub CargaMetasBeneficiadosSubActividad(idSubActividad As Integer)
        Using data As New DB(con.conectar())
            'CARGA METAS BENEFICIADOS
            Dim paramsMetas() As SqlParameter = New SqlParameter() _
    {
        New SqlParameter("@IdSubActividad", idSubActividad)
    }
            'metas
            Dim dtMetas As DataTable = data.ObtieneDatos("CargaMetasBeneficiadosSubActividad", paramsMetas).Tables(0)
            txtMetaSA1.Text = dtMetas.Rows(0)("Meta")
            txtMetaSA2.Text = dtMetas.Rows(1)("Meta")
            txtMetaSA3.Text = dtMetas.Rows(2)("Meta")
            txtMetaSA4.Text = dtMetas.Rows(3)("Meta")
            txtMetaSA5.Text = dtMetas.Rows(4)("Meta")
            txtMetaSA6.Text = dtMetas.Rows(5)("Meta")
            txtMetaSA7.Text = dtMetas.Rows(6)("Meta")
            txtMetaSA8.Text = dtMetas.Rows(7)("Meta")
            txtMetaSA9.Text = dtMetas.Rows(8)("Meta")
            txtMetaSA10.Text = dtMetas.Rows(9)("Meta")
            txtMetaSA11.Text = dtMetas.Rows(10)("Meta")
            txtMetaSA12.Text = dtMetas.Rows(11)("Meta")

            'beneficiados
            txtBenHSA1.Text = dtMetas.Rows(0)("Beneficiadios_Hombre")
            txtBenHSA2.Text = dtMetas.Rows(1)("Beneficiadios_Hombre")
            txtBenHSA3.Text = dtMetas.Rows(2)("Beneficiadios_Hombre")
            txtBenHSA4.Text = dtMetas.Rows(3)("Beneficiadios_Hombre")
            txtBenHSA5.Text = dtMetas.Rows(4)("Beneficiadios_Hombre")
            txtBenHSA6.Text = dtMetas.Rows(5)("Beneficiadios_Hombre")
            txtBenHSA7.Text = dtMetas.Rows(6)("Beneficiadios_Hombre")
            txtBenHSA8.Text = dtMetas.Rows(7)("Beneficiadios_Hombre")
            txtBenHSA9.Text = dtMetas.Rows(8)("Beneficiadios_Hombre")
            txtBenHSA10.Text = dtMetas.Rows(9)("Beneficiadios_Hombre")
            txtBenHSA11.Text = dtMetas.Rows(10)("Beneficiadios_Hombre")
            txtBenHSA12.Text = dtMetas.Rows(11)("Beneficiadios_Hombre")

            'mujeres
            txtBenMSA1.Text = dtMetas.Rows(0)("Beneficiados_Mujeres")
            txtBenMSA2.Text = dtMetas.Rows(1)("Beneficiados_Mujeres")
            txtBenMSA3.Text = dtMetas.Rows(2)("Beneficiados_Mujeres")
            txtBenMSA4.Text = dtMetas.Rows(3)("Beneficiados_Mujeres")
            txtBenMSA5.Text = dtMetas.Rows(4)("Beneficiados_Mujeres")
            txtBenMSA6.Text = dtMetas.Rows(5)("Beneficiados_Mujeres")
            txtBenMSA7.Text = dtMetas.Rows(6)("Beneficiados_Mujeres")
            txtBenMSA8.Text = dtMetas.Rows(7)("Beneficiados_Mujeres")
            txtBenMSA9.Text = dtMetas.Rows(8)("Beneficiados_Mujeres")
            txtBenMSA10.Text = dtMetas.Rows(9)("Beneficiados_Mujeres")
            txtBenMSA11.Text = dtMetas.Rows(10)("Beneficiados_Mujeres")
            txtBenMSA12.Text = dtMetas.Rows(11)("Beneficiados_Mujeres")

            lblIndicadorSA.Text = dtMetas.Rows(0)("Indicador")
        End Using
    End Sub

    Private Sub guardaMetasBeneficiadosSA(linea As String, meta As String, benH As String, benM As String, mes As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", linea),
                New SqlParameter("@Meta", meta),
                New SqlParameter("@Beneficiados", benH),
                New SqlParameter("@Beneficiadas", benM),
                New SqlParameter("@Mes", mes)
            }

            data.EjecutaCommand("GuardaMetasBeneficiadosSubActividad", params)
        End Using
    End Sub

    Protected Sub btnGuardaMetaSubActividad_Click(sender As Object, e As EventArgs)
        Try

            Dim sa As String = lblIdSubActividad.Text
            'ENERO
            guardaMetasBeneficiadosSA(sa, txtMetaSA1.Text, txtBenHSA1.Text, txtBenMSA1.Text, 1)

            'FEBRERO
            guardaMetasBeneficiadosSA(sa, txtMetaSA2.Text, txtBenHSA2.Text, txtBenMSA2.Text, 2)

            'MARZO
            guardaMetasBeneficiadosSA(sa, txtMetaSA3.Text, txtBenHSA3.Text, txtBenMSA3.Text, 3)

            'ABRIL
            guardaMetasBeneficiadosSA(sa, txtMetaSA4.Text, txtBenHSA4.Text, txtBenMSA4.Text, 4)

            'MAYO
            guardaMetasBeneficiadosSA(sa, txtMetaSA5.Text, txtBenHSA5.Text, txtBenMSA5.Text, 5)

            'JUNIO
            guardaMetasBeneficiadosSA(sa, txtMetaSA6.Text, txtBenHSA6.Text, txtBenMSA6.Text, 6)

            'JULIO
            guardaMetasBeneficiadosSA(sa, txtMetaSA7.Text, txtBenHSA7.Text, txtBenMSA7.Text, 7)

            'AGOSTO
            guardaMetasBeneficiadosSA(sa, txtMetaSA8.Text, txtBenHSA8.Text, txtBenMSA8.Text, 8)

            'SEPTIEMBRE
            guardaMetasBeneficiadosSA(sa, txtMetaSA9.Text, txtBenHSA9.Text, txtBenMSA9.Text, 9)

            'OCTUBRE
            guardaMetasBeneficiadosSA(sa, txtMetaSA10.Text, txtBenHSA10.Text, txtBenMSA10.Text, 10)

            'NOVIEMBRE
            guardaMetasBeneficiadosSA(sa, txtMetaSA11.Text, txtBenHSA11.Text, txtBenMSA11.Text, 11)

            'DICIEMBRE
            guardaMetasBeneficiadosSA(sa, txtMetaSA12.Text, txtBenHSA12.Text, txtBenMSA12.Text, 12)

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado Metas a la Sub Actividad {0} de la Actividad {1} perteneciente a la Secretaria {2} y Dependencia {3}", hdnSubActividadNombre.Value, ddlLinea.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDireccion.SelectedItem.Text))
#End Region

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "MATERIALES"

    Private Sub RecargaMateriales(idSubActividad As String, mes As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdSubActividad", idSubActividad),
            New SqlParameter("@Mes", mes)
        }
            grdMateriales.DataSource = data.ObtieneDatos("ObtieneMaterialSubActividad", params)
            grdMateriales.DataBind()

        End Using

        ActualizaPresupuestoDependencia()
    End Sub

    Private Sub ActualizaPresupuestoDependencia()
        Using data As New DB(con.conectar())

            'PRESUPUESTO
            Dim params2() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue)
        }
            Dim dt = data.ObtieneDatos("ObtienePresupuestoDependencia", params2).Tables(0)
            lblPresupuesto.Text = String.Format("{0:c2}", dt.Rows(0)("Presupuesto"))
            lblPresupuestoCapturado.Text = String.Format("{0:c2}", dt.Rows(0)("Capturado"))
            lblPresupuestoDiferencia.Text = String.Format("{0:c2}", dt.Rows(0)("Diferencia"))

        End Using
    End Sub

    Protected Sub btnAgregaMaterial_Click(sender As Object, e As EventArgs)
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkTodosMesesMateriales.Checked Then
            For i As Integer = 0 To ddlMesMateriales.Items.Count - 1
                AgregaMaterial(idSubActividad, ddlMes.Items(i).Value)
            Next
        Else
            AgregaMaterial(idSubActividad, mes)
        End If

        'Limpia campos materiales
        ddlMaterial.SelectedIndex = 0
        txtCantidadMaterial.Text = ""

#Region "GUARDA LOG"
        Helper.GuardaLog(Session("clave_empl"), String.Format("Le ha agregado Materiales a la Sub Actividad {0} de la Actividad {1} perteneciente a la Secretaria {2} y Dependencia {3}", hdnSubActividadNombre.Value, ddlLinea.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDireccion.SelectedItem.Text))
#End Region

        RecargaMateriales(idSubActividad, mes)
        ddlLinea_SelectedIndexChanged(Nothing, Nothing)

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "modal_material", "abreModalMateriales();", True)
    End Sub

    Private Sub AgregaMaterial(idSubactividad As String, mes As Integer)
        Dim valores = ddlMaterial.SelectedValue
        Dim tipo As Integer = Integer.Parse(valores.Split("|")(0))
        Dim id As Integer = Integer.Parse(valores.Split("|")(1))
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdSubActividad", idSubactividad),
            New SqlParameter("@Mes", mes),
            New SqlParameter("@Tipo", tipo),
            New SqlParameter("@IdReq", id),
            New SqlParameter("@Cantidad", txtCantidadMaterial.Text)
        }

            data.EjecutaCommand("AgregaMaterialSubActividad", params)
        End Using
    End Sub

    Protected Sub grdMateriales_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdColoniasAsignadas.PageIndex = e.NewPageIndex
        Dim idSubActividad As String = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text
        RecargaMateriales(idSubActividad, mes)
    End Sub

    Protected Sub ddlMesMateriales_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesMateriales.SelectedValue
        Dim idSubActividad As String = lblIdSubActividad.Text
        RecargaMateriales(idSubActividad, Integer.Parse(ddlMesMateriales.SelectedValue))
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

        Dim idSubActividad As String = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text
        RecargaMateriales(idSubActividad, mes)

        CargaPresupuesto()
    End Sub

#End Region

#Region "PERSONAS"

    Protected Sub chkMesPersonas_CheckedChanged(sender As Object, e As EventArgs)
        ddlMesPersonas.SelectedIndex = 0
        ddlMesPersonas.Enabled = Not chkMesPersonas.Checked
    End Sub

    Protected Sub ddlMesPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesPersonas.SelectedValue
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        CargaPersonasDependientes(idSubActividad, Integer.Parse(ddlMesPersonas.SelectedValue))
    End Sub

    Protected Sub btnGuardaPersonas_Click(sender As Object, e As EventArgs)
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesPersonas.Checked Then
            For i As Integer = 0 To ddlMesPersonas.Items.Count - 1
                GuardaPersona(idSubActividad, ddlMesPersonas.Items(i).Value)
            Next
        Else
            GuardaPersona(idSubActividad, mes)
        End If

        ddlLinea_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub GuardaPersona(idSubActividad As Integer, mes As Integer)
        For Each item As ListViewDataItem In lstPersonas.Items
            Dim clave_empl As Integer = DirectCast(item.FindControl("hdnClave"), HiddenField).Value
            Dim porcentaje As Integer = 0
            Integer.TryParse(DirectCast(item.FindControl("txtPorcentaje"), TextBox).Text, porcentaje)

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", idSubActividad),
                New SqlParameter("@Mes", mes),
                New SqlParameter("@Clave_Empl", clave_empl),
                New SqlParameter("@Horas", porcentaje)
            }
                data.EjecutaCommand("AgregaEmpleadoSubActividad", params)

            End Using
        Next

        CargaPersonasDependientes(idSubActividad, mes)
    End Sub

    Private Sub CargaPersonasDependientes(idSubActividad As String, mes As Integer)
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
                New SqlParameter("@IdSubActividad", idSubActividad),
                New SqlParameter("@Mes", mes)
            }

            lstPersonas.DataSource = data.ObtieneDatos("ObtieneEmpleadosSubActividad", params)
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
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesesVehiculos.Checked Then
            For i As Integer = 0 To ddlMesVehiculo.Items.Count - 1
                AgregaVehiculos(idSubActividad, ddlMesVehiculo.Items(i).Value)
            Next
        Else
            AgregaVehiculos(idSubActividad, mes)
        End If

        ddlLinea_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub AgregaVehiculos(idSubActividad As Integer, mes As Integer)
        For Each item As ListViewDataItem In lstVehiculos.Items
            Dim vehiculo As Integer = DirectCast(item.FindControl("hdnIdVehiculo"), HiddenField).Value
            Dim kilometros As Integer = 0
            Integer.TryParse(DirectCast(item.FindControl("txtPorcentajeVehiculo"), TextBox).Text, kilometros)

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", idSubActividad),
                New SqlParameter("@Mes", mes),
                New SqlParameter("@IdVehiculo", vehiculo),
                New SqlParameter("@Kilometros", kilometros)
            }
                data.EjecutaCommand("AgregaVehiculoSubActividad", params)
                CargaVehiculos(idSubActividad, mes)
            End Using
        Next
    End Sub

    Protected Sub ddlMesVehiculo_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesVehiculo.SelectedValue
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        CargaVehiculos(idSubActividad, Integer.Parse(ddlMesVehiculo.SelectedValue))
    End Sub

    Protected Sub chkMesesVehiculos_CheckedChanged(sender As Object, e As EventArgs)
        ddlMesVehiculo.SelectedIndex = 0
        ddlMesVehiculo.Enabled = Not chkMesesVehiculos.Checked
    End Sub

#End Region

#Region "BENEFICIADOS"

    Protected Sub ddlMesBeneficiados_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblMesTareaActual.Text = ddlMesBeneficiados.SelectedValue
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        CargaBeneficiados(idSubActividad, Integer.Parse(ddlMesBeneficiados.SelectedValue))
    End Sub

    Private Sub CargaBeneficiados(idSubActividad As Integer, mes As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", idSubActividad),
                New SqlParameter("@Mes", mes)
            }
            Dim ds As DataSet = data.ObtieneDatos("ObtieneBeneficiados", params)
            Try
                txtHombresBeneficiados.Text = ds.Tables(0).Rows(0)("Beneficiados_Hombre")
                txtMujeresBeneficiadas.Text = ds.Tables(0).Rows(0)("Beneficiados_Mujeres")
            Catch ex As Exception
                txtHombresBeneficiados.Text = 0
                txtMujeresBeneficiadas.Text = 0
            End Try

            Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", idSubActividad),
                New SqlParameter("@Mes", mes)
            }

            grdColoniasAsignadas.DataSource = data.ObtieneDatos("ObtieneColoniasBeneficiados", params2)
            grdColoniasAsignadas.DataBind()
        End Using
    End Sub

    Protected Sub btnGuardaBeneficiados_Click(sender As Object, e As EventArgs)
        Dim idSubActividad As String = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesBeneficiados.Checked Then
            For i As Integer = 0 To ddlMesBeneficiados.Items.Count - 1
                GuardaBeneficiados(idSubActividad, ddlMesBeneficiados.Items(i).Value)
            Next
        Else
            GuardaBeneficiados(idSubActividad, mes)
        End If
    End Sub

    Private Sub GuardaBeneficiados(idSubActividad As String, mes As Integer)
        Dim hombres As Integer = 0
        Integer.TryParse(txtHombresBeneficiados.Text, hombres)

        Dim mujeres As Integer = 0
        Integer.TryParse(txtMujeresBeneficiadas.Text, mujeres)

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", idSubActividad),
                New SqlParameter("@Mes", mes),
                New SqlParameter("@Hombres", hombres),
                New SqlParameter("@Mujeres", mujeres)
            }

            data.EjecutaCommand("ActualizaBeneficiados", params)

        End Using
    End Sub

    Protected Sub btnAgregaColonias_Click(sender As Object, e As EventArgs)
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text

        If chkMesBeneficiados.Checked Then
            For i As Integer = 0 To ddlMesBeneficiados.Items.Count - 1
                GuardaColonias(idSubActividad, ddlMesBeneficiados.Items(i).Value)
            Next
        Else
            GuardaColonias(idSubActividad, mes)
        End If

        CargaBeneficiados(idSubActividad, mes)
    End Sub

    Private Sub GuardaColonias(idSubActividad As Integer, mes As Integer)
        Dim claveCol As Integer = Integer.Parse(ddlColonias.SelectedValue)
        Dim todasColonias As Boolean = chkTodasColonias.Checked

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", idSubActividad),
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
        Dim idSubActividad As Integer = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text
        CargaBeneficiados(idSubActividad, mes)
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

        Dim idSubActividad As Integer = lblIdSubActividad.Text
        Dim mes As String = lblMesTareaActual.Text
        CargaBeneficiados(idSubActividad, mes)
    End Sub

#End Region

#Region "EVENTOS"

    Protected Sub lnkRecargaMaterialesEvento_Click(sender As Object, e As EventArgs)
        Dim idEvento As Integer = 0
        Try
            Integer.TryParse(Session("IdEventoSeleccionado").ToString(), idEvento)
        Catch ex As Exception
        End Try

        'Dim abierto As Boolean = False
        'abierto = ddlTipoEvento.SelectedValue.ToString() Is "1"
        'Dim aforo As Decimal = 0
        'Try
        '    Decimal.TryParse(txtAforoEstimado.Text, aforo)
        'Catch ex As Exception
        'End Try

        'Using data As New DB(con.conectar())
        '    Dim params() As SqlParameter = New SqlParameter() _
        '    {
        '        New SqlParameter("@Folio", idEvento),
        '        New SqlParameter("@AforoEstimado", aforo)
        '    }

        'grdMaterialesEvento.DataSource = data.ObtieneDatos("CargaMaterialesPorTipoEvento", params)
        'grdMaterialesEvento.DataBind()

        'End Using

        ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "modal_evento", "MuestraEditarEvento();", True)
    End Sub

    Protected Sub btnGuardaEvento_Click(sender As Object, e As EventArgs)
        Dim idEvento As Integer = 0
        Try
            Integer.TryParse(Session("IdEventoSeleccionado").ToString(), idEvento)
        Catch ex As Exception
        End Try

        ''Guarda evento
        'Dim provider As CultureInfo = New System.Globalization.CultureInfo("es-MX")
        'If ddlFrecuencia.SelectedValue = 1 Then
        '    'UNA VEZ
        '    Dim fechaEvento As DateTime = DateTime.ParseExact(txtFechaEvento.Text, "dd/MM/yyyy", provider)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaEvento, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        'ElseIf ddlFrecuencia.SelectedValue = 2 Then
        '    'SEMANAL
        '    Dim fechaIni As DateTime = DateTime.ParseExact(txtFechaInicioSemanal.Text, "dd/MM/yyyy", provider)
        '    Dim fechaFin As DateTime = DateTime.ParseExact(txtFechaFinSemanal.Text, "dd/MM/yyyy", provider)
        '    While fechaIni <= fechaFin
        '        If (chkLunes.Checked And fechaIni.DayOfWeek = DayOfWeek.Monday) _
        '            Or (chkMartes.Checked And fechaIni.DayOfWeek = DayOfWeek.Tuesday) _
        '            Or (chkMartes.Checked And fechaIni.DayOfWeek = DayOfWeek.Wednesday) _
        '            Or (chkMartes.Checked And fechaIni.DayOfWeek = DayOfWeek.Thursday) _
        '            Or (chkMartes.Checked And fechaIni.DayOfWeek = DayOfWeek.Friday) _
        '            Or (chkMartes.Checked And fechaIni.DayOfWeek = DayOfWeek.Saturday) _
        '            Or (chkMartes.Checked And fechaIni.DayOfWeek = DayOfWeek.Sunday) Then
        '            GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaIni, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '        End If

        '        fechaIni = fechaIni.AddDays(1)
        '    End While
        'ElseIf ddlFrecuencia.SelectedValue = 3 Then
        '    'Enero
        '    Dim fechaMensual As DateTime = New DateTime(2020, 1, ddlMensualEnero.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Febrero
        '    fechaMensual = New DateTime(2020, 2, ddlMensualFebrero.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Marzo
        '    fechaMensual = New DateTime(2020, 3, ddlMensualMarzo.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Abril
        '    fechaMensual = New DateTime(2020, 4, ddlMensualAbril.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Mayo
        '    fechaMensual = New DateTime(2020, 5, ddlMensualMayo.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Junio
        '    fechaMensual = New DateTime(2020, 6, ddlMensualJunio.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Julio
        '    fechaMensual = New DateTime(2020, 7, ddlMensualJulio.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Agosto
        '    fechaMensual = New DateTime(2020, 8, ddlMensualAgosto.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Septiembre
        '    fechaMensual = New DateTime(2020, 9, ddlMensualSeptiembre.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Octubre
        '    fechaMensual = New DateTime(2020, 10, ddlMensualOctubre.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Noviembre
        '    fechaMensual = New DateTime(2020, 11, ddlMensualNoviembre.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        '    'Diciembre
        '    fechaMensual = New DateTime(2020, 12, ddlMensualDiciembre.SelectedValue)
        '    GuardaEvento(idEvento, lblIdSubActividad.Text, txtNombreEvento.Text, fechaMensual, txtAforoEstimado.Text, ddlTipoEvento.SelectedValue = "1")
        'End If

        btnLimpiar_Click(Nothing, Nothing)

        CargaEventosSubActividad(Integer.Parse(lblIdSubActividad.Text))

        'Vuelva  cargar las tareas
        ddlLinea_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Function GuardaEvento(idEvento As Integer, idSubActividad As Integer, nombre As String, fecha As DateTime, aforo As Integer, aireLibre As Boolean) As Integer
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdEvento", idEvento),
                New SqlParameter("@IdSubActividad", idSubActividad),
                New SqlParameter("@Nombre", nombre),
                New SqlParameter("@Fecha", fecha),
                New SqlParameter("@Aforo", aforo),
                New SqlParameter("@AireLibre", aireLibre)}
            'New SqlParameter("@TipoEvento", ddlTipoEvento2.SelectedValue) }

            idEvento = data.ObtieneDatos("GuardaEvento", params).Tables(0).Rows(0)(0)
            Session("IdEventoSeleccionado") = idEvento

        End Using

        'For Each row As GridViewRow In grdMaterialesEvento.Rows
        '    Dim id As Integer = DirectCast(row.FindControl("hdnIdMaterial"), HiddenField).Value
        '    Dim cantidad As Decimal = DirectCast(row.FindControl("txtCantidad"), TextBox).Text
        '    GuardaMaterialEvento(idEvento, id, cantidad)
        'Next
    End Function

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
        'grdMaterialesEvento.DataSource = Nothing
        'grdMaterialesEvento.DataBind()
        Session("IdEventoSeleccionado") = 0
        muestraInformacionFrecuencia()
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
                'txtNombreEvento.Text = dt.Rows(0)("Nombre")
                'txtFechaEvento.Text = dt.Rows(0)("Fecha")
                'txtAforoEstimado.Text = dt.Rows(0)("Aforo")
                'Dim aireLibre As Boolean = dt.Rows(0)("AireLibre")
                'If aireLibre Then
                '    ddlTipoEvento.SelectedValue = "1"
                'Else
                '    ddlTipoEvento.SelectedValue = "2"
                'End If
            Catch ex As Exception
                'txtNombreEvento.Text = ""
                'txtFechaEvento.Text = ""
                'txtAforoEstimado.Text = ""
                'ddlTipoEvento.SelectedIndex = 0
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
        'txtNombreEvento.Text = ""
        'txtFechaEvento.Text = ""
        'txtAforoEstimado.Text = ""
        'ddlTipoEvento.SelectedIndex = 0

    End Sub

    Protected Sub btnGuardaMetas_Click(sender As Object, e As EventArgs)
        Try

            Dim linea As String = ddlLinea.SelectedValue
            'ENERO
            guardaMetasBeneficiados(linea, txtMeta1.Text, txtBenH1.Text, txtBenM1.Text, 1)

            'FEBRERO
            guardaMetasBeneficiados(linea, txtMeta2.Text, txtBenH2.Text, txtBenM2.Text, 2)

            'MARZO
            guardaMetasBeneficiados(linea, txtMeta3.Text, txtBenH3.Text, txtBenM3.Text, 3)

            'ABRIL
            guardaMetasBeneficiados(linea, txtMeta4.Text, txtBenH4.Text, txtBenM4.Text, 4)

            'MAYO
            guardaMetasBeneficiados(linea, txtMeta5.Text, txtBenH5.Text, txtBenM5.Text, 5)

            'JUNIO
            guardaMetasBeneficiados(linea, txtMeta6.Text, txtBenH6.Text, txtBenM6.Text, 6)

            'JULIO
            guardaMetasBeneficiados(linea, txtMeta7.Text, txtBenH7.Text, txtBenM7.Text, 7)

            'AGOSTO
            guardaMetasBeneficiados(linea, txtMeta8.Text, txtBenH8.Text, txtBenM8.Text, 8)

            'SEPTIEMBRE
            guardaMetasBeneficiados(linea, txtMeta9.Text, txtBenH9.Text, txtBenM9.Text, 9)

            'OCTUBRE
            guardaMetasBeneficiados(linea, txtMeta10.Text, txtBenH10.Text, txtBenM10.Text, 10)

            'NOVIEMBRE
            guardaMetasBeneficiados(linea, txtMeta11.Text, txtBenH11.Text, txtBenM11.Text, 11)

            'DICIEMBRE
            guardaMetasBeneficiados(linea, txtMeta12.Text, txtBenH12.Text, txtBenM12.Text, 12)

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado las Metas de la Actividad {0} perteneciente a la Secretaria {1} y Dependencia {2}", ddlLinea.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDireccion.SelectedItem.Text))
#End Region

        Catch ex As Exception

        End Try

    End Sub

    Private Sub guardaMetasBeneficiados(linea As String, meta As String, benH As String, benM As String, mes As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdLinea", linea),
                New SqlParameter("@Meta", meta),
                New SqlParameter("@Beneficiados", benH),
                New SqlParameter("@Beneficiadas", benM),
                New SqlParameter("@Mes", mes)
            }

            data.EjecutaCommand("GuardaMetasBeneficiadosLinea", params)
        End Using
    End Sub

    Private Sub LimpiaInformacionLineas()
        grdTareas.DataSource = Nothing
        grdTareas.DataBind()

        pnlComponenteLinea.Visible = False
        pnlIndicadorLinea.Visible = False
        pnlProgramaLinea.Visible = False
    End Sub
    Protected Sub ddlFrecuencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        muestraInformacionFrecuencia()
        ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "edita_evento_2", "MuestraEditarEvento();", True)
    End Sub

    Private Sub muestraInformacionFrecuencia()
        'pnlUnaVez.Visible = ddlFrecuencia.SelectedValue = "1"
        'pnlSemanal.Visible = ddlFrecuencia.SelectedValue = "2"
        'pnlMensual.Visible = ddlFrecuencia.SelectedValue = "3"
    End Sub
    Protected Sub btnCopiarMesSiguiente_Click(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSubActividad", lblIdSubActividad.Text),
                New SqlParameter("@Mes", ddlMesMateriales.SelectedValue)
            }

            data.EjecutaCommand("CopiaMaterialesSiguienteMes", params)
        End Using

        '
    End Sub

    Protected Sub btnRecargaEventos_Click(sender As Object, e As EventArgs)
        Try
            Dim idEvento = Integer.Parse(lblIdEvento.Text)
            CargaEventosSubActividad(idEvento)
        Catch ex As Exception

        End Try

    End Sub
End Class
