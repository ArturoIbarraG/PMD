
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading

Partial Class MasterGlobal
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            Dim culture As CultureInfo
            If Thread.CurrentThread.CurrentCulture.Name <> "es-MX" Then
                culture = CultureInfo.CreateSpecificCulture("es-MX")
                Thread.CurrentThread.CurrentUICulture = culture
            End If

            If Session("Clave_empl") Is Nothing Or Session("Clave_empl") = "0" Then
                Response.Redirect("~/Password.aspx")
            End If

            Me.Label1.Text = Session("NombreEmpl")

            cargaMenu()

        End If
    End Sub

    Public Sub OcultaHeader()
        pnlHeader.Visible = False
    End Sub


    Private Sub cargaMenu()
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@clave_empl", Session("Clave_empl"))}

            'carga asignados
            Dim dt As DataTable = data.ObtieneDatos("ObtieneMenuEmpleados", params).Tables(0)
            Session("MenuUsuarioPrincipal") = dt

            Try
                rptMenu.DataSource = dt.Select("idpadre = 0").CopyToDataTable()
                rptMenu.DataBind()
            Catch ex As Exception

            End Try


        End Using
    End Sub

    Protected Sub rptMenu_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim dt As DataTable = Session("MenuUsuarioPrincipal")
        Dim id As Integer = DirectCast(e.Item.FindControl("hdnId"), HiddenField).Value
        Dim rpt As Repeater = DirectCast(e.Item.FindControl("rptSecundario"), Repeater)

        Try
            rpt.DataSource = dt.Select(String.Format("idpadre = {0} and activo = True", id)).CopyToDataTable()
            rpt.DataBind()
        Catch ex As Exception

        End Try

    End Sub
End Class

