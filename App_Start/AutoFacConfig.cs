using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using NewBlogAPI.Data;
using NewBlogAPI.Components.BlogArticleFilesComponents.Services;
using NewBlogAPI.Components.BlogArticleFilesComponents.Services.Interfaces;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using NewBlogAPI.Components.AdminComponents.Services;
using NewBlogAPI.Components.BlogArticleComponents.Services;
using NewBlogAPI.Components.BlogArticleComponents.Services.Interfaces;
using NewBlogAPI.Components.AdminComponents.Services.Interfaces;

namespace NewBlogAPI.App_Start
{
    public static class AutoFacConfig
    {
        public static void Configuration(HttpConfiguration app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<BlogDBContext>().As<BlogDBContext>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().As<ApplicationUserManager>().InstancePerRequest();

            //Register services
            builder.RegisterType<BlogArticleService>().As<IBlogArticleService>().InstancePerRequest();
            builder.RegisterType<BlogArticleFilesService>().As<IBlogFilesService>().InstancePerRequest();
            builder.RegisterType<BlogAdminsService>().As<IBlogAdminsService>().InstancePerRequest();

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}