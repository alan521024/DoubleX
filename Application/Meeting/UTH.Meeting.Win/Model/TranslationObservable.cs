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

    public class TranslationObservable : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// 数据Id
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }
        private Guid _id;
        
        /// <summary>
        /// 会议Id
        /// </summary>
        public Guid MeetingId
        {
            get { return _meetingId; }
            set
            {
                _meetingId = value;
                RaisePropertyChanged(() => MeetingId);
            }
        }
        private Guid _meetingId;

        /// <summary>
        /// 记录ID
        /// </summary>
        public Guid RecordId
        {
            get { return _recordId; }
            set
            {
                _recordId = value;
                RaisePropertyChanged(() => RecordId);
            }
        }
        private Guid _recordId;

        /// <summary>
        /// 语言
        /// </summary>
        public string Langue
        {
            get { return _langue; }
            set
            {
                _langue = value;
                RaisePropertyChanged(() => Langue);
            }
        }
        private string _langue;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged(() => Content);
            }
        }
        private string _content;
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort
        {
            get { return _sort; }
            set
            {
                _sort = value;
                RaisePropertyChanged(() => Sort);
            }
        }
        private int _sort;

        /// <summary>
        /// 标识前缀
        /// </summary>
        public string Tag { get { return string.Format("译({0})", Langue); } }

    }
}
