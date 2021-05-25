<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.Cambios2" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="ConsultaGeneral.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>
    <script type="text/javascript">




        function alerta_Cambio_Exito() {
            var Sexy = new SexyAlertBox();
            Sexy.alert("<b>Cambio realizado correctamente!!</b>");

        }

        function alerta_Cambio_Error() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>No se guardo la información!!</b></p>");

        }





        function alerta_Cambio_Error_PuntoMapa() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>No se guardo:PUNTO EN MAPA</b></p>");

        }


        function alerta_Cambio_Error_Hora() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>Recuerde el formato de Horas, desde 00:00 hasta 23:59</b>");

        }

        function alerta_Cambio_Error_HoraAlcalde() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>HORA ALCALDE:Recuerde el formato de Horas, desde 00:00 hasta 23:59</b>");

        }

        function alerta_Cambio_Error_NoPermiteGuardarEvento() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>FECHA DEL EVENTO</h1><p><b>No se permite guardar el evento, porque no está dentro de los 3 días hábiles permitidos!!</b></p>");

        }

        function alerta_Cambio_Error_NoPermiteGuardarEventoYPuntoMapa() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<h1>FECHA DEL EVENTO</h1><br><p>La fecha seleccionada no está dentro de los 3 días hábiles permitidos</p><p>Favor de seleccionar una fecha válida!!</p>");

        }

        function alerta_Cambio_Error_Fecha() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>Recuerde el formato de fecha, YYYY-MM-DD </b>");

        }


        var map; //importante definirla fuera de la funcion initialize() para poderla usar desde otras funciones.
        var infowindow = new google.maps.InfoWindow({ maxWidth: 200 });
        var poly;
        var Polyx;
        var PxLngLat = [];
        var Munx;

        function initialize() {
            console.log('entrad-dd');
            var Lx1 = document.getElementById('TextLng').value;
            var Lx2 = document.getElementById('TextLat').value;
            if (Lx1 == '') {

                Munx = new google.maps.LatLng(25.754757464040086, -100.2895021203766);
                var mapOptions = {
                    zoom: 16,
                    center: Munx,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
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


        function Button2_onclick() {

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

        (function ($) {

            geolocalizame();

        })(jQuery);
    </script>
    <style type="text/css">
        .style1 {
            width: 400px;
            height: 4px;
        }

        .style20 {
            width: 763px;
        }

        .style21 {
            height: 24px;
        }

        .contenedor-evento h6 {
            margin-top: 15px;
            margin-bottom: 0px;
        }
    </style>

    <div class="container-fluid contenedor-evento">
        <div class="row">
            <div class="col-12 col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Folio:</h6>
                                    <asp:Label ID="lblFolio" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Nombre Evento:</h6>
                                    <asp:Label ID="lblNombreEvento" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Secretaria:</h6>
                                    <asp:Label ID="lblSecretaria" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Dirección:</h6>
                                    <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Actividad:</h6>
                                    <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Sub Actividad:</h6>
                                    <asp:Label ID="lblSubActividad" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Tipo Evento:</h6>
                                    <asp:Label ID="lblTipoEvento" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <h6 class="subtitle">Descripción:</h6>
                                    <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <h6 class="subtitle">Lugar:</h6>
                                    <asp:Label ID="lblLugar" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Colonia:</h6>
                                    <asp:Label ID="lblColonia" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Calle:</h6>
                                    <asp:Label ID="lblCalle" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Num.Ext:</h6>
                                    <asp:Label ID="lblNumExterior" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Num.Int:</h6>
                                    <asp:Label ID="lblNumInteior" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Fecha inicio evento:</h6>
                                    <asp:Label ID="lblFechaInicio" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Fecha fin evento:</h6>
                                    <asp:Label ID="lblFechaFin" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Hora de Inicio:</h6>
                                    <asp:Label ID="lblHoraInicio" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Hora de Término:</h6>
                                    <asp:Label ID="lblHoraTermino" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Seleccione si asistirá Alcalde:</h6>
                                    <asp:Label ID="lblAsistiraAlcalde" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Arribo de Alcalde:</h6>
                                    <asp:Label ID="lblArriboAlcalde" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Apoyo  de Servicios Públicos:</h6>
                                    <asp:Label ID="lblServiciosPublicos" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Salida de Alcalde:</h6>
                                    <asp:Label ID="lblHoraSalidaAlcalde" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Seleccione si asistirá Prensa:</h6>
                                    <asp:Label ID="lblAsistiraPrensa" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Núm.Beneficiado(s):</h6>
                                    <asp:Label ID="lblNumBeneficiados" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">¿Es evento presencial?:</h6>
                                    <asp:Label ID="lblEventoPresencial" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Núm. Ciudadanos:</h6>
                                    <asp:Label ID="lblNumCiudadanos" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Teléfono del Responsable:</h6>
                                    <asp:Label ID="lblTelefonoResponsable" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Responsable del Evento:</h6>
                                    <asp:Label ID="lblResponsableEvento" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <asp:TextBox ID="TextBox2" runat="server" Height="1px" ClientIDMode="Static" ReadOnly="True" Width="1px"
                            Style="margin-bottom: 0px" ForeColor="white"></asp:TextBox>
                        <asp:TextBox ID="TextLng" runat="server" Height="1px" ClientIDMode="Static" ReadOnly="True" Width="1px"></asp:TextBox>
                        <asp:TextBox ID="TextLat" runat="server" Height="1px" ClientIDMode="Static" ReadOnly="True" Width="1px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-12 col-md-6">
                <asp:Panel ID="Panel1" runat="server" Height="400px" Width="480px">
                    <div id="map_canvas" style="width: 470px; height: 390px">
                    </div>
                    <asp:TextBox ID="TextBox1" runat="server" Height="1px" ReadOnly="True" Width="1px"></asp:TextBox>
                    <%--<asp:TextBox ID="TextBox2" runat="server" Height="34px" ReadOnly="True" Width="169px"
                                    Style="margin-bottom: 0px" forecolor = "white" ></asp:TextBox>--%>
                    <asp:TextBox ID="TextTitulo" runat="server" Height="1px" ReadOnly="True" ClientIDMode="Static" Width="1px"></asp:TextBox>
                </asp:Panel>
                <div style="display: none;">
                    <asp:TextBox ID="txt_coord_x" runat="server" ClientIDMode="Static" ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txt_coord_y" runat="server" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Height="21px" Width="384px">
                    </asp:DropDownList>
                </div>

                <asp:Button ID="Button1" runat="server" Text="Ubicación actual" Width="116px" Visible="False"
                    Height="20px" />
                <asp:Button ID="btn_consultar" runat="server" Text="Consultar" Visible="False" Height="21px" />
                <asp:Button ID="btn_pone_punto" runat="server" Text="Pone punto" Visible="False"
                    Height="21px" />
                <div id="divgridMapa">
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </div>
                <hr />
                <asp:UpdatePanel ID="updOperador" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <h5>Operado por</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Puesto:</h6>
                                    <asp:Label ID="lblPuesto" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Empleado:</h6>
                                    <asp:Label ID="lblEmpleado" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Teléfono:</h6>
                                    <asp:Label ID="lblTelefonoOperador" runat="server"></asp:Label>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Correo:</h6>
                                    <asp:Label ID="lblCorreoOperador" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>

    <a href="http://apycom.com/" style="color: White"></a>
    <div id="divusuarioAlta">
        <asp:Label ID="Label3" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaAlta">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divinputbox">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </div>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {
            geolocalizame();
        }


        function alerta_Alta_Exito(folio) {
            var contenido = $('<div class="container-fluid">')
                .append('<div class"row"><div class="col-12"><p>El evento se ha creado correctamente, pero antes de poder ser "validado" deberá de completar la información en "Ficha" y "Requerimientos"</p></div></div><hr />')
                .append($('<div class="row"></div>').append($('<div class="col-6 text-left"></div>').append(createButton('Ir a "Ficha"', function () {
                    swal.close();
                    window.open('/PlaneacionFinanciera/Eventos2015/Ficha.aspx?folio=' + folio, '_self');
                }))).append($('<div class="col-6 text-right"></div>')
                    .append(createButton('Ir a "Requerimientos"', function () {
                        swal.close();
                        window.open('/PlaneacionFinanciera/Eventos2015/Requerimientos.aspx?folio=' + folio, '_self');
                        console.log('Req');
                    })))
                );

            swal({
                html: contenido,
                type: "info",
                showConfirmButton: false,
                showCancelButton: false
            });
        }

        function createButton(text, cb) {
            return $('<button class="btn  btn-primary">' + text + '</button>').on('click', cb);
        }

        function alerta_Alta_Error() {
            mensajeCustom('error', 'Algo salió mal', 'No se guardo la información!!');
        }

        function alerta_Alta_Error_PuntoMapa() {
            mensajeCustom('warning', 'Algo falta', 'No se guardo:PUNTO EN MAPA, Favor de Guardarlo en pantalla de CAMBIOS');
        }


        function alerta_Alta_Error_FechaPosterior() {
            mensajeCustom('error', 'Algo salió mal', 'No se permite guardar el evento, tiene que ser una fecha posterior al día de hoy!!');
        }



        function alerta_Alta_Volver_PuntoMapa() {
            mensajeCustom('info', 'PUNTO EN EL MAPA', 'Volver a Colocar Punto en el Mapa');
        }


        function alerta_Alta_Error_NoPermiteGuardarEvento() {
            var dias = '<%= PMD_WAS.VariablesGlobales.DIAS_ESPERA_EVENTO %>';
            mensajeCustom('info', 'FECHA DEL EVENTO', 'No se permite guardar el evento, porque no está dentro de los ' + dias + ' días hábiles permitidos!!');
        }

        function alerta_Alta_Error_NoPermiteGuardarEventoYPuntoMapa() {
            var dias = '<%= PMD_WAS.VariablesGlobales.DIAS_ESPERA_EVENTO %>';
            mensajeCustom('info', 'FECHA DEL EVENTO', 'No se permite guardar el evento, porque no está dentro de los ' + dias + ' días hábiles permitidos!!');
        }

        function alerta_Alta_Error_Fecha() {
            mensajeCustom('warning', 'Formato incorrecto', 'Recuerde el formato de fecha, YYYY-MM-DD');
        }

        function alerta_Alta_Error_FechaMayor() {
            mensajeCustom('warning', 'Formato incorrecto', 'La fecha de Fin no puede ser mayor a la de Inicio');
        }

        function alerta_Alta_Error_Hora() {
            mensajeCustom('warning', 'Formato incorrecto', 'Recuerde el formato de Hora, desde 00:00 hasta 23:59');
        }

        function alerta_Alta_Error_HoraAlcalde() {
            mensajeCustom('warning', 'Formato incorrecto', 'HORA ALCALDE:Recuerde el formato de Hora, desde 00:00 hasta 23:59');
        }


        var map; //importante definirla fuera de la funcion initialize() para poderla usar desde otras funciones.
        var infowindow = new google.maps.InfoWindow({ maxWidth: 200 });
        var poly;
        var Polyx;
        var PxLngLat = [];
        var Munx;
        function initialize() {
            try {
                var Lx1 = document.getElementById('TextLng').value;
                var Lx2 = document.getElementById('TextLat').value;
                if (Lx1 == '') {

                    Munx = new google.maps.LatLng(25.754757464040086, -100.2895021203766);
                    var mapOptions = {
                        zoom: 16,
                        center: Munx,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };


                    map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
                    navigator.geolocation.getCurrentPosition(pedirPosicion);

                } else {
                    console.log('entra aqui');
                    console.log(Lx2);
                    console.log(Lx1);
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
            catch (x) { console.log(x); }

        }

        function OpenPopupx() {
            try {
                var rx = document.getElementById('txt_coord_x').value;
                var ry = document.getElementById('txt_coord_y').value;
                var link = "Variables.aspx?vx=" + rx + "&vy=" + ry;
                window.open(link, "prueba", "location=1,status=1,scrollbars=yes,resizable=yes,left=2000,top=2000,width=5px,height=5px,titlebar=0,toolbar=0,menubar=0");
                return false;
            }
            catch (x) { console.log(x); }


        }


        function pedirPosicion(pos) {
            try {
                var centro = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
                map.setCenter(centro); //pedimos que centre el mapa..
                map.setMapTypeId(google.maps.MapTypeId.ROADMAP); //y lo volvemos un mapa callejero
                MarcaPunto(pos);
            }
            catch (x) { console.log(x); }

        }


        function geolocalizame() {
            try {
                initialize();
            }
            catch (x) { console.log(x); }



        }

        function showArrays(event) {
            try {
                var vertices = this.getPath();
                var contentString = "<b>Bermuda Triangle Polygon</b><br />";
                contentString += "Clicked Location: <br />" + event.latLng.lat() + "," + event.latLng.lng() + "<br />";
                infowindow.setContent(contentString);
                infowindow.setPosition(event.latLng);
                infowindow.open(map);
            }
            catch (x) { console.log(x); }


        }

        function GeneraPuntos() {
            try {
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
            catch (x) { console.log(x); }



        }

        function addLatLng() {
            try {
                var txt1 = document.getElementById('txt_coord_x').value;
                var txt2 = document.getElementById('txt_coord_y').value;
                var txtTx = document.getElementById('TextTitulo').value;
                var lat = txt1;
                var lng = txt2;
                var latlng = new google.maps.LatLng(lat, lng);
                PxLngLat.push(latlng);
            }
            catch (x) { console.log(x); }

        }



        function MarcaPunto(pos) {
            try {
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
            catch (x) { console.log(x); }

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

        (function ($) {
            geolocalizame();
        })(jQuery);
    </script>
</asp:Content>
