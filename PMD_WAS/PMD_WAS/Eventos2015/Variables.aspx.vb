
Partial Class Variables
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim var1 As String
        Dim var2 As String
        var1 = Request.QueryString("vx")
        var2 = Request.QueryString("vy")
        Session("coordx") = var1
        Session("coordy") = var2
        Response.Write("<script> self.close();</script>")

    End Sub
End Class
