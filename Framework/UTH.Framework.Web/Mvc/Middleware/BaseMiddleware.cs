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

            var exp = ex as DbxException;
            if (!exp.IsNull())
            {
                throw ex;
            }

            if (exp.Code.IsIn(EnumCode.认证错误, EnumCode.认证失败, EnumCode.认证过期, EnumCode.授权失败))
            {
                context.Response.StatusCode = HttpStatusCode.Unauthorized.GetValue();

                context.Response.Headers.Append(HeaderNames.WWWAuthenticate, exp.Code == EnumCode.认证错误 ? "Audience Error" : ExceptionHelper.GetMessage(exp));

                if (context.Request.Headers[HeaderNames.ContentType] == "application/json")
                {
                    var result = new ResultModel() { Code = exp.Code, Message = exp.Message };
                    if (result.Message.IsEmpty())
                    {
                        result.Message = result.Code.ToString() + " (提示消息需中/英多语处理)";
                    }
                    context.Response.Headers[HeaderNames.ContentType] = "application/json";
                    context.Response.WriteAsync(JsonHelper.Serialize(result));
                }
            }
        }
    }
}
