Partial Class Bienvenido
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("paso") = "1" Then
        Else
            Response.Redirect("Password.aspx")
        End If

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            'ASIGNA NOMBRE MAQUINA
            Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
            masterPage.AsignaNombrePagina(String.Format("<h6>Bienvenido {0}</h6>", Session("NombreEmpl")))

            Dim idPuesto = Integer.Parse(Session("Puesto"))
            If idPuesto = 1 Then
                'DIRECTOR
                Dim controlPuesto = LoadControl("~/UserControl/ucDashboardDireccion.ascx")
                pnlContainer.Controls.Add(controlPuesto)
            ElseIf idPuesto = 6 Then
                'SECRETARIO
                Dim controlPuesto = LoadControl("~/UserControl/ucDashboardSecretaria.ascx")
                pnlContainer.Controls.Add(controlPuesto)
            End If
        Catch ex As Exception
            Response.Redirect("~/Password.aspx")
        End Try
    End Sub
End Class
