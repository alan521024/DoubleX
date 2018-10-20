using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.MP.Containers;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;
using Senparc.Weixin.MP;

namespace UTH.Meeting.Web
{
    public class Startup
    {
        public Startup(IConfiguration _configuration)
        {
            Configuration = _configuration;
            hosting = new AppHosting();
        }

        public IHosting hosting { get; }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            EngineHelper.Worker.Startup(hosting).OnStart();

            var builder = services.AddWeb(Configuration, hosting, (option) =>
            {
                option.IsDynamicApi = false;
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

            services.AddMemoryCache();                              //使用本地缓存必须添加
            services.AddSession();                                  //使用Session

            services.AddSenparcGlobalServices(Configuration)        //Senparc.CO2NET 全局注册
                    .AddSenparcWeixinServices(Configuration);       //Senparc.Weixin 注册

            return builder.AddAutofacProvider(services, Configuration, hosting, (opt) =>
            {
                opt.Interceptors = interceptors.ToArray();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //引入EnableRequestRewind中间件
            app.UseEnableRequestRewind();
            app.UseSession();

            app.UseWeb(env, Configuration, hosting);

            // 启动 CO2NET 全局注册，必须！
            //关于 UseSenparcGlobal() 的更多用法见 CO2NET Demo：https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore/Startup.cs

            var senparcSetting = new SenparcSetting()
            {
                IsDebug = true,
                DefaultCacheNamespace = "DefaultCache",
                Cache_Redis_Configuration = "",//redis1:6379
                Cache_Memcached_Configuration = "",
                SenparcUnionAgentKey = ""
            };

            var senparcWeixinSetting = new SenparcWeixinSetting()
            {
                IsDebug = true,
                EncodingAESKey = "",
                WeixinAppId = "wx8eff62aca23ebf87",
                WeixinAppSecret = "b224033b6b4bf36970deaf5aa846ab4d"
            };


            //CO2NET 设置
            IRegisterService register = RegisterService.Start(env, senparcSetting).UseSenparcGlobal();

            register.ChangeDefaultCacheNamespace("DefaultCO2NETCache");

            //app.UseSenparcWeixinCacheMemcached();

            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting)
                .RegisterMpAccount(senparcWeixinSetting, "【盛派网络小助手】公众号");
        }
    }
}
