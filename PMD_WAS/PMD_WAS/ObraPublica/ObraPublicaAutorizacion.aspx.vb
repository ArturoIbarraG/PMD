Imports System.Data.SqlClient

Public Class ObraPublicaAutorizacion
    Inherits System.Web.UI.Page

    Dim con As New OPConexion

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarObrasPublicas()
    End Sub

    Private Sub CargarObrasPublicas()

        Using data As New DB(con.conectar())

            gridObrasPublicas.DataSource = data.ObtieneDatos("CargarObrasPublicas", Nothing)
            gridObrasPublicas.DataBind()

        End Using
    End Sub

    Protected Sub btnAutorizar_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(updObraPublicaAutorizacion, updObraPublicaAutorizacion.GetType(), "muestraModal", "abreModalPOA();", True)
    End Sub

End Class