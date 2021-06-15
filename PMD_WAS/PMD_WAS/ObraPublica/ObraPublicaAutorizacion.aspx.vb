Imports System.Data.SqlClient

Public Class ObraPublicaAutorizacion
    Inherits System.Web.UI.Page

    Dim con As New OPConexion

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarObrasPublicas()
    End Sub

    Private Sub CargarObrasPublicas()

        Using data As New DB(con.conectar())

            gridObrasPublicas.DataSource = data.ObtieneDatos("CargarObrasPublicas", Nothing)
            gridObrasPublicas.DataBind()

        End Using
    End Sub

    Protected Sub btnAutorizarRechazar_Command(sender As Object, e As CommandEventArgs)

        Dim id = e.CommandArgument

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@opID", id)}

            Dim dr = data.ObtieneDatos("CargarObraPublica", params).Tables(0).Rows(0)

            txtOPNombre.Text = dr("opNombre")
            txtOPDescripcion.Text = dr("opDescripcion")
            txtOPOrigenFondos.Text = "$" + dr("opOrigenFondos")
            txtOPMontoAsignacion.Text = dr("opMontoAsignacion")
            txtOPUbicacion.Text = dr("opUbicacion")

        End Using

        ScriptManager.RegisterStartupScript(updObraPublicaAutorizacion, updObraPublicaAutorizacion.GetType(), "abreModalOP", "abreModalOP();", True)
    End Sub

    Protected Sub btnAutorizarOP_Command(sender As Object, e As CommandEventArgs)
        If txtOPNumeroContrato.Text = "" Or
           txtOPContratista.Text = "" Or
           txtOPTipoAdjudicacion.Text = "" Or
           txtOPNumeroAdjudicacion.Text = "" Or
           txtOPMontoTotal.Text = "" Or
           txtOPMontoAnticipo.Text = "" Or
           txtOPFechaInicio.Text = "" Or
           txtOPFechaTerminacion.Text = "" Or
           txtOPFechaFirma.Text = "" Or
           txtOPFechaEstimaciones.Text = "" Then
            MsgBox("Faltan campos por llenar.")
        Else
            MsgBox("Todos los campos han sido llenados.")
        End If
    End Sub
End Class