Imports System.Data.SqlClient

Public Class AdministraPOA
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Asignación POA")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                Response.Redirect("~/Password.aspx")
            End If

            'If Session("Puesto") <> "1" Then
            '    Response.Redirect("~/Bienvenido.aspx")
            'End If

            Using data As New DB(con.conectar())
                'Carga Administracion
                ddlAdmon.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmon.DataTextField = "Nombr_admon"
                ddlAdmon.DataValueField = "Cve_admon"
                ddlAdmon.DataBind()
                ddlAdmon.Items.Insert(0, New ListItem("Selecciona la administración", "0"))

            End Using

        End If
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Try

            ddlSecretariaModal_SelectedIndexChanged(Nothing, Nothing)
            ScriptManager.RegisterStartupScript(updAgregaPOA, updAgregaPOA.GetType(), "muestraModal", "abreModalPOA();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub

    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@clave_empl", -1)
            }

            Dim secretarias = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataSource = secretarias
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
            ddlSecretaria.Items.Insert(0, New ListItem("Todos", "-1"))
            ddlSecretaria_SelectedIndexChanged(Nothing, Nothing)

            ddlSecretariaModal.DataSource = secretarias
            ddlSecretariaModal.DataTextField = "Nombr_secretaria"
            ddlSecretariaModal.DataValueField = "IdSecretaria"
            ddlSecretariaModal.DataBind()
            ddlSecretariaModal_SelectedIndexChanged(Nothing, Nothing)

        End Using

        'Carga el año
        Using data As New DB(con.conectar())
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

    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaProgramas()
        CargaPOA()
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaPOA()
    End Sub

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", -1)
            }

            Dim direcciones = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDireccion.DataSource = direcciones
            ddlDireccion.DataTextField = "Nombr_direccion"
            ddlDireccion.DataValueField = "IdDireccion"
            ddlDireccion.DataBind()
            ddlDireccion.Items.Insert(0, New ListItem("Todos", "-1"))

        End Using
    End Sub

    Protected Sub ddlSecretariaModal_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idSecretaria", ddlSecretariaModal.SelectedValue),
                New SqlParameter("@clave_empl", -1)
            }

            Dim direcciones = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDireccionModal.DataSource = direcciones
            ddlDireccionModal.DataTextField = "Nombr_direccion"
            ddlDireccionModal.DataValueField = "IdDireccion"
            ddlDireccionModal.DataBind()
        End Using
    End Sub

    Protected Sub ddlDireccionModal_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub ddlProgramaModal_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga el año
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idPrograma", ddlProgramaModal.SelectedValue)
            }

            ddlComponenteModal.DataSource = data.ObtieneDatos("ObtieneComponentes", params)
            ddlComponenteModal.DataTextField = "componente"
            ddlComponenteModal.DataValueField = "id"
            ddlComponenteModal.DataBind()
        End Using
        ddlComponenteModal_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub ddlComponenteModal_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@admon", ddlAdmon.SelectedValue),
                New SqlParameter("@anio", ddlAnio.SelectedValue),
                New SqlParameter("@programa", ddlProgramaModal.SelectedValue),
                New SqlParameter("@componente", ddlComponenteModal.SelectedValue)
            }

            ddlActividadModal.DataSource = data.ObtieneDatos("ObtieneActividadesPOA", params)
            ddlActividadModal.DataValueField = "id"
            ddlActividadModal.DataTextField = "nombreActividad"
            ddlActividadModal.DataBind()
        End Using
        ddlActividadModal_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub ddlActividadModal_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idActividad", ddlActividadModal.SelectedValue)}
            ddlSubActividadModal.DataSource = data.ObtieneDatos("ObtieneSubActividadesAdmin", params).Tables(0)
            ddlSubActividadModal.DataValueField = "id"
            ddlSubActividadModal.DataTextField = "nombreSubactividad"
            ddlSubActividadModal.DataBind()
        End Using
    End Sub

    Protected Sub btnGuardarPOA_Click(sender As Object, e As EventArgs)
        Try

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {
                    New SqlParameter("@claveAdmon", ddlAdmon.SelectedValue),
                    New SqlParameter("@anio", ddlAnio.SelectedValue),
                    New SqlParameter("@idSecretaria", ddlSecretariaModal.SelectedValue),
                    New SqlParameter("@idDireccion", ddlDireccionModal.SelectedValue),
                    New SqlParameter("@idSubActividad", ddlSubActividadModal.SelectedValue)
                }

                data.EjecutaCommand("GuardaPOA", params)
            End Using

            CargaPOA()
            ScriptManager.RegisterStartupScript(updAdminPOA, updAdminPOA.GetType(), "muestraModal", "cierraModalPOA();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub

    Private Sub CargaPOA()
        Try
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                    New SqlParameter("@anio", ddlAnio.SelectedValue)
                }

                grdPOA.DataSource = data.ObtieneDatos("ObtienePOA", params)
                grdPOA.DataBind()
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub

    Private Sub CargaProgramas()
        'Carga el año
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@anio", ddlAnio.SelectedValue)
            }

            ddlProgramaModal.DataSource = data.ObtieneDatos("ObtieneProgramas", params)
            ddlProgramaModal.DataTextField = "programa"
            ddlProgramaModal.DataValueField = "id"
            ddlProgramaModal.DataBind()
        End Using
        ddlProgramaModal_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub btnQuitar_Command(sender As Object, e As CommandEventArgs)
        Try
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@id", e.CommandArgument)
                }

                data.EjecutaCommand("QuitarPOA", params)
            End Using
            CargaPOA()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub
End Class