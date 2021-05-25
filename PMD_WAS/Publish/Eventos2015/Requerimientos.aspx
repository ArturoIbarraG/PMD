<%@ Page Language="VB" AutoEventWireup="false" Title="Requerimientos" MasterPageFile="~/MasterGlobal.master"
    Inherits="PMD_WAS.Requerimientos" Codebehind="Requerimientos.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache VaryByParam="none" Duration="1" Location="Client" %>


<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="estilo_requerimiento.css" rel="stylesheet" type="text/css" />
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <style type="text/css">
        .container-border {
            border: 1px solid #AAA;
            padding: 10px;
        }

        .labelIndicador {
            min-height: 50px;
            height: auto;
            display: block;
            border: 1px solid #DDD;
            margin-bottom: 15px;
            margin-top: 6px;
            padding: 7px;
        }

        h5 {
            font-size: 1.4em;
            margin-bottom: 6px;
        }

        #MainContent_txtFechaInsta_CalendarExtender_container {
            z-index: 9999;
        }
    </style>
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
            mensajeCustom('error', 'ERROR', 'Favor de capturar los requerimientos!!');
        }

        function alerta_Alta_Error_NoExisteFolio() {

            mensajeCustom('error', 'ERROR FOLIO EVENTO', 'No existe el Folio Capturado!!');
        }


        function alerta_Alta_Error_Hora() {
            mensajeCustom('warning', 'Favor de validar', 'Recuerde el formato de Hora, desde 00:00 hasta 23:59');
        }


        function alertaErrorEnvioCorreo() {
            mensajeCustom('error', 'E-m@il!!', 'No se envio correo,vuelva a intentarlo!!!');
        }


        function alerta_Req_NoHaSidoValidada() {

            mensajeCustom('warning', 'VALIDACION', 'La solicitud de evento ya fue validada, no se puede modificar!!');
        }

        function alerta_Req_NoHaSidoAprobada() {
            mensajeCustom('warning', 'VALIDACION', 'La solicitud de evento no fue aprobada por Relaciones Públicas!!');
        }



        var map; //importante definirla fuera de la funcion initialize() para poderla usar desde otras funciones.
        var infowindow = new google.maps.InfoWindow({ maxWidth: 200 });
        var poly;
        var Polyx;
        var PxLngLat = [];
        var Munx;
        function initialize() {

            var Lx1 = document.getElementById('TextLng').value;
            var Lx2 = document.getElementById('TextLat').value;
            if (Lx1 == '') {

                Munx = new google.maps.LatLng(25.754757464040086, -100.2895021203766);
                var mapOptions = {
                    zoom: 16,
                    center: Munx,
                    mapTypeId: google.maps.MapTypeId.SATELLITE
                };


                map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
                navigator.geolocation.getCurrentPosition(pedirPosicion);

            } else {

                Munx = new google.maps.LatLng(Lx2, Lx1);
                var mapOptions = {
                    zoom: 16,
                    center: Munx,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                GeneraPuntos();

                map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);

                ////

                var titulo = 'Punto';
                var latlng = new google.maps.LatLng(Lx2, Lx1);
                map.setCenter(latlng);
                marcador2 = new google.maps.Marker({
                    position: latlng,
                    draggable: true,
                    map: map,
                    title: titulo
                });
                ////

                Polyx = new google.maps.Polygon({
                    paths: PxLngLat,
                    strokeColor: "#6666FF",
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: "#3366CC",
                    fillOpacity: 0.15
                });
                Polyx.setMap(map);

                google.maps.event.addListener(marcador2, 'dragend', function () {
                    var Puntos = marcador2.getPosition();
                    var Latx = Puntos.lat();
                    var Lonx = Puntos.lng();
                    //alert(Latx + ',' + Lonx);
                    document.getElementById('txt_coord_x').value = Latx;
                    document.getElementById('txt_coord_y').value = Lonx;




                });
            }
        }

        function OpenPopupx() {

            var rx = document.getElementById('txt_coord_x').value;
            var ry = document.getElementById('txt_coord_y').value;
            var link = "Variables.aspx?vx=" + rx + "&vy=" + ry;
            window.open(link, "prueba", "location=1,status=1,scrollbars=yes,resizable=yes,left=2000,top=2000,width=5px,height=5px,titlebar=0,toolbar=0,menubar=0");
            return false;

        }


        function pedirPosicion(pos) {

            var centro = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
            map.setCenter(centro); //pedimos que centre el mapa..
            map.setMapTypeId(google.maps.MapTypeId.ROADMAP); //y lo volvemos un mapa callejero
            MarcaPunto(pos);
        }


        function geolocalizame() {

            initialize();


        }

        function showArrays(event) {
            var vertices = this.getPath();
            var contentString = "<b>Bermuda Triangle Polygon</b><br />";
            contentString += "Clicked Location: <br />" + event.latLng.lat() + "," + event.latLng.lng() + "<br />";
            infowindow.setContent(contentString);
            infowindow.setPosition(event.latLng);
            infowindow.open(map);

        }

        function GeneraPuntos() {

            cont = 0;
            var ix = 0;
            var cad = "";
            var iniciar = 0;
            var entrox = 0;
            var CadRx = "";
            var cadena = document.getElementById('TextBox2').value;
            var xIdx = "";
            var SubCad = "";
            var xPunto = "";

            for (ix = 0; ix <= cadena.length; ix++) {
                cad = "";
                cad = cadena.substring(ix - 1, ix);
                if (cad == "*" && iniciar == 1) { //inicia Cadena
                    iniciar = 0;
                    xPunto = "";
                    entrox = 0;
                    xIdx = "";
                    for (iy = 0; iy <= CadRx.length; iy++) {
                        SubCad = "";
                        SubCad = CadRx.substring(iy - 1, iy);

                        if (SubCad == "(") {
                            entrox = 1;
                        }
                        if (entrox == 1) {
                            xPunto = xPunto + SubCad;
                        }
                        if (entrox == 0) {
                            xIdx = xIdx + SubCad;
                        }
                    }

                    xPunto = xPunto.replace("(", "")
                    xPunto = xPunto.replace(")", "")

                    var mytool_array = xPunto.split(",");
                    //alert(mytool_array[0] + "-" + mytool_array[1] );
                    var Longx = mytool_array[0];
                    var Latx = mytool_array[1];
                    //crear Puntos

                    document.getElementById('TextLng').value = Longx;
                    document.getElementById('TextLat').value = Latx;
                    document.getElementById('txt_coord_x').value = Longx;
                    document.getElementById('txt_coord_y').value = Latx;
                    document.getElementById('TextTitulo').value = xIdx;

                    if (xPunto.length == 21) {

                        addLatLng();
                        cont = cont + 1;
                    }
                    cadRx = "";
                }
                if (cad == "*" && iniciar == 0) { //inicia Cadena
                    iniciar = 1;
                    CadRx = ""
                }
                if (cad != "*" && iniciar == 1) {
                    CadRx = CadRx + cad;
                }
            }

        }

        function addLatLng() {
            var txt1 = document.getElementById('txt_coord_x').value;
            var txt2 = document.getElementById('txt_coord_y').value;
            var txtTx = document.getElementById('TextTitulo').value;
            var lat = txt1;
            var lng = txt2;
            var latlng = new google.maps.LatLng(lat, lng);
            PxLngLat.push(latlng);
        }



        function MarcaPunto(pos) {

            //alert("¡Hola! Estas en : " + pos.coords.latitude + "," + pos.coords.longitude + " Rango de localización de +/- " + pos.coords.accuracy + " metros");
            var Lx1 = document.getElementById('TextLng').value;
            var Lx2 = document.getElementById('TextLat').value;

            if (Lx1 == '') {
                var txt1 = pos.coords.latitude;
                var txt2 = pos.coords.longitude;
            }
            else {
                var txt1 = document.getElementById('TextLat').value;
                var txt2 = document.getElementById('TextLng').value;
            }

            var titulo = 'Punto';
            var textoInfo = 'Información';

            var lat = parseFloat(txt1);
            var lng = parseFloat(txt2);
            document.getElementById('txt_coord_x').value = lat;
            document.getElementById('txt_coord_y').value = lng;

            var latlng = new google.maps.LatLng(lat, lng);
            map.setCenter(latlng);
            marcador = new google.maps.Marker({
                position: latlng,
                draggable: true,
                map: map,
                title: titulo
            });

            //google.maps.event.addListener(marcador, 'click', function () { //esto es para cuando sea click en el marcador
            google.maps.event.addListener(marcador, 'dragend', function () { //para cuando se arrastre el marcador
                var Puntos = marcador.getPosition();
                var Latx = Puntos.lat();
                var Lonx = Puntos.lng();
                document.getElementById('txt_coord_x').value = Latx;
                document.getElementById('txt_coord_y').value = Lonx;


            });
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
            if ((key < 46 || key > 46) && (key < 48 || key > 57)) {

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

            // 45 (-) , 46(.) Y 47 (/)   || (key >= 44 && key <= 46) ||  key >=48                                          
            if ((key >= 32 && key <= 32) || (key >= 44 && key <= 46) || (key >= 47 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241) || (key >= 193 && key <= 193) || (key >= 201 && key <= 201) || (key >= 205 && key <= 205) || (key >= 211 && key <= 211) || (key >= 218 && key <= 218) || (key >= 225 && key <= 225) || (key >= 233 && key <= 233) || (key >= 237 && key <= 237) || (key >= 243 && key <= 243) || (key >= 250 && key <= 250)) {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-center">
                        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Requerimientos de Evento"></asp:Label>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <asp:Label ID="lblidreq" runat="server" Visible="False"></asp:Label>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-2">
                                    <h2 style="margin-bottom: 6px;">Folio evento</h2>
                                    <asp:TextBox ID="txtFolio" runat="server" Width="100px" placeholder="INGRESE FOLIO"
                                        Font-Size="small" onkeypress="javascript:return solonumeros(event)" AutoPostBack="True"
                                        TabIndex="1"></asp:TextBox>
                                </div>
                                <div class="col-5">
                                    <asp:Panel ID="pnlProgramaLinea" runat="server">
                                        <h2>Nombre </h2>
                                        <b>
                                            <asp:Label ID="lblNombreEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                                <div class="col-5">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <h2>Fecha </h2>
                                        <b>
                                            <asp:Label ID="lblFechaEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <h2>Descripción</h2>
                                        <b>
                                            <asp:Label ID="lblDescripcionEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12 col-lg-8">

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <asp:DropDownList ID="ddlTipoReq" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoReq_SelectedIndexChanged">
                                        <asp:ListItem Text="Contrato" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Requisición" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Orden de Servicio" Value="3"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <br />
                            <asp:Panel ID="pnlRequerimientos" runat="server">
                                <div class="row">
                                    <div class="col-4">
                                        <asp:Label ID="Label5" runat="server" Text="Requerimiento"></asp:Label>
                                        <asp:DropDownList ID="drpReq" runat="server" Width="100%" AutoPostBack="True" CssClass="select-basic-simple" OnSelectedIndexChanged="drpReq_SelectedIndexChanged1"
                                            AppendDataBoundItems="True" Enabled="False" TabIndex="2">
                                            <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:Label ID="Label3" runat="server" Text="Cantidad"></asp:Label>
                                        <asp:TextBox ID="txtCantidad" runat="server" onkeypress="javascript:return solonumeros(event)"
                                            Width="100%" Enabled="False" TabIndex="3"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <asp:Label ID="Label6" runat="server" Text="Observaciones"></asp:Label>
                                        <asp:TextBox ID="txtObservaciones" runat="server" Width="100%" onkeypress="javascript:return  solonumletrasMayespacios(event)"
                                            TextMode="MultiLine" Enabled="False" TabIndex="4"></asp:TextBox>
                                    </div>
                                    <div class="col-1">
                                        <br />
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/agregar-icono-7533-128.png" OnClick="ImageButton1_Click"
                                            Height="25" Width="50" TabIndex="5" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlRequisiciones" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-4">
                                        <asp:Label ID="Label2" runat="server" Text="Requisición"></asp:Label>
                                        <asp:DropDownList ID="ddlRequisicion" runat="server" CssClass="select-basic-simple" Width="100%"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:Label ID="Label7" runat="server" Text="Cantidad"></asp:Label>
                                        <asp:TextBox ID="txtCantidadRequisicion" runat="server" onkeypress="javascript:return solonumeros(event)"
                                            Width="100%" TabIndex="3"></asp:TextBox>
                                    </div>

                                    <div class="col-4">
                                        <asp:Label ID="Label9" runat="server" Text="Observaciones"></asp:Label>
                                        <asp:TextBox ID="txtObservacionesRequisicion" runat="server" Width="100%" onkeypress="javascript:return  solonumletrasMayespacios(event)"
                                            TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                                    </div>
                                    <div class="col-1">
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="images/agregar-icono-7533-128.png" OnClick="ImageButton1_Click"
                                            Height="25" Width="50" TabIndex="5" />
                                    </div>
                                </div>
                            </asp:Panel>

                              <asp:Panel ID="pnlOrdenServicio" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-4">
                                        <asp:Label ID="Label8" runat="server" Text="Orden de Servicio"></asp:Label>
                                        <asp:DropDownList ID="ddlOrdenServicio" runat="server" CssClass="select-basic-simple" Width="100%"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:Label ID="Label10" runat="server" Text="Cantidad"></asp:Label>
                                        <asp:TextBox ID="txtCantidadOS" runat="server" onkeypress="javascript:return solonumeros(event)"
                                            Width="100%" TabIndex="3"></asp:TextBox>
                                    </div>

                                    <div class="col-4">
                                        <asp:Label ID="Label11" runat="server" Text="Observaciones"></asp:Label>
                                        <asp:TextBox ID="txtObservacionesOS" runat="server" Width="100%" onkeypress="javascript:return  solonumletrasMayespacios(event)"
                                            TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                                    </div>
                                    <div class="col-1">
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="images/agregar-icono-7533-128.png" OnClick="ImageButton1_Click"
                                            Height="25" Width="50" TabIndex="5" />
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="row">
                                <div class="col-3">
                                    <asp:Label ID="lblHrInsta" runat="server" Text="Hora Instalación:"></asp:Label>

                                    <asp:TextBox ID="txtHoraInsta" runat="server" size="5" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                        onkeyup="mascara(this,':',patron,true)" MaxLength="5"
                                        Enabled="False" TabIndex="7"></asp:TextBox>
                                </div>
                                <div class="col-2">
                                    <asp:Label ID="lblFechaInsta" runat="server" Text="Fecha Instalación:"></asp:Label>

                                    <asp:TextBox ID="txtFechaInsta" runat="server" Width="100%" Enabled="False"
                                        onkeypress="javascript:return nocaptura(event)"
                                        TabIndex="8"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaInsta_CalendarExtender" runat="server" TargetControlID="txtFechaInsta"
                                        TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 text-right">
                                    <asp:Label ID="lblInfoEvento" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <br />
                                    <asp:GridView ID="GridView1" runat="server" Width="90%" Height="90px" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" AutoGenerateDeleteButton="True"
                                        TabIndex="6">
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
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-12 col-lg-4">

                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <h5>Información general</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    <asp:Label ID="Label1" runat="server" Text="Seleccione si habrá mesa en presidium:"></asp:Label>
                                </div>
                                <div class="col-6">
                                    <asp:CheckBox ID="CheckMesaPresidium" runat="server"
                                        TextAlign="Left"
                                        TabIndex="9" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-6">
                                    <asp:Label ID="lblMaestroCeremonia" runat="server" Text="Maestro de Ceremonia:"></asp:Label>
                                </div>
                                <div class="col-6">
                                    <asp:DropDownList ID="drpCeremonia" Width="200px" runat="server"
                                        AutoPostBack="True" Enabled="False" TabIndex="10">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-6">
                                    <asp:Label ID="lblAforo" runat="server" Text="Aforo Total:"></asp:Label>
                                </div>
                                <div class="col-6">
                                    <asp:TextBox ID="txtAforo" runat="server" Width="80px"
                                        onkeypress="javascript:return solonumeros(event)" Enabled="False"
                                        TabIndex="11"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <hr />
                            <br />
                            <div class="row">
                                <div class="col-12">
                                    <b>
                                        <h2>PRESUPUESTO ACTUAL:</h2>
                                    </b>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4">
                                    <h4>SUBTOTAL</h4>
                                </div>
                                <div class="col-8">

                                    <h4>
                                        <asp:Label ID="lblSubtotalActual" runat="server" Text="$ 0"></asp:Label></h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4">
                                    <h4>IVA</h4>
                                </div>
                                <div class="col-8">

                                    <h4>
                                        <asp:Label ID="lblIvaActual" runat="server" Text="$ 0"></asp:Label></h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-4">
                                    <h4>TOTAL</h4>
                                </div>
                                <div class="col-8">

                                    <h4>
                                        <asp:Label ID="lblPresupuestoActual" runat="server" Text="$ 0"></asp:Label></h4>
                                </div>
                            </div>
                            <h3>
                        </div>
                    </div>
                </div>
                <br />
                <hr />
                <div class="row">
                    <div class="col-12">

                        <asp:Button ID="btnGuardar" CssClass="button" runat="server" Text="Guardar"
                            TabIndex="12" />

                        <asp:Button ID="btnCancelar" CssClass="button" runat="server" Text="Cancelar"
                            TabIndex="13" />

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalServiciosPublicos" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Requerimiento interno</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updRequerimientosInterno" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h4>Listado de requerimientos</h4>
                                    </div>
                                </div>
                                <div class="container-border">
                                    <asp:Repeater ID="rptRequerimientosInternos" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:CheckBox ID="chkAsignado" runat="server" Checked='<%# Eval("Asignado") %>' />
                                                    <asp:HiddenField ID="hdnIdRequerimiento" runat="server" Value='<%# Eval("Id") %>' />
                                                </div>
                                                <div class="col-9">
                                                    <asp:Label ID="lblNombreAsignado" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <asp:DropDownList ID="ddlCatalogoRequerimientos" runat="server" Width="100%"></asp:DropDownList>
                                    </div>
                                    <div class="col-6">
                                        <asp:Button ID="btnReqAgregarCatalogo" runat="server" Text="Agregar" OnClick="btnReqAgregarCatalogo_Click" />
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <input type="button" class="btn btn-info" data-dismiss="modal" value="Cancelar" />
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarRequerimiento" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btnGuardarRequerimiento_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <div id="modalImprevistos" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Requerimiento interno</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updImprevistos" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h3>Monto del cheque de imprevistos</h3>
                                        <asp:TextBox ID="txtMontoChequeImprevistos" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <h3>Catalago de imprevistos</h3>
                                    </div>
                                </div>
                                <div class="container-border">
                                    <asp:Repeater ID="rptImprevistos" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-3">
                                                    <asp:CheckBox ID="chkAsignado" runat="server" Checked='<%# Eval("Asignado") %>' />
                                                    <asp:HiddenField ID="hdnIdImprevisto" runat="server" Value='<%# Eval("Id") %>' />
                                                </div>
                                                <div class="col-9">
                                                    <asp:Label ID="lblNombreAsignado" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <input type="button" class="btn btn-info" data-dismiss="modal" value="Cancelar" />
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnAgregarImprevistos" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btnAgregarImprevistos_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>
    <a href="http://apycom.com/" style="color: White">.</a>

    <div id="divusuarioReq">
        <asp:Label ID="Label4" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaReq">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {
            $('.select-basic-simple').select2();
        }
        $(document).ready(function () {
            //
            $('.select-basic-simple').select2();
        });

        function abreModalRequerimientoInterno() {
            $('#modalServiciosPublicos').modal('show');
        }

        function cierraModalRequerimientoInterno() {
            $('#modalServiciosPublicos').modal('hide');
        }

        function abreModalImprevistos() {
            $('#modalImprevistos').modal('show');
        }

        function cierraModalImprevistos() {
            $('#modalImprevistos').modal('hide');
        }
    </script>

</asp:Content>
