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

namespace UTH.Meeting.Win.Areas.Conference.View
{
    /// <summary>
    /// Meeting.xaml 的交互逻辑
    /// </summary>
    public partial class Meeting : UTHPage
    {
        public Meeting(string code = null, MeetingOutput meeting = null)
        {
            InitializeComponent();
            Initialize(code, meeting);
        }

        MeetingViewModel viewModel;
        Win.View.Main parentWin;

        private void Initialize(string code = null, MeetingOutput meeting = null)
        {
            viewModel = DataContext as MeetingViewModel;
            viewModel.CheckNull();
            viewModel.Loading(code, meeting);
            this.Loaded += Preside_Loaded;
        }

        private void Preside_Loaded(object sender, RoutedEventArgs e)
        {
            parentWin = WpfHelper.GetPrament<UTH.Meeting.Win.View.Main>(this);
            if (!parentWin.IsNull())
            {
                parentWin.MeetingId = viewModel.Meeting.Id;
                parentWin.CloseAction = (obj, closeEvent) =>
                {
                    viewModel.Cancel();
                };
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

            viewModel.Stop();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Clear();
        }
    }
}
