PowerTable= {
    quotation: {
        thead: {
            html:` <th>#</th>
                    <th></th>
                    <th class="four wide">{0}</th>
                    <th class="six wide">{1}</th>
                    <th class="two wide">{2}</th>
                    <th class="two wide">{3}</th>
                    <th class="two wide">{4}</th>`,
            create: function (data) {

               var _Culture = getLanguage();
               var thead= PowerTable.quotation.thead.html;

                theadRes = new Language(_Culture + ".thead");

                if (data !== undefined) {
                    var d;
                } else {
                    thead= thead.format(theadRes.getStr("item"), theadRes.getStr("desc"),
                        theadRes.getStr("qty"), theadRes.getStr("unitPrice"), theadRes.getStr("amount"))

                    console.log(thead);
                };
                return thead;
            }
        },
        tbody: {
            html: ` <tr data-rowid="{0}" ><td data-name="srno">{1}</td>
                    <td data-name="del"><i class="feather red icon-trash" onclick="RemoveRow(this);"></i></td>
                    <td data-name="item"><div class="ui small fluid item  input ">
                            <input type="text" name="ItemName" onclick="bindItemDropdown(this)" onfocus="AddItemsRow(this)">
                            <input type="hidden" name="ItemsList[{0}].ItemId">
                        </div></td>
                    <td data-name="desc"><div class="ui small fluid input"><input type="text" name="ItemsList[{0}].Description"></div></td>
                    <td data-name="qty"><div class="ui small fluid input"><input type="number" name="ItemsList[{0}].Qty"></div></td>
                    <td data-name="unitPrice"><div class="ui small fluid input"><input type="number" name="ItemsList[{0}].UnitPrice"></div></td>
                    <td data-name="amount"><text name="ItemsList[{0}].amount"></text></td>
                    </tr>`,
            create: function (data) {
                var _Culture = getLanguage();
                var tbody = PowerTable.quotation.tbody.html;

                PowerTable.rownum +=  1;

                if (data !== undefined) {
                    var d;
                } else {
                    tbody = tbody.format(PowerTable.rownum, PowerTable.rownum+1); 
                    
                  
                }             
                return tbody;
            }


        }
    },
    feePayment: {
        thead: {
            html: ` <th>#</th>
                    <th></th>
                    <th class="four wide">{0}</th>
                    <th class="four wide">{1}</th>
                    <th class="two wide">{2}</th>
                    <th class="two wide">{3}</th>
                    <th class="two wide">{4}</th>
                   <th class="two wide">{4}</th>`,
            create: function (data) {

                var _Culture = getLanguage();
                var thead = PowerTable.feePayment.thead.html;

                theadRes = new Language(_Culture + ".feePayment.thead");

                if (data !== undefined) {
                    var d;
                } else {
                    thead = thead.format(
                        theadRes.getStr("feeType"),
                        theadRes.getStr("paymentType"),
                        theadRes.getStr("orginalAmount"),
                        theadRes.getStr("paidAmount"),
                        theadRes.getStr("istallments")
                    )
                    console.log(thead);
                };
                return thead;
            }
        },

    },

    rownum: 1,
    itemDropdown: {
        html: `<div class="ui small search fluid item selection dropdown">
                    <input type="hidden" name="country">
                    <i class="dropdown icon"></i>
                    <div class="default text">Select Country</div>       
                </div>`,
        get:function() {
            $.ajax({
                type: "Get",
                dataType:"JSON",
                url: '/Items/GetAllItems/',
                success: function (response) {
                    PowerTable.itemDropdown.data=response;
                }
            });


        },
        data: [],
        create: function () {
            return PowerTable.itemDropdown.html;
        },
        bind: function (elem) {
            $this = $(elem);

            $('.ui.item.dropdown').remove();

            $('.item.input').css("display", "flex");
  
            $this.parent().css("display", "none");

            $this.parent().parent().append(PowerTable.itemDropdown.html);

            $('.ui.item.dropdown').dropdown({
                values: PowerTable.itemDropdown.data,
                placeholder:"Select a Item",
                onChange: function (value, text, $choice) {
                   // text = $choice.find('.text').html();
                    console.log(text);
                    $this.val(text);
                    $this.next().val(value);
                }

            }).dropdown('set selected', $this.next().val());
        }

    },
    AddRow: function (elem) {
        $elem = $(elem);
        var $table = $elem.parents("table");
        var form=$table.attr("data-form-type");

        switch (form) {
            case "Quotation":
               $table.find('tbody').append(PowerTable.quotation.tbody.create());
                break;
            case"":
                break;
            default:
                break;
        }
        $elem.prop("onfocus", null).off("onfocus");
    },
    RemoveRow: function () {


    },
    Create: function ($table) {
        var type = $table.attr('data-form-type')


        switch (type) {
            case "Quotation":
               // $table.find('tbody').append(PowerTable.quotation.tbody.create());
                break;
            case "FeePayment":
                $table.find('thead tr').html(PowerTable.feePayment.thead.create())
                break;
            default:
                break;
        }
    }


}




String.prototype.format = String.prototype.format ||
    function () {
        "use strict";
        var str = this.toString();
        if (arguments.length) {
            var t = typeof arguments[0];
            var key;
            var args = ("string" === t || "number" === t) ?
                Array.prototype.slice.call(arguments)
                : arguments[0];

            for (key in args) {
                str = str.replace(new RegExp("\\{" + key + "\\}", "gi"), args[key]);
            }
        }

        return str;
    };