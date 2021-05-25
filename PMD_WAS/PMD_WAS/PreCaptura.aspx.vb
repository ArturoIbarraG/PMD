Imports System.Data
Imports Class1
Imports System.Data.SqlClient
Imports System.Drawing
Partial Class PreCaptura
    Inherits System.Web.UI.Page
    Dim HabilitaPlaneado As String
    Dim HabilitaReal As String

    Public HabilitaEne As Integer
    Public HabilitaFeb As Integer
    Public HabilitaMar As Integer
    Public HabilitaAbr As Integer
    Public HabilitaMay As Integer
    Public HabilitaJun As Integer
    Public HabilitaJul As Integer
    Public HabilitaAgo As Integer
    Public HabilitaSep As Integer
    Public HabilitaOct As Integer
    Public HabilitaNov As Integer
    Public HabilitaDic As Integer
    Public HabilitaAnual As Integer
    Public HabilitaAjustes As Integer

    Public Claves_Secr_Habilitadas As String

    Public Muestra As Integer

    Dim DigitosEnlaCadena As Integer
    Dim x As Integer

    Public Año As Integer
    Dim conx As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("paso") = 0 Then
                Response.Redirect("~/Password.aspx")
            End If
            LLenaDropAdmon() 'Llena drop de Administraciones
            LlenaDropAño() 'Llena drop de Años
        End If
    End Sub
    Public Sub LLenaDropAdmon()
        Dim SenDrop As New System.Data.SqlClient.SqlCommand("select Cve_admon,Nombr_admon from [PMD].Cat_Admon", conx.conectar)
        Dim Exe As System.Data.SqlClient.SqlDataReader
        Exe = SenDrop.ExecuteReader
        Try
            If Not Exe Is Nothing Then
                DropAdmon.DataSource = Exe
                DropAdmon.DataTextField = "Nombr_admon"
                DropAdmon.DataValueField = "Cve_admon"
                DropAdmon.Font.Size = 10
                DropAdmon.DataBind()
                Exe.NextResult()
            End If
            Exe.Close()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LlenaDropAño()    'LLENAR EL DROP AÑOS
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
    Protected Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFiltrar.Click
        PermisoSecr() 'Verifica las Secretarias a las que tiene permiso 
        StaTusMesesYAnual() '  Verifica el Status de que y que no esta habilitado en lo planeado y lo real
    End Sub
    Public Sub PermisoSecr()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''CHECA SI ESTE USUARIO ESTA EN  Permisos_Secr
        Dim stry As String
        Dim Rs As SqlDataReader
        Dim Com As New SqlCommand

        stry = ("select Secr from [PMD].Permiso_Secr where clave_empl='" & Session("Clave_empl") & "' and Admon='" & Me.DropAdmon.Text & "' ")
        Dim cmd As New Data.SqlClient.SqlCommand(stry, conx.conectar())
        Rs = cmd.ExecuteReader()

        If Rs.Read Then
            Dim LS = Rs(0) 'Lista de Secretrias
            Dim Cont = LS.ToString.Length
            Dim Real = Cont - 1
            Dim result = LS.ToString().Substring(0, Real)

            'Using data As New DB(con.conectar())
            '    Dim params() As SqlParameter = New SqlParameter() _
            '    {
            '        New SqlParameter("@idAnio", DropAnio.SelectedValue),
            '        New SqlParameter("@idAdmon", DropAdmon.SelectedValue),
            '        New SqlParameter("@idSecretarias", result)
            '    }

            '    GridView1.DataSource = data.ObtieneDatos("ObtieneSubActividadesLineasAjuste", params)
            '    GridView1.DataBind()

            Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select b.IdSecretaria,b.Nombr_Secretaria,c.IdDireccion,c.Nombr_Direccion from [PMD].Concentrado_PMD a " &
                                                           "inner join [PMD].Secretarias b on a.Cve_Secr=b.IdSecretaria and a.Cve_Admon=b.Admon " &
                                                           "inner join [PMD].Direcciones c on a.Cve_Dir=c.IdDireccion and a.Cve_Admon=c.Admon " &
                                                           "where a.Año='" & Me.DropAnio.Text & "' and a.Cve_admon='" & DropAdmon.Text & "' and a.Cve_Secr in (" & result & ") " &
                                                           "Group by b.IdSecretaria,b.Nombr_Secretaria,c.IdDireccion,c.Nombr_Direccion order by 1  ", conx.conectar)
            Dim Drsx2 As System.Data.SqlClient.SqlDataReader




            Drsx2 = RsGen2.ExecuteReader

            Try
                If Not Drsx2 Is Nothing Then
                    GridView1.DataSource = Drsx2
                    GridView1.Font.Size = 8
                    GridView1.DataBind()
                    Drsx2.NextResult()
                End If
            Finally
                Drsx2.Close()
            End Try
            Session("Año") = Me.DropAnio.Text 'Esta Variable Session Servira para guardar los montos con su respectivo año del que estan siendo consultados en las pantallas Captura y Captura_anual
                Session("Admon") = Me.DropAdmon.Text  'Igual que la variable Session solo que en esta guardaremos la cve de la administracion a la que pertenece ese año 
                Else
                PermisoDir() 'Si No encontro nada en Secretarias Ahora Checa si HAy permisos en Direcciones 
                Session("Año") = Me.DropAnio.Text
                Session("Admon") = Me.DropAdmon.Text


        End If



    End Sub
    Public Sub PermisoDir() '''''''''CHECA SI ESTE USUARIO Tiene Permisos de Direcciones
        Dim stry As String
        Dim Rs As SqlDataReader
        Dim Com As New SqlCommand

        stry = ("Select Direcciones from [PMD].Permisos_Dir  where clave_empl='" & Session("Clave_empl") & "' and Admon='" & Me.DropAdmon.Text & "'  ")
        Dim cmd As New Data.SqlClient.SqlCommand(stry, conx.conectar())
        Rs = cmd.ExecuteReader()

        If Rs.Read Then
            Dim LD = Rs(0) 'Lista de Direcciones
            Dim Cont = LD.ToString.Length
            Dim Real = Cont - 1
            Dim result = LD.ToString().Substring(0, Real)

            Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select b.IdSecretaria,b.Nombr_Secretaria,c.IdDireccion,c.Nombr_Direccion from [PMD].Concentrado_PMD a " &
                                                               "inner join [PMD].Secretarias b on a.Cve_Secr=b.IdSecretaria and a.Cve_Admon=b.Admon " &
                                                               "inner join [PMD].Direcciones c on a.Cve_Dir=c.IdDireccion and a.Cve_Admon=c.Admon " &
                                                               "where a.Año='" & Me.DropAnio.Text & "' and a.Cve_admon='" & DropAdmon.Text & "' and a.Cve_Dir in (" & result & ") " &
                                                               "Group by b.IdSecretaria,b.Nombr_Secretaria,c.IdDireccion,c.Nombr_Direccion order by 1  ", conx.conectar)

            Dim Drsx2 As System.Data.SqlClient.SqlDataReader
            Drsx2 = RsGen2.ExecuteReader

            Try
                If Not Drsx2 Is Nothing Then
                    GridView1.DataSource = Drsx2
                    GridView1.Font.Size = 8
                    GridView1.DataBind()
                    Drsx2.NextResult()
                End If
            Finally
                Drsx2.Close()
            End Try
        End If
    End Sub
    Public Sub StaTusMesesYAnual()
        x = 0
        '''''''''''''''''''''''''''''''''''''''''''''''''''Verifica el status deL MES
        Dim stry2 As String
        Dim rs2 As SqlClient.SqlDataReader
        stry2 = "select StatusMes1,StatusMes2,StatusMes3,StatusMes4,StatusMes5,StatusMes6,StatusMes7,StatusMes8,StatusMes9,StatusMes10,StatusMes11,StatusMes12,StatusAjustes,StatusAnual,Claves_Secr_Habilitadas from [PMD].Admin_meses where Año= '" & Me.DropAnio.Text & "' and Admon='" & Me.DropAdmon.Text & "' "
        Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conx.conectar())
        rs2 = cmd2.ExecuteReader()
        While rs2.Read()
            HabilitaEne = rs2(0)
            HabilitaFeb = rs2(1)
            HabilitaMar = rs2(2)
            HabilitaAbr = rs2(3)
            HabilitaMay = rs2(4)
            HabilitaJun = rs2(5)
            HabilitaJul = rs2(6)
            HabilitaAgo = rs2(7)
            HabilitaSep = rs2(8)
            HabilitaOct = rs2(9)
            HabilitaNov = rs2(10)
            HabilitaDic = rs2(11)
            HabilitaAjustes = rs2(12)
            HabilitaAnual = rs2(13)
            Claves_Secr_Habilitadas = rs2(14)
            MuestraPer(Claves_Secr_Habilitadas)

        End While
        rs2.Close()
    End Sub
    Private Function MuestraPer(ByVal num As String) As String
        Dim result As String
        Dim numero As String

        Dim bandera = 0
        Try
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
                If result <> "" Then
                    For Each row As GridViewRow In GridView1.Rows
                        If CType(row.FindControl("lblCveSecr"), Label).Text = result Then
                            row.Visible = True
                        Else
                        End If
                    Next
                End If
            End If
            x = x + 1
        End While
            End If
        Catch ex As Exception
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "error();", True)
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('No hay ninguna secretaría asignada para este filtro');", True)

        End Try
    End Function
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'oculto todas los registros  para despues en MuestraPer hacer visible solo las secretarias que esten habilitadas
        For Each row As GridViewRow In GridView1.Rows
            row.Visible = False
        Next
    End Sub

 

End Class

