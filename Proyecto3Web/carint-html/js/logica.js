$("#crearChofer").click(function () {

    $("#ConfirmarCrearChofer").css("visibility", "visible");
    $("#editarChofer").css("visibility", "hidden");
    $("#crearChofer").css("visibility", "hidden");
    $("#form-identificacion").prop('disabled', false);
    $("#form-nombre").prop('disabled', false);
    $("#form-apellidos").prop('disabled', false);
    $("#form-email").prop('disabled', false);
    $("#form-contrasena").prop('disabled', false);
});


$("#ConfirmarCrearChofer").click(function () {

var identificacion = $('#form-identificacion').val();
var nombre = $('#form-nombre ').val();
var apellidos = $('#form-apellidos ').val();
var email = $('#form-email ').val();
var contrasena = $('#form-contrasena ').val();
var estado = $("#form-estado").children(":selected")[0].label;

 jQuery.ajax({
            type: 'post',
            url: "https://localhost:7088/api/choferesControllers?identificacion=" + identificacion +" &nombre=" + nombre + "&apellidos=" + apellidos +"&email=" +email +"&contrasena=" +contrasena+ "&estado=" + estado + "",
            contentType: "application/json; charset=utf-8",
            cache: false, 
            datatype: 'jsonp',
            traditional: true,
            success: function (response) {
                  $('#modalMensaje').text("El chofer con identificacion " + identificacion + ", nombre " + nombre + ", apellidos  " + apellidos +", email " +email + " en estado " + estado + " fue agregado.");
                  $('#modalup').trigger('click');
            },
            failure: function (response) {
                  alert("Error: Chofer No Agregado")
            }
        });

});



$("#ConfirmarEditarChofer").click(function(){

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

