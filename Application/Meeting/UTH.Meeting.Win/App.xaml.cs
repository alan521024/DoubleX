using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
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
using UTH.Meeting.Win.View;
using UTH.Meeting.Win.ViewModel;
using System.Diagnostics;

namespace UTH.Meeting.Win
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

            DispatcherUnhandledException += App_DispatcherUnhandledException;

            Exit += App_Exit;

            appHosting = new AppHosting();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //only one
            WpfHelper.AppIsOnlyRun();

            //engine
            EngineHelper.Worker.Startup(appHosting);
            EngineHelper.Worker.OnStart();
            base.OnStartup(e);

            //server
            var process = WpfHelper.AppServerStart(AppHelper.ServerName, AppHelper.ServerPath, AppHelper.ServerObj, isOnly: true);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            WpfHelper.AppError(sender, e, () =>
            {
                WpfHelper.AppServerClose(AppHelper.ServerName);
                if (!EngineHelper.Configuration.IsDebugger)
                {
                    Environment.Exit(0);
                }
            });
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            WpfHelper.AppServerClose(AppHelper.ServerName);
        }
    }
}
