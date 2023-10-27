console.log('Hizo algo');
$("#IngresarSistema").click(function () {
    console.log('Hizo algo2');

    var identificacion = $('#form-identificacion').val();
    //var password = $('#form-password').val();

    jQuery.ajax({
        type: 'get',
        url: "https://localhost:7088/api/adminsControllers/" + identificacion,
        contentType: "application/json; charset=utf-8",
        cache: false,
        datatype: 'jsonp',
        traditional: true,
        success: function (response) {
            console.log("respuesta", response)

        },
        failure: function (response) {
            alert("Error: Chofer no encontrado")
        }
    });

});

/*
function login() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    // Estos son los valores predefinidos. En una aplicación real, no querrías almacenar contraseñas de esta manera.
    var correctUsername = "admin";
    var correctPassword = "12345";

    if (username === correctUsername && password === correctPassword) {
        window.location.href = 'mainPage.html';
    } else {
        var message = document.querySelector(".message");
        message.textContent = "Invalid username or password!";
        message.style.color = "red";
    }
}
*/
