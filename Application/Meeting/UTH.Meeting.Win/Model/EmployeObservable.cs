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
        /// 选中
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
        /// 编号(账号序号)  
        /// </summary>
        public string No
        {
            get { return _no; }
            set
            {
                _no = value;
                RaisePropertyChanged(() => No);
            }
        }
        private string _no;

        /// <summary>
        /// 员工编号
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged(() => Code);
            }
        }
        private string _code;

        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return _account; }
            set
            {
                _account = value;
                RaisePropertyChanged(() => Account);
            }
        }
        private string _account;

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                _mobile = value;
                RaisePropertyChanged(() => Mobile);
            }
        }
        private string _mobile;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }
        private string _email;

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
        
        /// <summary>
        /// 状态
        /// </summary>
        public EnumAccountStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }
        private EnumAccountStatus _status;



    }
}
