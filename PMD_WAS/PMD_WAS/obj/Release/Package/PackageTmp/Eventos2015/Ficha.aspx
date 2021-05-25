<%@ Page Language="VB" AutoEventWireup="false" Title="Ficha Evento" Inherits="PMD_WAS.Ficha" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Codebehind="Ficha.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache VaryByParam="none" Duration="1" Location="Client" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="estilo_ficha.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function alerta_Alta_Exito() {

            mensajeCustom('success', 'Proceso correcto', 'Alta con éxito!!');
        }

        function alerta_Alta_Error() {
            mensajeCustom('error', 'ERROR', 'No se guardo la información!!');
        }

        function alerta_Alta_Error_PuntoMapa() {

            mensajeCustom('error', 'ERROR', 'No se guardo:PUNTO EN MAPA');
        }

        function alerta_Alta_Error_DatosFaltantes() {
            mensajeCustom('error', 'ERROR', 'Favor de capturar los datos!!');
        }

        function alerta_Alta_Error_NoExisteFolio() {

            mensajeCustom('error', 'ERROR FOLIO EVENTO', 'No existe el Folio Capturado!!');
        }


        function alerta_Alta_Error_Hora() {

            mensajeCustom('warning', 'Favor de validar', 'Recuerde el formato de Hora, desde 00:00 hasta 23:59');
        }

        function alerta_Ficha_ErrorFolioIngresado() {
            mensajeCustom('error', 'ERROR', 'Favor de ingresar el número de folio a Consultar.');
        }

        function alerta_Ficha_ActDiferente() {

            mensajeCustom('error', 'ERROR', 'Favor de agregar una actividad diferente.');

        }

        function alertaEnviaCorreo() {
            mensajeCustom('success', 'E-m@il!!', 'Mensaje Enviado!!!');
        }

        function alertaErrorEnvioCorreo() {
            mensajeCustom('error', 'E-m@il!!', 'No se envio correo,vuelva a intentarlo!!!');
        }


        function alerta_Ficha_NoHaSidoValidada() {
            mensajeCustom('warning', 'VALIDACION', 'La solicitud de evento aún no ha sido validada por Relaciones Públicas!!');
        }

        function alerta_Ficha_NoHaSidoAprobada() {
            mensajeCustom('warning', 'VALIDACION', 'La solicitud de evento no fue aprobada por Relaciones Públicas!!');
        }

        //var map; //importante definirla fuera de la funcion initialize() para poderla usar desde otras funciones.
        //var infowindow = new google.maps.InfoWindow({ maxWidth: 200 });
        //var poly;
        //var Polyx;
        //var PxLngLat = [];
        //var Munx;
        //function initialize() {

        //    var Lx1 = document.getElementById('TextLng').value;
        //    var Lx2 = document.getElementById('TextLat').value;
        //    if (Lx1 == '') {

        //        Munx = new google.maps.LatLng(25.754757464040086, -100.2895021203766);
        //        var mapOptions = {
        //            zoom: 16,
        //            center: Munx,
        //            mapTypeId: google.maps.MapTypeId.SATELLITE
        //        };


        //        map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
        //        navigator.geolocation.getCurrentPosition(pedirPosicion);

        //    } else {

        //        Munx = new google.maps.LatLng(Lx2, Lx1);
        //        var mapOptions = {
        //            zoom: 16,
        //            center: Munx,
        //            mapTypeId: google.maps.MapTypeId.ROADMAP
        //        };
        //        GeneraPuntos();

        //        map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);

        //        ////

        //        var titulo = 'Punto';
        //        var latlng = new google.maps.LatLng(Lx2, Lx1);
        //        map.setCenter(latlng);
        //        marcador2 = new google.maps.Marker({
        //            position: latlng,
        //            draggable: true,
        //            map: map,
        //            title: titulo
        //        });
        //        ////

        //        Polyx = new google.maps.Polygon({
        //            paths: PxLngLat,
        //            strokeColor: "#6666FF",
        //            strokeOpacity: 0.8,
        //            strokeWeight: 2,
        //            fillColor: "#3366CC",
        //            fillOpacity: 0.15
        //        });
        //        Polyx.setMap(map);

        //        google.maps.event.addListener(marcador2, 'dragend', function () {
        //            var Puntos = marcador2.getPosition();
        //            var Latx = Puntos.lat();
        //            var Lonx = Puntos.lng();
        //            //alert(Latx + ',' + Lonx);
        //            document.getElementById('txt_coord_x').value = Latx;
        //            document.getElementById('txt_coord_y').value = Lonx;




        //        });
        //    }
        //}

        function OpenPopupx() {

            var rx = document.getElementById('txt_coord_x').value;
            var ry = document.getElementById('txt_coord_y').value;
            var link = "Variables.aspx?vx=" + rx + "&vy=" + ry;
            window.open(link, "prueba", "location=1,status=1,scrollbars=yes,resizable=yes,left=2000,top=2000,width=5px,height=5px,titlebar=0,toolbar=0,menubar=0");
            return false;

        }


        function pedirPosicion(pos) {

            //var centro = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
            //map.setCenter(centro); //pedimos que centre el mapa..
            //map.setMapTypeId(google.maps.MapTypeId.ROADMAP); //y lo volvemos un mapa callejero
            //MarcaPunto(pos);
        }


        function geolocalizame() {

            initialize();


        }

        function showArrays(event) {
            //var vertices = this.getPath();
            //var contentString = "<b>Bermuda Triangle Polygon</b><br />";
            //contentString += "Clicked Location: <br />" + event.latLng.lat() + "," + event.latLng.lng() + "<br />";
            //infowindow.setContent(contentString);
            //infowindow.setPosition(event.latLng);
            //infowindow.open(map);

        }

        function GeneraPuntos() {

            //cont = 0;
            //var ix = 0;
            //var cad = "";
            //var iniciar = 0;
            //var entrox = 0;
            //var CadRx = "";
            //var cadena = document.getElementById('TextBox2').value;
            //var xIdx = "";
            //var SubCad = "";
            //var xPunto = "";

            //for (ix = 0; ix <= cadena.length; ix++) {
            //    cad = "";
            //    cad = cadena.substring(ix - 1, ix);
            //    if (cad == "*" && iniciar == 1) { //inicia Cadena
            //        iniciar = 0;
            //        xPunto = "";
            //        entrox = 0;
            //        xIdx = "";
            //        for (iy = 0; iy <= CadRx.length; iy++) {
            //            SubCad = "";
            //            SubCad = CadRx.substring(iy - 1, iy);

            //            if (SubCad == "(") {
            //                entrox = 1;
            //            }
            //            if (entrox == 1) {
            //                xPunto = xPunto + SubCad;
            //            }
            //            if (entrox == 0) {
            //                xIdx = xIdx + SubCad;
            //            }
            //        }

            //        xPunto = xPunto.replace("(", "")
            //        xPunto = xPunto.replace(")", "")

            //        var mytool_array = xPunto.split(",");
            //        //alert(mytool_array[0] + "-" + mytool_array[1] );
            //        var Longx = mytool_array[0];
            //        var Latx = mytool_array[1];
            //        //crear Puntos

            //        document.getElementById('TextLng').value = Longx;
            //        document.getElementById('TextLat').value = Latx;
            //        document.getElementById('txt_coord_x').value = Longx;
            //        document.getElementById('txt_coord_y').value = Latx;
            //        document.getElementById('TextTitulo').value = xIdx;

            //        if (xPunto.length == 21) {

            //            addLatLng();
            //            cont = cont + 1;
            //        }
            //        cadRx = "";
            //    }
            //    if (cad == "*" && iniciar == 0) { //inicia Cadena
            //        iniciar = 1;
            //        CadRx = ""
            //    }
            //    if (cad != "*" && iniciar == 1) {
            //        CadRx = CadRx + cad;
            //    }
            //}

        }

        function addLatLng() {
            //var txt1 = document.getElementById('txt_coord_x').value;
            //var txt2 = document.getElementById('txt_coord_y').value;
            //var txtTx = document.getElementById('TextTitulo').value;
            //var lat = txt1;
            //var lng = txt2;
            //var latlng = new google.maps.LatLng(lat, lng);
            //PxLngLat.push(latlng);
        }



        function MarcaPunto(pos) {

            ////alert("¡Hola! Estas en : " + pos.coords.latitude + "," + pos.coords.longitude + " Rango de localización de +/- " + pos.coords.accuracy + " metros");
            //var Lx1 = document.getElementById('TextLng').value;
            //var Lx2 = document.getElementById('TextLat').value;

            //if (Lx1 == '') {
            //    var txt1 = pos.coords.latitude;
            //    var txt2 = pos.coords.longitude;
            //}
            //else {
            //    var txt1 = document.getElementById('TextLat').value;
            //    var txt2 = document.getElementById('TextLng').value;
            //}

            //var titulo = 'Punto';
            //var textoInfo = 'Información';

            //var lat = parseFloat(txt1);
            //var lng = parseFloat(txt2);
            //document.getElementById('txt_coord_x').value = lat;
            //document.getElementById('txt_coord_y').value = lng;

            //var latlng = new google.maps.LatLng(lat, lng);
            //map.setCenter(latlng);
            //marcador = new google.maps.Marker({
            //    position: latlng,
            //    draggable: true,
            //    map: map,
            //    title: titulo
            //});

            ////google.maps.event.addListener(marcador, 'click', function () { //esto es para cuando sea click en el marcador
            //google.maps.event.addListener(marcador, 'dragend', function () { //para cuando se arrastre el marcador
            //    var Puntos = marcador.getPosition();
            //    var Latx = Puntos.lat();
            //    var Lonx = Puntos.lng();
            //    document.getElementById('txt_coord_x').value = Latx;
            //    document.getElementById('txt_coord_y').value = Lonx;


            //});
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




//        function CheckAllEmp(Checkbox) {
//            var GridView3 = document.getElementById("<%=GridView3.ClientID %>");
//            for (i = 1; i < GridView3.rows.length; i++) {
//                GridView3.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
//            }
//        }
//   





    </script>
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
    </style>

    <div class="container-fluid">
        <div class="row">
            <div class="col-12 col-md-6">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Folio Evento:</h6>
                            <asp:TextBox ID="txtFolio" runat="server" placeholder="INGRESE FOLIO" CssClass="form-control"
                                Font-Size="small" onkeypress="javascript:return solonumeros(event)"
                                AutoPostBack="True" TabIndex="1"></asp:TextBox>
                            <asp:Label ID="lblFolioEvento" runat="server" Text="" Width="270px"></asp:Label>
                        </div>
                        <div class="col-12 col-md-6">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Antecedentes y Objetivo:</h6>
                            <asp:TextBox ID="txtAntecedentes" runat="server" Height="90px" TextMode="MultiLine" CssClass="form-control" onkeypress="javascript:return  solonumletrasMayespacios(event)" Enabled="False" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Vestimenta:</h6>
                            <asp:DropDownList ID="drpVestimenta" runat="server" CssClass="form-control" Enabled="False" TabIndex="3"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-5">
                            <h6 class="subtitle">Hora</h6>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtHr" runat="server" size="5" Enabled="False" onkeypress="javascript:return solonumeros(event)" CssClass="form-control"
                                        onkeyup="mascara(this,':',patron,true)" MaxLength="5"
                                        AutoPostBack="True" TabIndex="4"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-5">
                            <h6 class="subtitle">Programa</h6>
                            <asp:TextBox ID="txtActividad" runat="server" Width="340px" Height="35px"
                                TextMode="MultiLine"
                                onkeypress="javascript:return solonumletrasMayespacios(event)" Enabled="False"
                                TabIndex="5"></asp:TextBox>
                        </div>
                        <div class="col-2 text-center">
                            <h6 style="visibility: hidden;" class="subtitle">Programa</h6>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/agregar-icono-7533-128.png"
                                Height="25" Width="30" TabIndex="6" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-hover table-bordered" CellPadding="0" CellSpacing="0"
                                        ForeColor="#333333" GridLines="None" AutoGenerateDeleteButton="True"
                                        Font-Size="X-Small">
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
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-6">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            <div id="divGridInvitados">
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0"
                                    Width="100%" Enabled="False" TabIndex="6">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>

                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="8" align="center">INVITADOS
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td width="10%" align="left">
                                                            <asp:CheckBox ID="chkBxHeader" runat="server" Text="TODOS" OnCheckedChanged="Checked" AutoPostBack="true" />
                                                        </td>
                                                        <td width="60%" align="left">SECRETARIA
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <table width="100%">
                                                    <tr>

                                                        <td width="10%" align="left">
                                                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Eval("estatus") %>' />
                                                            <%-- checked =false--%>
                                                            <asp:Label ID="lblidsecr" runat="server" Width="10%" Text='<%# eval("id_secr")%>' Visible="False"></asp:Label>
                                                        </td>
                                                        <td width="60%" align="left">
                                                            <asp:Label ID="lblsecr" runat="server" Text='<%# eval("secretaria")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-12">
                            <h5>INVITADO ESPECIAL</h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Empresa:</h6>
                            <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control" onkeypress="javascript:return  solonumletrasMayespacios(event)" Enabled="False" TabIndex="7"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Nombre:</h6>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" onkeypress="javascript:return  solonumletrasMayespacios(event)" Enabled="False" TabIndex="8"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Puesto:</h6>
                            <asp:TextBox ID="txtPuesto" runat="server" CssClass="form-control" onkeypress="javascript:return  solonumletrasMayespacios(event)" Enabled="False" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Tel:</h6>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" onkeypress="javascript:return  solonumeros(event)" Enabled="False" MaxLength="10" TabIndex="10"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle">Contacto:</h6>
                            <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control" onkeypress="javascript:return  solonumletrasMayespacios(event)" Enabled="False" TabIndex="11"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <h6 class="subtitle" style="visibility: hidden;">Contacto:</h6>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="images/agregar-icono-7533-128.png"
                                Height="25" Width="30" TabIndex="12" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView2" runat="server" Width="100%" Height="90px" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AutoGenerateDeleteButton="True"
                                        Font-Size="X-Small">
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
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 text-right">
                <asp:Button ID="btnGuardar" CssClass="btn btn-secondary" runat="server" Text="Guardar"
                    TabIndex="13" />
                <asp:Button ID="btnCancelar" CssClass="btn btn-secondary" runat="server" Text="Cancelar"
                    TabIndex="14" />
            </div>
        </div>
    </div>


    <div id="divusuarioFicha">
        <asp:Label ID="Label4" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaFicha">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
</asp:Content>
