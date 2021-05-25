Imports System.Data
Partial Class Login3
    Inherits System.Web.UI.Page


    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click



   
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


            stry = "SELECT  clave_empl,pass_empl from Usuarios WHERE activo =1 and clave_empl = '" & Me.Txt_Usuario.Text & "' and pass_empl = '" & Me.Txt_Contraseña.Text & "'"

            'stry = "SELECT clave_empl,pass_empl,clave_secr,privilegio,seguridad from Usuarios " & _
            '       " WHERE activo = 1 and clave_empl = '" & Me.Txt_Usuario.Text & "'"
            tabla = conexion.sqlcon(stry)
            If tabla.Rows.Count < 1 Then

                Me.Label3.Text = "Verifique el número de nómina y contraseña"

                Limpiar()
                Visibles()
            Else


                Session("clave_empl") = tabla.Rows(0)("clave_empl").ToString

                If Session("clave_empl") <> 1234 Then

                    stry = "SELECT clave_secr,privilegio,seguridad from Usuarios " &
                   " WHERE activo = 1 and clave_empl = 1234"
                    tabla = conexion.sqlcon(stry)
                    If tabla.Rows.Count < 1 Then

                        Me.Label3.Text = "Verifique el número de nómina y contraseña"
                        Limpiar()
                        Visibles()
                    Else
                        Session("paso") = "1"
                        Session("privilegio") = tabla.Rows(0)("privilegio").ToString
                        Session("id_secr") = tabla.Rows(0)("clave_secr").ToString
                        Session("clave_empl") = Me.Txt_Usuario.Text
                        Session("UsuarioAdmin") = 1234
                        Session("seguridad") = tabla.Rows(0)("seguridad").ToString

                        '----Comente para pruebas--------------------------

                        stry = "insert into bitacora_sesion values('" & Me.Txt_Usuario.Text & "', GETDATE())"
                        resultado = conexion.sqlcambios(stry)
                        '-----------------------------------------------------------
                        Response.Redirect("Bienvenido.aspx")

                    End If


                  
                Else

                    Me.Label3.Text = "Verifique el numero de nómina y contraseña"
                    Limpiar()
                    Visibles()


                End If







            End If


        End If

    End Sub


    Function ValidarLogin() As Boolean

        Invisibles()
        ValidarLogin = True

        If Me.Txt_Usuario.Text = "" Then
            Me.Label1.Visible = True
            ValidarLogin = False

        End If

        If Me.Txt_Contraseña.Text = "" Then
            Me.Label2.Visible = True
            ValidarLogin = False

        End If


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


   
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'If Session("paso") = "1" Then
        '    Session("paso") = "0"
        'Else
        '    Response.Redirect("~/Password.aspx")
        'End If


    End Sub
End Class
