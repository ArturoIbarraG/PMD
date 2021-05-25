Imports System.Data.SqlClient

Public Class ucAgregaComentarioCotizacion
    Inherits System.Web.UI.UserControl
    Dim con As New conexion
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnAgregaComentario_Click(sender As Object, e As EventArgs)
        Try
            Dim mensaje As String = ""
            Dim correo As String = ""
            'Using data As New DB(con.Conectar())
            '    Dim params() As SqlParameter = New SqlParameter() {
            '        New SqlParameter("@idCotizacionDetalle", hdnIdCotizacionDetalle.Value),
            '        New SqlParameter("@empleado", Session("Clave_empl")),
            '        New SqlParameter("@comentario", txtComentario.Text)}
            '    data.EjecutaCommand("InsertaComentarioCotizacion", params)

            '    Dim paramC() As SqlParameter = New SqlParameter() {New SqlParameter("@idCotizacionDetalle", hdnIdCotizacionDetalle.Value)}
            '    Dim row = data.ObtieneDatos("ObtieneCotizacionDetalle", paramC).Tables(0).Rows(0)
            '    mensaje = String.Format("El usuario {0} ha agregado un comentario en el producto {1} de la cotizacion con folio {2}.", row("usuarioEnvio"), row("producto"), row("folio"))

            '    '
            '    correo = data.ObtieneDatos("ObtieneCotizacionDetalle", New SqlParameter() {New SqlParameter("@clave_empl", Session("Clave_empl"))}).Tables(0).Rows(0)("email")
            'End Using

            'Dim notificaciones = New IntelipolisEngine.Helper.NotificationHelper()

            'notificaciones.EnviaCorreo("", "Nuevo comentario en cotizacion", mensaje)
            Dim engine As New IntelipolisEngine.PMD.Cotizaciones()
            engine.AgregaComentarioCotizacion(hdnIdCotizacionDetalle.Value, Session("Clave_empl"), txtComentario.Text)

            txtComentario.Text = ""
            CargaComentariosProducto(hdnIdCotizacionDetalle.Value)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updComentarios, updComentarios.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Public Sub CargaComentariosProducto(idCotizacionDetalle As Integer)
        hdnIdCotizacionDetalle.Value = idCotizacionDetalle

        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idCotizacionDetalle", idCotizacionDetalle)}

            Dim dt = data.ObtieneDatos("ObtieneComentariosCotizacionDetalle", params).Tables(0)
            Session("ComentariosCotizacion") = dt
            rptComentarios.DataSource = dt
            rptComentarios.DataBind()

        End Using

        ScriptManager.RegisterStartupScript(updComentarios, updComentarios.GetType(), "ocultaModal", "abreModalComentarios();", True)
    End Sub
End Class