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

    /// <summary>
    /// ViewModel 基类
    /// </summary>
    public class UTHViewModel : ViewModelBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UTHViewModel() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public UTHViewModel(string title, string descript)
        {
            Title = title;
            Descript = descript;
        }

        #region 公共属性

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

        #endregion

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
    }
}
