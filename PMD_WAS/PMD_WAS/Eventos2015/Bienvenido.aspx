<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMDApplication.Bienvenido" Codebehind="Bienvenido.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <link href="estilo_bienvenido.css" rel="stylesheet" type="text/css" />
    <link href="menu.css" rel="stylesheet" type="text/css" />
    <script src="jquery.js" type="text/javascript"></script>
    <script src="menu.js" type="text/javascript"></script>
    <script src="Alertas/mootools.js" type="text/javascript"></script>
    <script src="Alertas/sexyalertbox.v1.1.js" type="text/javascript"></script>
    <link href="Alertas/sexyalertbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divprincipal" style="margin: 5% 10% 5% 10%">
        <div id="titulomaster">
            <img src="images/eventos2.png" />
            
        </div>
        <div id="divmenuprincipal">
            <div id="menu" style="background-color: #084B8A; margin-bottom: 5%">
                <ul class="menu">
                    <li><a href="Bienvenido.aspx"><span>INICIO</span></a></li>


                   <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 3 Then%>
                     <li><a href="#" class="parent"><span>EVENTO</span></a>
                     <div><ul>
                         <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 3 Then%>
                    <li><a href="Alta.aspx"><span>ALTA</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 3 Then%>
                    <li><a href="Cambios.aspx"><span>CAMBIOS</span></a></li>
                    <% End If%>
					 </ul></div>
                   </li>
                 <% End If%>

                    <% If Session("seguridad") = 1 Or Session("seguridad") = 6 Then%>
                    <li><a href="Validacion.aspx"><span>VALIDA EVENTO</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 4 Then%>
                    <li><a href="Requerimientos.aspx"><span>REQUERIMIENTO</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 5 Then%>
                    <li><a href="Ficha.aspx"><span>FICHA</span></a></li>
                    <% End If%>
               
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 7 Then%>
                     <li><a href="#" class="parent"><span>CONSULTAS</span></a>
                      
                     <div><ul>

                       <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 7 Then%>
                        <li><a href="ConsultaEstatus.aspx"><span>CONSULTA ESTATUS</span></a></li>
                        <% End If%>

				        <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 7 Then%>
                        <li><a href="Consulta.aspx"><span>CONSULTA MAPA</span></a></li>
                        <% End If%>

				<%--        <% If Session("seguridad") = 1 Or Session("seguridad") = 3 Or Session("seguridad") = 8 Or Session("seguridad") = 9 Then%>
                        <li><a href="ConsultaEvento.aspx"><span>CONSULTA EVENTO</span></a></li>
                        <% End If%>--%>
					 </ul></div>
                   </li>
                 <% End If%>
                 
                 <%--  <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 8 Then%>
                    <li><a href="Imagen.aspx"><span>DISEÑO</span></a></li>
                    <% End If%>
--%>
                    <li><a href="Evidencias.aspx"><span>EVIDENCIAS</span></a></li>
                    <li><a href="Salir.aspx"><span>SALIR</span></a></li>
                    <%-- <li><a href="Exportar.aspx"><span>EXPORTAR</span></a>--%>
                </ul>
            </div>
        </div>
        <div id="divRequerimiento">
          <%--  <img src="images/logo-inicio.jpg" width="100%" height="100%" />--%>
            <img src="images/logo_armas.png"  width="100%" height="100%"/>
            <div id="divValidarEvento">
            </div>
        </div>
    </div>
    <a href="http://apycom.com/" style="color: White">.</a>
    <div id="divusuarioVal">
        <asp:Label ID="Label4" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaVal">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    </form>
</body>
</html>
