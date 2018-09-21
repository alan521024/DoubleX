namespace UTH.Meeting.Win.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Timers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Ipc;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using Newtonsoft.Json.Linq;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using GalaSoft.MvvmLight.Messaging;
    using NAudio.Wave;
    using CommonServiceLocator;
    using culture = UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Framework.Wpf;
    using UTH.Domain;
    using UTH.Plug;
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;
    using UTH.Meeting.Server;

    /// <summary>
    /// 会议信息
    /// </summary>
    [Serializable]
    public class PresideViewModel : UTHViewModel
    {
        public PresideViewModel() : base(culture.Lang.metHuiYiShi, "")
        {
            Initialize();
        }

        #region 公共属性

        #endregion

        #region 私有变量

        #endregion

        #region 辅助操作

        private void Initialize()
        {
        }

        #endregion
    }
}