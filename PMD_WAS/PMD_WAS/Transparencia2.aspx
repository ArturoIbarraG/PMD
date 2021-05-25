<%@ Page Title="" Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.Transparencia2" Codebehind="Transparencia2.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  <link href="Css/UpdateProgress.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="margin: -2% 20% 0% 20%; text-align: center;">
        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Reporte De Transparencia"></asp:Label>
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
            <div id="DivDrop" style="text-align: left; font-size: 18px; margin-left:201px; margin-right:190px; border-bottom: 5px inset rgb(163, 156, 156);
                padding: 3px">
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
                        <%--Tarea:--%>
                        <asp:DropDownList ID="DropLinea" runat="server" Width="400px" AutoPostBack="True" Visible="false">
                        </asp:DropDownList>
                        <asp:CheckBox ID="CheckLinea" runat="server" AutoPostBack="True" Text="TODAS" Visible="false"/>
                    </div>
                    <div id="DivSublínea" style="margin: 0px 20px 0px 60px; text-align: left; font-size: medium;">
                        <%--SubLínea:--%>
                        <asp:DropDownList ID="DropSublinea" runat="server" Width="500px" AutoPostBack="True" Visible="false">
                        </asp:DropDownList>
                        <asp:CheckBox ID="CheckSubL" runat="server" AutoPostBack="True" Visible="false" Text="TODAS" />
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
           
               
            
          <h2 style=" border-bottom: 5px inset rgb(163, 156, 156); color:#0A6A6F; ">
                Líneas Programadas</h2>
            <div id="divg" style="text-align: center; margin: 3% 0% 2% 0%; overflow:auto; height:350px" >
                         <asp:GridView ID="GridView1" runat="server" CellPadding="4" GridLines="Horizontal" 
                    ForeColor="Black" BackColor="White" BorderColor="#CCCCCC" Width="99%"  AutoGenerateColumns="false"
                    BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black"/>
                    <HeaderStyle BackColor="#0A6A6F" Font-Bold="True" ForeColor="White"   />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table>
                                   
                                    <tr>
                                        <td align="center" width="80">
                                            <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label4" runat="server" 
                                                Text="Descripción"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label5" runat="server" 
                                                Text="Secretaría"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label7" runat="server" Text="Dirección"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label6" runat="server" Text="Clasificación"></asp:Label>
                                        </td>
                                        <td align="center" width="250">
                                            <asp:Label ID="Label9" runat="server" Text="Unidad de Medida"></asp:Label>
                                        </td>
                                        <td align="center" width="150">
                                            <asp:Label ID="Label11" runat="server" Text="Cantidad Planeada"></asp:Label>
                                        </td>
                                        <td align="center" width="150">
                                            <asp:Label ID="Label13" runat="server" Text="Cantidad Real"></asp:Label>
                                        </td>
                                        <td align="center" width="250">
                                            <asp:Label ID="Label1" runat="server" Text="Comentarios"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblId" runat="server" Font-Size="8" Text='<%# Eval("ID") %>' 
                                                Width="80"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescripcion" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Descr") %>' Width="200"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblMedida" runat="server" 
                                                Text='<%# Eval("Secretaría") %>' Width="200"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server"
                                                Text='<%# Eval("Dirección") %>' Width="200"></asp:Label>
                                        </td>
                                        <td width="200">
                                            <asp:Label ID="Label8" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Clasificación") %>' Width="200"></asp:Label>
                                        </td>
                                      
                                        <td width="250">
                                             <asp:Label ID="Label10" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Unidad_Medida") %>' Width="250"></asp:Label>
                                        </td>
                                        <td width="150">
                                           <asp:Label ID="Label12" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Planeado") %>' Width="150"></asp:Label>
                                        </td>
                                        <td width="150">
                                            <asp:Label ID="Label14" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Real") %>' Width="150"></asp:Label>
                                        </td>
                                      
                                        <td width="250">
                                            <asp:Label ID="Label15" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Comentarios") %>' Width="250"></asp:Label>
                                        </td>
                                         
                                        
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                      <table width="1000px"><tr><td align="center"><asp:Label ID="lblSD" runat="server" ForeColor="Blue" Text="Sin Datos" Font-Size="Large"></asp:Label></td></tr></table>
                     </EmptyDataTemplate>
                </asp:GridView>
            </div>
              </ContentTemplate>
    </asp:UpdatePanel>
            <h2 style=" border-bottom: 5px inset rgb(163, 156, 156); color:#0A6A6F;">
                Líneas Por solicitud</h2>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="div1" style="text-align: center; margin: 3% 0% 2% 0%;  overflow:scroll; height:650px"">
               
                <asp:GridView ID="GridView2" runat="server" CellPadding="4" GridLines="Horizontal" 
                    ForeColor="Black" BackColor="White" BorderColor="#CCCCCC" Width="99%"  AutoGenerateColumns="false"
                    BorderStyle="None" BorderWidth="1px">
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black"/>
                    <HeaderStyle BackColor="#0A6A6F" Font-Bold="True" ForeColor="White"   />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table>
                                   
                                    <tr>
                                        <td align="center" width="80">
                                            <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label4" runat="server" 
                                                Text="Descripción"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label5" runat="server" 
                                                Text="Secretaría"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label7" runat="server" Text="Dirección"></asp:Label>
                                        </td>
                                        <td align="center" width="250">
                                            <asp:Label ID="Label6" runat="server" Text="Clasificación"></asp:Label>
                                        </td>
                                        <td align="center" width="250">
                                            <asp:Label ID="Label9" runat="server" Text="Unidad de Medida"></asp:Label>
                                        </td>
                                        <td align="center" width="150">
                                            <asp:Label ID="Label11" runat="server" Text="Porcentaje Planeado"></asp:Label>
                                        </td>
                                        <td align="center" width="150">
                                            <asp:Label ID="Label13" runat="server" Text="Porcentaje Real"></asp:Label>
                                        </td>
                                        <td align="center" width="250">
                                            <asp:Label ID="Label1" runat="server" Text="Comentarios"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblId" runat="server" Font-Size="8" Text='<%# Eval("ID") %>' 
                                                Width="80"> </asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescripcion" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Descr") %>' Width="200"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblMedida" runat="server" 
                                                Text='<%# Eval("Secretaría") %>' Width="200"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label2" runat="server"
                                                Text='<%# Eval("Dirección") %>' Width="200"></asp:Label>
                                        </td>
                                        <td width="250">
                                            <asp:Label ID="Label8" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Clasificación") %>' Width="250"></asp:Label>
                                        </td>
                                      
                                        <td width="250">
                                             <asp:Label ID="Label10" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Unidad_Medida") %>' Width="250"></asp:Label>
                                        </td>
                                        <td width="150">
                                           <asp:Label ID="Label12" runat="server" Font-Size="8" 
                                                Text='<%# Eval("PorcentajePlaneado") %>' Width="150"></asp:Label>
                                        </td>
                                        <td width="150">
                                            <asp:Label ID="Label14" runat="server" Font-Size="8" 
                                                Text='<%# Eval("PorcentajeReal") %>' Width="150"></asp:Label>
                                        </td>
                                      
                                        <td width="250">
                                            <asp:Label ID="Label15" runat="server" Font-Size="8" 
                                                Text='<%# Eval("Comentarios") %>' Width="250"></asp:Label>
                                        </td>
                                         
                                        
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                      <table width="1000px"><tr><td align="center"><asp:Label ID="lblSD" runat="server" ForeColor="Blue" Text="Sin Datos" Font-Size="Large"></asp:Label></td></tr></table>
                     </EmptyDataTemplate>
                </asp:GridView>
               
            </div>
              </ContentTemplate>
    </asp:UpdatePanel>
     <div style=" text-align:right;"> <asp:Button runat="server" ID="btnExportar" Text="Exportar a Excel"/>
            </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
    </asp:UpdateProgress>
       
</asp:Content>

