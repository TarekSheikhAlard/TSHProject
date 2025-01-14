



function AddNew(level) {
    debugger;

    var parentid = $('#DepartmentID').val() == null || $('#DepartmentID').val() == undefined ? 0 : $('#DepartmentID').val();

    $.ajax({
        type: "Get",
        url: '/DepartmentTree/Create/',
        data: { level: level, _parentID: parentid },
        success: function (response) {
            $('#divForUpdate').empty();
            $('#divForAdd').html(response);
            $('#Add-Model').modal('show');
        }
    });



    //if ($('#Parentlevel').val() != 3 && $('#Parentlevel').val() != undefined) {

    //}
    //else {
    //    alert("1");
    //}
}

function Edit(id) {

    $.ajax({
        type: "Get",
        url: '/DepartmentTree/Edit/' + id,
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
        url: '/DepartmentTree/Delete/' + id,
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
        url: '/DepartmentTree/GetChild/' + id,
        success: function (response) {
            $('#div-record').html(response);
            $("input[name=DepartmentID]:hidden").val(id);
        }
    });
};

function GetRoot() {

    $.ajax({
        type: "Get",
        url: '/DepartmentTree/GetRoot',
        success: function (response) {
            $('#div-record').html(response);
            $("input[name=DepartmentID]:hidden").val(null);
        }
    });

}



