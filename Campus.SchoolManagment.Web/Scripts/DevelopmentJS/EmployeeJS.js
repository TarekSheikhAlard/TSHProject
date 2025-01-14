ErrMsgs = {
    noSalary: "Salary imformation not avalaible"
}


$(function () {
    BindHrModuleEvents();
})

function BindHrModuleEvents() {
    $('a.icon.button')
        .popup();
   // $('#Add-Model').modal();
    $('#Add').click(function () {
        //   semantic.clearCacheModals();
        $.ajax({
            type: "Get",
            url: '/Employee/Create/',
            success: function (response) {
                $('#divForUpdate').empty();
                $('#divForAdd').html(response);
                $('#Add-Model').modal('show');
            }
        });
    });
    $("#DownloadExcel").click(function () {
        var $parent = $('.download.button');
        var Deptid = $("#DepartmentTreeID").val();
        if (Deptid !== '') {
            $parent
                .addClass("loading");
            $.fileDownload("/Employee/ExportAll/" + Deptid, {
                successCallback: function (url) {
                    // alert();
                    $parent
                        .removeClass("loading");
                },
                failCallback: function (html, url) {
                    toastr.error("Error Occured while Download");
                    $parent
                        .removeClass("loading");
                }
            });
        }
        else {
            toastr.error("Select a Department ");
        }
    });
    $("#DownloadPDF").click(function () {
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            var $parent = $('.download.button');
            $parent
                .addClass("loading");
            $.fileDownload("/Employee/ExportPdfDetail/" + empid, {
                successCallback: function (url) {
                    // alert();
                    $parent
                        .removeClass("loading");
                },
                failCallback: function (html, url) {
                    toastr.error("Error Occured while Download");
                    $parent
                        .removeClass("loading");
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#EmployeeReturn').click(function () {
        // semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/EmployeeReturnHistory/Index/' + empid,
                success: function (response) {
                    $('#divForHistory').empty();
                    $('#divForHistory').html(response);
                    $('#History-Model').modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#ChangeDept').click(function () {
        //   semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/Employee/ChangeDepartment/',
                success: function (response) {
                    $('#divForChangeDept').empty();
                    $('#divForChangeDept').html(response);
                    $('#ChangeDept-Model').modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#ChangeSponsor').click(function () {
        // semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/Employee/ChangeSponsor/' + empid,
                success: function (response) {
                    $('#divForChangeSponsor').empty();
                    $('#divForChangeSponsor').html(response);
                    $('#ChangeSponsor-Model').modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#PayHousing').click(function () {
        //semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/Employee/PayHousing/' + empid,
                success: function (response) {
                    if (response.length > 0) {
                        $('#divForPayHousing').empty();
                        $('#divForPayHousing').html(response);
                        $('#PayHousing-Model').modal('show');
                    }
                    else {
                        toastr.error(ErrMsgs.noSalary);
                    }
                },
                complete: function () {
                    $this.removeClass('loading');
                },
                error: function () {
                    alert();
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#PayTickets').click(function () {
        //  semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/Employee/PayTickets/' + empid,
                success: function (response) {
                    if (response.length > 0) {
                        $('#divForPayTickets').empty();
                        $('#divForPayTickets').html(response);
                        $('#PayTickets-Model').modal('show');
                    }
                    else {
                        toastr.error(ErrMsgs.noSalary);
                    }
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#EmployeeVacation').click(function () {
        // semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/Employee/EmployeeVacation/' + empid,
                success: function (response) {
                    $('#divForEmployeeVacation').empty();
                    $('#divForEmployeeVacation').html(response);
                    $("#Employee_ID").val(empid);
                    $('#EmployeeVacation-Model').modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#EmployeePromotion').click(function () {
        //  semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/Employee/EmployeePromotion/' + empid,
                success: function (response) {
                    $('#divForEmployeePromotion').empty();
                    $('#divForEmployeePromotion').html(response);
                    $("#Employee_ID").val(empid);
                    $('#EmployeePromotion-Model').modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#EmployeeAssets').click(function () {
        //  semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/EmployeeAssets/Index/' + empid,
                success: function (response) {
                    $('#divForEmployeeAssets').empty();
                    $('#divForEmployeeAssets').html(response);
                    $("#Employee_ID").val(empid);
                    $('#EmployeeAssets-Model')
                        .modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#EmployeeLoans').click(function () {
        // semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/EmployeeLoans/Index/' + empid,
                success: function (response) {
                    $('#divForEmployeeLoans').empty();
                    $('#divForEmployeeLoans').html(response);
                    $("#Employee_ID").val(empid);
                    $('#EmployeeLoans-Model')
                        .modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
    $('#EmployeeTermination').click(function () {
        //    semantic.clearCacheModals();
        var empid = $("[name='empId']:checked").val();
        var $this = $(this);
        if (empid != undefined && empid != null) {
            $this.addClass('loading');
            $.ajax({
                type: "Get",
                url: '/Employee/EmployeeTermination/',
                success: function (response) {
                    $('#divForEmployeeTermination').empty();
                    $('#divForEmployeeTermination').html(response);
                    $("#Employee_ID").val(empid);
                    $('#EmployeeTermination-Model')
                        .modal('show');
                },
                complete: function () {
                    $this.removeClass('loading');
                }
            });
        }
        else {
            toastr.error("Select a Employee");
        }
    });
}

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/Employee/Edit/' + id,
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
        url: '/Employee/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });



};

function DeleteEntry(id) {

    $.ajax({
        type: "Get",
        url: '/Employee/DeleteEntry/' + id,
        success: function (response) {
            $('#divForDeleteEntry').html(response);
            $('#DeleteEntry-Model').modal('show');
        }
    });

};


function GetEmployes() {
    var id = $('#DepartmentTreeID').val();

    $.ajax({
        type: "Get",
        url: '/Employee/EmployeeList/' + id,
        success: function (response) {
            $('#div-record').empty();
            $('#div-record').html(response);

        }
    });

};

function SearchDepartment() {

    $.ajax({

        type: "Get",
        url: '/DepartmentTree/GetDepartments',
        success: function (response) {
            $('#divFordepartment').empty();
            $('#divFordepartment').html(response);
            $('#Department-Model').modal('show');
        }
    });



};

function TerminationDetails(id) {


    $.ajax({
        type: "Get",
        url: '/Employee/TerminationDetails/'+id,
        success: function (response) {
            $('#divForTerminationDetails').empty();
            $('#divForTerminationDetails').html(response);
            $('#TerminationDetails-Model').modal('show');
        }
    });


}
