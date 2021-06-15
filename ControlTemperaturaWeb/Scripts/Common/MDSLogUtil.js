var MDSLogUtil = (function ($) {
    var actions = {
        Debug: 'Debug',
        Info: 'Info',
        Warn: 'Warn',
        Error: 'Error'
    };
    var defaults = {
        string: '[]',
        number: '-1'
    };

    var logMessage = function(message, action) {
        $.ajax({
            type: 'POST',
            url: SitePath + 'Error/LogMsg',
            data: JSON.stringify({
                "message": message,
                "action": action
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false
        });
    };

    var init = function() {
        if (window && !window.onerror) {
            window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {
                var msg = "Ha ocurrido un error inesperado: " +
                    errorMsg +
                    " en la dirección: " +
                    (url || defaults.string) +
                    ". Línea: " +
                    (lineNumber || defaults.number) +
                    ". Columna: " +
                    (column || defaults.number) +
                    ". Detalle: " + 
                    (errorObj && errorObj.stack ? errorObj.stack : defaults.string) +
                    ".";
                logMessage(msg, actions.Error);
                return false;
            }
        }
    };

    return {
        init: init,
        error: function(message) {
            logMessage(message, actions.Error);
        },
        warn: function(message) {
            logMessage(message, actions.Warn);
        },
        info: function(message) {
            logMessage(message, actions.Info);
        },
        debug: function(message) {
            logMessage(message, actions.Debug);
        }
    };
})(jQuery);
