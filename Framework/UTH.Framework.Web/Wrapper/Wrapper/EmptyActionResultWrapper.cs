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

    /// <summary>
    /// 空结果封装对象
    /// </summary>
    public class EmptyActionResultWrapper : IResultWrapper
    {
        public void Wrap(ActionContext context, ref IActionResult result)
        {
            context.CheckNull();
            result.CheckNull();

            result = new ObjectResult(new ResultModel());
        }
    }
}
