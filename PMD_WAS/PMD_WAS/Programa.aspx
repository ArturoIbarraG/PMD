<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.Programa" Codebehind="Programa.aspx.vb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--ESTOS 2 SCRIPTS SON PARA HACER LA BUSQUEDA EN EL GRID YA CARGADO, Y EN EL GRID SOLO SE MANDA LLAMAR LA CLASE--%>
    <script src="Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/JScriptBusquedaEnGrid.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin: -2% 20% 0% 20%; text-align: center;">
        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Reporte Por Programa"></asp:Label>
    </div>
    <div id="DivAdmon" style="margin: 2% 10% 1% 20%; float: left; text-align: left; color: Green;
        font-size: medium; width: 100%">
        Administración:
        <asp:DropDownList ID="DropAdmon" runat="server" Width="300px" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" Width="175px" />
    </div>
    <div id="DivRadio1" style="margin: 0% 5% 0% 28%; text-align: left; float: left; font-size: 18px;
        width: 123px;">
        <asp:RadioButtonList runat="server" RepeatDirection="Vertical" ID="Radio1" AutoPostBack="True">
            <asp:ListItem Selected="True">Programas</asp:ListItem>
            <asp:ListItem>Secretarías</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div id="DivRadio2" style="margin: 0% 20% 0% 20%; text-align: left; font-size: 18px;">
        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="Radio2" AutoPostBack="True">
            <asp:ListItem Selected="True">Anual</asp:ListItem>
            <asp:ListItem>Mensual</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div id="DivDrop" style="text-align: left; font-size: 18px; margin-left: 201px; margin-right: 190px;
        border-bottom: 5px inset rgb(163, 156, 156);">
        Año:
        <asp:DropDownList ID="DropAño" runat="server" Width="100px">
        </asp:DropDownList>
        <asp:Label ID="LblMes" runat="server" Text="Label">Mes:</asp:Label>
        <asp:DropDownList ID="DropMes" runat="server" Width="150px">
            <asp:ListItem Value="1">ENERO</asp:ListItem>
            <asp:ListItem Value="2">FEBRERO</asp:ListItem>
            <asp:ListItem Value="3">MARZO</asp:ListItem>
            <asp:ListItem Value="4">ABRIL</asp:ListItem>
            <asp:ListItem Value="5">MAYO</asp:ListItem>
            <asp:ListItem Value="6">JUNIO</asp:ListItem>
            <asp:ListItem Value="7">JULIO</asp:ListItem>
            <asp:ListItem Value="8">AGOSTO</asp:ListItem>
            <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
            <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
            <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
            <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:UpdatePanel ID="UpdatePanelEje" runat="server" style="margin: 3% 0% 0% 15%;">
        <ContentTemplate>
            <div id="DivEje" style="margin: 0px 20px 0px 102px; text-align: left; font-size: medium;">
                Programa:
                <asp:DropDownList ID="DropEje" runat="server" Width="400px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CheckBox ID="CheckEje" runat="server" AutoPostBack="True" Text="TODAS" />
            </div>
            <div id="DivObj" style="margin: 0px 20px 0px 57px; text-align: left; font-size: medium;">
                Componente:
                <asp:DropDownList ID="DropObj" runat="server" Width="400px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CheckBox ID="CheckObj" runat="server" AutoPostBack="True" Text="TODAS" />
            </div>
            <div id="DivEstrategia" style="margin: 0px 20px 0px 54px; text-align: left; font-size: medium;">
                Actividad:
                <asp:DropDownList ID="DropEstr" runat="server" Width="400px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CheckBox ID="CheckEstr" runat="server" AutoPostBack="True" Text="TODAS" />
            </div>
            <div id="DivLínea" style="margin: 0px 20px 0px 85px; text-align: left; font-size: medium;">
                <%--Línea:--%>
                <asp:DropDownList ID="DropLinea" runat="server" Width="500px" AutoPostBack="True" Visible="false">
                </asp:DropDownList>
                <asp:CheckBox ID="CheckLinea" runat="server" AutoPostBack="True" Text="TODAS" Visible="false"/>
            </div>
            <div id="DivSublínea" style="margin: 0px 20px 0px 60px; text-align: left; font-size: medium;">
                <%--SubLínea:--%>
                <asp:DropDownList ID="DropSublinea" runat="server" Width="500px" AutoPostBack="True" Visible="false">
                </asp:DropDownList>
                <asp:CheckBox ID="CheckSubL" runat="server" AutoPostBack="True" Text="TODAS" Visible="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdateSecretaria" runat="server" style="margin: 3% 0% 0% 15%;">
        <ContentTemplate>
            <div id="DivSecr" style="margin: 0px 20px 20px 57px; text-align: left; font-size: medium;">
                Secretaría:
                <asp:DropDownList ID="DropSecr" runat="server" Width="500px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CheckBox ID="CheckSecr" runat="server" AutoPostBack="True" Text="TODAS" />
            </div>
            <div id="DivDir" style="margin: 0px 20px 20px 57px; text-align: left; font-size: medium;">
                Dirección:
                <asp:DropDownList ID="DropDir" runat="server" Width="500px">
                </asp:DropDownList>
                <asp:CheckBox ID="CheckDir" runat="server" AutoPostBack="True" Text="TODAS" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <h2 style="border-bottom: 5px inset rgb(163, 156, 156); color:#0A6A6F;">
        Líneas Programadas</h2>
        <div id="Div2" style="text-align: center; margin-top:2%;"><asp:Label ID="lblLista" runat="server" Text="Buscar En La Lista:" Style="font-family: Comic Sans MS;
            font-weight: bold; font-size: 15px;"></asp:Label>
        <input id="texto" onclick="return texto_onclick()" type="text" /></div>
    <div id="divg" style="text-align: center; margin: 1% 0% 2% 0%; overflow:auto; height:350px" >
        
        <asp:GridView ID="GridView1" class="filtrar" runat="server" CellPadding="4" Height="95px"
            Width="1000px" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#0A6A6F" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
            <EmptyDataTemplate>
                <table width="1000px">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblSD" runat="server" style="color:rgb(9, 84, 114); font-weight:bold;" Text="Sin Datos" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <h2 style="border-bottom: 5px inset rgb(163, 156, 156); color:#0A6A6F;">
        Líneas Por solicitud</h2>
        <div id="DivB" style="text-align: center; margin-top:2%;"> <asp:Label ID="Label1" runat="server" Text="Buscar En La Lista:" Style="font-family: Comic Sans MS;
            font-weight: bold; font-size: 15px;"></asp:Label>
        <input id="texto2" onclick="return texto_onclick()" type="text" /></div>
    <div id="div1" style="text-align: center; margin: 1% 0% 2% 0%; overflow:auto; height:350px" >
       
        <asp:GridView ID="GridView2" class="filtrar2" runat="server" CellPadding="4" Height="95px"
            Width="1000px" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px">
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#0A6A6F" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
            <EmptyDataTemplate>
                <table width="1000px">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblSD" runat="server" style="color:rgb(9, 84, 114); font-weight:bold;" Text="Sin Datos" Font-Size="Large"></asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
