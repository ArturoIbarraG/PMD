
Imports System.Data
Imports System.Data.SqlClient

Partial Class Eventos2015_CatalogoImprevistos
    Inherits System.Web.UI.Page

    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Cátalogo de imprevistos")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaClaveGastos()
            cargaImprevistos()
        End If
    End Sub

    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)
        cargaImprevistos()
    End Sub
    Protected Sub btnAgregarImprevisto_Click(sender As Object, e As EventArgs)
        limpiar()
        ScriptManager.RegisterStartupScript(updImprevistosGrid, updImprevistosGrid.GetType(), "abre_modal", "abreModalImprevisto();", True)
    End Sub
    Protected Sub grdImprevistos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdImprevistos.PageIndex = e.NewPageIndex
        cargaImprevistos()
    End Sub
    Protected Sub btnGuardaImprevisto_Click(sender As Object, e As EventArgs)
        Try
            Dim ID As Integer = Integer.Parse(lblIdImprevisto.Text)
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", ID),
                    New SqlParameter("@nombre", txtNombre.Text),
                    New SqlParameter("@clave", ddlClaveGastos.SelectedValue)}

                'carga asignados
                data.EjecutaCommand("GuardaImprevisto", params)
                cargaImprevistos()
                limpiar()
            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "ocultaModalImprevisto();", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CargaClaveGastos()
        Using data As New DB(con.conectar())

            'carga asignados
            ddlClaveGastos.DataTextField = "Descripcion"
            ddlClaveGastos.DataValueField = "Clave"
            ddlClaveGastos.DataSource = data.ObtieneDatos("CargaClaveGastos", Nothing).Tables(0)
            ddlClaveGastos.DataBind()
        End Using
    End Sub

    Private Sub cargaImprevistos()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@busqueda", txtBuscar.Text)}

            'carga asignados
            grdImprevistos.DataSource = data.ObtieneDatos("ObtieneListadoImprevistos", params).Tables(0)
            grdImprevistos.DataBind()
        End Using
        lblIdImprevisto.Text = "0"
    End Sub

    Private Sub limpiar()
        txtNombre.Text = ""
        ddlClaveGastos.SelectedIndex = 0
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        Dim id As Integer = Integer.Parse(e.CommandArgument)
        If e.CommandName = "editar" Then
            lblIdImprevisto.Text = id

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                Dim dt As DataTable = data.ObtieneDatos("ObtieneImprevisto", params).Tables(0)
                txtNombre.Text = dt.Rows(0)("nombre")

                If dt.Rows(0)("claveGastos") <> "" Then
                    ddlClaveGastos.SelectedValue = dt.Rows(0)("claveGastos")
                End If

            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "abreModalImprevisto();", True)
        ElseIf e.CommandName = "eliminar" Then
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                data.EjecutaCommand("EliminaImprevisto", params)
                cargaImprevistos()
            End Using
        End If
    End Sub
End Class
