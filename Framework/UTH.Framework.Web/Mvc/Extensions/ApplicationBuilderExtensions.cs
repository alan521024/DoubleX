namespace Microsoft.AspNetCore.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Globalization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using AutoMapper;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 当前配置
        /// </summary>
        private static WebAppServiceOptions current = new WebAppServiceOptions();

        /// <summary>
        /// 使用 Web 服务
        /// </summary>
        public static IApplicationBuilder UseWeb(this IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration, IHosting hosting)
        {
            //error
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            //request hosting
            app.UseMiddleware();

            //static,default,html,directory,...
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //authentication
            if (EngineHelper.Configuration.Authentication.AuthenticationType != EnumAuthenticationType.None)
            {
                app.UseAuthentication();
            }
            
            //mvc
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //resource
            if (!EngineHelper.Configuration.Culture.IsEmpty())
            {
                var languageProperties = typeof(Lang).GetProperties(BindingFlags.Static | BindingFlags.Public);
                EngineHelper.Configuration.Culture.Split(',').ToList().ForEach((s) =>
                {
                    var jsContent = ResourceHelper.ToJsonContent("__language", languageProperties, Lang.ResourceManager, new CultureInfo(s));
                    FilesHelper.SaveFile(FilesHelper.GetPath(string.Format("{0}/culture", env.WebRootPath)), string.Format("{0}.js", s.ToLower()), jsContent);
                });
            }

            return app;
        }

        /// <summary>
        /// Hosting 中间件(放在最前面)
        /// </summary>
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HostingMiddleware>();
        }
    }


}
