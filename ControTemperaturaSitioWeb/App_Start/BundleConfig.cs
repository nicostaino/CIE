using System.Web;
using System.Web.Optimization;

namespace ControlTemperaturaSitioWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/basics/css").Include(
                 // Homer Fonts
                 "~/Content/fonts.css",
                 // Pe-icon-7-stroke
                 "~/Icons/pe-icon-7-stroke/css/pe-icon-7-stroke.css",
                 // Font Awesome icons style
                 "~/Vendor/fontawesome/css/font-awesome.min.css",
                 // Bootstrap style
                 "~/Vendor/bootstrap/css/bootstrap.min.css",
                 // Bootstrap dialog
                 "~/Vendor/bootstrap-dialog/bootstrap-dialog.min.css",

                "~/Vendor/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css",
                 // Homer modificado
                 "~/Content/style.css",
                 // Estilo NCA
                 "~/Content/nca.css"
                 ));

            // Basic script
            bundles.Add(new ScriptBundle("~/bundles/basics/js").Include(
                "~/Vendor/core/core.min.js",
                "~/Vendor/jquery/jquery-2.2.0.min.js",
                "~/Vendor/bootstrap/js/bootstrap.min.js",
                "~/Vendor/blockui/jquery.blockUI.js",
                "~/Vendor/bootstrap-dialog/bootstrap-dialog.min.js",
                "~/Vendor/async/async.min.js",
                "~/Scripts/Common/CommonFunctions.js",
                "~/Scripts/Common/ConstJs.js",
                "~/Scripts/Common/UtilJs.js",
                "~/Scripts/Common/MDSJsUtil.js",
                "~/Scripts/Common/MDSLogUtil.js",
                "~/Scripts/Common/MDSMessageUtil.js",
                "~/Scripts/main.js",
                "~/Scripts/Componentes/pagesStackerNavigator.js",
                "~/Scripts/Componentes/gridTemperatura.js",
                "~/Scripts/homer.js",
                "~/Vendor/moment/moment.min.js"));


            // Page style
            bundles.Add(new StyleBundle("~/bundles/page/css").Include(
                "~/Vendor/datatables/media/css/dataTables.bootstrap.min.css",
                "~/Vendor/sweetalert/sweetalert.css",
                "~/Content/Componentes/gridTableTxDs.css",
                "~/Vendor/datatables-plugins/fixed-columns/css/fixedColumns.dataTables.min.css",
                "~/Vendor/select2-3.5.4/select2.min.css",
                "~/Vendor/select2-bootstrap/select2-bootstrap.css",

                //Incluir módulo de css de esta app
                "~/Content/Modulos/home.css"));
            // Page script
            bundles.Add(new ScriptBundle("~/bundles/page/js").Include(
                "~/Vendor/datatables/media/js/jquery.dataTables.min.js",
                "~/Vendor/datatables/media/js/dataTables.bootstrap.min.js",
                "~/Scripts/Componentes/gridTableTxDs.js",
                "~/Vendor/datatables-plugins/fixed-columns/js/fixedColumns.dataTables.min.js",
                "~/Vendor/sweetalert/sweetalert.min.js",
            "~/Vendor/select2-3.5.4/lodash.js",
            "~/Vendor/select2-3.5.4/select2.min.js",
            "~/Vendor/select2-3.5.4/select2_locale_es.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/GestorComplementoMain/js").Include(
             "~/Scripts/Modulos/home.js"
         ));


            bundles.Add(new ScriptBundle("~/bundles/ProveedoresBancaElectronica/js").Include(
           "~/Areas/ProvBancaElectronicaITAU/ModulosJs/home.js", "~/Areas/ProvBancaElectronicaITAU/ModulosJs/gridOrdenesPago.js"
       ));
            bundles.Add(new ScriptBundle("~/bundles/AdministradorComplementos/js").Include(
         "~/Areas/AdministradorComplementos/ModulosJs/home.js",
         "~/Areas/AdministradorComplementos/Componentes/gridComplementos.js"));
            // Page script

            bundles.Add(new ScriptBundle("~/bundles/ExportacionCotizacionDolar/js").Include(
                    "~/Areas/ExportacionCotizacionDolar/ModulosJs/home.js",
                    "~/Areas/ExportacionCotizacionDolar/ModulosJs/gridCotizaciones.js"));

            bundles.Add(new ScriptBundle("~/bundles/ImportadorOPDesdeExcel/js").Include(
    "~/Areas/ImportadorOPDesdeExcel/models/home.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker/js").Include(
                "~/Scripts/bootstrap-datetimepicker.js"
            ));
            bundles.Add(new StyleBundle("~/bundles/datetimepicker/css").Include(
                         "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/ExportacionOPProveedores/js").Include(
           "~/Areas/ExportacionOPProveedores/ModulosJs/home.js",
           "~/Areas/ExportacionOPProveedores/ModulosJs/gridExportacionOPProveedores.js"));


        }
    }
}
