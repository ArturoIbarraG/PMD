Imports System.Data
Imports Class1
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Web.UI.HtmlControls
Imports System.Globalization
Imports System.Text.RegularExpressions
Partial Class CapturaAjustes
    Inherits System.Web.UI.Page
    Dim Direccion As String
    Public Mes As String
    Public Secretaria As String
    Public NumMes As String 'Con el numero del mes calcularemos las semanas que contiene
    Dim Año As String
    Dim Cve_admon As String

    Dim conx As New Class1
    Public Comentarios As Integer 'Para Habilitar o Deshabilitar los comentarios del grid

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("paso") = "1" Then
        'Else
        '    Response.Redirect("Password.aspx")
        'End If

        'Titulo de la Pagina
        Me.LblAño.Text = Session("Año")
        Me.LabelMes.Text = Request.Params("Mes")
        Me.lblSecretaria.Text = Request.Params("Secr")
        Me.LblDireccion.Text = Request.Params("Dir")

        If IsPostBack = False Then
            DesgloseXdireccion() '''''''''FILTRA EL ID, ESTRATEGIA Y UNIDAD DE MEDIDA DEACUERDO AL AÑO DE TRAE EL VALOR DE LA VARIABLE SESION Y LA DIRECCION QUE SE TRAE EN EL REQUEST PARAMS
        End If
    End Sub
    Public Sub DesgloseXdireccion()
        'LA VARIABLE "Mes" NOS VA SERVIR PARA SABER EL TOTAL MENSUAL QUE ANTERIORMENTE EL USUARIO HABRA CAPTURADO EN SU ANUAL DEPENDIENDO EL MES
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID," & _
                                                           "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_Linea " & _
                                                           "else a.descr_sublinea end as Descr, a.Unidad_Medida,isnull(c.Ajuste,0)as Ajuste,isnull(c.Comentarios,'') as Com  " & _
                                                           "from Concentrado_PMD a  " & _
                                                          "left join Informacion_Ajustes c on a.ID=c.ID and a.cve_admon=c.admon " & _
                                                          "inner join Cat_tipo_linea f on f.Cve_tipo=a.Tipo_de_linea  " & _
                                                          "where a.Año='" & Session("Año") & "' and a.Cve_admon='" & Session("Admon") & "' and Direccion in ( '" & Me.LblDireccion.Text & "') and a.tipo_de_linea='1' ORDER BY 1", conx.conectar)
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
                GridView1.DataBind()
                Drsx2.NextResult()
            End If
            Drsx2.Close()

        Catch
        End Try
        'EN EL SEGUNDO GRID DESPLEGARA LAS LINEAS xSolicitud
        'LA VARIABLE "Mes" NOS VA SERVIR PARA SABER EL TOTAL MENSUAL QUE ANTERIORMENTE EL USUARIO HABRA CAPTURADO EN SU ANUAL DEPENDIENDO EL MES

        Dim Da1 As New System.Data.SqlClient.SqlDataAdapter
        Dim ds1 As New System.Data.DataSet
        Dim RsGen3 As New System.Data.SqlClient.SqlCommand("select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID, " & _
                                                        "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_Linea " & _
                                                         "else a.descr_sublinea end as Descr," & _
                                                        "a.Unidad_Medida,isnull(c.Ajuste,0)as Ajuste,isnull(c.Comentarios,'') as Com  " & _
                                                        "from Concentrado_PMD a  " & _
                                                         "left join Informacion_Ajustes c on a.ID=c.ID and a.cve_admon=c.admon " & _
                                                         "inner join Cat_tipo_linea f on f.Cve_tipo=a.Tipo_de_linea  " & _
                                                         "where a.Año='" & Session("Año") & "' and a.Cve_admon='" & Session("Admon") & "' and Direccion in ( '" & Me.LblDireccion.Text & "') and a.tipo_de_linea='2' ORDER BY 1", conx.conectar)
        'NOTA: con el inner join a la tabla de informacion_anual nos aseguramos que NO LES APARECERA NADA SI AUN NO HAN CAPTURADO SU ANUAL DE ESE ID

        Da1.SelectCommand = RsGen3
        Da1.Fill(ds1)

        Dim Drsx3 As System.Data.SqlClient.SqlDataReader
        Drsx3 = RsGen3.ExecuteReader

        Try
            If Not Drsx3 Is Nothing Then
                GridView2.DataSource = ds1
                GridView2.Font.Size = 10
                GridView2.DataBind()
                Drsx3.NextResult()
            End If
            Drsx3.Close()
        Catch
        End Try
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        InsertarAjustes()
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        InsertarAjustes()
    End Sub
    Public Sub InsertarAjustes()
        Dim Id As String
        Dim Ajuste As Double
        Dim Comentarios As String
        Dim Grid As System.Web.UI.WebControls.GridView

        If Me.TabContainer1.ActiveTabIndex = 0 Then
            Grid = GridView1 'Este es el grid de Las lineas programadas
        Else
            Grid = GridView2 'Este es el grid de Las lineas por solicitud
        End If
        For i = 0 To Grid.Rows.Count - 1
            Id = DirectCast(Grid.Rows(i).FindControl("lblId"), Label).Text()
            Ajuste = DirectCast(Grid.Rows(i).FindControl("txtAjuste"), TextBox).Text()
            Comentarios = DirectCast(Grid.Rows(i).FindControl("txtCom"), TextBox).Text()

            '''''''''''''''''''''''''''''''''''''Checa si ya esta capturado ese Id con el ajuste de su respectivo año
            Dim Stry1 As String
            Dim Rs1 As SqlDataReader

            Stry1 = "select ID from Informacion_Ajustes where ID='" & Id & "' and Admon=" & Session("Admon") & " and año=" & Session("Año") & ""

            Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
            Rs1 = cmd1.ExecuteReader()
            '''''''''''''''''''''''''''''''''''''''Si ya esta ese Id Solo Updatea los valores
            If Rs1.Read() = True Then

                If Comentarios = "" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "Comentarios();", True)
                    GridView1.Rows(i).FindControl("txtCom").Focus()
                    Return
                End If

                Dim Stry3 As String
                Dim Rs3 As SqlDataReader

                Stry3 = "update Informacion_Ajustes set Ajuste='" & Ajuste & "',Comentarios='" & Comentarios & "' where ID='" & Id & "' and  admon='" & Session("Admon") & "' and año='" & Session("Año") & "' "
                Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                Rs3 = cmd3.ExecuteReader()

                Rs3.Read()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                ''''''''''''''''''''''''''''''''''''''''Si No Inserta el Registro Completo

            Else
                If Ajuste <> 0 Then
                    If Comentarios = "" Then
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "Comentarios();", True)
                        GridView1.Rows(i).FindControl("txtCom").Focus()
                        Return
                    End If
                    Dim Stry4 As String
                    Dim Rs4 As SqlDataReader

                    Stry4 = "insert into Informacion_Ajustes(Id,Ajuste,Año,Admon,Comentarios) values ('" & Id & "','" & Ajuste & "','" & Session("Año") & "','" & Session("Admon") & "','" & Comentarios & "')"
                    Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                    Rs4 = cmd4.ExecuteReader()

                    Rs4.Read()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
                End If
            End If
        Next
    End Sub
End Class
