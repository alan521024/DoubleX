namespace UTH.Meeting.Win.ViewModel
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
    using System.Windows.Input;
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
    using UTH.Meeting.Domain;
    using System.Windows;

    /// <summary>
    /// 人员编辑信息
    /// </summary>
    public class EmployeEditViewModel : UTHViewModel
    {
        public EmployeEditViewModel() : base(culture.Lang.userYongHuGuanLi, "")
        {
            IsSingle = Visibility.Visible;
            IsBatch = Visibility.Collapsed;
            BatchStart = 0;
            BatchEnd = 0;
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; RaisePropertyChanged(() => Code); }
        }
        private string _code;

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }
        private string _name;

        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(() => Password); }
        }
        private string _password;

        public int BatchStart
        {
            get { return _batchStart; }
            set { _batchStart = value; RaisePropertyChanged(() => BatchStart); }
        }
        private int _batchStart;

        public int BatchEnd
        {
            get { return _batchEnd; }
            set { _batchEnd = value; RaisePropertyChanged(() => BatchEnd); }
        }
        private int _batchEnd;

        public Visibility IsSingle
        {
            get { return _isSingle; }
            set { _isSingle = value; RaisePropertyChanged(() => IsSingle); }
        }
        private Visibility _isSingle;

        public Visibility IsBatch
        {
            get { return _isBatch; }
            set { _isBatch = value; RaisePropertyChanged(() => IsBatch); }
        }
        private Visibility _isBatch;


        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    Save(obj);
                });
            }
        }

        public void Save(object obj)
        {
            var input = new EmployeEditInput()
            {
                Organize = CurrentUser.User.Organize,
                Code = Code,
                Name = Name,
                Password = Password,
                BatchStart = BatchStart,
                BatchEnd = BatchEnd
            };

            if (input.Code.IsEmpty())
            {
                throw new DbxException(EnumCode.提示消息, culture.Lang.userQingShuRuZhangHuBianHao);
            }

            if (input.Password.IsEmpty())
            {
                throw new DbxException(EnumCode.提示消息, culture.Lang.userQingShuRuMiMa);
            }

            if (IsBatch == Visibility.Visible)
            {
                if (input.BatchStart <= 0 || input.BatchEnd <= 0 || input.BatchEnd < input.BatchStart)
                {
                    throw new DbxException(EnumCode.提示消息, culture.Lang.sysQingShuRuQiZhiXingXi);
                }
                var result = PlugCoreHelper.ApiUrl.User.EmployeBatchAdd.GetResult<List<EmployeDTO>, EmployeEditInput>(input);
                if (result.Code != EnumCode.成功)
                {
                    throw new DbxException(EnumCode.提示消息, result.Message);
                }
            }
            else
            {
                var result = PlugCoreHelper.ApiUrl.User.EmployeInsert.GetResult<EmployeDTO, EmployeEditInput>(input);
                if (result.Code != EnumCode.成功)
                {
                    throw new DbxException(EnumCode.提示消息, result.Message);
                }

            }

            Code = string.Empty;
            Name = string.Empty;
            Password = string.Empty;
            BatchStart = 0;
            BatchEnd = 0;

            MessageAlert("成功", okAction: () =>
            {
                new AppViewModelLocator().EmployeModel.Query(1);
            });
            Close(obj.GetType().FullName);
        }
    }
}