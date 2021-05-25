<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.PreCaptura" Codebehind="PreCaptura.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 281px;
        }
        .style3
        {
            width: 100px;
        }
        .style4
        {
            width: 35px;
        }
        .style5
        {
            width: 85px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <div style="font-weight: 700; margin: 20px 0px 30px 0px;">
        <asp:Label ID="Label1" runat="server" Font-Italic="True" Font-Size="13pt" Text="Selecciona La Administración Y El Año Correspondiente Por El Que Desees Filtrar"></asp:Label></div>
    <div style="margin-left: 30px;">
        <table class="style1">
            <tr>
                <td class="style3">
                    <asp:Label ID="lblAdministracion" runat="server" Text="Administración:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:DropDownList ID="DropAdmon" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td class="style4">
                    <asp:Label ID="lblAño" runat="server" Text="Año:"></asp:Label>
                </td>
                <td class="style5">
                    <asp:DropDownList ID="DropAnio" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td class="style9">
                    <asp:Button ID="BtnFiltrar" runat="server" Text="Filtrar" Style="height: 26px" />
                </td>
            </tr>
        </table>
    </div>
    <div style="margin:2% 0% 0% 0%;">
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <table class="style1">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSecr" runat="server" Text="Secretaría" Width="100"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblArea" runat="server" Text="Área" Width="100"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEne" runat="server" Text="Ene" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFeb" runat="server" Text="Feb" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMar" runat="server" Text="Mar" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAbr" runat="server" Text="Abr" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMay" runat="server" Text="May" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblJun" runat="server" Text="Jun" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblJul" runat="server" Text="Jul" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAgo" runat="server" Text="Ago" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSept" runat="server" Text="Sept" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblOct" runat="server" Text="Oct" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNov" runat="server" Text="Nov" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDic" runat="server" Text="Dic" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAn" runat="server" Text="ANUAL" Width="50"></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="lblAjuste" runat="server" Text="AJUSTE" Width="50"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table class="style1">
                            <tr>
                                <td width="100">
                                    <asp:Label runat="server" ID="lblCveSecr" Text='<%#Eval("IdSecretaria")%>' Width="100px"
                                        Visible="false"></asp:Label>
                                  
                                    <asp:Label ID="lblNombrSecr" runat="server" Text='<%# Eval("Nombr_Secretaria") %>'
                                        Width="100"></asp:Label>
                                </td>
                                <td width="100">
                                    <asp:Label ID="lblNombrArea" runat="server" Text='<%# Eval("Nombr_Direccion") %>'
                                        Width="100"></asp:Label>
                                </td>
                                <td width="50">
                              
                                    <%--<%If HabilitaEne = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("ENERO"), (1), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                               <%-- <%End If%>--%>
                                <td width="50">
                                  <%--  <%If HabilitaFeb = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink2" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("FEBRERO"), (2), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                               <%-- <% End If%>--%>
                                <td width="50">
                                   <%-- <%If HabilitaMar = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink3" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("MARZO"), (3), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                               <%-- <%End If%>--%>
                                <td width="50">
                                   <%-- <%If HabilitaAbr = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink4" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("ABRIL"), (4), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                                <%--<%End If%>--%>
                                <td width="50">
                                  <%--  <%If HabilitaMay = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink5" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("MAYO"), (5), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                              <%--  <%End If%>--%>
                                <td width="50">
                              <%--      <%If HabilitaJun = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink6" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("JUNIO"), (6), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                                <%--<%End If%>--%>
                                <td width="50">
                                    <%--<%If HabilitaJul = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink7" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("JULIO"), (7), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                            <%--    <%End If%>--%>
                                <td width="50">
                                  <%--  <%If HabilitaAgo = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink8" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("AGOSTO"), (8), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                               <%-- <%End If%>--%>
                                <td width="50">
                                  <%--  <%If HabilitaSep = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink9" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("SEPTIEMBRE"), (9), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                               <%-- <%End If%>--%>
                                <td width="50">
                                   <%-- <%If HabilitaOct = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink10" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("OCTUBRE"), (10), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                              <%--  <%End If%>--%>
                                <td width="50">
                                  <%--  <%If HabilitaNov = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink11" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("NOVIEMBRE"), (11), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                                <%--<%End If%>--%>
                                <td width="55">
                                  <%--  <%If HabilitaDic = "1" Then%>--%>
                                    <asp:HyperLink ID="HyperLink12" runat="server" Visible="true" NavigateUrl='<%# String.Format("Captura.aspx?Dir={0}&Mes={1}&NumMes={2}&Secr={3}&CveSecr={4}&CveDir={5}", Eval("Nombr_Direccion"), ("DICIEMBRE"), (12), Eval("Nombr_Secretaria"), Eval("IdSecretaria"), Eval("IdDireccion")) %>'>Mensual</asp:HyperLink>
                                </td>
                               <%-- <%End If%>--%>
                                <td width="50">
                                    <%If HabilitaAnual = "1" Then%>
                                    <asp:HyperLink ID="HyperLink13" runat="server" Visible="true" NavigateUrl='<%# String.Format("CapturaAnual.aspx?Dir={0}&Mes={1}&Secr={2}", Eval("Nombr_Direccion"),("ANUAL"),Eval("Nombr_Secretaria")) %>'>Anual</asp:HyperLink>
                                </td>
                                <%End If%>
                                   <td width="40">
                                    <%If HabilitaAjustes = "1" Then%>
                                    <asp:HyperLink ID="HyperLink14" runat="server" Visible="true" NavigateUrl='<%# String.Format("CapturaAjustes.aspx?Dir={0}&Mes={1}&Secr={2}", Eval("Nombr_Direccion"),("AJUSTES"),Eval("Nombr_Secretaria")) %>'>Ajuste</asp:HyperLink>
                                </td>
                                <%End If%>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
