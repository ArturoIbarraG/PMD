Imports System.Data.SqlClient
Imports System.Globalization

Partial Class EventoPresupuesto
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Presupuesto")
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



    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''Carga Líneas
        'Using data As New DB(con.conectar())
        '    Dim params() As SqlParameter = New SqlParameter() _
        '    {
        '        New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
        '        New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
        '        New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
        '        New SqlParameter("@idAnio", ddlAnio.SelectedValue)
        '    }

        '    ddlLinea.DataSource = data.ObtieneDatos("ObtieneLineas", params)
        '    ddlLinea.DataTextField = "Nombr_linea"
        '    ddlLinea.DataValueField = "ID"
        '    ddlLinea.DataBind()
        'End Using
        'ddlLinea.Items.Insert(0, New ListItem("Selecciona la Línea", "0"))
    End Sub
    Protected Sub ddlLinea_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''Carga Tareas de la Linea
        'Using data As New DB(con.conectar())
        '    Dim params() As SqlParameter = New SqlParameter() _
        '    {
        '        New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
        '        New SqlParameter("@idDependencia", ddlDireccion.SelectedValue),
        '        New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
        '        New SqlParameter("@IdActividad", ddlLinea.SelectedValue)
        '    }
        '    grdEvento.DataSource = data.ObtieneDatos("ObtieneEventosTareas", params)
        '    grdEvento.DataBind()
        'End Using

    End Sub
    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.conectar())

            'ddlDireccion.DataSource = data.ObtieneDependencias(ddlSecretaria.SelectedValue)
            'ddlDireccion.DataTextField = "nombr_depe"
            'ddlDireccion.DataValueField = "clave_depe"
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlDireccion.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDireccion.DataTextField = "Nombr_direccion"
            ddlDireccion.DataValueField = "IdDireccion"
            ddlDireccion.DataBind()
        End Using
        ddlDireccion.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
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
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

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
    Protected Sub lnkDetalle_Command(sender As Object, e As CommandEventArgs)
        Dim idEvento As Integer = Integer.Parse(e.CommandArgument)
        'Carga el DETALLE
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@Folio", idEvento)
            }

            grdDesglocePresupuesto.DataSource = data.ObtieneDatos("ObtieneDesglocePresupuestoEventoPorFolio", params)
            grdDesglocePresupuesto.DataBind()

            'MUESTRA INFORMACION DE EL EVENTO
            Dim params2() As SqlParameter = New SqlParameter() _
          {
              New SqlParameter("@folio", idEvento)
          }

            Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params2).Tables(0)
            lblNombreEvento.Text = dt.Rows(0)("Nombre")
            lblTipoEvento.Text = dt.Rows(0)("Tipo")
            lblFechaEvento.Text = DateTime.Parse(dt.Rows(0)("Fecha")).ToString("dd-MMMM-yyyy")

        End Using

        pnlEventoDetalle.Visible = True
        pnlEventoPresupuesto.Visible = False
    End Sub
    Protected Sub lnkRegresar_Click(sender As Object, e As EventArgs)
        pnlEventoDetalle.Visible = False
        pnlEventoPresupuesto.Visible = True
    End Sub
End Class
