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
    /// EmployeEdit.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeEdit : UTHWindow
    {
        public EmployeEdit(bool isBatch = false)
        {
            InitializeComponent();
            Initialize(isBatch);
            this.Loaded += EmployeEdit_Loaded;
        }

        EmployeEditViewModel viewModel;
        Win.View.Main parent;

        private void Initialize(bool isBatch = false)
        {
            viewModel = DataContext as EmployeEditViewModel;
            viewModel.CheckNull();
            viewModel.MaskHide();

            viewModel.Code = "";
            viewModel.Name = "";
            viewModel.Password = "";
            viewModel.BatchStart = 0;
            viewModel.BatchEnd = 0;
            viewModel.IsSingle = Visibility.Visible;
            viewModel.IsBatch = Visibility.Collapsed;

            if (isBatch)
            {
                viewModel.IsBatch = Visibility.Visible;
                viewModel.IsSingle = Visibility.Collapsed;
                viewModel.BatchStart = 1;
                viewModel.BatchEnd = 5;
            }
            else
            {
                viewModel.IsBatch = Visibility.Collapsed;
                viewModel.IsSingle = Visibility.Visible;
                viewModel.BatchStart = 0;
                viewModel.BatchEnd = 0;
            }
        }

        private void EmployeEdit_Loaded(object sender, RoutedEventArgs e)
        {
            parent = this.GetParent<Win.View.Main>();
        }
    }
}
