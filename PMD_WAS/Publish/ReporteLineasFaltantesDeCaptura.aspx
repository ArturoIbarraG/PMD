<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.ReporteLineasFaltantesDeCaptura" Codebehind="ReporteLineasFaltantesDeCaptura.aspx.vb" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 34px;
        }
        .style3
        {
            width: 146px;
        }
        .style4
        {
            width: 53px;
        }
        .style5
        {
            width: 140px;
        }
    </style>
    <style type="text/css">
        .Links
        {
            color: rgb(9, 8, 2);
            font-size: 17px;
            font-weight: bold;
            text-decoration: none;
        }
        .Lin
        {
            color: rgb(9, 8, 2);
            font-size: 12px;
            font-weight: bold;
            text-decoration: none;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalPopup
        {
            background-color: #ffffdd;
            width: 250px;
            border-right: #ff0000 3px solid;
            padding-right: 1px;
            border-top: #ff0000 3px solid;
            padding-left: 1px;
            padding-bottom: 1px;
            border-left: #ff0000 3px solid;
            padding-top: 1px;
            border-bottom: #ff0000 3px solid;
        }
        .TextoModal
        {
            width: 20%;
            font-family: Century Gothic;
            font-size: 17px;
            font-weight: bold;
            color: White;
            background-color: rgb(42, 131, 153);
            font-family: Times New Roman;
        }
        .style6
        {
            width: 48px;
        }
        .style8
        {
            width: 272px;
        }
        .style9
        {
            width: 15px;
        }
    </style>
      <script type="text/javascript">
          /*ESTA FUNCION ES PARA CERRAR EL MODAL CON LA TECLA ESC*/
          document.onkeydown = TeclaEsc;

          function TeclaEsc(e) {
              var tecla = (window.event) ? e.which : e.keyCode;
              if (tecla == 27) {
                  $find('MainContent_ModalTab').hide();
                  return false;
              }
          }
          /*ESTA FUNCION MUESTRA EL MODAL*/
          function ModalPopUpShow() {

              $find('MainContent_ModalTab').show();

          }
          /*ESTA FUNCION CIERRA EL MODAL(LA MANDO LLAMAR DESDE UN BOTON*/
          function ModalHide() {
              $find('MainContent_ModalTab').hide();

          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
        <div style="font-size: large; color: #0000CC; font-family: 'lucida Bright';">Cuando el reporte de transparencia no se genere con el total de las lineas,posiblemente sea que algunas de ellas no se han capturado. </div><br />
        <br />
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
        Font-Names="Lucida Bright" AutoPostBack="True" Font-Size="11pt">
        <asp:ListItem Selected="True" Value="1">Líneas que no están programadas en el anual</asp:ListItem>
        <asp:ListItem Value="2">IDS que no estan planeados mensualmente,aunque su planeacion anual fue 0</asp:ListItem>
        <asp:ListItem Value="3">IDS que no estan planeados mensualmente,independientemente si su planeacion anual fue 0 o no</asp:ListItem>
    </asp:RadioButtonList>
      
        <br />
   
        <table class="style1">
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4" align="right">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style6" align="right">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label8" runat="server" Text="Administración:" 
                    Font-Names="Lucida Bright"></asp:Label>
            </td>
            <td class="style3">
                <asp:DropDownList ID="DropAdmon" runat="server" Width="200px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td class="style4" align="right">
                <asp:Label ID="Label6" runat="server" Text="Año:" Font-Names="Lucida Bright"></asp:Label>
            </td>
            <td class="style5">
                <asp:TextBox ID="txtAño" runat="server"></asp:TextBox>
            </td>
            <td class="style9">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtAño" ErrorMessage="*" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td class="style6" align="right">
                <asp:Label ID="lblMes" runat="server" Text="Mes:" Font-Names="Lucida Bright" 
                    Visible="False"></asp:Label>
            </td>
            <td class="style5">
                <asp:DropDownList ID="dropMes" runat="server" Width="120px" Visible="False">
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
            </td>
            <td class="style8">
                <asp:Button ID="Button1" runat="server" Text="Consultar" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td align="right" class="style4">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td align="right" class="style6">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="style8">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td align="right" class="style8">
                <asp:Label ID="lblCount" runat="server" Font-Names="Lucida Bright" 
                    Font-Size="13pt" ForeColor="#333300" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
       </ContentTemplate>
  </asp:UpdatePanel>
   <asp:UpdatePanel ID="Panel" runat="server">
      <ContentTemplate>
        <div style="  margin:0% 20% 0% 15%; overflow:scroll; height:500px">
                    <asp:GridView ID="GridView1" runat="server" 
        AutoGenerateColumns="False" CellPadding="4" 
                        Width="70%" ForeColor="#333333" GridLines="None" 
        AutoGenerateSelectButton="True">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td align="center" width="120">
                                                <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                                            </td>
                                                 <td align="center" width="120">
                                                <asp:Label ID="Label2" runat="server" 
                                                    Text="Clave_Secr"></asp:Label>
                                            </td>
                                              <td align="center" width="200">
                                                <asp:Label ID="Label28" runat="server"  Text="Secretaría"></asp:Label>
                                            </td>
                                              <td align="center" width="100">
                                                <asp:Label ID="Label1" runat="server" 
                                                    Text="Clave_Dir"></asp:Label>
                                            </td>
                                            <td align="center" width="200">
                                                <asp:Label ID="Label5" runat="server" 
                                                    Text="Dirección"></asp:Label>
                                            </td>
                                         
                                           
                                         
                                           
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                           <td align="center">
                                                <asp:Label ID="lblId" runat="server" Font-Size="10" Text='<%# Eval("ID") %>' 
                                                    Width="120"> </asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblCveSecr" runat="server" Font-Size="10" Text='<%# Eval("Cve_Secr") %>'
                                                    Width="120"></asp:Label>
                                            </td>
                                             <td align="center">
                                                <asp:Label ID="Label10" runat="server" Font-Size="10" Text='<%# Eval("Secretaria") %>'
                                                    Width="200"></asp:Label>
                                            </td>
                                         
                                             <td align="center">
                                                <asp:Label ID="lblCveDir" runat="server" Font-Size="10" Text='<%# Eval("Cve_Dir") %>'
                                                    Width="100"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblMedida" runat="server" Font-Size="10" Text='<%# Eval("Direccion") %>'
                                                    Width="200"></asp:Label>
                                            </td>
                                        
     
                                           
                                           
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="Pnl" runat="server" Width="100%"  BackColor="White">
        <asp:UpdatePanel ID="PanelModal" runat="server">
            <ContentTemplate>
                    <asp:GridView ID="GridView2" runat="server" 
                        AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        Width="50%" GridLines="Vertical" ShowFooter="True">
                        <AlternatingRowStyle BackColor="#E9E9E9" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table>
                                    <tr><td colspan="3" align="center" style="font-size:large">Bitácora de sesión</td></tr>
                                        <tr>
                                            <td align="center" width="200">
                                                <asp:Label ID="Label31" runat="server" 
                                                    Text="Nómina"></asp:Label>
                                            </td>
                                             <td align="center" width="200">
                                                <asp:Label ID="Label9" runat="server"  Text="Nombre"></asp:Label>
                                            </td>
                                              <td align="center" width="200">
                                                <asp:Label ID="Label33" runat="server"  Text="Último Acceso"></asp:Label>
                                            </td>
                                           
                                         
                                           
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                         
                                            <td align="center">
                                                <asp:Label ID="lblMedida0" runat="server" Font-Size="11" Text='<%# Eval("clave_empl") %>'
                                                    Width="200"></asp:Label>
                                            </td>
                                             <td align="center">
                                                <asp:Label ID="Label4" runat="server" Font-Size="11" Text='<%# Eval("Nombr_empl") %>'
                                                    Width="200"></asp:Label>
                                            </td>
                                             <td align="center">
                                                <asp:Label ID="Label34" runat="server" Font-Size="11" Text='<%# Eval("Fecha") %>'
                                                    Width="200"></asp:Label>
                                            </td>
     
                                           
                                           
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate><table width="100%"><tr><td align="right">Presione la tecla ESC para salir</td></tr></table></FooterTemplate>
                                
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F4F4F4" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                    <br />
                    <br />
                </div>
               
             </ContentTemplate>

        </asp:UpdatePanel>
      
         <div id="divBtn" style="text-align: center; visibility:hidden;">
            <input id="BtnSalir" value="Salir" type="button" onkeypress="" onclick="return BtnSalir_onclick()"  />
        </div>
    </asp:Panel>
               
   <asp:ModalPopupExtender ID="ModalTab" runat="server" PopupControlID="Pnl" CancelControlID="BtnSalir"
        DropShadow="true" TargetControlID="lnkPrueba" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="lnkPrueba" runat="server"></asp:LinkButton>
    
</asp:Content>

