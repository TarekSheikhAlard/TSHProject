$(function () {

    $('.receive.menu').tab();

    allItems();

   
   
});

var rid = 1;

Order = {
    items: {},
    itemDropdown:`
                <div class="ui mini search item selection dropdown">
                    <input type="hidden" name="country">
                    <i class="dropdown icon"></i>
                    <div class="default text">Select Country</div>
                    <div class="menu">
                        <div class="item" data-value="abc" >
                            <span class="description">2 new</span>
                            <span class="text">Important</span>
                        </div>
                    </div>
                </div>`
};

function AddItemsRow(elem) {
    var $elem = $(elem);
    $('.items.segment').append(

        $('<div/>', { class: "fields", "data-rid": rid + 1 }).append(
            orderDetailsHtml(rid + 1)
        )

    );

    rid = rid + 1;

    $elem.prop("onfocus", null).off("onfocus");

}

function allItems() {
    $.ajax({
        type: "Get",
        dataType :"JSON",
        url: '/items/GetAllItems/',
        success: function (response) {
            Order.items = response;
        }
    });

}

function GetItemDetails(elem) {
    var $elem = $(elem);
    var rid = $elem.parents(".fields").attr('data-rid');


    setTimeout(function () {
        $.ajax({
            type: "POST",
            dataType: "JSON",
            data: { barcode: $elem.val() },
            url: '/InventoryReceiveItems/GetItemDetails/',
            success: function (response) {

                $('[data-rid="' + rid + '"]').find('[name="ItemsList[' + rid + '].ItemId"]').val(response.ItemId);
                $('[data-rid="' + rid + '"]').find('[name="ItemDesc"]').val(response.ItemDesc);
                $('[data-rid="' + rid + '"]').find('[name="UnitPrice"]').val(response.UnitPrice);

                console.log(response);

            }
        });

    }, 0);
    //  console.log($elem.val())
}

function bindItemDropdown(elem) {
    $this = $(elem);
    $('.ui.item.dropdown').remove();

    $('.item.input').css("display", "inline-flex");


    $this.parent().css("display", "none");
    $this.parent().parent().append(Order.itemDropdown);

    $('.ui.item.dropdown').dropdown({
        onChange: function (value, text, $choice) {
            text = $choice.find('.text').html();
            
            $this.val(text);
            $this.next().val(value);
        }

    });

}

orderDetailsHtml = function (rid) {
    detailHtml = `     
            <div class="three wide field">
               
                <div class="ui mini item input">
                    <input type="text" class="itemInput"  name="ItemsList[`+ rid +`].ItemBarcodeNo"  onfocus="AddItemsRow(this)" onpaste="GetItemDetails(this)" onclick="bindItemDropdown(this)" />
                    <input type="hidden" name="ItemsList[`+rid+`].ItemId" />
                </div>
            </div>
            <div class="four wide field">
               
                <div class="ui mini input">
                    <input type="text" name="ItemDesc" readonly />

                </div>
            </div>
            <div class="two wide field">
               
                <div class="ui mini input">
                    <input type="text" name="ItemsList[`+ rid +`].POQty" />
                </div>
            </div>
            <div class="two wide field">
              

                <div class="ui mini input">
                    <select type="text" name="UnitName" readonly></select>
                </div>
            </div>

            <div class="two wide field">
              
                <div class="ui mini input">
                    <input type="text" name="UnitPrice" readonly />
                </div>
            </div>
            <div class="two wide field">
             
                <div class="ui mini input">
                    <input type="text" name="Total" />
                </div>
            </div>
            <div class=" inline-del">
                <i class="feather red icon-trash" onclick="delPOItem(this)" ></i>
            </div>
               
       
    `;
    return detailHtml;
};

delPOItem = function (elem) {
    alert();
};
