//$(document).ready(function () {
 
   
//    $('#Add').click(Add(2));
//    $('#AddLevel4').click(Add(3));

    
//    


//});
function Add() {

        $.ajax({
            type: "Get",
            url: '/PhysicalYear/Create/',
         
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');
               
            }
        });}
function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/PhysicalYear/Edit/' + id,
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
        url: '/PhysicalYear/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};



