$(document).ready(function () {
 
   
    $('#Add').click(function () {

        $.ajax({
            type: "Get",
            url: '/AccountEmployeeSalaryItem/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');
               
            }
        });
    });

});

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/AccountEmployeeSalaryItem/Edit/' + id,
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
        url: '/AccountEmployeeSalaryItem/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};



function BindSalaryItemTypeID(id) {

    var selectedValue = id;//$(this).val();
    $.ajax({
        url: '/AccountEmployeeSalaryItem/GetChild',
        type: "POST",
        data: { id: selectedValue },
        error: function (xhr, ajaxOptions, thrownError) {
            //alert(xhr.status);
            //alert(thrownError);
            var secondDdl = $('#SalaryItemID');
            secondDdl.empty();
            secondDdl.append(
                   $('<option/>', {
                       value: 0,
                       html: "Select"
                   })
               );

        },
        success: function (result) {
            //  alert(result);
            //here  bind second drop down list
            var secondDdl = $('#SalaryItemID');
            secondDdl.empty();
            secondDdl.append(
                   $('<option/>', {
                       value: 0,
                       html: "Select"
                   })
            );

            $.each(result, function () {

                secondDdl.append(
                    $('<option/>', {
                        value: this.Id,
                        html: this.Salary_itemName
                    })
                );
            });
        }
    });
};
