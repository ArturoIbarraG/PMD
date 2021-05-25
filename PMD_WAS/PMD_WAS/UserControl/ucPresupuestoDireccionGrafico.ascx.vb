Public Class ucPresupuestoDireccionGrafico
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub MuestraDesglocePresupuesto(secretaria As Integer, direccion As Integer, anio As Integer, nombreDireccion As String)
        hdnAnioUserControlPresupuesto.Value = anio
        hdnSecretariaUserControlPresupuesto.Value = secretaria
        hdnDireccionUserControlPresupuesto.Value = direccion
        hdnNombreDireccionUserControlPresupuesto.Value = nombreDireccion

        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "muestra_presupuesto", "muestraGraficoPresupuesto();", True)
    End Sub

End Class