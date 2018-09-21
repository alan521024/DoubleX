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
    /// JSON格式结果封装对象
    /// </summary>
    public class JsonActionResultWrapper : IResultWrapper
    {
        public void Wrap(ActionContext context, ref IActionResult result)
        {
            context.CheckNull();
            result.CheckNull();

            var jsonResult = result as JsonResult;
            if (jsonResult == null)
            {
                throw new ArgumentException($"{nameof(result)} should be JsonResult!");
            }

            if (!(jsonResult.Value is ResultModel))
            {
                jsonResult.Value = new ResultModel(jsonResult.Value);
            }
        }
    }
}
