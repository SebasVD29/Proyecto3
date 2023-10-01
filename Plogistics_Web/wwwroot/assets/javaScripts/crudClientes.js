var selectedRow = null;

function onFormSubmit(e) {
    event.preventDefault();
    var formData = readFormData();
    if (selectedRow == null) {
        insertNewRecord(formData);
    } else {
        updateRecord(formData);
    }
    resetForm();
}

function readFormData() {
    var formData = {};
    formData["fullName"] = document.getElementById("fullName").value;
    formData["docType"] = document.getElementById("docType").value;
    formData["docNumber"] = document.getElementById("docNumber").value;
    formData["address"] = document.getElementById("address").value;
    formData["email"] = document.getElementById("email").value;
    return formData;
}

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

function onEdit(td) {
    selectedRow = td.parentElement.parentElement;
    document.getElementById("fullName").value = selectedRow.cells[0].innerHTML;
    document.getElementById("docType").value = selectedRow.cells[1].innerHTML;
    document.getElementById("docNumber").value = selectedRow.cells[2].innerHTML;
    document.getElementById("address").value = selectedRow.cells[3].innerHTML;
    document.getElementById("email").value = selectedRow.cells[4].innerHTML;
}

function updateRecord(formData) {
    selectedRow.cells[0].innerHTML = formData.fullName;
    selectedRow.cells[1].innerHTML = formData.docType;
    selectedRow.cells[2].innerHTML = formData.docNumber;
    selectedRow.cells[3].innerHTML = formData.address;
    selectedRow.cells[4].innerHTML = formData.email;
}

function onDelete(td) {
    if (confirm('Do you want to delete this record?')) {
        row = td.parentElement.parentElement;
        document.getElementById('clientList').deleteRow(row.rowIndex);
        resetForm();
    }
}

function resetForm() {
    document.getElementById("fullName").value = '';
    document.getElementById("docType").value = '';
    document.getElementById("docNumber").value = '';
    document.getElementById("address").value = '';
    document.getElementById("email").value = '';
    selectedRow = null;
}
