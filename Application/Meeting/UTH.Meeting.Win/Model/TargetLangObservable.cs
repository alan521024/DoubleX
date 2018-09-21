namespace UTH.Meeting.Win
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using Newtonsoft.Json.Linq;
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
    using UTH.Plug.Multimedia;
    using UTH.Meeting.Domain;

    public class TargetLangObservable : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }
        private string _text;

        /// <summary>
        /// 语言
        /// </summary>
        public string Lang
        {
            get { return _lang; }
            set
            {
                _lang = value;
                RaisePropertyChanged(() => Lang);
            }
        }
        private string _lang;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }
        private bool _isSelected;

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable
        {
            get { return _isEnalbe; }
            set
            {
                _isEnalbe = value;
                RaisePropertyChanged(() => IsEnable);
            }
        }
        private bool _isEnalbe;
    }
}