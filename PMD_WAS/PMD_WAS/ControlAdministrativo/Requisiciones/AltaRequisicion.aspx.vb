Imports System.Data.SqlClient

Public Class AltaRequisicion
    Inherits System.Web.UI.Page
    Dim con As New conexion
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Solicitud de requisiciones")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaInformacionAdmin()
            CargaSecretarias()
            CreaTabla()
            CargaGrid()
            CargaAlmacenes()
        End If
    End Sub
    Private Sub CargaSecretarias()
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", hdnAdmon.Value),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))
    End Sub

    Private Sub CargaInformacionAdmin()
        Dim meses() = New String() {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}

        Using data As New DB(con.Conectar())
            Dim dt = data.ObtieneDatos("ObtieneInformacionAdministracion", Nothing).Tables(0)
            hdnAdmon.Value = dt.Rows(0)("idAdministracion")
            hdnAnio.Value = dt.Rows(0)("anio")
            hdnMes.Value = dt.Rows(0)("mes")
            Dim administracion = dt.Rows(0)("administracion")
            lblInformacionRequisicion.Text = String.Format("Solicitudes correspondientes al mes de <b>{0}</b> del <b>{1}</b> de la <b>{2}</b>", meses(dt.Rows(0)("mes") - 1), hdnAnio.Value, administracion)
        End Using
    End Sub

    Private Sub CreaTabla()
        If Session("TablaRequisiciones") Is Nothing Then
            Dim dt As New DataTable()
            dt.Columns.Add("Orden")
            dt.Columns.Add("IdRequisicion")
            dt.Columns.Add("Requisicion")
            dt.Columns.Add("Cantidad")
            dt.Columns.Add("ClaveGastos")
            Dim dr As DataRow
            dr = dt.NewRow
            dr("Orden") = "0"
            dr("Cantidad") = "0"

            dt.Rows.Add(dr)

            Session("TablaRequisiciones") = dt
        End If

    End Sub

    Private Sub CargaGrid()
        Dim dt = DirectCast(Session("TablaRequisiciones"), DataTable)
        grdRequisiciones.DataSource = dt
        grdRequisiciones.DataBind()

        CargaInfoPresupuesto()
    End Sub

    Protected Sub lnqRequisisciones_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs)
        Dim dt = DirectCast(Session("TablaRequisiciones"), DataTable)
        e.Result = dt.Select("", "Orden ASC").CopyToDataTable()
    End Sub

    Private Sub CargaRequerimientos()
        Using data As New DB(con.Conectar())

            '
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@empleado", Session("clave_empl")),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@anio", hdnAnio.Value),
                New SqlParameter("@mes", hdnMes.Value)}
            Session("CargaRequerimientosDirector") = data.ObtieneDatos("CargaRequerimientosDirectorRequisicion", params).Tables(0).Select("").CopyToDataTable()

        End Using
    End Sub

    Protected Sub grdRequisiciones_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt = DirectCast(Session("CargaRequerimientosDirector"), DataTable)
            Dim ddl = DirectCast(e.Row.FindControl("ddlRequisicion"), DropDownList)
            ddl.DataSource = Session("CargaRequerimientosDirector")
            ddl.DataTextField = "requerimiento"
            ddl.DataValueField = "id"
            ddl.DataBind()

            Dim hdn = DirectCast(e.Row.FindControl("hdnIdReq"), HiddenField)
            ddl.SelectedValue = hdn.Value
        End If
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim btn = DirectCast(sender, Button)
        Dim row = DirectCast(btn.NamingContainer, GridViewRow)
        Dim ddl = DirectCast(row.FindControl("ddlRequisicion"), DropDownList)
        Dim txt = DirectCast(row.FindControl("txtCantidad"), TextBox)

        Dim dt = DirectCast(Session("TablaRequisiciones"), DataTable)
        Dim dr = dt.NewRow
        dr("Orden") = (-1 * (dt.Rows.Count + 1))
        dr("IdRequisicion") = ddl.SelectedItem.Value
        dr("Requisicion") = ddl.SelectedItem.Text
        dr("Cantidad") = txt.Text
        dt.Rows.Add(dr)
        Session("TablaRequisiciones") = dt
        CargaGrid()
    End Sub

    Protected Sub btnGuardarRequisiciones_Click(sender As Object, e As EventArgs)
        Try
            Dim sb As New StringBuilder()

            Dim materiales As New List(Of IntelipolisEngine.PMD.Requisiciones.Material)
            For Each row As GridViewRow In grdRequisiciones.Rows
                Dim ddl = DirectCast(row.FindControl("ddlRequisicion"), DropDownList)
                Dim txt = DirectCast(row.FindControl("txtCantidad"), TextBox)

                If (ddl.SelectedValue = "" Or txt.Text = "" Or txt.Text = "0") Then
                    If grdRequisiciones.Rows.Count = 1 Then
                        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", "Favor de seleccionar al menos una Requisición."), True)
                        Return
                    End If
                    Continue For
                End If

                Dim tipo = ddl.SelectedValue.Split("|")(0)
                Dim req = ddl.SelectedValue.Split("|")(1)
                Dim cantidad = txt.Text

                Dim m As New IntelipolisEngine.PMD.Requisiciones.Material
                m.cantidad = cantidad
                m.idMaterial = req
                m.tipo = tipo
                materiales.Add(m)

                ''
                'Using data As New DB(con.Conectar())
                '    Dim params() As SqlParameter = New SqlParameter() {
                '        New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                '        New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                '        New SqlParameter("@mes", hdnMes.Value),
                '        New SqlParameter("@anio", hdnAnio.Value),
                '        New SqlParameter("@tipo", tipo),
                '        New SqlParameter("@idReq", req),
                '        New SqlParameter("@cantidad", cantidad),
                '        New SqlParameter("@empleado", Session("Clave_empl")),
                '        New SqlParameter("@idSubActividad", ddlSubActividad.SelectedValue)}

                '    Dim mensaje = data.EjecutaCommand("EnviaSolicitudRequisicion", params).ToString()
                '    If mensaje.Length > 0 Then
                '        sb.Append(mensaje + "<br />")
                '    End If
                'End Using
            Next

            Dim solRequisiciones As New IntelipolisEngine.PMD.Requisiciones
            Dim idCotizacion = 0
            If Request.QueryString("idCot") IsNot Nothing Then
                idCotizacion = Request.QueryString("idCot")
            End If
            solRequisiciones.GuardaSolicitudRequisicion(materiales, ddlAlmacen.SelectedValue, ddlSecretaria.SelectedValue, ddlDireccion.SelectedValue, hdnAnio.Value, hdnMes.Value, Session("Clave_empl"), ddlSubActividad.SelectedValue, idCotizacion)

            Response.Redirect("~/ControlAdministrativo/Requisiciones/AltaRequisicion.aspx")

            Session("TablaRequisiciones") = Nothing
            CreaTabla()
            CargaGrid()

            '
            If sb.ToString().Length > 0 Then
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','Las siguientes requisiciones no se puedieron enviar por no contar con suficiente presupuesto en el mes: <br />{2}');", "info", "Favor de validar", sb.ToString()), True)
            Else
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las requisiciones fueron enviadas correctamente."), True)
                ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "oculta", "ocultaModalAlmacen();", True)
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
    End Sub

    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        ''Carga Secretarias
        'Using data As New DataInfo()

        '    ddlSecretaria.DataSource = data.ObtieneSecretarias
        '    ddlSecretaria.DataTextField = "nombr_secr"
        '    ddlSecretaria.DataValueField = "clave_secr"
        '    ddlSecretaria.DataBind()
        'End Using
        'ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))
        'Using data As New DB(con.conectar())
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", hdnAdmon.Value),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

    End Sub
    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.Conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", hdnAdmon.Value),
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

    Protected Sub ddlMes_SelectedIndexChanged(sender As Object, e As EventArgs)
        Session("TablaRequisiciones") = Nothing
        CreaTabla()
        CargaGrid()
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)

        cargaActividades()

    End Sub

    Private Sub CargaInfoPresupuesto()
        'Carga Direcciones
        Using data As New DB(con.Conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@mes", hdnMes.Value),
                New SqlParameter("@anio", hdnAnio.Value)
            }

            Dim dr = data.ObtieneDatos("ObtienePresupuestoRequisicion", params).Tables(0).Rows(0)
            lblPresupuestoAutorizado.Text = String.Format("{0:c2}", dr("presupuestoAutorizado"))
            lblPresupuestoDisponible.Text = String.Format("{0:c2}", dr("presupuestoLibre"))
            lblPresupuestoReservado.Text = String.Format("{0:c2}", dr("presupuestoReservado"))
            lblPresupuestoComprometido.Text = String.Format("{0:c2}", dr("presupuestoComprometido"))

        End Using
    End Sub

    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaSubActividades()
        RecargaRequisiciones()
    End Sub

    Private Sub CargaSubActividades()
        Try
            Dim con As New Class1
            Dim claveEmpleado As Integer = Session("Clave_empl")

            Dim administracion = hdnAdmon.Value
            Dim anio = hdnAnio.Value

            'CARGA ACTIVIDADES
            Using data As New DB(con.conectar())

                'CARGA SUB ACTIVIDADES
                Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idLinea", ddlActividad.SelectedValue),
                  New SqlParameter("@clave_empl", claveEmpleado),
                  New SqlParameter("@idAdmon", administracion),
                  New SqlParameter("@idAnio", anio)
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

    Private Sub cargaActividades()
        'Carga Líneas
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", hdnAdmon.Value),
                New SqlParameter("@idAnio", hdnAnio.Value)
            }

            ddlActividad.DataSource = data.ObtieneDatos("ObtieneLineas", params)
            ddlActividad.DataTextField = "Nombr_linea"
            ddlActividad.DataValueField = "ID"
            ddlActividad.DataBind()
        End Using

        CargaRequerimientos()

        CargaSubActividades()

        RecargaRequisiciones()
    End Sub

    Protected Sub ddlSubActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        RecargaRequisiciones()
    End Sub

    Private Sub RecargaRequisiciones()
        pnlAltaSolicitud.Visible = True
        Session("TablaRequisiciones") = Nothing
        CreaTabla()
        CargaGrid()
    End Sub

    Protected Sub lnkEliminar_Command(sender As Object, e As CommandEventArgs)
        If e.CommandName = "quitar" Then
            Dim dt = DirectCast(Session("TablaRequisiciones"), DataTable)
            Dim row = dt.Select(String.Format("IdRequisicion = '{0}'", e.CommandArgument)).FirstOrDefault()
            dt.Rows.Remove(row)
            Session("TablaRequisiciones") = dt
            CargaGrid()
        End If
    End Sub

    Private Sub CargaAlmacenes()
        Using data As New DB(con.Conectar())
            ddlAlmacen.DataSource = data.ObtieneDatos("ObtieneAlmacenes", Nothing)
            ddlAlmacen.DataTextField = "nombre"
            ddlAlmacen.DataValueField = "id"
            ddlAlmacen.DataBind()
        End Using
    End Sub
End Class