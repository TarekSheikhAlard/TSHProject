$(document).ready(function () {
 
   
    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/BusExpenses/Create/',
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
        url: '/BusExpenses/Edit/' + id,
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
        url: '/BusExpenses/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};



function GetDetail(ID) {
    debugger;
    $('#myModal').modal('hide');
    $.ajax({
        type: "POST",
        url: '/BusExpenses/GetDetail',
        data: { id: ID },
        success: function (data) {
            $('#popupDetail').html(data);
            $('#myModal').modal('show');
        }
    });
}
