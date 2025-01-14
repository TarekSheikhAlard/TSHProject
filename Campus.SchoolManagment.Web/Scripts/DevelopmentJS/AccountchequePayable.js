﻿$(document).ready(function () {
    
   
    $('#Add').click(function () {
       
        $.ajax({
            type: "Get",
            url: '/AccountchequePayable/Create/',
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
        url: '/AccountchequePayable/Edit/' + id,
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
        url: '/AccountchequePayable/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};


function SearchAccount1() {

    $.ajax({

        type: "Get",
        url: '/AccountchequePayable/GetAccounts1',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });



};


function SearchAccount2() {

    $.ajax({

        type: "Get",
        url: '/AccountchequePayable/GetAccounts2',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });

};