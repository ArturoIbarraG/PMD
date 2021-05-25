﻿//Este primer Script es para el Primer Grid(Lineas Programadas)
$(document).ready(function () {
    //agregar una nueva columna con todo el texto 
    //contenido en las columnas de la grilla 
    // contains de Jquery es CaseSentive, por eso a minúscula
    $(".filtrar tr:has(td)").each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
                .hide().text(t).appendTo(this);
    });
    //Agregar el comportamiento al texto (se selecciona por el ID) 
    $("#texto").keyup(function () {
        var s = $(this).val().toLowerCase().split(" ");
        $(".filtrar tr:hidden").show();
        $.each(s, function () {
            $(".filtrar tr:visible .indexColumn:not(:contains('"
                     + this + "'))").parent().hide();
        });
    });
});

function texto_onclick() {

}

//Este Segundo Script es para el filtro en el Segundo Grid(Lineas Por Solicitud)
$(document).ready(function () {
    //agregar una nueva columna con todo el texto 
    //contenido en las columnas de la grilla 
    // contains de Jquery es CaseSentive, por eso a minúscula
    $(".filtrar2 tr:has(td)").each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
                .hide().text(t).appendTo(this);
    });
    //Agregar el comportamiento al texto (se selecciona por el ID) 
    $("#texto2").keyup(function () {
        var s = $(this).val().toLowerCase().split(" ");
        $(".filtrar2 tr:hidden").show();
        $.each(s, function () {
            $(".filtrar2 tr:visible .indexColumn:not(:contains('"
                     + this + "'))").parent().hide();
        });
    });
});

function texto_onclick() {

}