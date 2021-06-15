var MDSMessageUtil = (function ($) {
    //funciones privadas
    function togglerVerDetalle(self) {
        var container = $(self).parent('div').next('.detail-container');
        container.toggle();
    }

    function BootstrapDialogShow(message, titleText, buttonText) {
        titleText = (titleText == undefined) ? 'Web Service' : titleText;
        buttonText = (buttonText == undefined) ? 'Aceptar' : buttonText;

        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_DANGER,
            title: '::: ' + titleText + ' :::',
            message: message,
            buttons: [{
                label: buttonText,
                cssClass: 'btn-primary',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }],
        });
    }

    function ErrorDialogShow(message, titleText, buttonText, detail) {
        titleText = (titleText == undefined) ? 'Desktop Inspector' : titleText;
        buttonText = (buttonText == undefined) ? 'ACEPTAR' : buttonText.toUpperCase();

        var htmlMessage = "<div>"
            + "<div>" + message + "</div><br>"
            + "<div><a href='#' onclick='MDSMessageUtil.togglerVerDetalle(this);return false;'>Ver Detalle</a></div>"
            + "<div class='detail-container' style='display:none; overflow-x:scroll; '><p>" + detail + "</p></div>"
            + "</div>";


        BootstrapDialog.show({
            type: BootstrapDialog.TYPE_WARNING,
            title: '::: ' + titleText + ' :::',
            message: htmlMessage,
            buttons: [{
                label: buttonText,
                cssClass: 'btn-primary',
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }]
        });
    }

    function navegationApps() {
        $(document).on('click', '.url-apps', function () {
            var url = $(this).attr('data-href');
            fwkOperationsHelper.WindowOpen("", url);
        });
    }

    function closeRightSideBarOnBlur() {
        $('body').on('click touchstart', function (e) {
            try {
                var target = e.target;
                if ($(target).closest('.right-sidebar-toggle').length == 0) {
                    if ($('#right-sidebar').hasClass('sidebar-open')) {
                        $('#right-sidebar').click();
                    }
                }
            } catch (e) {

            }
        });
        //este codigo es solo para el iframe
        $("iframe").contents().on('click touchstart', function (e) {
            try {
                if ($('#right-sidebar').hasClass('sidebar-open')) {
                    $('#right-sidebar').click();
                }
            } catch (e) {

            }
        });
    }

    function initBootstrapMultimodal() {
        $('.modal').on('show.bs.modal', function (event) {
            var idx = $('.modal:visible').length;
            $(this).css('z-index', 1040 + (10 * idx));
        });
        $('.modal').on('shown.bs.modal', function (event) {
            var idx = ($('.modal:visible').length) - 1; // raise backdrop after animation.
            $('.modal-backdrop').not('.stacked').css('z-index', 1039 + (10 * idx));
            $('.modal-backdrop').not('.stacked').addClass('stacked');
        });
    }

    function padLeftZero(str) {
        var pad = "000000000000000000";
        return pad.substring(0, pad.length - str.length) + str;
    }

    function removeLeftZero(str) {
        return str.replace(/\b0+/g, '');
    }

    function text_truncate(text, length, sufix) {
        text = $.trim(text);

        if (text.Length <= length)
            return text;
        return text.substring(0, length) + sufix;
    }

    function stacktrace(error) {
        if (error && typeof error == "object" && error.hasOwnProperty('stack'))
            return error.stack;
        return error;
    }

    //funciones públicas
    return {
        Init: function() {
            initBootstrapMultimodal();
        },
        BootstrapDialogShow: function (message, titleText, buttonText) {
            BootstrapDialogShow(message, titleText, buttonText);
        },
        ErrorDialogShow: function(message, titleText, buttonText, detail) {
            ErrorDialogShow(message, titleText, buttonText, detail);
        },
        navegationApps: function() {
            navegationApps();
        },
        closeRightSideBarOnBlur: function() {
            closeRightSideBarOnBlur();
        },
        padLeftZero: function(str) {
            return padLeftZero(str);
        },
        removeLeftZero: function(str) {
            return removeLeftZero(str);
        },
        text_truncate: function(text, length, sufix) {
            return text_truncate(text, length, sufix);
        },
        trim: function(str) {
            return $.trim(str);
        },
        stacktrace: function (jsError) {
            return stacktrace(jsError);
        },
        togglerVerDetalle: function(self) {
            togglerVerDetalle(self);
        }
    };
})(jQuery);
//inicia el módulo
MDSMessageUtil.Init();
