using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionTienda
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            /*Cambiamos a un archivo bootstrap.bundle
            al igual que le quitamos el prefijo de ScriptBundle ya que la version 5 no responde con una descripcion
           completa de ScriptBundle (como en la 3) por lo que ahora solo indicamos un Bundle*/
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            /*Aqui solo vamos a utilizar el site.css ubicada en la ruta especificada (dentro de la carpeta content)*/
            bundles.Add(new StyleBundle("~/Content/css").Include(/*"~/Content/bootstrap.css",*/"~/Content/site.css"));
        }
    }
}
