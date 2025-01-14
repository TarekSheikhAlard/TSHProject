function SearchAsset() {

    $.ajax({
        type: "Get",
        url: '/AssetDepreciation/GetAsset',
        success: function (response) {
            $('#divForAsset').empty();
            $('#divForAsset').html(response);
            $('#Asset-Model').modal('show');

        }
    });
};


function GetDetail(ID) {
    debugger;
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


function Delete(id) {

    $.ajax({
        type: "Get",
        url: '/AssetDepreciation/Delete/' + id,
        success: function (response) {
            $('#divForDelete').html(response);
            $('#Delete-Model').modal('show');
        }
    });
};


function deleteClient(response) {
    response = JSON.parse(response);

    if (!response.IsError) {
       
        $.get("/AssetDepreciation/GetDepreciation/"+$('#SubmitDepreciation').attr("data-cache"), function (data) {
            $('#div-record')
                .removeClass("placeholder")
                .html(data);
        });
        $('#Delete-Model').modal('hide');

    }
}