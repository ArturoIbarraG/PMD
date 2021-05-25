
Imports System.Data.SqlClient

Partial Class Configuraciones
    Inherits System.Web.UI.Page
    Dim con As New conexion
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Configuración")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargaConfiguracion()
        End If
    End Sub

    Private Sub cargaConfiguracion()
        Using data As New DB(con.Conectar())

            Dim ds = data.ObtieneDatos("ObtieneConfiguracion", Nothing)

            If ds.Tables(0).Rows.Count > 0 Then

                'CORREOS
                txtCorreoAlcalde.Text = ds.Tables(0).Rows(0)("correoAlcalde")
                txtAuditoriaInterna.Text = ds.Tables(0).Rows(0)("correoAuditoriaInterna")
                txtAuditoriaExterna.Text = ds.Tables(0).Rows(0)("correoAuditoriaExterna")

                'EVENTO
                txtDiasEvento.Text = ds.Tables(0).Rows(0)("diasSolicitarEvento")

            End If

            'ETAPAS
            rptEtapaEventos.DataSource = ds.Tables(1)
            rptEtapaEventos.DataBind()

            'SUMA HORAS
            Dim sum As Decimal = Convert.ToDecimal(ds.Tables(1).Compute("SUM(horas)", String.Empty))
            lblTotalHoras.Text = String.Format("{0} horas / {1} días.", sum, (sum / 24))

        End Using
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        Try
            Using data As New DB(con.Conectar())
                'ACTUALIZAR CORREOS
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@correoAlcalde", txtCorreoAlcalde.Text),
                    New SqlParameter("@correoAuditoriaInterna", txtAuditoriaInterna.Text),
                    New SqlParameter("@correoAuditoriaExterna", txtAuditoriaExterna.Text),
                    New SqlParameter("@diasSolicitarEvento", txtDiasEvento.Text)
                }
                data.EjecutaCommand("ActualizaConfiguracion", params)

                For Each item As RepeaterItem In rptEtapaEventos.Items
                    Try
                        Dim id = Integer.Parse(DirectCast(item.FindControl("hdnId"), HiddenField).Value)
                        Dim horas = Integer.Parse(DirectCast(item.FindControl("txtHoras"), TextBox).Text)

                        Dim params2() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@id", id),
                    New SqlParameter("@horas", horas)
                }
                        data.EjecutaCommand("ActualizaEtapasEvento", params2)
                    Catch ex As Exception

                    End Try
                Next

            End Using

            cargaConfiguracion()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(updConfiguracion, updConfiguracion.GetType(), "mensaje_custom", String.Format("mensajeCustom('{0}','{1}','{2}');", "error", "Ocurrio un error", ex.ToString()), True)
        End Try
    End Sub
End Class
