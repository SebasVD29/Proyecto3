
        $("#ConfirmarBuscarIncidencia2").click(function () {
            // Asegúrate de que 'table' esté definida antes de usarla
            var table = $("#incidenciasList tbody");

            var item = [];
            item[0] = 1;
            item[1] = 1;
            item[2] = "ss";
            item[3] = "2022-12-14"
            item[4] = "nin";

            var newRow = $("<tr>");
            newRow.append("<td>" + item[0] + "</td>");
            newRow.append("<td>" + item[1] + "</td>");
            newRow.append("<td>" + item[2] + "</td>");
            newRow.append("<td>" + item[3] + "</td>");
            newRow.append("<td>" + item[4] + "</td>");
            table.append(newRow);
        });
   

$("#ConfirmarBuscarIncidencia").click(function () {
    var ruta = $('#form-Ruta').val();
    var fechaInicio = $('#form-fechaInicio').val();
    var fechaFinal = $('#form-fechaFinal').val();
    console.log(ruta,fechaInicio, fechaFinal);
    $.ajax({
        type: 'get',
        url: "https://localhost:7088/api/incidentesControllers/" + ruta + "?fechaInicio=" + fechaInicio + "&fechaFinal=" + fechaFinal +"",
        
        contentType: "application/json; charset=utf-8",
        cache: false,
        dataType: 'json', // Corregido: 'jsonp' por 'json'
        traditional: true,
        success: function (response) {
            var table = $('#incidenciasList tbody');
            table.empty(); // Limpiar el contenido actual de la tabla
           
            // Iterar sobre la lista de respuestas
            $.each(response, function (index, item) {
                console.log(item); // Puedes imprimir cada elemento del array si es necesario

                var newRow = $("<tr>");
                newRow.append("<td>" + item[0] + "</td>");
                newRow.append("<td>" + item[1] + "</td>");
                newRow.append("<td>" + item[2] + "</td>");
                newRow.append("<td>" + item[3] + "</td>");
                newRow.append("<td>" + item[4] + "</td>");
                table.append(newRow);
            });
           
        },


        error: function () {
            alert("Error: Incidente no encontrado");
        }
    });
});
