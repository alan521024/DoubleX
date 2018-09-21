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
    /// Preside.xaml 的交互逻辑
    /// </summary>
    public partial class Preside : UTHPage
    {
        public Preside()
        {
            InitializeComponent();
            Initialize();
        }

        PresideViewModel viewModel;
        Win.View.Main parentWin;

        private void Initialize()
        {
            viewModel = DataContext as PresideViewModel;
            viewModel.CheckNull();
            this.Loaded += Preside_Loaded;
        }

        private void Preside_Loaded(object sender, RoutedEventArgs e)
        {
            parentWin = WpfHelper.GetPrament<UTH.Meeting.Win.View.Main>(this);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            parentWin.mainFrame.Navigate(new Areas.Conference.View.Meeting());
        }
    }
}
