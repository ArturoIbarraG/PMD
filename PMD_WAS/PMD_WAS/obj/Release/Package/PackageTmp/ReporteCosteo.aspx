<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" Inherits="PMD_WAS.ReporteCosteo" Codebehind="ReporteCosteo.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Css/UpdateProgress.css" rel="stylesheet" type="text/css" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server" >
    <asp:ScriptManager ID="SCRIPTM" runat="server">
    </asp:ScriptManager>
    <div style=" margin-top:-2%;  margin-bottom:5%; margin-left:-7%; text-align: center; font-family:Lucida Bright;">
        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Reporte De Costeo"></asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                      
            <asp:UpdatePanel ID="UpdatePanelFiltro" runat="server" style="margin: 3% 0% 0% 15%;">
                <ContentTemplate>
                 <div id="DivColonia" style="margin: 0px 20px 0px 57px; text-align: left; font-size: medium;color: Green;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Colonia:
                        <asp:DropDownList ID="DropColonia" runat="server" Width="350px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:CheckBox ID="CheckColonia" runat="server" AutoPostBack="True" 
                            Text="TODAS" />
                    </div>
                    <div id="DivTipoAccion" style="margin: 0px 20px 0px 54px; text-align: left; font-size: medium;color: Green;">
                        &nbsp; Tipo de acción:
                        <asp:DropDownList ID="DropTipoAccion" runat="server" Width="350px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:CheckBox ID="CheckTipoAccion" runat="server" AutoPostBack="True" 
                            Text="TODAS" />
                    </div>
                 <div id="DivSecr" style="margin: 20px 20px 0px 57px; text-align: left; font-size: medium;color: Green;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Secretaría:
                        <asp:DropDownList ID="DropSecr" runat="server" Width="500px" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:CheckBox ID="CheckSecr" runat="server" AutoPostBack="True" Text="TODAS" />
                    </div>
                    <div id="DivDir" style="margin: 0px 20px 20px 57px; text-align: left; font-size: medium;color: Green;">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dirección:
                        <asp:DropDownList ID="DropDir" runat="server" Width="500px">
                        </asp:DropDownList>
                        <asp:CheckBox ID="CheckDir" runat="server" AutoPostBack="True" Text="TODAS" />
                    </div>
                   
                     <div id="DivBoton" style=" float: left; text-align:right; color: Green;
                font-size: medium; width: 100%">
                &nbsp;&nbsp;
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" Width="175px" />
                
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
           
            
          <h2 style=" border-bottom: 5px inset rgb(163, 156, 156); color:#0A6A6F; ">
                REPORTE</h2>
            <div id="divg" style="text-align: center; margin: 3% 0% 2% 0%; overflow:auto; height:350px" >
                         <asp:GridView ID="GridView1" runat="server" CellPadding="4" GridLines="Horizontal" 
                    ForeColor="Black" BackColor="White" BorderColor="#CCCCCC" Width="99%"  AutoGenerateColumns="true"
                    BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black"/>
                    <HeaderStyle BackColor="#0A6A6F" Font-Bold="True" ForeColor="White"   />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                   
                    <EmptyDataTemplate>
                      <table width="1000px"><tr><td align="center"><asp:Label ID="lblSD" runat="server" ForeColor="Blue" Text="Sin Datos" Font-Size="Large"></asp:Label></td></tr></table>
                     </EmptyDataTemplate>
                </asp:GridView>
            </div>
              </ContentTemplate>
    </asp:UpdatePanel>
     <div style=" text-align:right;"> <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel"/>
            </div>
   <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="Background"></div>
            <div class="Progress">
                  <p style="text-align: center" >
                  <br />
                        Cargando Datos, Espere por favor...<br /><br /><br />
                        <img src="Images/LoadingBarAzul.gif" / style="width:60%;">
                        <br />
                  </p>
                
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
       
</asp:Content>
