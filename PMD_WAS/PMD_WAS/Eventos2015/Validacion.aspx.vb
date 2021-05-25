Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion

Imports System.IO.MemoryStream
Imports System.Text
Imports System.Net
Imports System.Net.Mail

Partial Class Validacion
    Inherits System.Web.UI.Page


    Dim resultado As Integer
    Dim email_secr As String
    Dim todoslosemail As String
    Dim fecha_envio As String
    Dim nombre_evento As String
    Dim desc_evento As String
    Dim calle_evento As String
    Dim col_evento As String
    Dim numext_evento As String
    Dim fecha_evento As String
    Dim hr_evento As String
    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Valida Evento")
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim lnk As New LinkButton
        Dim DatoIX As String = ""
        Dim DAtos2 As String = ""

        lnk = CType(e.CommandSource, LinkButton)

        DatoIX = lnk.Text 'Este TextBox esta en el Modal
        DAtos2 = lnk.CommandArgument.ToString

        If e.CommandName = "alta" Then
            'ALTA
            Response.Redirect("/PlaneacionFinanciera/Eventos2015/ConsultaGeneral.aspx?FolioSS=" & DAtos2)
        ElseIf e.CommandName = "requerimiento" Then
            'REQUERIMIENTOS
            Response.Redirect("/PlaneacionFinanciera/Eventos2015/Requerimientos.aspx?folio=" & DAtos2)
        ElseIf e.CommandName = "ficha" Then
            'FICHA
            Response.Redirect("/PlaneacionFinanciera/Eventos2015/Ficha.aspx?folio=" & DAtos2)
        End If

    End Sub


    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles GridView1.SelectedIndexChanged


        'Dim con As New conexion


        'Dim row As GridViewRow = GridView1.SelectedRow
        '' Dim FolioEvento = CType(row.FindControl("LblFolio"), Label).Text

        'Dim RsGen2 As New System.Data.SqlClient.SqlCommand("SELECT Folio " & _
        '                                                 " ,nombre_evento " & _
        '                                                 " ,fecha " & _
        '                                                   " FROM [eventos].reg_evento  " & _
        '                                                   " WHERE validada= 0", con.Conectar)
        'Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        'Drsx2 = RsGen2.ExecuteReader
        'Drsx2.Read()

        'Try
        '    Me.DropRecurso.SelectedValue = Drsx2(11)
        '    txtArea.Text = Drsx2(0)
        '    txtCia.Text = Drsx2(1)
        '    txtProveedor.Text = Drsx2(2)
        '    txtUsuario.Text = Drsx2(3)

        '    txtArea.Text = Drsx2(2)
        '    txtCia.Text = Drsx2(1)
        '    txtProveedor.Text = Drsx2(3)
        '    txtUsuario.Text = Drsx2(4)

        'Finally
        '    Drsx2.Close()
        'End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("paso") = "1" Then

        Else
            Response.Redirect("~/Password.aspx")
        End If


        If Not IsPostBack = True Then
            'CARGA COMBOS
            Using data As New DB(con.conectar())
                'Carga Administracion
                ddlAdministracion.DataSource = data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdministracion.DataTextField = "Nombr_admon"
                ddlAdministracion.DataValueField = "Cve_admon"
                ddlAdministracion.DataBind()
            End Using

            CargaAnios()

            Session("AnimacionItem") = Nothing
            CargarEventosPorAprobar()

            Dim conn As New conexion()
            'CARGA CATALOGO ANIMACION
            Using data As New DB(conn.Conectar())

                'Carga tipo animacion
                Dim dtT = data.ObtieneDatos("ObtieneTipoAnimacion", Nothing).Tables(0)
                ddlTipoAnimacion.DataSource = dtT
                ddlTipoAnimacion.DataTextField = "nombre"
                ddlTipoAnimacion.DataValueField = "id"
                ddlTipoAnimacion.DataBind()

                'Carga animacion
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@tipo", ddlTipoAnimacion.SelectedValue)}
                Dim dt = data.ObtieneDatos("CargaCatalogoAnimacion", params).Tables(0)
                ddlAnimacion.DataSource = dt
                ddlAnimacion.DataTextField = "nombre"
                ddlAnimacion.DataValueField = "id"
                ddlAnimacion.DataBind()

                'Desgloce de Presupuesto
                Dim con As New Class1
                Dim paramsp() As SqlParameter = New SqlParameter() {New SqlParameter("@anio", ddlAnio.SelectedValue)}
                Dim dtR = data.ObtieneDatos("ObtieneResumenPresupuestoAnual", paramsp).Tables(0)

                lblPresupuestoComprometido.Text = String.Format("{0:c0} MXN", dtR.Rows(0)("presupuestoAutorizado"))
                lblPresupuestoDisponible.Text = String.Format("{0:c0} MXN", dtR.Rows(0)("presupuestoDisponible"))
                lblPresupuestoTotal.Text = String.Format("{0:c0} MXN", dtR.Rows(0)("presupuestoTotal"))

                'Desgloce de Animacion
                Dim dtR2 = data.ObtieneDatos("ObtienePresupuestoAnimacion", Nothing).Tables(0)

                lblPresupuestoAnimacionCompr.Text = String.Format("{0:c0} MXN", dtR2.Rows(0)("Comprometido"))
                lblPresupuestoAnimacionDisponible.Text = String.Format("{0:c0} MXN", dtR2.Rows(0)("Disponible"))
                lblPresupuestoAnimacionTotal.Text = String.Format("{0:c0} MXN", dtR2.Rows(0)("PresupuestoTotal"))

                hdnAnimacionComprometido.Value = dtR2.Rows(0)("Comprometido")
                hdnAnimacionDisponible.Value = dtR2.Rows(0)("Disponible")
                hdnAnimacionTotal.Value = dtR2.Rows(0)("PresupuestoTotal")

            End Using

        End If

    End Sub


    Sub CargarEventosPorAprobar()
        Dim con As New conexion
        Dim stry As String

        stry = "Select folio,nombre_evento,lugar,hr_ini,hr_alcalde,hr_salida,hr_fin,Convert(char, fecha, 103) as fecha,Responsable Secretario,Operador Solicitante,telefono_ope telefono, auditoriaInterna, auditoriaExterna, dbo.fn_ObtienePrespuestoEvento(folio) presupuesto, dbo.fn_ObtienePrespuestoComunicacion(folio) presupuestoComunicacion, CASE WHEN esPresencial = 1 THEN 'Sí' ELSE 'No' END esPresencial from [eventos].reg_evento where validada = 0 And procesado = 1 order by 1"
        Dim da As New SqlDataAdapter(stry, con.Conectar)
        Dim dt As New DataTable()

        Try

            da.Fill(dt)
            Me.GridView1.DataSource = dt
            If dt.Rows.Count = 0 Then
                'no hay nada
                Me.GridView1.DataBind()
            Else
                'si trae
                Me.GridView1.DataBind()
            End If
        Catch ex As Exception
            MsgBox(da.Fill(dt))
        End Try
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Dim con As New conexion
        Dim stry As String
        Dim x As Integer
        Dim y As String
        Dim z As Integer


        y = ""
        x = 0
        z = 0

        Dim FOLIOS_COMPLEMENTAR As New List(Of EventoComplemento)

        If GridView1.Rows.Count > 0 Then

            For Each row As GridViewRow In GridView1.Rows
                'Dim FolioEvento = CType(row.FindControl("LblFolioEvento"), Label).Text()
                Dim FolioEvento = CType(row.FindControl("hdnFolioEvento"), HiddenField).Value()
                Dim Aprobada = CType(row.FindControl("CheckAprobada"), CheckBox).Checked
                Dim NoAprobada = CType(row.FindControl("CheckNoAprobada"), CheckBox).Checked
                Dim AuditoriaInterna = CType(row.FindControl("CheckAuditoriaInterna"), CheckBox).Checked
                Dim AuditoriaExterna = CType(row.FindControl("CheckAuditoriaExterna"), CheckBox).Checked
                'Dim comentario As String = Convert.ToString(GridView1.DataKeys(row.RowIndex).Values("comentario"))
                Dim comentario As String = CType(row.FindControl("txtComentarioNoAprobado"), TextBox).Text()

                Dim Estatus As Integer


                If NoAprobada = "True" And Aprobada = "True" Then
                    'msj de favor de seleccionar solo 1 opcion, "Aprobar   O   No Aprobar"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Validacion_AmbosCheck();", True)
                    Exit Sub
                Else
                    If NoAprobada = "True" Then
                        ' No ha sido aprobada y por default se va a estatus 2
                        Estatus = 2 'NO procede, ha sido validada no procede
                    Else
                        If Aprobada = "False" Then
                            Estatus = 0 ' no ha sido validada, aprobada
                        Else
                            Estatus = 1 ' si ha sido validada, aprobada
                        End If
                    End If
                End If


                If Estatus = 0 Then
                    comentario = ""
                End If

                'If Aprobada = "False" Then
                '    Estatus = 0 ' no ha sido validada, aprobada
                'Else
                '    Estatus = 1 ' si ha sido validada, aprobada
                'End If


                '''''''''''''''''''''''''''''''''''''''''''''''''UPDATE

                Dim Rs As SqlDataReader
                If comentario = "" Then
                    stry = "update [eventos].reg_evento set validada='" & Estatus & "', auditoriaInterna='" & AuditoriaInterna & "', auditoriaExterna='" & AuditoriaExterna & "' where Folio='" & FolioEvento & "' "
                Else
                    stry = "update [eventos].reg_evento set validada='" & Estatus & "', comentario = '" & comentario & "', auditoriaInterna='" & AuditoriaInterna & "', auditoriaExterna='" & AuditoriaExterna & "' where Folio='" & FolioEvento & "' "
                End If

                Dim cmd As New Data.SqlClient.SqlCommand(stry, con.Conectar())
                Rs = cmd.ExecuteReader()
                Rs.Read()

                If Estatus = 1 Then
                    stry = "insert into [eventos].bitacora_movimiento values(" & Session("clave_empl") & ", 'VALIDACION','Se actualizó como aprobada el folio de evento','" & FolioEvento & "',getdate())"
                    resultado = conexion.sqlcambios(stry)
                End If

                If Estatus = 2 Then
                    stry = "insert into [eventos].bitacora_movimiento values(" & Session("clave_empl") & ", 'VALIDACION','Se actualizó como no aprobada el folio de evento','" & FolioEvento & "',getdate())"
                    resultado = conexion.sqlcambios(stry)
                End If

#Region "GUARDA LOG"
                Helper.GuardaLog(Session("clave_empl"), String.Format("Ha {0} el evento con folio {0}", If(Aprobada, "Aprobado", "Rechazado"), FolioEvento))
#End Region

                If Aprobada Then
                    'OBTIENE LA INFORMACION DE EL EVENTO
                    Using data As New DB(con.Conectar())
                        Dim params() As SqlParameter = New SqlParameter() _
                        {
                            New SqlParameter("@folio", FolioEvento)
                        }
                        Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params).Tables(0)
                        Dim idSecretaria = dt.Rows(0)("id_secr")
                        Dim idDireccion = dt.Rows(0)("id_depe")
                        Dim fecha = dt.Rows(0)("Fecha")
                        Dim hora = dt.Rows(0)("hr_ini")

                        'REGISTRA LA NOTIFICACION
                        stry = "INSERT INTO [pmd].Tbl_Notificaciones VALUES(" & idSecretaria & "," & idDireccion & ",'','" & String.Format("Se creó el evento Evento para la fecha {0} a las {1}. ", DateTime.Parse(fecha).ToString("dd/MM/yyyy"), hora) & "','" & DateTime.Parse(fecha).ToString("yyyyMMdd hh:mm:ss") & "','" & String.Format("https://admin.sanicolas.gob.mx/PlaneacionFinanciera/EventoDetalle.aspx?id={0}", FolioEvento) & "',0)"
                        conexion.sqlcambios(stry)

                        'ACTUALIZA EL PRESUPUESTO APROBADO
                        Dim params2() As SqlParameter = New SqlParameter() _
                       {
                           New SqlParameter("@folio", FolioEvento)
                       }
                        data.EjecutaCommand("ActualizarPresupuesto", params2)
                    End Using

                    Dim eventoComplemento = New EventoComplemento()
                    eventoComplemento.Folio = FolioEvento

                    FOLIOS_COMPLEMENTAR.Add(eventoComplemento)

                    'ENVIA LOS CORREOS DE AUDITORIA
                    If AuditoriaExterna Then
                        CorreoAuditoriaExterna(FolioEvento)
                    End If

                    If AuditoriaInterna Then
                        CorreoAuditoriaInterna(FolioEvento)
                    End If

                    'SI HAY COMENTARIOS ENVIA CORREOS
                    Dim eventosHelper As New IntelipolisEngine.Eventos.EventoHelper()
                    If Not String.IsNullOrEmpty(comentario) Then
                        eventosHelper.AgregaValidacionEvento(FolioEvento, Session("clave_empl"), 5)
                        EnviaCorreoComentariosAlcalde(FolioEvento, comentario)
                    Else
                        eventosHelper.AgregaValidacionEvento(FolioEvento, Session("clave_empl"), 4)
                    End If
                End If


            Next

            ''y = Left(y, Len(y) - 1) ' 
            'MUESTRA EL MODAL DE COMPLEMENTAR INFORMACION
            If FOLIOS_COMPLEMENTAR.Count > 0 Then
                hdnFolioActual.Value = 0
                Session("FoliosEventosArray") = FOLIOS_COMPLEMENTAR.ToArray()
                Dim foliosEventoArray() As EventoComplemento = Session("FoliosEventosArray")
                Dim index As Integer = hdnFolioActual.Value
                Dim folio = foliosEventoArray(index)

                CargaInfoEventoAnimacion(folio)

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "muestraModalComplementarInformacion();", True)
            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Validacion_Exito();", True)
            End If

            '            Sexy.alert("<b>Aprobada con éxito!!</b>");
            CargarEventosPorAprobar()

        End If
    End Sub

    Protected Sub btnGuardarComplemento_Click(sender As Object, e As EventArgs)
        'Try

        '    For Each row As RepeaterItem In rptComplemento.Items
        '        Dim folio = CType(row.FindControl("hdnFolioEvento"), HiddenField).Value

        '    Next

        'Catch ex As Exception

        'End Try
    End Sub

    Public Class AnimacionItem
        Public Property Folio As Integer
        Public Property IdAnimacion As Integer
        Public Property Animacion As String
        Public Property Cantidad As Integer
        Public Property TotalUnitario As Decimal
        Public Property Total As Decimal
    End Class

    Public Class EventoComplemento
        Public Property Folio As Integer
        Public Property Asistire As Boolean
        Public Property Animacion As List(Of AnimacionItem)
        Public Property FechaIni As DateTime
        Public Property FechaFin As DateTime
    End Class

    Protected Sub lnqAgregarAnimacion_Command(sender As Object, e As CommandEventArgs)
        Try

            Dim foliosEventoArray() As EventoComplemento = Session("FoliosEventosArray")
            Dim index As Integer = hdnFolioActual.Value
            Dim folio As EventoComplemento = foliosEventoArray(index)

            'Dim folio As New EventoComplemento
            'folio.Folio = 1425

            Dim idAnimacion As Integer = Integer.Parse(ddlAnimacion.SelectedValue)

            Dim animaciones As New List(Of AnimacionItem)
            If Session("AnimacionItem") IsNot Nothing Then
                animaciones = CType(Session("AnimacionItem"), List(Of AnimacionItem))
            End If

            If animaciones.Where(Function(x) x.Folio = folio.Folio And x.IdAnimacion = idAnimacion).Any() Then
                Return
            End If

            Dim total As Decimal
            Dim conn As New conexion
            Using data As New DB(conn.Conectar())
                Dim params() As SqlParameter = New SqlParameter() _
          {
              New SqlParameter("@idAnimacion", idAnimacion)
          }
                Dim dt = data.ObtieneDatos("ObtieneInfoAnimacion", params).Tables(0)
                total = dt.Rows(0)("total")
            End Using

            Dim nuevaAnimacion As New AnimacionItem()
            nuevaAnimacion.Folio = folio.Folio
            nuevaAnimacion.IdAnimacion = idAnimacion
            nuevaAnimacion.Animacion = ddlAnimacion.SelectedItem.Text
            nuevaAnimacion.Cantidad = txtCantidad.Text
            nuevaAnimacion.TotalUnitario = total
            nuevaAnimacion.Total = nuevaAnimacion.Cantidad * total
            animaciones.Add(nuevaAnimacion)

            Dim a = animaciones.Where(Function(x) x.Folio = folio.Folio).ToList()
            grdAnimacion.DataSource = a
            grdAnimacion.DataBind()

            folio.Animacion = a
            Session("AnimacionItem") = animaciones

            'ACTUALIZAR PRESUPUESTOS
            RecalculaPresupuestoAnimacion()

            txtCantidad.Text = ""
            ddlAnimacion.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RecalculaPresupuestoAnimacion()
        Dim animaciones As New List(Of AnimacionItem)
        If Session("AnimacionItem") IsNot Nothing Then
            animaciones = CType(Session("AnimacionItem"), List(Of AnimacionItem))
        End If

        Dim total As Decimal,
            comprometido As Decimal,
            disponible As Decimal

        total = hdnAnimacionTotal.Value
        comprometido = hdnAnimacionComprometido.Value
        disponible = hdnAnimacionTotal.Value

        For Each a In animaciones
            comprometido = comprometido + (a.Cantidad * a.TotalUnitario)
        Next

        disponible = total - comprometido
        lblPresupuestoAnimacionCompr.Text = String.Format("{0:c0} MXN", comprometido)
        lblPresupuestoAnimacionDisponible.Text = String.Format("{0:c0} MXN", disponible)
    End Sub

    Protected Sub rptComplemento_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.AlternatingItem.Item Then
            Dim folio = Integer.Parse(CType(e.Item.FindControl("hdnFolioEvento"), HiddenField).Value)
            Dim ddl = CType(e.Item.FindControl("ddlAnimacion"), DropDownList)
            Dim chk = CType(e.Item.FindControl("chkAsisteAlcalde"), CheckBox)
            Dim conn As New conexion()
            Using data As New DB(conn.Conectar())

                Dim dt = data.ObtieneDatos("CargaCatalogoAnimacion", Nothing).Tables(0)
                ddl.DataSource = dt
                ddl.DataTextField = "nombre"
                ddl.DataValueField = "id"
                ddl.DataBind()

            End Using

            Dim animaciones As New List(Of AnimacionItem)
            If Session("AnimacionItem") IsNot Nothing Then
                animaciones = CType(Session("AnimacionItem"), List(Of AnimacionItem))
            End If

            Dim gv = CType(e.Item.FindControl("grdAnimacion"), GridView)
            Dim a = animaciones.Where(Function(x) x.Folio = folio).ToList()
            gv.DataSource = a
            gv.DataBind()
        End If
    End Sub

    Protected Sub grdAnimacion_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim tabfila As New List(Of AnimacionItem)
        tabfila = Session("AnimacionItem")
        Dim grid = CType(sender, GridView)
        Dim row = grid.Rows(e.RowIndex)
        Dim index As Integer = -1
        Dim idAnimacion As Integer = CType(row.FindControl("hdnId"), HiddenField).Value
        Dim folio = CType(row.FindControl("hdnFolioEvento"), HiddenField).Value

        Dim animacino = tabfila.Where(Function(x) x.Folio = folio And x.IdAnimacion = idAnimacion).FirstOrDefault()
        tabfila.Remove(animacino)
        grid.DataSource = tabfila.Where(Function(x) x.Folio = folio)
        grid.DataBind()
        Session("AnimacionItem") = tabfila

        RecalculaPresupuestoAnimacion()
        'index = Buscar_Indice(HttpUtility.HtmlDecode(GridView1.Rows(e.RowIndex).Cells(2).Text), tabfila)
        'If index <> -1 Then
        '    tabfila.Rows.RemoveAt(index)
        '    GridView1.DataSource = tabfila
        '    GridView1.DataBind()
        '    Session("tabses1") = tabfila

        'End If
    End Sub

    Protected Sub btnTestCorreo_Click(sender As Object, e As EventArgs)
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params2() As SqlParameter = New SqlParameter() _
         {
             New SqlParameter("@folio", 1399)
         }

            Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params2).Tables(0)
            Dim sb As New StringBuilder()
            sb.AppendLine("<h2>Nuevo evento programado</h2>")
            sb.AppendLine(String.Format("<h4>{0}</h4>", "Se ha programado un evento donde se solicita Auditoria Interna."))
            sb.AppendLine("<p>Información de el evento</p>")
            sb.AppendLine(String.Format("<p><b>Nombre evento:</b> {0}</p>", dt.Rows(0)("Nombre")))
            sb.AppendLine(String.Format("<p><b>Fecha:</b> {0} {1}</p>", DateTime.Parse(dt.Rows(0)("Fecha")).ToString("dd-MMMM-yyyy"), dt.Rows(0)("hr_ini")))

            Dim helper As New Helper
            helper.EnviaCorreo("ricardo.eslava89@gmail.com", "Evento con Auditoria Interna", sb.ToString())

        End Using

    End Sub

    Private Sub CorreoAuditoriaInterna(folio As Integer)
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params2() As SqlParameter = New SqlParameter() _
         {
             New SqlParameter("@folio", folio)
         }

            Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params2).Tables(0)
            Dim sb As New StringBuilder()
            sb.AppendLine("<h2>Nuevo evento programado</h2>")
            sb.AppendLine(String.Format("<h4>{0}</h4>", "Se ha programado un evento donde se solicita Auditoria Interna."))
            sb.AppendLine("<p>Información de el evento</p>")
            sb.AppendLine(String.Format("<p><b>Nombre evento:</b> {0}</p>", dt.Rows(0)("Nombre")))
            sb.AppendLine(String.Format("<p><b>Fecha:</b> {0} {1}</p>", DateTime.Parse(dt.Rows(0)("Fecha")).ToString("dd-MMMM-yyyy"), dt.Rows(0)("hr_ini")))

            Dim helper As New Helper
            helper.EnviaCorreo("ricardo.eslava89@gmail.com", "Evento con Auditoria Interna", sb.ToString())

        End Using
    End Sub

    Private Sub EnviaCorreoComentariosAlcalde(folio As Integer, comentarios As String)
        Try

            Dim con As New Class1
            Using data As New DB(con.conectar())
                Dim params2() As SqlParameter = New SqlParameter() _
             {
                 New SqlParameter("@folio", folio)
             }

                Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params2).Tables(0)
                Dim sb As New StringBuilder()
                sb.AppendLine("<h2>Evento aprobado con comentarios</h2>")
                sb.AppendLine(String.Format("<h4>Se ha aprobado el evento con Comentarios de parte del Alcalde:</h4><br />."))
                sb.AppendLine("<p>Información de el evento</p>")
                sb.AppendLine(String.Format("<p><b>Nombre evento:</b> {0}</p>", dt.Rows(0)("Nombre")))
                sb.AppendLine(String.Format("<p><b>Fecha:</b> {0} {1}</p>", DateTime.Parse(dt.Rows(0)("Fecha")).ToString("dd-MMMM-yyyy"), dt.Rows(0)("hr_ini")))
                sb.AppendLine(String.Format("<p><b>Comentarios de el Alcalde:</b></p><br /><h4>{0}</h4>", comentarios))

                '
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", folio)}
                Dim dt2 = data.ObtieneDatos("ObtieneCorreoEnlaceAdministrativo", params).Tables(0)

                Dim helper As New Helper
                helper.EnviaCorreo(dt2.Rows(0)("email"), "Evento con Auditoria Interna", sb.ToString())

                'INSERTA NOTIFICACIONES
                Dim params3() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", folio)}
                Dim dt3 = data.ObtieneDatos("ObtieneEmpleadosEnlaces", params).Tables(0)
                Using intel As New IntelipolisEngine.Helper.NotificationHelper()
                    For Each row In dt3.Rows
                        intel.InsertaNotificacion("", "", row("clave_empl"), sb.ToString(), "")
                    Next
                End Using

            End Using
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CorreoAuditoriaExterna(folio As Integer)
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params2() As SqlParameter = New SqlParameter() _
         {
             New SqlParameter("@folio", folio)
         }

            Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params2).Tables(0)
            Dim sb As New StringBuilder()
            sb.AppendLine("<h2>Nuevo evento programado</h2>")
            sb.AppendLine(String.Format("<h4>{0}</h4>", "Se ha programado un evento donde se solicita Auditoria Interna."))
            sb.AppendLine("<p>Información de el evento</p>")
            sb.AppendLine(String.Format("<p><b>Nombre evento:</b> {0}</p>", dt.Rows(0)("Nombre")))
            sb.AppendLine(String.Format("<p><b>Fecha:</b> {0} {1}</p>", DateTime.Parse(dt.Rows(0)("Fecha")).ToString("dd-MMMM-yyyy"), dt.Rows(0)("hr_ini")))

            Dim helper As New Helper
            helper.EnviaCorreo("ricardo.eslava89@gmail.com", "Evento con Auditoria Externa", sb.ToString())

        End Using
    End Sub

    Private Sub CargaInfoEventoAnimacion(evento As EventoComplemento)

        'Carga el evento
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", evento.Folio)}
            Dim dt = data.ObtieneDatos("ObtieneEventoPorFolio", params).Tables(0)
            lblNombreEvento.Text = String.Format("Nombre: {0}", dt.Rows(0)("Nombre"))
            lblFechaEvento.Text = String.Format("Fecha: {0}", DateTime.Parse(dt.Rows(0)("Fecha")).ToString("ddd dd MMM yyyy"))
            lblHoraEvento.Text = String.Format("{0} - {1}", dt.Rows(0)("hr_ini"), dt.Rows(0)("hr_fin"))
            lblDireccion.Text = dt.Rows(0)("lugar")
            evento.FechaIni = DateTime.Parse(DateTime.Parse(dt.Rows(0)("Fecha")).ToString("yyyy-MM-dd") & " " & dt.Rows(0)("hr_ini"))
            evento.FechaFin = DateTime.Parse(DateTime.Parse(dt.Rows(0)("Fecha")).ToString("yyyy-MM-dd") & " " & dt.Rows(0)("hr_fin"))
        End Using

        Dim animaciones As New List(Of AnimacionItem)
        If Session("AnimacionItem") IsNot Nothing Then
            animaciones = CType(Session("AnimacionItem"), List(Of AnimacionItem))
        End If

        chkConfirmaAsistenciaAlcalde.Checked = evento.Asistire

        Dim a = animaciones.Where(Function(x) x.Folio = evento.Folio).ToList()
        grdAnimacion.DataSource = a
        grdAnimacion.DataBind()

        '
        Dim index As Integer = hdnFolioActual.Value
        Dim foliosEventoArray() As EventoComplemento = Session("FoliosEventosArray")
        lblEventoCount.Text = String.Format("Evento {0} de {1}.", index + 1, foliosEventoArray.Count())

        btnAnterior.Visible = index > 0
        If (index = foliosEventoArray.Count - 1) Then
            btnSiguiente.Text = "TERMINAR"
        Else
            btnSiguiente.Text = "Siguiente"
        End If

    End Sub

    Protected Sub btnAnterior_Click(sender As Object, e As EventArgs)
        Dim index As Integer = hdnFolioActual.Value
        index = index - 1
        Dim foliosEventoArray() As EventoComplemento = Session("FoliosEventosArray")
        Dim folio = foliosEventoArray(index)
        hdnFolioActual.Value = (index)
        CargaInfoEventoAnimacion(folio)
    End Sub

    Protected Sub btnSiguiente_Click(sender As Object, e As EventArgs)
        Dim index As Integer = hdnFolioActual.Value

        Dim foliosEventoArray() As EventoComplemento = Session("FoliosEventosArray")
        'GUARDA LO ANTERIOR
        foliosEventoArray(index).Asistire = chkConfirmaAsistenciaAlcalde.Checked

        If (index = foliosEventoArray.Count - 1) Then
            'GUARDA TODO
            'Carga el evento
            Dim con As New Class1
            Using data As New DB(con.conectar())
                For Each evento As EventoComplemento In foliosEventoArray
                    'ACTUALIZA EVENTO
                    Dim paramsE() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", evento.Folio), New SqlParameter("@asistira", evento.Asistire)}
                    data.EjecutaCommand("ActualizaEvento", paramsE)

                    If evento.Asistire = True Then
                        'ENVIA CORREO AL ALCALDE
                        Dim helper As New Helper
                        helper.EnviaCoreoCalendario(VariablesGlobales.CORREO_ALCALDE, "Nuevo evento programado", lblNombreEvento.Text, evento.FechaIni, evento.FechaFin, "Se ha programado el siguiente evento.", lblDireccion.Text)
                    End If

                    'GUARDA ANIMACIONES
                    'Dim animacionesEvento = evento.Animacion.Where(Function(x) x.Folio = evento.Folio).ToList()
                    If evento.Animacion IsNot Nothing Then
                        For Each animacion In evento.Animacion
                            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio", evento.Folio), New SqlParameter("@idAnimacion", animacion.IdAnimacion), New SqlParameter("@cantidad", animacion.Cantidad)}
                            data.EjecutaCommand("InsertaAnimacionEvento", params)
                        Next
                    End If
                Next
            End Using

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "Complemento_Exitoso();", True)

        Else

            'MUESTRA SIGUIENTE
            index = index + 1
            Dim folio = foliosEventoArray(index)
            hdnFolioActual.Value = index
            CargaInfoEventoAnimacion(folio)
        End If
    End Sub

    Protected Sub ddlTipoAnimacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        'CARGA CATALOGO ANIMACION
        Dim conn As New conexion()
        Using data As New DB(conn.Conectar())

            'Carga animacion
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@tipo", ddlTipoAnimacion.SelectedValue)
        }
            Dim dt = data.ObtieneDatos("CargaCatalogoAnimacion", params).Tables(0)
            ddlAnimacion.DataSource = dt
            ddlAnimacion.DataTextField = "nombre"
            ddlAnimacion.DataValueField = "id"
            ddlAnimacion.DataBind()

        End Using
    End Sub
    Protected Sub lnkEliminar_Command(sender As Object, e As CommandEventArgs)
        Dim animaciones As New List(Of AnimacionItem)
        If Session("AnimacionItem") IsNot Nothing Then
            animaciones = CType(Session("AnimacionItem"), List(Of AnimacionItem))
        End If


    End Sub

    Protected Sub ddlAdministracion_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaAnios()
    End Sub

    Private Sub CargaAnios()
        'CARGA COMBOS
        Using data As New DB(con.conectar())
            'Carga añios
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@idAdmon", ddlAdministracion.SelectedValue)}

            ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params)
            ddlAnio.DataTextField = "Año"
            ddlAnio.DataValueField = "Año"
            ddlAnio.DataBind()
        End Using

        Try
            ddlAnio.SelectedValue = Request.Cookies("pmd_anio").Value
        Catch ex As Exception
            ddlAnio.SelectedValue = Date.Now.Year
        End Try
    End Sub
End Class
