$(document).ready(function () {


    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/AccountReciveCash/Create/',
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
        url: '/AccountReciveCash/Edit/' + id,
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
        url: '/AccountReciveCash/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};


function SearchAccount() {
    debugger;
    $.ajax({

        type: "Get",
        url: '/AccountReciveCash/GetAccounts',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });



};