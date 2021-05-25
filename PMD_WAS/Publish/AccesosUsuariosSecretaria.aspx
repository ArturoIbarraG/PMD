<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" Inherits="PMD_WAS.AccesosUsuariosSecretaria" Codebehind="AccesosUsuariosSecretaria.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style5
        {
            width: 91px;
        }
        .style6
        {
            width: 540px;
        }
        .style7
        {
            width: 90px;
        }
        .style8
        {
            width: 164px;
        }
    </style>
    <script type="text/javascript">
        function ProcExitoso1() {
            alert("Los Permisos Se Han Asignado Exitosamente")

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="script" runat="server">
    </asp:ScriptManager>
        <div id="DivAdmon" style=" text-align: left; color: Green; font-size: medium;">
                Administración:
                <asp:DropDownList ID="DropAdmon" runat="server" Width="300px" AutoPostBack="True">
                </asp:DropDownList>
            </div>
    <div style="position: absolute;">
        <asp:Label ID="Label2" runat="server" Font-Size="20px" Text="Asignar Accesos Por :"></asp:Label>
    </div>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div id="Tit" style="margin: 0% 0% 5% 20%">
                <asp:RadioButtonList ID="RadioAcceso" runat="server" RepeatDirection="horizontal"
                    Font-Size="Large" AutoPostBack="True">
                    <asp:ListItem Value="1" Selected="True">Secretaría</asp:ListItem>
                    <asp:ListItem Value="2">Dirección</asp:ListItem>
                </asp:RadioButtonList>
            </div>
   
    <div id="drop" style="text-align: center;">
        <asp:Label ID="Label1" runat="server" Font-Size="14pt" Text="USUARIO:"></asp:Label>
        <asp:DropDownList ID="DropUsuarios" runat="server" Width="500px" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Button ID="BtnGrabar" runat="server" Text="Grabar" Width="100" />
    </div>
         </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="Up" runat="server">
        <ContentTemplate>
            <div id="Grids" style="text-align: center; margin: 5% 20% 0% 16%">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="style1">
                                    <tr>

                                    <td  align="center">
                                            <asp:Label ID="lblId" runat="server" Text="ID" Width="70"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSecretaria" runat="server" Text="Secretaria" Width="430"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSinPermiso" runat="server" Text="Sin Permiso" Width="100"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblConPermiso" runat="server" Text="Con Permiso" Width="100"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="style1">
                                    <tr>
                                    <td width="50">
                                            <asp:Label ID="lblCveSecr" runat="server" Text='<%# Eval("Idsecretaria") %>' ></asp:Label>
                                        </td>
                                        <td width="380">
                                            <asp:Label ID="lblNombrSecr" runat="server" Text='<%# Eval("Nombr_secretaria") %>' ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="RdbSinPer" runat="server" Width="170px" GroupName="Secr" Checked="true" />
                                            <asp:RadioButton ID="RdbConPer" runat="server" GroupName="Secr" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#0A6A6F" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table class="style1">
                                    <tr>
                                      <td>
                                            <asp:Label ID="lblCve" runat="server" Text="ID" Width="70"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDirecciones" runat="server" Text="Direcciones" Width="430"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSinPermisoDir" runat="server" Text="Sin Permiso" Width="100"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblConPermisoDir" runat="server" Text="Con Permiso" Width="100"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table class="style1">
                                    <tr>
                                     <td width="50">
                                            <asp:Label ID="lblCveDir" runat="server" Text='<%# Eval("IdDireccion") %>'></asp:Label>
                                        </td>
                                        <td width="400">
                                            <asp:Label ID="lblNombrDir" runat="server" Text='<%# Eval("Nombr_direccion") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="RdbSinPer" runat="server" Width="170px" GroupName="Dir" Checked="true" />
                                            <asp:RadioButton ID="RdbConPer" runat="server" GroupName="Dir" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#0A6A6F" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
