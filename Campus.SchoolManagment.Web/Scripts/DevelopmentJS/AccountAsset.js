$(document).ready(function () {
   

    $('#Add-asset').click(function () {

        $.ajax({
            type: "Get",
            url: '/AccountAsset/Create/',
            success: function (response) {
                $('#divForUpdate-asset').empty();
                $('#divForAdd-asset').html(response);
                $('#Add-Model-asset').modal('show');

            }
        });
    });

    $("#DownloadExcel-asset").click(function () {

        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/AccountAsset/ExportAll", {
            successCallback: function (url) {
                // alert();
                $parent
                    .removeClass("loading");
            },
            failCallback: function (html, url) {
                $parent
                    .removeClass("loading");
            }
        });


    })

    $("#DownloadPDF-asset").click(function () {

        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/AccountAsset/ExportPdf", {
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
        url: '/AccountAsset/Edit/' + id,
        success: function (response) {
            $('#divForAdd-asset').empty();
            $('#divForUpdate-asset').html(response);
            $('#Edit-Model-asset').modal('show');

        }
    });



};

function Delete(id) {

    $.ajax({
        type: "Get",
        url: '/AccountAsset/Delete/' + id,
        success: function (response) {
            $('#divForDelete-asset').html(response);
            $('#Delete-Model-asset').modal('show');
        }
    });



};

function SearchAsset() {
    
    $.ajax({

        type: "Get",
        url: '/AccountAsset/GetAsset',
        success: function (response) {
            $('#divForAsset').empty();
            $('#divForAsset').html(response);
            $('#Asset-Model').modal('show');

        }
    });
};

function SearchAccount(val) {

    debugger;
    $.ajax({
        type: "Get",
        url: '/AccountAsset/GetAccounts',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');
            $('#Save').attr("data-elem",val)
        }
    });
};


