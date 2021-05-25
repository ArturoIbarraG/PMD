
Imports System.Data
Imports System.Data.SqlClient

Partial Class Eventos2015_CatalogoRequisiciones
    Inherits System.Web.UI.Page
    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Catálogo de requisiciones")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaClaveGastos()
            cargaRequisiciones()
        End If
    End Sub

    Private Sub cargaRequisiciones()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@busqueda", txtBuscar.Text)}

            'carga asignados
            grdRequisiciones.DataSource = data.ObtieneDatos("ObtieneCatalogoRequisiciones", params).Tables(0)
            grdRequisiciones.DataBind()
        End Using
        lblIdRequisicion.Text = "0"
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

    Protected Sub btnGuardaRequisicion_Click(sender As Object, e As EventArgs)
        Try
            Dim ID As Integer = Integer.Parse(lblIdRequisicion.Text)
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", ID),
                    New SqlParameter("@nombre", txtNombreRequisicion.Text),
                    New SqlParameter("@clave", ddlClaveGastos.SelectedValue),
                    New SqlParameter("@costo", txtCostoUnitario.Text),
                    New SqlParameter("@iva", ddlIVA.SelectedValue)}

                'carga asignados
                data.EjecutaCommand("GuardaRequisicion", params)
                cargaRequisiciones()
                limpiar()
            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "ocultaModalRequisicion();", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        Dim id As Integer = Integer.Parse(e.CommandArgument)
        If e.CommandName = "editar" Then
            lblIdRequisicion.Text = id

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                Dim dt As DataTable = data.ObtieneDatos("ObtieneRequisicion", params).Tables(0)
                txtNombreRequisicion.Text = dt.Rows(0)("requisicion")
                txtCostoUnitario.Text = dt.Rows(0)("costo")
                lblTotal.Text = String.Format("{0:c2}", dt.Rows(0)("total"))
                ddlClaveGastos.SelectedValue = dt.Rows(0)("clave_gastos")
                ddlIVA.SelectedValue = dt.Rows(0)("porcentajeIVA")

            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "abreModalRequisicion();", True)
        ElseIf e.CommandName = "eliminar" Then
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                data.EjecutaCommand("EliminaRequisicion", params)
                cargaRequisiciones()
            End Using
        End If
    End Sub
    Protected Sub btnAgregarRequisicion_Click(sender As Object, e As EventArgs)
        limpiar()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "abreModalRequisicion();", True)
    End Sub

    Private Sub limpiar()
        txtNombreRequisicion.Text = ""
        txtCostoUnitario.Text = ""
        ddlClaveGastos.SelectedIndex = 0
        ddlIVA.SelectedIndex = 0
        lblTotal.Text = ""
    End Sub
    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)
        cargaRequisiciones()
    End Sub
    Protected Sub grdRequisiciones_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdRequisiciones.PageIndex = e.NewPageIndex
        cargaRequisiciones()
    End Sub

End Class
