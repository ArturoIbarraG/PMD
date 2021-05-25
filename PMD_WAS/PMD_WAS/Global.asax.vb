Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        Dim script = New ScriptResourceDefinition()
        script.Path = "~/scripts/jquery-3.5.1.min.js"
        script.DebugPath = "~/scripts/jquery-3.5.1.min.js"
        script.CdnPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.5.1.min.js"
        script.CdnDebugPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.5.1.min.js"

        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", script)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
        Session("paso") = "0"
        Session("Clave_empl") = "0"
        Session("Año") = "0"
        Session("seguridad") = "0"
        Session("Admon") = "0"
        Session("NombreEmpl") = ""
        'Session("textAdmon") = "0"
    End Sub
End Class