﻿$(document).ready(function () {

    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/MaterialManufacture/Create/',
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
        url: '/MaterialManufacture/Edit/' + id,
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
        url: '/MaterialManufacture/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};