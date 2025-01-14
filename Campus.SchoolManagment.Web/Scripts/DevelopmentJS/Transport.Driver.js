$(document).ready(function () {

    $('#AddDriver').click(function () {
        $.ajax({
            type: "Get",
            url: '/Transport/CreateDriver/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });

    $("#DownloadExcelDriver").click(function () {
        var $parent = $('.download.button');


        $parent
            .addClass("loading");
        $.fileDownload("/Transport/ExportExcelDriver/", {
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

    $("#DownloadPDFDriver").click(function () {

        var $this = $(this);

        var $parent = $('.download.button');
        $parent
            .addClass("loading");
        $.fileDownload("/Transport/ExportPdfDriver/", {
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

function EditDriver(id) {

    $.ajax({
        type: "Get",
        url: '/Transport/EditDriver/' + id,
        success: function (response) {
            $('#divForAdd').empty();
            $('#divForUpdate').html(response);
            $('#Edit-Model').modal('show');

        }
    });

};

function DeleteDriver(id) {

    $.ajax({
        type: "Get",
        url: '/Transport/DeleteDriver/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });

};