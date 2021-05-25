Imports System.Data.SqlClient
Partial Class LineasAprobadas
    Inherits System.Web.UI.Page
    Dim con As New Class1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Carga el año
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@idAdmon", 1)
                }

                ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params)
                ddlAnio.DataTextField = "Año"
                ddlAnio.DataValueField = "Año"
                ddlAnio.DataBind()
            End Using
            ddlAnio.Items.Insert(0, New ListItem("Selecciona el año", "0"))
            ddlAnio.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ddlLineas_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga secretarias
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 1),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaría", "0"))

    End Sub

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 1),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlDependencia.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDependencia.DataTextField = "Nombr_direccion"
            ddlDependencia.DataValueField = "IdDireccion"
            ddlDependencia.DataBind()
        End Using
        ddlDependencia.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
    End Sub

    Protected Sub ddlDependencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Presupuesto Lineas
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                    New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue)
                }

            grdPresupuestos.DataSource = data.ObtieneDatos("ObtienePresupuestosAprobados", params)
            grdPresupuestos.DataBind()
        End Using
    End Sub


End Class
