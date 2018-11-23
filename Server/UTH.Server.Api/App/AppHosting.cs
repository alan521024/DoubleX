namespace UTH.Server.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

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
            EngineHelper.LoggingInfo("UTH.Server.Api - Startup - ");

            //(1)注入初始
            DomainHelper.IocInitialization();

            //(2)领域配置
            EngineHelper.DomainProfile.List.ForEach(x => x.Configuration());

            //(3)对对映射
            EngineHelper.MapperInit();

            //(4)组件安装
            EngineHelper.Component.List.ForEach(x => x.Install());

            //己由Strap中方法执行了
            //EngineHelper.IocRelease();                         
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
