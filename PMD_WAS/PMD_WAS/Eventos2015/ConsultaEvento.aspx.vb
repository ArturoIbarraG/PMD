Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports conexion


Partial Class ConsultaEvento
    Inherits System.Web.UI.Page
    Dim stry As String
    Dim folio As Integer
    Dim tabla As DataTable
    Dim clave_secr As Integer

    Dim clave_depe As Integer

    Dim clave_colo As Integer
    Dim clave_evento As Integer
    Dim clave_calle As Integer
    Dim resultado As Integer
    Dim modiFolio As String
    Dim conx As New conexion
    Dim IDCOL As Integer
    Dim IDSECRE As Integer
    Dim privilegio As Integer
    'Dim corx As String
    Dim corx As Double
    Dim cory As Double

    Dim coordx As String
    Dim coordy As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("paso") = "1" Then

        Else
            Response.Redirect("~/Password.aspx")
        End If


        If Not IsPostBack = True Then


  

        End If


    End Sub

   
    Sub ExportarExcel()

        'stry = "SELECT folio as FOLIO, origen as ORIGEN,secretaria as SECRETARIA,evento AS EVENTO,colonia AS COLONIA,calle AS CALLE,num_ext AS NUM_EXT,num_int AS NUM_INT,Convert(char,fecha, 111) AS FECHA_EVENTO,num_benef as NUM_BENEFICIADOS  FROM [eventos].reg_evento re INNER JOIN origen o ON re.id_origen = o.id_origen INNER JOIN secretarias sec ON re.id_secr = sec.id_secr INNER JOIN colonias col ON re.id_col=col.id_col INNER JOIN calles c ON re.id_calle=c.id_calle INNER JOIN evento  e ON re.id_evento = e.id_evento Group BY  folio,origen,secretaria,evento,colonia,calle,num_ext,num_int,fecha,num_benef ORDER BY re.folio "
        stry = "SELECT re.folio as FOLIO, origen as ORIGEN,secretaria as SECRETARIA,evento AS EVENTO,nombr_colonia AS COLONIA,calle AS CALLE,num_ext AS NUM_EXT,num_int AS NUM_INT,Convert(char,fecha, 111) AS FECHA_EVENTO,num_benef as NUM_BENEFICIADOS,coordx as COORD_X,coordy AS COORD_Y " &
                "FROM [eventos].reg_evento re " &
                "INNER JOIN origen o ON re.id_origen = o.id_origen " &
                "INNER JOIN secretarias sec ON re.id_secr = sec.id_secr " &
                "INNER join [eventos].Xcolonias  col ON re.id_col=col.id_colonia " &
                "INNER JOIN calles c ON re.id_calle=c.id_calle " &
                "INNER JOIN evento  e ON re.id_evento = e.id_evento " &
                "INNER JOIN [eventos].map_evento m ON re.folio=m.folio " &
                "Group BY  re.FOLIO,o.origen,sec.secretaria,e.evento,col.nombr_colonia,c.calle,re.num_ext,re.num_int,re.fecha,re.num_benef,m.coordx,m.coordy ORDER BY re.folio"



        ''-----------------NUEVO QUERY POR LOS NUEVOS CAMBIOS, POR SI QUIEREN EXPORTAR LA INFORMACON

        '        SELECT 
        ' re.folio as FOLIO,re.nombre_evento AS EVENTO,sec.secretaria as SECRETARIA,dir.nombre_depe as DIRECCION, e.evento as TIPO_EVENTO,
        ' re.descripcion as DESCRIPCION,col.nombr_colonia AS COLONIA,c.calle AS CALLE,re.num_ext AS NUM_EXT,re.num_int AS NUM_INT,
        ' Convert(char,re.fecha, 111) AS FECHA_EVENTO,re.hora as HORA_EVENTO ,re.num_benef as NUM_BENEFICIADOS,m.coordx as COORD_X,m.coordy AS COORD_Y ,
        ' PRENSA = CASE re.prensa 
        '        WHEN '0' THEN 'SIN PRENSA'
        '        WHEN '1' THEN 'CON PRENSA'

        '            End
        'FROM [eventos].reg_evento re
        'INNER JOIN evento e ON re.id_evento = e.id_evento
        'INNER JOIN secretarias sec ON re.id_secr = sec.id_secr 
        'INNER JOIN direcciones dir ON re.id_depe = dir.clave_depe and re.id_secr =dir.clave_secr  
        'INNER join [eventos].Xcolonias  col ON re.id_col=col.id_colonia 
        'INNER JOIN calles c ON re.id_calle=c.id_calle 
        'INNER JOIN [eventos].map_evento m ON re.folio=m.folio 
        'Group BY  re.FOLIO,re.nombre_evento,sec.secretaria,dir.nombre_depe,e.evento, 
        're.descripcion,col.nombr_colonia,c.calle,re.num_ext,re.num_int,
        're.fecha,re.hora,re.num_benef,m.coordx,m.coordy,re.prensa
        'ORDER BY re.folio




        tabla = conexion.sqlcon(stry)
        Me.GridView1.DataSource = tabla
        Me.GridView1.DataBind()


        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Me.GridView1.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=REGISTRO_EVENTOS.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()


    End Sub




End Class
