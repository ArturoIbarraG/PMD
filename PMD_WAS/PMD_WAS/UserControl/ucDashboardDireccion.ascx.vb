Imports System.Data.SqlClient

Public Class ucDashboardDireccion
    Inherits System.Web.UI.UserControl
    Dim con As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Using data As New DB(con.conectar())

                'Carga Administraciones
                ddlAdmin.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmin.DataTextField = "Nombr_admon"
                ddlAdmin.DataValueField = "Cve_admon"
                ddlAdmin.DataBind()

                'Carg Anios
                Dim paramsAnios() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdmin.SelectedValue)}
                ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", paramsAnios)
                ddlAnio.DataTextField = "Año"
                ddlAnio.DataValueField = "Año"
                ddlAnio.DataBind()

                Try
                    ddlAnio.SelectedValue = Request.Cookies("pmd_anio").Value
                Catch ex As Exception
                End Try


                Dim paramsEmppleado() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", Session("Clave_empl"))}
                Dim row = data.ObtieneDatos("ObtieneEmpleado", paramsEmppleado).Tables(0).Rows(0)

                'Carga secretaria
                Dim paramsSec() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdmin.SelectedValue), New SqlParameter("@clave_empl", Session("Clave_empl"))}
                ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", paramsSec)
                ddlSecretaria.DataTextField = "Nombr_secretaria"
                ddlSecretaria.DataValueField = "IdSecretaria"
                ddlSecretaria.DataBind()
                ddlSecretaria.SelectedValue = row("IdSecretaria")

                'Carga Direccion
                Dim paramsDir() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdmin.SelectedValue), New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue), New SqlParameter("@clave_empl", Session("Clave_empl"))}
                ddlDireccion.DataSource = data.ObtieneDatos("ObtieneDireciones", paramsDir)
                ddlDireccion.DataTextField = "Nombr_direccion"
                ddlDireccion.DataValueField = "IdDireccion"
                ddlDireccion.DataBind()
                ddlDireccion.SelectedValue = row("IdDireccion")

                ddl_SelectedIndexChanged(Nothing, Nothing)

            End Using

        End If
    End Sub

    Protected Sub ddl_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Request.Cookies("pmd_anio").Value = ddlAnio.SelectedValue
        Catch ex As Exception

        End Try


        Using data As New DB(con.conectar())

            Dim paramsPres() As SqlParameter = New SqlParameter() {New SqlParameter("@IdAdmon", ddlAdmin.SelectedValue), New SqlParameter("@IdDependencia", Session("clave_depe")), New SqlParameter("@IdSecretaria", -1), New SqlParameter("@Anio", ddlAnio.SelectedValue)}

            Try
                Dim rowValue = data.ObtieneDatos("ObtieneInformacionPresupuesto", paramsPres).Tables(0).Rows(0)
                lblPresupuestoAsignado.Text = String.Format("{0:C2}", rowValue("Presupuesto"))
                lblPresupuestoAutorizado.Text = String.Format("{0:C2}", rowValue("Autorizado"))
                lblPresupuestoCapturado.Text = String.Format("{0:C2}", rowValue("Capturado"))
                lblPresupuestoComprometido.Text = String.Format("{0:C2}", rowValue("Comprometido"))
                lblPresupuestoDevengado.Text = String.Format("{0:C2}", rowValue("Devengado"))
            Catch ex As Exception
                lblPresupuestoAsignado.Text = String.Format("{0:C2}", 0)
                lblPresupuestoAutorizado.Text = String.Format("{0:C2}", 0)
                lblPresupuestoCapturado.Text = String.Format("{0:C2}", 0)
                lblPresupuestoComprometido.Text = String.Format("{0:C2}", 0)
                lblPresupuestoDevengado.Text = String.Format("{0:C2}", 0)
            End Try
        End Using
    End Sub
End Class