Imports System.Data.SqlClient

Public Class ucVistaCotizacion
    Inherits System.Web.UI.UserControl
    Dim con As New conexion
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub CargaCotizacionDetalle(idDetalle As Integer)
        hdnIdCotizacionDetalle.Value = idDetalle
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idDetalle", idDetalle)}
            Dim row = data.ObtieneDatos("ObtieneCotizacionDetalle", params).Tables(0).Rows(0)
            lblInformacionCotizacion.Text = String.Format("La cotización con folio {0} fue generada el {1:dd/MMM/yyyy} por {2}. <hr />", row("folio"), row("fechaEnvio"), row("usuarioEnvio"))

            'Dim params2() As SqlParameter = New SqlParameter() {New SqlParameter("@id", ID)}
            'Dim row2 = data.ObtieneDatos("ObtieneProductosCotizacion", params2).Tables(0).Rows(0)
            lblNombreProducto.Text = row("producto")
            lblComentarios.Text = row("comentarios")

            lblJustificacion.Text = row("justificacion")
            lblLugarEntrega.Text = row("lugarEntrega")

            If (row("idTipo") = 1) Then
                lblTipoCotizacion.Text = "Producto"
                tituloNombre.InnerText = "Nombre del producto:"
                lblCantidad.Text = row("cantidad")
                lblUnidad.Text = row("unidad")
            Else
                lblTipoCotizacion.Text = "Servicio"
                tituloNombre.InnerText = "Descripción del servicio"
                lblVigencia.Text = row("vigencia")
                lblFechaInicio.Text = String.Join("{0:dd-MM-yyyy}", row("fechaInicio"))
                lblFechaTerminacion.Text = String.Join("{0:dd-MM-yyyy}", row("fechaTermino"))
            End If
            pnlInfoProducto.Visible = row("idTipo") = 1
            pnlInfoServicio.Visible = row("idTipo") = 2

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


    End Sub

End Class