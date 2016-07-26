$(document).ready(function () {
    grillaVallas(4, 6);
    selectPropietarios("ID", "nombre", "clientes", "idCliente");
    $("#btnNuevoCircuito").click(function () {
        guardaCircuito();
    });
    $("#btnConsultarCircuito").click(function () {
        consultaCircuitos()
        showDialog('#dvconsultaCircuito');
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
    $("#btnDetalleCircuito").click(function () {
        $("#grupobntGrillas").hide();
        consultaDetellaCircuito()
    });
    $("#btnGrillaVallas").click(function () {
        $("#grupobntGrillas").show();
    });
    $("#btnGuardaPDF").click(function () {
        
    });
    
    $(".btnAgregar").hide(); //<-----------------------------------------
});

function grillaVallas(valor, numeroFiltro) {
    showDialog("#loader");
    $.ajax({
        type: "POST",
        async: true,
        url: "grilla.aspx/grillaVallas",
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
function guardaCircuito() {
    showDialog("#loader");
    var param = "'idCliente':" + $('#idCliente').val() + ",'nombre':'" + $('#nombre').val() + "'";
    $.ajax({
        type: "POST",
        async: true,
        url: "grilla.aspx/guardaCircuito",
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
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: guardaCircuito");
        }
    });
}
function agregarElemento(idElemento) {

    if ($('#idCircuito').val() > 0) {
        var param = "'idElemento':" + idElemento + ",'idCircuito':'" + $('#idCircuito').val() + "'";
        $.ajax({
            type: "POST",
            async: false,
            url: "grilla.aspx/agregarElemento",
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
        $.Notify({ caption: 'Elemento no agregado', content: 'Circuito no seleccionado', icon: '<span class="mif-cross"></span>', type: 'alert' });
    }

}
function selecionaCircuito(id) {

    var param = "'idCircuito':" + id + "";
    $.ajax({
        type: "POST",
        async: false,
        url: "grilla.aspx/selecionaCircuito",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            hideDialog('#dvconsultaCircuito');
            $('#idCircuito').val(id);
            consultaDetellaCircuito();
            $(".nombreCircuito").html(response.d);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: selecionaCircuito");
        }
    });
}
function consultaCircuitos() {

    $.ajax({
        type: "POST",
        async: false,
        url: "grilla.aspx/consultaCircuitos",
        // data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            document.getElementById('consultaCircuito').innerHTML = m;
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: consultaCircuitos");
        }
    });
}
function consultaDetellaCircuito() {
    var param = "'idCircuito':" + $('#idCircuito').val() + "";
    if ($('#idCircuito').val() > 0 || $('#idCircuito').val() != "") {
        $.ajax({
            type: "POST",
            async: false,
            url: "grilla.aspx/consultaDetellaCircuito",
            data: "{" + param + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var m = response.d;
                document.getElementById('consultaDetalleCircuito').innerHTML = m;
                return false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: consultaDetellaCircuito");
            }
        });
    }
    else {
        $.Notify({ caption: 'Ningún circuito seleccionado', content: 'Seleccione un circuito antes de continuar', icon: '<span class="mif-info"></span>', type: 'warning' });
    }

}
function eliminaVallaCircuito(idValla) {
    $.ajax({
        type: "POST",
        async: true,
        url: "grilla.aspx/eliminaVallaCircuito",
        data: "{'idValla':" + idValla + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            eval(response.d);
            consultaDetellaCircuito();
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: eliminaVallaCircuito");
        }
    });
}