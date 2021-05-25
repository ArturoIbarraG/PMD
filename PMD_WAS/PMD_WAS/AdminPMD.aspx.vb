Imports System.Data.SqlClient

Public Class AdminPMD
    Inherits System.Web.UI.Page

    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Administración PMD")
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

    'Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    CargaInformacionPMD()
    'End Sub

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

    '    CargaInformacionPMD()
    'End Sub

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

        CargaProgramas()
    End Sub

    Private Sub CargaProgramas()
        'Carga el año
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@anio", ddlAnio.SelectedValue)
            }

            grdPMD.DataSource = data.ObtieneDatos("ObtieneProgramas", params)
            grdPMD.DataBind()
        End Using
    End Sub



    Protected Sub btnEditar_Command(sender As Object, e As CommandEventArgs)
        Try
            If e.CommandName = "ver" Then
                Dim button = DirectCast(sender, Button)
                Dim grd As New GridView()
                grd = (button.NamingContainer).FindControl("grdComponentes")
                If button.Text = "+" Then
                    Using data As New DB(con.conectar())
                        Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idPrograma", e.CommandArgument)}
                        grd.DataSource = data.ObtieneDatos("ObtieneComponentes", params).Tables(0)
                        grd.DataBind()
                        grd.Visible = True
                    End Using
                    button.Text = "-"
                    hdnIDPrograma.Value = e.CommandArgument
                Else
                    button.Text = "+"
                    grd.DataSource = Nothing
                    grd.DataBind()
                    grd.Visible = False
                    hdnIDPrograma.Value = "0"
                End If
            ElseIf e.CommandName = "editarPrograma" Then
                Using data As New DB(con.conectar())
                    Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@anio", ddlAnio.SelectedValue),
                        New SqlParameter("@idPrograma", e.CommandName)
                    }
                    hdnIDPrograma.Value = e.CommandArgument
                    Dim programa = data.ObtieneDatos("ObtieneProgramas", params).Tables(0).Rows(0)
                    txtIdPrograma.Text = programa("idProg")
                    txtDescrPrograma.Text = programa("programa")
                    ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "abreModalPrograma();", True)
                End Using
            ElseIf e.CommandName = "nuevoComponente" Then
                Try
                    hdnIdComponente.Value = "0"
                    hdnIDPrograma.Value = e.CommandArgument
                    txtIdComponente.Text = ""
                    txtDescrComponente.Text = ""

                    ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "abreModalComponente();", True)
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
                End Try
            ElseIf e.CommandName = "editarComponente" Then
                Using data As New DB(con.conectar())
                    Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@idPrograma", hdnIDPrograma.Value),
                        New SqlParameter("@idComponente", e.CommandArgument)
                    }
                    hdnIdComponente.Value = e.CommandArgument
                    Dim componente = data.ObtieneDatos("ObtieneComponentes", params).Tables(0).Rows(0)
                    txtIdComponente.Text = componente("idComp")
                    txtDescrComponente.Text = componente("componente")
                    ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "abreModalComponente();", True)
                End Using
            End If



            'Dim id = e.CommandArgument
            'hdnID.Value = id
            'Using data As New DB(con.conectar())
            '    Dim params() As SqlParameter = New SqlParameter() _
            '    {
            '        New SqlParameter("@admon", ddlAdmon.SelectedValue),
            '        New SqlParameter("@anio", ddlAnio.SelectedValue),
            '        New SqlParameter("@secretaria", ddlSecretaria.SelectedValue),
            '        New SqlParameter("@direccion", ddlDireccion.SelectedValue),
            '        New SqlParameter("@id", id)
            '    }

            '    Dim dt = data.ObtieneDatos("ObtieneInformacionPMDPorId", params).Tables(0)
            '    ddlSecretariaModal.SelectedValue = dt.Rows(0)("cve_secr")

            '    ddlSecretariaModal_SelectedIndexChanged(Nothing, Nothing)

            '    ddlDireccionModal.SelectedValue = dt.Rows(0)("Cve_Dir")
            '    txtIdPrograma.Text = dt.Rows(0)("idProg")
            '    txtDescrPrograma.Text = dt.Rows(0)("programa")
            '    txtIdComponente.Text = dt.Rows(0)("idComp")
            '    txtDescrComponente.Text = dt.Rows(0)("objetivo")

            '    ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "abreModalPMD();", True)
            'End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
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

    'Protected Sub btnGuardarPMD_Click(sender As Object, e As EventArgs)
    '    Try
    '        If hdnID.Value = "" Then
    '            hdnID.Value = txtIdPrograma.Text + "." + txtIdComponente.Text
    '        End If

    '        Using data As New DB(con.conectar())
    '            Dim params() As SqlParameter = New SqlParameter() _
    '            {
    '                New SqlParameter("@admon", ddlAdmon.SelectedValue),
    '                New SqlParameter("@anio", ddlAnio.SelectedValue),
    '                New SqlParameter("@cve_secr", ddlSecretaria.SelectedValue),
    '                New SqlParameter("@secretaria", ddlSecretaria.SelectedItem.Text),
    '                New SqlParameter("@cve_dir", ddlDireccion.SelectedValue),
    '                New SqlParameter("@direccion", ddlDireccion.SelectedItem.Text),
    '                New SqlParameter("@id", hdnID.Value),
    '                New SqlParameter("@idProg", txtIdPrograma.Text),
    '                New SqlParameter("@programa", txtDescrPrograma.Text),
    '                New SqlParameter("@idComp", txtIdComponente.Text),
    '                New SqlParameter("@componente", txtDescrComponente.Text)
    '            }

    '            data.EjecutaCommand("GuardaInformacionPMD", params)

    '            CargaInformacionPMD()
    '            ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "ocultaModalPMD();", True)
    '        End Using
    '    Catch ex As Exception
    '        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
    '    End Try
    'End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaProgramas()
    End Sub

    Protected Sub btnGuardarComponetne_Click(sender As Object, e As EventArgs)
        Try
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@anio", ddlAnio.SelectedValue),
                    New SqlParameter("@id", hdnIdComponente.Value),
                    New SqlParameter("@idPrograma", hdnIDPrograma.Value),
                    New SqlParameter("@idComponente", txtIdComponente.Text),
                    New SqlParameter("@componente", txtDescrComponente.Text)
                }
                data.EjecutaCommand("GuardaComponente", params)

                CargaProgramas()
                ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "ocultaModalComponente();", True)
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnGuardarPrograma_Click(sender As Object, e As EventArgs)
        Try
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@anio", ddlAnio.SelectedValue),
                    New SqlParameter("@id", hdnIDPrograma.Value),
                    New SqlParameter("@idPrograma", txtIdPrograma.Text),
                    New SqlParameter("@programa", txtDescrPrograma.Text)
                }
                data.EjecutaCommand("GuardaPrograma", params)

                CargaProgramas()
                ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "ocultaModalPrograma();", True)
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub

    Protected Sub btnAgregarPrograma_Click(sender As Object, e As EventArgs)
        Try
            hdnIDPrograma.Value = "0"
            txtIdPrograma.Text = ""
            txtDescrPrograma.Text = ""

            ScriptManager.RegisterStartupScript(upAddPMD, upAddPMD.GetType(), "muestraModal", "abreModalPrograma();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub
End Class