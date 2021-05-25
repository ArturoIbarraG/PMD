Imports System.Data.SqlClient

Public Class CatalogoContratos
    Inherits System.Web.UI.Page

    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Cátalogo de Contratos")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaContratos()
            CargaClaveGastos()
        End If
    End Sub

    Private Sub CargaContratos()
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@busqueda", txtBuscar.Text)}

            'carga asignados
            Dim dt = data.ObtieneDatos("ObtieneCatalogoContratos", params).Tables(0)
            Session("CatalogoContrato") = dt
            grdRequerimientos.DataSource = dt
            grdRequerimientos.DataBind()
        End Using
    End Sub

    Protected Sub txtBuscar_TextChanged(sender As Object, e As EventArgs)
        CargaContratos()
    End Sub

    Protected Sub btn_Command(sender As Object, e As CommandEventArgs)
        If e.CommandName = "ver" Then
            Dim dt = DirectCast(Session("CatalogoContrato"), DataTable)
            Dim row = dt.Select(String.Format("codigo_contrato = '{0}'", e.CommandArgument))(0)
            txtNombre.Text = row("nombre_contrato")
            txtProveedor.Text = row("proveedor")
            txtCodigoContrato.Text = row("codigo_contrato")
            ddlClaveGastos.SelectedValue = row("clave_gastos")
            txtCodigoContrato.Enabled = False

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@codigo", row("codigo_contrato"))}

                'carga materiales
                Session("ProductosDelContratos") = data.ObtieneDatos("ObtieneMaterialesContratos", params).Tables(0)
                grdMaterialesContrato.DataSource = Session("ProductosDelContratos")
                grdMaterialesContrato.DataBind()
            End Using

            ScriptManager.RegisterStartupScript(updRequerimientosGrid, updRequerimientosGrid.GetType(), "materiales", "muestraModalInfoContrato();", True)
        ElseIf e.CommandName = "quitar" Then
            Dim id = e.CommandArgument
            Dim dtProductos = DirectCast(Session("ProductosDelContratos"), DataTable)
            Dim row = dtProductos.Select(String.Format("id='{0}'", id))(0)
            dtProductos.Rows.Remove(row)

            Session("ProductosDelContratos") = dtProductos
            If dtProductos.Rows.Count > 0 Then
                grdMaterialesContrato.DataSource = dtProductos
            Else
                grdMaterialesContrato.DataSource = Nothing
            End If

            grdMaterialesContrato.DataBind()
        End If
    End Sub

    Protected Sub grdRequerimientos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdRequerimientos.PageIndex = e.NewPageIndex
        CargaContratos()
    End Sub

    Protected Sub btnGuardarContrato_Click(sender As Object, e As EventArgs)
        Try
            Dim dtProductos As DataTable = DirectCast(Session("ProductosDelContratos"), DataTable)
            Dim contratosEngine = New IntelipolisEngine.PMD.Contratos()
            contratosEngine.GuardaContrato(txtNombre.Text, txtProveedor.Text, txtCodigoContrato.Text, ddlClaveGastos.SelectedValue, dtProductos)
            CargaContratos()
            ScriptManager.RegisterStartupScript(updRequerimientosGrid, updRequerimientosGrid.GetType(), "materiales", "ocultaModalInfoContrato();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequerimientosGrid, updRequerimientosGrid.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", ex.Message), True)
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

    Private Sub Limpiar()
        ddlClaveGastos.SelectedIndex = 0
        txtNombre.Text = ""
        txtProveedor.Text = ""
        txtCodigoContrato.Text = ""

        txtNombreProducto.Text = ""
        txtCostoUnitario.Text = ""
        txtIEPS.Text = ""
        txtIVA.Text = ""
        txtTotal.Text = ""
        txtUnidad.Text = ""
        txtCodigoContrato.Enabled = True
        grdMaterialesContrato.DataSource = Nothing
        grdMaterialesContrato.DataBind()
    End Sub

    Protected Sub btnAgregarContrato_Click(sender As Object, e As EventArgs)
        Try
            Limpiar()
            ScriptManager.RegisterStartupScript(updRequerimientosGrid, updRequerimientosGrid.GetType(), "materiales", "muestraModalInfoContrato();", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequerimientosGrid, updRequerimientosGrid.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", ex.Message), True)
        End Try
    End Sub


    Protected Sub btnAgregarProductp_Click(sender As Object, e As EventArgs)
        Try
            Dim dtProductos As DataTable = DirectCast(Session("ProductosDelContratos"), DataTable)
            If dtProductos Is Nothing Then
                dtProductos = New DataTable()
                dtProductos.Columns.Add("id")
                dtProductos.Columns.Add("requerimiento")
                dtProductos.Columns.Add("unidad")
                dtProductos.Columns.Add("costoUnitario")
                dtProductos.Columns.Add("iva")
                dtProductos.Columns.Add("total")
                dtProductos.Columns.Add("ieps")
            End If

            Dim row = dtProductos.NewRow()
            Dim id = 1
            If dtProductos.Rows.Count > 0 Then
                id = dtProductos.Compute("max(id)", "") + 1
            End If

            row("requerimiento") = txtNombreProducto.Text
            row("unidad") = txtUnidad.Text
            row("costoUnitario") = txtCostoUnitario.Text
            row("iva") = txtIVA.Text
            row("total") = txtTotal.Text
            row("ieps") = txtIEPS.Text
            row("id") = id
            dtProductos.Rows.Add(row)

            Session("ProductosDelContratos") = dtProductos
            grdMaterialesContrato.DataSource = dtProductos
            grdMaterialesContrato.DataBind()

            txtNombreProducto.Text = ""
            txtCostoUnitario.Text = ""
            txtIEPS.Text = ""
            txtIVA.Text = ""
            txtTotal.Text = ""
            txtUnidad.Text = ""
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequerimientosGrid, updRequerimientosGrid.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", ex.Message), True)
        End Try
    End Sub
End Class