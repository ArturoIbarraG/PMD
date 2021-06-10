$(document).ready(function () {

    $("#txtOPMonto").keypress(function () {
        alert("funciona");
    });

    function numberOnly(evt) {
        alert("funciona");
        var ch = String.fromCharCode(evt.which);

        if (!(/[0-9]/.test(ch))) {
            evt.preventDefault();
        }
    }
});