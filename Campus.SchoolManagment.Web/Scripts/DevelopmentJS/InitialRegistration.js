$(document).ready(function () {


    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/InitialRegistration/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });

    $("#DownloadPDF").click(function () {

        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/InitialRegistration/ExportPdf", {
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
        url: '/InitialRegistration/Edit/' + id,
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
        url: '/InitialRegistration/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};

function GetInitStuds(id) {

    $('.initStudent.dimmer').dimmer('show');

    $.ajax({
        type: "Get",
        url: '/InitialRegistration/GetStudents/' + id,
        success: function (response) {
        
            $('#div-record').html(response);

            $('#div-record').removeClass('placeholder');

        },
        complete: function () {

            $('.initStudent.dimmer').dimmer('hide');
        }
    });

}