Imports System.Data.SqlClient

Public Class AdminPOA
    Inherits System.Web.UI.Page
    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Administración POA")
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

    Protected Sub btnAgregaPOA_Click(sender As Object, e As EventArgs)
        'ddlSecretariaModal_SelectedIndexChanged(Nothing, Nothing)
        ddlDireccionModal_SelectedIndexChanged(Nothing, Nothing)
        'ddlProgramaModal.SelectedIndex = 0
        'ddlProgramaModal_SelectedIndexChanged(Nothing, Nothing)
        'ddlComponenteModal.SelectedIndex = 0
        txtIdActividad.Text = ""
        txtNombreActividad.Text = ""
        txtClasificacion.Text = ""
        txtIndicador.Text = ""
        txtUnidadMedida.Text = ""

        ddlProgramaModal.Enabled = True
        ddlComponenteModal.Enabled = True

        ScriptManager.RegisterStartupScript(updAdminPOA, updAdminPOA.GetType(), "muestraModal", "abreModalPOA();", True)
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)

        CargaInformacionPOA()
    End Sub

    Private Sub CargaInformacionPOA()
        'Carga el año
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@admon", ddlAdmon.SelectedValue),
                New SqlParameter("@anio", ddlAnio.SelectedValue),
                New SqlParameter("@programa", ddlPrograma.SelectedValue),
                New SqlParameter("@componente", ddlComponente.SelectedValue)
            }

            grdPOA.DataSource = data.ObtieneDatos("ObtieneActividadesPOA", params)
            grdPOA.DataBind()
        End Using
    End Sub

    Private Sub cargaProgramas()
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@anio", ddlAnio.SelectedValue)
            }

            ddlPrograma.DataSource = data.ObtieneDatos("ObtieneProgramas", params)
            ddlPrograma.DataTextField = "programa"
            ddlPrograma.DataValueField = "id"
            ddlPrograma.DataBind()
        End Using
        ddlPrograma.Items.Insert(0, New ListItem("Todos", "-1"))
    End Sub

    Private Sub cargaProgramasModal()
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
    End Sub

    Private Sub cargaComponentes()
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idPrograma", ddlPrograma.SelectedValue)
            }

            ddlComponente.DataSource = data.ObtieneDatos("ObtieneComponentes", params)
            ddlComponente.DataTextField = "componente"
            ddlComponente.DataValueField = "id"
            ddlComponente.DataBind()
        End Using
        ddlComponente.Items.Insert(0, New ListItem("Todos", "-1"))
    End Sub

    Private Sub cargaComponentesModal()
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
    End Sub

    'Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Using data As New DB(con.conectar())

    '        Dim params() As SqlParameter = New SqlParameter() _
    '        {
    '            New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
    '            New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
    '            New SqlParameter("@clave_empl", -1)
    '        }

    '        ddlDireccion.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
    '        ddlDireccion.DataTextField = "Nombr_direccion"
    '        ddlDireccion.DataValueField = "IdDireccion"
    '        ddlDireccion.DataBind()
    '    End Using
    '    ddlDireccion.Items.Insert(0, New ListItem("Todos", "-1"))

    '    CargaInformacionPOA()
    'End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        cargaProgramas()
        cargaProgramasModal()
        cargaComponentes()
        cargaComponentesModal()
        CargaInformacionPOA()
    End Sub

    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Using data As New DB(con.conectar())
        '    Dim params() As SqlParameter = New SqlParameter() _
        '    {
        '        New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
        '        New SqlParameter("@clave_empl", -1)
        '    }

        '    ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
        '    ddlSecretaria.DataTextField = "Nombr_secretaria"
        '    ddlSecretaria.DataValueField = "IdSecretaria"
        '    ddlSecretaria.DataBind()
        'End Using
        'ddlSecretaria.Items.Insert(0, New ListItem("Todos", "-1"))

        'ddlSecretaria_SelectedIndexChanged(Nothing, Nothing)

        'Using data As New DB(con.conectar())
        '    Dim params() As SqlParameter = New SqlParameter() _
        '    {
        '        New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
        '        New SqlParameter("@clave_empl", -1)
        '    }

        '    ddlSecretariaModal.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
        '    ddlSecretariaModal.DataTextField = "Nombr_secretaria"
        '    ddlSecretariaModal.DataValueField = "IdSecretaria"
        '    ddlSecretariaModal.DataBind()
        'End Using
        'ddlSecretariaModal.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

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

        cargaProgramas()
        cargaComponentes()

        cargaProgramasModal()
        cargaComponentesModal()
    End Sub

    'Protected Sub ddlSecretariaModal_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Using data As New DB(con.conectar())

    '        Dim params() As SqlParameter = New SqlParameter() _
    '        {
    '            New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
    '            New SqlParameter("@idSecretaria", ddlSecretariaModal.SelectedValue),
    '            New SqlParameter("@clave_empl", -1)
    '        }

    '        ddlDireccionModal.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
    '        ddlDireccionModal.DataTextField = "Nombr_direccion"
    '        ddlDireccionModal.DataValueField = "IdDireccion"
    '        ddlDireccionModal.DataBind()
    '    End Using
    '    ddlDireccionModal.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
    'End Sub

    Protected Sub btnGuardarPOA_Click(sender As Object, e As EventArgs)
        Try

            Dim idAct = 0
            Try
                Dim ids = txtIdActividad.Text.Split(".")
                idAct = ids(ids.Length - 1)
            Catch ex As Exception

            End Try


            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@id", hdnIdActividad.Value),
                    New SqlParameter("@anio", ddlAnio.SelectedValue),
                    New SqlParameter("@idPrograma", ddlProgramaModal.SelectedValue),
                    New SqlParameter("@idComponente", ddlComponente.SelectedValue),
                    New SqlParameter("@idActividad", txtIdActividad.Text),
                    New SqlParameter("@nombre", txtNombreActividad.Text),
                    New SqlParameter("@clasificacion", txtClasificacion.Text),
                    New SqlParameter("@unidadMedida", txtUnidadMedida.Text),
                    New SqlParameter("@indicador", txtIndicador.Text)
                }

                data.EjecutaCommand("GuardaActividadAdmin", params)

                CargaInformacionPOA()

                ScriptManager.RegisterStartupScript(updAdminPOA, updAdminPOA.GetType(), "muestraModal", "ocultaModalPOA();", True)
            End Using

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub

    Protected Sub ddlPrograma_SelectedIndexChanged(sender As Object, e As EventArgs)
        cargaComponentes()
        CargaInformacionPOA()
    End Sub

    Protected Sub ddlComponente_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaInformacionPOA()
    End Sub

    Protected Sub btnVer_Command(sender As Object, e As CommandEventArgs)
        If e.CommandName = "ver" Then
            Dim button = DirectCast(sender, Button)
            Dim grd As New GridView()
            grd = (button.NamingContainer).FindControl("grdSubActividades")
            If button.Text = "+" Then
                Using data As New DB(con.conectar())
                    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idActividad", e.CommandArgument)}
                    grd.DataSource = data.ObtieneDatos("ObtieneSubActividadesAdmin", params).Tables(0)
                    grd.DataBind()
                    grd.Visible = True
                End Using
                button.Text = "-"
                hdnIdActividad.Value = e.CommandArgument
            Else
                button.Text = "+"
                grd.DataSource = Nothing
                grd.DataBind()
                grd.Visible = False
                hdnIdActividad.Value = "0"
            End If
        ElseIf e.CommandName = "editar" Then
            Dim id = e.CommandArgument
            hdnIdActividad.Value = id

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                Dim dr = data.ObtieneDatos("ObtieneActividadesPorId", params).Tables(0).Rows(0)

                ddlProgramaModal.SelectedValue = dr("idPrograma")
                ddlProgramaModal_SelectedIndexChanged(Nothing, Nothing)

                ddlComponenteModal.SelectedValue = dr("idComponente")

                txtIdActividad.Text = dr("idActividad")
                txtNombreActividad.Text = dr("actividad")
                txtClasificacion.Text = dr("Clasificacion")
                txtUnidadMedida.Text = dr("Unidad_Medida")
                txtIndicador.Text = dr("Indicador")

                ddlProgramaModal.Enabled = False
                ddlComponenteModal.Enabled = False

            End Using
            ScriptManager.RegisterStartupScript(updAdminPOA, updAdminPOA.GetType(), "muestraModal", "abreModalPOA();", True)
        ElseIf e.CommandName = "add" Then

            Dim idActividad = e.CommandArgument.ToString().Split("|")(0)
            Dim idConcentrado = e.CommandArgument.ToString().Split("|")(1)
            lblIdActividad.Text = idActividad
            txtIdSubActividad.Text = ""
            txtNombreSubActividad.Text = ""
            txtIndicadorSA.Text = ""
            txtUnidadMedidaSA.Text = ""
            hdnIdActividad.Value = idConcentrado
            hdnIdSubActividad.Value = "0"
            ScriptManager.RegisterStartupScript(updAdminPOA, updAdminPOA.GetType(), "muestraModal", "abreModalSubActividad();", True)
        ElseIf e.CommandName = "editarSA" Then

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", e.CommandArgument)}
                Dim dr = data.ObtieneDatos("ObtieneSubActividadesAdminPorId", params).Tables(0).Rows(0)

                lblIdActividad.Text = dr("Id_Linea")
                txtIdSubActividad.Text = dr("Id_Subactividad")
                txtNombreSubActividad.Text = dr("Nombre")
                txtUnidadMedidaSA.Text = dr("Unidad_Medida")
                txtIndicadorSA.Text = dr("Indicador")

                hdnIdSubActividad.Value = e.CommandArgument
            End Using


            ScriptManager.RegisterStartupScript(updAdminPOA, updAdminPOA.GetType(), "muestraModal", "abreModalSubActividad();", True)
        End If
    End Sub

    Protected Sub btnGuardaSubActividad_Click(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@id", hdnIdSubActividad.Value),
                New SqlParameter("@idSubActividad", txtIdSubActividad.Text),
                New SqlParameter("@nombre", txtNombreSubActividad.Text),
                New SqlParameter("@indicador", txtIndicadorSA.Text),
                New SqlParameter("@unidadMedida", txtUnidadMedidaSA.Text),
                New SqlParameter("@idActividad", hdnIdActividad.Value),
                New SqlParameter("@anio", ddlAnio.SelectedValue)}

            data.EjecutaCommand("GuardaSubActividad", params)

        End Using

        ScriptManager.RegisterStartupScript(updAdminPOA, updAdminPOA.GetType(), "muestraModal", "ocultaModalSubActividad();", True)
    End Sub

    Protected Sub ddlDireccionModal_SelectedIndexChanged(sender As Object, e As EventArgs)
        cargaProgramasModal()
        cargaComponentesModal()
    End Sub

    Protected Sub ddlProgramaModal_SelectedIndexChanged(sender As Object, e As EventArgs)
        cargaComponentesModal()
    End Sub
End Class