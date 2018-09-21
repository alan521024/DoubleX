using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
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

namespace UTH.Meeting.Win.View
{
    /// <summary>
    /// RegAgreement.xaml 的交互逻辑
    /// </summary>
    public partial class RegAgreement : UTHWindow
    {
        RegAgreementViewModel viewModel;

        public RegAgreement()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            viewModel = DataContext as RegAgreementViewModel;
            viewModel.CheckNull();
            this.Loaded += (object sender, RoutedEventArgs e) =>
            {
                WpfHelper.ExcuteUI(StartTask);
            };
        }

        private void StartTask()
        {
            string path = string.Format(@"{0}\Assets\Document\reg-agreement.html", AppDomain.CurrentDomain.BaseDirectory);
            wbContent.NavigateToString(File.ReadAllText(path));
        }
    }
}
