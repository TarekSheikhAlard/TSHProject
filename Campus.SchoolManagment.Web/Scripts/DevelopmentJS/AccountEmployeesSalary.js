$(document).ready(function () {


    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/AccountEmployeesSalary/Create/',
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

        $.fileDownload("/AccountEmployeesSalary/ExportAll", {
            successCallback: function (url) {
                // alert();
                $parent
                    .removeClass("loading");
            },
            failCallback: function (html, url) {
                toastr.error("Error Occured while Download")
                $parent
                    .removeClass("loading");
            }
        });


    })



    $("#DownloadPDF").click(function () {

        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/AccountEmployeesSalary/ExportPdfAll", {
            successCallback: function (url) {
                // alert();
                $parent
                    .removeClass("loading");
            },
            failCallback: function (html, url) {
                toastr.error("Error Occured while Download")
                $parent
                    .removeClass("loading");
            }
        });


    })





});

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/AccountEmployeesSalary/Edit/' + id,
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
        url: '/AccountEmployeesSalary/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};

function DownloadPdfDetail(id, elem) {


    var $parent = $(elem).children();
    console.log($parent);

    var iconclass = "feather icon-download",
        spinnerClass = "spinner red loading icon";


    $parent
        .removeClass(iconclass)
        .addClass(spinnerClass)

    $.fileDownload("/AccountEmployeesSalary/ExportPdfDetail/" + id, {
        successCallback: function (url) {
            // alert();
            $parent
                .removeClass(spinnerClass)
                .addClass(iconclass)
        },
        failCallback: function (html, url) {
            toastr.error("Error Occured while Download")
            $parent
                .removeClass(spinnerClass)
                .addClass(iconclass)
        }
    });




}

