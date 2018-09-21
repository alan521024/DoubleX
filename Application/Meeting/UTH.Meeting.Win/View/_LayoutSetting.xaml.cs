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

namespace UTH.Meeting.Win.View
{
    /// <summary>
    /// _LayoutSetting.xaml 的交互逻辑
    /// </summary>
    public partial class _LayoutSetting : UTHWindow
    {
        _LayoutSettingViewModel viewModel;

        public _LayoutSetting()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            viewModel = DataContext as _LayoutSettingViewModel;
            viewModel.CheckNull();
            frame.Navigate(new Setting());
        }

        private void lvSettingNav_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (frame == null)
                return;

            var name = ((FrameworkElement)lvSettingNav.SelectedItem).Name;
            if (name == "setting")
            {
                frame.Navigate(new Setting());
            }
            if (name == "myself")
            {
                frame.Navigate(new MySelf());
            }
            if (name == "editpwd")
            {
                frame.Navigate(new EditPwd());
            }
        }
    }
}
