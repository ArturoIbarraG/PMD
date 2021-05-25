
Partial Class Test
    Inherits System.Web.UI.Page

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs)
        'Dim lugar = txtLugar.Text
        'Dim mensaje = txtMensaje.Text
        'Dim fechaIni = DateTime.Parse(txtFechaInicio.Text)
        'Dim fechaFin = DateTime.Parse(txtFechaFin.Text)
        'Dim helper As New Helper()
        'helper.EnviaCoreoCalendario(fechaIni, fechaFin, mensaje, lugar)
        'rmedinamx@gmail.com
        Dim helper As New Helper()
        helper.EnviaCoreoCalendario("ricardo.eslava89@gmail.com", "Nuevo evento", "Evento de Sistemas", DateTime.Now, DateTime.Now.AddHours(2), "Prueba de mensaje", "Sicilia 133, Lombardia Residencial, Apodaca Nuevo Leon")
    End Sub
End Class
