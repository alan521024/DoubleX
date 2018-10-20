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
using UTH.Meeting.Win.ViewModel;

namespace UTH.Meeting.Win.View
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : UTHPage
    {
        LoginViewModel viewModel;
        Window parent;

        public Login()
        {
            InitializeComponent();
            Initialize();
            this.Loaded += Login_Loaded;
        }

        private void Initialize()
        {
            viewModel = DataContext as LoginViewModel;
            viewModel.CheckNull();
            viewModel.Configuration(this);
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            parent = this.GetParent();
        }

        private void hlFindPwd_Click(object sender, RoutedEventArgs e)
        {
            parent.FindChild<Frame>("mainFrame").Navigate(new FindPwd());
        }

        private void hlRegist_Click(object sender, RoutedEventArgs e)
        {
            parent.FindChild<Frame>("mainFrame").Navigate(new Regist());
        }

    }
}
