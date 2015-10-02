using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Mapper.Concrete;
using ELib.BL.Services.Abstract;
using ELib.BL.Services.Concrete;
using ELib.DAL.Infrastructure.Abstract;
using ELib.DAL.Infrastructure.Concrete;
using ELib.DAL.Repositories.Abstract;
using ELib.DAL.Repositories.Concrete;
using ELib.Domain.Entities;
using ELib.Web.Infrastructure.Concrete;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace ELib.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // config.MapHttpAttributeRoutes();
        //    config.SuppressDefaultHostAuthentication();
        //    config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Routes.MapHttpRoute(
                "FileRoute",
                "api/file/{action}/{id}",
                new { controller = "File", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "AccountRoute",
                "api/account/{action}",
                new { controller = "Account" }
            );
            config.Routes.MapHttpRoute(
                "BookRoute",
                "api/books/{action}/{id}",
                new { controller = "Books", id = RouteParameter.Optional }
            );
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                 name: "ConnectionsApi",
                 routeTemplate: "api/{controller}/{id}/{action}",
                 defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
             );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            var container = new UnityContainer();

            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType(typeof(IBaseService<,>), typeof(BaseService<,>));
            container.RegisterType<IGenreService, GenreService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPublisherService, PublisherService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthorService, AuthorService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileService, FileService>(new HierarchicalLifetimeManager());
            container.RegisterType<IBookService, BookService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommentService, CommentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRatingService, RatingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRatingCommentService, RatingCommentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProfileService,ProfileService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrentProfileService, CurrentProfileService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILanguageService, LanguageService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubgenreService, SubgenreService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoryService, CategoryService>(new HierarchicalLifetimeManager());

            container.RegisterType<IMapper<Author, AuthorListDto>, AuthorsListMapping>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Person, CurrentPersonDto>, CurrentPersonMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Book,BookDto>, BookMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Author, AuthorDto>, AuthorMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Genre, GenreDto>, GenreMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Subgenre, SubgenreDto>, SubgenreMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Category, CategoryDto>, CategoryMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<BookInstance, BookInstanceDto>, BookInstanceMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Comment, CommentDto>, CommentMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Language, LanguageDto>, LanguageMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IMapper<Category, CategoryNestedDto>, CategoryNestedMapper>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
