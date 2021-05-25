Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DataInfo : Implements IDisposable

    Public Sub Dispose() Implements IDisposable.Dispose
    End Sub

    Public Sub CargaSecretariasWS()

        Dim con1 As New Class1
        Using data As New DB(con1.conectar())

            Dim dtActual As DataTable = data.ObtieneDatos("ObtieneTotalSecretarias", Nothing).Tables(0)

            If dtActual.Rows.Count = 0 Then

                Dim service = New WSServicio.Service
                Dim dt = service.ListaSecretariasDependencia("1", "Aq333c_jfnrRRQ!").Tables(0)
                Dim dataView As New DataView(dt) '.AsEnumerable().Select(Of  Select("clave_secr, nombr_secr").Distinct().CopyToDataTable()
                Dim dtNuevo = dataView.ToTable(True, "clave_secr", "nombr_secr")

                Dim dtSecretarias As New DataTable
                dtSecretarias.Columns.Add("IdSecretaria")
                dtSecretarias.Columns.Add("Nombr_Secretaria")
                dtSecretarias.Columns.Add("Admon")
                dtSecretarias.Columns.Add("Estatus")
                For Each dr As DataRow In dtNuevo.Rows
                    Dim row = dtSecretarias.NewRow()
                    row("IdSecretaria") = dr("clave_secr")
                    row("Nombr_Secretaria") = dr("nombr_secr")
                    row("Admon") = "1"
                    row("Estatus") = "1"
                    dtSecretarias.Rows.Add(row)
                Next
                'Inserta Secretarias
                Dim con2 As New Class1
                Using bulkCopy As New SqlBulkCopy(con2.conectar())
                    bulkCopy.DestinationTableName = "Secretarias"
                    bulkCopy.WriteToServer(dtSecretarias)
                End Using

            End If

        End Using

    End Sub

    Public Sub CargaDependenciasWS()
        Dim con1 As New Class1
        Using data As New DB(con1.conectar())
            Dim dtActual As DataTable = data.ObtieneDatos("ObtieneTotalDependencias", Nothing).Tables(0)
            If dtActual.Rows.Count = 0 Then

                Dim service = New WSServicio.Service
                Dim dt = service.ListaSecretariasDependencia("1", "Aq333c_jfnrRRQ!").Tables(0)
                Dim dataView As New DataView(dt) '.AsEnumerable().Select(Of  Select("clave_secr, nombr_secr").Distinct().CopyToDataTable()
                Dim dtNuevo = dataView.ToTable(True, "clave_secr", "clave_depe", "nombr_depe")

                Dim dtDependencias As New DataTable
                dtDependencias.Columns.Add("IdDireccion")
                dtDependencias.Columns.Add("IdSecretaria")
                dtDependencias.Columns.Add("Nombr_direccion")
                dtDependencias.Columns.Add("Admon")
                dtDependencias.Columns.Add("Estatus")
                For Each dr As DataRow In dtNuevo.Rows
                    Dim row = dtDependencias.NewRow()
                    row("IdDireccion") = dr("clave_depe")
                    row("IdSecretaria") = dr("clave_secr")
                    row("Nombr_direccion") = dr("nombr_depe")
                    row("Admon") = "1"
                    row("Estatus") = "1"
                    dtDependencias.Rows.Add(row)
                Next
                'Inserta Secretarias
                Dim con2 As New Class1
                Using bulkCopy As New SqlBulkCopy(con2.conectar())
                    bulkCopy.DestinationTableName = "Direcciones"
                    bulkCopy.WriteToServer(dtDependencias)
                End Using
            End If
        End Using
    End Sub

    Public Sub CargaEmpleadosWS()
        Dim con1 As New Class1

        Using data As New DB(con1.conectar())
            Dim dtActual As DataTable = data.ObtieneDatos("ObtieneEmpleadosTotales", Nothing).Tables(0)
            If dtActual.Rows.Count = 0 Then
                Dim service = New WSServicio.Service
                Dim dtEmpleadosTotales As New DataTable
                dtEmpleadosTotales.Columns.Add("clave_secr")
                dtEmpleadosTotales.Columns.Add("clave_depe")
                dtEmpleadosTotales.Columns.Add("clave_empl")
                dtEmpleadosTotales.Columns.Add("nombr_empl")
                dtEmpleadosTotales.Columns.Add("nombr_cate")
                dtEmpleadosTotales.Columns.Add("sueld_emp")

                'Carga dependencias
                Dim dtDependencias As DataTable = data.ObtieneDatos("ObtieneTotalDependencias", Nothing).Tables(0)
                For Each dep In dtDependencias.Rows
                    'obtiene empleados por dependencia
                    Dim s As String = dep("IdSecretaria")
                    Dim d As String = dep("IdDireccion")
                    Dim dtEmpleados = service.ListaEmpleados(s, d).Tables(0)
                    For Each e In dtEmpleados.Rows
                        Dim nRow = dtEmpleadosTotales.NewRow()
                        nRow("clave_secr") = s
                        nRow("clave_depe") = d
                        nRow("clave_empl") = e("clave_empl")
                        nRow("nombr_empl") = e("nombr_empl")
                        nRow("nombr_cate") = e("nombr_cate")
                        nRow("sueld_emp") = e("sueld_empl")
                        dtEmpleadosTotales.Rows.Add(nRow)
                    Next
                Next

                'Inserta Secretarias
                Dim con2 As New Class1
                Using bulkCopy As New SqlBulkCopy(con2.conectar())
                    bulkCopy.DestinationTableName = "Tbl_Empleados"
                    bulkCopy.WriteToServer(dtEmpleadosTotales)
                End Using
            End If
        End Using
    End Sub

    Public Sub CargaClavesGastoWS()
        Dim con1 As New Class1

        Using data As New DB(con1.conectar())
            Dim pass As String = "fg_3311$.2212v?​"
            Dim cia As String = "2"
            Dim clave As String = ""
            'Dim ws = New WsInfoFin.Service
            'Dim ds = ws.ConsultaArticulos(pass, clave, cia)
            'Dim i = ds.Tables.Count
        End Using

    End Sub

    Public Sub CargaPuestosWS()
        Dim con1 As New Class1

        Using data As New DB(con1.conectar())
            Dim dtActual As DataTable = data.ObtieneDatos("ObtienePuestosTotales", Nothing).Tables(0)
            If dtActual.Rows.Count = 0 Then
                Dim service = New WSServicio.Service
                Dim dtPuestosTotales As New DataTable
                dtPuestosTotales.Columns.Add("clave_secr")
                dtPuestosTotales.Columns.Add("clave_depe")
                dtPuestosTotales.Columns.Add("nombre_puesto")
                dtPuestosTotales.Columns.Add("cantidad")
                dtPuestosTotales.Columns.Add("clave_puesto")

                'Carga dependencias
                Dim dtDependencias As DataTable = data.ObtieneDatos("ObtieneTotalDependencias", Nothing).Tables(0)
                For Each dep In dtDependencias.Rows
                    'obtiene empleados por dependencia
                    Dim s As String = dep("IdSecretaria")
                    Dim d As String = dep("IdDireccion")
                    Dim dtPuestos = service.ConsXPuestos(s, d).Tables(0)
                    For Each e In dtPuestos.Rows
                        Dim nRow = dtPuestosTotales.NewRow()
                        nRow("clave_secr") = s
                        nRow("clave_depe") = d
                        nRow("nombre_puesto") = e("nombr_cate")
                        nRow("cantidad") = e("Column1")
                        nRow("clave_puesto") = 0
                        dtPuestosTotales.Rows.Add(nRow)
                    Next
                Next

                'Inserta Secretarias
                Dim con2 As New Class1
                Using bulkCopy As New SqlBulkCopy(con2.conectar())
                    bulkCopy.DestinationTableName = "Tbl_Puestos"
                    bulkCopy.WriteToServer(dtPuestosTotales)
                End Using
            End If
        End Using
    End Sub

    Public Sub CargaVehiculosWS()
        Dim con1 As New Class1

        Using data As New DB(con1.conectar())
            Dim dtActual As DataTable = data.ObtieneDatos("ObtieneVehiculosTotales", Nothing).Tables(0)
            If dtActual.Rows.Count = 0 Then
                Dim service = New WSServicio.Service
                Dim dtVehiculosTotales As New DataTable
                dtVehiculosTotales.Columns.Add("llave_vehi")
                dtVehiculosTotales.Columns.Add("placa_vehi")
                dtVehiculosTotales.Columns.Add("inven_vehi")
                dtVehiculosTotales.Columns.Add("marca_vehi")
                dtVehiculosTotales.Columns.Add("clave_secr")
                dtVehiculosTotales.Columns.Add("clave_depe")
                dtVehiculosTotales.Columns.Add("linea_vehi")
                dtVehiculosTotales.Columns.Add("noeco_vehi")
                dtVehiculosTotales.Columns.Add("model_vehi")

                'Carga dependencias
                Dim dtDependencias As DataTable = data.ObtieneDatos("ObtieneTotalDependencias", Nothing).Tables(0)
                For Each dep In dtDependencias.Rows
                    'obtiene empleados por dependencia
                    Dim s As String = dep("IdSecretaria")
                    Dim d As String = dep("IdDireccion")
                    Dim dtPuestos = service.ConsultaVehiculos("1", "Aq333c_jfnrRRQ!", s, d).Tables(0)
                    For Each e In dtPuestos.Rows
                        Dim nRow = dtVehiculosTotales.NewRow()
                        nRow("llave_vehi") = e("llave_vehi")
                        nRow("placa_vehi") = e("placa_vehi")
                        nRow("inven_vehi") = e("inven_vehi")
                        nRow("marca_vehi") = e("marca_vehi")
                        nRow("clave_secr") = e("clave_secr")
                        nRow("clave_secr") = s
                        nRow("clave_depe") = d
                        nRow("linea_vehi") = e("linea_vehi")
                        nRow("noeco_vehi") = e("noeco_vehi")
                        nRow("model_vehi") = e("model_vehi")
                        dtVehiculosTotales.Rows.Add(nRow)
                    Next
                Next

                'Inserta Secretarias
                Dim con2 As New Class1
                Using bulkCopy As New SqlBulkCopy(con2.conectar())
                    bulkCopy.DestinationTableName = "Tbl_Vehiculo"
                    bulkCopy.WriteToServer(dtVehiculosTotales)
                End Using
            End If
        End Using
    End Sub

    Public Function ObtieneDependencias(clave_secr As String) As DataTable
        Dim service = New WSServicio.Service
        Dim dt = service.ListaSecretariasDependencia("1", "Aq333c_jfnrRRQ!").Tables(0).Select("clave_secr = " & clave_secr).CopyToDataTable()
        Dim dataView As New DataView(dt) '.AsEnumerable().Select(Of  Select("clave_secr, nombr_secr").Distinct().CopyToDataTable()
        Return dataView.ToTable(True, "clave_depe", "nombr_depe")
    End Function

    Public Sub ListarEmpleados(secretaria As String, dependencia As String)
        Dim service = New WSServicio.Service
        Dim dtEmpleados = service.ListaEmpleados(secretaria, dependencia).Tables(0)

        If dtEmpleados.Rows.Count > 0 Then

            'Elimina empleados
            Dim con1 As New Class1
            Using data As New DB(con1.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@clave_secr", secretaria),
                New SqlParameter("@clave_depe", dependencia)
            }

                Dim dsActual = data.ObtieneDatos("ObtieneEmpleadosTotales", params).Tables(0)

                If dsActual.Rows.Count <> dtEmpleados.Rows.Count Then
                    Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@clave_secr", secretaria),
                New SqlParameter("@clave_depe", dependencia)
            }
                    data.EjecutaCommand("EliminaEmpleados", params2)

                    For Each r In dtEmpleados.Rows
                        Dim p() As SqlParameter = New SqlParameter() _
                            {
                            New SqlParameter("@clave_secr", secretaria),
                            New SqlParameter("@clave_depe", dependencia),
                            New SqlParameter("@clave_empl", r("clave_empl")),
                            New SqlParameter("@nombr_empl", r("nombr_empl")),
                            New SqlParameter("@nombr_cate", r("nombr_cate")),
                            New SqlParameter("@sueld_emp", r("sueld_emp"))
                            }
                        data.EjecutaCommand("InsertaEmpleados", p)

                    Next
                End If

            End Using

        End If
    End Sub

    Public Sub ObtienePuestos(secretaria As String, dependencia As String)
        Dim service = New WSServicio.Service
        Dim dtPuesto = service.ConsXPuestos(secretaria, dependencia).Tables(0)

        If dtPuesto.Rows.Count > 0 Then

            'Elimina vehiculos
            Dim con1 As New Class1
            Using data As New DB(con1.conectar())
                Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@clave_sec", secretaria),
                New SqlParameter("@clave_depe", dependencia)
            }

                Dim dsActual = data.ObtieneDatos("ObtienePuestos", params).Tables(0)

                If dsActual.Rows.Count <> dtPuesto.Rows.Count Then
                    Dim params2() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@clave_sec", secretaria),
                New SqlParameter("@clave_depe", dependencia)
            }
                    data.EjecutaCommand("EliminaPuestos", params2)

                    For Each r In dtPuesto.Rows
                        Dim p() As SqlParameter = New SqlParameter() _
                            {
                            New SqlParameter("@clave_sec", secretaria),
                            New SqlParameter("@clave_depe", dependencia),
                            New SqlParameter("@nombre_puesto", r("nombr_cate")),
                            New SqlParameter("@clave_puesto", 0),
                            New SqlParameter("@cantidad", r("Column1"))
                            }
                        data.EjecutaCommand("InsertaPuestos", p)

                    Next
                End If

            End Using

        End If
    End Sub

    Public Function ObtieneVehiculos(clave_secr As String, clave_depe As String, clave_admon As String, anio As String, linea As String, tarea As String, mes As Integer) As DataTable

        Dim con As New Class1
        Using data As New DB(con.conectar())
            Dim params() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@IdSecretaria", clave_secr),
                New SqlParameter("@IdDireccion", clave_depe),
                New SqlParameter("@IdAdmon", clave_admon),
                New SqlParameter("@IdAnio", anio),
                New SqlParameter("@IdLinea", linea),
                New SqlParameter("@IdSubActividad", tarea),
                New SqlParameter("@Mes", mes)
            }

            Return data.ObtieneDatos("ObtieneVehiculos", params).Tables(0)

        End Using
    End Function

    Public Function ObtieneArticulos(concepto_gasto As String) As DataSet
        'Dim service = New WsInfoFin.Service
        'Dim ds = service.ConsultaArticulos("fg_3311$.2212v?​", 2532, 2)

        Return New DataSet()
    End Function

End Class
