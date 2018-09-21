using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;
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
using UTH.Update.Win.View;
using UTH.Update.Win.ViewModel;

namespace UTH.Update.Win.View
{
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : UTHWindow
    {
        MainViewModel viewModel;

        public Main()
        {
            InitializeComponent();
            Initialize();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
        }

        private void Initialize()
        {
            viewModel = DataContext as MainViewModel;
            viewModel.CheckNull();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //杀掉进程
            if (AppHelper.ExecuteAppProcess > 0)
            {
                ProcessHelper.Kill(AppHelper.ExecuteAppProcess);
            }

            //执行更新
            viewModel.UpdateApp(AppHelper.Current.Versions, (version, filePath) =>
            {
                var runFile = viewModel.UpdateFile(version, filePath);
                if (!runFile.IsEmpty())
                {
                    //启动文件
                    ProcessHelper.Start(runFile, args: "", style: ProcessWindowStyle.Normal);

                    //关闭更新应用程序
                    WpfHelper.ExcuteUI(() =>
                    {
                        Thread.Sleep(1500);
                        this.Close();
                    });
                }
                else
                {
                    throw new DbxException(EnumCode.提示消息, "更新错误");
                }
            });
        }
    }
}
