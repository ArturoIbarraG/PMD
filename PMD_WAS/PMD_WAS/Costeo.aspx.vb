Imports System.Data.SqlClient
Imports Class1
Imports System.Data
Partial Class Costeo
    Inherits System.Web.UI.Page
    Dim conectar As New Class1
    Public CveSuministro As String
    Public FolioSiguiente As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LLenarDrops()
            GeneraFolio()
        End If
        Me.lblFecha.Text = DateString
    End Sub
    Public Sub LLenarDrops()

        'LLENA tipo de Programa
        Dim sent As New System.Data.SqlClient.SqlCommand("Select Cve_progr,Programa from CosteoTipoPrograma", conectar.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropTipoProg.DataSource = exe
                DropTipoProg.DataTextField = "Programa"
                DropTipoProg.DataValueField = "Cve_progr"
                DropTipoProg.DataBind()
                exe.NextResult()
            End If
            exe.Close()
        Catch ex As Exception

        End Try

        'LLENA colonias
        Colonia()

        'LLENA tipo de accion (tipos de trabajo)
        'Dim sent4 As New System.Data.Odbc.OdbcCommand("Select clave_titr,nombr_titr from iso9002@pdc3:CALLTIPOTRABAJO", conectar.conectarNomina)
        'Dim exe4 As System.Data.Odbc.OdbcDataReader

        'exe4 = sent4.ExecuteReader
        'Try
        '    If Not exe Is Nothing Then
        '        DropTipoAccion.DataSource = exe4
        '        DropTipoAccion.DataTextField = "nombr_titr"
        '        DropTipoAccion.DataValueField = "nombr_titr"
        '        DropTipoAccion.DataBind()
        '        exe4.NextResult()
        '    End If
        '    exe4.Close()
        'Catch ex As Exception

        'End Try



        Dim sent2 As New System.Data.SqlClient.SqlCommand("Select  idlinea,rtrim(actividad) actividad from CosteoPMD WHERE CLAVE_DEPE=245", conectar.conectar)
        Dim exe2 As System.Data.SqlClient.SqlDataReader

        exe2 = sent2.ExecuteReader
        Try
            If Not exe2 Is Nothing Then
                DropTipoAccion.DataSource = exe2
                DropTipoAccion.DataTextField = "actividad"
                DropTipoAccion.DataValueField = "idlinea"
                DropTipoAccion.DataBind()
                exe2.NextResult()
            End If
            exe2.Close()
        Catch ex2 As Exception

        End Try




        LLenarSuministros()

    End Sub
    Public Sub Colonia()

        Dim sent2 As New System.Data.SqlClient.SqlCommand("Select CveColonia,NombrColonia from CosteoColonia", conectar.conectar)
        Dim exe2 As System.Data.SqlClient.SqlDataReader

        exe2 = sent2.ExecuteReader
        Try
            If Not exe2 Is Nothing Then
                DropColonia.DataSource = exe2
                DropColonia.DataTextField = "NombrColonia"
                DropColonia.DataValueField = "NombrColonia"
                DropColonia.DataBind()
                exe2.NextResult()
            End If
            exe2.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub UnidadAdmin()

        Dim sent3 As New System.Data.SqlClient.SqlCommand("Select Cve_dep,Unidad_admin,sueldo_neto from CosteoSueldoXunidad where Cve_programa=" & DropTipoProg.SelectedValue & "", conectar.conectar)
        Dim exe3 As System.Data.SqlClient.SqlDataReader

        exe3 = sent3.ExecuteReader
        Try
            If Not exe3 Is Nothing Then
                DropUnidadAdmin.DataSource = exe3
                DropUnidadAdmin.DataTextField = "Unidad_admin"
                DropUnidadAdmin.DataValueField = "Unidad_admin"
                DropUnidadAdmin.DataBind()
                exe3.NextResult()
            End If
            exe3.Close()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtNumEmpl_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumEmpl.TextChanged
        CostoNomina()
    End Sub
    Public Sub LLenarSuministros()
        Dim stry As String
        stry = "select clave_arti as Clave,nombr_arti as Articulo from dbfinan@informatica:comarticulos"
        Dim sent As New System.Data.Odbc.OdbcCommand(stry, conectar.conectarNomina)
        Dim exe As System.Data.Odbc.OdbcDataReader
        exe = sent.ExecuteReader

        Try
            If Not exe Is Nothing Then
                GridView2.DataSource = exe
                GridView2.Font.Size = 8
                GridView2.DataBind()
                exe.NextResult()
            End If
        Catch ex As Exception
            exe.Close()
        End Try
    End Sub
    Protected Sub GridView2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.SelectedIndexChanged
        Dim row As GridViewRow = GridView2.SelectedRow

        Me.txtCveSuministro.Text = row.Cells(1).Text
        Me.txtSuministro.Text = row.Cells(2).Text

        PrecioDeSuministros()
    End Sub
    Public Sub PrecioDeSuministros()

        Dim stry As String
        stry = "select preci_arti from dbfinan@informatica:comarticulos where clave_arti='" & Me.txtCveSuministro.Text & "'"
        Dim sent As New System.Data.Odbc.OdbcCommand(stry, conectar.conectarNomina)
        Dim exe As System.Data.Odbc.OdbcDataReader
        exe = sent.ExecuteReader

        exe.Read()

        Try
            If Not exe Is Nothing Then

                Me.txtCostoUnitario.Text = exe(0)
                Me.txtcostoTotal.Text = (Me.txtCantidadSuministro.Text * Me.txtCostoUnitario.Text)

                'Me.txtCostoUnitario.Text = FormatCurrency(Me.txtCostoUnitario.Text, 2)
                ' Me.txtcostoTotal.Text = FormatCurrency(Me.txtcostoTotal.Text, 2)
            End If
        Catch ex As Exception
            exe.Close()
        End Try
    End Sub
    Protected Sub txtCantidadSuministro_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCantidadSuministro.TextChanged
        PrecioDeSuministros()
    End Sub
    Protected Sub DropUnidadAdmin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropUnidadAdmin.SelectedIndexChanged
        CostoNomina()
    End Sub
    Public Sub CostoNomina()
        'Dim Sent2 As New System.Data.SqlClient.SqlCommand("select Sueldo_neto,nombr_secr from CosteoSueldoXUnidad where Unidad_admin='" & Me.DropUnidadAdmin.SelectedValue & "'", conectar.conectar)
        'Dim exe2 As System.Data.SqlClient.SqlDataReader
        'exe2 = Sent2.ExecuteReader
        'exe2.Read()

        'txtCostoNomina.Text = Me.txtNumEmpl.Text * exe2(0)
        'Me.lblSecretaria.Text = exe2(1)

        ''txtCostoNomina.Text = FormatCurrency(txtCostoNomina.Text, 2)
        'exe2.Close()
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1
                Dim ID = GridView1.Rows(i).Cells(0).Text
                Dim DESCRIPCIÓN = GridView1.Rows(i).Cells(1).Text
                Dim CANTIDAD = GridView1.Rows(i).Cells(2).Text
                Dim COSTO = GridView1.Rows(i).Cells(3).Text
                Dim TOTAL = GridView1.Rows(i).Cells(4).Text

                Dim stry As String
                Dim commd As System.Data.SqlClient.SqlCommand
                stry = "insert into Costeo_det (FolioFicha,IdSum,DescrSum,Costo,Cantidad) values ('" & Trim(Me.lblFolio.Text) & "','" & ID & "','" & DESCRIPCIÓN & "','" & COSTO & "','" & CANTIDAD & "')"
                commd = New SqlCommand(stry, conectar.conectar)
                commd.ExecuteNonQuery()
            Next

            Dim Stry4 As String
            Dim Rs4 As SqlDataReader

            Stry4 = "insert into Costeo values ('" & Trim(Me.lblFolio.Text) & "','" & Me.lblFecha.Text & "','" & Trim(Me.DropTipoProg.Text) & "','" & Trim(Me.lblSecretaria.Text) & "','" & Trim(Me.DropUnidadAdmin.SelectedValue) & "','" & Trim(Me.DropColonia.SelectedValue) & "','" & Trim(Me.rdbPeriodo.SelectedValue) & "','" & Trim(Me.DropTipoAccion.SelectedValue) & "','" & Me.txthoras.Text & "','" & Me.txtvehiculos.Text & "','" & Me.txtNumEmpl.Text & "','" & Me.txtCostoNomina.Text & "','" & Me.TxtImporte.Text & "','" & Trim(Me.rdbTipo.SelectedValue) & "','" & Trim(Me.txtcomentarios.Text) & "')"
            Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conectar.conectar())
            Rs4 = cmd4.ExecuteReader()

            Rs4.Read()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)

            limpiar()
            GeneraFolio()

        End If
    End Sub
    Public Sub limpiar()
        txtcomentarios.Text = ""
        Me.GridView1.DataBind()

        txtCveSuministro.Text = ""
        txtSuministro.Text = ""
        txtCostoUnitario.Text = ""
        txtCantidadSuministro.Text = "1"
        txtcostoTotal.Text = ""
        TxtImporte.Text = ""

    End Sub
    Public Sub GeneraFolio()
        Dim stry2 As String
        Dim Rs1 As SqlDataReader
        Dim Com2 As New SqlCommand

        stry2 = ("select max(FolioFicha) as Maximo from Costeo")
        Dim cmd2 As New Data.SqlClient.SqlCommand(stry2, conectar.conectar())
        Rs1 = cmd2.ExecuteReader()

        If Rs1.Read() Then
            If IsDBNull(Rs1("Maximo")) = False Then
                FolioSiguiente = Rs1("Maximo") + 1
            Else
                FolioSiguiente = 1
            End If
        Else
            FolioSiguiente = 1
        End If
        Me.lblFolio.Text = FolioSiguiente
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If txtcostoTotal.Text = "" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "alert('Seleccione un articulo de la lista');", True)
        Else
            Dim ds As New DataSet
            Dim dt As DataTable
            Dim dr As DataRow
            dt = New DataTable
            dt.Columns.Add("ID")
            dt.Columns.Add("DESCRIPCIÓN")
            dt.Columns.Add("CANTIDAD")
            dt.Columns.Add("COSTO")
            dt.Columns.Add("TOTAL")

            If GridView1.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = GridView1.Rows(i).Cells(0).Text
                    dr("DESCRIPCIÓN") = GridView1.Rows(i).Cells(1).Text
                    dr("CANTIDAD") = GridView1.Rows(i).Cells(2).Text
                    dr("COSTO") = GridView1.Rows(i).Cells(3).Text
                    dr("TOTAL") = GridView1.Rows(i).Cells(4).Text
                    dt.Rows.Add(dr)
                Next
            End If
            dr = dt.NewRow
            dr("ID") = txtCveSuministro.Text
            dr("DESCRIPCIÓN") = txtSuministro.Text
            dr("CANTIDAD") = txtCantidadSuministro.Text
            dr("COSTO") = txtCostoUnitario.Text
            dr("TOTAL") = txtcostoTotal.Text
            dt.Rows.Add(dr)
            ds.Tables.Add(dt)

            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()

            Dim VentaTotal As Decimal


            'Este Recorre la columna del grid Total de Cada Articulo para ponerlo en el importe
            If GridView1.Rows.Count > 0 Then
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    Dim importe = GridView1.Rows(i).Cells(4).Text
                    VentaTotal = VentaTotal + importe
                    Me.TxtImporte.Text = VentaTotal

                    'Me.TxtImporte.Text = FormatCurrency(Me.TxtImporte.Text, 2)
                Next
            End If
        End If
    End Sub
    Protected Sub DropTipoProg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropTipoProg.SelectedIndexChanged

        'LLENA Unidad Administrativa
        UnidadAdmin()

        CostoNomina()
    End Sub
End Class
