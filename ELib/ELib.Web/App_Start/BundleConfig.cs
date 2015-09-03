using System.Web;
using System.Web.Optimization;

namespace ELib.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap*"));


            //.Include("~/Content/site.css")
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap*"));

            bundles.Add(new ScriptBundle("~/Views/Home/Angular")
                        .Include("~/Views/Home/Elib.js",
                                    "~/Views/Home/common/DataServiceFactory.js",
                                    "~/Views/Home/config.js",
                                    "~/Views/Home/book/BookController.js",
                                    "~/Views/Home/book/BooksController.js",
                                    "~/Views/Home/user/ProfileFactory.js",
                                    "~/Views/Home/user/ProfileController.js",
                                    "~/Views/Home/book/bookListDirective.js",
                                    
                                    "~/Views/Home/author/AuthorController.js",
                                    "~/Views/Home/author/AuthorsController.js",
                                    "~/Views/Home/author/authorListDirective.js",

                                    "~/Views/Home/user/CurrentProfileController.js",
                                    "~/Views/Home/user/CurrentProfileFactory.js"
                        )
                    );

        }
    }
}