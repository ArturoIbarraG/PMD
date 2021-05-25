
Imports System.Data.SqlClient

Partial Class AsignaPresupuestoDireccion
    Inherits System.Web.UI.Page

    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Asigna Presupuesto Dirección")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                Response.Redirect("~/Password.aspx")
            End If

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

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            'Regresa el Presupuesto Anual
            Dim paramsPres() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdAdmin", ddlAdmon.SelectedValue),
                New SqlParameter("@Anio", ddlAnio.SelectedValue),
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue)
            }

            Dim dtPres = data.ObtieneDatos("ObtieneMontoPresupuestoSecretaria", paramsPres).Tables(0)
            lblPresupuestoSecretaria.Text = String.Format("El Presupuesto de la Secretaría es: {0:c2}", dtPres.Rows(0)("Presupuesto"))
            hdnPresupuestoSecretaria.Value = dtPres.Rows(0)("Presupuesto")
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdAdmin", ddlAdmon.SelectedValue),
                New SqlParameter("@Anio", ddlAnio.SelectedValue),
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue)
            }

            Dim dt = data.ObtieneDatos("ObtienePresupuestoAsignadoDireccion", params).Tables(0)
            rptPresupuestoDireccion.DataSource = dt
            rptPresupuestoDireccion.DataBind()
        End Using
    End Sub
    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@clave_empl", -1)
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))
    End Sub
    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
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
            ddlAnio.Items.Insert(0, New ListItem("Selecciona el Año", "0"))
        End Using
    End Sub
    Protected Sub btnGuardarPresupuesto_Click(sender As Object, e As EventArgs)
        Try
            Dim presupuestoAsignado = Decimal.Parse(hdnPresupuestoSecretaria.Value)
            Dim listaPresupuesto As New List(Of Presupuesto)
            For Each row As RepeaterItem In rptPresupuestoDireccion.Items
                Dim monto = Integer.Parse(CType(row.FindControl("txtPresupuesto"), TextBox).Text.Replace(",", ""))
                Dim id = Integer.Parse(CType(row.FindControl("hdnPresupuesto"), HiddenField).Value)
                Dim p As New Presupuesto()
                p.Id = id
                p.Monto = monto
                listaPresupuesto.Add(p)
            Next

            If listaPresupuesto.Sum(Function(x) x.Monto) > presupuestoAsignado Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", "El presupuesto que se quiere Programar es mayor a el Presupuesto Asignado a la Secretaría"), True)
                Return
            End If

            Using data As New DB(con.conectar())
                For Each p In listaPresupuesto
                    Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@IdAdmin", ddlAdmon.SelectedValue),
                        New SqlParameter("@Anio", ddlAnio.SelectedValue),
                        New SqlParameter("@Monto", p.Monto),
                        New SqlParameter("@Id", p.Id),
                        New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue)
                    }

                    data.EjecutaCommand("GuardaPresupuestoDependencia", params)
                Next
            End Using

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Se ha asignado el presupuesto de las direcciones de la secretaría {2} del año {0} de la administración {1}.", ddlAnio.SelectedItem.Text, ddlAdmon.SelectedItem.Text, ddlSecretaria.SelectedItem.Text))
#End Region

            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", "Proceso terminado correctamente"), True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try
    End Sub

    Public Class Presupuesto
        Public Id As Integer
        Public Monto As Integer
    End Class
End Class
