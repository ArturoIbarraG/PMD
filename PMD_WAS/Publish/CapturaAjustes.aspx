<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" Inherits="PMD_WAS.CapturaAjustes" Codebehind="CapturaAjustes.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--*****************ESTO ES PARA QUE MUESTRE EL SEXY ALERT CUANDO CARGA LA PAGINA **************************--%>
    <%--**(Los scripts se encuentran en la carpeta SexyAlert y hago referencia a ellos en la pagina maestra**--%>
    <script type="text/javascript">
        window.addEvent('domready', function () {
            Sexy = new SexyAlertBox();
            Sexy.info("<b>Bienvenido!</b> <p>Recuerda Revisar Tanto Tus Líneas Programadas Como Tus Líneas Por Solicitud</p>");
        });
    </script>
        <script type="text/javascript">
            function Comentarios() {
                window.addEvent('domready', function () {
                    Sexy = new SexyAlertBox();
                    Sexy.alert("<b> Comentarios </b> <p>Para guardar la Línea con su ajuste, favor de poner sus observaciones</p>");
                });
            }
          
    </script>


    <script type="text/javascript">
        window.addEvent('domready', function () {
            var Sexy = new SexyAlertBox();
        });
    </script>
    <%--*******************************************--%>
    <script type="text/javascript">
        document.onload = function () {

        }
        function CompararCantidades() {
            alert("Cantidades No Coinciden ")
        }
    </script>
 <%--   <script type="text/javascript" language="javascript">
        window.onload = function () {
            var obj = document.getElementById('MainContent_GridView1_txt1_0');
            SumaRealP(obj, 1);
            SumaPlaneado(obj, 1);
        }
    </script>--%>
    <%--*******************************************--%>
    <script type="text/javascript">
        function ProcExitoso1() {
            alert("Se Ha Guardado Exitosamente")
        }
        function Nota() {
            alert("Falta Capturar El Anual De esta Dirección")
        }
    </script>
    <script type="text/javascript">
        function ValidarCadenaExpReg() {
            cadena = "^[A-Z]|[a-z]";
            re = new RegExp(cadena);

            if (document.getElementById("t").value.match(re))
                alert("Aceptado");
            else
                alert("Rechazado");
        }
    </script>
    <link href="Css/BorderTable.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="text-align: left; margin: 0px 0px 20px 0px">
        <asp:Label ID="Label23" Width="250px" ForeColor="Black" runat="server" Font-Size="17"
            Text="Captura De Información"></asp:Label>
        <asp:Label ID="LabelMes" ForeColor="Black" runat="server" Text="Label" Font-Size="17">  </asp:Label>
        <asp:Label ID="LblAño" ForeColor="Black" runat="server" Text="Label" Font-Size="17">  </asp:Label>
    </div>
    <div style="text-align: center; margin-bottom: 30px">
        <asp:Label ID="lblSecretaria" runat="server" Text="Label" Font-Size="25">   </asp:Label><br />
        <asp:Label ID="LblDireccion" runat="server" Text="Label" Font-Size="17pt"></asp:Label><br />
    </div>
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div style="text-align: center; margin-bottom: 30px">
        <asp:Label ID="Mensaje" runat="server" ForeColor="Red" Font-Size="Medium"> * Campos Obligatorios</asp:Label>
    </div>
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Width="100%">
        <asp:TabPanel runat="server" HeaderText="Líneas Programadas" ID="TabPanel1">
            <ContentTemplate>
                <asp:UpdatePanel ID="updGrid" runat="server"><ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" RowStyle-BorderStyle="Solid"
                            Style="margin: 2% 0% 0% 5% ">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="3">
                                                    Información
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70px;">
                                                    ID
                                                </td>
                                                <td style="width: 600px;">
                                                    Estrategia, Línea y SubLínea
                                                </td>
                                                <td style="width: 120px;">
                                                    Unidad de Medida
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 70px;">
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </td>
                                                <td style="width: 600px;">
                                                    <asp:Label ID="lblEstrategia" runat="server" Text='<%# Eval("Descr") %>'></asp:Label>
                                                </td>
                                                <td style="width: 140px; text-align: center;">
                                                    <asp:Label ID="lblUnidadMedida" runat="server" Text='<%# Eval("Unidad_Medida") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ajuste">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAjuste"  runat="server"
                                            Text='<%# Eval("Ajuste") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:Label runat="server" Text="%"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txtAjuste" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:HiddenField ID="HiddenComP" Value='<%# Eval("Com") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observaciones">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCom" runat="server" text='<%# Eval("Com") %>' TextMode="MultiLine" Width="130px" onkeyup="ValidarCadenaExpReg()"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table width="1000px">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblSD" runat="server" ForeColor="Blue" Text="Sin Datos" Font-Size="Large"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
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
                    
</ContentTemplate>
</asp:UpdatePanel>







                <div style="text-align: center; margin-top: 30px">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <a style="margin-right: 250px;" href="javascript:window.history.back();">« Volver atrás</a><asp:Button
                                ID="Button1" runat="server" Text="Guardar" Style="width: 100px" />
                        
</ContentTemplate>
</asp:UpdatePanel>







                </div>
            
</ContentTemplate>
        






</asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Líneas Por Solicitud" >
            <ContentTemplate>
               
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="margin: 2% 0% 0% 5% ">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="3">
                                                    Información
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 80px;">
                                                    ID
                                                </td>
                                                <td style="width: 600px;">
                                                    Estrategia, Línea y SubLínea
                                                </td>
                                                <td style="width: 120px;">
                                                    Unidad de Medida
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 70px;">
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </td>
                                                <td style="width: 600px;">
                                                    <asp:Label ID="lblEstrategia" runat="server" Text='<%# Eval("Descr") %>'></asp:Label>
                                                </td>
                                                <td style="width: 140px; text-align: center;">
                                                    <asp:Label ID="lblUnidadMedida" runat="server" Text='<%# Eval("Unidad_Medida") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ajuste">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAjuste" onkeyup="javascript:SumaPlaneado(this, '1');" runat="server"
                                            Text='<%# Eval("Ajuste") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                          <asp:Label ID="Label1" runat="server" Text="%"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txtAjuste" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:HiddenField ID="Hidden2ComP" Value='<%# Eval("Com") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observaciones" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCom" runat="server" text='<%# Eval("Com") %>' TextMode="MultiLine" Width="130px"></asp:TextBox><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <table width="1000px">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblSD" runat="server" ForeColor="Blue" Text="Sin Datos" Font-Size="Large"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="text-align: center; margin-top: 30px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <a style="margin-right: 250px;" href="javascript:window.history.back();">« Volver atrás</a>
                            <asp:Button ID="Button2" runat="server" Text="Guardar" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            
</ContentTemplate>
        






</asp:TabPanel>
    </asp:TabContainer>
    <br />
</asp:Content>
