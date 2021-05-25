<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" Inherits="PMD_WAS.Costeo" Codebehind="Costeo.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--ESTOS 2 SCRIPTS SON PARA HACER LA BUSQUEDA EN EL GRID YA CARGADO, Y EN EL GRID SOLO SE MANDA LLAMAR LA CLASE--%>
    <script src="Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/JScriptBusquedaEnGrid.js" type="text/javascript"></script>
    <style type="text/css">
        .labels
        {
            text-align: right;
        }
        .style1
        {
            width: 100%;
        }
        .Tit
        {
            color: Blue;
            font-family:Lucida Bright;
            font-weight:bold;
            font-size:15px;
        }
        .style2
        {
            width: 149px;
        }
        .style3
        {
            width: 146px;
        }
        .style4
        {
        }
        .style9
        {
            width: 585px;
        }
        .style10
        {
            width: 392px;
        }
        .style11
        {
            width: 582px;
        }
        .style13
        {
            width: 88px;
        }
        .style14
        {
            width: 96px;
        }
        .style15
        {
            width: 119px;
        }
        .style16
        {
            width: 85px;
        }
        .style17
        {
            width: 56px;
        }
        .style18
        {
            width: 65px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td class="style9">
                <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Folio:" 
                    Font-Bold="True" Font-Names="Lucida Bright" ForeColor="#FF6600" 
                    Width="80px"></asp:Label><asp:Label
                    ID="lblFolio" Font-Size="X-Large" runat="server" Text="Label" 
                    Font-Names="Lucida Bright" ForeColor="#000099"></asp:Label>
            </td>
            <td align="right">
                <asp:Label ID="lblFecha" runat="server" Text="Label" Font-Bold="True" 
                    Font-Size="15pt" ForeColor="#000099"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <h3 class="Tit">
                    Costeo</h3>
            </td>
            <td>
                <h3 class="Tit">
                    Transportación</h3>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <table class="style1">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblTipoProg" runat="server" Width="140px" CssClass="labels" Text="Tipo de programa:"></asp:Label>
                        </td>
                        <td> <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                            <asp:DropDownList ID="DropTipoProg" runat="server" Width="300px" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                            </ContentTemplate></asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblUnidad" runat="server" Width="140px" CssClass="labels" Text="Unidad Admin:"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DropUnidadAdmin" runat="server" AutoPostBack="True" Width="300px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblColonia" runat="server" Width="140px" CssClass="labels" Text="Colonia:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropColonia" runat="server" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    </table>
            </td>
            <td>
                <table class="style1">
                    <tr>
                        <td class="style3">
                            <asp:Label ID="lblHoras" runat="server" Width="140px" CssClass="labels" Text="Horas:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txthoras" runat="server" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="lblVehiculo" runat="server" Width="140px" CssClass="labels" Text="Número de vehículos:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtvehiculos" runat="server" Width="80px" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label15" runat="server" CssClass="labels" Text="Número de empleados:"
                                Width="140px"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtNumEmpl" runat="server" AutoPostBack="True" onkeypress="javascript:return solonumeros(event)"
                                        Text="1" Width="80px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <h3 class="Tit">
                    Periodicidad</h3>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="Label3" runat="server" CssClass="labels" Text="Costo de Nómina: $"
                            Width="324px"></asp:Label>
                        <asp:TextBox ID="txtCostoNomina" runat="server" Enabled="False" Width="80px" 
                            BackColor="#F8AF6D"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center" class="style9">
                <asp:RadioButtonList ID="rdbPeriodo" runat="server" RepeatDirection="Horizontal"
                    Width="300px">
                    <asp:ListItem Value="Mensual" Selected="True">Mensual</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="center">
                <asp:Label ID="lblSecretaria" runat="server" Font-Size="16pt" Font-Bold="True" 
                    Font-Italic="True" Font-Names="Lucida Bright" ForeColor="#003399"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style4" colspan="2">
                <table class="style1">
                    <tr>
                        <td class="style11">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style11">
                            <asp:Label ID="lblTipoAcción" runat="server" CssClass="labels" 
                                Text="Tipo de acción:" Font-Bold="False" Font-Names="Lucida Bright" 
                                Font-Size="13pt" ForeColor="#FF6600"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblComen" runat="server" CssClass="labels" Text="Comentarios:" 
                                Font-Names="Lucida Bright" Font-Size="13pt" ForeColor="#FF6600"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style11">
                            <asp:DropDownList ID="DropTipoAccion" runat="server" Width="568px" 
                                Height="22px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcomentarios" runat="server" TextMode="MultiLine" 
                                Width="403px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style9">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style9">
                <h3 class="Tit">
                    Tipo de Suministros</h3>
            </td>
            <td>
                <asp:RadioButtonList ID="rdbTipo" runat="server" RepeatDirection="Horizontal" Width="300px">
                    <asp:ListItem Value="Requisición" Selected="True">Requisición</asp:ListItem>
                    <asp:ListItem Value="Donativo">Donativo</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="center" class="style4" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <div id="Div2" style="text-align: center; margin-top: 1%;">
                            <asp:Label ID="lblLista" runat="server" Text="Buscar En La Lista:" Style="font-family: Comic Sans MS;
                                font-weight: bold; font-size: 15px;"></asp:Label>
                            <input id="texto" onclick="return texto_onclick()" type="text" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center" class="style4" colspan="2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="height: 150px; width: 500px; overflow: scroll;" align="center">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateSelectButton="True" 
                                CellPadding="4" class="filtrar" GridLines="None" Width="545px" 
                                ForeColor="#333333">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
    <br />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="style1">
                <tr>
                    <td class="style15">
                        <asp:Label ID="lblCostoUnitario" runat="server" CssClass="labels" Text="Costo Unitario: $"></asp:Label>
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtCostoUnitario" runat="server" Width="80px" BackColor="#99CCFF"
                            Enabled="False"></asp:TextBox>
                    </td>
                    <td class="style16">
                        <asp:Label ID="Label2" runat="server" Width="80px" CssClass="labels" Text="Cantidad:"></asp:Label>
                    </td>
                    <td class="style17">
                        <asp:TextBox ID="txtCantidadSuministro" runat="server" Width="45px" AutoPostBack="True"
                            Text="1" BorderColor="#FF6600"></asp:TextBox>
                    </td>
                    <td class="style18">
                        <asp:Label ID="Label16" runat="server" Width="54px" CssClass="labels" Text="Total: $"></asp:Label>
                    </td>
                    <td class="style13">
                        <asp:TextBox ID="txtcostoTotal" runat="server" Width="80px" BackColor="#99CCFF" Enabled="False"></asp:TextBox>
                    </td>
                    <td class="style16">
                        <asp:Label ID="lblReq" runat="server" Width="80px" CssClass="labels" Text="Suministro:"></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox ID="txtCveSuministro" runat="server" Width="42px" BackColor="#99CCFF"
                            Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txtSuministro" runat="server" Width="340px" BackColor="#99CCFF"
                            Enabled="False"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="41px" 
                            ImageUrl="~/Images/agregar-icono-7533-128.png" Width="44px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style=" border-color: Blue; border-style: groove;" align="center">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" Width="90%" Height="131px" 
                CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="40px" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div style="text-align: right;">
                <asp:Label ID="lblImporte" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="#000099">Importe:     $</asp:Label>
                <asp:TextBox ID="TxtImporte" runat="server" Enabled="False" Width="80px" BackColor="#99CCFF"></asp:TextBox></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div style=" text-align:center;">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Width="150px" />
            </div>
                </asp:Content>
