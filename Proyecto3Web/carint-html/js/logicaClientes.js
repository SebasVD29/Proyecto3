//
$("#crearCliente").click(function () {

    $("#ConfirmarCrearCliente").css("visibility", "visible");
    $("#editarCliente").css("visibility", "hidden");
    $("#buscarCliente").css("visibility", "hidden");
    $("#crearCliente").css("visibility", "hidden");
    $("#form-identificacion").css('background-color', "#ffffff52");
    $("#form-nombreCompleto").css('background-color', "#ffffff52");
    $("#form-direccion").css('background-color', "#ffffff52");
    $("#form-telefono").css('background-color', "#ffffff52");
    $("#form-email").css('background-color', "#ffffff52");
    $("#form-identificacion").prop('disabled', false);
    $("#form-nombreCompleto").prop('disabled', false);
    $("#form-direccion").prop('disabled', false);
    $("#form-telefono").prop('disabled', false);
    $("#form-email").prop('disabled', false);
    $("#form-estado").prop('disabled', false);
});



$("#ConfirmarCrearCliente").click(function () {

    var identificacion = $('#form-identificacion').val();
    var nombre = $('#form-nombreCompleto ').val();
    var direccion = $('#form-direccion ').val();
    var telefono = $('#form-telefono ').val();
    var correo = $('#form-email ').val();
    var estado = $("#form-estado").children(":selected")[0].label;

    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/clientesControllers?identificacion=" + identificacion + " &nombre=" + nombre + "&direccion=" + direccion + "&telefono=" + telefono + "&email=" + correo + + "&estado=" + estado + "",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("El cliente con identificacion " + identificacion + ", nombre " + nombre + ", direccion  " + direccion + ", telefono " + telefono  + " email " + email, + " en estado " + estado + " fue agregado.");
            $('#modalup').trigger('click');
        },
        failure: function (response) {
            alert("Error: Chofer No Agregado")
        }
    });

});

<<<<<<< Updated upstream
/*
$("#editarChofer").click(function(){
=======

$("#buscarCliente").click(function () {

    $("#ConfirmarCrearCliente").css("visibility", "hidden");
    $("#ConfirmarBuscarCliente").css("visibility", "visible");
    $("#editarCliente").css("visibility", "hidden");
    $("#crearCliente").css("visibility", "hidden");
    $("#form-identificacion").prop('disabled', false);
    $("#form-identificacion").css('background-color', "#ffffff52");
    $("#form-nombreCompleto").css('background-color', "#f0f0f0");
    $("#form-direccion").css('background-color', "#f0f0f0");
    $("#form-telefono").css('background-color', "#f0f0f0");
    $("#form-email").css('background-color', "#f0f0f0");
    $("#buscarCliente").css("visibility", "hidden");
>>>>>>> Stashed changes

});


$("#ConfirmarBuscarCliente").click(function () {
    $("#ConfirmarBuscarCliente").css("visibility", "hidden");
    $("#ConfirmarCrearCliente").css("visibility", "hidden");
    $("#buscarCliente").css("visibility", "hidden");
    $("#ConfirmarEditarCliente").css("visibility", "visible");
    $("#crearCliente").css("visibility", "hidden");
    $("#editarCliente").css("visibility", "visible");
    $("#form-identificacion").css('background-color', "#f0f0f0");
    $("#form-nombreCompleto").css('background-color', "#ffffff52");
    $("#form-direccion").css('background-color', "#ffffff52");
    $("#form-telefono").css('background-color', "#ffffff52");
    $("#form-email").css('background-color', "#ffffff52");
    $("#form-identificacion").prop('disabled', true);
    $("#form-nombreCompleto").prop('disabled', false);
    $("#form-direccion").prop('disabled', false);
    $("#form-telefono").prop('disabled', false);
    $("#form-email").prop('disabled', false);
    $("#form-estado").prop('disabled', false);
});

$("#ConfirmarBuscarCliente").click(function () {

    var identificacion = $('#form-identificacion').val();


    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/clientesControllers/" + identificacion,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $("#form-identificacion").prop('disabled', true);
            $('#form-nombreCompleto').val(response[0]);
            $('#form-direccion').val(response[1]);
            $('#form-telefono').val(response[2]);
            $('#form-telefono').val(response[3]);
            $('#form-estado').val(0).change();
            //$('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos + ", email " + email + " en estado " + estado + " fue agregado.");
            // $('#modalup').trigger('click');
        },
        failure: function (response) {
            alert("Error: Chofer no encontrado")
        }
    });

});


$("#editarCliente").click(function () {

    var identificacion = $('#form-identificacion').val();
    var nombre = $('#form-nombreCompleto ').val();
    var direccion = $('#form-direccion ').val();
    var telefono = $('#form-telefono ').val();
    var correo = $('#form-email ').val();
    var estado = $("#form-estado").val();

    jQuery.ajax({
        type: 'put',
        url: "https://localhost:7088/api/clientesControllers/" + identificacion + " &nombreCompleto=" + nombre + "&direccion=" + direccion + "&telefono=" + telefono + "&email=" + correo + "&estado=" + estado + "",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("El cliente con identificacion " + identificacion + ", nombre " + nombre + ", direccion  " + direccion + ", telefono " + telefono + " email " + correo + ", en estado " + estado + " fue editado exitosamente.");
            $('#modalup').trigger('click');
        },
        failure: function (response) {
            alert("Error: Cliente No Actualizado")
        }
    });

});
/**/