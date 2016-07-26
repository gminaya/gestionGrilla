$(document).ready(function () {

    grillaVallas(4, 6);
    $("#btnGrillaVallas").click(function () {
        $("#grupobntGrillas").show();
    });
    $("#btnNuevaGrigada").click(function () {
        if ($("#nombre").val() != "" && $("#fechaOperacion").val() != "") {
            guardaGrigada();
        }
    });
    $("#filtro").change(function () {
        if ($("#filtro").val() == 1) {
            $("#fechaInicio").prop("disabled", false);
            $("#fechaFin").prop("disabled", true);
            $("#calle").prop("disabled", true);
        }
        else if ($("#filtro").val() == 2) {
            $("#fechaInicio").prop("disabled", false);
            $("#fechaFin").prop("disabled", false);
            $("#calle").prop("disabled", true);
        }
        else if ($("#filtro").val() == 3) {
            $("#fechaInicio").prop("disabled", true);
            $("#fechaFin").prop("disabled", true);
            $("#calle").prop("disabled", false);
        }
        else if ($("#filtro").val() == 4) {
            $("#fechaInicio").prop("disabled", false);
            $("#fechaFin").prop("disabled", true);
            $("#calle").prop("disabled", false);
        }
        else {
            $("#fechaInicio").prop("disabled", false);
            $("#fechaFin").prop("disabled", false);
            $("#calle").prop("disabled", false);

        }


    });
    $("#btnFiltrarGrilla").click(function () {
        if ($("#filtro").val() > 0) {
            grillaVallas(4, $("#filtro").val())
        }

    });
    $("#btnConsultarBrigada").click(function () {
        consultaBrigada()
        showDialog('#dvconsultaBrigada');
    });
    $("#btnDetalleBrigada").click(function () {
        $("#grupobntGrillas").hide();
        consultaDetellaBrigada()
    });
});

function grillaVallas(valor, numeroFiltro) {
    showDialog("#loader");
    $.ajax({
        type: "POST",
        async: true,
        url: "grillaOperaciones.aspx/grillaVallas",
        data: "{disponibilidad:" + valor + ",'segundoFiltro':" + numeroFiltro + ",'fechaInicio':'" + $("#fechaInicio").val() + "','fechaFin':'" + $("#fechaFin").val() + "','calle':'" + $("#calle").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            document.getElementById('dvGrilla').innerHTML = m;
            hideDialog("#loader");
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            hideDialog("#loader");
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: grillaVallas");
        }
    });
}
function agregarElemento(idElemento) {

    if ($('#idBrigada').val() > 0) {
        var param = "'idElemento':" + idElemento + ",'idBrigada':'" + $('#idBrigada').val() + "'";
        $.ajax({
            type: "POST",
            async: false,
            url: "grillaOperaciones.aspx/agregarElemento",
            data: "{" + param + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                eval(response.d);
                return false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

                alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: agregarElemento");
            }
        });
    }
    else {
        $.Notify({ caption: 'Elemento no agregado', content: 'Brigada no seleccionada', icon: '<span class="mif-cross"></span>', type: 'alert' });
    }

}
function guardaGrigada() {
    showDialog("#loader");
    var param = "'nombre':'" + $('#nombre').val() + "','fechaOperacion':'" + $('#fechaOperacion').val() + "'";
    $.ajax({
        type: "POST",
        async: true,
        url: "grillaOperaciones.aspx/guardaGrigada",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            hideDialog("#loader");
            eval(response.d);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            hideDialog("#loader");
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: guardaGrigada");
        }
    });
}
function selecionaBrigada(id) {

    var param = "'idBrigada':" + id + "";
    $.ajax({
        type: "POST",
        async: false,
        url: "grillaOperaciones.aspx/selecionaBrigada",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            hideDialog('#dvconsultaBrigada');
            $('#idBrigada').val(id);
           consultaDetellaBrigada();
            $(".nombreBrigada").html(response.d);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: selecionaCircuito");
        }
    });
}
function consultaBrigada() {

    $.ajax({
        type: "POST",
        async: false,
        url: "grillaOperaciones.aspx/consultaBrigada",
        // data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            document.getElementById('consultaBrigada').innerHTML = m;
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: consultaBrigada");
        }
    });
}
function consultaDetellaBrigada() {
    var param = "'idBrigada':" + $('#idBrigada').val() + "";
    if ( $('#idBrigada').val() != "") {
        $.ajax({
            type: "POST",
            async: false,
            url: "grillaOperaciones.aspx/consultaDetellaBrigada",
            data: "{" + param + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var m = response.d;
                document.getElementById('consultaDetalleBrigada').innerHTML = m;
                return false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: consultaDetellaCircuito");
            }
        });
    }
    else {
        $.Notify({ caption: 'Ninguna brigada seleccionada', content: 'Seleccione un circuito antes de continuar', icon: '<span class="mif-info"></span>', type: 'warning' });
    }

}
function eliminaElementoBrigada(idElemento) {
    $.ajax({
        type: "POST",
        async: true,
        url: "grillaOperaciones.aspx/eliminaElementoBrigada",
        data: "{'idElemento':" + idElemento + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            eval(response.d);
            consultaDetellaBrigada();
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: eliminaElementoBrigada");
        }
    });
}