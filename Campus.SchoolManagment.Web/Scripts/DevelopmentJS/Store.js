﻿$(document).ready(function () {


    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/Store/Create/',
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

        $.fileDownload("/Store/ExportAll", {
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

    $("#DownloadPDF").click(function () {

        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/Store/ExportPdf", {
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
        url: '/Store/Edit/' + id,
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
        url: '/Store/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};

function SearchInventory(nameElem, IdElem) {
    $.ajax({
        type: "Get",
        url: '/InventoryTree/GetInventory',
        success: function (response) {
            $('#divForInventory').empty();
            $('#divForInventory').html(response);
            $('#Inventory-Model').modal('show');
            $('.Inventory button#ok').attr("data-elem-value", IdElem)
            $('.Inventory button#ok').attr("data-elem-name", nameElem)

        }
    });
};

function SearchAccount(nameElem, IdElem) {
    $('#' + nameElem).addClass('loading');
    $.ajax({
        type: "Get",
        url: '/Store/GetAccounts',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');
            $('.account.form #Save').attr("data-elem-value", IdElem)
            $('.account.form #Save').attr("data-elem-name", nameElem)
        },
        complete: function () {

            $('#' + nameElem).removeClass('loading');
        }
    });
};


