<%@ Page Language="VB" AutoEventWireup="false" Title="Evidencias" Inherits="PMD_WAS.Evidencias" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Codebehind="Evidencias.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache VaryByParam="none" Duration="1" Location="Client" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="Estilo_evidencias.css" rel="stylesheet" type="text/css" />
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <!----AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU-->
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>

    <style type="text/css">
        .style1 {
            width: 400px;
            height: 4px;
        }

        .style20 {
            width: 763px;
        }

        #divlblNombreOperador {
            width: 100px;
        }
    </style>
    <style type="text/css">
        #divlblfolioReq {
            width: 129px;
            height: 22px;
        }

        #divlblidReq {
            width: 75px;
        }

        #divlblReq {
            width: 114px;
        }

        #divdrpReq {
            height: 22px;
            width: 184px;
        }

        #divlblCantidad {
            width: 122px;
        }

        .style1 {
            width: 380px;
        }

        .table-responsive {
            max-height: 350px;
            width: 100%;
        }
    </style>

    <div class="container">
        <div class="row">
            <div class="col-12 col-md-6">
                <h6 class="subtitle">Fecha inicio:</h6>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-12 col-md-6">
                <h6 class="subtitle">Fecha fin:</h6>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-md-6">
                <h6 class="subtitle">Evento</h6>
                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-12 col-md-6 text-right">
                   <h6 class="subtitle" style="visibility:hidden;">Evento</h6>
                <asp:Button ID="Button1" runat="server" Text="Consultar" CssClass="btn btn-secondary" />
            </div>
        </div>
    </div>
    <hr />
    <div class="container-fluid">
        <div class="row">
            <div class="col-6">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333"
                        GridLines="None" Width="100%" Font-Size="Medium">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" width="50">
                                                <asp:Label ID="Label7" runat="server" Text="Folio"></asp:Label>
                                            </td>
                                            <td align="left" width="150">
                                                <asp:Label ID="Label11" runat="server" Text="Evento"></asp:Label>
                                            </td>
                                            <td align="left" width="150">
                                                <asp:Label ID="Label22" runat="server" Text="Lugar"></asp:Label>
                                            </td>
                                            <td align="left" width="80">
                                                <asp:Label ID="Label2" runat="server" Text="Fecha Evento"></asp:Label>
                                            </td>

                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <%-- <td align="center" width="60">
                                                        <asp:Label ID="LblFolioEvento" runat="server" Text='<%# Eval("Folio") %>'></asp:Label>
                                                    </td>--%>
                                            <td align="left" width="50">
                                                <asp:LinkButton ID="LblFolioEvento" runat="server" Text='<%#Eval("Folio")%>' Style="font-family: Candara;"
                                                    Width="30px" CommandName="Buscar1" CommandArgument='<%# Eval("Folio")%>' CssClass="Lin"></asp:LinkButton>
                                            </td>
                                            <td align="left" width="150">
                                                <asp:Label ID="lblEvento" runat="server" Text='<%# Eval("nombre_evento") %>'></asp:Label>
                                            </td>
                                            <td align="left" width="150">
                                                <asp:Label ID="lblLugar" runat="server" Text='<%# Eval("lugar") %>'></asp:Label>
                                            </td>

                                            <td align="left" width="80">
                                                <asp:Label ID="lblFechaEvento" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
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
            <div class="col-6">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Folio" Font-Bold="True"
                                ForeColor="#003399"></asp:Label></td>
                        <td class="style1">
                            <asp:Label ID="Label6" runat="server" ForeColor="#003399"></asp:Label></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Evento" Font-Bold="True"
                                ForeColor="#003399"></asp:Label></td>
                        <td class="style1">
                            <asp:Label ID="Label8" runat="server" ForeColor="#003399"></asp:Label></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Lugar" Font-Bold="True"
                                ForeColor="#003399"></asp:Label></td>
                        <td class="style1">
                            <asp:Label ID="Label9" runat="server" ForeColor="#003399"></asp:Label></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Fecha" Font-Bold="True"
                                ForeColor="#003399"></asp:Label></td>
                        <td class="style1">
                            <asp:Label ID="Label10" runat="server" ForeColor="#003399"></asp:Label></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label27" runat="server" Text="Foto" Font-Bold="True"
                                ForeColor="#003399"></asp:Label></td>
                        <td class="style1">
                            <asp:FileUpload ID="FileUpload1" runat="server" Enabled="False" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label28" runat="server" Text="Foto" Font-Bold="True"
                                ForeColor="#003399"></asp:Label></td>
                        <td class="style1">
                            <asp:Button ID="Button2" runat="server" Text="Nuevo" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="style1">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="style1">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333"
                    GridLines="None" Width="100%" Font-Size="small">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="left" width="50">
                                            <asp:Label ID="Label29" runat="server" Text="Folio"></asp:Label>
                                        </td>
                                        <td align="left" width="50">
                                            <asp:Label ID="Label30" runat="server" Text="Num"></asp:Label>
                                        </td>
                                        <td align="left" width="100">
                                            <asp:Label ID="Label31" runat="server" Text="Imagen"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td align="left" width="50">
                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("folio") %>'></asp:Label>
                                        </td>
                                        <td align="left" width="50">
                                            <asp:Label ID="lblEvento0" runat="server" Text='<%# Eval("cosec") %>'></asp:Label>
                                        </td>

                                        <td align="left" width="100">
                                            <asp:Image ID="ImageRx" runat="server" Height="80px" Width="80px" ImageUrl='<%# Eval("archivo") %>' />
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

    <script type="text/javascript">



        function alerta_Alta_Exito() {
            var Sexy = new SexyAlertBox();
            Sexy.alert("<b>Alta con éxito!!</b>");
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
            Sexy.error("<h1>ERROR</h1><p><b>Favor de capturar los datos!!</b></p>");
        }

        function alerta_Alta_Error_NoExisteFolio() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR FOLIO EVENTO</h1><p><b>No existe el Folio Capturado!!</b></p>");

        }


        function alerta_Alta_Error_Hora() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>Recuerde el formato de Hora, desde 00:00 hasta 23:59</b>");

        }

        function alerta_Ficha_ErrorFolioIngresado() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>Favor de ingresar el número de folio a Consultar.</b></p>");

        }

        function alerta_Ficha_ActDiferente() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<h1>ERROR</h1><p><b>Favor de agregar una actividad diferente.</b></p>");

        }

        function alertaEnviaCorreo() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>E-m@il!!</b> <p>Mensaje Enviado!!!</p>");

        }

        function alertaErrorEnvioCorreo() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<b>E-m@il!!</b> <p>No se envio correo,vuelva a intentarlo!!!</p>");

        }


        function alerta_Ficha_NoHaSidoValidada() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<h1>VALIDACION</h1><p><b>La solicitud de evento aún no ha sido validada por Relaciones Públicas!!</b></p>");
        }

        function alerta_Ficha_NoHaSidoAprobada() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<h1>VALIDACION</h1><p><b>La solicitud de evento no fue aprobada por Relaciones Públicas!!</b></p>");
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

        function isValidEmail(mail) {
            return /^\w+([\.\+\-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(mail);
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
</asp:Content>
