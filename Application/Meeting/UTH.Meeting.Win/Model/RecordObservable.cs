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

    /// <summary>
    /// 会议记录信息
    /// </summary>
    public class RecordObservable : ViewModelBase, INotifyPropertyChanged
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
        /// 本地Id
        /// </summary>
        public Guid LocalId
        {
            get { return _localId; }
            set
            {
                _localId = value;
                RaisePropertyChanged(() => LocalId);
            }
        }
        private Guid _localId;

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
        /// 翻译语言以'|'分割
        /// </summary>
        public string LangueTrs
        {
            get { return _langueTrs; }
            set
            {
                _langueTrs = value;
                RaisePropertyChanged(() => LangueTrs);
            }
        }
        private string _langueTrs;

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
        /// 描述
        /// </summary>
        public string Des
        {
            get { return _des; }
            set
            {
                _des = value;
                RaisePropertyChanged(() => Des);
            }
        }
        private string _des;

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
        /// 同步
        /// </summary>
        public int SyncType
        {
            get { return _syncType; }
            set
            {
                _syncType = value;
                RaisePropertyChanged(() => SyncType);
            }
        }
        private int _syncType;

        /// <summary>
        /// 当前段、语是否结束
        /// </summary>
        public bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                _isComplete = value;
                RaisePropertyChanged(() => IsComplete);
            }
        }
        private bool _isComplete;

        /// <summary>
        /// 刷新时间(拼接为句子)
        /// </summary>
        public DateTime RefreshDt
        {
            get { return _refreshDt; }
            set
            {
                _refreshDt = value;
                RaisePropertyChanged(() => RefreshDt);
            }
        }
        private DateTime _refreshDt;

        /// <summary>
        /// 标识前缀
        /// </summary>
        public string Tag { get { return string.Format("源({0})", Langue); } }

        /// <summary>
        /// 翻译列表
        /// </summary>
        public ObservableCollection<TranslationObservable> Translations
        {
            get { return _translations; }
            set
            {
                _translations = value;
                RaisePropertyChanged(() => Translations);
            }
        }
        private ObservableCollection<TranslationObservable> _translations = new ObservableCollection<TranslationObservable>();

    }
}
