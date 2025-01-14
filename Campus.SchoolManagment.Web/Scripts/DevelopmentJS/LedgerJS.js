
function GetLedgerReport(el) {
    $el = $(el);
    value = $('input[name="AccountTree"]').val();
    from = $('input[name="From"]').val();
    to = $('input[name="To"]').val();
    if (value == "" || value== undefined) {
        toastr.error("You Must Select Account");
    }
    else {
        $el.addClass("loading")
        $.ajax({
            type: "Post",
            url: '/AccountDailyJournal/Ledger',
            data: { AccountTreeID: value,From:from,To:to },
            success: function (data) {
                $('#div-record').html(data);
            },
            complete: function () {
                $el.removeClass("loading")
            }
        });

    }
}
