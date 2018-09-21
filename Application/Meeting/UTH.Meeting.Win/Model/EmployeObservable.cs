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
    /// 员工信息
    /// </summary>
    public class EmployeObservable : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged(() => Index);
            }
        }
        private int _index;

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
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        private string _name;
    }
}
