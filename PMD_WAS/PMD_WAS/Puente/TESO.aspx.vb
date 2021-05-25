Public Class TESO
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim generalEngine = New IntelipolisEngine.PMD.General()
        Dim dependencia = RegresaSoloDireccion(generalEngine.ObtieneDependencia(Session("clave_depe"), Session("id_secr")))
        Dim url As String = ""
        If Request.QueryString("rh") IsNot Nothing Then
            url = QuitaCaracteres(String.Format("https://admin.sanicolas.gob.mx/TESO/Menu/Menu?Length=4#SubMenuRH&user={0}&pass={1}&nombre={2}&dependencia={3}", Session("Clave_empl").ToString().Trim(), Session("P4$W0r0_S354mo").ToString().Trim(), RegresaSoloNombre(Session("NombreEmpl").ToString().Trim()), dependencia.Trim()))
        Else
            url = QuitaCaracteres(String.Format("https://admin.sanicolas.gob.mx/TESO?user={0}&pass={1}&nombre={2}&dependencia={3}", Session("Clave_empl").ToString().Trim(), Session("P4$W0r0_S354mo").ToString().Trim(), RegresaSoloNombre(Session("NombreEmpl").ToString().Trim()), dependencia.Trim()))
        End If

        Response.Redirect(url)
    End Sub

    Private Function RegresaSoloNombre(nombre As String) As String
        Try
            Return nombre.Split(" ")(0)
        Catch ex As Exception
            Return nombre
        End Try
    End Function
    Private Function RegresaSoloDireccion(dependencia As String) As String
        Try
            dependencia = dependencia.Replace("DIRECCIÓN DE ", "")
            dependencia = dependencia.Replace("dirección de ", "")
            Return dependencia
        Catch ex As Exception
            Return dependencia
        End Try
    End Function

    Private Function QuitaCaracteres(url As String) As String
        url = url.Replace(" ", "_")
        url = url.Replace("Á", "A")
        url = url.Replace("É", "E")
        url = url.Replace("Í", "I")
        url = url.Replace("Ó", "O")
        url = url.Replace("Ú", "U")
        url = url.Replace("á", "a")
        url = url.Replace("é", "e")
        url = url.Replace("í", "i")
        url = url.Replace("ó", "o")
        url = url.Replace("ú", "u")

        Return url
    End Function

End Class