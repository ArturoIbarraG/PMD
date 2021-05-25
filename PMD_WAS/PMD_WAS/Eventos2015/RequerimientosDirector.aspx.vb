Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion
Partial Class RequerimientosDirector
    Inherits System.Web.UI.Page

    Dim stry As String
    Dim folio As Integer
    Dim tabla As DataTable
    Dim clave_secr As Integer
    Dim con As New conexion
    Dim clave_depe As Integer

    Dim clave_colo As Integer
    Dim clave_evento As Integer
    Dim clave_calle As Integer
    Dim resultado As Integer
    Dim modiFolio As String
    Dim conx As New conexion
    Dim IDCOL As Integer
    Dim IDSECRE As Integer
    Dim privilegio As Integer
    'Dim corx As String
    Dim corx As Double
    Dim cory As Double

    Dim coordx As String
    Dim coordy As String
    Dim folio_req As Integer
    Dim cantidad_decimal As Double
    Dim cantidad As String
    Dim folio_evento As String
    Dim ds As New DataSet
    Dim requerimiento As String
    Dim nombre_evento As String
    Dim tabfila As New DataTable

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Requerimientos")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("paso") = "1" Then

        Else
            Response.Redirect("~/Password.aspx")
        End If


        If Not IsPostBack = True Then
            usuario()
            cargaReq()
            cargarCeremonia()
            CargaCatalogoRequerimientos()

            'Valida si tiene un folio lo carga en automatico
            Try
                Dim folio = Request.QueryString("folio")
                If Not folio Is Nothing Then
                    txtFolio.Text = folio

                    txtFolio_TextChanged(Nothing, Nothing)
                End If

            Catch ex As Exception

            End Try

            Session("RequerimientosServInternoEvento") = Nothing
            Session("RequerimientosImprevistosEvento") = Nothing
            Session("RequerimientosEvento") = Nothing
            CargaCatalogoRequerimientosInternos()

            Dim soloLectura = Request.QueryString("readOnly")
            If Not soloLectura Is Nothing Then
                containerRequerimientos.Visible = False
                btnGuardar.Visible = False
                btnCancelar.Visible = False
                btnValidarEvento.Visible = False

                'Dim masterPage = DirectCast(Me.Master, MasterGlobal)
                'masterPage.OcultaHeader()
            End If
        End If

    End Sub

    Sub usuario()
        If Session("privilegio") = 1 Then  'todas las secretarias
            'Admin
            Me.Label4.Text = "Admin:"
            Me.Label19.Text = Session("clave_empl")
        Else
            'Usuario
            Me.Label4.Text = "Usuario:"
            Me.Label19.Text = Session("clave_empl")
        End If
    End Sub




    Sub cargarCeremonia()

        stry = " select '' ID, '' nombre UNION  select id ,nombre from [eventos].cat_ceremonia order by id "
        tabla = conexion.sqlcon(stry)


        If tabla.Rows.Count < 1 Then

            Me.drpCeremonia.Items.Clear()
            Me.drpCeremonia.Items.Add("")
        Else


            drpCeremonia.DataSource = tabla
            drpCeremonia.DataTextField = "nombre"
            drpCeremonia.DataValueField = "id"
            drpCeremonia.DataBind()


        End If

    End Sub


    Sub cargaReq()
        Dim con As New conexion
        Dim stry As String

        'stry = "select '' ID_REQ, '' REQUERIMIENTO UNION select  id_req,requerimiento from [eventos].cat_requerimiento order by id_req"
        stry = "select '' ID_REQ, '' REQUERIMIENTO UNION select  id_req,requerimiento from [eventos].cat_requerimiento order by  REQUERIMIENTO"

        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader()
        Try


            While tDrsx.Read


                'drpReq.DataSource = tDrsx
                'drpReq.DataTextField = "requerimiento"
                'drpReq.DataValueField = "id_req"
                'drpReq.DataBind()

                ''drpReq.Items.Insert(0, New ListItem("<Seleccione un Item>", "0"))

            End While
        Finally
            tDrsx.Close()
        End Try

        'REQUISICIONES
        'stry = "select '' ID_REQ, '' REQUERIMIENTO UNION select  id_req,requerimiento from [eventos].cat_requerimiento order by id_req"
        stry = "select '' ID_REQ, '' REQUISICION UNION select  id_requisicion, requisicion from [eventos].cat_requisiciones where eliminado = 0 order by  REQUISICION"

        tRsGen = New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        tDrsx = tRsGen.ExecuteReader()
        Try


            While tDrsx.Read


                'ddlRequisicion.DataSource = tDrsx
                'ddlRequisicion.DataTextField = "REQUISICION"
                'ddlRequisicion.DataValueField = "id_req"
                'ddlRequisicion.DataBind()

                ''drpReq.Items.Insert(0, New ListItem("<Seleccione un Item>", "0"))

            End While
        Finally
            tDrsx.Close()
        End Try

        'ORDEND E SERVICIO
        'stry = "select '' ID_REQ, '' REQUERIMIENTO UNION select  id_req,requerimiento from [eventos].cat_requerimiento order by id_req"
        stry = "select '' ID_OS, '' ORDENSERVICIO UNION select  id_os, ordenservicio from [eventos].cat_ordenservicio where eliminado = 0 order by  ORDENSERVICIO"

        tRsGen = New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        tDrsx = tRsGen.ExecuteReader()
        Try


            While tDrsx.Read


                'ddlOrdenServicio.DataSource = tDrsx
                'ddlOrdenServicio.DataTextField = "ORDENSERVICIO"
                'ddlOrdenServicio.DataValueField = "ID_OS"
                'ddlOrdenServicio.DataBind()

                ''drpReq.Items.Insert(0, New ListItem("<Seleccione un Item>", "0"))

            End While
        Finally
            tDrsx.Close()
        End Try

        recalculaPresupuesto()

    End Sub

    Public Class RequerimientoItem
        Public Property Tipo As Integer
        Public Property Id As Integer
        Public Property Requerimiento As String
        Public Property Observaciones As String
        Public Property HoraInstalación As String
        Public Property FechaInstalación As DateTime?
        Public Property Cantidad As Integer
        Public Property Subtotal As Decimal
        Public Property IVA As Integer
        Public Property CostoUnitario As Decimal
        Public Property Total As Decimal
    End Class

    'Protected Sub drpReq_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpReq.SelectedIndexChanged
    '    'Me.lblidreq.Text = Me.drpReq.SelectedValue
    '    Me.txtCantidad.Focus()
    'End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)

        If Me.txtFolio.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar el folio');", True)
            Exit Sub
        End If

        'If ddlTipoReq.SelectedValue = "1" And (Me.lblidreq.Text = "" Or Me.lblidreq.Text = "0") Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor seleccionar un requerimiento');", True)
        '    Exit Sub
        'End If

        'If ddlTipoReq.SelectedValue = "2" And (Me.ddlRequisicion.Text = "") Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor seleccionar una requisición');", True)
        '    Exit Sub
        'End If

        'If ddlTipoReq.SelectedValue = "3" And (Me.ddlOrdenServicio.Text = "") Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor seleccionar una orden de servicio');", True)
        '    Exit Sub
        'End If

        ''If Me.txtObservaciones.Text = "" Then
        ''    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de capturar Observaciones');", True)
        ''    Exit Sub
        ''End If

        'If ddlTipoReq.SelectedValue = "1" And Me.txtCantidad.Text = "0" Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor ingresar una cantidad valida');", True)
        '    Exit Sub
        'End If

        'If ddlTipoReq.SelectedValue = "2" And Me.txtCantidadRequisicion.Text = "0" Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor ingresar una cantidad valida');", True)
        '    Exit Sub
        'End If

        'If ddlTipoReq.SelectedValue = "3" And Me.txtCantidadOS.Text = "0" Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor ingresar una cantidad valida');", True)
        '    Exit Sub
        'End If

        'If ddlTipoReq.SelectedValue = "2" And Me.txtCostoRequisicion.Text = "0" Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor ingresar un costo valido');", True)
        '    Exit Sub
        'End If


        'Dim dt As New DataTable
        'dt = Session("tabses")
        'Dim dr As DataRow
        'dt = New DataTable
        'dt.Columns.Add("Id")
        'dt.Columns.Add("Tipo")
        'dt.Columns.Add("Descripcion")
        'dt.Columns.Add("Observaciones")
        'dt.Columns.Add("HoraInstalación")
        'dt.Columns.Add("FechaInstalación")
        'dt.Columns.Add("Cantidad")
        'dt.Columns.Add("Subtotal")
        'dt.Columns.Add("IVA")
        'dt.Columns.Add("CostoUnitario")
        'dt.Columns.Add("Total")
        Dim listaRequerimientos As New List(Of RequerimientoItem)
        listaRequerimientos = Session("ListaRequerimientosDirector")


        'If grdRequerimientos.Rows.Count > 0 Then
        '    For i As Integer = 0 To grdRequerimientos.Rows.Count - 1

        '        dr = dt.NewRow

        '        If ddlTipoReq.SelectedValue = "1" And Me.lblidreq.Text = GridView1.Rows(i).Cells(2).Text Then
        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar un requerimiento diferente');", True)
        '            Exit Sub
        '        End If

        '        If ddlTipoReq.SelectedValue = "2" And ddlRequisicion.SelectedValue = GridView1.Rows(i).Cells(2).Text Then
        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar una requisición diferente');", True)
        '            Exit Sub
        '        End If

        '        If ddlTipoReq.SelectedValue = "3" And ddlOrdenServicio.SelectedValue = GridView1.Rows(i).Cells(2).Text Then
        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar una requisición diferente');", True)
        '            Exit Sub
        '        End If

        '        dr("TIPO") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(1).Text)
        '        dr("ID") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
        '        dr("DESCRIPCIÓN") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
        '        dr("CANTIDAD") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)

        '        If Me.GridView1.Rows(i).Cells(4).Text = "&nbsp;" Then
        '            dr("OBSERVACION") = ""
        '        Else
        '            dr("OBSERVACION") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
        '        End If

        '        dr("COSTO") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(6).Text)
        '        dr("IVA") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(7).Text)
        '        dr("TOTAL") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(8).Text)
        '        dt.Rows.Add(dr)
        '    Next
        'End If


        ''dr = dt.NewRow


        'If ddlTipoReq.SelectedValue = "1" Then
        '    'CargaCombo para sacar descripcion
        '    CargaRequerimientos()

        '    dr("TIPO") = "Contrato"
        '    dr("ID") = HttpUtility.HtmlDecode(Me.lblidreq.Text)
        '    dr("DESCRIPCIÓN") = HttpUtility.HtmlDecode(requerimiento)
        '    dr("CANTIDAD") = HttpUtility.HtmlDecode(Me.txtCantidad.Text)
        '    dr("OBSERVACION") = HttpUtility.HtmlDecode(Me.txtObservaciones.Text)
        '    'OBTIENE EL COSTO DEL REQUEREIMIENTO
        '    Dim costo As Decimal = 0
        '    Using data As New DB(con.Conectar())

        '        'ELIMINA LOS REQUERIMIENTOS ANTERIORES
        '        Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@idReq", HttpUtility.HtmlDecode(Me.lblidreq.Text))}
        '        Dim dtCosto = data.ObtieneDatos("ObtieneCostoRequerimiento", paramsD).Tables(0)

        '        dr("COSTO") = dtCosto.Rows(0)("costo")
        '        dr("IVA") = dtCosto.Rows(0)("porcentajeIVA")
        '        dr("TOTAL") = dtCosto.Rows(0)("total")
        '    End Using
        'ElseIf ddlTipoReq.SelectedValue = "2" Then
        '    dr("TIPO") = "Requisición"
        '    dr("ID") = ddlRequisicion.SelectedValue()
        '    dr("DESCRIPCIÓN") = ddlRequisicion.SelectedItem.Text
        '    dr("CANTIDAD") = HttpUtility.HtmlDecode(Me.txtCantidadRequisicion.Text)
        '    'OBTIENE EL COSTO DEL REQUISICION
        '    Dim costo As Decimal = 0
        '    Using data As New DB(con.Conectar())

        '        Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@idReq", HttpUtility.HtmlDecode(Me.ddlRequisicion.SelectedValue))}
        '        Dim dtCosto = data.ObtieneDatos("ObtieneCostoRequisicion", paramsD).Tables(0)

        '        dr("COSTO") = dtCosto.Rows(0)("costo")
        '        dr("IVA") = dtCosto.Rows(0)("porcentajeIVA")
        '        dr("TOTAL") = dtCosto.Rows(0)("total")
        '    End Using

        '    dr("OBSERVACION") = HttpUtility.HtmlDecode(Me.txtObservacionesRequisicion.Text)
        'ElseIf ddlTipoReq.SelectedValue = "3" Then
        '    dr("TIPO") = "Orden de Servicio"
        '    dr("ID") = ddlOrdenServicio.SelectedValue()
        '    dr("DESCRIPCIÓN") = ddlOrdenServicio.SelectedItem.Text
        '    dr("CANTIDAD") = HttpUtility.HtmlDecode(Me.txtCantidadOS.Text)
        '    'OBTIENE EL COSTO DEL REQUISICION
        '    Dim costo As Decimal = 0
        '    Using data As New DB(con.Conectar())

        '        Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@idReq", HttpUtility.HtmlDecode(Me.ddlOrdenServicio.SelectedValue))}
        '        Dim dtCosto = data.ObtieneDatos("ObtieneCostoOrdenServicio", paramsD).Tables(0)

        '        dr("COSTO") = dtCosto.Rows(0)("costo")
        '        dr("IVA") = dtCosto.Rows(0)("porcentajeIVA")
        '        dr("TOTAL") = dtCosto.Rows(0)("total")
        '    End Using

        '    dr("OBSERVACION") = HttpUtility.HtmlDecode(Me.txtObservacionesOS.Text)
        'End If

        'dt.Rows.Add(dr)
        'ds.Tables.Add(dt)
        'Session.Add("tabses", dt)
        ''GridView1.DataSource = ds.Tables(0)
        'GridView1.DataSource = dt
        'GridView1.DataBind()

        'Me.txtCantidad.Text = ""
        'Me.txtObservaciones.Text = ""
        'Me.txtHoraInsta.Text = ""
        'Me.txtFechaInsta.Text = ""

        'Me.drpReq.Focus()

        'Me.ddlRequisicion.SelectedIndex = 0
        'Me.txtCantidadRequisicion.Text = ""
        'Me.txtObservacionesRequisicion.Text = ""

        recalculaPresupuesto()

    End Sub

    Private Function ObtieneSiguienteIdRequisicion() As Integer
        Dim id As Integer = 0
        Try
            Dim dt As New DataTable
            dt = Session("tabses")

            For Each dr In dt.Rows
                Dim actual As Integer = dr("ID")
                If actual < id Then
                    id = actual
                End If
            Next

        Catch ex As Exception
        End Try

        Return id - 1
    End Function

    Sub CargaRequerimientos()

        'REQUERIMIENTOS
        stry = "select requerimiento from [eventos].cat_requerimiento where id_req =  " & Me.lblidreq.Text & "  order by id_req"
        tabla = conexion.sqlcon(stry)

        'If tabla.Rows.Count < 1 Then
        '    Me.drpReq.Items.Clear()
        '    Me.drpReq.Items.Add("")
        'Else

        '    If ddlTipoReq.SelectedValue = 1 Then
        '        requerimiento = tabla.Rows(0)("requerimiento")
        '    Else
        '        requerimiento = ddlRequisicion.Text
        '    End If

        'End If

    End Sub

    Private Sub recalculaPresupuesto()
        Try
            Dim lista As New List(Of RequerimientoItem)()
            lista = Session("RequerimientosEvento")
            Dim costoTotal As Decimal = 0
            Dim ivaTotal As Decimal = 0
            Dim totalTotal As Decimal = 0
            If lista.Count > 0 Then
                For Each dr In lista
                    Dim cantidad As Integer = dr.Cantidad
                    Dim total As Decimal = dr.Total
                    Dim costo As Decimal = dr.CostoUnitario
                    Dim iva As Decimal = dr.IVA
                    costoTotal = costoTotal + total 'costoTotal = costoTotal + (costo * cantidad)

                    Try
                        If iva > 0 Then
                            ivaTotal = ivaTotal + (costo * (iva / 100))
                        End If
                    Catch ex As Exception

                    End Try
                    totalTotal = totalTotal + (total)
                Next
            End If

            lblSubtotalActual.Text = String.Format("{0:c2}", costoTotal - ivaTotal)
            lblIvaActual.Text = String.Format("{0:c2}", ivaTotal)
            lblPresupuestoActual.Text = String.Format("{0:c2}", (costoTotal))
        Catch ex As Exception

        End Try

    End Sub


    'Protected Sub GridView1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting



    '    'Dim tabfila As New DataTable
    '    tabfila = Session("tabses")
    '    Dim index As Integer = -1
    '    '3
    '    index = Buscar_Indice(GridView1.Rows(e.RowIndex).Cells(1).Text, GridView1.Rows(e.RowIndex).Cells(2).Text, tabfila)
    '    If index <> -1 Then
    '        Dim rowDeleted = tabfila.Rows(index)
    '        If rowDeleted("ID") = 21 Then
    '            Dim requerimientos As New List(Of RequerimientoInterno)
    '            Session("RequerimientosServInternoEvento") = requerimientos
    '        ElseIf rowDeleted("ID") = 26 Then
    '            Dim imprevistos As New List(Of CatalogoImprevistos)
    '            Session("RequerimientosImprevistosEvento") = imprevistos
    '        End If

    '        tabfila.Rows.RemoveAt(index)
    '        GridView1.DataSource = tabfila
    '        GridView1.DataBind()
    '        Session("tabses") = tabfila

    '    End If


    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)

    'End Sub

    Public Function Buscar_Indice(ByVal tipo As String, ByVal textobusqueda As String, ByVal tabfila As DataTable) As Integer


        Dim iindice As Integer = -1
        Dim encontrado As Boolean = False
        Dim contador As Integer = 0
        Dim row As DataRow



        While encontrado = False And contador <= tabfila.Rows.Count


            row = tabfila.Rows(contador)
            'row(2)
            If (row(1) = textobusqueda And HttpUtility.HtmlDecode(row(0)) = HttpUtility.HtmlDecode(tipo)) Then
                encontrado = True
            End If

            iindice = contador
            contador = contador + 1

        End While
        Return iindice
    End Function

    Protected Sub txtFolio_TextChanged(sender As Object, e As System.EventArgs) Handles txtFolio.TextChanged


        Me.txtFolio.Enabled = False


        If Me.txtFolio.Text <> "" Then

            'Consulta el folio haber si existe

            Dim stry As String
            Dim tabla As DataTable
            stry = "select * from [eventos].reg_evento where folio =  " & Me.txtFolio.Text & ""
            tabla = conexion.sqlcon(stry)

            If tabla.Rows.Count < 1 Then
                Me.txtFolio.Text = ""
                Me.txtFolio.Enabled = True
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('No existe el folio');", True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_NoExisteFolio();", True)
                Exit Sub

            Else

                'VALIDA SI EL EVENTO YA ESTA VALIDADO
                Dim helper As New IntelipolisEngine.Eventos.EventoHelper()
                Dim dt = helper.ObtieneEstatusValidacionEvento(Me.txtFolio.Text)
                Try
                    If dt.Rows(0)("validadoEnlaceAdmin") = True And dt.Rows(0)("validadoDireccion") = False Then
                        'EL EVENTO ESTA LISTO PARA VALIDAR POR DIRECTOR
                        btnValidarEvento.Enabled = True
                    ElseIf dt.Rows(0)("validadoDireccion") = True Then
                        'EL EVENTO YA HA SIDO VALIDADO POR EL DIRECTOR
                        btnValidarEvento.Enabled = False
                    ElseIf dt.Rows(0)("validadoEnlaceAdmin") = False Then
                        'EL EVENTO AUN NO HA SIDO VALIDADO POR EL ENLACE NO LO PUEDE VER EL DIRECTOR
                        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "mensaje", "alert('El evento aún no ha sido validado por el Enlace Administrativo, no se puede revisar.');", True)
                        btnValidarEvento.Enabled = False
                        txtFolio.Enabled = True
                        Return
                    End If
                Catch ex As Exception

                End Try

                'Muestra la informacion de el evento
                lblNombreEvento.Text = tabla.Rows(0)("nombre_evento").ToString()
                lblFechaEvento.Text = String.Format("{0:dd MMMM yyyy}", tabla.Rows(0)("fecha"))
                lblDescripcionEvento.Text = tabla.Rows(0)("descripcion").ToString()

                'Dim validada As Boolean
                Dim validada As Integer
                validada = tabla.Rows(0)("validada").ToString

                ' If validada = True Then
                'If validada = 0 Then 'YA FUE VALIDADA

                ''muestra el gdrid con lo capturado
                Dim con As New conexion

                CargaRequerimientosDirector()
                'stry = "select 'Contrato' TIPO, a.id_req as ID,b.requerimiento as DESCRIPCION,a.cantidad AS CANTIDAD,a.observacion as OBSERVACION, ISNULL(b.costo,1) COSTO, b.porcentajeIVA IVA, b.total TOTAL  from [eventos].reg_requerimiento a inner join [eventos].cat_requerimiento b on a.id_req=b.id_req where a.folio_evento = " & Me.txtFolio.Text & ""
                'stry += "UNION ALL select 'Requisición' TIPO, a.id_req as ID, r.requisicion as DESCRIPCION,a.cantidad AS CANTIDAD,a.observaciones as OBSERVACION, ISNULL(r.costo,1) COSTO, r.porcentajeIVA IVA, r.total from [eventos].reg_requisicion a (nolock) inner join eventos.cat_requisiciones r on a.id_req = r.id_requisicion  where a.folio_evento =" & Me.txtFolio.Text & ""
                'stry += "UNION ALL select 'Orden de Servicio' TIPO, a.id_os as ID, r.ordenServicio as DESCRIPCION,a.cantidad AS CANTIDAD,a.observaciones as OBSERVACION, ISNULL(r.costo,1) COSTO, r.porcentajeIVA IVA, r.total from [eventos].reg_ordenServicio a (nolock) inner join eventos.cat_ordenservicio r on a.id_os = r.id_os  where a.folio_evento =" & Me.txtFolio.Text & ""

                'Dim da As New SqlDataAdapter(stry, con.Conectar)
                'Dim dt As New DataTable
                'da.Fill(dt)

                'Session("tabses") = dt

                'Me.GridView1.DataSource = dt
                'Me.GridView1.DataBind()

                stry = "select  presidium,aforoTotal,id_ceremonia FROM eventos.reg_evento_requerimiento where folio_evento =" & Me.txtFolio.Text
                tabla = conexion.sqlcon(stry)
                If tabla.Rows.Count < 1 Then
                    'es nulo no trae nada

                Else
                    'If IsDBNull(tabla.Rows(0)("hr_insta")) Then
                    '    Me.txtHoraInsta.Text = ""
                    'Else
                    '    Me.txtHoraInsta.Text = tabla.Rows(0)("hr_insta")

                    'End If

                    'If IsDBNull(tabla.Rows(0)("fecha_insta")) Then
                    '    Me.txtFechaInsta.Text = ""
                    'Else


                    '    Me.txtFechaInsta.Text = tabla.Rows(0)("fecha_insta")


                    'End If


                    If tabla.Rows(0)("presidium") = False Then
                        Me.CheckMesaPresidium.Checked = False
                    Else
                        Me.CheckMesaPresidium.Checked = True
                    End If

                    If IsDBNull(tabla.Rows(0)("aforoTotal")) Then
                        Me.txtAforo.Text = ""
                    Else

                        Me.txtAforo.Text = tabla.Rows(0)("aforoTotal")
                    End If

                    If IsDBNull(tabla.Rows(0)("id_ceremonia")) Then
                        Me.drpCeremonia.Text = ""
                    Else
                        Me.drpCeremonia.Text = tabla.Rows(0)("id_ceremonia")

                    End If
                End If

                'stry = "select a.id_ceremonia FROM [eventos].reg_requerimiento  a inner join [eventos].cat_ceremonia b  on a.id_ceremonia =b.id where a.folio_evento =" & Me.txtFolio.Text & "  GROUP BY id_ceremonia"
                'tabla = conexion.sqlcon(stry)

                'If tabla.Rows.Count < 1 Then
                '    'es nulo no trae nada
                '    cargarCeremonia()
                'Else


                '    If IsDBNull(tabla.Rows(0)("id_ceremonia")) Then
                '        Me.drpCeremonia.Text = ""
                '    Else
                '        Me.drpCeremonia.Text = tabla.Rows(0)("id_ceremonia")

                '    End If

                'End If

                'Me.txtHoraInsta.Enabled = True
                'Me.txtFechaInsta.Enabled = True
                Me.CheckMesaPresidium.Enabled = True
                Me.txtAforo.Enabled = True
                'Me.btnValidarEvento.Enabled = True
                Me.txtObservaciones.Enabled = True
                'Me.drpReq.Enabled = True
                Me.drpCeremonia.Enabled = True
                Me.txtCantidad.Enabled = True



                'Else
                '    If validada = 1 Then
                '        'mensaje de que No ha sido validada por Relacion Publica
                '        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('No ha sido validado por Relacion Publica');", True)
                '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Req_NoHaSidoValidada();", True)
                '        Exit Sub

                '    Else
                '        If validada = 2 Then
                '            'MENSAJE QUE NO HA SIDO APROBADA POR RELACIONES PUBLICAS
                '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Req_NoHaSidoAprobada();", True)
                '            Exit Sub
                '        End If
                '    End If

                'End If

            End If
        End If

        recalculaPresupuesto()
    End Sub

    Private Sub CargaRequerimientosDirector()
        'CARGA LOS REQUERIMIENTOS DE EL EVENTO
        Dim requerimientosLista As New List(Of RequerimientoItem)()
        Using data As New DB(con.Conectar())

            'carga asignados
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text)}
            Dim dt = data.ObtieneDatos("CargaRequerimientosEvento", params).Tables(0)
            For Each row In dt.Rows
                Dim rq As New RequerimientoItem()
                rq.Tipo = row("TIPO")
                rq.Id = row("ID_REQUERIMIENTO")
                rq.Requerimiento = row("REQUERIMIENTO")
                rq.Cantidad = row("CANTIDAD")
                rq.Observaciones = row("OBSERVACION")
                rq.CostoUnitario = row("COSTO")
                rq.IVA = row("IVA")
                rq.Total = row("TOTAL")
                Try
                    rq.HoraInstalación = row("HORA_INSTALACION")
                Catch ex As Exception

                End Try
                Try
                    rq.FechaInstalación = row("FECHA_INSTALACION")
                Catch ex As Exception

                End Try

                requerimientosLista.Add(rq)
            Next
            Session("RequerimientosEvento") = requerimientosLista
        End Using

        grdRequerimientos.DataSource = requerimientosLista.OrderBy(Function(x) x.Tipo).ThenBy(Function(x) x.Tipo).ToList()
        grdRequerimientos.DataBind()
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Me.txtFolio.Enabled = True


        Me.txtFolio.Text = ""
        Me.txtCantidad.Text = ""
        Me.txtObservaciones.Text = ""
        'Me.txtHoraInsta.Text = ""
        'Me.txtFechaInsta.Text = ""
        Me.txtAforo.Text = ""
        Me.CheckMesaPresidium.Checked = False
        Me.txtFolio.Enabled = True
        'Me.drpReq.Enabled = False
        Me.drpCeremonia.Enabled = False
        Me.txtCantidad.Enabled = False
        Me.txtObservaciones.Enabled = False
        'Me.txtHoraInsta.Enabled = False
        'Me.txtFechaInsta.Enabled = False
        Me.CheckMesaPresidium.Enabled = False
        Me.txtAforo.Enabled = False


        'Me.GridView1.DataSourceID = Nothing
        'Me.GridView1.DataSource = Nothing
        'Me.GridView1.DataBind()


    End Sub

    'Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click

    '    Dim presidium As Integer
    '    If Me.CheckMesaPresidium.Checked = False Then
    '        presidium = 0
    '    Else
    '        presidium = 1
    '    End If


    '    If Me.txtAforo.Text = "" Then
    '        Me.txtAforo.Text = "0"


    '    End If

    '    If Len(Me.txtHoraInsta.Text) = 0 Then
    '    Else
    '        If Len(Me.txtHoraInsta.Text) <> 5 Then
    '            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)
    '            Exit Sub
    '        End If
    '    End If

    '    If Me.GridView1.Rows.Count <> 0 And Me.txtFolio.Text <> "" Then


    '        Dim stry As String
    '        Dim tabla As DataTable
    '        stry = "select folio_req from [eventos].reg_requerimiento WHERE folio_evento = " & Me.txtFolio.Text & ""
    '        tabla = conexion.sqlcon(stry)

    '        If tabla.Rows.Count < 1 Then
    '            'no existe folio por lo tanto no tiene capturado nada 
    '            'ELIMINA REQUISICIONES
    '            stry = "delete from [eventos].reg_requisicion WHERE folio_evento = " & Me.txtFolio.Text & ""
    '            resultado = conexion.sqlcambios(stry)

    '            If GridView1.Rows.Count > 0 Then
    '                For i As Integer = 0 To GridView1.Rows.Count - 1
    '                    Dim tipo = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(1).Text).ToLower()
    '                    If tipo = "contrato" Then
    '                        Dim ID = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
    '                        Dim DESCRIPCIÓN = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
    '                        Dim CANTIDAD = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)
    '                        Dim OBSERVACION As String

    '                        If Me.GridView1.Rows(i).Cells(5).Text = "&nbsp;" Then

    '                            OBSERVACION = ""
    '                        Else
    '                            OBSERVACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
    '                        End If


    '                        crearfolioReq()
    '                        If Me.drpCeremonia.Text = 0 Then

    '                            If Me.txtFechaInsta.Text = "" Then
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & "," & Trim(Me.txtAforo.Text) & ",getdate()," & Session("clave_empl") & ")"
    '                            Else
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & "," & Me.txtAforo.Text & ",getdate()," & Session("clave_empl") & ")"
    '                            End If


    '                        Else

    '                            If Me.txtFechaInsta.Text = "" Then
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & "," & Trim(Me.txtAforo.Text) & ",getdate()," & Session("clave_empl") & ")"
    '                            Else
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & "," & Me.txtAforo.Text & ",getdate()," & Session("clave_empl") & ")"
    '                            End If

    '                        End If

    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
    '                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
    '                            Exit Sub
    '                        End If
    '                    ElseIf tipo = "requisición" Then
    '                        'REQUISICIONES
    '                        Dim COSTO = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(6).Text)
    '                        Dim ID_REQUISICION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
    '                        Dim DESCRIPCIÓN = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
    '                        Dim CANTIDAD = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)
    '                        Dim OBSERVACION As String

    '                        If Me.GridView1.Rows(i).Cells(5).Text = "&nbsp;" Then

    '                            OBSERVACION = ""
    '                        Else
    '                            OBSERVACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
    '                        End If

    '                        stry = String.Format("INSERT INTO [eventos].reg_requisicion (folio_evento,id_req,cantidad,observaciones,hr_insta,fecha_insta,fecha_captura,empl_captura) VALUES({0},{1},{2},'{3}','','',GETDATE(),{4})",
    '                                             Trim(txtFolio.Text), ID_REQUISICION, CANTIDAD, OBSERVACION, Session("clave_empl"))

    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
    '                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
    '                            Exit Sub
    '                        End If
    '                    ElseIf tipo = "ordern de servicio" Then
    '                        'ORDEN DE SERVICIO
    '                        Dim COSTO = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(6).Text)
    '                        Dim ID_REQUISICION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
    '                        Dim DESCRIPCIÓN = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
    '                        Dim CANTIDAD = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)
    '                        Dim OBSERVACION As String

    '                        If Me.GridView1.Rows(i).Cells(5).Text = "&nbsp;" Then

    '                            OBSERVACION = ""
    '                        Else
    '                            OBSERVACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
    '                        End If

    '                        stry = String.Format("INSERT INTO [eventos].reg_ordenServicio (folio_evento,id_os,cantidad,observaciones,hr_insta,fecha_insta,fecha_captura,empl_captura) VALUES({0},{1},{2},'{3}','','',GETDATE(),{4})",
    '                                             Trim(txtFolio.Text), ID_REQUISICION, CANTIDAD, OBSERVACION, Session("clave_empl"))

    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
    '                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
    '                            Exit Sub
    '                        End If
    '                    End If


    '                    '-------------------------
    '                    '    If Me.txtFechaInsta.Text = "" Then

    '                    '        If OBSERVACION = "" Then
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,null,'" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        Else
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        End If

    '                    '    Else
    '                    '        If Me.txtObservaciones.Text = "" Then
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,null,'" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        Else
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        End If


    '                    '    End If


    '                    'Else

    '                    '    If Me.txtFechaInsta.Text = "" Then

    '                    '        If Me.txtObservaciones.Text = "" Then
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,'" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        Else
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        End If


    '                    '    Else
    '                    '        If Me.txtObservaciones.Text = "" Then
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,'" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        Else
    '                    '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                    '        End If


    '                    '    End If



    '                Next

    '                If Me.drpCeremonia.Text <> 0 Then
    '                    'EL EVENTO SI TENDRA MAESTRO DE CEREMONIA ENTONCES, INSERTA EN ENVIA CORREO
    '                    stry = "SELECT  correo FROM eventos.correos_ceremonia WHERE id_ceremonia = " & Me.drpCeremonia.SelectedValue & " "
    '                    tabla = conexion.sqlcon(stry)
    '                    If tabla.Rows.Count < 1 Then
    '                        'No envia informacion no encotro nada
    '                    Else
    '                        'formato =  2 (FICHA)
    '                        Dim correo_ceremonia As String = tabla.Rows(0)("correo")
    '                        stry = "INSERT INTO [eventos].envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "', '" & Trim(correo_ceremonia) & "',0, null, 2)"
    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                        End If
    '                    End If
    '                End If

    '                ''GUARDA EL REGISTRO EN REG EVENTO
    '                'stry = String.Format("UPDATE [eventos].reg_evento SET requerimientos='{0}' WHERE folio = {1}", True, txtFolio.Text)
    '                'conexion.sqlcambios(stry)

    '                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alert('Se guardo la informacion con Éxito');", True)
    '                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito();", True)


    '            End If


    '        Else
    '            'ya existe informacion, la elimina y luego inserta
    '            folio_req = tabla.Rows(0)("folio_req").ToString
    '            stry = "delete from [eventos].reg_requerimiento WHERE folio_evento = " & Me.txtFolio.Text & ""
    '            resultado = conexion.sqlcambios(stry)

    '            'ELIMINA REQUISICIONES
    '            stry = "delete from [eventos].reg_requisicion WHERE folio_evento = " & Me.txtFolio.Text & ""
    '            resultado = conexion.sqlcambios(stry)

    '            'ELIMINA ORDENES DE SERVICIO
    '            stry = "delete from [eventos].reg_ordenServicio WHERE folio_evento = " & Me.txtFolio.Text & ""
    '            resultado = conexion.sqlcambios(stry)

    '            stry = "insert into [eventos].bitacora_movimiento values(" & Session("clave_empl") & ", 'REQUERIMIENTO','Se elimino el folio req para volver a capturar'," & Trim(folio_req) & ",getdate())"
    '            resultado = conexion.sqlcambios(stry)

    '            'insertamos la captura nueva

    '            If GridView1.Rows.Count > 0 Then


    '                For i As Integer = 0 To GridView1.Rows.Count - 1
    '                    Dim tipo = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(1).Text).ToLower()
    '                    If tipo = "contrato" Then
    '                        Dim ID = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
    '                        Dim DESCRIPCIÓN = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
    '                        Dim CANTIDAD = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)
    '                        'Dim OBSERVACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)

    '                        Dim OBSERVACION As String

    '                        If Me.GridView1.Rows(i).Cells(5).Text = "&nbsp;" Then

    '                            OBSERVACION = ""
    '                        Else
    '                            OBSERVACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
    '                        End If

    '                        crearfolioReq()

    '                        If Me.drpCeremonia.Text = 0 Then

    '                            If Me.txtFechaInsta.Text = "" Then
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & "," & Me.txtAforo.Text & ",getdate()," & Session("clave_empl") & ")"
    '                            Else
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & "," & Me.txtAforo.Text & ",getdate()," & Session("clave_empl") & ")"
    '                            End If


    '                        Else

    '                            If Me.txtFechaInsta.Text = "" Then
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & "," & Me.txtAforo.Text & ",getdate()," & Session("clave_empl") & ")"
    '                            Else
    '                                stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & "," & Me.txtAforo.Text & ",getdate()," & Session("clave_empl") & ")"
    '                            End If
    '                        End If

    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
    '                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
    '                            Exit Sub

    '                        Else


    '                            stry = "insert into [eventos].bitacora_movimiento values(" & Session("clave_empl") & ", 'REQUERIMIENTO','Se actualizó la informacion del folio req'," & Trim(folio) & ",getdate())"
    '                            resultado = conexion.sqlcambios(stry)


    '                        End If



    '                        '    If Me.txtFechaInsta.Text = "" Then
    '                        '        If Me.txtObservaciones.Text = "" Then
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,null,'" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        Else
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        End If

    '                        '    Else
    '                        '        If Me.txtObservaciones.Text = "" Then
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,null,'" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        Else
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "',null,'" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        End If


    '                        '    End If


    '                        'Else

    '                        '    If Me.txtFechaInsta.Text = "" Then

    '                        '        If Me.txtObservaciones.Text = "" Then
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,'" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        Else
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "',null," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        End If


    '                        '    Else
    '                        '        If Me.txtObservaciones.Text = "" Then
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "',null,'" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        Else
    '                        '            stry = "INSERT INTO [eventos].reg_requerimiento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & ID & "','" & CANTIDAD & "','" & OBSERVACION & "','" & Me.drpCeremonia.SelectedValue & "','" & Me.txtHoraInsta.Text & "','" & Me.txtFechaInsta.Text & "'," & Trim(presidium) & ",getdate()," & Session("clave_empl") & ")"
    '                        '        End If


    '                        '    End If
    '                    ElseIf tipo = "requisición" Then
    '                        'REQUISICIONES
    '                        Dim COSTO = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(6).Text)
    '                        Dim ID_REQUISICION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
    '                        Dim DESCRIPCIÓN = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
    '                        Dim CANTIDAD = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)
    '                        Dim OBSERVACION As String

    '                        If Me.GridView1.Rows(i).Cells(5).Text = "&nbsp;" Then

    '                            OBSERVACION = ""
    '                        Else
    '                            OBSERVACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
    '                        End If

    '                        stry = String.Format("INSERT INTO [eventos].reg_requisicion (folio_evento,id_req,cantidad,observaciones,hr_insta,fecha_insta,fecha_captura,empl_captura) VALUES({0},{1},{2},'{3}','','',GETDATE(),{4})",
    '                                             Trim(txtFolio.Text), ID_REQUISICION, CANTIDAD, OBSERVACION, Session("clave_empl"))
    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
    '                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
    '                            Exit Sub
    '                        End If
    '                    ElseIf tipo = "orden de servicio" Then
    '                        'REQUISICIONES
    '                        Dim COSTO = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(6).Text)
    '                        Dim ID_REQUISICION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
    '                        Dim DESCRIPCIÓN = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
    '                        Dim CANTIDAD = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)
    '                        Dim OBSERVACION As String

    '                        If Me.GridView1.Rows(i).Cells(5).Text = "&nbsp;" Then

    '                            OBSERVACION = ""
    '                        Else
    '                            OBSERVACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
    '                        End If

    '                        stry = String.Format("INSERT INTO [eventos].reg_ordenServicio (folio_evento,id_os,cantidad,observaciones,hr_insta,fecha_insta,fecha_captura,empl_captura) VALUES({0},{1},{2},'{3}','','',GETDATE(),{4})",
    '                                             Trim(txtFolio.Text), ID_REQUISICION, CANTIDAD, OBSERVACION, Session("clave_empl"))
    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
    '                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
    '                            Exit Sub
    '                        End If

    '                    End If

    '                Next

    '                If Me.drpCeremonia.Text <> 0 Then
    '                    'EL EVENTO SI TENDRA MAESTRO DE CEREMONIA ENTONCES, INSERTA EN ENVIA CORREO
    '                    stry = "SELECT  correo FROM [eventos].correos_ceremonia WHERE id_ceremonia = " & Me.drpCeremonia.SelectedValue & " "
    '                    tabla = conexion.sqlcon(stry)
    '                    If tabla.Rows.Count < 1 Then
    '                        'No envia informacion no encotro nada
    '                    Else
    '                        'formato =  2 (FICHA)
    '                        Dim correo_ceremonia As String = tabla.Rows(0)("correo")
    '                        stry = "INSERT INTO [eventos].envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "','" & Trim(correo_ceremonia) & "',0, null, 2)"
    '                        resultado = conexion.sqlcambios(stry)
    '                        If resultado = -1 Then
    '                        End If
    '                    End If
    '                End If

    '                ''GUARDA EL REGISTRO EN REG EVENTO
    '                'stry = String.Format("UPDATE [eventos].reg_evento SET requerimientos='{0}' WHERE folio = {1}", True, txtFolio.Text)
    '                'conexion.sqlcambios(stry)

    '                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alert('Se guardo la informacion con Éxito');", True)
    '                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito();", True)


    '            End If
    '        End If

    '        'AGREGA LOS REQUERIMIENTOS INTERNOS
    '        Dim requerimientos As New List(Of RequerimientoInterno)
    '        If Session("RequerimientosServInternoEvento") IsNot Nothing Then
    '            requerimientos = Session("RequerimientosServInternoEvento")
    '        End If

    '        If requerimientos IsNot Nothing And requerimientos.Count > 0 Then
    '            Using data As New DB(con.Conectar())

    '                'ELIMINA LOS REQUERIMIENTOS ANTERIORES
    '                Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text)}
    '                data.EjecutaCommand("EliminaRequerimientosInterno", paramsD)

    '                For Each row In requerimientos.Where(Function(x) x.Asignado = True)
    '                    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text), New SqlParameter("@idReq", row.Id), New SqlParameter("@cantidad", 1)}
    '                    data.EjecutaCommand("AgregaRequerimientoInternoEvento", params)
    '                Next

    '            End Using
    '        End If


    '        'AGREGA LOS IMPREVISTOS
    '        Dim imprevistos As New List(Of CatalogoImprevistos)
    '        If Session("RequerimientosImprevistosEvento") IsNot Nothing Then
    '            imprevistos = Session("RequerimientosImprevistosEvento")
    '        End If

    '        If imprevistos IsNot Nothing And imprevistos.Count > 0 Then
    '            Using data As New DB(con.Conectar())

    '                'ELIMINA LOS IMPREVISTOS ANTERIORES
    '                Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text)}
    '                data.EjecutaCommand("EliminaImprevistos", paramsD)

    '                For Each row In imprevistos.Where(Function(x) x.Asignado = True)
    '                    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text), New SqlParameter("@idImp", row.Id), New SqlParameter("@cantidad", 1)}
    '                    data.EjecutaCommand("AgregaImprevistosEvento", params)
    '                Next

    '            End Using
    '        End If


    '        '----------------------------------
    '        'REVISA QUE EL REQUERIMIENTO AUDIENCIA (id = 18) SEA MATOY E IGUAL A 300 Y ENVIA CORREO

    '        'stry = "select b.folio, a.folio_req,a.id_req,a.cantidad, b.nombre_evento " & _
    '        '        "from [eventos].reg_requerimiento  a " & _
    '        '        "left join [eventos].reg_evento  b " & _
    '        '        "on a.folio_evento = b.folio " & _
    '        '        "WHERE(a.folio_evento = 1 And a.id_req = 18 And a.cantidad >= 300)"
    '        'tabla = conexion.sqlcon(stry)
    '        'If tabla.Rows.Count < 1 Then
    '        'Else

    '        '    '---------------------------------
    '        '    cantidad_decimal = tabla.Rows(0)("cantidad").ToString
    '        '    cantidad = CStr(cantidad_decimal)
    '        '    nombre_evento = tabla.Rows(0)("nombre_evento").ToString
    '        '    folio_evento = tabla.Rows(0)("folio").ToString

    '        '    'ENVIA CORREO A CARLOS VILLARREAL
    '        '    Dim email As String = "rocio.marflo@gmail.com" 'PARA para quien quiere que le llegue el msj
    '        '    Dim respuestabody As String = "Buen Día" + Environment.NewLine + Environment.NewLine + "Se requiere para el evento:" + Environment.NewLine + folio_evento + " " + "-" + " " + nombre_evento + Environment.NewLine + Environment.NewLine + "La cantidad de: " + cantidad + " personas." + Environment.NewLine + Environment.NewLine + "Saludos!"
    '        '    Dim correo As New System.Net.Mail.MailMessage
    '        '    correo = New System.Net.Mail.MailMessage()
    '        '    correo.From = New System.Net.Mail.MailAddress("laumarflo@hotmail.com") 'DE cambiar por el corre de Clara
    '        '    correo.To.Add(email) 'PARA
    '        '    correo.Subject = "REQUERIMIENTO PARA EL EVENTO: " + " " + Me.txtFolio.Text + " " + nombre_evento    'TITULO
    '        '    correo.Body = respuestabody  'CUERPO
    '        '    correo.IsBodyHtml = False
    '        '    correo.Priority = System.Net.Mail.MailPriority.Normal

    '        '    Dim smtp As New System.Net.Mail.SmtpClient("smtp.live.com", 25)
    '        '    smtp.Host = "smtp.live.com"
    '        '    smtp.Credentials = New System.Net.NetworkCredential("", "")
    '        '    smtp.EnableSsl = True
    '        '    Try
    '        '        smtp.Send(correo)
    '        '        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alertaEnviaCorreo();", True)
    '        '    Catch ex As Exception
    '        '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alertaErrorEnvioCorreo();", True)
    '        '    End Try
    '        'End If
    '        '-------------------------------------------------------------------------------------------
    '        'VERIFICA SI HAY AFORO, SI EXISTE AFORO SE INSERTA A ENVIA CORREO CON FORMATO 1 (EVENTO)

    '        If Me.txtAforo.Text >= "1" Then
    '            'ENVIA CORREO A 'AFORO'
    '            stry = "SELECT correo FROM [eventos].CORREOS WHERE TIPO = 'AFORO'"
    '            Dim tRsGen3 As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
    '            Dim tDrsx3 As System.Data.SqlClient.SqlDataReader
    '            tDrsx3 = tRsGen3.ExecuteReader()
    '            Try
    '                While tDrsx3.Read
    '                    Dim correo_aforo As String = tDrsx3(0)
    '                    stry = "INSERT INTO [eventos].envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "','" & Trim(correo_aforo) & "',0, null, 1)"
    '                    resultado = conexion.sqlcambios(stry)
    '                    If resultado = -1 Then
    '                    End If
    '                End While
    '            Finally
    '                tDrsx3.Close()
    '            End Try
    '        End If
    '        '------------------------------------------------------------------------------------------


    '        Me.txtFolio.Text = ""
    '        Me.txtAforo.Text = ""
    '        Me.txtCantidad.Text = ""
    '        Me.txtObservaciones.Text = ""
    '        Me.txtHoraInsta.Text = ""
    '        Me.txtFechaInsta.Text = ""
    '        Me.txtFolio.Enabled = True
    '        Me.CheckMesaPresidium.Checked = False
    '        Me.txtFolio.Enabled = True
    '        Me.drpReq.Enabled = False
    '        Me.txtCantidad.Enabled = False
    '        Me.drpCeremonia.Enabled = False
    '        Me.txtObservaciones.Enabled = False
    '        Me.txtHoraInsta.Enabled = False
    '        Me.txtFechaInsta.Enabled = False
    '        Me.CheckMesaPresidium.Enabled = False
    '        Me.txtAforo.Enabled = False

    '        Me.GridView1.DataSourceID = Nothing
    '        Me.GridView1.DataSource = Nothing
    '        Me.GridView1.DataBind()

    '    Else
    '        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar el folio del evento y Seleccionar los Requerimientos!!');", True)
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_DatosFaltantes();", True)

    '    End If

    'End Sub

    Sub crearfolioReq()

        Session("folioReq") = 0
        stry = "Select max(folio_req) as folio from [eventos].reg_requerimiento"
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            MsgBox("No se encontro la información")
        Else
            If (tabla.Rows(0)("folio").ToString) = "" Then
                folio = 1
                Session("folioReq") = folio
                'Me.txtFolio.Text = folio
            Else
                folio = tabla.Rows(0)("folio").ToString
                folio = folio + 1
                Session("folioReq") = folio
                'Me.txtFolio.Text = folio

            End If
        End If
    End Sub

    'Protected Sub txtHoraInsta_TextChanged(sender As Object, e As System.EventArgs) Handles txtHoraInsta.TextChanged
    '    If Len(Me.txtHoraInsta.Text) = 0 Then
    '    Else
    '        If Len(Me.txtHoraInsta.Text) <> 5 Then
    '            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)
    '            Exit Sub
    '        End If
    '    End If

    '    Me.txtFechaInsta.Focus()

    'End Sub


    'Protected Sub txtFechaInsta_TextChanged(sender As Object, e As System.EventArgs) Handles txtFechaInsta.TextChanged
    '    Me.CheckMesaPresidium.Focus()

    'End Sub

    Protected Sub CheckMesaPresidium_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckMesaPresidium.CheckedChanged
        Me.txtAforo.Focus()

    End Sub
    'Protected Sub drpReq_SelectedIndexChanged1(sender As Object, e As EventArgs)
    '    Try
    '        If drpReq.SelectedValue = "21" Then
    '            Dim folio As Integer = 0
    '            Try
    '                folio = Integer.Parse(txtFolio.Text)
    '            Catch ex As Exception

    '            End Try
    '            CargaInformacionRequerimientoInterno(folio)
    '            ScriptManager.RegisterStartupScript(updRequerimientosInterno, updRequerimientosInterno.GetType(), "abre_modal_requerimiento", "abreModalRequerimientoInterno();", True)
    '        ElseIf drpReq.SelectedValue = "26" Then
    '            Dim folio As Integer = 0
    '            Try
    '                folio = Integer.Parse(txtFolio.Text)
    '            Catch ex As Exception

    '            End Try
    '            CargaInformacionImprevistos(folio)
    '            ScriptManager.RegisterStartupScript(updRequerimientosInterno, updRequerimientosInterno.GetType(), "abre_modal_imprevistos", "abreModalImprevistos();", True)
    '        End If
    '    Catch ex As Exception
    '        Dim errorMessage = ex.ToString()
    '    End Try
    'End Sub

    Public Class RequerimientoInterno
        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Asignado As String
    End Class

    Public Class CatalogoImprevistos
        Public Property Id As Integer
        Public Property Nombre As String
        Public Property Asignado As String
    End Class


    Private Sub CargaInformacionImprevistos(folio As Integer)
        Dim imprevistos = New List(Of CatalogoImprevistos)
        If Session("RequerimientosImprevistosEvento") IsNot Nothing Then
            imprevistos = CType(Session("RequerimientosImprevistosEvento"), List(Of CatalogoImprevistos))
        End If


        If imprevistos Is Nothing Or imprevistos.Count = 0 Then

            Using data As New DB(con.Conectar())

                'carga asignados
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", folio)}
                Dim dt = data.ObtieneDatos("ObtieneImprevistosEvento", params).Tables(0)
                For Each row In dt.Rows
                    Dim rq As New CatalogoImprevistos
                    rq.Asignado = row("Asignado")
                    rq.Id = row("Id")
                    rq.Nombre = row("Nombre")
                    imprevistos.Add(rq)
                Next
                Session("RequerimientosImprevistosEvento") = imprevistos
            End Using

        End If

        'Carga Administracion
        rptImprevistos.DataSource = imprevistos
        rptImprevistos.DataBind()
    End Sub

    Private Sub CargaInformacionRequerimientoInterno(folio As Integer)
        Dim requerimientos = New List(Of RequerimientoInterno)
        If Session("RequerimientosServInternoEvento") IsNot Nothing Then
            requerimientos = CType(Session("RequerimientosServInternoEvento"), List(Of RequerimientoInterno))
        End If


        If requerimientos Is Nothing Or requerimientos.Count = 0 Then

            Using data As New DB(con.Conectar())

                'carga asignados
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio_evento", folio)}
                Dim dt = data.ObtieneDatos("ObtieneRequerimientoInternoEvento", params).Tables(0)
                For Each row In dt.Rows
                    Dim rq As New RequerimientoInterno
                    rq.Asignado = row("asignado")
                    rq.Id = row("id")
                    rq.Nombre = row("nombre")
                    requerimientos.Add(rq)
                Next
                Session("RequerimientosServInternoEvento") = requerimientos
            End Using

        End If

        'Carga Administracion
        rptRequerimientosInternos.DataSource = requerimientos
        rptRequerimientosInternos.DataBind()
    End Sub

    Private Sub CargaCatalogoRequerimientosInternos()
        Using data As New DB(con.Conectar())

            'carga catalogo requerimientos
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@masUsados", False)}
            ddlCatalogoRequerimientos.DataSource = data.ObtieneDatos("ObtieneCatalogoRequerimientoInterno", params)
            ddlCatalogoRequerimientos.DataTextField = "nombre"
            ddlCatalogoRequerimientos.DataValueField = "id"
            ddlCatalogoRequerimientos.DataBind()
        End Using
    End Sub

    Private Sub CargaCatalogoRequerimientos()
        Using data As New DB(con.Conectar())

            'carga catalogo requerimientos
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@empleado", Session("clave_empl"))}
            ddlRequerimientoDirector.DataSource = data.ObtieneDatos("CargaRequerimientosDirector", params)
            ddlRequerimientoDirector.DataTextField = "requerimiento"
            ddlRequerimientoDirector.DataValueField = "id"
            ddlRequerimientoDirector.DataBind()
        End Using
    End Sub

    'Protected Sub btnGuardarRequerimiento_Click(sender As Object, e As EventArgs)

    '    Dim requerimientos = New List(Of RequerimientoInterno)
    '    For Each row In rptRequerimientosInternos.Items

    '        Dim checked As Boolean = DirectCast(row.FindControl("chkAsignado"), CheckBox).Checked
    '        Dim id As Integer = DirectCast(row.FindControl("hdnIdRequerimiento"), HiddenField).Value
    '        Dim nombre As String = DirectCast(row.FindControl("lblNombreAsignado"), Label).Text
    '        Dim req As New RequerimientoInterno
    '        req.Asignado = checked
    '        req.Id = id
    '        req.Nombre = nombre

    '        requerimientos.Add(req)
    '    Next

    '    Session("RequerimientosServInternoEvento") = requerimientos

    '    Dim dt As New DataTable
    '    dt = Session("tabses")
    '    Dim dr As DataRow

    '    dt = New DataTable
    '    dt.Columns.Add("TIPO")
    '    dt.Columns.Add("ID")
    '    dt.Columns.Add("DESCRIPCIÓN")
    '    dt.Columns.Add("CANTIDAD")
    '    dt.Columns.Add("OBSERVACION")
    '    dt.Columns.Add("COSTO")
    '    dt.Columns.Add("IVA")
    '    dt.Columns.Add("TOTAL")
    '    Session.Add("Tabla", dt)


    '    If GridView1.Rows.Count > 0 Then
    '        For i As Integer = 0 To GridView1.Rows.Count - 1

    '            dr = dt.NewRow

    '            If HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text) = 21 Then
    '                Continue For
    '            End If
    '            dr("TIPO") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(1).Text)
    '            dr("ID") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
    '            dr("DESCRIPCIÓN") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
    '            dr("CANTIDAD") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)

    '            If Me.GridView1.Rows(i).Cells(5).Text = "&nbsp;" Then
    '                dr("OBSERVACION") = ""
    '            Else
    '                dr("OBSERVACION") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(5).Text)
    '            End If
    '            dr("COSTO") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(6).Text)
    '            dr("IVA") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(7).Text)
    '            dr("TOTAL") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(8).Text)

    '            dt.Rows.Add(dr)
    '        Next
    '    End If

    '    dr = dt.NewRow

    '    dr("TIPO") = "Contrato"
    '    dr("ID") = HttpUtility.HtmlDecode(Me.lblidreq.Text)
    '    dr("DESCRIPCIÓN") = drpReq.SelectedItem.Text
    '    dr("CANTIDAD") = requerimientos.Where(Function(x) x.Asignado = True).Count
    '    Dim requerimientosLista As String = ""
    '    For Each row In requerimientos.Where(Function(x) x.Asignado = True)
    '        requerimientosLista += row.Nombre & ", "
    '    Next
    '    requerimientosLista = requerimientosLista.Substring(0, requerimientosLista.Length - 2)
    '    dr("OBSERVACION") = requerimientosLista
    '    dr("COSTO") = 1

    '    dt.Rows.Add(dr)
    '    ds.Tables.Add(dt)
    '    Session.Add("tabses", dt)
    '    GridView1.DataSource = dt
    '    GridView1.DataBind()

    '    drpReq.SelectedIndex = 0
    '    ScriptManager.RegisterStartupScript(updRequerimientosInterno, updRequerimientosInterno.GetType(), "cierra_modal_requerimiento", "cierraModalRequerimientoInterno();", True)
    'End Sub

    Protected Sub btnReqAgregarCatalogo_Click(sender As Object, e As EventArgs)
        Try
            Dim requerimientos As New List(Of RequerimientoInterno)()
            For Each row In rptRequerimientosInternos.Items

                Dim checked As Boolean = DirectCast(row.FindControl("chkAsignado"), CheckBox).Checked
                Dim id As Integer = DirectCast(row.FindControl("hdnIdRequerimiento"), HiddenField).Value
                Dim nombre As String = DirectCast(row.FindControl("lblNombreAsignado"), Label).Text
                Dim r As New RequerimientoInterno
                r.Asignado = checked
                r.Id = id
                r.Nombre = nombre

                requerimientos.Add(r)
            Next
            'requerimientos = Session("RequerimientosServInternoEvento")
            If Not requerimientos.Where(Function(x) x.Id = ddlCatalogoRequerimientos.SelectedValue).Any() Then
                Dim req As New RequerimientoInterno
                req.Asignado = False
                req.Nombre = ddlCatalogoRequerimientos.SelectedItem.Text
                req.Id = ddlCatalogoRequerimientos.SelectedValue
                req.Asignado = True
                requerimientos.Add(req)
            Else


            End If

            'Carga Administracion
            rptRequerimientosInternos.DataSource = requerimientos
            rptRequerimientosInternos.DataBind()
        Catch ex As Exception
            Dim exx As String = ex.Message
        End Try

    End Sub

    Protected Sub btnAgregarImprevistos_Click(sender As Object, e As EventArgs)

        Dim imprevistos = New List(Of CatalogoImprevistos)
        For Each row In rptImprevistos.Items

            Dim checked As Boolean = DirectCast(row.FindControl("chkAsignado"), CheckBox).Checked
            Dim id As Integer = DirectCast(row.FindControl("hdnIdImprevisto"), HiddenField).Value
            Dim nombre As String = DirectCast(row.FindControl("lblNombreAsignado"), Label).Text
            Dim req As New CatalogoImprevistos
            req.Asignado = checked
            req.Id = id
            req.Nombre = nombre

            imprevistos.Add(req)
        Next

        Session("RequerimientosImprevistosEvento") = imprevistos

        Dim requerimientos As New List(Of RequerimientoItem)()
        If Session("RequerimientosEvento") IsNot Nothing Then
            requerimientos = Session("RequerimientosEvento")
        End If

        Dim value = ddlRequerimientoDirector.SelectedValue
        Dim tipo As Integer = Integer.Parse(value.Split("|")(0))
        Dim idR As Integer = Integer.Parse(value.Split("|")(1))

        Dim requ As New RequerimientoItem()
        requ.Cantidad = txtMontoChequeImprevistos.Text
        requ.CostoUnitario = 1
        requ.IVA = 16
        requ.Requerimiento = ddlRequerimientoDirector.SelectedItem.Text
        requ.Tipo = tipo
        requ.Id = idR
        requ.Total = requ.Cantidad

        Dim requerimientosLista As String = ""
        For Each row In imprevistos.Where(Function(x) x.Asignado = True)
            requerimientosLista += row.Nombre & ", "
        Next
        requerimientosLista = requerimientosLista.Substring(0, requerimientosLista.Length - 2)
        requ.Observaciones = requerimientosLista
        requerimientos.Add(requ)
        ddlRequerimientoDirector.SelectedIndex = 0
        Session("RequerimientosEvento") = requerimientos

        grdRequerimientos.DataSource = requerimientos.OrderBy(Function(x) x.Tipo).ThenBy(Function(x) x.Tipo).ToList()
        grdRequerimientos.DataBind()
        recalculaPresupuesto()

        ScriptManager.RegisterStartupScript(updRequerimientosInterno, updRequerimientosInterno.GetType(), "cierra_modal_requerimiento", "cierraModalImprevistos();", True)
    End Sub

    'Protected Sub ddlTipoReq_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    pnlRequerimientos.Visible = (ddlTipoReq.SelectedValue = "1")
    '    pnlRequisiciones.Visible = (ddlTipoReq.SelectedValue = "2")
    '    pnlOrdenServicio.Visible = (ddlTipoReq.SelectedValue = "3")
    'End Sub

    Protected Sub btnAgregarRequerimiento_Click(sender As Object, e As EventArgs)

        Dim requerimientos As New List(Of RequerimientoItem)()
        If Session("RequerimientosEvento") IsNot Nothing Then
            requerimientos = Session("RequerimientosEvento")
        End If

        Try
            Dim value = ddlRequerimientoDirector.SelectedValue
            Dim tipo As Integer = Integer.Parse(value.Split("|")(0))
            Dim id As Integer = Integer.Parse(value.Split("|")(1))

            If requerimientos.Where(Function(x) x.Tipo = tipo And x.Id = id).Any() Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar un requerimiento diferente');", True)
                Exit Sub
            End If

            Dim presupuestoActual As Decimal = 0
            Dim presupuestoTotal As Decimal = 0
            Dim dt As DataTable
            Using data As New DB(con.Conectar())

                'carga asignados
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@tipo", tipo),
                    New SqlParameter("@id", id),
                       New SqlParameter("@folio", txtFolio.Text)}
                dt = data.ObtieneDatos("ObtieneInfoRequerimientoDireccion", params).Tables(0)

                Dim dtPres = data.ObtieneDatos("ObtienePresupuestoComprometido", New SqlParameter() {New SqlParameter("@folio", txtFolio.Text),
                                  New SqlParameter("@id", id),
                                  New SqlParameter("@tipo", tipo)}).Tables(0)
                presupuestoActual = dtPres.Rows(0)("Comprometido")
                presupuestoTotal = dtPres.Rows(0)("Presupuesto")
            End Using


            ''VALIDA QUE NO SE PASE DE EL PRESUPUESTO
            'Dim p As Decimal = (dt.Rows(0)("costoUnitario") * txtCantidad.Text)

            ''DESGLOSA EL PRESUPUESTO
            'Dim lista As New List(Of PrespupuestoDetalle)()
            'Try
            '    lista = Session("dtDesglocePresupuestoE")
            'Catch ex As Exception

            'End Try

            'Dim contrato As String = dt.Rows(0)("codigo_contrato") + " - " + dt.Rows(0)("nombre_contrato")
            'Dim currentP As Decimal = lista.FirstOrDefault(Function(x) x.Contrato = contrato).Comprometido
            'If (currentP + p) > presupuestoTotal Then
            '    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "alert", String.Format("muestraMensajePresupuestoMayor('{0}');", contrato), True)
            '    Return
            'End If

            Dim r As New RequerimientoItem()
            r.Id = id
            r.HoraInstalación = txtHoraInstalacion.Text
            Try
                r.FechaInstalación = DateTime.Parse(txtFechaInstalacion.Text)
            Catch ex As Exception

            End Try

            r.IVA = dt.Rows(0)("iva")
            r.Cantidad = txtCantidad.Text
            r.Observaciones = txtObservaciones.Text
            r.Requerimiento = ddlRequerimientoDirector.SelectedItem.Text
            r.CostoUnitario = dt.Rows(0)("costoUnitario")
            r.Tipo = tipo
            If r.IVA > 0 Then
                r.Total = (r.CostoUnitario * r.Cantidad) * (1 + (r.IVA / 100))
            Else
                r.Total = r.CostoUnitario * r.Cantidad
            End If
            requerimientos.Add(r)

            Session("RequerimientosEvento") = requerimientos

            '
            txtCantidad.Text = ""
            txtHoraInstalacion.Text = ""
            txtFechaInstalacion.Text = ""
            ddlRequerimientoDirector.SelectedIndex = 0
            txtObservaciones.Text = ""

        Catch ex As Exception

        Finally
            grdRequerimientos.DataSource = requerimientos.OrderBy(Function(x) x.Tipo).ThenBy(Function(x) x.Tipo).ToList()
            grdRequerimientos.DataBind()
            recalculaPresupuesto()
            'grdRequerimientos.DataMember = "RequerimientoItem"
        End Try
    End Sub
    Protected Sub lnkEliminar_Command(sender As Object, e As CommandEventArgs)
        If e.CommandName = "delete" Then
            Dim tipo As Integer = e.CommandArgument.ToString().Split("|")(0)
            Dim id As Integer = e.CommandArgument.ToString().Split("|")(1)

            Dim requerimientos As New List(Of RequerimientoItem)()
            If Session("RequerimientosEvento") IsNot Nothing Then
                requerimientos = Session("RequerimientosEvento")
            End If
            For Each row In requerimientos
                If row.Id = id And row.Tipo = tipo Then
                    requerimientos.Remove(row)
                    Exit For
                End If
            Next

            Session("RequerimientosEvento") = requerimientos
            grdRequerimientos.DataSource = requerimientos.OrderBy(Function(x) x.Tipo).ThenBy(Function(x) x.Tipo).ToList()
            grdRequerimientos.DataBind()

            recalculaPresupuesto()
            'Using data As New DB(con.Conectar())

            '    'carga asignados
            '    Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@num_empleado", Session("clave_empl")),
            '          New SqlParameter("@idReq", id),
            '            New SqlParameter("@tipo", tipo),
            '              New SqlParameter("@folioEvento", Me.txtFolio.Text)}
            '    data.EjecutaCommand("EliminaRequerimientoDirector", params)

            'End Using

            'CargaRequerimientosDirector()
        End If
    End Sub
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Dim presidium As Integer
        If Me.CheckMesaPresidium.Checked = False Then
            presidium = 0
        Else
            presidium = 1
        End If


        If Me.txtAforo.Text = "" Then
            Me.txtAforo.Text = "0"


        End If

        Dim requerimientos As New List(Of RequerimientoItem)()
        If Session("RequerimientosEvento") IsNot Nothing Then
            requerimientos = Session("RequerimientosEvento")
        End If

        If requerimientos.Count = 0 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alert('Favor de agregar al menos un requerimiento');", True)
        Else

            'ELIMINA REQUISICIONES
            stry = "delete from [eventos].reg_requerimientos_director WHERE folioEvento = " & Me.txtFolio.Text & ""
            resultado = conexion.sqlcambios(stry)

            'AGREGA LA INFORMACION DE LA REQUESISCION
            Using data As New DB(con.Conectar())

                'carga asignados
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text),
                      New SqlParameter("@presidium", CheckMesaPresidium.Checked),
                        New SqlParameter("@aforoTotal", txtAforo.Text),
                          New SqlParameter("@idCeremonia", drpCeremonia.SelectedValue),
                          New SqlParameter("@presupuesto", 0)}
                data.EjecutaCommand("GuardaRequisicionEvento", params)

            End Using

            'GUARDA LAS REQUESICIONES
            For Each row In requerimientos
                Using data As New DB(con.Conectar())

                    'carga asignados
                    Dim params() As SqlParameter = New SqlParameter() {
                        New SqlParameter("@folioEvento", Me.txtFolio.Text),
                          New SqlParameter("@tipo", row.Tipo),
                            New SqlParameter("@id", row.Id),
                              New SqlParameter("@cantidad", row.Cantidad),
                              New SqlParameter("@observaciones", row.Observaciones),
                              New SqlParameter("@horaInstalacion", row.HoraInstalación),
                              New SqlParameter("@fechaInstalacion", row.FechaInstalación),
                              New SqlParameter("@empleado", Session("clave_empl"))}
                    data.EjecutaCommand("InsertaRequerimientoEvento", params)

                End Using
            Next

            If Me.drpCeremonia.Text <> 0 Then
                'EL EVENTO SI TENDRA MAESTRO DE CEREMONIA ENTONCES, INSERTA EN ENVIA CORREO
                stry = "SELECT  correo FROM eventos.correos_ceremonia WHERE id_ceremonia = " & Me.drpCeremonia.SelectedValue & " "
                tabla = conexion.sqlcon(stry)
                If tabla.Rows.Count < 1 Then
                    'No envia informacion no encotro nada
                Else
                    'formato =  2 (FICHA)
                    Dim correo_ceremonia As String = tabla.Rows(0)("correo")
                    stry = "INSERT INTO [eventos].envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "', '" & Trim(correo_ceremonia) & "',0, null, 2)"
                    resultado = conexion.sqlcambios(stry)
                    If resultado = -1 Then
                    End If
                End If
            End If

            stry = "insert into [eventos].bitacora_movimiento values(" & Session("clave_empl") & ", 'REQUERIMIENTO','Se actualizó la informacion del folio req'," & Trim(folio) & ",getdate())"
            resultado = conexion.sqlcambios(stry)

            'AGREGA LOS REQUERIMIENTOS INTERNOS
            Dim requerimientosInternos As New List(Of RequerimientoInterno)
            If Session("RequerimientosServInternoEvento") IsNot Nothing Then
                requerimientosInternos = Session("RequerimientosServInternoEvento")
            End If

            If requerimientosInternos IsNot Nothing And requerimientosInternos.Count > 0 Then
                Using data As New DB(con.Conectar())

                    'ELIMINA LOS REQUERIMIENTOS ANTERIORES
                    Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text)}
                    data.EjecutaCommand("EliminaRequerimientosInterno", paramsD)

                    For Each row In requerimientosInternos.Where(Function(x) x.Asignado = True)
                        Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text), New SqlParameter("@idReq", row.Id), New SqlParameter("@cantidad", 1)}
                        data.EjecutaCommand("AgregaRequerimientoInternoEvento", params)
                    Next

                End Using
            End If


            'AGREGA LOS IMPREVISTOS
            Dim imprevistos As New List(Of CatalogoImprevistos)
            If Session("RequerimientosImprevistosEvento") IsNot Nothing Then
                imprevistos = Session("RequerimientosImprevistosEvento")
            End If

            If imprevistos IsNot Nothing And imprevistos.Count > 0 Then
                Using data As New DB(con.Conectar())

                    'ELIMINA LOS IMPREVISTOS ANTERIORES
                    Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text)}
                    data.EjecutaCommand("EliminaImprevistos", paramsD)

                    For Each row In imprevistos.Where(Function(x) x.Asignado = True)
                        Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", Me.txtFolio.Text), New SqlParameter("@idImp", row.Id), New SqlParameter("@cantidad", 1)}
                        data.EjecutaCommand("AgregaImprevistosEvento", params)
                    Next

                End Using
            End If



            'VERIFICA SI HAY AFORO, SI EXISTE AFORO SE INSERTA A ENVIA CORREO CON FORMATO 1 (EVENTO)

            If Me.txtAforo.Text >= "1" Then
                'ENVIA CORREO A 'AFORO'
                stry = "SELECT correo FROM [eventos].CORREOS WHERE TIPO = 'AFORO'"
                Dim tRsGen3 As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
                Dim tDrsx3 As System.Data.SqlClient.SqlDataReader
                tDrsx3 = tRsGen3.ExecuteReader()
                Try
                    While tDrsx3.Read
                        Dim correo_aforo As String = tDrsx3(0)
                        stry = "INSERT INTO [eventos].envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "','" & Trim(correo_aforo) & "',0, null, 1)"
                        resultado = conexion.sqlcambios(stry)
                        If resultado = -1 Then
                        End If
                    End While
                Finally
                    tDrsx3.Close()
                End Try
            End If
            '------------------------------------------------------------------------------------------

#Region "GUARDA LOG"
            Helper.GuardaLog(Session("clave_empl"), String.Format("Ha actualizado los Requerimientos del evento con Folio {0}", folio))
#End Region

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito();", True)

            Me.txtFolio.Text = ""
            Me.txtAforo.Text = ""
            Me.txtCantidad.Text = ""
            Me.txtObservaciones.Text = ""
            Me.txtFolio.Enabled = True
            Me.CheckMesaPresidium.Checked = False
            Me.txtFolio.Enabled = True
            Me.txtCantidad.Enabled = False
            Me.drpCeremonia.SelectedIndex = 0
            Me.drpCeremonia.Enabled = False
            Me.txtObservaciones.Enabled = False
            Me.CheckMesaPresidium.Enabled = False
            Me.txtAforo.Enabled = False
            Me.lblNombreEvento.Text = ""
            Me.lblFechaEvento.Text = ""
            Me.lblDescripcionEvento.Text = ""
            Me.btnValidarEvento.Enabled = False
            Me.grdRequerimientos.DataSource = Nothing
            Me.grdRequerimientos.DataBind()
        End If

    End Sub
    Protected Sub ddlRequerimientoDirector_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddlRequerimientoDirector.SelectedValue = "0|1" Then
                Dim folio As Integer = 0
                Try
                    folio = Integer.Parse(txtFolio.Text)
                Catch ex As Exception

                End Try
                CargaInformacionRequerimientoInterno(folio)
                ScriptManager.RegisterStartupScript(updRequerimientosInterno, updRequerimientosInterno.GetType(), "abre_modal_requerimiento", "abreModalRequerimientoInterno();", True)
            ElseIf ddlRequerimientoDirector.SelectedValue = "0|2" Then
                Dim folio As Integer = 0
                Try
                    folio = Integer.Parse(txtFolio.Text)
                Catch ex As Exception

                End Try
                CargaInformacionImprevistos(folio)
                ScriptManager.RegisterStartupScript(updRequerimientosInterno, updRequerimientosInterno.GetType(), "abre_modal_imprevistos", "abreModalImprevistos();", True)
            End If
        Catch ex As Exception
            Dim errorMessage = ex.ToString()
        End Try
    End Sub
    Protected Sub btnValidarEvento_Click(sender As Object, e As EventArgs)
        Dim helper As New IntelipolisEngine.Eventos.EventoHelper()
        helper.AgregaValidacionEvento(txtFolio.Text, Session("clave_empl"), 2)

#Region "GUARDA LOG"
        PMD_WAS.Helper.GuardaLog(Session("clave_empl"), String.Format("Ha Validado el Evento con Folio {0}", folio))
#End Region

        txtFolio_TextChanged(Nothing, Nothing)
    End Sub
    Protected Sub grdRequerimientos_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Dim soloLectura = Request.QueryString("readOnly")
        If Not soloLectura Is Nothing Then
            Try
                'Dim row = grdRequerimientos.Rows(e.Row.RowIndex)
                Dim i As Integer = e.Row.Cells.Count
                e.Row.Cells(9).Visible = False
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class
