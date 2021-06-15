var Home = Home ||
{
    
    init: function () {
        //TODO implement rest of init logic 
        Home.User = $("#UsuarioActualemteLogeado").attr("name");
        console.log(Home.User);
        $(".splash").addClass("hidden");
       
        Home.initDT();
        Home.addBuscarEmpleadosParaCampoListener();
        Home.FillComboCampos();
        Home.addDescargarExcelListener();
        UtilJs.pluginConfig("#gridTemperatura", "gridTemperatura", []);

    },
    initDT: function () {
        $('#datetimepickerDesde').datetimepicker({
            format: 'DD/MM/YYYY'
        });

        $('#datetimepickerHasta').datetimepicker({
            useCurrent: false,
            format: 'DD/MM/YYYY'
            //Important! See issue #1075
        });
        $("#datetimepickerDesde").on("dp.change", function (e) {
            $('#datetimepickerHasta').data("DateTimePicker").minDate(e.date);
        });
        $("#datetimepickerHasta").on("dp.change", function (e) {
            $('#datetimepickerDesde').data("DateTimePicker").maxDate(e.date);
        });
    },
    FillComboCampos: function () {

        UtilJs.blockScreen();
        $.ajax({
            url: SitePath + "obtenerCamposDelUsuario?user="+Home.User,
            timeout: timing.ajax2min,
            success: function (result) {
                var func = function (opt, item) {
                    opt.text(item.Name);
                    opt.val(item.Id); 
                    return opt;
                };
                 UtilJs.fillSelect2('#select_Campo', result, func, { placeholder: 'SELECCIONE', allowClear: false }, null, true,true);
$.unblockUI();
                      
            }
        });
    },
    //handdleLogin: function () {
    //    var correo = $("#defaultForm-email").val();
    //    var password = $("#defaultForm-pass").val();

    //    UtilJs.blockScreen();
    //    $.ajax({
    //        url: SitePath + "/GetuserForLogin?correo=" + correo + "&password=" + password,
    //        timeout: timing.ajax2min,
    //        success: function (result) {
                 
    //            Home.User = result;
    //            if (Home.User==0) {
    //                $("#MensajeDeerror").text("Usuario o contraseña incorrecto, vuelva a intentar");
    //                setTimeout(function () { $("#MensajeDeerror").text(""); }, 5000);
    //            } else {
    //                $("#modalLoginForm").modal("hide");

    //                Home.init();
    //            }
    //            $.unblockUI();

    //        }
    //    });
    //},


       addBuscarEmpleadosParaCampoListener: function () {
        
           UtilJs.addCommonListener({
               selector: "#select_Campo",
               event:"change",
               callback: function (e) {

                   UtilJs.blockScreen();
                   $.ajax({
                    url: SitePath + "EmpleadosPorCampo?IdCampo=" + $("#select_Campo").val(),
                    timeout: timing.ajax2min,
                    data: { User:Home.User },
                    success: function (result) {
                        var func = function (opt, item) {
                            opt.text(item.Name);
                            opt.val(item.Id);
                            return opt;
                        };
                        console.log(result);
                        UtilJs.fillSelect2('#select_Traajador', result, func, { placeholder: 'SELECCIONE TRABAJADOR', allowClear: true }, null, false);
                        $.unblockUI();

                    }
                });
               }
           });

           
           UtilJs.addCommonListener({
               selector: "#BtnBuscar",
               callback: function (e) {
                   UtilJs.blockScreen();
                   $.ajax({
                       url: SitePath + "Temperaturas?IdCampo=" + $("#select_Campo").val() + "&IdTrabajador=" + $("#select_Traajador").val()+"&desde=" + $("#FechaDesde").val() + "&hasta=" + $("#FechaHasta").val(),
                       timeout: timing.ajax2min,
                       data: { User: Home.User },
                       success: function (result) {
                           UtilJs.pluginConfig("#gridTemperatura", "gridTemperatura", result);
                           $.unblockUI();
                       }
                   });
               }
           });


    },

    addDescargarExcelListener: function () {

        UtilJs.addCommonListener({
            selector: "#btnDescargarExel",
            callback: function (e) {
               
                $(".exportexcel").click()
            }
        });
    }
}