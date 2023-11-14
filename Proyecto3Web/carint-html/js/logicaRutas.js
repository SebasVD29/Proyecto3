
$("#buscarRuta").click(function () {

    var identificacion = $('#form-identificacion').val();
    var array; 
    removeOptions(document.getElementById('rutaSelect'));
    if (!validateId(identificacion)) {
        alert("El numero de identificacion solo numeros y debe tener al menos 9 digitos")
        return;
    }  
    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/RutasPorCliente/" + identificacion,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            array = response;
            for (let i = 0; i < array.length; i++) {
                $('#rutaSelect').append($('<option>', {
                    value: array[i][0],
                    text: array[i][1]
                }));
            }
            if (!array.length) alert("Rutas no Encontradas o Cliente no existe")
        },
        failure: function (response) {
            alert("Error")
        }
    });

});

$("#cargarRuta").click(function () {

    removeOptions(document.getElementById('DropDownChofer'));
    removeOptions(document.getElementById('DropDownCamion'));

   

    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/choferesControllers",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            array = response;
            for (let i = 0; i < array.length; i++) {
                $('#DropDownChofer').append($('<option>', {
                    value: array[i][0],
                    text: array[i][1] + " " + array[i][2]
                }));
            }
            if (!array.length) alert("Choferes no Encontradas o Cliente no existe")
        },
        failure: function (response) {
            alert("Error")
        }
    });

    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/Camiones",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            array = response;
            for (let i = 0; i < array.length; i++) {
                $('#DropDownCamion').append($('<option>', {
                    value: array[i][0],
                    text: array[i][0] 
                }));
            }
            if (!array.length) alert("Camiones no Encontradas o Cliente no existe")
        },
        failure: function (response) {
            alert("Error")
        }
    });


    var identificacion = $('#rutaSelect').val();

    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/Rutas/" + identificacion,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#nombreRuta').val(response[0]);
            $('#finalPaisRuta').val(response[1]);
            $('#finalCiudadRuta').val(response[2]);
        },
        failure: function (response) {
            alert("Error")
        }
    });

});


$("#editarRuta").click(function () {

    var identificacion = $('#rutaSelect').val();
    var direccion = $('#direccionRuta').val();
    var chofer = $('#DropDownChofer').val();  //Chofer
    var camion = $('#DropDownCamion').val();    //Camion
    var estado = $('#estado').val();  // Estado
    var inicio = $("#fechaInicioRuta").val();  // Fecha inicio
    var final = $("#fechaFinalRuta").val();  // Fecha final

    jQuery.ajax({
        type: 'put',
        url: "https://localhost:7088/api/Rutas/" + identificacion + "?descripcion=" + direccion + "&idChofer=" + chofer + "&placa=" + camion + "&estado=" + estado + "&inicio=" + inicio + "&final=" + final + "",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("La ruta fue guardada exitosamente");
            $('#modalup').trigger('click');

            setTimeout(
                function () {
                    location.reload();
                }, 3000);
        },
        failure: function (response) {
            alert("Error: datos no guardados")
        }
    });

});


function removeOptions(selectElement) {
    var i, L = selectElement.options.length - 1;
    for (i = L; i >= 0; i--) {
        selectElement.remove(i);
    }
}

function validateId(Identificacion) {

    var pattern = new RegExp("\\d{9}");
    return pattern.test(Identificacion);

}