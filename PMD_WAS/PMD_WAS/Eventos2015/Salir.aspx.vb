
Partial Class Salir
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Response.Redirect("~/Password.aspx")

    End Sub
End Class
