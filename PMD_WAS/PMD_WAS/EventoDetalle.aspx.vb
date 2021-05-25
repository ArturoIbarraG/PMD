Imports System.Data
Imports System.Data.SqlClient

Partial Class EventoDetalle
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Try

                CargaActividades()

                CargaSubActividades()

                RecargaEvento()

            Catch ex As Exception
                'Response.Redirect("~/Password.aspx")
            End Try
        End If
    End Sub

    Private Sub RecargaEvento()
        Dim folio As Integer = Request.QueryString("id")

        'Carga el evento
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@folio", folio)
            }

            Try
                Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params).Tables(0)
                lblAforo.Text = String.Format("{0:N0}", dt.Rows(0)("Aforo"))
                lblTipoEvento.Text = dt.Rows(0)("Tipo")
                lblEvento.Text = dt.Rows(0)("Nombre")
                lblDescripcion.Text = dt.Rows(0)("Descripcion")
                lblFecha.Text = DateTime.Parse(dt.Rows(0)("Fecha")).ToString("dd-MMMM-yyyy")

                If dt.Rows(0)("Id_Linea") = "" Then
                    ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "Asigna_Linea", "muestraModalAsignaLinea();", True)
                Else
                    lblActividad.Text = String.Format("{0}-{1}", dt.Rows(0)("Id_Linea"), dt.Rows(0)("Linea"))
                    lblSubActividad.Text = String.Format("{0}-{1}", dt.Rows(0)("Id_Subactividad"), dt.Rows(0)("Subactividad"))
                End If
            Catch ex As Exception

            End Try

            Dim params2() As SqlParameter = New SqlParameter() _
           {
               New SqlParameter("@Folio", folio)
           }

            Dim dt2 = data.ObtieneDatos("ObtieneDesglocePresupuestoEventoPorFolio", params2).Tables(0)
            grdDesglocePresupuesto.DataSource = dt2
            grdDesglocePresupuesto.DataBind()

            lblTotalPresupuesto.Text = String.Format("PRESUPUESTO: {0:c2}", Convert.ToDecimal(dt2.Compute("SUM(Total)", String.Empty)))
        End Using
    End Sub

    Private Sub CargaActividades()
        Try

            Dim claveEmpleado As Integer = Session("Clave_empl")

            'CARGA ACTIVIDADES
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@clave_empl", claveEmpleado)
            }

                ddlActividad.DataSource = data.ObtieneDatos("ObtieneLineasEmpleado", params)
                ddlActividad.DataTextField = "Nombr_linea"
                ddlActividad.DataValueField = "ID"
                ddlActividad.DataBind()

            End Using

        Catch ex As Exception
            'Response.Redirect("~/Password.aspx")
        End Try
    End Sub

    Private Sub CargaSubActividades()
        Try

            Dim claveEmpleado As Integer = Session("Clave_empl")

            'CARGA ACTIVIDADES
            Using data As New DB(con.conectar())

                'CARGA SUB ACTIVIDADES
                Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idLinea", ddlActividad.SelectedValue),
                  New SqlParameter("@clave_empl", claveEmpleado)
            }

                Dim dt = data.ObtieneDatos("ObtieneSubActividadesLineaEmpleado", params2)
                ddlSubActividad.DataSource = dt
                ddlSubActividad.DataTextField = "Nombre"
                ddlSubActividad.DataValueField = "Id"
                ddlSubActividad.DataBind()

            End Using

        Catch ex As Exception
            'Response.Redirect("~/Password.aspx")
        End Try
    End Sub

    Protected Sub btnAsignaLinea_Click(sender As Object, e As EventArgs)
        Try
            Dim folio As Integer = Request.QueryString("id")

            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Folio", folio),
                    New SqlParameter("@IdSubActividad", ddlSubActividad.SelectedValue)
                }

                data.EjecutaCommand("AsignaSubActividadEvento", params)

            End Using

            ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "SubActividad_Asignada", "ocultaModalAsignaLinea();", True)

            RecargaEvento()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaSubActividades()
    End Sub
    Protected Sub btnAgregarPresupuesto_Click(sender As Object, e As EventArgs)
        Dim folio As Integer = Request.QueryString("id")
        Dim aforo As Decimal = 0
        Decimal.TryParse(lblAforo.Text.Replace(",", ""), aforo)

        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@Folio", folio),
                New SqlParameter("@AforoEstimado", aforo),
                New SqlParameter("@Empleado", Session("Clave_empl"))
            }

            grdMaterialesEvento.DataSource = data.ObtieneDatos("CargaMaterialesPorTipoEvento", params)
            grdMaterialesEvento.DataBind()

        End Using

        ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "modal_evento", "muestraModalMateriales();", True)
    End Sub
    Protected Sub btnGuardaEvento_Click(sender As Object, e As EventArgs)
        Dim Folio As Integer = Request.QueryString("id")
        For Each row As GridViewRow In grdMaterialesEvento.Rows
            Dim id As Integer = DirectCast(row.FindControl("hdnIdMaterial"), HiddenField).Value
            Dim cantidad As Decimal = DirectCast(row.FindControl("txtCantidad"), TextBox).Text
            GuardaMaterialEvento(Folio, id, cantidad)
        Next

        RecargaEvento()
        ScriptManager.RegisterStartupScript(updEvento, updEvento.GetType(), "modal_evento", "ocultaModalMateriales();", True)
    End Sub

    Private Sub GuardaMaterialEvento(idEvento As Integer, idMaterial As Integer, cantidad As Decimal)
        Dim claveEmpleado As Integer = Session("Clave_empl")
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@Folio", idEvento),
                New SqlParameter("@IdMaterial", idMaterial),
                New SqlParameter("@Cantidad", cantidad),
                New SqlParameter("@Clave_empl", claveEmpleado)
            }

            data.EjecutaCommand("GuardaMaterialesEvento", params)

        End Using

    End Sub
End Class
