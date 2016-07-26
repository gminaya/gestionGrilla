$(document).ready(function () {
    $('select').select2();
    $("#tab-control").tabcontrol();
    //$("#progressBar").hide();
    })


$(function () {
    $('.sidebar').on('click', 'li', function () {
        if (!$(this).hasClass('active')) {
            $('.sidebar li').removeClass('active');
            $(this).addClass('active');
        }
    })
})
function showDialog(id) {
    var dialog = $(id).data('dialog');
    dialog.open();
}
function hideDialog(id) {
    var dialog = $(id).data('dialog');
    dialog.close();
}
function showCharms(id) {
    var charm = $(id).data("charm");
    if (charm.element.data("opened") === true) {
        charm.close();
    } else {
        charm.open();
    }
}
function selectPropietarios(valor, nombre, tabla,  id) {
    $.ajax({
        type: "POST",
        async: false,
        url: "/helper.aspx/selectPropietarios",
        data: "{'valor':'" + valor + "', 'nombre':'" + nombre + "', 'tabla':'" + tabla + "', 'id':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            eval(response.d);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: selectPropietarios");
        }
    });
}
