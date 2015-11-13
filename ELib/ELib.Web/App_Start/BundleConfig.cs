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
                .Include("~/Content/bootstrap*",
                           "~/Content/custom.css",
                           "~/Content/main.css",
                           "~/Content/profile.css",
                           "~/Content/ng-rating.css",
                           "~/Content/font-awesome.min.css",
                           "~/Content/bold-text.css"));

            bundles.Add(new ScriptBundle("~/Views/Home/Angular")
                        .Include("~/Views/Home/Elib.js",
                                    "~/Views/Home/common/authServiceFactory.js",
                                    "~/Views/Home/common/authInterceptorServiceFactory.js",
                                    "~/Views/Home/common/dataServiceFactory.js",
                                    "~/Views/Home/common/libPageCountChangerDirective.js",
                                    "~/Views/Home/book/bookRepository.js",
                                    "~/Views/Home/book/FilterController.js",
                                    "~/Views/Home/config.js",
                                    "~/Views/Home/index/IndexController.js",
                                    "~/Views/Home/index/SearchController.js",
                                    "~/Views/Home/filter/libFilterDirective.js",
                                    "~/Views/Home/index/MenuController.js",
                                    "~/Views/Home/book/BookController.js",
                                    "~/Views/Home/book/BooksController.js",
                                    "~/Views/Home/user/profileFactory.js",
                                    "~/Views/Home/user/ProfileController.js",
                                    "~/Views/Home/files/fileFactory.js",
                                    "~/Views/Home/files/FileController.js",
                                    "~/Views/Home/book/bookListDirective.js",

                                    "~/Views/Home/author/AuthorController.js",
                                    "~/Views/Home/author/AuthorsController.js",
                                    "~/Views/Home/author/authorListDirective.js",
                                    "~/Views/Home/author/NewAuthorController.js",

                                    "~/Views/Home/help/HelpController.js",
                                    "~/Views/Home/book/NewBookController.js",
                                    "~/Views/Home/registration/RegistrationController.js",
                                     "~/Views/Home/login/LoginController.js",
                                    "~/Views/Home/user/CurrentProfileController.js",
                                    "~/Views/Home/user/currentProfileFactory.js",

                                    "~/Views/Home/publisher/PublisherController.js",
                                    "~/Views/Home/publisher/PublishersController.js",
                                    "~/Views/Home/publisher/NewPublisherController.js",

                                    "~/Views/Home/mainpage/MainController.js",
                                    "~/Views/Home/mainpage/ModalController.js",
                                    "~/Views/Home/mainpage/ModalInstanceController.js",
                                    "~/Views/Home/book/RatingController.js",
                                    "~/Views/Home/comments/CommentsRepository.js",
                                    "~/Views/Home/comments/CommentsDirective.js",
                                    "~/Views/Home/comments/CommentsConstants.js",
                                    "~/Views/Home/common/sortingDirective.js"

                        )
                    );

        }
    }
}