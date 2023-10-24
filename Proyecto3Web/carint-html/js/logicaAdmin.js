
$("#crearChofer").click(function () {

    $("#ConfirmarCrearChofer").css("visibility", "visible");
    $("#editarChofer").css("visibility", "hidden");
    $("#buscarChofer").css("visibility", "hidden");
    $("#crearChofer").css("visibility", "hidden");
    $("#form-identificacion").css('background-color', "#ffffff52");
    $("#form-nombre").css('background-color', "#ffffff52");
    $("#form-apellidos").css('background-color', "#ffffff52");
    $("#form-email").css('background-color', "#ffffff52");
    $("#form-contrasena").css('background-color', "#ffffff52");
    $("#form-identificacion").prop('disabled', false);
    $("#form-nombre").prop('disabled', false);
    $("#form-apellidos").prop('disabled', false);
    $("#form-email").prop('disabled', false);
    $("#form-contrasena").prop('disabled', false);
    $("#form-estado").prop('disabled', false);
});

$("#ConfirmarCrearChofer").click(function () {

    var identificacion = $('#form-identificacion').val();
    var nombre = $('#form-nombre ').val();
    var apellidos = $('#form-apellidos ').val();
    var email = $('#form-email ').val();
    var contrasena = $('#form-contrasena ').val();
    var estado = $("#form-estado").children(":selected")[0].label;
    if (contrasena == "") {
        alert("Ingrese una contrasena")
        return;
    }  
    if (identificacion == "") {
        alert("Ingrese una identificacion")
        return;
    }  
    if (apellidos == "") {
        alert("Ingrese apellidos")
        return;
    }
    if (nombre == "") {
            alert("Ingrese el nombre")
            return;
    } 

    if (email == "") {
        alert("Ingrese un email valido")
        return;
    } 
    
    if (!validateEmail(email)) {
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
    if (!validateLetras(apellidos)) {
        alert("El apellido debe contener solo letras")
        return;
    } 
    
    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/choferesControllers?identificacion=" + identificacion + " &nombre=" + nombre + "&apellidos=" + apellidos + "&email=" + email + "&contrasena=" + contrasena + "&estado=" + estado + "",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos + ", email " + email + " en estado " + estado + " fue agregado.");
            $('#modalup').trigger('click');
            setTimeout(
                function () {
                    location.reload();
                }, 3000);
        },
        failure: function (response) {
            alert("Error: Chofer No Agregado")
        }
    });

});

$("#buscarChofer").click(function () {

    $("#ConfirmarCrearChofer").css("visibility", "hidden");
    $("#ConfirmarBuscarChofer").css("visibility", "visible");
    $("#editarChofer").css("visibility", "hidden");
    $("#crearChofer").css("visibility", "hidden");
    $("#form-identificacion").prop('disabled', false);
    $("#form-identificacion").css('background-color', "#ffffff52");
    $("#form-nombre").css('background-color', "#f0f0f0");
    $("#form-apellidos").css('background-color', "#f0f0f0");
    $("#form-email").css('background-color', "#f0f0f0");
    $("#form-contrasena").css('background-color', "#f0f0f0");
    $("#buscarChofer").css("visibility", "hidden");

});
$("#ConfirmarBuscarChofer").click(function () {
    $("#ConfirmarBuscarChofer").css("visibility", "hidden");
    $("#ConfirmarCrearChofer").css("visibility", "hidden");
    $("#buscarChofer").css("visibility", "hidden");
    $("#ConfirmarEditarChofer").css("visibility", "visible");
    $("#crearChofer").css("visibility", "hidden");
    $("#editarChofer").css("visibility", "visible");
    $("#form-identificacion").css('background-color', "#f0f0f0");
    $("#form-nombre").css('background-color', "#ffffff52");
    $("#form-apellidos").css('background-color', "#ffffff52");
    $("#form-email").css('background-color', "#ffffff52");
    $("#form-contrasena").css('background-color', "#ffffff52");
    $("#form-identificacion").prop('disabled', true);
    $("#form-nombre").prop('disabled', false);
    $("#form-apellidos").prop('disabled', false);
    $("#form-email").prop('disabled', false);
    $("#form-contrasena").prop('disabled', false);
    $("#form-estado").prop('disabled', false);
});

$("#ConfirmarBuscarChofer").click(function () {

    var identificacion = $('#form-identificacion').val();
  

    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/choferesControllers/" + identificacion,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $("#form-identificacion").prop('disabled', true);
            $('#form-nombre').val(response[0]);
            $('#form-apellidos').val(response[1]);
            $('#form-email').val(response[2]);
            $('#form-estado').val(0).change();
            //$('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos + ", email " + email + " en estado " + estado + " fue agregado.");
           // $('#modalup').trigger('click');
        },
        failure: function (response) {
            alert("Error: Chofer no encontrado")
        }
    });

});

$("#editarChofer").click(function(){

      var identificacion = $('#form-identificacion').val();
      var nombre = $('#form-nombre ').val();
      var apellidos = $('#form-apellidos ').val();
      var email = $('#form-email ').val();
      var contrasena = $('#form-contrasena ').val();
      var estado = $("#form-estado").children(":selected")[0].label;

    if (!validateEmail(email)) {
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
    if (!validateLetras(apellidos)) {
        alert("Por favor ingrese un apellido valido")
        return;
    } 
    if (apellidos == "") {
        alert("Ingrese apellidos")
        return;
    }
    if (nombre == "") {
        alert("Ingrese el nombre")
        return;
    }

    if (email == "") {
        alert("Ingrese un email valido")
        return;
    } 
    if (contrasena == "") {
        contrasena = "NoCambiarContrasena";
    } 
       jQuery.ajax({
                  type: 'put',
           url: "https://localhost:7088/api/choferesControllers/" + identificacion + "?identificacion=" + identificacion + "&nombre=" + nombre + "&apellidos=" + apellidos +"&email=" +email +"&contrasena=" +contrasena+ "&estado=" + estado + "",
                  contentType: "application/json; charset=utf-8",
                  cache: false, 
                  datatype: 'jsonp',
                  traditional: true,
                  success: function (response) {
                        $('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos +", email " +email  + " en estado " + estado + " fue actualizado.");
                         $('#modalup').trigger('click');

                      setTimeout(
                          function () {
                              location.reload();
                          }, 3000);
                  },
                  failure: function (response) {
                        alert("Error: Chofer No Actualizado")
                  }
              });
      
      });

function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}

function validateId(Identificacion) {

    var pattern = new RegExp("\\d{9}");
    return pattern.test(Identificacion);

}
function validateLetras(Letras) {

    var pattern = new RegExp("[a-zA-Z\s]+");
    return pattern.test(Letras);

}
/*
function validatePassword(contrasena) {

    var pattern = new RegExp("[a-zA-Z\s]+");
    return pattern.test(contrasena);

}
*/

