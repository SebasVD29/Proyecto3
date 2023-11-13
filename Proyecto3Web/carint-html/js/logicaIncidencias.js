function insertNewRecord(data) {
    var table = document.getElementById("clientList").getElementsByTagName('tbody')[0];
    var newRow = table.insertRow(table.length);
    newRow.insertCell(0).innerHTML = data.fullName;
    newRow.insertCell(1).innerHTML = data.docType;
    newRow.insertCell(2).innerHTML = data.docNumber;
    newRow.insertCell(3).innerHTML = data.address;
    newRow.insertCell(4).innerHTML = data.email;
    newRow.insertCell(5).innerHTML = `<button onClick="onEdit(this)">Edit</button> <button onClick="onDelete(this)">Delete</button>`;
}

$("#ConfirmarBuscarIncidencia").click(function () {
    var ruta = $('#form-Ruta').val();
    var fechaInicio = $('#form-fechaInicio').val();
    var fechaFinal = $('#form-fechaFinal').val();
    console.log(fechaInicio, fechaFinal);
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
            $.each(response, function (index, incidente) {
                var newRow = $("<tr>");
                newRow.append("<td>" + incidente[0] + "</td>");
                newRow.append("<td>" + incidente[1] + "</td>");
                newRow.append("<td>" + incidente[2] + "</td>");
                newRow.append("<td>" + incidente[3] + "</td>");
                newRow.append("<td>" + incidente[4] + "</td>");
                table.append(newRow);
            });
        },
        error: function () {
            alert("Error: Incidente no encontrado");
        }
    });
});
