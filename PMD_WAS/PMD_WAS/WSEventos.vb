Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WSEventos
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function ObtieneEventos(numEmpleado As Integer) As String
        Dim con As New Class1
        Dim dt As DataTable
        'Carga eventos
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@clave_empl", numEmpleado)
                }

            dt = data.ObtieneDatos("ObtieneEventosCalendario", params).Tables(0)
            dt.TableName = "Eventos"
        End Using

        Dim serialicer = New JavaScriptSerializer()

        Dim eventos As List(Of Evento) = New List(Of Evento)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim e As New Evento()
            e.Fecha = dt.Rows(i)("Fecha")
            e.Nombre = dt.Rows(i)("Nombre")
            e.Id = dt.Rows(i)("Id")
            eventos.Add(e)
        Next

        Return serialicer.Serialize(eventos)
    End Function

    Public Class Evento
        Public Id As Integer
        Public Nombre As String
        Public Fecha As String
    End Class

    Public Class Notificacion
        Public Id As Integer
        Public Mensaje As String
        Public Fecha As String
        Public Url As String
        Public Leido As Boolean
    End Class

    Public Class DesgloceAnual
        Public Tipo As String
        Public Enero As Decimal
        Public Febrero As Decimal
        Public Marzo As Decimal
        Public Abril As Decimal
        Public Mayo As Decimal
        Public Junio As Decimal
        Public Julio As Decimal
        Public Agosto As Decimal
        Public Septiembre As Decimal
        Public Octubre As Decimal
        Public Noviembre As Decimal
        Public Diciembre As Decimal
    End Class

    <WebMethod()>
    Public Function ObtieneMensajes(clave_empl As Integer) As String
        Dim con As New Class1
        Dim dt As DataTable
        'Carga eventos
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@clave_empl", clave_empl)
                }

            dt = data.ObtieneDatos("ObtieneNotificaciones", params).Tables(0)
            dt.TableName = "Notificaciones"
        End Using

        Dim serialicer = New JavaScriptSerializer()

        Dim notificaciones As List(Of Notificacion) = New List(Of Notificacion)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim n As New Notificacion()
            n.Fecha = dt.Rows(i)("Fecha")
            n.Mensaje = dt.Rows(i)("Mensaje")
            n.Id = dt.Rows(i)("Id")
            n.Url = dt.Rows(i)("Url")
            n.Leido = dt.Rows(i)("Leido")
            notificaciones.Add(n)
        Next

        Return serialicer.Serialize(notificaciones)
    End Function


    Public Class EventoPresupuesto

    End Class

    <WebMethod()>
    Public Function RegresaGraficoAnualEventosPresupuesto(año As Integer, estatus As Integer) As String
        Try
            Dim helperEventos As New IntelipolisEngine.Eventos.EventoHelper()
            Dim dt As DataTable = helperEventos.ObtieneGraficoEventos(año, estatus)

            Dim desgloce As List(Of DesgloceAnual) = New List(Of DesgloceAnual)
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim d As New DesgloceAnual()
                d.Tipo = dt.Rows(i)("nombre")
                d.Enero = dt.Rows(i)("Enero")
                d.Febrero = dt.Rows(i)("Febrero")
                d.Marzo = dt.Rows(i)("Marzo")
                d.Abril = dt.Rows(i)("Abril")
                d.Mayo = dt.Rows(i)("Mayo")
                d.Junio = dt.Rows(i)("Junio")
                d.Julio = dt.Rows(i)("Julio")
                d.Agosto = dt.Rows(i)("Agosto")
                d.Septiembre = dt.Rows(i)("Septiembre")
                d.Octubre = dt.Rows(i)("Octubre")
                d.Noviembre = dt.Rows(i)("Noviembre")
                d.Diciembre = dt.Rows(i)("Diciembre")
                desgloce.Add(d)
            Next

            Dim serialicer = New JavaScriptSerializer()
            Return serialicer.Serialize(desgloce)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    <WebMethod()>
    Public Function RegresaComportamientoAnualPresupuestoEventos() As String
        Try
            Dim con As New Class1
            Dim ds As DataSet
            'Carga eventos
            Using data As New DB(con.conectar())
                ds = data.ObtieneDatos("ObtieneResumenGraficoEventos", Nothing)
            End Using


            Dim serialicer = New JavaScriptSerializer()
            Dim desgloce As List(Of DesgloceAnual) = New List(Of DesgloceAnual)
            For i As Integer = 0 To ds.Tables.Count - 1
                Dim dt As DataTable = ds.Tables(i)
                Dim d As New DesgloceAnual()
                d.Enero = dt.Rows(0)("Enero")
                d.Febrero = dt.Rows(0)("Febrero")
                d.Marzo = dt.Rows(0)("Marzo")
                d.Abril = dt.Rows(0)("Abril")
                d.Mayo = dt.Rows(0)("Mayo")
                d.Junio = dt.Rows(0)("Junio")
                d.Julio = dt.Rows(0)("Julio")
                d.Agosto = dt.Rows(0)("Agosto")
                d.Septiembre = dt.Rows(0)("Septiembre")
                d.Octubre = dt.Rows(0)("Octubre")
                d.Noviembre = dt.Rows(0)("Noviembre")
                d.Diciembre = dt.Rows(0)("Diciembre")
                desgloce.Add(d)
            Next

            Return serialicer.Serialize(desgloce)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    <WebMethod()>
    Public Function MarcaLeidos(clave_empl As Integer) As String
        Dim con As New Class1
        Dim dt As DataTable
        'Carga eventos
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@clave_empl", clave_empl)
                }

            dt = data.EjecutaCommand("MarcaNotificacionesLeidas", params)
        End Using

        Return ""
    End Function

    <WebMethod()>
    Public Function ObtienePresupuestoDireccionGrafico(anio As String, secretaria As String, direccion As String) As String
        Try
            Dim con As New Class1
            Dim dtDatos As DataTable
            'Carga eventos
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@IdSecretaria", secretaria),
                         New SqlParameter("@IdDependencia", direccion),
                          New SqlParameter("@IdAnio", anio)
                    }

                dtDatos = data.ObtieneDatos("ObtienePresupuestoDireccion", params).Tables(0)
            End Using

            Dim total As Decimal = dtDatos.Compute("SUM(Total)", "")
            Dim listado As New List(Of DesglocePresupuesto)
            For Each r As DataRow In dtDatos.Rows
                Dim d As New DesglocePresupuesto()
                d.Id = r("Id_Linea")
                d.Descripcion = String.Format("{0} - {1}", r("Id_Linea"), r("Linea"))
                d.Presupuesto = r("Total")
                d.Porcentaje = d.Presupuesto / total
                listado.Add(d)
            Next

            Dim serialicer = New JavaScriptSerializer()
            Return serialicer.Serialize(listado)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    <WebMethod()>
    Public Function ObtienePresupuestoActividadGrafico(anio As String, secretaria As String, direccion As String, actividad As String) As String
        Try
            Dim con As New Class1
            Dim dtDatos As DataTable
            'Carga eventos
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@IdSecretaria", secretaria),
                         New SqlParameter("@IdDependencia", direccion),
                          New SqlParameter("@IdAnio", anio),
                          New SqlParameter("@Actividad", actividad)
                    }

                dtDatos = data.ObtieneDatos("ObtienePresupuestoActividad", params).Tables(0)
            End Using

            Dim total As Decimal = dtDatos.Compute("SUM(Total)", "")
            Dim listado As New List(Of DesglocePresupuesto)
            For Each r As DataRow In dtDatos.Rows
                Dim d As New DesglocePresupuesto()
                d.Id = r("Id_Subactividad")
                d.Descripcion = String.Format("{0} - {1}", r("Id_Subactividad"), r("SubActividad"))
                d.Presupuesto = r("Total")
                d.Porcentaje = d.Presupuesto / total
                listado.Add(d)
            Next

            Dim serialicer = New JavaScriptSerializer()
            Return serialicer.Serialize(listado)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    <WebMethod()>
    Public Function ObtienePresupuestoSubActividadGrafico(anio As String, secretaria As String, direccion As String, subActividad As String) As String
        Try
            Dim con As New Class1
            Dim dtDatos As DataTable
            'Carga eventos
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@IdSecretaria", secretaria),
                         New SqlParameter("@IdDependencia", direccion),
                          New SqlParameter("@IdAnio", anio),
                          New SqlParameter("@SubActividad", subActividad)
                    }

                dtDatos = data.ObtieneDatos("ObtienePresupuestoSubActividad", params).Tables(0)
            End Using

            Dim total As Decimal = dtDatos.Compute("SUM(Total)", "")
            Dim listado As New List(Of DesglocePresupuesto)
            For Each r As DataRow In dtDatos.Rows
                Dim d As New DesglocePresupuesto()
                d.Id = r("Id")
                d.Descripcion = String.Format("{0}", r("Tipo"))
                d.Presupuesto = r("Total")
                d.Porcentaje = d.Presupuesto / total
                listado.Add(d)
            Next

            Dim serialicer = New JavaScriptSerializer()
            Return serialicer.Serialize(listado)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    <WebMethod()>
    Public Function ObtieneDesglocePresupuestoGrafico(anio As String, secretaria As String, direccion As String, actividad As String, subActividad As String) As String
        Try
            Dim con As New Class1
            Dim dtDatos As DataTable
            'Carga eventos
            Using data As New DB(con.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@IdSecretaria", secretaria),
                         New SqlParameter("@IdDependencia", direccion),
                          New SqlParameter("@IdAnio", anio),
                          New SqlParameter("@Actividad", actividad),
                          New SqlParameter("@SubActividad", subActividad)
                    }

                dtDatos = data.ObtieneDatos("ObtieneDesglocePresupuestoGrafico", params).Tables(0)
            End Using

            Dim total As Decimal = dtDatos.Compute("SUM(Total)", "")
            Dim listado As New List(Of DesglocePresupuesto)
            For Each r As DataRow In dtDatos.Rows
                Dim d As New DesglocePresupuesto()
                d.Id = r("Id")
                d.Descripcion = String.Format("{0}", r("Tipo"))
                d.Presupuesto = r("Total")
                d.Porcentaje = d.Presupuesto / total
                listado.Add(d)
            Next

            Dim serialicer = New JavaScriptSerializer()
            Return serialicer.Serialize(listado)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Class DesglocePresupuesto
        Public Id As String
        Public Descripcion As String
        Public Presupuesto As Decimal
        Public Porcentaje As Decimal
    End Class

    Public Class Material
        Public Nombre As String
        Public Cantidad As String
        Public Costo As String
        Public Total As String
    End Class

    <WebMethod()>
    Public Function ObtieneMateriales(id As Integer, clave As Integer) As String
        Dim con As New Class1
        Dim dt As DataTable
        'Carga eventos
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@idEvento", id),
                    New SqlParameter("@ClaveGastos", clave)
                }

            dt = data.ObtieneDatos("ObtieneMaterialEvento", params).Tables(0)
            dt.TableName = "Material"
        End Using

        Dim serialicer = New JavaScriptSerializer()

        Dim material As List(Of Material) = New List(Of Material)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim n As New Material()
            n.Nombre = dt.Rows(i)("Nombre")
            n.Cantidad = String.Format("{0:n0}", dt.Rows(i)("Cantidad"))
            n.Costo = String.Format("{0:C2}", dt.Rows(i)("Costo"))
            n.Total = String.Format("{0:C2}", dt.Rows(i)("Total"))
            material.Add(n)
        Next

        Return serialicer.Serialize(material)
    End Function

    <WebMethod()>
    Public Function ObtieneInformacionPresupuesto(admon As Integer, anio As Integer, secretaria As Integer, direccion As Integer) As String
        Dim con As New Class1
        Dim dt As DataTable
        'Carga eventos
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdAdmon", admon),
                    New SqlParameter("@IdDependencia", direccion),
                      New SqlParameter("@IdSecretaria", secretaria),
                    New SqlParameter("@Anio", anio)
                }

            dt = data.ObtieneDatos("ObtieneInformacionPresupuesto", params).Tables(0)
            Dim presupuesto = New List(Of InformacionPresupuesto)
            For Each row In dt.Rows
                Dim info = New InformacionPresupuesto
                info.Capturado = row("Capturado")
                info.Direccion = row("Nombr_direccion")
                info.Autorizado = row("Autorizado")
                info.Comprometido = row("Comprometido")
                info.Devengado = row("Devengado")
                info.Presupuesto = row("Presupuesto")
                info.Secretaria = row("Nombr_secretaria")
                info.IdDireccion = row("IdDireccion")
                info.IdSecretaria = row("IdSecretaria")
                presupuesto.Add(info)
            Next

            Dim serialicer = New JavaScriptSerializer()
            Return serialicer.Serialize(presupuesto)
        End Using
    End Function

    <WebMethod()>
    Public Function ObtieneProgressPresupuesto(anio As Integer, dependencia As Integer)
        Dim con As New Class1
        Dim dt As DataTable
        'Carga eventos
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@IdDependencia", dependencia),
                    New SqlParameter("@Anio", anio)
                }

            dt = data.ObtieneDatos("ObtieneProgressPresupuesto", params).Tables(0)
            Dim presupuesto = New List(Of ProgressPresupuesto)
            For Each row In dt.Rows
                Dim info = New ProgressPresupuesto
                info.Presupuesto = row("Presupuesto")
                info.Capturado = row("Capturado")
                info.Autorizado = row("Autorizado")
                info.Comprometido = row("Comprometido")
                info.Devengado = row("Devengado")
                presupuesto.Add(info)
            Next

            Dim serialicer = New JavaScriptSerializer()
            Return serialicer.Serialize(presupuesto)
        End Using
    End Function

    Public Class ProgressPresupuesto
        Public Presupuesto As Decimal
        Public Capturado As Decimal
        Public Autorizado As Decimal
        Public Comprometido As Decimal
        Public Devengado As Decimal
    End Class

    Public Class InformacionPresupuesto
        Public IdSecretaria As Integer
        Public Secretaria As String
        Public IdDireccion As Integer
        Public Direccion As String
        Public Presupuesto As Decimal
        Public Capturado As Decimal
        Public Autorizado As Decimal
        Public Devengado As Decimal
        Public Comprometido As Decimal
    End Class

End Class