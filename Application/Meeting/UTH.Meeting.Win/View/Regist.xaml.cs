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
    /// Regist.xaml 的交互逻辑
    /// </summary>
    public partial class Regist : Page
    {
        RegistViewModel viewModel;

        public Regist()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            viewModel = DataContext as RegistViewModel;
            viewModel.CheckNull();
        }

        private void hlRegAgreement_Click(object sender, RoutedEventArgs e)
        {
            RegAgreement regAgreementForm = new RegAgreement();
            regAgreementForm.ShowDialog();
        }

        private void hlLogin_Click(object sender, RoutedEventArgs e)
        {
            WpfHelper.GetParent<Window>(this).FindChild<Frame>("mainFrame")
                .Navigate(new Login());
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var msg = viewModel.SendCaptchaCode();
            if (!msg.IsEmpty())
            {
                WpfHelper.Message(msg);
            }
            else
            {
                WpfHelper.Message(culture.Lang.sysFaSongChengGong);
            }

            new Thread(() =>
            {
                ThreadHelper.Countdown((p) =>
                {
                    WpfHelper.ExcuteUI(() =>
                    {
                        var pro = Math.Abs((p / 1000) - 15);
                        if (pro < 1)
                        {
                            viewModel.CanSend = true;
                            viewModel.SendText = culture.Lang.sysHuoQuYanZhengMa;
                        }
                        else
                        {
                            viewModel.CanSend = false;
                            viewModel.SendText = string.Format("{0}({1})", culture.Lang.sysDaoJiShi, pro);
                        }
                    });
                }, 15000);
            }).Start();
        }

        private void btnRegist_Click(object sender, RoutedEventArgs e)
        {
            var msg = viewModel.Regist();
            if (!msg.IsEmpty())
            {
                WpfHelper.Message(msg);
                return;
            }
            WpfHelper.Message(culture.Lang.userZhuCeChengGongXiaoXi, () =>
            {
                WpfHelper.GetParent<Window>(this).FindChild<Frame>("mainFrame")
                    .Navigate(new Login());
            });
        }
    }
}
