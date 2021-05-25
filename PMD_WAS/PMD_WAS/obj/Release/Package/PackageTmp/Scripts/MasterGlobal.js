(function ($) {
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

function marcaLeidos(){
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

function infor() {
    alert("Ha entrado");
    Sexy.info('<br/><p> Gracias!!  <br/> Tu Opinión es Muy Importante </p>');
}
function error() {
    alert("Ha entrado");
    Sexy.info('<p>No hay ninguna secretaría asignada para este filtro</p>');
}
function MensajeRedireccionar() {
    var myTextField = document.getElementById("TextBox3").value;
    if (confirm("Quieres Continuar?") == true) {
        location.href = 'Consulta.aspx';
        //window.open('Consulta.aspx', '_self ');
    }
}

function redondeo2decimales(numero) {
    var original = parseFloat(numero);
    var result = Math.round(original * 100) / 100;
    return result;
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

    if (key == 8) { // BACKSPACE para Firefox
        return true;
    }
    else if (key < 46 || key > 57) {
        return false;
    }


    return true;
}

