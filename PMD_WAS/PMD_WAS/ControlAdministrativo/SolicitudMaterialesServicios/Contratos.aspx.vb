Imports System.Data.SqlClient

Public Class Contratos
    Inherits System.Web.UI.Page

    Dim con As New conexion
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Solicitud de materiales y servicios")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaAlmacenes()
            CargaInformacionAdmin()
            CargaSecretarias()
            CreaTabla()
            'CargaGrid()
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
            dt.Columns.Add("IdRequisicion")
            dt.Columns.Add("Contrato")
            dt.Columns.Add("Requerimiento")
            dt.Columns.Add("Cantidad")
            dt.Columns.Add("ClaveGastos")

            Session("TablaRequisiciones") = dt
        End If

    End Sub

    Private Sub CargaGrid()
        Dim dt = DirectCast(Session("TablaRequisiciones"), DataTable)
        grdRequisiciones.DataSource = dt
        grdRequisiciones.DataBind()

        'CargaInfoPresupuesto()

    End Sub

    Protected Sub lnqRequisisciones_Selecting(sender As Object, e As LinqDataSourceSelectEventArgs)
        Dim dt = DirectCast(Session("TablaRequisiciones"), DataTable)
        e.Result = dt
    End Sub

    Private Sub CargaRequerimientos()
        Using data As New DB(con.Conectar())
            '
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@anio", hdnAnio.Value),
                New SqlParameter("@mes", hdnMes.Value)}

            Dim dt = data.ObtieneDatos("ObtieneContratosPresupuesto", params).Tables(0)
            Dim view = New DataView(dt)
            Dim dtC = view.ToTable(True, "codigo_contrato", "nombre_contrato")
            ddlContrato.Items.Clear()
            For Each row In dtC.Rows
                ddlContrato.Items.Add(New ListItem(row("nombre_contrato"), row("codigo_contrato")))
            Next
            ddlContrato.SelectedIndex = 0

            ddlRequerimiento.Items.Clear()
            Dim dtR = dt.Select(String.Format("codigo_contrato='{0}'", ddlContrato.SelectedValue)).CopyToDataTable()
            For Each row In dtR.Rows
                ddlRequerimiento.Items.Add(New ListItem(row("requerimiento"), row("id")))
            Next
            ddlRequerimiento.SelectedIndex = 0

            Session("CargaContratosRequerimientos") = dt
        End Using
    End Sub

    Private Sub CargaOrdenesdeAbastecimiento()
        Using data As New DB(con.Conectar())
            Dim params() As SqlParameter = New SqlParameter() {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@mes", hdnMes.Value),
                New SqlParameter("@anio", hdnAnio.Value),
                New SqlParameter("@idSubActividad", ddlSubActividad.SelectedValue),
                New SqlParameter("@contrato", ddlContrato.SelectedValue)}

            Dim dt = data.ObtieneDatos("ObtieneOrdenesSurtidoContrato", params).Tables(0)
            grdOrdenesSurtido.DataSource = dt
            grdOrdenesSurtido.DataBind()

            lblTotalUtilizado.Text = String.Format("Total: {0:c2}", dt.Compute("SUM(total)", ""))
        End Using

        CargaInfoPresupuesto()
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
        CargaRequerimientos()
        CargaOrdenesdeAbastecimiento()
    End Sub

    Private Sub CargaInfoPresupuesto()
        'Carga Direcciones
        Using data As New DB(con.Conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@mes", hdnMes.Value),
                New SqlParameter("@anio", hdnAnio.Value),
                New SqlParameter("@contrato", ddlContrato.SelectedValue)
            }

            Dim dr = data.ObtieneDatos("ObtienePresupuestoContrato", params).Tables(0).Rows(0)
            lblPresupuestoAutorizado.Text = String.Format("{0:c2}", dr("presupuestoAutorizado"))
            lblPresupuestoDisponible.Text = String.Format("{0:c2}", dr("presupuestoDisponible"))
            lblPresupuestoUtilizado.Text = String.Format("{0:c2}", dr("presupuestoUtilizado"))

        End Using
    End Sub

    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaSubActividades()
        RecargaRequisiciones()
        CargaOrdenesdeAbastecimiento()
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

    Protected Sub btnAgregarRequerimiento_Click(sender As Object, e As EventArgs)
        Try

            Dim idReq = Integer.Parse(ddlRequerimiento.SelectedValue)
            Dim contrato = ddlContrato.SelectedItem.Text
            Dim requerimiento = ddlRequerimiento.SelectedItem.Text
            Dim claveGastos As String = ""
            Dim dt = DirectCast(Session("TablaRequisiciones"), DataTable)
            If dt.Rows.Count > 0 Then
                Using data As New DB(con.Conectar())
                    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@id", idReq)}
                    claveGastos = data.ObtieneDatos("ObtieneLineas", params).Tables(0).Rows(0)("clave_gastos")
                End Using
                If claveGastos <> dt.Rows(0)("ClaveGastos") > 0 Then
                    ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "diferenteClaveGastos", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", "No se pueden generar Ordenes de "), True)
                    Return
                End If
            End If

            Dim dr = dt.NewRow
            dr("IdRequisicion") = idReq
            dr("Contrato") = contrato
            dr("Requerimiento") = requerimiento
            dr("Cantidad") = txtCantidad.Text
            dt.Rows.Add(dr)
            Session("TablaRequisiciones") = dt
            CargaGrid()

            'ddlContrato.SelectedIndex = 0
            'ddlRequerimiento.SelectedIndex = 0
            txtCantidad.Text = "0"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlContrato_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim dt = DirectCast(Session("CargaContratosRequerimientos"), DataTable)
        ddlRequerimiento.Items.Clear()
        Dim dtR = dt.Select(String.Format("codigo_contrato='{0}'", ddlContrato.SelectedValue)).CopyToDataTable()
        For Each row In dtR.Rows
            ddlRequerimiento.Items.Add(New ListItem(row("requerimiento"), row("id")))
        Next
        ddlRequerimiento.SelectedIndex = 0

        CargaOrdenesdeAbastecimiento()
    End Sub

    Protected Sub btnEnviaOrdenesSurtido_Click(sender As Object, e As EventArgs)
        Try
            Dim sb As New StringBuilder()

            Dim materiales = New List(Of IntelipolisEngine.PMD.OrdenAbastecimiento.Material)
            For Each row As GridViewRow In grdRequisiciones.Rows
                Dim hdn = DirectCast(row.FindControl("hdnIdReq"), HiddenField)
                Dim txt = DirectCast(row.FindControl("txtCantidad"), TextBox)

                Dim m = New IntelipolisEngine.PMD.OrdenAbastecimiento.Material
                m.cantidad = Decimal.Parse(txt.Text)
                m.idRequerimiento = Integer.Parse(hdn.Value)
                materiales.Add(m)

                ''If (hdn.SelectedValue = "" Or txt.Text = "" Or txt.Text = "0") Then
                ''    If grdRequisiciones.Rows.Count = 1 Then
                ''        ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "warning", "Favor de validar", "Favor de seleccionar al menos una Requisición."), True)
                ''        Return
                ''    End If
                ''    Continue For
                ''End If

                'Dim req = hdn.Value
                'Dim cantidad = txt.Text

                ''
                'Using data As New DB(con.Conectar())
                '    Dim params() As SqlParameter = New SqlParameter() {
                '        New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                '        New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                '        New SqlParameter("@mes", hdnMes.Value),
                '        New SqlParameter("@anio", hdnAnio.Value),
                '        New SqlParameter("@idReq", req),
                '        New SqlParameter("@cantidad", cantidad),
                '        New SqlParameter("@empleado", Session("Clave_empl")),
                '        New SqlParameter("@idSubActividad", ddlSubActividad.SelectedValue)}

                '    Dim mensaje = data.EjecutaCommand("EnviaSolicitudOrdenSurtido", params).ToString()
                '    If mensaje.Length > 0 Then
                '        sb.Append(mensaje + "<br />")
                '    End If
                'End Using
            Next

            Dim ordenAbastecimientoEngine = New IntelipolisEngine.PMD.OrdenAbastecimiento
            ordenAbastecimientoEngine.GuardaOrdenAbastecimiento(materiales, Integer.Parse(ddlAlmacen.SelectedValue), Integer.Parse(ddlSecretaria.SelectedValue), ddlDireccion.SelectedValue, hdnAnio.Value, hdnMes.Value, Session("Clave_empl"), ddlSubActividad.SelectedValue)

            Session("TablaRequisiciones") = Nothing
            CreaTabla()
            CargaGrid()
            CargaOrdenesdeAbastecimiento()
            ScriptManager.RegisterStartupScript(updEnviar, updEnviar.GetType(), "ocultaModal", "ocultaModalContratos();", True)
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las Ordenes de Abastecimiento fueron enviadas correctamente."), True)

            'If sb.ToString().Length > 0 Then
            '    ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','Los siguientes requerimientos no se puedieron enviar por no contar con suficiente presupuesto en el mes: <br />{2}');", "info", "Favor de validar", sb.ToString()), True)
            'Else
            '    ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "info", String.Format("mensajeCustom('{0}','{1}','{2}');", "success", "Proceso terminado", "Las Ordenes de Surtido fueron enviadas correctamente."), True)
            'End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updRequisiciones, updRequisiciones.GetType(), "error", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Favor de validar", ex.Message), True)
        End Try
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

End Class