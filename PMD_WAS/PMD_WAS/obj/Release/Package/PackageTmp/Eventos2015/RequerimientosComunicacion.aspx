<%@ Page Language="VB" AutoEventWireup="false" Title="Requerimientos" MasterPageFile="~/MasterPage/MasterNuevaImagen.master"
    Inherits="PMD_WAS.RequerimientosComunicacion" Codebehind="RequerimientosComunicacion.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache VaryByParam="none" Duration="1" Location="Client" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="estilo_requerimiento.css" rel="stylesheet" type="text/css" />
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

        function OpenPopupx() {

            var rx = document.getElementById('txt_coord_x').value;
            var ry = document.getElementById('txt_coord_y').value;
            var link = "Variables.aspx?vx=" + rx + "&vy=" + ry;
            window.open(link, "prueba", "location=1,status=1,scrollbars=yes,resizable=yes,left=2000,top=2000,width=5px,height=5px,titlebar=0,toolbar=0,menubar=0");
            return false;

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
                    <div class="col-12 col-md-7">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <h6 class="subtitle">Folio evento</h6>
                                    <asp:TextBox ID="txtFolio" runat="server" placeholder="INGRESE FOLIO" CssClass="form-control"
                                        Font-Size="small" onkeypress="javascript:return solonumeros(event)" AutoPostBack="True"
                                        TabIndex="1"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-6">
                                    <asp:Panel ID="pnlProgramaLinea" runat="server">
                                        <h6 class="subtitle">Nombre </h6>
                                        <b>
                                            <asp:Label ID="lblNombreEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                                <div class="col-6">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <h6 class="subtitle">Fecha </h6>
                                        <b>
                                            <asp:Label ID="lblFechaEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <h6 class="subtitle">Descripción</h6>
                                        <b>
                                            <asp:Label ID="lblDescripcionEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-5">
                        <asp:Label ID="lblidreq" runat="server" Visible="False"></asp:Label><br />
                        <br />
                        <br />
                        <br />
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
                                    <asp:DropDownList ID="drpCeremonia" Width="100%" runat="server" CssClass="form-control"
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
                                    <asp:TextBox ID="txtAforo" runat="server" Width="100%" CssClass="form-control"
                                        onkeypress="javascript:return solonumeros(event)" Enabled="False"
                                        TabIndex="11"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12">
                        <br />
                        <h3>Requerimientos</h3>
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-lg-7">
                        <div id="containerRequerimientos" class="container-fluid" runat="server">
                            <div class="row">
                                <div class="col-6 col-md-4">
                                    <p>Tipo</p>
                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="-1" Text="Todos"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Contratos"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Requisiciones"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Ordenes de Servicio"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-6 col-md-4">
                                    <p>Contrato</p>
                                    <asp:DropDownList ID="ddlContrato" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-6 col-md-4">
                                    <p>Proveedor</p>
                                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-6 col-md-4">
                                    <p>Requerimiento</p>
                                    <asp:DropDownList ID="ddlRequerimientoDirector" runat="server" CssClass="form-control select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlRequerimientoDirector_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-6 col-md-2">
                                    <p>Cantidad</p>
                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                </div>
                                <div class="col-6 col-md-2">
                                    <p>Hora instalación</p>
                                    <asp:TextBox ID="txtHoraInstalacion" runat="server" size="5" Width="100%"
                                        onkeyup="mascara(this,':',patron,true)" MaxLength="5" CssClass="form-control"
                                        TabIndex="7"></asp:TextBox>
                                </div>
                                <div class="col-6 col-md-2">
                                    <p>Fecha instalación</p>
                                    <asp:TextBox ID="txtFechaInstalacion" runat="server" Width="100%"
                                        onkeypress="javascript:return nocaptura(event)" CssClass="form-control"
                                        TabIndex="8"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaInsta_CalendarExtender" runat="server" TargetControlID="txtFechaInstalacion"
                                        TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <p>Observaciones</p>
                                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 text-right">
                                    <br />
                                    <asp:Button ID="btnAgregarRequerimiento" runat="server" CssClass="btn btn-secondary" Text="Agregar requerimiento" OnClick="btnAgregarRequerimiento_Click" />
                                </div>
                            </div>
                        </div>


                        <div class="container-fluid">

                            <div class="row">
                                <div class="col-12 text-right">
                                    <asp:Label ID="lblInfoEvento" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtBuscar" runat="server" placeholder="Buscar..." Width="100%" onkeyup="javascript:buscaRequerimiento(this,event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <br />
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdRequerimientos" runat="server" CssClass="table table-hover table-bordered" Width="100%" Height="90px" CellPadding="4" ClientIDMode="Static" OnRowDataBound="grdRequerimientos_RowDataBound"
                                            ForeColor="#333333" GridLines="None" TabIndex="6" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="Tipo" DataField="TipoNombre" Visible="false" />
                                                <asp:TemplateField HeaderText="Contrato">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContrato" runat="server" Text='<%# String.Format("{0} - {1}", Eval("Contrato"), Eval("Proveedor")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Descripcion" DataField="Requerimiento" />
                                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" DataFormatString="{0:n0}" />
                                                <asp:BoundField HeaderText="Hora instalación" DataField="HoraInstalación" />
                                                <asp:BoundField HeaderText="Fecha instalación" DataField="FechaInstalación" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField HeaderText="Observaciones" DataField="Observaciones" />
                                                <asp:BoundField HeaderText="Costo Unitario" DataField="CostoUnitario" DataFormatString="{0:c2}" />
                                                <asp:BoundField HeaderText="IVA %" DataField="IVA" DataFormatString="{0:n0} %" />
                                                <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:c2}" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("Id") %>' />
                                                        <asp:HiddenField ID="hdnTipo" runat="server" Value='<%# Eval("Tipo") %>' />
                                                        <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# String.Format("{0}|{1}", Eval("Tipo"), Eval("Id")) %>' Text="Eliminar" CommandName="delete" OnCommand="lnkEliminar_Command"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <h4>No hay información que mostrar.</h4>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-lg-5">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <b>
                                        <h5>Presupuesto actual:</h5>
                                    </b>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4">
                                    <h5>Subtotal</h5>
                                </div>
                                <div class="col-8 text-right">
                                    <h5>
                                        <asp:Label ID="lblSubtotalActual" runat="server" Text="$ 0"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4">
                                    <h5>IVA</h5>
                                </div>
                                <div class="col-8 text-right">

                                    <h5>
                                        <asp:Label ID="lblIvaActual" runat="server" Text="$ 0"></asp:Label></h5>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-4">
                                    <h5>Total</h5>
                                </div>
                                <div class="col-8 text-right">

                                    <h5>
                                        <asp:Label ID="lblPresupuestoActual" runat="server" Text="$ 0"></asp:Label></h5>
                                </div>
                            </div>
                            <h3>
                        </div>
                        <br />
                        <br />
                        <div class="container-fluid content-requerimientos">
                            <div class="row">
                                <div class="col-12">
                                    <h5>Desglose presupuesto</h5>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-3 text-center">
                                    <b>CONTRATO</b>
                                </div>
                                <div class="col-3 text-center">
                                    <b>PROVEEDOR</b>
                                </div>
                                <div class="col-2 text-center">
                                    <b title="PRESUPUESTO">PPTO.</b>
                                </div>
                                <div class="col-2 text-center">
                                    <b title="COMPROMETIDO">COMP.</b>
                                </div>
                                <div class="col-2 text-center">
                                    <b title="DISPONIBLE">DISP.</b>
                                </div>
                            </div>
                            <hr />
                            <asp:Repeater ID="rptDetallePresupuesto" runat="server">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-3">
                                            <asp:Label ID="lblContrato" runat="server" Text='<%# Eval("Contrato") %>'></asp:Label>
                                        </div>
                                        <div class="col-3">
                                            <asp:Label ID="lblProveedor" runat="server" Text='<%# Eval("Proveedor") %>'></asp:Label>
                                        </div>
                                        <div class="col-2 text-right">
                                            <asp:Label ID="lblPresupuestoDisponible" runat="server" Text='<%# String.Format("{0:c2}", Eval("Presupuesto")) %>'></asp:Label>
                                        </div>
                                        <div class="col-2 text-right">
                                            <asp:Label ID="lblPresupuestoUtilizado" runat="server" Text='<%# String.Format("{0:c2}", Eval("Comprometido")) %>'></asp:Label>
                                        </div>
                                        <div class="col-2 text-right">
                                            <asp:Label ID="Label2" runat="server" Text='<%# String.Format("{0:C2}", Eval("Disponible")) %>'></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <br />
                <hr />
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnValidarEvento" runat="server" Enabled="false" CssClass="btn btn-success" Text="VALIDAR EVENTO" OnClientClick="javascript: return confirm('El evento será marcado como validado y será pasado a la siguiente etapa de el proceso. ¿Deseas continuar?');" OnClick="btnValidarEvento_Click" />
                    </div>
                    <div class="col-6 text-right">
                        <asp:Button ID="btnCancelar" CssClass="btn btn-secondary right" runat="server" Text="Cancelar" Visible="false" TabIndex="13" />
                        <asp:Button ID="btnGuardar" CssClass="btn btn-secondary" runat="server" Text="Guardar" OnClick="btnGuardar_Click"
                            TabIndex="12" />

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
                                        <asp:Button ID="btnGuardarRequerimiento" runat="server" CssClass="btn btn-info" Text="Guardar" />
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

        function buscaRequerimiento(o, e) {
            var valor = $(o).val().toLowerCase();
            if (valor != '') {
                $('#grdRequerimientos tbody tr').each(function (i) {
                    if (i > 0) {
                        var html = $(this).html().toLowerCase();
                        if (html.indexOf(valor) > 0) {
                            $(this).show();
                        }
                        else {
                            $(this).hide();
                        }
                    }

                });
                //$('#grdRequerimientos tbody tr:eq(0)').show();
            }
            else {
                $('#grdRequerimientos tbody tr').show();
            }
        }


    </script>

</asp:Content>
