Imports System.Data.SqlClient

Public Class Bitacora
    Inherits System.Web.UI.Page
    Dim con As New Class1
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Bitácora de movimientos")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim fecha = DateTime.Now
            txtfechaIni.Text = IntelipolisEngine.Helper.Helper.GetFirstDayMonth(fecha).ToString("yyyy-MM-dd")
            txtfechaFin.Text = IntelipolisEngine.Helper.Helper.GetLastDayMonth(fecha).ToString("yyyy-MM-dd")

            btnBuscar_Click(Nothing, Nothing)
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Try
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() {New SqlParameter("@fechaIni", IntelipolisEngine.Helper.Helper.GetFirstHourOfDay(DateTime.Parse(txtfechaIni.Text))), New SqlParameter("@fechaFin", IntelipolisEngine.Helper.Helper.GetLastHourOfDay(DateTime.Parse(txtfechaFin.Text)))}

                grdBitacora.DataSource = data.ObtieneDatos("ObtieneBitacora", params)
                grdBitacora.DataBind()
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updBitacora, updBitacora.GetType(), "muestra_mensaje", String.Format("mensajeCustom('error', 'Ocurrio un error', {0});", "Ocurrió un error al mostrar la información, favor de intentarlo nuevamente más tarde."), True)
        End Try
    End Sub
End Class