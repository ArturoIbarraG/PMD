<%@ Page Language="VB" AutoEventWireup="false" Title="Consulta Estatus" Inherits="PMD_WAS.ConsultaEstatus" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Codebehind="ConsultaEstatus.aspx.vb" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="estilo_consultaestatus.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">



        function alerta_Validacion_Exito() {
            var Sexy = new SexyAlertBox();
            Sexy.alert("<b>Validadas con éxito!!</b>");

        }

        function alerta_NoHayInfo() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>EVENTOS</b> <p>No existen folios por mostrar</p>");

        }



        function alertaEnviaCorreo() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>E-m@il!!</b> <p>Mensaje Enviado!!!</p>");

        }

        function alertaErrorEnvioCorreo() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<b>E-m@il!!</b> <p>No se envio correo,vuelva a intentarlo!!!</p>");

        }


        function alerta_Validacion_AmbosCheck() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<b>Check Box</b> <p>Favor de Seleccionar solo 1 Opción 'Aprobar o  No Aprobar' !!</p>");

        }



        function alerta_Alta_Error() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>No se guardo la información!!</b></p>");
        }

        function alerta_Alta_Error_PuntoMapa() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>No se guardo:PUNTO EN MAPA</b></p>");

        }


        function alerta_Alta_Error_DatosFaltantes() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>Favor de capturar todos los datos!!</b></p>");

        }

        function alerta_Alta_Error_NoExisteFolio() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR FOLIO EVENTO</h1><p><b>No existe el Folio Capturado!!</b></p>");

        }


        function alerta_Alta_Error_Hora() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>Recuerde el formato de Hora, desde 00:00 hasta 23:59</b>");

        }
        function solonumeros(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if (key < 48 || key > 57) {
                return false;
            }

            return true;
        }

        // solo numeros con decimales
        function solonumerosdecimales(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if ((key < 46 && key > 46) && (key < 48 || key > 57)) {

                return false;
            }

            return true;
        }


        //No se captura nada//

        function nocaptura(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if (key < 0 || key > 0) {
                return false;
            }

            return true;
        }



        function solonumletras(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if ((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122)) {
                return true;
            }

            return false;
        }

        function solonumletrasMayMinyespacios(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            //46(.) Y 47 (/)
            if ((key >= 32 && key <= 32) || (key >= 45 && key <= 45) || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241)) {
                return true;
            }

            return false;
        }


        function solonumletrasMayespacios(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            //46(.) Y 47 (/)
            if ((key >= 32 && key <= 32) || (key >= 44 && key <= 46) || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241) || (key >= 193 && key <= 193) || (key >= 201 && key <= 201) || (key >= 205 && key <= 205) || (key >= 211 && key <= 211) || (key >= 218 && key <= 218) || (key >= 225 && key <= 225) || (key >= 233 && key <= 233) || (key >= 237 && key <= 237) || (key >= 243 && key <= 243) || (key >= 250 && key <= 250)) {
                return true;
            }

            return false;
        }


        function solonumletrasMay(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            //46(.) Y 47 (/) Y 45(-)
            if ((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241)) {
                return true;
            }

            return false;
        }


        function solonumerosparafecha(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if (key < 48 || key > 58) {
                return false;
            }

            return true;
        }



        var patron = new Array(2, 2)
        function mascara(d, sep, pat, nums) {
            if (d.valant != d.value) {
                val = d.value
                largo = val.length
                val = val.split(sep)
                val2 = ''
                for (r = 0; r < val.length; r++) {
                    val2 += val[r]
                }
                if (nums) {
                    for (z = 0; z < val2.length; z++) {
                        if (isNaN(val2.charAt(z))) {
                            letra = new RegExp(val2.charAt(z), "g")
                            val2 = val2.replace(letra, "")
                        }
                    }
                }
                val = ''
                val3 = new Array()
                for (s = 0; s < pat.length; s++) {
                    val3[s] = val2.substring(0, pat[s])
                    val2 = val2.substr(pat[s])
                }
                for (q = 0; q < val3.length; q++) {
                    if (q == 0) {
                        val = val3[q]
                    }
                    else {
                        if (val3[q] != "") {
                            val += sep + val3[q]
                        }
                    }
                }
                d.value = val
                d.valant = val
            }
            if (val.length == 5) {
                hora = val.split(":")
                if ((hora[0] > 23) || (hora[1] > 59))
                    alert("Recuerde el formato de Horas, desde 00:00 hasta 23:59 ")

            }

        }

    </script>

    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333"
                    GridLines="None"
                    Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="center" width="60">
                                            <asp:Label ID="Label7" runat="server" Text="Folio"></asp:Label>
                                        </td>

                                        <td align="left" width="500">
                                            <asp:Label ID="Label11" runat="server" Text="Evento"></asp:Label>
                                        </td>
                                        <td align="left" width="700">
                                            <asp:Label ID="Label22" runat="server" Text="Lugar"></asp:Label>
                                        </td>
                                        <td align="left" width="400">
                                            <asp:Label ID="Label2" runat="server" Text="Fecha Evento"></asp:Label>
                                        </td>
                                        <td align="left" width="200">
                                            <asp:Label ID="Label5" runat="server" Text="Hora Inicio"></asp:Label>
                                        </td>
                                        <td align="left" width="150">
                                            <asp:Label ID="Label6" runat="server" Text="Arribo Alcalde"></asp:Label>
                                        </td>
                                        <td align="center" width="150">
                                            <asp:Label ID="Label21" runat="server" Text="Salida Alcalde"></asp:Label>
                                        </td>
                                        <td align="center" width="200">
                                            <asp:Label ID="Label8" runat="server" Text="Hora Término"></asp:Label>
                                        </td>
                                        <td align="left" width="200">
                                            <asp:Label ID="Label16" runat="server" Text="Secretario"></asp:Label>
                                        </td>
                                        <td align="left" width="200">
                                            <asp:Label ID="Label17" runat="server" Text="Solicitante"></asp:Label>
                                        </td>
                                        <td align="left" width="200">
                                            <asp:Label ID="Label18" runat="server" Text="Telefono"></asp:Label>
                                        </td>

                                        <td align="right" width="50">
                                            <asp:Label ID="Label3" runat="server" Text="Estatus"></asp:Label>
                                        </td>


                                        <td align="center" width="600">
                                            <asp:Label ID="Label24" runat="server" Text="Comentario"></asp:Label>
                                        </td>

                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td align="left" width="30">
                                            <asp:Label ID="LblFolioEvento" runat="server" Text='<%# Eval("Folio") %>'></asp:Label>
                                        </td>
                                        <%--                                                    <td align="left" width="30">
                                                      <asp:LinkButton ID="LblFolioEvento" runat="server" Text='<%#Eval("Folio")%>' style="font-family:Candara;"
                                                    Width="30px" CommandName="Buscar1" CommandArgument='<%# Eval("Folio")%>' CssClass="Lin"></asp:LinkButton>
                                                    </td> --%>

                                        <td align="left" width="400">
                                            <asp:Label ID="lblEvento" runat="server" Text='<%# Eval("nombre_evento") %>'></asp:Label>
                                        </td>
                                        <td align="left" width="500">
                                            <asp:Label ID="lblLugar" runat="server" Text='<%# Eval("lugar") %>'></asp:Label>
                                        </td>

                                        <td align="left" width="60">
                                            <asp:Label ID="lblFechaEvento" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                                        </td>
                                        <td align="center" width="300">
                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("hr_ini") %>'></asp:Label>
                                        </td>
                                        <td align="right" width="200">
                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("hr_alcalde") %>'></asp:Label>
                                        </td>

                                        <td align="right" width="300">
                                            <asp:Label ID="Label20" runat="server" Text='<%# Eval("hr_salida") %>'></asp:Label>
                                        </td>

                                        <td align="right" width="600">
                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("hr_fin") %>'></asp:Label>
                                        </td>

                                        <td align="right" width="200">
                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("Secretario") %>'></asp:Label>
                                        </td>
                                        <td align="right" width="200">
                                            <asp:Label ID="Label14" runat="server" Text='<%# Eval("Solicitante") %>'></asp:Label>
                                        </td>
                                        <td align="right" width="80">
                                            <asp:Label ID="Label15" runat="server" Text='<%# Eval("Telefono") %>'></asp:Label>
                                        </td>

                                        <td align="center" width="500">

                                            <asp:Label ID="lblEstatus" runat="server" Text='<%#Eval("estatus") %>' Width="50"></asp:Label>

                                        </td>

                                        <td align="left" width="100">

                                            <asp:Label ID="lblComentario" runat="server" Text='<%#Eval("comentario") %>' Width="90px"></asp:Label>
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
            </div>
        </div>
    </div>


    <a href="http://apycom.com/" style="color: White">.</a>
    <div id="divusuarioVal">
        <asp:Label ID="Label4" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaVal">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
</asp:Content>
