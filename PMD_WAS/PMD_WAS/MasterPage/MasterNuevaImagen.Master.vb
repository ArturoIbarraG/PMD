Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading

Public Class MasterNuevaImagen
    Inherits System.Web.UI.MasterPage
    Dim con As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Dim nombreUsuario As String = Session("NombreEmpl")
                Me.lblNombreUsuario.Text = nombreUsuario.Split(" ")(0)
                Me.lblLetraHeader.Text = Me.lblNombreUsuario.Text.Substring(0, 1)
                Me.lblLetraHeaderMini.Text = Me.lblNombreUsuario.Text.Substring(0, 1)

                Dim culture As CultureInfo
                If Thread.CurrentThread.CurrentCulture.Name <> "es-MX" Then
                    culture = CultureInfo.CreateSpecificCulture("es-MX")
                    Thread.CurrentThread.CurrentUICulture = culture
                End If

                If Session("Clave_empl") Is Nothing Or Session("Clave_empl") = "0" Then
                    Response.Redirect("https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Password.aspx")
                End If

                'Carga informacion
                Using data As New DB(con.conectar())

                    'Carga Administraciones
                    ddlAdmonModal.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                    ddlAdmonModal.DataTextField = "Nombr_admon"
                    ddlAdmonModal.DataValueField = "Cve_admon"
                    ddlAdmonModal.DataBind()

                    'Carg Anios
                    Dim paramsAnios() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdmonModal.SelectedValue)}
                    ddlAnioModal.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", paramsAnios)
                    ddlAnioModal.DataTextField = "Año"
                    ddlAnioModal.DataValueField = "Año"
                    ddlAnioModal.DataBind()

                    'Carga Secretaria
                    Dim paramsSec() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdmonModal.SelectedValue), New SqlParameter("@clave_empl", Session("Clave_empl"))}
                    Dim dtSec = data.ObtieneDatos("ObtieneSecretarias", paramsSec).Tables(0)
                    ddlSecretariaModal.DataSource = dtSec
                    ddlSecretariaModal.DataTextField = "Nombr_secretaria"
                    ddlSecretariaModal.DataValueField = "IdSecretaria"
                    ddlSecretariaModal.DataBind()
                    If dtSec.Rows.Count > 1 Then
                        ddlSecretariaModal.Items.Insert(0, New ListItem("Todos", "-1"))
                    End If

                    'Carga direccion
                    Dim paramsDir() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdmonModal.SelectedValue), New SqlParameter("@idSecretaria", ddlSecretariaModal.SelectedValue), New SqlParameter("@clave_empl", Session("Clave_empl"))}
                    Dim dtDir = data.ObtieneDatos("ObtieneDireciones", paramsDir).Tables(0)
                    ddlDireccionModal.DataSource = dtDir
                    ddlDireccionModal.DataTextField = "Nombr_direccion"
                    ddlDireccionModal.DataValueField = "IdDireccion"
                    ddlDireccionModal.DataBind()
                    If dtDir.Rows.Count > 1 Then
                        ddlDireccionModal.Items.Insert(0, New ListItem("Todos", "-1"))
                    End If

                    'Carga Direccion Usuario
                    Dim paramsDireccion() As SqlParameter = New SqlParameter() {New SqlParameter("@admon", ddlAdmonModal.SelectedValue), New SqlParameter("@clave_direccion", Session("clave_depe"))}
                    Dim dt = data.ObtieneDatos("ObtieneDireccion", paramsDireccion).Tables(0)
                    lblDireccion.Text = dt.Rows(0)("Nombr_direccion")

                    hdnDireccionMasterModal.Value = Session("clave_depe")
                    Try
                        comboAnioProgressPresupuesto.SelectedValue = Request.Cookies("pmd_anio").Value
                        ddlAnioModal.SelectedValue = Request.Cookies("pmd_anio").Value
                    Catch ex As Exception
                    End Try

                End Using

                CargaDetallePresupuesto()

                cargaMenu()
            End If
        Catch ex As Exception
            Response.Redirect("~/Password.aspx")
        End Try
    End Sub

    Private Sub CargaDetallePresupuesto()
        Dim con As New Class1
        Dim dt As DataTable
        'Carga eventos
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdAdmon", ddlAdmonModal.SelectedValue),
                    New SqlParameter("@IdDependencia", ddlDireccionModal.SelectedValue),
                      New SqlParameter("@IdSecretaria", ddlSecretariaModal.SelectedValue),
                    New SqlParameter("@Anio", ddlAnioModal.SelectedValue)
                }

            dt = data.ObtieneDatos("ObtieneInformacionPresupuesto", params).Tables(0)
            grdModalPresupuesto.DataSource = dt
            grdModalPresupuesto.DataBind()
        End Using

    End Sub

    Private Sub cargaMenu()
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", Session("Clave_empl"))}

            'carga asignados
            Dim dt As DataTable = data.ObtieneDatos("ObtieneMenuEmpleados", params).Tables(0)
            Session("MenuUsuarioPrincipal") = dt

            Try
                Dim dtPadres = dt.Select("idpadre = 0").CopyToDataTable()
                'For Each row In dtPadres.Rows
                '    If dt.Select(String.Format("idpadre = {0} and activo = True", row("id"))).Count() <= 0 Then
                '        row("activo") = False
                '    Else
                '        row("activo") = True
                '    End If
                'Next

                rptMenuPrincipal.DataSource = dtPadres.Select("activo = True").CopyToDataTable()
                rptMenuPrincipal.DataBind()
            Catch ex As Exception

            End Try


        End Using
    End Sub

    Protected Sub rptMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim dt As DataTable = Session("MenuUsuarioPrincipal")
        Dim id As Integer = DirectCast(e.Item.FindControl("hdnIdMenu"), HiddenField).Value
        Dim rpt As Repeater = DirectCast(e.Item.FindControl("rptSubMenu"), Repeater)

        Try
            rpt.DataSource = dt.Select(String.Format("idpadre = {0} and activo = True", id)).CopyToDataTable()
            rpt.DataBind()
        Catch ex As Exception
            Dim errr As String = ex.ToString()
        End Try

    End Sub

    Public Sub AsignaNombrePagina(nombre As String)
        lblNombrePagina.Text = nombre
    End Sub

    Protected Sub rptSubMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim dt As DataTable = Session("MenuUsuarioPrincipal")
        Dim id As Integer = DirectCast(e.Item.FindControl("hdnIdSubMenu"), HiddenField).Value
        Dim rpt As Repeater = DirectCast(e.Item.FindControl("rptSubSubMenu"), Repeater)

        Try
            rpt.DataSource = dt.Select(String.Format("idpadre = {0} and activo = True", id)).CopyToDataTable()
            rpt.DataBind()
        Catch ex As Exception
            Dim errr As String = ex.ToString()
        End Try
    End Sub
End Class