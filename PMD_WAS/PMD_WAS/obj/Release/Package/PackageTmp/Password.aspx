<%@ Page Title="Iniciar Sesión" Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.Password" CodeBehind="Password.aspx.vb" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Iniciar sesión</title>

    <link href="/PlaneacionFinanciera/Content/bootstrap.min.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width" />
    <link href="/PlaneacionFinanciera/imagenes/favicon.ico" rel="icon">
    <script src="/PlaneacionFinanciera/Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>

    <style type="text/css">
        h2 {
            margin-bottom: 0px;
        }

        ::-webkit-input-placeholder { /* Edge */
            color: #BBB;
        }

        :-ms-input-placeholder { /* Internet Explorer 10-11 */
            color: #BBB;
        }

        ::placeholder {
            color: #BBB;
        }



        .bodyLogin {
            margin: 0px auto;
            position: relative;
            border: 1px solid #AAA;
            background-color: #FFF;
            max-width: 90%;
            text-align: center;
            width: 450px;
            margin-top: 4%;
            border-radius: 8px;
            padding: 15px;
        }

        #loginForm {
            border: none !important;
        }

        #ForgotPasswordHyperLink {
            COLOR: BLUE;
            FONT-SIZE: 15PX;
            PADDING-TOP: 15PX;
            DISPLAY: INLINE-BLOCK;
            FONT-WEIGHT: 600;
        }

        #RememberMe {
            width: 14px;
            top: 2px;
            position: relative;
        }

            #RememberMe + label {
                font-size: 13px;
            }

        body {
            background-repeat: no-repeat;
            background: url('/imagenes/background_login.jpg') no-repeat !important;
            background-size: cover !important;
        }

        .bodyLogin img {
            width: 140px;
        }

        .layer {
            background-color: rgba(0, 0, 0, 0.8);
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
        }

        .logoPrincipal {
            width: 100px !important;
            float: left;
            margin-top: 15px;
        }

        .logoSecundario {
            float: right;
        }
    </style>

</head>
<body>

    <form id="Form1" runat="server" autocomplete="off">
        <div class="layer"></div>
        <div class="bodyLogin">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-12 text-center">
                        <asp:Image ID="imgLogo" runat="server" CssClass="logoPrincipal" ImageUrl="~/images/logo.png?v=1" />
                        <asp:Image ID="Image1" runat="server" CssClass="logoSecundario" ImageUrl="~/images/san_nicolas.png?v=1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center">
                        <h2 class="simple">Iniciar sesión</h2>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <section id="loginForm">
                            <div class="form-horizontal">
                                <hr />
                                <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                    <p class="text-danger">
                                        <asp:Literal runat="server" ID="FailureText" />
                                    </p>
                                </asp:PlaceHolder>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" TabIndex="0" placeholder="Usuario" onkeypress="javascript:return solonumeros(event)" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox1"
                                            CssClass="text-danger" ErrorMessage="El usuario es obligatorio." />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="TextBox2" TextMode="Password" CssClass="form-control" placeholder="Contraseña" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox2" CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-12">
                                        <asp:Button runat="server" ID="Button1" Text="Iniciar sesión" CssClass="btn btn-secondary" />
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>


                </div>
            </div>
        </div>

        <script type="text/javascript">
            $('input[id$="Email"]').focus();
            function solonumeros(e) {

                var key;

                if (window.event) // IE
                {
                    key = e.keyCode;
                }
                else if (e.which) // Netscape/Firefox/Opera
                {
                    key = e.which;
                }
                //46(.) Y 47 (/)
                if (key < 48 || key > 57) {
                    return false;
                }

                return true;
            }
        </script>
    </form>

</body>
</html>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style4
        {
            width: 319px;
        }
    </style>
    <script type="text/javascript">
        function Proceso() {
            alert("Usuario o Contraseña Incorrecta")
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 style="margin-top: 5%; font-size: 20px">
        INICIAR SESIÓN</h2>
    <p style="font-size: 18px">
        Especifique su nombre de usuario y contraseña.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Registrarse </asp:HyperLink>
        si no tiene una cuenta.
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" BorderColor="#CCCCCC"
            Font-Bold="True" Font-Size="13pt" ForeColor="Red" />
    </p>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Usuario:"></asp:Label><br />
        <asp:TextBox ID="TextBox1" runat="server" CssClass="passwordEntry"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
            Display="Dynamic" ErrorMessage="Usuario  Obligatorio" Font-Bold="True" Font-Size="14pt"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Contraseña:"></asp:Label><br />
        <asp:TextBox ID="TextBox2" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
            Display="Dynamic" ErrorMessage="Contraseña  Obligatoria" Font-Bold="True" Font-Size="14pt"
            ForeColor="Red">*</asp:RequiredFieldValidator><br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Iniciar Sesión" Width="100px" />
    </div>
</asp:Content>--%>
