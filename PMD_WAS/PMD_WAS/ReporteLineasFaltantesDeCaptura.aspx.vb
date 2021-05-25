Imports Class1
Imports System.Data.SqlClient
Imports System.Data

Partial Class ReporteLineasFaltantesDeCaptura
    Inherits System.Web.UI.Page
    Dim stry As String
    Dim conx As New Class1
    Dim x As Integer
    Dim DigitosEnlaCadena As Integer
    Dim ClaveSecr As String
    Dim ClaveDir As String

    Dim NominasConAccesos As String
    Dim Clave_empl As String
    Dim Claves_Secr_Habilitadas As String
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                Response.Redirect("~/Password.aspx")
            End If
            LlenarAdmon()
            txtAño.Text = 2015
        End If
    End Sub
    Public Sub LlenarAdmon()
        'LLENAR EL DROP ADMINISTRACION
        Dim sent As New System.Data.SqlClient.SqlCommand("Select Cve_admon,Nombr_admon from [PMD].Cat_Admon", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropAdmon.DataSource = exe
                DropAdmon.DataTextField = "Nombr_Admon"
                DropAdmon.DataValueField = "Cve_Admon"
                DropAdmon.DataBind()
                exe.NextResult()
            End If
            exe.Close()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Select Case RadioButtonList1.Text
            Case "1"
                Faltantes1()
            Case "2"
                Faltantes2()
            Case "3"
                Faltantes3()
        End Select
        count()
    End Sub
    Public Sub count()
        Dim stry As String
        Select Case RadioButtonList1.Text
            Case "1"
                stry = "select count(distinct(Id)) from [PMD].concentrado_Pmd  where Año='" & txtAño.Text & "' and Cve_admon='" & DropAdmon.Text & "' and id not in(select id from [PMD].informacion_anual where Año_P='" & txtAño.Text & "' and Cve_admon='" & DropAdmon.Text & "' )"
            Case "2"
                stry = "select count(distinct(Id)) from [PMD].Concentrado_Pmd where Cve_admon='" & DropAdmon.Text & "' and Año='" & txtAño.Text & "' and id in (select distinct(Id) from [PMD].informacion_anual  where " & dropMes.Text & "=0 and Año_P='" & txtAño.Text & "' and id not in(select id from [PMD].informacion_planeada where Año_P='" & txtAño.Text & "' and mes='" & dropMes.Text & "'))"

            Case "3"
                stry = "select count(distinct(Id)) from [PMD].Concentrado_Pmd where Cve_admon='" & DropAdmon.Text & "' and Año='" & txtAño.Text & "' and id in (select distinct(Id) from [PMD].informacion_anual  where  Año_P='" & txtAño.Text & "' and id not in(select id from [PMD].informacion_planeada where Año_P='" & txtAño.Text & "' and mes='" & dropMes.Text & "'))"
        End Select

        Dim sent As New Data.SqlClient.SqlCommand(stry, conx.conectar)
        Dim exe As Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader

        exe.Read()

        Try
            If Not exe Is Nothing Then
                lblCount.Text = " Total de líneas: " & exe(0)
                Me.lblCount.Visible = True
            End If
        Catch ex As Exception
            exe.Close()
        End Try

    End Sub
    Public Sub Faltantes1()
        'Líneas que no están programadas en el anual
        Dim sent As New Data.SqlClient.SqlCommand("select distinct(Id),Cve_Dir, Direccion,Cve_Secr, Secretaria  from [PMD].concentrado_Pmd  where Año='" & txtAño.Text & "' and Cve_admon='" & DropAdmon.Text & "' and id not in(select id from [PMD].informacion_anual where Año_P='" & txtAño.Text & "' and Cve_admon='" & DropAdmon.Text & "' )", conx.conectar)
        Dim exe As Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                GridView1.DataSource = exe
                GridView1.Font.Size = 12
                GridView1.DataBind()
                exe.NextResult()

            End If
        Catch ex As Exception
            exe.Close()
        End Try
    End Sub
    Public Sub Faltantes2()
        'IDS que no estan planeados mensualmente,aunque su planeacion anual fue 0
        Dim sent As New Data.SqlClient.SqlCommand("select distinct(Id),Cve_Dir, Direccion,Cve_Secr, Secretaria  from [PMD].Concentrado_Pmd " &
                                                   "where Cve_admon='" & DropAdmon.Text & "' and Año='" & txtAño.Text & "' and id in (select distinct(Id) from [PMD].informacion_anual  where " & dropMes.Text & "=0 and Año_P='" & txtAño.Text & "' and id not in(select id from [PMD].informacion_planeada where Año_P='" & txtAño.Text & "' and mes='" & dropMes.Text & "'))", conx.conectar)
        Dim exe As Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                GridView1.DataSource = exe
                GridView1.Font.Size = 12
                GridView1.DataBind()
                exe.NextResult()

            End If
        Catch ex As Exception
            exe.Close()
        End Try
    End Sub
    Public Sub Faltantes3()
        'IDS que no estan planeados mensualmente,independientemente si su planeacion anual fue 0 o no
        Dim sent As New Data.SqlClient.SqlCommand("select distinct(Id),Cve_Dir, Direccion,Cve_Secr, Secretaria  from [PMD].Concentrado_Pmd " &
                                                   "where Cve_admon='" & DropAdmon.Text & "' and Año='" & txtAño.Text & "' and id in (select distinct(Id) from [PMD].informacion_anual  where  Año_P='" & txtAño.Text & "' and id not in(select id from [PMD].informacion_planeada where Año_P='" & txtAño.Text & "' and mes='" & dropMes.Text & "'))", conx.conectar)
        Dim exe As Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                GridView1.DataSource = exe
                GridView1.Font.Size = 12
                GridView1.DataBind()
                exe.NextResult()

            End If
        Catch ex As Exception
            exe.Close()
        End Try
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow
        ClaveSecr = Trim(CType(row.FindControl("lblCveSecr"), Label).Text)
        ClaveDir = Trim(CType(row.FindControl("lblCveDir"), Label).Text)
        PermisosSecr()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Titulo", "ModalPopUpShow();", True)
    End Sub
    Public Sub PermisosSecr()

        '''''''''''''''''''''''''''''''''''''''''''''''''''Checa los nominas que tienen permisos por secretarias
        Dim stry2 As String
        Dim rs2 As SqlClient.SqlDataReader
        stry2 = "select Clave_empl,Secr,Admon  from [PMD].Permiso_Secr where Admon='" & Me.DropAdmon.Text & "' "
        Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
        rs2 = cmd2.ExecuteReader()
        While rs2.Read()
            Clave_empl = rs2(0)
            Claves_Secr_Habilitadas = rs2(1)
            RevisaPer(Claves_Secr_Habilitadas)
            x = 0
        End While
        rs2.Close()

        'Quita la ultima comilla que se agrega para que no marque error en la consulta
        Dim LS = NominasConAccesos 'Lista de NominasAcceso
        Dim Cont = LS.ToString.Length
        Dim Real = Cont - 1
        NominasConAccesos = LS.ToString().Substring(0, Real)
        '----------------------------------------------------

        'Consulto La ultima entrada en el sistema de esas nominas guardadas en la bitacora de sesion
        Dim sent2 As New System.Data.SqlClient.SqlCommand("SELECT Distinct(a.clave_empl),b.Nombr_empl,max(convert (varchar(30),a.fecha,120) )Fecha  " &
                                                            "from [PMD].Bitacora_Sesion a,[PMD].Usuarios b  where a.Clave_empl=b.Clave_empl and a.Clave_empl in (" & NominasConAccesos & ") " &
                                                            " group by a.Clave_empl,b.Nombr_empl  order by 3", conx.conectar)

        'Si quisieramos agregarle un rango de fechas
        '"and  convert (varchar(30),fecha,120) between '2015-01-07 1:00:00' and '2015-01-08 23:60:60' " & _
        Dim exe2 As Data.SqlClient.SqlDataReader

        exe2 = sent2.ExecuteReader
        Try
            If Not exe2 Is Nothing Then
                GridView2.DataSource = exe2
                GridView2.Font.Size = 12
                GridView2.DataBind()
                exe2.NextResult()

            End If
        Catch ex As Exception
            exe2.Close()
        End Try

    End Sub
    Private Function RevisaPer(ByVal num As String) As String
        Dim result As String
        Dim numero As String
        If num = 0 Then
        Else
            numero = CType(num, String)
            Dim numDividido() As String
            numDividido = numero.ToString().Split(",")
            DigitosEnlaCadena = numDividido.Length

            result = numDividido(x)

            While DigitosEnlaCadena <> x

                If DigitosEnlaCadena = x Then
                    Return result
                Else
                    result = numDividido(x)


                    If ClaveSecr = result Then
                        'Todas las nominas que tienen acceso a esa secretaria
                        NominasConAccesos &= Clave_empl & ","
                    End If

                End If
                x = x + 1
            End While

     
        End If
      
    End Function

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        Select Case RadioButtonList1.Text
            Case "1"
                lblMes.Visible = False
                dropMes.Visible = False

            Case "2"
                lblMes.Visible = True
                dropMes.Visible = True
            Case "3"
                lblMes.Visible = True
                dropMes.Visible = True
        End Select
    End Sub
End Class
