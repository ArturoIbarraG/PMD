Imports Class1
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System
Imports System.Web.UI
Partial Class ReporteCosteo
    Inherits System.Web.UI.Page
    Dim conectar As New Class1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Session("Paso") = "0" Then
                'Response.Redirect("~/Password.aspx")
            End If
            LLenarDrops()
        End If


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
        Consulta()

    End Sub
    Public Sub Consulta()
        Dim stry As String
        stry = "select a.FolioFicha " &
      ",a.FechaCap " &
      ",b.Programa " &
      ",a.Secr " &
      ",a.Dep " &
      ",a.Colonia " &
      ",a.Periodo " &
      ",a.TipoAccion " &
      ",a.Horas " &
      ",a.Vehiculos " &
      ",a.Personas " &
      ",a.CostoNomina " &
      ",a.CostoSum " &
      ",a.Tipo " &
      ",a.Comentarios from Costeo a " &
      "inner join CosteoTipoPrograma b on a.Programa=b.Cve_progr "

        If CheckColonia.Checked = False Then
            stry = stry & " and a.Colonia= '" & Me.DropColonia.SelectedValue & "' "
        End If
        If CheckTipoAccion.Checked = False Then
            stry = stry & " and a.TipoAccion= '" & Me.DropTipoAccion.SelectedValue & "' "
        End If

        If Me.CheckSecr.Checked = False Then
            stry = stry & " and a.Secr='" & Me.DropSecr.SelectedValue & "' "
            If Me.CheckDir.Checked = False Then
                stry = stry & " and a.Dep='" & Me.DropDir.SelectedValue & "' "
            End If
        End If

        Session("Excel") = stry

        stry = stry & "order by 1"

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

    Public Sub LLenarDrops()
        'LlenarAdmon()
        LlenarSecretarias()
        LlenarDirecciones()
        LlenarTipoAccion()
        LlenarColonia()
    End Sub

    Private Sub LlenarSecretarias()
        Dim sent4 As New System.Data.SqlClient.SqlCommand("Select IdSecretaria,Nombr_secretaria from Secretarias", conectar.conectar)
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
        Dim sent As New System.Data.SqlClient.SqlCommand("Select IdDireccion,Nombr_direccion from Direcciones where IdSecretaria='" & Me.DropSecr.SelectedValue & "'", conectar.conectar)
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
    Public Sub LlenarColonia()

        Dim sent2 As New System.Data.SqlClient.SqlCommand("Select CveColonia,NombrColonia from CosteoColonia", conectar.conectar)
        Dim exe2 As System.Data.SqlClient.SqlDataReader

        exe2 = sent2.ExecuteReader
        Try
            If Not exe2 Is Nothing Then
                DropColonia.DataSource = exe2
                DropColonia.DataTextField = "NombrColonia"
                DropColonia.DataValueField = "NombrColonia"
                DropColonia.DataBind()
                exe2.NextResult()
            End If
            exe2.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LlenarTipoAccion()
        'LLENA tipo de accion (tipos de trabajo)
        Dim sent3 As New System.Data.Odbc.OdbcCommand("Select clave_titr,nombr_titr from iso9002@pdc3:CALLTIPOTRABAJO", conectar.conectarNomina)
        Dim exe3 As System.Data.Odbc.OdbcDataReader

        exe3 = sent3.ExecuteReader
        Try
            If Not exe3 Is Nothing Then
                DropTipoAccion.DataSource = exe3
                DropTipoAccion.DataTextField = "nombr_titr"
                DropTipoAccion.DataValueField = "nombr_titr"
                DropTipoAccion.DataBind()
                exe3.NextResult()
            End If
            exe3.Close()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub CheckObj_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckColonia.CheckedChanged
        If CheckColonia.Checked = True Then
            DropColonia.Enabled = False
        Else
            DropColonia.Enabled = True
        End If
    End Sub
    Protected Sub CheckEstr_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckTipoAccion.CheckedChanged
        If CheckTipoAccion.Checked = True Then
            DropTipoAccion.Enabled = False
        Else
            DropTipoAccion.Enabled = True
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
