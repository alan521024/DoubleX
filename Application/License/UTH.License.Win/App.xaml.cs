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
using UTH.Infrastructure.Resource;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;
using UTH.License.Win.View;
using UTH.License.Win.ViewModel;

namespace UTH.License.Win
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
            string msgText = string.Empty;

            if (e.Exception.InnerException != null)
            {
                DbxException exception = e.Exception.InnerException as DbxException;
                if (exception != null)
                {
                    if (exception.Code == EnumCode.提示消息 || exception.Code == EnumCode.校验失败)
                    {
                        e.Handled = true;
                    }
                    WpfHelper.Error(exception.Message);
                    return;
                }
                else
                {
                    msgText = ExceptionHelper.GetMessage(e.Exception.InnerException);
                }
            }

            if (msgText.IsEmpty())
            {
                msgText = ExceptionHelper.GetMessage(e.Exception);
            }

            if (msgText.IsEmpty())
            {
                msgText = "未知错误";
            }

            WpfHelper.Error(string.Format("Error encountered! Please contact support. {0} {1}", Environment.NewLine, msgText), () => {
                Shutdown(1);
            });
        }
    }
}
