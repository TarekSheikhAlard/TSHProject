$(document).ready(function () {
 
   
    $('#Add').click(function Add() {

        $.ajax({
            type: "Get",
            url: '/AccFeeItem/Create/',
          
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });
    
});
function Add() {

        $.ajax({
            type: "Get",
            url: '/AccFeeItem/Create/',
            data:{level : level},
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');
               
            }
        });}
function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/AccFeeItem/Edit/' + id,
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
        url: '/AccFeeItem/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};



