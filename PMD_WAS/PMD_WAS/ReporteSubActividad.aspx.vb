
Imports System.Data.SqlClient

Partial Class ReporteSubActividad
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered 
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Paso") = "0" Then
            Response.Redirect("~/Password.aspx")
        End If

        If Session("Puesto") <> "1" Then
            Response.Redirect("~/Bienvenido.aspx")
        End If

        If Not IsPostBack Then

            Using data As New DB(con.conectar())
                'Carga Administracion
                ddlAdmon.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmon.DataTextField = "Nombr_admon"
                ddlAdmon.DataValueField = "Cve_admon"
                ddlAdmon.DataBind()

                'Carga Clave Gastos
                ddlClaveGastos.DataSource = data.ObtieneDatos("CargaClavesGastos", Nothing)
                ddlClaveGastos.DataTextField = "Descripcion"
                ddlClaveGastos.DataValueField = "Clave"
                ddlClaveGastos.DataBind()
                ddlClaveGastos.Items.Insert(0, New ListItem("Todos", "-1"))

                'Carga secretarias
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", -1),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }
                ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
                ddlSecretaria.DataTextField = "Nombr_secretaria"
                ddlSecretaria.DataValueField = "IdSecretaria"
                ddlSecretaria.DataBind()
                ddlSecretaria.Items.Insert(0, New ListItem("Todos", "-1"))

                'Carga año
                Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3)
            }

                ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params2)
                ddlAnio.DataTextField = "Año"
                ddlAnio.DataValueField = "Año"
                ddlAnio.DataBind()

                'Carga dependencias
                Dim params3() As SqlParameter = New SqlParameter() _
           {
               New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
               New SqlParameter("@idSecretaria", -1),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
           }

                ddlDireccion.DataSource = data.ObtieneDatos("ObtieneDireciones", params3)
                ddlDireccion.DataTextField = "Nombr_direccion"
                ddlDireccion.DataValueField = "IdDireccion"
                ddlDireccion.DataBind()
                ddlDireccion.Items.Insert(0, New ListItem("Todos", "-1"))

                'Carga Líneas
                Dim params4() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@idSecretaria", -1),
                    New SqlParameter("@idDireccion", -1),
                    New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                    New SqlParameter("@idAnio", ddlAnio.SelectedValue)
                }

                ddlLinea.DataSource = data.ObtieneDatos("ObtieneLineas", params4)
                ddlLinea.DataTextField = "Nombr_linea"
                ddlLinea.DataValueField = "ID"
                ddlLinea.DataBind()
                ddlLinea.Items.Insert(0, New ListItem("Todo", "-1"))

                'Carga Sub Actividades
                Dim params5() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@idLinea", -1)
                }

                ddlSubActividad.DataSource = data.ObtieneDatos("ObtieneSubActividades", params5)
                ddlSubActividad.DataTextField = "SubActividad"
                ddlSubActividad.DataValueField = "Id"
                ddlSubActividad.DataBind()
                ddlSubActividad.Items.Insert(0, New ListItem("Todo", "-1"))
            End Using
        End If
    End Sub
    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Todos", "-1"))

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
    End Sub

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlDireccion.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDireccion.DataTextField = "Nombr_direccion"
            ddlDireccion.DataValueField = "IdDireccion"
            ddlDireccion.DataBind()
        End Using
        ddlDireccion.Items.Insert(0, New ListItem("Todos", "-1"))
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Líneas
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue)
            }

            ddlLinea.DataSource = data.ObtieneDatos("ObtieneLineas", params)
            ddlLinea.DataTextField = "Nombr_linea"
            ddlLinea.DataValueField = "ID"
            ddlLinea.DataBind()
        End Using
        ddlLinea.Items.Insert(0, New ListItem("Todo", "-1"))

    End Sub

    Protected Sub ddlLinea_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Sub Actividades
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idLinea", ddlLinea.SelectedValue)
            }

            ddlSubActividad.DataSource = data.ObtieneDatos("ObtieneSubActividades", params)
            ddlSubActividad.DataTextField = "SubActividad"
            ddlSubActividad.DataValueField = "Id"
            ddlSubActividad.DataBind()
        End Using
        ddlSubActividad.Items.Insert(0, New ListItem("Todo", "-1"))
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@IdDependencia", ddlDireccion.SelectedValue),
                New SqlParameter("@Año", ddlAnio.SelectedValue),
                New SqlParameter("@IdAdmon", ddlAdmon.SelectedValue)
            }

            grdReporte.DataSource = data.ObtieneDatos("ObtieneTablaConcentrado", params)
            grdReporte.DataBind()
        End Using
    End Sub
    Protected Sub btnExportar_Click(sender As Object, e As EventArgs)
        If grdReporte.Rows.Count > 0 Then
            Try
                'grdReporte.Columns(0).Visible = False
                Response.ClearContent()
                Response.Buffer = True
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "Concentrado.xls"))
                Response.ContentEncoding = Encoding.UTF8
                Response.ContentType = "application/ms-excel"
                Dim sw As New IO.StringWriter()
                Dim htw As New HtmlTextWriter(sw)
                grdReporte.RenderControl(htw)
                Response.Write(sw.ToString())
                Response.[End]()

            Catch ex As Exception
            End Try
        End If
        'Response.Clear()
        'Response.AddHeader("content-disposition", "attachment;filename=Concentrado.xls")
        'Response.ContentType = "application/vnd.xls"

        'Dim stringWrite As System.IO.StringWriter = New IO.StringWriter()

        'Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

        'grdReporte.RenderControl(htmlWrite)

        'Response.Write(stringWrite.ToString())

        'Response.End()
    End Sub
End Class
