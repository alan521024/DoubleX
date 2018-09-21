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

namespace UTH.Meeting.Win
{
    /// <summary>
    /// Startup.xaml 的交互逻辑
    /// </summary>
    public partial class Startup : UTHWindow
    {
        StartupViewModel viewModel;

        public Startup()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            viewModel = DataContext as StartupViewModel;
            viewModel.CheckNull();

            new Thread(() => { StartTask(); }).Start();
        }

        private void StartTask()
        {
            WpfHelper.ExcuteUI(() =>
            {
                try
                {
                    viewModel.CheckLicense();
                    viewModel.CheckVersion();
                    viewModel.UpdateUse();
                    this.ToMain();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

        }

        private void ToMain()
        {
            if (viewModel.ProgressValue < 100)
            {
                throw new DbxException(EnumCode.初始失败);
            }
            _LayoutAccount form = new _LayoutAccount();
            form.Show();
            this.Close();
        }
    }
}
