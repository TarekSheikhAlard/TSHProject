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

            $('.grade.dropdown').dropdown({ values: {} });

            gradeList.empty();
            gradeList.append(
                $('<option/>', {
                    value: "",
                    html: "Select"
                })
            );
            $.each(result, function () {

                gradeList.append(
                    $('<option/>', {
                        value: this.GradeID,
                        html: this.Gname                 })
                );
            });
        }
    });
};


function GetConfigs() {
    var year = $("select#YearID").val();
    var gradeID = $("select#GradeID").val();
    var $form = $('.gradeFeeConfig.form')
    $form.form('validate form');
    var yearID = year === null || year === undefined ? null : year;

    $('#div-record').addClass('loading');
    if ($form.form('is valid')) {
        $('.feeConfig.dimmer').dimmer('show');
        $.ajax({
            type: "Post",
            url: '/GradesFeeConfig/GetFeeConfig/',
            data: { GradeID: gradeID},
            success: function (response) {
                $('.main.segment')
                    .empty()
                    .html(response);
                BindSortable();

            },
            complete: function () {
                $('.feeConfig.dimmer').dimmer('hide');

            }
        });
    }

};


$("#GradeID").change(function () {
    GetConfigs();

})


function BindSortable() {
    $("#sortable").sortable({
        animation: 150,
        //handle: ".handle",
        ghostClass: "sortable-ghost", // Class name for the drop placeholder
        chosenClass: "sortable-chosen", // Class name for the chosen item
        dragClass: "sortable-drag",
        onEnd: function (evt) {
            var itemEl = evt.item;  // dragged HTMLElement
            evt.to;    // target list
            evt.from;  // previous list
            evt.oldIndex;  // element's old index within old parent
            evt.newIndex;  // element's new index within new parent
            evt.oldDraggableIndex; // element's old index within old parent, only counting draggable elements
            evt.newDraggableIndex; // element's new index within new parent, only counting draggable elements
            evt.clone // the clone element
            evt.pullMode;  // when item is in another sortable: `"clone"` if cloning, `true` if moving

            var $elem = $(itemEl);
            var nIndex = evt.newIndex + 1,
                oIndex = evt.oldIndex + 1;

            $elem.parent().children().each(function (i, el) {
                i += 1
                $(el).attr('data-index', i);
                $(el).find('[data-selector="Priority"]').val(i);
            });


            // $elem.parent().find('li[data-index="'+nIndex+'"]').attr('data-index', oIndex)
            //$elem.attr('data-index', nIndex);


        }

    });
}


function GetFeesReport() {

    var YearID = $('#YearID').dropdown('get value');
    if (YearID !=="") {
        $.ajax({
            type: "Get",
            url: '/GradesFeeConfig/FeesReport/' + YearID,

            success: function (response) {
                $('#divForReport').empty();
                $('#divForReport').html(response);
                $('#Fee-Report-Model').modal('show');

            }
        });
    } else {
        toastr.error("Select Study Year ");
    }


}

