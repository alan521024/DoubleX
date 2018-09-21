using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

namespace UTH.Update.Win
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
            ToMain();
        }

        private void ToMain()
        {
            Main main = new Main();
            main.Show();
            this.Close();
        }
    }
}
