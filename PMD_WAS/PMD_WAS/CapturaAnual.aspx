<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" Inherits="PMD_WAS.CapturaAnual" Codebehind="CapturaAnual.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function ProcExitoso1() {
            alert("Se Ha Guardado Exitosamente")
        }
    </script>
    <script type="text/javascript">
        function SumaPlaneado(obj, id) {
            var t1 = obj.id.replace('txt' + id, 'txt1');
            var t2 = obj.id.replace('txt' + id, 'txt2');
            var t3 = obj.id.replace('txt' + id, 'txt3');
            var t4 = obj.id.replace('txt' + id, 'txt4');
            var t5 = obj.id.replace('txt' + id, 'txt5');
            var t6 = obj.id.replace('txt' + id, 'txt6');
            var t7 = obj.id.replace('txt' + id, 'txt7');
            var t8 = obj.id.replace('txt' + id, 'txt8');
            var t9 = obj.id.replace('txt' + id, 'txt9');
            var t10 = obj.id.replace('txt' + id, 'txt10');
            var t11 = obj.id.replace('txt' + id, 'txt11');
            var t12 = obj.id.replace('txt' + id, 'txt12');
            var l = obj.id.replace('txt' + id, 'LblTotalP');


            var txt1 = document.getElementById(t1).value;
            var txt2 = document.getElementById(t2).value;
            var txt3 = document.getElementById(t3).value;
            var txt4 = document.getElementById(t4).value;
            var txt5 = document.getElementById(t5).value;
            var txt6 = document.getElementById(t6).value;
            var txt7 = document.getElementById(t7).value; 
            var txt8 = document.getElementById(t8).value;
            var txt9 = document.getElementById(t9).value;
            var txt10 = document.getElementById(t10).value;
            var txt11 = document.getElementById(t11).value;
            var txt12 = document.getElementById(t12).value;

            document.getElementById(l).innerHTML = parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5) + parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10) + parseFloat(txt11) + parseFloat(txt12);
            var numero = redondeo2decimales(document.getElementById(l).innerHTML);
            document.getElementById(l).innerHTML = numero

        }
    </script>
    <script type="text/javascript">
    
        function PorcentajePlaneado(obj, id) {
            var t1 = obj.id.replace('txt' + id, 'txt1');
            var t2 = obj.id.replace('txt' + id, 'txt2');
            var t3 = obj.id.replace('txt' + id, 'txt3');
            var t4 = obj.id.replace('txt' + id, 'txt4');
            var t5 = obj.id.replace('txt' + id, 'txt5');
            var t6 = obj.id.replace('txt' + id, 'txt6');
            var t7 = obj.id.replace('txt' + id, 'txt7');
            var t8 = obj.id.replace('txt' + id, 'txt8');
            var t9 = obj.id.replace('txt' + id, 'txt9');
            var t10 = obj.id.replace('txt' + id, 'txt10');
            var t11 = obj.id.replace('txt' + id, 'txt11');
            var t12 = obj.id.replace('txt' + id, 'txt12');
            var lp = obj.id.replace('txt' + id, 'LblTotalPP');

            


            var txt1 = document.getElementById(t1).value;
            var txt2 = document.getElementById(t2).value;
            var txt3 = document.getElementById(t3).value;
            var txt4 = document.getElementById(t4).value;
            var txt5 = document.getElementById(t5).value;
            var txt6 = document.getElementById(t6).value;
            var txt7 = document.getElementById(t7).value;
            var txt8 = document.getElementById(t8).value;
            var txt9 = document.getElementById(t9).value;
            var txt10 = document.getElementById(t10).value;
            var txt11 = document.getElementById(t11).value;
            var txt12 = document.getElementById(t12).value;


            var x = 0;
            if (parseFloat(txt1) == 0) {
                x= x + 1;

            }
            if (parseFloat(txt2) == 0) {
                x = x + 1;
            }
            if (parseFloat(txt3) == 0) {
                x = x + 1;
            }
            if (parseFloat(txt4) == 0) {
                x = x + 1;
            }
             if (parseFloat(txt5) == 0) {
                 x = x + 1;
            }
            if (parseFloat(txt6) == 0) {
                x = x + 1;
            }
            if (parseFloat(txt7) == 0) {
                x = x + 1;
            }
            if (parseFloat(txt8) == 0) {
                x = x + 1;
            }
             if (parseFloat(txt9) == 0) {
                 x = x + 1;
            }
            if (parseFloat(txt10) == 0) {
                x = x + 1;
            }
            if (parseFloat(txt11) == 0) {
                x = x + 1;
            }
            if (parseFloat(txt12) == 0) {
                x = x + 1;
            }

           var resultado = (12 - x);
          
        //alert("El resultado de la resta es: " + resultado) 

         //El numero que arroja la variable resultado es por el que se tendra que dividir la suma de los text que esten llenos  
            document.getElementById(lp).innerHTML = (parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5) + parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10) + parseFloat(txt11) + parseFloat(txt12)) / resultado;
            var numero = redondeo2decimales(document.getElementById(lp).innerHTML);
            document.getElementById(lp).innerHTML = numero

            //Si la variable da 0 es porque hay puros ceros en los textbox entonces para que no marque el NAN le asignaremos un 0 
            if (resultado == 0) {
                //alert("entro");
                document.getElementById(lp).innerHTML = 0;
            }
        
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
     <div style="text-align: left; margin: 0px 0px 20px 0px">
        <asp:Label ID="Label23" Width="250px" ForeColor="Black" runat="server" Font-Size="17"
            Text="Captura De Información"></asp:Label>
        <asp:Label ID="LabelMes" ForeColor="Black" runat="server" Text="Label" Font-Size="17">  </asp:Label>
        <asp:Label ID="LblAño" ForeColor="Black" runat="server" Text="Label" Font-Size="17">  </asp:Label>
        </div>
    <div style="text-align: center; margin-bottom: 30px">
        <asp:Label ID="lblSecretaria" runat="server" Text="Label" Font-Size="25">   </asp:Label><br />
        <asp:Label ID="LblArea" runat="server" Text="Label" Font-Size="17">   </asp:Label><br />
    </div>
   
          <div style="text-align: center; margin-bottom: 30px">
        <asp:Label ID="Mensaje" runat="server" ForeColor="Red" Font-Size="Medium"> * Campos Obligatorios</asp:Label>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td colspan="17">
                                Líneas:
                                <asp:Label ID="txt_tipo_linea" runat="server" Text="LÍNEAS PROGRAMADAS" ForeColor="White"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="80">
                                <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                            </td>
                            <td align="center" width="170">
                                <asp:Label ID="Label4" runat="server" Text="Estrategia, Línea y Sublínea" Font-Size="Smaller"></asp:Label>
                            </td>
                            <td align="center" width="70">
                                <asp:Label ID="Label5" runat="server" Text="Unidad De Medida" Font-Size="Smaller"></asp:Label>
                            </td>
                            <td align="center" width="40">
                                <asp:Label ID="Label7" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td align="center" width="50">
                                <asp:Label ID="Label6" runat="server" Text="Costo Anual"></asp:Label>
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
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("ID") %>' Font-Size="8" Width="80"> </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descr") %>'
                                    Font-Size="8" Width="170"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblMedida" runat="server" Text='<%# Eval("Unidad_Medida") %>' Font-Size="8"
                                    Width="70"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblPlaneado" runat="server" Text="P" Width="40" Height="40px"></asp:Label>
                            </td>
                             <td width="50">
                                <asp:TextBox ID="txtCostoAnual" runat="server"
                                    Text='<%# Eval("CostoAnual") %>' Width="48" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txtCostoAnual" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <%If Habilita = "1" Then%>
                            <td width="43">
                                <asp:TextBox ID="txt1" onkeyup="javascript:SumaPlaneado(this, '1');" runat="server"
                                    Text='<%# Eval("Enero")  %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt1" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt2" onkeyup="javascript:SumaPlaneado(this, '2');" runat="server"
                                    Text='<%# Eval("Febrero") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt2" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt3" onkeyup="javascript:SumaPlaneado(this, '3');" runat="server"
                                    Text='<%# Eval("Marzo") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt3" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt4" onkeyup="javascript:SumaPlaneado(this, '4');" runat="server"
                                    Text='<%# Eval("Abril") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt4" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt5" onkeyup="javascript:SumaPlaneado(this, '5');" runat="server"
                                    Text='<%# Eval("Mayo") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt5" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt6" onkeyup="javascript:SumaPlaneado(this, '6');" runat="server"
                                    Text='<%# Eval("Junio") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt6" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt7" onkeyup="javascript:SumaPlaneado(this, '7');" runat="server"
                                    Text='<%# Eval("Julio") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt7" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt8" onkeyup="javascript:SumaPlaneado(this, '8');" runat="server"
                                    Text='<%# Eval("Agosto") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt8" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt9" onkeyup="javascript:SumaPlaneado(this, '9');" runat="server"
                                    Text='<%# Eval("Septiembre") %>' Width="40"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt9" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt10" onkeyup="javascript:SumaPlaneado(this, '10');" runat="server"
                                    Text='<%# Eval("Octubre") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt10" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt11" onkeyup="javascript:SumaPlaneado(this, '11');" runat="server"
                                    Text='<%# Eval("Noviembre") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt11" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt12" onkeyup="javascript:SumaPlaneado(this, '12');" runat="server"
                                    Text='<%# Eval("Diciembre") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt12" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="50" align="center">
                                <asp:Label ID="LblTotalP" Height="40px" runat="server" Text='<%# Eval("Acumulado") %>' ></asp:Label>
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
    <div style="margin: 30px 0px 20px 0px;">
    </div>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <table>
                        <td colspan="17">
                            Líneas:
                            <asp:Label ID="txt_tipo_linea" runat="server" Text="LÍNEAS POR SOLICITUD" ForeColor="White"></asp:Label>
                        </td>
                        <tr>
                        </tr>
                        <tr>
                            <td align="center" width="80">
                                <asp:Label ID="Label3" runat="server" Text="ID"></asp:Label>
                            </td>
                            <td align="center" width="170">
                                <asp:Label ID="Label4" runat="server" Text="Estrategia, Línea y Sublínea" Font-Size="Smaller"></asp:Label>
                            </td>
                            <td align="center" width="70">
                                <asp:Label ID="Label5" runat="server" Text="Unidad De Medida" Font-Size="Smaller"></asp:Label>
                            </td>
                            <td align="center" width="40">
                                <asp:Label ID="Label7" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td align="center" width="50">
                                <asp:Label ID="Label8" runat="server" Text="Costo Anual"></asp:Label>
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
                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("ID") %>' Font-Size="8" Width="80"> </asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descr") %>'
                                    Font-Size="8" Width="170"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblMedida" runat="server" Text='<%# Eval("Unidad_Medida") %>' Font-Size="8"
                                    Width="70"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblPlaneado" runat="server" Text="P" Width="40" Height="40px"></asp:Label>
                            </td>
                             <td width="50">
                                <asp:TextBox ID="txtCostoAnual" runat="server"
                                    Text='<%# Eval("CostoAnual")  %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txtCostoAnual" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <%If Habilita = "1" Then%>
                            <td width="43">
                                <asp:TextBox ID="txt1" onkeyup="javascript:PorcentajePlaneado(this, '1');" runat="server"
                                    Text='<%# Eval("Enero")  %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt1" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt2" onkeyup="javascript:PorcentajePlaneado(this, '2');" runat="server"
                                    Text='<%# Eval("Febrero") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt2" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt3" onkeyup="javascript:PorcentajePlaneado(this, '3');" runat="server"
                                    Text='<%# Eval("Marzo") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt3" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt4" onkeyup="javascript:PorcentajePlaneado(this, '4');" runat="server"
                                    Text='<%# Eval("Abril") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt4" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt5" onkeyup="javascript:PorcentajePlaneado(this, '5');" runat="server"
                                    Text='<%# Eval("Mayo") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt5" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt6" onkeyup="javascript:PorcentajePlaneado(this, '6');" runat="server"
                                    Text='<%# Eval("Junio") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt6" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt7" onkeyup="javascript:PorcentajePlaneado(this, '7');" runat="server"
                                    Text='<%# Eval("Julio") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt7" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt8" onkeyup="javascript:PorcentajePlaneado(this, '8');" runat="server"
                                    Text='<%# Eval("Agosto") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt8" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt9" onkeyup="javascript:PorcentajePlaneado(this, '9');" runat="server"
                                    Text='<%# Eval("Septiembre") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt9" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt10" onkeyup="javascript:PorcentajePlaneado(this, '10');" runat="server"
                                    Text='<%# Eval("Octubre") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt10" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt11" onkeyup="javascript:PorcentajePlaneado(this, '11');" runat="server"
                                    Text='<%# Eval("Noviembre") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt11" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="43">
                                <asp:TextBox ID="txt12" onkeyup="javascript:PorcentajePlaneado(this, '12');" runat="server"
                                    Text='<%# Eval("Diciembre") %>' Width="40" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Introducir Valor"
                                            Text="*" ControlToValidate="txt12" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td width="30">
                                <asp:Label ID="LblTotalPP" height="40px" runat="server" Text='<%# Eval("Acumulado") %>' maxlength="4"
                                    Width="30"></asp:Label>
                            </td>
                            <td width="40" align="right">
                                <asp:Label ID="lblSigno" runat="server" height="40px" Text="%" Width="40"></asp:Label>
                            </td>
                            <%End If%>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle ForeColor="#003399" BackColor="White" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView>
    <%-- NOTA: Este update panel se utiliza para que al guardar los datos no Haga un postback en el aspx--%>
    <asp:UpdatePanel ID="Updte2" runat="server">
        <ContentTemplate>
            <div style="text-align: center; margin-top: 30px;">
                <a style="margin-right: 250px;" href="javascript:window.history.back();">&laquo; Volver
                    atrás</a>
                <asp:Button ID="Button1" Text="Guardar" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
