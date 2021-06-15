var MDSJsUtil = (function ($) {
    var defaults = {
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        cache: false,
        timeout: 60000
    };

    var traces = [];

    function addTrace(trace) {
        traces.push(trace);
    }

    function clearTrace(resetTrace) {
        if (!(resetTrace == false))
            traces = [];
    }

    function printTrace() {
        try {
            var $traceInfo = $('#traceInfo');
            if ($traceInfo.length === 0) {
                var htmlTrace = '<div class="modal fade" id="traceInfo" tabindex="-1" role="dialog" aria-hidden="true">'
                    + '<div class="modal-dialog modal-lg">'
                    + '<div class="modal-content">'
                    + '<div class="color-line"></div>'
                    + '<div class="modal-header">'
                    + '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>'
                    + '<h4 class="modal-title modal-title-md">Trace</h4>'
                    + '</div>'
                    + '<div class="modal-body">'
                    + '</div>'
                    + '<div class="modal-footer">'
                    + '<button type="button" class="btn btn-default" data-dismiss="modal">CERRAR</button>'
                    + '</div>'
                    + '</div>'
                    + '</div>'
                    + '</div>';
                $(htmlTrace).appendTo('body');
                $traceInfo = $('#traceInfo');
            }

            if (traces.length > 0) {
                var transactions = [];
                var dalTime = 0;
                var txtime = 0;
                var computer = false;
                var webTime = 0;
                var tracesCant = traces.length;
                for (var i = 0; i < tracesCant; i++) {
                    var trace = traces[i];
                    if (trace) {
                        dalTime += parseInt(trace.DalTime);
                        txtime += parseInt(trace.TxTime);
                        computer = computer || trace.Server;
                        webTime += parseInt(trace.WebTime);
                        var tranCant = trace.Transactions.length;
                        for (var k = 0; k < tranCant; k++) {
                            transactions.push(trace.Transactions[k]);
                        }
                    }
                }

                var htmlTrans = '<label>- Transacciones</label><div class="table-responsive">'
                    + '<table cellpadding="1" cellspacing="1" class="table">'
                    + '<thead>'
                    + '<tr>'
                    + '<th>Nombre</th>'
                    + '<th style="text-align: right;">Backend Time</th>'
                    //+ '<th>DalTime</th>'
                    + '</tr>'
                    + '</thead>'
                    + '<tbody>';
                var transCant = transactions.length;
                for (var j = 0; j < transCant; j++) {
                    var t = transactions[j];
                    htmlTrans += '<tr>'
                        + '<td>' + t.Name + '</td>'
                        + '<td style="text-align: right;">' + t.BackendTime + 'ms</td>'
                        //+ '<td>' + t.DalTime + 'ms</td>'
                        + '</tr>';
                }

                htmlTrans += '</tbody></table></div>';
                $traceInfo.find('.modal-body').html(htmlTrans);
            } else {
                $traceInfo.find('.modal-body').html('');
            }
            $traceInfo.modal('show');
        } catch (e) {
            return;
        }
    }

    function ajaxPromise(options) {
        var settings = $.extend({}, defaults, options);
        return $.ajax(settings);
    }

    function blockUI() {
        if (!isUIBlocked()) {
            $.blockUI({ message: '<h5>Procesando...</h5>', baseZ: 50000 });
        }
    }

    function isUIBlocked() {
        return typeof $(window).data() == "object" && $(window).data()["blockUI.isBlocked"] == 1;
    }

    function checkPendingRequest() {
        if ($.active > 0) {
            window.setTimeout(checkPendingRequest, 500);
        }
        else {
            if (isUIBlocked()) {
                setTimeout(function () { $.unblockUI(); }, 500);
            }
        }
    }

    function unblockUI() {
        checkPendingRequest();
    }

    function genericMessageError(e) {
        MDSMessageUtil.ErrorDialogShow("Ocurrió un error al realizar la acción.", "Aviso", "Aceptar", e);
    }

    function baseAjaxFail(xhr, textStatus, errorThrown, errorCallback) {
        unblockUI();
        if (xhr.readyState === 0 || textStatus === "timeout") {
            MDSMessageUtil.ErrorDialogShow("Se interrumpió la conexión con el servidor. \n\n Por favor intente nuevamente.", ":::Aviso:::", "Aceptar", errorThrown);
        }

        if (typeof errorCallback === 'function') {
            errorCallback.call(this, xhr.readyState, textStatus, errorThrown);
        } else {
            genericMessageError(errorThrown);
        }
    }

    function baseAjax(options, successCallback, errorCallback) {
        var jqxhr = ajaxPromise(options);
        jqxhr.done(function (response) {
            try {
                //Luciano agregar validacion de que el usuario sea el indicado para el complemento 
                if (false && IsCredentialException(response)) {
                    ShowCredentialExceptionDialog(response, function () {
                        document.location = document.location;
                    });
                } else {
                    //el trace siempre debe venir ya sea exito o error
                    if (response && response.hasOwnProperty('Trace') && response.Trace)
                        addTrace(response.Trace);

                    if (response && response.Code != undefined && response.Code != 0) {
                        unblockUI();
                        if (typeof errorCallback === 'function') {
                            errorCallback.call(this, response.Code, response.ErrorMessage, response.ErrorDetail);
                        }
                    }
                    else {
                        if (typeof successCallback === 'function') {
                            $.when(successCallback.call(this, response)).done(function () {
                                unblockUI();
                            });
                        }
                    }
                }
            } catch (e) {
                unblockUI();
                genericMessageError(MDSMessageUtil.stacktrace(e));
                return false;
            }
        });

        jqxhr.fail(function (xhr, textStatus, errorThrown) {
            baseAjaxFail(xhr, textStatus, errorThrown, errorCallback);
        });
    }

    function baseHtmlAjax(options, successCallback, errorCallback) {
        if (!options.hasOwnProperty('dataType'))
            options.dataType = "html";
        var jqxhr = ajaxPromise(options);
        jqxhr.done(function (response, textStatus, request) {
            try {
                var response_json = "";
                try {
                    response_json = $.parseJSON(response);
                } catch (e) {
                    //no hacer nada aqui
                }
                if (IsCredentialException(response_json)) {
                    ShowCredentialExceptionDialog(response_json, function () {
                        document.location = document.location;
                    });
                } else {
                    if (response_json == "" && typeof successCallback === 'function') {
                        //cuando la respuesta es HTML el Trace viene en el header
                        if (request.getResponseHeader('Trace'))
                            addTrace($.parseJSON(request.getResponseHeader('Trace')));

                        $.when(successCallback.call(this, response)).done(function () {
                            unblockUI();
                        });
                    }
                    else {
                        //el trace viene en el json de error
                        if (response_json && response_json.hasOwnProperty('Trace') && response_json.Trace)
                            addTrace(response_json.Trace);

                        unblockUI();
                        if (typeof errorCallback === 'function') {
                            errorCallback.call(this, response_json.Code, response_json.ErrorMessage, response_json.ErrorDetail);
                       }
                    }
                }
            } catch (e2) {
                unblockUI();
                genericMessageError(MDSMessageUtil.stacktrace(e2));
                return false;
            }
        });

        jqxhr.fail(function (xhr, textStatus, errorThrown) {
            baseAjaxFail(xhr, textStatus, errorThrown, errorCallback);
        });
    }

    return {
        addTraces: function (traces) {
            addTrace(traces);
        },
        chainedAjax: function (options) {
            return ajaxPromise(options);
        },
        initTrace: function (traceInfoSelector) {
            $(traceInfoSelector).on('click', function () {
                printTrace();
            });
        },
        ajax: function (options, successCallback, errorCallback, resetTrace) {
            clearTrace(resetTrace);
            baseAjax(options, successCallback, errorCallback);
        },
        htmlAjax: function (options, successCallback, errorCallback, resetTrace) {
            clearTrace(resetTrace);
            baseHtmlAjax(options, successCallback, errorCallback);
        },
        parallelAjax: function (options, globalSuccessCallback, globalErrorCallback, resetTrace) {
            clearTrace(resetTrace);
            if (typeof async !== 'undefined') {
                try {
                    if (typeof options === "object") {
                        var parallelObject = {};
                        $.each(options, function (key, value) {
                            if (value && typeof value === "object") {
                                var ajaxOptions = {};

                                if (value.hasOwnProperty('ajaxOptions')) {
                                    ajaxOptions = value.ajaxOptions;
                                }

                                parallelObject[key] = function (callback) {
                                    var funcAjax = baseAjax;
                                    if (ajaxOptions.hasOwnProperty('dataType') && ajaxOptions.dataType.toLowerCase() == 'html')
                                        funcAjax = baseHtmlAjax;

                                    funcAjax(ajaxOptions,
                                        function (response) {
                                            var success = {};
                                            success[key] = {
                                                Response: response
                                            };
                                            return callback(null, success);
                                        },
                                        function (codeError, messageError, detailError) {
                                            var error = {};
                                            error[key] = {
                                                CodeError: codeError,
                                                MessageError: messageError,
                                                DetailError: detailError
                                            }
                                            return callback(codeError, error);
                                        });
                                }
                            }
                        });
                        async.parallel(parallelObject,
                            function (error, result) {
                                if (error) {
                                    unblockUI();
                                    if (typeof globalErrorCallback == 'function') {
                                        var errors = {};
                                        $.each(result, function (key, value) {
                                            errors[key] = value[key];
                                        });
                                        globalErrorCallback.call(this, error, errors);
                                    } else {
                                        genericMessageError(error);
                                    }

                                } else {
                                    if (typeof globalSuccessCallback == 'function') {
                                        var responses = {};
                                        $.each(result, function (key, value) {
                                            responses[key] = value[key].Response;
                                        });
                                        $.when(globalSuccessCallback.call(this, responses)).done(function () {
                                            unblockUI();
                                        });
                                    }
                                }
                            });
                    }
                } catch (e) {
                    unblockUI();
                    genericMessageError(MDSMessageUtil.stacktrace(e));
                    return false;
                }
            }
        },
        isUIBlocked: function () {
            return isUIBlocked();
        },
        blockUI: function () {
            blockUI();
        },
        unblockUI: function () {
            unblockUI();
        }
    };
})(jQuery);

