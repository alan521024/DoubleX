namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class WebExceptionFilter : IExceptionFilter, ITransientDependency
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.Exception.IsNull())
            {
                EngineHelper.LoggingError(context.Exception.ToString());
            }

            var attribute = ObjectHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(context.ActionDescriptor.GetMethodInfo(), AspNetCoreMvcHelper.DefaultWebExtendAttribute);
            if (!attribute.IsExceptionWrapper)
            {
                return;
            }

            if (WebHelper.IsWebObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                context.Result = new ObjectResult(WebHelper.GetExceptionResult(context.Exception));
                context.Exception = null;
                return;
            }

            //TODO:可测试下，如出现异常，content.result应该为null,不管原方法return 什么样类，都为null
            if (!context.Result.IsNull())
            {
                var result = context.Result;
                ResultWrapperFactory.CreateFor(context).Wrap(context, ref result);
                return;
            }

         }
    }
}
