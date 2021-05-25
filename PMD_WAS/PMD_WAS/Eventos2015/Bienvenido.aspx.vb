
Partial Class Bienvenido
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("paso") = "1" Then

        Else
            Response.Redirect("~/Password.aspx")
        End If



        If Not Me.IsPostBack Then
            'Button1.Attributes.Add("Onclick", "javascript:OpenCatalago(‘Text1‘, false ,515, 400,consulta.aspx‘)

            'Button2.Attributes.Add("Onclick", "javascript:VentanaDialogoModal(‘WebVentana2.aspx‘,Text1.value,‘400‘,‘400‘,‘Text1‘);")
            'Button3.Attributes.Add("Onclick", "javascript:VentanaDialogoNoModal(‘WebVentana3.aspx‘,window,‘400‘,‘400‘,‘Text1‘);")
            '
            'Dim usuario As String = Request.Params("usuario")
            'Dim pass As String = Request.Params("pass")
        End If
    End Sub

End Class
