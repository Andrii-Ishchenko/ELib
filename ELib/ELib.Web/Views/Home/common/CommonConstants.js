(function () {
    angular.module("elib")
           .constant('COMMON_CONST', {
               'LOGIN':            '/login',
               'UNAUTHORIZED_USER': 401,
               'REGISTRATION_URL': 'api/account/register',
               'BASE_URL':         "/api/",
               'URL_ID':           "/:id/",
               'TEMPLATE_URL':     "/views/home/common/lib-page-changer.html",
               'SORTING':          '/views/home/common/sorting-directive.html'
           });
})();