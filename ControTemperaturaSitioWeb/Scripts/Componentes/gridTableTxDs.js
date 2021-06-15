
; (function ($) {

    $.fn.gridTableTxDs = function (method) {

        // plugin's default options
        var defaults = {}

        // this will hold the merged default and user-provided properties
        // you will have to access the plugin's properties through this object!
        // settings.propertyName
        var settings = {

        }

        // public methods can be called as
        // $(selector).gridTableTxDs('methodName', arg1, arg2, ... argn)
        var methods = {

            // this the constructor method that gets called when
            // the object is created
            init: function (options) {

                // iterate through all the DOM elements we are
                // attaching the plugin to
                return this.each(function () {

                    // Junta los defaults con las options y los guarda en settings
                    settings = $.extend({}, defaults, options);
                    $(this).data("settings", settings);

                    // Crea una Lista para todas las definiciones de columnas
                    $(this).data("columns", settings.columns);

                    // Crea una Lista para todas las filas
                    var rows = new Array();
                    $(this).data("rows", rows);

                    // Toma el id del elemento como identificador principal
                    var idGrid = $(this).attr('id');

                    var tableStructureString = "<div id='mainDivGrilla" + idGrid + "' style='padding:16px;' ></div>";

                    $(this).html(tableStructureString);
                });
            },

            agregarFila: function (rowData, idrow) {
                var idGrid = $(this).attr('id');
                var rows = $('#' + idGrid).data("rows");
                rowData.IdRow = idrow;
                rows.push(rowData);
            },

            redibujar: function () {

                var idGrid = $(this).attr('id');
                var rows = $('#' + idGrid).data("rows");
                var columns = $('#' + idGrid).data("columns");
                return helpers.redraw(idGrid, columns, rows);
            },

            eliminarTodasLasFilas: function () {
                var idGrid = $(this).attr('id');
            },

        }

        // private methods
        // private methods can be called as
        // helpers.methodName(arg1, arg2, ... argn)
        var helpers = {
            redraw: function (idGrid, columns, rows) {

                var settings = $('#' + idGrid).data("settings");

                var htmlTable = '<table id="datatatable_' + idGrid + '" class="display table responsive" width="100%">'
                    + '<thead>'
                    + '<tr>';

                if (columns != undefined) {

                    for (var colidx in columns) {
                        var column = columns[colidx];

                        if (column.type == 'semaphore') {
                            htmlTable += '<th class="grid-rotulo" id="celdaBarraColor">' + column.name + '</th>';
                        } else {
                            htmlTable += '<th class="grid-rotulo">' + column.name + '</th>';
                        }

                    }
                }

                htmlTable += '</tr></thead><tbody>';

                for (var ci in rows) {
                    var row = rows[ci];

                    htmlTable += '<tr data-idrow="' + row.IdRow + '">';

                    if (columns != undefined) {

                        for (var colidx in columns) {
                            var column = columns[colidx];

                            if (column.type == 'semaphore') {
                                htmlTable += '<td><div class="grid-semaforo ' + row[colidx] + '">&nbsp;</div></td>';
                            } else {

                                if (column.highlighted != undefined && column.highlighted == true) {
                                    htmlTable += '<td class="grid-texto-resaltado1">' + row[colidx] + '</td>';
                                } else {
                                    htmlTable += '<td>' + row[colidx] + '</td>';
                                }
                            }

                        }
                    }

                    htmlTable += '</tr>';

                }

                htmlTable += "</tbody></table>";

                $("#mainDivGrilla" + idGrid).append(htmlTable);

                // Inicializa el DataTable plugin
                var opts = $.extend({}, {
                    "bAutoWidth": false, //para que no se cambie el tamaño de las columnas al hacer resize
                    "bLengthChange": false,
                    "bInfo": false,
                    "language": {
                        "zeroRecords": "No se encontraron registros" ,
                        "infoEmpty":  "No se encontraron registros" ,
                            "search":  "BUSCAR" ,
                            "paginate": {
                                "sFirst":  "Primera" , // This is the link to the first page
                                "sPrevious": "Anterior" , // This is the link to the previous page
                                "sNext": "Siguiente" , // This is the link to the next page
                                "sLast":  "Última"  // This is the link to the last page
                            }
                    },
                    "order": []
                }, settings.datatableOptions || {});

                var table = $('#datatatable_' + idGrid).DataTable(opts);

                //fix header responsive 
                $('#datatatable_' + idGrid + '_wrapper .dataTables_scrollHeadInner').css('width', '100%');
                $('#datatatable_' + idGrid + '_wrapper .dataTables_scrollHeadInner table').css('width', '100%');

                // Mecanismo de selección de filas
                if (settings.selection) {
                    $('#datatatable_' + idGrid + ' tbody').on('click', 'tr', function () {

                        if ($(this).hasClass('grid-rowselected')) {
                            $(this).removeClass('grid-rowselected');
                        } else {
                            table.$('tr.grid-rowselected').removeClass('grid-rowselected');
                            $(this).addClass('grid-rowselected');
                        }

                        var datarow = table.row(this).data();
                        var idrow = $(this).data("idrow");
                        if (settings.fOnClick != undefined) {
                            settings.fOnClick(datarow, idrow);
                        }

                    });
                }
                return table;

            },

        }

        // if a method as the given argument exists
        if (methods[method]) {

            // call the respective method
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));

            // if an object is given as method OR nothing is given as argument
        } else if (typeof method === 'object' || !method) {

            // call the initialization method
            return methods.init.apply(this, arguments);

            // otherwise
        } else {

            // trigger an error
            $.error('Method "' + method + '" does not exist in gridTableTxDs plugin!');

        }

    }

})(jQuery);
