namespace Microsoft.AspNetCore.Mvc
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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    public static class ActionDescriptorExtensions
    {
        public static ControllerActionDescriptor AsControllerActionDescriptor(this ActionDescriptor actionDescriptor)
        {
            var controllerActionDescriptor = actionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null)
            {
                throw new Exception($"{nameof(actionDescriptor)} should be type of {typeof(ControllerActionDescriptor).AssemblyQualifiedName}");
            }

            return controllerActionDescriptor;
        }

        public static MethodInfo GetMethodInfo(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.AsControllerActionDescriptor().MethodInfo;
        }

        public static bool IsControllerAction(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor is ControllerActionDescriptor;
        }
    }
}