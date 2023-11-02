
// Clave secreta para el cifrado (debe mantenerse segura)
const claveSecreta = "claveSuperSecreta";

// Datos a cifrar
const datos = "Información confidencial";

// Cifrado
const datosCifrados = CryptoJS.AES.encrypt(datos, claveSecreta).toString();
console.log("Datos cifrados: " + datosCifrados);

// Descifrado
const datosDescifrados = CryptoJS.AES.decrypt(datosCifrados, claveSecreta).toString(CryptoJS.enc.Utf8);
console.log("Datos descifrados: " + datosDescifrados);

$("#crearAdmin").click(function () {

    $("#ConfirmarCrearAdmin").css("visibility", "visible");
    $("#editarAdmin").css("visibility", "hidden");
    $("#buscarAdmin").css("visibility", "hidden");
    $("#crearAdmin").css("visibility", "hidden");
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
    $("#form-estado").prop('disabled', true);
});

$("#ConfirmarCrearAdmin").click(function () {

    var identificacion = $('#form-identificacion').val();
    var nombre = $('#form-nombre').val();
    var apellidos = $('#form-apellidos ').val();
    var email = $('#form-email ').val();
    var contrasena = $('#form-contrasena ').val();
    var estado = 1;
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
    if (!validatePassword(contrasena)) {
        alert("La contrasena debe contener al menos: Una mayuscula," +
            "una minuscula, un numero, un caracter especial y " + 
            "ser mayor o igual a 8 digitos.")
        return;
    }

    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/adminsControllers?identificacion=" + identificacion + " &nombre=" + nombre + "&apellidos=" + apellidos + "&email=" + email + "&contrasena=" + contrasena + "&estado=" + estado + "",
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

$("#buscarAdmin").click(function () {

    $("#ConfirmarCrearAdmin").css("visibility", "hidden");
    $("#ConfirmarBuscarAdmin").css("visibility", "visible");
    $("#editarAdmin").css("visibility", "hidden");
    $("#crearAdmin").css("visibility", "hidden");
    $("#form-identificacion").prop('disabled', false);
    $("#form-identificacion").css('background-color', "#ffffff52");
    $("#form-nombre").css('background-color', "#f0f0f0");
    $("#form-apellidos").css('background-color', "#f0f0f0");
    $("#form-email").css('background-color', "#f0f0f0");
    $("#form-contrasena").css('background-color', "#f0f0f0");
    $("#buscarAdmin").css("visibility", "hidden");

});
$("#ConfirmarBuscarAdmin").click(function () {
    $("#ConfirmarBuscarAdmin").css("visibility", "hidden");
    $("#ConfirmarCrearAdmin").css("visibility", "hidden");
    $("#buscarAdmin").css("visibility", "hidden");
   // $("#ConfirmarEditarChofer").css("visibility", "visible");
    $("#crearAdmin").css("visibility", "hidden");
    $("#editarAdmin").css("visibility", "visible");
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

$("#ConfirmarBuscarAdmin").click(function () {

    var identificacion = $('#form-identificacion').val();


    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/adminsControllers/" + identificacion,
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

        },
        failure: function (response) {
            alert("Error: Chofer no encontrado")
        }
    });

});

$("#editarAdmin").click(function () {

    var identificacion = $('#form-identificacion').val();
    var nombre = $('#form-nombre ').val();
    var apellidos = $('#form-apellidos ').val();
    var email = $('#form-email ').val();
    var contrasena = $('#form-contrasena ').val();
    var estado = $("#form-estado").val();

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
    if((!contrasena == "") && (!validatePassword(contrasena))){
        alert("La contrasena debe contener al menos: Una mayuscula," +
            "una minuscula, un numero, un caracter especial y " + 
            "ser mayor o igual a 8 digitos.")
        return;
    }
    if (contrasena == "") {
        contrasena = "NoCambiarContrasena";
    }
    jQuery.ajax({
        type: 'put',
        url: "https://localhost:7088/api/adminsControllers/" + identificacion + "?identificacion=" + identificacion + "&nombre=" + nombre + "&apellidos=" + apellidos + "&email=" + email + "&contrasena=" + contrasena + "&estado=" + estado + "",
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            $('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos + ", email " + email + " en estado " + estado + " fue actualizado.");
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

function validatePassword(contrasena) {

    var re = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$/;
    return re.test(contrasena);

}


