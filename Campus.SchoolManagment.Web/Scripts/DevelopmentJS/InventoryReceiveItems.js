$(function () {

    $('.receive.menu').tab()

   
})
var rid = 2

function AddItemsRow(elem) {
    var $elem = $(elem);
    $('.items.segment').append(

        $('<div/>', { class: "fields","data-rid":rid+1 }).append(
            rowHtml
        )

    )
    rid = rid + 1;

    $elem.prop("onfocus", null).off("onfocus");

}


function GetItemDetails(elem) {
    var $elem = $(elem);
    var rid = $elem.parents(".fields").attr('data-rid');
    var id = parseInt(rid)-1
    
    setTimeout(function () {
        $.ajax({
            type: "POST",
            dataType:"JSON",
            data: { barcode:$elem.val() },
            url: '/InventoryReceiveItems/GetItemDetails/',
            success: function (response) {
               
                $('[data-rid="' + rid + '"]').find('[name="ItemsList['+ id+'].ItemId"]').val(response.ItemId);
                $('[data-rid="' + rid + '"]').find('[name="ItemDesc"]').val(response.ItemDesc);
                $('[data-rid="' + rid + '"]').find('[name="UnitPrice"]').val(response.UnitPrice);

                console.log(response)
               
            }
        });
     
    }, 0);
  //  console.log($elem.val())
}


var rowHtml = `
        <div class="three wide field">    
            <div class="ui mini input">
                <input type ="text" name="ItemBarcode" onfocus="AddItemsRow(this)" onpaste="GetItemDetails(this)" />
             <input type="hidden" name="ItemsList[0].ItemId" />
            </div>
        </div>
        <div class="four wide field">         
            <div class="ui mini input">
                <input type="text" name="ItemDesc"  readonly />
               
            </div>
        </div>
        <div class="two wide field">          
            <div class="ui mini input">
                <input type="text" name="UnitNo"  />
            </div>
        </div>
        <div class="two wide field">

            <div class="ui mini input">
                <input type="text" name="UnitName" readonly />
            </div>
        </div>
        <div class="two wide field">
            
            <div class="ui mini input">
                <input type="text" name="POQty" />             
            </div>
        </div>
        <div class="two wide field">
           
            <div class="ui mini input">
                <input type="text" name="ItemsList[0].ReceivedQty" />
            </div>
        </div>
      <div class="two wide field">
           
            <div class="ui mini input">
                <input type="text" name="UnitPrice" readonly />
            </div>
        </div>
        <div class="two wide field">
            
            <div class="ui mini input">
              
                    <input type="text" name="ItemsList[0].Discount" />
            
            </div>
        </div>
        <div class="two wide field">  
            <div class="ui mini input">
                    <input type="text" name="Total" />
            </div>
        </div>
    `

