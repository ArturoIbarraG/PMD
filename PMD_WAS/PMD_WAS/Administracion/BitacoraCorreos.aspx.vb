Imports System.Data.SqlClient

Public Class BitacoraCorreos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtfechaIni.Text = IntelipolisEngine.Helper.Helper.GetFirstHourOfDay(Date.Now).ToString("yyyy-MM-dd")
            txtfechafin.Text = IntelipolisEngine.Helper.Helper.GetLastHourOfDay(Date.Now).ToString("yyyy-MM-dd")
        End If
    End Sub


    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
        {
            New SqlParameter("@fechaIni", DateTime.Parse(txtfechaIni.Text)),
             New SqlParameter("@fechaFin", DateTime.Parse(txtfechafin.Text)),
             New SqlParameter("@enviado", ddlEstatus.SelectedValue)
        }
            Dim dt = data.ObtieneDatos("ObtieneBitacoraCorreo", params).Tables(0)
            grdBitacoraCorreo.DataSource = dt
            grdBitacoraCorreo.DataBind()
        End Using
    End Sub
End Class