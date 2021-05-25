Imports Class1
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI.DataVisualization.Charting     'Para utilizar el control chart
Imports System.Drawing

Partial Class ReporteGrafico
    Inherits System.Web.UI.Page
    Dim conectar As New Class1

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        LineasProgramadas()
        LineasPorSolicitud()
    End Sub
    Public Sub LineasProgramadas()
        Dim stry As String
        Select Case Me.Radio1.SelectedValue
            Case "Programas"
                stry = "select b.desc_eje as Programa,(sum(d.acumulado)/sum(c.acumulado) )*100 as Porcentaje " &
                                                      "from [PMD].Concentrado_PMD a " &
                                                      "inner join [PMD].Ejes b on a.Eje=b.IdEje and a.Cve_admon=b.Admon "
            Case "Secretarías"

                stry = "select b.Nombr_secretaria as Secretarias,(sum(d.acumulado)/sum(c.acumulado) )*100 as Porcentaje " &
                                                                 "from [PMD].Concentrado_PMD a " &
                                                                 "left join [PMD].Secretarias b on a.Cve_Secr=b.Idsecretaria "

        End Select
        stry = stry & "left join [PMD].Informacion_Planeada c on a.ID=c.ID and a.Cve_admon='" & Me.DropAdmon.Text & "' " &
                      "left join [PMD].Informacion_Real d on a.ID=d.ID  and c.Mes=d.Mes and c.Año_P=d.Año_R and c.Cve_admon=d.Cve_admon " &
                       "where c.Año_P='" & Me.DropAño.Text & "' and c.Cve_admon='" & Me.DropAdmon.Text & "'"

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
                stry = stry & " and a.tipo_de_linea='1' group by b.desc_eje"
            Case "Secretarías"
                If Me.CheckSecr.Checked = False Then
                    stry = stry & " and a.Cve_Secr='" & Me.DropSecr.SelectedValue & "' "
                    If Me.CheckDir.Checked = False Then
                        stry = stry & " and a.Cve_Dir='" & Me.DropDir.SelectedValue & "' "
                    End If
                End If
                stry = stry & " and a.tipo_de_linea='1' group by b.Nombr_secretaria"
        End Select

        Dim sent As New System.Data.SqlClient.SqlCommand(stry, conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = sent.ExecuteReader

        Chart1.Series.Add("Porcentaje")
        'Chart1.Legends.Add("Porcentaje")
        Try
            While exe.Read
                Chart1.Series("Porcentaje").Points.AddXY(exe(0), exe(1))

                Chart1.Series("Porcentaje").IsValueShownAsLabel = True
                'Para darle Formato
                'Chart1.Series("Porcentaje").LabelFormat = "${0.00}"
                Chart1.Series("Porcentaje").LabelFormat = "{0}%"
                Chart1.Series("Porcentaje").LabelForeColor = Color.White


                Chart1.Series("Porcentaje").Color = Color.RoyalBlue
                'Chart1.Series("Porcentaje").XValueMember = "Porcentaje"

            End While
        Finally
            exe.Close()
        End Try
        ''Habilita el 3D
        Chart1.ChartAreas(0).Area3DStyle.Enable3D = True
        'Propiedades del 3D
        Chart1.ChartAreas(0).Area3DStyle.Rotation = 0
        Chart1.ChartAreas(0).Area3DStyle.Inclination = 10
        'Chart1.ChartAreas(0).Area3DStyle.IsRightAngleAxes = True
        'Chart1.ChartAreas(0).Area3DStyle.PointDepth = 10
        'Chart1.ChartAreas(0).Area3DStyle.PointGapDepth = 35

        Chart1.Series("Porcentaje").ChartType = DataVisualization.Charting.SeriesChartType.RangeBar
    End Sub
    Public Sub LineasPorSolicitud()
        Dim stry As String
        Select Case Me.Radio1.SelectedValue
            Case "Programas"
                stry = "select b.desc_eje as Programa,(((sum(d.acumulado)/sum(e.acumulado))*100)/(sum(c.acumulado)/Count(c.acumulado)))*100 as Porcentaje  " &
                                                      "from [PMD].Concentrado_PMD a " &
                                                      "inner join [PMD].Ejes b on a.Eje=b.IdEje and a.Cve_admon=b.Admon "
            Case "Secretarías"

                stry = "select b.Nombr_secretaria as Secretarias,(((sum(d.acumulado)/sum(e.acumulado))*100)/(sum(c.acumulado)/Count(c.acumulado)))*100 as Porcentaje  " &
                                                                 "from [PMD].Concentrado_PMD a " &
                                                                 "left join [PMD].Secretarias b on a.Cve_Secr=b.Idsecretaria "

        End Select
        stry = stry & "left join [PMD].Informacion_Planeada c on a.ID=c.ID and a.Cve_admon='" & Me.DropAdmon.Text & "' " &
                                                      "left join [PMD].Informacion_Real d on a.ID=d.ID  and c.Mes=d.Mes and c.Año_P=d.Año_R and c.Cve_admon=d.Cve_admon " &
                                                      "left join [PMD].Informacion_Solicitada e on a.ID=e.ID  and c.Mes=e.Mes and c.Año_P=e.Año and c.Cve_admon=e.Cve_admon " &
                                                      "where c.Año_P='" & Me.DropAño.Text & "' and c.Cve_admon='" & Me.DropAdmon.Text & "'"

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

                stry = stry & " and a.tipo_de_linea='2' group by b.desc_eje"
            Case "Secretarías"
                If Me.CheckSecr.Checked = False Then
                    stry = stry & " and a.Cve_Secr='" & Me.DropSecr.SelectedValue & "' "
                    If Me.CheckDir.Checked = False Then
                        stry = stry & " and a.Cve_Dir='" & Me.DropDir.SelectedValue & "' "
                    End If
                End If
                stry = stry & " and a.tipo_de_linea='2' group by b.Nombr_secretaria"
        End Select


        Dim sent As New System.Data.SqlClient.SqlCommand(stry, conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader
        exe = sent.ExecuteReader

        Chart2.Series.Add("Porcentaje")
        Try
            While exe.Read
                Chart2.Series("Porcentaje").Points.AddXY(exe(0), exe(1))
                Chart2.Series("Porcentaje").IsValueShownAsLabel = True
                'Para darle Formato
                Chart2.Series("Porcentaje").LabelFormat = "{0}%"
                Chart2.Series("Porcentaje").LabelForeColor = Color.White
                Chart2.Series("Porcentaje").Color = Color.RoyalBlue
            End While
        Finally
            exe.Close()
        End Try
        ''Habilita el 3D
        Chart2.ChartAreas(0).Area3DStyle.Enable3D = True
        'Propiedades del 3D
        Chart2.ChartAreas(0).Area3DStyle.Rotation = 0
        Chart2.ChartAreas(0).Area3DStyle.Inclination = 10

        Chart2.Series("Porcentaje").ChartType = DataVisualization.Charting.SeriesChartType.RangeBar
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                'Response.Redirect("~/Password.aspx")
            End If
            LLenarDrops()
        End If
        EventosRadioButton()
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
    Public Sub LlenarSecretarias()
        'LLENAR EL DROP Secretarias
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
    Public Sub LLenaDropAño()
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
        LLenaDropAño()
        LlenarEjes()
        LimpiarDrops(1)
    End Sub

End Class
