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

namespace UTH.Framework.Wpf
{
    /// <summary>
    /// MahApps窗体基类
    /// </summary>
    public class UTHWindow : MetroWindow, IFormBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UTHWindow()
        {
            var type = this.GetType();

            #region Event Regist & Uninstall

            Messenger.Default.Register<object>(this, $"{type.FullName}_CLOSE", (obj) =>
            {
                this.Close();
            });

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);

            #endregion
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        public Action<object, System.ComponentModel.CancelEventArgs> CloseAction { get; set; }
    }

}
