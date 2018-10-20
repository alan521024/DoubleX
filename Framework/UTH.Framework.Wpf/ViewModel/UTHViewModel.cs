namespace UTH.Framework.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;
    using System.Threading;
    using System.Drawing.Imaging;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Navigation;
    using Newtonsoft.Json.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using System.Windows.Input;

    /// <summary>
    /// ViewModel 基类
    /// </summary>
    public class UTHViewModel : ViewModelBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UTHViewModel(string title = null, string descript = null)
        {
            Title = title;
            Descript = descript;
        }

        /// <summary>
        /// 访问会话
        /// </summary>
        public IApplicationSession Session { get { return CurrentUser; } }

        /// <summary>
        /// 访问会话
        /// </summary>
        public IApplicationSession CurrentUser
        {
            get
            {
                return EngineHelper.Resolve<IApplicationSession>();
            }
        }

        /// <summary>
        /// 从属对象
        /// </summary>
        public DependencyObject DependencyObj { get; protected set; }

        /// <summary>
        /// 设置视图Model
        /// </summary>
        /// <param name="obj"></param>
        public void Configuration(DependencyObject obj = null)
        {
            DependencyObj = obj;
        }

        #region 窗体控件

        /// <summary>
        /// 窗体标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(() => Title); }
        }
        private string _title = "";

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Descript
        {
            get { return _descript; }
            set { _descript = value; RaisePropertyChanged(() => Descript); }
        }
        private string _descript = "";

        /// <summary>
        /// 关闭当前
        /// </summary>
        /// <param name="name"></param>
        protected void Close(string name)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                Messenger.Default.Send<object>(this, $"{name}_CLOSE");
            });
        }

        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="title"></param>
        /// <param name="img"></param>
        /// <param name="owner"></param>
        /// <param name="okAction"></param>
        /// <param name="cancelAction"></param>
        protected void MessageAlert(string txt, string title = null, MessageBoxImage img = MessageBoxImage.None, Window owner = null, Action okAction = null, Action cancelAction = null)
        {
            switch (img)
            {
                case MessageBoxImage.Information:
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        WpfHelper.Success(txt, title, okAction, owner);
                    });
                    break;
                case MessageBoxImage.Error:
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        WpfHelper.Error(txt, title, okAction, owner);
                    });
                    break;
                case MessageBoxImage.Question:
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        WpfHelper.Confirm(txt, title, okAction, cancelAction, owner);
                    });
                    break;
                case MessageBoxImage.None:
                default:
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        WpfHelper.Message(txt, title, okAction, owner);
                    });
                    break;
            }
        }

        #endregion

        #region 遮罩操作(Mask)

        /// <summary>
        /// 是否显示遮罩
        /// </summary>
        public Visibility IsMask
        {
            get { return _isMask; }
            set { _isMask = value; RaisePropertyChanged(() => IsMask); }
        }
        private Visibility _isMask = Visibility.Collapsed;

        /// <summary>
        /// 遮罩层提示
        /// </summary>
        public string MaskTip
        {
            get { return _maskTip; }
            set
            {
                _maskTip = value;
                RaisePropertyChanged(() => MaskTip);
            }
        }
        private string _maskTip;

        /// <summary>
        /// 是示Mask
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="dialy"></param>
        public void MaskShow(string tip, int dialy = 0)
        {
            if (IsMask != Visibility.Visible)
            {
                IsMask = Visibility.Visible;
            }
            MaskTip = tip;
        }

        /// <summary>
        /// 隐藏Mask
        /// </summary>
        public void MaskHide()
        {
            if (IsMask != Visibility.Collapsed)
            {
                IsMask = Visibility.Collapsed;
            }
            MaskTip = "";
        }

        #endregion
    }
}
