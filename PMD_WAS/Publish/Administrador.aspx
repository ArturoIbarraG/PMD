<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.Administrador" Codebehind="Administrador.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function ProcExitoso1() {
            alert("Cambios Realizados")

        }
    </script>
    <style type="text/css">
        .ListBox
        {
            /*border-style: inset;
            border-width: 0px;*/
            border: inset;
            font-size: 8pt;
            font-family: Verdana;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
    <br />
    <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="12pt" Text="Habilitar o Deshabilitar Según Sea El Caso "></asp:Label>
    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="p">
        <ContentTemplate>
            <div id="DivAdmon" style="float: left; text-align: left; color: Green; font-size: medium;">
                Administración:
                <asp:DropDownList ID="DropAdmon" runat="server" Width="300px" AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div>
                <asp:ListBox ID="ListSecr" runat="server" SelectionMode="Multiple" Style="width: 460px;
                    height: 156px;" class="ListBox" EnableTheming="True"></asp:ListBox>
                <asp:CheckBox runat="server" Text="TODAS" Width="120px" ID="CheckTodas" AutoPostBack="True"
                    OnCheckedChanged="Unnamed1_CheckedChanged" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="margin: 2% 0% 2% 0%">
        <h2>
            <asp:Label ID="LblTit3" runat="server" Text="Activo-Inactivo" Style="color: rgb(48, 119, 21);
                margin-left: 44%"></asp:Label></h2>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td align="center" width="60">
                                    <asp:Label ID="Label7" runat="server" Text="Año"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label9" runat="server" Text="Ene"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label11" runat="server" Text="Feb"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label13" runat="server" Text="Mar"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label1" runat="server" Text="Abr"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label2" runat="server" Text="May"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label15" runat="server" Text="Jun"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label17" runat="server" Text="Jul"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label19" runat="server" Text="Ago"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label24" runat="server" Text="Sep"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label25" runat="server" Text="Oct"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label26" runat="server" Text="Nov"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label27" runat="server" Text="Dic"></asp:Label>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label3" runat="server" Text="ANUAL"></asp:Label>
                                </td>
                                </td>
                                <td align="center" width="70">
                                    <asp:Label ID="Label5" runat="server" Text="AJUSTES"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table class="style1">
                            <tr>
                                <td align="center" width="70">
                                    <asp:Label ID="LblAño" runat="server" Text='<%# Eval("Año") %>'></asp:Label>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label29" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanEneA" runat="server" GroupName="EneP" />
                                                <asp:RadioButton ID="RdbPlanEneI" runat="server" GroupName="EneP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label30" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealEneA" runat="server" GroupName="EneR" />
                                                <asp:RadioButton ID="RdbRealEneI" runat="server" GroupName="EneR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label31" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanFebA" runat="server" GroupName="FebP" />
                                                <asp:RadioButton ID="RdbPlanFebI" runat="server" GroupName="FebP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label32" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealFebA" runat="server" GroupName="FebR" />
                                                <asp:RadioButton ID="RdbRealFebI" runat="server" GroupName="FebR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label33" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanMarA" runat="server" GroupName="MarP" />
                                                <asp:RadioButton ID="RdbPlanMarI" runat="server" GroupName="MarP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label34" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealMarA" runat="server" GroupName="MarR" />
                                                <asp:RadioButton ID="RdbRealMarI" runat="server" GroupName="MarR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label35" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanAbrA" runat="server" GroupName="AbrP" />
                                                <asp:RadioButton ID="RdbPlanAbrI" runat="server" GroupName="AbrP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label36" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealAbrA" runat="server" GroupName="AbrR" />
                                                <asp:RadioButton ID="RdbRealAbrI" runat="server" GroupName="AbrR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label37" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanMayA" runat="server" GroupName="MayP" />
                                                <asp:RadioButton ID="RdbPlanMayI" runat="server" GroupName="MayP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label38" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealMayA" runat="server" GroupName="MayR" />
                                                <asp:RadioButton ID="RdbRealMayI" runat="server" GroupName="MayR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label39" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanJunA" runat="server" GroupName="JunP" />
                                                <asp:RadioButton ID="RdbPlanJunI" runat="server" GroupName="JunP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label40" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealJunA" runat="server" GroupName="JunR" />
                                                <asp:RadioButton ID="RdbRealJunI" runat="server" GroupName="JunR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanJulA" runat="server" GroupName="JulP" />
                                                <asp:RadioButton ID="RdbPlanJulI" runat="server" GroupName="JulP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label42" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealJulA" runat="server" GroupName="JulR" />
                                                <asp:RadioButton ID="RdbRealJulI" runat="server" GroupName="JulR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label43" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanAgoA" runat="server" GroupName="AgoP" />
                                                <asp:RadioButton ID="RdbPlanAgoI" runat="server" GroupName="AgoP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label44" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealAgoA" runat="server" GroupName="AgoR" />
                                                <asp:RadioButton ID="RdbRealAgoI" runat="server" GroupName="AgoR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label45" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanSeptA" runat="server" GroupName="SeptP" />
                                                <asp:RadioButton ID="RdbPlanSeptI" runat="server" GroupName="SeptP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label46" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealSeptA" runat="server" GroupName="SeptR" />
                                                <asp:RadioButton ID="RdbRealSeptI" runat="server" GroupName="SeptR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label47" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanOctA" runat="server" GroupName="OctP" />
                                                <asp:RadioButton ID="RdbPlanOctI" runat="server" GroupName="OctP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label48" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealOctA" runat="server" GroupName="OctR" />
                                                <asp:RadioButton ID="RdbRealOctI" runat="server" GroupName="OctR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label49" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanNovA" runat="server" GroupName="NovP" />
                                                <asp:RadioButton ID="RdbPlanNovI" runat="server" GroupName="NovP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label50" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealNovA" runat="server" GroupName="NovR" />
                                                <asp:RadioButton ID="RdbRealNovI" runat="server" GroupName="NovR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label51" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanDicA" runat="server" GroupName="DicP" />
                                                <asp:RadioButton ID="RdbPlanDicI" runat="server" GroupName="DicP" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label52" runat="server" Text="R:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbRealDicA" runat="server" GroupName="DicR" />
                                                <asp:RadioButton ID="RdbRealDicI" runat="server" GroupName="DicR" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanAnualA" runat="server" GroupName="Anual" />
                                                <asp:RadioButton ID="RdbPlanAnualI" runat="server" GroupName="Anual" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                 <td width="70">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="P:" Width="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="RdbPlanAjustesA" runat="server" GroupName="Ajustes" />
                                                <asp:RadioButton ID="RdbPlanAjustesI" runat="server" GroupName="Ajustes" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
    </div>
    <div style="text-align: center;">
        <asp:Button ID="BtnCambiosMensual" runat="server" Text="Grabar Cambios Mensual" Height="26px" />
    </div>
</asp:Content>
