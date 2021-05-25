Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion
Partial Class Imagen
    Inherits System.Web.UI.Page
    Dim stry As String
    Dim folioEvento As Integer
    Dim folioReq As Integer
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
    Dim folio_req As Integer
    Dim cantidad_decimal As Double
    Dim cantidad As String
    Dim folio_evento As String
    Dim ds As New DataSet
    Dim requerimiento As String
    Dim nombre_evento As String
    Dim tabfila As New DataTable









    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("paso") = "1" Then
        Else
            Response.Redirect("~/Password.aspx")
        End If


        If Not IsPostBack = True Then
            usuario()
            creaFolioEvento()
            cargaReq()
        End If




    End Sub

    Sub creaFolioEvento()
        Session("folioEvento") = 0
        stry = "Select max(folio_evento) as folioEvento from [eventos].reg_evento_imagen"
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            MsgBox("No se encontro la información")
        Else
            If (tabla.Rows(0)("folioEvento").ToString) = "" Then

                folioEvento = 1
                Me.lblIdEventoImagen.Text = folioEvento

                Session("folioEvento") = folioEvento

            Else
                folioEvento = tabla.Rows(0)("folioEvento").ToString
                folioEvento = folioEvento + 1
                Session("folioEvento") = folioEvento
                Me.lblIdEventoImagen.Text = folioEvento

            End If
        End If


    End Sub

    Sub creaFolioReq()
        Session("folioReq") = 0
        stry = "Select max(folio_Req) as folioReq from reg_req_imagen"
        tabla = conexion.sqlcon(stry)

        If tabla.Rows.Count < 1 Then
            MsgBox("No se encontro la información")
        Else
            If (tabla.Rows(0)("folioReq").ToString) = "" Then
                folioReq = 1
                Session("folioReq") = folioReq

            Else
                folioReq = tabla.Rows(0)("folioReq").ToString
                folioReq = folioReq + 1
                Session("folioReq") = folioReq


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



    Sub cargaReq()

        Dim con As New conexion
        Dim stry As String



        stry = "select '' ID_REQ, '' REQUERIMIENTO UNION select  id_req,requerimiento from cat_req_imagen order by id_req"

        Dim tRsGen As New System.Data.SqlClient.SqlCommand(stry, con.Conectar)
        Dim tDrsx As System.Data.SqlClient.SqlDataReader
        tDrsx = tRsGen.ExecuteReader()


        'drpReq.Items.Add("")


        Try


            While tDrsx.Read


                drpReq.DataSource = tDrsx
                drpReq.DataTextField = "requerimiento"
                drpReq.DataValueField = "id_req"
                drpReq.DataBind()

                'drpReq.Items.Insert(0, New ListItem("<Seleccione un Item>", "0"))

            End While
        Finally
            tDrsx.Close()
        End Try

    End Sub





    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click



        If Len(Trim(Me.txtFechaEvento.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub

        End If

        If Len(Me.txtHoraEvento.Text) = 0 Then
        Else
            If Len(Me.txtHoraEvento.Text) <> 5 Then
                'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)
                Exit Sub
            End If
        End If

        If Len(Trim(Me.txtFechaEntrega.Text)) < 10 Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Fecha();", True)
            Exit Sub

        End If


        If Me.GridView1.Rows.Count <> 0 And Me.lblIdEventoImagen.Text <> "" Then
            creaFolioEvento()
            creaFolioReq()


            stry = "INSERT INTO [eventos].reg_evento_imagen VALUES (" & Session("folioEvento") & "," & Session("folioReq") & ",'" & Me.txtNombreEvento.Text & "','" & Me.txtFechaEvento.Text & "','" & Me.txtHoraEvento.Text & "','" & Me.txtFechaEntrega.Text & "',getdate()," & Session("clave_empl") & ")"
            resultado = conexion.sqlcambios(stry)
            If resultado = -1 Then
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                Exit Sub


            Else

                If GridView1.Rows.Count > 0 Then
                    For i As Integer = 0 To GridView1.Rows.Count - 1
                        Dim ID = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(1).Text)
                        Dim DESCRIPCIÓN = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(2).Text)
                        Dim CANTIDAD = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(3).Text)
                        Dim ESPECIFICACION = HttpUtility.HtmlDecode(GridView1.Rows(i).Cells(4).Text)

                        stry = "INSERT INTO reg_req_imagen VALUES (" & Session("folioEvento") & "," & Session("folioReq") & ",'" & ID & "','" & CANTIDAD & "','" & ESPECIFICACION & "')"
                        resultado = conexion.sqlcambios(stry)
                        If resultado = -1 Then
                            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Erro, No se guardo la informacion');", True)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error();", True)
                            Exit Sub
                        End If
                    Next

                End If


            End If

            Me.txtCantidad.Text = ""
            Me.txtObservaciones.Text = ""
            Me.txtNombreEvento.Text = ""
            Me.txtFechaEvento.Text = ""
            Me.txtHoraEvento.Text = ""
            Me.txtFechaEntrega.Text = ""
            Me.GridView1.DataSourceID = Nothing
            Me.GridView1.DataSource = Nothing
            Me.GridView1.DataBind()
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alert('Se guardo la informacion con Éxito');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Exito();", True)
         

            creaFolioEvento()

        Else
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar el folio del evento y Seleccionar los Requerimientos!!');", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_DatosFaltantes();", True)

        End If


    End Sub


    Protected Sub drpReq_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpReq.SelectedIndexChanged
        Me.lblidreq.Text = Me.drpReq.SelectedValue
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        'If Me.txtFolio.Text = "" Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar el folio');", True)
        '    Exit Sub
        'End If

        If Me.lblidreq.Text = "" Or Me.lblidreq.Text = "0" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor seleccionar un requerimiento');", True)
            Exit Sub
        End If

        If Me.txtObservaciones.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de capturar Observaciones');", True)
            Exit Sub
        End If

        If Me.txtCantidad.Text = "0" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor ingresar una cantidad valida');", True)
            Exit Sub
        End If


        If Me.txtCantidad.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de ingresar la cantidad');", True)

        Else



            Dim dt As New DataTable
            dt = Session("tabses")
            Dim dr As DataRow




            dt = New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("DESCRIPCIÓN")
            dt.Columns.Add("CANTIDAD")
            dt.Columns.Add("ESPECIFICACION")
            Session.Add("Tabla", dt)


            If GridView1.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1

                    dr = dt.NewRow

                    If Me.lblidreq.Text = GridView1.Rows(i).Cells(1).Text Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de agregar un requerimiento diferente');", True)
                        Exit Sub
                    End If
                    dr("ID") = GridView1.Rows(i).Cells(1).Text
                    dr("DESCRIPCIÓN") = GridView1.Rows(i).Cells(2).Text
                    dr("CANTIDAD") = GridView1.Rows(i).Cells(3).Text
                    dr("ESPECIFICACION") = GridView1.Rows(i).Cells(4).Text

                    dt.Rows.Add(dr)
                Next
            End If




            dr = dt.NewRow


            'CargaCombo para sacar descripcion
            CargaRequerimientos()



            dr("ID") = Me.lblidreq.Text
            dr("DESCRIPCIÓN") = requerimiento
            dr("CANTIDAD") = Me.txtCantidad.Text
            dr("ESPECIFICACION") = Me.txtObservaciones.Text


            dt.Rows.Add(dr)
            ds.Tables.Add(dt)
            Session.Add("tabses", dt)
            'GridView1.DataSource = ds.Tables(0)
            GridView1.DataSource = dt
            GridView1.DataBind()

            Me.txtCantidad.Text = ""
            Me.txtObservaciones.Text = ""











        End If
    End Sub


    Sub CargaRequerimientos()


        stry = "select requerimiento from cat_req_imagen where id_req =  " & Me.lblidreq.Text & "  order by id_req"
        tabla = conexion.sqlcon(stry)


        If tabla.Rows.Count < 1 Then

            Me.drpReq.Items.Clear()
            Me.drpReq.Items.Add("")
        Else
            requerimiento = tabla.Rows(0)("requerimiento")


        End If
    End Sub



    Protected Sub GridView1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting



        'Dim tabfila As New DataTable
        tabfila = Session("tabses")
        Dim index As Integer = -1
        '3
        index = Buscar_Indice(GridView1.Rows(e.RowIndex).Cells(1).Text, tabfila)
        If index <> -1 Then
            tabfila.Rows.RemoveAt(index)
            GridView1.DataSource = tabfila
            GridView1.DataBind()
            Session("tabses") = tabfila

        End If


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)

    End Sub


        Public Function Buscar_Indice(ByVal textobusqueda As String, ByVal tabfila As DataTable) As Integer


        Dim iindice As Integer = -1
        Dim encontrado As Boolean = False
        Dim contador As Integer = 0
        Dim row As DataRow



        While encontrado = False And contador <= tabfila.Rows.Count


            row = tabfila.Rows(contador)
            'row(2)
            If (row(0) = textobusqueda) Then
                encontrado = True
            End If

            iindice = contador
            contador = contador + 1

        End While
        Return iindice
    End Function





    'Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
    '    limpiar()
    'End Sub


    Sub limpiar()

        Me.txtCantidad.Text = ""
        Me.txtObservaciones.Text = ""
        Me.txtNombreEvento.Text = ""
        Me.txtFechaEvento.Text = ""
        Me.txtHoraEvento.Text = ""
        Me.txtFechaEntrega.Text = ""
        Me.GridView1.DataSourceID = Nothing
        Me.GridView1.DataSource = Nothing
        Me.GridView1.DataBind()
        creaFolioEvento()
    End Sub

    Protected Sub txtHoraEvento_TextChanged(sender As Object, e As System.EventArgs) Handles txtHoraEvento.TextChanged
        If Len(Me.txtHoraEvento.Text) = 0 Then
        Else
            If Len(Me.txtHoraEvento.Text) <> 5 Then
                'ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "alert('Recuerde el formato de Horas, desde 00:00 hasta 23:59')", True)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_Alta_Error_Hora();", True)
                Exit Sub
            End If
        End If
    End Sub


End Class
