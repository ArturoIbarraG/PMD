<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.AltaDeAdministradores" Codebehind="AltaDeAdministradores.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Css/StyleTextBoxs.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server" >
<div style=" font-size:15px;">
    <h2 style="border-bottom: 5px inset rgb(163, 156, 156); margin: 3% 0% 3% 0%">
        Dar de alta a empleados para el uso del sistema</h2>
    Empleados: <asp:DropDownList ID="DropUsuarios" runat="server" CssClass="mac"  Width="450px">
    </asp:DropDownList><br /><br />
    
    Contraseña que se le asignará:<asp:TextBox ID="txtPassword" runat="server" CssClass="mac" placeholder="Password" Width="230px" TextMode="Password"></asp:TextBox>
      Confirme  la contraseña:<asp:TextBox ID="txtConfirmaP" runat="server" CssClass="mac" placeholder="Password" Width="230px" TextMode="Password"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ErrorMessage="Contraseña No Coincide" ControlToCompare="txtPassword" 
        ControlToValidate="txtConfirmaP" ForeColor="#990000"></asp:CompareValidator>
<br /><br />
    Nivel De Seguridad:<asp:DropDownList ID="DropSeguridad"  CssClass="mac" Width="247px" runat="server">
    </asp:DropDownList><br /><br />
    
    <asp:Button runat="server" ID="Guardar" Text="Guardar Datos" style=" margin:2% 0% 2% 30%; width:145px; height:25px;"/>

    </div>
</asp:Content>
