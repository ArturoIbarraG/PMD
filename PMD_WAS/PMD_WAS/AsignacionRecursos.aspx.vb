Imports System.Data.SqlClient
Imports System.Data
Partial Class AsignacionRecursos
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Asignación de recursos humanos")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Using data As New DB(con.conectar())
                'Carga Administracion
                ddlAdmon.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmon.DataTextField = "Nombr_admon"
                ddlAdmon.DataValueField = "Cve_admon"
                ddlAdmon.DataBind()
                ddlAdmon.Items.Insert(0, New ListItem("Selecciona la administración", "0"))
            End Using
            ddlAnio.Items.Insert(0, New ListItem("Selecciona el año", "0"))
            ddlAnio.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga secretarias
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaría", "0"))
    End Sub
    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlDependencia.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDependencia.DataTextField = "Nombr_direccion"
            ddlDependencia.DataValueField = "IdDireccion"
            ddlDependencia.DataBind()
        End Using
        ddlDependencia.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
    End Sub
    Protected Sub ddlDependencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlTipoAsignacion_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Protected Sub ddlTipoAsignacion_SelectedIndexChanged(sender As Object, e As EventArgs)

        If ddlTipoAsignacion.SelectedValue = 1 Then
            labelElementoAsignacion.Text = "Empleado:"
            'Asignacion por Empleado
            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue)
                }

                Dim dt As DataTable = data.ObtieneDatos("ObtieneEmpleadosAsignacion", params).Tables(0)
                Session("TableAsignar") = dt
                ddlElementoAsignar.DataSource = dt
                ddlElementoAsignar.DataTextField = "nombr_empl"
                ddlElementoAsignar.DataValueField = "clave_empl"
                ddlElementoAsignar.DataBind()

                Dim paramsP() As SqlParameter = New SqlParameter() _
              {
                   New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                  New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue)
              }

                ddlPuestoAgregar.DataSource = data.ObtieneDatos("ObtienePuestosAsignacion", paramsP).Tables(0)
                ddlPuestoAgregar.DataTextField = "nombr_cate"
                ddlPuestoAgregar.DataValueField = "nombr_cate"
                ddlPuestoAgregar.DataBind()

            End Using

            AsignacionPorPersona()
        Else
            labelElementoAsignacion.Text = "Puesto:"

            'Asignacion por Puesto
            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                     New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue)
                }

                Dim dt As DataTable = data.ObtieneDatos("ObtienePuestosAsignacion", params).Tables(0)
                Session("TableAsignar") = dt
                ddlElementoAsignar.DataSource = dt
                ddlElementoAsignar.DataTextField = "nombr_cate"
                ddlElementoAsignar.DataValueField = "nombr_cate"
                ddlElementoAsignar.DataBind()

                ddlPuestoAgregar.DataSource = dt
                ddlPuestoAgregar.DataTextField = "nombr_cate"
                ddlPuestoAgregar.DataValueField = "nombr_cate"
                ddlPuestoAgregar.DataBind()
            End Using

            AsignacionPorPuesto()
        End If

        pnlAsignacionRecursos.Visible = True
    End Sub
    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
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

            ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

            'Carga el año

            Dim params1() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue)
            }

            ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params1)
            ddlAnio.DataTextField = "Año"
            ddlAnio.DataValueField = "Año"
            ddlAnio.DataBind()

            ''Carga Meses
            'ddlMesAsignacionRecursos.DataSource = data.ObtieneDatos("ObtieneMeses", Nothing)
            'ddlMesAsignacionRecursos.DataTextField = "Mes"
            'ddlMesAsignacionRecursos.DataValueField = "Cve_mes"
            'ddlMesAsignacionRecursos.DataBind()
        End Using

    End Sub

    Private Sub AsignacionPorPersona()
        Dim dtAsignar As DataTable = DirectCast(Session("TableAsignar"), DataTable)
        Dim persona = ddlElementoAsignar.SelectedItem.Text
        Dim numEmpleado = ddlElementoAsignar.SelectedValue
        Dim puesto = dtAsignar.Select(String.Format("clave_empl = {0}", numEmpleado)).FirstOrDefault()("nombr_cate")

        lblElementoAsignar.Text = String.Format("<div class=""row""><div class=""col-4""><b>Núm. Nomina:</b> {1}</div><div class=""col-4""><b>Persona:</b> {0}</div><div class=""col-4""><b>Puesto:</b> {2}</div></div>", persona, numEmpleado, puesto)
        lblEmpleadosContador.Text = String.Format("{0} empleados en Total.", dtAsignar.Rows.Count)

        'Carga Líneas
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDependencia.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue)
            }

            Dim dt As DataTable = data.ObtieneDatos("ObtieneLineas", params).Tables(0)
            rptEmpleadoSubActividad.DataSource = dt
            rptEmpleadoSubActividad.DataBind()
        End Using
    End Sub

    Private Sub AsignacionPorPuesto()
        Dim puesto = ddlElementoAsignar.SelectedItem.Text
        lblElementoAsignar.Text = String.Format("<div class=""row""><div class=""col-12""><b>Puesto:</b> {0}</div></div>", puesto)

        'Carga Líneas
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDependencia.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue)
            }

            Dim dt As DataTable = data.ObtieneDatos("ObtieneLineas", params).Tables(0)
            rptEmpleadoSubActividad.DataSource = dt
            rptEmpleadoSubActividad.DataBind()
        End Using
    End Sub

    Protected Sub ddlElementoAsignar_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoAsignacion.SelectedValue = "1" Then
            AsignacionPorPersona()
        Else
            AsignacionPorPuesto()
        End If
    End Sub

    Protected Sub btnSiguiente_Click(sender As Object, e As EventArgs)
        Try
            ddlElementoAsignar.SelectedIndex = ddlElementoAsignar.SelectedIndex + 1
        Catch ex As Exception
            ddlElementoAsignar.SelectedIndex = 0
        End Try

        If ddlTipoAsignacion.SelectedValue = "1" Then
            AsignacionPorPersona()
        Else
            AsignacionPorPuesto()
        End If
    End Sub

    Protected Sub btnAnterior_Click(sender As Object, e As EventArgs)
        Try
            ddlElementoAsignar.SelectedIndex = ddlElementoAsignar.SelectedIndex - 1
        Catch ex As Exception
            ddlElementoAsignar.SelectedIndex = ddlElementoAsignar.Items.Count - 1
        End Try

        If ddlTipoAsignacion.SelectedValue = "1" Then
            AsignacionPorPersona()
        Else
            AsignacionPorPuesto()
        End If
    End Sub

    Protected Sub rptEmpleadoSubActividad_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Footer Then
                'If ddlFrecuenciaAsignacion.SelectedValue = "1" Then
                DirectCast(e.Item.FindControl("labelFooterAsignacionMensual"), Label).Text = "Porcentaje de <b>Horas</b> por <b>Mes</b>:"
                e.Item.FindControl("pnlFooterAnual").Visible = False
                'Else
                '    DirectCast(e.Item.FindControl("labelFooterAsignacionAnual"), Label).Text = "Porcentaje de <b>Horas</b> por <b>Año</b>:"
                '    e.Item.FindControl("pnlFooterMensual").Visible = False
                'End If
            Else

                Dim nombre As String = ""
                'If ddlFrecuenciaAsignacion.SelectedValue = "2" Then
                'nombre = "rptSubActividadesAños"
                '    e.Item.FindControl("pnlMensual").Visible = False
                'Else
                nombre = "rptSubActividadesMeses"
                e.Item.FindControl("pnlAnual").Visible = False
                'End If
                Dim rpt As Repeater = DirectCast(e.Item.FindControl(nombre), Repeater)
                Dim linea As String = DirectCast(e.Item.FindControl("hdnLinea"), HiddenField).Value

                If ddlTipoAsignacion.SelectedValue = "1" Then
                    'EMPLEADOS
                    Using data As New DB(con.conectar())
                        Dim params() As SqlParameter = New SqlParameter() _
                        {
                            New SqlParameter("@Linea", linea),
                            New SqlParameter("@ClaveEmpleado", ddlElementoAsignar.SelectedValue),
                            New SqlParameter("@Frecuencia", 1),
                            New SqlParameter("@IdAnio", ddlAnio.SelectedValue)
                        }

                        rpt.DataSource = data.ObtieneDatos("ObtieneAsignacionRecursosPorEmpleado", params)
                        rpt.DataBind()
                    End Using
                Else
                    'PUESTOS
                    Using data As New DB(con.conectar())
                        Dim params() As SqlParameter = New SqlParameter() _
                        {
                            New SqlParameter("@IdSecreataria", ddlSecretaria.SelectedValue),
                            New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue),
                            New SqlParameter("@Linea", linea),
                            New SqlParameter("@Puesto", ddlElementoAsignar.SelectedValue),
                            New SqlParameter("@Frecuencia", 1),
                            New SqlParameter("@IdAnio", ddlAnio.SelectedValue)
                        }

                        rpt.DataSource = data.ObtieneDatos("ObtieneAsignacionRecursosPorPuesto", params)
                        rpt.DataBind()
                    End Using
                End If

            End If
        Catch ex As Exception
            Dim er As String = ex.Message
        End Try

    End Sub

    Protected Sub rptSubActividades_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        Try
            If e.Item.ItemType = ListItemType.Footer Then
                Dim repeater As Repeater = DirectCast(e.Item.Parent, Repeater)
                Dim panel As Panel = DirectCast(e.Item.FindControl("pnlSinDatos"), Panel)
                panel.Visible = repeater.Items.Count = 0
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlFrecuenciaAsignacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlTipoAsignacion_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub btnGuardarCambios_Click(sender As Object, e As EventArgs)
        If ddlTipoAsignacion.SelectedValue = "1" Then
            'EMPLEADO
            For Each item As RepeaterItem In rptEmpleadoSubActividad.Items

                If Not (item.ItemType = ListItemType.Footer) Then

                    Dim linea As String = DirectCast(item.FindControl("hdnLinea"), HiddenField).Value
                    'If ddlFrecuenciaAsignacion.SelectedValue = "1" Then
                    'MENSUAL
                    Dim repeater As Repeater = DirectCast(item.FindControl("rptSubActividadesMeses"), Repeater)
                    For Each subItem As RepeaterItem In repeater.Items
                        Dim idSubActividad As Integer = DirectCast(subItem.FindControl("hdnSubActividad"), HiddenField).Value

                        'ENERO
                        Dim enero As Integer = DirectCast(subItem.FindControl("txt1"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 1, ddlElementoAsignar.SelectedValue, enero)

                        'FEBRERO
                        Dim febrero As Integer = DirectCast(subItem.FindControl("txt2"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 2, ddlElementoAsignar.SelectedValue, febrero)

                        'MARZO
                        Dim marzo As Integer = DirectCast(subItem.FindControl("txt3"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 3, ddlElementoAsignar.SelectedValue, marzo)

                        'ABRIL
                        Dim abril As Integer = DirectCast(subItem.FindControl("txt4"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 4, ddlElementoAsignar.SelectedValue, abril)

                        'MAYO
                        Dim mayo As Integer = DirectCast(subItem.FindControl("txt5"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 5, ddlElementoAsignar.SelectedValue, mayo)

                        'JUNIO
                        Dim junio As Integer = DirectCast(subItem.FindControl("txt6"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 6, ddlElementoAsignar.SelectedValue, junio)

                        'JULIO
                        Dim julio As Integer = DirectCast(subItem.FindControl("txt7"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 7, ddlElementoAsignar.SelectedValue, julio)

                        'AGOSTO
                        Dim agosto As Integer = DirectCast(subItem.FindControl("txt8"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 8, ddlElementoAsignar.SelectedValue, agosto)

                        'SEPTIEMBRE
                        Dim septiembre As Integer = DirectCast(subItem.FindControl("txt9"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 9, ddlElementoAsignar.SelectedValue, septiembre)

                        'OCTUBRE
                        Dim octubre As Integer = DirectCast(subItem.FindControl("txt10"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 10, ddlElementoAsignar.SelectedValue, octubre)

                        'NOVIEMBRE
                        Dim noviembre As Integer = DirectCast(subItem.FindControl("txt11"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 11, ddlElementoAsignar.SelectedValue, noviembre)

                        'DICIEMBRE
                        Dim diciembre As Integer = DirectCast(subItem.FindControl("txt12"), TextBox).Text
                        GuardaEmpleado(idSubActividad, 12, ddlElementoAsignar.SelectedValue, diciembre)
                    Next
                    'Else
                    '    'ANUAL
                    '    Dim repeater As Repeater = DirectCast(item.FindControl("rptSubActividadesAños"), Repeater)
                    '    For Each subItem As RepeaterItem In repeater.Items
                    '        Dim idSubActividad As Integer = DirectCast(subItem.FindControl("hdnSubActividad"), HiddenField).Value

                    '        'ENERO
                    '        Dim anual As Integer = DirectCast(subItem.FindControl("txtAnual"), TextBox).Text
                    '        GuardaEmpleado(idSubActividad, 1, ddlElementoAsignar.SelectedValue, anual)

                    '        'FEBRERO
                    '        GuardaEmpleado(idSubActividad, 2, ddlElementoAsignar.SelectedValue, anual)

                    '        'MARZO
                    '        GuardaEmpleado(idSubActividad, 3, ddlElementoAsignar.SelectedValue, anual)

                    '        'ABRIL
                    '        GuardaEmpleado(idSubActividad, 4, ddlElementoAsignar.SelectedValue, anual)

                    '        'MAYO
                    '        GuardaEmpleado(idSubActividad, 5, ddlElementoAsignar.SelectedValue, anual)

                    '        'JUNIO
                    '        GuardaEmpleado(idSubActividad, 6, ddlElementoAsignar.SelectedValue, anual)

                    '        'JULIO
                    '        GuardaEmpleado(idSubActividad, 7, ddlElementoAsignar.SelectedValue, anual)

                    '        'AGOSTO
                    '        GuardaEmpleado(idSubActividad, 8, ddlElementoAsignar.SelectedValue, anual)

                    '        'SEPTIEMBRE
                    '        GuardaEmpleado(idSubActividad, 9, ddlElementoAsignar.SelectedValue, anual)

                    '        'OCTUBRE
                    '        GuardaEmpleado(idSubActividad, 10, ddlElementoAsignar.SelectedValue, anual)

                    '        'NOVIEMBRE
                    '        GuardaEmpleado(idSubActividad, 11, ddlElementoAsignar.SelectedValue, anual)

                    '        'DICIEMBRE
                    '        GuardaEmpleado(idSubActividad, 12, ddlElementoAsignar.SelectedValue, anual)
                    '    Next
                    'End If

                End If
            Next

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado la asignacion de Recursos Humanos a el Empleado {0} con núm. de nómina {1} de la secretaria {2} y direccion {3}", ddlElementoAsignar.SelectedItem.Text, ddlElementoAsignar.SelectedValue, ddlSecretaria.SelectedItem.Text, ddlDependencia.SelectedItem.Text))
#End Region
        Else
            'PUESTO
            For Each item As RepeaterItem In rptEmpleadoSubActividad.Items

                If Not (item.ItemType = ListItemType.Footer) Then
                    Dim linea As String = DirectCast(item.FindControl("hdnLinea"), HiddenField).Value
                    Dim puesto As String = ddlElementoAsignar.SelectedValue
                    Dim dtPuestos As DataTable = DirectCast(Session("TableAsignar"), DataTable)
                    'If ddlFrecuenciaAsignacion.SelectedValue = "1" Then
                    'MENSUAL
                    Dim repeater As Repeater = DirectCast(item.FindControl("rptSubActividadesMeses"), Repeater)
                    For Each subItem As RepeaterItem In repeater.Items
                        Dim idSubActividad As Integer = DirectCast(subItem.FindControl("hdnSubActividad"), HiddenField).Value

                        'ENERO
                        Dim enero As Integer = DirectCast(subItem.FindControl("txt1"), TextBox).Text
                        GuardaPuesto(idSubActividad, 1, ddlElementoAsignar.SelectedValue, enero)

                        'FEBRERO
                        Dim febrero As Integer = DirectCast(subItem.FindControl("txt2"), TextBox).Text
                        GuardaPuesto(idSubActividad, 2, ddlElementoAsignar.SelectedValue, febrero)

                        'MARZO
                        Dim marzo As Integer = DirectCast(subItem.FindControl("txt3"), TextBox).Text
                        GuardaPuesto(idSubActividad, 3, ddlElementoAsignar.SelectedValue, marzo)

                        'ABRIL
                        Dim abril As Integer = DirectCast(subItem.FindControl("txt4"), TextBox).Text
                        GuardaPuesto(idSubActividad, 4, ddlElementoAsignar.SelectedValue, abril)

                        'MAYO
                        Dim mayo As Integer = DirectCast(subItem.FindControl("txt5"), TextBox).Text
                        GuardaPuesto(idSubActividad, 5, ddlElementoAsignar.SelectedValue, mayo)

                        'JUNIO
                        Dim junio As Integer = DirectCast(subItem.FindControl("txt6"), TextBox).Text
                        GuardaPuesto(idSubActividad, 6, ddlElementoAsignar.SelectedValue, junio)

                        'JULIO
                        Dim julio As Integer = DirectCast(subItem.FindControl("txt7"), TextBox).Text
                        GuardaPuesto(idSubActividad, 7, ddlElementoAsignar.SelectedValue, julio)

                        'AGOSTO
                        Dim agosto As Integer = DirectCast(subItem.FindControl("txt8"), TextBox).Text
                        GuardaPuesto(idSubActividad, 8, ddlElementoAsignar.SelectedValue, agosto)

                        'SEPTIEMBRE
                        Dim septiembre As Integer = DirectCast(subItem.FindControl("txt9"), TextBox).Text
                        GuardaPuesto(idSubActividad, 9, ddlElementoAsignar.SelectedValue, septiembre)

                        'OCTUBRE
                        Dim octubre As Integer = DirectCast(subItem.FindControl("txt10"), TextBox).Text
                        GuardaPuesto(idSubActividad, 10, ddlElementoAsignar.SelectedValue, octubre)

                        'NOVIEMBRE
                        Dim noviembre As Integer = DirectCast(subItem.FindControl("txt11"), TextBox).Text
                        GuardaPuesto(idSubActividad, 11, ddlElementoAsignar.SelectedValue, noviembre)

                        'DICIEMBRE
                        Dim diciembre As Integer = DirectCast(subItem.FindControl("txt12"), TextBox).Text
                        GuardaPuesto(idSubActividad, 12, ddlElementoAsignar.SelectedValue, diciembre)
                    Next
                    'Else
                    '    'ANUAL
                    '    Dim repeater As Repeater = DirectCast(item.FindControl("rptSubActividadesAños"), Repeater)
                    '    For Each subItem As RepeaterItem In repeater.Items
                    '        Dim idSubActividad As Integer = DirectCast(subItem.FindControl("hdnSubActividad"), HiddenField).Value

                    '        'ENERO
                    '        Dim anual As Integer = DirectCast(subItem.FindControl("txtAnual"), TextBox).Text
                    '        GuardaPuesto(idSubActividad, 1, ddlElementoAsignar.SelectedValue, anual)

                    '        'FEBRERO
                    '        GuardaPuesto(idSubActividad, 2, ddlElementoAsignar.SelectedValue, anual)

                    '        'MARZO
                    '        GuardaPuesto(idSubActividad, 3, ddlElementoAsignar.SelectedValue, anual)

                    '        'ABRIL
                    '        GuardaPuesto(idSubActividad, 4, ddlElementoAsignar.SelectedValue, anual)

                    '        'MAYO
                    '        GuardaPuesto(idSubActividad, 5, ddlElementoAsignar.SelectedValue, anual)

                    '        'JUNIO
                    '        GuardaPuesto(idSubActividad, 6, ddlElementoAsignar.SelectedValue, anual)

                    '        'JULIO
                    '        GuardaPuesto(idSubActividad, 7, ddlElementoAsignar.SelectedValue, anual)

                    '        'AGOSTO
                    '        GuardaPuesto(idSubActividad, 8, ddlElementoAsignar.SelectedValue, anual)

                    '        'SEPTIEMBRE
                    '        GuardaPuesto(idSubActividad, 9, ddlElementoAsignar.SelectedValue, anual)

                    '        'OCTUBRE
                    '        GuardaPuesto(idSubActividad, 10, ddlElementoAsignar.SelectedValue, anual)

                    '        'NOVIEMBRE
                    '        GuardaPuesto(idSubActividad, 11, ddlElementoAsignar.SelectedValue, anual)

                    '        'DICIEMBRE
                    '        GuardaPuesto(idSubActividad, 12, ddlElementoAsignar.SelectedValue, anual)
                    '    Next
                    'End If
                End If

            Next

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado la asignacion de Recursos Humanos a el Puesto {0} de la secretaria {1} y direccion {2}", ddlElementoAsignar.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDependencia.SelectedItem.Text))
#End Region
        End If

        ScriptManager.RegisterStartupScript(updAgregarEmpleado, updAgregarEmpleado.GetType(), "quitar_validar_cambios", "quitaValidarCambios();", True)
    End Sub

    Private Sub GuardaEmpleado(idSubActividad As Integer, mes As Integer, clave_empl As Integer, porcentaje As Integer)
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
    End Sub

    Private Sub GuardaPuesto(idSubActividad As Integer, mes As Integer, puesto As String, porcentaje As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdSecreataria", idSubActividad),
            New SqlParameter("@IdDependencia", idSubActividad),
            New SqlParameter("@IdSubActividad", idSubActividad),
            New SqlParameter("@Mes", mes),
            New SqlParameter("@Puesto", puesto),
            New SqlParameter("@Porcentaje", porcentaje)
        }
            data.EjecutaCommand("AgregaPuestoSubActividad", params)

        End Using
    End Sub

    Protected Sub btnAgregarEmpleados_Click(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@Secretaria", ddlSecretaria.SelectedValue),
            New SqlParameter("@Dependencia", ddlDependencia.SelectedValue),
            New SqlParameter("@Puesto", ddlPuestoAgregar.SelectedValue),
            New SqlParameter("@Cantidad", txtCantidadEmpleados.Text),
            New SqlParameter("@Modalidad", ddlModalidad.SelectedItem.Text)
        }
            data.EjecutaCommand("SolicitaRecursoHumano", params)

        End Using

#Region "GUARDA LOG"
        Helper.GuardaLog(Session("clave_empl"), String.Format("Ha solicitado {0} nuevo(s) lugares para el Puesto {1} con modalidad {2}, para la dirección {3} de la secretaría {4}", txtCantidadEmpleados.Text, ddlPuestoAgregar.SelectedItem.Text, ddlModalidad.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDependencia.SelectedItem.Text))
#End Region

        ddlPuestoAgregar.SelectedIndex = 0
        txtCantidadEmpleados.Text = ""
        ddlModalidad.SelectedIndex = 0
        ddlTipoAsignacion_SelectedIndexChanged(Nothing, Nothing)
        ScriptManager.RegisterStartupScript(updAgregarEmpleado, updAgregarEmpleado.GetType(), "ocultaModal", "ocultaModalSolicitarRecurso();", True)
    End Sub
End Class
