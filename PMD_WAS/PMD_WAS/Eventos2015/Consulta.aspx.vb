Imports System.Data
Imports System.IO
Imports System.Data.SqlClient

Imports conexion

Partial Class Consulta
    Inherits System.Web.UI.Page

    Dim stry As String
    Dim tabla As DataTable
    Dim CadTotal As String
    Dim pto As Integer = 1
    Dim cuantosnulos As Double = 0
    Dim totalfolios As Double = 0

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Consulta Mapa")
    End Sub

    Protected Sub btn_analizar_Click(sender As Object, e As System.EventArgs) Handles btn_analizar.Click
        If Me.btn_analizar.Text = "Analizar" Then

            Me.btn_analizar.Text = "Analizar"
            Dim Fx_suceso As String = ""
            Dim Fx_colonia As String = ""
            Dim Fx_fecha As String = ""
            Dim Fx_fecha2 As String = ""


            Fx_fecha = "  a.fecha between '" & Me.txt_fechaini.Text & "' and '" & txt_fechafin.Text & "'"
            'Fx_fecha2 = "  a.fdpet_serv between '" & txt_fechaini.Text & " 00:00' and '" & txt_fechafin.Text & " 23:59'"

            If drp_colonia.Text = "TODOS" Then
                drp_colonia.Text = 0
            End If


            If drp_colonia.Text = 0 Then
                drp_colonia.Text = "TODOS"
            End If

            ConsultarEventos(Fx_fecha, Trim(drp_secre.Text), Trim(drp_colonia.Text))
            'ActualizarCIAC(Fx_fecha, Trim(drp_tipo.Text), Trim(drp_colonia.Text))
            'ActualizarCASC(Fx_fecha2, Trim(drp_tipo.Text), Trim(drp_colonia.Text))
            'Panel2.Visible = True



            Me.btn_analizar.Text = "Nueva Consulta"
            DeshabilitaCampos()
            'Panel2.Visible = True
            muestra_lbl()
        Else
            habilitaCampos()
            Me.btn_analizar.Text = "Analizar"
            'Panel2.Visible = False
            esconde_lbl()
        End If
    End Sub


    Public Sub ConsultarEventos(ByVal Fechas As String, ByVal TipoSuceso As String, ByVal Colonias As String)
        CadTotal = "*"
        Dim cadLnx As String = ""
        Dim CadLat As String = ""
        Dim NumX As String = ""
        Dim Cadfilter As String = ""
        Dim FiltroSuceso As String = ""



        If TipoSuceso <> "TODOS" Then

            Cadfilter = " and b.id_secr IN (" & drp_secre.SelectedValue & ")"
        Else
            Cadfilter = " "
        End If

        If Colonias <> "TODOS" Then

            Cadfilter = Cadfilter & " and B.id_col=" & Me.drp_colonia.SelectedValue
        Else

            Cadfilter = Cadfilter

        End If
        'stry = "select a.clave_soli,a.clave_orig,a.fdpet_soli,a.solic_soli,a.clave_trab,a.clave_col2,b.coord_lati,b.coord_long,a.clave_secr,e.nombr_secr " & _
        '       "from linsolicitud a " & _
        '       "inner join lin_coordweb b on a.clave_soli=b.clave_soli and a.clave_orig=b.clave_orig " & _
        '       "inner join linsecretaria e on  e.clave_secr=a.clave_secr " & _
        '       "where " & Fechas & " " & Cadfilter

        Dim con As New conexion
        Dim stry As String

        'stry = "select a.nombre_evento,a.fecha,a.hr_ini,a.descripcion,c.secretaria,a.folio,b.coordx,b.coordy,b.id_col " & _
        '      "from [eventos].reg_evento a inner join [eventos].map_evento b on a.folio =b.folio " & _
        '      "inner join secretarias c on a.id_secr = c.id_secr" & _
        '      " where " & Fechas & " " & Cadfilter



        stry = "select a.nombre_evento,a.fecha,a.hr_ini,a.descripcion,e.CALLE, d.NOMBR_COLONIA,A.NUM_EXT,A.NUM_INT,c.secretaria,a.folio,b.coordx,b.coordy,b.id_col,c.id_secr,a.alcalde " &
      "from [eventos].reg_evento a inner join [eventos].map_evento b on a.folio =b.folio " &
      "inner join [eventos].secretarias c on a.id_secr = c.id_secr " &
      "inner join [eventos].Xcolonias  d on a.id_col = d.id_colonia " &
      "inner join [eventos].calles e on a.id_calle = e.id_calle and a.id_col = e.id_col " &
      " where " & Fechas & " " & Cadfilter



        Dim RsGen2 As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader
        'Drsx2.Read()
        Try
            While Drsx2.Read


                If IsDBNull(Drsx2("coordx")) Then
                    cadLnx = AgregaPunto(0, 3)
                    cuantosnulos = cuantosnulos + 1
                Else

                    cadLnx = Trim(Drsx2("coordx"))
                End If


                If IsDBNull(Drsx2("coordy")) Then
                    CadLat = AgregaPunto(0, 5)
                    cuantosnulos = cuantosnulos + 1
                Else
                    CadLat = Trim(Drsx2("coordy"))
                End If


                Dim alcalde As Integer
                If IsDBNull(Drsx2("alcalde")) Then
                    alcalde = 0
                Else
                    If (Drsx2("alcalde")) = True Then
                        alcalde = 1
                    Else
                        alcalde = 0
                    End If

                End If

                'Dim prensa As Integer
                'If IsDBNull(Drsx2("prensa")) Then
                '    prensa = 0
                'Else
                '    If (Drsx2("prensa")) = True Then
                '        prensa = 1
                '    Else
                '        prensa = 0
                '    End If

                'End If
                Dim lugar As String = "CALLE:" & Trim(Drsx2("calle")) + " " + "COL." & Trim(Drsx2("nombr_colonia")) + " " + "NUM:" & Trim(Drsx2("num_ext")) + " " + Trim(Drsx2("num_int"))

                NumX = "" & Trim(Drsx2("nombre_evento")) & "#" & Trim(Drsx2("fecha")) & "#" & Trim(Drsx2("hr_ini")) & "#" & Trim(Drsx2("descripcion")) & "#" & Trim(Drsx2("id_secr")) & "#" & lugar & "#" & Trim(alcalde)
                'NumX = "" & Trim(Drsx2("nombre_evento")) & "#" & Trim(Drsx2("fecha")) & "#" & Trim(Drsx2("hr_ini")) & "#" & Trim(Drsx2("descripcion")) & "#" & Trim(Drsx2("secretaria"))
                'NumX = "" & Trim(Drsx2("nombre_evento")) & "#" & Trim(Drsx2("fecha")) & "#" & Trim(Drsx2("hr_ini")) & "#" & Trim(Drsx2("descripcion")) & "#" & Trim(prensa) & "#" & Trim(Drsx2("secretaria"))
                'NumX = "EVENTO:" & Trim(Drsx2("nombre_evento")) & "#FECHA:" & Trim(Drsx2("fecha")) & "#HORA:" & Trim(Drsx2("hora")) & "#DESCRIPCION: " & Trim(Drsx2("descripcion")) & "#" & Trim(Drsx2("id_secr")) & "#" & Trim(Drsx2("secretaria"))
                'NumX = "TRABAJO: " & Trim(tDrsx(3)) & "#" & Trim(tDrsx("clave_orig")) & "#" & Trim(tDrsx("clave_soli")) & "#FECHA: " & Trim(tDrsx("fdpet_soli")) & "#" & Trim(tDrsx("clave_secr")) & "#" & Trim(tDrsx("nombr_secr"))
                CadTotal = CadTotal & NumX & "(" & cadLnx & "," & CadLat & ")*"
                pto = pto + 1
                totalfolios = totalfolios + 1
            End While
        Finally
            Drsx2.Close()
        End Try


        'tabla = conexion.sqlcon(stry)

        'If tabla.Rows.Count < 1 Then
        '    MsgBox("No se encontro la información")
        'Else
        'If IsDBNull(Me.tabla.Rows(0)("coordx")) Then
        '    cadLnx = AgregaPunto(0, 3)
        '    cuantosnulos = cuantosnulos + 1
        'Else

        '    cadLnx = Trim(Me.tabla.Rows(0)("coordy"))
        'End If
        'End If

        lblCuantosNulos.Text = cuantosnulos
        lbl_total.Text = totalfolios
        TextBox3.Text = CadTotal
        TextBox3.Visible = True



    End Sub


    Private Sub DeshabilitaCampos()
        txt_fechaini.Enabled = False
        txt_fechafin.Enabled = False
        drp_secre.Enabled = False
        drp_colonia.Enabled = False
    End Sub
    Private Sub habilitaCampos()
        txt_fechaini.Enabled = True
        txt_fechafin.Enabled = True
        drp_secre.Enabled = True
        drp_colonia.Enabled = True
    End Sub

    Private Sub esconde_lbl()
        lbl_total.Visible = False
        lbl_total1.Visible = False
        lbl_nulos.Visible = False
        lblCuantosNulos.Visible = False
    End Sub
    Private Sub muestra_lbl()
        lbl_total.Visible = True
        lbl_total1.Visible = True
        lbl_nulos.Visible = True
        lblCuantosNulos.Visible = True
    End Sub

    Private Function AgregaPunto(ByVal cadx As String, ByVal pos As Integer) As String
        Dim Rcad As String = ""
        Dim ix As Integer
        Dim cad As String = ""
        For ix = 1 To Len(cadx)
            cad = Mid(cadx, ix, 1)
            If ix = pos Then
                Rcad = Rcad & "." & cad
            Else
                Rcad = Rcad & cad
            End If
        Next
        AgregaPunto = Rcad
    End Function


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load



        If Session("paso") = "1" Then
        Else
            Response.Redirect("~/Password.aspx")
        End If




        If IsPostBack = False Then

            usuario()

            txt_fechaini.Text = Format(Now, "yyyy-MM-dd")

            txt_fechafin.Text = Format(Now, "yyyy-MM-dd")



            drp_secre.Items.Clear()
            drp_colonia.Items.Clear()

            CargaColonia()
            CargaSecre()

        Else

        End If

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

    Private Sub CargaColonia()




        stry = "SELECT '0' id_col,'TODOS' nombr_colonia  UNION select c.id_col,col.nombr_colonia from [eventos].calles c INNER join [eventos].Xcolonias  col ON c.id_col=col.id_colonia group by c.id_col,col.nombr_colonia order by ID_COL  "


        'select c.id_col,col.nombr_colonia from calles c INNER join [eventos].Xcolonias  col ON c.id_col=col.id_colonia  group by c.id_col,col.nombr_colonia  order by col.nombr_colonia  
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            'MsgBox("No se encontro la información")
            drp_colonia.Items.Clear()
            drp_colonia.Items.Add("")
        Else
            drp_colonia.DataSource = tabla
            drp_colonia.DataTextField = "NOMBR_colonia"
            drp_colonia.DataValueField = "id_col"
            drp_colonia.DataBind()
            drp_colonia.Items.Add("TODOS")
        End If








    End Sub
    Private Sub CargaSecre()
        drp_secre.Items.Add("TODOS")
    End Sub


   
End Class
