

function AddNew(level) {
    debugger;

    var parentid = $('#AssetID').val() == null || $('#AssetID').val() == undefined ? 0 : $('#AssetID').val();

    $.ajax({
        type: "Get",
        url: '/AssetTree/Create/',
        data: { level: level, _parentID: parentid},
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
        url: '/AssetTree/Edit/' + id,
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
        url: '/AssetTree/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });
};



function GetChild(id) { 
    $.ajax({
        type: "Get",
        url: '/AssetTree/GetChild/' + id,
        success: function (response) {
            $('#div-record').html(response);
          $("input[name=AssetID]:hidden").val(id);
        }
    });
};

function GetRoot() {
  
    $.ajax({
        type: "Get",
        url: '/AssetTree/GetRoot',
        success: function (response) {
            $('#div-record').html(response);
           $("input[name=AssetID]:hidden").val(null);
        }
    });

}



