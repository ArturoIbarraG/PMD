<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMDApplication.ReporteGrafico" Codebehind="ReporteGrafico.aspx.vb" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="margin: -2% 20% 0% 20%; text-align: center;">
        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Reporte Gráfico"></asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                border-bottom: 5px inset rgb(163, 156, 156); padding: 3px">
                Año:
                <asp:DropDownList ID="DropAño" runat="server" Width="100px">
                </asp:DropDownList>
                <asp:Label ID="LblMes" runat="server" Text="Label">Mes:</asp:Label>
                <asp:DropDownList ID="DropMes" runat="server" Width="150px">
                    <asp:ListItem>ENERO</asp:ListItem>
                    <asp:ListItem>FEBRERO</asp:ListItem>
                    <asp:ListItem>MARZO</asp:ListItem>
                    <asp:ListItem>ABRIL</asp:ListItem>
                    <asp:ListItem>MAYO</asp:ListItem>
                    <asp:ListItem>JUNIO</asp:ListItem>
                    <asp:ListItem>JULIO</asp:ListItem>
                    <asp:ListItem>AGOSTO</asp:ListItem>
                    <asp:ListItem>SEPTIEMBRE</asp:ListItem>
                    <asp:ListItem>OCTUBRE</asp:ListItem>
                    <asp:ListItem>NOVIEMBRE</asp:ListItem>
                    <asp:ListItem>DICIEMBRE</asp:ListItem>
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
                        <asp:CheckBox ID="CheckSubL" runat="server" AutoPostBack="True" Text="TODAS" Visible="false"/>
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
            <h2 style="border-bottom: 5px inset rgb(163, 156, 156);">
                Avance Líneas Programadas</h2>
            <asp:Chart ID="Chart1" runat="server" 
                    BackGradientStyle="HorizontalCenter" 
                    BackSecondaryColor="AliceBlue" BorderlineColor="Transparent" Width="800px" 
                     PaletteCustomColors="Teal">
                    <Series>
                        <asp:Series ChartType="StackedBar" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="Transparent" />
                </asp:Chart>
            <h2 style="border-bottom: 5px inset rgb(163, 156, 156);">
                Avance Líneas Por Solicitud</h2>
            <asp:Chart ID="Chart2" runat="server" 
                    BackGradientStyle="HorizontalCenter" 
                    BackSecondaryColor="AliceBlue" BorderlineColor="Transparent" Width="800px" 
                     PaletteCustomColors="Teal">
                    <Series>
                        <asp:Series ChartType="StackedBar" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                    <BorderSkin BackColor="Transparent" />
                </asp:Chart>
         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
