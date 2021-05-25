Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion

Imports System.IO.MemoryStream
Imports System.Text
Imports System.Net
Imports System.Net.Mail


Partial Class ConsultaEstatus
    Inherits System.Web.UI.Page


    Dim resultado As Integer
    Dim email_secr As String
    Dim todoslosemail As String
    Dim fecha_envio As String
    Dim nombre_evento As String
    Dim desc_evento As String
    Dim calle_evento As String
    Dim col_evento As String
    Dim numext_evento As String
    Dim fecha_evento As String
    Dim hr_evento As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Consulta estatus")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("paso") = "1" Then

        Else
            Response.Redirect("~/Password.aspx")
        End If


        If Not IsPostBack = True Then

            CargarEventos()
        End If




    End Sub


    Sub CargarEventos()
        Dim con As New conexion
        Dim stry As String

        'FALTA AGREGAR COMENTARIO
        'stry = "Select folio,nombre_evento,lugar,hr_ini,hr_alcalde,hr_salida,hr_fin,Convert(char, fecha, 103) as fecha,Responsable Secretario,Operador Solicitante,telefono_ope telefono,(CASE WHEN (validada = '0') THEN 'PENDIENTE' WHEN (validada = '1') THEN 'VALIDADA' WHEN (validada = '2') THEN 'NO APROBADA' END) AS estatus from [eventos].reg_evento where fecha >= '2016/05/17'  order by 1"
        stry = "Select folio,nombre_evento,lugar,hr_ini,hr_alcalde,hr_salida,hr_fin,Convert(char, fecha, 103) as fecha,Responsable Secretario,Operador Solicitante,telefono_ope telefono,(CASE WHEN (validada = '0') THEN 'PENDIENTE' WHEN (validada = '1') THEN 'VALIDADA' WHEN (validada = '2') THEN 'NO APROBADA' END) AS estatus,comentario from [eventos].reg_evento where month(fecha) >= month( GETDATE()) -1  order by 1"

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




     
End Class
