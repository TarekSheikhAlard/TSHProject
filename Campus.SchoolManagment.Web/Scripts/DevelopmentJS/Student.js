$(document).ready(function () {

  

    $('#Add').click(function () {
        $.ajax({
            type: "Get",
            url: '/Student/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');

            }
        });
    });








    $("#DownloadExcel").click(function () {

        var yid = $('#YearID').val();
        var gradeid = $('#GradeID').val();
        if (yid !== '' && gradeid !== '') {
            var $parent = $('.download.button');

            $parent
                .addClass("loading");

            $.fileDownload("/Student/ExportAll/?GradeID=" + gradeid + "&YearID=" + yid, {
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
        } else {
            toastr.error("Select Year and Grade")

        }


    });



    $("#DownloadPDF").click(function () {

        var $parent = $('.download.button');
        var yid = $('#YearID').val();
        var gradeid = $('#GradeID').val();

        $parent
            .addClass("loading");

        if (yid !== '' && gradeid !== '') {
            $.fileDownload("/Student/ExportPdfAll/?GradeID=" + gradeid + "&YearID=" + yid, {
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
        } else {
            toastr.error("Select Year and Grade")
        }


    });


});

function GetStudents() {

    var year = $("select#YearID").val();

    var gradeID = $("select#GradeID").val();

   var yearID = year == null || year == undefined ? null : year;

    $('#div-record').addClass('loading')



  //var table =   $('#StudentTable'.dataTable({
  //      "ajax": {
  //          "url": '/Student/StudentList/',
  //          "data": function (d) {
  //              d.GradeID = gradeID;
  //              d.YearID = yearID;
  //          }
  //      }
  //});
  //  table.ajax.reload();
    $('#StudentTable').DataTable().ajax.reload();


    //$.ajax({
    //    type: "Post",
    //    url: '/Student/StudentList/',
    //    data: { GradeID: gradeID, YearID: yearID},
    //    success: function (response) {


    //        $('#div-record')
    //            .removeClass('ui placeholder segment')
    //            .empty();
             

          

    //        table.ajax.reload();
          
           

    //    },
    //    complete: function () {
    //        $('#div-record').removeClass('loading')

    //    }
    //});

};

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/Student/Edit/' + id,
        success: function (response) {
            $('#divForAdd').empty();
            $('#divForUpdate').html(response);
            $('#Edit-Model').modal('show');
      
        }
    });



};

function EditConfig(id) {

    $.ajax({
        type: "Get",
        url: '/Student/EditConfig/' + id,
        success: function (response) {
            ///$('#divForAdd').empty();
            $('#divForEditConfig').html(response);
            $('#EditConfig-Model').modal('show');

        }
    });

};


function DeleteConfig(id) {

    $.ajax({
        type: "Get",
        url: '/Student/DeleteConfig/' + id,
        success: function (response) {
            ///$('#divForAdd').empty();
            $('#divForDeleteConfig').html(response);
            $('#DeleteConfig-Model').modal('show');

          

        }
    });

};

function OnSuccessDeleteConfig(response) {
 
    $("table#ConfigDetails tr[data-id='" + response + "']").remove();
    $('#DeleteConfig-Model').modal('hide');
    var refId = $('[name="Ref_Id"]').val();

    getFeeDropdownList(refId);

}




function Delete(id) {

    $.ajax({
        type: "Get",
        url: '/Student/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });
};



function Configration(StudentRefId)
{
  
    var Data = { StudentRefId: StudentRefId };
 
    $.ajax({
        type: "Get",
        url: '/Student/Configuration',
        data: Data,
        success: function (response)
        {
            $('#divForConfigration').html(response);
            $('#Configuration-Model').modal('show');
        }
    });
};

function FeeSearch(StudentRefId)
{
    var Data = { StudentRefId: StudentRefId};
    debugger;
    $.ajax({
        type: "Get",
        url: '/Student/FeeSelect',
        data: Data,
        success: function (response) {
            $('#divForFeeSearch').html(response);
            //$(".ui.dimmer.modals #FeeSearch-Model").remove();
            $('#FeeSearch-Model').modal('show');
        }
    });
};

function SearchAccount1() {

    $.ajax({

        type: "Get",
        url: '/Student/GetAccounts1',
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
        url: '/Student/GetAccounts2',
        success: function (response) {
            $('#divForaccount').empty();
            $('#divForaccount').html(response);
            $('#Account-Model').modal('show');

        }
    });



};

function GetFeePayment(id) {
    $('.payment.dimmer').dimmer('show');
    $.ajax({
        type: "Get",
        url: '/Student/FeeSearch/' + id,
        data: { ConfigID: id },
        success: function (response) {
            $('#divForPayment')
                .empty()
                .removeClass('placeholder')
                .html(response);
            
        },
        complete: function () {

            $('.payment.dimmer').dimmer('hide');
        }
    });

}

function GetGrade(id) {
   

    var selectedValue = id;
    $.ajax({
        url: '/Student/PopulateGradeList',
        type: "POST",
        data: { id: selectedValue },
        error: function (xhr, ajaxOptions, thrownError) {

            var gradeList = $('#GradeID');
            gradeList.empty();
            gradeList.append(
                $('<option/>', {
                    value: "",
                    html: "Select"
                })
            );

        },
        success: function (result) {
            var gradeList = $('#GradeID');
            gradeList.empty();

            gradeList.append(
                $('<option/>', {
                    value: "",
                    html: "Select"
                })
            );

            $.each(result, function (index) {    
              //console.log(index)
                if (index===0) {
                    gradeList.append(
                        $('<option/>', {
                            value: this.GradeID,
                            html: this.Gname,
                            selected:true
                        })
                    );
                }
                gradeList.append(
                    $('<option/>', {
                        value: this.GradeID,
                        html: this.Gname
                    })
                );
            });

            GetStudents();
        }
    });
}

function onSuccessFeePayment() {

    var $Amount = $('.ui.payment.form').find('input#Amount');
    var $PAmount = $('.ui.paymentDetails.form').find('input#PaidAmount');
    var $TAmount = $('.ui.paymentDetails.form').find('input#TotalAmount');
    var $RAmount = $('.ui.paymentDetails.form').find('input#remainderAmount');
    var pamt, ramt;
   

    pamt = parseFloat($PAmount.val()) + parseFloat($Amount.val())
    $PAmount.val(pamt);
    ramt = parseFloat($TAmount.val()) - pamt;
    $RAmount.val(ramt);
    $('.ui.payment.form').form('clear');

    $('.payment.dimmer').dimmer('hide');
};

function onBeginFeePayment() {
    $('.payment.dimmer').dimmer('show');
};

function DownloadPdfDetail(id, elem) {


    var $parent = $(elem).children();
    console.log($parent);

    var iconclass = "feather icon-download",
        spinnerClass = "spinner red loading icon";


    $parent
        .removeClass(iconclass)
        .addClass(spinnerClass)

    $.fileDownload("/Student/ExportPdfDetail/" + id, {
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

Bus = {
    Fees: {}
};


function GetBusFees(elem) {
    var $elem = $(elem),
    id = $elem.val(),
        $form = $(".AddBusConfig.form"),
        $checkbox = $('.trip.checkbox.checked input'),
        trip;
    $("#toast-container").remove();
    $.ajax({
        type: "Get",
        dataType:"JSON",
        url: '/Student/GetBusFees/' + id,
        success: function (response) {
            console.log(response);

            if (response.hasOwnProperty("BusID")) {

                Bus.Fees = response;
              
                $form.find("#Amount-bus").attr("placeholder", "Select trip to get amount");
                //if ($checkbox.length > 0) {           
                //        $form.find("#Amount").val(parseFloat(Bus.Fees.SingleTripCost));
                
                //}
            

            } else {
                
                Bus.Fees = {};
                toastr.error("Fees information not available");
             
            }
        },
        complete: function () {
            $('.trip.checkbox').checkbox('uncheck');
            $form.find("#Amount-bus").val('');
            $form.find("#Discount-bus").val('');
            $form.find("#Tax-bus").val('');
            $form.find("#Description-bus").val('');
         
        }
    });


}


function GetBusConfig(id) {
  
    //$.ajax({
    //    type: "Get",
    //    dataType: "JSON",
    //    url: '/Student/GetBusConfig/' + id,
    //    success: function (response) {
    //        console.log(response);

    //        if (response.hasOwnProperty("BusID")) {

    //            Bus.Fees = response;

    //            $form.find("#Amount-bus").attr("placeholder", "Select trip to get amount");
    //            //if ($checkbox.length > 0) {           
    //            //        $form.find("#Amount").val(parseFloat(Bus.Fees.SingleTripCost));

    //            //}


    //        } else {

    //            Bus.Fees = {};
    //            toastr.error("Fees information not available");

    //        }
    //    },
    //    complete: function () {
    //        $('.trip.checkbox').checkbox('uncheck');
    //        $form.find("#Amount-bus").val('');
    //        $form.find("#Discount-bus").val('');
    //        $form.find("#Tax-bus").val('');
    //        $form.find("#Description-bus").val('');

    //    }
    //});    //$.ajax({
    //    type: "Get",
    //    dataType: "JSON",
    //    url: '/Student/GetBusConfig/' + id,
    //    success: function (response) {
    //        console.log(response);

    //        if (response.hasOwnProperty("BusID")) {

    //            Bus.Fees = response;

    //            $form.find("#Amount-bus").attr("placeholder", "Select trip to get amount");
    //            //if ($checkbox.length > 0) {           
    //            //        $form.find("#Amount").val(parseFloat(Bus.Fees.SingleTripCost));

    //            //}


    //        } else {

    //            Bus.Fees = {};
    //            toastr.error("Fees information not available");

    //        }
    //    },
    //    complete: function () {
    //        $('.trip.checkbox').checkbox('uncheck');
    //        $form.find("#Amount-bus").val('');
    //        $form.find("#Discount-bus").val('');
    //        $form.find("#Tax-bus").val('');
    //        $form.find("#Description-bus").val('');

    //    }
    //});


}







//function GetClass(id) {
   
//    var selectedValue = id;//$(this).val();
//    $.ajax({
//        url: '/Student/GetClass',
//        type: "POST",
//        data: { id: selectedValue },
//        error: function (xhr, ajaxOptions, thrownError) {
//            //alert(xhr.status);
//            //alert(thrownError);
//            var ClassID = $('#ClassID');
//            ClassID.empty();
//            ClassID.append(
//                   $('<option/>', {
//                       value: "",
//                       html: "Select"
//                   })
//               );

//        },
//        success: function (result) {
//            //debugger;
//            // alert(result);
//            //here  bind second drop down list
//            var ClassID = $('#ClassID');
//            ClassID.empty();
//            ClassID.append(
//                   $('<option/>', {
//                       value: "",
//                       html: "Select"
//                   })
//               );
//            $.each(result, function () {
//                ClassID.append(
//                    $('<option/>', {
//                        value: this.ClassID,
//                        html: this.ClassName
//                    })
//                );
//            });
//        }
//    });
//};
