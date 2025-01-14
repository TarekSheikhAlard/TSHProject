$(document).ready(function () {
 
    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/AccountDailyJournal/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model')
                    .modal('show');
                    
                
               
            }
        });
    });

    $("#DownloadExcel").click(function () {

        var $parent = $('.download.button');

        $parent
            .addClass("loading");

        $.fileDownload("/AccountDailyJournal/ExportAll", {
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

        $.fileDownload("/AccountDailyJournal/ExportPdf", {
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

    $("#DownloadExcelPost").click(function () {
        if ($("#From").val() === "" || $("#To").val() === "" || $("#From").val() === "From" || $("#To").val() === "To" || $("#From").val() === undefined || $("#To").val() === undefined) {

            toastr.error("You Must Select Dates")
        }
        else {
            var $parent = $('.download.button');

            $parent
                .addClass("loading");

            $.fileDownload('/AccountDailyJournal/ExportExcelForPost?From=' + $("#From").val().toString() + '&To=' + $("#To").val().toString(), {
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
        }

    });

    $("#DownloadPDFPost").click(function () {
        if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined) {

            toastr.error("You Must Select Dates")
        }
        else {
            var $parent = $('.download.button');

            $parent
                .addClass("loading");

            var from = $("#From").val();
            var to = $("#To").val();
            var formTo = from + "+" + to;

            $.fileDownload('/AccountDailyJournal/ExportPdfForpost?From=' + $("#From").val().toString() + '&To=' + $("#To").val().toString(), {
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
        }

    });

    $("#DownloadExcelUnPost").click(function () {
        if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined) {

            toastr.error("You Must Select Dates")
        }
        else {
            var $parent = $('.download.button');

            $parent
                .addClass("loading");

            $.fileDownload('/AccountDailyJournal/ExportExcelForUnPost?From=' + $("#From").val().toString() + '&To=' + $("#To").val().toString(), {
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

        }
    });

    $("#DownloadPDFUnPost").click(function () {
        if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined) {

            toastr.error("You Must Select Dates")
        }
        else {
            var $parent = $('.download.button');

            $parent
                .addClass("loading");

            var from = $("#From").val();
            var to = $("#To").val();
            var formTo = from + "+" + to;

            $.fileDownload('/AccountDailyJournal/ExportPdfForUnpost?From=' + $("#From").val().toString() + '&To=' + $("#To").val().toString(), {
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
        }

    });

    $('.modal.image-viewer').modal({
        closable: true
    });
});

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/AccountDailyJournal/Edit/' + id,
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
        url: '/AccountDailyJournal/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};

function SearchAccount() {

    $.ajax({
        
        type: "Get",
        url: '/AccountDailyJournal/GetAccounts',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });



};

function GetDetail(ID) {
   
    $('#myModal').modal('hide');
    $.ajax({
        type: "POST",
        url: '/AccountDailyJournal/GetDetail',
        data: { id: ID },
        success: function (data) {
            $('#popupDetail').html(data);
            $('#myModal').modal('show');
        }
    });
}

function Search() {

    if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined) {
        toastr.error("You Must Select Dates")
    }
    else {
        debugger;
        $.ajax({
            type: "Post",
            url: '/AccountDailyJournal/searchForpost',
            data: { From: $("#From").val(), To: $("#To").val() },
            success: function (data) {
                $('#div-record').html(data);
            }
        });
    }
}

function Search1() {

    if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined) {
        toastr.error("You Must Select Dates")
    }
    else {
        debugger;
        $.ajax({
            type: "Post",
            url: '/AccountDailyJournal/searchForUNpost',
            data: { From: $("#From").val(), To: $("#To").val() },
            success: function (data) {
                $('#div-record').html(data);
            }
        });
    }
}

function SearchAccountProf() {

    if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined ){
        toastr.error("You Must Select Dates")
    }
    else {
        debugger;
        var cx = $("#From").val();
        var xc = $("#Accounttree").val();
        var cv = $("#To").val();
       
        $.ajax({
            type: "Post",
            url: '/AccountDailyJournal/AccountProf',
            data: { From: $("#From").val(), To: $("#To").val(), Accounttree:$("#Accounttree").val()},
           
            success: function (data) {
                $('#div-record').html(data);
            }
        });
    }
}

function SearchAccountsubsidiaryledger() {

    if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined) {
        toastr.error("You Must Select Dates")
    }
    else {
        debugger;
        var cx = $("#From").val();
        var xc = $("#AccountCode").val();
        var cv = $("#To").val();

        $.ajax({
            type: "Post",
            url: '/AccountDailyJournal/subsidiaryledgerList',
            data: { From: $("#From").val(), To: $("#To").val(), AccountCode: $("#AccountCode").val() },

            success: function (data) {
                $('#div-record').html(data);
            }
        });
    }
}

function DownloadPdfDetail(id,elem) {


    var $parent = $(elem).children();
   

    var iconclass="feather icon-download",
        spinnerClass = "spinner red loading icon";


    $parent
        .removeClass(iconclass)
         .addClass(spinnerClass)
   
       $.fileDownload("/AccountDailyJournal/ExportPdfDetail/"+id, {
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

function GetList(id) {
    $('.loading.dimmer').dimmer('show');
    $.ajax({
        type: "POST",
        url: '/AccountDailyJournal/GetListByType',
        data: { id },
        success: function (data) {

            updateList(data, function () {
                $('.loading.dimmer').dimmer('hide');
            });
            
        }
    });
}

function updateList(html, onComplete) {
    $('#div-record').html(html);

    if (typeof onComplete === 'function') { $('.loading.dimmer').dimmer('hide'); }

}

populateCostCenter = function () {
    $.ajax({
        type: "POST",
        url: '/AccountCostCenter/CostCenterSelectList',
        dataType: "JSON",
        success: function (data) {
            //$('.CostCenterID.dropdown .menu').children()
            $('.CostCenterID.dropdown').each(function (index,el) {

                if ($(el).find('.menu').children('div').length === 0) {

                  
                    $(el).dropdown({
                        values: data.data,
                        clearable:true
                        
                    });

                }

            });
        }

    });

};

AddDetailItem = function (a) {

    if ($("#Edit1").val() === "" || a === true) {

        for (var i = 0; i < 1; i++) {
            $.ajax({
                type: "POST",
                url: '/AccountDailyJournal/AddDetail',
                data: { DailyJournalID: $("#DailyJournalID").val() },
                success: function (data) {
                   
                    $('#DetailContainer').append(data);
                }
            });
        }
    }


};


BindEventForJournal = function () {
  
        $('.accountSelect.dropdown').dropdown({
            onChange: function (value, text, $choice) {
                onChangeItemDropdown(value, $choice);
                showHideDel($choice);
            },
            match: 'both',
            ignoreCase: true,
            fullTextSearch: true
        });



};

showHideDel = function ($choice) {
    if ($choice.parents('tbody').find('tr').length > 1) {
      // $choice.parents('tbody').find("tr td[data-name='del'] i").removeClass('hide');
    //$choice.parents('tbody').find("tr td[data-name='del'] i").css('display', 'block');
}
}

onChangeItemDropdown = function (value, $choice) {

    // if ($choice.parents('tbody').find('tr').length == 2) {

    //    $choice.parents('tbody').find("tr td[data-name='del'] i").css('display', 'block');

    //}
 
    if ($choice.parents('tr').nextAll('tr').length === 0) {
        AddDetailItem(true);
        //$choice.parents('tr').parent().append(itemRowHtml.format(parseInt(rowid) + 1, parseInt(rowid) + 2))
    }
}

zerofy = function (el, name) {
    var $el = $(el);

    if (name === "credit") {
        $el.parent().next().find('.Credit').val("0.00");
    }
    if (name === "debit") {
        $el.parent().prev().find('.Debit').val("0.00");
    }


};

ViewAttachedDocument = function (filename) {
    var $model = $('.modal.image-viewer'),
        root = $model.attr('data-root'),
        path = root + filename;
    $model.find('img').attr('src', path);
    $model.modal('show');

};

/*Grid functions*/

RemoveRow = function (elem) {
    var $elem = $(elem);

    //var rowid = $elem.parents('tr').attr('data-rowid');
    var table = $elem.parents('tbody');

    $elem.parents('tr').remove();
    
   // refreshIndexTable(table);

    if (table.children('tr').length === 1) {
        table.find("td[data-name='del'] i").addClass('hide');

       
        //.children().children("td[data-name='del']").chi.css('display', 'none')
    }
    bindElasticDescTextArea();
   

}

bindElasticDescTextArea = function () {

    var $masterDesc = $($('textarea.desc')[0]);
    var $notMasterDesc = $('textarea.desc:not(.master)');

    $masterDesc.removeClass('not-master').addClass('master');

    $masterDesc.off('keyup');
    $notMasterDesc.off('keyup');

    $masterDesc.on('keyup', function () {

        $('textarea.desc:not(.master):not(.not-master)').val($(this).val())
        $('[name="Note"]').val($(this).val());
    });

    $notMasterDesc.on('keyup', function () {

        $(this).addClass('not-master');

    });

}

SetSelectedDropdown = function ($elem) {
    var value;
    $.each($elem.find('.accountSelect.dropdown'), function (index, el) {
        value = $(el).find('.value input').val();
        exVal = $(el).find('.text').html();
        if (value != '' && value != null && (exVal == '' || exVal =='Search')) {
            $(el).dropdown('set selected', value);
        }
       // console.log($(el).find('.value input'))
        
       
    });
    $.each($elem.find('.CostCenterID.dropdown'), function (index, el) {
        value = $(el).find('.value input').val();
        exVal = $(el).find('.text').html();
        // console.log($(el).find('.value input'))
        
        if (value != '' && value != null && (exVal == '' || exVal == 'Search')) {
            $(el).dropdown('set selected', value);
        }
       

    });



}



GetReferenceNo = function (date,el) {
    $.ajax({
        type: "POST",
        url: '/AccountDailyJournal/GetReferenceNo',
        data: { journalDate: date },
        success: function (data) {
            el.val(data);
        }
    });

}

