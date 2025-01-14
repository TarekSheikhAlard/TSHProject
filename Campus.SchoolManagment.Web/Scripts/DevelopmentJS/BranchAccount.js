$(document).ready(function () {


    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/BranchAccount/Create/',
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

        $.fileDownload("/BranchAccount/ExportPdf", {
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
        url: '/BranchAccount/Edit/' + id,
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
        url: '/BranchAccount/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};



function GetBranch(id) {

    var selectedValue = id;//$(this).val();
    $.ajax({
        url: '/BranchAccount/GetBranchs',
        type: "POST",
        data: { id: selectedValue },
        error: function (xhr, ajaxOptions, thrownError) {
            //alert(xhr.status);
            //alert(thrownError);
            var BranchID = $('#BranchID');
            BranchID.empty();
            BranchID.append(
                   $('<option/>', {
                       value: "",
                       html: "Select"
                   })
               );

        },
        success: function (result) {
            //debugger;
            // alert(result);
            //here  bind second drop down list
            var BranchID = $('#BranchID');
            BranchID.empty();
            BranchID.append(
                   $('<option/>', {
                       value: "",
                       html: "Select"
                   })
               );
            $.each(result, function () {
                BranchID.append(
                    $('<option/>', {
                        value: this.ID,
                        html: this.BranchName
                    })
                );
            });
        }
    });
};
