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
using UTH.Meeting.Domain;
using UTH.Meeting.Win.Areas.User.View;
using UTH.Meeting.Win.Areas.Conference.View;
using UTH.Meeting.Win.ViewModel;
namespace UTH.Meeting.Win.View
{
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : UTHWindow
    {
        public Main()
        {
            InitializeComponent();
            Initialize();
            this.Closing += Main_Closing;
        }

        MainViewModel viewModel;

        public Guid MeetingId { get; set; }

        private void Initialize()
        {
            viewModel = DataContext as MainViewModel;
            viewModel.CheckNull();
            this.ShowMeeting();
        }

        public void ShowMeeting()
        {
            if (viewModel.CurrentUser.User.Type == EnumAccountType.组织)
            {
                mainFrame.Navigate(new Areas.Conference.View.Preside());
            }
            if (viewModel.CurrentUser.User.Type == EnumAccountType.人员)
            {
                mainFrame.Navigate(new Areas.Conference.View.Participant());
            }
        }

        private void btnEmploye_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Areas.User.View.EmployeList());
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                WpfHelper.ExcuteUI(() =>
                {
                    viewModel.MaskShow("正在导出....");
                });

                viewModel.Export(MeetingId);

                WpfHelper.ExcuteUI(() =>
                {
                    viewModel.MaskHide();
                    System.Diagnostics.Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(MeetingHelper.GetMeetingTextFile(MeetingId)));
                });
            });
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            _LayoutSetting form = new _LayoutSetting();
            form.Owner = this;
            form.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            _LayoutAccount form = new _LayoutAccount();
            form.Show();
            this.Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Help form = new Help();
            form.ShowDialog();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About form = new About();
            form.ShowDialog();
        }

        private void hlUpdate_Click(object sender, RoutedEventArgs e)
        {
            WpfHelper.AppUpdate(VersionHelper.Get(), AppHelper.Current);
        }

        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseAction?.Invoke(sender, e);
        }
    }
}
