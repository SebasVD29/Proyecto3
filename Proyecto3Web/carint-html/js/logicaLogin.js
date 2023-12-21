var code;
function createCaptcha() {
    //clear the contents of captcha div first 
    document.getElementById('captcha').innerHTML = "";
    var charsArray =
        "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@!#$%^&*";
    var lengthOtp = 6;
    var captcha = [];
    for (var i = 0; i < lengthOtp; i++) {
        //below code will not allow Repetition of Characters
        var index = Math.floor(Math.random() * charsArray.length + 1); //get the next character from the array
        if (captcha.indexOf(charsArray[index]) == -1)
            captcha.push(charsArray[index]);
        else i--;
    }
    var canv = document.createElement("canvas");
    canv.id = "captcha";
    canv.width = 100;
    canv.height = 50;
    var ctx = canv.getContext("2d");
    ctx.font = "25px Georgia";
    ctx.strokeText(captcha.join(""), 0, 30);
    //storing captcha so that can validate you can save it somewhere else according to your specific requirements
    code = captcha.join("");
    document.getElementById("captcha").appendChild(canv); // adds the canvas to the body element
}
function validateCaptcha() {
    event.preventDefault();
   
    if (document.getElementById("cpatchaTextBox").value == code) {
        alert("Captcha Valido")
        $("#IngresarSistema").css("visibility", "visible");
    } else {
        alert("Captcha incorrecto. Intente de nuevo");
        createCaptcha();
    }
}
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