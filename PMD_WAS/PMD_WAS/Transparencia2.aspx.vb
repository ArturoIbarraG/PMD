Imports Class1
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System
Imports System.Web.UI
Partial Class Transparencia2
    Inherits System.Web.UI.Page
    Dim conectar As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                'Response.Redirect("~/Password.aspx")
            End If
            LLenarDrops()
        End If
        EventosRadioButton()

    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        ExportarLineasProgramadas()
    End Sub
    Public Sub ExportarLineasProgramadas()
        Try
            Dim ds As New DataSet
            Dim da As SqlClient.SqlDataAdapter
            Dim sb As StringBuilder = New StringBuilder()
            Dim sw As StringWriter = New StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
            Dim pagina As Page = New Page

            da = New SqlClient.SqlDataAdapter(Session("Excel"), conectar.conectar)
            da.Fill(ds)

            GridView1.EnableViewState = False
            GridView1.AllowPaging = False
            GridView1.DataSource = ds
            GridView1.DataBind()
            'grilla.Columns(0).Visible = False
            Dim form = New HtmlForm
            pagina.EnableEventValidation = False
            pagina.DesignerInitialize()
            pagina.Controls.Add(form)
            form.Controls.Add(GridView1)
            pagina.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()

        Catch ex As Exception
            'MsgBox(ex.ToString())
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        System.Threading.Thread.Sleep(3000)
        LineasProgramadas()
        LineasPorSolicitud()
    End Sub
    Public Sub LineasProgramadas()
        Dim stry As String
        stry = "select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID," &
              "a.Eje as Programa,a.Objetivo as Componente,a.Estrategia as Actividad,a.Linea as Tarea, " &
              "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_estrategia else a.Descr_estrategia end as Descr " &
              ",a.Direccion as Dirección,d.Comentarios,a.Secretaria as Secretaría,a.Clasificacion as Clasificación,a.Unidad_Medida,c.Acumulado as Planeado,d.Acumulado as Real " &
              "from [PMD].Concentrado_PMD a " &
              "inner join [PMD].Estrategias b on a.Estrategia=b.IdEstrategia  and a.Objetivo=b.IdObjetivo and a.Eje=b.IdEje  and a.Cve_admon=b.Admon " &
              "left join [PMD].Informacion_Planeada c on a.ID=c.ID and a.Año=c.Año_P " &
              "left join [PMD].Informacion_Real d on a.ID=d.ID  and c.Mes=d.Mes and c.Año_P=d.Año_R and c.Cve_admon=d.Cve_admon " &
              "where a.Año='" & Me.DropAño.Text & "' and c.Cve_admon='" & Me.DropAdmon.Text & "'"

        If Radio2.Text = "Mensual" Then
            stry = stry & " and c.Mes='" & Me.DropMes.Text & "' "
        Else
        End If

        Select Case Me.Radio1.SelectedValue
            Case "Programas"
                If CheckEje.Checked = False Then
                    stry = stry & " and a.Eje= '" & Me.DropEje.SelectedValue & "' "
                End If
                If CheckObj.Checked = False Then
                    stry = stry & " and a.Objetivo= '" & Me.DropObj.SelectedValue & "' "
                End If
                If CheckEstr.Checked = False Then
                    stry = stry & " and a.Estrategia= '" & Me.DropEstr.SelectedValue & "' "
                End If
                If CheckLinea.Checked = False Then
                    stry = stry & "and a.linea='" & Me.DropLinea.SelectedValue & "' "
                End If
                If CheckSubL.Checked = False Then
                    stry = stry & "and a.Sublinea='" & Me.DropSublinea.SelectedValue & "'"
                End If
            Case "Secretarías"
                If Me.CheckSecr.Checked = False Then
                    stry = stry & " and a.Cve_Secr='" & Me.DropSecr.SelectedValue & "' "
                    If Me.CheckDir.Checked = False Then
                        stry = stry & " and a.Cve_Dir='" & Me.DropDir.SelectedValue & "' "
                    End If
                End If
        End Select
        'el tipo de Linea es para las lineas programadas
        stry = stry & " and a.tipo_de_linea='1'"

        Session("Excel") = stry

        stry = stry & "order by 1,2,3,4"

        Dim sent As New System.Data.SqlClient.SqlCommand(stry, conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = sent.ExecuteReader

        Try
            If Not exe Is Nothing Then
                GridView1.DataSource = exe
                GridView1.Font.Size = 8
                GridView1.DataBind()
                exe.NextResult()
            End If
        Catch ex As Exception
            exe.Close()
        End Try

    End Sub
    Public Sub LineasPorSolicitud()
        Dim stry As String

        stry = "select distinct(dbo.fn_SplitString2(a.ID,'.'))as Digitos,a.ID, " &
            "a.Eje as Programa,a.Objetivo as Componente,a.Estrategia as Actividad,a.Linea as Tarea, " &
                "case when dbo.fn_SplitString2(a.ID,'.') = 4 then a.Descr_estrategia else a.Descr_estrategia end as Descr," &
                "a.Direccion as Dirección,d.Comentarios,a.Secretaria as Secretaría,a.Clasificacion as Clasificación,a.Unidad_Medida," &
                "c.Acumulado as PorcentajePlaneado," &
                "case when ((d.acumulado)=0 and (e.acumulado)=0) then 100 else " &
                "((ISNULL(NULLIF(d.Acumulado, ''), '0')/ISNULL(NULLIF(e.Acumulado, ''), '1'))*100) end as PorcentajeReal " &
                "from [PMD].Concentrado_PMD a " &
                "left join [PMD].Informacion_Planeada c on a.ID=c.ID and a.Año=c.Año_P  and a.Cve_admon=c.Cve_admon " &
                "left join [PMD].Informacion_Real d on c.ID=d.ID and c.Año_P =d.Año_R and c.Mes=d.Mes " &
                "left join [PMD].Informacion_Solicitada e on d.ID=e.ID  and d.Mes=e.Mes and d.Año_R=e.Año " &
                "where a.Año='" & Me.DropAño.Text & "'  and a.Cve_admon='" & Me.DropAdmon.Text & "' "


        If Radio2.Text = "Mensual" Then
            stry = stry & " and c.Mes='" & Me.DropMes.Text & "' "
        Else
        End If

        Select Case Me.Radio1.SelectedValue
            Case "Programas"
                If CheckEje.Checked = False Then
                    stry = stry & " and a.Eje= '" & Me.DropEje.SelectedValue & "' "
                End If
                If CheckObj.Checked = False Then
                    stry = stry & " and a.Objetivo= '" & Me.DropObj.SelectedValue & "' "
                End If
                If CheckEstr.Checked = False Then
                    stry = stry & " and a.Estrategia= '" & Me.DropEstr.SelectedValue & "' "
                End If
                If CheckLinea.Checked = False Then
                    stry = stry & "and a.linea='" & Me.DropLinea.SelectedValue & "' "
                End If
                If CheckSubL.Checked = False Then
                    stry = stry & "and a.Sublinea='" & Me.DropSublinea.SelectedValue & "'"
                End If
            Case "Secretarías"
                If Me.CheckSecr.Checked = False Then
                    stry = stry & " and a.Cve_Secr='" & Me.DropSecr.SelectedValue & "' "
                    If Me.CheckDir.Checked = False Then
                        stry = stry & " and a.Cve_Dir='" & Me.DropDir.SelectedValue & "' "
                    End If
                End If
        End Select
        'el tipo de Linea es para las lineas programadas
        stry = stry & " and a.tipo_de_linea='2' order by 1,2,3,4"

        Session("Excel") = Session("Excel") & " Union " & stry


        Dim sent As New System.Data.SqlClient.SqlCommand(stry, conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                GridView2.DataSource = exe
                GridView2.Font.Size = 8
                GridView2.DataBind()
                exe.NextResult()
            End If
        Catch ex As Exception
            exe.Close()
        End Try



    End Sub
    Public Sub LLenarDrops()
        LlenarAdmon()
        LlenaDropAño()
        LlenarEjes()
        LlenarObjetivos()
        LLenarEstrategias()
        LlenarLineas()
        LlenarSubLineas()
        LlenarSecretarias()
        LlenarDirecciones()
    End Sub
    Public Sub LlenarAdmon()
        'LLENAR EL DROP ADMINISTRACION
        Dim sent As New System.Data.SqlClient.SqlCommand("Select Cve_admon,Nombr_admon from [PMD].Cat_Admon", conectar.conectar)
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
    Private Sub LlenarSecretarias()
        Dim sent4 As New System.Data.SqlClient.SqlCommand("Select IdSecretaria,Nombr_secretaria from [PMD].Secretarias", conectar.conectar)
        Dim exe4 As System.Data.SqlClient.SqlDataReader

        exe4 = sent4.ExecuteReader
        Try
            If Not exe4 Is Nothing Then
                DropSecr.DataSource = exe4
                DropSecr.DataTextField = "Nombr_secretaria"
                DropSecr.DataValueField = "IdSecretaria"
                DropSecr.DataBind()
                exe4.NextResult()
            End If
            exe4.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LlenaDropAño()
        'LLENAR EL DROP AÑOS
        Dim sent2 As New System.Data.SqlClient.SqlCommand("Select Año from [PMD].Admin_meses where Admon='" & Me.DropAdmon.Text & "'", conectar.conectar)
        Dim exe2 As System.Data.SqlClient.SqlDataReader

        exe2 = sent2.ExecuteReader
        Try
            If Not exe2 Is Nothing Then
                DropAño.DataSource = exe2
                DropAño.DataTextField = "Año"
                DropAño.DataValueField = "Año"
                DropAño.DataBind()
                exe2.NextResult()
            End If
            exe2.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub EventosRadioButton()
        Select Case Me.Radio1.Text
            Case "Programas"
                Me.UpdateSecretaria.Visible = False
                Me.UpdatePanelEje.Visible = True
            Case "Secretarías"
                Me.UpdateSecretaria.Visible = True
                Me.UpdatePanelEje.Visible = False
        End Select

        Select Case Me.Radio2.Text
            Case "Anual"
                Me.DropMes.Visible = False
                Me.LblMes.Visible = False
            Case "Mensual"
                Me.DropMes.Visible = True
                Me.LblMes.Visible = True
            Case "Semana"
        End Select
    End Sub
    Protected Sub CheckEje_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckEje.CheckedChanged
        If CheckEje.Checked = True Then
            'Deshabilito los Drop
            DropEje.Enabled = False
            DropObj.Enabled = False
            DropEstr.Enabled = False
            DropLinea.Enabled = False
            DropSublinea.Enabled = False
            'Deshabilito Los Check
            CheckObj.Enabled = False
            CheckEstr.Enabled = False
            CheckLinea.Enabled = False
            CheckSubL.Enabled = False
            'Palomeo Los Check Deshabilitados
            CheckObj.Checked = True
            CheckEstr.Checked = True
            CheckLinea.Checked = True
            CheckSubL.Checked = True
        Else
            DropEje.Enabled = True
            DropObj.Enabled = True
            DropEstr.Enabled = True
            DropLinea.Enabled = True
            DropSublinea.Enabled = True
            CheckObj.Enabled = True
            CheckEstr.Enabled = True
            CheckLinea.Enabled = True
            CheckSubL.Enabled = True
            'Deshabilito Los Chek
            CheckObj.Checked = False
            CheckEstr.Checked = False
            CheckLinea.Checked = False
            CheckSubL.Checked = False
        End If
    End Sub
    Protected Sub CheckObj_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckObj.CheckedChanged
        If CheckObj.Checked = True Then
            DropObj.Enabled = False
            DropEstr.Enabled = False
            DropLinea.Enabled = False
            DropSublinea.Enabled = False

            CheckEstr.Enabled = False
            CheckLinea.Enabled = False
            CheckSubL.Enabled = False

            CheckEstr.Checked = True
            CheckLinea.Checked = True
            CheckSubL.Checked = True
        Else
            DropObj.Enabled = True
            DropEstr.Enabled = True
            DropLinea.Enabled = True
            DropSublinea.Enabled = True

            CheckEstr.Enabled = True
            CheckLinea.Enabled = True
            CheckSubL.Enabled = True

            CheckEstr.Checked = False
            CheckLinea.Checked = False
            CheckSubL.Checked = False
        End If
    End Sub
    Protected Sub CheckEstr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckEstr.CheckedChanged
        If CheckEstr.Checked = True Then
            DropEstr.Enabled = False
            DropLinea.Enabled = False
            DropSublinea.Enabled = False

            CheckLinea.Enabled = False
            CheckSubL.Enabled = False

            CheckLinea.Checked = True
            CheckSubL.Checked = True
        Else
            DropEstr.Enabled = True
            DropLinea.Enabled = True
            DropSublinea.Enabled = True

            CheckLinea.Enabled = True
            CheckSubL.Enabled = True

            CheckLinea.Checked = False
            CheckSubL.Checked = False
        End If
    End Sub
    Protected Sub CheckLinea_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckLinea.CheckedChanged
        If CheckLinea.Checked = True Then
            DropLinea.Enabled = False
            DropSublinea.Enabled = False
            CheckSubL.Enabled = False

            CheckSubL.Checked = True
        Else
            DropLinea.Enabled = True
            DropSublinea.Enabled = True

            CheckSubL.Enabled = True

            CheckSubL.Checked = False
        End If
    End Sub
    Protected Sub CheckSubL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckSubL.CheckedChanged
        If CheckSubL.Checked = True Then
            DropSublinea.Enabled = False
        Else
            DropSublinea.Enabled = True
        End If
    End Sub
    Protected Sub CheckSecr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckSecr.CheckedChanged
        If CheckSecr.Checked = True Then
            DropSecr.Enabled = False
            DropDir.Enabled = False
            CheckDir.Enabled = False

            CheckDir.Checked = True

        Else
            DropSecr.Enabled = True
            DropDir.Enabled = True
            CheckDir.Enabled = True
            CheckDir.Checked = False
        End If
    End Sub
    Protected Sub CheckDir_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckDir.CheckedChanged
        If CheckDir.Checked = True Then
            DropDir.Enabled = False
        Else
            DropDir.Enabled = True
        End If
    End Sub
    Protected Sub DropSecr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropSecr.SelectedIndexChanged
        LlenarDirecciones()

    End Sub
    Private Sub LlenarDirecciones()
        'LLENAR EL DROP Direcciones Deacuerdo a la Secretaria Elegida
        Dim sent As New System.Data.SqlClient.SqlCommand("Select IdDireccion,Nombr_direccion from [PMD].Direcciones where IdSecretaria='" & Me.DropSecr.SelectedValue & "'", conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropDir.DataSource = exe
                DropDir.DataTextField = "Nombr_direccion"
                DropDir.DataValueField = "IdDireccion"
                DropDir.DataBind()
                exe.NextResult()
            End If
            exe.Close()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DropEje_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropEje.SelectedIndexChanged
        LlenarObjetivos()
        LLenarEstrategias()
        LlenarLineas()
        LlenarSubLineas()

    End Sub
    Private Sub LlenarObjetivos()
        'LLENAR EL DROP Objetivos Deacuerdo al eje Elegido
        Dim sent As New System.Data.SqlClient.SqlCommand("Select IdObjetivo,Nombr_obj from [PMD].Objetivos where IdEje='" & Me.DropEje.SelectedValue & "' and Admon='" & Me.DropAdmon.SelectedValue & "'", conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropObj.DataSource = exe
                DropObj.DataTextField = "Nombr_obj"
                DropObj.DataValueField = "IdObjetivo"
                DropObj.DataBind()
                exe.NextResult()
                If DropObj.Items.Count = 1 Then
                    LLenarEstrategias()
                End If
            End If
            exe.Close()
        Catch ex As Exception

        End Try
        LimpiarDrops(2)

    End Sub
    Private Sub LLenarEstrategias()
        'LLENAR EL DROP Estrategias Deacuerdo al objetivo Elegido
        Dim sent As New System.Data.SqlClient.SqlCommand("Select IdEstrategia,Nombr_estr from [PMD].Estrategias where IdObjetivo='" & Me.DropObj.SelectedValue & "' and IdEje='" & Me.DropEje.SelectedValue & "' and Admon='" & Me.DropAdmon.SelectedValue & "'", conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropEstr.DataSource = exe
                DropEstr.DataTextField = "Nombr_estr"
                DropEstr.DataValueField = "IdEstrategia"
                DropEstr.DataBind()
                exe.NextResult()
                If DropEstr.Items.Count = 1 Then
                    LlenarLineas()
                End If
            End If
            exe.Close()
        Catch ex As Exception

        End Try
        LimpiarDrops(3)

    End Sub
    Private Sub LlenarLineas()
        'LLENAR EL DROP Lineas Deacuerdo al Estrategia Elegida
        Dim sent As New System.Data.SqlClient.SqlCommand("Select IdLinea,Nombr_linea from [PMD].Lineas where IdEstrategia='" & Me.DropEstr.SelectedValue & "' and  IdObjetivo='" & Me.DropObj.SelectedValue & "' and IdEje='" & Me.DropEje.SelectedValue & "' and Admon='" & Me.DropAdmon.SelectedValue & "'", conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropLinea.DataSource = exe
                DropLinea.DataTextField = "Nombr_linea"
                DropLinea.DataValueField = "IdLinea"
                DropLinea.DataBind()
                exe.NextResult()
                If DropLinea.Items.Count = 1 Then
                    LlenarSubLineas()
                End If
            End If
            exe.Close()
        Catch ex As Exception

        End Try
        LimpiarDrops(4)

    End Sub
    Private Sub LlenarSubLineas()
        'LLENAR EL DROP subLineas Deacuerdo a la Linea Elegida
        Dim sent As New System.Data.SqlClient.SqlCommand("Select IdSublinea,Nombr_Sub from [PMD].Sublineas where IdLinea='" & Me.DropLinea.SelectedValue & "' and IdEstrategia='" & Me.DropEstr.SelectedValue & "' and  IdObjetivo='" & Me.DropObj.SelectedValue & "' and IdEje='" & Me.DropEje.SelectedValue & "' and Admon='" & Me.DropAdmon.SelectedValue & "'", conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropSublinea.DataSource = exe
                DropSublinea.DataTextField = "Nombr_Sub"
                DropSublinea.DataValueField = "IdSublinea"
                DropSublinea.DataBind()
                'If DropSublinea.Items.Count = 0 Then
                '    DropSublinea.DataTextField = "Sin Datos"
                'End If
                exe.NextResult()
            End If
            exe.Close()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DropObj_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropObj.SelectedIndexChanged
        LLenarEstrategias()
        LlenarLineas()
        LlenarSubLineas()
    End Sub
    Protected Sub DropEstr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropEstr.SelectedIndexChanged
        LlenarLineas()
        LlenarSubLineas()
    End Sub
    Protected Sub DropLinea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropLinea.SelectedIndexChanged
        LlenarSubLineas()
    End Sub
    Private Sub LimpiarDrops(ByVal Index As Integer)
        Select Case Index
            Case "1" 'Cuando se Hace El Postbak en El drop Admon
                DropObj.Items.Clear()
                DropEstr.Items.Clear()
                DropLinea.Items.Clear()
                DropSublinea.Items.Clear()
            Case "2" ''Cuando Seleccionas algun Eje Limpia lo que alla podido estar en los grid siguientes exceptuando los objetivos que desplieguen del eje seleccionado
                DropEstr.Items.Clear()
                DropLinea.Items.Clear()
                DropSublinea.Items.Clear()
            Case "3" ''Cuando Seleccionas algun Objetivo Limpia lo que alla podido estar en los grid siguientes exceptuando las estrategias que desplieguen del Objetivo seleccionado
                ' DropLinea.Items.Clear()
                DropSublinea.Items.Clear()
            Case "4" 'Cuando Seleccionas algun Estrategia Limpia lo que alla podido estar en los grid siguientes exceptuando las Lineas que desplieguen del la Estrategia seleccionado
                DropSublinea.Items.Clear()
        End Select
    End Sub
    Protected Sub DropAdmon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropAdmon.SelectedIndexChanged
        LlenaDropAño()

        LlenarEjes()
        LimpiarDrops(1)

    End Sub
    Public Sub LlenarEjes()
        'LLENAR DROP EJES
        Dim sent3 As New System.Data.SqlClient.SqlCommand("Select IdEje,Desc_eje from [PMD].Ejes where Admon='" & Me.DropAdmon.SelectedValue & "'", conectar.conectar)
        Dim exe3 As System.Data.SqlClient.SqlDataReader
        exe3 = sent3.ExecuteReader
        Try
            If Not exe3 Is Nothing Then
                DropEje.DataSource = exe3
                DropEje.DataTextField = "Desc_eje"
                DropEje.DataValueField = "IdEje"
                DropEje.DataBind()
                exe3.NextResult()

            End If
            exe3.Close()

        Catch ex As Exception

        End Try
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
