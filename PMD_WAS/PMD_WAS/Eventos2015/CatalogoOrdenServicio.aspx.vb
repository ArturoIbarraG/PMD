
Imports System.Data
Imports System.Data.SqlClient

Partial Class Eventos2015_CatalogoOrdenServicio
    Inherits System.Web.UI.Page

    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Cátalogo de ordenes de servicio")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaClaveGastos()
            cargaOrdenesServicio()
        End If
    End Sub

    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)
        cargaOrdenesServicio()
    End Sub
    Protected Sub btnAgregarOrdenServicio_Click(sender As Object, e As EventArgs)
        limpiar()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "abreModalOrdenServicio();", True)
    End Sub
    Protected Sub grdOrdenServicio_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdOrdenServicio.PageIndex = e.NewPageIndex
        cargaOrdenesServicio()
    End Sub
    Protected Sub btnGuardaOrdenServicio_Click(sender As Object, e As EventArgs)
        Try
            Dim ID As Integer = Integer.Parse(lblIdOrdenServicio.Text)
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", ID),
                    New SqlParameter("@nombre", txtNombreOS.Text),
                    New SqlParameter("@clave", ddlClaveGastos.SelectedValue),
                    New SqlParameter("@costo", txtCostoUnitario.Text),
                    New SqlParameter("@iva", ddlIVA.SelectedValue)}

                'carga asignados
                data.EjecutaCommand("GuardaOrdenServicio", params)
                cargaOrdenesServicio()
                limpiar()
            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "ocultaModalOrdenServicio();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        Dim id As Integer = Integer.Parse(e.CommandArgument)
        If e.CommandName = "editar" Then
            lblIdOrdenServicio.Text = id

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                Dim dt As DataTable = data.ObtieneDatos("ObtieneOrdenServicio", params).Tables(0)
                txtNombreOS.Text = dt.Rows(0)("ordenServicio")
                txtCostoUnitario.Text = dt.Rows(0)("costo")
                lblTotal.Text = String.Format("{0:c2}", dt.Rows(0)("total"))

                If dt.Rows(0)("clave_gastos") <> "" Then
                    ddlClaveGastos.SelectedValue = dt.Rows(0)("clave_gastos")
                End If

                ddlIVA.SelectedValue = dt.Rows(0)("porcentajeIVA")

            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "abre_modal", "abreModalOrdenServicio();", True)
        ElseIf e.CommandName = "eliminar" Then
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", id)}

                'carga asignados
                data.EjecutaCommand("EliminaOrdenServicio", params)
                cargaOrdenesServicio()
            End Using
        End If
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

    Private Sub cargaOrdenesServicio()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@busqueda", txtBuscar.Text)}

            'carga asignados
            grdOrdenServicio.DataSource = data.ObtieneDatos("ObtieneCatalogoOrdenServicio", params).Tables(0)
            grdOrdenServicio.DataBind()
        End Using
        lblIdOrdenServicio.Text = "0"
    End Sub


    Private Sub limpiar()
        txtNombreOS.Text = ""
        txtCostoUnitario.Text = ""
        ddlClaveGastos.SelectedIndex = 0
        ddlIVA.SelectedIndex = 0
        lblTotal.Text = ""
    End Sub
End Class
