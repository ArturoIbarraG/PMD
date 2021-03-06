<%@ Page Language="VB" AutoEventWireup="false" Title="Presupuesto" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.EventoPresupuesto" Codebehind="EventoPresupuesto.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .enlaceAnterior {
            cursor: pointer;
            text-decoration: underline !important;
            margin-right: 10px;
            font-size: 13px;
        }

        #enlaceRegresarAnterior {
            color: #444;
        }

        .item-asignacion input {
            max-width: 50px !important;
            text-align: right;
        }

        .table-footer span {
            text-align: right;
        }

        select {
            max-width: 100%;
            width: 350px;
        }

        input {
            max-width: 100%;
        }

        .item {
            padding: 7px;
        }

        .tabla-pmd > .item:nth-child(2n+1):not(:first-child) {
            background-color: #EEE;
        }

        .table {
            max-width: 100%;
        }

        .btn-header {
            float: right;
            font-size: 12px;
            cursor: pointer;
            text-decoration: underline !important;
        }

        .labelIndicador {
            height: 50px;
            display: block;
            border: 1px solid #DDD;
            margin-bottom: 15px;
            margin-top: 6px;
            padding: 7px;
        }

        #listadoInformacionGrafico, #listadoPresupuestoDetalle {
            margin: 0px;
            padding: 0px;
        }

            #listadoInformacionGrafico li, #listadoPresupuestoDetalle li {
                list-style: none;
                width: 100%;
                display: inline-block;
            }

                #listadoInformacionGrafico li a {
                    display: inline-block;
                }

                #listadoInformacionGrafico li span.badge {
                    width: 22px;
                    height: 20px;
                    display: inline-block;
                    border-radius: 4px;
                    margin-top: 4px;
                    border: 1px solid #777;
                }

                #listadoInformacionGrafico li a {
                    display: inline-block;
                    text-decoration: underline;
                    cursor: pointer;
                }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="updEventoPresupuesto" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Administración:</h6>
                        <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged" CssClass="form-control select-basic-simple"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Año</h6>
                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secretaría/Instituto:</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged" CssClass="form-control select-basic-simple"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Direcciones:</h6>
                        <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-control select-basic-simple" onchange="javascript:muestraGraficoPresupuesto();"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 col-md-8">
                        <h6 id="enlaceRegresarAnterior"></h6>
                        <br />
                        <div id="contenedorGrafico"></div>
                    </div>
                    <div class="col-12 col-md-4">
                        <ul id="listadoInformacionGrafico"></ul>
                        <hr />
                        <ul id="listadoPresupuestoDetalle"></ul>
                        <hr />
                        <h4 id="totalPresupuestoDetalle"></h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <asp:Panel ID="pnlEventoPresupuesto" runat="server">
                            <asp:GridView ID="grdEvento" runat="server" CssClass="table table-hover table-bordered" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="ID SUB ACTIVIDAD" DataField="Id_Subactividad" />
                                    <asp:BoundField HeaderText="SUB ACTIVIDAD" DataField="Subactividad" />
                                    <asp:BoundField HeaderText="INDICADOR" DataField="Indicador" />
                                    <asp:BoundField HeaderText="EVENTO" DataField="Evento" />
                                    <asp:BoundField HeaderText="TIPO EVENTO" DataField="TipoEvento" />
                                    <asp:BoundField HeaderText="FECHA" DataField="Fecha" />
                                    <asp:TemplateField HeaderText="Presupuesto">
                                        <ItemTemplate>
                                            <b style="text-decoration: underline; text-align: right;">
                                                <asp:LinkButton ID="lnkDetalle" runat="server" ToolTip="Desglose de presupuesto" Text='<%# String.Format("{0:c2}", Eval("Presupuesto")) %>' OnCommand="lnkDetalle_Command" CommandName="detalle" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                            </b>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-12">
                                                <h5>No hay información disponible</h5>
                                            </div>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlEventoDetalle" runat="server" Visible="false">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6">
                                        <asp:LinkButton ID="lnkRegresar" runat="server" OnClick="lnkRegresar_Click" Text="< REGRESAR"></asp:LinkButton>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-4">
                                        <h2>Evento</h2>
                                        <b>
                                            <asp:Label ID="lblNombreEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </div>
                                    <div class="col-4">
                                        <h2>Tipo de evento</h2>
                                        <b>
                                            <asp:Label ID="lblTipoEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </div>
                                    <div class="col-4">
                                        <h2>Fecha evento</h2>
                                        <b>
                                            <asp:Label ID="lblFechaEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView ID="grdDesglocePresupuesto" runat="server" CssClass="table table-hover table-bordered" Width="100%" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="SECRETARIA" DataField="Secretaria" />
                                                <asp:BoundField HeaderText="DEPENDENCIA" DataField="Direccion" />
                                                <asp:BoundField HeaderText="ID LINEA" DataField="Id_Linea" />
                                                <asp:BoundField HeaderText="LINEA" DataField="Descr_estrategia" />
                                                <asp:BoundField HeaderText="ID SUB ACTIVIDAD" DataField="Id_Subactividad" />
                                                <asp:BoundField HeaderText="SUBACTIVIDAD" DataField="Subactividad" />
                                                <asp:BoundField HeaderText="ID CLAVE GASTOS" DataField="Clave_Gastos" />
                                                <asp:BoundField HeaderText="CLAVE GASTOS" DataField="ClaveGastos" />
                                                <asp:TemplateField HeaderText="Presupuesto">
                                                    <ItemTemplate>
                                                        <b style="text-align: right;">

                                                            <asp:Label ID="lblTotal" runat="server" Style="cursor: pointer; text-decoration: underline;" data-clave='<%# Eval("Clave_Gastos") %>' data-evento='<%# Eval("Folio") %>' Text='<%# String.Format("{0:c2}", Eval("Total")) %>' onclick='javascript:abreModalEventos(this,event);'></asp:Label>
                                                        </b>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <h3>No hay información disponible</h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalDetalle" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Detalle Materiales</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div id="contentMaterial"></div>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-secondary right" data-dismiss="modal">Aceptar</a>
                    <div id="desglocePresupuesto"></div>

                </div>
            </div>
        </div>
    </div>
  
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/jquery-ui-1.12.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {
            //
            $(".datepicker").datepicker({ "dateFormat": "dd/mm/yy" });
        }

        $(document).ready(function () {
            $(".datepicker").datepicker({ "dateFormat": "dd/mm/yy" });
        });

        var IdActividadSeleccionada;
        var ActividadSeleccionada;
        var IdSubActividadSeleccionada;
        var SubActividadSeleccionada;
        function abreModalEventos(o, e) {
            var id = $(o).data('evento');
            var clave = $(o).data('clave');
            var urlAlertas = '<%= ResolveClientUrl("~/WSEventos.asmx/ObtieneMateriales") %>';
            console.log(clave);
            var params = '{id:' + id + ',clave:' + clave + '}'
            console.log(id);
            $.ajax({
                type: 'POST',
                url: urlAlertas,
                contentType: "application/json; charset=utf-8",
                dataType: 'text',
                data: params,
                success: function (data) {

                    try {
                        var json = JSON.parse(data).d;
                        json = JSON.parse(json);

                        if (json != undefined && json.length > 0) {
                            var html = '<table class="table table-hover"><thead><tr><th>MATERIAL</th><th>CANTIDAD</th><th>COSTO UNITARIO</th><th>TOTAL</th></tr></thead><tbody>';
                            for (var i = 0; i < json.length; i++) {
                                var m = json[i];
                                html += '<tr><td>' + m.Nombre + '</td><td>' + m.Cantidad + '</td><td>' + m.Costo + '</td><td>' + m.Total + '</td></tr>';
                            }

                            if (html.indexOf('</td></tr>') == -1)
                                html += '<tr><td colspan="4"><h5>NO HAY INFORMACION SIN MOSTRAR</h5></td></tr>';

                            html += '</tbody></table>';
                            $('#contentMaterial').html(html);
                            $('#modalDetalle').modal('show');
                        }
                        else {

                        }
                    }
                    catch (x) {
                        console.log(x);
                    }

                },
                error: function (xhr) {
                    console.log(xhr);
                }
            });
        }

        function muestraGraficoPresupuesto() {
            $('#enlaceRegresarAnterior').text('Desglose de Presupuesto de ' + $('select[id*="ddlDireccion"] option:selected').text());

            var urlPresupuesto = '<%= ResolveClientUrl("~/WSEventos.asmx/ObtienePresupuestoDireccionGrafico") %>';
            var secretaria = $('select[id*="ddlSecretaria"] option:selected').val();
            var direccion = $('select[id*="ddlDireccion"] option:selected').val();
            var anio = $('select[id*="ddlAnio"] option:selected').val();
            var params = '{anio:' + anio + ',secretaria:' + secretaria + ',direccion:' + direccion + '}';
            $.ajax({
                type: 'POST',
                url: urlPresupuesto,
                contentType: "application/json; charset=utf-8",
                dataType: 'text',
                data: params,
                success: function (data) {
                    try {
                        var content = $('#contenedorGrafico');
                        content.html('<h5>Obteniendo la informacion</h5>');

                        var json = JSON.parse(data).d;
                        json = JSON.parse(json);
                        var colorActual;
                        if (json != undefined && json.length > 0) {

                            var contenedorListado = $('#listadoInformacionGrafico');
                            contenedorListado.html('');
                            var etiquetas = [];
                            var presupuesto = [];
                            var colores = [];
                            for (i = 0; i < json.length; i++) {
                                var p = json[i];
                                var porcentaje = p.Porcentaje * 100;
                                var label = p.Descripcion + ' - $ ' + Number(p.Presupuesto).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }) + ' - ' + Number(porcentaje).toFixed(2) + '%';
                                etiquetas.push(label);
                                presupuesto.push(porcentaje);
                                var color = materialColor();
                                while (color == colorActual)
                                    color = materialColor();
                                colorActual = color;
                                colores.push(color);
                                var lista = $('<li />')
                                lista.html('<div class="col-1"><span class="badge" style="background-color:' + color + ';"></span></div><div class="col-11"><a title="Mostrar detalle" onclick="javascript:muestraDetalleActividad(\'' + p.Id + '\',\'' + p.Descripcion + '\');">' + label + '</a></div>');
                                contenedorListado.append(lista);
                            }

                            var configPie = {
                                type: 'pie',
                                data: {
                                    datasets: [{
                                        data: presupuesto,
                                        backgroundColor: colores,
                                        label: 'Actividades'
                                    }],
                                    labels: etiquetas
                                },
                                options: {
                                    responsive: true,
                                    legend: {
                                        display: false
                                    },
                                    tooltips: {
                                        mode: 'single',
                                        intersect: false,
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return data.labels[tooltipItem.index];
                                            }
                                        }
                                    },
                                }
                            };

                            content.html('');
                            content.append('<canvas id="canvasGrafico"></canvas>');
                            var ctx2 = document.getElementById('canvasGrafico').getContext('2d');
                            new Chart(ctx2, configPie);
                        }
                        else {
                            limpiaPresupuesto();
                        }
                    }
                    catch (x) {
                        console.log(x);
                        limpiaPresupuesto();
                    }

                    muestraDesglocePresupuestoGrafico('-1', '-1');
                },
                error: function (xhr) {
                    console.log(xhr);
                    limpiaPresupuesto();
                }
            });

            function limpiaPresupuesto() {
                $('#contenedorGrafico').html('<h5>No hay informacion que mostrar</h5>');
                $('#listadoInformacionGrafico').html('');
                limpiaDatosDesgloce();
            }
        }

        function muestraDetalleActividad(idActividad, actividad) {
            //
            $('#enlaceRegresarAnterior').html('<a class="enlaceAnterior" onclick="javascript:muestraGraficoPresupuesto();"><< ANTERIOR</a> Desglose de Presupuesto de la Actividad ' + actividad);

            IdActividadSeleccionada = idActividad;
            ActividadSeleccionada = actividad;
            var urlPresupuesto = '<%= ResolveClientUrl("~/WSEventos.asmx/ObtienePresupuestoActividadGrafico") %>';
            var secretaria = $('select[id*="ddlSecretaria"] option:selected').val();
            var direccion = $('select[id*="ddlDireccion"] option:selected').val();
            var anio = $('select[id*="ddlAnio"] option:selected').val();
            var params = '{anio:' + anio + ',secretaria:' + secretaria + ',direccion:' + direccion + ',actividad:"' + idActividad + '"}';
            $.ajax({
                type: 'POST',
                url: urlPresupuesto,
                contentType: "application/json; charset=utf-8",
                dataType: 'text',
                data: params,
                success: function (data) {
                    try {
                        var content = $('#contenedorGrafico');
                        content.html('<h5>Obteniendo la informacion</h5>');

                        var json = JSON.parse(data).d;
                        json = JSON.parse(json);
                        if (json != undefined && json.length > 0) {

                            var contenedorListado = $('#listadoInformacionGrafico');
                            contenedorListado.html('');
                            var etiquetas = [];
                            var presupuesto = [];
                            var colores = [];
                            var colorActual;
                            for (i = 0; i < json.length; i++) {
                                var p = json[i];
                                var porcentaje = p.Porcentaje * 100;
                                var label = p.Descripcion + ' - $ ' + Number(p.Presupuesto).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }) + ' - ' + Number(porcentaje).toFixed(2) + '%';
                                etiquetas.push(label);
                                presupuesto.push(porcentaje);
                                var color = materialColor();
                                while (color == colorActual)
                                    color = materialColor();
                                colorActual = color;

                                colores.push(color);
                                var lista = $('<li />')
                                lista.html('<div class="col-1"><span class="badge" style="background-color:' + color + ';"></span></div><div class="col-11"><a title="Mostrar detalle" onclick="javascript:muestraDetalleSubActividad(\'' + p.Id + '\',\'' + p.Descripcion + '\');">' + label + '</a></div>');
                                contenedorListado.append(lista);
                            }

                            var configPie = {
                                type: 'pie',
                                data: {
                                    datasets: [{
                                        data: presupuesto,
                                        backgroundColor: colores,
                                        label: 'Sub Actividades'
                                    }],
                                    labels: etiquetas
                                },
                                options: {
                                    responsive: true,
                                    legend: {
                                        display: false
                                    },
                                    tooltips: {
                                        mode: 'single',
                                        intersect: false,
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return data.labels[tooltipItem.index];
                                            }
                                        }
                                    },
                                }
                            };

                            content.html('');
                            content.append('<canvas id="canvasGrafico"></canvas>');
                            var ctx2 = document.getElementById('canvasGrafico').getContext('2d');
                            new Chart(ctx2, configPie);
                        }
                        else {
                            limpiaActividad();
                        }
                    }
                    catch (x) {
                        console.log(x);
                        limpiaActividad();
                    }

                    muestraDesglocePresupuestoGrafico(idActividad, '-1');
                },
                error: function (xhr) {
                    console.log(xhr);
                    limpiaActividad();
                }
            });

            function limpiaActividad() {
                $('#contenedorGrafico').html('<h5>No hay informacion que mostrar</h5>');
                $('#listadoInformacionGrafico').html('');
                limpiaDatosDesgloce();
            }
        }

        function muestraDetalleSubActividad(idSubActividad, subActividad) {
            //
            $('#enlaceRegresarAnterior').html('<a class="enlaceAnterior" onclick="javascript:muestraDetalleActividad(\'' + IdActividadSeleccionada + '\',\'' + ActividadSeleccionada + '\');"><< ANTERIOR</a> Desglose de Presupuesto de la Sub Actividad ' + subActividad);

            IdSubActividadSeleccionada = idSubActividad;
            SubActividadSeleccionada = subActividad;
            var urlPresupuesto = '<%= ResolveClientUrl("~/WSEventos.asmx/ObtienePresupuestoSubActividadGrafico") %>';
            var secretaria = $('select[id*="ddlSecretaria"] option:selected').val();
            var direccion = $('select[id*="ddlDireccion"] option:selected').val();
            var anio = $('select[id*="ddlAnio"] option:selected').val();
            var params = '{anio:' + anio + ',secretaria:' + secretaria + ',direccion:' + direccion + ',subActividad:"' + idSubActividad + '"}';
            $.ajax({
                type: 'POST',
                url: urlPresupuesto,
                contentType: "application/json; charset=utf-8",
                dataType: 'text',
                data: params,
                success: function (data) {
                    try {
                        var content = $('#contenedorGrafico');
                        content.html('<h5>Obteniendo la informacion</h5>');

                        var json = JSON.parse(data).d;
                        json = JSON.parse(json);
                        var colorActual;
                        if (json != undefined && json.length > 0) {

                            var contenedorListado = $('#listadoInformacionGrafico');
                            contenedorListado.html('');
                            var etiquetas = [];
                            var presupuesto = [];
                            var colores = [];
                            for (i = 0; i < json.length; i++) {
                                var p = json[i];
                                var porcentaje = p.Porcentaje * 100;
                                var label = p.Descripcion + ' - $ ' + Number(p.Presupuesto).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }) + ' - ' + Number(porcentaje).toFixed(2) + '%';
                                etiquetas.push(label);
                                presupuesto.push(porcentaje);
                                var color = materialColor();
                                while (color == colorActual)
                                    color = materialColor();
                                colorActual = color;
                                colores.push(color);
                                var lista = $('<li />')
                                lista.html('<div class="col-1"><span class="badge" style="background-color:' + color + ';"></span></div><div class="col-11"><label>' + label + '</label></div>');
                                contenedorListado.append(lista);
                            }

                            var configPie = {
                                type: 'pie',
                                data: {
                                    datasets: [{
                                        data: presupuesto,
                                        backgroundColor: colores,
                                        label: 'Sub Actividades'
                                    }],
                                    labels: etiquetas
                                },
                                options: {
                                    responsive: true,
                                    legend: {
                                        display: false
                                    },
                                    tooltips: {
                                        mode: 'single',
                                        intersect: false,
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return data.labels[tooltipItem.index];
                                            }
                                        }
                                    },
                                }
                            };

                            content.html('');
                            content.append('<canvas id="canvasGrafico"></canvas>');
                            var ctx2 = document.getElementById('canvasGrafico').getContext('2d');
                            new Chart(ctx2, configPie);
                        }
                        else {
                            limpiaDatos();
                        }
                    }
                    catch (x) {
                        console.log(x);
                        limpiaDatos();
                    }

                    muestraDesglocePresupuestoGrafico('-1', idSubActividad);
                },
                error: function (xhr) {
                    console.log(xhr);
                    limpiaDatos();
                }
            });

            function limpiaDatos() {
                $('#contenedorGrafico').html('<h5>No hay informacion que mostrar</h5>');
                $('#listadoInformacionGrafico').html('');
                limpiaDatosDesgloce();
            }
        }

        function muestraDesglocePresupuestoGrafico(actividad, subActividad) {
            var urlDesglocePresupuesto = '<%= ResolveClientUrl("~/WSEventos.asmx/ObtieneDesglocePresupuestoGrafico") %>';
            var secretaria = $('select[id*="ddlSecretaria"] option:selected').val();
            var direccion = $('select[id*="ddlDireccion"] option:selected').val();
            var anio = $('select[id*="ddlAnio"] option:selected').val();
            var params = '{anio:' + anio + ',secretaria:' + secretaria + ',direccion:' + direccion + ',actividad:"' + actividad + '",subActividad:"' + subActividad + '"}';
            $.ajax({
                type: 'POST',
                url: urlDesglocePresupuesto,
                contentType: "application/json; charset=utf-8",
                dataType: 'text',
                data: params,
                success: function (data) {
                    try {
                        var json = JSON.parse(data).d;
                        json = JSON.parse(json);
                        if (json != undefined && json.length > 0) {

                            var contenedorListado = $('#listadoPresupuestoDetalle');
                            contenedorListado.html('');
                            var total = 0;
                            for (i = 0; i < json.length; i++) {
                                var p = json[i];
                                total = total + p.Presupuesto;
                                var label = p.Descripcion;
                                var lista = $('<li />')
                                lista.html('<div class="col-7"><h5>' + label + '</h5></div><div class="col-5 text-right">$ ' + Number(p.Presupuesto).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }) + '</div>');
                                contenedorListado.append(lista);
                            }

                            //Muestra el Total
                            $('#totalPresupuestoDetalle').html('<div class="col-7">Total presupuesto:</div><div class="col-5 text-right">$' + Number(total).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }) + '</div>');
                        }
                        else {
                            limpiaDatosDesgloce();
                        }
                    }
                    catch (x) {
                        console.log(x);
                        limpiaDatosDesgloce();
                    }

                },
                error: function (xhr) {
                    console.log(xhr);
                    limpiaDatosDesgloce();
                }
            });
        }

        function limpiaDatosDesgloce() {
            $('#listadoPresupuestoDetalle').html('');
            $('#totalPresupuestoDetalle').html('');
            $('#enlaceRegresarAnterior').html('');
        }

        function pickRandomProperty(obj) {
            var result;
            var count = 0;
            for (var prop in obj)
                if (Math.random() < 1 / ++count)
                    result = prop;
            return result;
        }

        function materialColor() {
            // colors from https://github.com/egoist/color-lib/blob/master/color.json
            var colors = {
                "red": {
                    "50": "#ffebee",
                    "100": "#ffcdd2",
                    "200": "#ef9a9a",
                    "300": "#e57373",
                    "400": "#ef5350",
                    "500": "#f44336",
                    "600": "#e53935",
                    "700": "#d32f2f",
                    "800": "#c62828",
                    "900": "#b71c1c",
                    "hex": "#f44336",
                    "a100": "#ff8a80",
                    "a200": "#ff5252",
                    "a400": "#ff1744",
                    "a700": "#d50000"
                },
                "pink": {
                    "50": "#fce4ec",
                    "100": "#f8bbd0",
                    "200": "#f48fb1",
                    "300": "#f06292",
                    "400": "#ec407a",
                    "500": "#e91e63",
                    "600": "#d81b60",
                    "700": "#c2185b",
                    "800": "#ad1457",
                    "900": "#880e4f",
                    "hex": "#e91e63",
                    "a100": "#ff80ab",
                    "a200": "#ff4081",
                    "a400": "#f50057",
                    "a700": "#c51162"
                },
                "purple": {
                    "50": "#f3e5f5",
                    "100": "#e1bee7",
                    "200": "#ce93d8",
                    "300": "#ba68c8",
                    "400": "#ab47bc",
                    "500": "#9c27b0",
                    "600": "#8e24aa",
                    "700": "#7b1fa2",
                    "800": "#6a1b9a",
                    "900": "#4a148c",
                    "hex": "#9c27b0",
                    "a100": "#ea80fc",
                    "a200": "#e040fb",
                    "a400": "#d500f9",
                    "a700": "#aa00ff"
                },
                "deepPurple": {
                    "50": "#ede7f6",
                    "100": "#d1c4e9",
                    "200": "#b39ddb",
                    "300": "#9575cd",
                    "400": "#7e57c2",
                    "500": "#673ab7",
                    "600": "#5e35b1",
                    "700": "#512da8",
                    "800": "#4527a0",
                    "900": "#311b92",
                    "hex": "#673ab7",
                    "a100": "#b388ff",
                    "a200": "#7c4dff",
                    "a400": "#651fff",
                    "a700": "#6200ea"
                },
                "indigo": {
                    "50": "#e8eaf6",
                    "100": "#c5cae9",
                    "200": "#9fa8da",
                    "300": "#7986cb",
                    "400": "#5c6bc0",
                    "500": "#3f51b5",
                    "600": "#3949ab",
                    "700": "#303f9f",
                    "800": "#283593",
                    "900": "#1a237e",
                    "hex": "#3f51b5",
                    "a100": "#8c9eff",
                    "a200": "#536dfe",
                    "a400": "#3d5afe",
                    "a700": "#304ffe"
                },
                "blue": {
                    "50": "#e3f2fd",
                    "100": "#bbdefb",
                    "200": "#90caf9",
                    "300": "#64b5f6",
                    "400": "#42a5f5",
                    "500": "#2196f3",
                    "600": "#1e88e5",
                    "700": "#1976d2",
                    "800": "#1565c0",
                    "900": "#0d47a1",
                    "hex": "#2196f3",
                    "a100": "#82b1ff",
                    "a200": "#448aff",
                    "a400": "#2979ff",
                    "a700": "#2962ff"
                },
                "lightBlue": {
                    "50": "#e1f5fe",
                    "100": "#b3e5fc",
                    "200": "#81d4fa",
                    "300": "#4fc3f7",
                    "400": "#29b6f6",
                    "500": "#03a9f4",
                    "600": "#039be5",
                    "700": "#0288d1",
                    "800": "#0277bd",
                    "900": "#01579b",
                    "hex": "#03a9f4",
                    "a100": "#80d8ff",
                    "a200": "#40c4ff",
                    "a400": "#00b0ff",
                    "a700": "#0091ea"
                },
                "cyan": {
                    "50": "#e0f7fa",
                    "100": "#b2ebf2",
                    "200": "#80deea",
                    "300": "#4dd0e1",
                    "400": "#26c6da",
                    "500": "#00bcd4",
                    "600": "#00acc1",
                    "700": "#0097a7",
                    "800": "#00838f",
                    "900": "#006064",
                    "hex": "#00bcd4",
                    "a100": "#84ffff",
                    "a200": "#18ffff",
                    "a400": "#00e5ff",
                    "a700": "#00b8d4"
                },
                "teal": {
                    "50": "#e0f2f1",
                    "100": "#b2dfdb",
                    "200": "#80cbc4",
                    "300": "#4db6ac",
                    "400": "#26a69a",
                    "500": "#009688",
                    "600": "#00897b",
                    "700": "#00796b",
                    "800": "#00695c",
                    "900": "#004d40",
                    "hex": "#009688",
                    "a100": "#a7ffeb",
                    "a200": "#64ffda",
                    "a400": "#1de9b6",
                    "a700": "#00bfa5"
                },
                "green": {
                    "50": "#e8f5e9",
                    "100": "#c8e6c9",
                    "200": "#a5d6a7",
                    "300": "#81c784",
                    "400": "#66bb6a",
                    "500": "#4caf50",
                    "600": "#43a047",
                    "700": "#388e3c",
                    "800": "#2e7d32",
                    "900": "#1b5e20",
                    "hex": "#4caf50",
                    "a100": "#b9f6ca",
                    "a200": "#69f0ae",
                    "a400": "#00e676",
                    "a700": "#00c853"
                },
                "lime": {
                    "50": "#f9fbe7",
                    "100": "#f0f4c3",
                    "300": "#dce775",
                    "400": "#d4e157",
                    "500": "#cddc39",
                    "600": "#c0ca33",
                    "700": "#afb42b",
                    "800": "#9e9d24",
                    "900": "#827717",
                    "hex": "#cddc39",
                    "a100": "#f4ff81",
                    "a400": "#c6ff00",
                    "a700": "#aeea00"
                },
                "yellow": {
                    "50": "#fffde7",
                    "100": "#fff9c4",
                    "200": "#fff59d",
                    "300": "#fff176",
                    "400": "#ffee58",
                    "500": "#ffeb3b",
                    "600": "#fdd835",
                    "700": "#fbc02d",
                    "800": "#f9a825",
                    "900": "#f57f17",
                    "hex": "#ffeb3b",
                    "a100": "#ffff8d",
                    "a200": "#ffff00",
                    "a400": "#ffea00",
                    "a700": "#ffd600"
                },
                "amber": {
                    "50": "#fff8e1",
                    "100": "#ffecb3",
                    "200": "#ffe082",
                    "300": "#ffd54f",
                    "400": "#ffca28",
                    "500": "#ffc107",
                    "600": "#ffb300",
                    "700": "#ffa000",
                    "800": "#ff8f00",
                    "900": "#ff6f00",
                    "hex": "#ffc107",
                    "a100": "#ffe57f",
                    "a200": "#ffd740",
                    "a400": "#ffc400",
                    "a700": "#ffab00"
                },
                "orange": {
                    "50": "#fff3e0",
                    "100": "#ffe0b2",
                    "200": "#ffcc80",
                    "300": "#ffb74d",
                    "400": "#ffa726",
                    "500": "#ff9800",
                    "600": "#fb8c00",
                    "700": "#f57c00",
                    "800": "#ef6c00",
                    "900": "#e65100",
                    "hex": "#ff9800",
                    "a100": "#ffd180",
                    "a200": "#ffab40",
                    "a400": "#ff9100",
                    "a700": "#ff6d00"
                },
                "deepOrange": {
                    "50": "#fbe9e7",
                    "100": "#ffccbc",
                    "200": "#ffab91",
                    "300": "#ff8a65",
                    "400": "#ff7043",
                    "500": "#ff5722",
                    "600": "#f4511e",
                    "700": "#e64a19",
                    "800": "#d84315",
                    "900": "#bf360c",
                    "hex": "#ff5722",
                    "a100": "#ff9e80",
                    "a200": "#ff6e40",
                    "a400": "#ff3d00",
                    "a700": "#dd2c00"
                },
                "brown": {
                    "50": "#efebe9",
                    "100": "#d7ccc8",
                    "200": "#bcaaa4",
                    "300": "#a1887f",
                    "400": "#8d6e63",
                    "500": "#795548",
                    "600": "#6d4c41",
                    "700": "#5d4037",
                    "800": "#4e342e",
                    "900": "#3e2723",
                    "hex": "#795548"
                },
                "grey": {
                    "300": "#e0e0e0",
                    "500": "#9e9e9e",
                    "600": "#757575",
                    "700": "#616161",
                    "800": "#424242",
                    "hex": "#9e9e9e"
                },
                "blueGrey": {
                    "100": "#cfd8dc",
                    "200": "#b0bec5",
                    "300": "#90a4ae",
                    "400": "#78909c",
                    "500": "#607d8b",
                    "600": "#546e7a",
                    "700": "#455a64",
                    "800": "#37474f",
                    "900": "#263238",
                    "hex": "#607d8b"
                }
            }
            // pick random property
            //var property = pickRandomProperty(colors);
            var colorList = colors[pickRandomProperty(colors)];
            var newColorKey = pickRandomProperty(colorList);
            var newColor = colorList[newColorKey];
            return newColor;
        }

        function hexToRgb(hex) {
            var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
            return result ? {
                r: parseInt(result[1], 16),
                g: parseInt(result[2], 16),
                b: parseInt(result[3], 16)
            } : null;
        }
    </script>
</asp:Content>
