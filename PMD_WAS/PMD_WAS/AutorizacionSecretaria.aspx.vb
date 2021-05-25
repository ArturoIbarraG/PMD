Imports System.Data.SqlClient

Public Class AutorizacionSecretaria
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Autorización de presupuestos")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaCombos()

            'OBTIENE SECRETARIA
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", Session("Clave_empl"))}
                ViewState("IdSecretaria") = data.ObtieneDatos("ObtieneEmpleado", params).Tables(0).Rows(0)("IdSecretaria")
            End Using

            Try
                ddlAnio.SelectedValue = Request.Cookies("pmd_anio").Value
            Catch ex As Exception
            End Try
        End If
    End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaPresupuestoDirecciones()
    End Sub

    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaAnios()
    End Sub

    Private Sub CargaCombos()
        CargaAdmons()
        CargaAnios()
    End Sub

    Private Sub CargaAdmons()

        Using data As New DB(con.conectar())
            'Carga Administracion
            ddlAdmon.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
            ddlAdmon.DataTextField = "Nombr_admon"
            ddlAdmon.DataValueField = "Cve_admon"
            ddlAdmon.DataBind()
            ddlAdmon.Items.Insert(0, New ListItem("Selecciona la administración", "0"))
        End Using
    End Sub

    Private Sub CargaAnios()

        Using data As New DB(con.conectar())
            'Carga el año
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdmon.SelectedValue)}

            ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params)
            ddlAnio.DataTextField = "Año"
            ddlAnio.DataValueField = "Año"
            ddlAnio.DataBind()
        End Using
    End Sub

    Private Sub CargaPresupuestoDirecciones()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@IdSecretaria", ViewState("IdSecretaria"))}

            Dim dtPresupuesto = data.ObtieneDatos("CargaAutorizacionesSecretaria", params).Tables(0)
            grdAutorizaciones.DataSource = dtPresupuesto
            grdAutorizaciones.DataBind()

        End Using
    End Sub

    Protected Sub lnkPresupuesto_Command(sender As Object, e As CommandEventArgs)
        Try
            If e.CommandName = "detalle" Then
                Dim idDir As Integer = e.CommandArgument.ToString().Split("|")(0)
                Dim direccion As String = e.CommandArgument.ToString().Split("|")(1)

                ucDetallePresupuesto.MuestraDesglocePresupuesto(ViewState("IdSecretaria"), idDir, ddlAnio.SelectedValue, direccion)
                ScriptManager.RegisterStartupScript(updAutorizaciones, updAutorizaciones.GetType(), "muestraModal", "muestraModalDetallePresupuesto();", True)
            ElseIf e.CommandName = "autorizar" Then
                Dim idSec As Integer = e.CommandArgument.ToString().Split("|")(0)
                Dim idDir As Integer = e.CommandArgument.ToString().Split("|")(1)
                Dim direccion As String = e.CommandArgument.ToString().Split("|")(2)
                Dim total As Decimal = e.CommandArgument.ToString().Split("|")(3)

                lblInfoPresupuesto.Text = String.Format("Se autorizará el Presupuesto de la dirección {0} por un monto total de {1:c2}", direccion, total)
                hdnIdDireccionSeleccionada.Value = idDir
                hdnIdSecretariaSeleccionada.Value = idSec
                hdnTotalPresupuesto.Value = total
                hdnNombreDireccionSeleccionada.Value = direccion
                ScriptManager.RegisterStartupScript(updAutorizaciones, updAutorizaciones.GetType(), "muestraModal", "muestraModalAutorizacion();", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnGuardarAutorizacion_Click(sender As Object, e As EventArgs)
        Try
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@IdSecretaria", hdnIdSecretariaSeleccionada.Value),
                    New SqlParameter("@IdDireccion", hdnIdDireccionSeleccionada.Value),
                    New SqlParameter("@ClaveEmpleado", Session("Clave_empl")),
                    New SqlParameter("@Total", hdnTotalPresupuesto.Value),
                    New SqlParameter("@Comentarios", txtObservaciones.Text)}

                data.EjecutaCommand("GuardaAutorizacionSecretaria", params)

#Region "GUARDA LOG"
                Helper.GuardaLog(Session("clave_empl"), String.Format("Se ha autorizado el presupuesto de {0}", hdnNombreDireccionSeleccionada.Value))
#End Region

                ScriptManager.RegisterStartupScript(updAutorizaciones, updAutorizaciones.GetType(), "muestraModal", "ocultaModalAutorizacion();", True)
                ddlAnio_SelectedIndexChanged(Nothing, Nothing)
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class