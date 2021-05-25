<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.Imagen" Codebehind="Imagen.aspx.vb" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache VaryByParam="none" Duration="1" Location="Client" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="estilo_imageninterno.css" rel="stylesheet" type="text/css" />
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>
    <link href="menu.css" rel="stylesheet" type="text/css" />
    <script src="jquery.js" type="text/javascript"></script>
    <script src="menu.js" type="text/javascript"></script>
    <script src="Alertas/mootools.js" type="text/javascript"></script>
    <script src="Alertas/sexyalertbox.v1.1.js" type="text/javascript"></script>
    <link href="Alertas/sexyalertbox.css" rel="stylesheet" type="text/css" />
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


        function alertaErrorEnvioCorreo() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<b>E-m@il!!</b> <p>No se envio correo,vuelva a intentarlo!!!</p>");

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
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="divprincipal" style="margin: 5% 10% 5% 10%">
        <div id="titulomaster">
            <img src="images/eventos2.png" />
        </div>
        <div id="divmenuprincipal">
            <div id="menu" style="background-color: #084B8A; margin-bottom: 5%">
                  <ul class="menu">
                    <li><a href="Bienvenido.aspx"><span>INICIO</span></a></li>


                   <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 3 Then%>
                     <li><a href="#" class="parent"><span>EVENTO</span></a>
                     <div><ul>
                         <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 3 Then%>
                    <li><a href="Alta.aspx"><span>ALTA</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 3 Then%>
                    <li><a href="Cambios.aspx"><span>CAMBIOS</span></a></li>
                    <% End If%>
					 </ul></div>
                   </li>
                 <% End If%>

                    <% If Session("seguridad") = 1 Or Session("seguridad") = 6 Then%>
                    <li><a href="Validacion.aspx"><span>VALIDA EVENTO</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 4 Then%>
                    <li><a href="Requerimientos.aspx"><span>REQUERIMIENTO</span></a></li>
                    <% End If%>
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 5 Then%>
                    <li><a href="Ficha.aspx"><span>FICHA</span></a></li>
                    <% End If%>
               
                    <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 7 Then%>
                     <li><a href="#" class="parent"><span>CONSULTAS</span></a>
                      
                     <div><ul>

                       <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 7 Then%>
                        <li><a href="ConsultaEstatus.aspx"><span>CONSULTA ESTATUS</span></a></li>
                        <% End If%>


				        <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 7 Then%>
                        <li><a href="Consulta.aspx"><span>CONSULTA MAPA</span></a></li>
                        <% End If%>

				<%--        <% If Session("seguridad") = 1 Or Session("seguridad") = 3 Or Session("seguridad") = 8 Or Session("seguridad") = 9 Then%>
                        <li><a href="ConsultaEvento.aspx"><span>CONSULTA EVENTO</span></a></li>
                        <% End If%>--%>
					 </ul></div>
                   </li>
                 <% End If%>
                 
                <%--   <% If Session("seguridad") = 1 Or Session("seguridad") = 2 Or Session("seguridad") = 8 Then%>
                    <li><a href="Imagen.aspx"><span>DISEÑO</span></a></li>
                    <% End If%>--%>

                    <li><a href="Evidencias.aspx"><span>EVIDENCIAS</span></a></li>
                    <li><a href="Salir.aspx"><span>SALIR</span></a></li>
                    <%-- <li><a href="Exportar.aspx"><span>EXPORTAR</span></a>--%>
                </ul>
            </div>
        </div>
        <div id="divRequerimiento">
           <%-- <div id="divlblfolioReq">
                <asp:Label ID="label2" runat="server" Text="Folio Evento"></asp:Label>
            </div>--%>
           

            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
            <div id="divlblReq">
                <asp:Label ID="Label5" runat="server" Text="Requerimiento"></asp:Label>
            </div>
                         <%--    <div id="divlblidReq">
                <asp:TextBox ID="txtFolio" runat="server" Width="100px" placeholder="INGRESE FOLIO"
                    Font-Size="small" onkeypress="javascript:return solonumeros(event)" AutoPostBack="True"></asp:TextBox>
            </div>--%>
  

                    <div id="divdrpReq">
                        <asp:DropDownList ID="drpReq" runat="server" Width="146px" AutoPostBack="True" 
                            AppendDataBoundItems="True"> 
                        <asp:ListItem Value="">Seleccione un item</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblidreq" runat="server" Visible="False"></asp:Label>
                    </div>

                            <div id="divtxtCantidad">
                <asp:TextBox ID="txtCantidad" runat="server" onkeypress="javascript:return solonumerosdecimales(event)"
                    Width="65px" AutoPostBack="True"></asp:TextBox>
            </div>

            <div id = "divtxtObservacion">
                <asp:TextBox ID="txtObservaciones" runat="server" Width= "323px" onkeypress="javascript:return  solonumletrasMayespacios(event)"
                    TextMode="MultiLine" AutoPostBack="True" Height ="50px" ></asp:TextBox>
            </div>

            <div id="divlblHoraInsta">
                <asp:Label ID="lblHrInsta" runat="server" Text="Hora del Evento:"></asp:Label>
            </div>
            <div id ="divtxtHoraInsta">
                <asp:TextBox ID="txtHoraEvento" runat="server" size = "5" Width="45px" onkeypress="javascript:return solonumeros(event)"
                            onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate = "txtHoraEvento" ForeColor = "red" ></asp:RequiredFieldValidator>
            
            </div>
                    
            <div id ="divlblFechaInsta">
                <asp:Label ID="lblFechaInsta" runat="server" Text="Fecha de entrega:"></asp:Label>
            </div>
            <div id ="divtxtFechaInsta">
                <asp:TextBox ID="txtFechaEvento" runat="server" Width="80px" 
                    onkeypress="javascript:return nocaptura(event)" AutoPostBack="True"></asp:TextBox>
                  <asp:CalendarExtender ID="txtFechaEvento_CalendarExtender" runat="server" TargetControlID="txtFechaEvento"
                    TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor= "Red" ControlToValidate ="txtFechaEvento"  ></asp:RequiredFieldValidator>
            </div>

            <div id ="divlblResponsable">
                <asp:Label ID="lblFechaEntrega" runat="server" Text="Fecha del Evento:"></asp:Label>
            </div>
            <div id = "divtxtFechaEntrega">
                <asp:TextBox ID="txtFechaEntrega" runat="server" Width= "80px" 
                    onkeypress="javascript:return nocaptura(event)"></asp:TextBox>
                <asp:CalendarExtender ID="txtFechaEntrega_CalendarExtender" runat="server" TargetControlID="txtFechaEntrega"
                    TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate ="txtFechaEntrega" ForeColor = "red" ></asp:RequiredFieldValidator>

            </div>
            <div id ="divlblIdEventoImagen">
                <asp:Label ID="lblIdEventoImagen" runat="server" Text=""></asp:Label>
            </div>
            <div id ="divlblFolioEventoImagen">
                <asp:Label ID="lblFolioEventoImagen" runat="server" Text="Folio Evento:"></asp:Label>
            </div>

            <div id="divlblCeremonia">
                <asp:Label ID="Label1" runat="server" Text="Nombre del Evento:"></asp:Label>
            </div>
            <div id="divDrpCeremonia">
                <asp:TextBox id = "txtNombreEvento" runat="server" Text="" Width="405px" 
                    onkeypress="javascript:return  solonumletrasMayespacios(event)" 
                    AutoPostBack="True" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate= "txtNombreEvento" ForeColor = "red" ></asp:RequiredFieldValidator>

            </div>


            <div id="divlblCantidad">
                <asp:Label ID="Label3" runat="server" Text="Cantidad"></asp:Label>
            </div>

            <div id = "divlblObservacion">
                <asp:Label ID="Label6" runat="server" Text="Especificación:"></asp:Label>
            </div>
    
            <div id="divImagenAgregar">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/agregar-icono-7533-128.png"
                    Height="25" Width="50" />
            </div>

         
         </ContentTemplate>
            </asp:UpdatePanel>



                    <div id="DivGrid">
                        <asp:GridView ID="GridView1" runat="server" Width="90%" Height="90px" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AutoGenerateDeleteButton="True" Font-Size = "small">
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

            <br />

            
               
        </div>

        <div id="divGuardar">
            <asp:Button ID="btnGuardar" CssClass="button" runat="server" Text="Guardar"  />
        </div>
<%--        <div id ="divCancelar">
            <asp:Button ID="btnCancelar"  CssClass="button" runat="server" Text="Cancelar" />
        </div>
--%>
    </div>
    <a href="http://apycom.com/" style="color: White">.</a>

     <div id="divusuarioReq">
        <asp:Label ID="Label4" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaReq">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    </form>
</body>
</html>
