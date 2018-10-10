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
using System.Collections.ObjectModel;
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
        public EmployeList()
        {
            InitializeComponent();
            Initialize();
            this.Loaded += EmployeList_Loaded;
        }

        EmployeViewModel viewModel;
        Win.View.Main parent;

        private void Initialize()
        {
            viewModel = DataContext as EmployeViewModel;
            viewModel.CheckNull();

            pager.Configuration(1, 10);
            crumbs1.Items = AppHelper.GetMainNavigationCrumbs(new CrumbData() { Text = "用户管理", IsText = true, Split = "" });
        }

        private void EmployeList_Loaded(object sender, RoutedEventArgs e)
        {
            parent = this.GetParent<Win.View.Main>();
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

        private void crumbs1_ItemSelect(object sender, RoutedEventArgs e)
        {
            var orgSource = e.OriginalSource as Hyperlink;
            orgSource.CheckNull();

            var data = orgSource.DataContext as CrumbData;
            data.CheckNull();

            AppHelper.MainNavigationCrumbsAction(parent, data);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeEdit edit = new EmployeEdit();
            edit.Owner = parent;
            edit.ShowDialog();
        }
    }
}
