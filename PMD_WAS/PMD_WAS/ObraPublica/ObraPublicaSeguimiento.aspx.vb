Imports System.Data.SqlClient

Public Class ObraPublicaSeguimiento
    Inherits System.Web.UI.Page

    Dim con As New OPConexion
    Dim TiposAdjudicacion() As String = {"CONVOCATORIA PUBLICA"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarObrasPublicas()
    End Sub

    Private Sub CargarObrasPublicas()

        Using data As New DB(con.conectar())

            gridObrasPublicas.DataSource = data.ObtieneDatos("CargarObrasPublicas", Nothing)
            gridObrasPublicas.DataBind()

        End Using
    End Sub

    Protected Sub btnVerEditar_Command(sender As Object, e As CommandEventArgs)

        limpiarCampos()

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

            If dr("opEstatus") = "Pendiente" Then
                txtOPNumeroContrato.Enabled = False
                ddlOPContratista.Enabled = False
                ddlOPTipoAdjudicacion.Enabled = False
                txtOPNumeroAdjudicacion.Enabled = False
                txtOPMontoTotal.Enabled = False
                txtOPMontoAnticipo.Enabled = False
                txtOPFechaInicio.Enabled = False
                txtOPFechaTerminacion.Enabled = False
                txtOPFechaFirma.Enabled = False
                txtOPFechaEstimaciones.Enabled = False
            Else
                'Cargar los tipos de adjudicación y los contratistas
                ddlOPTipoAdjudicacion.DataSource = TiposAdjudicacion
                ddlOPTipoAdjudicacion.DataBind()
                cargarContratistas()

                txtOPNumeroContrato.Text = dr("opNumeroContrato")
                ddlOPContratista.SelectedValue = dr("opContratistaID")
                ddlOPTipoAdjudicacion.Text = dr("opTipoAdjudicacion")
                txtOPNumeroAdjudicacion.Text = dr("opNumeroAdjudicacion")
                txtOPMontoTotal.Text = dr("opMontoTotal")
                txtOPMontoAnticipo.Text = dr("opMontoAnticipo")
                txtOPFechaInicio.Text = DateTime.Parse(dr("opFechaInicio")).ToString("yyyy-MM-dd")
                txtOPFechaTerminacion.Text = DateTime.Parse(dr("opFechaTerminacion")).ToString("yyyy-MM-dd")
                txtOPFechaFirma.Text = DateTime.Parse(dr("opFechaFirmaContrato")).ToString("yyyy-MM-dd")
                txtOPFechaEstimaciones.Text = dr("opFechaEstimaciones")

                txtOPMontoTotal.Text = montoAsignacionString
            End If

        End Using

        ScriptManager.RegisterStartupScript(updObraPublicaSeguimiento, updObraPublicaSeguimiento.GetType(), "abreModalOP", "abreModalOP();", True)

    End Sub

    Private Sub cargarContratistas()
        Using data As New DB(con.conectar())
            ddlOPContratista.DataSource = data.ObtieneDatos("CargarContratistas", Nothing)
            ddlOPContratista.DataTextField = "conNombre"
            ddlOPContratista.DataValueField = "conID"
            ddlOPContratista.DataBind()
        End Using
    End Sub

    Private Sub limpiarCampos()
        txtOPID.Text = ""
        txtOPNombre.Text = ""
        txtOPDescripcion.Text = ""
        txtOPOrigenFondos.Text = ""
        txtOPMontoAsignacion.Text = ""
        txtOPUbicacion.Text = ""
        txtOPNumeroContrato.Text = ""
        'ddlOPContratista.SelectedValue = 0
        'ddlOPTipoAdjudicacion.SelectedValue = 0
        txtOPNumeroAdjudicacion.Text = ""
        txtOPMontoTotal.Text = ""
        txtOPMontoAnticipo.Text = ""
        txtOPFechaInicio.Text = ""
        txtOPFechaTerminacion.Text = ""
        txtOPFechaFirma.Text = ""
        txtOPFechaEstimaciones.Text = ""
    End Sub

End Class