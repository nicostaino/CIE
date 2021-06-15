var UtilJs = UtilJs ||
{
    //Variable en que deberá almacenarse el redirect para su envío en cada ajax
    redirect: '',

    //Muestra es un modal de bootstrap utilizando el BootstrapDialog
    showBootstrapModal: function (settings) {
        var options = {
            type: settings.type || BootstrapDialog.TYPE_DEFAULT,
            size: settings.size || BootstrapDialog.SIZE_NORMAL,
            message: '<div style="margin:10px;">' + settings.message + '</div>',
            title: settings.title
        };
        if (settings.buttons != undefined)
            if (settings.buttons.length === 0) {
                options.buttons = [
                    {
                        label:  'ACEPTAR' ,
                        cssClass: 'btn-primary',
                        action: function (dialogItself) {
                            dialogItself.close();
                        }
                    }
                ];
            } else {
                options.buttons = settings.buttons;
            }
        if (settings.onShown != undefined)
            options.onshown = settings.onShown;
        if (settings.onHidden != undefined)
            options.onhidden = settings.onHidden;
        BootstrapDialog.show(options);
    },

    // Bloquea la pantalla y muestra un mensaje
    // Solo se puede desbloquear con la función $.unblockUI
    blockScreen: function (mensaje) {
        mensaje = mensaje ||  'Procesando'  + '...';
        $.blockUI({
            message: mensaje,
            baseZ: 50000,
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                color: '#ffffff',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            }
        });
    },

    //Invoca un plugin y llama al método que lo redibuja
    pluginConfig: function (containerId, plugin, data, method) {
        containerId = containerId.charAt(0) === '#' ? containerId : '#' + containerId;
        $(containerId)[plugin]({
            data: data
        });
        method = method || 'redraw';
        $(containerId)[plugin](method);
    },

    //Permite conocer si un elemento está sin definir
    isUndefined: function (obj) {
        return obj == undefined;
    },

    //Para conocer si un elemento html no tiene contenido
    isEmptyHtml: function (el) {
        return !$.trim($(el).html());
    },

    //Para conocer si un obj está vacío
    isEmptyObj: function (obj) {
        return $.isEmptyObject(obj);
    },

    //Devuelve si una cadena está vacía o sólo contiene espacios
    isEmptyStr: function (str) {
        if (typeof str === 'string')
            return str === '';
        return false;
    },

    //Devuelve si un objeto es null
    isNullObj: function (obj) {
        return obj === null;
    },

    //Permite conocer si una cadena está sin definir, vacía o null
    isNullOrEmptyStr: function (str) {
        if (typeof str === 'string')
            return UtilJs.isNullObj(str) || str === 'null' || UtilJs.isEmptyStr(str);
        return UtilJs.isUndefined(str);
    },

    //Repite una cadena de texto un determinado número de veces
    repeatString: function (str, times) {
        return (new Array(times + 1)).join(str);
    },

    //Agrega un evento (click por defecto) a un contenedor (body) por defecto. Evita duplicar el evento
    addCommonListener: function (listener) {
        listener.event = listener.event || 'click';
        var container = listener.container ? $(listener.container) : $(document.body);
        if (listener.container) {
            container.off(listener.event);
            container.on(listener.event, listener.callback);
        } else {
            container.off(listener.event, listener.selector);
            container.on(listener.event, listener.selector, listener.callback);
        }
    },

    //Función que retorna el evento soportado por el navegador utilizado para suscribirse al finalizar una animación
    TransitionEvent: function () {
        var t, el = document.createElement('element');
        for (t in transitions) {
            if (el.style[t] !== undefined) {
                return transitions[t];
            }
        }
    },

    //Agrega un evento el cual en su callback se suscribe a una transición para cuando termine ejecutar determinado callback
    addTransitionListener: function (listener) {
        var opts = {
            event: listener.event || 'click',
            callback: function () {
                UtilJs.addCommonListener({
                    event: listener.animationEvent || UtilJs.TransitionEvent(),
                    container: listener.animationContainer,
                    callback: listener.animationCallback
                });
            }
        };
        if (listener.selector)
            opts.selector = listener.selector;
        else if (listener.container)
            opts.container = listener.container;
        UtilJs.addCommonListener(opts);
    },

    //Elimina un evento (click por defecto) a un contenedor (body) por defecto. Evita duplicar el evento
    removeCommonListener: function (listener) {
        listener.event = listener.event || 'click';
        var container = listener.container ? $(listener.container) : $(document.body);
        container.off(listener.event, listener.selector);
    },

    //Convierte el valor a entero, si el valor no es un número devuelve false.
    toInt: function (value) {
        if (isNaN(value)) {
            return false;
        }
        var x = parseFloat(value);
        var flag = (x | 0) === x;
        return !flag ? false : x;
    },

    //Determina si un valor es entero
    isInt: function (value) {
        return !isNaN(value) && data === parseInt(value, 10);
    },

    //Obtener la parte numérica mayor que cero de una cadena de texto
    getIntFromStr: function (str, base) {
        var strNum = str.replace(/[^\d.]/g, '');
        var result = 0;
        if (!UtilJs.isEmptyStr(strNum) && !isNaN(strNum))
            result = parseInt(strNum, base || 10);
        return result;
    },

    //Convierte un string a entero pudiendo especificar que una base. Retorna cero en caso de no ser un entero válido
    getInt: function (str, base) {
        var result = parseInt(str, base || 10);
        return !isNaN(result) ? result : 0;
    },

    //Comprueba si una cadena de texto termina en una subcadena
    endsWith: function (origin, target) {
        return (origin.substr(target.length * -1, target.length) === target);
    },

    //Obtener un objeto con el alto y ancho de la ventana.
    viewport: function () {
        var e = window
            , a = 'inner';
        if (!('innerWidth' in window)) {
            a = 'client';
            e = document.documentElement || document.body;
        }
        return { width: e[a + 'Width'], height: e[a + 'Height'] }
    },

    //Ajusta el ancho de las columnas de un datatable si se especifica su id o de todos los que se encuentren visibles
    adjustDataTableWidth: function (selectorId) {
        if (undefined !== selectorId && typeof selectorId === 'string') {
            selectorId = selectorId.charAt(0) === '#' ? selectorId : '#' + selectorId;
            $(selectorId).DataTable().columns.adjust();
        }
        else $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    },

    //Determina si el ancho es mayor que alto del viewport
    isLandscape: function () {
        var vp = UtilJs.viewport();
        return vp.width > vp.height;
    },

    //Abre una url en un tab nuevo
    handleUrl: function (url) {
        fwkOperationsHelper.WindowOpen('', url);
    },

    //Adiciona valores a un select2.
    fillSelect2: function (select, data, populateFunc, select2Options, defaultVal, doTrigger,selectFirstBydefault) {
        var $select = $(select);
        $select.empty();
        if (selectFirstBydefault == undefined || !selectFirstBydefault) {
            $select.append('<option></option>');
        }
   
        $.each(data,
            function (index, item) {
                var $option = populateFunc($('<option />'), item);
                $select.append($option);
            });
        var defOptions = {
            language: 'es'
        };
        var settings = typeof select2Options === 'object' ? $.extend({}, defOptions, select2Options) : defOptions;
        $select.select2(settings);

        if (undefined !== defaultVal && !UtilJs.isNullOrEmptyStr(defaultVal)) {
            $select.select2('val', defaultVal);
        }
        doTrigger = undefined === doTrigger ? true : doTrigger;
        if (doTrigger) {
            $select.trigger('change');
        }
    },

    /***
   * Wraper for Ajax Requests in Traces
   * @param {} request object. Mandatory url and success function 
   * @returns {} 
   */
    execAjaxTrace: function (request) {
        var blockScreen = request.blockScreen == undefined ? true : request.blockScreen;
        request.message = request.message ||  'Ejecutando'  + '...';
        request.type = request.type || httpMethod.Post;

        var stringify = request.stringify ? request.stringify : request.type === httpMethod.Post ? true : false;
        var data = request.data || {};
        data.Empresa =  localStorage.currentCompnay;
         
        var options = {
            url: request.url,
            type: request.type,
            data: stringify ? JSON.stringify(data) : data
        };
        if (request.contentType) options.contentType = request.contentType;
        if (request.dataType) options.dataType = request.dataType;
        if (request.cache) options.cache = request.cache;
        if (request.timeout) options.timeout = request.timeout;

        var clearTrace = undefined != request.clearTrace ? request.clearTrace : false;
        if (blockScreen)
            UtilJs.blockScreen(request.message);
        MDSJsUtil.ajax(options,
            function (response) {
                //success
                request.success.call(this, response.Data);
            },
            function (codeError, messageError, detailError) {
                //error
                if (codeError !== 0 && messageError !== defaultStr.Timeout) {
                    if (undefined != request.errorBE) {
                        request.errorBE.call(this, codeError, messageError, detailError);
                    } else {
                         var errorText = !messageError || UtilJs.isEmptyStr(messageError)
                            ? 'Hubo un problema al actualizar la operación.'
                            : messageError;
                         ErrorDialogShow(errorText,  'Aviso' , 'ACEPTAR' , detailError);
                    }
                } else {
                    if (request.timeoutError)
                        request.timeoutError.call(this, codeError, messageError, detailError);
                }
            },
            //vaciar el trace
            clearTrace
        );
    },
    handleUrlApp: function(e) {
            var self = $(e);
var url = self.attr("data-href");
if (url !== undefined) {
    fwkOperationsHelper.WindowOpen("", url);
}
return false;
    },

    mostrarMensaje: function (icono, titulo, texto, ) {
        swal({
            icon: icono,
            title: titulo,
            text: texto,
            buttons: {
                confirm: {
                    text: "ACEPTAR",
                    value: true,
                    visible: true,
                    className: "",
                    closeModal: true,
                    focus: false

                }
            },

        });
    }
}