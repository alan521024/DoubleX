using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using Autofac;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;

namespace UTH.Update.Win
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
            EngineHelper.LoggingInfo("UTH.Update.Win - Startup - ");

            //(1)领域相关初始配置
            DomainConfiguration.Initialize(opt =>
            {
                opt.Repositorys.Add(new KeyValueModel<Type, Type>(typeof(IRepository<>), typeof(SqlSugarRepository<>)));
                opt.Repositorys.Add(new KeyValueModel<Type, Type>(typeof(IRepository<,>), typeof(SqlSugarRepository<,>)));
            });

            //(2)组件安装初始配置
            EngineHelper.Component.List.ForEach(x => x.Install());

            //(3)访问处理
            EngineHelper.RegisterType<IAccessor, WpfAccessor>();
            EngineHelper.RegisterType<ISessionService, IdentifierSession>();
            EngineHelper.RegisterType<ITokenService, TokenService>();

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
