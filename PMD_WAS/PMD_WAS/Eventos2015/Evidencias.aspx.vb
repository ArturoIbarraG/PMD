Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion

Imports System.IO.MemoryStream
Imports System.Text
Imports System.Net
Imports System.Net.Mail

Partial Class Evidencias
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Evidencias")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim con As New conexion
        Dim stry As String

        'FALTA AGREGAR COMENTARIO
        'stry = "Select folio,nombre_evento,lugar,hr_ini,hr_alcalde,hr_salida,hr_fin,Convert(char, fecha, 103) as fecha,Responsable Secretario,Operador Solicitante,telefono_ope telefono,(CASE WHEN (validada = '0') THEN 'PENDIENTE' WHEN (validada = '1') THEN 'VALIDADA' WHEN (validada = '2') THEN 'NO APROBADA' END) AS estatus from [eventos].reg_evento where fecha >= '2016/05/17'  order by 1"
        stry = "Select folio,nombre_evento,lugar,Convert(char, fecha, 103) as fecha from [eventos].reg_evento where month(fecha) >= month( GETDATE()) -1  order by 1"

        Dim da As New SqlDataAdapter(stry, con.Conectar)
        Dim dt As New DataTable()


        Try

            da.Fill(dt)
            Me.GridView1.DataSource = dt
            If dt.Rows.Count = 0 Then
                'no hay nada
                Me.GridView1.DataBind()
                'MSJ DE QUE NO HAY FOLIOS
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_NoHayInfo();", True)


            Else
                'si trae
                Me.GridView1.DataBind()
            End If
        Catch ex As Exception
            MsgBox(da.Fill(dt))
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim lnk As New LinkButton
        Dim DatoIX As String = ""
        Dim DAtos2 As String = ""

        lnk = CType(e.CommandSource, LinkButton)

        DatoIX = lnk.Text 'Este TextBox esta en el Modal
        DAtos2 = lnk.CommandArgument.ToString
        Label6.Text = DatoIX
        ConsultaEvento(DatoIX)
        BuscaImagen(DatoIX)
    End Sub

    Private Sub ConsultaEvento(FolioRx As String)
        Dim strCon As New System.Data.SqlClient.SqlConnection
        Dim ValorTx As String = ""
        Dim server As String = "WIN-6CFDTR9QMDB" '"localhost"
        Dim uid As String = "user_envio"  'localhost
        Dim pwd As String = "mexico78"
        Dim database As String = "eventos"
        strCon.ConnectionString = "Data Source=(local)\SQL2012;Initial Catalog=Planeacion_Financiera;Persist Security Info=True;Integrated Security=SSPI;" '"server=WIN-VS5TZOS43BN; uid=user_envio; pwd=mexico78; DATABASE=eventos"
        strCon.Open()
        Dim RsGen As New System.Data.SqlClient.SqlCommand("select folio,nombre_evento,lugar,Convert(char, fecha, 103) as fecha from [eventos].reg_evento where folio= " & FolioRx, strCon)
        Dim Drsx As System.Data.SqlClient.SqlDataReader
        Drsx = RsGen.ExecuteReader
        Try
            While Drsx.Read
                Label6.Text = Drsx(0)
                Label8.Text = Drsx(1)
                Label9.Text = Drsx(2)
                Label10.Text = Drsx(3)
            End While
        Finally
            Drsx.Close()
        End Try
        strCon.Close()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Nuevo" Then
            Button2.Text = "Guardar"
            FileUpload1.Enabled = True

        Else
            Button2.Text = "Nuevo"
            FileUpload1.Enabled = False


            Dim path As String = Server.MapPath("~/UploadedImages/")
            Dim fileOK As Boolean = False
            If FileUpload1.HasFile Then
                Dim fileExtension As String
                Dim PtoI As Integer = 0
                Dim Larg As Integer = 0
                fileExtension = System.IO.Path.
                    GetExtension(FileUpload1.FileName).ToLower()
                Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif"}
                For i As Integer = 0 To allowedExtensions.Length - 1
                    If fileExtension = allowedExtensions(i) Then
                        fileOK = True
                    End If
                Next
                If fileOK Then
                    Try
                        FileUpload1.PostedFile.SaveAs(path &
                             FileUpload1.FileName)
                        Dim Ext As String = ""
                        Dim NFilex As String = ""
                        Dim NxPath As String = ""
                        PtoI = InStr(FileUpload1.FileName, ".")
                        Larg = Len(FileUpload1.FileName) - PtoI + 1
                        Ext = Mid(FileUpload1.FileName, PtoI, Larg)
                        'PtoI = Len(FileUpload1.FileName) - Ext
                        Label28.Text = SQLExecute("exec SP_AltaEvidencia " & Label6.Text & ",'','',''")
                        NFilex = path & "Evento_" & Format(Val(Label6.Text), "0000") & "_" & Format(Val(Label28.Text), "000") & Ext
                        Rename(path & FileUpload1.FileName, NFilex)
                        NxPath = "UploadedImages/" & "Evento_" & Format(Val(Label6.Text), "0000") & "_" & Format(Val(Label28.Text), "000") & Ext
                        SQLExecute("update [eventos].tbl_evidencias set archivo ='" & NxPath.Trim & "' where folio=" & Label6.Text & " and cosec=" & Label28.Text)
                        BuscaImagen(Val(Label6.Text))
                        'Label20.Text = "Archivo cargado exitosamente!"
                    Catch ex As Exception
                        'Label20.Text = "El Archivo no se puede cargar, intente de nuevo"
                    End Try
                Else
                    'Label20.Text = "El formato del archivo es incorrecto"
                End If
            End If
        End If
    End Sub

    Private Function SQLExecute(SQLR As String) As String
        Dim strCon As New System.Data.SqlClient.SqlConnection
        Dim ValorTx As String = ""
        Dim server As String = "WIN-6CFDTR9QMDB" '"localhost"
        Dim uid As String = "user_envio"  'localhost
        Dim pwd As String = "mexico78"
        Dim database As String = "eventos"
        strCon.ConnectionString = "server=WIN-VS5TZOS43BN; uid=user_envio; pwd=mexico78; DATABASE=eventos"
        strCon.Open()
        Dim RsGen As New System.Data.SqlClient.SqlCommand(SQLR, strCon)
        Dim Drsx As System.Data.SqlClient.SqlDataReader
        Drsx = RsGen.ExecuteReader
        Try
            While Drsx.Read
                ValorTx = Drsx(0)
            End While
        Finally
            Drsx.Close()
        End Try
        strCon.Close()
        SQLExecute = ValorTx
    End Function
    Private Sub BuscaImagen(Imx As String)
        Dim con As New conexion
        Dim stry As String

        'FALTA AGREGAR COMENTARIO
        'stry = "Select folio,nombre_evento,lugar,hr_ini,hr_alcalde,hr_salida,hr_fin,Convert(char, fecha, 103) as fecha,Responsable Secretario,Operador Solicitante,telefono_ope telefono,(CASE WHEN (validada = '0') THEN 'PENDIENTE' WHEN (validada = '1') THEN 'VALIDADA' WHEN (validada = '2') THEN 'NO APROBADA' END) AS estatus from [eventos].reg_evento where fecha >= '2016/05/17'  order by 1"
        stry = "Select folio,cosec,'http://eventos.sanicolas.gob.mx/eventos/'  + rtrim(archivo) archivo from [eventos].tbl_evidencias where folio=" & Imx & "  order by cosec"

        Dim da As New SqlDataAdapter(stry, con.Conectar)
        Dim dt As New DataTable()


        Try

            da.Fill(dt)
            Me.GridView2.DataSource = dt
            If dt.Rows.Count = 0 Then
                'no hay nada
                Me.GridView2.DataBind()
                'MSJ DE QUE NO HAY FOLIOS
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "alerta_NoHayInfo();", True)

            Else
                'si trae
                Me.GridView2.DataBind()
            End If
        Catch ex As Exception
            MsgBox(da.Fill(dt))
        End Try
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class

