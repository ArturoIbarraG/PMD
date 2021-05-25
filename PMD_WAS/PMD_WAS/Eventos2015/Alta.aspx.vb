Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion
Imports IntelipolisEngine.Helper

Partial Class frm_mapa
    Inherits System.Web.UI.Page
    Dim stry As String
    Dim folio As Integer
    Dim tabla As DataTable
    Dim clave_secr As Integer
    'Dim clave_colo As Integer
    Dim IDCOL As Integer
    Dim IDSECRE As Integer
    Dim clave_calle As Integer
    Dim resultado As Integer
    Dim conx As New conexion
    Dim coordx As String
    Dim coordy As String
    Dim fecha_hoy As Date
    Dim fecha_captura As String
    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Alta de evento")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsPostBack = False Then
        '    Me.Label18.Text = Session("NombreEmpl")
        'End If

        If Session("paso") = "1" Then
        Else
            Response.Redirect("~/Password.aspx")
        End If

        If Not IsPostBack = True Then
            Using data As New DB(con.conectar())
                'Carga Administracion
                ddlAdmon.DataSource = Data.ObtieneDatos("ObtieneAdministraciones", Nothing)
                ddlAdmon.DataTextField = "Nombr_admon"
                ddlAdmon.DataValueField = "Cve_admon"
                ddlAdmon.DataBind()

                ddlAdmon_SelectedIndexChanged(Nothing, Nothing)

                Try
                    ddlAnio.SelectedValue = Request.Cookies("pmd_anio").Value
                Catch ex As Exception
                End Try
            End Using

            'llenaCBO()
            Dim ColoEnviada As String
            Dim IDCOL As Integer

            ColoEnviada = Request.QueryString("IDCOL")
            IDCOL = Session("IDCOL")
            ColoEnviada = IDCOL
            If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)

            DibujarCol()


            usuario()
            Me.Label19.Text = Session("clave_empl")
            inicia()
            habilitar()
            cargaCombos()


            Me.txtfechaIni.Text = Format(Now, "yyyy-MM-dd")
            Me.txtfechaFin.Text = Format(Now, "yyyy-MM-dd")

            AplicaPermisos()

            'MUESTRA LA SECRETARIA SI LA MANDAN
            Try
                'SECRETARIA
                Dim secretaria = Request.QueryString("secretaria")
                If Not secretaria Is Nothing Then
                    ddlSecretaria.SelectedValue = secretaria
                    ddlSecretaria_SelectedIndexChanged(Nothing, Nothing)
                End If


                'DIRECCION 
                Dim direccion = Request.QueryString("direccion")
                If Not direccion Is Nothing Then
                    ddlDireccion.SelectedValue = direccion
                    ddlDireccion_SelectedIndexChanged(Nothing, Nothing)
                End If

                'ACTIVIDAD
                Dim actividad = Request.QueryString("actividad")
                If Not actividad Is Nothing Then
                    ddlActividad.SelectedValue = actividad
                    ddlActividad_SelectedIndexChanged(Nothing, Nothing)
                End If

                'SUBACTIVIDAD
                Dim subactividad = Request.QueryString("subactividad")
                If Not subactividad Is Nothing Then
                    ddlSubActividad.SelectedValue = subactividad
                End If

            Catch ex As Exception

            End Try
        End If
        'TextBox3.Text = Request.QueryString("coordx")

    End Sub

    Private Sub AplicaPermisos()
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.Enabled = True
            Dim dt = data.ObtieneDatos("ObtieneEmpleado", params).Tables(0)

            Try
                ddlSecretaria.SelectedValue = dt.Rows(0)("IdSecretaria")

                If Session("Puesto") = "4" Then
                    ddlSecretaria.Enabled = True
                Else
                    ddlSecretaria.Enabled = False
                End If

            Catch ex As Exception

            End Try


            Session("IDSECRE") = ddlSecretaria.SelectedValue

            ddlSecretaria_SelectedIndexChanged(Nothing, Nothing)
            'CargarDirecciones()
            'Si es director puede moverse entre direcciones
            If dt.Rows(0)("IdPuesto") <> 1 Then
                ddlDireccion.Enabled = False
            End If

            Try
                ddlDireccion.SelectedValue = dt.Rows(0)("IdDireccion")
            Catch ex As Exception

            End Try

            If Session("Puesto") = "4" Then
                ddlDireccion.Enabled = True
            End If

        End Using

        ddlDireccion_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Function Laborales(FInicio As String, FFin As String) As Integer



        Dim cantdias As Integer
        Dim diashabiles As Integer
        Dim DiaSemana As Integer
        Dim diasfestivos As Integer


        cantdias = (DateDiff("D", FInicio, FFin))
        diashabiles = 0


        For i = 0 To cantdias
            DiaSemana = Weekday(DateAdd("D", i, FInicio))
            If DiaSemana <> 1 And DiaSemana <> 7 Then diashabiles = diashabiles + 1
        Next
        Laborales = diashabiles


        stry = "select count(fecha) as dias from [eventos].dias_festivos where fecha between '" & FInicio & "' and '" & FFin & "'"
        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, conx.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader()

        Try
            While tDrsx.Read

                diasfestivos = tDrsx(0).ToString
                Laborales = Laborales - diasfestivos
            End While
        Finally
            tDrsx.Close()
        End Try



    End Function



#Region "BOTONES"
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        'If ValidarAlta() = False Then
        '    Me.label13.Visible = True
        '    Exit Sub
        'End If

        If Len(Me.txthrini.Text) < 5 Or Len(Me.txthrfin.Text) < 5 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)
            Exit Sub

        End If


        If Len(Trim(Me.txtfechaIni.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub
        End If

        If Len(Trim(Me.txtfechaFin.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub
        End If

        If Me.txtTelefonoResponsable.Text = "" Then
            Me.txtTelefonoResponsable.Text = 0
        End If


        Dim alcalde As Integer
        If Me.CheckAlcalde.Checked = True Then
            alcalde = 1
        Else
            alcalde = 0
        End If

        Dim prensa As Integer
        If Me.CheckPrensa.Checked = True Then
            prensa = 1
        Else
            prensa = 0
        End If

        Dim presencial As Integer
        If Me.chkEsPresencial.Checked = True Then
            presencial = 1
        Else
            presencial = 0
        End If

        Dim limpiar As Integer
        If Me.CheckLimpieza.Checked = True Then
            limpiar = 1
        Else
            limpiar = 0
        End If

        If Session("UsuarioAdmin") <> 1234 Then


            stry = "select CONVERT (char, GETDATE(),111) as fecha_captura"

            tabla = conexion.sqlcon(stry)
            If tabla.Rows.Count < 1 Then
            Else
                fecha_captura = tabla.Rows(0)("fecha_captura")
                fecha_captura = Trim(fecha_captura)
            End If

            Dim diashabiles As Integer
            diashabiles = 0
            diashabiles = Laborales(fecha_captura, Me.txtfechaIni.Text)

            Dim diasEspera = VariablesGlobales.DIAS_ESPERA_EVENTO
            If diashabiles > diasEspera Then

                'GUARDA EVENTO

                If Me.CheckAlcalde.Checked = True Then
                    If Len(Me.txthrAlcalde.Text) < 5 Or Len(Me.txtHrSalidaAlcalde.Text) < 5 Then
                        'alerta_Cambio_Error_HoraAlcalde
                        'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('HORA ALCALDE:Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_HoraAlcalde();", True)
                        Exit Sub
                    End If
                Else
                    Me.txthrAlcalde.Text = ""
                    Me.txtHrSalidaAlcalde.Text = ""
                End If




                IDCOL = Session("IDCOL")
                coordx = Session("coordx")
                coordy = Session("coordy")

                '---------------------------INSERTA EL REGISTRO
                creaFolio()
                folio = Session("folio")

                'If alcalde <> 0 Then

                'Es necesario que se Validada por Relaciones Públicas
                stry = "INSERT INTO [eventos].reg_evento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtEvento.Text) & "'," & Trim(Me.ddlSecretaria.Text) & "," & Trim(Me.ddlDireccion.Text) & "," & Trim(Me.ddlTipoEvento.Text) & ",'" & Trim(Me.txtDescripcion.Text) & "'," & Trim(Me.ddlColonia.Text) & "," & Trim(Me.ddlCalle.Text) & ",'" & Trim(Me.txtext.Text) & "','" & Trim(UCase(Me.txtint.Text)) & "','" & Trim(Me.txtfechaIni.Text) & "','" & Trim(Me.txthrini.Text) & "','" & Trim(Me.txthrfin.Text) & "'," & Trim(Me.txtnumbenef.Text) & "," & Trim(alcalde) & ",'" & Trim(Me.txthrAlcalde.Text) & "'," & Trim(prensa) & "," & Trim(limpiar) & ",'" & Trim(Me.txtResponsableEvento.Text) & "'," & Trim(Me.txtTelefonoResponsable.Text) & ",'" & Trim(Me.ddlEmpleadoOperador.SelectedItem.Text) & "','" & Trim(Me.ddlPuestoOperador.SelectedItem.Text) & "'," & Trim(Me.txtTelefonoOperador.Text) & ",'" & Trim(Me.txtCorreoOperador.Text) & "',getdate()," & Session("clave_empl") & ",'" & Trim(Me.txtLugar.Text) & "','" & Trim(Me.txtHrSalidaAlcalde.Text) & "',NULL,0,NULL,NULL,NULL," & Trim(Me.txtAsistentes.Text) & ",0,0,0,'" & Trim(Me.txtfechaFin.Text) & "'," + Trim(presencial) + "," + ddlAdmon.SelectedValue + "," + ddlAnio.SelectedValue + ")"
                'Else

                '    'No es necesario que sea Validada
                '    stry = "INSERT INTO [eventos].reg_evento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtEvento.Text) & "'," & Trim(Me.ddlSecretaria.Text) & "," & Trim(Me.ddlDireccion.Text) & "," & Trim(Me.ddlTipoEvento.Text) & ",'" & Trim(Me.txtDescripcion.Text) & "'," & Trim(Me.ddlColonia.Text) & "," & Trim(Me.ddlCalle.Text) & ",'" & Trim(Me.txtext.Text) & "','" & Trim(UCase(Me.txtint.Text)) & "','" & Trim(Me.txtfecha.Text) & "','" & Trim(Me.txthrini.Text) & "','" & Trim(Me.txthrfin.Text) & "'," & Trim(Me.txtnumbenef.Text) & "," & Trim(alcalde) & ",'" & Trim(Me.txthrAlcalde.Text) & "'," & Trim(prensa) & "," & Trim(limpiar) & ",'" & Trim(Me.txtResponsableEvento.Text) & "'," & Trim(Me.txtTelefonoResponsable.Text) & ",'" & Trim(Me.txtOperadopor.Text) & "','" & Trim(Me.txtPuestoOperador.Text) & "'," & Trim(Me.txtTelefonoOperador.Text) & ",'" & Trim(Me.txtCorreoOperador.Text) & "',getdate()," & Session("clave_empl") & ",'" & Trim(Me.txtLugar.Text) & "','" & Trim(Me.txtHrSalidaAlcalde.Text) & "',NULL,1)"
                'End If

                'INSERTA EL REGISTRO DE ACTIVIDAD Y SUB ACTIVIDAD
                Dim con As New Class1
                Using data As New DB(con.conectar())
                    Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@Folio", folio),
                    New SqlParameter("@IdSubActividad", ddlSubActividad.SelectedValue)
                }

                    data.EjecutaCommand("AsignaSubActividadEvento", params)

                End Using

                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                    'ERROR
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                    Exit Sub
                Else
                    '---------------------INSERTA PUNTO ----------------------------------
                    stry = "INSERT INTO [eventos].map_evento VALUES (" & Trim(folio) & "," & Trim(coordx) & "," & Trim(coordy) & "," & Trim(IDCOL) & ")"
                    resultado = conexion.sqlcambios(stry)
                    If resultado = -1 Then
                        'NO GUARDA NINGUN REGISTRO 
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_PuntoMapa();", True)
                    Else
                    End If
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito('" & folio & "');", True)

                    'MANDA ALERTAS DE CORREOS Y NOTIFICACIONES A LOS DEPARTAMENTOS DE EVENTOS Y COMUNICACIONES
                    Using notiHelper As New IntelipolisEngine.Helper.NotificationHelper()

                        'Manda alerta a comunicacion
                        notiHelper.EnviaNotificacionNuevoEventoDepComunicacion(folio)

                        'Manda alerta a eventos
                        notiHelper.EnviaNotificacionNuevoEventoDepEventos(folio)

                        'Manda alerta a Control Administrativo
                        notiHelper.EnviaNotificacionNuevoEventoDepControlAdministrativo(folio)

                        'Notifica a el usuario el Alta de el Folio
                        notiHelper.NotificaUsuarioEventoCreado(folio, Session("clave_empl"))

                    End Using

#Region "GUARDA LOG"
                    Helper.GuardaLog(Session("clave_empl"), String.Format("Ha agregado un nuevo evento con Folio {0}", folio))
#End Region
                    inicia()
                End If
            Else
                ' NO GUARDA EVENTO
                'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('No se permite guardar el evento, porque no está dentro de los 3 días hábiles permitidos!!')", True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_NoPermiteGuardarEvento();", True)
                Exit Sub

            End If

        Else

            '-----------------------------------------------------------------------------------------------------------
            'Valida que capture una fecha posterior al dia de hoy
            stry = "select CONVERT (char, GETDATE(),111) as fecha_captura"
            tabla = conexion.sqlcon(stry)
            If tabla.Rows.Count < 1 Then
            Else

                fecha_hoy = tabla.Rows(0)("fecha_captura")
                fecha_hoy = Trim(fecha_hoy)
                fecha_captura = Format(fecha_hoy, "yyyy-MM-dd")

            End If

            If Me.txtfechaIni.Text > fecha_captura Then

                'Guarda la informacion ya que es mayor al dia de hoy
                'GUARDA EVENTO
                If Me.CheckAlcalde.Checked = True Then
                    If Len(Me.txthrAlcalde.Text) < 5 Or Len(Me.txtHrSalidaAlcalde.Text) < 5 Then
                        'alerta_Cambio_Error_HoraAlcalde
                        'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('HORA ALCALDE:Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_HoraAlcalde();", True)
                        Exit Sub
                    End If
                Else
                    Me.txthrAlcalde.Text = ""
                    Me.txtHrSalidaAlcalde.Text = ""
                End If


                IDCOL = Session("IDCOL")
                coordx = Session("coordx")
                coordy = Session("coordy")



                '---------------------------INSERTA EL REGISTRO
                creaFolio()
                folio = Session("folio")

                'If alcalde <> 0 Then
                'Es necesario que se Validada por Relaciones Públicas
                stry = "INSERT INTO [eventos].reg_evento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtEvento.Text) & "'," & Trim(Me.ddlSecretaria.Text) & "," & Trim(Me.ddlDireccion.Text) & "," & Trim(Me.ddlTipoEvento.Text) & ",'" & Trim(Me.txtDescripcion.Text) & "'," & Trim(Me.ddlColonia.Text) & "," & Trim(Me.ddlCalle.Text) & ",'" & Trim(Me.txtext.Text) & "','" & Trim(UCase(Me.txtint.Text)) & "','" & Trim(Me.txtfechaIni.Text) & "','" & Trim(Me.txthrini.Text) & "','" & Trim(Me.txthrfin.Text) & "'," & Trim(Me.txtnumbenef.Text) & "," & Trim(alcalde) & ",'" & Trim(Me.txthrAlcalde.Text) & "'," & Trim(prensa) & "," & Trim(limpiar) & ",'" & Trim(Me.txtResponsableEvento.Text) & "'," & Trim(Me.txtTelefonoResponsable.Text) & ",'" & Trim(Me.ddlEmpleadoOperador.SelectedItem.Text) & "','" & Trim(Me.ddlPuestoOperador.SelectedItem.Text) & "'," & Trim(Me.txtTelefonoOperador.Text) & ",'" & Trim(Me.txtCorreoOperador.Text) & "',getdate()," & Session("clave_empl") & ",'" & Trim(Me.txtLugar.Text) & "','" & Trim(Me.txtHrSalidaAlcalde.Text) & "',NULL,0,NULL,NULL,NULL," & Me.txtAsistentes.Text & ",0,0,0'" & Trim(Me.txtfechaFin.Text) & "'," + presencial + "," + ddlAdmon.SelectedValue + "," + ddlAnio.SelectedValue + ")"
                'Else
                '    'No es necesario que sea Validada
                '    stry = "INSERT INTO [eventos].reg_evento VALUES (" & Trim(folio) & ",'" & Trim(Me.txtEvento.Text) & "'," & Trim(Me.ddlSecretaria.Text) & "," & Trim(Me.ddlDireccion.Text) & "," & Trim(Me.ddlTipoEvento.Text) & ",'" & Trim(Me.txtDescripcion.Text) & "'," & Trim(Me.ddlColonia.Text) & "," & Trim(Me.ddlCalle.Text) & ",'" & Trim(Me.txtext.Text) & "','" & Trim(UCase(Me.txtint.Text)) & "','" & Trim(Me.txtfecha.Text) & "','" & Trim(Me.txthrini.Text) & "','" & Trim(Me.txthrfin.Text) & "'," & Trim(Me.txtnumbenef.Text) & "," & Trim(alcalde) & ",'" & Trim(Me.txthrAlcalde.Text) & "'," & Trim(prensa) & "," & Trim(limpiar) & ",'" & Trim(Me.txtResponsableEvento.Text) & "'," & Trim(Me.txtTelefonoResponsable.Text) & ",'" & Trim(Me.txtOperadopor.Text) & "','" & Trim(Me.txtPuestoOperador.Text) & "'," & Trim(Me.txtTelefonoOperador.Text) & ",'" & Trim(Me.txtCorreoOperador.Text) & "',getdate()," & Session("clave_empl") & ",'" & Trim(Me.txtLugar.Text) & "','" & Trim(Me.txtHrSalidaAlcalde.Text) & "',NULL,1)"
                'End If
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                    'ERROR
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                    Exit Sub
                Else

                    '---------------------INSERTA PUNTO ----------------------------------
                    stry = "INSERT INTO [eventos].map_evento VALUES (" & Trim(folio) & "," & Trim(coordx) & "," & Trim(coordy) & "," & Trim(IDCOL) & ")"
                    resultado = conexion.sqlcambios(stry)
                    If resultado = -1 Then
                        'ERROR
                        'NO GUARDA NINGUN REGISTRO 
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_PuntoMapa();", True)
                    Else
                    End If

                    'REGISTRA LA ETAPA DEL EVENTO 
                    stry = String.Format("INSERT INTO bitacora_evento(id, folio, etapa, fecha_mov, clave_empl) VALUES({0}, {1}, GETDATE(), {2})", folio, 1, Session("clave_empl"))
                    resultado = conexion.sqlcambios(stry)

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito('" & folio & "');", True)

                    'MANDA ALERTAS DE CORREOS Y NOTIFICACIONES A LOS DEPARTAMENTOS DE EVENTOS Y COMUNICACIONES
                    Using notiHelper As New IntelipolisEngine.Helper.NotificationHelper()

                        'Manda alerta a comunicacion
                        notiHelper.EnviaNotificacionNuevoEventoDepComunicacion(folio)

                        'Manda alerta a eventos
                        notiHelper.EnviaNotificacionNuevoEventoDepEventos(folio)

                        'Manda alerta a Control Administrativo
                        notiHelper.EnviaNotificacionNuevoEventoDepControlAdministrativo(folio)

                        'Notifica a el usuario el Alta de el Folio
                        notiHelper.NotificaUsuarioEventoCreado(folio, Session("clave_empl"))

                    End Using

#Region "GUARDA LOG"
                    Helper.GuardaLog(Session("clave_empl"), String.Format("Ha agregado un nuevo evento con Folio {0}", folio))
#End Region

                    inicia()

                End If

            Else
                'mensaje de que no se puede guardar la fecha porque no s epeude capturar el mismo dia tiene que ser uan fecha posterior al dia de hoy
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_FechaPosterior();", True)
                Exit Sub
            End If
            '-------------------------------------------------------------------------------------------------------



        End If

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        inicia()
        'Me.lblmsjalta.Visible = False
        'Me.lblmsjalta.Text = ""

        'llenaCBO()
        Dim ColoEnviada As String
        Dim IDCOL As Integer

        ColoEnviada = Request.QueryString("IDCOL")
        IDCOL = Session("IDCOL")
        ColoEnviada = IDCOL
        If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)

        DibujarCol()




    End Sub


    'Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
    '    ExportarExcel()
    'End Sub

#End Region

#Region "RUTINAS"

    Sub usuario()
        If Session("privilegio") = 1 Then  'todas las secretarias
            'Admin
            Me.Label3.Text = "Admin:"
            Me.Label19.Text = Session("clave_empl")
        Else
            'Usuario
            Me.Label3.Text = "Usuario:"
            Me.Label19.Text = Session("clave_empl")
        End If
    End Sub


    Sub creaFolio()
        Session("folio") = 0
        stry = "Select max(folio) as folio from [eventos].reg_evento"
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            MsgBox("No se encontro la información")
        Else
            If (tabla.Rows(0)("folio").ToString) = "" Then
                folio = 1
                Session("folio") = folio
                Me.txtfolio.Text = folio
            Else
                folio = tabla.Rows(0)("folio").ToString
                folio = folio + 1
                Session("folio") = folio
                Me.txtfolio.Text = folio

            End If
        End If


    End Sub




    Sub cargaCombos()




        'cargaSecretarias


        If Session("privilegio") = 1 Then  'todas las secretarias
            'Exportar a Excel
            'Me.btnExcel.Visible = True
            stry = "select '' id_secr, '' secretaria UNION select id_secr,secretaria from [eventos].secretarias order by id_secr"
            tabla = conexion.sqlcon(stry)
        Else
            clave_secr = Session("id_secr") 'solo la secretaria con que se dio de alta
            stry = "select '' id_secr, '' secretaria UNION select id_secr,secretaria from [eventos].secretarias where id_secr = " & clave_secr & " order by id_secr"
            tabla = conexion.sqlcon(stry)
        End If

        If tabla.Rows.Count < 1 Then
            'MsgBox("No se encontro la información")
            Me.ddlSecretaria.Items.Clear()
            Me.ddlSecretaria.Items.Add("")
        Else
            ddlSecretaria.DataSource = tabla
            ddlSecretaria.DataTextField = "secretaria"
            ddlSecretaria.DataValueField = "id_secr"
            ddlSecretaria.DataBind()

        End If




        ''cargaOrigen --------CAMBIO A DIRECCION
        'stry = "select * from origen order by 1"
        'tabla = conexion.sqlcon(stry)

        'If tabla.Rows.Count < 1 Then

        '    Me.ddlDireccion.Items.Clear()
        '    Me.ddlDireccion.Items.Add("")
        'Else

        '    ddlDireccion.DataSource = tabla
        '    ddlDireccion.DataTextField = "origen"
        '    ddlDireccion.DataValueField = "id_origen"
        '    ddlDireccion.DataBind()

        'End If

        'carga Tipo Eventos
        stry = "select * from [eventos].evento order by 1"
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then

            Me.ddlTipoEvento.Items.Clear()
            Me.ddlTipoEvento.Items.Add("")
        Else
            Me.ddlTipoEvento.DataSource = tabla
            Me.ddlTipoEvento.DataTextField = "evento"
            Me.ddlTipoEvento.DataValueField = "id_evento"
            Me.ddlTipoEvento.DataBind()
        End If


        'cargaColonias
        stry = "select c.id_col,col.nombr_colonia from [eventos].calles c INNER join [eventos].Xcolonias col ON c.id_col=col.id_colonia group by c.id_col,col.nombr_colonia order by col.nombr_colonia  "
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            'MsgBox("No se encontro la información")
            Me.ddlColonia.Items.Clear()
            Me.ddlColonia.Items.Add("")
        Else
            ddlColonia.DataSource = tabla
            ddlColonia.DataTextField = "NOMBR_colonia"
            ddlColonia.DataValueField = "id_col"
            ddlColonia.DataBind()
            IDCOL = Me.ddlColonia.Text
        End If


    End Sub


    Sub CargarCallesColonia()
        'cargaCalles
        IDCOL = Session("IDCOL")
        stry = "select id_calle,calle from [eventos].calles where id_COL=" & IDCOL & " order by calle"

        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            'MsgBox("No se encontro la información")
            Me.ddlCalle.Items.Clear()
            Me.ddlCalle.Items.Add("")
        Else

            Me.ddlCalle.DataSource = tabla
            Me.ddlCalle.DataTextField = "calle"
            Me.ddlCalle.DataValueField = "id_calle"
            Me.ddlCalle.DataBind()
            'Me.ddlCalle.Text = tabla.Rows(0)("id_calle")
        End If
    End Sub

    Sub CargarCalles()
        'cargaCalles
        stry = "select id_calle,calle from [eventos].calles where id_COL=" & IDCOL & " and id_calle = " & clave_calle & " order by calle"

        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            'MsgBox("No se encontro la información")
            Me.ddlCalle.Items.Clear()
            Me.ddlCalle.Items.Add("")
        Else

            Me.ddlCalle.DataSource = tabla
            Me.ddlCalle.DataTextField = "calle"
            Me.ddlCalle.DataValueField = "id_calle"
            Me.ddlCalle.DataBind()
            'Me.ddlCalle.Text = tabla.Rows(0)("id_calle")
        End If
    End Sub
    Sub inicia()
        limpiar()
        creaFolio()
        Invisibles()
    End Sub

    Sub limpiar()

        Me.txtEvento.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtLugar.Text = ""
        Me.txtext.Text = ""
        Me.txtint.Text = ""
        Me.txtfechaIni.Text = ""
        Me.txtfechaFin.Text = ""
        Me.txthrini.Text = ""
        Me.txthrfin.Text = ""
        Me.txthrAlcalde.Text = ""
        Me.txtHrSalidaAlcalde.Text = ""
        Me.txtnumbenef.Text = ""
        Me.CheckPrensa.Checked = False
        Me.CheckAlcalde.Checked = False
        Me.CheckLimpieza.Checked = False
        Me.txtResponsableEvento.Text = ""
        Me.txtTelefonoResponsable.Text = ""
        Me.ddlEmpleadoOperador.SelectedIndex = 0
        Me.ddlPuestoOperador.SelectedIndex = 0
        Me.txtTelefonoOperador.Text = ""
        Me.txtCorreoOperador.Text = ""
        updOperador.Update()

    End Sub

    Sub Invisibles()

        'Me.label13.Visible = False
        Me.label16.Visible = False
        Me.label14.Visible = False
        Me.label15.Visible = False
        'Me.label17.Visible = False
        'Me.label18.Visible = False
        'Me.label1.Visible = False
        Me.label23.Visible = False



    End Sub
    Sub habilitar()
        Me.txtEvento.Enabled = True
        Me.ddlSecretaria.Enabled = True
        Me.ddlDireccion.Enabled = True
        Me.ddlTipoEvento.Enabled = True
        Me.ddlColonia.Enabled = True
        Me.ddlCalle.Enabled = True
        Me.txtext.Enabled = True
        Me.txtint.Enabled = True
        Me.txtfechaIni.Enabled = True
        Me.txtfechaFin.Enabled = True
        Me.txtnumbenef.Enabled = True
        Me.ddlEmpleadoOperador.Enabled = True
        Me.ddlPuestoOperador.Enabled = True
        Me.txtTelefonoOperador.Enabled = True
        Me.txtCorreoOperador.Enabled = True
        Me.txtAsistentes.Enabled = True




    End Sub
    Sub deshabilita()

        Me.txtEvento.Enabled = False
        Me.ddlSecretaria.Enabled = False
        Me.ddlDireccion.Enabled = False
        Me.ddlColonia.Enabled = False
        Me.ddlCalle.Enabled = False
        Me.txtext.Enabled = False
        Me.txtint.Enabled = False
        Me.txtfechaIni.Enabled = False
        Me.txtfechaFin.Enabled = False
        Me.txtnumbenef.Enabled = False
        Me.ddlEmpleadoOperador.Enabled = False
        Me.ddlPuestoOperador.Enabled = False
        Me.txtTelefonoOperador.Enabled = False
        Me.txtCorreoOperador.Enabled = False
        Me.txtAsistentes.Enabled = False


    End Sub
    'Sub consulta()



    '    ''Dim stry2 As String
    '    ''Dim rs2 As SqlClient.SqlDataReader
    '    stry = "SELECT r.folio,r.id_origen,r.id_secr,r.id_evento,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,Convert(char,r.fecha, 111) as fecha,r.num_benef FROM [eventos].reg_evento r INNER JOIN calles c ON r.id_calle =c.id_calle WHERE r.folio ='" & modiFolio & "' Group by r.folio,r.id_origen,r.id_secr,r.id_evento,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,r.fecha,r.num_benef"
    '    ''Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.Conectar())
    '    ''rs2 = cmd2.ExecuteReader()
    '    ''rs2.Read()

    '    tabla = conexion.sqlcon(stry)
    '    If tabla.Rows.Count < 1 Then
    '        Me.lblmsjalta.Text = "No Existe el Folio"
    '        Me.lblmsjalta.Visible = True
    '        deshabilita()
    '    Else

    '        Try

    '            If IsDBNull(Me.tabla.Rows(0)("folio")) Then
    '                Me.txtfolio.Text = ""
    '            Else
    '                Me.txtfolio.Text = tabla.Rows(0)("folio")
    '            End If
    '            If IsDBNull(tabla.Rows(0)("id_origen")) Then
    '                Me.ddl1.Text = ""
    '            Else
    '                Me.ddl1.Text = tabla.Rows(0)("id_origen")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("id_secr")) Then
    '                Me.ddlSecretaria.Text = ""
    '            Else
    '                Me.ddlSecretaria.Text = tabla.Rows(0)("id_secr")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("id_evento")) Then
    '                Me.ddlDireccion.Text = ""
    '            Else
    '                Me.ddlDireccion.Text = tabla.Rows(0)("id_evento")
    '            End If
    '            If IsDBNull(Me.tabla.Rows(0)("id_col")) Then
    '                Me.ddlColonia.Text = ""
    '            Else
    '                Me.ddlColonia.Text = tabla.Rows(0)("id_col")
    '                IDCOL = Me.ddlColonia.Text
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("num_ext")) Then
    '                Me.txtext.Text = ""
    '            Else
    '                Me.txtext.Text = tabla.Rows(0)("num_ext")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("num_int")) Then
    '                Me.txtint.Text = ""
    '            Else
    '                Me.txtint.Text = tabla.Rows(0)("num_int")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("fecha")) Then
    '                Me.txtfecha.Text = ""
    '            Else
    '                Me.txtfecha.Text = tabla.Rows(0)("fecha")
    '            End If
    '            If IsDBNull(tabla.Rows(0)("num_benef")) Then
    '                Me.txtnumbenef.Text = ""
    '            Else
    '                Me.txtnumbenef.Text = tabla.Rows(0)("num_benef")
    '            End If

    '            If IsDBNull(tabla.Rows(0)("num_benef")) Then
    '                Me.txtnumbenef.Text = ""
    '            Else
    '                Me.txtnumbenef.Text = tabla.Rows(0)("num_benef")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("id_calle")) Then
    '                Me.ddlCalle.Text = ""
    '            Else
    '                'Me.ddlCalle.Text = tabla.Rows(0)("id_calle")
    '                clave_calle = tabla.Rows(0)("id_calle")
    '                CargarCalles()
    '            End If


    '            'If IsDBNull(Me.tabla.Rows(0)("calle")) Then
    '            '    Me.ddlCalle.Text = ""
    '            'Else
    '            '    'CargarCalles()
    '            '    'Dim clave_calle As Integer
    '            '    'clave_calle = tabla.Rows(0)("id_calle")
    '            '    'Me.DropDownList1.SelectedItem.Text = tabla.Rows(0)("id_calle")
    '            '    Me.ddlCalle.Items.Clear()
    '            '    'Me.DropDownList1.Items.Add("")
    '            '    Dim i As Integer ' se crea la variable i para llenar el listbox
    '            '    i = 0
    '            '    For Each row As DataRow In tabla.Rows ' sirve para recorrer toda la tabla
    '            '        Me.ddlCalle.Items.Add(tabla.Rows(i)("calle").ToString())
    '            '        i = i + 1
    '            '    Next
    '            'End If

    '            Session("cambio") = 1
    '            habilitar()
    '        Catch ex As Exception
    '            'No existe ese folio
    '            Me.lblmsjalta.Text = "No Existe el Folio"
    '            Me.lblmsjalta.Visible = True
    '            deshabilita()
    '        End Try
    '    End If
    'End Sub
    Sub ExportarExcel()

        'stry = "SELECT folio as FOLIO, origen as ORIGEN,secretaria as SECRETARIA,evento AS EVENTO,nombr_colonia AS COLONIA,calle AS CALLE,num_ext AS NUM_EXT,num_int AS NUM_INT,Convert(char,fecha, 111) AS FECHA_EVENTO,num_benef as NUM_BENEFICIADOS  FROM [eventos].reg_evento re INNER JOIN origen o ON re.id_origen = o.id_origen INNER JOIN secretarias sec ON re.id_secr = sec.id_secr INNER join [eventos].Xcolonias  col ON re.id_col=col.id_colonia INNER JOIN calles c ON re.id_calle=c.id_calle INNER JOIN evento  e ON re.id_evento = e.id_evento Group BY  folio,origen,secretaria,evento,nombr_colonia,calle,num_ext,num_int,fecha,num_benef ORDER BY re.folio "
        stry = "SELECT re.folio as FOLIO, origen as ORIGEN,secretaria as SECRETARIA,evento AS EVENTO,nombr_colonia AS COLONIA,calle AS CALLE,num_ext AS NUM_EXT,num_int AS NUM_INT,Convert(char,fecha, 111) AS FECHA_EVENTO,num_benef as NUM_BENEFICIADOS,num_asistentes NUM_ASISTENTES, coordx as COORD_X,coordy AS COORD_Y " &
                "FROM [eventos].reg_evento re " &
                "INNER JOIN [eventos].origen o ON re.id_origen = o.id_origen " &
                "INNER JOIN [eventos].secretarias sec ON re.id_secr = sec.id_secr " &
                "INNER JOIN [eventos].xcolonias col ON re.id_col=col.id_colonia " &
                "INNER JOIN [eventos].calles c ON re.id_calle=c.id_calle " &
                "INNER JOIN [eventos].evento  e ON re.id_evento = e.id_evento " &
                "INNER JOIN [eventos].[eventos].map_evento m ON re.folio=m.folio " &
                "Group BY  re.FOLIO,o.origen,sec.secretaria,e.evento,col.nombr_colonia,c.calle,re.num_ext,re.num_int,re.fecha,re.num_benef,m.coordx,m.coordy ORDER BY re.folio"


        ''-----------------NUEVO QUERY POR LOS NUEVOS CAMBIOS, POR SI QUIEREN EXPORTAR LA INFORMACON

        '        SELECT 
        ' re.folio as FOLIO,re.nombre_evento AS EVENTO,sec.secretaria as SECRETARIA,dir.nombre_depe as DIRECCION, e.evento as TIPO_EVENTO,
        ' re.descripcion as DESCRIPCION,col.nombr_colonia AS COLONIA,c.calle AS CALLE,re.num_ext AS NUM_EXT,re.num_int AS NUM_INT,
        ' Convert(char,re.fecha, 111) AS FECHA_EVENTO,re.hora as HORA_EVENTO ,re.num_benef as NUM_BENEFICIADOS,m.coordx as COORD_X,m.coordy AS COORD_Y ,
        ' PRENSA = CASE re.prensa 
        '        WHEN '0' THEN 'SIN PRENSA'
        '        WHEN '1' THEN 'CON PRENSA'

        '            End
        'FROM [eventos].reg_evento re
        'INNER JOIN evento e ON re.id_evento = e.id_evento
        'INNER JOIN secretarias sec ON re.id_secr = sec.id_secr 
        'INNER JOIN direcciones dir ON re.id_depe = dir.clave_depe and re.id_secr =dir.clave_secr  
        'INNER join [eventos].Xcolonias  col ON re.id_col=col.id_colonia 
        'INNER JOIN calles c ON re.id_calle=c.id_calle 
        'INNER JOIN [eventos].map_evento m ON re.folio=m.folio 
        'Group BY  re.FOLIO,re.nombre_evento,sec.secretaria,dir.nombre_depe,e.evento, 
        're.descripcion,col.nombr_colonia,c.calle,re.num_ext,re.num_int,
        're.fecha,re.hora,re.num_benef,m.coordx,m.coordy,re.prensa
        'ORDER BY re.folio




        tabla = conexion.sqlcon(stry)
        Me.GridView1.DataSource = tabla
        Me.GridView1.DataBind()


        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Me.GridView1.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=REGISTRO_EVENTOS.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()


    End Sub
#End Region

#Region "FUNCIONES"
    Function ValidarAlta() As Boolean

        Invisibles()

        ValidarAlta = True

        'If Me.txtfolio.Text = "" Then
        '    Me.label13.Visible = True
        '    ValidarAlta = False
        'End If
        'If Me.txtEvento.Text = "" Then
        '    Me.label1.Visible = True
        '    ValidarAlta = False
        'End If
        If Me.ddlSecretaria.Text = "" Then
            Me.label16.Visible = True
            ValidarAlta = False
        End If
        'If Me.ddlDireccion.Text = "" Then
        '    Me.label1.Visible = True
        '    ValidarAlta = False
        'End If
        If Me.ddlColonia.Text = "" Then
            Me.label16.Visible = True
            ValidarAlta = False
        End If
        If Me.ddlCalle.Text = "" Then
            Me.label16.Visible = True
            ValidarAlta = False
        End If

        'If Me.txtext.Text = "" Then
        '    If Me.txtint.Text = "" Then
        '        Me.label15.Visible = True
        '        ValidarAlta = False
        '    End If
        'End If
        'If Me.txtint.Text = "" Then
        '    If Me.txtext.Text = "" Then
        '        Me.label15.Visible = True
        '        ValidarAlta = False
        '    End If
        'End If
        'If Me.txtfecha.Text = "" Then
        '    Me.label18.Visible = True
        '    ValidarAlta = False
        'End If
        'If Not IsDate(Me.txtfecha.Text) Then
        '    Me.label18.Visible = True
        '    ValidarAlta = False
        'End If
        'If Me.txtnumbenef.Text = "" Then
        '    Me.label17.Visible = True
        '    ValidarAlta = False
        'End If

        If Me.txthrfin.Text = "" Then
            Me.label23.Visible = True
            ValidarAlta = False
        End If

        If Len(Me.txthrini.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)

            Me.txthrini.Text = ""
            'FALTA MSJ DE ERROR
            ValidarAlta = False

        End If

        If Len(Me.txthrfin.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)
            Me.txthrfin.Text = ""
            'FALTA MSJ DE ERROR
            ValidarAlta = False

        End If

        If Len(Me.txthrAlcalde.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_HoraAlcalde();", True)
            Me.txthrAlcalde.Text = ""
            'FALTA MSJ DE ERROR
            ValidarAlta = False

        End If


    End Function
#End Region

#Region "COMBOS"
    Protected Sub ddlColonia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlColonia.SelectedIndexChanged
        Dim IDCOL As Integer

        IDCOL = Me.ddlColonia.Text
        Session("IDCOL") = IDCOL
        CargarCallesColonia()
        'ColoEnviada = Request.QueryString("IDCOL")
        Dim ColoEnviada As String
        ColoEnviada = IDCOL
        If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)

        DibujarCol()
        'Response.Redirect("frm_mapa.aspx")
        Me.ddlCalle.Focus()
    End Sub


    Protected Sub ddlSecretaria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSecretaria.SelectedIndexChanged
        Dim IDSECRE As Integer

        IDSECRE = Me.ddlSecretaria.Text
        Session("IDSECRE") = IDSECRE
        CargarDirecciones()
        'CargarEventoSecre()
        CargaResponsabledelEvento()
        Me.ddlDireccion.Focus()

        ddlDireccion_SelectedIndexChanged(Nothing, Nothing)
    End Sub



#End Region

    Sub CargaResponsabledelEvento()
        'cargaDirecciones
        IDSECRE = Session("IDSECRE")
        stry = "select director, telefonoDirector from [eventos].secretarias  WHERE id_secr = " & IDSECRE & ""
        tabla = conexion.sqlcon(stry)
        If tabla.Rows.Count < 1 Then
            Me.txtResponsableEvento.Text = ""
        Else
            Me.txtResponsableEvento.Text = tabla.Rows(0)("director")
            Me.txtTelefonoResponsable.Text = tabla.Rows(0)("telefonoDirector")
        End If
    End Sub

    Sub CargarDirecciones()

        'cargaDirecciones
        IDSECRE = Session("IDSECRE")
        stry = "select a.clave_depe,a.nombre_depe from [eventos].direcciones a INNER JOIN [eventos].secretarias b ON a.clave_secr=b.id_secr WHERE b.id_secr = " & IDSECRE & ""
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            Me.ddlDireccion.Items.Clear()
            Me.ddlDireccion.Items.Add("")
        Else

            Me.ddlDireccion.DataSource = tabla
            Me.ddlDireccion.DataTextField = "nombre_depe"
            Me.ddlDireccion.DataValueField = "clave_depe"
            Me.ddlDireccion.DataBind()
            'Me.ddlCalle.Text = tabla.Rows(0)("id_calle")
        End If


    End Sub
    'Sub CargarEventoSecre()
    '    'cargaCalles
    '    IDSECRE = Session("IDSECRE")

    '    stry = "select e.id_evento,e.evento from secr_evento s INNER JOIN evento e ON s.id_evento=e.id_evento WHERE id_secr = " & IDSECRE & " and e.activo=1"
    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then

    '        Me.ddlDireccion.Items.Clear()
    '        Me.ddlDireccion.Items.Add("")
    '    Else

    '        Me.ddlDireccion.DataSource = tabla
    '        Me.ddlDireccion.DataTextField = "evento"
    '        Me.ddlDireccion.DataValueField = "id_evento"
    '        Me.ddlDireccion.DataBind()

    '    End If
    'End Sub



    Public Sub UbicaColonias(ByVal Tcolx As String)
        Dim con As New conexion
        Dim stry As String

        Dim Colonia As String = ""
        Dim Longx As String = ""
        Dim Latix As String = ""
        Dim CadFinal As String = "*"
        TextTitulo.Text = Trim(DropDownList1.Text)

        stry = "select * from [eventos].TbColonias where mun=47 and   municipio='3'  and colonia in (select nombr_colonia from [eventos].xcolonias where id_colonia=" & Tcolx & ")"
        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader
        Try
            While tDrsx.Read
                Longx = Trim(tDrsx(3))
                Latix = Trim(tDrsx(4))
                CadFinal = CadFinal & "(" & Latix & "," & Longx & ")*"
            End While
        Finally
            tDrsx.Close()
        End Try

        TextLng.Text = Longx
        TextLat.Text = Latix
        TextBox2.Text = CadFinal
        llenaCBO(Tcolx)
    End Sub

    Public Sub llenaCBO(ByVal xIdc As String)
        Dim con As New conexion
        Dim stry As String

        Dim Colonia As String = ""

        DropDownList1.Items.Clear()
        stry = "select colonia,mun,count(mun) from [eventos].TbColonias where mun=47  and municipio='3' and colonia in (select nombr_colonia from [eventos].xcolonias where id_colonia=" & xIdc & ") group by colonia, mun order by colonia"
        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader()

        Try
            While tDrsx.Read
                DropDownList1.Items.Add(tDrsx(0))
            End While
        Finally
            tDrsx.Close()
        End Try


    End Sub
    Protected Sub btn_consultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_consultar.Click
        DibujarCol()
    End Sub
    Public Sub DibujarCol()
        Dim con As New conexion
        Dim stry As String

        Dim Colonia As String = ""
        Dim Longx As String = ""
        Dim Latix As String = ""
        Dim CadFinal As String = "*"
        TextTitulo.Text = Trim(DropDownList1.Text)

        stry = "select * from [eventos].TbColonias where mun=47  and  colonia='" & DropDownList1.Text.Trim & "' and municipio='3'"
        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader
        Try
            While tDrsx.Read
                Longx = Trim(tDrsx(3))
                Latix = Trim(tDrsx(4))
                CadFinal = CadFinal & "(" & Latix & "," & Longx & ")*"
            End While
        Finally
            tDrsx.Close()
        End Try
        TextLng.Text = Longx
        TextLat.Text = Latix
        TextBox2.Text = CadFinal

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'PARA QUE NO SE MUESTREN LOS MENUS
        Session("DESACTIVA_MENU") = "1"

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        'PARA QUE SE MUESTREN LOS MENUS
        Session("DESACTIVA_MENU") = "0"

    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.TextBox1.Text = ""
        Me.TextTitulo.Text = ""
        Me.TextLat.Text = ""
        Me.TextLng.Text = ""
    End Sub






    Protected Sub txthrini_TextChanged(sender As Object, e As System.EventArgs) Handles txthrini.TextChanged

        If Len(Me.txthrini.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)

        Else

        End If
        Me.txthrAlcalde.Focus()
        'Me.CheckAlcalde.Focus()


    End Sub







    Protected Sub txthrfin_TextChanged(sender As Object, e As System.EventArgs) Handles txthrfin.TextChanged
        If Len(Me.txthrfin.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)


        Else

        End If


        Me.CheckLimpieza.Focus()



    End Sub

    Protected Sub txthrAlcalde_TextChanged(sender As Object, e As System.EventArgs) Handles txthrAlcalde.TextChanged
        If Len(Me.txthrAlcalde.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_HoraAlcalde();", True)

        Else

        End If

        Me.txtHrSalidaAlcalde.Focus()
    End Sub

    Protected Sub txtfechaFin_TextChanged(sender As Object, e As System.EventArgs) Handles txtfechaFin.TextChanged
        If Len(Trim(Me.txtfechaIni.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub

        End If

        If Len(Trim(Me.txtfechaFin.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub

        End If

        Dim fechaInicio = DateTime.Parse(txtfechaIni.Text)
        Dim fechaFin = DateTime.Parse(txtfechaFin.Text)
        If fechaFin < fechaInicio Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_FechaMayor();", True)
            Exit Sub
        End If

    End Sub

    Protected Sub txtfecha_TextChanged(sender As Object, e As System.EventArgs) Handles txtfechaIni.TextChanged

        If Len(Trim(Me.txtfechaIni.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub

        End If

        If Len(Trim(Me.txtfechaFin.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub

        End If


        If Session("UsuarioAdmin") <> 1234 Then


            stry = "select CONVERT(char, GETDATE(),111) as fecha_captura"

            tabla = conexion.sqlcon(stry)
            If tabla.Rows.Count < 1 Then
            Else
                fecha_captura = tabla.Rows(0)("fecha_captura")
                fecha_captura = Trim(fecha_captura)
            End If

            Dim diashabiles As Integer
            diashabiles = 0
            diashabiles = Laborales(fecha_captura, Me.txtfechaIni.Text)

            Dim puesto = Session("Puesto")
            If diashabiles > VariablesGlobales.DIAS_ESPERA_EVENTO Then

            Else
                If puesto = 4 Then
                Else
                    ' NO GUARDA EVENTO
                    'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('No se permite guardar el evento, porque no está dentro de los 3 días hábiles permitidos!!')", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_NoPermiteGuardarEventoYPuntoMapa();", True)
                    Exit Sub
                End If
            End If

        Else
            'se agrego el else porque:
            'Si es usuario 1234 igual valida que SOLO capture eventos posteriores al día de hoy.
            'Valida que capture una fecha posterior al dia de hoy

            stry = "select CONVERT (char, GETDATE(),111) as fecha_captura"
            tabla = conexion.sqlcon(stry)
            If tabla.Rows.Count < 1 Then
            Else
                fecha_hoy = tabla.Rows(0)("fecha_captura")
                fecha_hoy = Trim(fecha_hoy)
                fecha_captura = Format(fecha_hoy, "yyyy-MM-dd")
            End If

            If Me.txtfechaIni.Text > fecha_captura Then
                'No se hace nada porque es valida la fecha y permite Guardar la informacion ya que es mayor al dia de hoy
            Else
                'mensaje de que no se puede guardar la fecha porque no s epeude capturar el mismo dia tiene que ser uan fecha posterior al dia de hoy
                ' NO GUARDA FECHA
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_FechaPosterior();", True)
                Exit Sub
            End If


        End If
        txtfechaFin.Text = txtfechaIni.Text

        Me.CheckAlcalde.Focus()

    End Sub


    Protected Sub CheckAlcalde_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckAlcalde.CheckedChanged
        'Me.txthrAlcalde.Focus()
        Me.txthrini.Focus()
    End Sub

    Protected Sub CheckPrensa_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckPrensa.CheckedChanged
        Me.txtnumbenef.Focus()
    End Sub


    Protected Sub txtHrSalidaAlcalde_TextChanged(sender As Object, e As System.EventArgs) Handles txtHrSalidaAlcalde.TextChanged

        If Len(Me.txtHrSalidaAlcalde.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)

        Else

        End If


        Me.txthrfin.Focus()

    End Sub

    Protected Sub CheckLimpieza_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckLimpieza.CheckedChanged
        Me.CheckPrensa.Focus()
    End Sub

    Protected Sub ddlDireccion_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim con As New Class1
        Using data As New DB(con.conectar())

            'PUESTOS
            Dim params2() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@clave_sec", ddlSecretaria.SelectedValue),
             New SqlParameter("@clave_depe", ddlDireccion.SelectedValue)
        }
            Dim dt = data.ObtieneDatos("ObtienePuestos", params2).Tables(0)
            ddlPuestoOperador.DataSource = dt
            ddlPuestoOperador.DataValueField = "clave_puesto"
            ddlPuestoOperador.DataTextField = "nombre_puesto"
            ddlPuestoOperador.DataBind()
            updOperador.Update()
        End Using
        ddlPuestoOperador_SelectedIndexChanged(Nothing, Nothing)
        cargaActividades()
    End Sub

    Private Sub cargaActividades()
        'Carga Líneas
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@idDireccion", ddlDireccion.SelectedValue),
                New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
                New SqlParameter("@idAnio", ddlAnio.SelectedValue)
            }

            ddlActividad.DataSource = data.ObtieneDatos("ObtieneLineas", params)
            ddlActividad.DataTextField = "Nombr_linea"
            ddlActividad.DataValueField = "ID"
            ddlActividad.DataBind()
        End Using
        ddlActividad.Items.Insert(0, New ListItem("Selecciona la Línea", "0"))
    End Sub

    Protected Sub ddlPuestoOperador_SelectedIndexChanged(sender As Object, e As EventArgs)

        Try
            Dim con As New Class1
            Using data As New DB(con.conectar())

                'PUESTOS
                Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@clave_secr", ddlSecretaria.SelectedValue),
                 New SqlParameter("@clave_dep", ddlDireccion.SelectedValue),
                    New SqlParameter("@puesto", ddlPuestoOperador.SelectedItem.Text)
            }
                Dim dt = data.ObtieneDatos("ObtieneEmpleadosPuesto", params2).Tables(0)
                ddlEmpleadoOperador.DataSource = dt
                ddlEmpleadoOperador.DataValueField = "clave_empl"
                ddlEmpleadoOperador.DataTextField = "nombr_empl"
                ddlEmpleadoOperador.DataBind()
                updOperador.Update()
            End Using
            ddlEmpleadoOperador_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlEmpleadoOperador_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim con As New Class1
        Using data As New DB(con.conectar())

            'PUESTOS
            Dim params2() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@clave_empl", ddlEmpleadoOperador.SelectedValue)
        }
            Dim dt = data.ObtieneDatos("ObtieneInfoEmpleado", params2).Tables(0)
            txtCorreoOperador.Text = dt.Rows(0)("email").ToString()
            txtTelefonoOperador.Text = dt.Rows(0)("telefono").ToString()
            updOperador.Update()
        End Using
    End Sub


    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargaSubActividades()
    End Sub

    Private Sub CargaSubActividades()
        Try
            Dim con As New Class1
            Dim claveEmpleado As Integer = Session("Clave_empl")

            Dim administracion = ddlAdmon.SelectedValue
            Dim anio = ddlAnio.SelectedValue
            Try
                administracion = Request.QueryString("administracion")
                anio = Request.QueryString("anio")
            Catch ex As Exception

            End Try

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

    Protected Sub ddlAdmon_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Using data As New DB(con.conectar())
        '    Dim params() As SqlParameter = New SqlParameter() _
        '    {
        '        New SqlParameter("@idAdmon", ddlAdmon.SelectedValue),
        '        New SqlParameter("@clave_empl", -1)
        '    }

        '    ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
        '    ddlSecretaria.DataTextField = "Nombr_secretaria"
        '    ddlSecretaria.DataValueField = "IdSecretaria"
        '    ddlSecretaria.DataBind()
        'End Using
        'ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaria", "0"))

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
End Class
