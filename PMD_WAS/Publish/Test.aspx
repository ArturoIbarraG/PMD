<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.Test"  MasterPageFile="~/MasterGlobal.master"  Codebehind="Test.aspx.vb" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
     <div>
            <p>Fecha inicio</p>
            <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
            <br /><br />
            <p>Fecha fin</p>
            <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
            <br /><br />
            <p>Lugar</p>
            <asp:TextBox ID="txtLugar" runat="server"></asp:TextBox>
            <br /><br />
            <p>Mensaje</p>
            <asp:TextBox ID="txtMensaje" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar correo" OnClick="btnEnviar_Click" />
        </div>

</asp:Content>
