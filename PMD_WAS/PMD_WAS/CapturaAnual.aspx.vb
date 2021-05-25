Imports System.Data
Imports Class1
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Web.UI.HtmlControls
Partial Class CapturaAnual
    Inherits System.Web.UI.Page
    Dim Direccion As String
    Dim Mes As String
    Dim Año As String
    Dim CveAdmon As String
    Public Secretaria As String
    Dim conx As New Class1
    Public Habilita As Integer
    Public HabilitaReal As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("paso") = "1" Then

        Else
            Response.Redirect("Password.aspx")

        End If

        Dim dir As String = Request.Params("Dir")
        Dim M As String = Request.Params("Mes")
        Dim S As String = Request.Params("Secr")
        Direccion = dir
        Mes = M
        Secretaria = S
        '''''''''''''UTILIZA LA VARIABLE SESSION QUE TRAE EL VALOR QUE SE LE ASIGNO EN LA PANTALLA PRECAPTURA
        Año = Session("Año")
        CveAdmon = Session("Admon")

        'Titulo de la Pagina
        Me.LblAño.Text = Año
        Me.LabelMes.Text = Mes
        Me.lblSecretaria.Text = Secretaria
        Me.LblArea.Text = Direccion
        '''''''''''''''''''''''
        If IsPostBack = False Then
            '''''''''FILTRA EL ID, ESTRATEGIA Y UNIDAD DE MEDIDA DEACUERDO AL AÑO DE TRAE EL VALOR DE LA VARIABLE SESION Y LA DIRECCION QUE SE TRAE EN EL REQUEST PARAMS
            DesgloseXdireccion()
        End If
        '''''''''''''VERIFICA EL STATUS DE QUE Y QUE NO ESTA HABILITADO EN LO PLANEADO Y LO REAL
        StaTusPyR()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        GuardaLineasPlaneadas()
        GuardaLineasProgramadas()
    End Sub
    Public Sub DesgloseXdireccion()
        'EN EL PRIMER GRID DESPLAGARA LAS LINEAS PROGRAMADAS eso lo define en el where tipo_de_linea
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID," &
                                                           "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_Linea else a.descr_sublinea end as Descr, " &
                                                           "a.Unidad_Medida,isnull(b.CostoAnual,0)as CostoAnual,isnull(b.Enero,0)as Enero,isnull(b.Febrero,0)as Febrero, " &
                                                           "isnull(b.Marzo,0)as Marzo,isnull(b.Abril,0)as Abril,isnull(b.Mayo,0)as Mayo,isnull(b.Junio,0)as Junio, " &
                                                           "isnull(b.Julio,0)as Julio,isnull(b.Agosto,0)as Agosto,isnull(b.Septiembre,0)as Septiembre,isnull(b.Octubre,0)as Octubre, " &
                                                           "isnull(b.Noviembre,0)as Noviembre,isnull(b.Diciembre,0)as Diciembre,isnull(b.Acumulado,0)as Acumulado,c.Nombr_tipo " &
                                                           " from pmd.Concentrado_PMD a " &
                                                           "left join Informacion_anual b on b.ID=a.ID and b.Cve_admon='" & CveAdmon & "'  and a.año=b.Año_P " &
                                                           "inner join Cat_tipo_linea c on c.Cve_tipo=a.Tipo_de_linea " &
                                                           "where a.Año='" & Año & "' and a.Cve_admon='" & CveAdmon & "' and a.Secretaria in ('" & Request.Params("Secr") & "') and Direccion in ( '" & Direccion & "') and a.tipo_de_linea='1' ", conx.conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader
        Try
            If Not Drsx2 Is Nothing Then
                GridView1.DataSource = Drsx2
                GridView1.Font.Size = 12
                GridView1.DataBind()
                Drsx2.NextResult()
            End If
        Finally
            Drsx2.Close()
        End Try
        'EN EL SEGUNDO GRID DESPLEGARA LAS LINEAS POR SOLICITUD
        Dim sent As New System.Data.SqlClient.SqlCommand("select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID," & _
                                                         "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_Linea else a.descr_sublinea end as Descr, " & _
                                                         "a.Unidad_Medida,isnull(b.CostoAnual,0)as CostoAnual,isnull(b.Enero,100)as Enero,isnull(b.Febrero,100)as Febrero, " & _
                                                         "isnull(b.Marzo,100)as Marzo,isnull(b.Abril,100)as Abril,isnull(b.Mayo,100)as Mayo,isnull(b.Junio,100)as Junio, " & _
                                                         "isnull(b.Julio,100)as Julio,isnull(b.Agosto,100)as Agosto,isnull(b.Septiembre,100)as Septiembre,isnull(b.Octubre,100)as Octubre, " & _
                                                         "isnull(b.Noviembre,100)as Noviembre,isnull(b.Diciembre,100)as Diciembre,isnull(b.Acumulado,100)as Acumulado,c.Nombr_tipo " & _
                                                         "from Concentrado_PMD a " & _
                                                         "left join Informacion_anual b on b.ID=a.ID and b.Cve_admon='" & CveAdmon & "'  and a.año=b.Año_P " & _
                                                         "inner join Cat_tipo_linea c on c.Cve_tipo=a.Tipo_de_linea " & _
                                                         "where a.Año='" & Año & "' and a.Cve_admon='" & CveAdmon & "' and a.Secretaria in ('" & Request.Params("Secr") & "') and Direccion in ( '" & Direccion & "') and a.tipo_de_linea='2' ", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                GridView2.DataSource = exe
                GridView2.Font.Size = 12
                GridView2.DataBind()
                exe.NextResult()
            End If
        Catch ex As Exception
            Drsx2.Close()
        End Try
    End Sub
    Public Sub StaTusPyR()
        '''''''''''''''''''''''''''''''''''''''''''''''''''Verifica el status de Lo Planeado
        Dim stry2 As String
        Dim rs2 As SqlClient.SqlDataReader
        stry2 = "select HabilitaPlaneado from Admin where Mes='" & Mes & "'"

        Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
        rs2 = cmd2.ExecuteReader()
        While rs2.Read()
            Habilita = rs2(0)
        End While
        rs2.Close()
        '''''''''''''''''''''''''''''''''''''''''''''''''''Verifica el status de Lo Real
        Dim stry3 As String
        Dim rs3 As SqlClient.SqlDataReader
        stry3 = "select HabilitaReal from Admin where Mes='" & Mes & "'"

        Dim cmd3 As New Data.SqlClient.SqlCommand(stry3, conx.conectar())
        rs3 = cmd3.ExecuteReader()
        While rs3.Read()
            HabilitaReal = rs3(0)
        End While
        rs3.Close()
    End Sub
    Public Sub GuardaLineasPlaneadas()
        ''''''''''''''''''''''''''''VERIFICA SI ESTA HABILITADO LO PLANEADO
        If Habilita = 1 Then

            Dim varCostoAnual As Double
            Dim var1 As Double
            Dim var2 As Double
            Dim var3 As Double
            Dim var4 As Double
            Dim var5 As Double
            Dim var6 As Double
            Dim var7 As Double
            Dim var8 As Double
            Dim var9 As Double
            Dim var10 As Double
            Dim var11 As Double
            Dim var12 As Double
            Dim Resultado As Double
            Dim varId As String

            Dim x As Integer
            x = 0

            For Each row As GridViewRow In GridView1.Rows

                varId = DirectCast(GridView1.Rows(x).FindControl("lblId"), Label).Text()
                varCostoAnual = DirectCast(GridView1.Rows(x).FindControl("txtCostoAnual"), TextBox).Text()
                var1 = DirectCast(GridView1.Rows(x).FindControl("txt1"), TextBox).Text()
                var2 = DirectCast(GridView1.Rows(x).FindControl("txt2"), TextBox).Text()
                var3 = DirectCast(GridView1.Rows(x).FindControl("txt3"), TextBox).Text()
                var4 = DirectCast(GridView1.Rows(x).FindControl("txt4"), TextBox).Text()
                var5 = DirectCast(GridView1.Rows(x).FindControl("txt5"), TextBox).Text()
                var6 = DirectCast(GridView1.Rows(x).FindControl("txt6"), TextBox).Text()
                var7 = DirectCast(GridView1.Rows(x).FindControl("txt7"), TextBox).Text()
                var8 = DirectCast(GridView1.Rows(x).FindControl("txt8"), TextBox).Text()
                var9 = DirectCast(GridView1.Rows(x).FindControl("txt9"), TextBox).Text()
                var10 = DirectCast(GridView1.Rows(x).FindControl("txt10"), TextBox).Text()
                var11 = DirectCast(GridView1.Rows(x).FindControl("txt11"), TextBox).Text()
                var12 = DirectCast(GridView1.Rows(x).FindControl("txt12"), TextBox).Text()
                Resultado = var1 + var2 + var3 + var4 + var5 + var6 + var7 + var8 + var9 + var10 + var11 + var12

                x = x + 1
                '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                Dim Stry1 As String
                Dim Rs1 As SqlDataReader

                Stry1 = "select ID from Informacion_Anual where ID='" & varId & "' and Año_P='" & Año & "' and Cve_admon='" & CveAdmon & "'"

                Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                Rs1 = cmd1.ExecuteReader()
                '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Año Solo Updatea los valores
                If Rs1.Read() = True Then

                    Dim Stry3 As String
                    Dim Rs3 As SqlDataReader

                    Stry3 = "update Informacion_Anual set CostoAnual='" & varCostoAnual & "',Enero='" & var1 & "',Febrero='" & var2 & "',Marzo='" & var3 & "',Abril= '" & var4 & "', Mayo='" & var5 & "',Junio='" & var6 & "',Julio='" & var7 & "',Agosto='" & var8 & "',Septiembre= '" & var9 & "', Octubre='" & var10 & "',Noviembre= '" & var11 & "', Diciembre='" & var12 & "',Acumulado= '" & Resultado & "' where ID='" & varId & "' and Año_P='" & Año & "' and Cve_admon='" & CveAdmon & "' "
                    Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                    Rs3 = cmd3.ExecuteReader()

                    Rs3.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                    ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                Else
                    Dim Stry4 As String
                    Dim Rs4 As SqlDataReader

                    Stry4 = "insert into Informacion_Anual(ID,CostoAnual,Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre,Acumulado,Año_P,Cve_admon) values ('" & varId & "','" & varCostoAnual & "','" & var1 & "','" & var2 & "','" & var3 & "','" & var4 & "','" & var5 & "','" & var6 & "','" & var7 & "','" & var8 & "','" & var9 & "','" & var10 & "','" & var11 & "','" & var12 & "','" & Resultado & "','" & Año & "','" & CveAdmon & "')"
                    Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                    Rs4 = cmd4.ExecuteReader()

                    Rs4.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                End If
            Next
        End If
    End Sub
    Public Sub GuardaLineasProgramadas()
        ''''''''''''''''''''''''''''VERIFICA SI ESTA HABILITADO LO PLANEADO
        If Habilita = 1 Then

            Dim varCostoAnual As Double
            Dim var1 As Double
            Dim var2 As Double
            Dim var3 As Double
            Dim var4 As Double
            Dim var5 As Double
            Dim var6 As Double
            Dim var7 As Double
            Dim var8 As Double
            Dim var9 As Double
            Dim var10 As Double
            Dim var11 As Double
            Dim var12 As Double
            Dim Resultado As Double
            Dim varId As String

            Dim x As Integer
            x = 0

            For Each row As GridViewRow In GridView2.Rows
                varId = DirectCast(GridView2.Rows(x).FindControl("lblId"), Label).Text()
                varCostoAnual = DirectCast(GridView2.Rows(x).FindControl("txtCostoAnual"), TextBox).Text()
                var1 = DirectCast(GridView2.Rows(x).FindControl("txt1"), TextBox).Text()
                var2 = DirectCast(GridView2.Rows(x).FindControl("txt2"), TextBox).Text()
                var3 = DirectCast(GridView2.Rows(x).FindControl("txt3"), TextBox).Text()
                var4 = DirectCast(GridView2.Rows(x).FindControl("txt4"), TextBox).Text()
                var5 = DirectCast(GridView2.Rows(x).FindControl("txt5"), TextBox).Text()
                var6 = DirectCast(GridView2.Rows(x).FindControl("txt6"), TextBox).Text()
                var7 = DirectCast(GridView2.Rows(x).FindControl("txt7"), TextBox).Text()
                var8 = DirectCast(GridView2.Rows(x).FindControl("txt8"), TextBox).Text()
                var9 = DirectCast(GridView2.Rows(x).FindControl("txt9"), TextBox).Text()
                var10 = DirectCast(GridView2.Rows(x).FindControl("txt10"), TextBox).Text()
                var11 = DirectCast(GridView2.Rows(x).FindControl("txt11"), TextBox).Text()
                var12 = DirectCast(GridView2.Rows(x).FindControl("txt12"), TextBox).Text()
                Resultado = (var1 + var2 + var3 + var4 + var5 + var6 + var7 + var8 + var9 + var10 + var11 + var12) / 12

                x = x + 1
                '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el mes de su respectivo año
                Dim Stry1 As String
                Dim Rs1 As SqlDataReader

                Stry1 = "select ID from Informacion_Anual where ID='" & varId & "' and Año_P='" & Año & "' and Cve_admon='" & CveAdmon & "' "

                Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                Rs1 = cmd1.ExecuteReader()
                '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id con ese Año Solo Updatea los valores
                If Rs1.Read() = True Then

                    Dim Stry3 As String
                    Dim Rs3 As SqlDataReader

                    Stry3 = "update Informacion_Anual set CostoAnual='" & varCostoAnual & "',Enero='" & var1 & "',Febrero=' " & var2 & " ',Marzo='" & var3 & "',Abril= '" & var4 & "', Mayo='" & var5 & "',Junio='" & var6 & "',Julio=' " & var7 & " ',Agosto='" & var8 & "',Septiembre= '" & var9 & "', Octubre='" & var10 & "',Noviembre= '" & var11 & "', Diciembre='" & var12 & "',Acumulado= ' " & Resultado & "' where ID='" & varId & "' and Año_P='" & Año & "' and Cve_admon='" & CveAdmon & "' "
                    Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                    Rs3 = cmd3.ExecuteReader()

                    Rs3.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                    ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo
                Else
                    Dim Stry4 As String
                    Dim Rs4 As SqlDataReader

                    Stry4 = "insert into Informacion_Anual(ID,CostoAnual,Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre,Acumulado,Año_P,Cve_admon) values ('" & varId & "','" & varCostoAnual & "','" & var1 & "','" & var2 & "','" & var3 & "','" & var4 & "','" & var5 & "','" & var6 & "','" & var7 & "','" & var8 & "','" & var9 & "','" & var10 & "','" & var11 & "','" & var12 & "','" & Resultado & "','" & Año & "','" & CveAdmon & "')"
                    Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                    Rs4 = cmd4.ExecuteReader()

                    Rs4.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                End If
            Next
        End If
    End Sub


End Class
