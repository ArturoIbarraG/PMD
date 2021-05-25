Imports System.Data.SqlClient

Public Class CotizacionRequisicion
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

            CargaInformacionAdmin()
            CargaSecretarias()
            CargaCotizaciones()
            CargaMotivosRechazo()

            LimpiarCampos()
        End If
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

            Dim dt = data.ObtieneDatos("ObtieneCotizacionesAdquisiciones", params).Tables(0)
            Session("TotalCotizacionesAdquisiciones") = dt
            grdCotizacion.DataSource = dt
            grdCotizacion.DataBind()

        End Using

        pnlAgregarCotizacion.Visible = True
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        If e.CommandName = "oficio" Then
            Dim folio = e.CommandArgument
            Dim helper = New IntelipolisEngine.PMD.Cotizaciones()
            Dim url = helper.GeneraDocumento(folio)

            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestra_pdf", String.Format("openPDFIFrame('{0}')", ResolveClientUrl(url)), True)
        ElseIf e.CommandName = "ver" Then
            CargaCotizacion(e.CommandArgument)
        ElseIf e.CommandName = "comentarios" Then
            ucComentariosCotizacion.CargaComentariosProducto(e.CommandArgument)
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
        ElseIf e.CommandName = "documentos" Then
            hdnIdCotizacion.Value = e.CommandArgument

            Using data As New DB(con.Conectar())

                Dim dtFiles As New DataTable()
                dtFiles.Columns.Add("Id")
                dtFiles.Columns.Add("Nombre")
                dtFiles.Columns.Add("URL")
                dtFiles.Columns.Add("FilePath")
                dtFiles.Columns.Add("Guardado")
                dtFiles.Columns.Add("Eliminado")

                Dim pF() As SqlParameter = New SqlParameter() {New SqlParameter("@id", e.CommandArgument)}
                Dim dtF = data.ObtieneDatos("ObtieneDocumentosCotizacion", pF).Tables(0)
                For Each row In dtF.Rows
                    Dim dRow = dtFiles.NewRow()
                    dRow("Guardado") = True
                    dRow("Eliminado") = False
                    dRow("Id") = row("id")
                    dRow("Nombre") = row("nombreArchivo")
                    dRow("URL") = row("rutaArchivo")
                    dtFiles.Rows.Add(dRow)
                Next
                grdDocumentosCotizacion.DataSource = dtFiles
                grdDocumentosCotizacion.DataBind()
                Session("DocumentosAgregadosCotizacion") = dtFiles
            End Using

            ScriptManager.RegisterStartupScript(updArchivosCotizacion, updArchivosCotizacion.GetType(), "muestra_modal", "abreModalAceptaCotizacion();", True)
        ElseIf e.CommandName = "documentoSeleccionado" Then
            ObtieneDocumentoSeleccionado(e.CommandArgument)
        End If
    End Sub

    Private Sub ObtieneDocumentoSeleccionado(idCotizacion As Integer)
        Using data As New DB(con.Conectar())

            Dim pF() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idCotizacion)}
            Dim row = data.ObtieneDatos("ObtieneDocumentosCotizacionSeleccionado", pF).Tables(0).Rows(0)
            Dim url = row("rutaArchivo")

            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestra_pdf", String.Format("openPDFIFrame('{0}')", ResolveClientUrl("" + url)), True)
        End Using
    End Sub

    Private Sub CargaCotizacion(id As Integer)
        ucCotizacion.CargaCotizacionDetalle(id)
        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "correcto", "muestraModalCotizacion();", True)
    End Sub

    Protected Sub chkTodos_CheckedChanged(sender As Object, e As EventArgs)
        Dim dt = DirectCast(Session("TotalCotizacionesAdquisiciones"), DataTable)
        For Each row In dt.Rows
            row("seleccionado") = (DirectCast(sender, CheckBox)).Checked
        Next

        grdCotizacion.DataSource = dt
        grdCotizacion.DataBind()

        Session("TotalCotizacionesAdquisiciones") = dt
    End Sub


    Protected Sub btnComplementar_Click(sender As Object, e As EventArgs)
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

            Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
            engine.ComplementarCotizacionAdquisiciones(cotizaciones, Session("Clave_empl"), txtComentariosComplementear.Text)

            CargaCotizaciones()
            ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "ocultaModal", "ocultaModalComplementar();", True)
            ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las Cotizaciones fueron cambiadas al estatus Complementar correctamente."), True)
        Catch ex As Exception
            'Response.Redirect("~/Password.aspx")
        End Try
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
                engine.RechazaCotizacionAdquisicioens(cotizaciones, ddlMotivoRechazo.SelectedValue, txtComentariosRechazo.Text, Session("Clave_empl"))

                CargaCotizaciones()
                ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "ocultaModal", "ocultaModalRechazo();", True)
                ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las Cotizaciones fueron rechazadas correctamente."), True)

            End If
        Catch ex As Exception
            'lblInformacionRequisicion.Text = ex.ToString();
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
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

    Protected Sub afUploadCotizacion_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        If Session("DocumentosAgregadosCotizacion") Is Nothing Then
            Dim dt As New DataTable()
            dt.Columns.Add("Id")
            dt.Columns.Add("Nombre")
            dt.Columns.Add("URL")
            dt.Columns.Add("FilePath")
            dt.Columns.Add("Guardado")
            dt.Columns.Add("Eliminado")
            Session("DocumentosAgregadosCotizacion") = dt
        End If
        Dim dtFiles = DirectCast(Session("DocumentosAgregadosCotizacion"), DataTable)

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
            Dim dtFiles = DirectCast(Session("DocumentosAgregadosCotizacion"), DataTable)
            Dim row = dtFiles.Select(String.Format("Id='{0}'", id))(0)
            If row("Guardado") = True Then
                row("Eliminado") = True
            Else
                dtFiles.Rows.Remove(row)
            End If

            Session("ArchivosAgregadosCotizacion") = dtFiles
            If dtFiles.Select("Eliminado = 'False'").Count > 0 Then
                grdDocumentosCotizacion.DataSource = dtFiles.Select("Eliminado = 'False'").CopyToDataTable()
            Else
                grdDocumentosCotizacion.DataSource = Nothing
            End If

            grdDocumentosCotizacion.DataBind()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnRecargaArchivos_Click(sender As Object, e As EventArgs)
        afUploadCotizacion.ClearAllFilesFromPersistedStore()
        afUploadCotizacion.ClearFileFromPersistedStore()

        Dim dtFiles = DirectCast(Session("DocumentosAgregadosCotizacion"), DataTable)
        If dtFiles.Rows.Count > 0 Then
            grdDocumentosCotizacion.DataSource = dtFiles.Select("Eliminado = 'False'").CopyToDataTable()
        Else
            grdDocumentosCotizacion.DataSource = dtFiles
        End If
        grdDocumentosCotizacion.DataBind()
    End Sub

    Private Sub LimpiarCampos()
        SubmissionID = IntelipolisEngine.Helper.UploadControlHelper.GenerateUploadedFilesStorageKey()
        IntelipolisEngine.Helper.UploadControlHelper.AddUploadedFilesStorage(SubmissionID)
    End Sub

    Protected Sub btnGuardarDocumentosCotizacion_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDocumentos = DirectCast(Session("DocumentosAgregadosCotizacion"), DataTable)
            Dim cotizacionEngine = New IntelipolisEngine.PMD.Cotizaciones()
            cotizacionEngine.GuardaDocumentosCotizacion(hdnIdCotizacion.Value, dtDocumentos, Session("Clave_empl"))
            ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "ocultaModal", "cierraModalAceptaCotizacion();", True)
        Catch ex As Exception
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
                    Dim sol As New IntelipolisEngine.PMD.Cotizaciones
                    cotizaciones.Add(hdn.Value)
                End If
            Next

            Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
            engine.AutorizaCotizacionAdquisiciones(cotizaciones, Session("Clave_empl"))

            CargaCotizaciones()
            ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las Cotizaciones fueron cambiadas al estatus Autorizadas correctamente."), True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnHabilitarSolicitudRequisiciones_Click(sender As Object, e As EventArgs)
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

            Dim engine = New IntelipolisEngine.PMD.Cotizaciones()
            engine.HabilitarRequisicion(cotizaciones, Session("Clave_empl"))

            CargaCotizaciones()
            ScriptManager.RegisterStartupScript(updRechazarSolicitud, updRechazarSolicitud.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "El proceso termino correctamente."), True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub
End Class