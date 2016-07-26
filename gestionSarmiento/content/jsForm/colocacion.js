$(document).ready(function () {

    $("#btnConsultarCircuito").click(function () {
        consultaCircuitos()
        showDialog('#dvconsultaCircuito');
    });
    $("#btnAsignar").click(function () {
        asignarColocacion();
    });

});

function consultaCircuitos() {

    $.ajax({
        type: "POST",
        async: false,
        url: "colocacion.aspx/consultaCircuitos",
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
            url: "colocacion.aspx/consultaDetellaCircuito",
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
function selecionaCircuito(id) {

    var param = "'idCircuito':" + id + "";
    $.ajax({
        type: "POST",
        async: true,
        url: "colocacion.aspx/selecionaCircuito",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            hideDialog('#dvconsultaCircuito');
            $('#idCircuito').val(id);
            consultaDetellaCircuito();
            $(".nombreCircuito").html(response.d);
            valoresCliente(id)
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: selecionaCircuito");
        }
    });
}
function valoresCliente(idCircuito) {
    var param = "'id':" + idCircuito + "";
    $.ajax({
        type: "POST",
        async: false,
        url: "colocacion.aspx/valoresCliente",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            eval(response.d);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: valoresCliente");
        }
    });


}

function valoresElemento(id) {
    var param = "'id':" + id + "";
    $.ajax({
        type: "POST",
        async: false,
        url: "colocacion.aspx/valoresElemento",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            eval(response.d);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: valoresElemento");
        }
    });


}

function asignarColocacion() {
    showDialog("#loader");
    var param = "'idElemento':" + $('#idElemento').val() + ",'idCliente':'" + $('#idCliente').val() + "','arte':'" + $('#arte').val() + "','fechaInicio':'" + $('#fechaInicio').val() + "','fechaFin':'" + $('#fechaFin').val() + "'";
   

    $.ajax({
        type: "POST",
        async: true,
        url: "colocacion.aspx/asignarColocacion",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            hideDialog("#loader");
            eval(response.d);
            consultaDetellaCircuito();
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            hideDialog("#loader")
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: asignarColocacion");
        }
    });
}