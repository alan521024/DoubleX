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
    /// 方法日志记录拦截器
    /// </summary>
    public class LoggingInterceptor : ILoggingInterceptor, IInterceptor
    {
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
        }

        private void InterceptAfter(IInvocation invocation, Stopwatch sw)
        {
            if (!invocation.Method.DeclaringType.FullName.StartsWith("Microsoft"))
            {
                StringBuilder argBuild = new StringBuilder();
                if (!invocation.Arguments.IsEmpty())
                {
                    for (var i = 0; i < invocation.Arguments.Length; i++) {
                        argBuild.AppendFormat("[index:{0} value:{1}] | ", i, invocation.Arguments[i].IsNull() ? "null" : invocation.Arguments[i].ToString());
                    }
                }

                EngineHelper.LoggingService(string.Format("Target:{0}\r\n  Method:{1}\r\n  Params:{2}\r\n  Times:{3}",
                    invocation.TargetType.ToString(),
                    invocation.Method.ToString(),
                    argBuild.ToString(),
                    sw.Elapsed.TotalMilliseconds));
            }
        }
    }
}
