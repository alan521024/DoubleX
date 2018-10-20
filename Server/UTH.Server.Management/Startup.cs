using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;

namespace UTH.Server.Management
{
    public class Startup
    {
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
            hosting = new AppHosting();
        }
        public IConfiguration configuration { get; }

        public IHosting hosting { get; }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            EngineHelper.Worker.Startup(hosting).OnStart();

            var builder = services.AddWeb(Configuration, hosting, (option) =>
            {
                option.IsDynamicApi = true;
                option.IsAutofacDIService = true;

                option.Filters.Add<WebActionFilter>();
                option.Filters.Add<WebExceptionFilter>();
                option.Filters.Add<WebResultFilter>();

                DynamicApiHelper.ApiComponents.ForEach((item) =>
                {
                    option.MvcApplicationParts.Add(new AssemblyPart(item.Assemblies));
                });

            });

            var interceptors = DomainConfiguration.Options.Interceptors.ToList();
            interceptors.Add(typeof(INotifyInterceptor));
            interceptors.Add(typeof(ICaptchaInterceptor));

            return builder.AddAutofacProvider(services, Configuration, hosting, (opt) =>
            {
                opt.Interceptors = interceptors.ToArray();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseWeb(env, Configuration, hosting);
        }
    }
}
