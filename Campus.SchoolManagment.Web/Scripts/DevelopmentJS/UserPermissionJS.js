$(document).ready(function () {

    semantic.init();

    $('#Add').click(function () {
        $.ajax({
            type: "Get",
            url: '/UserPermission/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });




    $("#RoleID").change(function () {
        var id =this.value
        $.ajax({
            type: "Get",
            url: '/UserPermission/Create/'+id,
            success: function (response) {
                $('#div-record').empty();
                $('#div-record').html(response);
                $('#div-record').removeClass('placeholder');

            }
        });
       
    })


});

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/UserPermission/Edit/' + id,
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
        url: '/UserPermission/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};