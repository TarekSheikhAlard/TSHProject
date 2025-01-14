$(document).ready(function () {

    $('#AddSupervisor').click(function () {
        $.ajax({
            type: "Get",
            url: '/Transport/CreateSupervisor/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });

    $("#DownloadExcelSupervisor").click(function () {
        var $parent = $('.download.button');


        $parent
            .addClass("loading");
        $.fileDownload("/Transport/ExportExcelSupervisor/", {
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

    $("#DownloadPDFSupervisor").click(function () {

        var $this = $(this);

        var $parent = $('.download.button');
        $parent
            .addClass("loading");
        $.fileDownload("/Transport/ExportPdfSupervisor/", {
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

function EditSupervisor(id) {

    $.ajax({
        type: "Get",
        url: '/Transport/EditSupervisor/' + id,
        success: function (response) {
            $('#divForAdd').empty();
            $('#divForUpdate').html(response);
            $('#Edit-Model').modal('show');

        }
    });

};

function DeleteSupervisor(id) {

    $.ajax({
        type: "Get",
        url: '/Transport/DeleteSupervisor/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });

};