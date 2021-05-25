Imports System.Data.SqlClient

Public Class Cotizaciones
    Inherits System.Web.UI.Page
    Dim con As New conexion

    Property SubmissionID() As String
        Get
            Return hdnClaveSubmisionID.Value
        End Get

        Set
            hdnClaveSubmisionID.Value = Value
        End Set

    End Property

    Property Cotizacion() As IntelipolisEngine.PMD.Cotizaciones.Cotizacion
        Get
            If Session("IntelipolisEngine.PMD.Cotizaciones.Cotizacion") Is Nothing Then
                Session("IntelipolisEngine.PMD.Cotizaciones.Cotizacion") = New IntelipolisEngine.PMD.Cotizaciones.Cotizacion()
            End If
            Return Session("IntelipolisEngine.PMD.Cotizaciones.Cotizacion")
        End Get
        Set(value As IntelipolisEngine.PMD.Cotizaciones.Cotizacion)
            Session("IntelipolisEngine.PMD.Cotizaciones.Cotizacion") = value
        End Set
    End Property

    ReadOnly Property UploadedFilesStorage() As IntelipolisEngine.Helper.UploadControlHelper.UploadedFilesStorage
        Get
            Return IntelipolisEngine.Helper.UploadControlHelper.GetUploadedFilesStorageByKey(SubmissionID)
        End Get
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Cotizaciones")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("clave_depe") = "245" Then
                Response.Redirect("~/ControlAdministrativo/Requisiciones/CotizacionRequisicion.aspx")
            ElseIf Session("Puesto") = "5" Then
                Response.Redirect("~/ControlAdministrativo/Requisiciones/CotizacionAutorizacion.aspx")
            End If

            CargaInformacionAdmin()
            CargaSecretarias()
            CargaCotizaciones()
            CargaCatalogoEspecificacion()
            CargaCatalogoUnidad()
            CargaCatalogoAlmacenes()
        End If
    End Sub

#Region "CARGA INFORMACION"
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

    Private Sub CargaCatalogoEspecificacion()
        Using data As New DB(con.Conectar())

            ddlTipoEspecificacion.DataSource = data.ObtieneDatos("ObtieneCatalogoEspecificaciones", Nothing)
            ddlTipoEspecificacion.DataTextField = "nombre"
            ddlTipoEspecificacion.DataValueField = "id"
            ddlTipoEspecificacion.DataBind()
        End Using
    End Sub

    Private Sub CargaCatalogoUnidad()
        Using data As New DB(con.Conectar())

            ddlUnidad.DataSource = data.ObtieneDatos("ObtieneCatalogoUnidad", Nothing)
            ddlUnidad.DataTextField = "nombre"
            ddlUnidad.DataValueField = "id"
            ddlUnidad.DataBind()
        End Using
    End Sub
    Private Sub CargaCatalogoAlmacenes()
        Using data As New DB(con.Conectar())

            ddlAlmacen.DataSource = data.ObtieneDatos("ObtieneAlmacenes", Nothing)
            ddlAlmacen.DataTextField = "nombre"
            ddlAlmacen.DataValueField = "id"
            ddlAlmacen.DataBind()
        End Using
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

    Private Sub CargaCotizaciones()
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idSubActividad", ddlSubActividad.SelectedValue),
                New SqlParameter("@mes", hdnMes.Value),
                New SqlParameter("@anio", hdnAnio.Value)}

            Dim dt = data.ObtieneDatos("ObtieneCotizaciones", params).Tables(0)
            Session("TotalCotizaciones") = dt
            grdCotizacion.DataSource = dt
            grdCotizacion.DataBind()

        End Using

        pnlAgregarCotizacion.Visible = True
    End Sub
#End Region

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

    Protected Sub btnAgregarCotizacion_Click(sender As Object, e As EventArgs)
        Try
            ddlTipoProducto.Enabled = True
            limpiarCampos()
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestraModal", "muestraModalCotizacion();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Private Sub limpiarCampos()
        SubmissionID = IntelipolisEngine.Helper.UploadControlHelper.GenerateUploadedFilesStorageKey()
        IntelipolisEngine.Helper.UploadControlHelper.AddUploadedFilesStorage(SubmissionID)

        hdnIdCotizacion.Value = 0

        txtProducto.Text = ""
        txtDescripcionProducto.Text = ""
        ddlTipoEspecificacion.SelectedIndex = 0
        txtEspecificacion.Text = ""
        lblInformacionCotizacion.Text = ""
        txtCantidad.Text = ""
        txtFechaInicio.Text = ""
        txtFechaTerminacion.Text = ""
        txtJustificacion.Text = ""
        ddlAlmacen.SelectedIndex = 0
        ddlUnidad.SelectedIndex = 0
        txtVigencia.Text = ""

        Session("ArchivosAgregadosCotizacion") = Nothing
        Session("EspecificacionesCotizacion") = Nothing
        grdEspecificaciones.DataSource = Session("EspecificacionesCotizacion")
        grdEspecificaciones.DataBind()

        grdArchivosCotizacion.DataSource = Session("ArchivosAgregadosCotizacion")
        grdArchivosCotizacion.DataBind()

    End Sub

    Private Sub CargaCotizacionDetalle(ByVal idDetalle As Integer)

        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idDetalle", idDetalle)}
            Dim row = data.ObtieneDatos("ObtieneCotizacionDetalle", params).Tables(0).Rows(0)
            lblInformacionCotizacion.Text = String.Format("La cotización con folio {0} fue generada el {1:dd/MMM/yyyy} por {2}. <hr />", row("folio"), row("fechaEnvio"), row("usuarioEnvio"))
            hdnIdCotizacion.Value = row("idCotizacion")
            hdnIdCotizacionDetalle.Value = idDetalle

            'Dim params2() As SqlParameter = New SqlParameter() {New SqlParameter("@id", ID)}
            'Dim row2 = data.ObtieneDatos("ObtieneProductosCotizacion", params2).Tables(0).Rows(0)
            txtProducto.Text = row("producto")
            txtDescripcionProducto.Text = row("comentarios")

            If row("idTipo") = 1 Then
                txtCantidad.Text = row("cantidad")
                ddlUnidad.SelectedValue = row("idUnidad")
            Else
                txtVigencia.Text = row("vigencia")
                txtFechaInicio.Text = String.Join("{0:dd-MM-yyyy}", row("fechaInicio"))
                txtFechaTerminacion.Text = String.Join("{0:dd-MM-yyyy}", row("fechaTermino"))
            End If

            txtJustificacion.Text = row("justificacion")
            ddlAlmacen.SelectedValue = row("lugarEntrega")
            ddlTipoProducto.SelectedValue = row("idTipo")
            ddlTipoProducto.Enabled = False
            pnlInfoProducto.Visible = row("idTipo") = 1
            pnlInfoServicio.Visible = row("idTipo") = 2
            If ddlTipoProducto.SelectedValue = "1" Then
                tituloNombre.InnerText = "Nombre del producto:"
            Else
                tituloNombre.InnerText = "Descripción del servicio"
            End If

            'Dim idDetalle = row2("id")
            Dim params3() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idDetalle)}
            Dim dt = data.ObtieneDatos("ObtieneProductosCotizacionEspecificaciones", params3).Tables(0)

            Dim dtEspecificaciones As New DataTable()
            dtEspecificaciones.Columns.Add("Id")
            dtEspecificaciones.Columns.Add("Nombre")
            dtEspecificaciones.Columns.Add("Especificacion")
            dtEspecificaciones.Columns.Add("Guardado")
            dtEspecificaciones.Columns.Add("Eliminado")

            For Each row In dt.Rows
                Dim dRow = dtEspecificaciones.NewRow()
                dRow("Guardado") = True
                dRow("Eliminado") = False
                dRow("Id") = row("id")
                dRow("Nombre") = row("tipo")
                dRow("Especificacion") = row("especificacion")
                dtEspecificaciones.Rows.Add(dRow)
            Next
            grdEspecificaciones.DataSource = dtEspecificaciones
            grdEspecificaciones.DataBind()
            Session("EspecificacionesCotizacion") = dtEspecificaciones

            Dim dtFiles As New DataTable()
            dtFiles.Columns.Add("Id")
            dtFiles.Columns.Add("Nombre")
            dtFiles.Columns.Add("URL")
            dtFiles.Columns.Add("FilePath")
            dtFiles.Columns.Add("Guardado")
            dtFiles.Columns.Add("Eliminado")

            Dim pF() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idDetalle)}
            Dim dtF = data.ObtieneDatos("ObtieneArchivosCotizacion", pF).Tables(0)
            For Each row In dtF.Rows
                Dim dRow = dtFiles.NewRow()
                dRow("Guardado") = True
                dRow("Eliminado") = False
                dRow("Id") = row("id")
                dRow("Nombre") = row("nombreArchivo")
                dRow("URL") = row("rutaArchivo")
                dtFiles.Rows.Add(dRow)
            Next
            grdArchivosCotizacion.DataSource = dtFiles
            grdArchivosCotizacion.DataBind()
            Session("ArchivosAgregadosCotizacion") = dtFiles
        End Using

        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "muestraModalCotizacion();", True)
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        If e.CommandName = "ver" Then
            CargaInfoCotizacion(e.CommandArgument)
        ElseIf e.CommandName = "comentarios" Then
            ucComentariosCotizacion.CargaComentariosProducto(e.CommandArgument)
        ElseIf e.CommandName = "editar" Then
            CargaCotizacionDetalle(e.CommandArgument)
        ElseIf e.CommandName = "quitar" Then
            hdnIdCotizacionEliminar.Value = e.CommandArgument
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "muestraAlertaEliminarCotizacion();", True)
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
        ElseIf e.CommandName = "agregar" Then
            NuevoProductoCotizacion(e.CommandArgument)
        ElseIf e.CommandName = "muestraComentario" Then
            MuestraInfoCotizacion(e.CommandArgument)
        ElseIf e.CommandName = "documentos" Then
            MuestraDocumentosCotizacion(e.CommandArgument)
        ElseIf e.CommandName = "documentoSeleccionado" Then
            ObtieneDocumentoSeleccionado(e.CommandArgument)
        ElseIf e.CommandName = "habilitarRequisicion" Then
            HabilitarRequisicion(e.CommandArgument)
        End If
    End Sub

    Private Sub HabilitarRequisicion(idCotizacion As String)
        Response.Redirect("~/ControlAdministrativo/Requisiciones/AltaRequisicion?idCot=" + idCotizacion)
    End Sub

    Private Sub ObtieneDocumentoSeleccionado(idCotizacion As Integer)
        Using data As New DB(con.Conectar())

            Dim pF() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idCotizacion)}
            Dim row = data.ObtieneDatos("ObtieneDocumentosCotizacionSeleccionado", pF).Tables(0).Rows(0)
            Dim url = row("rutaArchivo")

            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestra_pdf", String.Format("openPDFIFrame('{0}')", ResolveClientUrl(url)), True)
        End Using
    End Sub

    Private Sub MuestraDocumentosCotizacion(id As Integer)

        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}
            Dim dt = data.ObtieneDatos("ObtieneDocumentosCotizacion", params).Tables(0)
            rptDocumentos.DataSource = dt
            rptDocumentos.DataBind()
        End Using

        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "muestraModalDocumentos();", True)
    End Sub

    Private Sub MuestraInfoCotizacion(id As Integer)
        Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
        Dim infoCotizacion = engine.ObtieneEstatusCotizacion(id)
        lblInfoEstatusCotizacion.Text = String.Format("La cotización con folio {0} tiene el estatus <b>{1}</b> con los siguientes comentarios: <br/><b>{2}</b>", infoCotizacion.Folio, infoCotizacion.Estatus, infoCotizacion.Comentarios)

        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestraModal", "muestraModalEstatusCotizacion();", True)
    End Sub

    Private Sub NuevoProductoCotizacion(idCotizacion As Integer)
        limpiarCampos()
        hdnIdCotizacion.Value = idCotizacion
        hdnIdCotizacionDetalle.Value = 0

        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idCotizacion)}
            Dim row = data.ObtieneDatos("ObtieneCotizacion", params).Tables(0).Rows(0)
            ddlTipoProducto.SelectedValue = row("idTipo")
            ddlTipoProducto.Enabled = False
            pnlInfoProducto.Visible = row("idTipo") = 1
            pnlInfoServicio.Visible = row("idTipo") = 2
        End Using

        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestraModal", "muestraModalCotizacion();", True)
    End Sub

    Private Sub CargaInfoCotizacion(id As Integer)
        ucCotizacion.CargaCotizacionDetalle(id)
        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "muestraModalInfoCotizacion();", True)
    End Sub


    Protected Sub btnGuardarCotizacion_Click(sender As Object, e As EventArgs)
        Try
            Dim dtEspecificaciones = DirectCast(Session("EspecificacionesCotizacion"), DataTable)
            Dim dtFiles = DirectCast(Session("ArchivosAgregadosCotizacion"), DataTable)

            Dim cotizacionesEngine = New IntelipolisEngine.PMD.Cotizaciones()
            Dim fechaInicio As DateTime = Nothing
            Dim fechaFin As DateTime = Nothing
            Dim cantidad As Decimal = Nothing
            Dim idUnidad As Integer = Nothing
            If ddlTipoProducto.SelectedValue = 2 Then
                fechaInicio = DateTime.Parse(txtFechaInicio.Text)
                fechaFin = DateTime.Parse(txtFechaTerminacion.Text)
            Else
                idUnidad = Integer.Parse(ddlUnidad.SelectedValue)
                cantidad = Decimal.Parse(txtCantidad.Text)
            End If


            Dim folio = cotizacionesEngine.GuardaCotizacion(hdnIdCotizacion.Value, hdnAnio.Value, hdnMes.Value, ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue, ddlSubActividad.SelectedValue, hdnIdCotizacionDetalle.Value, txtProducto.Text, txtDescripcionProducto.Text, dtEspecificaciones, dtFiles, Session("Clave_empl"), ddlTipoProducto.SelectedValue, cantidad, idUnidad, fechaInicio, fechaFin, txtVigencia.Text, txtJustificacion.Text, ddlAlmacen.SelectedValue)
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "ocultaModalCotizacion();", True)
            limpiarCampos()
            CargaCotizaciones()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Try

            If String.IsNullOrEmpty(txtEspecificacion.Text) Then
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", "Favor de agregar la especificacion antes de agregarla."), True)
                Return
            End If

            If Session("EspecificacionesCotizacion") Is Nothing Then
                Dim dt As New DataTable()
                dt.Columns.Add("Id")
                dt.Columns.Add("Nombre")
                dt.Columns.Add("Especificacion")
                dt.Columns.Add("Guardado")
                dt.Columns.Add("Eliminado")
                Session("EspecificacionesCotizacion") = dt
            End If
            Dim dtEspecificaciones = DirectCast(Session("EspecificacionesCotizacion"), DataTable)
            Dim row = dtEspecificaciones.NewRow()
            row("Id") = ddlTipoEspecificacion.SelectedValue
            row("Nombre") = ddlTipoEspecificacion.SelectedItem.Text
            row("Especificacion") = txtEspecificacion.Text
            row("Guardado") = False
            row("Eliminado") = False
            dtEspecificaciones.Rows.Add(row)
            Session("EspecificacionesCotizacion") = dtEspecificaciones

            grdEspecificaciones.DataSource = dtEspecificaciones.Select("Eliminado = 'False'").CopyToDataTable()
            grdEspecificaciones.DataBind()

            ddlTipoEspecificacion.SelectedIndex = 0
            txtEspecificacion.Text = ""

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub lnkEliminar_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim id = e.CommandArgument
            Dim dtEspecificacion = DirectCast(Session("EspecificacionesCotizacion"), DataTable)
            Dim row = dtEspecificacion.Select(String.Format("Id='{0}'", id))(0)
            If row("Guardado") = True Then
                row("Eliminado") = True
            Else
                dtEspecificacion.Rows.Remove(row)
            End If

            Session("EspecificacionesCotizacion") = dtEspecificacion
            If dtEspecificacion.Select("Eliminado = 'False'").Count > 0 Then
                grdEspecificaciones.DataSource = dtEspecificacion.Select("Eliminado = 'False'").CopyToDataTable()
            Else
                grdEspecificaciones.DataSource = Nothing
            End If

            grdEspecificaciones.DataBind()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub afUploadCotizacion_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        If Session("ArchivosAgregadosCotizacion") Is Nothing Then
            Dim dt As New DataTable()
            dt.Columns.Add("Id")
            dt.Columns.Add("Nombre")
            dt.Columns.Add("URL")
            dt.Columns.Add("FilePath")
            dt.Columns.Add("Guardado")
            dt.Columns.Add("Eliminado")
            Session("ArchivosAgregadosCotizacion") = dt
        End If
        Dim dtFiles = DirectCast(Session("ArchivosAgregadosCotizacion"), DataTable)

        If UploadedFilesStorage Is Nothing Then
            IntelipolisEngine.Helper.UploadControlHelper.AddUploadedFilesStorage(SubmissionID)
        End If

        Dim id = dtFiles.Rows.Count + 1
        Dim tempFileInfo = IntelipolisEngine.Helper.UploadControlHelper.AddUploadedFileInfo(SubmissionID, e.FileName, afUploadCotizacion.PostedFile.InputStream, id)

        Dim row = dtFiles.NewRow()
        row("Id") = tempFileInfo.UniqueId
        row("Nombre") = tempFileInfo.OriginalFileName
        row("Guardado") = False
        row("Eliminado") = False
        row("FilePath") = tempFileInfo.FilePath
        dtFiles.Rows.Add(row)

        afUploadCotizacion.PostedFile.SaveAs(tempFileInfo.FilePath)
        afUploadCotizacion.ClearAllFilesFromPersistedStore()
        afUploadCotizacion.ClearFileFromPersistedStore()

    End Sub

    Protected Sub lnqQuitarArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim id = e.CommandArgument
            Dim dtFiles = DirectCast(Session("ArchivosAgregadosCotizacion"), DataTable)
            Dim row = dtFiles.Select(String.Format("Id='{0}'", id))(0)
            If row("Guardado") = True Then
                row("Eliminado") = True
            Else
                dtFiles.Rows.Remove(row)
            End If

            Session("ArchivosAgregadosCotizacion") = dtFiles
            If dtFiles.Select("Eliminado = 'False'").Count > 0 Then
                grdArchivosCotizacion.DataSource = dtFiles.Select("Eliminado = 'False'").CopyToDataTable()
            Else
                grdArchivosCotizacion.DataSource = Nothing
            End If

            grdArchivosCotizacion.DataBind()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnRecargaArchivos_Click(sender As Object, e As EventArgs)
        afUploadCotizacion.ClearAllFilesFromPersistedStore()
        afUploadCotizacion.ClearFileFromPersistedStore()

        Dim dtFiles = DirectCast(Session("ArchivosAgregadosCotizacion"), DataTable)
        If dtFiles.Rows.Count > 0 Then
            grdArchivosCotizacion.DataSource = dtFiles.Select("Eliminado = 'False'").CopyToDataTable()
        Else
            grdArchivosCotizacion.DataSource = dtFiles
        End If
        grdArchivosCotizacion.DataBind()
    End Sub

    Protected Sub btnEliminarCotizacion_Click(sender As Object, e As EventArgs)
        Try
            Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
            engine.EliminaCotizacion(hdnIdCotizacionEliminar.Value)
            CargaCotizaciones()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub ddlTipoProducto_SelectedIndexChanged(sender As Object, e As EventArgs)
        pnlInfoProducto.Visible = (ddlTipoProducto.SelectedValue = "1")
        pnlInfoServicio.Visible = (ddlTipoProducto.SelectedValue = "2")
        If ddlTipoProducto.SelectedValue = "1" Then
            tituloNombre.InnerText = "Nombre del producto:"
        Else
            tituloNombre.InnerText = "Descripción del servicio"
        End If
    End Sub

    Protected Sub chkTodos_CheckedChanged(sender As Object, e As EventArgs)
        Dim dt = DirectCast(Session("TotalCotizaciones"), DataTable)
        For Each row In dt.Rows
            row("seleccionado") = (DirectCast(sender, CheckBox)).Checked
        Next

        grdCotizacion.DataSource = dt
        grdCotizacion.DataBind()

        Session("TotalCotizaciones") = dt
    End Sub

    Protected Sub btnSolicitarAutorizacion_Click(sender As Object, e As EventArgs)
        Try
            Dim cotizaciones As New List(Of Integer)
            For Each row As GridViewRow In grdCotizacion.Rows
                Dim checkbox = DirectCast(row.FindControl("chkSeleccionar"), CheckBox)
                If checkbox.Checked Then
                    Dim hdn = DirectCast(row.FindControl("hdnId"), HiddenField)
                    cotizaciones.Add(hdn.Value)
                End If
            Next

            If cotizaciones.Count() = 0 Then
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", "Debe de seleccionar al menos una cotización."), True)
            End If

            Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
            engine.SolicitarAutorizacionControlAdministrativo(cotizaciones, Session("Clave_empl"))

            CargaCotizaciones()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnGuardarSeleccionDocumento_Click(sender As Object, e As EventArgs)
        Try
            Dim idCotizacion = 0
            Dim idDocumento = 0
            For Each item As RepeaterItem In rptDocumentos.Items
                Dim checkbox = DirectCast(item.FindControl("chkDocumentos"), CheckBox)
                If checkbox.Checked Then
                    idCotizacion = DirectCast(item.FindControl("hdnIdCotizacion"), HiddenField).Value
                    idDocumento = DirectCast(item.FindControl("hdnIdDocumento"), HiddenField).Value
                    Exit For
                End If
            Next

            Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
            engine.GuardaSeleccionDocumento(idCotizacion, idDocumento, Session("Clave_empl"))
            CargaCotizaciones()
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestraModal", "ocultaModalDocumentos();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub
End Class