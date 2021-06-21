Imports System.Data.SqlClient

Public Class ObraPublicaAutorizacion
    Inherits System.Web.UI.Page

    Dim con As New OPConexion
    Dim TiposAdjudicacion() As String = {"CONVOCATORIA PUBLICA"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarObrasPublicasPendientes()
    End Sub

    Private Sub CargarObrasPublicasPendientes()

        Using data As New DB(con.conectar())

            gridObrasPublicas.DataSource = data.ObtieneDatos("CargarObrasPublicasPendientes", Nothing)
            gridObrasPublicas.DataBind()

        End Using
    End Sub

    Protected Sub btnAutorizarRechazar_Command(sender As Object, e As CommandEventArgs)

        cargarContratistas()

        Dim id = e.CommandArgument

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@opID", id)}

            Dim dr = data.ObtieneDatos("CargarObraPublica", params).Tables(0).Rows(0)

            txtOPID.Text = dr("opID")
            txtOPNombre.Text = dr("opNombre")
            txtOPDescripcion.Text = dr("opDescripcion")
            txtOPOrigenFondos.Text = dr("opOrigenFondos")
            Dim montoAsignacionString = dr("opMontoAsignacion")
            txtOPMontoAsignacion.Text = montoAsignacionString.ToString()
            txtOPUbicacion.Text = dr("opUbicacion")

            txtOPMontoTotal.Text = montoAsignacionString

            'Cargar los tipos de adjudicación
            ddlOPTipoAdjudicacion.DataSource = TiposAdjudicacion
            ddlOPTipoAdjudicacion.DataBind()

        End Using

        ScriptManager.RegisterStartupScript(updObraPublicaAutorizacion, updObraPublicaAutorizacion.GetType(), "abreModalOP", "abreModalOP();", True)
    End Sub

    Protected Sub btnAutorizarOP_Command(sender As Object, e As CommandEventArgs)
        If txtOPNumeroContrato.Text = "" Or
           ddlOPContratista.Text = "" Or
           ddlOPTipoAdjudicacion.Text = "" Or
           txtOPNumeroAdjudicacion.Text = "" Or
           txtOPMontoTotal.Text = "" Or
           txtOPMontoAnticipo.Text = "" Or
           txtOPFechaInicio.Text = "" Or
           txtOPFechaTerminacion.Text = "" Or
           txtOPFechaFirma.Text = "" Or
           txtOPFechaEstimaciones.Text = "" Then

            ScriptManager.RegisterStartupScript(updObraPublicaAutorizacion, updObraPublicaAutorizacion.GetType(), "muestraMensajeError", "muestraErrorFaltanCampos();", True)

        Else
            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@opID", txtOPID.Text),
                    New SqlParameter("@numcon", txtOPNumeroContrato.Text),
                    New SqlParameter("@conID", ddlOPContratista.SelectedValue),
                    New SqlParameter("@tipoAdj", ddlOPTipoAdjudicacion.Text),
                    New SqlParameter("@numAdj", txtOPNumeroAdjudicacion.Text),
                    New SqlParameter("@monTot", Decimal.Parse(txtOPMontoTotal.Text)),
                    New SqlParameter("@monAnt", Decimal.Parse(txtOPMontoAnticipo.Text)),
                    New SqlParameter("@fechaIni", txtOPFechaInicio.Text),
                    New SqlParameter("@fechaTer", txtOPFechaTerminacion.Text),
                    New SqlParameter("@fechaFir", txtOPFechaFirma.Text),
                    New SqlParameter("@fechaEst", txtOPFechaEstimaciones.Text)
                }

                data.EjecutaCommand("AutorizarObraPublica", params)

                ScriptManager.RegisterStartupScript(updObraPublicaAutorizacion, updObraPublicaAutorizacion.GetType(), "confirmaAutorizacion", "muestraConfirmaAutorizacion();", True)

            End Using
        End If
    End Sub

    Private Sub cargarContratistas()
        Using data As New DB(con.conectar())
            ddlOPContratista.DataSource = data.ObtieneDatos("CargarContratistas", Nothing)
            ddlOPContratista.DataTextField = "conNombre"
            ddlOPContratista.DataValueField = "conID"
            ddlOPContratista.DataBind()
        End Using
    End Sub

End Class