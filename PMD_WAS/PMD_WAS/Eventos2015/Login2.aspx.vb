Imports System.Data


Partial Class Login2
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click


        If Not IsPostBack = False Then


            Dim stry As String
            Dim tabla As DataTable
            Dim resultado As Integer

            If ValidarLogin() = False Then
                Me.Label3.Text = "* Campo Obligatorio"
                Me.Label3.Visible = True

                Exit Sub
            End If
            Session("paso") = "0"
            Session("seguridad") = "0"



            stry = "SELECT clave_empl,pass_empl,clave_secr,privilegio,seguridad from Usuarios " &
                   " WHERE activo = 1 and clave_empl = '" & Me.Txt_Usuario.Text & "' and pass_empl='" & Me.Txt_Contraseña.Text & "'"

            tabla = conexion.sqlcon(stry)
            If tabla.Rows.Count < 1 Then
                'MsgBox("Verifique su usuario y password")
                Me.Label3.Text = "Verifique usuario y contraseña"
                Limpiar()
                Visibles()
            Else


                'Items.Add(tabla.Rows(i)("producto").ToString())
                Session("paso") = "1"
                Session("privilegio") = tabla.Rows(0)("privilegio")
                Session("id_secr") = tabla.Rows(0)("clave_secr")
                Session("clave_empl") = tabla.Rows(0)("clave_empl")
                Session("pass_empl") = tabla.Rows(0)("pass_empl")
                'Session("seguridad") = "1"
                Session("seguridad") = tabla.Rows(0)("seguridad")


                '------Comente para pruebas------------------------
                stry = "insert into bitacora_sesion values('" & Me.Txt_Usuario.Text & "', GETDATE())"
                resultado = conexion.sqlcambios(stry)
                '----------------------------------------------------

                Session("UsuarioAdmin") = 0

                If Session("clave_empl") <> 1234 Then

                    Response.Redirect("Bienvenido.aspx")
                Else

                    Response.Redirect("Login3.aspx")
                End If




            End If


        End If


        'Try
        '    Dim cmd As New Data.SqlClient.SqlCommand(stry, Con.conexionsql)
        '    rc = cmd.ExecuteReader()

        '    While rc.Read()
        '        Session("foliosol") = rc(0).ToString.Trim
        '        Session("passwordsol") = rc(1).ToString.Trim
        '        correcto = 1
        '    End While

        '    Con.desconectarsql()
        'Catch ex As Exception
        '    Response.Redirect("err.aspx")
        'End Try

        'If correcto = 1 Then
        '    Response.Redirect("frmseguimiento.aspx")
        'Else
        '    Me.lblmsg.Text = "*Verifique su No.Solicitud y su Contraseña"
        'End If

    End Sub



    Function ValidarLogin() As Boolean

        Invisibles()
        ValidarLogin = True

        If Me.Txt_Usuario.Text = "" Then
            Me.Label1.Visible = True
            ValidarLogin = False
            'Exit Function
        End If
        If Me.Txt_Contraseña.Text = "" Then
            Me.Label2.Visible = True
            ValidarLogin = False
            'Exit Function
        End If
        'ValidarLogin = True
    End Function


    Sub Invisibles()
        Me.Label1.Visible = False
        Me.Label2.Visible = False
        Me.Label3.Visible = False
    End Sub

    Sub Visibles()
        Me.Label1.Visible = True
        Me.Label2.Visible = True
        Me.Label3.Visible = True
    End Sub


    Sub Limpiar()
        Me.Txt_Usuario.Text = ""
        Me.Txt_Contraseña.Text = ""
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack = True Then
            Invisibles()
        End If
    End Sub
End Class



