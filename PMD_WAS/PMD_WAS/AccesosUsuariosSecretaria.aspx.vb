Imports Class1
Imports System.Data.SqlClient
Imports System.Data
Partial Class AccesosUsuariosSecretaria
    Inherits System.Web.UI.Page
    Dim conx As New Class1
    Dim x As Integer
    Dim DigitosEnlaCadena As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("paso") = "1" Then
        Else
            Response.Redirect("Password.aspx")
        End If
        If IsPostBack = False Then
            LlenarAdmon()
            CargaUsuarios()
            OpcionRadio()

        End If
    End Sub
    Public Sub LlenarAdmon()
        'LLENAR EL DROP ADMINISTRACION
        Dim sent As New System.Data.SqlClient.SqlCommand("Select Cve_admon,Nombr_admon from [PMD].Cat_Admon", conx.conectar)
        Dim exe As System.Data.SqlClient.SqlDataReader

        exe = sent.ExecuteReader
        Try
            If Not exe Is Nothing Then
                DropAdmon.DataSource = exe
                DropAdmon.DataTextField = "Nombr_Admon"
                DropAdmon.DataValueField = "Cve_Admon"
                DropAdmon.DataBind()
                exe.NextResult()
            End If
            exe.Close()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BtnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGrabar.Click
        InsertPermisos()
    End Sub
    Protected Sub RadioButtonList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioAcceso.SelectedIndexChanged
        OpcionRadio()
    End Sub
    Private Sub CargaUsuarios()
        '''''''''''''''''''''''''''''''''''''''''''''''''drop de usuarios
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select Clave_empl,Nombr_empl from [PMD].Usuarios", conx.conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader
        Try
            If Not Drsx2 Is Nothing Then
                DropUsuarios.DataSource = Drsx2
                DropUsuarios.Font.Size = 12
                DropUsuarios.DataTextField = "nombr_empl"
                DropUsuarios.DataValueField = "clave_empl"
                DropUsuarios.DataBind()
                Drsx2.NextResult()
            End If
        Finally
            Drsx2.Close()
        End Try

    End Sub
    Private Sub CargaSecr()
        ''''''''''''''''''''''''''''''''''''''''''''''''' Llena Secretarias
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select IdSecretaria,Nombr_secretaria from [PMD].Secretarias where Admon='" & Me.DropAdmon.Text & "'", conx.conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader
        Try
            If Not Drsx2 Is Nothing Then
                GridView1.DataSource = Drsx2
                GridView1.Font.Size = 12
                GridView1.DataBind()
                Drsx2.NextResult()
            End If
        Finally
            Drsx2.Close()
        End Try
        Permiso()
    End Sub
    Private Sub CargaDir()
        ''''''''''''''''''''''''''''''''''''''''''''''''' Llena Direcciones
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select IdDireccion,Nombr_direccion from [PMD].Direcciones where Admon='" & Me.DropAdmon.Text & "'", conx.conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader

        Try
            If Not Drsx2 Is Nothing Then
                GridView2.DataSource = Drsx2
                GridView2.Font.Size = 12
                GridView2.DataBind()
                Drsx2.NextResult()
            End If
        Finally
            Drsx2.Close()
        End Try
        PermisoDir()
    End Sub
    Private Sub Permiso()
        x = 0
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select Secr from [PMD].Permiso_Secr where clave_empl='" & Me.DropUsuarios.Text & "' and Admon='" & Me.DropAdmon.Text & "'", conx.conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader
        If Drsx2.Read() = True Then
            Dim LD = Drsx2(0)
            MuestraPer(LD)
        Else

        End If
    End Sub
    Private Sub PermisoDir()
        x = 0
        Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select Direcciones from [PMD].Permisos_Dir where clave_empl='" & Me.DropUsuarios.Text & "' and Admon='" & Me.DropAdmon.Text & "'", conx.conectar)
        Dim Drsx2 As System.Data.SqlClient.SqlDataReader
        Drsx2 = RsGen2.ExecuteReader
        If Drsx2.Read() = True Then
            Dim LD = Drsx2(0)
            MuestraPerDir(LD)
        End If
    End Sub
    Private Function MuestraPer(ByVal num As String) As String
        Dim result As String
        Dim numero As String
        If num = 0 Then
        Else
            numero = CType(num, String)
            Dim numDividido() As String
            numDividido = numero.ToString().Split(",")
            DigitosEnlaCadena = numDividido.Length

            result = numDividido(x)

            While DigitosEnlaCadena <> x

                If DigitosEnlaCadena = x Then
                    Return result
                Else
                    result = numDividido(x)

                    Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select IdSecretaria from [PMD].Secretarias where idSecretaria='" & result & "' and Admon='" & Me.DropAdmon.Text & "' ", conx.conectar)
                    Dim Drsx2 As System.Data.SqlClient.SqlDataReader
                    Drsx2 = RsGen2.ExecuteReader

                    If Drsx2.Read() = True Then
                        For Each row As GridViewRow In GridView1.Rows
                            If CType(row.FindControl("lblCveSecr"), Label).Text = result Then
                                CType(row.FindControl("RdbConPer"), RadioButton).Checked = "true" 'Este Radiobutton es Con Permiso por lo tanto le doy checked
                            Else
                                CType(row.FindControl("RdbSinPer"), RadioButton).Checked = "true" 'Este Radiobutton es Sin Permiso por lo tanto le doy checked 
                            End If
                        Next

                    End If
                End If
                x = x + 1
            End While
        End If
    End Function
    Private Function MuestraPerDir(ByVal num As String) As String
        Dim result As String
        Dim numero As String
        If num = 0 Then
        Else
            numero = CType(num, String)
            Dim numDividido() As String
            numDividido = numero.ToString().Split(",")
            DigitosEnlaCadena = numDividido.Length

            result = numDividido(x)

            While DigitosEnlaCadena <> x

                If DigitosEnlaCadena = x Then
                    Return result
                Else
                    result = numDividido(x)

                    Dim RsGen2 As New System.Data.SqlClient.SqlCommand("select IdDireccion from [PMD].Direcciones where idDireccion='" & result & "' and Admon='" & Me.DropAdmon.Text & "' ", conx.conectar)
                    Dim Drsx2 As System.Data.SqlClient.SqlDataReader
                    Drsx2 = RsGen2.ExecuteReader

                    If Drsx2.Read() = True Then
                        For Each row As GridViewRow In GridView2.Rows
                            If CType(row.FindControl("lblCveDir"), Label).Text = result Then
                                CType(row.FindControl("RdbConPer"), RadioButton).Checked = "true" 'Este Radiobutton es Con Permiso por lo tanto le doy checked
                            Else
                                CType(row.FindControl("RdbSinPer"), RadioButton).Checked = "true" 'Este Radiobutton es Sin Permiso por lo tanto le doy checked 
                            End If
                        Next

                    End If
                End If
                x = x + 1
            End While
        End If
    End Function
    Private Sub InsertPermisos()
        Dim x As Integer
        x = 0
        Dim Claves = ""

        Dim IdUsuario As Integer
        IdUsuario = Me.DropUsuarios.Text

        Select Case RadioAcceso.Text
            Case 1 'SECRETARIAS
                '''''''''''''''Elimino los permisos que alla podido tener ese IdUsuario en Permisos Por Direcciones
                Dim Stry5 As String
                Dim Rs5 As SqlDataReader
                Stry5 = "delete [PMD].Permisos_Dir where Clave_empl='" & IdUsuario & "' and Admon='" & Me.DropAdmon.Text & "'"
                Dim cmd5 As New Data.SqlClient.SqlCommand(Stry5, conx.conectar())
                Rs5 = cmd5.ExecuteReader()
                Rs5.Read()

                ''''''''''''''''''''''''Verifica Si Este Usuario Ya Esta EN la tabla de permisos Secretaria
                Dim Stry1 As String
                Dim Rs1 As SqlDataReader

                Stry1 = "select * from [PMD].Permiso_Secr where Clave_empl='" & IdUsuario & "' and Admon='" & Me.DropAdmon.Text & "' "

                Dim cmd1 As New Data.SqlClient.SqlCommand(Stry1, conx.conectar())
                Rs1 = cmd1.ExecuteReader()

                '''''''''''''''''''''''''''''''''''''''Si ya esta ese Usuario Solo Updatea los valores
                If Rs1.Read() = True Then
                    For Each row As GridViewRow In GridView1.Rows
                        If CType(row.FindControl("RdbConPer"), RadioButton).Checked = True Then

                            Claves &= CType(row.FindControl("lblCveSecr"), Label).Text & ","
                        End If
                    Next
                    'var1 = DirectCast(GridView1.Rows(x).FindControl("RadioButtonSecr"), RadioButtonList).Text()
                    Dim Stry3 As String
                    Dim Rs3 As SqlDataReader

                    Stry3 = "Update [PMD].Permiso_Secr set Secr='" & Claves & "',Admon='" & Me.DropAdmon.Text & "' where Clave_empl='" & IdUsuario & "'"
                    Dim cmd3 As New Data.SqlClient.SqlCommand(Stry3, conx.conectar())
                    Rs3 = cmd3.ExecuteReader()

                    Rs3.Read()
                    'x = x + 1

                Else ''''''''''''''''''''''''''''''''''''''''Si No, Inserta la clave De Usuario
                    For Each row As GridViewRow In GridView1.Rows
                        If CType(row.FindControl("RdbConPer"), RadioButton).Checked = True Then

                            Claves &= CType(row.FindControl("lblCveSecr"), Label).Text & ","
                        End If
                    Next
                    Dim Stry4 As String
                    Dim Rs4 As SqlDataReader
                    Stry4 = "insert into [PMD].Permiso_Secr(clave_empl,Secr,Admon) values('" & IdUsuario & "','" & Claves & "','" & Me.DropAdmon.Text & "')"
                    Dim cmd4 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                    Rs4 = cmd4.ExecuteReader()
                    Rs4.Read()
                End If
            Case 2 'DIRECCIONES
                '''''''''''''''Elimino los permisos que alla podido tener ese IdUsuario en Permisos Por Secretaria
                Dim Stry5 As String
                Dim Rs5 As SqlDataReader
                Stry5 = "delete [PMD].Permiso_Secr where Clave_empl='" & IdUsuario & "' and Admon='" & Me.DropAdmon.Text & "' "
                Dim cmd6 As New Data.SqlClient.SqlCommand(Stry5, conx.conectar())
                Rs5 = cmd6.ExecuteReader()
                Rs5.Read()
                '''''''''''''''Elimino los permisos que alla podido tener ese IdUsuario en Permisos Por Direcciones
                Dim Stry As String
                Dim Rs As SqlDataReader
                Stry = "delete [PMD].Permisos_Dir where Clave_empl='" & IdUsuario & "' and Admon='" & Me.DropAdmon.Text & "'"
                Dim cmd4 As New Data.SqlClient.SqlCommand(Stry, conx.conectar())
                Rs = cmd4.ExecuteReader()
                Rs.Read()

                'Verifica las claves a las que tendra acceso

                For Each row As GridViewRow In GridView2.Rows
                    If CType(row.FindControl("RdbConPer"), RadioButton).Checked = True Then
                        Claves &= CType(row.FindControl("lblCveDir"), Label).Text & ","
                    End If
                Next
                Dim Stry4 As String
                Dim Rs4 As SqlDataReader
                Stry4 = "insert into [PMD].Permisos_Dir(Clave_empl,Direcciones,Admon) values('" & IdUsuario & "','" & Claves & "','" & Me.DropAdmon.Text & "')"
                Dim cmd5 As New Data.SqlClient.SqlCommand(Stry4, conx.conectar())
                Rs4 = cmd5.ExecuteReader()
                Rs4.Read()
        End Select
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mostrar", "ProcExitoso1();", True)
    End Sub
    Protected Sub DropUsuarios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropUsuarios.SelectedIndexChanged
        Select Case RadioAcceso.Text
            Case 1 '''''''''''''''''Secretaría
                CargaSecr()
                GridView2.DataBind()
            Case 2 '''''''''''''''''Dirección
                CargaDir()
                GridView1.DataBind()
        End Select
    End Sub
    Protected Sub DropAdmon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropAdmon.SelectedIndexChanged
        OpcionRadio()
    End Sub
    Private Sub OpcionRadio()
        Select Case RadioAcceso.Text
            Case 1 '''''''''''''''''Secretaría
                CargaSecr()
                GridView2.DataBind()
            Case 2 '''''''''''''''''Dirección
                CargaDir()
                GridView1.DataBind()
        End Select
    End Sub
End Class
