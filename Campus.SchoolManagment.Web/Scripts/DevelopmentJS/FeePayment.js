

getAllStudents = function () {
    $('.student.dropdown').addClass("loading")
    $.ajax({
        type: "Get",
        url: '/Student/getAllStudents/',
        dataType :"JSON",
        success: function (response) {

            if (!response.IsError) {
         

                $('.student.dropdown').dropdown({

                    values: response.data,
                    onChange: function (value, text, $choice) {
                        GetFeePaymentTable(value);
                    }

                })


            }
        },
        complete: function () {
            $('.student.dropdown').removeClass("loading")

        }
    });


}


 InitC =function() {
  
    $('.date-picker').datepicker({
        format: 'yyyy/mm/dd'
    });

};

GetFeePaymentTable = function (id) {
    $('.feePayment.dimmer').dimmer('show');

    $.ajax({
        type: "Get",
        url: '/Student/GetStudentsFeePayment/'+id,
        
        dataType: "html",
        success: function (response) {

            $("#FeeTable").html(response);            
        },
        complete: function () {
            $('.feePayment.dimmer').dimmer('hide');

        }
    });



}

RefreshFeePaymentTable = function (id) {
    GetFeePaymentTable(id);
    $('[name="amountPaid"]').val(0);

};

BindInstallmentDropdown = function () {
    $(".ui.installment.dropdown").dropdown({
        clearable: false,
        useLabels: false,
        preserveHTML:true,
        onChange: function (value, text, $choice) {
            //onClear Event
            if ($choice === undefined) {
                onClearInstallmentDropdown($(this));
            }
        },
        onAdd: function (addedValue, addedText, $addedChoice) {
            var $parent = $(this); //$addedChoice.parents('.installment.dropdown');
            var fees,totalFees;
            $parent.find('i').addClass('clear');
            if ($addedChoice.attr('data-pamount') !== undefined) {
              
                fees = parseFloat($addedChoice.attr('data-pamount'));
            } else {
                fees = parseFloat($parent.attr('data-factor'));
            }
             
            //   var selected = $parent.find('.item.active').length + 1;
            var $input = $parent.parents('tr').find('.amount.input input');
           
            if ($input.attr('data-enabled') === 'true') {
                $input.attr('data-enabled', 'false');
                $input.val('');
                
            }


            totalFees = $input.val();

          
         
           
            totalFees = isNaN(totalFees) || totalFees==""  ? 0 : parseFloat(totalFees);

           

            $parent.parents('tr').find('.amount.input input ').val(totalFees + fees);
            $addedChoice.next().removeClass('disabled');
            $addedChoice.next().find('clr-icon').css('display', 'none');

            console.log($parent.dropdown('get value'));
            updateTotal();
            updateAmountBox();

        },
        onRemove: function (removedValue, removedText, $removedChoice) {
           
            var $parent = $(this);// $removedChoice.parents('.installment.dropdown');
               var fees ,
                selected = $parent.find('.item.active').length > 0 ? $parent.find('.item.active').length - 1 : 0,
                $next = $removedChoice.next();
          

            if ($removedChoice.attr('data-pamount') !== undefined) {

                fees = parseFloat($removedChoice.attr('data-pamount'));
            } else {
                fees = parseFloat($parent.attr('data-factor'));
            }


            var totalFees = $parent.parents('tr').find('.amount.input input ').val();

            totalFees = isNaN(totalFees) || totalFees == "" ? 0 : parseFloat(totalFees);


            $parent.parents('tr').find('.amount.input input ').val(totalFees - fees);

            //$removedChoice.nextAll().each(function (index, el) {



            //})
            //console.log($($next.context))

            if ($next.length > 0 && !$next.hasClass('disabled')) {
                var nextvalue = $next.attr('data-value');

                $next.addClass('disabled').removeClass('selected');

                $next.find('clr-icon').css('display', 'inline-block');

                if ($next.hasClass('active')) {

                    $next.removeClass('active');

                    setTimeout(function () {
                        $parent.dropdown('remove selected', nextvalue);
                    });
                }
          
               // console.log($parent.dropdown('get value'));

                if ($parent.dropdown('get value').indexOf(',')===-1) {
                
                    $parent.find('i').removeClass('clear');
                }
            }

            //$parent.dropdown('clear');

           
            updateTotal();
            updateAmountBox();
        }
    });

}

BindCashAccDropdown = function () {
    var $el = $('.cash.account.dropdown');
    $el.addClass('loading');
    $.ajax({
        type: "Get",
        url: '/AccountTree/quickAccountTreeList/',

        dataType: "JSON",
        success: function (response) {
            $el.dropdown({
                values:response
            });
           
        },
        complete: function () {
            $el.removeClass('loading');

        }
    });


}

BindAmountBox = function () {
    $('[name="amountPaid"]').on('keyup', function () {
        updateAmountBox();
    })

    //$('[name="amountBalance"]').on('blur', function () {
    //    alert();
    //    value = $(this).val();
    //    if (value < 0) {

    //        return false;
    //    } else
    //        return true;

    //})

    
}

getTotal = function () {
    var amount, total = 0;
    $('.ui.amount.input input ').each(function (index, el) {
        el = $(el)
        value = el.val() == null || isNaN(el.val()) || el.val() == "" ? 0 : el.val();
        if (!isNaN(el.val())) {
            total = total + parseFloat(value)
        }
       
    })
    return total;

   
}

updateTotal = function () {
    $("#grandTotal").html(getTotal);
    
}

updateAmountBox = function () {
   
    var value = $('[name="amountPaid"]').val()
    var total = getTotal();

    var balance = 0;
    balance = value - total;
    $('[name="amountBalance"]').val(balance);


}

EnableAnyAmountPayment = function (el) {
    var $el = $(el);
    $el.off('keypress');
    //$el.val("");
    var $dropdown = $el.parents('tr').find('.installment.dropdown');
    $dropdown.dropdown('clear');

    $el.attr('data-enabled', true);
    onClearInstallmentDropdown($dropdown);
    //alert();

};


OnAmountKeyup = function (e,el) {

    updateTotal();
  
    if (e.keyCode === 8 | e.keyCode === 46) {
        var $el = $(el);
        var $dropdown = $el.parents('tr').find('.installment.dropdown');
        if ($dropdown.dropdown('get values').length>0) {
            $dropdown.dropdown('clear');
            onClearInstallmentDropdown($dropdown);

        }
    
    };
};


function onClearInstallmentDropdown(el) {
    var $parent = el;
    $parent.find('i').removeClass('clear');
    var $list = $parent.find('.menu .item'), flag = false;
    $parent.parents('tr').find('.amount.input input ').val('');
    $list.each(function (index, elem) {
        if (flag) {
            $(this).addClass('disabled')
                .find('clr-icon')
                .css('display', 'inline-block');
        }
        if (!$(this).hasClass('disabled') && flag === false) {
            flag = true;
        }
    });
    updateTotal();
    updateAmountBox();
}

/*---------------Automatic Fee Payment Scripts --------------*/


getFeeDetails = function () {
    var location = window.location.pathname;
    var studAcdID = location.split('/')[3];

    $.ajax({
        url: '/student/getFeeDetailsAutomatic/' + studAcdID,
        type: "GET",
        dataType:"JSON",
        success: function (response) {
           
            window.AutFeeDetail = response.data;
            buildPopupFeeDetails();
        }
    });
};
buildPopupFeeDetails = function () {
    var data = window.AutFeeDetail,
        html = '<table class="ui very basic collapsing celled table">';
        html +=`<thead>
                <tr><th>Fees Type</th>
                <th>Amount</th>
                <th>Installment</th>
               
                </tr></thead>
              <tbody>`;
       
    var Name, Amount, installment, TotalAmount;
    for (var i = 0; i < data.length; i++) {
        Name = data[i].AccountName;
        Amount = data[i].Amount;
        installment = data[i].AutomaticCount;
       
        if (Name.toLowerCase().indexOf('income') > -1) {
            Name = Name.toLowerCase().replace("income", "");
        }
        //if (i-1>0) {
        //    TotalAmount = data[i - 1].Amount + "+" + data[i].Amount ;
        //}

        html += "<tr><td>" + Name + "</td><td>" + Amount + " </td><td>" + installment + " </td></tr>";

       
    }
    html += "</tbody></table>";


    $('.feeDetails.icon.button').popup({
        html: html,
        position: 'right center'

    });
}
selectFeeOnPriority = function (el) {
    var $el = $(el);
    var amt = $el.val();
    var feeDetails = window.AutFeeDetail,feeAmt;
    var $feeIndicator = $("#FeeTable tbody tr");

    $feeIndicator.removeClass('selected');
    resetFeeSelection();

    if (!isNaN(amt) && amt > 0) {
       
        for (var i = 0; i < feeDetails.length; i++) {
            feeAmt = feeDetails[i].Amount;
            if (amt < 0) { break;}
      
            if (amt >= feeAmt) {
                $("#FeeTable tbody tr[data-rid='" + i + "']").addClass('selected');
                window.AutFeeDetail[i].isSelected = true;
                //$feeIndicator.find("[data-rid='" + i + "']")
            }
            amt = amt - feeAmt; 
        }

    }

};

resetFeeSelection = function () {
    var feeDetails = window.AutFeeDetail;
    if (feeDetails.length > 0) {
        for (var i = 0; i < feeDetails.length; i++) {

            feeDetails[i].isSelected = false;

        }
    }

};






