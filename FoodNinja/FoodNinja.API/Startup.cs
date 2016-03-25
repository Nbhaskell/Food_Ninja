using FoodNinja.API.OAuth;
using FoodNinja.Core.Domain;
using FoodNinja.Core.Infrastructure;
using FoodNinja.Core.Repository;
using FoodNinja.Data.Infrastructure;
using FoodNinja.Data.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(FoodNinja.API.Startup))]
namespace FoodNinja.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureSimpleInjector(app);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            WebApiConfig.Register(config);

            ConfigureOAuth(app, container);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, Container container)
        {
            Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;

            var authenticationOptions = new OAuthBearerAuthenticationOptions();

            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new FoodNinjaAuthorizationServerProvider(authRepositoryFactory)
            };

            app.UseOAuthAuthorizationServer(authorizationOptions);
            app.UseOAuthBearerAuthentication(authenticationOptions);
        }

        public Container ConfigureSimpleInjector(IAppBuilder app)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            //Infrastructure
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();
            container.Register<IUserStore<NinjaUser, int>, UserStore>(Lifestyle.Scoped);

            //Repositories
            container.Register<IInviteRepository, InviteRepository>();
            container.Register<INinjaUserRepository, NinjaUserRepository>();
            container.Register<IOrderRepository, OrderRepository>();
            container.Register<IParticipationRepository, ParticipationRepository>();
            container.Register<IRestaurantLocationRepository, RestaurantLocationRepository>();
            container.Register<IRestaurantOptionRepository, RestaurantOptionRepository>();
            container.Register<IRestaurantRepository, RestaurantRepository>();
            container.Register<ITeamRepository, TeamRepository>();
            container.Register<IRoleRepository, RoleRepository>();
            container.Register<IUserRoleRepository, UserRoleRepository>();
            container.Register<IAuthorizationRepository, AuthorizationRepository>(Lifestyle.Scoped);

            app.Use(async (ContextBoundObject, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Verify();

            return container;


            // Potentially risky
            //    var repositoryAssembly = typeof(FoodNinja.Data.Infrastructure.DatabaseFactory).Assembly;

            //    var registrations =
            //        from type in repositoryAssembly.GetExportedTypes()
            //        where type.Namespace == "FoodNinja.Data.Repository"
            //        where type.GetInterfaces().Any()
            //        select new { Service = type.GetInterfaces().Single(), Implementation = type };

            //    foreach (var reg in registrations)
            //    {
            //        container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);
            //    }
            //  Potentially risky

            //    container.Verify();

            //    app.Use(async (context, next) =>
            //    {
            //        using (container.BeginExecutionContextScope())
            //        {
            //            await next();
            //        }
            //    });

            //    return container;
            //}
        }
    }
}
