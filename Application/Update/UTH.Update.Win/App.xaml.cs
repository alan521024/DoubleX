using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;

namespace UTH.Update.Win
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private IHosting appHosting { get; set; }

        public App()
        {
            DispatcherHelper.Initialize();
            appHosting = new AppHosting();
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //engine
            EngineHelper.Worker.Startup(appHosting);
            EngineHelper.Worker.OnStart();
            base.OnStartup(e);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (!e.IsNull() && !e.Exception.IsNull())
            {
                WpfHelper.SystemLog("UTH.UPDATE.Error", e.Exception.ToString());
            }

            WpfHelper.AppError(sender, e, () =>
            {
                if (!EngineHelper.Configuration.IsDebugger)
                {
                    Shutdown(0);
                }
            });
        }
    }
}
