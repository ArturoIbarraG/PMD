(function ($) {
    try { $('.select-basic-simple').select2(); } catch (x) { console.error(x); }

    function timerAlertas() {
        setTimeout(function () {
            cargaAlertas();
            timerAlertas();
        }, 60000);
    }
    cargaAlertas();
    timerAlertas();

    cargaAlertas();
})(jQuery);

$(document).ready(function () {
    //Carga el Anio
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
    try { $('.select-basic-simple').select2(); } catch (x) { console.error(x); }
});

function actualizaAnioProgressPresupuesto() {
    var anio = $('#comboAnioProgressPresupuesto option:selected').val();
    setCookie("pmd_anio", anio, 365);
    muestraProgressPresupuesto();
}

// request permission on page load
document.addEventListener('DOMContentLoaded', function () {
    if (!Notification) {
        alert('Desktop notifications not available in your browser. Try Chromium.');
        return;
    }

    if (Notification.permission !== 'granted') {
        Notification.requestPermission(function () {
            setTimeout(function () {
                //notifyMe('test','');
            }, 500);
        });
    }
});

function notifyMe(titulo, mensaje, url) {
    if (Notification.permission !== 'granted')
        Notification.requestPermission();
    else {
        var notification = new Notification(titulo, {
            icon: '/Images/san_nicolas.png',
            body: mensaje,
        });
        notification.onclick = function () {
            window.focus();
        };

    }
}

var window_focus = true;

window.onblur = function () { console.log('false'); window_focus = false; }
window.onfocus = function () { console.log('true'); window_focus = true; }

function marcaLeidos() {
    $.ajax({
        type: 'POST',
        url: urlMarcaLeidos,
        contentType: "application/json; charset=utf-8",
        dataType: 'text',
        data: params,
        success: function (data) {

        },
        error: function (data) {

        }
    });
}

function cargaAlertas() {
    try {

        //agrega el evento de check
        $('#chkAlertas').unbind('change');
        $('#chkAlertas').change(function () {
            if ($('.toggle-content').length > 0) {
                $.ajax({
                    type: 'POST',
                    url: urlMarcaLeidos,
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataTransfer: 'json',
                    success: function (data) {
                        setTimeout(function () {
                            $('div.alert-action').attr("data-count", null);
                            $('.sinLeer').removeClass('sinLeer');
                        }, 2000);
                    },
                    error: function (xhr) {
                    }
                });
            }
        });

        var countSinLeer = 0;
        var mensaje = '';
        var url = '';
        $.ajax({
            type: 'POST',
            url: urlAlertas,
            contentType: "application/json; charset=utf-8",
            dataType: 'text',
            data: params,
            success: function (data) {
                try {
                    var json = JSON.parse(data).d;

                    //Alertas
                    var alertasParent = $('div.alert-action');
                    json = JSON.parse(json);

                    if (json != undefined && json.length > 0) {

                        countSinLeer = 0;
                        $('#divAlertas  .toggle-content').remove();
                        parentDiv = $('<div class="toggle-content"><div class="header">MIS MENSAJES</div></div>');
                        for (i = 0; i < json.length; i++) {
                            var alertas = json[i];
                            url = alertas.Url;

                            mensaje = alertas.Mensaje;
                            console.log(alertas.Leido);
                            var tipoClass = '';
                            if (alertas.Leido === false) {
                                countSinLeer++;
                                tipoClass = 'sinLeer';
                            }

                            var body = $('<a />').addClass(tipoClass).attr('href', url);
                            body.append($('<span class="fecha">Fecha alerta: ' + alertas.Fecha + '</span>'));
                            //body.append($('<span class="opcion">Cliente: ' + alertas.cliente + '</span>'));
                            body.append($('<div class="mensaje" />').html('<h5>' + alertas.Mensaje + '</h5>'));
                            //body.attr('data-estatus', alertas.estatus);
                            parentDiv.append(body);
                        }
                        alertasParent.find('>label').append(parentDiv);
                        if (countSinLeer > 0)
                            alertasParent.attr("data-count", countSinLeer);
                        else
                            alertasParent.attr("data-count", undefined);

                        //MOSTRAR NOTIFICACIONES
                        console.log('FOCUS ACTUAL: ' + window_focus);
                        if (window_focus == false) {
                            if (countSinLeer == 1) {
                                setTimeout(function () {
                                    notifyMe('Nueva actividad', mensaje, url);
                                }, 500);

                            }
                            else if (countSinLeer > 1) {
                                notifyMe('Nueva actividad', 'Tienes nuevos mensajes.', url);
                            }
                        }
                    }
                    else {
                        alertasParent.attr("data-count", undefined);
                        $('#divAlertas  .toggle-content').remove();
                        alertasParent.find('>label').append($('<div class="toggle-content"><div class="header">Alertas</div><div class="empty"> <i class="fas fa-inbox"></i> <span>Tu bandeja está vacia</span></div></div>'));
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
    catch (x) {
        console.log(x);
    }
}

function mensajeCustom(tipo, titulo, texto) {
    swal({
        type: tipo,
        title: titulo,
        text: texto,
        confirmButtonText: 'Aceptar'
    });
}

function ProcExitoso1() {
    alert("Se Ha Guardado Exitosamente");

}

function validaNumeros(obj, e) {
    var key = e.keyCode ? e.keyCode : e.which;
    if ((key > 47 && key < 58) || key == 190 || key == 8 || key == 9 || (key > 36 && key < 41) || key == 46)
        return true;

    e.preventDefault();
    return false;
}

function validaCampos(obj, e) {
    try {
        var lnk = $(obj);
        var parent = $(lnk.data('target'));
        var correcto = true;
        parent.find('.campo_obligatorio.error').removeClass('error');

        if (lnk.attr('class').indexOf('sub') > -1) {
            parent.find('.campo_obligatorio.sub').each(function () {
                var obj = $(this);
                if (obj.is('input')) {
                    if (obj.val() == '') {
                        correcto = false;
                        obj.addClass('error');
                    }
                }
                else if (obj.is('select')) {
                    if (obj.find('option:selected').val() < 0) {
                        correcto = false;
                        obj.addClass('error');
                    }
                }
                else if (obj.is('textarea')) {
                    if (obj.val() == '') {
                        correcto = false;
                        obj.addClass('error');
                    }
                }

            });

        }
        else {
            parent.find('.campo_obligatorio:not(.sub)').each(function () {

                var obj = $(this);
                if (obj.is('input')) {
                    if (obj.val() == '') {
                        correcto = false;
                        obj.addClass('error');
                    }
                }
                else if (obj.is('select')) {
                    if (obj.find('option:selected').val() < 0) {
                        correcto = false;
                        obj.addClass('error');
                    }
                }
                else if (obj.is('textarea')) {
                    if (obj.val() == '') {
                        correcto = false;
                        obj.addClass('error');
                    }
                }
                else if (obj.is('table')) {
                    var txt = obj.find('input:first');
                    if (txt.val() == '') {
                        correcto = false;
                        txt.addClass('error');
                    }
                }
            });
        }

        if (!correcto) {
            if ($('.apfm-container.apfm-open').length > 0) {

                $('.apfm-container.apfm-open').scrollTop($('.campo_obligatorio.error').first().offset().top);
            }
            else {
                //  alert('2');
                $(window).scrollTop($('.campo_obligatorio.error').first().offset().top);
            }

            e.stopPropagation();
            e.preventDefault();
            return false;
        }
    }
    catch (x) {
        console.log(x);
    }
}

function cargaAlertas() {
    try {

        //agrega el evento de check
        $('#chkAlertas').unbind('change');
        $('#chkAlertas').change(function () {
            if ($('.toggle-content').length > 0) {
                $.ajax({
                    type: 'POST',
                    url: urlMarcaLeidos,
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataTransfer: 'json',
                    success: function (data) {
                        setTimeout(function () {
                            $('div.alert-action').attr("data-count", null);
                            $('.sinLeer').removeClass('sinLeer');
                        }, 2000);
                    },
                    error: function (xhr) {
                    }
                });
            }
        });

        var countSinLeer = 0;
        var mensaje = '';
        var url = '';
        $.ajax({
            type: 'POST',
            url: urlAlertas,
            contentType: "application/json; charset=utf-8",
            dataType: 'text',
            data: params,
            success: function (data) {
                try {
                    var json = JSON.parse(data).d;

                    //Alertas
                    var alertasParent = $('div.alert-action');
                    json = JSON.parse(json);

                    if (json != undefined && json.length > 0) {

                        countSinLeer = 0;
                        $('#divAlertas  .toggle-content').remove();
                        parentDiv = $('<div class="toggle-content"><div class="header">MIS MENSAJES</div></div>');
                        for (i = 0; i < json.length; i++) {
                            var alertas = json[i];
                            url = alertas.Url;

                            mensaje = alertas.Mensaje;
                            console.log(alertas.Leido);
                            var tipoClass = '';
                            if (alertas.Leido === false) {
                                countSinLeer++;
                                tipoClass = 'sinLeer';
                            }

                            var body = $('<a />').addClass(tipoClass).attr('href', url);
                            body.append($('<span class="fecha">Fecha alerta: ' + alertas.Fecha + '</span>'));
                            //body.append($('<span class="opcion">Cliente: ' + alertas.cliente + '</span>'));
                            body.append($('<div class="mensaje" />').html('<h5>' + alertas.Mensaje + '</h5>'));
                            //body.attr('data-estatus', alertas.estatus);
                            parentDiv.append(body);
                        }
                        alertasParent.find('>label').append(parentDiv);
                        if (countSinLeer > 0)
                            alertasParent.attr("data-count", countSinLeer);
                        else
                            alertasParent.attr("data-count", undefined);

                        //MOSTRAR NOTIFICACIONES
                        console.log('FOCUS ACTUAL: ' + window_focus);
                        if (window_focus == false) {
                            if (countSinLeer == 1) {
                                setTimeout(function () {
                                    notifyMe('Nueva actividad', mensaje, url);
                                }, 500);

                            }
                            else if (countSinLeer > 1) {
                                notifyMe('Nueva actividad', 'Tienes nuevos mensajes.', url);
                            }
                        }
                    }
                    else {
                        alertasParent.attr("data-count", undefined);
                        $('#divAlertas  .toggle-content').remove();
                        alertasParent.find('>label').append($('<div class="toggle-content"><div class="header">Alertas</div><div class="empty"> <i class="fas fa-inbox"></i> <span>Tu bandeja está vacia</span></div></div>'));
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
    catch (x) {
        console.log(x);
    }
}

function muestraProgressPresupuesto() {
    var anio = getCookie("pmd_anio");
    if (anio === '') {
        var today = new Date();
        setCookie("pmd_anio", today.getFullYear(), 365);
        anio = getCookie("pmd_anio");
    }
    var direccion = $('#hdnDireccionMasterModal').val();
    var params = '{anio:' + anio + ',dependencia:'+ direccion +'}';
    $.ajax({
        type: 'POST',
        url: urlProgressPresupuesto,
        contentType: "application/json; charset=utf-8",
        dataType: 'text',
        data: params,
        success: function (data) {
            try {
                var json = JSON.parse(data).d;
                json = JSON.parse(json)[0];
                var contentProgress = $('#contentProgressPresupuestoMaster');
                contentProgress.html('');

                //Muestra Presupuesto Capturado
                var capturado = (json.Capturado / json.Presupuesto) * 100;
                contentProgress.append($('<div class="progress-bar progress-bar-striped progress-bar-animated bg-info" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: ' + capturado +'%"></div>'));

                //Muestra Presupuesto Autorizado
                var autorizado = (json.Autorizado / json.Presupuesto) * 100;
                contentProgress.append($('<div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: ' + autorizado +'%"></div>'));

                //Muestra Presupuesto Comprometido
                var comprometido = (json.Comprometido / json.Presupuesto) * 100;
                contentProgress.append($('<div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: ' + comprometido + '%"></div>'));

                //Muestra Presupuesto Devengado
                var devengado = (json.Devengado / json.Presupuesto) * 100;
                contentProgress.append($('<div class="progress-bar progress-bar-striped progress-bar-animated bg-warning" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: ' + devengado  +'%"></div>'));
            }
            catch (x) {
                $('#progressPresupuestoMaster').css('width', '0%');
                console.log(x);
            }
        },
        error: function (xhr) {
            console.log(xhr);
            $('#progressPresupuestoMaster').css('width', '0%');
        }
    });
}

function muestraInformacionPresupuesto() {
    $('#modalPresupuesto').modal('show');
    
    $('div.cargando-info').show();
    var admon = $('select[id*="ddlAdmonModal"] option:selected').val();
    var secretaria = $('select[id*="ddlSecretariaModal"] option:selected').val();
    var direccion = $('select[id*="ddlDireccionModal"] option:selected').val();
    var anio = $('select[id*="ddlAnioModal"] option:selected').val();
    var params = '{admon:' + admon + ',anio:' + anio + ',secretaria:' + secretaria + ',direccion:' + direccion + '}';
    $.ajax({
        type: 'POST',
        url: urlPresupuesto,
        contentType: "application/json; charset=utf-8",
        dataType: 'text',
        data: params,
        success: function (data) {
            try {
                var json = JSON.parse(data).d;
                json = JSON.parse(json);
                if (json.length > 0) {
                    var contenedor = $('#contenedorGraficoInfoPresupuesto');
                    contenedor.html('');
                    for (var i = 0; i < json.length; i++) {
                        var p = json[i];
                        var etiquetas = ['Presupuesto capturado: $' + Number(p.Capturado).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }),
                        'Presupuesto autorizado: $' + Number(p.Autorizado).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }),
                        'Presupuesto devengado: $' + Number(p.Devengado).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; }),
                        'Presupuesto comprometido: $' + Number(p.Comprometido).toFixed(2).replace(/./g, function (c, i, a) { return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c; })];
                        var totales = [p.Capturado, p.Autorizado, p.Devengado, p.Comprometido];
                        var colores = ['#17a2b8', '#007bff', '#ffc107', '#28a745'];

                        var configPie = {
                            type: 'pie',
                            data: {
                                datasets: [{
                                    data: totales,
                                    backgroundColor: colores,
                                    label: 'Presupuesto'
                                }],
                                labels: etiquetas
                            },
                            options: {
                                responsive: true,
                                legend: {
                                    display: true
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

                        var canvasName = 'canvasPie_' + p.IdDireccion;
                        contenedor.append('<div class="row"><div class="col-6"><h6>' + p.Direccion + '</h6><br /><canvas id="' + canvasName + '"></canvas></div></div>');
                        var ctx2 = document.getElementById(canvasName).getContext('2d');
                        new Chart(ctx2, configPie);
                    }
                }
                else {
                    $('#contenedorGraficoInfoPresupuesto').html('<h5>No hay informacion que mostrar</h5>');
                }
                terminaProceso();
            }
            catch (x) {
                console.log(x);
                terminaProceso();
            }

        },
        error: function (xhr) {
            console.log(xhr);
            terminaProceso();
        }
    });

    function terminaProceso() {
        $('div.cargando-info').hide();
    }

}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
