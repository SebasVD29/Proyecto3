/*$("#crearChofer").click(function(){

var identificacion = $('#form-identificacion').val();
var nombre = $('#form-nombre ').val();
var apellidos = $('#form-apellidos ').val();
var email = $('#form-email ').val();
var fecha = dateTime;
var contrasena = $('#form-contrasena ').val();
var estado = $("#form-estado").children(":selected")[0].label;

 jQuery.ajax({
            type: 'post',
            url: "https://localhost:7088/api/choferesControllers?identificacion=" + identificacion +" &nombre=" + nombre + "&apellidos=" + apellidos +"&email=" +email +"&contrasena=" +contrasena+ + "&estado=" + estado + "",
            contentType: "application/json; charset=utf-8",
            cache: false, 
            datatype: 'jsonp',
            traditional: true,
            success: function (response) {
                  $('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos +", email " +email + " fecha " + fecha, + " en estado " + estado + " fue agregado.");
                  $('#modalup').trigger('click');
            },
            failure: function (response) {
                  alert("Error: Chofer No Agregado")
            }
        });

});

*/
$("#crearCliente").click(function () {

    var identificacion = $('#form-identificacion').val();
    var nombre = $('#form-nombreCompleto ').val();
    var direccion = $('#form-direccion ').val();
    var telefono = $('#form-telefono ').val();
    var correo = $('#form-email ').val();
    var estado = $("#form-estado").val();

    jQuery.ajax({
        type: 'post',
        url: "https://localhost:7088/api/Clientes?identificadorCliente=" + identificacion + " &nombreCompleto=" + nombre + "&telefono=" + telefono + "&direccion=" + direccion + "&email=" + correo + + "&estado=" + estado + "",
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



/*
$("#editarChofer").click(function(){

      var identificacion = $('#form-identificacion').val();
      var nombre = $('#form-nombre ').val();
      var apellidos = $('#form-apellidos ').val();
      var email = $('#form-email ').val();
      var contrasena = $('#form-contrasena ').val();
      var estado = $("#form-estado").children(":selected")[0].label;
      
       jQuery.ajax({
                  type: 'put',
                  url: "https://localhost:7088/api/choferesControllers?identificacion=" + identificacion +" &nombre=" + nombre + "&apellidos=" + apellidos +"&email=" +email +"&contrasena=" +contrasena+ + "&estado=" + estado + "",
                  contentType: "application/json; charset=utf-8",
                  cache: false, 
                  datatype: 'jsonp',
                  traditional: true,
                  success: function (response) {
                        $('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos +", email " +email  + " en estado " + estado + " fue actualizado.");
                        $('#modalup').trigger('click');
                  },
                  failure: function (response) {
                        alert("Error: Chofer No Actualizado")
                  }
              });
      
      });

*/