$(document).ready(function () {
    $('#Add').click(function () {
        $.ajax({
            type: "Get",
            url: '/AccountSupplier/Create/',
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
        url: '/AccountSupplier/Edit/' + id,
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
        url: '/AccountSupplier/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });
};


function SearchAccount() {
   
    $.ajax({
        type: "Get",
        url: '/AccountSupplier/GetAccounts',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');
        }
    });
};


function SendPO(elem) {
    $elem = $(elem);
    id = $elem.attr("data-id");
    $elem.addClass("loading");
    $.ajax({
        type: "POST",
        data: {id},
        url: '/Document/SendDocument',
        success: function (response) {
        
        },
        complete: function () {
            $elem.removeClass("loading");
        }
    });



}