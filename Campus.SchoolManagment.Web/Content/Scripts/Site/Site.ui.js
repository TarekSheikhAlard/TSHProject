semantic = {
    init: function () {
        $('.ui.accordion')
            .accordion()
            ;
      


        var $sidebar = $('.ui.sidebar');
        $sidebar.sidebar({
            closable: false,
            dimPage: false,
            transition: "push",
            onShow: function () {
                $('.main.container').css({
                    width: "78%"
                });
                $('table.dataTable ').DataTable().columns.adjust().draw();

            },
            onHide: function () {
          
                $('.main.container').css({
                    width: "auto"
                });
                $('table.dataTable ').DataTable().columns.adjust().draw();

            }
        });


        if (!$sidebar.hasClass('visible')) {
            $sidebar.sidebar('show');

        }

        $('.ui.dropdown')
            .dropdown({
                debug: false,
                match: 'both',
                ignoreCase: true,
                fullTextSearch: true
            })
            ;

     




        $('.ui.popup')
            .popup()
            ;


        $('.pop')
            .popup({
                exclusive: false,
                hoverable: true,
                movePopup: false,
                closable: false,
                inline: false,
                position: "bottom center",
                on: 'hover',
                debug: true



            })
            ;


        $('.ui.modal').modal({
            centered: true,
            closable: false,
            inverted: false,
            blurring: false,
            detachable: true,
            autofocus: false,
            allowMultiple: true,
            transition: 'fade up',
            observeChanges: true,
            onShow: function () {
                setTimeout(function () { $('.ui.dropdown').dropdown(); }, 5)


            }
        });

        $('.ui.account.modal').modal({
            centered: true,
            closable: false,
            allowMultiple: true

        })



        if (!$('.anim').hasClass("animated")) {
            $('.anim').transition({
                animation: 'fade up',
                duration: 700,
                interval: 50,
            });
            $('.anim').addClass("animated");

        }

    },

    clearCacheModals: function () {

        $('.ui.dimmer.modals.page').html("");


    },

    accountQuickBox: function (callback) {

        var $dropdowns = $('.accountSelect.dropdown[data-filter]');

        var $elem, filter;

        $.each($dropdowns, function (index, elem) {


            $elem = $(elem);
            if ($elem.find(".menu").children('div').length === 0) {
                $elem.addClass('loading');

                filter = parseInt($elem.attr('data-filter'));

                $elem.attr('data-index', index);

                $.ajax({
                    type: "Get",
                    url: '/AccountTree/quickAccountTreeList/',
                    data: { id: filter, index },
                    dataType: "JSON",
                    success: function (response) {
                        $('.accountSelect.dropdown[data-index="' + response.index + '"]')
                            .dropdown({
                                values: response.list,
                                clearable: true,
                                placeholder: 'Search',
                                match: 'both',
                                ignoreCase: true,
                                fullTextSearch: true
                            });


                    },
                    complete: function (response) {
                        response = response.responseJSON;
                        $('.accountSelect.dropdown[data-index="' + response.index + '"]')
                            .removeClass('loading');
                        if (typeof callback === 'function') {
                            callback();
                        }
                    }
                });
            }

        });

        $dropdowns = $('.accountSelect.dropdown:not([data-filter])');
        //  $dropdowns = $('.accountSelect.dropdown:not([data-filter]) .menu:not(:has(div.item))').parent();

        console.log($dropdowns);
        $dropdowns.addClass('loading');

        if ($dropdowns.length > 0) {
            $.ajax({
                type: "Get",
                url: '/AccountTree/quickAccountTreeList/',
                dataType: "JSON",
                success: function (response) {

                    $.each($dropdowns, function (index, elem) {
                        if ($(elem).find('.menu').children('div').length === 0) {
                            $(elem)
                                .dropdown({
                                    values: response.list,
                                    clearable: true,
                                    placeholder: 'Search',
                                    match: 'both',
                                    ignoreCase: true,
                                    fullTextSearch: true
                                });
                        }
                    });

                },
                complete: function (response) {
                    $dropdowns
                        .removeClass('loading');

                    if (typeof callback === 'function') {
                        callback();
                    }
                }
            });
        }

    }

};


$(function () {



    //$(window).scroll(function () {

    //    var top = $(window).scrollTop()
    //    if (top > 15) {

    //        $('.accordion.menu').css('top', '0');
    //    }
    //    if (top < 15) {

    //        $('.accordion.menu').css('top', '46px');
    //    }

    //});


    $("a.title.header.animate").click(function () {
        var $this = $(this);
        //console.log($this.next().);
    });


    //$(".mainmenu.accordion.menu div a").click(function () {

    //    var $this = $(this);

    //    //var $elem = $(".mainmenu.accordion.menu div a");

    //    //$elem.removeClass('active')//.parent().removeClass('active')

    //    $this.addClass('active')//.parent().parent().addClass('active');

    //    //console.log(this);

    //    //if ($this.parent().hasClass('content')) {
    //    //    alert();
    //    //    $this.parent().addClass('visible active');
    //    //}


    //})
});



$(document).on('pjax:complete', function (event, data, status, xhr, options) {

    cmps();
});

$(function () {
    cmps();
})

function cmps() {


    var xcmps = getCookie("CampusXLibrel")

    if (xcmps != undefined && xcmps.length > 0) {
        var xlib = JSON.parse(xcmps);
        //var cntrl = getController().toLowerCase();


        console.log(xlib)

        if (xlib.length > 0) {
            xlib.forEach(function (no) {

                $("[data-pid='" + no.PageID + "']").remove();

            });
        }
    }

}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}


function getLanguage() {
    var lang = getCookie("_lang").substring(0, 2)

    if (lang == "" || lang == undefined || lang == null)
        lang = "en"

    if (lang == 'en' || lang == 'ar')
        return lang.toLowerCase()
    else
        return 'en'


}

function getController() {
    var location = window.location.pathname;
    var controller = location.split('/')[1];
    return controller
}
function getAction() {
    var location = window.location.pathname;
    var action = location.split('/')[2];
    return action
}
function readImgURL(input, el) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        var $el = $(el);
        reader.onload = function (e) {
            $el.attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}



function Language(lang) {

    var __construct = function () {
        if (eval('typeof ' + lang) == 'undefined') {
            lang = "en";
        }
        return;
    }()

    this.getStr = function (str, defaultStr) {
        defaultStr = "No Translation"
        var retStr = eval('eval(lang).' + str);

        if (typeof retStr != 'undefined') {
            return retStr;
        } else {
            if (typeof defaultStr != 'undefined') {
                return defaultStr;
            } else {
                return eval('en.' + str);
            }
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