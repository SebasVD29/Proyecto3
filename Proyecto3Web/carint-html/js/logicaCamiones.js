$("#crearCamion").click(function () {

    $("#ConfirmarCrearCamion").css("visibility", "visible");
    $("#editarCamion").css("visibility", "hidden");
    $("#buscarCamion").css("visibility", "hidden");
    $("#crearCamion").css("visibility", "hidden");
    $("#form-numeroPlaca").css('background-color', "#ffffff52");
    $("#form-Marca").css('background-color', "#ffffff52");
    $("#form-Modelo").css('background-color', "#ffffff52");
    $("#form-Fabricacion").css('background-color', "#ffffff52");
    $("#form-numeroPlaca").prop('disabled', false);
    $("#form-Marca").prop('disabled', false);
    $("#form-Modelo").prop('disabled', false);
    $("#form-Fabricacion").prop('disabled', false);
    $("#form-Estado").prop('disabled', false);

});


$("#ConfirmarCrearCamion").click(function () {
    var numeroPlaca = $('#form-numeroPlaca').val();
    var Marca = $('#form-Marca').val();
    var Modelo = $('#form-Modelo').val();
    var Fabricacion = parseInt($('#form-Fabricacion').val());
    var Estado = $("#form-Estado").children(":selected").val(); // Cambiado a obtener el texto del option
    if (numeroPlaca == "") {
        alert("Ingrese un número de placa")
        return;
    }
    if (Marca == "") {
        alert("Ingrese la Marca")
        return;
    }
    if (Modelo == "") {
        alert("Ingrese el Modelo")
        return;
    }
    if (Fabricacion == "") {
        alert("Ingrese el año de fabricación")
        return;
    }


    if (!validatePlaca(numeroPlaca)) {
        alert("El número de placa debe tener al menos 6 digitos")
        return;
    }
    if (!validateLetras(Marca)) {
        alert("La marca debe contener solo letras")
        return;
    }
    if (!validateLetras(Modelo)) {
        alert("La marca debe contener solo letras")
        return;
    }

    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/Camiones",
        data: JSON.stringify({
            numeroPlaca: numeroPlaca,
            Marca: Marca,
            Modelo: Modelo,
            Fabricacion: Fabricacion,
            Estado: Estado
        }),
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: 'json', // Cambiado de 'jsonp' a 'json'
        success: function (response) {
            console.log(response); // Imprime la respuesta del servidor en la consola del navegador
            $('#modalMensaje').text("El Camion con el numero de placa " + numeroPlaca + ", Marca " + Marca + ", Modelo  " + Modelo + ", fabricado en " + Fabricacion + " en Estado " + Estado + " fue agregado.");
            $('#modalup').trigger('click');
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


$("#buscarCamion").click(function () {

    $("#ConfirmarCrearCamion").css("visibility", "hidden");
    $("#ConfirmarBuscarCamion").css("visibility", "visible");
    $("#editarCamion").css("visibility", "hidden");
    $("#crearCamion").css("visibility", "hidden");
    $("#form-numeroPlaca").prop('disabled', false);
    $("#form-numeroPlaca").css('background-color', "#ffffff52");
    $("#form-Marca").css('background-color', "#f0f0f0");
    $("#form-Modelo").css('background-color', "#f0f0f0");
    $("#form-Fabricacion").css('background-color', "#f0f0f0");
    $("#buscarCamion").css("visibility", "hidden");

});



$("#ConfirmarBuscarCamion").click(function () {

    var numeroPlaca = $('#form-numeroPlaca').val();

    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/Camiones/" + numeroPlaca,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'json',
        traditional: true,
        success: function (response) {
            console.log(response);
            if (Array.isArray(response) && response.length >= 1) {
                var camion = response[0]; // Accedes al primer objeto dentro del array
                $('#form-numeroPlaca').val(camion.numeroPlaca);
                $('#form-Marca').val(camion.marca);
                $('#form-Modelo').val(camion.modelo);
                $('#form-Fabricacion').val(camion.fabricacion);
                $('#form-Estado').val(camion.Estado).change(); // Cambiado a .change() para seleccionar el Estado
                $("#editarCamion").css("visibility", "visible");
                $("#ConfirmarBuscarCamion").css("visibility", "hidden");
                $("#form-numeroPlaca").css('background-color', "#ffffff52");
                $("#form-Marca").css('background-color', "#ffffff52");
                $("#form-Modelo").css('background-color', "#ffffff52");
                $("#form-Fabricacion").css('background-color', "#ffffff52");
                $("#form-numeroPlaca").prop('disabled', false);
                $("#form-Marca").prop('disabled', false);
                $("#form-Modelo").prop('disabled', false);
                $("#form-Fabricacion").prop('disabled', false);
                $("#form-Estado").prop('disabled', false);
            } else {
                alert("Error: Datos de camión no encontrados en la respuesta");
            }
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


$(document).ready(function() {
    var currentYear = new Date().getFullYear();
    for (var i = currentYear; i >= currentYear - 20; i--) {
        $('#form-Fabricacion').append($('<option>', {
            value: i,
            text: i
        }));
    }
});

$("#editarCamion").click(function(){

      var numeroPlaca = $('#form-numeroPlaca').val();
      var Marca = $('#form-Marca ').val();
      var Modelo = $('#form-Modelo ').val();
      var Fabricacion = $('#form-Fabricacion ').val();
      var Estado = $("#form-Estado").val();
      
      jQuery.ajax({
        type: 'put',
        url: "https://localhost:7088/api/Camiones/" + numeroPlaca,
        data: JSON.stringify({
            numeroPlaca: numeroPlaca,
            Marca: Marca,
            Modelo: Modelo,
            Fabricacion: Fabricacion,
            Estado: Estado
        }),
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: 'json', // Cambiado de 'jsonp' a 'json'
        success: function (response) {
         
            $('#modalMensaje').text("El Camion con el numero de placa " + numeroPlaca + ", Marca " + Marca + ", Modelo  " + Modelo + ", fabricado en " + Fabricacion + " en Estado " + Estado + " fue agregado.");
            $('#modalup').trigger('click');
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

function validatePlaca(numeroPlaca) {

    var pattern = new RegExp("\\d{6}");
    return pattern.test(numeroPlaca);

}
function validateLetras(Letras) {

    var pattern = new RegExp("[a-zA-Z\s]+");
    return pattern.test(Letras);

}
