Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion
Partial Class Cambios2
    Inherits System.Web.UI.Page

    Dim stry As String
    Dim folio As Integer
    Dim tabla As DataTable
    Dim clave_secr As Integer

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
    Dim fecha_captura As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Consulta evento")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("paso") = "1" Then

        Else
            Response.Redirect("~/Password.aspx")
        End If


        If Not IsPostBack = True Then

            'corx = Request.QueryString("field1")
            'txt_coord_x.Text = corx
            'cory = Request.QueryString("field2")
            'txt_coord_y.Text = cory

            'PonePunto()

            'llenaCBO()
            Dim ColoEnviada As String
            Dim IDCOL As Integer

            ColoEnviada = Request.QueryString("IDCOL")
            IDCOL = Session("IDCOL")
            ColoEnviada = IDCOL
            If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)
            DibujarCol()



            usuario()
            inicia()
            'cargaCombos()

            CargaInfoEvento(Request.QueryString("FolioSS"))
            'Me.txtfolio.Text = Request.QueryString("FolioSS")
            'ConsultarR(txtfolio.Text)

            ''VALIDA SI ES READ ONLY
            'Dim soloLectura = Request.QueryString("readOnly")
            'If Not soloLectura Is Nothing Then
            '    Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
            '    masterPage()
            'End If
        End If

    End Sub

    Private Sub CargaInfoEvento(folio As Integer)
        Dim con As New Class1
        'Carga el evento
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@folio", folio)
            }

            Try
                Dim dr = data.ObtieneDatos("ObtieneEventoPorFolio", params).Tables(0).Rows(0)
                lblActividad.Text = dr("Linea")
                lblArriboAlcalde.Text = dr("hr_alcalde")
                lblAsistiraAlcalde.Text = dr("Alcalde")
                lblAsistiraPrensa.Text = dr("Prensa")
                lblCalle.Text = dr("Calle")
                lblColonia.Text = dr("Colonia")
                lblCorreoOperador.Text = dr("correo_ope")
                lblDescripcion.Text = dr("Descripcion")
                lblDireccion.Text = dr("Nombr_direccion")
                lblEmpleado.Text = dr("operador")
                lblEventoPresencial.Text = dr("esPresencial")
                lblFechaFin.Text = dr("fechaFin")
                lblFechaInicio.Text = dr("Fecha")
                lblFolio.Text = dr("Folio")
                lblHoraInicio.Text = dr("hr_ini")
                lblHoraSalidaAlcalde.Text = dr("hr_salida")
                lblHoraTermino.Text = dr("hr_fin")
                lblLugar.Text = dr("lugar")
                lblNombreEvento.Text = dr("Nombre")
                lblNumBeneficiados.Text = dr("num_benef")
                lblNumCiudadanos.Text = dr("Aforo")
                lblNumExterior.Text = dr("Num_ext")
                lblNumInteior.Text = dr("Num_int")
                lblPuesto.Text = dr("puesto_ope")
                lblResponsableEvento.Text = dr("responsable")
                lblSecretaria.Text = dr("Nombr_secretaria")
                lblServiciosPublicos.Text = dr("ServPublicos")
                lblSubActividad.Text = dr("Subactividad")
                lblTelefonoOperador.Text = dr("telefono_ope")
                lblTelefonoResponsable.Text = dr("telefono")
                lblTipoEvento.Text = dr("Tipo")

            Catch ex As Exception

            End Try
        End Using
    End Sub

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

    Sub inicia()
        'limpiar()
        ''creaFolio()
        'Invisibles()
    End Sub

    'Sub cargaCombos()


    '    'cargaSecretarias


    '    If Session("privilegio") = 1 Then  'todas las secretarias
    '        'Exportar a Excel
    '        'Me.btnExportar.Visible = True
    '        stry = "select '' id_secr, '' secretaria UNION select id_secr,secretaria from eventos.secretarias order by id_secr"
    '        tabla = conexion.sqlcon(stry)
    '    Else
    '        clave_secr = Session("id_secr") 'solo la secretaria con que se dio de alta
    '        stry = "select '' id_secr, '' secretaria UNION select id_secr,secretaria from eventos.secretarias where id_secr = " & clave_secr & " order by id_secr"
    '        tabla = conexion.sqlcon(stry)
    '    End If

    '    If tabla.Rows.Count < 1 Then
    '        'MsgBox("No se encontro la información")
    '        Me.ddl2.Items.Clear()
    '        Me.ddl2.Items.Add("")
    '    Else
    '        ddl2.DataSource = tabla
    '        ddl2.DataTextField = "secretaria"
    '        ddl2.DataValueField = "id_secr"
    '        ddl2.DataBind()

    '    End If

    '    'carga Tipo Eventos
    '    stry = "select * from eventos.evento order by 1"
    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then

    '        Me.DropDownList2.Items.Clear()
    '        Me.DropDownList2.Items.Add("")
    '    Else
    '        Me.DropDownList2.DataSource = tabla
    '        Me.DropDownList2.DataTextField = "evento"
    '        Me.DropDownList2.DataValueField = "id_evento"
    '        Me.DropDownList2.DataBind()
    '    End If


    '    'cargaColonias
    '    stry = "select c.id_col,col.nombr_colonia from eventos.calles c INNER join [eventos].Xcolonias  col ON c.id_col=col.id_colonia group by c.id_col,col.nombr_colonia order by col.nombr_colonia  "
    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then
    '        'MsgBox("No se encontro la información")
    '        Me.ddl4.Items.Clear()
    '        Me.ddl4.Items.Add("")
    '    Else
    '        ddl4.DataSource = tabla
    '        ddl4.DataTextField = "NOMBR_colonia"
    '        ddl4.DataValueField = "id_col"
    '        ddl4.DataBind()
    '        IDCOL = Me.ddl4.Text
    '        'clave_colo = Me.ddl4.Text

    '    End If









    'End Sub

    'Sub CargarCallesColonia()
    '    'cargaCalles
    '    'stry = "select id_calle,calle from calles where id_COL=" & clave_colo & " order by calle"
    '    IDCOL = Session("IDCOL")
    '    stry = "select id_calle,calle from eventos.calles where id_COL=" & IDCOL & " order by calle"

    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then
    '        'MsgBox("No se encontro la información")
    '        Me.ddl5.Items.Clear()
    '        Me.ddl5.Items.Add("")
    '    Else

    '        Me.ddl5.DataSource = tabla
    '        Me.ddl5.DataTextField = "calle"
    '        Me.ddl5.DataValueField = "id_calle"
    '        Me.ddl5.DataBind()
    '        'Me.ddl5.Text = tabla.Rows(0)("id_calle")
    '    End If
    'End Sub

    'Sub CargarCalles()
    '    'cargaCalles
    '    stry = "select id_calle,calle from eventos.calles where id_COL=" & clave_colo & " and id_calle = " & clave_calle & " order by calle"

    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then
    '        'MsgBox("No se encontro la información")
    '        Me.ddl5.Items.Clear()
    '        Me.ddl5.Items.Add("")
    '    Else

    '        Me.ddl5.DataSource = tabla
    '        Me.ddl5.DataTextField = "calle"
    '        Me.ddl5.DataValueField = "id_calle"
    '        Me.ddl5.DataBind()
    '        'Me.ddl5.Text = tabla.Rows(0)("id_calle")
    '    End If
    'End Sub

    'Sub CargarDepe()
    '    'cargaCalles


    '    stry = "  select clave_depe,nombre_depe from eventos.direcciones where clave_secr=" & clave_secr & " and clave_depe = " & clave_depe & " order by clave_depe"
    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then
    '        'MsgBox("No se encontro la información")
    '        Me.ddl3.Items.Clear()
    '        Me.ddl3.Items.Add("")
    '    Else

    '        Me.ddl3.DataSource = tabla
    '        Me.ddl3.DataTextField = "nombre_depe"
    '        Me.ddl3.DataValueField = "clave_depe"
    '        Me.ddl3.DataBind()
    '        'Me.ddl5.Text = tabla.Rows(0)("id_calle")
    '    End If
    'End Sub

    'Sub limpiar()


    '    Me.txtnumbenef.Text = ""
    '    Me.txtext.Text = ""
    '    Me.txtint.Text = ""
    '    Me.txtfecha.Text = ""
    '    Me.txthrini.Text = ""
    '    Me.txthrfin.Text = ""
    '    Me.txthralcalde.Text = ""
    '    Me.txtHrSalidaAlcalde.Text = ""
    '    Me.txtEvento.Text = ""
    '    Me.txtDescripcion.Text = ""
    '    Me.txtLugar.Text = ""
    '    Me.CheckPrensa.Checked = False
    '    Me.CheckAlcalde.Checked = False
    '    Me.CheckLimpieza.Checked = False
    '    Me.txtResponsableEvento.Text = ""
    '    Me.txtTelefonoResponsable.Text = ""
    '    Me.txtOperadopor.Text = ""
    '    Me.txtPuestoOperador.Text = ""
    '    Me.txtTelefonoOperador.Text = ""
    '    Me.txtCorreoOperador.Text = ""



    'End Sub

    'Sub Invisibles()
    '    'Me.label13.Visible = False
    '    Me.label16.Visible = False
    '    Me.label14.Visible = False
    '    Me.label15.Visible = False
    '    Me.label17.Visible = False
    '    Me.label18.Visible = False
    '    'Me.label1.Visible = False
    '    Me.label23.Visible = False



    'End Sub
    'Sub habilitar()



    '    Me.ddl2.Enabled = True
    '    Me.ddl3.Enabled = True
    '    Me.ddl4.Enabled = True
    '    Me.ddl5.Enabled = True
    '    Me.DropDownList2.Enabled = True
    '    Me.txtEvento.Enabled = True
    '    Me.txtDescripcion.Enabled = True
    '    Me.txtLugar.Enabled = True
    '    Me.txtEvento.Enabled = True
    '    Me.txtext.Enabled = True
    '    Me.txtint.Enabled = True
    '    Me.txtfecha.Enabled = True
    '    Me.txthrini.Enabled = True
    '    Me.txthrfin.Enabled = True
    '    Me.txthralcalde.Enabled = True
    '    Me.txtHrSalidaAlcalde.Enabled = True
    '    Me.txtnumbenef.Enabled = True
    '    Me.CheckPrensa.Enabled = True
    '    Me.CheckAlcalde.Enabled = True
    '    Me.CheckLimpieza.Enabled = True
    '    Me.txtResponsableEvento.Enabled = True
    '    Me.txtTelefonoResponsable.Enabled = True
    '    Me.txtOperadopor.Enabled = True
    '    Me.txtPuestoOperador.Enabled = True
    '    Me.txtTelefonoOperador.Enabled = True
    '    Me.txtCorreoOperador.Enabled = True



    'End Sub
    'Sub deshabilita()
    '    Me.txtEvento.Enabled = False
    '    Me.ddl2.Enabled = False
    '    Me.ddl3.Enabled = False
    '    Me.ddl4.Enabled = False
    '    Me.ddl5.Enabled = False
    '    Me.DropDownList2.Enabled = False
    '    Me.txtext.Enabled = False
    '    Me.txtint.Enabled = False
    '    Me.txtfecha.Enabled = False
    '    Me.txtnumbenef.Enabled = False
    '    Me.txtDescripcion.Enabled = False
    '    Me.txtLugar.Enabled = False
    '    Me.txthrini.Enabled = False
    '    Me.txthrfin.Enabled = False
    '    Me.txthralcalde.Enabled = False
    '    Me.txtHrSalidaAlcalde.Enabled = False
    '    Me.CheckPrensa.Enabled = False
    '    Me.CheckAlcalde.Enabled = False
    '    Me.CheckLimpieza.Enabled = False
    '    Me.txtResponsableEvento.Enabled = False
    '    Me.txtTelefonoResponsable.Enabled = False
    '    Me.txtOperadopor.Enabled = False
    '    Me.txtPuestoOperador.Enabled = False
    '    Me.txtTelefonoOperador.Enabled = False
    '    Me.txtCorreoOperador.Enabled = False

    'End Sub
    ''Sub consulta()



    ''    ''Dim stry2 As String
    ''    ''Dim rs2 As SqlClient.SqlDataReader
    ''    stry = "SELECT r.folio,r.id_origen,r.id_secr,r.id_evento,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,Convert(char,r.fecha, 111) as fecha,r.num_benef FROM [eventos].reg_evento r INNER JOIN calles c ON r.id_calle =c.id_calle WHERE r.folio ='" & modiFolio & "' Group by r.folio,r.id_origen,r.id_secr,r.id_evento,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,r.fecha,r.num_benef"
    ''    ''Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.Conectar())
    ''    ''rs2 = cmd2.ExecuteReader()
    ''    ''rs2.Read()

    ''    tabla = conexion.sqlcon(stry)
    ''    If tabla.Rows.Count < 1 Then
    ''        Me.lblmsjalta.Text = "No Existe el Folio"
    ''        Me.lblmsjalta.Visible = True
    ''        deshabilita()
    ''    Else

    ''        Try

    ''            If IsDBNull(Me.tabla.Rows(0)("folio")) Then
    ''                Me.txtfolio.Text = ""
    ''            Else
    ''                Me.txtfolio.Text = tabla.Rows(0)("folio")
    ''            End If
    ''            If IsDBNull(tabla.Rows(0)("id_origen")) Then
    ''                Me.ddl1.Text = ""
    ''            Else
    ''                Me.ddl1.Text = tabla.Rows(0)("id_origen")
    ''            End If

    ''            If IsDBNull(Me.tabla.Rows(0)("id_secr")) Then
    ''                Me.ddl2.Text = ""
    ''            Else
    ''                Me.ddl2.Text = tabla.Rows(0)("id_secr")
    ''            End If

    ''            If IsDBNull(Me.tabla.Rows(0)("id_evento")) Then
    ''                Me.ddl3.Text = ""
    ''            Else
    ''                Me.ddl3.Text = tabla.Rows(0)("id_evento")
    ''            End If
    ''            If IsDBNull(Me.tabla.Rows(0)("id_col")) Then
    ''                Me.ddl4.Text = ""
    ''            Else
    ''                Me.ddl4.Text = tabla.Rows(0)("id_col")
    ''                IDCOL = Me.ddl4.Text
    ''            End If

    ''            If IsDBNull(Me.tabla.Rows(0)("num_ext")) Then
    ''                Me.txtext.Text = ""
    ''            Else
    ''                Me.txtext.Text = tabla.Rows(0)("num_ext")
    ''            End If

    ''            If IsDBNull(Me.tabla.Rows(0)("num_int")) Then
    ''                Me.txtint.Text = ""
    ''            Else
    ''                Me.txtint.Text = tabla.Rows(0)("num_int")
    ''            End If

    ''            If IsDBNull(Me.tabla.Rows(0)("fecha")) Then
    ''                Me.txtfecha.Text = ""
    ''            Else
    ''                Me.txtfecha.Text = tabla.Rows(0)("fecha")
    ''            End If
    ''            If IsDBNull(tabla.Rows(0)("num_benef")) Then
    ''                Me.txtnumbenef.Text = ""
    ''            Else
    ''                Me.txtnumbenef.Text = tabla.Rows(0)("num_benef")
    ''            End If

    ''            If IsDBNull(tabla.Rows(0)("num_benef")) Then
    ''                Me.txtnumbenef.Text = ""
    ''            Else
    ''                Me.txtnumbenef.Text = tabla.Rows(0)("num_benef")
    ''            End If

    ''            If IsDBNull(Me.tabla.Rows(0)("id_calle")) Then
    ''                Me.ddl5.Text = ""
    ''            Else
    ''                'Me.ddl5.Text = tabla.Rows(0)("id_calle")
    ''                clave_calle = tabla.Rows(0)("id_calle")
    ''                CargarCalles()
    ''            End If


    ''            'If IsDBNull(Me.tabla.Rows(0)("calle")) Then
    ''            '    Me.ddl5.Text = ""
    ''            'Else
    ''            '    'CargarCalles()
    ''            '    'Dim clave_calle As Integer
    ''            '    'clave_calle = tabla.Rows(0)("id_calle")
    ''            '    'Me.DropDownList1.SelectedItem.Text = tabla.Rows(0)("id_calle")
    ''            '    Me.ddl5.Items.Clear()
    ''            '    'Me.DropDownList1.Items.Add("")
    ''            '    Dim i As Integer ' se crea la variable i para llenar el listbox
    ''            '    i = 0
    ''            '    For Each row As DataRow In tabla.Rows ' sirve para recorrer toda la tabla
    ''            '        Me.ddl5.Items.Add(tabla.Rows(i)("calle").ToString())
    ''            '        i = i + 1
    ''            '    Next
    ''            'End If

    ''            Session("cambio") = 1
    ''            habilitar()
    ''        Catch ex As Exception
    ''            'No existe ese folio
    ''            Me.lblmsjalta.Text = "No Existe el Folio"
    ''            Me.lblmsjalta.Visible = True
    ''            deshabilita()
    ''        End Try
    ''    End If
    ''End Sub
    'Sub ExportarExcel()

    '    'stry = "SELECT folio as FOLIO, origen as ORIGEN,secretaria as SECRETARIA,evento AS EVENTO,colonia AS COLONIA,calle AS CALLE,num_ext AS NUM_EXT,num_int AS NUM_INT,Convert(char,fecha, 111) AS FECHA_EVENTO,num_benef as NUM_BENEFICIADOS  FROM [eventos].reg_evento re INNER JOIN origen o ON re.id_origen = o.id_origen INNER JOIN secretarias sec ON re.id_secr = sec.id_secr INNER JOIN colonias col ON re.id_col=col.id_col INNER JOIN calles c ON re.id_calle=c.id_calle INNER JOIN evento  e ON re.id_evento = e.id_evento Group BY  folio,origen,secretaria,evento,colonia,calle,num_ext,num_int,fecha,num_benef ORDER BY re.folio "
    '    stry = "SELECT re.folio as FOLIO, origen as ORIGEN,secretaria as SECRETARIA,evento AS EVENTO,nombr_colonia AS COLONIA,calle AS CALLE,num_ext AS NUM_EXT,num_int AS NUM_INT,Convert(char,fecha, 111) AS FECHA_EVENTO,num_benef as NUM_BENEFICIADOS,coordx as COORD_X,coordy AS COORD_Y " &
    '            "FROM [eventos].reg_evento re " &
    '            "INNER JOIN eventos.origen o ON re.id_origen = o.id_origen " &
    '            "INNER JOIN eventos.secretarias sec ON re.id_secr = sec.id_secr " &
    '            "INNER join [eventos].Xcolonias  col ON re.id_col=col.id_colonia " &
    '            "INNER JOIN eventos.calles c ON re.id_calle=c.id_calle " &
    '            "INNER JOIN eventos.evento  e ON re.id_evento = e.id_evento " &
    '            "INNER JOIN [eventos].map_evento m ON re.folio=m.folio " &
    '            "Group BY  re.FOLIO,o.origen,sec.secretaria,e.evento,col.nombr_colonia,c.calle,re.num_ext,re.num_int,re.fecha,re.num_benef,m.coordx,m.coordy ORDER BY re.folio"



    '    ''-----------------NUEVO QUERY POR LOS NUEVOS CAMBIOS, POR SI QUIEREN EXPORTAR LA INFORMACON

    '    '        SELECT 
    '    ' re.folio as FOLIO,re.nombre_evento AS EVENTO,sec.secretaria as SECRETARIA,dir.nombre_depe as DIRECCION, e.evento as TIPO_EVENTO,
    '    ' re.descripcion as DESCRIPCION,col.nombr_colonia AS COLONIA,c.calle AS CALLE,re.num_ext AS NUM_EXT,re.num_int AS NUM_INT,
    '    ' Convert(char,re.fecha, 111) AS FECHA_EVENTO,re.hora as HORA_EVENTO ,re.num_benef as NUM_BENEFICIADOS,m.coordx as COORD_X,m.coordy AS COORD_Y ,
    '    ' PRENSA = CASE re.prensa 
    '    '        WHEN '0' THEN 'SIN PRENSA'
    '    '        WHEN '1' THEN 'CON PRENSA'

    '    '            End
    '    'FROM [eventos].reg_evento re
    '    'INNER JOIN evento e ON re.id_evento = e.id_evento
    '    'INNER JOIN secretarias sec ON re.id_secr = sec.id_secr 
    '    'INNER JOIN direcciones dir ON re.id_depe = dir.clave_depe and re.id_secr =dir.clave_secr  
    '    'INNER join [eventos].Xcolonias  col ON re.id_col=col.id_colonia 
    '    'INNER JOIN calles c ON re.id_calle=c.id_calle 
    '    'INNER JOIN [eventos].map_evento m ON re.folio=m.folio 
    '    'Group BY  re.FOLIO,re.nombre_evento,sec.secretaria,dir.nombre_depe,e.evento, 
    '    're.descripcion,col.nombr_colonia,c.calle,re.num_ext,re.num_int,
    '    're.fecha,re.hora,re.num_benef,m.coordx,m.coordy,re.prensa
    '    'ORDER BY re.folio




    '    tabla = conexion.sqlcon(stry)
    '    Me.GridView1.DataSource = tabla
    '    Me.GridView1.DataBind()


    '    Dim sb As StringBuilder = New StringBuilder()
    '    Dim sw As StringWriter = New StringWriter(sb)
    '    Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
    '    Dim pagina As Page = New Page
    '    Dim form = New HtmlForm
    '    Me.GridView1.EnableViewState = False
    '    pagina.EnableEventValidation = False
    '    pagina.DesignerInitialize()
    '    pagina.Controls.Add(form)
    '    form.Controls.Add(Me.GridView1)
    '    pagina.RenderControl(htw)
    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AddHeader("Content-Disposition", "attachment;filename=REGISTRO_EVENTOS.xls")
    '    Response.Charset = "UTF-8"
    '    Response.ContentEncoding = Encoding.Default
    '    Response.Write(sb.ToString())
    '    Response.End()


    'End Sub


    '#Region "FUNCIONES"
    '    Function ValidarAlta() As Boolean

    '        Invisibles()

    '        ValidarAlta = True

    '        'If Me.txtfolio.Text = "" Then
    '        '    Me.label13.Visible = True
    '        '    ValidarAlta = False
    '        'End If
    '        'If Me.txtEvento.Text = "" Then
    '        '    Me.label1.Visible = True
    '        '    ValidarAlta = False
    '        'End If
    '        If Me.ddl2.Text = "" Then
    '            Me.label16.Visible = True
    '            ValidarAlta = False
    '        End If
    '        'If Me.ddl3.Text = "" Then
    '        '    Me.label1.Visible = True
    '        '    ValidarAlta = False
    '        'End If
    '        If Me.ddl4.Text = "" Then
    '            Me.label16.Visible = True
    '            ValidarAlta = False
    '        End If
    '        If Me.ddl5.Text = "" Then
    '            Me.label16.Visible = True
    '            ValidarAlta = False
    '        End If

    '        'If Me.txtext.Text = "" Then
    '        '    If Me.txtint.Text = "" Then
    '        '        Me.label15.Visible = True
    '        '        ValidarAlta = False
    '        '    End If
    '        'End If
    '        'If Me.txtint.Text = "" Then
    '        '    If Me.txtext.Text = "" Then
    '        '        Me.label15.Visible = True
    '        '        ValidarAlta = False
    '        '    End If
    '        'End If
    '        If Me.txtfecha.Text = "" Then
    '            Me.label18.Visible = True
    '            ValidarAlta = False
    '        End If
    '        If Not IsDate(Me.txtfecha.Text) Then
    '            Me.label18.Visible = True
    '            ValidarAlta = False
    '        End If
    '        If Me.txtnumbenef.Text = "" Then
    '            Me.label17.Visible = True
    '            ValidarAlta = False
    '        End If

    '        If Me.txthrini.Text = "" Then
    '            'Me.label23.Visible = True
    '            'ValidarAlta = False
    '        End If


    '        If Len(Me.txthrini.Text) <> 5 Then
    '            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_Hora();", True)
    '            Me.txthrini.Text = ""
    '            'FALTA MSJ DE ERROR
    '            ValidarAlta = False

    '        End If


    '        If Len(Me.txthrfin.Text) <> 5 Then
    '            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_Hora();", True)
    '            Me.txthrfin.Text = ""
    '            'FALTA MSJ DE ERROR
    '            ValidarAlta = False

    '        End If

    '        If Len(Me.txthralcalde.Text) <> 5 Then
    '            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_HoraAlcalde();", True)
    '            Me.txthralcalde.Text = ""
    '            'FALTA MSJ DE ERROR
    '            ValidarAlta = False
    '        End If

    '    End Function
    '#End Region

    '#Region "COMBOS"
    '    Protected Sub ddl4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl4.SelectedIndexChanged


    '        '-----------------------------1er opcion

    '        '-----------------------------------------------------

    '        'Dim ColoEnviada As String
    '        'Dim IDCOL As Integer

    '        'ColoEnviada = Request.QueryString("IDCOL")
    '        'IDCOL = Session("IDCOL")
    '        'ColoEnviada = IDCOL
    '        'If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)

    '        'DibujarCol()

    '        '-----------------------------------------------------

    '        '--------------------2da opcion
    '        'Dim IDCOL As Integer
    '        'Dim ColoEnviada As String

    '        'IDCOL = Me.ddl4.Text
    '        ''SE AGREGO---------------------------
    '        'ColoEnviada = Request.QueryString("IDCOL")
    '        ''---------------------------------------

    '        'Session("IDCOL") = IDCOL


    '        'CargarCallesColonia()

    '        'ColoEnviada = IDCOL
    '        'If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)

    '        'DibujarCol()



    '        '------------------3er opcion
    '        Dim IDCOL As Integer

    '        IDCOL = Me.ddl4.Text
    '        Session("IDCOL") = IDCOL
    '        CargarCallesColonia()
    '        'ColoEnviada = Request.QueryString("IDCOL")
    '        Dim ColoEnviada As String
    '        ColoEnviada = IDCOL
    '        If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)

    '        DibujarCol()

    '        '------------------------------------------------------------

    '        'Dim IDCOL As Integer

    '        'IDCOL = Me.ddl4.Text
    '        'CargarCallesColonia()

    '        'Dim ColoEnviada As String
    '        'ColoEnviada = IDCOL
    '        'If ColoEnviada <> "" Then UbicaColonias(ColoEnviada)

    '        'DibujarCol()

    '        Me.ddl5.Focus()



    '    End Sub


    '    Protected Sub ddl2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl2.SelectedIndexChanged
    '        Dim IDSECRE As Integer

    '        IDSECRE = Me.ddl2.Text
    '        Session("IDSECRE") = IDSECRE
    '        CargarDirecciones()
    '        'CargarEventoSecre()
    '        CargaResponsabledelEvento()

    '        Me.ddl3.Focus()







    '    End Sub



    '#End Region

    '    Sub CargaResponsabledelEvento()
    '        'cargaDirecciones
    '        IDSECRE = Session("IDSECRE")
    '        stry = "select director from eventos.secretarias  WHERE id_secr = " & IDSECRE & ""
    '        tabla = conexion.sqlcon(stry)
    '        If tabla.Rows.Count < 1 Then
    '            Me.txtResponsableEvento.Text = ""
    '        Else
    '            Me.txtResponsableEvento.Text = tabla.Rows(0)("director")
    '        End If

    '    End Sub
    '    Sub CargarDirecciones()

    '        'cargaDirecciones
    '        IDSECRE = Session("IDSECRE")
    '        stry = "select a.clave_depe,a.nombre_depe from eventos.direcciones a INNER JOIN eventos.secretarias b ON a.clave_secr=b.id_secr WHERE b.id_secr = " & IDSECRE & ""
    '        tabla = conexion.sqlcon(stry)

    '        If tabla.Rows.Count < 1 Then
    '            Me.ddl3.Items.Clear()
    '            Me.ddl3.Items.Add("")
    '        Else

    '            Me.ddl3.DataSource = tabla
    '            Me.ddl3.DataTextField = "nombre_depe"
    '            Me.ddl3.DataValueField = "clave_depe"
    '            Me.ddl3.DataBind()

    '        End If
    '    End Sub

    '    'Sub CargarEventoSecre()
    '    '    'cargaCalles
    '    '    IDSECRE = Session("IDSECRE")
    '    '    stry = "select e.id_evento,e.evento from secr_evento s INNER JOIN evento e ON s.id_evento=e.id_evento WHERE id_secr = " & IDSECRE & " and e.activo=1"
    '    '    tabla = conexion.sqlcon(stry)

    '    '    If tabla.Rows.Count < 1 Then

    '    '        Me.ddl3.Items.Clear()
    '    '        Me.ddl3.Items.Add("")
    '    '    Else

    '    '        Me.ddl3.DataSource = tabla
    '    '        Me.ddl3.DataTextField = "evento"
    '    '        Me.ddl3.DataValueField = "id_evento"
    '    '        Me.ddl3.DataBind()

    '    '    End If
    '    'End Sub

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






    'Protected Sub txthrini_TextChanged(sender As Object, e As System.EventArgs) Handles txthrini.TextChanged

    '    If Len(Me.txthrini.Text) <> 5 Then
    '        'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_Hora();", True)
    '    Else
    '        'Me.CheckAlcalde.Focus()
    '        Me.txthrini.Focus()
    '    End If

    'End Sub

    'Public Sub ConsultarR(txtfolioX As String)
    '    Me.txtfolio.Enabled = False

    '    If Me.txtfolio.Text <> "" Then
    '        inicia()
    '        'Me.lblmsjalta.Text = ""
    '        'Me.lblmsjalta.Visible = False
    '        Me.lblmsjcambio.Visible = False

    '        Me.txtfolio.Text = txtfolioX
    '        'CARGA EL PUNTO MAPA

    '        Dim stry As String
    '        Dim tabla As DataTable
    '        stry = "select id_secr from [eventos].reg_evento r INNER JOIN [eventos].map_evento m  On r.folio=m.folio where r.folio =  " & Me.txtfolio.Text & ""
    '        tabla = conexion.sqlcon(stry)

    '        If tabla.Rows.Count < 1 Then

    '            Me.lblmsjcambio.Visible = True
    '            Me.lblmsjcambio.Text = "*No existe el folio"
    '            inicia()
    '            deshabilita()
    '            Me.txtfolio.Enabled = True
    '            Me.txtfolio.Text = ""
    '            limpiarMapa()

    '        Else


    '            'no hay problema  no marcara error



    '            consulta()
    '            btnCancelar.Visible = False
    '            btnAceptar.Visible = False


    '        End If

    '    Else
    '        'si no no puede modificarla
    '        Me.lblmsjcambio.Text = "*No pertenece al area"
    '        Me.lblmsjcambio.Visible = True
    '        deshabilita()
    '        Me.txtfolio.Enabled = True
    '        Me.txtfolio.Text = ""
    '        Exit Sub
    '    End If


    'End Sub

    'Protected Sub txtfolio_TextChanged(sender As Object, e As System.EventArgs) Handles txtfolio.TextChanged


    '    Me.txtfolio.Enabled = False

    '    If Me.txtfolio.Text <> "" Then
    '        inicia()
    '        'Me.lblmsjalta.Text = ""
    '        'Me.lblmsjalta.Visible = False
    '        Me.lblmsjcambio.Visible = False


    '        'CARGA EL PUNTO MAPA

    '        Dim stry As String
    '        Dim tabla As DataTable
    '        stry = "select id_secr from [eventos].reg_evento r INNER JOIN [eventos].map_evento m  On r.folio=m.folio where r.folio =  " & Me.txtfolio.Text & ""
    '        tabla = conexion.sqlcon(stry)

    '        If tabla.Rows.Count < 1 Then

    '            Me.lblmsjcambio.Visible = True
    '            Me.lblmsjcambio.Text = "*No existe el folio"
    '            inicia()
    '            deshabilita()
    '            Me.txtfolio.Enabled = True
    '            Me.txtfolio.Text = ""
    '            limpiarMapa()

    '        Else


    '            'no hay problema  no marcara error



    '            consulta()
    '            btnCancelar.Visible = False
    '            btnAceptar.Visible = False


    '        End If

    '    Else
    '        'si no no puede modificarla
    '        Me.lblmsjcambio.Text = "*No pertenece al area"
    '        Me.lblmsjcambio.Visible = True
    '        deshabilita()
    '        Me.txtfolio.Enabled = True
    '        Me.txtfolio.Text = ""
    '        Exit Sub
    '    End If



    'End Sub


    Sub limpiarMapa()
        Dim corx As String
        Dim cory As String
        corx = ""
        txt_coord_x.Text = corx
        cory = ""
        txt_coord_y.Text = cory
        PonePunto()
    End Sub

    'Sub consulta()



    '    'Dim stry2 As String
    '    'Dim rs2 As SqlClient.SqlDataReader


    '    'stry = "SELECT r.folio,r.id_origen,r.id_secr,r.id_evento,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,Convert(char,r.fecha, 111) as fecha,r.num_benef FROM [eventos].reg_evento r INNER JOIN calles c ON r.id_calle =c.id_calle WHERE r.folio ='" & Trim(Me.txtfolio.Text) & "' Group by r.folio,r.id_origen,r.id_secr,r.id_evento,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,r.fecha,r.num_benef"


    '    stry = "SELECT r.folio,r.nombre_evento,r.id_secr,R.id_depe,r.id_evento,r.descripcion,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,Convert(char,r.fecha, 111) as fecha,r.hr_ini,r.hr_fin,r.num_benef,r.alcalde,hr_alcalde,r.prensa,r.limpiar,r.responsable,r.telefono,r.operador,r.puesto_ope,r.telefono_ope,r.correo_ope,r.lugar,r.hr_salida  FROM [eventos].reg_evento r INNER JOIN eventos.calles c ON r.id_calle =c.id_calle  WHERE r.folio ='" & Trim(Me.txtfolio.Text) & "' Group by r.folio,r.nombre_evento,r.id_secr,R.id_depe,r.id_evento,r.descripcion,r.id_col,c.calle,c.id_calle,r.num_ext,r.num_int,r.fecha,r.hr_ini,r.hr_fin,r.num_benef,r.alcalde,r.hr_alcalde,r.prensa,r.limpiar,r.responsable,r.telefono,r.operador,r.puesto_ope,r.telefono_ope,r.correo_ope,r.lugar,r.hr_salida"

    '    'Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.Conectar())
    '    'rs2 = cmd2.ExecuteReader()
    '    'rs2.Read()




    '    tabla = conexion.sqlcon(stry)
    '    If tabla.Rows.Count < 1 Then
    '        Me.lblmsjcambio.Visible = True
    '        Me.lblmsjcambio.Text = "*No existe el folio"
    '        inicia()
    '        deshabilita()
    '        Me.txtfolio.Enabled = True
    '        Me.txtfolio.Text = ""
    '    Else

    '        Try

    '            Dim secr_cambio As Integer
    '            secr_cambio = tabla.Rows(0)("id_secr")
    '            clave_secr = Session("id_secr")
    '            If secr_cambio <> clave_secr Then
    '                If Session("privilegio") = 1 Then
    '                    'no hay problema  no marcara error
    '                Else
    '                    'si no no puede modificarla
    '                    Me.lblmsjcambio.Text = "*No pertenece al area"
    '                    Me.lblmsjcambio.Visible = True
    '                    inicia()
    '                    deshabilita()
    '                    Me.txtfolio.Enabled = True
    '                    Me.txtfolio.Text = ""
    '                    Exit Sub
    '                End If
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("folio")) Then
    '                Me.txtfolio.Text = ""
    '            Else
    '                Me.txtfolio.Text = tabla.Rows(0)("folio")
    '            End If
    '            'If IsDBNull(tabla.Rows(0)("id_origen")) Then
    '            'Me.ddl1.Text = ""
    '            'Else
    '            'Me.ddl1.Text = tabla.Rows(0)("id_origen")
    '            'End If
    '            If IsDBNull(Me.tabla.Rows(0)("nombre_evento")) Then
    '                Me.txtEvento.Text = ""
    '            Else
    '                Me.txtEvento.Text = tabla.Rows(0)("nombre_evento")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("descripcion")) Then
    '                Me.txtDescripcion.Text = ""
    '            Else
    '                Me.txtDescripcion.Text = tabla.Rows(0)("descripcion")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("lugar")) Then
    '                Me.txtLugar.Text = ""
    '            Else
    '                Me.txtLugar.Text = tabla.Rows(0)("lugar")
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

    '            If IsDBNull(Me.tabla.Rows(0)("hr_ini")) Then
    '                Me.txthrini.Text = ""
    '            Else

    '                Me.txthrini.Text = tabla.Rows(0)("hr_ini")

    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("hr_fin")) Then
    '                Me.txthrfin.Text = ""
    '            Else

    '                Me.txthrfin.Text = tabla.Rows(0)("hr_fin")

    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("hr_salida")) Then
    '                Me.txtHrSalidaAlcalde.Text = ""
    '            Else

    '                Me.txtHrSalidaAlcalde.Text = tabla.Rows(0)("hr_salida")

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


    '            If tabla.Rows(0)("alcalde") = False Then
    '                Me.CheckAlcalde.Checked = False
    '            Else
    '                Me.CheckAlcalde.Checked = True
    '            End If

    '            If tabla.Rows(0)("prensa") = False Then
    '                Me.CheckPrensa.Checked = False
    '            Else
    '                Me.CheckPrensa.Checked = True
    '            End If

    '            If tabla.Rows(0)("limpiar") = False Then
    '                Me.CheckLimpieza.Checked = False
    '            Else
    '                Me.CheckLimpieza.Checked = True
    '            End If




    '            If IsDBNull(Me.tabla.Rows(0)("hr_alcalde")) Then
    '                Me.txthralcalde.Text = ""
    '            Else

    '                Me.txthralcalde.Text = tabla.Rows(0)("hr_alcalde")

    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("responsable")) Then
    '                Me.txtResponsableEvento.Text = ""
    '            Else

    '                Me.txtResponsableEvento.Text = tabla.Rows(0)("responsable")

    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("telefono")) Then
    '                Me.txtTelefonoResponsable.Text = ""
    '            Else

    '                Me.txtTelefonoResponsable.Text = tabla.Rows(0)("telefono")

    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("operador")) Then
    '                Me.txtOperadopor.Text = ""
    '            Else

    '                Me.txtOperadopor.Text = tabla.Rows(0)("operador")

    '            End If


    '            If IsDBNull(Me.tabla.Rows(0)("puesto_ope")) Then
    '                Me.txtPuestoOperador.Text = ""
    '            Else

    '                Me.txtPuestoOperador.Text = tabla.Rows(0)("puesto_ope")

    '            End If


    '            If IsDBNull(Me.tabla.Rows(0)("telefono_ope")) Then
    '                Me.txtTelefonoOperador.Text = ""
    '            Else

    '                Me.txtTelefonoOperador.Text = tabla.Rows(0)("telefono_ope")

    '            End If


    '            If IsDBNull(Me.tabla.Rows(0)("correo_ope")) Then
    '                Me.txtCorreoOperador.Text = ""
    '            Else

    '                Me.txtCorreoOperador.Text = tabla.Rows(0)("correo_ope")

    '            End If



    '            If IsDBNull(Me.tabla.Rows(0)("id_secr")) Then
    '                Me.ddl2.Text = ""
    '            Else
    '                Me.ddl2.Text = tabla.Rows(0)("id_secr")
    '                clave_secr = Me.ddl2.Text
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("id_depe")) Then
    '                Me.ddl3.Text = ""
    '            Else

    '                clave_depe = tabla.Rows(0)("id_depe")

    '            End If




    '            If IsDBNull(Me.tabla.Rows(0)("id_col")) Then
    '                Me.ddl4.Text = ""
    '            Else
    '                Me.ddl4.Text = tabla.Rows(0)("id_col")
    '                clave_colo = Me.ddl4.Text
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("id_evento")) Then
    '                Me.DropDownList2.Text = ""
    '            Else
    '                DropDownList2.Text = tabla.Rows(0)("id_evento")
    '            End If

    '            If IsDBNull(Me.tabla.Rows(0)("id_calle")) Then
    '                Me.ddl5.Text = ""
    '            Else
    '                clave_calle = tabla.Rows(0)("id_calle")
    '            End If

    '            CargarDepe()
    '            'CargarColonias()
    '            CargarCalles()
    '            stry = "select * from [eventos].map_evento where folio = " & Me.txtfolio.Text & ""
    '            tabla = conexion.sqlcon(stry)
    '            If tabla.Rows.Count < 1 Then
    '                Me.lblmsjcambio.Visible = True
    '                Me.lblmsjcambio.Text = "*No existe el folio"
    '                inicia()
    '                deshabilita()
    '                Me.txtfolio.Enabled = True
    '                Me.txtfolio.Text = ""
    '                limpiarMapa()

    '            Else

    '                corx = tabla.Rows(0)("coordy")
    '                txt_coord_x.Text = corx
    '                cory = tabla.Rows(0)("coordx")
    '                txt_coord_y.Text = cory
    '                PonePunto()



    '            End If

    '            'If IsDBNull(Me.tabla.Rows(0)("calle")) Then
    '            '    Me.ddl5.Text = ""
    '            'Else
    '            '    'CargarCalles()
    '            '    'Dim clave_calle As Integer
    '            '    'clave_calle = tabla.Rows(0)("id_calle")
    '            '    'Me.DropDownList1.SelectedItem.Text = tabla.Rows(0)("id_calle")
    '            '    Me.ddl5.Items.Clear()
    '            '    'Me.DropDownList1.Items.Add("")
    '            '    Dim i As Integer ' se crea la variable i para llenar el listbox
    '            '    i = 0
    '            '    For Each row As DataRow In tabla.Rows ' sirve para recorrer toda la tabla
    '            '        Me.ddl5.Items.Add(tabla.Rows(i)("calle").ToString())
    '            '        i = i + 1
    '            '    Next
    '            'End If

    '            Session("cambio") = 1
    '            habilitar()
    '            Me.btnAceptar.Visible = True
    '        Catch ex As Exception
    '            Me.lblmsjcambio.Visible = True
    '            Me.lblmsjcambio.Text = "*No existe el folio"
    '            deshabilita()
    '            Me.txtfolio.Enabled = True
    '            Me.txtfolio.Text = ""
    '        End Try
    '    End If
    'End Sub


    Public Sub PonePunto()
        Dim con As New conexion
        ''Dim stry As String

        Dim Colonia As String = ""
        Dim Longx As String = ""
        Dim Latix As String = ""
        Dim CadFinal As String = "*"
        ''TextTitulo.Text = Trim(DropDownList1.Text)

        ''stry = "select * from [eventos].TbColonias where mun=47 and  idcol=" & Tcolx
        ''Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con1.ConectaComercio1)
        ''Dim tDrsx As System.Data.SqlClient.SqlDataReader
        ''tDrsx = tRsGen.ExecuteReader
        ''Try
        ''    While tDrsx.Read
        ''        Longx = Trim(tDrsx(3))
        ''        Latix = Trim(tDrsx(4))
        ''        CadFinal = CadFinal & "(" & Latix & "," & Longx & ")*"
        ''    End While
        ''Finally
        ''    tDrsx.Close()
        ''End Try
        TextLng.Text = txt_coord_x.Text
        TextLat.Text = txt_coord_y.Text
        TextBox2.Text = CadFinal
        ''llenaCBO(Tcolx)
    End Sub

    'Sub CargarColonias()
    '    'cargaColonias
    '    stry = "select id_colonia,nombr_colonia from [eventos].xcolonias WHERE id_colonia=" & clave_colo & " order by id_colonia  "
    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then
    '        'MsgBox("No se encontro la información")
    '        Me.ddl4.Items.Clear()
    '        Me.ddl4.Items.Add("")
    '    Else
    '        ddl4.DataSource = tabla
    '        ddl4.DataTextField = "nombr_colonia"
    '        ddl4.DataValueField = "id_colonia"
    '        ddl4.DataBind()
    '    End If
    'End Sub



    'Sub CargarEvento()
    '    'cargaCalles

    '    'stry = "select e.id_evento,e.evento from evento e INNER JOIN secr_evento s ON e.id_evento=s.id_evento WHERE s.id_secr = " & clave_secr & " and e.id_evento=" & clave_evento & ""
    '    stry = "select id_evento,evento from eventos.evento"
    '    tabla = conexion.sqlcon(stry)

    '    If tabla.Rows.Count < 1 Then
    '        'MsgBox("No se encontro la información")
    '        Me.DropDownList2.Items.Clear()
    '        Me.DropDownList2.Items.Add("")
    '    Else

    '        Me.DropDownList2.DataSource = tabla
    '        Me.DropDownList2.DataTextField = "evento"
    '        Me.DropDownList2.DataValueField = "id_evento"
    '        Me.DropDownList2.DataBind()

    '    End If
    'End Sub


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


        stry = "select count(fecha) as dias from eventos.dias_festivos where fecha between '" & FInicio & "' and '" & FFin & "'"
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

    'Protected Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

    '    'If ValidarAlta() = False Then
    '    '    Me.label13.Visible = True
    '    '    Exit Sub
    '    'End If




    '    If Len(Me.txthrini.Text) < 5 Or Len(Me.txthrfin.Text) < 5 Then

    '        'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_Hora();", True)
    '        Exit Sub
    '    End If


    '    If Me.CheckAlcalde.Checked = True Then
    '        If Len(Me.txthralcalde.Text) < 5 Then
    '            'alerta_Cambio_Error_HoraAlcalde
    '            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('HORA ALCALDE:Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_HoraAlcalde();", True)
    '            Exit Sub
    '        End If
    '    Else
    '        Me.txthralcalde.Text = ""
    '    End If


    '    If Len(Trim(Me.txtfecha.Text)) < 10 Then
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", " alerta_Cambio_Error_Fecha();", True)
    '        Exit Sub
    '    End If



    '    If Me.txtTelefonoResponsable.Text = "" Then
    '        Me.txtTelefonoResponsable.Text = 0
    '    End If



    '    If Session("UsuarioAdmin") <> 1234 Then

    '        'stry = "select CONVERT (date, GETDATE()) as fecha_captura"
    '        stry = "select CONVERT(char, GETDATE(),111) as fecha_captura"
    '        tabla = conexion.sqlcon(stry)
    '        If tabla.Rows.Count < 1 Then
    '        Else
    '            fecha_captura = tabla.Rows(0)("fecha_captura")
    '            fecha_captura = Trim(fecha_captura)
    '        End If

    '        Dim diashabiles As Integer
    '        diashabiles = 0
    '        diashabiles = Laborales(fecha_captura, Me.txtfecha.Text)

    '        If diashabiles > 4 Then


    '            Dim alcalde As Integer
    '            If Me.CheckAlcalde.Checked = True Then
    '                alcalde = 1
    '            Else
    '                alcalde = 0
    '            End If

    '            Dim prensa As Integer
    '            If Me.CheckPrensa.Checked = True Then
    '                prensa = 1
    '            Else
    '                prensa = 0
    '            End If
    '            Dim limpiar As Integer
    '            If Me.CheckLimpieza.Checked = True Then
    '                limpiar = 1
    '            Else
    '                limpiar = 0
    '            End If



    '            IDCOL = Session("IDCOL")
    '            coordx = Session("coordx")
    '            coordy = Session("coordy")



    '            folio = Me.txtfolio.Text
    '            If alcalde <> 0 Then
    '                'Es necesario que sea Validada por Relaciones Publicas
    '                stry = "UPDATE [eventos].reg_evento set nombre_evento = '" & Trim(Me.txtEvento.Text) & "',id_secr=" & Trim(Me.ddl2.Text) & ",id_depe=" & Trim(Me.ddl3.Text) & ",id_evento=" & Trim(Me.DropDownList2.Text) & ",descripcion = '" & Trim(Me.txtDescripcion.Text) & "', id_col=" & Trim(Me.ddl4.Text) & ",id_calle=" & Trim(Me.ddl5.Text) & ",num_ext='" & Trim(Me.txtext.Text) & "',num_int='" & Trim(Me.txtint.Text) & "',fecha='" & Trim(Me.txtfecha.Text) & "',hr_ini='" & Trim(Me.txthrini.Text) & "',hr_fin='" & Trim(Me.txthrfin.Text) & "',num_benef=" & Trim(Me.txtnumbenef.Text) & ",alcalde =" & alcalde & ",hr_alcalde ='" & Trim(Me.txthralcalde.Text) & "',prensa =" & prensa & ",limpiar = " & limpiar & ",responsable ='" & Me.txtResponsableEvento.Text & "',telefono = " & Me.txtTelefonoResponsable.Text & ",operador = '" & Trim(Me.txtOperadopor.Text) & "',puesto_ope ='" & Trim(Me.txtPuestoOperador.Text) & "',telefono_ope = " & Trim(Me.txtTelefonoOperador.Text) & ",correo_ope = '" & Trim(Me.txtCorreoOperador.Text) & "', validada = 0  WHERE folio =" & Trim(Me.txtfolio.Text) & ""
    '            Else
    '                ' No es necesario que sea Validada
    '                stry = "UPDATE [eventos].reg_evento set nombre_evento = '" & Trim(Me.txtEvento.Text) & "',id_secr=" & Trim(Me.ddl2.Text) & ",id_depe=" & Trim(Me.ddl3.Text) & ",id_evento=" & Trim(Me.DropDownList2.Text) & ",descripcion = '" & Trim(Me.txtDescripcion.Text) & "', id_col=" & Trim(Me.ddl4.Text) & ",id_calle=" & Trim(Me.ddl5.Text) & ",num_ext='" & Trim(Me.txtext.Text) & "',num_int='" & Trim(Me.txtint.Text) & "',fecha='" & Trim(Me.txtfecha.Text) & "',hr_ini='" & Trim(Me.txthrini.Text) & "',hr_fin='" & Trim(Me.txthrfin.Text) & "',num_benef=" & Trim(Me.txtnumbenef.Text) & ",alcalde =" & alcalde & ",hr_alcalde ='" & Trim(Me.txthralcalde.Text) & "',prensa =" & prensa & ",limpiar = " & limpiar & ",responsable ='" & Me.txtResponsableEvento.Text & "',telefono = " & Me.txtTelefonoResponsable.Text & ",operador = '" & Trim(Me.txtOperadopor.Text) & "',puesto_ope ='" & Trim(Me.txtPuestoOperador.Text) & "',telefono_ope = " & Trim(Me.txtTelefonoOperador.Text) & ",correo_ope= '" & Trim(Me.txtCorreoOperador.Text) & "',validada = 1  WHERE folio =" & Trim(Me.txtfolio.Text) & ""
    '            End If

    '            resultado = conexion.sqlcambios(stry)
    '            If resultado = -1 Then
    '                'ERROR
    '                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error();", True)
    '                Exit Sub
    '            Else


    '                stry = "select * from [eventos].map_evento where folio = " & Trim(folio) & " "
    '                tabla = conexion.sqlcon(stry)
    '                If tabla.Rows.Count < 1 Then
    '                    'no existe registro inserta
    '                    stry = "INSERT INTO [eventos].map_evento VALUES (" & Trim(folio) & "," & Trim(coordx) & "," & Trim(coordy) & "," & Trim(IDCOL) & ")"
    '                    resultado = conexion.sqlcambios(stry)
    '                Else
    '                    'si hay registro y update 
    '                    stry = "UPDATE [eventos].map_evento SET coordx = " & Trim(coordx) & ", coordy = " & Trim(coordy) & ",id_col =" & Trim(IDCOL) & " WHERE FOLIO = " & Trim(folio) & " "
    '                    resultado = conexion.sqlcambios(stry)
    '                End If
    '                If resultado = -1 Then
    '                    'ERROR, NO GUARDA NINGUN REGISTRO 
    '                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_PuntoMapa();", True)
    '                    'Exit Sub
    '                End If

    '                stry = "insert into eventos.bitacora_movimiento values(" & Session("clave_empl") & ", 'CAMBIOS','Cambios al folio de evento'," & Trim(folio) & " ,getdate())"
    '                resultado = conexion.sqlcambios(stry)




    '                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Exito();", True)
    '                Me.txtfolio.Enabled = False
    '                deshabilita()

    '            End If

    '        Else
    '            ' NO GUARDA EVENTO
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_NoPermiteGuardarEvento();", True)
    '            Exit Sub


    '        End If

    '    Else

    '        Dim alcalde As Integer
    '        If Me.CheckAlcalde.Checked = True Then
    '            alcalde = 1
    '        Else
    '            alcalde = 0
    '        End If

    '        Dim prensa As Integer
    '        If Me.CheckPrensa.Checked = True Then
    '            prensa = 1
    '        Else
    '            prensa = 0
    '        End If
    '        Dim limpiar As Integer
    '        If Me.CheckLimpieza.Checked = True Then
    '            limpiar = 1
    '        Else
    '            limpiar = 0
    '        End If

    '        IDCOL = Session("IDCOL")
    '        coordx = Session("coordx")
    '        coordy = Session("coordy")



    '        folio = Me.txtfolio.Text
    '        'stry = "INSERT INTO [eventos].map_evento VALUES (" & Trim(folio) & "," & Trim(coordx) & "," & Trim(coordy) & "," & Trim(IDCOL) & ")"
    '        If alcalde <> 0 Then
    '            'Es necesario que sea Validada por Relaciones Publicas
    '            stry = "UPDATE [eventos].reg_evento set nombre_evento = '" & Trim(Me.txtEvento.Text) & "',id_secr=" & Trim(Me.ddl2.Text) & ",id_depe=" & Trim(Me.ddl3.Text) & ",id_evento=" & Trim(Me.DropDownList2.Text) & ",descripcion = '" & Trim(Me.txtDescripcion.Text) & "', id_col=" & Trim(Me.ddl4.Text) & ",id_calle=" & Trim(Me.ddl5.Text) & ",num_ext='" & Trim(Me.txtext.Text) & "',num_int='" & Trim(Me.txtint.Text) & "',fecha='" & Trim(Me.txtfecha.Text) & "',hr_ini='" & Trim(Me.txthrini.Text) & "',hr_fin='" & Trim(Me.txthrfin.Text) & "',num_benef=" & Trim(Me.txtnumbenef.Text) & ",alcalde =" & alcalde & ",hr_alcalde ='" & Trim(Me.txthralcalde.Text) & "',prensa =" & prensa & ",limpiar = " & limpiar & ",responsable ='" & Me.txtResponsableEvento.Text & "',telefono = " & Me.txtTelefonoResponsable.Text & ",operador = '" & Trim(Me.txtOperadopor.Text) & "',puesto_ope = '" & Trim(Me.txtPuestoOperador.Text) & "',telefono_ope =" & Trim(Me.txtTelefonoOperador.Text) & ",correo_ope ='" & Trim(Me.txtCorreoOperador.Text) & "', validada = 0  WHERE folio =" & Trim(Me.txtfolio.Text) & ""
    '        Else
    '            ' No es necesario que sea Validada
    '            stry = "UPDATE [eventos].reg_evento set nombre_evento = '" & Trim(Me.txtEvento.Text) & "',id_secr=" & Trim(Me.ddl2.Text) & ",id_depe=" & Trim(Me.ddl3.Text) & ",id_evento=" & Trim(Me.DropDownList2.Text) & ",descripcion = '" & Trim(Me.txtDescripcion.Text) & "', id_col=" & Trim(Me.ddl4.Text) & ",id_calle=" & Trim(Me.ddl5.Text) & ",num_ext='" & Trim(Me.txtext.Text) & "',num_int='" & Trim(Me.txtint.Text) & "',fecha='" & Trim(Me.txtfecha.Text) & "',hr_ini='" & Trim(Me.txthrini.Text) & "',hr_fin='" & Trim(Me.txthrfin.Text) & "',num_benef=" & Trim(Me.txtnumbenef.Text) & ",alcalde =" & alcalde & ",hr_alcalde ='" & Trim(Me.txthralcalde.Text) & "',prensa =" & prensa & ",limpiar = " & limpiar & ",responsable ='" & Me.txtResponsableEvento.Text & "',telefono = " & Me.txtTelefonoResponsable.Text & ",operador = '" & Trim(Me.txtOperadopor.Text) & "',puesto_ope = '" & Trim(Me.txtPuestoOperador.Text) & "',telefono_ope = " & Trim(Me.txtTelefonoOperador.Text) & ",correo_ope = '" & Trim(Me.txtCorreoOperador.Text) & "', validada = 1  WHERE folio =" & Trim(Me.txtfolio.Text) & ""
    '        End If
    '        resultado = conexion.sqlcambios(stry)
    '        If resultado = -1 Then
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error();", True)
    '            Exit Sub
    '        Else



    '            stry = "select * from [eventos].map_evento where folio = " & Trim(folio) & " "
    '            tabla = conexion.sqlcon(stry)
    '            If tabla.Rows.Count < 1 Then
    '                'no existe registro inserta
    '                stry = "INSERT INTO [eventos].map_evento VALUES (" & Trim(folio) & "," & Trim(coordx) & "," & Trim(coordy) & "," & Trim(IDCOL) & ")"
    '                resultado = conexion.sqlcambios(stry)
    '            Else
    '                'si hay registro y update 
    '                stry = "UPDATE [eventos].map_evento SET coordx = " & Trim(coordx) & ", coordy = " & Trim(coordy) & ",id_col =" & Trim(IDCOL) & " WHERE FOLIO = " & Trim(folio) & " "
    '                resultado = conexion.sqlcambios(stry)
    '            End If

    '            If resultado = -1 Then
    '                'NO GUARDA NINGUN REGISTRO 
    '                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_PuntoMapa();", True)
    '                'Exit Sub
    '            End If

    '            stry = "insert into eventos.bitacora_movimiento values(" & Session("clave_empl") & ", 'CAMBIOS','Cambios al folio de evento'," & Trim(folio) & " ,getdate())"
    '            resultado = conexion.sqlcambios(stry)

    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Exito();", True)
    '            Me.txtfolio.Enabled = False
    '            deshabilita()

    '        End If
    '    End If




    'End Sub



    'Protected Sub btnExportar_Click(sender As Object, e As System.EventArgs) Handles btnExportar.Click
    '    ExportarExcel()
    'End Sub

    'Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
    '    Me.txtfolio.Enabled = True


    '    Me.txtfolio.Text = ""
    '    'Me.lblmsjalta.Text = ""
    '    Me.txthralcalde.Text = ""
    '    Me.txthralcalde.Enabled = False


    '    'Me.lblmsjalta.Visible = False
    '    'Me.lblmsjcambio.Visible = False
    '    'inicia()


    '    Me.txtnumbenef.Text = ""
    '    Me.txtext.Text = ""
    '    Me.txtint.Text = ""
    '    Me.txtfecha.Text = ""
    '    Me.txthrini.Text = ""
    '    Me.txthrfin.Text = ""
    '    Me.txthralcalde.Text = ""
    '    Me.txtHrSalidaAlcalde.Text = ""
    '    Me.txtEvento.Text = ""
    '    Me.txtDescripcion.Text = ""
    '    Me.txtLugar.Text = ""
    '    Me.CheckPrensa.Checked = False
    '    Me.CheckAlcalde.Checked = False
    '    Me.CheckLimpieza.Checked = False
    '    Me.txtResponsableEvento.Text = ""
    '    Me.txtTelefonoResponsable.Text = ""
    '    Me.txtOperadopor.Text = ""
    '    Me.txtPuestoOperador.Text = ""
    '    Me.txtTelefonoOperador.Text = ""
    '    Me.txtCorreoOperador.Text = ""




    '    deshabilita()
    '    Me.btnAceptar.Visible = False
    '    Me.btnCancelar.Visible = False
    '    Dim corx As String
    '    Dim cory As String
    '    corx = Request.QueryString("field1")
    '    txt_coord_x.Text = corx
    '    cory = Request.QueryString("field2")
    '    txt_coord_y.Text = cory
    '    PonePunto()



    'End Sub


    'Protected Sub txthrfin_TextChanged(sender As Object, e As System.EventArgs) Handles txthrfin.TextChanged


    '    If Len(Me.txthrfin.Text) <> 5 Then
    '        'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_Hora();", True)
    '    End If
    '    Me.CheckPrensa.Focus()

    'End Sub

    'Protected Sub txtfecha_TextChanged(sender As Object, e As System.EventArgs) Handles txtfecha.TextChanged

    '    If Len(Trim(Me.txtfecha.text)) < 10 Then
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
    '        Exit Sub

    '    End If

    '    If Session("UsuarioAdmin") <> 1234 Then

    '        'stry = "select CONVERT (date, GETDATE()) as fecha_captura"
    '        stry = "select CONVERT(char, GETDATE(),111) as fecha_captura"
    '        tabla = conexion.sqlcon(stry)
    '        If tabla.Rows.Count < 1 Then
    '        Else
    '            fecha_captura = tabla.Rows(0)("fecha_captura")
    '            fecha_captura = Trim(fecha_captura)
    '        End If

    '        Dim diashabiles As Integer
    '        diashabiles = 0
    '        diashabiles = Laborales(fecha_captura, Me.txtfecha.Text)

    '        If diashabiles > 4 Then
    '            Me.txthrini.Focus()
    '        Else
    '            ' NO GUARDA EVENTO
    '            'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('No se permite guardar el evento, porque no está dentro de los 3 días hábiles permitidos!!')", True)
    '            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Cambio_Error_NoPermiteGuardarEventoYPuntoMapa();", True)
    '            Exit Sub
    '        End If

    '    End If

    '    Me.CheckAlcalde.Focus()

    'End Sub



    'Protected Sub CheckAlcalde_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckAlcalde.CheckedChanged
    '    'Me.txthralcalde.Focus()
    '    Me.txthrini.Focus()
    'End Sub

    'Protected Sub txthralcalde_TextChanged(sender As Object, e As System.EventArgs) Handles txthralcalde.TextChanged
    '    'Me.txtnumbenef.Focus()
    '    Me.txtHrSalidaAlcalde.Focus()
    'End Sub

    'Protected Sub txtnumbenef_TextChanged(sender As Object, e As System.EventArgs) Handles txtnumbenef.TextChanged
    '    'Me.txthrfin.Focus()
    '    Me.txtResponsableEvento.Focus()
    'End Sub

    'Protected Sub CheckPrensa_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckPrensa.CheckedChanged
    '    'Me.CheckLimpieza.Focus()
    '    Me.txtnumbenef.Focus()
    'End Sub

    'Protected Sub CheckLimpieza_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckLimpieza.CheckedChanged
    '    ' Me.txtResponsableEvento.Focus()
    '    Me.CheckPrensa.Focus()
    'End Sub

    'Protected Sub ddl5_TextChanged(sender As Object, e As System.EventArgs) Handles ddl5.TextChanged
    '    Me.txtext.Focus()
    'End Sub
End Class
