Imports System.Data.SqlClient

Public Class SolicitudesRequisiciones
    Inherits System.Web.UI.Page

    Dim con As New conexion
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Solicitud de requisiciones")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Response.Redirect("~/ControlAdministrativo/Requisiciones/Seguimiento.aspx")


            CargaInformacionAdmin()
            CargaSecretarias()
            CargaProveedores()
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
                New SqlParameter("@clave_empl", -1)
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))
    End Sub

    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''Carga Secretarias
        'Using data As New DataInfo()

        '    ddlSecretaria.DataSource = data.ObtieneSecretarias
        '    ddlSecretaria.DataTextField = "nombr_secr"
        '    ddlSecretaria.DataValueField = "clave_secr"
        '    ddlSecretaria.DataBind()
        'End Using
        'ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))
        'Using data As New DB(con.conectar())
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", hdnAdmon.Value),
                New SqlParameter("@clave_empl", -1)
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

    End Sub
    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.Conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", hdnAdmon.Value),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", -1)
            }

            ddlDireccion.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDireccion.DataTextField = "Nombr_direccion"
            ddlDireccion.DataValueField = "IdDireccion"
            ddlDireccion.DataBind()
        End Using
        ddlDireccion.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))

    End Sub

    Protected Sub ddlMes_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaRequisiciones()
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaRequisiciones()
    End Sub

    Private Sub CargaRequisiciones()
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@mes", hdnMes.Value),
                New SqlParameter("@anio", hdnAnio.Value)}

            Dim dt = data.ObtieneDatos("ObtieneSolicitudRequisicion", params).Tables(0)
            Session("SolicitudeRequisicion") = dt
            grdSolicitudes.DataSource = dt
            grdSolicitudes.DataBind()

        End Using
    End Sub

    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaRequisiciones()
    End Sub

    Protected Sub ddlSubActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaRequisiciones()
    End Sub

    Protected Sub chkTodos_CheckedChanged(sender As Object, e As EventArgs)
        Dim dt = DirectCast(Session("SolicitudeRequisicion"), DataTable)
        For Each row In dt.Rows
            row("seleccionado") = (DirectCast(sender, CheckBox)).Checked
        Next

        grdSolicitudes.DataSource = dt
        grdSolicitudes.DataBind()

        Session("SolicitudeRequisicion") = dt
    End Sub

    Protected Sub btnEnviaOrdenesSurtido_Click(sender As Object, e As EventArgs)
        Try
            Dim solicitudes As New List(Of Integer)
            For Each row As GridViewRow In grdSolicitudes.Rows
                Dim checkbox = DirectCast(row.FindControl("chkSeleccionar"), CheckBox)
                If checkbox.Checked Then
                    Dim hdn = DirectCast(row.FindControl("hdnId"), HiddenField)
                    Dim sol As New IntelipolisEngine.PMD.Requisiciones.SolicitudRequisicion
                    solicitudes.Add(hdn.Value)
                End If
            Next

            If solicitudes.Count() <= 0 Then
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "info", "Favor de validar", "Favor de seleccionar al menos una solicitud para poder ser autorizada."), True)
            Else
                Dim engine As New IntelipolisEngine.PMD.Requisiciones()
                engine.AutorizaRequisicion(solicitudes, ddlProveedor.SelectedValue, ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue, hdnAnio.Value, hdnMes.Value, Session("Clave_empl"))

                Session("SolicitudeRequisicion") = Nothing
                CargaRequisiciones()
                ScriptManager.RegisterStartupScript(updEnviar, updEnviar.GetType(), "ocultaModal", "ocultaModalAutorizacion();", True)
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las solicitudes de Requisicion fueron autorizadas correctamente."), True)

            End If
        Catch ex As Exception
            'lblInformacionRequisicion.Text = ex.ToString();
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub lnkTotalMateriales_Command(sender As Object, e As CommandEventArgs)
        Dim folio = Integer.Parse(e.CommandArgument)
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@id", folio)
            }

            Dim dt = data.ObtieneDatos("ObtieneDetalleSolicitudRequisicion", params).Tables(0)
            grdListaMateriales.DataSource = dt
            grdListaMateriales.DataBind()

            lblTotalMateriales.Text = String.Format("Total: {0:c2}", dt.Compute("SUM(total)", ""))
            '
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestra_pdf", "muestraModalMateriales()", True)
        End Using
    End Sub

    Protected Sub btnRechazarSolicitud_Click(sender As Object, e As EventArgs)
        Try
            Dim solicitudes As New List(Of Integer)
            For Each row As GridViewRow In grdSolicitudes.Rows
                Dim checkbox = DirectCast(row.FindControl("chkSeleccionar"), CheckBox)
                If checkbox.Checked Then
                    Dim hdn = DirectCast(row.FindControl("hdnId"), HiddenField)
                    Dim sol As New IntelipolisEngine.PMD.Requisiciones.SolicitudRequisicion
                    solicitudes.Add(hdn.Value)
                End If
            Next

            If solicitudes.Count() <= 0 Then
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "info", "Favor de validar", "Favor de seleccionar al menos una solicitud para poder ser autorizada."), True)
            Else
                Dim engine As New IntelipolisEngine.PMD.Requisiciones()
                engine.RechazaRequisicion(solicitudes, ddlMotivoRechazo.SelectedValue, txtComentariosRechazo.Text, Session("Clave_empl"))

                Session("SolicitudeRequisicion") = Nothing
                CargaRequisiciones()
                ScriptManager.RegisterStartupScript(updEnviar, updEnviar.GetType(), "ocultaModal", "ocultaModalRechazo();", True)
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las solicitudes de Requisicion fueron rechazadas correctamente."), True)

            End If
        Catch ex As Exception
            'lblInformacionRequisicion.Text = ex.ToString();
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Private Sub CargaProveedores()
        Dim con As New Class1
        Using data As New DB(con.conectar())

            Dim dt = data.ObtieneDatos("CargaCatalogoProveedor", Nothing).Tables(0)
            ddlProveedor.DataValueField = "id"
            ddlProveedor.DataTextField = "nombre"
            ddlProveedor.DataSource = dt
            ddlProveedor.DataBind()

        End Using
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