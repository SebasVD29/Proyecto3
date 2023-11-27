
$("#IngresarSistema").click(function () {
    var correo = $('#form-correo').val();
    var password = $('#form-password').val();

    if (!validateEmail(correo)) {
        alert("Correo Invalido")
        return;
    }

    if (correo == "") {
        alert("Ingrese un email valido")
        return;
    }
    if (password == "") {
        alert("Ingrese la contraseña")
        return;
    }

    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/AdminsControllers/login?correo=" + correo+"&password="+password,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            window.location.href = 'MenuAdmin.html';
            console.log("respuesta", response)

        },
        error: function (xhr, status, error){
            responseData = xhr.responseJSON;
            if (responseData) {
                alert("Error: Administrador no encontrado", + responseData.message);
            }
        }
    });

});
function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}
