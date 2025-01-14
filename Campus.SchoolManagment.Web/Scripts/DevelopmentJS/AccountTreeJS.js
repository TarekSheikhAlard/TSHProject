
$("#DownloadExcel").click(function () {

 
   
        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/AccountTree/ExportExcel/", {
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
   

});
$("#DownloadPDF").click(function () {



    var $parent = $('.download.button');

    $parent
        .addClass("loading");

    $.fileDownload("/AccountTree/ExportPdf/", {
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


});

function AddNew() {
    if ($('.add.popup').hasClass('render')) {
         $.ajax({
            type: "Get",
            url: '/AccountTree/Create/'  ,
           // data: { level: level, _parentID: $('#AccountID').val() },
            success: function (response) {
                $('#divForUpdate').empty();
                //$('#divForAdd').html(response);
                //$('#Add-Model').modal('show');
                $('.add.popup.render')
                    .html(response)
                    .removeClass('render');

                GetAccountTypesDropdown($('.accountType.dropdown'));

            }
        });
    }
}

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/AccountTree/Edit/' + id,
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
        url: '/AccountTree/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });
};



function GetChild(id) {
    debugger
    $.ajax({
        type: "Get",
        url: '/AccountTree/GetChild/' + id,
        success: function (response) {
            $('#div-record').html(response);
          $("input[name=AccountID]:hidden").val(id);
        }
    });
};

function GetAccountTypesDropdown($elem) {
 
    $.ajax({
        type: "Get",
        dataType:"JSON",
        url: '/AccountTree/AccountTypeList',
        success: function (response) {
            $elem.dropdown({
                values: response.list,
                clearable: true,
                placeholder: 'Search',
                match: 'both',
                ignoreCase: true,
                fullTextSearch: true

            });
        }
    });
}

function hideTree() {

    $elem = $('.tree.segment');

    $elem
        .transition('fade right');

   
        
   // $('#AccountTreeTable').DataTable().columns.adjust().draw();
    setTimeout(function () {
        $('.accounTree.grid').find('.five.tree').removeClass('wide column');

        $('.accounTree.grid').find('.eleven.tree').removeClass('eleven wide column').addClass('sixteen  wide column');
        $('.showTree').transition('fade ');
    }, 230);
    setTimeout(function () {

        $('#AccountTreeTable').DataTable().columns.adjust().draw();
    },565);
   
}

function showTree() {
    $('.showTree').transition('fade ');
    $elem = $('.tree.segment');
    $('.accounTree.grid').find('.five.tree').addClass('wide column');
    $('.accounTree.grid').find('.sixteen.tree').removeClass('sixteen wide column').addClass('eleven  wide column');

   

    // $('#AccountTreeTable').DataTable().columns.adjust().draw();
    setTimeout(function () {
      

        $elem
            .transition('fade right');
        $('#AccountTreeTable').DataTable().columns.adjust().draw();
      

    }, 340);
    setTimeout(function () {
      
      
    }, 500);

}