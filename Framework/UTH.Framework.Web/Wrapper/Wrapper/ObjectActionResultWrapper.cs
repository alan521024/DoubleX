namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Buffers;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// Object结果封装对象
    /// </summary>
    public class ObjectActionResultWrapper : IResultWrapper
    {
        private readonly IServiceProvider serviceProvider;

        public ObjectActionResultWrapper(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }

        public void Wrap(ActionContext context, ref IActionResult result)
        {
            context.CheckNull();
            result.CheckNull();

            var objectResult = result as ObjectResult;
            if (objectResult == null)
            {
                throw new ArgumentException($"{nameof(result)} should be ObjectResult!");
            }

            if (!(objectResult.Value is IResultModel))
            {
                //eg:假如返回类型为string formatters string的format 序例化对象时会报错
                if (!objectResult.Formatters.Any(f => f is JsonOutputFormatter))
                {
                    objectResult.Formatters.Add(
                        new JsonOutputFormatter(
                            serviceProvider.GetRequiredService<IOptions<MvcJsonOptions>>().Value.SerializerSettings,
                            serviceProvider.GetRequiredService<ArrayPool<char>>()
                        )
                    );
                }
                objectResult.Value = new ResultModel(objectResult.Value);
            }
        }
    }
}
