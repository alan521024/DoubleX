using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Autofac;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Domain;
using UTH.Plug;
using UTH.Framework.Wpf;

namespace UTH.Meeting.Server
{
    /// <summary>
    /// 应用宿主
    /// </summary>
    public class AppHosting : AbsHosting, IHosting
    {
        public AppHosting() : this(Guid.NewGuid(), null, null, null, null)
        {
            OnStart = (obj) => { };
            OnStop = (obj) => { };
            OnBegin = (obj) => { Begin(obj); };
            OnEnd = (obj) => { End(obj); };
        }

        public AppHosting(Action<object> start, Action<object> stop, Action<object> begin, Action<object> end) : this(Guid.NewGuid(), start, stop, begin, end)
        {

        }

        public AppHosting(Guid key, Action<object> start, Action<object> stop, Action<object> begin, Action<object> end)
        {
            Key = Guid.NewGuid();
            OnStart = start;
            OnStop = stop;
            OnBegin = begin;
            OnEnd = end;
        }

        public override Guid Key { get; }

        public override Action<object> OnStart { get; }

        public override Action<object> OnStop { get; }

        public override Action<object> OnBegin { get; }

        public override Action<object> OnEnd { get; }

        public override void Startup()
        {
            //(0)应用运行初始配置
            LoggingManager.AddLoggerAdapter(new Log4netLoggerAdapter());  //增加日志组件
            EngineHelper.LoggingInfo("UTH Meeting Win Server - Startup - ");

            //(1)领域相关初始配置
            DomainConfiguration.Initialize();
            
            //(2)组件安装初始配置
            EngineHelper.Component.List.ForEach(x => x.Install());

            //(3)会话访问认证授权
            EngineHelper.RegisterType<IAccessor, WpfAccessor>();
            EngineHelper.RegisterType<IApplicationSession, IdentifierSession>();
            EngineHelper.RegisterType<ITokenService, TokenService>();

            //接口访问处理
            PlugCoreHelper.ApiServerAuthError = (token) =>
            {
                //throw new DbxException(EnumCode.认证错误);
            };
            PlugCoreHelper.ApiServerResultError = (code, msg, obj) =>
            {
                //throw new DbxException(code, msg);
            };

            //IOC注入
            EngineHelper.ContainerBuilder<IContainer>();
        }

        private void Begin(object obj)
        {
            //HttpContext context = obj as HttpContext;
        }

        private void End(object obj)
        {
            //HttpContext context = obj as HttpContext;
        }
    }
}
