namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using System.Reflection;
    using Castle.DynamicProxy;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;


    /// <summary>
    /// 应用服务调用日志记录拦截器接口
    /// </summary>
    public class ApplicationLoggingInterceptor : IApplicationLoggingInterceptor, IInterceptor
    {
        private readonly ILog applicationLogger = LoggingManager.GetLogger("Application");

        private Guid currentId = Guid.Empty;

        public void Intercept(IInvocation invocation)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //excute before
            InterceptBefore(invocation);

            //proceed
            invocation.Proceed();

            sw.Stop();

            //excute after
            InterceptAfter(invocation, sw);
        }

        private void InterceptBefore(IInvocation invocation)
        {
            currentId = Guid.NewGuid();

            StringBuilder argBuild = new StringBuilder();
            if (!invocation.Arguments.IsEmpty())
            {
                for (var i = 0; i < invocation.Arguments.Length; i++)
                {
                    var argValue = invocation.Arguments[i].IsNull() ? "null" : invocation.Arguments[i].ToString();
                    if (invocation.Arguments[i] is IInput)
                    {
                        argValue = JsonHelper.Serialize(invocation.Arguments[i]);
                    }
                    argBuild.Append($"[i:{i} value:{argValue}] | ");
                }
            }

            applicationLogger.Info($"---------------start[{currentId.ToString("N")}]---------------\r\n Target:{invocation.TargetType} - {invocation.Method.Name} \r\n Params:{argBuild}");
        }

        private void InterceptAfter(IInvocation invocation, Stopwatch sw)
        {
            var returnValue = invocation.ReturnValue.IsNull() ? "null" : invocation.ReturnValue.ToString();
            if (invocation.ReturnValue is IOutput) {
                returnValue = JsonHelper.Serialize(invocation.ReturnValue);
            }

            applicationLogger.Info($"---------------end[{currentId.ToString("N")}]---------------\r\n Target:{invocation.TargetType} - {invocation.Method.Name} \r\n Return:{returnValue} \r\n Stopwatch:{sw.ElapsedMilliseconds}");
        }
    }
}
