
Imports System.Data
Imports System.Data.SqlClient

Partial Class CatalogoRequerimiento
    Inherits System.Web.UI.Page

    Dim con As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaClaveGastos()
            cargaRequerimientos()
        End If
    End Sub
    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)
        cargaRequerimientos()
    End Sub

    Protected Sub btnAgregarRequerimientos_Click(sender As Object, e As EventArgs)
        limpiar()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "abreModalRequerimiento();", True)
    End Sub
    Protected Sub grdRequerimientos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdRequerimientos.PageIndex = e.NewPageIndex
        cargaRequerimientos()
    End Sub
    Protected Sub btnGuardaRequerimiento_Click(sender As Object, e As EventArgs)
        Try
            Dim ID As Integer = Integer.Parse(lblIdRequerimiento.Text)
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", ID),
                    New SqlParameter("@nombre", txtNombreRequerimiento.Text),
                    New SqlParameter("@clave", ddlClaveGastos.SelectedValue),
                    New SqlParameter("@costo", txtCostoUnitario.Text),
                    New SqlParameter("@iva", ddlIVA.SelectedValue)}

                'carga asignados
                data.EjecutaCommand("GuardaRequerimiento", params)
                cargaRequerimientos()
                limpiar()
            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "ocultaModalRequerimiento();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        Dim id As Integer = Integer.Parse(e.CommandArgument)
        If e.CommandName = "editar" Then
            lblIdRequerimiento.Text = id

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                Dim dt As DataTable = data.ObtieneDatos("ObtieneRequerimiento", params).Tables(0)
                txtNombreRequerimiento.Text = dt.Rows(0)("requerimiento")
                txtCostoUnitario.Text = dt.Rows(0)("costo")
                lblTotal.Text = String.Format("{0:c2}", dt.Rows(0)("total"))
                ddlClaveGastos.SelectedValue = dt.Rows(0)("clave_gastos")
                ddlIVA.SelectedValue = dt.Rows(0)("porcentajeIVA")

            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "abreModalRequerimiento();", True)
        ElseIf e.CommandName = "eliminar" Then
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                data.EjecutaCommand("EliminaRequerimiento", params)
                cargaRequerimientos()
            End Using
        End If
    End Sub

    Private Sub limpiar()
        txtNombreRequerimiento.Text = ""
        txtCostoUnitario.Text = ""
        ddlClaveGastos.SelectedIndex = 0
        ddlIVA.SelectedIndex = 0
        lblTotal.Text = ""
    End Sub

    Private Sub cargaRequerimientos()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@busqueda", txtBuscar.Text)}

            'carga asignados
            grdRequerimientos.DataSource = data.ObtieneDatos("ObtieneCatalogoRequerimientos", params).Tables(0)
            grdRequerimientos.DataBind()
        End Using
        lblIdRequerimiento.Text = "0"
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
End Class
