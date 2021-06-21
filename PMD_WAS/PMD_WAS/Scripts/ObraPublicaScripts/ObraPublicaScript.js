$(document).ready(function () {

    function IsNumber(evt) {
        alert("Funciona!");
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }

    function muestraErrorFaltanCampos() {
        alert("Faltan campos por llenar.");
    }

    function muestraConfirmaAutorizacion() {
        alert("Obra Pública creada correctamente.");
    }

});