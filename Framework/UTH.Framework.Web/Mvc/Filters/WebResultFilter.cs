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
    /// 返回结果过滤器
    /// </summary>
    public class WebResultFilter : IResultFilter, ITransientDependency
    {
        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            var attribute = ObjectHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(context.ActionDescriptor.GetMethodInfo(), AspNetCoreMvcHelper.DefaultWebExtendAttribute);

            var method = context.ActionDescriptor.GetMethodInfo();
            var serviceAction = DynamicApiHelper.GetServiceAction(method.DeclaringType, method);
            if (!serviceAction.IsNull())
            {
                attribute.IsResultWrapper = BoolHelper.Get(serviceAction.ResultWrapper);
            }

            if (attribute.IsResultWrapper)
            {
                var result = context.Result;
                ResultWrapperFactory.CreateFor(context).Wrap(context, ref result);
            }
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}
