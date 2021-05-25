Imports System.Drawing
Imports System.Data

Partial Class ConsultaSecretaria
    Inherits System.Web.UI.Page
    Dim conx As New Class1
    Dim CveSecr As String
    Dim CveEje As String
    Dim CveObj As String
    Dim CveEstr As String
    Dim CveLin As String
    Dim CveSubL As String
    Public Signo As Integer
    Public Mes As String
    Public IDLinea As String

    Dim stry As String
    Dim stry2 As String
    Dim NumMes As Integer
    Dim año As Integer
    Public cont As String
    Public contador As Integer
    Dim Fecha As Date
    Dim Semana As Integer
    Dim Resta As Integer
    Dim Tcero As Double
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                'Response.Redirect("~/Password.aspx")
            End If
            CargaDrop()
            LlenaDropAño()
            LlenarSecretarias()
            'LlenarEjes()
        End If
    End Sub
    'LLENAR EL DROP AÑOS
    Private Sub LlenaDropAño()
        Dim sent2 As New System.Data.SqlClient.SqlCommand("Select Año from [PMD].Admin_meses where Admon='" & Me.DropAdmon.Text & "'", conx.conectar)
        Dim exe2 As System.Data.SqlClient.SqlDataReader

        exe2 = sent2.ExecuteReader
        Try
            If Not exe2 Is Nothing Then
                DropAnio.DataSource = exe2
                DropAnio.DataTextField = "Año"
                DropAnio.DataValueField = "Año"
                DropAnio.DataBind()
                exe2.NextResult()
            End If
            exe2.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub CargaDrop()
        Dim sent As New System.Data.SqlClient.SqlCommand("Select Cve_admon,Nombr_admon from [PMD].Cat_Admon", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropAdmon.DataSource = exe
                DropAdmon.DataTextField = "Nombr_admon"
                DropAdmon.DataValueField = "Cve_admon"
                DropAdmon.DataBind()
                exe.NextResult()

            End If
        Catch ex As Exception
            exe.Close()
        End Try

    End Sub
    Public Sub LlenarSecretarias()
        'LLENAR DROP EJES
        Dim sent3 As New System.Data.SqlClient.SqlCommand("Select IdSecretaria,Nombr_secretaria from [PMD].Secretarias where Admon='" & Me.DropAdmon.SelectedValue & "'", conx.conectar)
        Dim exe3 As System.Data.SqlClient.SqlDataReader
        exe3 = sent3.ExecuteReader
        Try
            If Not exe3 Is Nothing Then
                GridSecr.DataSource = exe3
                GridSecr.DataBind()
            End If
            exe3.Close()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub DropAdmon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropAdmon.SelectedIndexChanged
        LlenaDropAño()
        LlenarSecretarias()
        LimpiarGrids(1)
    End Sub
    Private Sub LimpiarGrids(ByVal Index As Integer)
        Select Case Index
           
            Case "1" 'Cuando se Hace El Postbak en El drop Admon
                GridObj.DataBind()
                GridEstr.DataBind()
                'GridLin.DataBind()
                'GridSubL.DataBind()
            Case "2" ''Cuando Seleccionas algun Eje Limpia lo que alla podido estar en los grid siguientes exceptuando los objetivos que desplieguen del eje seleccionado
                GridEstr.DataBind()
                'GridLin.DataBind()
                'GridSubL.DataBind()
            Case "3" ''Cuando Seleccionas algun Objetivo Limpia lo que alla podido estar en los grid siguientes exceptuando las estrategias que desplieguen del Objetivo seleccionado
                'GridLin.DataBind()
                'GridSubL.DataBind()
            Case "4" 'Cuando Seleccionas algun Estrategia Limpia lo que alla podido estar en los grid siguientes exceptuando las Lineas que desplieguen del la Estrategia seleccionado
                'GridSubL.DataBind()
        End Select
    End Sub
    Protected Sub gridSecr_onRowCommadn(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridSecr.RowCommand
        If e.CommandName = "Buscar" Then
            Dim lnk As New LinkButton
            lnk = CType(e.CommandSource, LinkButton) 'El CommandSource es como el Text de un objeto
            Me.NombrSecr.Text = lnk.Text 'Este TextBox esta en el Modal

            CveSecr = lnk.CommandArgument.ToString() 'El CommandSource es como el Value de un objeto
            Me.Secr.Text = CveSecr 'Este label esta en el Modal

            RecorreFilas(0) 'RECORRER FILAS DEL GRID para cambiar el color del seleccionado

            'LLENAR EL Grid Eje Deacuerdo a la Secretaria Elegida
            Dim sent As New System.Data.SqlClient.SqlCommand("Select a.IdEje,a.Desc_eje from [PMD].Ejes a " &
                                                             "inner join [PMD].Concentrado_Pmd b on a.IdEje=b.Eje and a.Admon=b.Cve_admon " &
                                                             "where a.Admon='" & Me.DropAdmon.SelectedValue & "' and b.Cve_secr='" & Me.Secr.Text & "' group by a.IdEje,a.Desc_eje order by 1", conx.conectar)
            Dim exe As System.Data.SqlClient.SqlDataReader
            exe = sent.ExecuteReader
            Try
                If Not exe Is Nothing Then
                    GridEje.DataSource = exe
                    GridEje.DataBind()
                End If
                exe.Close()
            Catch ex As Exception
            End Try
        End If
        LimpiarGrids(1)
    End Sub
    Protected Sub GridEje_OnRowCommadn(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridEje.RowCommand
        If e.CommandName = "Buscar" Then
            Dim lnk As New LinkButton
            lnk = CType(e.CommandSource, LinkButton) 'El CommandSource es como el Text de un objeto
            Me.InfEje.Text = lnk.Text 'Este TextBox esta en el Modal

            CveEje = lnk.CommandArgument.ToString() 'El CommandSource es como el Value de un objeto
            Me.E.Text = CveEje 'Este label esta en el Modal

            RecorreFilas(1) 'RECORRER FILAS DEL GRID para cambiar el color del seleccionado

            'LLENAR EL Grid Objetivos Deacuerdo al eje Elegido
            Dim sent As New System.Data.SqlClient.SqlCommand("Select a.IdObjetivo,a.Nombr_obj " &
                                                             "from [PMD].Objetivos a " &
                                                             "inner join [PMD].Concentrado_Pmd b on a.IdEje=b.Eje and a.IdObjetivo=b.Objetivo and a.Admon=b.Cve_admon " &
                                                             "where a.IdEje='" & Me.E.Text & "' and b.Cve_secr='" & Me.Secr.Text & "' and a.Admon='" & Me.DropAdmon.SelectedValue & "' group by a.IdObjetivo,a.Nombr_obj order by 1 ", conx.conectar)

            Dim exe As System.Data.SqlClient.SqlDataReader
            exe = sent.ExecuteReader
            Try
                If Not exe Is Nothing Then
                    GridObj.DataSource = exe
                    GridObj.DataBind()
                End If
                exe.Close()
            Catch ex As Exception
            End Try
        End If
        LimpiarGrids(2)
    End Sub
    Protected Sub GridObj_OnRowCommadn(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridObj.RowCommand
        Dim lnk As New LinkButton
        lnk = CType(e.CommandSource, LinkButton)
        Me.InfObj.Text = lnk.Text 'Este TextBox esta en el Modal
        CveObj = lnk.CommandArgument.ToString
        Me.O.Text = CveObj

        RecorreFilas(2)

        'LLENAR EL Grid Estrategias De acuerdo al Objetivo Elegido
        Dim sent As New System.Data.SqlClient.SqlCommand("Select a.IdEstrategia,a.Nombr_estr " &
                                                         "from [PMD].Estrategias a " &
                                                         "inner join [PMD].Concentrado_Pmd b on a.IdEje=b.Eje and a.IdObjetivo=b.Objetivo and a.IdEstrategia=b.Estrategia and a.Admon=b.Cve_admon " &
                                                         "where a.IdEje = '" & Me.E.Text & "' And a.IdObjetivo ='" & Me.O.Text & "' And b.Cve_secr = '" & Me.Secr.Text & "' And a.Admon = '" & Me.DropAdmon.SelectedValue & "' group by a.IdEstrategia,a.Nombr_estr  ", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                GridEstr.DataSource = exe
                GridEstr.DataBind()
                End If
            exe.Close()
        Catch ex As Exception
            End Try
        LimpiarGrids(3)
    End Sub
    Protected Sub GridEstr_OnRowCommadn(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridEstr.RowCommand
        Dim lnk As New LinkButton
        lnk = CType(e.CommandSource, LinkButton)
        Me.InfEstr.Text = lnk.Text 'Este TextBox esta en el Modal
        CveEstr = lnk.CommandArgument.ToString
        Me.Es.Text = CveEstr

        RecorreFilas(3)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)
        ''LLENAR EL GridlINEAS De acuerdo a la Estrategia Elegida
        'Dim sent As New System.Data.SqlClient.SqlCommand("Select a.IdLinea,a.Nombr_linea " &
        '                                                 "from Lineas a " &
        '                                                 "inner join Concentrado_Pmd b on a.IdEje=b.Eje and a.IdObjetivo=b.Objetivo and a.IdEstrategia=b.Estrategia and a.IdLinea=b.Linea and a.Admon=b.Cve_admon " &
        '                                                 "where a.IdEje ='" & Me.E.Text & "' And a.IdObjetivo ='" & Me.O.Text & "' And a.IdEstrategia = '" & Me.Es.Text & "' And b.Cve_secr = '" & Me.Secr.Text & "' And Admon ='" & Me.DropAdmon.SelectedValue & "' group by a.IdLinea,a.Nombr_linea order by 1 ", conx.conectar)
        'Dim exe As System.Data.SqlClient.SqlDataReader
        'exe = sent.ExecuteReader
        'Try
        '    If Not exe Is Nothing Then
        '        GridLin.DataSource = exe
        '        GridLin.DataBind()

        '    End If
        '    exe.Close()
        'Catch ex As Exception

        'End Try
        LimpiarGrids(4)
    End Sub
    'Protected Sub GridLin_OnRowCommadn(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridLin.RowCommand
    '    Dim lnk As New LinkButton
    '    lnk = CType(e.CommandSource, LinkButton)
    '    Me.InfLin.Text = lnk.Text 'Este TextBox esta en el Modal
    '    CveLin = lnk.CommandArgument.ToString
    '    Me.L.Text = CveLin

    '    RecorreFilas(4)
    '    'LLENAR EL GridlSUBLINEAS De acuerdo a la LINEA Elegida
    '    Dim sent As New System.Data.SqlClient.SqlCommand("Select a.IdSublinea,a.Nombr_Sub " & _
    '                                                     "from Sublineas a " & _
    '                                                     "inner join Concentrado_Pmd b on a.IdEje=b.Eje and a.IdObjetivo=b.Objetivo and a.IdEstrategia=b.Estrategia and a.IdSubLinea=b.Sublinea and a.Admon=b.Cve_admon " & _
    '                                                     "where a.IdEje='" & Me.E.Text & "' and  a.IdObjetivo='" & Me.O.Text & "' and a.IdEstrategia='" & Me.Es.Text & "' and a.IdLinea='" & Me.L.Text & "' and b.Cve_secr='" & Me.Secr.Text & "' and a.Admon='" & Me.DropAdmon.SelectedValue & "' group by a.IdSublinea,a.Nombr_Sub  order by 1", conx.conectar)

    '    Dim exe As System.Data.SqlClient.SqlDataReader
    '    exe = sent.ExecuteReader

    '    If exe.HasRows = False Then 'Si no Tiene Sublineas Se muestra el Modal
    '        Me.InfSub.Text = ""
    '        Me.S.Text = ""
    '        Sel()
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)
    '        GridSubL.DataBind()

    '    Else 'si tiene Sublineas Llena el gridSubl
    '        Try
    '            If Not exe Is Nothing Then
    '                GridSubL.DataSource = exe
    '                GridSubL.DataBind()
    '                End If
    '            exe.Close()
    '        Catch ex As Exception
    '            End Try
    '        End If
    'End Sub
    'Protected Sub GridSubl_OnRowCommadn(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridSubL.RowCommand
    '    Dim lnk As New LinkButton
    '    lnk = CType(e.CommandSource, LinkButton)
    '    Me.InfSub.Text = lnk.Text 'Este TextBox esta en el Modal
    '    CveSubL = lnk.CommandArgument.ToString
    '    Me.S.Text = CveSubL
    '    RecorreFilas(5)

    '    Sel()
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)
    'End Sub
    Private Sub RecorreFilas(ByVal index As Integer)
        Dim seleccionado As String
        Dim Grid As System.Web.UI.WebControls.GridView
        Select Case index
            Case "0"
                Grid = GridSecr
                seleccionado = CveSecr
            Case "1"
                Grid = GridEje
                seleccionado = CveEje
            Case "2"
                Grid = GridObj
                seleccionado = CveObj
            Case "3"
                Grid = GridEstr
                seleccionado = CveEstr
            Case "4"
                'Grid = GridLin
                seleccionado = CveLin
            Case "5"
                'Grid = GridSubL
                seleccionado = CveSubL
        End Select
        Dim Cve As String
        For i As Integer = 0 To Grid.Rows.Count - 1
            Cve = DirectCast(Grid.Rows(i).FindControl("LinkButton1"), LinkButton).CommandArgument
            If Cve = seleccionado Then
                DirectCast(Grid.Rows(i).FindControl("LinkButton1"), LinkButton).ForeColor = Color.Black
            Else
                DirectCast(Grid.Rows(i).FindControl("LinkButton1"), LinkButton).ForeColor = Color.Green
            End If
        Next
    End Sub
    Public Sub Sel() 'Hace El Select con el ID para saber su Unidad de medida y la clasificacion e la linea

        stry = "select a.ID,a.tipo_de_Linea,b.Nombr_tipo from [PMD].Concentrado_PMD a inner join [PMD].Cat_tipo_linea b on a.tipo_de_linea=b.Cve_tipo where a.ID='" & Me.E.Text & "." & Me.O.Text & "." & Me.Es.Text & ""
        'If Me.S.Text = "" Then
        '    stry = stry & "'"
        'Else
        '    stry = stry & "." & Me.S.Text & "' "
        'End If

        stry = stry & " and a.Año='" & Me.DropAnio.Text & "' and a.Cve_admon='" & Me.DropAdmon.Text & "' "
        Dim sent As New System.Data.SqlClient.SqlCommand(stry, conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        If exe.Read() = True Then
            IDLinea = exe(0)
            Me.CveTipoLinea.Text = exe(1)
            Me.MensajeTipoLinea.Text = exe(2)



            '''''''''''DEPENDIENDO DEL TIPO DE LINEA BUSCARA SU PROGRAMACION ANUAL
            If Me.CveTipoLinea.Text = 1 Then
                'Linea Programada
                stry2 = "select a.ID,a.Unidad_Medida,a.Clasificacion,isnull(b.Enero,0)as Enero,isnull(b.Febrero,0)as Febrero, " &
                                                             "isnull(b.Marzo,0)as Marzo,isnull(b.Abril,0)as Abril,isnull(b.Mayo,0)as Mayo,isnull(b.Junio,0)as Junio, " &
                                                             "isnull(b.Julio,0)as Julio,isnull(b.Agosto,0)as Agosto,isnull(b.Septiembre,0)as Septiembre,isnull(b.Octubre,0)as Octubre, " &
                                                             "isnull(b.Noviembre,0)as Noviembre,isnull(b.Diciembre,0)as Diciembre,isnull(b.Acumulado,0)as Acumulado,c.Nombr_tipo " &
                                                             " from [PMD].Concentrado_PMD a " &
                                                             "left join [PMD].Informacion_anual b on b.ID=a.ID and b.Cve_admon='" & Me.DropAdmon.Text & "'" &
                                                             "inner join [PMD].Cat_tipo_linea c on c.Cve_tipo=a.Tipo_de_linea " &
                                                             "where a.Año='" & Me.DropAnio.Text & "' and a.Cve_admon='" & Me.DropAdmon.Text & "' and a.tipo_de_linea=" & Me.CveTipoLinea.Text & " and a.ID='" & Me.E.Text & "." & Me.O.Text & "." & Me.Es.Text & ""
            Else
                Signo = 1

                'Linea por Solicitud
                stry2 = "select a.ID,a.Unidad_Medida,a.Clasificacion,isnull(b.Enero,100)as Enero,isnull(b.Febrero,100)as Febrero, " &
                                                             "isnull(b.Marzo,100)as Marzo,isnull(b.Abril,100)as Abril,isnull(b.Mayo,100)as Mayo,isnull(b.Junio,100)as Junio, " &
                                                             "isnull(b.Julio,100)as Julio,isnull(b.Agosto,100)as Agosto,isnull(b.Septiembre,100)as Septiembre,isnull(b.Octubre,100)as Octubre, " &
                                                             "isnull(b.Noviembre,100)as Noviembre,isnull(b.Diciembre,100)as Diciembre,isnull(b.Acumulado,100)as Acumulado,c.Nombr_tipo " &
                                                             "from [PMD].Concentrado_PMD a " &
                                                             "left join [PMD].Informacion_anual b on b.ID=a.ID and b.Cve_admon='" & Me.DropAdmon.Text & "'" &
                                                             "inner join [PMD].Cat_tipo_linea c on c.Cve_tipo=a.Tipo_de_linea " &
                                                             "where a.Año='" & Me.DropAnio.Text & "' and a.Cve_admon='" & Me.DropAdmon.Text & "' and a.tipo_de_linea=" & Me.CveTipoLinea.Text & " and a.ID='" & Me.E.Text & "." & Me.O.Text & "." & Me.Es.Text & ""
            End If
            'If Me.S.Text = "" Then
            '    stry2 = stry2 & "'"
            'Else
            '    stry2 = stry2 & "." & Me.S.Text & "' "
            'End If

            Dim RsGen2 As New System.Data.SqlClient.SqlCommand(stry2, conx.conectar)
            Dim Drsx2 As System.Data.SqlClient.SqlDataReader
            Drsx2 = RsGen2.ExecuteReader
            Try
                If Not Drsx2 Is Nothing Then
                    GridaAnual1.DataSource = Drsx2
                    GridaAnual1.Font.Size = 10
                    GridaAnual1.DataBind()
                    Drsx2.NextResult()
                End If
            Finally
                Drsx2.Close()
            End Try
        Else
            GridaAnual1.DataBind()

        End If

        GridView1.DataBind()
        GridView2.DataBind()

    End Sub
    Protected Sub GridaAnual1_OnRowCommadn(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridaAnual1.RowCommand
        Select Case e.CommandName
            Case "fEne"
                NumMes = "1"
                Mes = "ENERO"

            Case "fFeb"
                NumMes = "2"
                Mes = "FEBRERO"
            Case "fMar"
                NumMes = "3"
                Mes = "MARZO"
            Case "fAbr"
                NumMes = "4"
                Mes = "ABRIL"
            Case "fMay"
                NumMes = "5"
                Mes = "MAYO"
            Case "fJun"
                NumMes = "6"
                Mes = "JUNIO"
            Case "fJul"
                NumMes = "7"
                Mes = "JULIO"
            Case "fAgo"
                NumMes = "8"
                Mes = "AGOSTO"
            Case "fSep"
                NumMes = "9"
                Mes = "SEPTIEMBRE"
            Case "fOct"
                NumMes = "10"
                Mes = "OCTUBRE"
            Case "fNov"
                NumMes = "11"
                Mes = "NOVIEMBRE"
            Case "fDic"
                NumMes = "12"
                Mes = "DICIEMBRE"
        End Select




        ConsultaDetalleDeLinea()

    End Sub
    Public Sub ConsultaDetalleDeLinea()
        'LA VARIABLE "Mes" NOS VA SERVIR PARA SABER EL TOTAL MENSUAL QUE ANTERIORMENTE EL USUARIO HABRA CAPTURADO EN SU ANUAL DEPENDIENDO EL MES
        If Me.CveTipoLinea.Text = 1 Then



            Dim stry4 = "select distinct(a.ID),a.Descr_estrategia,a.Unidad_Medida,c." & Mes & ",isnull(d.Semana1,0)as semana1,isnull(d.Semana2,0)as Semana2,isnull(d.Semana3,0)as Semana3,isnull(d.Semana4,0)as Semana4,isnull(d.Semana5,0)as Semana5,isnull(d.Semana6,0)as Semana6,isnull(d.Acumulado,0) as Acumulado,d.ComentariosP as ComP, " &
                                                                "isnull(e.Semana1,0)as semana1R,isnull(e.Semana2,0)as Semana2R,isnull(e.Semana3,0)as Semana3R,isnull(e.Semana4,0)as Semana4R,isnull(e.Semana5,0)as Semana5R,isnull(e.Semana6,0)as Semana6R,isnull(e.Acumulado,0) as AcumuladoR,e.Comentarios as ComR " &
                                                                "from [PMD].Concentrado_PMD a " &
                                                               "inner join [PMD].Informacion_Anual c on a.ID=c.ID and c.Cve_admon='" & Me.DropAdmon.Text & "' " &
                                                               "left join [PMD].Informacion_Planeada d on d.ID=a.ID and d.Mes='" & Mes & "' " &
                                                               "left join [PMD].Informacion_Real e on e.ID=a.ID and e.Mes='" & Mes & "' " &
                                                                "inner join [PMD].Cat_tipo_linea f on f.Cve_tipo=a.Tipo_de_linea " &
                                                                "where a.Año='" & DropAnio.Text & "' and a.Cve_admon='" & Me.DropAdmon.Text & "' and a.tipo_de_linea='" & Me.CveTipoLinea.Text & "' and a.ID='" & Me.E.Text & "." & Me.O.Text & "." & Me.Es.Text & ""
            'If Me.S.Text = "" Then
            '    stry4 = stry4 & "'"
            'Else
            '    stry4 = stry4 & "." & Me.S.Text & "' "
            'End If

            Dim RsGen2 As New System.Data.SqlClient.SqlCommand(stry4, conx.conectar)
            'NOTA: con el inner join a la tabla de informacion_anual nos aseguramos que NO LES APARECERA NADA SI AUN NO HAN CAPTURADO SU ANUAL DE ESE ID
            Dim Da As New System.Data.SqlClient.SqlDataAdapter
            Dim ds As New System.Data.DataSet

            Da.SelectCommand = RsGen2
            Da.Fill(ds)

            Dim Drsx2 As System.Data.SqlClient.SqlDataReader
            Drsx2 = RsGen2.ExecuteReader
            Try
                If Not Drsx2 Is Nothing Then
                    GridView1.DataSource = ds
                    GridView1.Font.Size = 10
                    CreaColumnasDs(ds)
                    GridView1.DataBind()
                    Drsx2.NextResult()
                End If
                Drsx2.Close()
                ' ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "Nota();", True)
            Catch
            End Try
            GridView2.DataBind()
        Else
            Dim stry4 = "select distinct(a.ID),a.Descr_estrategia,a.Unidad_Medida,c." & Mes & ",isnull(d.Semana1,0)as semana1,isnull(d.Semana2,0)as Semana2,isnull(d.Semana3,0)as Semana3,isnull(d.Semana4,0)as Semana4,isnull(d.Semana5,0)as Semana5,isnull(d.Semana6,0)as Semana6,isnull(d.Acumulado,0) as Acumulado,d.ComentariosP as ComP, " &
                                                           "isnull(e.Semana1,0)as semana1R,isnull(e.Semana2,0)as Semana2R,isnull(e.Semana3,0)as Semana3R,isnull(e.Semana4,0)as Semana4R,isnull(e.Semana5,0)as Semana5R,isnull(e.Semana6,0)as Semana6R,isnull(e.Acumulado,0) as AcumuladoR,e.Comentarios as ComR, " &
                                                           "isnull(f.Semana1,0)as semana1S,isnull(f.Semana2,0)as Semana2S,isnull(f.Semana3,0)as Semana3S,isnull(f.Semana4,0)as Semana4S,isnull(f.Semana5,0)as Semana5S,isnull(f.Semana6,0)as Semana6S,isnull(f.Acumulado,0) as AcumuladoS " &
                                                           "from [PMD].Concentrado_PMD a " &
                                                           "inner join [PMD].Informacion_Anual c on a.ID=c.ID and c.Cve_admon='" & Me.DropAdmon.Text & "' " &
                                                           "left join [PMD].Informacion_Planeada d on d.ID=a.ID and d.Mes='" & Mes & "' " &
                                                           "left join [PMD].Informacion_Real e on e.ID=a.ID and e.Mes='" & Mes & "' " &
                                                           "left join [PMD].Informacion_Solicitada f on f.ID=a.ID and f.Mes='" & Mes & "' " &
                                                           "inner join [PMD].Cat_tipo_linea g on g.Cve_tipo=a.Tipo_de_linea " &
                                                           "where a.Año='" & Me.DropAnio.Text & "' and a.Cve_admon='" & Me.DropAdmon.Text & "' and a.tipo_de_linea='" & Me.CveTipoLinea.Text & "' and a.ID='" & Me.E.Text & "." & Me.O.Text & "." & Me.Es.Text & ""
            'If Me.S.Text = "" Then
            '    stry4 = stry4 & "'"
            'Else
            '    stry4 = stry4 & "." & Me.S.Text & "' "
            'End If

            Dim RsGen2 As New System.Data.SqlClient.SqlCommand(stry4, conx.conectar)
            'NOTA: con el inner join a la tabla de informacion_anual nos aseguramos que NO LES APARECERA NADA SI AUN NO HAN CAPTURADO SU ANUAL DE ESE ID
            Dim Da As New System.Data.SqlClient.SqlDataAdapter
            Dim ds As New System.Data.DataSet

            Da.SelectCommand = RsGen2
            Da.Fill(ds)

            Dim Drsx2 As System.Data.SqlClient.SqlDataReader
            Drsx2 = RsGen2.ExecuteReader
            Try
                If Not Drsx2 Is Nothing Then
                    GridView2.DataSource = ds
                    GridView2.Font.Size = 10
                    CreaColumnasDs(ds)
                    GridView2.DataBind()
                    Drsx2.NextResult()
                End If
                Drsx2.Close()
                ' ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "Nota();", True)
            Catch
            End Try

            GridView1.DataBind()
            ObtenerValoresGrid2()


        End If

    End Sub
    Private Function CreaColumnasDs(ByVal ds As DataSet) As DataSet
        año = Me.DropAnio.Text

        Dim dsFinal As New DataSet
        Dim semanas As Integer = CalculaSemanas(NumMes, año)

        Dim SemanaInicial As Integer
        Dim SemanaFinal As Integer

        'Validacion que el numero de mes esta en el rango 
        If NumMes < 1 Or NumMes > 12 Then
            Throw New ArgumentException("El Numero del mes ha de estar entre 1 y 12", "NumeroMes")
        End If
        'obtenemos la primera semana del mes 
        SemanaInicial = DatePart(DateInterval.WeekOfYear, New Date(año, NumMes, 1), Microsoft.VisualBasic.FirstDayOfWeek.Monday)

        'obtenemos la ultima semana del mes 
        SemanaFinal = DatePart(DateInterval.WeekOfYear, New Date(año, NumMes + 1, 1).AddDays(-1), Microsoft.VisualBasic.FirstDayOfWeek.Monday)

        cont = (SemanaFinal + 1) - SemanaInicial 'CUANTAS SEMANAS TIENE EL MES
        Me.HiddenField1.Value = cont 'Paso el valor a un HiddenField para poder tomarlo desde javascript 


        If cont = 5 Then 'SI SON 5 OCULTARA LA 6ta COLUMNA
            For i As Integer = 1 To GridView1.Columns.Count - 4
                CalculaDiasSemana()
                If i = 1 Then
                    GridView1.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaPrimeraSemana(SemanaInicial)
                    GridView2.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaPrimeraSemana(SemanaInicial)
                Else
                    If i = 5 Then
                        GridView1.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaUltimaSemana(SemanaInicial)
                        GridView2.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaUltimaSemana(SemanaInicial)
                    Else
                        GridView1.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaSemanas(SemanaInicial)
                        GridView2.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaSemanas(SemanaInicial)
                    End If

                End If

                SemanaInicial = SemanaInicial + 1

                GridView1.Columns(6).Visible = False 'OCULTA COLUMNA
                GridView2.Columns(6).Visible = False 'OCULTA COLUMNA
            Next
        Else
            For i As Integer = 1 To GridView1.Columns.Count - 3
                CalculaDiasSemana()
                If i = 1 Then
                    GridView1.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaPrimeraSemana(SemanaInicial)
                    GridView2.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaPrimeraSemana(SemanaInicial)
                Else
                    If i = 6 Then
                        GridView1.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaUltimaSemana(SemanaInicial)
                        GridView2.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaUltimaSemana(SemanaInicial)
                    Else
                        GridView1.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaSemanas(SemanaInicial)
                        GridView2.Columns(i).HeaderText = SemanaInicial.ToString() & "<br />" & DiaSemanas(SemanaInicial)
                    End If
                End If


                SemanaInicial = SemanaInicial + 1
                GridView1.Columns(6).Visible = True  'SI SON 6, MUESTRA LA COLUMNA
                GridView2.Columns(6).Visible = True  'SI SON 6, MUESTRA LA COLUMNA
            Next
        End If
        Return ds
    End Function
    Private Function CalculaSemanas(ByVal NumeroMes As Integer, ByVal año As Integer) As Integer
        Dim SemanaInicial As Integer
        Dim SemanaFinal As Integer

        'Validacion que el numero de mes esta en el rango 
        If NumeroMes < 1 Or NumeroMes > 12 Then
            Throw New ArgumentException("El Numero del mes ha de estar entre 1 y 12", "NumeroMes")
        End If
        'obtenemos la primera semana del mes 
        SemanaInicial = DatePart(DateInterval.WeekOfYear, New Date(año, NumeroMes, 1), Microsoft.VisualBasic.FirstDayOfWeek.Monday)

        'obtenemos la ultima semana del mes 
        SemanaFinal = DatePart(DateInterval.WeekOfYear, New Date(año, NumeroMes + 1, 1).AddDays(-1), Microsoft.VisualBasic.FirstDayOfWeek.Monday)

        contador = SemanaInicial   'el contador incia en la semana inicial de ese mes
        Return (SemanaFinal - (SemanaInicial - 1))

    End Function
    Public Sub CalculaDiasSemana()
        'Dim Fecha As Date
        'Dim Semana As Integer
        'Dim Resta As Integer

        Semana = contador
        Fecha = CDate("1 / 1 / " & año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (Semana - 1) * 7 - Resta, Fecha)

        'Label4.Text = Fecha.AddDays(6)   ' Le sumamos 6 dias a la fecha para que termine la semana en domingo
        'lbld1.Text = Day(Fecha) & "-" & Day(Fecha.AddDays(6))
    End Sub
    Private Function DiaPrimeraSemana(ByVal numSemana As Integer) As String
        'Nota:Esta Funcion Se Puso Para Cuando Calcula Los dias que abarca la Primer Semana De Un Mes
        'EJ. Enero 2015, La Primera semana Tomando En cuenta Que Comience de Lunes a Domingo Seria del 29 de Dic 2014 al 4 Enero 2015
        'Sin Embargo Visualmente Solo Queremos ver los dias de Enero entonces solo Pondra el dia 1 Enero 2015 al 4 enero 2015
        Dim FEchaF As String
        Fecha = CDate("1 / 1 / " & año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (numSemana - 1) * 7 - Resta, Fecha)
        FEchaF = Day(Fecha) & "-" & Day(Fecha.AddDays(6))

        Return Day("1 / 1 / " & año) & "-" & Day(Fecha.AddDays(6))

    End Function
    Private Function DiaUltimaSemana(ByVal numSemana As Integer) As String
        'Nota:Esta Funcion Se Puso Para Cuando Calcula Los dias que abarca la Ultima Semana De Un Mes
        'EJ. Enero 2015, La Ultima semana Tomando En cuenta Que Comience de Lunes a Domingo Seria del 26 de Ene 2015 al 1 Febrero 2015
        'Sin Embargo Visualmente Solo Queremos ver los dias de Enero entonces solo Pondra el dia 26 Enero 2015 al 31 enero 2015
        Dim FEchaF As String
        Fecha = CDate("1 / 1 / " & año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (numSemana - 1) * 7 - Resta, Fecha)
        FEchaF = Day(Fecha) & "-" & Day(Fecha.AddDays(6))

        'Dim fechafin As String = Fecha.AddMonths(1).AddDays(-1)
        Dim ultimoDiaDelMes As Date

        ultimoDiaDelMes = DateSerial(Year(Fecha), Month(Fecha) + 1, 0)
        Return Day(Fecha) & "-" & Day(ultimoDiaDelMes)

    End Function
    Private Function DiaSemanas(ByVal numSemana As Integer) As String
        Fecha = CDate("1 / 1 / " & año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (numSemana - 1) * 7 - Resta, Fecha)
        Return Day(Fecha) & "-" & Day(Fecha.AddDays(6))
    End Function
    Private Sub ObtenerValoresGrid2()
        For Each row As GridViewRow In GridView2.Rows
            Dim suma As Integer = 0
            If CType(row.FindControl("LblTotalP"), Label) IsNot Nothing Then
                Dim txt1 = CType(row.FindControl("txt1"), TextBox).Text
                Dim txt2 = CType(row.FindControl("txt2"), TextBox).Text
                Dim txt3 = CType(row.FindControl("txt3"), TextBox).Text
                Dim txt4 = CType(row.FindControl("txt4"), TextBox).Text
                Dim txt5 = CType(row.FindControl("txt5"), TextBox).Text
                Dim txt6 = CType(row.FindControl("txt6"), TextBox).Text
                Dim txt7 = CType(row.FindControl("txt7"), TextBox).Text
                Dim txt8 = CType(row.FindControl("txt8"), TextBox).Text
                Dim txt9 = CType(row.FindControl("txt9"), TextBox).Text
                Dim txt10 = CType(row.FindControl("txt10"), TextBox).Text
                Dim txt11 = CType(row.FindControl("txt11"), TextBox).Text
                Dim txt12 = CType(row.FindControl("txt12"), TextBox).Text
                Dim txt13 = CType(row.FindControl("txt13"), TextBox).Text
                Dim txt14 = CType(row.FindControl("txt14"), TextBox).Text
                Dim txt15 = CType(row.FindControl("txt15"), TextBox).Text
                Dim txt16 = CType(row.FindControl("txt16"), TextBox).Text
                Dim txt17 = CType(row.FindControl("txt17"), TextBox).Text
                Dim txt18 = CType(row.FindControl("txt18"), TextBox).Text
                'Dim txtCom2 = CType(row.FindControl("txtCom2"), TextBox).Text

                'Estos son los textbox de la sexta columna si esta oculta
                If txt11 = "" Then
                    txt11 = "0"
                    CType(row.FindControl("txt11"), TextBox).Text = 0 'este es para que no desencadene la validacion si es que esta oculta
                End If
                If txt12 = "" Then
                    txt12 = "0"
                    CType(row.FindControl("txt12"), TextBox).Text = 0 'este es para que no desencadene la validacion si es que esta oculta
                End If
                If txt18 = "" Then
                    txt18 = "0" '
                    CType(row.FindControl("txt18"), TextBox).Text = 0 'este es para que no desencadene la validacion si es que esta oculta
                End If
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                CType(row.FindControl("LblTotalP"), Label).Text = (Double.Parse(txt1) + Double.Parse(txt2) + Double.Parse(txt3) + Double.Parse(txt4) + Double.Parse(txt5) + Double.Parse(txt11)) / Me.HiddenField1.Value
                CType(row.FindControl("LblTotalR"), Label).Text = Double.Parse(txt6) + Double.Parse(txt7) + Double.Parse(txt8) + Double.Parse(txt9) + Double.Parse(txt10) + Double.Parse(txt12)
                CType(row.FindControl("LblTotalS"), Label).Text = Double.Parse(txt13) + Double.Parse(txt14) + Double.Parse(txt15) + Double.Parse(txt16) + Double.Parse(txt17) + Double.Parse(txt18)


                'VALIDACIONES A LA HORA DE CALCULAR LOS PORCENTAJES
                'Primera Columna
                If txt6 = 0 And txt13 = 0 Then
                    CType(row.FindControl("lblPor1"), Label).Text = 0

                Else
                    If txt6 > 0 And txt13 = 0 Then
                        CType(row.FindControl("lblPor1"), Label).Text = 100
                    Else
                        CType(row.FindControl("lblPor1"), Label).Text = (Double.Parse(txt6) / Double.Parse(txt13)) * 100
                    End If

                End If
                'Segunda Columna
                If txt7 = 0 And txt14 = 0 Then
                    CType(row.FindControl("lblPor2"), Label).Text = 0

                Else
                    If txt7 > 0 And txt14 = 0 Then
                        CType(row.FindControl("lblPor2"), Label).Text = 100
                    Else
                        CType(row.FindControl("lblPor2"), Label).Text = (Double.Parse(txt7) / Double.Parse(txt14)) * 100
                    End If

                End If
                'Tercera Columna
                If txt8 = 0 And txt15 = 0 Then
                    CType(row.FindControl("lblPor3"), Label).Text = 0

                Else
                    If txt8 > 0 And txt15 = 0 Then
                        CType(row.FindControl("lblPor3"), Label).Text = 100
                    Else
                        CType(row.FindControl("lblPor3"), Label).Text = (Double.Parse(txt8) / Double.Parse(txt15)) * 100
                    End If

                End If
                'Cuarta Columna
                If txt9 = 0 And txt16 = 0 Then
                    CType(row.FindControl("lblPor4"), Label).Text = 0

                Else
                    If txt9 > 0 And txt16 = 0 Then
                        CType(row.FindControl("lblPor4"), Label).Text = 100
                    Else
                        CType(row.FindControl("lblPor4"), Label).Text = (Double.Parse(txt9) / Double.Parse(txt16)) * 100
                    End If

                End If
                'QUINTA Columna
                If txt10 = 0 And txt17 = 0 Then
                    CType(row.FindControl("lblPor5"), Label).Text = 0

                Else
                    If txt10 > 0 And txt17 = 0 Then
                        CType(row.FindControl("lblPor5"), Label).Text = 100
                    Else
                        CType(row.FindControl("lblPor5"), Label).Text = (Double.Parse(txt10) / Double.Parse(txt17)) * 100
                    End If

                End If
                'SEXTA Columna
                If txt12 = 0 And txt18 = 0 Then
                    CType(row.FindControl("lblPor6"), Label).Text = 0

                Else
                    If txt12 > 0 And txt18 = 0 Then
                        CType(row.FindControl("lblPor6"), Label).Text = 100
                    Else
                        CType(row.FindControl("lblPor6"), Label).Text = (Double.Parse(txt12) / Double.Parse(txt18)) * 100
                    End If

                End If
                'COLUMNA ACUMULADO MENSUAL 
                If CType(row.FindControl("LblTotalR"), Label).Text = 0 And CType(row.FindControl("LblTotalS"), Label).Text = 0 Then
                    CType(row.FindControl("lblTotalPorcentaje"), Label).Text = 0

                Else
                    If CType(row.FindControl("LblTotalR"), Label).Text > 0 And CType(row.FindControl("LblTotalS"), Label).Text = 0 Then
                        CType(row.FindControl("lblTotalPorcentaje"), Label).Text = 100
                    Else
                        CType(row.FindControl("lblTotalPorcentaje"), Label).Text = (CType(row.FindControl("LblTotalR"), Label).Text / CType(row.FindControl("LblTotalS"), Label).Text) * 100
                    End If

                End If

                ' ''''''''Damos Formato de las decimales
                CType(row.FindControl("lblTotalPorcentaje"), Label).Text = Math.Round(CType(row.FindControl("lblTotalPorcentaje"), Label).Text * 100) / 100
                CType(row.FindControl("lblPor1"), Label).Text = Math.Round(CType(row.FindControl("lblPor1"), Label).Text * 100) / 100
                CType(row.FindControl("lblPor2"), Label).Text = Math.Round(CType(row.FindControl("lblPor2"), Label).Text * 100) / 100
                CType(row.FindControl("lblPor3"), Label).Text = Math.Round(CType(row.FindControl("lblPor3"), Label).Text * 100) / 100
                CType(row.FindControl("lblPor4"), Label).Text = Math.Round(CType(row.FindControl("lblPor4"), Label).Text * 100) / 100
                CType(row.FindControl("lblPor5"), Label).Text = Math.Round(CType(row.FindControl("lblPor5"), Label).Text * 100) / 100
                CType(row.FindControl("lblPor6"), Label).Text = Math.Round(CType(row.FindControl("lblPor6"), Label).Text * 100) / 100

                'CType(row.FindControl("lblTotalPorcentaje"), Label).Text = regresaDecimales(CType(row.FindControl("lblTotalPorcentaje"), Label).Text, 2)

            End If
        Next
    End Sub


End Class

