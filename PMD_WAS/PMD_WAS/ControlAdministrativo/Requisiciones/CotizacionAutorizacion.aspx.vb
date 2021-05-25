Imports System.Data.SqlClient
Public Class CotizacionAutorizacion
    Inherits System.Web.UI.Page
    Dim con As New conexion
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Autorizacion de cotizaciones")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("Puesto") = "4" Then
            '    Response.Redirect("~/ControlAdministrativo/Requisiciones/CotizacionAutorizacion.aspx")
            'End If

            CargaInformacionAdmin()
            CargaSecretarias()
            CargaCotizaciones()
            CargaMotivosRechazo()
        End If
    End Sub

    Private Sub CargaInformacionAdmin()
        Dim meses() = New String() {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}

        Using data As New DB(con.Conectar())
            Dim dt = data.ObtieneDatos("ObtieneInformacionAdministracion", Nothing).Tables(0)
            hdnAdmon.Value = dt.Rows(0)("idAdministracion")
            hdnAnio.Value = dt.Rows(0)("anio")
            hdnMes.Value = dt.Rows(0)("mes")
            Dim administracion = dt.Rows(0)("administracion")
            lblInformacionRequisicion.Text = String.Format("Solicitudes correspondientes al mes de <b>{0}</b> del <b>{1}</b> de la <b>{2}</b>", meses(dt.Rows(0)("mes") - 1), hdnAnio.Value, administracion)
        End Using
    End Sub

    Private Sub CargaSecretarias()
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", hdnAdmon.Value),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))
    End Sub

    Private Sub CargaCotizaciones()
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idSubActividad", ddlSubActividad.SelectedValue),
                New SqlParameter("@mes", hdnMes.Value),
                New SqlParameter("@anio", hdnAnio.Value)}

            Dim dt = data.ObtieneDatos("ObtieneCotizacionesAutorizacion", params).Tables(0)
            Session("TotalCotizacionesAutorizacion") = dt
            grdCotizacion.DataSource = dt
            grdCotizacion.DataBind()

        End Using

        pnlAgregarCotizacion.Visible = True
    End Sub

    Private Sub cargaActividades()
        'Carga Líneas
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", hdnAdmon.Value),
                New SqlParameter("@idAnio", hdnAnio.Value)
            }

            ddlActividad.DataSource = data.ObtieneDatos("ObtieneLineas", params)
            ddlActividad.DataTextField = "Nombr_linea"
            ddlActividad.DataValueField = "ID"
            ddlActividad.DataBind()
        End Using

        CargaSubActividades()

        CargaCotizaciones()
    End Sub

    Private Sub CargaSubActividades()
        Try
            Dim con As New Class1
            Dim claveEmpleado As Integer = Session("Clave_empl")

            Dim administracion = hdnAdmon.Value
            Dim anio = hdnAnio.Value

            'CARGA ACTIVIDADES
            Using data As New DB(con.conectar())

                'CARGA SUB ACTIVIDADES
                Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idLinea", ddlActividad.SelectedValue),
                  New SqlParameter("@clave_empl", claveEmpleado),
                  New SqlParameter("@idAdmon", administracion),
                  New SqlParameter("@idAnio", anio)
            }

                Dim dt = data.ObtieneDatos("ObtieneSubActividadesLineaEmpleado", params2)
                ddlSubActividad.DataSource = dt
                ddlSubActividad.DataTextField = "Nombre"
                ddlSubActividad.DataValueField = "Id"
                ddlSubActividad.DataBind()

            End Using

        Catch ex As Exception
            'Response.Redirect("~/Password.aspx")
        End Try
    End Sub

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.Conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", hdnAdmon.Value),
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
        cargaActividades()
    End Sub

    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaSubActividades()
        CargaCotizaciones()
    End Sub

    Protected Sub ddlSubActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaCotizaciones()
    End Sub

    Protected Sub btnRechazarCotizacion_Click(sender As Object, e As EventArgs)
        Try
            Dim cotizaciones As New List(Of Integer)
            For Each row As GridViewRow In grdCotizacion.Rows
                Dim checkbox = DirectCast(row.FindControl("chkSeleccionar"), CheckBox)
                If checkbox.Checked Then
                    Dim hdn = DirectCast(row.FindControl("hdnId"), HiddenField)
                    Dim sol As New IntelipolisEngine.PMD.Cotizaciones
                    cotizaciones.Add(hdn.Value)
                End If
            Next

            If cotizaciones.Count() <= 0 Then
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "info", "Favor de validar", "Favor de seleccionar al menos una cotizacion para poder ser rechazada."), True)
            Else
                Dim engine As New IntelipolisEngine.PMD.Cotizaciones()
                engine.RechazaCotizacionControlAdministrativo(cotizaciones, ddlMotivoRechazo.SelectedValue, txtComentariosRechazo.Text, Session("Clave_empl"))

                CargaCotizaciones()
                ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "ocultaModal", "ocultaModalRechazo();", True)
                ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las Cotizaciones fueron rechazadas correctamente."), True)

            End If
        Catch ex As Exception
            'lblInformacionRequisicion.Text = ex.ToString();
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnAutorizarCotizacion_Click(sender As Object, e As EventArgs)
        Try
            Dim cotizaciones As New List(Of Integer)
            For Each row As GridViewRow In grdCotizacion.Rows
                Dim checkbox = DirectCast(row.FindControl("chkSeleccionar"), CheckBox)
                If checkbox.Checked Then
                    Dim hdn = DirectCast(row.FindControl("hdnId"), HiddenField)
                    cotizaciones.Add(hdn.Value)
                End If
            Next

            If cotizaciones.Count() <= 0 Then
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "info", "Favor de validar", "Favor de seleccionar al menos una cotizacion para poder ser autorizada."), True)
            Else
                Dim engine As New IntelipolisEngine.PMD.Cotizaciones()
                engine.AutorizaCotizacionControlAdministrativo(cotizaciones, Session("Clave_empl"))

                CargaCotizaciones()
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las cotizaciones fueron autorizadas correctamente."), True)

            End If
        Catch ex As Exception
            'lblInformacionRequisicion.Text = ex.ToString();
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        If e.CommandName = "ver" Then
            'Dim folio = e.CommandArgument
            'Dim helper = New IntelipolisEngine.PMD.Cotizaciones()
            'Dim url = helper.GeneraDocumento(folio)
            CargaInfoCotizacion(e.CommandArgument)
            'ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestra_pdf", String.Format("openPDFIFrame('{0}')", ResolveClientUrl(url)), True)
        ElseIf e.CommandName = "comentarios" Then
            ucComentariosCotizacion.CargaComentariosProducto(e.CommandArgument)
        ElseIf e.CommandName = "editar" Then
            CargaCotizacion(e.CommandArgument)
        ElseIf e.CommandName = "productos" Then
            Dim button = DirectCast(sender, Button)
            Dim grd As New GridView()
            grd = (button.NamingContainer).FindControl("grdProductos")
            If button.Text = "+" Then
                Using data As New DB(con.Conectar())
                    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", e.CommandArgument)}
                    grd.DataSource = data.ObtieneDatos("ObtieneProductosCotizacion", params).Tables(0)
                    grd.DataBind()
                    grd.Visible = True
                End Using
                button.Text = "-"
            Else
                button.Text = "+"
                grd.DataSource = Nothing
                grd.DataBind()
                grd.Visible = False
            End If
        ElseIf e.CommandName = "muestraComentario" Then
            MuestraInfoCotizacion(e.CommandArgument)

        End If
    End Sub

    Private Sub MuestraInfoCotizacion(id As Integer)
        Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
        Dim infoCotizacion = engine.ObtieneEstatusCotizacion(id)
        lblInfoEstatusCotizacion.Text = String.Format("La cotización con folio {0} tiene el estatus <b>{1}</b> con los siguientes comentarios: <br/><b>{2}</b>", infoCotizacion.Folio, infoCotizacion.Estatus, infoCotizacion.Comentarios)

        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestraModal", "muestraModalEstatusCotizacion();", True)
    End Sub

    Private Sub CargaInfoCotizacion(id As Integer)
        ucCotizacion.CargaCotizacionDetalle(id)
        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "muestraModalInfoCotizacion();", True)
    End Sub

    Private Sub CargaCotizacion(id As Integer)
        'hdnIdCotizacion.Value = id
        'Using data As New DB(con.Conectar())
        '    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}
        '    Dim row = data.ObtieneDatos("ObtieneCotizacion", params).Tables(0).Rows(0)
        '    lblInformacionCotizacion.Text = String.Format("La cotización con folio {0} fue generada el {1:dd/MMM/yyyy} por {2}. <hr />", row("folio"), row("fechaEnvio"), row("usuarioEnvio"))


        '    Dim params2() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}
        '    Dim row2 = data.ObtieneDatos("ObtieneProductosCotizacion", params2).Tables(0).Rows(0)
        '    txtProducto.Text = row2("producto")
        '    txtDescripcionProducto.Text = row2("comentarios")
        '    txtCantidad.Text = row2("cantidad")
        '    txtUnidad.Text = row2("unidad")
        '    txtJustificacion.Text = row2("justificacion")
        '    txtLugarEntrega.Text = row("lugarEntrega")
        '    txtVigencia.Text = row2("vigencia")
        '    txtFechaInicio.Text = String.Join("{0:dd-MM-yyyy}", row2("fechaInicio"))
        '    txtFechaTerminacion.Text = String.Join("{0:dd-MM-yyyy}", row2("fechaTermino"))
        '    ddlTipoProducto.SelectedValue = row2("idTipo")
        '    pnlInfoProducto.Visible = row2("idTipo") = 1
        '    pnlInfoServicio.Visible = row2("idTipo") = 2
        '    If ddlTipoProducto.SelectedValue = "1" Then
        '        tituloNombre.InnerText = "Nombre del producto:"
        '    Else
        '        tituloNombre.InnerText = "Descripción del servicio"
        '    End If

        '    Dim idDetalle = row2("id")
        '    Dim params3() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idDetalle)}
        '    Dim dt = data.ObtieneDatos("ObtieneProductosCotizacionEspecificaciones", params3).Tables(0)

        '    Dim dtEspecificaciones As New DataTable()
        '    dtEspecificaciones.Columns.Add("Id")
        '    dtEspecificaciones.Columns.Add("Nombre")
        '    dtEspecificaciones.Columns.Add("Especificacion")
        '    dtEspecificaciones.Columns.Add("Guardado")
        '    dtEspecificaciones.Columns.Add("Eliminado")

        '    For Each row In dt.Rows
        '        Dim dRow = dtEspecificaciones.NewRow()
        '        dRow("Guardado") = True
        '        dRow("Eliminado") = False
        '        dRow("Id") = row("id")
        '        dRow("Nombre") = row("tipo")
        '        dRow("Especificacion") = row("especificacion")
        '        dtEspecificaciones.Rows.Add(dRow)
        '    Next
        '    grdEspecificaciones.DataSource = dtEspecificaciones
        '    grdEspecificaciones.DataBind()
        '    Session("EspecificacionesCotizacion") = dtEspecificaciones

        '    Dim dtFiles As New DataTable()
        '    dtFiles.Columns.Add("Id")
        '    dtFiles.Columns.Add("Nombre")
        '    dtFiles.Columns.Add("URL")
        '    dtFiles.Columns.Add("FilePath")
        '    dtFiles.Columns.Add("Guardado")
        '    dtFiles.Columns.Add("Eliminado")

        '    Dim pF() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idDetalle)}
        '    Dim dtF = data.ObtieneDatos("ObtieneArchivosCotizacion", pF).Tables(0)
        '    For Each row In dtF.Rows
        '        Dim dRow = dtFiles.NewRow()
        '        dRow("Guardado") = True
        '        dRow("Eliminado") = False
        '        dRow("Id") = row("id")
        '        dRow("Nombre") = row("nombreArchivo")
        '        dRow("URL") = row("rutaArchivo")
        '        dtFiles.Rows.Add(dRow)
        '    Next
        '    grdArchivosCotizacion.DataSource = dtFiles
        '    grdArchivosCotizacion.DataBind()
        '    Session("ArchivosAgregadosCotizacion") = dtFiles
        'End Using

        'ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "muestraModalCotizacion();", True)
    End Sub

    Protected Sub chkTodos_CheckedChanged(sender As Object, e As EventArgs)
        Dim dt = DirectCast(Session("TotalCotizacionesAutorizacion"), DataTable)
        For Each row In dt.Rows
            row("seleccionado") = (DirectCast(sender, CheckBox)).Checked
        Next

        grdCotizacion.DataSource = dt
        grdCotizacion.DataBind()

        Session("TotalCotizacionesAutorizacion") = dt
    End Sub

    Private Sub CargaMotivosRechazo()
        Dim con As New Class1
        Using data As New DB(con.conectar())

            Dim dt = data.ObtieneDatos("CargaCatalogoMotivosRechazoSolicitudrequisicion", Nothing).Tables(0)
            ddlMotivoRechazo.DataValueField = "id"
            ddlMotivoRechazo.DataTextField = "nombre"
            ddlMotivoRechazo.DataSource = dt
            ddlMotivoRechazo.DataBind()

        End Using
    End Sub
End Class