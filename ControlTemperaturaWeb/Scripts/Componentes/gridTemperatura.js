; (function ($) {
    $.fn.gridTemperatura = function (method) {

        // plugin's default options
        var defaults = {}

        // this will hold the merged default and user-provided properties
        // you will have to access the plugin's properties through this object!
        // settings.propertyName
        var settings = {
        }

        // public methods can be called as
        // $(selector).gridAtributos('methodName', arg1, arg2, ... argn)
        var methods = {

            // this the constructor method that gets called when
            // the object is created
            init: function (options) {

                // iterate through all the DOM elements we are
                // attaching the plugin to
                return this.each(function () {

                    settings = $.extend({}, defaults, options);
                    $(this).data("settings", settings);
                    $(this).data("data", settings.data);

                    // Toma el id del elemento como identificador principal
                    var idTiles = $(this).attr("id");

                    $(this).addClass("pg-content");

                    // Arma los elementos principales de la grilla
                    var gridTemperatura = "<table id='container" + idTiles + "' class='display table responsive' style='width:100%;' data-effect='bounceInLeft' >"
                        + "</table>";

                    $(this).html(gridTemperatura);
                });
            },

            redraw: function () {
                var idTiles = $(this).attr("id");
                var data = $("#" + idTiles).data("data");
                helpers.redraw(idTiles, data);

                // Anima la visualización
                $("#container" + idTiles).animatePanel();
            },

            getSelectedItem: function () {
                var idTiles = $(this).attr("id");
                return $("#container" + idTiles).DataTable().rows($('.gridTemperaturaItem:checked').parents('td')).data().toArray();
            }

        }

        // private methods
        // private methods can be called as
        // helpers.methodName(arg1, arg2, ... argn)
        var helpers = {
            redraw: function (idTiles, data) {
                $("#container" + idTiles).DataTable({
                    paging: false, info: false, scrollY: '62vh', scrollX: true, language: tableLang, order: [], destroy: true,
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            extend: 'excel',
                            text: '<i class="glyphicon glyphicon-save"></i> Descargar Excel',
                            className: 'hidden exportexcel',
                            filename: 'Control de temperaturas',
                            exportOptions: {
                                format: {
                                    body: function (data, row, column, node) {
                                        var x = '';
                                        if (column == 0) {
                                            if (isNaN(data) && data.indexOf('</span>') != -1) {
                                                x = data.substring(data.indexOf('</span>') + 8, data.length)
                                            }
                                            else {
                                                x = data;
                                            }
                                        } else if (column == 3 || column == 2) {
                                            data = data.replace('<span class="badge  badge-pill badge-success">', "");
                                            data = data.replace('<span class="badge  badge-pill badge-warning">', "");
                                            data = data.replace('<span class="badge  badge-pill badge-danger">', "");
                                            data = data.replace('<span class="badge  badge-pill badge-secondary">', "");
                                            data = data.replace('<span class="badge badge-pill badge-info">', "");
                                            data = data.replace('</span>', "");
                                            x = data;
                                        } else {
                                            x = data;
                                        }
                                     
                                        return x;
                                    }
                                }
                            }
                        }],
                    data: data,
                    columnDefs: [{
                        targets: 0,
                        render: $.fn.dataTable.render.moment('Do MMM YYYY')
                    }],
                    columns: [
                        //{
                        //    data: null, className: 'textoizquierdacabecera', title:  'PROYECTO' , order: true, render: function (data, type, row, meta) {
                        //        return '<spam  style="float: right;padding-right: 14px;;" class="colorAzul text-right"><strong>' + data.PROYECTO +'</strong> </spam>';
                        //    }
                        //},
                        {
                            data: 'fecha', title: 'Fecha Registro', type: 'date', render: function (data, type, row) {
                                return data ? '<span class = "hidden">' + moment(data).format('YYYYMMDD') + '</span> ' + moment(data).format('DD/MM/YYYY') : '';
                            }
                        },
                        { data: 'DNI', title: 'DNI' }, 
                        {
                            data: 'temperatura', title: 'Temperatura', render: function (data, type, row) {
                                var htmlreturn = ''
                                if (data <= 37.5) htmlreturn = '<span class="badge  badge-pill badge-success">' + data + '</span> ';
                                if (data > 37.5 && data <= 39.50) htmlreturn = '<span class="badge  badge-pill badge-warning">' + data + '</span> ';
                                if (data > 39.5) htmlreturn = '<span class="badge  badge-pill badge-danger">' + data + '</span> ';

                                return htmlreturn;
                            }
                        },
                       {
                           data: 'Local', title: 'Tipo Trabajador', render: function (data, type, row) {
                               return data==true ? '<span class="badge  badge-pill badge-secondary">Local</span> ' : '<span class="badge badge-pill badge-info">Externo</span>';
                            }
                        },
                        { data: 'NombreTrabajador', title: 'Nombre Trabajador' },
                        { data: 'Campo', title: 'Campo' },
                        { data: 'UsuarioTomador', title: 'Registrado Por' },
                         
                    ]
                
                     
             
                });
                //helpers.addEvents();
                //$("#containergridTemperatura_wrapper > div:nth-child(2)").prepend('<div class="col-sm-6"></div><div class="col-sm-6" style="padding-top: 10px;"><spam style="float: left;text-align: center;width: 61%;border-bottom: 1px solid #ddd !important;margin-left: -4%;"><strong>Temperatura</strong></spam></div>');

                $("#containergridTemperatura_filter").prepend('<button style="margin-right: 10px;" class="btn btn-success" id="btnDescargarExel" ><i class="glyphicon glyphicon-save"></i>Generar Excel</button>');

            },

            //addEvents: function () {
            //    UtilJs.addCommonListener({
            //        selector: '.gridTemperaturaItem',
            //        callback: function (event) {
            //            var prevState = $(this).prop('checked');
            //            $('.gridTemperaturaItem').prop('checked', false);
            //            $(this).prop('checked', prevState);
            //        }
            //    });
            //}
        }

        // if a method as the given argument exists
        if (methods[method]) {

            // call the respective method
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));

            // if an object is given as method OR nothing is given as argument
        } else if (typeof method === "object" || !method) {

            // call the initialization method
            return methods.init.apply(this, arguments);

            // otherwise
        } else {

            // trigger an error
            $.error('Method "' + method + '" does not exist in gridTemperatura plugin!');
        }
    }
})(jQuery);