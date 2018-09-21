namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// 返回结果封装对象创建
    /// </summary>
    public static class ResultWrapperFactory
    {
        public static IResultWrapper CreateFor(ResultExecutingContext context)
        {
            context.CheckNull();
            context.Result.CheckNull();

            return ReturnWrapper(context.HttpContext, context.Result, context.HttpContext.RequestServices);
        }

        public static IResultWrapper CreateFor(ExceptionContext context)
        {

            context.CheckNull();
            return ReturnWrapper(context.HttpContext, context.Result, context.HttpContext.RequestServices);
        }
        
        private static IResultWrapper ReturnWrapper(HttpContext context, IActionResult result, IServiceProvider provider = null)
        {
            context.CheckNull();
            context.Request.CheckNull();
            context.Response.CheckNull();

            if (context.Request.IsAjaxRequest())
            {
                WebHelper.SetNoCache(context.Response);
            }

            if (result is ObjectResult)
            {
                provider.CheckNull();
                return new ObjectActionResultWrapper(provider);
            }

            if (result is JsonResult)
            {
                return new JsonActionResultWrapper();
            }

            if (result is EmptyResult)
            {
                return new EmptyActionResultWrapper();
            }

            return new NullActionResultWrapper();
        }

    }
}
