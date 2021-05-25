<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.ConsultaEvento" Codebehind="ConsultaEvento.aspx.vb" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="menu.css" rel="stylesheet" type="text/css" />
    <script src="jquery.js" type="text/javascript"></script>
    <script src="menu.js" type="text/javascript"></script>
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <link href="estilo_consultaevento.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>
    <%--        <script src="tema/jquery.js" type="text/javascript"></script>
    <script src="tema/menu.js" type="text/javascript"></script>
    <link href="tema/menu.css" rel="stylesheet" type="text/css" />--%>
    <script src="Alertas/mootools.js" type="text/javascript"></script>
    <script src="Alertas/sexyalertbox.v1.1.js" type="text/javascript"></script>
    <link href="Alertas/sexyalertbox.css" rel="stylesheet" type="text/css" />
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




        //        function solonumletrasMayespacios(e) {

        //            var key;

        //            if (window.event) // IE
        //            {
        //                key = e.keyCode;
        //            }
        //            else if (e.which) // Netscape/Firefox/Opera
        //            {
        //                key = e.which;
        //            }

        //            //46(.) Y 47 (/)
        //            if ((key >= 32 && key <= 32) || (key >= 45 && key <= 45) || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241)) {
        //                return true;
        //            }

        //            return false;
        //        }




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
    <style type="text/css">
        .style1
        {
            width: 400px;
            height: 4px;
        }
        .style20
        {
            width: 763px;
        }
        .style21
        {
            height: 24px;
        }
    </style>
    <title></title>
</head>
<body onload="geolocalizame();">
    <form id="form1" runat="server">

    <asp:ScriptManager ID="smajax" runat="server">
    </asp:ScriptManager>

    <div id="divprincipal" style="margin: 5% 10% 5% 10%">
        <div id="titulomaster">
            <img src="images/eventos2.png" />
        </div>
        <div id="divmenuprincipal">
            <div id="menu" style="background-color: #084B8A; margin-bottom: 5%">
                   <ul class="menu">
                    <li><a href="Bienvenido.aspx"><span>INICIO</span></a></li>
                   <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Then%>
                     <li><a href="#" class="parent"><span>EVENTO</span></a>
                     <div><ul>
                         <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Then%>
                    <li><a href="Alta.aspx"><span>ALTA</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Then%>
                    <li><a href="Cambios.aspx"><span>CAMBIOS</span></a></li>
                    <% End If%>
					 </ul></div>
                   </li>
                 <% End If%>

        <%--            <% If Session("seguridad") = 1 Or Session("seguridad") = 3 Or Session("seguridad") = 8 Or Session("seguridad") = 9 Then%>
                    <li><a href="ConsultaEvento.aspx"><span>CONSULTA EVENTO</span></a></li>
                    <% End If%>--%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 6 Then%>
                    <li><a href="Validacion.aspx"><span>VALIDA EVENTO</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 4 Then%>
                    <li><a href="Requerimientos.aspx"><span>REQUERIMIENTO</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 5 Or Session("seguridad") = 9 Then%>
                    <li><a href="Ficha.aspx"><span>FICHA</span></a></li>
                    <% End If%>
                    <%--<% If Session("seguridad") = 1 Or Session("seguridad") = 7 Or Session("seguridad") = 8 Or Session("seguridad") = 9 Then%>
                    <li><a href="Consulta.aspx"><span>CONSULTA</span></a></li>
                    <% End If%>--%>

                    <% If Session("seguridad") = 1 Or Session("seguridad") = 3 Or Session("seguridad") = 7 Or Session("seguridad") = 8 Or Session("seguridad") = 9 Then%>
                     <li><a href="#" class="parent"><span>CONSULTAS</span></a>
                      
                     <div><ul>

                       <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 7 Then%>
                        <li><a href="ConsultaEstatus.aspx"><span>CONSULTA ESTATUS</span></a></li>
                        <% End If%>

				        <% If Session("seguridad") = 1 Or Session("seguridad") = 7 Or Session("seguridad") = 8 Or Session("seguridad") = 9 Then%>
                        <li><a href="Consulta.aspx"><span>CONSULTA MAPA</span></a></li>
                        <% End If%>

				<%--        <% If Session("seguridad") = 1 Or Session("seguridad") = 3 Or Session("seguridad") = 8 Or Session("seguridad") = 9 Then%>
                        <li><a href="ConsultaEvento.aspx"><span>CONSULTA EVENTO</span></a></li>
                        <% End If%>--%>
					 </ul></div>
                   </li>
                 <% End If%>
                 
                   <% If Session("seguridad") = 1 Then%>
                    <li><a href="Imagen.aspx"><span>DISEÑO</span></a></li>
                    <% End If%>

                    <li><a href="Evidencias.aspx"><span>EVIDENCIAS</span></a></li>
                    <li><a href="Salir.aspx"><span>SALIR</span></a></li>
                    <%-- <li><a href="Exportar.aspx"><span>EXPORTAR</span></a>--%>
                </ul>
            </div>
        </div>
        
        <div id ="divConsultaEvento">

        <div id ="divGridEvento">
         <asp:GridView ID="GridView1" runat="server"  CssClass="letra" BackColor="White" 
                 BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                 ForeColor="#333333" GridLines="Horizontal" >
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                 <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#7F7979" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </div>

        </div>
    </div>
    <a href="http://apycom.com/" style="color: White"></a>
    <div id="divusuarioConsulta">
        <asp:Label ID="Label3" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaConsulta">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divinputbox">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </div>
    </form>

</body>
</html>
