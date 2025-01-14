$(document).ready(function () {
    $('#Add').click(function () {
        $.ajax({
            type: "Get",
            url: '/EmployeeLoans/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model-EmployeeLoans').modal('show');

            }
        });
    });
});

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/EmployeeLoans/Edit/' + id,
        success: function (response) {
           // $('#divForAdd').empty();
            $('#divForUpdate-EmployeeLoans').html(response);
            $('#Edit-Model-EmployeeLoans')
                .modal({ allowMultiple: true })
                .modal('show');

        }
    });



};

function Delete(id) {

    $.ajax({
        type: "Get",
        url: '/EmployeeLoans/Delete/' + id,
        success: function (response) {
            $('#divForDelete-EmployeeLoans').html(response);
            $('#Delete-Model-EmployeeLoans')
                .modal({ allowMultiple: true })
                .modal('show');
        }
    });



};
