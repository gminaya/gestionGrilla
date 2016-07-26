$(document).ready(function () {
    selectPropietarios("ID", "nombre", "calles", "calleC");
    selectPropietarios("ID", "nombre", "calles", "calle");
    selectPropietarios("ID", "nombre", "propietarios", "idPropietario");
    selectPropietarios("ID", "nombre", "sectores", "zonaC");
    selectPropietarios("ID", "nombre", "ciudades", "ciudadC");
    $("#rutaReal").hide();
    $("#btnGuardarValla").hide();
    $("#btnGuardar").click(function () {
        guardaCierre();
    });
    $("#btnGuardarValla").click(function () {
        guardaValla();

    });
    $("#btnBuscar").click(function () {
        consultaCierres();
        showDialog('#consulta');
    });
    $("#rutaImagen").change(function () {
        $("#rutaReal").val($("#rutaImagen").val());

    });
    $('#id').change(function () {
        valoresValla();
    });
    $('#idCierreC').change(function () {
        if ($('#idCierreC').val() > 0) {
            valoresCierre();
            consultaVallas($('#idCierreC').val());
            $("#btnGuardarValla").show();
        }


    });
   
})


function PreviewImage() {
    //var oFReader = new FileReader();
    //oFReader.readAsDataURL(document.getElementById("rutaImagen").files[0]);

    //oFReader.onload = function (oFREvent) {
    //    document.getElementById("uploadPreview").src = oFREvent.target.result;
    //};
}
function consultaVallas(id) {

    $.ajax({
        type: "POST",
        async: false,
        url: "espacios.aspx/consultaVallas",
        data: "{ 'id':" + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            document.getElementById('dvConsultaVallas').innerHTML = m;
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: consultaVallas");
        }
    });
}
function consultaCierres() {

    $.ajax({
        type: "POST",
        async: false,
        url: "espacios.aspx/consultaCierres",
        // data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var m = response.d;
            document.getElementById('dvConsultaCierre').innerHTML = m;
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: consultaCierres");
        }
    });
}
function guardaValla() {
    showDialog("#loader");
    var param = "'idCierre':" + $('#idCierreC').val() + ",'descripcion':'" + $('#descripcion').val() + "'";
    param += ",'calle':'" + $('#calle').val() + "', 'latitud':'" + $('#latitud').val() + "', 'longitud':'" + $('#longitud').val() + "'";
    param += ", 'tipo':'" + $('#tipo').val() + "' , 'disponibilidad':'" + $('#disponibilidad').val() + "','rutaImagen':'RUTA' , 'URLvideo':'" + $('#URLvideo').val() + "'";

    $.ajax({
        type: "POST",
        async: true,
        url: "espacios.aspx/guardaValla",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            hideDialog("#loader");
            eval(response.d);
            consultaVallas($('#idCierreC').val());
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            hideDialog("#loader");
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: guardaValla");
        }
    });
}
function guardaCierre() {
    showDialog("#loader");
    var param = "'idPropietario':" + $('#idPropietario').val() + ",'licencia':'" + $('#licencia').val() + "','descripcion':'" + $('#descripcionC').val() + "','ciudad':'" + $('#ciudadC').val();
    param += "','zona':'" + $('#zonaC').val() + "','calle':'" + $('#calleC').val() + "', 'latitud':'" + $('#latitudC').val() + "', 'longitud':'" + $('#longitudC').val() + "'";

    $.ajax({
        type: "POST",
        async: true,
        url: "espacios.aspx/guardaCierre",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            hideDialog("#loader");
            eval(response.d);

            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            hideDialog("#loader")
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: guardaCierre");
        }
    });
}

function buscaIdValla(id) {
    $('#id').val(id).change();
    
}
function buscaIdCierre(id) {
    $('#idCierreC').val(id).change();
    hideDialog("#consulta");
}
function valoresValla(id) {

    var param = "'id':" + $('#id').val() + "";

    $.ajax({
        type: "POST",
        async: false,
        url: "espacios.aspx/valoresValla",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            eval(response.d);
           
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: valoresValla");
        }
    });


}
function valoresCierre(id) {

    var param = "'id':" + $('#idCierreC').val() + "";

    $.ajax({
        type: "POST",
        async: false,
        url: "espacios.aspx/valoresCierre",
        data: "{" + param + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            eval(response.d);
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: valoresCierre");
        }
    });


}


function eliminaValla(idValla) {
    $.ajax({
        type: "POST",
        async: true,
        url: "espacios.aspx/eliminaValla",
        data: "{'idValla':" + idValla + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            eval(response.d);
            consultaVallas($('#idCierreC').val());
            return false;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Respuesta = " + XMLHttpRequest.responseText + "\n Estatus = " + textStatus + "\n Error = " + errorThrown + "Error: eliminaValla");
        }
    });
}
