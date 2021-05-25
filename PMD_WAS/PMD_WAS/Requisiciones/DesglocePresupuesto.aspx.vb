Imports System.Data.SqlClient

Public Class DesglocePresupuesto
    Inherits System.Web.UI.Page

    Dim con As New conexion
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Desgloce de presupuesto")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '
            Using data As New DB(con.Conectar())
                'Carga Administracion
                ddlAdmon.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmon.DataTextField = "Nombr_admon"
                ddlAdmon.DataValueField = "Cve_admon"
                ddlAdmon.DataBind()
                ddlAdmon.Items.Insert(0, New ListItem("Selecciona la administración", "0"))

                'Carga Meses
                ddlMes.DataSource = data.ObtieneDatos("ObtieneMeses", Nothing)
                ddlMes.DataTextField = "Mes"
                ddlMes.DataValueField = "Cve_mes"
                ddlMes.DataBind()

            End Using
        End If
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
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

        'Carga el año
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue)
            }

            ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params)
            ddlAnio.DataTextField = "Año"
            ddlAnio.DataValueField = "Año"
            ddlAnio.DataBind()
        End Using

    End Sub
    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.Conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
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
        CargaDesglocePresupuesto()
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaDesglocePresupuesto()
    End Sub

    Private Sub CargaDesglocePresupuesto()
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@mes", ddlMes.SelectedValue),
                New SqlParameter("@anio", ddlAnio.SelectedValue)}

            grdDesglocePresupuesto.DataSource = data.ObtieneDatos("ObtieneDesglocePresupuestoRequisicion", params).Tables(0)
            grdDesglocePresupuesto.DataBind()

        End Using
    End Sub

End Class