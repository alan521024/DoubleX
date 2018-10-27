using System;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using Microsoft.Net.Http.Headers;

namespace UTH.Framework
{
    /// <summary>
    /// 中间件抽象基类
    /// </summary>
    public abstract class BaseMiddleware
    {
        protected RequestDelegate Next { get; set; }

        protected BaseMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        public abstract Task Invoke(HttpContext context);

        protected void ExceptionResult(HttpContext context, Exception ex)
        {
            if (ex.IsNull())
            {
                return;
            }

            var exception = ex as DbxException;
            if (!exception.IsNull())
            {
                switch (exception.Code)
                {
                    case EnumCode.认证错误:
                    case EnumCode.认证失败:
                    case EnumCode.认证过期:
                    case EnumCode.授权失败:
                        context.Response.StatusCode = HttpStatusCode.Unauthorized.GetValue();
                        context.Response.Headers.Append(HeaderNames.WWWAuthenticate, exception.Code == EnumCode.认证错误 ? "Audience Error" : ExceptionHelper.GetMessage(exception));
                        if (context.Request.Headers[HeaderNames.ContentType] == "application/json")
                        {
                            var result = new ResultModel() { Code = exception.Code, Message = Lang.sysRenZhengShiBaiHuoGuoQi };
                            context.Response.Headers[HeaderNames.ContentType] = "application/json";
                            context.Response.WriteAsync(JsonHelper.Serialize(result));
                        }
                        return;
                }
            }

            throw ex;
        }
    }
}
