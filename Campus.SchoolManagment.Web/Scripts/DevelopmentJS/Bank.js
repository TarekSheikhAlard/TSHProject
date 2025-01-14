$(document).ready(function () {


    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/Bank/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });
    $("#DownloadExcel").click(function () {
        var $parent = $('.download.button');
  
       
            $parent
                .addClass("loading");
            $.fileDownload("/Bank/ExportExcel/", {
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
    $("#DownloadPDF").click(function () {
     
        var $this = $(this);
 
            var $parent = $('.download.button');
            $parent
                .addClass("loading");
            $.fileDownload("/Bank/ExportPdf/", {
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

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/Bank/Edit/' + id,
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
        url: '/Bank/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};