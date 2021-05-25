<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.Login3" Codebehind="Login3.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="estilo_login_especial.css" rel="stylesheet" type="text/css" />
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
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

         function resize() {
             resizeTo(50, 500);
             focus();

         } 

       

    self.blur()

</script>


    </script>

</head>
<body >
 
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  <div id="Master_Div">
 
    <div id="Div_Pie_Imagen">
        
    <div id="Div_Pie_Imagen_Texto1">Eventos
    </div>
    <div id="Div_Pie_Imagen_Texto2">-
    </div>
    </div>
    <div id="Div_Logo">
   <%--<img src="images/logosannicolas.png" width="100%" height="100%"/>--%>
        <img src="images/logosannicolas_new.png" width="100%" height="100%"/>
    </div>
    <asp:UpdatePanel ID="UpdateTextbox" runat="server" height="100%" width="100%">
    <ContentTemplate>
    <div id="Div_Usuario">
    <asp:TextBox ID="Txt_Usuario" Height="80%" Width="100%" placeholder="Número de Nomina" Font-Size="Large" runat="server" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
    </div>
    <div id="Div_Contraseña">
    <asp:TextBox ID="Txt_Contraseña"  Height="80%" Width="100%" placeholder="Contraseña" Font-Size="Large" TextMode="Password" runat="server"></asp:TextBox>
    </div>
    <div id="Div_Label1"><asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
    </div>
    <div id="Div_Label2"><asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
    </div>
    <div id="Div_Label3"><asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </div>
    <div id="Div_Btn_Aceptar"> 
        <asp:Button ID="Button1" CssClass="button" runat="server" width= "55px" Text="Entrar" />
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div id="Div_Info">
    <%--<a style=" text-decoration:none; color:#000099;" href="http://www.fcfm.uanl.mx//">Conoce más acerca de nuestra facultad FCFM</a>--%>
    </div>
    <div id="Div_Pie">
    <div id="Div_Info_Sys">San Nicolás de los Garza, Nuevo León, México
    </div>
    <div id="Div_facebook">Eventos
    <%--<a style=" text-decoration:none; color:#000099;" href="http://www.facebook.com//">Síguenos en facebook</a>--%>
    </div>
    </div>
    </div>
    </form>
</body>
</html>
