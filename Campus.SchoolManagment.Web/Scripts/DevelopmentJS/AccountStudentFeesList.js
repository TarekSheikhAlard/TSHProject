$(document).ready(function () {

        $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/AccountStudentFeesList/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });


});

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/AccountStudentFeesList/Edit/' + id,
        success: function (response) {
            $('#divForAdd').empty();
            $('#divForUpdate').html(response);
            $('#Edit-Model').modal('show');

        }
    });



};

function Delete(id) {

    $.ajax({
        type: "Get",
        url: '/AccountStudentFeesList/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};