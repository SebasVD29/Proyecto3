$("#crearChofer").click(function(){

var identificacion = $('#form-identificacion').val();
var nombre = $('#form-nombre ').val();
var apellidos = $('#form-apellidos ').val();
var email = $('#form-email ').val();
var contrasena = $('#form-contrasena ').val();
var fecha = $('#form-fecha ').val();
var estado = $("#form-estado").children(":selected")[0].label;

 jQuery.ajax({
            type: 'post',
            url: "https://localhost:7088/api/choferesControllers?identificacion=" + identificacion +" &nombre=" + nombre + "&apellidos=" + apellidos +"&email=" +email +"&contrasena=" +contrasena+ "&fecha=" + fecha + "&estado=" + estado + "",
            contentType: "application/json; charset=utf-8",
            cache: false, 
            datatype: 'jsonp',
            traditional: true,
            success: function (response) {
                  $('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos +", email " +email + " fecha " + fecha + " en estado " + estado + " fue agregado.");
                  $('#modalup').trigger('click');
            },
            failure: function (response) {
                  alert("Error: Chofer No Agregado")
            }
        });

});
