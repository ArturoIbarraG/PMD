Imports System.Data.SqlClient

Public Class ObraPublicaAlta
    Inherits System.Web.UI.Page
    Dim con As New OPConexion

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Try

            Using data As New DB(con.conectar())

                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@numcon", txtOPNumContrato.Text),
                    New SqlParameter("@nombre", txtOPNombre.Text),
                    New SqlParameter("@contratista", txtOPContratista.Text),
                    New SqlParameter("@representante", txtOPRepresentante.Text),
                    New SqlParameter("@orifon", txtOPOrigenFondos.Text),
                    New SqlParameter("@domcon", txtOPDomicilioContratista.Text),
                    New SqlParameter("@desc", txtOPDescripcion.Text),
                    New SqlParameter("@ubi", txtOPUbicacion.Text),
                    New SqlParameter("@monto", Decimal.Parse(txtOPMonto.Text)),
                    New SqlParameter("@montoant", Decimal.Parse(txtOPMontoAnticipo.Text)),
                    New SqlParameter("@fechaini", txtOPFechaInicio.Text),
                    New SqlParameter("@fechafin", txtOPFechaTerminacion.Text),
                    New SqlParameter("@fechacon", txtOPFechaFirma.Text)
                }

                data.EjecutaCommand("NuevaObraPublicaPrueba", params)

            End Using

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", ex.Message), True)
        End Try

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "oculta_modal", String.Format("alert('{0}');", "Obra Pública creada correctamente."), True)

    End Sub
End Class