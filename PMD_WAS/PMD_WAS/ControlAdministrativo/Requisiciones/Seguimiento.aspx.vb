Imports System.Data.SqlClient

Public Class Seguimiento1
    Inherits System.Web.UI.Page

    Dim con As New conexion
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Seguimiento")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'If Session("Puesto") = "4" Then
            '    Response.Redirect("~/ControlAdministrativo/SolicitudesRequisiciones.aspx")
            'End If

            CargaInformacionAdmin()
            CargaSecretarias()
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
                New SqlParameter("@clave_empl", Session("Clave_empl"))
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
                New SqlParameter("@clave_empl", Session("Clave_empl"))
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
                New SqlParameter("@anio", hdnAnio.Value),
                New SqlParameter("@todos", True)}

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

    Protected Sub lnkTotalMateriales_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim folio = Integer.Parse(e.CommandArgument)
            If e.CommandName = "materiales" Then

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
            ElseIf e.CommandName = "estatus" Then
                Dim con As New Class1
                Using data As New DB(con.conectar())
                    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idSolicitud", folio)}

                    Dim dr = data.ObtieneDatos("ObtieneInformacionSolicitudRequisicion", params).Tables(0).Rows(0)
                    Dim estatus = dr("estatus")
                    If estatus = "Enviada" Then
                        lblInfoEstatus.Text = String.Format("La solicitud fue <b>Generada</b> por <b>{0}</b> el <b>{1:dd/MMM/yyyy HH:mm}</b> con folio <b>{2}</b>", dr("usuarioSolicitud"), dr("fechaSolicitud"), dr("folio"))
                    ElseIf estatus = "Autorizada" Then
                        lblInfoEstatus.Text = String.Format("La solicitud fue <b>Autorizada</b> por <b>{0}</b> el <b>{1:dd/MMM/yyyy HH:mm}</b> con folio de requisicion <b>{2}</b>", dr("usuarioAutorizado"), dr("fechaAutorizado"), dr("folio"))
                    ElseIf estatus = "Rechazada" Then
                        lblInfoEstatus.Text = String.Format("La solicitud fue <b>Rechazada</b> por <b>{0}</b> el <b>{1:dd/MMM/yyyy HH:mm}</b> con el siguiente motivo: <b>{2}</b><br />Comentarios: <b>{3}</b>", dr("usuarioRechazo"), dr("fechaRechazo"), dr("motivoRechazo"), dr("comentarios"))
                    End If

                    '
                    ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestra_info", "muestraModalInfoEstatus()", True)
                End Using

            ElseIf e.CommandName = "imprimir" Then
                Dim helper = New IntelipolisEngine.PMD.Requisiciones()
                Dim url = helper.RegresaFormatoSolicitudRequisicion(folio)

                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "muestra_pdf", String.Format("openModalIFrame('{0}')", ResolveClientUrl(url)), True)


            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try

    End Sub

    Protected Sub lnkImprimirFolio_Command(sender As Object, e As CommandEventArgs)

    End Sub
End Class