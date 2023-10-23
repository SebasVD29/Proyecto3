
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

    $("#ConfirmarCrearCamion").click(function () {
        var numeroPlaca = $('#form-numeroPlaca').val();
        var Marca = $('#form-Marca').val();
        var Modelo = $('#form-Modelo').val();
        var Fabricacion = $('#form-Fabricacion').val();
        var estado = $("#form-estado option:selected").text();

        $.ajax({
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
            success: function (response) {
            alert("Error: Camion No Agregado")
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
    $("#ConfirmarBuscarCamion").css("visibility", "hidden");
    $("#ConfirmarCrearCamion").css("visibility", "hidden");
    $("#buscarCamion").css("visibility", "hidden");
    $("#ConfirmarEditarCamion").css("visibility", "visible");
    $("#crearCamion").css("visibility", "hidden");
    $("#editarCamion").css("visibility", "visible");
    $("#form-numeroPlaca").css('background-color', "#f0f0f0");
    $("#form-Marca").css('background-color', "#ffffff52");
    $("#form-Modelo").css('background-color', "#ffffff52");
    $("#form-Fabricacion").css('background-color', "#ffffff52");
    $("#form-numeroPlaca").prop('disabled', true);
    $("#form-Marca").prop('disabled', false);
    $("#form-Modelo").prop('disabled', false);
    $("#form-Fabricacion").prop('disabled', false);
    $("#form-estado").prop('disabled', false);
});

$("#ConfirmarBuscarCamion").click(function () {


    var numeroPlaca = $('#form-numeroPlaca').val();

    $.ajax({
        type: 'get',
        url: "https://localhost:7088/api/camionesControllers/" + numeroPlaca,
        contentType: "application/json; charset=utf-8",
        cache: false,
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

