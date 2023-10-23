
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
    $("#form-estado").prop('disabled', false);
});

$("#ConfirmarCrearCamion").click(function () {
    var numeroPlaca = $('#form-numeroPlaca').val();
    var Marca = $('#form-Marca').val();
    var Modelo = $('#form-Modelo').val();
    var Fabricacion = $('#form-Fabricacion').val();
    var estado = $("#form-estado").children(":selected").text(); // Cambiado a obtener el texto del option

    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/camionesControllers",
        data: JSON.stringify({
            numeroPlaca: numeroPlaca,
            Marca: Marca,
            Modelo: Modelo,
            Fabricacion: Fabricacion,
            Estado: estado
        }),
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: 'json', // Cambiado de 'jsonp' a 'json'
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("El Camion con el numero de placa " + numeroPlaca + ", Marca " + Marca + ", Modelo  " + Modelo + ", fabricado en " + Fabricacion + " en estado " + estado + " fue agregado.");
            $('#modalup').trigger('click');
        },
        error: function (xhr, status, error) {
            alert("Error: Camion No Agregado. " + error);
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
        url: "https://localhost:7088/api/camionesControllers/" + numeroPlaca,
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: 'json', // Cambiado de 'jsonp' a 'json'
        traditional: true,
        success: function (response) {
            $("#form-numeroPlaca").prop('disabled', true);
            $('#form-Marca').val(response[0]);
            $('#form-Modelo').val(response[1]);
            $('#form-Fabricacion').val(response[2]);
            $('#form-estado').val(response[3]).change();
            $('#modalMensaje').text("El Camion con numeroPlaca " + numeroPlaca + ", Marca " + response[0] + ", Modelo  " + response[1] + ", Fabricacion " + response[2] + " en estado " + response[3] + " fue agregado.");
            $('#modalup').trigger('click');
        },
        error: function (xhr, status, error) {
            alert("Error: Camion no encontrado. " + error);
        }
    });
});


$("#ConfirmarBuscarCamion").click(function () {

    var numeroPlaca = $('#form-numeroPlaca').val();
  

    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/camionesControllers/" + numeroPlaca,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $("#form-numeroPlaca").prop('disabled', true);
            $('#form-Marca').val(response[0]);
            $('#form-Modelo').val(response[1]);
            $('#form-Fabricacion').val(response[2]);
            $('#form-estado').val(0).change();
            $('#modalMensaje').text("El Camion con numeroPlaca " + numeroPlaca + ", Marca " + Marca + ", Modelo  " + Modelo + ", Fabricacion " + Fabricacion + " en estado " + estado + " fue agregado.");
            $('#modalup').trigger('click');
        },
        failure: function (response) {
            alert("Error: Camion no encontrado")
        }
    });

});

$("#editarCamion").click(function(){

      var numeroPlaca = $('#form-numeroPlaca').val();
      var Marca = $('#form-Marca ').val();
      var Modelo = $('#form-Modelo ').val();
      var Fabricacion = $('#form-Fabricacion ').val();
      var estado = $("#form-estado").children(":selected")[0].label;
      
       jQuery.ajax({
                  type: 'put',
           url: "https://localhost:7088/api/camionesControllers/" + numeroPlaca + "?numeroPlaca=" + numeroPlaca + "&Marca=" + Marca + "&Modelo=" + Modelo +"&Fabricacion=" +Fabricacion + "&estado=" + estado + "",
                  contentType: "application/json; charset=utf-8",
                  cache: false, 
                  datatype: 'jsonp',
                  traditional: true,
                  success: function (response) {
                        $('#modalMensaje').text("El Camion con el número de placa " + numeroPlaca + ", Marca " + Marca + ", Modelo  " + Modelo +", Fabricacion " +Fabricacion  + " en estado " + estado + " fue actualizado.");
                        $('#modalup').trigger('click');
                  },
                  failure: function (response) {
                        alert("Error: Camion No Actualizado")
                  }
              });
      
      });

