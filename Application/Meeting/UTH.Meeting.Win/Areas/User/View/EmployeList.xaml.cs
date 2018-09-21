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

namespace UTH.Meeting.Win.Areas.User.View
{
    /// <summary>
    /// EmployeList.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeList : UTHPage
    {
        EmployeViewModel viewModel;
        Win.View.Main parentWin;

        public EmployeList()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            viewModel = DataContext as EmployeViewModel;
            viewModel.CheckNull();
            pager.Configuration(1, 10);
        }

        private void pager_PagerChanged(object sender, RoutedEventArgs e)
        {
            var obj = e.Source as Pager2;
            var current = obj.IsNull() ? 1 : obj.PageIndex;
            var size = obj.IsNull() ? 10 : obj.PageSize;
            var model = viewModel?.Query(current, size);
            if (!model.IsNull())
            {
                pager.SetTotal((int)model.Total);
            }
        }
    }

}
