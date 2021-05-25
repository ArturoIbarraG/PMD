Imports System.Data.SqlClient
Partial Class AutorizacionLineas
    Inherits System.Web.UI.Page
    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Autorización de gastos")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Carga el año
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@idAdmon", 3)
                }

                ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params)
                ddlAnio.DataTextField = "Año"
                ddlAnio.DataValueField = "Año"
                ddlAnio.DataBind()
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

    Protected Sub lnkModals_Command(sender As Object, e As CommandEventArgs)
        Dim lnk = DirectCast(sender, LinkButton)
        Dim parameters() As String = lnk.CommandArgument.Split("|")
        Dim idSubActividad As Integer = Integer.Parse(parameters(0))
        Dim claveGastos As Integer = Integer.Parse(parameters(1))
        'Dim sueldos = DirectCast(DirectCast(lnk.NamingContainer, GridViewRow).FindControl("hdnSueldo"), HiddenField).Value
        'Dim vehiculos = DirectCast(DirectCast(lnk.NamingContainer, GridViewRow).FindControl("hdnMateriales"), HiddenField).Value
        'Dim materiales = DirectCast(DirectCast(lnk.NamingContainer, GridViewRow).FindControl("hdnVehiculos"), HiddenField).Value
        'Dim clave_gasto = e.CommandArgument.ToString()
        Dim estatus As Integer
        If e.CommandName = "aprobar" Then
            estatus = 1
            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Id_SubActividad", idSubActividad),
                    New SqlParameter("@Id_ClaveGastos", claveGastos),
                    New SqlParameter("@Id_Estatus", estatus)
                }

                data.EjecutaCommand("RegistraEstatusSubActividad", params)
                ddlDependencia_SelectedIndexChanged(Nothing, Nothing)
            End Using

        ElseIf e.CommandName = "reducir" Then
            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Id_SubActividad", idSubActividad),
                    New SqlParameter("@Id_ClaveGastos", claveGastos),
                    New SqlParameter("@Id_Estatus", estatus)
                }

                data.EjecutaCommand("RegistraEstatusSubActividad", params)
                ddlDependencia_SelectedIndexChanged(Nothing, Nothing)
            End Using

        ElseIf e.CommandName = "rechazar" Then
            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Id_SubActividad", idSubActividad),
                    New SqlParameter("@Id_ClaveGastos", claveGastos),
                    New SqlParameter("@Id_Estatus", estatus)
                }

                data.EjecutaCommand("RegistraEstatusSubActividad", params)
                ddlDependencia_SelectedIndexChanged(Nothing, Nothing)
            End Using
        End If
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
            Dim helper As New IntelipolisEngine.Eventos.EventoHelper()

            'Carga clave
            lblSecretaria.Text = ddlSecretaria.SelectedValue
        End Using
        ddlDependencia.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
    End Sub

    Private Sub cargaGastos()
        lblPresupuestoLeyenda.Text = String.Format("Presupuesto {0}", ddlAnio.SelectedItem.Text)
        'Carga Presupuesto Lineas
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                    New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue),
                      New SqlParameter("@IdLinea", ddlActividad.SelectedValue)
                }

            Dim dtTotales = data.ObtieneDatos("ObtieneSubActividadesAutorizacion", params).Tables(0)
            grdPresupuestos.DataSource = dtTotales
            grdPresupuestos.DataBind()

            'Muestra los Totales
            lblTotal.Text = String.Format("Total: {0:c2}", dtTotales.Compute("SUM(Total)", ""))

            'Carga clave
            lblDependencia.Text = ddlDependencia.SelectedValue

            'CARGA PRESUPUESTO
            Dim params2() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Año", ddlAnio.SelectedValue)
                }

            Dim dt = data.ObtieneDatos("ObtienePresupuesto", params2).Tables(0)
            rptPresupuesto.DataSource = dt
            rptPresupuesto.DataBind()

            'Muestra los totales
            lblFooterPresupuestado.Text = String.Format("{0:c2}", dt.Compute("SUM(Presupuestado)", ""))
            lblFooterCapturado.Text = String.Format("{0:c2}", dt.Compute("SUM(Capturado)", ""))
            lblFooterDiferencia.Text = String.Format("{0:c2}", dt.Compute("SUM(Diferencia)", ""))
        End Using
    End Sub

    Protected Sub ddlDependencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        cargaActividades()

        cargaGastos()
    End Sub

    Protected Sub btnGuardaReducir_Click(sender As Object, e As EventArgs)
        Dim estatus = 2
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Id_Linea", hdnLinea.Value),
                    New SqlParameter("@Id_Secretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@Id_Dependencia", ddlDependencia.SelectedValue),
                    New SqlParameter("@Año", ddlAnio.SelectedValue),
                    New SqlParameter("@Sueldo", txtReducirSueldo.Text),
                    New SqlParameter("@Materiales", txtReducirMateriales.Text),
                    New SqlParameter("@Vehiculos", txtReducirVehiculos.Text),
                    New SqlParameter("@Estatus", estatus),
                    New SqlParameter("@Clave_Empl", Integer.Parse(Session("Clave_empl").ToString())),
                    New SqlParameter("@Comentarios", txtComentariosReducir.Text)
                }

            data.EjecutaCommand("RegistraEstatusLinea", params)
        End Using
        txtComentariosReducir.Text = ""
        txtReducirVehiculos.Text = ""
        txtReducirSueldo.Text = ""
        txtReducirMateriales.Text = ""
        ddlDependencia_SelectedIndexChanged(Nothing, Nothing)
        ScriptManager.RegisterStartupScript(updModalReducir, updModalReducir.GetType(), "modal_reducir", "ocultaModalReducir();", True)
    End Sub

    Protected Sub btnRechazar_Click(sender As Object, e As EventArgs)
        Dim estatus = 0
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Id_Linea", hdnLinea.Value),
                    New SqlParameter("@Id_Secretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@Id_Dependencia", ddlDependencia.SelectedValue),
                    New SqlParameter("@Año", ddlAnio.SelectedValue),
                    New SqlParameter("@Sueldo", hdnSueldoRechazar.Value),
                    New SqlParameter("@Materiales", hdnMaterialesRechazar.Value),
                    New SqlParameter("@Vehiculos", hdnVehiculosRechazar.Value),
                    New SqlParameter("@Estatus", estatus),
                    New SqlParameter("@Clave_Empl", Integer.Parse(Session("Clave_empl").ToString())),
                    New SqlParameter("@Comentarios", txtComentariosRechazar.Text)
                }

            data.EjecutaCommand("RegistraEstatusLinea", params)
        End Using
        hdnSueldoRechazar.Value = ""
        hdnVehiculosRechazar.Value = ""
        hdnMaterialesRechazar.Value = ""
        txtComentariosRechazar.Text = ""
        ddlDependencia_SelectedIndexChanged(Nothing, Nothing)
        ScriptManager.RegisterStartupScript(updRechazar, updRechazar.GetType(), "modal_reducir", "ocultaModalRechazar();", True)
    End Sub

    Protected Sub grdPresupuestos_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        Using data As New DB(con.conectar())
            ''FUNCION DE EL GASTO
            'Dim ddlFuncionGasto As DropDownList = e.Row.FindControl("ddlFuncionGasto")
            'If Not ddlFuncionGasto Is Nothing Then
            '    ddlFuncionGasto.DataValueField = "Id"
            '    ddlFuncionGasto.DataTextField = "Nombre"
            '    ddlFuncionGasto.DataSource = data.ObtieneDatos("ObtieneFuncionesGasto", Nothing).Tables(0)
            '    ddlFuncionGasto.DataBind()
            '    ddlFuncionGasto.Items.Insert(0, New ListItem("", "-1"))

            '    Try
            '        Dim funcionGasto As String = DirectCast(e.Row.FindControl("hdnFuncionGasto"), HiddenField).Value
            '        ddlFuncionGasto.SelectedValue = funcionGasto
            '    Catch ex As Exception

            '    End Try

            'End If

            ''RECURSOS
            'Dim recurso As String = ""
            'Dim ddlTipoRecurso As DropDownList = e.Row.FindControl("ddlTipoRecurso")
            'If Not ddlTipoRecurso Is Nothing Then
            '    ddlTipoRecurso.DataValueField = "Id"
            '    ddlTipoRecurso.DataTextField = "Nombre"
            '    ddlTipoRecurso.DataSource = data.ObtieneDatos("ObtieneTipoRecursos", Nothing).Tables(0)
            '    ddlTipoRecurso.DataBind()
            '    ddlTipoRecurso.Items.Insert(0, New ListItem("", "-1"))

            '    Try
            '        recurso = DirectCast(e.Row.FindControl("hdnRecurso"), HiddenField).Value
            '        ddlTipoRecurso.SelectedValue = recurso
            '    Catch ex As Exception

            '    End Try
            'End If

            'FUENTES FINANCIAMIENTO
            Dim ddlFuenteFinanciamiento As DropDownList = e.Row.FindControl("ddlFuenteFinanciamiento")
            If Not ddlFuenteFinanciamiento Is Nothing Then
                ddlFuenteFinanciamiento.DataValueField = "Id"
                ddlFuenteFinanciamiento.DataTextField = "Descripcion"

                ddlFuenteFinanciamiento.DataSource = data.ObtieneDatos("ObtieneFuenteFinanciamiento", Nothing).Tables(0)
                ddlFuenteFinanciamiento.DataBind()
                ddlFuenteFinanciamiento.Items.Insert(0, New ListItem("", "-1"))

                Try
                    Dim ff = DirectCast(e.Row.FindControl("hdnFF"), HiddenField).Value
                    ddlFuenteFinanciamiento.SelectedValue = ff
                Catch ex As Exception

                End Try
            End If


            'RECURSOS
            Dim ddlEstatus As DropDownList = e.Row.FindControl("ddlEstatus")
            If Not ddlEstatus Is Nothing Then
                Try
                    Dim estatus = DirectCast(e.Row.FindControl("hdnEstatus"), HiddenField).Value
                    ddlEstatus.SelectedValue = estatus
                Catch ex As Exception

                End Try
            End If
        End Using

    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        For Each row As GridViewRow In grdPresupuestos.Rows
            Try
                Dim idSubActividad As Integer = Integer.Parse(DirectCast(row.FindControl("hdnIdSubActividad"), HiddenField).Value)
                Dim claveGastos As Integer = Integer.Parse(DirectCast(row.FindControl("hdnClaveGastos"), HiddenField).Value)

                ''Funcion Gastos
                'Dim IdFuncionGastos = 0
                'Dim ddlFuncionGasto As DropDownList = row.FindControl("ddlFuncionGasto")
                'If ddlFuncionGasto.SelectedIndex > 0 Then
                '    IdFuncionGastos = Integer.Parse(ddlFuncionGasto.SelectedValue)
                'End If

                ''TIPO RECURSO
                'Dim IdRecurso = 0
                'Dim ddlRecurso As DropDownList = row.FindControl("ddlTipoRecurso")
                'If ddlRecurso.SelectedIndex > 0 Then
                '    IdRecurso = Integer.Parse(ddlRecurso.SelectedValue)
                'End If

                'FUENTE FINANCIAMIENTO
                Dim IdFuenteFinanciamiento = 0
                Dim ddlFuenteFinanciamiento As DropDownList = row.FindControl("ddlFuenteFinanciamiento")
                If ddlFuenteFinanciamiento.SelectedIndex > 0 Then
                    IdFuenteFinanciamiento = Integer.Parse(ddlFuenteFinanciamiento.SelectedValue)
                End If

                'ESTATUS
                Dim IdEstatus = 0
                Dim ddlEstatus As DropDownList = row.FindControl("ddlEstatus")
                If ddlEstatus.SelectedIndex > 0 Then
                    IdEstatus = Integer.Parse(ddlEstatus.SelectedValue)
                End If

                'TOTAL
                Dim total As Decimal = Decimal.Parse(DirectCast(row.FindControl("hdnTotal"), HiddenField).Value)

                'EVENTO
                Dim folioEvento As Integer = Integer.Parse(DirectCast(row.FindControl("hdnFolioEvento"), HiddenField).Value)

                Using data As New DB(con.conectar())

                    Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@Id_SubActividad", idSubActividad),
                        New SqlParameter("@Clave_Gastos", claveGastos),
                        New SqlParameter("@Id_FuncionGasto", 0),
                        New SqlParameter("@Id_Recurso", 0),
                        New SqlParameter("@Id_FuenteFinanciamiento", IdFuenteFinanciamiento),
                        New SqlParameter("@Id_Estatus", IdEstatus),
                        New SqlParameter("@Total", total),
                        New SqlParameter("@Folio", folioEvento)
                    }
                    data.EjecutaCommand("ActualizaSubActividadAutorizacion", params)
                End Using
            Catch ex As Exception
                Dim ee As String = ex.ToString()
            End Try
        Next

        'Actualiza presupuestos
        Using data As New DB(con.conectar())
            data.EjecutaCommand("ActualizaPresupuesto", Nothing)
        End Using

        ddlDependencia_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub ddlTipoRecurso_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())

            'FUENTES FINANCIAMIENTO
            Dim gvrow As GridViewRow = TryCast(sender, DropDownList).NamingContainer  '(GridViewRow)((DropDownList)sender).NamingContainer;

            Dim ddlFuenteFinanciamiento As DropDownList = gvrow.FindControl("ddlFuenteFinanciamiento")
            Dim recurso As String = TryCast(gvrow.FindControl("ddlTipoRecurso"), DropDownList).SelectedValue
            If Not ddlFuenteFinanciamiento Is Nothing Then
                ddlFuenteFinanciamiento.DataValueField = "Id"
                ddlFuenteFinanciamiento.DataTextField = "Nombre"
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdRecurso", recurso)
            }

                ddlFuenteFinanciamiento.DataSource = data.ObtieneDatos("ObtieneFuenteFinanciamientoPorRecurso", params).Tables(0)
                ddlFuenteFinanciamiento.DataBind()
                ddlFuenteFinanciamiento.Items.Insert(0, New ListItem("", "-1"))

                Try
                    Dim ff = DirectCast(gvrow.FindControl("hdnFF"), HiddenField).Value
                    ddlFuenteFinanciamiento.SelectedValue = ff
                Catch ex As Exception

                End Try
            End If

        End Using
    End Sub
    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        cargaGastos()
    End Sub

    Private Sub cargaActividades()
        'Carga Líneas
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDependencia.SelectedValue),
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue)
            }

            ddlActividad.DataSource = data.ObtieneDatos("ObtieneLineas", params)
            ddlActividad.DataTextField = "Nombr_linea"
            ddlActividad.DataValueField = "ID"
            ddlActividad.DataBind()
        End Using
        ddlActividad.Items.Insert(0, New ListItem("Todas", "-1"))
    End Sub
End Class
