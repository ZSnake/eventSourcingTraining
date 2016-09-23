using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using log4net.Config;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Rewardle.Boilerplate.Api.Test;
using Rewardle.Boilerplate.Infrastructure.BootstrapperTasks;
using Rewardle.Bootstrapping;
using Rewardle.Core.Infrastructure;
using Rewardle.Core.Infrastructure.BootstrapperTasks;
using Rewardle.Core.Infrastructure.Web.ExceptionHandling;
using Rewardle.Core.Infrastructure.Web.Middleware;
using Swashbuckle.Application;

namespace Rewardle.Boilerplate.Host
{
    public class Startup
    {
        const string Issuer = "Rewardle.Boilerplate";

        public void Configuration(IAppBuilder app)
        {
            XmlConfigurator.Configure();

            app.Use<ResponseTimeMiddleware>();
            app.Use<RequestLoggingMiddleware>();

            IContainer container = GetContainer();

            app.UseAutofacMiddleware(container);

            app.UseCors(CorsOptions.AllowAll);
            ConfigureOAuthResourceServer(app);

            var webApiConfig = new HttpConfiguration();

            var corsAttr = new EnableCorsAttribute(Application.CorsConfig.Origins, Application.CorsConfig.Headers,
                Application.CorsConfig.Methods);
            webApiConfig.EnableCors(corsAttr);

            webApiConfig.Filters.Add(new GlobalExceptionFilter());

            webApiConfig.MapHttpAttributeRoutes();

            webApiConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            webApiConfig.EnableSwagger(
                c =>
                {
                    c.SingleApiVersion("v1", "Rewardle.Boilerplate API");

                    c.IncludeXmlComments(GetXmlCommentsPathForControllers());
                }).EnableSwaggerUi();

            app.UseWebApi(webApiConfig);
            app.UseAutofacWebApi(webApiConfig);
        }

        string GetXmlCommentsPathForControllers()
        {

            return String.Format(@"{0}Rewardle.Boilerplate.Api.XML", AppDomain.CurrentDomain.BaseDirectory);
        }

        IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            BootstrapperTasks.RunAllInAssemblies(builder, typeof (WireUpEventStore).Assembly,
                typeof (WireUpDispatchers).Assembly);
            builder.RegisterApiControllers(typeof (TestController).Assembly).InstancePerRequest();
            IContainer container = builder.Build();
            return container;
        }

        

        void ConfigureOAuthResourceServer(IAppBuilder app)
        {
            const string audience = "099153c2625149bc8ecb3e85e03f0022";
            byte[] secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] {audience},
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                                                   {
                                                       new SymmetricKeyIssuerSecurityTokenProvider(Issuer, secret)
                                                   }
                });
        }
    }
}