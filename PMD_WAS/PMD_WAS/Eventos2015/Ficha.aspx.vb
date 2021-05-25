Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion
Imports System.IO.MemoryStream
Imports System.Text
Imports System.Net
Imports System.Net.Mail

Partial Class Ficha
    Inherits System.Web.UI.Page

    Dim stry As String

    Dim folio As Integer
    Dim tabla As DataTable
    Dim clave_secr As Integer
    Dim resultado As Integer
    Dim ds As New DataSet
    Dim actividad As String
    Dim tabfila As New DataTable
    Dim con As New conexion
    Dim todoslosemail As String
    Dim fecha_envio As String
    Dim nombre_evento As String
    Dim desc_evento As String
    Dim calle_evento As String
    Dim col_evento As String
    Dim numext_evento As String
    Dim fecha_evento As String
    Dim hr_evento As String
    Dim aprobada As Boolean

    Dim vestimenta As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Ficha Evento")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session("paso") = "1" Then

        Else
            Response.Redirect("~/Password.aspx")
        End If


        If Not IsPostBack = True Then

            'Valida si tiene un folio lo carga en automatico
            Try
                Dim folio = Request.QueryString("folio")
                If Not folio Is Nothing Then
                    txtFolio.Text = folio

                    txtFolio_TextChanged(Nothing, Nothing)
                End If

            Catch ex As Exception

            End Try

            Session("ConsecAct") = 0
            usuario()
            cargaInvitados()
            cargaVestimenta()

            'VALIDA SI ES READ ONLY
            Dim soloLectura = Request.QueryString("readOnly")
            If Not soloLectura Is Nothing Then
                btnGuardar.Visible = False
                btnCancelar.Visible = False
                ImageButton2.Visible = False
                ImageButton1.Visible = False

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

    Sub cargaInvitados()


        'Dim con As New conexion


        'stry = "Select id_secr, secretaria,estatus = 0 from secretarias order by 1"
        ''stry = " select f.clave_factura,f.clave_proveedor,p.proveedor,f.fecha_factura,s.secretaria,d.dependencia,f.fecha_alta,f.monto_factura,f.estatus, (CASE WHEN (F.ESTATUS = '0') THEN 'PENDIENTE' ELSE 'APLICADA' END) AS tramite,(CASE WHEN (F.ESTATUS = '0') THEN 'True' ELSE 'False' END) AS habilitar  from facturas f  inner join secretaria s ON s.clave_secr = f.clave_secr  INNER join dependencia d ON d.clave_depe = f.clave_depe inner join proveedor p ON f.clave_proveedor = p.clave_proveedor WHERE estatus in (0,1)"

        'Dim da As New SqlDataAdapter(stry, con.Conectar)
        Dim dt As New DataTable()
        Dim conn As New conexion()
        Using data As New DB(conn.Conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@folio", txtFolio.Text)
                }
            dt = data.ObtieneDatos("ObtieneInvitadosFicha", params).Tables(0)
            Try

                'da.Fill(dt)
                Me.GridView3.DataSource = dt
                If dt.Rows.Count = 0 Then
                    'no hay nada
                    Me.GridView3.DataBind()
                Else
                    'si trae
                    Me.GridView3.DataBind()
                End If
            Catch ex As Exception
                'MsgBox(da.Fill(dt))
            End Try

        End Using



    End Sub


    Sub cargaVestimenta()

        stry = "Select id_vest, vestimenta from [eventos].cat_vestimenta order by 1"

        tabla = conexion.sqlcon(stry)


        If tabla.Rows.Count < 1 Then

            Me.drpVestimenta.Items.Clear()
            Me.drpVestimenta.Items.Add("")
        Else


            Me.drpVestimenta.DataSource = tabla
            Me.drpVestimenta.DataTextField = "vestimenta"
            Me.drpVestimenta.DataValueField = "id_vest"
            Me.drpVestimenta.DataBind()


        End If
    End Sub
    'Sub cargaActividades()


    '    'stry = " select '' id_act, '' actividad UNION  select id_act,actividad from cat_actividad order by id_act "
    '    'tabla = conexion.sqlcon(stry)


    '    'If tabla.Rows.Count < 1 Then

    '    '    Me.drpAvtividad.Items.Clear()
    '    '    Me.drpAvtividad.Items.Add("")
    '    'Else


    '    '    Me.drpAvtividad.DataSource = tabla
    '    '    Me.drpAvtividad.DataTextField = "actividad"
    '    '    Me.drpAvtividad.DataValueField = "id_act"
    '    '    Me.drpAvtividad.DataBind()


    '    'End If


    '    Dim con As New conexion
    '    Dim stry As String


    '    stry = " select '' id_act, '' actividad UNION  select id_act,actividad from cat_actividad order by id_act "


    '    Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
    '    Dim tDrsx As System.Data.SqlClient.SqlDataReader
    '    tDrsx = tRsGen.ExecuteReader()

    '    Try


    '        While tDrsx.Read


    '            drpAvtividad.DataSource = tDrsx
    '            drpAvtividad.DataTextField = "actividad"
    '            drpAvtividad.DataValueField = "id_act"
    '            drpAvtividad.DataBind()


    '        End While
    '    Finally
    '        tDrsx.Close()
    '    End Try



    'End Sub
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

                ''Dim validada As Boolean = tabla.Rows(0)("validada").ToString
                ''If validada = True Then
                Dim validada As Integer = tabla.Rows(0)("validada").ToString

                If validada = 1 Or validada = 0 Then
                    'Ya fue validada y existe el folio
                    Me.lblFolioEvento.Text = tabla.Rows(0)("nombre_evento").ToString


                    'Me.lblHrIniFija.Text = tabla.Rows(0)("hr_ini").ToString
                    'Me.lblHrArrFija.Text = tabla.Rows(0)("hr_alcalde").ToString
                    'Me.lblHrCerrFija.Text = tabla.Rows(0)("hr_fin").ToString

                    stry = "select * from [eventos].reg_ficha  WHERE folio_evento = " & Me.txtFolio.Text & ""
                    tabla = conexion.sqlcon(stry)
                    If tabla.Rows.Count < 1 Then
                        'NO EXISTE FICHA, se habilita para que empieze a capturar
                        cargaInvitados()
                        CargaProgramaAlcalde()
                        habilitar()
                    Else
                        'Deshabilita
                        habilitar()
                        'MUESTRA LOS DATOS GRAL DE FICHA
                        Me.txtAntecedentes.Text = tabla.Rows(0)("antecedentes")
                        'MUESTRA VESTIMENTA 
                        CargaVestimentaGuardada()
                        'MUESTRA LAS ACTIVIDADES DEL PROGRAMA
                        CargaPrograma()
                        'MUESTRA LAS SECRETARIAS INVITADAS
                        CargaSecretariasInvitadas()
                        'MUESTRA LOS INVITADOS ESPECIALES
                        CargaInvitadosEspeciales()
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Ya se ha capturado la ficha');", True)
                    End If

                Else
                    If validada = 0 Then
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('No ha sido validado por Relacion Publica');", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Ficha_NoHaSidoValidada();", True)
                        Exit Sub
                    Else
                        If validada = 2 Then
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Ficha_NoHaSidoAprobada();", True)
                            Exit Sub
                        End If
                    End If
                End If

            End If
        Else
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar el numero de folio a Consultar');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Ficha_ErrorFolioIngresado();", True)

        End If
    End Sub


    Sub habilitar()

        Me.txtAntecedentes.Enabled = True
        Me.drpVestimenta.Enabled = True
        Me.txtActividad.Enabled = True
        Me.txtHr.Enabled = True
        Me.GridView1.Enabled = True
        Me.GridView2.Enabled = True
        Me.GridView3.Enabled = True
        Me.txtEmpresa.Enabled = True
        Me.txtNombre.Enabled = True
        Me.txtPuesto.Enabled = True
        Me.txtTelefono.Enabled = True
        Me.txtContacto.Enabled = True
    End Sub
    Sub CargaVestimentaGuardada()
        stry = "select a.id_vest,b.vestimenta from [eventos].reg_ficha A INNER JOIN [eventos].cat_vestimenta b on a.id_vest = b.id_vest WHERE a.folio_evento = " & Me.txtFolio.Text & ""
        tabla = conexion.sqlcon(stry)
        If tabla.Rows.Count < 1 Then
        Else
            ' CargaVestimenta()
            Me.drpVestimenta.Text = tabla.Rows(0)("id_vest")
        End If

    End Sub

    Sub CargaPrograma()

        Dim con As New conexion
        stry = "SELECT HR,ACTIVIDAD FROM [eventos].PROGRAMA A INNER JOIN [eventos].reg_ficha B ON A.folio_ficha = B.folio_ficha   WHERE  B.folio_evento = " & Me.txtFolio.Text & " order by hr"
        Dim da As New SqlDataAdapter(stry, con.Conectar)
        Dim dt As New DataTable
        da.Fill(dt)
        Session("tabses1") = dt
        Me.GridView1.DataSource = dt
        Me.GridView1.DataBind()

    End Sub
    Sub CargaProgramaAlcalde()

        Dim con As New conexion

        stry = "select hr_ini, hr_alcalde, hr_salida, hr_fin  from [eventos].reg_evento where folio =  " & Me.txtFolio.Text & ""
        tabla = conexion.sqlcon(stry)
        If tabla.Rows.Count < 1 Then
        Else
            Dim hrini As String
            Dim hralcalde As String
            Dim hrsalida As String
            Dim hrfin As String



            Dim dt As New DataTable
            dt = Session("tabses1")

            Dim dr As DataRow
            dt = New DataTable
            dt.Columns.Add("HORA")
            dt.Columns.Add("PROGRAMA")
            Session.Add("Tabla", dt)


            If IsDBNull(Me.tabla.Rows(0)("hr_ini")) Then
            Else
                If Me.tabla.Rows(0)("hr_ini") = "" Then
                Else
                    hrini = tabla.Rows(0)("hr_ini")
                    dr = dt.NewRow
                    dr("HORA") = hrini
                    dr("PROGRAMA") = "Inicio del Evento"
                    dt.Rows.Add(dr)
                End If

            End If



            If IsDBNull(Me.tabla.Rows(0)("hr_alcalde")) Then
            Else
                If Me.tabla.Rows(0)("hr_alcalde") = "" Then
                Else
                    hralcalde = tabla.Rows(0)("hr_alcalde")
                    dr = dt.NewRow
                    dr("HORA") = hralcalde
                    dr("PROGRAMA") = "Arribo del Alcalde"
                    dt.Rows.Add(dr)
                End If

            End If


            If IsDBNull(Me.tabla.Rows(0)("hr_salida")) Then
            Else

                If Me.tabla.Rows(0)("hr_salida") = "" Then
                Else
                    hrsalida = tabla.Rows(0)("hr_salida")
                    dr = dt.NewRow
                    dr("HORA") = hrsalida
                    dr("PROGRAMA") = "Salida del Alcalde"
                    dt.Rows.Add(dr)
                End If
            End If


            If IsDBNull(Me.tabla.Rows(0)("hr_fin")) Then
            Else

                If Me.tabla.Rows(0)("hr_fin") = "" Then
                Else
                    hrfin = tabla.Rows(0)("hr_fin")
                    dr = dt.NewRow
                    dr("HORA") = hrfin
                    dr("PROGRAMA") = "Término del Evento"
                    dt.Rows.Add(dr)
                End If
            End If


            ds.Tables.Add(dt)
            Session.Add("tabses1", dt)
            GridView1.DataSource = dt
            GridView1.DataBind()

        End If



    End Sub

    Sub CargaSecretariasInvitadas()
        Dim con As New conexion
        stry = "select id_secr, secretaria, estatus = isnull((idcheck), 0)" &
                "from [eventos].secretarias s " &
                "left join (select i.folio_ficha, invitados, idcheck = 1 " &
                "			from [eventos].invitados i " &
                "			inner join [eventos].reg_ficha r " &
                "				on i.folio_ficha = r.folio_ficha " &
                "			where r.folio_evento = " & Me.txtFolio.Text & ") invita " &
                "	on s.id_secr = invita.invitados " &
                "GROUP BY id_secr, secretaria, idcheck " &
                "ORDER BY id_secr, secretaria"

        Dim da As New SqlDataAdapter(stry, con.Conectar)
        Dim dt As New DataTable()

        Try

            da.Fill(dt)
            Me.GridView3.DataSource = dt
            If dt.Rows.Count = 0 Then
                'no hay nada
                Me.GridView3.DataBind()
            Else
                'si trae
                Me.GridView3.DataBind()
            End If
        Catch ex As Exception
            MsgBox(da.Fill(dt))
        End Try



        'Dim RsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        'Dim Drsx As System.Data.SqlClient.SqlDataReader
        'Drsx = RsGen.ExecuteReader
        'Try
        '    While Drsx.Read

        '    End While
        'Finally
        '    Drsx.Close()
        'End Try


    End Sub

    Sub CargaInvitadosEspeciales()

        stry = "SELECT EMPRESA,NOMBRE,PUESTO,TELEFONO,CORREO as CONTACTO FROM [eventos].invitados_esp  A INNER JOIN [eventos].reg_ficha B ON A.folio_ficha = B.folio_ficha   WHERE  B.folio_evento = " & Me.txtFolio.Text & " order by EMPRESA"
        Dim da2 As New SqlDataAdapter(stry, con.Conectar)
        Dim dt2 As New DataTable
        da2.Fill(dt2)
        Session("tabses2") = dt2
        Me.GridView2.DataSource = dt2
        Me.GridView2.DataBind()

    End Sub

    'Sub CargaActividad()
    '    stry = "select actividad from cat_actividad where id_act =  " & Me.lblidActividad.Text & "  order by id_act"
    '    tabla = conexion.sqlcon(stry)


    '    If tabla.Rows.Count < 1 Then
    '        Me.drpAvtividad.Items.Clear()
    '        Me.drpAvtividad.Items.Add("")
    '    Else
    '        actividad = tabla.Rows(0)("actividad")

    '    End If

    'End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        If Me.txtFolio.Text = "" Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar el folio');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Ficha_ErrorFolioIngresado();", True)
            Exit Sub

        ElseIf Len(Me.txtHr.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)



        ElseIf Me.txtActividad.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor seleccionar una actividad');", True)
            Exit Sub


        ElseIf Me.txtHr.Text = "00:00" Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor ingresar una hora valida');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)

            Exit Sub

        ElseIf Me.txtHr.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar la hora');", True)
            Exit Sub


        Else

            'Dim consec As Integer

            'If GridView1.Rows.Count > 0 Then 'ya hay mas de 1

            '    Dim filaConsec As Integer
            '    For filaConsec = 0 To Me.GridView1.Rows.Count - 1
            '        consec = Me.GridView1.Rows.Count + 1
            '        Session("ConsecAct") = consec
            '    Next


            'Else

            '    consec = 1
            '    Session("ConsecAct") = consec
            'End If


            Dim dt As New DataTable
            dt = Session("tabses1")
            Dim dr As DataRow


            dt = New DataTable
            'dt.Columns.Add("NO.")
            dt.Columns.Add("HORA")
            dt.Columns.Add("PROGRAMA")
            Session.Add("Tabla", dt)

            'Buscar duplicado
            For i As Integer = 0 To GridView1.Rows.Count - 1
                If Me.txtActividad.Text = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text) Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar una actividad diferente');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Ficha_ActDiferente() ;", True)

                    Exit Sub
                End If
            Next



            If GridView1.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1

                    dr = dt.NewRow

                    If Me.txtActividad.Text = GridView1.Rows(i).Cells(2).Text Then
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar una actividad diferente');", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Ficha_ActDiferente() ;", True)

                        Exit Sub
                    End If
                    'dr("NO.") = GridView1.Rows(i).Cells(1).Text
                    dr("HORA") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(1).Text)
                    dr("PROGRAMA") = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)

                    dt.Rows.Add(dr)
                Next
            End If




            dr = dt.NewRow

            'dr("NO.") = consec
            dr("HORA") = HttpUtility.HtmlDecode(Me.txtHr.Text)
            dr("PROGRAMA") = HttpUtility.HtmlDecode(Me.txtActividad.Text)


            dt.Rows.Add(dr)
            ds.Tables.Add(dt)
            Session.Add("tabses1", dt)
            'GridView1.DataSource = ds.Tables(0)
            dt.DefaultView.Sort = "HORA ASC"
            GridView1.DataSource = dt
            GridView1.DataBind()


            Me.txtHr.Text = ""
            Me.txtActividad.Text = ""


            Me.txtHr.Focus()

        End If
    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim tabfila As New DataTable
        tabfila = Session("tabses1")
        Dim index As Integer = -1
        '3 '1 antes checaba hora(.cells(1)) ahora checara actividad(.cells(2))
        index = Buscar_Indice(HttpUtility.HtmlDecode(GridView1.Rows(e.RowIndex).Cells(2).Text), tabfila)
        If index <> -1 Then
            tabfila.Rows.RemoveAt(index)
            GridView1.DataSource = tabfila
            GridView1.DataBind()
            Session("tabses1") = tabfila

        End If




        ' ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)
    End Sub


    Public Function Buscar_Indice(ByVal textobusqueda As String, ByVal tabfila As DataTable) As Integer


        Dim iindice As Integer = -1
        Dim encontrado As Boolean = False
        Dim contador As Integer = 0
        Dim row As DataRow



        While encontrado = False And contador <= tabfila.Rows.Count


            row = tabfila.Rows(contador)
            'row(0)'revisaba hora, ahora sera actividad
            If (row(1) = textobusqueda) Then
                encontrado = True
            End If

            iindice = contador
            contador = contador + 1

        End While
        Return iindice
    End Function



    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click

        If Me.txtFolio.Text = "" Then
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar el folio');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Ficha_ErrorFolioIngresado();", True)
            Exit Sub
        End If

        If Me.txtEmpresa.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de capturar la empresa ');", True)
            Exit Sub
        End If

        If Me.txtNombre.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de capturar el nombre ');", True)
            Exit Sub
        End If

        If Me.txtPuesto.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de capturar el puesto ');", True)
            Exit Sub
        End If


        If Me.txtTelefono.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor capturar el telefono');", True)
            Exit Sub
        End If

        If Len(Me.txtTelefono.Text) < 8 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor capturar un telefono válido');", True)
            Exit Sub
        End If

        If Me.txtContacto.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de capturar el Contacto');", True)
            Exit Sub
        Else




            Dim dt As New DataTable
            dt = Session("tabses2")
            Dim dr As DataRow




            dt = New DataTable
            dt.Columns.Add("EMPRESA")
            dt.Columns.Add("NOMBRE")
            dt.Columns.Add("PUESTO")
            dt.Columns.Add("TELEFONO")
            dt.Columns.Add("CONTACTO")
            Session.Add("Tabla", dt)


            If GridView2.Rows.Count > 0 Then
                For i As Integer = 0 To GridView2.Rows.Count - 1

                    dr = dt.NewRow

                    ' If Me.txtEmpresa.Text = GridView2.Rows(i).Cells(1).Text Then
                    If Me.txtNombre.Text = GridView2.Rows(i).Cells(2).Text Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar un Nombre diferente');", True)
                        Exit Sub
                    End If
                    dr("EMPRESA") = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(1).Text)
                    dr("NOMBRE") = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(2).Text)
                    dr("PUESTO") = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(3).Text)
                    dr("TELEFONO") = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(4).Text)
                    dr("CONTACTO") = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(5).Text)

                    dt.Rows.Add(dr)
                Next
            End If




            dr = dt.NewRow

            dr("EMPRESA") = HttpUtility.HtmlDecode(Me.txtEmpresa.Text)
            dr("NOMBRE") = HttpUtility.HtmlDecode(Me.txtNombre.Text)
            dr("PUESTO") = HttpUtility.HtmlDecode(Me.txtPuesto.Text)
            dr("TELEFONO") = HttpUtility.HtmlDecode(Me.txtTelefono.Text)
            dr("CONTACTO") = HttpUtility.HtmlDecode(Me.txtContacto.Text)

            dt.Rows.Add(dr)
            ds.Tables.Add(dt)
            Session.Add("tabses2", dt)
            'GridView1.DataSource = ds.Tables(0)
            GridView2.DataSource = dt
            GridView2.DataBind()




            Me.txtEmpresa.Text = ""
            Me.txtNombre.Text = ""
            Me.txtPuesto.Text = ""
            Me.txtTelefono.Text = ""
            Me.txtContacto.Text = ""


            Me.txtEmpresa.Focus()




        End If
    End Sub

    Protected Sub GridView2_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting
        Dim tabfila2 As New DataTable

        tabfila2 = Session("tabses2")
        Dim index2 As Integer = -1
        '3
        'index2 = Buscar_Indice2(GridView2.Rows(e.RowIndex).Cells(1).Text, tabfila2)
        index2 = Buscar_Indice2(HttpUtility.HtmlDecode(GridView2.Rows(e.RowIndex).Cells(2).Text), tabfila2)
        If index2 <> -1 Then
            tabfila2.Rows.RemoveAt(index2)
            GridView2.DataSource = tabfila2
            GridView2.DataBind()
            Session("tabses2") = tabfila2

        End If


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)
    End Sub
    Public Function Buscar_Indice2(ByVal textobusqueda2 As String, ByVal tabfila2 As DataTable) As Integer


        Dim iindice2 As Integer = -1
        Dim encontrado2 As Boolean = False
        Dim contador2 As Integer = 0
        Dim row2 As DataRow



        While encontrado2 = False And contador2 <= tabfila2.Rows.Count


            row2 = tabfila2.Rows(contador2)
            'row(0)
            If (row2(1) = textobusqueda2) Then
                encontrado2 = True
            End If

            iindice2 = contador2
            contador2 = contador2 + 1

        End While
        Return iindice2
    End Function



    'Protected Sub drpAvtividad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpAvtividad.SelectedIndexChanged
    '    Me.lblidActividad.Text = Me.drpAvtividad.SelectedValue
    'End Sub



    Protected Sub txtHr_TextChanged(sender As Object, e As System.EventArgs) Handles txtHr.TextChanged
        If Len(Me.txtHr.Text) <> 5 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)
        End If

        Me.txtActividad.Focus()

    End Sub


    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        'Session("ConsecAct") = 0
        Me.txtFolio.Enabled = True
        limpiar()
        deshailitar()
    End Sub



    Sub limpiar()
        Me.txtFolio.Text = ""
        Me.lblFolioEvento.Text = ""
        Me.txtAntecedentes.Text = ""
        Me.txtActividad.Text = ""
        Me.txtHr.Text = ""
        Me.txtEmpresa.Text = ""
        Me.txtNombre.Text = ""
        Me.txtPuesto.Text = ""
        Me.txtTelefono.Text = ""
        Me.txtContacto.Text = ""
        Me.GridView1.DataSourceID = Nothing
        Me.GridView1.DataSource = Nothing
        Me.GridView1.DataBind()
        Me.GridView2.DataSourceID = Nothing
        Me.GridView2.DataSource = Nothing
        Me.GridView2.DataBind()
        Me.GridView3.DataSourceID = Nothing
        Me.GridView3.DataSource = Nothing
        Me.GridView3.DataBind()

    End Sub
    Sub deshailitar()
        Me.txtAntecedentes.Enabled = False
        Me.drpVestimenta.Enabled = False
        Me.txtActividad.Enabled = False
        Me.txtHr.Enabled = False
        Me.GridView3.Enabled = False
        Me.txtEmpresa.Enabled = False
        Me.txtNombre.Enabled = False
        Me.txtPuesto.Enabled = False
        Me.txtTelefono.Enabled = False
        Me.txtContacto.Enabled = False

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click


        Dim con As New conexion
        Dim stry As String



        Dim sicheco As Boolean = False
        For Each row As GridViewRow In GridView3.Rows
            Dim listaAux As New ArrayList()
            Dim chkcampo As CheckBox = CType(row.FindControl("CheckBox2"), CheckBox)
            Dim var1 = chkcampo.Checked
            If chkcampo.Checked Then
                ' si esta checado al menso 1 vez
                sicheco = True
            End If
        Next

        If Me.GridView1.Rows.Count <> 0 And Me.txtFolio.Text <> "" And Me.txtAntecedentes.Text <> "" And Me.drpVestimenta.Text <> "" And sicheco = True Then

            'checa si existe datos en reg ficha 
            stry = "SELECT * from [eventos].reg_ficha where folio_evento = " & Me.txtFolio.Text & ""
            tabla = conexion.sqlcon(stry)
            If tabla.Rows.Count < 1 Then
                'sino existe datos eN reg ficha inserta en reg ficha y se ejecuta el trigger de insertar en envio correo y se inserta tabla por tabla en codigo vb
                crearfolioFicha()
                stry = "INSERT INTO [eventos].reg_ficha VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & Trim(Me.txtAntecedentes.Text) & "','" & Trim(Me.drpVestimenta.Text) & "',getdate()," & Session("clave_empl") & ")"
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                    Exit Sub
                Else

                    GuardaPrograma()
                    GuardaInivtados() 'Se ejecuta trigger inserta_enviocorreo_invitados
                    GuardaInivtadosEsp() 'Se ejecuta trigger inserta_enviocorreo_invitados_esp
                    'Session("ConsecAct") = 0 

                    ''GUARDA EL REGISTRO EN REG EVENTO
                    'stry = String.Format("UPDATE [eventos].reg_evento SET ficha='{0}' WHERE folio = {1}", True, txtFolio.Text)
                    'conexion.sqlcambios(stry)

                    Me.txtFolio.Enabled = True
                    limpiar()
                    deshailitar()

#Region "GUARDA LOG"
                    Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado le Ficha de el evento con Folio {0}", folio))
#End Region
                    ' ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alert('Se guardo la informacion con Éxito');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito();", True)
                End If
            Else
                'si existe datos en reg ficha,borra la info de [eventos].reg_ficha actual y manda ejecutar el trigger de delete a programa, invitados e invitados_esp
                'e inserta en reg ficha y se ejecuta el trigger de insertar en envio correo y se inserta tabla por tabla en codigo
                stry = "DELETE from [eventos].reg_ficha where folio_evento = " & Me.txtFolio.Text & ""
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                    Exit Sub
                Else
                    crearfolioFicha()
                    stry = "INSERT INTO [eventos].reg_ficha VALUES (" & Trim(folio) & ",'" & Trim(Me.txtFolio.Text) & "','" & Trim(Me.txtAntecedentes.Text) & "','" & Trim(Me.drpVestimenta.Text) & "',getdate()," & Session("clave_empl") & ")"
                    resultado = conexion.sqlcambios(stry)
                    If resultado = -1 Then
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                        Exit Sub
                    Else
                        'ELIMINA LOS ELMENTOS DE FICHA
                        Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@folio_ficha", folio)}
                        Using data As New DB(con.Conectar())
                            data.EjecutaCommand("EliminaRegistrosFichaEvento", params)
                        End Using

                        GuardaPrograma()
                        GuardaInivtados() 'S
                        GuardaInivtadosEsp() '
                        'Session("ConsecAct") = 0 

                        ''GUARDA EL REGISTRO EN REG EVENTO
                        'stry = String.Format("UPDATE [eventos].reg_evento SET ficha='{0}' WHERE folio = {1}", True, txtFolio.Text)
                        'conexion.sqlcambios(stry)

                        'ENVIA LOS CORREOS DE LAS SECRETARIAS INVOLUCRADAS
                        Using notificacionesHelper As New IntelipolisEngine.Helper.NotificationHelper
                            notificacionesHelper.EnviaNotificacionDireccionesInvitadas(txtFolio.Text)
                        End Using

#Region "GUARDA LOG"
                        Helper.GuardaLog(Session("clave_empl"), String.Format("Ha guardado la Ficha del evento con Folio {0}", folio))
#End Region

                        'For Each row As GridViewRow In GridView3.Rows
                        '    Dim listaAux As New ArrayList()
                        '    Dim chkcampo As CheckBox = CType(row.FindControl("CheckBox2"), CheckBox)
                        '    Dim lblidsecr As Label = CType(row.FindControl("lblidsecr"), Label)
                        '    Dim var1 = chkcampo.Checked
                        '    If chkcampo.Checked Then

                        '        Using data As New DB(con.Conectar())

                        '            'Obtiene informacion de Correo
                        '            Dim paramsD() As SqlParameter = New SqlParameter() {New SqlParameter("@cve_secr", lblidsecr.Text), New SqlParameter("@folio_evento", Me.txtFolio.Text)}
                        '            Dim dt = data.ObtieneDatos("ObtieneFormatoCorreoSecretarias", paramsD).Tables(0)

                        '            Dim helper As New Helper
                        '            helper.EnviaCorreo(dt.Rows(0)("correo"), "Nuevo evento programado para secretaría " & dt.Rows(0)("Asunto"), dt.Rows(0)("Mensaje"))

                        '        End Using


                        '        'recorrer el grid template
                        '        stry = "INSERT INTO [eventos].invitados VALUES (" & Trim(folio) & "," & lblidsecr.Text & ")"
                        '        'Dim cmd1 As New Data.SqlClient.SqlCommand(stry, con.Conectar)
                        '        'cmd1.ExecuteNonQuery()
                        '        resultado = conexion.sqlcambios(stry)
                        '        If resultado = -1 Then
                        '            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                        '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                        '            Exit Sub

                        '        End If
                        '    End If
                        'Next

                        Me.txtFolio.Enabled = True
                        limpiar()
                        deshailitar()

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito();", True)
                    End If


                End If

            End If

        Else

            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alert('Favor de capturar todos los datos');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_DatosFaltantes();", True)
        End If

    End Sub




    Sub GuardaPrograma()
        'ACTIVIDADES 
        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1

                Dim HORA = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(1).Text)
                Dim PROGRAMA = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
                stry = "INSERT INTO [eventos].programa VALUES (" & Trim(folio) & ",'" & Trim(HORA) & "','" & Trim(PROGRAMA) & "')"
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                    Exit Sub
                End If

            Next

        End If
    End Sub

    Sub GuardaInivtados()

        'INVITADOS

        For Each row As GridViewRow In GridView3.Rows
            Dim listaAux As New ArrayList()
            Dim chkcampo As CheckBox = CType(row.FindControl("CheckBox2"), CheckBox)
            Dim lblidsecr As Label = CType(row.FindControl("lblidsecr"), Label)
            Dim var1 = chkcampo.Checked
            If chkcampo.Checked Then
                'recorrer el grid template
                stry = "INSERT INTO [eventos].invitados VALUES (" & Trim(folio) & "," & lblidsecr.Text & ")"
                'Dim cmd1 As New Data.SqlClient.SqlCommand(stry, con.Conectar)
                'cmd1.ExecuteNonQuery()
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                    Exit Sub

                End If
            End If
        Next




        'VALIDA QUE SI SELECCIONO ALCALDE , YA NO SE LE ENVIE A CLARA, SINO, LE PUEDE LLEGAR LA INVITACION

        stry = "SELECT alcalde FROM [eventos].reg_evento WHERE folio = " & Me.txtFolio.Text & ""
        tabla = conexion.sqlcon(stry)
        If tabla.Rows.Count < 1 Then
            'no existe info
        Else
            Dim alcalde As Boolean = tabla.Rows(0)("alcalde").ToString
            If alcalde = True Then
                'se inserta sin el correo de Clara
                InsertaCorreosInvitadosSinClara()
            Else
                'se isnerta todos los correos
                InsertaCorreosInvitados()
            End If
        End If






    End Sub

    Sub InsertaCorreosInvitadosSinClara()
        'VERIFICA SI HAY INVITADOS, SI LOS HAY SE INSERTA A ENVIA CORREO CON FORMATO 2 (FICHA)
        stry = "SELECT email  FROM [eventos].invitados a " &
                "INNER JOIN [eventos].email_secretarias b " &
                "ON a.invitados = b.id_secr " &
                "INNER JOIN [eventos].reg_ficha c " &
                "ON a.folio_ficha = c.folio_ficha " &
                "WHERE a.folio_ficha = " & Trim(folio) & " and c.folio_evento = " & Me.txtFolio.Text & " and email <> 'clara@claravillarreal.com'"
        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader()

        Try
            While tDrsx.Read
                Dim email_secr As String = tDrsx(0)
                stry = "INSERT INTO [eventos].envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "','" & Trim(email_secr) & "',0, null, 2)"
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                End If
            End While
        Finally
            tDrsx.Close()
        End Try
    End Sub



    Sub InsertaCorreosInvitados()
        'VERIFICA SI HAY INVITADOS, SI LOS HAY SE INSERTA A ENVIA CORREO CON FORMATO 2 (FICHA)
        stry = "SELECT email  FROM [eventos].invitados a " &
                "INNER JOIN [eventos].email_secretarias b " &
                "ON a.invitados = b.id_secr " &
                "INNER JOIN [eventos].reg_ficha c " &
                "ON a.folio_ficha = c.folio_ficha " &
                "WHERE a.folio_ficha = " & Trim(folio) & " and c.folio_evento = " & Me.txtFolio.Text & ""
        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader()

        Try
            While tDrsx.Read
                Dim email_secr As String = tDrsx(0)
                stry = "INSERT INTO [eventos].envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "','" & Trim(email_secr) & "',0, null, 2)"
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                End If
            End While
        Finally
            tDrsx.Close()
        End Try
    End Sub

    Sub GuardaInivtadosEsp()
        'INVITADOS ESPECIALES
        If GridView2.Rows.Count > 0 Then
            For i As Integer = 0 To GridView2.Rows.Count - 1
                Dim EMPRESA = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(1).Text)
                Dim NOMBRE = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(2).Text)
                Dim PUESTO = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(3).Text)
                Dim TELEFONO = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(4).Text)
                Dim CORREO = HttpUtility.HtmlDecode(GridView2.Rows(i).Cells(5).Text)
                stry = "INSERT INTO [eventos].invitados_esp VALUES (" & Trim(folio) & ",'" & Trim(EMPRESA) & "','" & Trim(NOMBRE) & "','" & Trim(PUESTO) & "','" & Trim(TELEFONO) & "','" & Trim(CORREO) & "')"
                resultado = conexion.sqlcambios(stry)
                If resultado = -1 Then
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                    Exit Sub
                End If

            Next

        End If


        ''VERIFICA SI HAY INVITADOS ESPECIALES , SI LOS HAY, INSERTA A ENVIA CORREO CON FORMATO 2

        'stry = "SELECT a.folio_ficha, a.correo,b.folio_evento " & _
        '"FROM invitados_esp a " & _
        '"INNER JOIN [eventos].reg_ficha b " & _
        '"ON a.folio_ficha = b.folio_ficha  " & _
        '"where a.folio_ficha = " & Trim(folio) & ""

        'Dim tRsGen2 As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        'Dim tDrsx2 As System.Data.SqlClient.SqlDataReader
        'tDrsx2 = tRsGen2.ExecuteReader()

        'Try
        '    While tDrsx2.Read
        '        Dim correo_esp As String = tDrsx2(1)
        '        stry = "INSERT INTO envia_correo VALUES('" & Trim(Me.txtFolio.Text) & "','" & Trim(correo_esp) & "',0, null, 2)"
        '        resultado = conexion.sqlcambios(stry)
        '        If resultado = -1 Then
        '        End If
        '    End While
        'Finally
        '    tDrsx2.Close()
        'End Try





    End Sub
    Sub crearfolioFicha()

        Session("folioFicha") = 0
        stry = "Select max(folio_ficha) as folio from [eventos].reg_ficha"
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            MsgBox("No se encontro la información")
        Else
            If (tabla.Rows(0)("folio").ToString) = "" Then
                folio = 1
                Session("folioFicha") = folio
                'Me.txtFolio.Text = folio
            Else
                folio = tabla.Rows(0)("folio").ToString
                folio = folio + 1
                Session("folioFicha") = folio
                'Me.txtFolio.Text = folio

            End If
        End If
    End Sub







    Sub SeleccionaTodosInvitados()


        Dim con As New conexion


        stry = "Select id_secr, secretaria,estatus = 1 from [eventos].secretarias order by 1"

        Dim da As New SqlDataAdapter(stry, con.Conectar)
        Dim dt As New DataTable()


        Try

            da.Fill(dt)
            Me.GridView3.DataSource = dt
            If dt.Rows.Count = 0 Then
                'no hay nada
                Me.GridView3.DataBind()
            Else
                'si trae
                Me.GridView3.DataBind()
            End If
        Catch ex As Exception
            MsgBox(da.Fill(dt))
        End Try


    End Sub

    Protected Sub GridView3_DataBound(sender As Object, e As System.EventArgs) Handles GridView3.DataBound
        '    Dim checoTodos As Boolean = False
        '    For Each row As GridViewRow In GridView3.Rows
        '        Dim checkTodos As CheckBox = CType(row.FindControl("CheckTodos"), CheckBox)
        '        If checkTodos.Checked Then
        '            'query seleccionando todas las secretarias
        '            SeleccionaTodosInvitados()
        '        Else
        '            'query sin seleccionar todas las secretarias
        '            cargaInvitados()
        '        End If
        '    Next
    End Sub

    Protected Sub Checked(sender As Object, e As EventArgs)


        Dim chk As CheckBox = DirectCast(GridView3.HeaderRow.FindControl("chkBxHeader"), CheckBox)

        For Each row As GridViewRow In GridView3.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim checkbox As CheckBox = DirectCast(row.FindControl("CheckBox2"), CheckBox)

                If chk.Checked Then
                    checkbox.Checked = True
                Else
                    checkbox.Checked = False
                End If
            End If
        Next


        'Dim activeCheckBox = TryCast(sender, CheckBox)

        'If activeCheckBox IsNot Nothing Then

        '    Dim isChecked = activeCheckBox.Checked
        '    Dim tempCheckBox = New CheckBox()



        '    For Each gvRow As GridViewRow In GridView3.Rows
        '        tempCheckBox = TryCast(gvRow.FindControl("chkBxHeader"), CheckBox)
        '        If tempCheckBox IsNot Nothing Then
        '            tempCheckBox.Checked = Not isChecked
        '        End If
        '    Next



        '    If isChecked Then
        '        activeCheckBox.Checked = True
        '    End If



        'End If

        'Dim activeCheckBox = TryCast(sender, CheckBox)

        'If activeCheckBox IsNot Nothing Then

        '    Dim isChecked = activeCheckBox.Checked
        '    If isChecked = True Then
        '        'query seleccionando todas las secretarias
        '        SeleccionaTodosInvitados()
        '        Dim tempCheckBox = New CheckBox()
        '        'tempCheckBox = TryCast(GridView3.FindControl("chkBxHeader"), CheckBox)
        '        tempCheckBox.Checked = True

        '    Else
        '        'query sin seleccionar todas las secretarias
        '        cargaInvitados()
        '        Dim tempCheckBox = New CheckBox()
        '        tempCheckBox.Checked = False
        '    End If

        ' End If


    End Sub







End Class
