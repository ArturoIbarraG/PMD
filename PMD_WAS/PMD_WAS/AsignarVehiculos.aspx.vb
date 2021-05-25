Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class AsignarVehiculos
    Inherits System.Web.UI.Page

    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Asignación de vehiculos")
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
            labelElementoAsignacion.Text = "Vehiculo:"
            'Asignacion por Empleado
            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue)
                }

                Dim dt As DataTable = data.ObtieneDatos("ObtieneVehiculosAsignacion", params).Tables(0)
                Session("TableAsignar") = dt
                ddlElementoAsignar.DataSource = dt
                ddlElementoAsignar.DataTextField = "Nombre"
                ddlElementoAsignar.DataValueField = "llave_vehi"
                ddlElementoAsignar.DataBind()

                Dim paramsP() As SqlParameter = New SqlParameter() _
              {
                   New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                  New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue)
              }

                'ddlTipoUsoAgregar.DataSource = dt.DefaultView.ToTable(True, "tipo_uso")
                'ddlTipoUsoAgregar.DataTextField = "tipo_uso"
                'ddlTipoUsoAgregar.DataValueField = "tipo_uso"
                'ddlTipoUsoAgregar.DataBind()

            End Using

            AsignacionPorVehiculo()
        Else
            labelElementoAsignacion.Text = "Tipo de uso:"

            'Asignacion por Tipo de uso
            Using data As New DB(con.conectar())

                ddlElementoAsignar.DataSource = data.ObtieneDatos("ObtieneTipoAsignacion", Nothing).Tables(0)
                ddlElementoAsignar.DataTextField = "tipo_uso"
                ddlElementoAsignar.DataValueField = "tipo_uso"
                ddlElementoAsignar.DataBind()

                'ddlTipoUsoAgregar.DataSource = dt.DefaultView.ToTable(True, "tipo_uso")
                'ddlTipoUsoAgregar.DataTextField = "tipo_uso"
                'ddlTipoUsoAgregar.DataValueField = "tipo_uso"
                'ddlTipoUsoAgregar.DataBind()
            End Using

            AsignacionPorTipoUso()
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

    Protected Sub btnSiguiente_Click(sender As Object, e As EventArgs)
        Try
            ddlElementoAsignar.SelectedIndex = ddlElementoAsignar.SelectedIndex + 1
        Catch ex As Exception
            ddlElementoAsignar.SelectedIndex = 0
        End Try

        If ddlTipoAsignacion.SelectedValue = "1" Then
            AsignacionPorVehiculo()
        Else
            AsignacionPorTipoUso()
        End If
    End Sub

    Protected Sub btnAnterior_Click(sender As Object, e As EventArgs)
        Try
            ddlElementoAsignar.SelectedIndex = ddlElementoAsignar.SelectedIndex - 1
        Catch ex As Exception
            ddlElementoAsignar.SelectedIndex = ddlElementoAsignar.Items.Count - 1
        End Try

        If ddlTipoAsignacion.SelectedValue = "1" Then
            AsignacionPorVehiculo()
        Else
            AsignacionPorTipoUso()
        End If
    End Sub

    Private Sub AsignacionPorVehiculo()
        Dim dtAsignar As DataTable = DirectCast(Session("TableAsignar"), DataTable)
        Dim vehiculo = ddlElementoAsignar.SelectedItem.Text
        Dim llave = ddlElementoAsignar.SelectedValue
        Dim tipoUso = dtAsignar.Select(String.Format("llave_vehi = {0}", llave)).FirstOrDefault()("tipo_uso")
        Dim marca = dtAsignar.Select(String.Format("llave_vehi = {0}", llave)).FirstOrDefault()("marca_vehi")
        Dim placa = dtAsignar.Select(String.Format("llave_vehi = {0}", llave)).FirstOrDefault()("placa_vehi")
        lblElementoAsignar.Text = String.Format("<div class=""row""><div class=""col-4""><b>Placas:</b> {1}</div><div class=""col-4""><b>Vehiculo:</b> {0}</div><div class=""col-4""><b>Tipo uso:</b> {2}</div></div>", vehiculo, placa, tipoUso)

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
            rptVehiculoSubActividad.DataSource = dt
            rptVehiculoSubActividad.DataBind()
        End Using
    End Sub

    Private Sub AsignacionPorTipoUso()
        Dim tipoUso = ddlElementoAsignar.SelectedItem.Text
        lblElementoAsignar.Text = String.Format("<div class=""row""><div class=""col-12""><b>Tipo uso:</b> {0}</div></div>", tipoUso)

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
            rptVehiculoSubActividad.DataSource = dt
            rptVehiculoSubActividad.DataBind()
        End Using
    End Sub

    Protected Sub btnAgregaVehiculo_Click(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@Secretaria", ddlSecretaria.SelectedValue),
            New SqlParameter("@Dependencia", ddlDependencia.SelectedValue),
            New SqlParameter("@TipoUso", ddlTipoUsoAgregar.SelectedItem.Text),
            New SqlParameter("@Cantidad", txtCantidadVehiculos.Text)
        }
            data.EjecutaCommand("SolicitaRecursoVehiculo", params)

        End Using

#Region "GUARDA LOG"
        Helper.GuardaLog(Session("clave_empl"), String.Format("Ha solicitado {0} nuevo(s) vehiculos para uso {1} perteneciente a la direccion {3} de la secretaria {2}", txtCantidadVehiculos.Text, ddlElementoAsignar.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDependencia.SelectedItem.Text))
#End Region

        ddlTipoUsoAgregar.SelectedIndex = 0
        txtCantidadVehiculos.Text = ""
        ddlTipoAsignacion_SelectedIndexChanged(Nothing, Nothing)
        ScriptManager.RegisterStartupScript(updAgregaVehiculo, updAgregaVehiculo.GetType(), "ocultaModal", "ocultaModalSolicitarRecurso();", True)
    End Sub
    Protected Sub rptVehiculoSubActividad_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Footer Then
                DirectCast(e.Item.FindControl("labelFooterAsignacionMensual"), Label).Text = "Porcentaje de <b>Uso</b> por <b>Mes</b>:"
            Else

                Dim nombre As String = ""
                nombre = "rptSubActividadesMeses"

                Dim rpt As Repeater = DirectCast(e.Item.FindControl(nombre), Repeater)
                Dim linea As String = DirectCast(e.Item.FindControl("hdnLinea"), HiddenField).Value

                If ddlTipoAsignacion.SelectedValue = "1" Then
                    'EMPLEADOS
                    Using data As New DB(con.conectar())
                        Dim params() As SqlParameter = New SqlParameter() _
                        {
                            New SqlParameter("@Linea", linea),
                            New SqlParameter("@LlaveVehi", ddlElementoAsignar.SelectedValue),
                            New SqlParameter("@IdAnio", ddlAnio.SelectedValue)
                        }

                        rpt.DataSource = data.ObtieneDatos("ObtieneAsignacionPorVehiculo", params)
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
                            New SqlParameter("@TipoUso", ddlElementoAsignar.SelectedValue),
                            New SqlParameter("@IdAnio", ddlAnio.SelectedValue)
                        }

                        rpt.DataSource = data.ObtieneDatos("ObtieneAsignacionVehiculoPorTipoUso", params)
                        rpt.DataBind()
                    End Using
                End If

            End If
        Catch ex As Exception
            Dim er As String = ex.Message
        End Try
    End Sub
    Protected Sub ddlElementoAsignar_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoAsignacion.SelectedValue = "1" Then
            AsignacionPorVehiculo()
        Else
            AsignacionPorTipoUso()
        End If
    End Sub
    Protected Sub btnGuardarCambios_Click(sender As Object, e As EventArgs)
        If ddlTipoAsignacion.SelectedValue = "1" Then
            'VEHICULO
            For Each item As RepeaterItem In rptVehiculoSubActividad.Items

                If Not (item.ItemType = ListItemType.Footer) Then

                    Dim linea As String = DirectCast(item.FindControl("hdnLinea"), HiddenField).Value

                    Dim repeater As Repeater = DirectCast(item.FindControl("rptSubActividadesMeses"), Repeater)
                    For Each subItem As RepeaterItem In repeater.Items
                        Dim idSubActividad As Integer = DirectCast(subItem.FindControl("hdnSubActividad"), HiddenField).Value

                        'ENERO
                        Dim enero As Integer = DirectCast(subItem.FindControl("txt1"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 1, ddlElementoAsignar.SelectedValue, enero)

                        'FEBRERO
                        Dim febrero As Integer = DirectCast(subItem.FindControl("txt2"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 2, ddlElementoAsignar.SelectedValue, febrero)

                        'MARZO
                        Dim marzo As Integer = DirectCast(subItem.FindControl("txt3"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 3, ddlElementoAsignar.SelectedValue, marzo)

                        'ABRIL
                        Dim abril As Integer = DirectCast(subItem.FindControl("txt4"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 4, ddlElementoAsignar.SelectedValue, abril)

                        'MAYO
                        Dim mayo As Integer = DirectCast(subItem.FindControl("txt5"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 5, ddlElementoAsignar.SelectedValue, mayo)

                        'JUNIO
                        Dim junio As Integer = DirectCast(subItem.FindControl("txt6"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 6, ddlElementoAsignar.SelectedValue, junio)

                        'JULIO
                        Dim julio As Integer = DirectCast(subItem.FindControl("txt7"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 7, ddlElementoAsignar.SelectedValue, julio)

                        'AGOSTO
                        Dim agosto As Integer = DirectCast(subItem.FindControl("txt8"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 8, ddlElementoAsignar.SelectedValue, agosto)

                        'SEPTIEMBRE
                        Dim septiembre As Integer = DirectCast(subItem.FindControl("txt9"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 9, ddlElementoAsignar.SelectedValue, septiembre)

                        'OCTUBRE
                        Dim octubre As Integer = DirectCast(subItem.FindControl("txt10"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 10, ddlElementoAsignar.SelectedValue, octubre)

                        'NOVIEMBRE
                        Dim noviembre As Integer = DirectCast(subItem.FindControl("txt11"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 11, ddlElementoAsignar.SelectedValue, noviembre)

                        'DICIEMBRE
                        Dim diciembre As Integer = DirectCast(subItem.FindControl("txt12"), TextBox).Text
                        GuardaVehiculo(idSubActividad, 12, ddlElementoAsignar.SelectedValue, diciembre)
                    Next

                End If
            Next

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado la asignacion de uso del vehículo {0} perteneciente a la direccion {2} de la secretaria {1}", ddlElementoAsignar.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDependencia.SelectedItem.Text))
#End Region
        Else
            'TIPO USO
            For Each item As RepeaterItem In rptVehiculoSubActividad.Items

                If Not (item.ItemType = ListItemType.Footer) Then
                    Dim linea As String = DirectCast(item.FindControl("hdnLinea"), HiddenField).Value
                    Dim puesto As String = ddlElementoAsignar.SelectedItem.Text
                    Dim dtPuestos As DataTable = DirectCast(Session("TableAsignar"), DataTable)
                    'If ddlFrecuenciaAsignacion.SelectedValue = "1" Then
                    'MENSUAL
                    Dim repeater As Repeater = DirectCast(item.FindControl("rptSubActividadesMeses"), Repeater)
                    For Each subItem As RepeaterItem In repeater.Items
                        Dim idSubActividad As Integer = DirectCast(subItem.FindControl("hdnSubActividad"), HiddenField).Value

                        'ENERO
                        Dim enero As Integer = DirectCast(subItem.FindControl("txt1"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 1, ddlElementoAsignar.SelectedItem.Text, enero)

                        'FEBRERO
                        Dim febrero As Integer = DirectCast(subItem.FindControl("txt2"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 2, ddlElementoAsignar.SelectedItem.Text, febrero)

                        'MARZO
                        Dim marzo As Integer = DirectCast(subItem.FindControl("txt3"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 3, ddlElementoAsignar.SelectedItem.Text, marzo)

                        'ABRIL
                        Dim abril As Integer = DirectCast(subItem.FindControl("txt4"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 4, ddlElementoAsignar.SelectedItem.Text, abril)

                        'MAYO
                        Dim mayo As Integer = DirectCast(subItem.FindControl("txt5"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 5, ddlElementoAsignar.SelectedItem.Text, mayo)

                        'JUNIO
                        Dim junio As Integer = DirectCast(subItem.FindControl("txt6"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 6, ddlElementoAsignar.SelectedItem.Text, junio)

                        'JULIO
                        Dim julio As Integer = DirectCast(subItem.FindControl("txt7"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 7, ddlElementoAsignar.SelectedItem.Text, julio)

                        'AGOSTO
                        Dim agosto As Integer = DirectCast(subItem.FindControl("txt8"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 8, ddlElementoAsignar.SelectedItem.Text, agosto)

                        'SEPTIEMBRE
                        Dim septiembre As Integer = DirectCast(subItem.FindControl("txt9"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 9, ddlElementoAsignar.SelectedItem.Text, septiembre)

                        'OCTUBRE
                        Dim octubre As Integer = DirectCast(subItem.FindControl("txt10"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 10, ddlElementoAsignar.SelectedItem.Text, octubre)

                        'NOVIEMBRE
                        Dim noviembre As Integer = DirectCast(subItem.FindControl("txt11"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 11, ddlElementoAsignar.SelectedItem.Text, noviembre)

                        'DICIEMBRE
                        Dim diciembre As Integer = DirectCast(subItem.FindControl("txt12"), TextBox).Text
                        GuardaTipoUso(idSubActividad, 12, ddlElementoAsignar.SelectedItem.Text, diciembre)
                    Next

                End If

            Next

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado la asignacion de uso de los vehiculos con tipo de uso {0} perteneciente a la direccion {2} de la secretaria {1}", ddlElementoAsignar.SelectedItem.Text, ddlSecretaria.SelectedItem.Text, ddlDependencia.SelectedItem.Text))
#End Region

        End If

        ScriptManager.RegisterStartupScript(updAgregaVehiculo, updAgregaVehiculo.GetType(), "quitar_validar_cambios", "quitaValidarCambios();", True)
    End Sub

    Private Sub GuardaVehiculo(idSubActividad As Integer, mes As Integer, llave As Integer, porcentaje As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdSubActividad", idSubActividad),
            New SqlParameter("@Mes", mes),
            New SqlParameter("@IdVehiculo", llave),
            New SqlParameter("@Porcentaje", porcentaje)
        }
            data.EjecutaCommand("AgregaVehiculoSubActividad", params)

        End Using
    End Sub

    Private Sub GuardaTipoUso(idSubActividad As Integer, mes As Integer, tipoUso As String, porcentaje As Integer)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@IdSecreataria", idSubActividad),
            New SqlParameter("@IdDependencia", idSubActividad),
            New SqlParameter("@IdSubActividad", idSubActividad),
            New SqlParameter("@Mes", mes),
            New SqlParameter("@TipoUso", tipoUso),
            New SqlParameter("@Porcentaje", porcentaje)
        }
            data.EjecutaCommand("AgregaVehiculoTipoUsoSubActividad", params)

        End Using
    End Sub

    Protected Sub rptSubActividadesMeses_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Footer Then
                Dim repeater As Repeater = DirectCast(e.Item.Parent, Repeater)
                Dim panel As Panel = DirectCast(e.Item.FindControl("pnlSinDatos"), Panel)
                panel.Visible = repeater.Items.Count = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
