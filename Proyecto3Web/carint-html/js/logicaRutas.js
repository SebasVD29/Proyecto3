
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
        error: function (xhr, status, error) {
            responseData = xhr.responseJSON;
            if (responseData) {
                alert("Error: " + responseData.error + ". Detalles del error: " + responseData.message);
            } else {
                alert("Error desconocido: " + error);
            }
        
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
        error: function (xhr, status, error) {
            responseData = xhr.responseJSON;
            if (responseData) {
                alert("Error: " + responseData.error + ". Detalles del error: " + responseData.message);
            } else {
                alert("Error desconocido: " + error);
            }
        
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
        error: function (xhr, status, error) {
            responseData = xhr.responseJSON;
            if (responseData) {
                alert("Error: " + responseData.error + ". Detalles del error: " + responseData.message);
            } else {
                alert("Error desconocido: " + error);
            }
        
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
            $('#nombreDireccionRuta').val(response[1]);
            $('#finalPaisDireccionRuta').val(response[2]);
            $('#finalCiudadDireccionRuta').val(response[3]);
        },
        error: function (xhr, status, error) {
            responseData = xhr.responseJSON;
            if (responseData) {
                alert("Error: " + responseData.error + ". Detalles del error: " + responseData.message);
            } else {
                alert("Error desconocido: " + error);
            }
        
        }
    });

});


$("#guardarRuta").click(function () {
    var nombreRuta = $('#nombreRuta').val();
    var idDireccionRuta = $('#rutaSelect').val();
    var chofer = $('#DropDownChofer').val();  //Chofer
    var camion = $('#DropDownCamion').val();    //Camion
    var idCliente = $('#form-identificacion').val();
    var descripcion = $('#descripcion').val();
    var inicio = $("#fechaInicioRuta").val();  // Fecha inicio
    var final = $("#fechaFinalRuta").val();  // Fecha final
    var estado = $('#estado').val();  // Estado
    
    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/Rutas?nombre="+nombreRuta+"&idDireccionRuta="+idDireccionRuta+
        "&idChofer="+chofer+"&placa="+camion+"&idCliente="+idCliente+"&descripcion="+descripcion+"&inicio="+inicio+
        "&final="+final+"&estadoEntrega="+estado+"",
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
        error: function (xhr, status, error) {
            responseData = xhr.responseJSON;
            if (responseData) {
                alert("Error: " + responseData.error + ". Detalles del error: " + responseData.message);
            } else {
                alert("Error desconocido: " + error);
            }
        
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