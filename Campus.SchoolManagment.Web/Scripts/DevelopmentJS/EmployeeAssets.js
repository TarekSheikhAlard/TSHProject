$(document).ready(function () {
    //$('#Add').click(function () {

    //    $.ajax({
    //        type: "Get",
    //        url: '/EmployeeAssets/Create/',
    //        success: function (response) {
    //            $('#divForUpdate').empty();
    //            $('#divForAdd').html(response);
    //            $('#Add-Model').modal('show');

    //        }
    //    });
    //});
});

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/EmployeeAssets/Edit/' + id,
        success: function (response) {
         
            $('#divForUpdate-EmployeeAssets').html(response);
            $('#Edit-Model-EmployeeAssets')
                .modal({ allowMultiple: true })
                .modal('show');

        }
    });



};

function Delete(id) {

    $.ajax({
        type: "Get",
        url: '/EmployeeAssets/Delete/' + id,
        success: function (response) {
            $('#divForDelete-EmployeeAssets').html(response);
            $('#Delete-Model-EmployeeAssets')
                .modal({ allowMultiple: true })
                .modal('show');
        }
    });



};

function SearchAsset() {

    $.ajax({
        type: "Get",
        url: '/EmployeeAssets/GetAsset',
        success: function (response) {
            $('#divForAsset').empty();
            $('#divForAsset').html(response);
            $('#Asset-Model')
                .modal({ allowMultiple: true})
                .modal('show');

        }
    });
};