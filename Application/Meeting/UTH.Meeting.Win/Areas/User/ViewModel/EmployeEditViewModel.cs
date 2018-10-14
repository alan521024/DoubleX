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

    /// <summary>
    /// 人员编辑信息
    /// </summary>
    public class EmployeEditViewModel : UTHViewModel
    {
        public EmployeEditViewModel() : base(culture.Lang.userYongHuGuanLi, "")
        {
        }

        public string No
        {
            get { return _no; }
            set { _no = value; RaisePropertyChanged(() => No); }
        }
        private string _no;

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
                No = No,
                Name = Name,
                Password = Password
            };

            var result = "/api/user/employe/create".GetResult<EmployeOutput, EmployeEditInput>(input);
            if (result.Code == EnumCode.成功)
            {
                No = string.Empty;
                Name = string.Empty;
                Password = string.Empty;

                Message("成功", okAction: () =>
                {
                    new AppViewModelLocator().EmployeModel.Query(1);
                });
                Close(obj.GetType().FullName);
            }
            else
            {
                throw new DbxException(EnumCode.提示消息, result.Message);
            }
        }
    }
}