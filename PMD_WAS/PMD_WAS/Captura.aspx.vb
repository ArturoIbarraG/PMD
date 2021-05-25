Imports System.Data
Imports Class1

Imports System.Data.SqlClient
Imports System.Collections
Imports System.Web.UI.HtmlControls
Imports System.Globalization
Imports System.Text.RegularExpressions

Partial Class Captura
    Inherits System.Web.UI.Page
    Dim Direccion As String
    Public Mes As String
    Public Secretaria As String
    Public NumMes As String 'Con el numero del mes calcularemos las semanas que contiene
    Dim Año As String
    Dim Cve_admon As String

    Dim conx As New Class1

    Dim Habilita As Integer 'Para Habilitar o Deshabilitar la captura de lo planeado
    Dim HabilitaReal As Integer 'Para Habilitar o Deshabilitar la captura de lo Real
    Public Comentarios As Integer 'Para Habilitar o Deshabilitar los comentarios del grid
    Public Registro As String
    Public contador As Integer
    Public cont As String
    Dim Fecha As Date
    Dim Semana As Integer
    Dim Resta As Integer
    Dim Tcero As Double
    Dim P As Integer 'P por primera semana que analizara para sacar los dias en la funcion DiasSemanas
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("paso") = "1" Then
        Else
            Response.Redirect("Password.aspx")
        End If

        Dim dir As String = Request.Params("Dir")
        Dim M As String = Request.Params("Mes")
        Dim Num As String = Request.Params("NumMes")
        Dim S As String = Request.Params("Secr")
        'Dim CveSecr As String = Request.Params("CveSecr")
        'Dim CveDir As String = Request.Params("CveDir")

        Direccion = dir
        Mes = M
        NumMes = Num
        Secretaria = S
        '''''''''''''UTILIZA LA VARIABLE SESSION QUE TRAE EL VALOR QUE SE LE ASIGNO EN LA PANTALLA PRECAPTURA
        Año = Session("Año")
        Cve_admon = Session("Admon")

        'Titulo de la Pagina
        Me.LblAño.Text = Año
        Me.LabelMes.Text = Mes
        Me.lblSecretaria.Text = Secretaria
        Me.LblArea.Text = Direccion
        '''''''''''''''''''''''
        CalculaSemanas(NumMes, Año) ''''CALCULARA LAS SEMANAS CONTENIDAS EN ESE AÑO Y MES
        If IsPostBack = False Then
            StaTusPyR() 'VERIFICA EL STATUS DE QUE Y QUE NO ESTA HABILITADO EN LO PLANEADO Y LO REAL
            DesgloseXdireccion() '''''''''FILTRA EL ID, ESTRATEGIA Y UNIDAD DE MEDIDA DEACUERDO AL AÑO DE TRAE EL VALOR DE LA VARIABLE SESION Y LA DIRECCION QUE SE TRAE EN EL REQUEST PARAMS

        End If
        'Con las funciones en Javascript solo pintamos en los objetos los valores de las operaciones que hacemos,asi que tenemos que jalarlos a vb para que al refrescar la pagina no se borren los resultados de esas operaciones
        ObtenerValoresGrid1()
        ObtenerValoresGrid2()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Esto es para ocultar las lineas con programacion 0
        RecorreFilasGrid1()
        RecorreFilasGrid2()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        StaTusPyR() 'VERIFICA EL STATUS DE QUE Y QUE NO ESTA HABILITADO EN LO PLANEADO Y LO REAL
        VerificaCantidadesGrid1() 'ANTES DE GUARDAR VERIFICARA QUE TODOS LOS TOTALES DE LO PLANEADO SEAN IGUALES AL TOTAL MENSUAL
    
    End Sub
    Public Sub DesgloseXdireccion()
        'LA VARIABLE "Mes" NOS VA SERVIR PARA SABER EL TOTAL MENSUAL QUE ANTERIORMENTE EL USUARIO HABRA CAPTURADO EN SU ANUAL DEPENDIENDO EL MES

        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID," &
                                                           "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_estrategia else a.Descr_estrategia end as Descr " &
                                                           ",a.Unidad_Medida,c." & Mes & ",isnull(d.Semana1,0)as semana1,isnull(d.Semana2,0)as Semana2,isnull(d.Semana3,0)as Semana3,isnull(d.Semana4,0)as Semana4,isnull(d.Semana5,0)as Semana5,isnull(d.Semana6,0)as Semana6,isnull(d.Acumulado,0) as Acumulado,d.ComentariosP as ComP, " &
                                                           "isnull(e.Semana1,0)as semana1R,isnull(e.Semana2,0)as Semana2R,isnull(e.Semana3,0)as Semana3R,isnull(e.Semana4,0)as Semana4R,isnull(e.Semana5,0)as Semana5R,isnull(e.Semana6,0)as Semana6R,isnull(e.Acumulado,0) as AcumuladoR,e.Comentarios as ComR " &
                                                           "from Concentrado_PMD a " &
                                                           "inner join Informacion_Anual c on a.ID=c.ID and c.Cve_admon='" & Cve_admon & "' and a.año=c.Año_P " &
                                                           "left join Informacion_Planeada d on d.ID=a.ID and d.Mes='" & Mes & "' and a.año=d.Año_P " &
                                                           "left join Informacion_Real e on e.ID=a.ID and e.Mes='" & Mes & "' and a.año=e.Año_R " &
                                                           "inner join Cat_tipo_linea f on f.Cve_tipo=a.Tipo_de_linea " &
                                                           "where a.Año='" & Año & "' and a.Cve_admon='" & Cve_admon & "' and a.Cve_Secr in ('" & Request.Params("CveSecr") & "') and a.Cve_Dir in ( '" & Request.Params("CveDir") & "') and a.tipo_de_linea='1'", conx.conectar)
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
        'EN EL SEGUNDO GRID DESPLEGARA LAS LINEAS xSolicitud
        'LA VARIABLE "Mes" NOS VA SERVIR PARA SABER EL TOTAL MENSUAL QUE ANTERIORMENTE EL USUARIO HABRA CAPTURADO EN SU ANUAL DEPENDIENDO EL MES

        Dim Da1 As New System.Data.SqlClient.SqlDataAdapter
        Dim ds1 As New System.Data.DataSet

        Dim RsGen3 As New System.Data.SqlClient.SqlCommand("select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID, " &
                                                           "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_estrategia else a.Descr_estrategia end as Descr, " &
                                                           "a.Unidad_Medida,c." & Mes & ",isnull(d.Semana1,0)as semana1,isnull(d.Semana2,0)as Semana2,isnull(d.Semana3,0)as Semana3,isnull(d.Semana4,0)as Semana4,isnull(d.Semana5,0)as Semana5,isnull(d.Semana6,0)as Semana6,isnull(d.Acumulado,100) as Acumulado,d.ComentariosP as ComP, " &
                                                           "isnull(e.Semana1,0)as semana1R,isnull(e.Semana2,0)as Semana2R,isnull(e.Semana3,0)as Semana3R,isnull(e.Semana4,0)as Semana4R,isnull(e.Semana5,0)as Semana5R,isnull(e.Semana6,0)as Semana6R,isnull(e.Acumulado,0) as AcumuladoR,e.Comentarios as ComR, " &
                                                           "isnull(f.Semana1,0)as semana1S,isnull(f.Semana2,0)as Semana2S,isnull(f.Semana3,0)as Semana3S,isnull(f.Semana4,0)as Semana4S,isnull(f.Semana5,0)as Semana5S,isnull(f.Semana6,0)as Semana6S,isnull(f.Acumulado,0) as AcumuladoS " &
                                                           "from Concentrado_PMD a " &
                                                           "inner join Informacion_Anual c on a.ID=c.ID and c.Cve_admon='" & Cve_admon & "' and a.año=c.Año_P " &
                                                           "left join Informacion_Planeada d on d.ID=a.ID and d.Mes='" & Mes & "' and a.año=d.Año_P " &
                                                            "left join Informacion_Real e on e.ID=a.ID and e.Mes='" & Mes & "' and a.año=e.Año_R " &
                                                             "left join Informacion_Solicitada f on f.ID=a.ID and f.Mes='" & Mes & "' and a.año=f.Año " &
                                                           "inner join Cat_tipo_linea g on g.Cve_tipo=a.Tipo_de_linea " &
                                                           "where a.Año='" & Año & "' and a.Cve_admon='" & Cve_admon & "' and a.Cve_Secr in ('" & Request.Params("CveSecr") & "') and a.Cve_Dir in ( '" & Request.Params("CveDir") & "') and a.tipo_de_linea='2' ", conx.conectar)
        'NOTA: con el inner join a la tabla de informacion_anual nos aseguramos que NO LES APARECERA NADA SI AUN NO HAN CAPTURADO SU ANUAL DE ESE ID

        Da1.SelectCommand = RsGen3
        Da1.Fill(ds1)

        Dim Drsx3 As System.Data.SqlClient.SqlDataReader
        Drsx3 = RsGen3.ExecuteReader

        Try
            If Not Drsx3 Is Nothing Then
                GridView2.DataSource = ds1
                GridView2.Font.Size = 10
                CreaColumnasDs(ds1)
                GridView2.DataBind()
                Drsx3.NextResult()
            End If
            Drsx3.Close()
        Catch
        End Try
    End Sub
    Private Function CreaColumnasDs(ByVal ds As DataSet) As DataSet
        Dim dsFinal As New DataSet
        Dim semanas As Integer = CalculaSemanas(NumMes, Año)

        Dim SemanaInicial As Integer
        Dim SemanaFinal As Integer

        'Validacion que el numero de mes esta en el rango 
        If NumMes < 1 Or NumMes > 12 Then
            Throw New ArgumentException("El Numero del mes ha de estar entre 1 y 12", "NumeroMes")
        End If
        'obtenemos la primera semana del mes 
        SemanaInicial = DatePart(DateInterval.WeekOfYear, New Date(Año, NumMes, 1), Microsoft.VisualBasic.FirstDayOfWeek.Monday)

        If NumMes <> 12 Then
            'obtenemos la ultima semana del mes 
            SemanaFinal = DatePart(DateInterval.WeekOfYear, New Date(Año, NumMes + 1, 1).AddDays(-1), Microsoft.VisualBasic.FirstDayOfWeek.Monday)
        Else
            SemanaFinal = DatePart(DateInterval.WeekOfYear, New Date(Now.Year + 1, 1, 1).AddDays(-1), Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1)

        End If
       
        'Dim cont As Integer
        cont = (SemanaFinal + 1) - SemanaInicial 'CUANTAS SEMANAS TIENE EL MES
        Me.HiddenField1.Value = cont 'Paso el valor a un HiddenField para poder tomarlo desde javascript 


        If cont = 5 Then 'SI SON 5 OCULTARA LA 6ta COLUMNA
            For i As Integer = 1 To GridView1.Columns.Count - 5
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
            For i As Integer = 1 To GridView1.Columns.Count - 4
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

        If NumeroMes <> 12 Then
            'obtenemos la ultima semana del mes 
            SemanaFinal = DatePart(DateInterval.WeekOfYear, New Date(año, NumeroMes + 1, 1).AddDays(-1), Microsoft.VisualBasic.FirstDayOfWeek.Monday)
        Else
            SemanaFinal = DatePart(DateInterval.WeekOfYear, New Date(Now.Year + 1, 1, 1).AddDays(-1), Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1)

        End If
       
        contador = SemanaInicial   'el contador incia en la semana inicial de ese mes
        Return (SemanaFinal - (SemanaInicial - 1))

    End Function
    Private Function DiaSemanas(ByVal numSemana As Integer) As String
        Fecha = CDate("1 / 1 / " & Año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (numSemana - 1) * 7 - Resta, Fecha)
        Return Day(Fecha) & "-" & Day(Fecha.AddDays(6))
    End Function
    Private Function DiaPrimeraSemana(ByVal numSemana As Integer) As String
        'Nota:Esta Funcion Se Puso Para Cuando Calcula Los dias que abarca la Primer Semana De Un Mes
        'EJ. Enero 2015, La Primera semana Tomando En cuenta Que Comience de Lunes a Domingo Seria del 29 de Dic 2014 al 4 Enero 2015
        'Sin Embargo Visualmente Solo Queremos ver los dias de Enero entonces solo Pondra el dia 1 Enero 2015 al 4 enero 2015
        Dim FEchaF As String
        Fecha = CDate("1 / 1 / " & Año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (numSemana - 1) * 7 - Resta, Fecha)
        FEchaF = Day(Fecha) & "-" & Day(Fecha.AddDays(6))

        Return Day("1 / 1 / " & Año) & "-" & Day(Fecha.AddDays(6))

    End Function
    Private Function DiaUltimaSemana(ByVal numSemana As Integer) As String
        'Nota:Esta Funcion Se Puso Para Cuando Calcula Los dias que abarca la Ultima Semana De Un Mes
        'EJ. Enero 2015, La Ultima semana Tomando En cuenta Que Comience de Lunes a Domingo Seria del 26 de Ene 2015 al 1 Febrero 2015
        'Sin Embargo Visualmente Solo Queremos ver los dias de Enero entonces solo Pondra el dia 26 Enero 2015 al 31 enero 2015
        Dim FEchaF As String
        Fecha = CDate("1 / 1 / " & Año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (numSemana - 1) * 7 - Resta, Fecha)
        FEchaF = Day(Fecha) & "-" & Day(Fecha.AddDays(6))

        'Dim fechafin As String = Fecha.AddMonths(1).AddDays(-1)
        Dim ultimoDiaDelMes As Date

        ultimoDiaDelMes = DateSerial(Year(Fecha), Month(Fecha) + 1, 0)
        Return Day(Fecha) & "-" & Day(ultimoDiaDelMes)

    End Function
    Public Sub CalculaDiasSemana()
        'Dim Fecha As Date
        'Dim Semana As Integer
        'Dim Resta As Integer

        Semana = contador
        Fecha = CDate("1 / 1 / " & Año)
        Resta = Weekday(Fecha) - 2 ''Toma como comienzo el Lunes

        Fecha = DateAdd("d", (Semana - 1) * 7 - Resta, Fecha)

        'Label4.Text = Fecha.AddDays(6)   ' Le sumamos 6 dias a la fecha para que termine la semana en domingo
        'lbld1.Text = Day(Fecha) & "-" & Day(Fecha.AddDays(6))
    End Sub
    Public Sub StaTusPyR()
        '''''''''''''''''''''''''''''''''''''''''''''''''''Verifica el status de Lo Planeado
        Dim stry2 As String
        Dim rs2 As SqlClient.SqlDataReader
        Select Case Mes
            Case "ENERO"
                stry2 = "select eneP,eneR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "FEBRERO"
                stry2 = "select febP,febR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "MARZO"
                stry2 = "select marP,marR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "ABRIL"
                stry2 = "select abrP,abrR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "MAYO"
                stry2 = "select mayP,mayR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "JUNIO"
                stry2 = "select junP,junR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "JULIO"
                stry2 = "select julP,julR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & ""
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "AGOSTO"
                stry2 = "select agoP,agoR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "SEPTIEMBRE"
                stry2 = "select sepP,sepR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "OCTUBRE"
                stry2 = "select octP,octR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "NOVIEMBRE"
                stry2 = "select novP,novR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
            Case "DICIEMBRE"
                stry2 = "select dicP,dicR from Admin_meses where Año='" & Año & "' and admon=" & Cve_admon & " "
                Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
                rs2 = cmd2.ExecuteReader()
                rs2.Read()
                Habilita = rs2(0)
                HabilitaReal = rs2(1)
                rs2.Close()
        End Select
    End Sub
    Public Sub VerificaCantidadesGrid1()
        If Habilita = 1 Then 'este solo verifica lo planeado si esta activado.
            ''''''''''''''''''''''''''''VERIFICA SI EL TOTAL QUE ESTA CAPTURANDO ES IGUAL AL TOTAL MENSUAL QUE YA HABIA CAPTURADO EN SU ANUAL
            Dim varId As String
            Dim var1 As Decimal
            Dim var2 As Decimal
            Dim var3 As Decimal
            Dim var4 As Decimal
            Dim var5 As Decimal
            Dim var11 As Decimal

            Dim Resultado As Decimal
            Dim TotalM As Decimal 'Esta es la Variable Total Mensual de ese Id

            Dim x As Integer
            x = 0
            For i As Integer = 0 To GridView1.Rows.Count - 1
                Tcero = DirectCast(GridView1.Rows(x).FindControl("lbltotalMensual"), Label).Text

                If (chkCom0.Checked = False And Tcero = 0) Then 'si estan ocultas las lineas que se programaron en ceros su mensual se brinca la validacion de cantidades
                    x = x + 1
                Else
                    varId = DirectCast(GridView1.Rows(x).FindControl("lblId"), Label).Text()
                    var1 = DirectCast(GridView1.Rows(x).FindControl("txt1"), TextBox).Text()
                    var2 = DirectCast(GridView1.Rows(x).FindControl("txt2"), TextBox).Text()
                    var3 = DirectCast(GridView1.Rows(x).FindControl("txt3"), TextBox).Text()
                    var4 = DirectCast(GridView1.Rows(x).FindControl("txt4"), TextBox).Text()
                    var5 = DirectCast(GridView1.Rows(x).FindControl("txt5"), TextBox).Text()

                    If GridView1.Columns(6).Visible = True Then
                        var11 = DirectCast(GridView1.Rows(x).FindControl("txt11"), TextBox).Text()
                    Else
                        var11 = 0
                    End If


                    Resultado = var1 + var2 + var3 + var4 + var5 + var11

                    TotalM = DirectCast(GridView1.Rows(x).FindControl("lblTotalMensual"), Label).Text()

                    If Resultado = TotalM Then
                        x = x + 1
                    Else
                        Registro = x + 1
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('El Registro " + Registro + " no Coincide con el Total Mensual Programado');", True)

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('La Línea " + varId + " no cumple/excede la meta mensual,favor de mencionar el porqué');", True)
                        GridView1.Rows(x).FindControl("txt1").Focus()
                        Return
                    End If
                End If
            Next
            ' '''''''''Si Todos los registros estaban correctos, Guarda
            GuardaPlaneado()
        Else
            'No hace la verificacion pq planeado no esta activado
            'Guarda()
        End If

        If HabilitaReal = 1 Then 'este solo verifica lo planeado si esta activado.
            ''''''''''''''''''''''''''''VERIFICA SI EL TOTAL QUE ESTA CAPTURANDO ES IGUAL AL TOTAL MENSUAL QUE YA HABIA CAPTURADO EN SU ANUAL
            Dim var6 As Double
            Dim var7 As Double
            Dim var8 As Double
            Dim var9 As Double
            Dim var10 As Double
            Dim var12 As Double
            Dim ResultadoReal As Double
            Dim varIdReal As String
            Dim txtCom2 As String 'Variable que Guarda Los Comentarios
            Dim TotalM As Double 'Esta es la Variable Total Mensual de ese Id
            Dim x As Integer
            x = 0


            For Each row As GridViewRow In GridView1.Rows
                varIdReal = DirectCast(GridView1.Rows(x).FindControl("lblId"), Label).Text()
                var6 = DirectCast(GridView1.Rows(x).FindControl("txt6"), TextBox).Text()
                var7 = DirectCast(GridView1.Rows(x).FindControl("txt7"), TextBox).Text()
                var8 = DirectCast(GridView1.Rows(x).FindControl("txt8"), TextBox).Text()
                var9 = DirectCast(GridView1.Rows(x).FindControl("txt9"), TextBox).Text()
                var10 = DirectCast(GridView1.Rows(x).FindControl("txt10"), TextBox).Text()
                'var12 = DirectCast(GridView1.Rows(x).FindControl("txt12"), TextBox).Text()

                txtCom2 = DirectCast(GridView1.Rows(x).FindControl("txtCom2"), TextBox).Text()

                If GridView1.Columns(6).Visible = True Then
                    var12 = DirectCast(GridView1.Rows(x).FindControl("txt12"), TextBox).Text()
                Else
                    var12 = 0
                End If


                ResultadoReal = var6 + var7 + var8 + var9 + var10 + var12


                TotalM = DirectCast(GridView1.Rows(x).FindControl("lblTotalMensual"), Label).Text()

                If ResultadoReal = TotalM Then
                    x = x + 1
                Else
                    'En lo Real No Importa Si Es Diferente el Total Del Mes Capturado Con el Total Acumulado
                    'Pero Sí debe de poner un comentario del porque es diferente
                    If txtCom2 = "" Then
                        GridView1.Rows(x).FindControl("txtCom2").Focus()

                        If chkCom.Checked = False Then
                            GridView1.Columns(GridView1.Columns.Count - 1).Visible = True
                        End If
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('El Registro " + Registro + " no Coincide con el Total Mensual Programado, por lo tanto guarde un comentario.');", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('La Línea " + varIdReal + " no cumple/excede la meta mensual,favor de mencionar el porqué');", True)
                        Return
                    Else
                        If validar_Mail(txtCom2) = True Then
                            x = x + 1
                        Else
                            Return
                        End If
                    End If

                End If
            Next
            ' '''''''''Si Todos los registros estaban correctos, Guarda lo REal
            GuardaReal()
        Else
            'Si no esta habilitado no hace nada
        End If
        DesgloseXdireccion() '''''''''FILTRA EL ID, ESTRATEGIA Y UNIDAD DE MEDIDA DEACUERDO AL AÑO DE TRAE EL VALOR DE LA VARIABLE SESION Y LA DIRECCION QUE SE TRAE EN EL REQUEST PARAMS

        'Con las funciones en Javascript solo pintamos en los objetos los valores de las operaciones que hacemos,asi que tenemos que jalarlos a vb para que al refrescar la pagina no se borren los resultados de esas operaciones
        ObtenerValoresGrid1()
        ObtenerValoresGrid2()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ''Esto es para ocultar las lineas con programacion 0
        RecorreFilasGrid1()
        RecorreFilasGrid2()
        For Each row As GridViewRow In GridView1.Rows
            Dim txtCom = CType(row.FindControl("HiddenComP"), HiddenField).Value
            Dim txtCom2 = CType(row.FindControl("HiddenComR"), HiddenField).Value
            CType(row.FindControl("txtCom"), TextBox).Text = txtCom
            CType(row.FindControl("txtCom2"), TextBox).Text = txtCom2
        Next
    End Sub
    Private Function validar_Mail(ByVal sMail As String) As Boolean
        ' retorna true o false   'Aqui Valida que el textbox comienze con una letra o numero
        Dim pattern As String = "^[A-Z]|[a-z]|[0-9]"
        Dim match As Match = Regex.Match(sMail, pattern)

        If match.Success Then
            If sMail.Length < 10 Then 'Aqui valida que tenga al menos 10 espacios el texbox
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('El Comentario deberá tener mínimo 10 caracteres ');", True)
                Return False
            Else
                Return True
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Favor de capturar un comentario válido ');", True)
            Return False
        End If
    End Function
    Public Sub VerificaCantidadesGrid2()
        If Habilita = 1 Then 'este solo verifica lo planeado si esta activado.
            Dim x = 1
            Registro = x
            For Each row As GridViewRow In GridView2.Rows
                Dim suma As Integer = 0


                If CType(row.FindControl("LblTotalP"), Label) IsNot Nothing Then
                    Dim varIdReal = CType(row.FindControl("lblId"), Label).Text()
                    Dim txtCom = CType(row.FindControl("txtCom"), TextBox).Text()
                    Dim txt1 = CType(row.FindControl("txt1"), TextBox).Text
                    Dim txt2 = CType(row.FindControl("txt2"), TextBox).Text
                    Dim txt3 = CType(row.FindControl("txt3"), TextBox).Text
                    Dim txt4 = CType(row.FindControl("txt4"), TextBox).Text
                    Dim txt5 = CType(row.FindControl("txt5"), TextBox).Text
                    Dim txt11 = CType(row.FindControl("txt11"), TextBox).Text
                    If txt11 = "" Then 'Esta es el textbox de la sexta columna si esta oculta
                        txt11 = "0"
                    End If
                    CType(row.FindControl("LblTotalP"), Label).Text = (Double.Parse(txt1) + Double.Parse(txt2) + Double.Parse(txt3) + Double.Parse(txt4) + Double.Parse(txt5) + Double.Parse(txt11)) / Me.HiddenField1.Value

                    If CType(row.FindControl("LblTotalP"), Label).Text = CType(row.FindControl("lblTotalMensual"), Label).Text Then
                    Else
                        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('El Registro " + Registro + " no Coincide con el Total Mensual Programado, favor de corregirlo.');", True)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('La Línea " + varIdReal + " no cumple/excede la meta mensual,favor de mencionar el porqué');", True)
                        CType(row.FindControl("txt1"), TextBox).Focus()
                        'row.Cells(0).BackColor = Drawing.Color.Coral

                        Return
                    End If
                End If
                Registro = Registro + 1

            Next
            GuardaPlaneadoLineasProgramadas()
        Else
            'Si no esta habilitado no hace nada
        End If

        If HabilitaReal = 1 Then 'este solo verifica lo planeado si esta activado.
            Dim x = 0
            Registro = x
            For Each row As GridViewRow In GridView2.Rows
                Dim suma As Integer = 0
                If CType(row.FindControl("LblTotalP"), Label) IsNot Nothing Then
                    Dim varIdReal = CType(row.FindControl("lblId"), Label).Text()
                    Dim txtCom2 = CType(row.FindControl("txtCom2"), TextBox).Text()
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
                    If txt11 = "" Then 'Esta es el textbox de la sexta columna si esta oculta
                        txt11 = "0"
                    End If
                    If txt12 = "" Then 'Esta es el textbox de la sexta columna si esta oculta
                        txt12 = "0"
                    End If
                    

                    Dim txt13 = CType(row.FindControl("txt13"), TextBox).Text
                    Dim txt14 = CType(row.FindControl("txt14"), TextBox).Text
                    Dim txt15 = CType(row.FindControl("txt15"), TextBox).Text
                    Dim txt16 = CType(row.FindControl("txt16"), TextBox).Text
                    Dim txt17 = CType(row.FindControl("txt17"), TextBox).Text
                    Dim txt18 = CType(row.FindControl("txt18"), TextBox).Text
                    If txt18 = "" Then 'Esta es el textbox de la sexta columna si esta oculta
                        txt18 = "0"
                    End If


                    CType(row.FindControl("LblTotalP"), Label).Text = (Double.Parse(txt1) + Double.Parse(txt2) + Double.Parse(txt3) + Double.Parse(txt4) + Double.Parse(txt5) + Double.Parse(txt11)) / Me.HiddenField1.Value
                   
                    CType(row.FindControl("LblTotalR"), Label).Text = Double.Parse(txt6) + Double.Parse(txt7) + Double.Parse(txt8) + Double.Parse(txt9) + Double.Parse(txt10) + Double.Parse(txt12)
                    CType(row.FindControl("LblTotalS"), Label).Text = Double.Parse(txt13) + Double.Parse(txt14) + Double.Parse(txt15) + Double.Parse(txt16) + Double.Parse(txt17) + Double.Parse(txt18)

                    'PORCENTAJES EN LABELS

                    'CType(row.FindControl("lblTotalPorcentaje"), Label).Text = (CType(row.FindControl("LblTotalR"), Label).Text / CType(row.FindControl("LblTotalS"), Label).Text) * 100
                    'CType(row.FindControl("lblTotalPorcentaje"), Label).Text = Math.Round(CType(row.FindControl("lblTotalPorcentaje"), Label).Text * 100) / 100
                    ' ''''''''Utilizo la funcion REsegresa Decimales

                    'CType(row.FindControl("lblTotalPorcentaje"), Label).Text = regresaDecimales(CType(row.FindControl("lblTotalPorcentaje"), Label).Text, 2)


                    If CType(row.FindControl("LblTotalMensual"), Label).Text = CType(row.FindControl("lblTotalPorcentaje"), Label).Text Then
                        x = x + 1
                    Else
                        If txtCom2 = "" Then
                            GridView2.Rows(x).FindControl("txtCom2").Focus()

                            If chkCom.Checked = False Then
                                GridView2.Columns(GridView2.Columns.Count - 1).Visible = True
                            End If

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('La Línea " + varIdReal + " no cumple/excede la meta mensual,favor de mencionar el porqué');", True)
                            Return
                        Else
                            If validar_Mail(txtCom2) = True Then
                                x = x + 1
                            Else
                                Return
                            End If
                        End If

                    End If
                End If
                Registro = Registro + 1
            Next
            GuardaRealLineasProgramadas()
        Else
            'Si no esta habilitado no hace nada
        End If
        DesgloseXdireccion() '''''''''FILTRA EL ID, ESTRATEGIA Y UNIDAD DE MEDIDA DEACUERDO AL AÑO DE TRAE EL VALOR DE LA VARIABLE SESION Y LA DIRECCION QUE SE TRAE EN EL REQUEST PARAMS

        'Con las funciones en Javascript solo pintamos en los objetos los valores de las operaciones que hacemos,asi que tenemos que jalarlos a vb para que al refrescar la pagina no se borren los resultados de esas operaciones
        ObtenerValoresGrid1()
        ObtenerValoresGrid2()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Esto es para ocultar las lineas con programacion 0
        RecorreFilasGrid1()
        RecorreFilasGrid2()
     
        For Each row As GridViewRow In GridView2.Rows
            Dim txtCom = CType(row.FindControl("Hidden2ComP"), HiddenField).Value
            Dim txtCom2 = CType(row.FindControl("Hidden2ComR"), HiddenField).Value
            CType(row.FindControl("txtCom"), TextBox).Text = txtCom
            CType(row.FindControl("txtCom2"), TextBox).Text = txtCom2
        Next
    End Sub
    Public Sub GuardaPlaneado()
        ''''''''''''''''''''''''''''VERIFICA SI ESTA HABILITADO LO PLANEADO
        If Habilita = 1 Then
            Dim varId As String
            Dim var1 As Double
            Dim var2 As Double
            Dim var3 As Double
            Dim var4 As Double
            Dim var5 As Double
            Dim var11 As Double
            Dim txtCom As String 'Variable que Guarda Los Comentarios
            Dim Resultado As Double


            Dim x As Integer
            x = 0

            For i = 0 To GridView1.Rows.Count - 1
                Tcero = DirectCast(GridView1.Rows(x).FindControl("lblTotalMensual"), Label).Text

                If (chkCom0.Checked = False And Tcero = 0) Then

                    'Ante si las lineas estaban ocultas y su programacion mensual era 0 se brincaba su guardado...pero debido a que en el reporte de transparencia 
                    'se pedia que aparecieran esas lineas ahora se guardan en 0 solo en la tabla de Informacion_planeada
                    ' 
                    varId = DirectCast(GridView1.Rows(x).FindControl("lblId"), Label).Text()
                    var1 = 0
                    var2 = 0
                    var3 = 0
                    var4 = 0
                    var5 = 0
                    txtCom = DirectCast(GridView1.Rows(x).FindControl("txtCom"), TextBox).Text()
                    var11 = 0

                    Resultado = var1 + var2 + var3 + var4 + var5 + var11

                    x = x + 1
                    '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                    Dim Stry1 As String
                    Dim Rs1 As SqlDataReader

                    Stry1 = "select Mes from Informacion_Planeada where ID='" & varId & "' and Mes='" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "'"

                    Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                    Rs1 = cmd1.ExecuteReader()
                    '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Mes Solo Updatea los valores
                    If Rs1.Read() = True Then

                        Dim Stry3 As String
                        Dim Rs3 As SqlDataReader

                        Stry3 = "update Informacion_Planeada set Semana1='" & var1 & "',Semana2=' " & var2 & " ',Semana3='" & var3 & "',Semana4= '" & var4 & "', Semana5='" & var5 & "',Semana6='" & var11 & "', Acumulado= ' " & Resultado & "',ComentariosP='" & txtCom & "' where ID='" & varId & "' and Mes= '" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                        Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                        Rs3 = cmd3.ExecuteReader()

                        Rs3.Read()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                        ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                    Else
                        Dim Stry4 As String
                        Dim Rs4 As SqlDataReader

                        Stry4 = "insert into Informacion_Planeada values ('" & varId & "','" & var1 & "','" & var2 & "','" & var3 & "','" & var4 & "','" & var5 & "','" & var11 & "',' " & Resultado & "','" & Mes & "','" & Año & "','" & Cve_admon & "','" & txtCom & "')"
                        Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                        Rs4 = cmd4.ExecuteReader()

                        Rs4.Read()
                    End If

                Else

                    'For Each row As GridViewRow In GridView1.Rows
                    varId = DirectCast(GridView1.Rows(x).FindControl("lblId"), Label).Text()
                    var1 = DirectCast(GridView1.Rows(x).FindControl("txt1"), TextBox).Text()
                    var2 = DirectCast(GridView1.Rows(x).FindControl("txt2"), TextBox).Text()
                    var3 = DirectCast(GridView1.Rows(x).FindControl("txt3"), TextBox).Text()
                    var4 = DirectCast(GridView1.Rows(x).FindControl("txt4"), TextBox).Text()
                    var5 = DirectCast(GridView1.Rows(x).FindControl("txt5"), TextBox).Text()
                    txtCom = DirectCast(GridView1.Rows(x).FindControl("txtCom"), TextBox).Text()

                    If GridView1.Columns(6).Visible = True Then
                        var11 = DirectCast(GridView1.Rows(x).FindControl("txt11"), TextBox).Text()
                    Else
                        var11 = 0
                    End If

                    Resultado = var1 + var2 + var3 + var4 + var5 + var11

                    x = x + 1
                    '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                    Dim Stry1 As String
                    Dim Rs1 As SqlDataReader

                    Stry1 = "select Mes from Informacion_Planeada where ID='" & varId & "' and Mes='" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "'"

                    Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                    Rs1 = cmd1.ExecuteReader()
                    '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Mes Solo Updatea los valores
                    If Rs1.Read() = True Then

                        Dim Stry3 As String
                        Dim Rs3 As SqlDataReader

                        Stry3 = "update Informacion_Planeada set Semana1='" & var1 & "',Semana2=' " & var2 & " ',Semana3='" & var3 & "',Semana4= '" & var4 & "', Semana5='" & var5 & "',Semana6='" & var11 & "', Acumulado= ' " & Resultado & "',ComentariosP='" & txtCom & "' where ID='" & varId & "' and Mes= '" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                        Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                        Rs3 = cmd3.ExecuteReader()

                        Rs3.Read()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                        ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                    Else
                        Dim Stry4 As String
                        Dim Rs4 As SqlDataReader

                        Stry4 = "insert into Informacion_Planeada values ('" & varId & "','" & var1 & "','" & var2 & "','" & var3 & "','" & var4 & "','" & var5 & "','" & var11 & "',' " & Resultado & "','" & Mes & "','" & Año & "','" & Cve_admon & "','" & txtCom & "')"
                        Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                        Rs4 = cmd4.ExecuteReader()

                        Rs4.Read()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                    End If
                End If

            Next
        End If

    End Sub
    Public Sub GuardaPlaneadoLineasProgramadas()
        ''''''''''''''''''''''''''''VERIFICA SI ESTA HABILITADO LO PLANEADO
        If Habilita = 1 Then
            Dim varId As String
            Dim var1 As Double
            Dim var2 As Double
            Dim var3 As Double
            Dim var4 As Double
            Dim var5 As Double
            Dim var11 As Double
            Dim txtCom As String 'Variable que Guarda Los Comentarios
            Dim Resultado As Double

            Dim x As Integer
            x = 0

            For i = 0 To GridView2.Rows.Count - 1
                Tcero = DirectCast(GridView2.Rows(x).FindControl("lblTotalMensual"), Label).Text

                If (chkCom0.Checked = True And Tcero = 0) Then
                    'Ante si las lineas estaban ocultas y su programacion mensual era 0 se brincaba su guardado...pero debido a que en el reporte de transparencia 
                    'se pedia que aparecieran esas lineas ahora se guardan en 0 solo en la tabla de Informacion_planeada
                    ' 
                    varId = DirectCast(GridView2.Rows(x).FindControl("lblId"), Label).Text()
                    var1 = 0
                    var2 = 0
                    var3 = 0
                    var4 = 0
                    var5 = 0
                    txtCom = DirectCast(GridView2.Rows(x).FindControl("txtCom"), TextBox).Text()
                    var11 = 0


                    Resultado = 0

                    x = x + 1
                    '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                    Dim Stry1 As String
                    Dim Rs1 As SqlDataReader

                    Stry1 = "select Mes from Informacion_Planeada where ID='" & varId & "' and Mes='" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "'"

                    Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                    Rs1 = cmd1.ExecuteReader()
                    '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Mes Solo Updatea los valores
                    If Rs1.Read() = True Then

                        Dim Stry3 As String
                        Dim Rs3 As SqlDataReader

                        Stry3 = "update Informacion_Planeada set Semana1='" & var1 & "',Semana2=' " & var2 & " ',Semana3='" & var3 & "',Semana4= '" & var4 & "', Semana5='" & var5 & "',Semana6='" & var11 & "', Acumulado= ' " & Resultado & "',ComentariosP='" & txtCom & "' where ID='" & varId & "' and Mes= '" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                        Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                        Rs3 = cmd3.ExecuteReader()

                        Rs3.Read()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                        ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                    Else
                        Dim Stry4 As String
                        Dim Rs4 As SqlDataReader

                        Stry4 = "insert into Informacion_Planeada values ('" & varId & "','" & var1 & "','" & var2 & "','" & var3 & "','" & var4 & "','" & var5 & "','" & var11 & "',' " & Resultado & "','" & Mes & "','" & Año & "','" & Cve_admon & "','" & txtCom & "')"
                        Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                        Rs4 = cmd4.ExecuteReader()

                        Rs4.Read()

                    End If
                Else

                    'For Each row As GridViewRow In GridView1.Rows
                    varId = DirectCast(GridView2.Rows(x).FindControl("lblId"), Label).Text()
                    var1 = DirectCast(GridView2.Rows(x).FindControl("txt1"), TextBox).Text()
                    var2 = DirectCast(GridView2.Rows(x).FindControl("txt2"), TextBox).Text()
                    var3 = DirectCast(GridView2.Rows(x).FindControl("txt3"), TextBox).Text()
                    var4 = DirectCast(GridView2.Rows(x).FindControl("txt4"), TextBox).Text()
                    var5 = DirectCast(GridView2.Rows(x).FindControl("txt5"), TextBox).Text()
                    txtCom = DirectCast(GridView2.Rows(x).FindControl("txtCom"), TextBox).Text()

                    If GridView2.Columns(6).Visible = True Then
                        var11 = DirectCast(GridView2.Rows(x).FindControl("txt11"), TextBox).Text()
                    Else
                        var11 = 0
                    End If

                    Resultado = (var1 + var2 + var3 + var4 + var5 + var11) / Me.HiddenField1.Value

                    x = x + 1
                    '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                    Dim Stry1 As String
                    Dim Rs1 As SqlDataReader

                    Stry1 = "select Mes from Informacion_Planeada where ID='" & varId & "' and Mes='" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "'"

                    Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                    Rs1 = cmd1.ExecuteReader()
                    '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Mes Solo Updatea los valores
                    If Rs1.Read() = True Then

                        Dim Stry3 As String
                        Dim Rs3 As SqlDataReader

                        Stry3 = "update Informacion_Planeada set Semana1='" & var1 & "',Semana2=' " & var2 & " ',Semana3='" & var3 & "',Semana4= '" & var4 & "', Semana5='" & var5 & "',Semana6='" & var11 & "', Acumulado= ' " & Resultado & "',ComentariosP='" & txtCom & "' where ID='" & varId & "' and Mes= '" & Mes & "' and Año_P='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                        Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                        Rs3 = cmd3.ExecuteReader()

                        Rs3.Read()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                        ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                    Else
                        Dim Stry4 As String
                        Dim Rs4 As SqlDataReader

                        Stry4 = "insert into Informacion_Planeada values ('" & varId & "','" & var1 & "','" & var2 & "','" & var3 & "','" & var4 & "','" & var5 & "','" & var11 & "',' " & Resultado & "','" & Mes & "','" & Año & "','" & Cve_admon & "','" & txtCom & "')"
                        Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                        Rs4 = cmd4.ExecuteReader()

                        Rs4.Read()
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                    End If
                End If

            Next
        End If

    End Sub
    Public Sub GuardaReal()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''VERIFICA SI ESTA HABILITADO LO REAL
        If HabilitaReal = 1 Then
            Dim var6 As Double
            Dim var7 As Double
            Dim var8 As Double
            Dim var9 As Double
            Dim var10 As Double
            Dim var12 As Double
            Dim ResultadoReal As Double
            Dim varIdReal As String
            Dim txtCom2 As String 'Variable que Guarda Los Comentarios

            Dim x As Integer
            x = 0

            For Each row As GridViewRow In GridView1.Rows
                varIdReal = DirectCast(GridView1.Rows(x).FindControl("lblId"), Label).Text()
                var6 = DirectCast(GridView1.Rows(x).FindControl("txt6"), TextBox).Text()
                var7 = DirectCast(GridView1.Rows(x).FindControl("txt7"), TextBox).Text()
                var8 = DirectCast(GridView1.Rows(x).FindControl("txt8"), TextBox).Text()
                var9 = DirectCast(GridView1.Rows(x).FindControl("txt9"), TextBox).Text()
                var10 = DirectCast(GridView1.Rows(x).FindControl("txt10"), TextBox).Text()
                'var12 = DirectCast(GridView1.Rows(x).FindControl("txt12"), TextBox).Text()
                txtCom2 = DirectCast(GridView1.Rows(x).FindControl("txtCom2"), TextBox).Text()
                If GridView1.Columns(6).Visible = True Then
                    var12 = DirectCast(GridView1.Rows(x).FindControl("txt12"), TextBox).Text()
                Else
                    var12 = 0
                End If


                ResultadoReal = var6 + var7 + var8 + var9 + var10 + var12

                x = x + 1
                '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                Dim Stry1 As String
                Dim Rs1 As SqlDataReader

                Stry1 = "select Mes from Informacion_Real where ID='" & varIdReal & "' and Mes='" & Mes & "' and Año_R='" & Año & "' and Cve_admon='" & Cve_admon & "' "

                Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                Rs1 = cmd1.ExecuteReader()
                '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Mes Solo Updatea los valores
                If Rs1.Read() = True Then
                    Dim Stry3 As String
                    Dim Rs3 As SqlDataReader

                    Stry3 = "update Informacion_Real set Semana1='" & var6 & "',Semana2=' " & var7 & " ',Semana3='" & var8 & "',Semana4= '" & var9 & "', Semana5='" & var10 & "',Semana6='" & var12 & "', Acumulado= ' " & ResultadoReal & "',Comentarios='" & txtCom2 & "',Cve_admon='" & Cve_admon & "' where ID='" & varIdReal & "' and Mes= '" & Mes & "' and Año_R='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                    Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                    Rs3 = cmd3.ExecuteReader()

                    Rs3.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                    ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                Else
                    Dim Stry4 As String
                    Dim Rs4 As SqlDataReader

                    Stry4 = "insert into Informacion_Real values ('" & varIdReal & "','" & var6 & "','" & var7 & "','" & var8 & "','" & var9 & "','" & var10 & "','" & var12 & "',' " & ResultadoReal & "','" & txtCom2 & "','" & Mes & "','" & Año & "','" & Cve_admon & "')"
                    Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                    Rs4 = cmd4.ExecuteReader()

                    Rs4.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                End If
            Next
        End If
    End Sub
    Private Sub GuardaRealLineasProgramadas()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''VERIFICA SI ESTA HABILITADO LO REAL
        If HabilitaReal = 1 Then
            'Dim var6 As Double
            'Dim var7 As Double
            'Dim var8 As Double
            'Dim var9 As Double
            'Dim var10 As Double
            Dim var12 As Double
            Dim var18 As Double
            'Dim ResultadoReal As Double
            'Dim varIdReal As String
            'Dim txtCom As String 'Variable que Guarda Los Comentarios

            Dim x As Integer
            x = 0

            For Each row As GridViewRow In GridView2.Rows
                Dim varIdReal = DirectCast(GridView2.Rows(x).FindControl("lblId"), Label).Text()
                Dim var6 As Double = DirectCast(GridView2.Rows(x).FindControl("txt6"), TextBox).Text()
                Dim var7 As Double = DirectCast(GridView2.Rows(x).FindControl("txt7"), TextBox).Text()
                Dim var8 As Double = DirectCast(GridView2.Rows(x).FindControl("txt8"), TextBox).Text()
                Dim var9 As Double = DirectCast(GridView2.Rows(x).FindControl("txt9"), TextBox).Text()
                Dim var10 As Double = DirectCast(GridView2.Rows(x).FindControl("txt10"), TextBox).Text()
                If GridView2.Columns(6).Visible = True Then
                    'var12 = DirectCast(GridView1.Rows(x).FindControl("txt12"), TextBox).Text()
                    var12 = DirectCast(GridView2.Rows(x).FindControl("txt12"), TextBox).Text()
                    var18 = DirectCast(GridView2.Rows(x).FindControl("txt18"), TextBox).Text()
                Else
                    var12 = 0
                    var18 = 0
                End If

                Dim var13 As Double = DirectCast(GridView2.Rows(x).FindControl("txt13"), TextBox).Text()
                Dim var14 As Double = DirectCast(GridView2.Rows(x).FindControl("txt14"), TextBox).Text()
                Dim var15 As Double = DirectCast(GridView2.Rows(x).FindControl("txt15"), TextBox).Text()
                Dim var16 As Double = DirectCast(GridView2.Rows(x).FindControl("txt16"), TextBox).Text()
                Dim var17 As Double = DirectCast(GridView2.Rows(x).FindControl("txt17"), TextBox).Text()

                Dim txtCom2 = DirectCast(GridView2.Rows(x).FindControl("txtCom2"), TextBox).Text()

                Dim ResultadoRealizado = var6 + var7 + var8 + var9 + var10 + var12
                Dim ResultadoSolicitado = var13 + var14 + var15 + var16 + var17 + var18
                'Dim PorcentajeRealizado As Double = DirectCast(GridView2.Rows(x).FindControl("lblTotalPorcentaje"), Label).Text()

                x = x + 1
                '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                Dim Stry1 As String
                Dim Rs1 As SqlDataReader

                Stry1 = "select Mes from Informacion_Real where ID='" & varIdReal & "' and Mes='" & Mes & "' and Año_R='" & Año & "' and Cve_admon='" & Cve_admon & "' "

                Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                Rs1 = cmd1.ExecuteReader()

                '''''
                'Dim Stry5 As String
                'Dim Rs5 As SqlDataReader

                'Stry5 = "select Mes from Informacion_Solicitada where ID='" & varIdReal & "' and Mes='" & Mes & "' and Año='" & Año & "' and Cve_admon='" & Cve_admon & "' "

                'Dim cmd5 As New Data.SqlClient.SqlCommand(Stry5, conx.conectar())
                'Rs5 = cmd5.ExecuteReader()

                '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Mes Solo Updatea los valores
                If Rs1.Read() = True Then

                    Dim Stry3 As String
                    Dim Rs3 As SqlDataReader

                    Stry3 = "update Informacion_Real set Semana1='" & var6 & "',Semana2=' " & var7 & " ',Semana3='" & var8 & "',Semana4= '" & var9 & "', Semana5='" & var10 & "',Semana6='" & var12 & "', Acumulado= ' " & ResultadoRealizado & "',Comentarios='" & txtCom2 & "',Cve_admon='" & Cve_admon & "' where ID='" & varIdReal & "' and Mes= '" & Mes & "' and Año_R='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                    Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                    Rs3 = cmd3.ExecuteReader()

                    Rs3.Read()


                    'Dim Stry6 As String
                    'Dim Rs6 As SqlDataReader

                    'Stry6 = "update Informacion_Solicitada set Semana1='" & var13 & "',Semana2=' " & var14 & " ',Semana3='" & var15 & "',Semana4= '" & var16 & "', Semana5='" & var17 & "',Semana6='" & var18 & "', Acumulado= ' " & ResultadoSolicitado & "',Comentarios='" & txtCom2 & "',Cve_admon='" & Cve_admon & "' where ID='" & varIdReal & "' and Mes= '" & Mes & "' and Año='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                    'Dim cmd6 As New Data.SqlClient.SqlCommand(Stry6, conx.conectar())
                    'Rs6 = cmd6.ExecuteReader()

                    'Rs6.Read()
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                    ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                Else
                    Dim Stry4 As String
                    Dim Rs4 As SqlDataReader

                    Stry4 = "insert into Informacion_Real values ('" & varIdReal & "','" & var6 & "','" & var7 & "','" & var8 & "','" & var9 & "','" & var10 & "','" & var12 & "',' " & ResultadoRealizado & "','" & txtCom2 & "','" & Mes & "','" & Año & "','" & Cve_admon & "')"
                    Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                    Rs4 = cmd4.ExecuteReader()

                    Rs4.Read()
                    ' ''''
                    'Dim Stry7 As String
                    'Dim Rs7 As SqlDataReader

                    'Stry7 = "insert into Informacion_Solicitada values ('" & varIdReal & "','" & var13 & "','" & var14 & "','" & var15 & "','" & var16 & "','" & var17 & "','" & var18 & "',' " & ResultadoSolicitado & "','" & txtCom2 & "','" & Mes & "','" & Año & "','" & Cve_admon & "')"
                    'Dim cmd7 As New Data.SqlClient.SqlCommand(Stry7, conx.conectar())
                    'Rs7 = cmd7.ExecuteReader()

                    'Rs7.Read()
                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                End If

                'Nuevo
                Stry1 = "select Mes from Informacion_Solicitada where ID='" & varIdReal & "' and Mes='" & Mes & "' and Año='" & Año & "' and Cve_admon='" & Cve_admon & "' "

                Dim cmd2 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                Rs1 = cmd2.ExecuteReader()


                '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Mes Solo Updatea los valores
                If Rs1.Read() = True Then

                    Dim Stry6 As String
                    Dim Rs6 As SqlDataReader

                    Stry6 = "update Informacion_Solicitada set Semana1='" & var13 & "',Semana2=' " & var14 & " ',Semana3='" & var15 & "',Semana4= '" & var16 & "', Semana5='" & var17 & "',Semana6='" & var18 & "', Acumulado= ' " & ResultadoSolicitado & "',Comentarios='" & txtCom2 & "',Cve_admon='" & Cve_admon & "' where ID='" & varIdReal & "' and Mes= '" & Mes & "' and Año='" & Año & "' and Cve_admon='" & Cve_admon & "' "
                    Dim cmd6 As New Data.SqlClient.SqlCommand(Stry6, conx.conectar())
                    Rs6 = cmd6.ExecuteReader()

                    Rs6.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                    ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                Else
                
                    Dim Stry7 As String
                    Dim Rs7 As SqlDataReader

                    Stry7 = "insert into Informacion_Solicitada values ('" & varIdReal & "','" & var13 & "','" & var14 & "','" & var15 & "','" & var16 & "','" & var17 & "','" & var18 & "',' " & ResultadoSolicitado & "','" & txtCom2 & "','" & Mes & "','" & Año & "','" & Cve_admon & "')"
                    Dim cmd7 As New Data.SqlClient.SqlCommand(Stry7, conx.conectar())
                    Rs7 = cmd7.ExecuteReader()

                    Rs7.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                End If



                '**************************************



            Next
        End If
    End Sub
    Protected Sub chkCom_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCom.CheckedChanged
        GridView1.Columns(GridView1.Columns.Count - 1).Visible = chkCom.Checked
        For Each row As GridViewRow In GridView1.Rows
            Dim txtCom = CType(row.FindControl("HiddenComP"), HiddenField).Value
            Dim txtCom2 = CType(row.FindControl("HiddenComR"), HiddenField).Value
            CType(row.FindControl("txtCom"), TextBox).Text = txtCom
            CType(row.FindControl("txtCom2"), TextBox).Text = txtCom2
        Next
    End Sub
    Private Sub ObtenerValoresGrid1()
        For Each row As GridViewRow In GridView1.Rows
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
              
                If txt11 = "" Then 'Esta es el textbox de la sexta columna si esta oculta
                    txt11 = "0"
                    CType(row.FindControl("txt11"), TextBox).Text = 0
                End If
                If txt12 = "" Then 'Esta es el textbox de la sexta columna si esta oculta
                    txt12 = "0"
                    CType(row.FindControl("txt12"), TextBox).Text = 0
                End If
                CType(row.FindControl("LblTotalP"), Label).Text = Double.Parse(txt1) + Double.Parse(txt2) + Double.Parse(txt3) + Double.Parse(txt4) + Double.Parse(txt5) + Double.Parse(txt11)
                CType(row.FindControl("LblTotalR"), Label).Text = Double.Parse(txt6) + Double.Parse(txt7) + Double.Parse(txt8) + Double.Parse(txt9) + Double.Parse(txt10) + Double.Parse(txt12)
                
            End If
        Next
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim txt1 As New TextBox
        txt1 = e.Row.FindControl("txt1")
        Dim txt6 As New TextBox
        txt6 = e.Row.FindControl("txt6")
        Dim txtCom As New TextBox
        txtCom = e.Row.FindControl("txtCom")
        Dim txtCom2 As New TextBox
        txtCom2 = e.Row.FindControl("txtCom2")
        If Not txt1 Is Nothing And Not txt6 Is Nothing Then
            txt1.Enabled = Habilita
            txt6.Enabled = HabilitaReal
            txtCom.Visible = Habilita
            txtCom2.Visible = HabilitaReal
        End If

        Dim txt2 As New TextBox
        txt2 = e.Row.FindControl("txt2")
        Dim txt7 As New TextBox
        txt7 = e.Row.FindControl("txt7")
        If Not txt2 Is Nothing And Not txt7 Is Nothing Then
            txt2.Enabled = Habilita
            txt7.Enabled = HabilitaReal
        End If

        Dim txt3 As New TextBox
        txt3 = e.Row.FindControl("txt3")
        Dim txt8 As New TextBox
        txt8 = e.Row.FindControl("txt8")
        If Not txt3 Is Nothing And Not txt8 Is Nothing Then
            txt3.Enabled = Habilita
            txt8.Enabled = HabilitaReal
        End If

        Dim txt4 As New TextBox
        txt4 = e.Row.FindControl("txt4")
        Dim txt9 As New TextBox
        txt9 = e.Row.FindControl("txt9")
        If Not txt4 Is Nothing And Not txt9 Is Nothing Then
            txt4.Enabled = Habilita
            txt9.Enabled = HabilitaReal
        End If

        Dim txt5 As New TextBox
        txt5 = e.Row.FindControl("txt5")
        Dim txt10 As New TextBox
        txt10 = e.Row.FindControl("txt10")
        If Not txt5 Is Nothing And Not txt10 Is Nothing Then
            txt5.Enabled = Habilita
            txt10.Enabled = HabilitaReal
        End If


        Dim txt11 As New TextBox
        txt11 = e.Row.FindControl("txt11")
        Dim txt12 As New TextBox
        txt12 = e.Row.FindControl("txt12")
        If Not txt11 Is Nothing And Not txt12 Is Nothing Then
            txt11.Enabled = Habilita
            txt12.Enabled = HabilitaReal
        End If
    End Sub
    Private Sub RecorreFilasGrid1()
        'RECORRER FILAS DEL GRID para poder ocultar los que traigan valor 0 en su total mensual
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Tcero = DirectCast(GridView1.Rows(i).FindControl("lblTotalMensual"), Label).Text
            If Tcero = 0 Then
                If chkCom0.Checked = True Then
                    GridView1.Rows(i).Visible = True
                Else
                    GridView1.Rows(i).Visible = False
                End If
            End If
        Next
    End Sub
    Protected Sub chkCom0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCom0.CheckedChanged
        RecorreFilasGrid1()
    End Sub
    Private Sub RecorreFilasGrid2()
        'RECORRER FILAS DEL GRID para poder ocultar los que traigan valor 0 en su total mensual
        For i As Integer = 0 To GridView2.Rows.Count - 1
            Tcero = DirectCast(GridView2.Rows(i).FindControl("lblTotalMensual"), Label).Text
            If Tcero = 0 Then
                If ChkCorte2.Checked = True Then
                    GridView2.Rows(i).Visible = True
                Else
                    GridView2.Rows(i).Visible = False
                End If
            End If
        Next
    End Sub
    Protected Sub ChkCom2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCom2.CheckedChanged
        GridView2.Columns(GridView2.Columns.Count - 1).Visible = ChkCom2.Checked
        For Each row As GridViewRow In GridView2.Rows
            Dim txtCom = CType(row.FindControl("Hidden2ComP"), HiddenField).Value
            Dim txtCom2 = CType(row.FindControl("Hidden2ComR"), HiddenField).Value
            CType(row.FindControl("txtCom"), TextBox).Text = txtCom
            CType(row.FindControl("txtCom2"), TextBox).Text = txtCom2
        Next
    End Sub
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
                If ((CType(row.FindControl("LblTotalR"), Label).Text = 0) And (CType(row.FindControl("LblTotalS"), Label).Text = 0) And (CType(row.FindControl("lblTotalMensual"), Label).Text = 0)) Then
                    CType(row.FindControl("lblTotalPorcentaje"), Label).Text = 0
                Else
                    CType(row.FindControl("lblTotalPorcentaje"), Label).Text = 100
                End If

                If CType(row.FindControl("LblTotalR"), Label).Text > 0 And CType(row.FindControl("LblTotalS"), Label).Text = 0 Then
                    CType(row.FindControl("lblTotalPorcentaje"), Label).Text = 100
                End If
                If CType(row.FindControl("LblTotalR"), Label).Text > 0 And CType(row.FindControl("LblTotalS"), Label).Text > 0 Then
                    CType(row.FindControl("lblTotalPorcentaje"), Label).Text = (CType(row.FindControl("LblTotalR"), Label).Text / CType(row.FindControl("LblTotalS"), Label).Text) * 100
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
    Protected Sub ChkCorte2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkCorte2.CheckedChanged
        RecorreFilasGrid2()
    End Sub
    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        Dim txt1 As New TextBox
        txt1 = e.Row.FindControl("txt1")
        Dim txt6 As New TextBox
        txt6 = e.Row.FindControl("txt6")
        Dim txt13 As New TextBox
        txt13 = e.Row.FindControl("txt13")
        Dim txtCom As New TextBox
        txtCom = e.Row.FindControl("txtCom")
        Dim txtCom2 As New TextBox
        txtCom2 = e.Row.FindControl("txtCom2")
        If (Not txt1 Is Nothing And Not txt6 Is Nothing And Not txt13 Is Nothing) Then
            txt1.Enabled = Habilita
            txt6.Enabled = HabilitaReal
            txt13.Enabled = HabilitaReal
            txtCom.Visible = Habilita
            txtCom2.Visible = HabilitaReal
        End If

        Dim txt2 As New TextBox
        txt2 = e.Row.FindControl("txt2")
        Dim txt7 As New TextBox
        txt7 = e.Row.FindControl("txt7")
        Dim txt14 As New TextBox
        txt14 = e.Row.FindControl("txt14")
        If (Not txt2 Is Nothing And Not txt7 Is Nothing And Not txt14 Is Nothing) Then
            txt2.Enabled = Habilita
            txt7.Enabled = HabilitaReal
            txt14.Enabled = HabilitaReal
           
        End If

        Dim txt3 As New TextBox
        txt3 = e.Row.FindControl("txt3")
        Dim txt8 As New TextBox
        txt8 = e.Row.FindControl("txt8")
        Dim txt15 As New TextBox
        txt15 = e.Row.FindControl("txt15")
        If (Not txt3 Is Nothing And Not txt8 Is Nothing And Not txt15 Is Nothing) Then
            txt3.Enabled = Habilita
            txt8.Enabled = HabilitaReal
            txt15.Enabled = HabilitaReal
        End If

        Dim txt4 As New TextBox
        txt4 = e.Row.FindControl("txt4")
        Dim txt9 As New TextBox
        txt9 = e.Row.FindControl("txt9")
        Dim txt16 As New TextBox
        txt16 = e.Row.FindControl("txt16")
        If (Not txt4 Is Nothing And Not txt9 Is Nothing And Not txt16 Is Nothing) Then
            txt4.Enabled = Habilita
            txt9.Enabled = HabilitaReal
            txt16.Enabled = HabilitaReal
        End If

        Dim txt5 As New TextBox
        txt5 = e.Row.FindControl("txt5")
        Dim txt10 As New TextBox
        txt10 = e.Row.FindControl("txt10")
        Dim txt17 As New TextBox
        txt17 = e.Row.FindControl("txt17")
        If (Not txt5 Is Nothing And Not txt10 Is Nothing And Not txt17 Is Nothing) Then
            txt5.Enabled = Habilita
            txt10.Enabled = HabilitaReal
            txt17.Enabled = HabilitaReal
        End If

        Dim txt11 As New TextBox
        txt11 = e.Row.FindControl("txt11")
        Dim txt12 As New TextBox
        txt12 = e.Row.FindControl("txt12")
        Dim txt18 As New TextBox
        txt18 = e.Row.FindControl("txt18")
        If (Not txt11 Is Nothing And Not txt12 Is Nothing And Not txt18 Is Nothing) Then
            txt11.Enabled = Habilita
            txt12.Enabled = HabilitaReal
            txt18.Enabled = HabilitaReal
        End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        StaTusPyR() 'VERIFICA EL STATUS DE QUE Y QUE NO ESTA HABILITADO EN LO PLANEADO Y LO REAL
        VerificaCantidadesGrid2() 'ANTES DE GUARDAR VERIFICARA QUE TODOS LOS TOTALES DE LO PLANEADO SEAN IGUALES AL TOTAL MENSUAL
    End Sub
    Private Function regresaDecimales(ByVal num As Double, ByVal cantDec As Integer) As Decimal
        Dim result As Decimal
        Dim numero As Decimal
        If num = 0 Then
        Else
            numero = CType(num, Decimal)
            Dim numDividido() As String
            numDividido = numero.ToString().Split(".")
            If numDividido.Length > 1 Then
                If numDividido(1).Length > cantDec Then
                    result = CType(numDividido(0) & "." & numDividido(1).Substring(0, cantDec), Decimal)
                Else
                    result = CType(numDividido(0) & "." & completaDec(numDividido(1), cantDec), Decimal)
                End If
            Else
                result = CType(numero.ToString() & "." & completaDec("0", cantDec), Decimal)
            End If
        End If
       
        Return result
    End Function
    Private Function completaDec(ByVal num As String, ByVal cant As Integer) As String
        While num.Length < cant
            num = num & "0"
        End While
        Return num
    End Function
End Class
