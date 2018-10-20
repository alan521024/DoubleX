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
            No = "";
            Status = EnumAccountStatus.Default;
        }

        /// <summary>
        /// 查询编号
        /// </summary>
        public string No
        {
            get { return _no; }
            set { _no = value; RaisePropertyChanged(() => No); }
        }
        private string _no;

        /// <summary>
        /// 查询状态
        /// </summary>
        public EnumAccountStatus Status
        {
            get { return _status; }
            set { _status = value; RaisePropertyChanged(() => Status); }
        }
        private EnumAccountStatus _status;

        /// <summary>
        /// 分页控件
        /// </summary>
        public Pager2 Pager { get; set; }

        /// <summary>
        /// 员工数据
        /// </summary>
        public ObservableCollection<EmployeObservable> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged(() => Source);
            }
        }
        private ObservableCollection<EmployeObservable> _source = new ObservableCollection<EmployeObservable>();

        /// <summary>
        /// 查询事件 
        /// </summary>
        public ICommand OnSearchCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    Query();
                });
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
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
        public PagingModel<EmployeDTO> Query(int page = 0, int size = 0, string no = null, int status = -1)
        {
            Source = new ObservableCollection<EmployeObservable>();

            PagingModel<EmployeDTO> model = new PagingModel<EmployeDTO>()
            {
                Total = 0,
                Rows = new List<EmployeDTO>()
            };

            if (page == 0)
            {
                page = Pager.PageIndex;
            }

            if (size == 0)
            {
                size = Pager.PageSize;
            }

            var result = PlugCoreHelper.ApiUrl.User.EmployePaging.GetResult<PagingModel<EmployeDTO>, QueryInput<EmployeDTO>>(new QueryInput<EmployeDTO>()
            {
                Page = page,
                Size = size,
                Query = new EmployeDTO()
                {
                    Organize = CurrentUser.User.Organize,
                    No = No
                }
            });

            if (result.Code == EnumCode.成功)
            {
                model = result.Obj;
                var _index = 1;
                result.Obj.Rows.ForEach(item =>
                {
                    Source.Add(new EmployeObservable()
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
            }

            Pager?.Sync((int)model.Total);

            return model;
        }

        /// <summary>
        /// 员工删除
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(object obj)
        {
            MessageAlert("是否删除?", img: System.Windows.MessageBoxImage.Question, okAction: () =>
            {
                var input = new EmployeEditInput()
                {
                    Organize = CurrentUser.User.Organize,
                    Ids = Source.Where(x => x.IsSelected).Select(x => x.Id).ToList()
                };

                var result = "/api/user/employe/delete".GetResult<int, EmployeEditInput>(input);
                if (result.Code == EnumCode.成功)
                {
                    MessageAlert("成功", okAction: () =>
                    {
                        Query(1, size: Pager.PageSize);
                    });
                }
                else
                {
                    throw new DbxException(EnumCode.提示消息, result.Message);
                }
            });
        }
    }
}