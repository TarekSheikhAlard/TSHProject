$(document).ready(function () {

    $('#AddBus').click(function () {
        $.ajax({
            type: "Get",
            url: '/Transport/CreateBus/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });

    $("#DownloadExcelBus").click(function () {
        var $parent = $('.download.button');


        $parent
            .addClass("loading");
        $.fileDownload("/Transport/ExportExcelBus/", {
            successCallback: function (url) {
                // alert();
                $parent
                    .removeClass("loading");
            },
            failCallback: function (html, url) {
                toastr.error("Error Occured while Download");
                $parent
                    .removeClass("loading");
            }
        });

    });

    $("#DownloadPDFBus").click(function () {

        var $this = $(this);

        var $parent = $('.download.button');
        $parent
            .addClass("loading");
        $.fileDownload("/Transport/ExportPdfBus/", {
            successCallback: function (url) {
                // alert();
                $parent
                    .removeClass("loading");
            },
            failCallback: function (html, url) {
                toastr.error("Error Occured while Download");
                $parent
                    .removeClass("loading");
            }
        });

    });
});

function EditBus(id) {

    $.ajax({
        type: "Get",
        url: '/Transport/EditBus/' + id,
        success: function (response) {
            $('#divForAdd').empty();
            $('#divForUpdate').html(response);
            $('#Edit-Model').modal('show');

        }
    });

};

function DeleteBus(id) {

    $.ajax({
        type: "Get",
        url: '/Transport/DeleteBus/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });

};

