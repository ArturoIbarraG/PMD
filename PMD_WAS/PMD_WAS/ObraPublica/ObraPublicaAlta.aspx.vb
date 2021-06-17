Imports System.Data.SqlClient

Public Class ObraPublicaAlta
    Inherits System.Web.UI.Page
    Dim con As New OPConexion

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        If txtOPNombre.Text = "" Or
                txtOPOrigenFondos.Text = "" Or
                txtOPDescripcion.Text = "" Or
                txtOPUbicacion.Text = "" Or
                txtOPMontoAsignacion.Text = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "muestraError", String.Format("alert('{0}');", "Faltan campos por llenar."), True)
        Else

            Try

                Using data As New DB(con.conectar())

                    Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@opnombre", txtOPNombre.Text),
                    New SqlParameter("@opmontoasig", Decimal.Parse(txtOPMontoAsignacion.Text)),
                    New SqlParameter("@opdesc", txtOPDescripcion.Text),
                    New SqlParameter("@opubicacion", txtOPUbicacion.Text),
                    New SqlParameter("@oporigenfondos", txtOPOrigenFondos.Text)
                }

                    data.EjecutaCommand("ObraPublicaAlta", params)

                End Using

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
            End Try

            ScriptManager.RegisterStartupScript(updObraPublicaAlta, updObraPublicaAlta.GetType(), "confirmaAlta", "muestraConfirmaAlta();", True)

        End If

    End Sub
End Class