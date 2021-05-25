Imports System.Data
Imports Class1
Imports System.Data.SqlClient
Partial Class Administrador
    Inherits System.Web.UI.Page
    Dim conx As New Class1
    Dim año As Integer

    Dim RdbPlan1 As String
    Dim RdbPlan2 As String
    Dim RdbPlan3 As String
    Dim RdbPlan4 As String
    Dim RdbPlan5 As String
    Dim RdbPlan6 As String
    Dim RdbPlan7 As String
    Dim RdbPlan8 As String
    Dim RdbPlan9 As String
    Dim RdbPlan10 As String
    Dim RdbPlan11 As String
    Dim RdbPlan12 As String
    Dim RdbPlanAnual As String
    Dim RdbPlanAjustes As String

    Dim RdbReal1 As String
    Dim RdbReal2 As String
    Dim RdbReal3 As String
    Dim RdbReal4 As String
    Dim RdbReal5 As String
    Dim RdbReal6 As String
    Dim RdbReal7 As String
    Dim RdbReal8 As String
    Dim RdbReal9 As String
    Dim RdbReal10 As String
    Dim RdbReal11 As String
    Dim RdbReal12 As String
    Dim LD As String 'Lista de Secretarias a las que tendra acceso
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("paso") = "1" Then
        Else
            Response.Redirect("Password.aspx")
        End If

        If IsPostBack = False Then
            LlenarAdmon()
            LlenaSecr()
            CargarGridPorMeses()
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
    Public Sub LlenaSecr()
        'LLENAR EL list de Secretarias
        Dim sent As New System.Data.SqlClient.SqlCommand("Select IdSecretaria,Nombr_secretaria from [PMD].Secretarias where Admon='" & Me.DropAdmon.Text & "' ", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                ListSecr.DataSource = exe
                ListSecr.DataTextField = "Nombr_secretaria"
                ListSecr.DataValueField = "IdSecretaria"
                ListSecr.DataBind()
                exe.NextResult()
            End If
            exe.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub CargarGridPorMeses()
        ''''''''''''''''''''''''''''''''''''''''''''''''' Llena Grid
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select Año from [PMD].Admin_meses where Admon='" & Me.DropAdmon.Text & "'", conx.conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader
        Try
            If Not Drsx2 Is Nothing Then
                GridView3.DataSource = Drsx2
                GridView3.Font.Size = 8
                GridView3.DataBind()
                Drsx2.NextResult()
            End If
        Finally
            Drsx2.Close()
        End Try
        VerificarMesesHabilitados()
    End Sub
    Private Sub VerificarMesesHabilitados()
        Dim x As Integer
        x = 0

        For Each row As GridViewRow In GridView3.Rows
            If CType(row.FindControl("lblAño"), Label) IsNot Nothing Then
                Dim Año = CType(row.FindControl("lblAño"), Label).Text()

                Dim RsGen2 As New System.Data.SqlClient.SqlCommand("Select * from [PMD].Admin_meses where Año='" & Año & "' ", conx.conectar)
                Dim Drsx2 As System.Data.SqlClient.SqlDataReader
                Drsx2 = RsGen2.ExecuteReader
                Drsx2.Read()

                'CHECO LOS valores De lo PLANEADO
                RdbPlan1 = Drsx2(1)
                RdbPlan2 = Drsx2(3)
                RdbPlan3 = Drsx2(5)
                RdbPlan4 = Drsx2(7)
                RdbPlan5 = Drsx2(9)
                RdbPlan6 = Drsx2(11)
                RdbPlan7 = Drsx2(13)
                RdbPlan8 = Drsx2(15)
                RdbPlan9 = Drsx2(17)
                RdbPlan10 = Drsx2(19)
                RdbPlan11 = Drsx2(21)
                RdbPlan12 = Drsx2(23)
                RdbPlanAjustes = Drsx2(37)
                RdbPlanAnual = Drsx2(38)

                'CHECO LOS Rvalores de lo  REAL
                RdbReal1 = Drsx2(2)
                RdbReal2 = Drsx2(4)
                RdbReal3 = Drsx2(6)
                RdbReal4 = Drsx2(8)
                RdbReal5 = Drsx2(10)
                RdbReal6 = Drsx2(12)
                RdbReal7 = Drsx2(14)
                RdbReal8 = Drsx2(16)
                RdbReal9 = Drsx2(18)
                RdbReal10 = Drsx2(20)
                RdbReal11 = Drsx2(22)
                RdbReal12 = Drsx2(24)

                Drsx2.Close()

                If RdbPlan1 = 1 Then
                    CType(row.FindControl("RdbPlanEneA"), RadioButton).Checked = "true" 'Este Radiobutton es Activo por lo tanto le doy checked 
                Else
                    CType(row.FindControl("RdbPlanEneI"), RadioButton).Checked = "true" 'Este Radiobutton es Inactivo por lo tanto le doy checked
                End If

                If RdbPlan2 = 1 Then
                    CType(row.FindControl("RdbPlanFebA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanFebI"), RadioButton).Checked = "true"
                End If

                If RdbPlan3 = 1 Then
                    CType(row.FindControl("RdbPlanMarA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanMarI"), RadioButton).Checked = "true"
                End If

                If RdbPlan4 = 1 Then
                    CType(row.FindControl("RdbPlanAbrA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanAbrI"), RadioButton).Checked = "true"
                End If

                If RdbPlan5 = 1 Then
                    CType(row.FindControl("RdbPlanMayA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanMayI"), RadioButton).Checked = "true"
                End If
                If RdbPlan6 = 1 Then
                    CType(row.FindControl("RdbPlanJunA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanJunI"), RadioButton).Checked = "true"
                End If
                If RdbPlan7 = 1 Then
                    CType(row.FindControl("RdbPlanJulA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanJulI"), RadioButton).Checked = "true"
                End If
                If RdbPlan8 = 1 Then
                    CType(row.FindControl("RdbPlanAgoA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanAgoI"), RadioButton).Checked = "true"
                End If
                If RdbPlan8 = 1 Then
                    CType(row.FindControl("RdbPlanSeptA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanSeptI"), RadioButton).Checked = "true"
                End If
                If RdbPlan10 = 1 Then
                    CType(row.FindControl("RdbPlanOctA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanOctI"), RadioButton).Checked = "true"
                End If
                If RdbPlan11 = 1 Then
                    CType(row.FindControl("RdbPlanNovA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanNovI"), RadioButton).Checked = "true"
                End If
                If RdbPlan12 = 1 Then
                    CType(row.FindControl("RdbPlanDicA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanDicI"), RadioButton).Checked = "true"
                End If
                If RdbPlanAjustes = 1 Then
                    CType(row.FindControl("RdbPlanAjustesA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanAjustesI"), RadioButton).Checked = "true"
                End If

                If RdbPlanAnual = 1 Then
                    CType(row.FindControl("RdbPlanAnualA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbPlanAnualI"), RadioButton).Checked = "true"
                End If
                '''''''''''''''Ahora Los Radiobutton que manejan lo Habilitado Real  de cada mes
                If RdbReal1 = 1 Then
                    CType(row.FindControl("RdbRealEneA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealEneI"), RadioButton).Checked = "true"
                End If
                If RdbReal2 = 1 Then
                    CType(row.FindControl("RdbRealFebA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealFebI"), RadioButton).Checked = "true"
                End If
                If RdbReal3 = 1 Then
                    CType(row.FindControl("RdbRealMarA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealMarI"), RadioButton).Checked = "true"
                End If
                If RdbReal4 = 1 Then
                    CType(row.FindControl("RdbRealAbrA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealAbrI"), RadioButton).Checked = "true"
                End If
                If RdbReal5 = 1 Then
                    CType(row.FindControl("RdbRealMayA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealMayI"), RadioButton).Checked = "true"
                End If
                If RdbReal6 = 1 Then
                    CType(row.FindControl("RdbRealJunA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealJunI"), RadioButton).Checked = "true"
                End If
                If RdbReal7 = 1 Then
                    CType(row.FindControl("RdbRealJulA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealJulI"), RadioButton).Checked = "true"
                End If
                If RdbReal8 = 1 Then
                    CType(row.FindControl("RdbRealAgoA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealAgoI"), RadioButton).Checked = "true"
                End If
                If RdbReal9 = 1 Then
                    CType(row.FindControl("RdbRealSeptA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealSeptI"), RadioButton).Checked = "true"
                End If
                If RdbReal10 = 1 Then
                    CType(row.FindControl("RdbRealOctA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealOctI"), RadioButton).Checked = "true"
                End If
                If RdbReal11 = 1 Then
                    CType(row.FindControl("RdbRealNovA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealNovI"), RadioButton).Checked = "true"
                End If
                If RdbReal12 = 1 Then
                    CType(row.FindControl("RdbRealDicA"), RadioButton).Checked = "true"
                Else
                    CType(row.FindControl("RdbRealDicI"), RadioButton).Checked = "true"
                End If

            End If
        Next
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCambiosMensual.Click
        Dim x As Integer
        x = 0

        For Each row As GridViewRow In GridView3.Rows
            año = DirectCast(GridView3.Rows(x).FindControl("lblAño"), Label).Text()
            'CHECO LOS RADIO BUTTON LIST DE LO PLANEADO
            If CType(row.FindControl("RdbPlanEneA"), RadioButton).Checked = "true" Then
                RdbPlan1 = 1
            Else
                RdbPlan1 = 0
            End If
            If CType(row.FindControl("RdbPlanFebA"), RadioButton).Checked = "true" Then
                RdbPlan2 = 1
            Else
                RdbPlan2 = 0
            End If

            If CType(row.FindControl("RdbPlanMarA"), RadioButton).Checked = "true" Then
                RdbPlan3 = 1
            Else
                RdbPlan3 = 0
            End If
            If CType(row.FindControl("RdbPlanAbrA"), RadioButton).Checked = "true" Then
                RdbPlan4 = 1
            Else
                RdbPlan4 = 0
            End If
            If CType(row.FindControl("RdbPlanMayA"), RadioButton).Checked = "true" Then
                RdbPlan5 = 1
            Else
                RdbPlan5 = 0
            End If
            If CType(row.FindControl("RdbPlanJunA"), RadioButton).Checked = "true" Then
                RdbPlan6 = 1
            Else
                RdbPlan6 = 0
            End If

            If CType(row.FindControl("RdbPlanJulA"), RadioButton).Checked = "true" Then
                RdbPlan7 = 1
            Else
                RdbPlan7 = 0
            End If
            If CType(row.FindControl("RdbPlanAgoA"), RadioButton).Checked = "true" Then
                RdbPlan8 = 1
            Else
                RdbPlan8 = 0
            End If
            If CType(row.FindControl("RdbPlanSeptA"), RadioButton).Checked = "true" Then
                RdbPlan9 = 1
            Else
                RdbPlan9 = 0
            End If
            If CType(row.FindControl("RdbPlanOctA"), RadioButton).Checked = "true" Then
                RdbPlan10 = 1
            Else
                RdbPlan10 = 0
            End If
            If CType(row.FindControl("RdbPlanNovA"), RadioButton).Checked = "true" Then
                RdbPlan11 = 1
            Else
                RdbPlan11 = 0
            End If
            If CType(row.FindControl("RdbPlanDicA"), RadioButton).Checked = "true" Then
                RdbPlan12 = 1
            Else
                RdbPlan12 = 0
            End If

            If CType(row.FindControl("RdbPlanAjustesA"), RadioButton).Checked = "true" Then
                RdbPlanAjustes = 1
            Else
                RdbPlanAjustes = 0
            End If
            If CType(row.FindControl("RdbPlanAnualA"), RadioButton).Checked = "true" Then
                RdbPlanAnual = 1
            Else
                RdbPlanAnual = 0
            End If
            'CHECO LOS RADIO BUTTON LIST DE LO Real
            If CType(row.FindControl("RdbRealEneA"), RadioButton).Checked = "true" Then
                RdbReal1 = 1
            Else
                RdbReal1 = 0
            End If
            If CType(row.FindControl("RdbRealFebA"), RadioButton).Checked = "true" Then
                RdbReal2 = 1
            Else
                RdbReal2 = 0
            End If

            If CType(row.FindControl("RdbRealMarA"), RadioButton).Checked = "true" Then
                RdbReal3 = 1
            Else
                RdbReal3 = 0
            End If
            If CType(row.FindControl("RdbRealAbrA"), RadioButton).Checked = "true" Then
                RdbReal4 = 1
            Else
                RdbReal4 = 0
            End If
            If CType(row.FindControl("RdbRealMayA"), RadioButton).Checked = "true" Then
                RdbReal5 = 1
            Else
                RdbReal5 = 0
            End If
            If CType(row.FindControl("RdbRealJunA"), RadioButton).Checked = "true" Then
                RdbReal6 = 1
            Else
                RdbReal6 = 0
            End If

            If CType(row.FindControl("RdbRealJulA"), RadioButton).Checked = "true" Then
                RdbReal7 = 1
            Else
                RdbReal7 = 0
            End If
            If CType(row.FindControl("RdbRealAgoA"), RadioButton).Checked = "true" Then
                RdbReal8 = 1
            Else
                RdbReal8 = 0
            End If
            If CType(row.FindControl("RdbRealSeptA"), RadioButton).Checked = "true" Then
                RdbReal9 = 1
            Else
                RdbReal9 = 0
            End If
            If CType(row.FindControl("RdbRealOctA"), RadioButton).Checked = "true" Then
                RdbReal10 = 1
            Else
                RdbReal10 = 0
            End If
            If CType(row.FindControl("RdbRealNovA"), RadioButton).Checked = "true" Then
                RdbReal11 = 1
            Else
                RdbReal11 = 0
            End If
            If CType(row.FindControl("RdbRealDicA"), RadioButton).Checked = "true" Then
                RdbReal12 = 1
            Else
                RdbReal12 = 0
            End If


            ''''''''''''''SI CUALQUIERA DE LAS DOS VARIABLES FUERA DIFERENTE DE 0, EL STATUS DEL MES(VAR4) CAMBIA A 1(hABILITADO) PARA LA PANTALLA DE PreCaptura
            Dim StatusMes1 As Integer
            Dim StatusMes2 As Integer
            Dim StatusMes3 As Integer
            Dim StatusMes4 As Integer
            Dim StatusMes5 As Integer
            Dim StatusMes6 As Integer
            Dim StatusMes7 As Integer
            Dim StatusMes8 As Integer
            Dim StatusMes9 As Integer
            Dim StatusMes10 As Integer
            Dim StatusMes11 As Integer
            Dim StatusMes12 As Integer

            If (RdbPlan1 = 0 And RdbReal1 = 0) Then
                StatusMes1 = 0
            Else
                StatusMes1 = 1
            End If
            If (RdbPlan2 = 0 And RdbReal2 = 0) Then
                StatusMes2 = 0
            Else
                StatusMes2 = 1
            End If
            If (RdbPlan3 = 0 And RdbReal3 = 0) Then
                StatusMes3 = 0
            Else
                StatusMes3 = 1
            End If
            If (RdbPlan4 = 0 And RdbReal4 = 0) Then
                StatusMes4 = 0
            Else
                StatusMes4 = 1
            End If
            If (RdbPlan5 = 0 And RdbReal5 = 0) Then
                StatusMes5 = 0
            Else
                StatusMes5 = 1
            End If
            If (RdbPlan6 = 0 And RdbReal6 = 0) Then
                StatusMes6 = 0
            Else
                StatusMes6 = 1
            End If
            If (RdbPlan7 = 0 And RdbReal7 = 0) Then
                StatusMes7 = 0
            Else
                StatusMes7 = 1
            End If
            If (RdbPlan8 = 0 And RdbReal8 = 0) Then
                StatusMes8 = 0
            Else
                StatusMes8 = 1
            End If
            If (RdbPlan9 = 0 And RdbReal9 = 0) Then
                StatusMes9 = 0
            Else
                StatusMes9 = 1
            End If
            If (RdbPlan10 = 0 And RdbReal10 = 0) Then
                StatusMes10 = 0
            Else
                StatusMes10 = 1
            End If
            If (RdbPlan11 = 0 And RdbReal11 = 0) Then
                StatusMes11 = 0
            Else
                StatusMes11 = 1
            End If
            If (RdbPlan12 = 0 And RdbReal12 = 0) Then
                StatusMes12 = 0
            Else
                StatusMes12 = 1
            End If
            '''''''''VERIFICO QUE SECRETARIAS SON LAS QUE TENDRAN ACTIVADAS LO PLANEADO Y LO REAL
            SecretariasSeleccionadas()

            '''''''''''''''''''''''''''''''''''''''''''''''''INSERTO EN ADMIN_meses los estatus de lo planeado y lo real por mes en cada año de la correspondiente Admon y Con las secretarias que tendran Habilitado o desabilitado los meses
            Dim Stry4 As String
            Dim Rs4 As SqlDataReader
            Stry4 = "Update Admin_meses set eneP='" & RdbPlan1 & "',febP='" & RdbPlan2 & "',marP='" & RdbPlan3 & "',abrP='" & RdbPlan4 & "',mayP='" & RdbPlan5 & "',junP='" & RdbPlan6 & "',julP='" & RdbPlan7 & "',agoP='" & RdbPlan8 & "',sepP='" & RdbPlan9 & "',octP='" & RdbPlan10 & "',novP='" & RdbPlan11 & "',dicP='" & RdbPlan12 & "',eneR='" & RdbReal1 & "',febR='" & RdbReal2 & "',marR='" & RdbReal3 & "',abrR='" & RdbReal4 & "',mayR='" & RdbReal5 & "',junR='" & RdbReal6 & "',julR='" & RdbReal7 & "',agoR='" & RdbReal8 & "',sepR='" & RdbReal9 & "',octR='" & RdbReal10 & "',novR='" & RdbReal11 & "',dicR='" & RdbReal12 & "',StatusMes1= '" & StatusMes1 & "',StatusMes2= '" & StatusMes2 & "',StatusMes3= '" & StatusMes3 & "',StatusMes4= '" & StatusMes4 & "',StatusMes5= '" & StatusMes5 & "',StatusMes6= '" & StatusMes6 & "',StatusMes7= '" & StatusMes7 & "',StatusMes8= '" & StatusMes8 & "',StatusMes9= '" & StatusMes9 & "',StatusMes10= '" & StatusMes10 & "',StatusMes11= '" & StatusMes11 & "',StatusMes12= '" & StatusMes12 & "',StatusAjustes='" & RdbPlanAjustes & "',StatusAnual='" & RdbPlanAnual & "',Claves_Secr_Habilitadas='" & LD & "'  where Año='" & año & "' and Admon='" & Me.DropAdmon.Text & "'"
            Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
            Rs4 = cmd4.ExecuteReader()
            Rs4.Read()
            x = x + 1
        Next
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
    End Sub
    Private Sub SecretariasSeleccionadas()
        LD = ""
        If Me.CheckTodas.Checked = True Then
            For Each item As ListItem In ListSecr.Items
                LD &= item.Value.ToString & ","
            Next
        Else
            For Each item As ListItem In ListSecr.Items
                If item.Selected Then
                    LD &= item.Value.ToString & ","
                End If
            Next
        End If
    End Sub
    Protected Sub DropAdmon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropAdmon.SelectedIndexChanged
        LlenaSecr()
    End Sub
    Protected Sub Unnamed1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckTodas.CheckedChanged
        If Me.CheckTodas.Checked = True Then
            Me.ListSecr.Visible = False
        Else
            Me.ListSecr.Visible = True
            LlenaSecr()
        End If
    End Sub
End Class
