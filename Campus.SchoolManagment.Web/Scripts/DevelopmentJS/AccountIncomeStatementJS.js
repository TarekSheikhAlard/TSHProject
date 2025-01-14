
function Search() {

    if ($("#From").val() == "" || $("#To").val() == "" || $("#From").val() == "From" || $("#To").val() == "To" || $("#From").val() == undefined || $("#To").val() == undefined) {
        alert("You Must Select Dates");
    }
    else {
        $.ajax({
            type: "Post",
            url: '/AccountIncomeStatement/List',
            data: { From: $("#From").val(), To: $("#To").val() },
            success: function (data) {
                $('#div-record').html(data);
            }
        });
    }
}
