var Main = Main ||
{
    init: function (model) {
        //Call Js Error Handler module if defined.
     //   MDSLogUtil.init();
     ////   UtilJs.redirect = model.redirect;
     //   //TODO implement rest of init logic

     //   Main.InitCombos();
     //   Main.InicializarGrilla();
     //   Main.InitEventos();
      
    },

    
    InitEventos: function () {

        
        UtilJs.addCommonListener({
            selector: '#BuscarResponsableBtn',
            callback: function (event) {
                var obj1 = {
                    PROYECTO: 521,
                    CLIENTE: 'H00850000- GENER',
                    NORMA: 'MS-24 \ 050XF',
                    COMERCIAL: true,
                    HIGHEND: false,
                    PRODTO: true,
                    TECNOLOGO: true,
                    ASTE: true,
                    VIGENTE: '20/11/2019' 
                };
                var obj2 = {
                    PROYECTO: 521,
                    CLIENTE: 'H00850000- GENER',
                    NORMA: 'MS-24 \ 050XF',
                    COMERCIAL: false,
                    HIGHEND: true,
                    PRODTO: true,
                    TECNOLOGO: true,
                    ASTE: false,
                    VIGENTE: '20/11/2019'
                };
                var obj3 = {
                    PROYECTO: 521,
                    CLIENTE: 'H00850000- GENER',
                    NORMA: 'MS-24 \ 050XF',
                    COMERCIAL: true,
                    HIGHEND: true,
                    PRODTO: true,
                    TECNOLOGO: false,
                    ASTE: true,
                    VIGENTE: '20/11/2019'
                };
                var temp = [obj1, obj2, obj3];
                UtilJs.pluginConfig("#gridResponsable", "gridResponsable", temp);
            }
        });

        UtilJs.addCommonListener({
            selector: '#findClienteBtn',
            callback: function (event) {
                $("#modalSeleccionarCliente").modal();
            }
        });
    },
    InitCombos: function () {
      //  Main.CargarComboEstado('#comboTipOfertaSearch');
        $("#select_estado").select2({ "allowClear": true, 'placeholder': "ESTADO" });
        $("#select_usuarioCreador").select2({ "allowClear": true, 'placeholder': "USUARIO CREADOR"});
        $("#select_TipoProyecto").select2({ "allowClear": true, 'placeholder':"TIPO PROYECTO " });
        
    },
    ReajustarGrilla:  function() {
    
                  setTimeout(function () { $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust(); }, 380);
    },
    initSelectSociedad: function () {
        UtilJs.execAjaxTrace({
            url: SitePath + "AltaOFAs/findAllSociedades",
            timeout: timing.ajax2min,
            success: function (result) {
                var func = function (opt, item) {
                    opt.text(item.razonSocial);
                    opt.val(item.codigo);
                    return opt;
                };

                UtilJs.fillSelect2('#selectSociedad-Index', result.SociedadesItems, func, { placeholder:  'SELECCIONE' , allowClear: true }, null, false);
            }
        });
    },
    InicializarGrilla: function () {
        UtilJs.pluginConfig("#gridResponsable", "gridResponsable", []);
    },

}