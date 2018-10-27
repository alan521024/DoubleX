using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Threading;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;

namespace UTH.Framework
{
    /// <summary>
    /// 宿主/服务中间件
    /// ref:http://www.cnblogs.com/niklai/p/5665272.html
    /// </summary>
    public class HostingMiddleware : BaseMiddleware
    {
        public HostingMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override async Task Invoke(HttpContext context)
        {
            //DO:开始操作
            HttpRequest request = context.Request;

            //DO:设置语言环境
            WebHelper.SetCulture(context);

            //DO:执行Hosting->Begin
            EngineHelper.Worker.OnBegin(context);
            
            //DO:调用结束
            context.Response.OnCompleted(ResponseCompletedCallback, context);

            //DO:继续下一下中间件
            //await Next(context);
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                ExceptionResult(context, ex);
            }
        }

        private Task ResponseCompletedCallback(object obj)
        {
            //DO:执行Hosting->OnEnd
            EngineHelper.Worker.OnEnd(obj);

            //DO:结束操作
            return Task.FromResult(0);
        }
    }
}

