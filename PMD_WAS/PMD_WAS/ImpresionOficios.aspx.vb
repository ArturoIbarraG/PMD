
Imports System.Data.SqlClient

Partial Class ImpresionOficios
    Inherits System.Web.UI.Page
    Dim con As New Class1

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'ASIGNA NOMBRE MAQUINA
        Dim masterPage = DirectCast(Me.Master, MasterNuevaImagen)
        masterPage.AsignaNombrePagina("Impresión de oficio presupuestal")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Carga el año
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@idAdmon", 3)
                }

                ddlAnio.DataSource = data.ObtieneDatos("ObtieneAñosAdministracion", params)
                ddlAnio.DataTextField = "Año"
                ddlAnio.DataValueField = "Año"
                ddlAnio.DataBind()
            End Using
            ddlAnio.Items.Insert(0, New ListItem("Selecciona el año", "0"))
            ddlAnio.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga secretarias
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlSecretaria.DataSource = data.ObtieneDatos("ObtieneSecretarias", params)
            ddlSecretaria.DataTextField = "Nombr_secretaria"
            ddlSecretaria.DataValueField = "IdSecretaria"
            ddlSecretaria.DataBind()
        End Using
        ddlSecretaria.Items.Insert(0, New ListItem("Selecciona la secretaría", "0"))

    End Sub

    Protected Sub ddlSecretaria_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Direcciones
        Using data As New DB(con.conectar())

            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@idAdmon", 3),
                New SqlParameter("@idSecretaria", ddlSecretaria.SelectedValue),
                New SqlParameter("@clave_empl", Session("Clave_empl"))
            }

            ddlDependencia.DataSource = data.ObtieneDatos("ObtieneDireciones", params)
            ddlDependencia.DataTextField = "Nombr_direccion"
            ddlDependencia.DataValueField = "IdDireccion"
            ddlDependencia.DataBind()
            Dim helper As New IntelipolisEngine.Eventos.EventoHelper()

            'Carga clave
            lblSecretaria.Text = ddlSecretaria.SelectedValue
        End Using
        ddlDependencia.Items.Insert(0, New ListItem("Selecciona la Dirección", "0"))
    End Sub

    Protected Sub ddlDependencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Carga Presupuesto Lineas
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdAnio", ddlAnio.SelectedValue),
                    New SqlParameter("@IdSecretaria", ddlSecretaria.SelectedValue),
                    New SqlParameter("@IdDependencia", ddlDependencia.SelectedValue)
                }

            grdOficios.DataSource = data.ObtieneDatos("ObtieneSubActividadesAutorizadas", params)
            grdOficios.DataBind()

            'Carga clave
            lblDependencia.Text = ddlDependencia.SelectedValue

        End Using
    End Sub
    Protected Sub lnqMostrarPDF_Command(sender As Object, e As CommandEventArgs)
        'Try
        Dim folio = Integer.Parse(e.CommandArgument)

            Dim helper = New IntelipolisEngine.Eventos.EventoHelper()
            Dim url = helper.RegresaFormatoOficioURL(folio)

            ScriptManager.RegisterStartupScript(updOficios, updOficios.GetType(), "muestra_pdf", String.Format("openModalIFrame('{0}')", ResolveClientUrl(url)), True)
        'Catch ex As Exception
        '    ScriptManager.RegisterStartupScript(updOficios, updOficios.GetType(), "muestra_error", String.Format("muestraMensajeError('{0}')", ex.Message), True)
        'End Try
    End Sub
End Class
