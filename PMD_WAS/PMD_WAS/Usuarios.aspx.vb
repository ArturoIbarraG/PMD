Imports System.Data.SqlClient
Imports System.Data

Partial Class Usuarios
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Listado de usuarios")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaSecretarias(ddlSecretaria, True)
            CargaDirecciones(ddlSecretaria, ddlDireccion, True)

            CargaSecretarias(ddlSecretariaModal, False)
            CargaDirecciones(ddlSecretariaModal, ddlDireccionModal, False)

            recarga()
        End If
    End Sub

    Private Sub CargaSecretarias(ddl As DropDownList, agregarTodos As Boolean)
        'Carga secretarias
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@clave_empl", -1)
            }

            ddl.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddl.DataTextField = "Nombr_secretaria"
            ddl.DataValueField = "IdSecretaria"
            ddl.DataBind()
        End Using
        If agregarTodos Then
            ddl.Items.Insert(0, New ListItem("Todas", "-1"))
        End If

    End Sub

    Private Sub CargaDirecciones(ddlSecr As DropDownList, ddl As DropDownList, agregarTodos As Boolean)
        'Carga Direcciones
        Dim idSec As Integer = -1
        Try
            idSec = ddlSecr.SelectedValue
        Catch ex As Exception

        End Try
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@idSecretaria", idSec),
                New SqlParameter("@clave_empl", -1)
            }

            ddl.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddl.DataTextField = "Nombr_direccion"
            ddl.DataValueField = "IdDireccion"
            ddl.DataBind()
        End Using
        If agregarTodos Then
            ddl.Items.Insert(0, New ListItem("Todas", "-1"))
        End If
    End Sub
    Private Sub recarga()
        'Carga el evento
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                   New SqlParameter("@usuario", txtBuscar.Text)
            }

            grdUsuarios.DataSource = data.ObtieneDatos("ObtieneUsuarios", params)
            grdUsuarios.DataBind()

        End Using
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        For Each row As GridViewRow In grdUsuarios.Rows
            Try
                Dim empleado As Integer = Integer.Parse(DirectCast(row.FindControl("hdnEmpleado"), HiddenField).Value)
                Dim correo As String = DirectCast(row.FindControl("txtCorreo"), TextBox).Text

                'Carga el evento
                Using data As New DB(con.conectar())
                    Dim params() As SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@clave_empl", empleado),
                   New SqlParameter("@correo", correo)
               }
                    data.EjecutaCommand("GuardaCorreoUsuario", params)
                End Using

            Catch ex As Exception

            End Try
        Next

        recarga()
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDireccion.SelectedIndexChanged
        recarga()
    End Sub

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSecretaria.SelectedIndexChanged
        CargaDirecciones(ddlSecretaria, ddlDireccion, True)
    End Sub

    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)
        recarga()
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        Dim id As Integer = Integer.Parse(e.CommandArgument)
        If e.CommandName = "editar" Then
            lblClaveEmpleado.Text = id
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text)}

                'carga asignados
                Dim dt As DataTable = data.ObtieneDatos("ObtieneEmpleado", params).Tables(0)
                txtClaveEmpleado.Text = dt.Rows(0)("Clave_empl")
                txtNombreEmpleado.Text = dt.Rows(0)("Nombr_empl")
                Try
                    ddlSecretariaModal.SelectedValue = dt.Rows(0)("IdSecretaria")
                Catch ex As Exception

                End Try
                CargaDirecciones(ddlSecretariaModal, ddlDireccionModal, False)
                Try
                    ddlDireccionModal.SelectedValue = dt.Rows(0)("IdDireccion")
                Catch ex As Exception

                End Try

                txtCorreoModal.Text = dt.Rows(0)("Correo")
                ddlPuestoModal.SelectedValue = dt.Rows(0)("IdPuesto")
                pnlContrasenia.Visible = False

                Dim param2() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text)}
                Dim dtDir As DataTable = data.ObtieneDatos("ObtieneDireccionesEmpleados", param2).Tables(0)
                grdDirecciones.DataSource = dtDir
                grdDirecciones.DataBind()
                Session("DireccionUsuario") = dtDir
            End Using

            cargaMenuUsuario()

            ScriptManager.RegisterStartupScript(updInfoEmpleado, updInfoEmpleado.GetType(), "muestra_modal", "muestraModalUsuario();", True)
        ElseIf e.CommandName = "eliminar" Then
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", id)}

                'carga asignados
                data.EjecutaCommand("EliminaUsuario", params)
                recarga()
            End Using
        End If

    End Sub
    Protected Sub ddlSecretariaModal_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaDirecciones(ddlSecretariaModal, ddlDireccionModal, True)
    End Sub
    Protected Sub btnAgregaUsuario_Click(sender As Object, e As EventArgs)
        limpiar()
        cargaMenuUsuario()
        pnlContrasenia.Visible = True
        ScriptManager.RegisterStartupScript(updInfoEmpleado, updInfoEmpleado.GetType(), "muestra_modal", "muestraModalUsuario();", True)
    End Sub

    Private Sub limpiar()
        Session("DireccionUsuario") = Nothing
        txtClaveEmpleado.Text = ""
        txtNombreEmpleado.Text = ""
        ddlSecretariaModal.SelectedIndex = 0
        ddlSecretaria.SelectedIndex = 0
        ddlDireccion.SelectedIndex = 0
        ddlDireccionModal.SelectedIndex = 0
        lblClaveEmpleado.Text = "0"
        pnlContrasenia.Visible = False
    End Sub
    Protected Sub btnGuardarUsuario_Click(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            If pnlContrasenia.Visible = True Then
                'CUANDO ES UN NUEVO USUARIO
                Dim dt = data.ObtieneDatos("ValidaUsuarioExistente", New SqlParameter() {New SqlParameter("@clave_empl", txtClaveEmpleado.Text)}).Tables(0)

                If dt.Rows(0)(0) = 1 Then
                    ScriptManager.RegisterStartupScript(updInfoEmpleado, updInfoEmpleado.GetType(), "mensaje_existente", "muestraMensajeUsuarioExistente();", True)
                    Return
                End If
            End If

            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", txtClaveEmpleado.Text),
                New SqlParameter("@nombre", txtNombreEmpleado.Text),
                New SqlParameter("@idSecretaria", ddlSecretariaModal.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccionModal.SelectedValue),
                New SqlParameter("@correo", txtCorreoModal.Text),
                New SqlParameter("@contra", txtContrasenia.Text),
                New SqlParameter("@puesto", ddlPuestoModal.SelectedValue)}

            data.EjecutaCommand("GuardaUsuario", params)

            If lblClaveEmpleado.Text = "0" Then
                lblClaveEmpleado.Text = txtClaveEmpleado.Text
            End If

            'elimina el menu actual
            Dim p() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text)}
            data.EjecutaCommand("EliminaMenuUsuario", p)

            ''GUARDA EL MENU
            'For Each row In rptMenuHeader.Items

            '    Dim id As Integer = DirectCast(row.FindControl("hdnId"), HiddenField).Value

            '    Dim rpt As Repeater = DirectCast(row.FindControl("rptMenuSecundario"), Repeater)
            '    For Each r In rpt.Items
            '        Dim id2 As Integer = DirectCast(r.FindControl("hdnId"), HiddenField).Value
            '        Dim activo As Boolean = DirectCast(r.FindControl("chkActivo"), CheckBox).Checked

            '        If activo Then
            '            Dim p2() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text), New SqlParameter("@idMenu", id2)}
            '            data.EjecutaCommand("GuardaMenuUsuario", p2)
            '        End If

            '    Next
            'Next

            'NUEVA FORMA DE MENU ACTUAL
            For Each row In rptMenuPrincipal.Items

                Dim id As Integer = DirectCast(row.FindControl("hdnId"), HiddenField).Value
                Dim activoP As Boolean = DirectCast(row.FindControl("chkActivo"), CheckBox).Checked
                If activoP Then
                    Dim par() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text), New SqlParameter("@idMenu", id)}
                    data.EjecutaCommand("GuardaMenuUsuario", par)
                End If

                Dim rpt As Repeater = DirectCast(row.FindControl("rptSubMenu"), Repeater)
                For Each r In rpt.Items
                    Dim id2 As Integer = DirectCast(r.FindControl("hdnId"), HiddenField).Value
                    Dim activo As Boolean = DirectCast(r.FindControl("chkActivo"), CheckBox).Checked

                    If activo Then
                        Dim p2() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text), New SqlParameter("@idMenu", id2)}
                        data.EjecutaCommand("GuardaMenuUsuario", p2)
                    End If

                    Dim rpt2 As Repeater = DirectCast(r.FindControl("rptSubSubMenu"), Repeater)
                    If rpt2 IsNot Nothing Then
                        For Each r2 In rpt2.Items
                            Dim id22 As Integer = DirectCast(r2.FindControl("hdnId"), HiddenField).Value
                            Dim activo2 As Boolean = DirectCast(r2.FindControl("chkActivo"), CheckBox).Checked

                            If activo2 Then
                                Dim p4() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text), New SqlParameter("@idMenu", id22)}
                                data.EjecutaCommand("GuardaMenuUsuario", p4)
                            End If

                        Next
                    End If
                Next
            Next

            'GUARDA
            GuardaDireccionesUsuario()
        End Using

        recarga()
        ScriptManager.RegisterStartupScript(updInfoEmpleado, updInfoEmpleado.GetType(), "oculta_modal", "ocultaModalUsuario();", True)
    End Sub

    Private Sub cargaMenuUsuario()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", lblClaveEmpleado.Text)}

            'carga asignados
            Dim dt As DataTable = data.ObtieneDatos("ObtieneMenuEmpleados", params).Tables(0)
            Session("MenuUsuarioactual") = dt
            Try
                rptMenuPrincipal.DataSource = dt.Select("idpadre = 0").CopyToDataTable()
                rptMenuPrincipal.DataBind()
            Catch ex As Exception

            End Try
        End Using
    End Sub
    Protected Sub rptMenuSecundario_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim dt As DataTable = Session("MenuUsuarioactual")
        Dim id As Integer = DirectCast(e.Item.FindControl("hdnId"), HiddenField).Value
        Dim rpt As Repeater = DirectCast(e.Item.FindControl("rptMenuSecundario"), Repeater)

        rpt.DataSource = dt.Select(String.Format("idpadre = {0}", id)).CopyToDataTable()
        rpt.DataBind()
    End Sub

    Protected Sub rptMenuPrincipal_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim dt As DataTable = Session("MenuUsuarioactual")
        Dim id As Integer = DirectCast(e.Item.FindControl("hdnId"), HiddenField).Value
        Dim rpt As Repeater = DirectCast(e.Item.FindControl("rptSubMenu"), Repeater)

        Try
            rpt.DataSource = dt.Select(String.Format("idpadre = {0}", id), "nombre asc").CopyToDataTable()
            rpt.DataBind()
        Catch ex As Exception
            Dim errr As String = ex.ToString()
        End Try
    End Sub

    Protected Sub rptSubMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim dt As DataTable = Session("MenuUsuarioactual")
        Dim id As Integer = DirectCast(e.Item.FindControl("hdnId"), HiddenField).Value
        Dim rpt As Repeater = DirectCast(e.Item.FindControl("rptSubSubMenu"), Repeater)

        Try
            rpt.DataSource = dt.Select(String.Format("idpadre = {0}", id), "nombre asc").CopyToDataTable()
            rpt.DataBind()
        Catch ex As Exception
            Dim errr As String = ex.ToString()
        End Try
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        If Session("DireccionUsuario") Is Nothing Then
            Dim dtN As New DataTable()
            dtN.Columns.Add("IdDireccion")
            dtN.Columns.Add("IdSecretaria")
            dtN.Columns.Add("Direccion")
            dtN.Columns.Add("Secretaria")
            Session("DireccionUsuario") = dtN
        End If

        Dim dt As DataTable = Session("DireccionUsuario")
        Dim dr = dt.NewRow()
        dr("IdDireccion") = ddlDireccionModal.SelectedValue
        dr("IdSecretaria") = ddlSecretariaModal.SelectedValue
        dr("Direccion") = ddlDireccionModal.SelectedItem.Text
        dr("Secretaria") = ddlSecretariaModal.SelectedItem.Text
        dt.Rows.Add(dr)

        grdDirecciones.DataSource = dt
        grdDirecciones.DataBind()

        Session("DireccionUsuario") = dt
    End Sub

    Protected Sub btnQuitar_Command(sender As Object, e As CommandEventArgs)
        Dim idDireccion = e.CommandArgument
        Dim dt As DataTable = Session("DireccionUsuario")
        Dim row = dt.Select(String.Format("IdDireccion = '{0}'", idDireccion)).FirstOrDefault()
        dt.Rows.Remove(row)

        grdDirecciones.DataSource = dt
        grdDirecciones.DataBind()

        Session("DireccionUsuario") = dt
    End Sub

    Private Sub GuardaDireccionesUsuario()
        If Not Session("DireccionUsuario") Is Nothing Then

            Dim dt As DataTable = Session("DireccionUsuario")
            Using data As New DB(con.conectar())
                Dim pD() As SqlParameter = New SqlParameter() {
                        New SqlParameter("@clave_empl", txtClaveEmpleado.Text)}
                data.EjecutaCommand("EliminaDireccionesUsuario", pD)


                For Each row In dt.Rows
                    Dim params() As SqlParameter = New SqlParameter() {
                        New SqlParameter("@clave_empl", txtClaveEmpleado.Text),
                        New SqlParameter("@idDireccion", row("IdDireccion")),
                        New SqlParameter("@idSecretaria", row("IdSecretaria"))}

                    data.EjecutaCommand("GuardaDireccionesEmpleado", params)

                Next
            End Using
        End If
    End Sub
End Class
