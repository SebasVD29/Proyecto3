
$("#buscarRuta").click(function () {

    var identificacion = $('#form-identificacion').val();
    var array; 

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


    //Cargar Choferes y Camiones

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
            $('#direccionRuta').val(response[3]);
            $('#fechaInicioRuta').val(response[9]);
            $('#fechaFinalRuta').val(response[10]);
        },
        failure: function (response) {
            alert("Error")
        }
    });

});