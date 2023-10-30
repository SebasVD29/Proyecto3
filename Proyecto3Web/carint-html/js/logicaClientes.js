
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
    $("#form-estado").prop('disabled', true);
});



$("#ConfirmarCrearCliente").click(function () {

    var identificacion = $('#form-identificacion').val();
    var nombre = $('#form-nombreCompleto ').val();
    var direccion = $('#form-direccion ').val();
    var telefono = $('#form-telefono ').val();
    var correo = $('#form-email ').val();
    var estado = 1;


    if (identificacion == "") {
        alert("Ingrese una identificacion")
        return;
    }
    if (nombre == "") {
        alert("Ingrese nombre")
        return;
    }
    if (direccion == "") {
        alert("Ingrese direccion")
        return;
    }

    if (telefono == "") {
        alert("Ingrese un numero de telefono")
        return;
    }

    if (!validateEmail(correo)) {
        alert("Correo Invalido")
        return;
    }
    if (!validateId(identificacion)) {
        alert("El numero de identificacion debe tener al menos 9 digitos")
        return;
    }
    if (!validateLetras(nombre)) {
        alert("El nombre debe contener solo letras")
        return;
    }
    if (!validateLetras(direccion)) {
        alert("La direccion debe contener solo letras")
        return;
    } 
    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/clientesControllers?identificadorCliente=" + identificacion + " &nombreCompleto=" + nombre + "&direccion=" + direccion + "&telefono=" + telefono  + "&email=" + correo + "&estado=" + estado + "",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("El cliente con identificacion " + identificacion + ", nombre " + nombre + ", direccion  " + direccion + ", telefono " + telefono  + " email " + correo + ", en estado " + estado + " fue agregado exitosamente.");
            $('#modalup').trigger('click');
            setTimeout(
                function () {
                    location.reload();
                }, 3000);
        },
        failure: function (response) {
            alert("Error: Cliente  No Agregado")
        }
    });

});

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
            $('#form-email').val(response[3]);
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


    if (!validateEmail(correo)) {
        alert("Correo Invalido")
        return;
    }
    if (!validateId(identificacion)) {
        alert("El numero de identificacion debe tener al menos 9 digitos")
        return;
    }
    if (!validateLetras(nombre)) {
        alert("Por favor ingrese un nombre valido")
        return;
    }
    if (!validateLetras(direccion)) {
        alert("Por favor ingrese una direccion valida")
        return;
    }
  
    if (nombre == "") {
        alert("Ingrese el nombre")
        return;
    }
    if (telefono == "") {
        alert("Ingrese el nombre")
        return;
    }

    if (correo == "") {
        alert("Ingrese un email valido")
        return;
    }
    
    jQuery.ajax({
        type: 'put',
        url:" https://localhost:7088/api/clientesControllers/" + identificacion + "?identificadorCliente= "+ identificacion+"&nombreCompleto=" + nombre + "&direccion=" + direccion + "&telefono=" + telefono + "&email=" + correo + "&estado=" + estado + "" ,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("El cliente con identificacion " + identificacion + ", nombre " + nombre + ", direccion  " + direccion + ", telefono " + telefono + " email " + correo + ", en estado " + estado + " fue editado exitosamente.");
            $('#modalup').trigger('click');
            setTimeout(
                function () {
                    location.reload();
                }, 3000);
        },
        failure: function (response) {
            alert("Error: Cliente No Actualizado")
        }
    });

});
function validateEmail(correo) {
    var re = /\S+@\S+\.\S+/;
    return re.test(correo);
}

function validateId(Identificacion) {

    var pattern = new RegExp("\\d{9}");
    return pattern.test(Identificacion);

}
function validateLetras(Letras) {

    var pattern = new RegExp("[a-zA-Z\s]+");
    return pattern.test(Letras);

}