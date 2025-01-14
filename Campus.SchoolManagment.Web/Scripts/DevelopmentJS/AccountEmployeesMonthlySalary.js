$(document).ready(function () {


    $('#Add').click(function () {
        var $this = $(this);
        $this.addClass("loading");
        $.ajax({
            type: "Get",
            url: '/AccountEmployeesMonthlySalary/Create/',
            success: function (response) {
              
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            },
            complete: function () {
                $this.removeClass("loading");
            }


        });
    });



    $("#DownloadExcel").click(function () {

        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/AccountEmployeesMonthlySalary/ExportAll", {
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

        $.fileDownload("/AccountEmployeesMonthlySalary/ExportPdfAll", {
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
        url: '/AccountEmployeesMonthlySalary/Edit/' + id,
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
        url: '/AccountEmployeesMonthlySalary/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};




function SearchAccount1() {

    $.ajax({

        type: "Get",
        url: '/AccountEmployeesMonthlySalary/GetAccounts1',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });



};


function SearchAccount2() {

    $.ajax({

        type: "Get",
        url: '/AccountEmployeesMonthlySalary/GetAccounts2',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });
};



function SearchAccount3() {

    $.ajax({

        type: "Get",
        url: '/AccountEmployeesMonthlySalary/GetAccounts3',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });



};


function SearchAccount4() {

    $.ajax({

        type: "Get",
        url: '/AccountEmployeesMonthlySalary/GetAccounts4',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

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

    $.fileDownload("/AccountEmployeesMonthlySalary/ExportPdfDetail/" + id, {
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
