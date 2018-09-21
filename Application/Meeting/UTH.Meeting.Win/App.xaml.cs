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
        public App()
        {
            DispatcherHelper.Initialize();

            appHosting = new AppHosting();

            Exit += App_Exit;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            AppHelper.ServerClose();
        }

        private IHosting appHosting { get; set; }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            //init 
            AppHelper.ServerClose();

            //run
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            if (processes.Where(x => x.Id != current.Id).Count() > 0)
            {
                WpfHelper.Message(culture.Lang.sysChengXuYiJingYunXingQingWuChongFuCaoZuo);
                Environment.Exit(1);
            }
            
            //engine
            EngineHelper.Worker.Startup(appHosting);
            EngineHelper.Worker.OnStart();
            base.OnStartup(e);

            //server
            AppHelper.ServerStart(isClose: true);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (EngineHelper.Configuration.IsDebugger)
            {
                e.Handled = true;
            }

            DbxException exception = e.Exception as DbxException;
            if (exception.IsNull() && e.Exception.InnerException != null)
            {
                exception = e.Exception.InnerException as DbxException;
            }

            if (exception != null && (exception.Code == EnumCode.提示消息 || exception.Code == EnumCode.校验失败))
            {
                e.Handled = true;
                WpfHelper.Error(exception.Message);
                return;
            }

            if (exception != null && exception.Code == EnumCode.初始失败)
            {
                e.Handled = true;
                WpfHelper.Error("初始化错误，请检查设备/服务/会议信息并重启应用程序 ", () =>
                {
                    AppShutdown();
                });
                return;
            }

            string msgText = ExceptionHelper.GetMessage(e.Exception);
            if (e.Exception.InnerException != null)
            {
                msgText = ExceptionHelper.GetMessage(e.Exception.InnerException);
            }
            if (msgText.IsEmpty())
            {
                msgText = "未知错误";
            }
            EngineHelper.LoggingError(msgText);
            
            WpfHelper.Error(string.Format("System Error: {0} {1}", Environment.NewLine, msgText), () =>
            {
                AppShutdown();
            });
        }

        private void AppShutdown()
        {
            AppHelper.ServerClose();
            if (!EngineHelper.Configuration.IsDebugger)
            {
                Environment.Exit(0);
            }
        }
    }
}
