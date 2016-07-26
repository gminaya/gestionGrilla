$(document).ready(function () {
    $("#usuarioNombre").hide();
    $("#btnIniciar").click(function () {
        if ($("#usuario").val() != '') {
            if ($("#clave").val() != '') {
                iniciarSesion($("#usuario").val(), $("#clave").val());
            }
        }
    });

    $("#usuario").blur(function () {
        if ($("#usuario").val().length > 6) {
            selecionaUsuario($("#usuario").val());
        }
    });

    //$("#btnCambiarClave").click(function () {
    //    if ($('#contrasenaNueva').val() == $('#contrasenaRepite').val()) {
    //        cambiarClave($("#usuario").val(), $("#clave").val());
    //    }
    //    else {
    //        alert("La contraseña no coincide, por favor vuelva a intentar.");
    //        limpiarModal();
    //        $('#contrasenaNueva').focus();

    //    }
    //});

});

function iniciarSesion(usuario, password) {

    var param = "'usuario':'" + usuario + "','password':'" + password + "'";

    $.ajax({
        type: "POST",
        async: false,
        url: "Default.aspx/iniciarSesion",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == 1) {
                window.location.href = 'inicio.aspx';
            }
            else if (response.d == 2) {
                limpiarModal();
                $("#dvCambioClave").modal("show");
                alert("Favor renovar su contraseña")
                $("#contrasenaNueva").focus();
                return false;
            }
            else if (response.d == 0) {
                alert("Usuario no encontrado")
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: iniciarSesion");
        }
    });
}
function selecionaUsuario(usuario) {

    var param = "'usuario':'" + usuario + "'";
    $.ajax({
        type: "POST",
        async: true,
        url: "Default.aspx/selecionaUsuario",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d != "") {
                $('#usuario').hide();
                $('#usuarioNombre').val(response.d);
                $("#usuarioNombre").show();
            }
            else {
                alert("no encontrado");
                $.Notify({ caption: 'Usuario', content: 'No encontrado', icon: '<span class="mif-info"></span>', type: 'warning' });
                $('#usuario').val("").focus();
            }
           
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: selecionaUsuario");
        }
    });
}