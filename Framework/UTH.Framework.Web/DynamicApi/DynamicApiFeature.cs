namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 动态Api 控制器特性 用户判断模块Service类是否能作为api 控制器
    /// </summary>
    public class DynamicApiFeature : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo controllerType)
        {
            //if (controllerType.FullName.Contains("UTH.Modeule"))
            //{
            //    var tag = "ttt";
            //}
            var isController = DynamicApiHelper.IsServiceController(controllerType);
            if (isController)
                return true;

            return base.IsController(controllerType);
        }
    }
}
