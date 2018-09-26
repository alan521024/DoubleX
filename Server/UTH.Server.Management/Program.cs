using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UTH.Infrastructure.Resource;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;

namespace UTH.Server.Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            string urls = EngineHelper.Configuration.Hosting;

            var host = WebHost.CreateDefaultBuilder(args)
                .UseUrls(urls.Split(','))
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
