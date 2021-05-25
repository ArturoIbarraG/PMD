<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.ConsultaSecretaria" Codebehind="ConsultaSecretaria.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <asp:HiddenField ID="HiddenField1" runat="server" />
    <div style="margin: -4% 20% 2% 20%; text-align: center;">
        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Consulta Por Secretaría"></asp:Label>
        <blockquote>
            Podrás Navegar por los diferentes Programas para buscar un proyecto o compromiso.</blockquote>
       
        <asp:Label ID="lblAdministracion" runat="server" Text="Administración:"></asp:Label>
        <asp:DropDownList ID="DropAdmon" runat="server" Width="250px" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Label ID="Label6" runat="server" Text="Año:"></asp:Label>
        <asp:DropDownList ID="DropAnio" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </div>
    <asp:UpdatePanel ID="Panel" runat="server">
        <ContentTemplate>
            <table width="100%" style="height: 400px">
                <tr>
                  <td align="center" style="height: 30px; border: outset; background-color: #e3ebff">
                        Secretarias
                    </td>
                    <td align="center" style="height: 30px; border: outset; background-color: #e3ebff">
                        Programas
                    </td>
                    <td align="center" style="border: outset; background-color: #e3ebff">
                        Componentes
                    </td>
                    <td align="center" style="border: outset; background-color: #e3ebff">
                        Actividades
                    </td>
                  <%--  <td align="center" style="border: outset; background-color: #e3ebff">
                        Líneas
                    </td>
                    <td align="center" style="border: outset; background-color: #e3ebff">
                        Sublíneas
                    </td>--%>
                </tr>
                <tr>
                <td width="20%" style="border: inset">
                        <asp:GridView ID="GridSecr" runat="server" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="CveL" runat="server" Width="20px" Text='<%#Eval ("IdSecretaria") %>'></asp:Label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval ("Nombr_secretaria") %>' style="/* display:inline-block; */font-size:13px; font-family:Candara;/*color:rgb(51, 32, 100);*/"
                                            CommandName="Buscar" CommandArgument='<%# Eval("IdSecretaria") %>' CssClass="Links" Width="90%" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td width="15%" style="border: inset">
                        <asp:GridView ID="GridEje" runat="server" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="CveL" runat="server" Width="15px" Text='<%#Eval ("IdEje") %>'></asp:Label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval ("Desc_eje") %>' style="/* display:inline-block; */font-size:13px; font-family:Candara;"
                                            CommandName="Buscar" CommandArgument='<%# Eval("IdEje") %>' CssClass="Links" Font-Size="14px" Width="90%"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td width="15%" style="border: inset">
                        <asp:GridView ID="GridObj" runat="server" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval ("Nombr_obj") %>' style="/* display:inline-block; */font-size:13px; font-family:Candara;"
                                            CommandName="Buscar" CommandArgument='<%# Eval("IdObjetivo") %>' CssClass=" Links" Width="90%"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td width="15%" style="border: inset">
                        <asp:GridView ID="GridEstr" runat="server" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval ("Nombr_estr") %>' style="/* display:inline-block; */font-size:13px; font-family:Candara;"
                                            Width="90%" CommandName="Buscar" CommandArgument='<%# Eval("IdEstrategia") %>'
                                            CssClass="Links" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                   <%-- <td width="20%" style="border: inset">
                        <asp:GridView ID="GridLin" runat="server" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="CveL" runat="server" Width="15px" Text='<%#Eval ("IdLinea") %>'></asp:Label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval ("Nombr_linea") %>' style="/* display:inline-block; */font-size:13px; font-family:Candara;"
                                            Width="150px" CommandName="Buscar" CommandArgument='<%# Eval("IdLinea") %>' CssClass="Lin"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td width="20%" style="border: inset">
                        <asp:GridView ID="GridSubL" runat="server" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="CveL" runat="server" Width="15px" Text='<%#Eval ("IdSublinea") %>'></asp:Label>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval ("Nombr_Sub") %>' style="/* display:inline-block; */font-size:13px; font-family:Candara;"
                                            CommandName="Buscar" CommandArgument='<%# Eval("IdSublinea") %>' CssClass="Lin"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>--%>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="Pnl" runat="server" Width="1040" Height="545px" BackColor="White">
        <asp:UpdatePanel ID="PanelModal" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="background-color: rgb(61, 19, 124); font-weight: bold; font-size: 15px;
                            color: White;">
                            INFORMACIÓN
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center">
                    <tr>
                        <td class="TextoModal">
                            Clave
                        </td>
                        <td>
                            <asp:Label ID="E" runat="server"></asp:Label>.
                            <asp:Label ID="O" runat="server"></asp:Label>.
                            <asp:Label ID="Es" runat="server"></asp:Label>.
                            <%--<asp:Label ID="L" runat="server"></asp:Label>.
                            <asp:Label ID="S" runat="server"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="TextoModal">
                            Secretaria</td>
                        <td>
                            <asp:Label ID="Secr" runat="server"></asp:Label>
                            <asp:Label ID="NombrSecr" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TextoModal">
                            Programa
                        </td>
                        <td>
                            <asp:Label ID="InfEje" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TextoModal">
                            Componente
                        </td>
                        <td>
                            <asp:Label ID="InfObj" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TextoModal">
                            Actividad
                        </td>
                        <td>
                            <asp:Label ID="InfEstr" runat="server"></asp:Label>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td class="TextoModal">
                            Línea
                        </td>
                        <td>
                            <asp:Label ID="InfLin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TextoModal">
                            Sublínea
                        </td>
                        <td>
                            <asp:Label ID="InfSub" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
                <br />
                <div style="width: 100%; margin-left:.2%; text-align:center"">
                <asp:Label ID="CveTipoLinea" runat="server" Font-Size="15px" Visible="False"></asp:Label>
                <asp:Label ID="MensajeTipoLinea" runat="server" Font-Size="15px"></asp:Label>
                    <asp:GridView ID="GridaAnual1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        Width="99%" ForeColor="Black" GridLines="Horizontal">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td align="center" width="80">
                                                <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                                            </td>
                                            
                                            <td align="center" width="70">
                                                <asp:Label ID="Label5" runat="server" Font-Size="Smaller" Text="Unidad De Medida"></asp:Label>
                                            </td>
                                              <td align="center" width="70">
                                                <asp:Label ID="Label8" runat="server" Font-Size="Smaller" Text="Clasificación"></asp:Label>
                                            </td>
                                            <td align="center" width="40">
                                                <asp:Label ID="Label7" runat="server" Text="Tipo"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label9" runat="server" Text="Ene"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label11" runat="server" Text="Feb"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label13" runat="server" Text="Mar"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label1" runat="server" Text="Abr"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label2" runat="server" Text="May"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label15" runat="server" Text="Jun"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label17" runat="server" Text="Jul"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label19" runat="server" Text="Ago"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label24" runat="server" Text="Sep"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label25" runat="server" Text="Oct"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label26" runat="server" Text="Nov"></asp:Label>
                                            </td>
                                            <td align="center" width="43">
                                                <asp:Label ID="Label27" runat="server" Text="Dic"></asp:Label>
                                            </td>
                                            <td align="center" width="60">
                                                <asp:Label ID="Label20" runat="server" Text="Total"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblId" runat="server" Font-Size="8" Text='<%# Eval("ID") %>' Width="80"> </asp:Label>
                                            </td>
                                           
                                            <td align="center">
                                                <asp:Label ID="lblMedida" runat="server" Font-Size="8" Text='<%# Eval("Unidad_Medida") %>'
                                                    Width="70"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="Label4" runat="server" Font-Size="8" Text='<%# Eval("Clasificacion") %>'
                                                    Width="70"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblPlaneado" runat="server" Text="P" Height="40px" Width="40"></asp:Label>
                                            </td>
     
                                            <td width="43">
                                                <asp:TextBox ID="txt1" runat="server" Text='<%# Eval("Enero")  %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fEne" CommandName="fEne" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt2" runat="server" Text='<%# Eval("Febrero") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fFeb" CommandName="fFeb"  ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt3" runat="server" Text='<%# Eval("Marzo") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fMar" CommandName="fMar" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt4" runat="server" Text='<%# Eval("Abril") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fAbr" CommandName="fAbr" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt5" runat="server" Text='<%# Eval("Mayo") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fMay" CommandName="fMay" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt6" runat="server" Text='<%# Eval("Junio") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fJun" CommandName="fJun" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt7" runat="server" Text='<%# Eval("Julio") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fJul" CommandName="fJul" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt8" runat="server" Text='<%# Eval("Agosto") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fAgo" CommandName="fAgo" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt9" runat="server" Text='<%# Eval("Septiembre") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fSep" CommandName="fSep" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt10" runat="server" Text='<%# Eval("Octubre") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fOct" CommandName="fOct" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt11" runat="server" Text='<%# Eval("Noviembre") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fNov" CommandName="fNov" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="43">
                                                <asp:TextBox ID="txt12" runat="server" Text='<%# Eval("Diciembre") %>' Width="40" Enabled="false"></asp:TextBox>
                                                <asp:ImageButton runat="server" ID="fDic" CommandName="fDic" ImageUrl="~/Images/Flecha.jpg" Height="20" Width="10" /> 
                                            </td>
                                            <td width="30">
                                                <asp:Label ID="LblTotalPP" runat="server"  Font-Size="18px" Height="40px" maxlength="4" Text='<%# Eval("Acumulado") %>'
                                                    Width="30"></asp:Label>
                                            </td>
                                            <% If Signo = 1 Then%>
                                            <td align="right" width="40">
                                                <asp:Label ID="lblSigno" runat="server"  Text="%" Height="40px" Width="40"></asp:Label>
                                            </td>
                                            <%End If%>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#2a8399" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>
                </div>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" RowStyle-BorderStyle="Solid"
                            Style="margin-right: 0px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                            
                                                <td style="width: 80px;" rowspan="2">
                                                    Semana
                                                    <br />
                                                    Tipo
                                                </td>
                                            </tr>
                                         
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                               
                                                <td align="center" style="width: 60px;">
                                                    <asp:Label ID="Label6" runat="server" Text="P" Style="display: inline-block; height: 20px;"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label7" runat="server" Text="R" Style="display: inline-block;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt1"  runat="server"
                                            Text='<%# Eval("Semana1") %>' Width="50" Enabled="false" ></asp:TextBox>
                                       
                                        <br />
                                        <asp:TextBox ID="txt6"  runat="server"
                                            Text='<%# Eval("Semana1R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt2"  runat="server"
                                            Text='<%# Eval("Semana2") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txt7"  runat="server"
                                            Text='<%# Eval("Semana2R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt3"  runat="server"
                                            Text='<%# Eval("Semana3") %>' Width="50" Enabled="false"></asp:TextBox>
                                        
                                        <br />
                                        <asp:TextBox ID="txt8"  runat="server"
                                            Text='<%# Eval("Semana3R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt4"  runat="server"
                                            Text='<%# Eval("Semana4") %>' Width="50" Enabled="false" ></asp:TextBox>  <br />
                                        <asp:TextBox ID="txt9"  runat="server" 
                                            Text='<%# Eval("Semana4R") %>' Width="50" Enabled="false" ></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt5"  runat="server" 
                                            Text='<%# Eval("Semana5") %>' Width="50" Enabled="false" ></asp:TextBox>
                                        
                                        <br />
                                        <asp:TextBox ID="txt10"  runat="server"
                                            Text='<%# Eval("Semana5R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt11"  runat="server"
                                            Text='<%#Eval("Semana6") %>' Width="50" Enabled="false"></asp:TextBox>
                                        
                                        <br />
                                        <asp:TextBox ID="txt12"  runat="server"
                                            Text='<%# Eval("Semana6R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acumulado Mensual">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="ipd" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label ID="LblTotalP" runat="server" Text='<%# Eval("Acumulado") %>' Width="50"
                                                    Height="20px"></asp:Label>
                                                <asp:HiddenField ID="hdnTotal" runat="server" />
                                                <br />
                                                <asp:Label ID="LblTotalR" runat="server" Text='<%# Eval("AcumuladoR") %>' Width="60"
                                                   ></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Mensual">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updTM" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalMensual" runat="server" Text='<%# Eval(Mes) %>' Width="50px"></asp:Label>
                                                           
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                            </Columns>
                           
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#2A8399" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="margin-right: 0px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                               
                                                <td style="width: 100px;" rowspan="2">
                                                    Semana
                                                    <br />
                                                    Tipo
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                               
                                                <td align="center" style="width: 70px;">
                                                    <asp:Label ID="lblP" runat="server" Style="display: inline-block; height: 6px;">Programa</asp:Label>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label2" runat="server" Style="display: inline-block; ">Realizado:</asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label3" runat="server" Style="display: inline-block; height: 8px;">Solicitado:</asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label5" runat="server" Style="display: inline-block; ">Porcentaje:</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt1" runat="server" Text='<%# Eval("Semana1") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txt6" runat="server" Text='<%# Eval("Semana1R") %>' Width="50" Enabled="false"></asp:TextBox>
                                       
                                        <br />
                                        <asp:TextBox ID="txt13" runat="server" Text='<%# Eval("Semana1S") %>' Width="50" Enabled="false" ></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblPor1" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="lblSigno1" runat="server" Text="%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt2" runat="server" Text='<%# Eval("Semana2") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txt7" runat="server" Text='<%# Eval("Semana2R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txt14" runat="server" Text='<%# Eval("Semana2S") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblPor2" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="lblSigno2" runat="server" Text="%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt3" runat="server" Text='<%# Eval("Semana3") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txt8" runat="server" Text='<%# Eval("Semana3R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txt15" runat="server" Text='<%# Eval("Semana3S") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblPor3" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="lblSigno3" runat="server" Text="%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt4"  runat="server" Text='<%# Eval("Semana4") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txt9" runat="server" Text='<%# Eval("Semana4R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txt16" runat="server"  Text='<%# Eval("Semana4S") %>' Width="50" Enabled="false" ></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblPor4" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="lblSigno4" runat="server" Text="%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt5" runat="server" Text='<%# Eval("Semana5") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txt10" runat="server" Text='<%# Eval("Semana5R") %>' Width="50" Enabled="false"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txt17" runat="server" Text='<%# Eval("Semana5S") %>' Width="50" Enabled="false"  ></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblPor5" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="lblSigno5" runat="server" Text="%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt11" runat="server" Text='<%#Eval("Semana6") %>' Width="50" Enabled="false" ></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txt12" runat="server" Text='<%# Eval("Semana6R") %>' Width="50"  Enabled="false"  ></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txt18" runat="server" Text='<%# Eval("Semana6S") %>' Width="50"  Enabled="false" ></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblPor6" runat="server" Text="0"></asp:Label>
                                        <asp:Label ID="lblSigno6" runat="server" Text="%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acumulado Mensual">
                                    <ItemTemplate>
                                        <asp:Label ID="LblTotalP" runat="server" Text='<%# Eval("Acumulado") %>' Width="50"
                                            Height="7"></asp:Label><asp:Label ID="lblSigno" runat="server" Width="10" Height="5">%</asp:Label>
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Label ID="LblTotalR" runat="server" Text='<%# Eval("AcumuladoR") %>' Width="60"
                                            ></asp:Label>
                                        <br />
                                        <asp:Label ID="lblTotalS" runat="server" Text='<%# Eval("AcumuladoS") %>'> </asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblTotalPorcentaje" runat="server" Width="50"  Text="0"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text="%"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Mensual">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblTotalMensual" runat="server" Text='<%# Eval(Mes) %>' Width="36px"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text='%' Width="10px"></asp:Label>
                                                    <asp:HiddenField ID="Hidden2ComP" Value='<%# Eval("ComP") %>' runat="server" />
                                                    <asp:HiddenField ID="Hidden2ComR" Value='<%# Eval("ComR") %>' runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observaciones" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCom" runat="server" TextMode="MultiLine" Width="130px"></asp:TextBox><br />
                                        <asp:TextBox ID="txtCom2" runat="server" TextMode="MultiLine" Width="130px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                           
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#2A8399" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <div id="divBtn" style="text-align: center;">
            <input id="BtnSalir" value="Salir" type="button" onkeypress="" onclick="return BtnSalir_onclick()" />
        </div>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalTab" runat="server" PopupControlID="Pnl" CancelControlID="BtnSalir"
        DropShadow="true" TargetControlID="lnkPrueba" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:LinkButton ID="lnkPrueba" runat="server"></asp:LinkButton>
</asp:Content>
