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
    using Newtonsoft.Json.Linq;
    using System.Windows.Input;
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
    /// 组员信息
    /// </summary>
    public class EmployeViewModel : UTHViewModel
    {
        public EmployeViewModel() : base(culture.Lang.userYongHuGuanLi, "")
        {
        }

        /// <summary>
        /// 员工列表
        /// </summary>
        public ObservableCollection<EmployeObservable> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        private ObservableCollection<EmployeObservable> _items = new ObservableCollection<EmployeObservable>();

        public ICommand OnDeleteCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    Delete(obj);
                });
            }
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="form"></param>
        /// <param name="size"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        public PagingModel<EmployeOutput> Query(int page, int size = 10, string name = null, int status = -1)
        {
            Items = new ObservableCollection<EmployeObservable>();
            var result = PlugCoreHelper.ApiUrl.User.EmployePaging.GetResult<PagingModel<EmployeOutput>, QueryInput>(new QueryInput()
            {
                Page = page,
                Size = size,
                Query = new JObject() {
                    new JProperty("organize",CurrentUser.User.Organize)
                }
            });
            if (result.Code == EnumCode.成功)
            {
                var _index = 1;
                Items = new ObservableCollection<EmployeObservable>();
                result.Obj.Rows.ForEach(item =>
                {
                    Items.Add(new EmployeObservable()
                    {
                        Index = _index,
                        Id = item.Id,
                        No = item.No,
                        Name = item.Name,
                        Status = item.Status,
                        IsSelected = false
                    });
                    _index++;
                });
                return result.Obj;
            }
            return null;
        }

        public void Delete(object obj)
        {
            Message("是否删除?", img: System.Windows.MessageBoxImage.Question, okAction: () =>
            {
                var input = new EmployeEditInput()
                {
                    Organize = CurrentUser.User.Organize,
                    Ids = Items.Where(x => x.IsSelected).Select(x => x.Id).ToList()
                };

                var result = "/api/user/employe/delete".GetResult<int, EmployeEditInput>(input);
                if (result.Code == EnumCode.成功)
                {
                    Message("成功", okAction: () =>
                    {
                        new AppViewModelLocator().EmployeModel.Query(1);
                    });
                }
                else
                {
                    throw new DbxException(EnumCode.提示消息, result.Message);
                }

                //new AppViewModelLocator().EmployeManageModel.Query(1);
            });

            //var input = new EmployeEditInput()
            //{
            //    Organize = CurrentUser.User.Organize,
            //    No = No,
            //    Name = Name,
            //    Password = Password
            //};

            //var result = "/api/user/employe/create".GetResult<EmployeOutput, EmployeEditInput>(input);
            //if (result.Code == EnumCode.成功)
            //{
            //    No = string.Empty;
            //    Name = string.Empty;
            //    Password = string.Empty;

            //    Message("成功", okAction: () =>
            //    {
            //        new AppViewModelLocator().EmployeManageModel.Query(1);
            //    });
            //    Close(obj.GetType().FullName);
            //}
            //else
            //{
            //    throw new DbxException(EnumCode.提示消息, result.Message);
            //}
        }
    }
}